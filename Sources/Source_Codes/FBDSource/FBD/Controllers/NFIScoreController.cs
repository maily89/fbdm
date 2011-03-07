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
    public class NFIScoreController : Controller
    {
        //
        // GET: /NFIScore/
        /// <summary>
        /// Display the list of industries, non-financial indexes and corresponding non-financial index score
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            NFIScoreViewModel viewModel = new NFIScoreViewModel();
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                List<BusinessIndustries> lstIndustries = BusinessIndustries.SelectIndustries();
                List<BusinessNonFinancialIndex> lstNonFinancialIndexes = BusinessNonFinancialIndex.SelectNonFinancialIndex(FBDModel);

                // If there is no information to choose from the drop down list, return empty-data View
                if (lstIndustries.Count() < 1 || lstNonFinancialIndexes.Count() < 1)
                {
                    return View(viewModel);
                }

                // Create View model with the selected industry, non-financial index
                viewModel = BusinessNonFinancialIndexScore.CreateViewModelByIndustryByNonFinancialIndex(FBDModel,
                                         lstIndustries[0].IndustryID, lstNonFinancialIndexes[0].IndexID);
            }
            catch (Exception)
            {
                // Return View with error message
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_DISPLAY_NFISCORE;
                return View(viewModel);
            }

            return View(viewModel);
        }

        /// <summary>
        /// Perform actions posted from View
        /// There are two actions available:
        /// 1. Choose an industry or non-financial index to display information
        /// 2. Saving information
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
                if (formCollection["Industry"] != null
                        || formCollection["NonFinancialIndex"] != null)
                {
                    // ... then create view model for displaying information
                    NFIScoreViewModel viewModelForDisplayingInformation = BusinessNonFinancialIndexScore
                                                            .CreateViewModelByIndustryByNonFinancialIndex(
                                                            FBDModel,
                                                            formCollection["Industry"].ToString(),
                                                            formCollection["NonFinancialIndex"].ToString());

                    return View(viewModelForDisplayingInformation);
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_DISPLAY_NFISCORE;
                return RedirectToAction("Index");
            }

            // If the action posted to Controller is Saving information changes
            try
            {
                if (formCollection["Save"] != null)
                {
                    NFIScoreViewModel viewModelForSavingScore = new NFIScoreViewModel();

                    // Iterate all the rows of non-financial index proportion list
                    for (int i = 0; i < int.Parse(formCollection["NumberOfScoreRows"].ToString()); i++)
                    {
                        NFIScoreRowViewModel rowForSavingScore = new NFIScoreRowViewModel();

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

                    viewModelForSavingScore.IndustryID = formCollection["IndustryID"].ToString();
                    viewModelForSavingScore.IndexID = formCollection["IndexID"].ToString();

                    // Perform saving information changes posted from View
                    string errorLevel = BusinessNonFinancialIndexScore
                                            .EditMultipleNonFinancialIndexScore(
                                            FBDModel, viewModelForSavingScore);

                    // The view model after saving information
                    NFIScoreViewModel viewModelAfterUpdating = BusinessNonFinancialIndexScore
                                                                .CreateViewModelByIndustryByNonFinancialIndex(
                                                                FBDModel,
                                                                formCollection["IndustryID"].ToString(),
                                                                formCollection["IndexID"].ToString());
                    // If saving gets error
                    if (errorLevel != null)
                    {
                        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_UPDATE_NFISCORE, errorLevel);
                        return View(viewModelAfterUpdating);
                    }
                    // If saving gets success
                    else
                    {
                        TempData[Constants.SCC_MESSAGE] = Constants.SCC_UPDATE_NFISCORE;
                        return View(viewModelAfterUpdating);
                    }
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_POST_NFISCORE;
                return RedirectToAction("Index");
            }

            return View(new NFIScoreViewModel());
        }
    }
}
