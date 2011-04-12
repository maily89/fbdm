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
    public class INVCollateralIndexScoreController : Controller
    {
        //
        // GET: /INVCollateralIndexScore/

        /// <summary>
        /// Create index view with a list of collatealindex score form db 
        /// </summary>
        /// <returns> index view </returns>
        
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            INVCollateralIndexScoreViewModel viewModel = new INVCollateralIndexScoreViewModel();
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                List<IndividualCollateralIndex> lstCollateralIndex = IndividualCollateralIndex.SelectCollateralIndex();
                //List<BusinessFinancialIndex> lstFinancialIndexes = BusinessFinancialIndex.SelectFinancialIndex(FBDModel);

                // If there is no information to choose from the drop down list, return empty-data View
                if (lstCollateralIndex.Count() < 1)
                {
                    return View(viewModel);
                }

                // Create View model with the selected industry, scale and financial index
                viewModel = IndividualCollateralIndexScore.CreateViewModelByCollateral(FBDModel,
                                         lstCollateralIndex[0].IndexID);
            }
            catch (Exception)
            {
                // Return View with error message
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_MESSAGE,Constants.INV_COLLATERAL_INDEX);
                return View(viewModel);
            }

            return View(viewModel);
        }


        /// <summary>
        /// process display a collateralindexscore and save the updated score
        /// </summary>
        /// <param name="formCollection">formCollection</param>
        /// <returns>index view</returns>
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If action posted to Controller is choosing item from drop down list...
                if (formCollection["Collateral Index"] != null )
                {
                    // ... then create view model for displaying information
                    INVCollateralIndexScoreViewModel viewModelForDisplayingInformation = IndividualCollateralIndexScore
                                                            .CreateViewModelByCollateral(FBDModel,
                                                            formCollection["Collateral Index"].ToString());

                    return View(viewModelForDisplayingInformation);
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST,Constants.INV_COLLATERAL_INDEX);
                return RedirectToAction("Index");
            }

            // If the action posted to Controller is Saving information changes
            try
            {
                if (formCollection["Save"] != null)
                {
                    if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
                    {
                        return RedirectToAction("Unauthorized", "SYSAuths");
                    }
                    INVCollateralIndexScoreViewModel viewModelForSavingScore = new INVCollateralIndexScoreViewModel();

                    // Iterate all the rows of financial index proportion list
                    for (int i = 0; i < int.Parse(formCollection["NumberOfScoreRows"].ToString()); i++)
                    {
                        INVCollateralScoreRowViewModel rowForSavingScore = new INVCollateralScoreRowViewModel();

                        // If the row is checked by checkbox
                        if (formCollection["ScoreRows[" + i + "].Checked"].ToString().Equals("true,false")
                                || formCollection["ScoreRows[" + i + "].Checked"].ToString().Equals("True,False")
                                    || formCollection["ScoreRows[" + i + "].Checked"].ToString().Equals("TRUE,FALSE"))
                        {
                            // Mark the row as 'Checked'
                            rowForSavingScore.Checked = true;
                        }

                        rowForSavingScore.LevelID = decimal.Parse(formCollection["ScoreRows[" + i + "].LevelID"].ToString());
                        rowForSavingScore.strFromValue = formCollection["ScoreRows[" + i + "].FromValue"].ToString();
                        rowForSavingScore.strToValue = formCollection["ScoreRows[" + i + "].ToValue"].ToString();
                        //try
                        //{
                        //    rowForSavingScore.FromValue = decimal.Parse(formCollection["ScoreRows[" + i + "].FromValue"].ToString());
                        //}
                        //catch (Exception)
                        //{
                        //   rowForSavingScore.FromValue = 0;
                        //}

                        //try
                        //{
                        //    rowForSavingScore.ToValue = decimal.Parse(formCollection["ScoreRows[" + i + "].ToValue"].ToString());
                        //}
                        //catch (Exception)
                        //{
                        //    rowForSavingScore.ToValue = 0;
                            
                        //}

                        rowForSavingScore.FixedValue = formCollection["ScoreRows[" + i + "].FixedValue"].ToString();
                        rowForSavingScore.ScoreID = int.Parse(formCollection["ScoreRows[" + i + "].ScoreID"].ToString());

                        // Add the row to the View Model
                        viewModelForSavingScore.ScoreRows.Add(rowForSavingScore);
                    }

                    viewModelForSavingScore.CollateralIndexID = formCollection["CollateralIndexID"].ToString();

                    // Perform saving information changes posted from View
                    string errorLevel = IndividualCollateralIndexScore
                                            .EditMultipleCollateralIndexScore(
                                            FBDModel, viewModelForSavingScore);

                    // The view model after saving information
                    INVCollateralIndexScoreViewModel viewModelAfterUpdating = IndividualCollateralIndexScore
                                                                .CreateViewModelByCollateral(
                                                                FBDModel,
                                                                formCollection["CollateralIndexID"].ToString());
                    // If saving gets error
                    if (errorLevel != null)
                    {
                        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_UPDATE_SCORE, errorLevel);
                        return View(viewModelAfterUpdating);
                    }
                    // If saving gets success
                    else
                    {
                        TempData[Constants.SCC_MESSAGE] = Constants.SCC_UPDATE_SCORE;
                        return View(viewModelAfterUpdating);
                    }
                }
            }
            catch (Exception )
            {

                TempData[Constants.ERR_MESSAGE] = Constants.ERR_UPDATE_SCORE_COMMON;
                return RedirectToAction("Index");
            }

            return View(new INVCollateralIndexScoreViewModel());
        }
    }
}
