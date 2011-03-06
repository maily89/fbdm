using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.ViewModels;
using FBD.Models;
using FBD.CommonUtilities;


namespace FBD.Controllers
{
    public class INVBasicIndexScoreController : Controller
    {
        //
        // GET: /INVBasicIndexScore/

        public ActionResult Index()
        {

            INVBasicIndexScoreViewModel viewModel = new INVBasicIndexScoreViewModel();
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                List<IndividualBasicIndex> lstbasicIndex = IndividualBasicIndex.SelectBasicIndex();
                List<IndividualBorrowingPurposes> lstBorrowingPP = IndividualBorrowingPurposes.SelectBorrowingPPList();
                //List<BusinessFinancialIndex> lstFinancialIndexes = BusinessFinancialIndex.SelectFinancialIndex(FBDModel);

                // If there is no information to choose from the drop down list, return empty-data View
                if (lstbasicIndex.Count() < 1 || lstBorrowingPP.Count() < 1)
                {
                    return View(viewModel);
                }

                // Create View model with the selected industry, scale and financial index
                viewModel = IndividualBasicIndexScore.CreateViewModelByBasicAndPurposeIndex(FBDModel,
                                         lstbasicIndex[0].IndexID, lstBorrowingPP[0].PurposeID);
            }
            catch (Exception)
            {
                // Return View with error message
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_DISPLAY_FISCORE;
                return View(viewModel);
            }

            return View(viewModel);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
         [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            { 
                // If action posted to Controller is choosing item from drop down list...
                if (formCollection["Basic Index"] != null
                    || formCollection["Borrowing Purpose"] != null
                       )
                {
                    // ... then create view model for displaying information
                    INVBasicIndexScoreViewModel viewModelForDisplayingInformation = IndividualBasicIndexScore
                                                            .CreateViewModelByBasicAndPurposeIndex(                                                            FBDModel,
                                                            formCollection["Basic Index"].ToString(),
                                                            formCollection["Borrowing Purpose"].ToString());

                    return View(viewModelForDisplayingInformation);                                                            
                }
            }
            catch(Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_DISPLAY_FISCORE;
                return RedirectToAction("Index");
            }

            // If the action posted to Controller is Saving information changes
            try
            {
                if (formCollection["Save"] != null)
                {
                    INVBasicIndexScoreViewModel viewModelForSavingScore = new INVBasicIndexScoreViewModel();

                    // Iterate all the rows of financial index proportion list
                    for (int i = 0; i < int.Parse(formCollection["NumberOfScoreRows"].ToString()); i++)
                    {
                        INVBasicScoreRowViewModel rowForSavingScore = new INVBasicScoreRowViewModel();

                        // If the row is checked by checkbox
                        if (formCollection["ScoreRows[" + i + "].Checked"].ToString().Equals("true,false")
                                || formCollection["ScoreRows[" + i + "].Checked"].ToString().Equals("True,False")
                                    || formCollection["ScoreRows[" + i + "].Checked"].ToString().Equals("TRUE,FALSE"))
                        {
                            // Mark the row as 'Checked'
                            rowForSavingScore.Checked = true;
                        }

                        rowForSavingScore.LevelID = decimal.Parse(formCollection["ScoreRows[" + i + "].LevelID"].ToString());

                        try
                        {
                            rowForSavingScore.FromValue = decimal.Parse(formCollection["ScoreRows[" + i + "].FromValue"].ToString());
                        }
                        catch (Exception)
                        {
                            rowForSavingScore.FromValue = 0;
                        }

                        try
                        {
                            rowForSavingScore.ToValue = decimal.Parse(formCollection["ScoreRows[" + i + "].ToValue"].ToString());
                        }
                        catch (Exception)
                        {
                            rowForSavingScore.ToValue = 0;
                        }

                        rowForSavingScore.FixedValue = formCollection["ScoreRows[" + i + "].FixedValue"].ToString();
                        rowForSavingScore.ScoreID = int.Parse(formCollection["ScoreRows[" + i + "].ScoreID"].ToString());

                        // Add the row to the View Model
                        viewModelForSavingScore.ScoreRows.Add(rowForSavingScore);
                    }

                    viewModelForSavingScore.basicIndexID = formCollection["basicIndexID"].ToString();
                    viewModelForSavingScore.BorrowingPPID = formCollection["BorrowingPPID"].ToString();

                    // Perform saving information changes posted from View
                    string errorLevel = IndividualBasicIndexScore
                                            .EditMultipleBasicIndexScore(
                                            FBDModel, viewModelForSavingScore);

                    // The view model after saving information
                    INVBasicIndexScoreViewModel viewModelAfterUpdating = IndividualBasicIndexScore
                                                                .CreateViewModelByBasicAndPurposeIndex(
                                                                FBDModel,
                                                                formCollection["basicIndexID"].ToString(),
                                                                formCollection["BorrowingPPID"].ToString());
                    // If saving gets error
                    if (errorLevel != null)
                    {
                        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_UPDATE_FISCORE, errorLevel);
                        return View(viewModelAfterUpdating);
                    }
                    // If saving gets success
                    else
                    {
                        TempData[Constants.SCC_MESSAGE] = Constants.SCC_UPDATE_FISCORE;
                        return View(viewModelAfterUpdating);
                    }
                }
            }
            catch(Exception e)
            {

                TempData[Constants.ERR_MESSAGE] = e.ToString();//Constants.ERR_POST_FISCORE;
                return RedirectToAction("Index");
            }

            return View(new INVBasicIndexScoreViewModel());
        }
    }
}
