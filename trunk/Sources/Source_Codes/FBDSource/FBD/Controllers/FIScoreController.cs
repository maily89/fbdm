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
    public class FIScoreController : Controller
    {
        //
        // GET: /FIScore/
        /// <summary>
        /// Display the list of industries, scale types, financial indexes and corresponding financial index score
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FIScoreViewModel viewModel = new FIScoreViewModel();
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                List<BusinessIndustries> lstIndustries = BusinessIndustries.SelectIndustries();
                List<BusinessScales> lstScales = BusinessScales.SelectScales();
                List<BusinessFinancialIndex> lstFinancialIndexes = BusinessFinancialIndex.SelectFinancialLeafIndex(FBDModel);

                // If there is no information to choose from the drop down list, return empty-data View
                if (lstIndustries.Count() < 1 || lstScales.Count() < 1 || lstFinancialIndexes.Count() < 1)
                {
                    return View(viewModel);
                }

                // Create View model with the selected industry, scale and financial index
                viewModel = BusinessFinancialIndexScore.CreateViewModelByIndustryByScaleByFinancialIndex(FBDModel,
                                         lstIndustries[0].IndustryID, lstScales[0].ScaleID, lstFinancialIndexes[0].IndexID);
            }
            catch (Exception)
            {
                // Return View with error message
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_INDUSTRY + " or " 
                                                                                    + Constants.BUSINESS_SCALE);
                return View(viewModel);
            }

            return View(viewModel);
        }

        /// <summary>
        /// Perform actions posted from View
        /// There are two actions available:
        /// 1. Choose an industry, business scale or financial index to display information
        /// 2. Saving information
        /// </summary>
        /// <param name="formCollection">form Collection of data posted from Client side</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            { 
                // If action posted to Controller is choosing item from drop down list...
                if (formCollection["Industry"] != null
                    || formCollection["Scale"] != null
                        || formCollection["FinancialIndex"] != null)
                {
                    if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
                    {
                        return RedirectToAction("Unauthorized", "SYSAuths");
                    }
                    // ... then create view model for displaying information
                    FIScoreViewModel viewModelForDisplayingInformation = BusinessFinancialIndexScore
                                                            .CreateViewModelByIndustryByScaleByFinancialIndex(
                                                            FBDModel,
                                                            formCollection["Industry"].ToString(),
                                                            formCollection["Scale"].ToString(),
                                                            formCollection["FinancialIndex"].ToString());

                    return View(viewModelForDisplayingInformation);                                                            
                }
            }
            catch(Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_INDUSTRY + " or "
                                                                                    + Constants.BUSINESS_SCALE);
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
                    FIScoreViewModel viewModelForSavingScore = new FIScoreViewModel();

                    // Iterate all the rows of financial index proportion list
                    for (int i = 0; i < int.Parse(formCollection["NumberOfScoreRows"].ToString()); i++)
                    {
                        FIScoreRowViewModel rowForSavingScore = new FIScoreRowViewModel();

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
                            throw new Exception();
                            //rowForSavingScore.FromValue = 0;
                        }

                        try
                        {
                            rowForSavingScore.ToValue = decimal.Parse(formCollection["ScoreRows[" + i + "].ToValue"].ToString());
                        }
                        catch (Exception)
                        {
                            throw new Exception();
                            //rowForSavingScore.ToValue = 0;
                        }

                        rowForSavingScore.FixedValue = formCollection["ScoreRows[" + i + "].FixedValue"].ToString();
                        rowForSavingScore.ScoreID = int.Parse(formCollection["ScoreRows[" + i + "].ScoreID"].ToString());

                        // Add the row to the View Model
                        viewModelForSavingScore.ScoreRows.Add(rowForSavingScore);
                    }

                    viewModelForSavingScore.IndustryID = formCollection["IndustryID"].ToString();
                    viewModelForSavingScore.ScaleID = formCollection["ScaleID"].ToString();
                    viewModelForSavingScore.IndexID = formCollection["IndexID"].ToString();

                    // Perform saving information changes posted from View
                    string errorLevel = BusinessFinancialIndexScore
                                            .EditMultipleFinancialIndexScore(
                                            FBDModel, viewModelForSavingScore);

                    // The view model after saving information
                    FIScoreViewModel viewModelAfterUpdating = BusinessFinancialIndexScore
                                                                .CreateViewModelByIndustryByScaleByFinancialIndex(
                                                                FBDModel,
                                                                formCollection["IndustryID"].ToString(),
                                                                formCollection["ScaleID"].ToString(),
                                                                formCollection["IndexID"].ToString());
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
            catch(Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_CONTROLLER_PARSING;
                return RedirectToAction("Index");
            }

            return View(new FIScoreViewModel());
        }
    }
}
