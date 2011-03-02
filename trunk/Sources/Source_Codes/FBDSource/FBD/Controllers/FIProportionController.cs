using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;

namespace FBD.Controllers
{
    public class FIProportionController : Controller
    {
        //
        // GET: /FIProportion/

        public ActionResult Index()
        {
            // The view model to be exchanged
            FIProportionViewModel viewModel = new FIProportionViewModel();
            try
            {
                // The list of business industries to be binded to drop down list
                List<BusinessIndustries> lstIndustries = new List<BusinessIndustries>();

                lstIndustries = BusinessIndustries.SelectIndustries();

                // If there is no industries available in the system
                // then return empty-data View
                if (lstIndustries.Count() < 1)
                    return View(viewModel);

                // Create View model with the drop down list displays the first industry of the list
                viewModel = BusinessFinancialIndexProportion.CreateViewModelByIndustry(lstIndustries[0].IndustryID);
            }
            catch (Exception)
            {
                // Display error message
                TempData["Message"] = CommonUtilities.Constants.ERR_DISPLAY_FIPROPORTION;
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            // If the action posted to Controller is done by selecting drop down list of industries
            try
            {
                if (formCollection["Industry"] != null)
                {
                    // Create View model with input industry selected from drop down list
                    FIProportionViewModel viewModelForSelectingIndustries = BusinessFinancialIndexProportion
                                                            .CreateViewModelByIndustry(formCollection["Industry"].ToString());

                    return View(viewModelForSelectingIndustries);
                }
            }
            catch (Exception)
            {
                // Display error message when displaying information
                TempData["Message"] = CommonUtilities.Constants.ERR_DISPLAY_FIPROPORTION;
                return RedirectToAction("Index");
            }

            // If the action posted to Controller is Saving information from View
            try
            {
                if (formCollection["Save"] != null)
                {
                    FIProportionViewModel viewModelForSavingProportion = new FIProportionViewModel();

                    // With each financial index row in the list posted from View
                    for (int i = 0; i < int.Parse(formCollection["NumberOfProportionRows"].ToString()); i++)
                    {
                        // Create new row
                        FIProportionRowViewModel rowForSavingProportion = new FIProportionRowViewModel();
                                                
                        if (formCollection["ProportionRows[" + i + "].Checked"] != null)
                        {
                            // If the row [i] is checked by the checkbox
                            if (formCollection["ProportionRows[" + i + "].Checked"].ToString().Equals("true,false")
                                || formCollection["ProportionRows[" + i + "].Checked"].ToString().Equals("True,False")
                                    || formCollection["ProportionRows[" + i + "].Checked"].ToString().Equals("TRUE,FALSE"))
                            {
                                // Mark the row as 'Checked'
                                rowForSavingProportion.Checked = true;
                            }
                        }

                        // Assign the index ID to the row
                        rowForSavingProportion.IndexID = formCollection["ProportionRows[" + i + "].IndexID"].ToString();

                        // Assign the index Name to the row
                        rowForSavingProportion.IndexName = formCollection["ProportionRows[" + i + "].IndexName"].ToString();

                        // Assign the proportion value to the row
                        if (formCollection["ProportionRows[" + i + "].Proportion"] != null)
                        {
                            try
                            {
                                rowForSavingProportion.Proportion = decimal.
                                                        Parse(formCollection["ProportionRows[" + i + "].Proportion"].ToString());

                            }
                            catch (Exception)
                            {
                                rowForSavingProportion.Proportion = 0;
                            }
                        }

                        // Assign the proportion ID to the row
                        // As default, the proportion ID is -1, and if the row exists in BusinessFinancialIndexProportion
                        // table, the proportion ID will be assigned new integer value
                        rowForSavingProportion.ProportionID = int.Parse(
                                                        formCollection["ProportionRows[" + i + "].ProportionID"].ToString());

                        // Add the row to the view model
                        viewModelForSavingProportion.ProportionRows.Add(rowForSavingProportion);
                    }

                    // Get industry ID from View
                    viewModelForSavingProportion.IndustryID = formCollection["IndustryID"].ToString();

                    // Saving the information with input is View Model created above
                    // then return the error index
                    string errorIndex = BusinessFinancialIndexProportion
                                                    .EditFinancialIndexProportion(viewModelForSavingProportion);

                    // If some errors occur
                    if (errorIndex != null)
                    {
                        // Display error message when updating
                        TempData["Message"] = string.Format(CommonUtilities.Constants.ERR_UPDATE_FIPROPORTION, errorIndex);
                        FIProportionViewModel viewModelAfterError = BusinessFinancialIndexProportion
                                                            .CreateViewModelByIndustry(formCollection["IndustryID"].ToString());
                        return View(viewModelAfterError);
                    }
                    // If no error occurs
                    else
                    {
                        // Display successful message
                        TempData["Message"] = CommonUtilities.Constants.SCC_UPDATE_FIPROPORTION;
                        FIProportionViewModel viewModelAfterSuccess = BusinessFinancialIndexProportion
                                                            .CreateViewModelByIndustry(formCollection["IndustryID"].ToString());
                        return View(viewModelAfterSuccess);
                    }
                }
            }
            catch (Exception)
            {
                // Error message when handling in Controller
                TempData["Message"] = CommonUtilities.Constants.ERR_POST_FIPROPORTION;
                return RedirectToAction("Index");
            }

            return View(new FIProportionViewModel());
        }
    }
}
