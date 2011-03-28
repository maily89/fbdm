using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
using FBD.CommonUtilities;
namespace FBD.Controllers
{
    //TODO: Paging for customers
    public class RNKRankBusinessController : Controller
    {
        //
        // GET: /RKNRankBusiness/

        public ActionResult Index()
        {
            List<CustomersBusinessRanking> model = null;
            try
            {
                model = CustomersBusinessRanking.SelectBusinessRankings();
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_BUSINESS_RANKING);

            }
            return View(model);
        }

        #region Add
        //
        // GET: /RKNRankBusiness/Create

        public ActionResult AddStep1()
        {
            //TODO: check branch
            var model = new RNKPeriodViewModel();
            try
            {
                model.ReportingPeriods = SystemReportingPeriods.SelectReportingPeriods();
                model.BusinessCustomer = CustomersBusinesses.SelectBusinesses();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_REPORTING_PERIOD);
            }
            return View(model);
        }

        //
        // Post: /RKNRankBusiness/Add
        [HttpPost]
        public ActionResult AddStep1(RNKPeriodViewModel data)
        {
            if (data == null) return null;
            var model = new RNKPeriodViewModel();

            if (data.CustomerID>0 && !string.IsNullOrEmpty(data.PeriodID))
            {
                var ranking = CustomersBusinessRanking.SelectRankingByPeriodAndCustomer(data.PeriodID, data.CustomerID);
                RNKBusinessRankingViewModel addModel = new RNKBusinessRankingViewModel();
                if (ranking == null) // if this is new customer
                {
                    ranking = new CustomersBusinessRanking();
                    
                    addModel.IsNew = true;
                }
                else return View("Edit",ranking.ID); // if this is not a new customer ranking
                addModel.BusinessRanking = ranking;
                ranking.DateModified = DateTime.Now;
                addModel.PeriodID = data.PeriodID;
                addModel.CustomerID = data.CustomerID;
                var customer = CustomersBusinesses.SelectBusinessByID(data.CustomerID);
                addModel.CIF = customer.CIF;
                return View("Add",addModel);
            }

            //else: ERROR
            model.ReportingPeriods = SystemReportingPeriods.SelectReportingPeriods();
            model.BusinessCustomer = CustomersBusinesses.SelectBusinesses();
            TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_REPORTING_PERIOD);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(RNKBusinessRankingViewModel rknBusinessRankingViewModel)
        {
            int id;
            if (rknBusinessRankingViewModel == null) return null;
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var ranking = rknBusinessRankingViewModel.BusinessRanking;



                    ranking.CustomersBusinesses = CustomersBusinesses.SelectBusinessByID(rknBusinessRankingViewModel.CustomerID, entity);
                    ranking.BusinessIndustries = BusinessIndustries.SelectIndustryByID(rknBusinessRankingViewModel.IndustryID, entity);
                    ranking.BusinessTypes = BusinessTypes.SelectTypeByID(rknBusinessRankingViewModel.TypeID, entity);
                    ranking.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(rknBusinessRankingViewModel.LoanID, entity);
                    ranking.SystemReportingPeriods = SystemReportingPeriods.SelectReportingPeriodByID(rknBusinessRankingViewModel.PeriodID, entity);
                    //line.BusinessIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessIndustries", "IndustryID", data.IndustryID);
                    if (rknBusinessRankingViewModel.IsNew)
                        CustomersBusinessRanking.AddBusinessRanking(ranking, entity);
                    else CustomersBusinessRanking.EditBusinessRanking(ranking, entity);
                    id = ranking.ID;
                }
                else throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_BUSINESS_RANKING);
                return View("AddScore", ScaleList(id));
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_RANKING);

                return View(rknBusinessRankingViewModel);
            }
        }

        [HttpPost]
        public ActionResult AddScore(List<RNKScaleRow> rknScaleRow)
        {
            try
            {
                FBDEntities entity = new FBDEntities();
                int rankID = 0;

                foreach (RNKScaleRow item in rknScaleRow)
                {
                    var temp = new CustomersBusinessScale();
                    rankID = item.RankingID;
                    temp.Score = item.Score;
                    temp.Value = item.Value;

                    temp.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(item.RankingID, entity);
                    temp.BusinessScaleCriteria = BusinessScaleCriteria.SelectScaleCriteriaByID(item.CriteriaID, entity);

                    CustomersBusinessScale.AddBusinessScale(temp, entity);
                }
                // Mark Scale ScoreList.
                if (rankID > 0) RNKScaleMarking.SaveScaleScore(rankID, entity);

                return RedirectToAction("AddFinancialScore", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_SCALE);
                return View(rknScaleRow);
            }
        }



        public ActionResult AddFinancialScore(int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
                ranking.BusinessIndustriesReference.Load();

                if (ranking.BusinessScales == null)
                    RNKScaleMarking.SaveScaleScore(id, entities);
                ranking.BusinessScalesReference.Load();
                string scale = ranking.BusinessScales.ScaleID;

                string IndustryID = ranking.BusinessIndustries.IndustryID;

                List<BusinessFinancialIndex> indexList = BusinessFinancialIndex.SelectFinancialIndex(entities);

                List<RNKFinancialRow> financial = new List<RNKFinancialRow>();
                foreach (BusinessFinancialIndex item in indexList)
                {
                    var temp = new RNKFinancialRow();
                    temp.RankingID = id;
                    temp.Index = item;
                    item.BusinessFinancialIndexScore.Load();
                    temp.ScoreList = BusinessFinancialIndexScore.SelectScoreByIndustryByScaleByFinancialIndex(entities, IndustryID, scale, item.IndexID);

                    financial.Add(temp);
                }
                return View(financial);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult AddFinancialScore(List<RNKFinancialRow> rnkFinancialRow)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                int rankID=0;
                foreach (RNKFinancialRow item in rnkFinancialRow)
                {
                    rankID=item.RankingID;
                    CustomersBusinessFinancialIndex indexScore = new CustomersBusinessFinancialIndex();

                    indexScore.BusinessFinancialIndex = BusinessFinancialIndex.SelectFinancialIndexByID(entities,item.Index.IndexID);

                    indexScore.Value = item.Score.ToString();
                    indexScore.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(rankID, entities);

                    if (item.ScoreID >0)
                    {
                        BusinessFinancialIndexScore score = BusinessFinancialIndexScore.SelectBusinessFinancialIndexScoreByScoreID(entities, item.ScoreID);
                        score.BusinessFinancialIndexLevelsReference.Load();
                        indexScore.Value = score.FixedValue;
                        indexScore.BusinessFinancialIndexLevels = score.BusinessFinancialIndexLevels;
                    }

                    entities.AddToCustomersBusinessFinancialIndex(indexScore);
                }
                return RedirectToAction("AddNonFinancialIndex",new{id=rankID});
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                return View(rnkFinancialRow);
            }
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            return View(id);
        }

        public ActionResult EditInfo(int id)
        {
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
            RNKBusinessRankingViewModel addModel = new RNKBusinessRankingViewModel();

            ranking.DateModified = DateTime.Now;

            ranking.SystemReportingPeriodsReference.Load();
            addModel.PeriodID = ranking.SystemReportingPeriods.PeriodID;
            
            var customer = ranking.CustomersBusinesses;
            addModel.CustomerID = customer.BusinessID;
            addModel.CIF = customer.CIF;
            TempData["EditMode"] = "EditMode";
            return View("Add", addModel);

        }

        [HttpPost]
        public ActionResult EditInfo(RNKBusinessRankingViewModel rknBusinessRankingViewModel,int id)
        {
            int rankingID = id;
            if (rknBusinessRankingViewModel == null) return null;
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var ranking = rknBusinessRankingViewModel.BusinessRanking;

                    ranking.CustomersBusinesses = CustomersBusinesses.SelectBusinessByID(rknBusinessRankingViewModel.CustomerID, entity);
                    ranking.BusinessIndustries = BusinessIndustries.SelectIndustryByID(rknBusinessRankingViewModel.IndustryID, entity);
                    ranking.BusinessTypes = BusinessTypes.SelectTypeByID(rknBusinessRankingViewModel.TypeID, entity);
                    ranking.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(rknBusinessRankingViewModel.LoanID, entity);
                    ranking.SystemReportingPeriods = SystemReportingPeriods.SelectReportingPeriodByID(rknBusinessRankingViewModel.PeriodID, entity);

                    CustomersBusinessRanking.EditBusinessRanking(ranking, entity);
                    rankingID = ranking.ID;
                }
                else throw new Exception();

                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.CUSTOMER_BUSINESS_RANKING);
                return RedirectToAction("Edit", new { id = rankingID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_RANKING);

                return View(rknBusinessRankingViewModel);
            }
        }

        public ActionResult EditScale(int id)
        {
            if (CustomersBusinessScale.SelectBusinessScaleByRankingID(id).Count <= 0)
            {
                return View("AddScore", ScaleList(id));
            }
            TempData["EditMode"] = "EditMode";

            return View("AddScore", ScaleEditList(id));
        }

        [HttpPost]
        public ActionResult EditScale(List<RNKScaleRow> rnkScaleRow,int id)
        {
            try
            {
                FBDEntities entity = new FBDEntities();
                int rankID = id;

                
                foreach (RNKScaleRow item in rnkScaleRow)
                {
                    var temp = CustomersBusinessScale.SelectBusinessScaleByID(item.CustomerScaleID);
                    rankID = item.RankingID;
                    temp.Score = item.Score;
                    temp.Value = item.Value;

                    temp.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(item.RankingID, entity);
                    temp.BusinessScaleCriteria = BusinessScaleCriteria.SelectScaleCriteriaByID(item.CriteriaID, entity);

                    CustomersBusinessScale.EditBusinessScale(temp, entity);
                }
                // Mark Scale ScoreList.
                if (rankID > 0) RNKScaleMarking.SaveScaleScore(rankID, entity);

                return RedirectToAction("AddFinancialScore", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_SCALE);
                TempData["EditMode"] = "EditMode";
                return View("AddScore",rnkScaleRow);
            }
        }

        
        public ActionResult EditFinancialScore(int id)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Private
        private List<RNKScaleRow> ScaleList(int id)
        {
            var ranking=CustomersBusinessRanking.SelectBusinessRankingByID(id);

            var scaleCriteria = BusinessScaleCriteria.SelectScaleCriteria();
            List<RNKScaleRow> scale = new List<RNKScaleRow>();
            foreach (BusinessScaleCriteria item in scaleCriteria)
            {
                var temp = new RNKScaleRow();
                temp.CriteriaID = item.CriteriaID;
                temp.RankingID = id;
                temp.CriteriaName = item.CriteriaName;
                scale.Add(temp);
            }
            return scale;
        }

        private List<RNKScaleRow> ScaleEditList(int id)
        {
            var entities=new FBDEntities();
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);

            var customerScale = CustomersBusinessScale.SelectBusinessScaleByRankingID(id);
            
            List<RNKScaleRow> scale = new List<RNKScaleRow>();
            foreach (CustomersBusinessScale item in customerScale)
            {
                var temp = new RNKScaleRow();
               
                item.BusinessScaleCriteriaReference.Load();
                temp.CriteriaID = item.BusinessScaleCriteria.CriteriaID;
                temp.RankingID = id;
                temp.CriteriaName = item.BusinessScaleCriteria.CriteriaName;
                temp.Value = item.Value;
                temp.CustomerScaleID = item.ID;
                scale.Add(temp);
            }

            return scale;
        }

        //private List<RNKFinancialRow> FinancialRow(int rankingID)
        //{
        //    var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID);

        //    var index = BusinessFinancialIndex.SelectFinancialIndex(new FBDEntities());

        //    List<RNKFinancialRow> financial = new List<RNKFinancialRow>();
        //    foreach (BusinessFinancialIndex item in index)
        //    {
        //        var temp = new RNKFinancialRow();
        //        temp.IndexID = item.IndexID;
        //        temp.IndexName = item.IndexName;
        //        if (item.ValueType=="C")
        //        {
        //            temp.Levels=BusinessFinancialIndexLevels.SelectFinancialIndexLevels(
        //        }
        //    }
        //}
        
        //
        // GET: /RNKRank/Delete/5
        /// <summary>
        /// Delete rank
        /// </summary>
        /// <param name="rankingID"></param>
        /// <returns></returns>
        #endregion
        public ActionResult Delete(int id)
        {
            try
            {
                int result = CustomersBusinessRanking.DeleteBusinessRanking(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.CUSTOMER_BUSINESS_RANKING);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.CUSTOMER_BUSINESS_RANKING);
                return RedirectToAction("Index");
            }

        }
    }
}
