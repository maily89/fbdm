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
    public class RNKRankIndividualController : Controller
    {
        public ActionResult Index()
        {
            List<CustomersIndividualRanking> model = null;
            try
            {
                model = CustomersIndividualRanking.SelectIndividualRankings();
                if (model == null)
                {
                    throw new Exception();
                }

            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_INDIVIDUAL_RANKING);

            }
            return View(model);
        }

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

        [HttpPost]
        public ActionResult Add(RNKIndividualViewModel model)
        {
            if (model == null) return null;

            try
            {
                if (model.CustomerID > 0 && model.Date != null)
                {
                    var ranking = CustomersIndividualRanking.SelectRankingByDateAndCustomer(model.Date, model.CustomerID);

                    if (ranking == null)
                    {
                        ranking = new CustomersIndividualRanking();
                    }
                    else return View("Edit", ranking.ID);
                    ranking.Date = model.Date;
                    ranking.DateModified = DateTime.Now;
                    model.IndividualRanking = ranking;

                    var customer = CustomersIndividuals.SelectIndividualByID(model.CustomerID);
                    model.CIF = customer.CIF;

                    return View("AddInfo", model);
                }

                model.IndividualCustomer = CustomersIndividuals.SelectIndividuals();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                return View(model);
            }

            TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddInfo(RNKIndividualViewModel model)
        {
            int rankID;

            if (model == null) return null;
            try
            {
                if (ModelState.IsValid)
                {
                    rankID = CopyModelToRanking(model,true);
                }
                else throw new Exception();

                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                return RedirectToAction("AddBasic", new { id = rankID });

            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);

                return View(model);
            }
        }

        public ActionResult AddBasicScore(int id)
        {
            try
            {
                List<RNKBasicRow> financial = CustomersIndividualBasicIndex.LoadBasicIndex(id, true);
                return View(financial);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult AddBasicScore(List<RNKBasicRow> rnkBasicRow,int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                int rankID = id;
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
                RNKBasicMarking.CalculateBasicScore(id, true, entities);
                if (TempData["BasicRedirect"] != null)
                    return RedirectToAction("Edit", new { id = rankID });
                return RedirectToAction("AddCollateralScore", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                CustomersIndividualBasicIndex.Reload(rnkBasicRow);

                if (TempData["BasicRedirect"] != null)
                    TempData["BasicRedirect"] = "Edit";
                return View(rnkBasicRow);
            }
        }
        public ActionResult AddCollateralScore(int id)
        {
            try
            {
                List<RNKCollateralRow> financial = CustomersIndividualCollateralIndex.LoadCollateralIndex(id, true);
                return View(financial);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult AddCollateralScore(List<RNKCollateralRow> rnkCollateralRow, int id)
        {
            try
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
                if (TempData["CollateralRedirect"] != null)
                    return RedirectToAction("Edit", new { id = rankID });
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                CustomersIndividualCollateralIndex.Reload(rnkCollateralRow);

                if (TempData["CollateralRedirect"] != null)
                    TempData["CollateralRedirect"] = "Edit";
                return View(rnkCollateralRow);
            }
        }
        #region Edit
        public ActionResult Edit(int id)
        {
            return View(id);
        }

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
            addModel.CustomerID = customer.IndividualID;
            addModel.CIF = customer.CIF;

           
            TempData["EditMode"] = "EditMode";
            return View("Add", addModel);

        }

        [HttpPost]
        public ActionResult EditInfo(RNKIndividualViewModel model)
        {
            int rankID;

            if (model == null) return null;
            try
            {
                if (ModelState.IsValid)
                {
                    rankID = CopyModelToRanking(model,false);
                }
                else throw new Exception();

                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);
                return RedirectToAction("Edit", new { id = rankID });

            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_INDIVIDUAL_RANKING);

                return View(model);
            }
        }

        public ActionResult EditBasicScore(int id)
        {
            //Check if there's no saved data=> return
            List<RNKBasicRow> basic = null;
            if (CustomersIndividualBasicIndex.SelectBasicIndexByRankingID(id).Count <= 0)
            {
                basic = CustomersIndividualBasicIndex.LoadBasicIndex(id, true);
                TempData["BasicRedirect"] = "Edit";
                return View("AddBasicScore", basic);
            }

            //Change to edit
            TempData["EditMode"] = "EditMode";

            basic = CustomersIndividualBasicIndex.LoadBasicIndex(id, false);
            return View("AddBasicScore", basic);
        }

        [HttpPost]
        public ActionResult EditBasicScore(List<RNKBasicRow> rnkBasicRow,int id)
        {
            try
            {
                FBDEntities entity = new FBDEntities();
                int rankID = id;

                //Load BasicRow and save them.
                foreach (RNKBasicRow item in rnkBasicRow)
                {
                    if (item.LeafIndex)
                    {
                        CustomersIndividualBasicIndex indexScore = CustomersIndividualBasicIndex.SelectBasicIndexByID(item.CustomerBasicID, entity);

                        CustomersIndividualBasicIndex.ConvertBasicRowToModel(entity, item, indexScore);

                        CustomersIndividualBasicIndex.EditBasicIndex(indexScore, entity);
                    }

                }

                RNKBasicMarking.CalculateBasicScore(id, false, entity);

                return RedirectToAction("Edit", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                TempData["EditMode"] = "EditMode";
                CustomersIndividualBasicIndex.Reload(rnkBasicRow);
                return View("AddBasicScore", rnkBasicRow);
            }
        }

        public ActionResult EditCollateralScore(int id)
        {
            //Check if there's no saved data=> return
            List<RNKCollateralRow> collateral = null;
            if (CustomersIndividualCollateralIndex.SelectCollateralIndexByRankingID(id).Count <= 0)
            {
                collateral = CustomersIndividualCollateralIndex.LoadCollateralIndex(id, true);
                TempData["CollateralRedirect"] = "Edit";
                return View("AddCollateralScore", collateral);
            }

            //Change to edit
            TempData["EditMode"] = "EditMode";

            collateral = CustomersIndividualCollateralIndex.LoadCollateralIndex(id, false);
            return View("AddCollateralScore", collateral);
        }

        [HttpPost]
        public ActionResult EditCollateralScore(List<RNKCollateralRow> rnkCollateralRow, int id)
        {
            try
            {
                FBDEntities entity = new FBDEntities();
                int rankID = id;

                //Load CollateralRow and save them.
                foreach (RNKCollateralRow item in rnkCollateralRow)
                {
                    if (item.LeafIndex)
                    {
                        CustomersIndividualCollateralIndex indexScore = CustomersIndividualCollateralIndex.SelectCollateralIndexByID(item.CustomerCollateralID, entity);

                        CustomersIndividualCollateralIndex.ConvertCollateralRowToModel(entity, item, indexScore);

                        CustomersIndividualCollateralIndex.EditCollateralIndex(indexScore, entity);
                    }

                }

                RNKCollateralMarking.CalculateCollateralScore(id, false, entity);

                return RedirectToAction("Edit", new { id = rankID });
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS_FINANCIAL_INDEX);
                TempData["EditMode"] = "EditMode";
                CustomersIndividualCollateralIndex.Reload(rnkCollateralRow);
                return View("AddCollateralScore", rnkCollateralRow);
            }
        }
        #endregion
        #region Private

        private static int CopyModelToRanking(RNKIndividualViewModel model, bool isAdd)
        {
            int rankID;
            var entity = new FBDEntities();
            var ranking = model.IndividualRanking;

            ranking.CustomersIndividuals = CustomersIndividuals.SelectIndividualByID(model.CustomerID, entity);
            ranking.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(model.LoanTermID);
            ranking.IndividualBorrowingPurposes = IndividualBorrowingPurposes.SelectBorrowingPPByID(model.PurposeID);
            if (isAdd)
                CustomersIndividualRanking.AddIndividualRanking(ranking, entity);
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

    }
}
