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
    public class RNKRankBusinessController : Controller
    {
        //
        // GET: /RKNRankBusiness/
        // TODO: check user ID
        public ActionResult Index()
        {
            List<CustomersBusinessRanking> customerRankList = null;
            try
            {
                //load customer ranking list
                customerRankList = CustomersBusinessRanking.SelectBusinessRankings();
                if (customerRankList == null)
                {
                    throw new Exception();
                }
                foreach (CustomersBusinessRanking item in customerRankList)
                {
                    item.BusinessScalesReference.Load();
                    item.CustomersBusinessesReference.Load();
                    item.BusinessRanksReference.Load();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_BUSINESS_RANKING);

            }
            RNKBusinessIndex index = new RNKBusinessIndex();
            index.CustomerRanking = customerRankList;
            return View(index);
           
        }

        [HttpPost]
        public ActionResult Index(RNKBusinessIndex data)
        {
            List<CustomersBusinessRanking> model = null;
            try
            {
                
                model = CustomersBusinessRanking.SelectRankingByPeriodAndCifAndBranch(data.PeriodID,data.Cif,data.BranchID);
                if (model == null)
                {
                    throw new Exception();
                }
                foreach (CustomersBusinessRanking item in model)
                {
                    item.BusinessScalesReference.Load();
                    item.CustomersBusinessesReference.Load();
                    item.BusinessRanksReference.Load();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_BUSINESS_RANKING);

            }
            data.CustomerRanking = model;
            return View(data);
        }

        #region Add
        
        /// <summary>
        /// this method display choose customer view
        /// this method do not save any data to database yet!
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This methods get data from choose customer view,and return view for adding customer detailed info
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddStep1(RNKPeriodViewModel data)
        {
            if (data == null) return null;
            var model = new RNKPeriodViewModel();
            try
            {
                if (data.CustomerID > 0 && !string.IsNullOrEmpty(data.PeriodID))
                {
                    //adding ranking object to model
                    var ranking = CustomersBusinessRanking.SelectRankingByPeriodAndCustomer(data.PeriodID, data.CustomerID);
                    RNKBusinessRankingViewModel addModel = new RNKBusinessRankingViewModel();
                    if (ranking == null) // if this is new customer
                    {
                        ranking = new CustomersBusinessRanking();

                        addModel.IsNew = true;
                    }
                    else return RedirectToAction("DetailGeneral", new { id = ranking.ID });
                    addModel.BusinessRanking = ranking;
                    ranking.DateModified = DateTime.Now;
                    addModel.PeriodID = data.PeriodID;
                    addModel.CustomerID = data.CustomerID;

                    //loading customer
                    var customer = CustomersBusinesses.SelectBusinessByID(data.CustomerID);
                    customer.SystemBranchesReference.Load();
                    addModel.CIF = customer.CIF;

                    //adding customer info
                    RNKCustomerInfo temp = new RNKCustomerInfo();
                    temp.CIF = customer.CIF;
                    temp.CustomerName = customer.CustomerName;
                    temp.ReportingPeriod = SystemReportingPeriods.SelectReportingPeriodByID(data.PeriodID).PeriodName;

                    if (customer.SystemBranches != null)
                    temp.Branch = customer.SystemBranches.BranchName;
                    addModel.CustomerInfo = temp;
                    return View("Add", addModel);
                }
                throw new Exception();
            }
            catch
            {
                //else: ERROR
                model.ReportingPeriods = SystemReportingPeriods.SelectReportingPeriods();
                model.BusinessCustomer = CustomersBusinesses.SelectBusinesses();
              
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_RANKING);

                return View(model);
            }
        }
        /// <summary>
        /// this method is used to add detail customer ranking info
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(RNKBusinessRankingViewModel data)
        {
            int id;
            if (data == null) return null;
            try
            {
                if (ModelState.IsValid)
                {

                    var entity = new FBDEntities();
                    var ranking = data.BusinessRanking;
                    
                    ranking.CustomersBusinesses = CustomersBusinesses.SelectBusinessByID(data.CustomerID, entity);
                    if(data.IndustryID!=null)
                    ranking.BusinessIndustries = BusinessIndustries.SelectIndustryByID(data.IndustryID, entity);

                    if(data.TypeID!=null)
                    ranking.BusinessTypes = BusinessTypes.SelectTypeByID(data.TypeID, entity);

                    if (data.LoanID!=null)
                    ranking.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(data.LoanID, entity);

                    ranking.SystemReportingPeriods = SystemReportingPeriods.SelectReportingPeriodByID(data.PeriodID, entity);

                    if(data.CustomerTypeID!=null)
                    ranking.SystemCustomerTypes = SystemCustomerTypes.SelectTypeByID(data.CustomerTypeID, entity);
                    //line.BusinessIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessIndustries", "IndustryID", data.IndustryID);
                    if (data.IsNew)
                        CustomersBusinessRanking.AddBusinessRanking(ranking, entity);
                    else CustomersBusinessRanking.EditBusinessRanking(ranking, entity);

                    
                    id = ranking.ID;
                    ViewData["RankID"] = id;
                }
                else throw new Exception();

                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_BUSINESS_RANKING);
                return View("AddScore", CustomersBusinessScale.LoadScaleRow(id));
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_RANKING);
                
                RNKCustomerInfo temp = new RNKCustomerInfo();
                var customer = CustomersBusinesses.SelectBusinessByID(data.CustomerID);
                temp.CIF = customer.CIF;
                temp.CustomerName = customer.CustomerName;

                if(data.PeriodID!=null)
                temp.ReportingPeriod = SystemReportingPeriods.SelectReportingPeriodByID(data.PeriodID).PeriodName;

                customer.SystemBranchesReference.Load();
                if(customer.SystemBranches!=null)
                temp.Branch = customer.SystemBranches.BranchName;
                data.CustomerInfo = temp;
                if(data.BusinessRanking!=null)
                ViewData["RankID"] = data.BusinessRanking.ID;
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult AddScore(List<RNKScaleRow> rknScaleRow,int rankID)
        {
            ViewData["RankID"] = rankID;
            return View(rknScaleRow);
        }

        [HttpPost]
        public ActionResult SaveScore(List<RNKScaleRow> rknScaleRow,string SaveBack,string SaveNext,string Back,string rankID)
        {
            ViewData["RankID"] = rankID;
            if (Back != null) return View("AddScore", rknScaleRow);

            try
            {
                FBDEntities entity = new FBDEntities();
                int rankingID = 0;

                foreach (RNKScaleRow item in rknScaleRow)
                {
                    var temp = new CustomersBusinessScale();
                    rankingID = item.RankingID;
                    temp.Score = item.Score;
                    temp.Value = item.Value;

                    temp.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(item.RankingID, entity);
                    temp.BusinessScaleCriteria = BusinessScaleCriteria.SelectScaleCriteriaByID(item.CriteriaID, entity);

                    CustomersBusinessScale.AddBusinessScale(temp, entity);
                }
                // Mark Scale ScoreList.
                if (rankingID > 0) RNKScaleMarking.SaveScaleScore(rankingID, entity);
                if(SaveNext!=null)
                    return RedirectToAction("AddFinancialScore", new { id = rankID!=null?rankID:rankingID.ToString() });
                return RedirectToAction("Index");
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_SCALE);
                return View("AddScore",rknScaleRow);
            }
        }
        [HttpPost]
        public ActionResult AddScoreCalculate(List<RNKScaleRow> rknScaleRow,string rankID)
        {
            ViewData["RankID"] = rankID;
            if (rknScaleRow==null || rknScaleRow.Count<=0) return View(rknScaleRow);

            GenerateScoreCalculate(rknScaleRow);
            return View(rknScaleRow);
        }


        private void GenerateScoreCalculate(List<RNKScaleRow> rknScaleRow)
        {
            var rankingID = rknScaleRow[0].RankingID;
            CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID);
            ranking.BusinessIndustriesReference.Load();
            if (ranking.BusinessIndustries == null) return;
            string industryID = ranking.BusinessIndustries.IndustryID;
            // Load row and all of their information available
            decimal sum = 0;
            BusinessScales scale = RNKScaleMarking.ScaleTempMarking(rankingID, industryID, rknScaleRow, out sum);
            // Marking them.
            ViewData["ScaleScore"] = sum;

            if (scale != null)
            ViewData["Scale"] = scale.Scale;
        }


        
        public ActionResult Ranking(int id)
        {
            var model = new RNKRankFinal();
            try
            {
                ViewData["RankID"] = id.ToString();
                RNKRankMarking.SaveBusinessRank(id);
                LoadRankingViewModel(id, model);

                //TODO: Cluster Rank handle

            }
            catch
            {
                //TODO: Add to constants
                TempData[Constants.ERR_MESSAGE] = "There's error when loading ranking for business customer";
            }
            return View(model);

        }

        private static void LoadRankingViewModel(int id, RNKRankFinal model)
        {
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
            ranking.BusinessScalesReference.Load();
            ranking.BusinessRanksReference.Load();
            ranking.CustomersBusinessScale.Load();
            ranking.BusinessIndustriesReference.Load();


            if (ranking.BusinessScales != null)
                model.Scale = ranking.BusinessScales.Scale;

            decimal temp;
            if (ranking.BusinessIndustries != null)
            {
                RNKScaleMarking.ScaleMarking(id, ranking.BusinessIndustries.IndustryID, new FBDEntities(), ranking.CustomersBusinessScale.ToList(), out temp);

                model.ScaleScore = temp;
            }

            model.FinancialResult = ranking.FinancialScore != null ? ranking.FinancialScore.Value : 0;
            model.NonFinancialResult = ranking.NonFinancialScore != null ? ranking.NonFinancialScore.Value : 0;


            try
            {
                model.FinancialProportion = BusinessRankingStructure.SelectRankingStructureByIndexAndAudit(Constants.RNK_STRUCTURE_FINANCIAL_INDEX, ranking.AuditedStatus).Percentage.Value;
                model.FinancialScore = model.FinancialResult / model.FinancialProportion * 100;
            }
            catch
            {
                model.FinancialProportion = 0;
                model.FinancialScore = 0;
            }

            try
            {
                model.NonFinancialProportion = BusinessRankingStructure.SelectRankingStructureByIndexAndAudit(Constants.RNK_STRUCTURE_NONFINANCIAL_INDEX, ranking.AuditedStatus).Percentage.Value;
                model.NonFinancialScore = model.NonFinancialResult / model.NonFinancialProportion * 100;
            }
            catch
            {
                model.NonFinancialProportion = 0;
                model.NonFinancialScore = 0;
            }

            model.TotalScore = model.FinancialResult + model.NonFinancialResult;
            if (ranking.BusinessRanks != null)
                model.ClassRank = ranking.BusinessRanks.Rank;
        }
        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddFinancialScore(int id,string Edit)
        {
            
            try
            {
                ViewData["RankID"] = id;
                if (!string.IsNullOrEmpty(Edit)) ViewData["Edit"] = true;
                List<RNKFinancialRow> financial = CustomersBusinessFinancialIndex.LoadFinancialIndex(id,string.IsNullOrEmpty(Edit)? true:false);
                return View(financial);
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(string.IsNullOrEmpty(Edit) ? Constants.ERR_ADD_POST : Constants.ERR_EDIT_POST, Constants.INV_BASIC_INDEX);
                if(!string.IsNullOrEmpty(Edit)) return RedirectToAction("DetailFinancial",new{id=id});
                return RedirectToAction("Index");
            }
        }

        

        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="rnkFinancialRow"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveFinancialScore(List<RNKFinancialRow> rnkFinancialRow,string Edit, string SaveBack, string SaveNext, string Back,string SaveRerank, int rankID)
        {

            try
            {
                ViewData["RankID"] = rankID.ToString();
                if(!string.IsNullOrEmpty(Edit)){
                    ViewData["Edit"]=Edit;
                }
                if (Back != null && string.IsNullOrEmpty(Edit)) return View("AddFinancialScore", CustomersBusinessFinancialIndex.Reload(rnkFinancialRow));

                if(string.IsNullOrEmpty(Edit))
                {
                    int ranking = AddFinancialList(rnkFinancialRow, rankID);
                }
                else
                {
                    EditFinancialList(rnkFinancialRow, rankID);
                }

                if(SaveNext!=null)
                return RedirectToAction("AddNonFinancialScore",new{id=rankID});

                if(SaveRerank!=null)
                    return RedirectToAction("Rerank",new{id=rankID,redirectAction="DetailFinancial"});
                if(!string.IsNullOrEmpty(Edit))
                {
                    return RedirectToAction("DetailFinancial",new {id=rankID});
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                CustomersBusinessFinancialIndex.Reload(rnkFinancialRow);                
                return View("AddFinancialScore",rnkFinancialRow);
            }
        }

        private static int AddFinancialList(List<RNKFinancialRow> rnkFinancialRow, int rankID)
        {
                        FBDEntities entities = new FBDEntities();
                        int ranking = rankID;

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
                        RNKFinancialMarking.CalculateFinancialScore(rankID , true, entities);
        return ranking;
        }

        [HttpPost]
        public ActionResult AddFinancialCalculate(List<RNKFinancialRow> rnkFinancialRow,string Edit, string rankID)
        {
            //return viewdata state
            ViewData["RankID"] = rankID;
            if(!string.IsNullOrEmpty(Edit)) ViewData["Edit"]=Edit;
            if (rnkFinancialRow == null || rnkFinancialRow.Count <= 0) return View(rnkFinancialRow);

            var rankingID = System.Convert.ToInt16(rankID);
            CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID);

            decimal final;
            decimal prop;
            RNKFinancialMarking.CalculateTempFinancial(rankingID, rnkFinancialRow,out final,out prop);

            ViewData["FinancialScore"] = final;
            ViewData["FinancialProportion"] = prop;
            ViewData["FinancialResult"] = final * prop / 100;
            return View(rnkFinancialRow);
        }
        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddNonFinancialScore(int id,string Edit)
        {
            try
            {
                if(!string.IsNullOrEmpty(Edit)){
                ViewData["Edit"] = Edit;
                }
                ViewData["RankID"] = id.ToString();
                List<RNKNonFinancialRow> nonFinancial = CustomersBusinessNonFinancialIndex.LoadNonFinancialIndex(id,string.IsNullOrEmpty(Edit)? true:false );

                return View(nonFinancial);
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(Edit)) return RedirectToAction("DetailNonFinancial", new { id = id });

                return RedirectToAction("Index");
            }
        }



        /// <summary>
        /// Add Score
        /// </summary>
        /// <param name="rnkNonFinancialRow"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveNonFinancialScore(List<RNKNonFinancialRow> rnkNonFinancialRow, string SaveBack, string SaveNext, string Back,string SaveRerank, int rankID,string Edit)
        {
             
            try
            {
                ViewData["RankID"] = rankID.ToString();
                if (!string.IsNullOrEmpty(Edit))
                {
                    ViewData["Edit"] = Edit;
                }
                if (Back != null && string.IsNullOrEmpty(Edit)) return View("AddNonFinancialScore", CustomersBusinessNonFinancialIndex.Reload(rnkNonFinancialRow));

                if (string.IsNullOrEmpty(Edit))
                {
                    AddNonFinancialList(rnkNonFinancialRow, rankID);
                }
                else
                {
                    EditNonFinancialList(rnkNonFinancialRow, rankID);
                }
                if(SaveBack!=null)
                return RedirectToAction("Index");

                if (SaveRerank != null)
                    return RedirectToAction("Rerank", new { id = rankID, redirectAction = "DetailNonFinancial" });
                if (!string.IsNullOrEmpty(Edit))
                {
                    return RedirectToAction("DetailNonFinancial", new { id = rankID });
                }
                else
                return RedirectToAction("Ranking", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                CustomersBusinessNonFinancialIndex.Reload(rnkNonFinancialRow);
                ViewData["RankID"] = rankID.ToString();
                return View(rnkNonFinancialRow);
            }
        }

        private static void AddNonFinancialList(List<RNKNonFinancialRow> rnkNonFinancialRow, int rankID)
        {
            FBDEntities entities = new FBDEntities();

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
            RNKNonFinancialMarking.CalculateNonFinancialScore(rankID, true, entities);
        }

        [HttpPost]
        public ActionResult AddNonFinancialCalculate(List<RNKNonFinancialRow> rnkNonFinancialRow, string rankID,string Edit)
        {
            ViewData["RankID"] = rankID;
            if (!string.IsNullOrEmpty(Edit)) ViewData["Edit"] = Edit;

            if (rnkNonFinancialRow == null || rnkNonFinancialRow.Count <= 0) return View(rnkNonFinancialRow);

            var rankingID = rnkNonFinancialRow[0].RankingID;
            CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID);

            decimal final;
            decimal prop;
            RNKNonFinancialMarking.CalculateTempNonFinancial(rankingID, rnkNonFinancialRow, out final, out prop);

            ViewData["NonFinancialScore"] = final;
            ViewData["NonFinancialProportion"] = prop;
            ViewData["NonFinancialResult"] = final * prop / 100;
            return View(rnkNonFinancialRow);
        }

        #endregion

        #region Edit


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

            addModel.CustomerInfo = RNKCustomerInfo.GetBusinessRankingInfo(ranking.ID);
            return View(addModel);

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
                    ranking.SystemCustomerTypes = SystemCustomerTypes.SelectTypeByID(rknBusinessRankingViewModel.CustomerTypeID, entity);

                    CustomersBusinessRanking.EditBusinessRanking(ranking, entity);
                    rankingID = ranking.ID;
                }
                else throw new Exception();

                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.CUSTOMER_BUSINESS_RANKING);
                return RedirectToAction("DetailGeneral", new { id = rankingID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_RANKING);
                rknBusinessRankingViewModel.CustomerInfo = RNKCustomerInfo.GetBusinessRankingInfo(rknBusinessRankingViewModel.BusinessRanking.ID);
                return View(rknBusinessRankingViewModel);
            }
        }

        public ActionResult EditScore(int id)
        {
            ViewData["RankID"] = id.ToString();
            return View(CustomersBusinessScale.LoadAndAddScaleRow(id));
        }

        [HttpPost]
        public ActionResult EditScoreCalculate(List<RNKScaleRow> rknScaleRow, string rankID)
        {
            ViewData["RankID"] = rankID;
            if (rknScaleRow == null || rknScaleRow.Count <= 0) return View(rknScaleRow);

            GenerateScoreCalculate(rknScaleRow);
            return View(rknScaleRow);
        }


        [HttpPost]
        public ActionResult EditScoreSave(List<RNKScaleRow> rnkScaleRow,string SaveBack,string SaveRerank,string Back,int id)
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
                if (Back != null) return View("EditScore", rnkScaleRow);

                if (SaveRerank != null)
                    return RedirectToAction("Rerank", new { id = rankID, redirectAction = "DetailScale" });

                return RedirectToAction("DetailScale", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_SCALE);

                return View(rnkScaleRow);
            }
        }

        
        //public ActionResult EditFinancialScore(int id)
        //{
        //    //Check if there's no saved data=> return
        //    List<RNKFinancialRow> financial=null;
        //    if (CustomersBusinessFinancialIndex.SelectFinancialIndexByRankingID(id).Count <= 0)
        //    {
        //        financial = CustomersBusinessFinancialIndex.LoadFinancialIndex(id, true);
        //        TempData["FinancialRedirect"] = "Edit";
        //        return View("AddFinancialScore",financial);
        //    }

        //    //Change to edit
        //    TempData["EditMode"] = "EditMode";

        //    financial = CustomersBusinessFinancialIndex.LoadFinancialIndex(id, false);
        //    return View("AddFinancialScore", financial);
        //}
        //[HttpPost]
        //public ActionResult EditFinancialScore(List<RNKFinancialRow> rnkFinancialRow,int id)
        //{
        //    try
        //    {
        //        int rankID = EditFinancialList(rnkFinancialRow, id);

        //        return RedirectToAction("Edit", new { id = rankID });
        //    }
        //    catch (Exception)
        //    {
        //        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
        //        TempData["EditMode"] = "EditMode";
        //        CustomersBusinessFinancialIndex.Reload(rnkFinancialRow);
        //        return View("AddFinancialScore", rnkFinancialRow);
        //    }
        //}

        private static int EditFinancialList(List<RNKFinancialRow> rnkFinancialRow, int id)
        {
                        FBDEntities entity = new FBDEntities();
                        int rankID = id;

                        //Load FinancialRow and save them.
                        foreach (RNKFinancialRow item in rnkFinancialRow)
                        {
                            if (item.LeafIndex)
                            {
                                CustomersBusinessFinancialIndex indexScore = CustomersBusinessFinancialIndex.SelectFinancialIndexByID(item.CustomerScoreID,entity);

                                CustomersBusinessFinancialIndex.ConvertFinancialRowToModel(entity, item, indexScore);

                                CustomersBusinessFinancialIndex.EditFinancialIndex(indexScore, entity);
                            }

                        }

                        RNKFinancialMarking.CalculateFinancialScore(id, false, entity);
        return rankID;
        }

        //public ActionResult EditNonFinancialScore(int id)
        //{
        //    //Check if there's no saved data=> return
        //    List<RNKNonFinancialRow> nonFinancial = null;
        //    if (CustomersBusinessNonFinancialIndex.SelectNonFinancialIndexByRankingID(id).Count <= 0)
        //    {
        //        nonFinancial = CustomersBusinessNonFinancialIndex.LoadNonFinancialIndex(id, true);
        //        TempData["NonFinancialRedirect"] = "Edit";
        //        return View("AddNonFinancialScore", nonFinancial);
        //    }

        //    //Change to edit
        //    TempData["EditMode"] = "EditMode";

        //    nonFinancial = CustomersBusinessNonFinancialIndex.LoadNonFinancialIndex(id, false);
        //    return View("AddNonFinancialScore", nonFinancial);
        //}
        //[HttpPost]
        //public ActionResult EditNonFinancialScore(List<RNKNonFinancialRow> rnkNonFinancialRow, int id)
        //{
        //    try
        //    {
        //        int rankID = EditNonFinancialList(rnkNonFinancialRow, id);

        //        return RedirectToAction("Edit", new { id = rankID });
        //    }
        //    catch (Exception)
        //    {
        //        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
        //        TempData["EditMode"] = "EditMode";
        //        CustomersBusinessNonFinancialIndex.Reload(rnkNonFinancialRow);
        //        return View("AddNonFinancialScore", rnkNonFinancialRow);
        //    }
        //}

        private static int EditNonFinancialList(List<RNKNonFinancialRow> rnkNonFinancialRow, int id)
        {
            FBDEntities entity = new FBDEntities();
            int rankID = id;

            //Load NonFinancialRow and save them.
            foreach (RNKNonFinancialRow item in rnkNonFinancialRow)
            {
                if (item.LeafIndex)
                {
                    CustomersBusinessNonFinancialIndex indexScore = CustomersBusinessNonFinancialIndex.SelectNonFinancialIndexByID(item.CustomerScoreID, entity);

                    CustomersBusinessNonFinancialIndex.ConvertNonFinancialRowToModel(entity, item, indexScore);

                    CustomersBusinessNonFinancialIndex.EditNonFinancialIndex(indexScore, entity);
                }

            }

            RNKNonFinancialMarking.CalculateNonFinancialScore(id, false, entity);
            return rankID;
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

        #region ViewDetail
        public ActionResult DetailGeneral(int id)
        {
            try
            {
                ViewData["RankID"] = id.ToString();
                var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
                ranking.LoadAll();

                RNKBusinessRankingViewModel model = new RNKBusinessRankingViewModel();
                model.BusinessRanking = ranking;
                model.CustomerInfo = RNKCustomerInfo.GetBusinessRankingInfo(id);
                return View(model);
            }
            catch
            {
                //TODO: add error text to constant
                TempData[Constants.ERR_MESSAGE] = "Error occured when loading the ranked customers. Please try again later";
                return RedirectToAction("Index");
            }
        }
        public ActionResult DetailScale(int id)
        {
            try
            {
                TempData["DetailView"] = "EditMode";
                ViewData["RankID"] = id.ToString();

                var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
                ranking.BusinessScalesReference.Load();
                ranking.CustomersBusinessScale.Load();
                ranking.BusinessIndustriesReference.Load();


                if (ranking.BusinessScales != null)
                    ViewData["Scale"] = ranking.BusinessScales.Scale;

                decimal temp;
                if (ranking.BusinessIndustries != null)
                {
                    RNKScaleMarking.ScaleMarking(id, ranking.BusinessIndustries.IndustryID, new FBDEntities(), ranking.CustomersBusinessScale.ToList(), out temp);

                    ViewData["ScaleScore"] = temp;
                }
            }
            catch
            {
                //TODO: ERRor message here
            }
            return View(CustomersBusinessScale.LoadAndAddScaleRow(id));
        
        }
        public ActionResult DetailFinancial(int id)
        {
            ViewData["RankID"] = id.ToString();
            try
            {
                List<RNKFinancialRow> financial = CustomersBusinessFinancialIndex.LoadFinancialIndex(id,false);
                var ranking=CustomersBusinessRanking.SelectBusinessRankingByID(id);
                var tempScore=ranking.FinancialScore;
                ViewData["FinancialResult"] = tempScore;
                try
                {
                    ViewData["FinancialProportion"] = BusinessRankingStructure.SelectRankingStructureByIndexAndAudit(Constants.RNK_STRUCTURE_FINANCIAL_INDEX, ranking.AuditedStatus).Percentage.Value;
                }
                catch
                {
                    ViewData["FinancialProportion"] = "0";
                }
                try
                {
                    var prop = System.Convert.ToDecimal(ViewData["FinancialProportion"]);
                    if (prop != 0 && tempScore != null)
                    {
                        ViewData["FinancialScore"] = tempScore / prop * 100;
                    }
                }
                catch
                {
                    ViewData["FinancialScore"] = "0";
                }
                ViewData["DetailView"] = "true";
                return View(financial);
            }
            catch (Exception)
            {
                //TODO: ERROR MESSAGE
                ViewData["DetailView"] = true;
                return RedirectToAction("DetailGeneral");
            }
        }

        public ActionResult DetailNonFinancial(int id)
        {
            ViewData["RankID"] = id.ToString();
            try
            {
                List<RNKNonFinancialRow> NonFinancial = CustomersBusinessNonFinancialIndex.LoadNonFinancialIndex(id, false);
                var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
                var tempScore = ranking.NonFinancialScore;
                ViewData["NonFinancialResult"] = tempScore;
                try
                {
                    ViewData["NonFinancialProportion"] = BusinessRankingStructure.SelectRankingStructureByIndexAndAudit(Constants.RNK_STRUCTURE_NONFINANCIAL_INDEX, ranking.AuditedStatus).Percentage.Value;
                }
                catch
                {
                    ViewData["NonFinancialProportion"] = "0";
                }
                try
                {
                    var prop = System.Convert.ToDecimal(ViewData["NonFinancialProportion"]);
                    if (prop != 0 && tempScore != null)
                    {
                        ViewData["NonFinancialScore"] = tempScore / prop * 100;
                    }
                }
                catch
                {
                    ViewData["NonFinancialScore"] = "0";
                }
                ViewData["DetailView"] = "true";
                return View(NonFinancial);
            }
            catch (Exception)
            {
                //TODO: ERROR MESSAGE
                ViewData["DetailView"] = true;
                return RedirectToAction("DetailGeneral");
            }
        }

        #endregion
        #region ViewCalculated
        public ActionResult EditScaleCalculate(int id)
        {
            return null;
        }
        #endregion

        public ActionResult Rerank(int id,string redirectAction)
        {
            RNKRankFinal model = new RNKRankFinal();
            
            try
            {
                ViewData["RankID"] = id.ToString();
                ViewData["redirectAction"] = redirectAction;
                ViewData["Edit"] = true;
                RNKRankMarking.RemarkAllBusinessRanking(id);
                
                LoadRankingViewModel(id, model);
                TempData["Dialog"] = "Rank was saved successfully";
                return View("Ranking",model);
            }
            catch
            {
                //TODO: ERROR message here
            }
            return View("Ranking", model);
        }
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
