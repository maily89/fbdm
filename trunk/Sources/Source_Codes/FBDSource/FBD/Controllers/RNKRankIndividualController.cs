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
    public class RNKRankIndividualController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<CustomersIndividualRanking> customerRankList = null;
            try
            {
                customerRankList = CustomersIndividualRanking.SelectIndividualRankings();
                foreach (CustomersIndividualRanking item in customerRankList)
                {
                    item.CustomersIndividualsReference.Load();
                    item.IndividualSummaryRanksReference.Load();
                }
                if (customerRankList == null)
                {
                    throw new Exception();
                }

            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_INDIVIDUAL_RANKING);
            }
            RNKIndividualIndex index = new RNKIndividualIndex();
            index.CustomerRanking = customerRankList;
            return View(index);
        }

        /// <summary>
        /// index after search post data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(RNKIndividualIndex data)
        {
            List<CustomersIndividualRanking> customerRankList = null;
            try
            {

                customerRankList = CustomersIndividualRanking.SelectRankingByDateAndCifAndBranch(data.FromDate,data.ToDate, data.Cif, data.BranchID);
                foreach (CustomersIndividualRanking item in customerRankList)
                {
                    item.CustomersIndividualsReference.Load();
                    item.IndividualSummaryRanksReference.Load();
                }
                if (customerRankList == null)
                {
                    throw new Exception();
                }

            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_INDIVIDUAL_RANKING);
            }
            
            data.CustomerRanking = customerRankList;

            return View(data);
        }

        /// <summary>
        /// display choose customer view
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new RNKIndividualViewModel();
            try
            {
                model.IndividualCustomer = CustomersIndividuals.SelectIndividuals();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_INDIVIDUAL);
            }
            return View(model);
        }

        /// <summary>
        /// This methods get data from choose customer view,and return view for adding customer detailed info
        /// (similar to AddStep1 in RankBusiness)
        /// this method do not save any data to database yet!
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(RNKIndividualViewModel model)
        {
            if (model == null) return null;
            try
            {
                if (model.CustomerID > 0 && model.Date != null)
                {
                    //adding ranking object to model
                    var ranking = CustomersIndividualRanking.SelectRankingByDateAndCustomer(model.Date, model.CustomerID);

                    if (ranking == null)
                    {
                        ranking = new CustomersIndividualRanking();
                    }
                    else return RedirectToAction("DetailGeneral", new { id = ranking.ID });
                    ranking.Date = model.Date;
                    ranking.UserID = Session[Constants.SESSION_USER_ID].ToString();

                    ranking.DateModified = DateTime.Now;
                    model.IndividualRanking = ranking;

                    //loading customer
                    var customer = CustomersIndividuals.SelectIndividualByID(model.CustomerID);
                    model.CIF = customer.CIF;
                    customer.SystemBranchesReference.Load();
                   
                    //adding rnkcustomerinfo for view
                    RNKCustomerInfo tempInfo=new RNKCustomerInfo();
                    tempInfo.CIF=customer.CIF;
                    tempInfo.CustomerName = customer.CustomerName;
                    tempInfo.Date = model.Date;
                    if (customer.SystemBranches != null)
                    tempInfo.Branch = customer.SystemBranches.BranchName;

                    model.CustomerInfo = tempInfo;
                    return View("AddInfo", model);
                }

                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                model.IndividualCustomer = CustomersIndividuals.SelectIndividuals();
                return View(model);
            }

        }

        /// <summary>
        /// This method is used to handle detailed info added or edit
        /// AFTER this step, ranking info is actually saved to database
        /// This method handle both add and edit class
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddInfo(RNKIndividualViewModel model,string Edit)
        {
            
            int rankID;
            
            if (model == null) return null;
            try
            {
                if (!string.IsNullOrEmpty(Edit)) ViewData["Edit"] = Edit;
                if (ModelState.IsValid)
                {
                    if (String.IsNullOrEmpty(Edit))
                    {
                        //this is the actual rankID get from database. After this step, we use rankID to manage.
                        rankID = CopyModelToRanking(model, true);
                    }
                    else
                    {
                        rankID = CopyModelToRanking(model, false);
                    }
                }
                else throw new Exception();

                if (string.IsNullOrEmpty(Edit))
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                    return RedirectToAction("AddBasicScore", new { id = rankID });
                }
                else
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                    return RedirectToAction("DetailGeneral", new { id = rankID });

                }

            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(string.IsNullOrEmpty(Edit)?Constants.ERR_ADD_POST:Constants.ERR_EDIT_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);

                RNKCustomerInfo temp = new RNKCustomerInfo();
                var customer = CustomersIndividuals.SelectIndividualByID(model.CustomerID);
                temp.CIF = customer.CIF;
                temp.CustomerName = customer.CustomerName;

                temp.Date = model.Date;

                customer.SystemBranchesReference.Load();
                if (customer.SystemBranches != null)
                    temp.Branch = customer.SystemBranches.BranchName;

                model.CustomerInfo = temp;

                if (model.IndividualRanking != null)
                    ViewData["RankID"] = model.IndividualRanking.ID;
                return View(model);
            }
        }

        public ActionResult AddBasicScore(int id,string Edit)
        {
            
            try
            {
                ViewData["RankID"] = id;
                if (!string.IsNullOrEmpty(Edit)) ViewData["Edit"] = true;

                List<RNKBasicRow> financial=null;
                if (string.IsNullOrEmpty(Edit))
                {
                    financial = CustomersIndividualBasicIndex.LoadBasicIndex(id, true);
                }
                else
                {
                    financial = CustomersIndividualBasicIndex.LoadBasicIndex(id,false);
                }

                return View(financial);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AddBasicCalculate(List<RNKBasicRow> rnkBasicRow,string Edit,string rankID)
        {
            //return viewdata state
            ViewData["RankID"] = rankID;
            if (!string.IsNullOrEmpty(Edit)) ViewData["Edit"] = Edit;
            if (rnkBasicRow == null || rnkBasicRow.Count <= 0) return View(rnkBasicRow);

            var rankingID = System.Convert.ToInt32(rankID);
            CustomersIndividualRanking ranking = CustomersIndividualRanking.SelectIndividualRankingByID(rankingID);

            decimal final=RNKBasicMarking.CalculateTempBasic(rankingID, rnkBasicRow);

            var basicRank = RNKBasicMarking.GetRank(final);
            ViewData["BasicScore"] = final;
            if(basicRank!=null)
            ViewData["BasicRank"] = basicRank.Rank;
            return View(rnkBasicRow);
        }
        [HttpPost]
        public ActionResult SaveBasicScore(List<RNKBasicRow> rnkBasicRow, string Edit, string SaveBack, string SaveNext, string Back, string SaveRerank, int rankID)
        {
            try
            {
                ViewData["RankID"] = rankID.ToString();
                if (!string.IsNullOrEmpty(Edit))
                {
                    ViewData["Edit"] = Edit;
                }

                if (Back != null) return View("AddBasicScore", CustomersIndividualBasicIndex.Reload(rnkBasicRow));

                if (string.IsNullOrEmpty(Edit))
                {
                    AddBasicList(rnkBasicRow, rankID);
                }
                else
                {
                    EditBasicList(rnkBasicRow, rankID);
                }

                if (SaveNext != null)
                    return RedirectToAction("AddCollateralScore", new { id = rankID });

                if (SaveRerank != null)
                    return RedirectToAction("Rerank", new { id = rankID, redirectAction = "DetailBasic" });
                if (!string.IsNullOrEmpty(Edit))
                {
                    return RedirectToAction("DetailBasic", new { id = rankID });
                }
                else
                    return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(string.IsNullOrEmpty(Edit)?Constants.ERR_ADD_POST:Constants.ERR_EDIT_POST, Constants.INV_BASIC_INDEX);
                CustomersIndividualBasicIndex.Reload(rnkBasicRow);

                return View("AddBasicScore", rnkBasicRow);
            }
        }

        private static void AddBasicList(List<RNKBasicRow> rnkBasicRow, int rankID)
        {
            FBDEntities entities = new FBDEntities();
            foreach (RNKBasicRow item in rnkBasicRow)
            {
                if (item.LeafIndex)
                {
                    CustomersIndividualBasicIndex indexScore = new CustomersIndividualBasicIndex();
                    indexScore = CustomersIndividualBasicIndex.ConvertBasicRowToModel(entities, item, indexScore);

                    entities.AddToCustomersIndividualBasicIndex(indexScore);
                }
            }
            entities.SaveChanges();
            RNKBasicMarking.CalculateBasicScore(rankID, true, entities);
        }
        public ActionResult AddCollateralScore(int id,string Edit)
        {
            try
            {
                ViewData["RankID"] = id;
                if (!string.IsNullOrEmpty(Edit)) ViewData["Edit"] = true;

                List<RNKCollateralRow> financial = CustomersIndividualCollateralIndex.LoadCollateralIndex(id,string.IsNullOrEmpty(Edit)? true:false);
                return View(financial);
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_COLLATERAL_INDEX);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AddCollateralCalculate(List<RNKCollateralRow> rnkCollateralRow, string Edit, string rankID)
        {
            //return viewdata state
            ViewData["RankID"] = rankID;
            if (!string.IsNullOrEmpty(Edit)) ViewData["Edit"] = Edit;
            if (rnkCollateralRow == null || rnkCollateralRow.Count <= 0) return View(rnkCollateralRow);

            var rankingID = System.Convert.ToInt32(rankID);
            CustomersIndividualRanking ranking = CustomersIndividualRanking.SelectIndividualRankingByID(rankingID);

            decimal final = RNKCollateralMarking.CalculateTempCollateral(rankingID, rnkCollateralRow);

            var collateralRank = RNKCollateralMarking.GetRank(final);
            ViewData["CollateralScore"] = final;
            if(collateralRank!=null)
            ViewData["CollateralRank"] = collateralRank.Rank;
            return View(rnkCollateralRow);
        }

        [HttpPost]
        public ActionResult SaveCollateralScore(List<RNKCollateralRow> rnkCollateralRow, string Edit, string SaveBack, string SaveNext, string Back, string SaveRerank, int rankID)
        {
            try
            {
                ViewData["RankID"] = rankID.ToString();
                if (!string.IsNullOrEmpty(Edit))
                {
                    ViewData["Edit"] = Edit;
                }

                if (Back != null) return View("AddCollateralScore", CustomersIndividualCollateralIndex.Reload(rnkCollateralRow));

                if (string.IsNullOrEmpty(Edit))
                {
                    AddCollateralList(rnkCollateralRow, rankID);
                }
                else
                {
                    EditCollateralList(rnkCollateralRow, rankID);
                }


                if (SaveNext != null)
                    return RedirectToAction("Ranking", new { id = rankID });

                if (SaveRerank != null)
                    return RedirectToAction("Rerank", new { id = rankID, redirectAction = "DetailCollateral" });
                if (!string.IsNullOrEmpty(Edit))
                {
                    return RedirectToAction("DetailCollateral", new { id = rankID });
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {
                //TODO: define error message
                TempData[Constants.ERR_MESSAGE] = string.Format(string.IsNullOrEmpty(Edit) ? Constants.ERR_ADD_POST : Constants.ERR_EDIT_POST, Constants.INV_COLLATERAL_INDEX);
                CustomersIndividualCollateralIndex.Reload(rnkCollateralRow);

                return View("AddCollateralScore",rnkCollateralRow);
            }
        }

        private static int AddCollateralList(List<RNKCollateralRow> rnkCollateralRow, int id)
        {
            FBDEntities entities = new FBDEntities();
            int rankID = id;
            foreach (RNKCollateralRow item in rnkCollateralRow)
            {
                if (item.LeafIndex)
                {
                    CustomersIndividualCollateralIndex indexScore = new CustomersIndividualCollateralIndex();
                    indexScore = CustomersIndividualCollateralIndex.ConvertCollateralRowToModel(entities, item, indexScore);

                    entities.AddToCustomersIndividualCollateralIndex(indexScore);
                }
            }
            entities.SaveChanges();
            RNKCollateralMarking.CalculateCollateralScore(id, true, entities);
            return rankID;
        }
        #region Edit
        //public ActionResult Edit(int id)
        //{
        //    return View(id);
        //}

        public ActionResult EditInfo(int id)
        {
            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);
            RNKIndividualViewModel addModel = new RNKIndividualViewModel();

            ranking.DateModified = DateTime.Now;

            ranking.CustomersLoanTermReference.Load();
            if (ranking.CustomersLoanTerm != null)
                addModel.LoanTermID = ranking.CustomersLoanTerm.LoanTermID;

            ranking.IndividualBorrowingPurposesReference.Load();
            if (ranking.IndividualBorrowingPurposes != null)
                addModel.PurposeID = ranking.IndividualBorrowingPurposes.PurposeID;

            ranking.CustomersIndividualsReference.Load();
            var customer = ranking.CustomersIndividuals;
            addModel.IndividualRanking = ranking;
            addModel.CustomerID = customer.IndividualID;
            addModel.CIF = customer.CIF;

            addModel.CustomerInfo = RNKCustomerInfo.GetIndividualRankingInfo(id);

            ViewData["RankID"] = id;
            ViewData["Edit"] = "Edit";
            return View("AddInfo", addModel);

        }

        //[HttpPost]
        //public ActionResult EditInfo(RNKIndividualViewModel model)
        //{
        //    int rankID;

        //    if (model == null) return null;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            rankID = CopyModelToRanking(model,false);
        //        }
        //        else throw new Exception();

        //        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);
        //        return RedirectToAction("Edit", new { id = rankID });

        //    }
        //    catch
        //    {
        //        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);

        //        return View(model);
        //    }
        //}

        //public ActionResult EditBasicScore(int id)
        //{
        //    //Check if there's no saved data=> return
        //    List<RNKBasicRow> basic = null;
        //    if (CustomersIndividualBasicIndex.SelectBasicIndexByRankingID(id).Count <= 0)
        //    {
        //        basic = CustomersIndividualBasicIndex.LoadBasicIndex(id, true);
        //        TempData["BasicRedirect"] = "Edit";
        //        return View("AddBasicScore", basic);
        //    }

        //    //Change to edit
        //    TempData["EditMode"] = "EditMode";

        //    basic = CustomersIndividualBasicIndex.LoadBasicIndex(id, false);
        //    return View("AddBasicScore", basic);
        //}

        //[HttpPost]
        //public ActionResult EditBasicScore(List<RNKBasicRow> rnkBasicRow,int id)
        //{
        //    try
        //    {
        //        int rankID = EditBasicList(rnkBasicRow, id);

        //        return RedirectToAction("Edit", new { id = rankID });
        //    }
        //    catch (Exception)
        //    {
        //        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
        //        TempData["EditMode"] = "EditMode";
        //        CustomersIndividualBasicIndex.Reload(rnkBasicRow);
        //        return View("AddBasicScore", rnkBasicRow);
        //    }
        //}

        private static int EditBasicList(List<RNKBasicRow> rnkBasicRow, int id)
        {
            FBDEntities entity = new FBDEntities();
            int rankID = id;

            //Load BasicRow and save them.
            foreach (RNKBasicRow item in rnkBasicRow)
            {
                if (item.LeafIndex)
                {
                    CustomersIndividualBasicIndex indexScore = CustomersIndividualBasicIndex.SelectBasicIndexByID(item.CustomerScoreID, entity);

                    CustomersIndividualBasicIndex.ConvertBasicRowToModel(entity, item, indexScore);

                    CustomersIndividualBasicIndex.EditBasicIndex(indexScore, entity);
                }

            }

            RNKBasicMarking.CalculateBasicScore(id, false, entity);
            return rankID;
        }

        //public ActionResult EditCollateralScore(int id)
        //{
        //    //Check if there's no saved data=> return
        //    List<RNKCollateralRow> collateral = null;
        //    if (CustomersIndividualCollateralIndex.SelectCollateralIndexByRankingID(id).Count <= 0)
        //    {
        //        collateral = CustomersIndividualCollateralIndex.LoadCollateralIndex(id, true);
        //        TempData["CollateralRedirect"] = "Edit";
        //        return View("AddCollateralScore", collateral);
        //    }

        //    //Change to edit
        //    TempData["EditMode"] = "EditMode";

        //    collateral = CustomersIndividualCollateralIndex.LoadCollateralIndex(id, false);
        //    return View("AddCollateralScore", collateral);
        //}

        //[HttpPost]
        //public ActionResult EditCollateralScore(List<RNKCollateralRow> rnkCollateralRow, int id)
        //{
        //    try
        //    {
        //        int rankID = EditCollateralList(rnkCollateralRow, id);

        //        return RedirectToAction("Edit", new { id = rankID });
        //    }
        //    catch (Exception)
        //    {
        //        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
        //        TempData["EditMode"] = "EditMode";
        //        CustomersIndividualCollateralIndex.Reload(rnkCollateralRow);
        //        return View("AddCollateralScore", rnkCollateralRow);
        //    }
        //}

        private static int EditCollateralList(List<RNKCollateralRow> rnkCollateralRow, int id)
        {
            FBDEntities entity = new FBDEntities();
            int rankID = id;

            //Load CollateralRow and save them.
            foreach (RNKCollateralRow item in rnkCollateralRow)
            {
                if (item.LeafIndex)
                {
                    CustomersIndividualCollateralIndex indexScore = CustomersIndividualCollateralIndex.SelectCollateralIndexByID(item.CustomerScoreID, entity);

                    CustomersIndividualCollateralIndex.ConvertCollateralRowToModel(entity, item, indexScore);

                    CustomersIndividualCollateralIndex.EditCollateralIndex(indexScore, entity);
                }

            }

            RNKCollateralMarking.CalculateCollateralScore(id, false, entity);
            return rankID;
        }
        #endregion
        #region Private

        private static int CopyModelToRanking(RNKIndividualViewModel model, bool isAdd)
        {
            int rankID;
            var entity = new FBDEntities();
            var ranking = model.IndividualRanking;
            
            ranking.CustomersIndividuals = CustomersIndividuals.SelectIndividualByID(model.CustomerID, entity);
            ranking.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(model.LoanTermID,entity);
            ranking.IndividualBorrowingPurposes = IndividualBorrowingPurposes.SelectBorrowingPPByID(model.PurposeID,entity);

            if (isAdd)
            {
                ranking.Date = model.Date;
                CustomersIndividualRanking.AddIndividualRanking(ranking, entity);
            }
            else
            {
                CustomersIndividualRanking.EditIndividualRanking(ranking, entity);
            }
            rankID = ranking.ID;
            return rankID;
        }
        #endregion
        public ActionResult Delete(int id)
        {
            try
            {
                int result = CustomersIndividualRanking.DeleteIndividualRanking(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                return RedirectToAction("Index");
            }

        }

        public ActionResult Ranking(int id)
        {
            var model = new RNKRankIndividualFinal();
            try
            {
                FBDEntities entities = new FBDEntities();
                ViewData["RankID"] = id.ToString();
                RNKRankMarking.SaveIndividualRank(id,entities);
                LoadRankingViewModel(id, model);

                //TODO: Cluster Rank handle

            }
            catch
            {
                //TODO: Add to constants
                TempData[Constants.ERR_MESSAGE] = "There's error when loading ranking for individual customer";
            }
            return View(model);

        }

        public ActionResult Rerank(int id, string redirectAction)
        {
            RNKRankIndividualFinal model = new RNKRankIndividualFinal();

            try
            {
                ViewData["RankID"] = id.ToString();
                ViewData["redirectAction"] = redirectAction;
                ViewData["Edit"] = true;
                RNKRankMarking.RemarkAllIndividualRanking(id);

                LoadRankingViewModel(id, model);
                TempData["Dialog"] = "Rank was saved successfully";
                return View("Ranking", model);
            }
            catch
            {
                //TODO: ERROR message here
            }
            return View("Ranking", model);
        }


        private void LoadRankingViewModel(int id, RNKRankIndividualFinal model)
        {
            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);
            ranking.IndividualSummaryRanksReference.Load();

            if (ranking.IndividualSummaryRanks != null)
            {
                model.ClassRank = ranking.IndividualSummaryRanks.Evaluation;
            }

            if (ranking.BasicIndexScore != null)
            {
                model.BasicScore = ranking.BasicIndexScore.Value;
                var temp = RNKBasicMarking.GetRank(model.BasicScore);
                model.BasicRank = temp != null ? temp.Rank : null;
            }

            if (ranking.CollateralIndexScore != null)
            {
                model.CollateralScore = ranking.CollateralIndexScore.Value;
                var temp = RNKCollateralMarking.GetRank(model.CollateralScore);
                model.CollateralRank = temp != null ? temp.Rank : null;
            }
        }

        #region ViewDetail
        public ActionResult DetailGeneral(int id)
        {
            try
            {
                ViewData["RankID"] = id.ToString();
                ViewData["DetailView"] = true;
                var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);
                ranking.LoadAll();

                RNKIndividualViewModel model = new RNKIndividualViewModel();
                model.IndividualRanking = ranking;
                model.CustomerInfo = RNKCustomerInfo.GetIndividualRankingInfo(id);
                return View(model);
            }
            catch
            {
                //TODO: add error text to constant
                TempData[Constants.ERR_MESSAGE] = "Error occured when loading the ranked customers. Please try again later";
                return RedirectToAction("Index");
            }
        }

        public ActionResult DetailBasic(int id)
        {
            try
            {
                TempData["DetailView"] = "true";
                ViewData["RankID"] = id.ToString();
                List<RNKBasicRow> basic = CustomersIndividualBasicIndex.LoadBasicIndex(id, false);
                var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);
                var tempScore = ranking.BasicIndexScore;
                ViewData["BasicScore"] = tempScore;

                if (tempScore != null)
                {
                    IndividualBasicRanks basicRank = RNKBasicMarking.GetRank(tempScore.Value);
                    if(basicRank!=null)
                    ViewData["BasicRank"] = basicRank.Rank;
                }


                return View(basic);
            }
            catch
            {
                ViewData["DetailView"] = true;
                return RedirectToAction("DetailGeneral");
            }
        }

        public ActionResult DetailCollateral(int id)
        {
            try
            {
                TempData["DetailView"] = "true";
                ViewData["RankID"] = id.ToString();
                List<RNKCollateralRow> collateral = CustomersIndividualCollateralIndex.LoadCollateralIndex(id, false);
                var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);
                var tempScore = ranking.CollateralIndexScore;
                ViewData["CollateralScore"] = tempScore;

                if (tempScore != null)
                {
                    IndividualCollateralRanks collateralRank = RNKCollateralMarking.GetRank(tempScore.Value);
                    if (collateralRank != null)
                        ViewData["CollateralRank"] = collateralRank.Rank;
                }


                return View(collateral);
            }
            catch
            {
                ViewData["DetailView"] = true;
                return RedirectToAction("DetailGeneral");
            }
        }
        #endregion

    }
}
