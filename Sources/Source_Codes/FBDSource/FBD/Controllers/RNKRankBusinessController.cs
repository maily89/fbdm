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
                foreach (CustomersBusinessRanking item in model)
                {
                    item.BusinessScalesReference.Load();
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
            TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_RANKING);
            
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
                return View("AddScore", CustomersBusinessScale.ScaleList(id));
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


        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddFinancialScore(int id)
        {
            try
            {
                List<RNKFinancialRow> financial = CustomersBusinessFinancialIndex.LoadFinancialIndex(id, true);
                return View(financial);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        

        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="rnkFinancialRow"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFinancialScore(List<RNKFinancialRow> rnkFinancialRow,int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                int rankID=id;
                foreach (RNKFinancialRow item in rnkFinancialRow)
                {
                    if (item.LeafIndex)
                    {
                        CustomersBusinessFinancialIndex indexScore = new CustomersBusinessFinancialIndex();
                        indexScore = CustomersBusinessFinancialIndex.ConvertFinancialRowToModel(entities, item, indexScore);

                        entities.AddToCustomersBusinessFinancialIndex(indexScore);
                    }
                }
                entities.SaveChanges();
                RNKFinancialMarking.CalculateFinancialScore(id, true, entities);
                if (TempData["FinancialRedirect"] != null)
                    return RedirectToAction("Edit", new { id = rankID });
                return RedirectToAction("AddNonFinancialIndex",new{id=rankID});
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                CustomersBusinessFinancialIndex.Reload(rnkFinancialRow);

                if (TempData["FinancialRedirect"] != null)
                    TempData["FinancialRedirect"] = "Edit";
                return View(rnkFinancialRow);
            }
        }

        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddNonFinancialScore(int id)
        {
            try
            {
                List<RNKNonFinancialRow> nonFinancial = CustomersBusinessNonFinancialIndex.LoadNonFinancialIndex(id, true);

                return View(nonFinancial);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }



        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="rnkNonFinancialRow"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNonFinancialScore(List<RNKNonFinancialRow> rnkNonFinancialRow, int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                int rankID = id;
                foreach (RNKNonFinancialRow item in rnkNonFinancialRow)
                {
                    if (item.LeafIndex)
                    {
                        CustomersBusinessNonFinancialIndex indexScore = new CustomersBusinessNonFinancialIndex();
                        indexScore = CustomersBusinessNonFinancialIndex.ConvertNonFinancialRowToModel(entities, item, indexScore);

                        entities.AddToCustomersBusinessNonFinancialIndex(indexScore);
                    }
                }
                entities.SaveChanges();
                RNKNonFinancialMarking.CalculateNonFinancialScore(id, true, entities);
                if (TempData["NonFinancialRedirect"] != null)
                    return RedirectToAction("Edit", new { id = rankID });
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                CustomersBusinessNonFinancialIndex.Reload(rnkNonFinancialRow);
                if (TempData["NonFinancialRedirect"] != null)
                    TempData["NonFinancialRedirect"] = "Edit";//Rewrite temp data
                return View(rnkNonFinancialRow);
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

            ranking.BusinessIndustriesReference.Load();
            if(ranking.BusinessIndustries!=null)
            addModel.IndustryID = ranking.BusinessIndustries.IndustryID;

            ranking.BusinessTypesReference.Load();
            if(ranking.BusinessTypes!=null)
            addModel.TypeID = ranking.BusinessTypes.TypeID;

            ranking.CustomersLoanTermReference.Load();
            if (ranking.CustomersLoanTerm != null)
                addModel.LoanID = ranking.CustomersLoanTerm.LoanTermID;

            ranking.SystemCustomerTypesReference.Load();
            if (ranking.SystemCustomerTypes != null)
                addModel.CustomerTypeID = ranking.SystemCustomerTypes.TypeID;

            ranking.CustomersBusinessesReference.Load();
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
                return View("AddScore", CustomersBusinessScale.ScaleList(id));
            }
            TempData["EditMode"] = "EditMode";

            return View("AddScore", CustomersBusinessScale.ScaleEditList(id));
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
                    var temp = CustomersBusinessScale.SelectBusinessScaleByID(item.CustomerScaleID,entity);
                    rankID = item.RankingID;
                    temp.Score = item.Score;
                    temp.Value = item.Value;

                    temp.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(item.RankingID, entity);
                    temp.BusinessScaleCriteria = BusinessScaleCriteria.SelectScaleCriteriaByID(item.CriteriaID, entity);

                    CustomersBusinessScale.EditBusinessScale(temp, entity);
                }
                // Mark Scale ScoreList.
                if (rankID > 0) RNKScaleMarking.SaveScaleScore(rankID, entity);

                return RedirectToAction("Edit", new { id = rankID });
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
            //Check if there's no saved data=> return
            List<RNKFinancialRow> financial=null;
            if (CustomersBusinessFinancialIndex.SelectFinancialIndexByRankingID(id).Count <= 0)
            {
                financial = CustomersBusinessFinancialIndex.LoadFinancialIndex(id, true);
                TempData["FinancialRedirect"] = "Edit";
                return View("AddFinancialScore",financial);
            }

            //Change to edit
            TempData["EditMode"] = "EditMode";

            financial = CustomersBusinessFinancialIndex.LoadFinancialIndex(id, false);
            return View("AddFinancialScore", financial);
        }
        [HttpPost]
        public ActionResult EditFinancialScore(List<RNKFinancialRow> rnkFinancialRow,int id)
        {
            try
            {
                FBDEntities entity = new FBDEntities();
                int rankID = id;

                //Load FinancialRow and save them.
                foreach (RNKFinancialRow item in rnkFinancialRow)
                {
                    if (item.LeafIndex)
                    {
                        CustomersBusinessFinancialIndex indexScore = CustomersBusinessFinancialIndex.SelectFinancialIndexByID(item.CustomerFinancialID,entity);

                        CustomersBusinessFinancialIndex.ConvertFinancialRowToModel(entity, item, indexScore);

                        CustomersBusinessFinancialIndex.EditFinancialIndex(indexScore, entity);
                    }

                }

                RNKFinancialMarking.CalculateFinancialScore(id, false, entity);

                return RedirectToAction("Edit", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                TempData["EditMode"] = "EditMode";
                CustomersBusinessFinancialIndex.Reload(rnkFinancialRow);
                return View("AddFinancialScore", rnkFinancialRow);
            }
        }

        public ActionResult EditNonFinancialScore(int id)
        {
            //Check if there's no saved data=> return
            List<RNKNonFinancialRow> nonFinancial = null;
            if (CustomersBusinessNonFinancialIndex.SelectNonFinancialIndexByRankingID(id).Count <= 0)
            {
                nonFinancial = CustomersBusinessNonFinancialIndex.LoadNonFinancialIndex(id, true);
                TempData["NonFinancialRedirect"] = "Edit";
                return View("AddNonFinancialScore", nonFinancial);
            }

            //Change to edit
            TempData["EditMode"] = "EditMode";

            nonFinancial = CustomersBusinessNonFinancialIndex.LoadNonFinancialIndex(id, false);
            return View("AddNonFinancialScore", nonFinancial);
        }
        [HttpPost]
        public ActionResult EditNonFinancialScore(List<RNKNonFinancialRow> rnkNonFinancialRow, int id)
        {
            try
            {
                FBDEntities entity = new FBDEntities();
                int rankID = id;

                //Load NonFinancialRow and save them.
                foreach (RNKNonFinancialRow item in rnkNonFinancialRow)
                {
                    if (item.LeafIndex)
                    {
                        CustomersBusinessNonFinancialIndex indexScore = CustomersBusinessNonFinancialIndex.SelectNonFinancialIndexByID(item.CustomerNonFinancialID, entity);

                        CustomersBusinessNonFinancialIndex.ConvertNonFinancialRowToModel(entity, item, indexScore);

                        CustomersBusinessNonFinancialIndex.EditNonFinancialIndex(indexScore, entity);
                    }

                }

                RNKNonFinancialMarking.CalculateNonFinancialScore(id, false, entity);

                return RedirectToAction("Edit", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                TempData["EditMode"] = "EditMode";
                CustomersBusinessNonFinancialIndex.Reload(rnkNonFinancialRow);
                return View("AddNonFinancialScore", rnkNonFinancialRow);
            }
        }
        #endregion

        #region Private
        
        
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
