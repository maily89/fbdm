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
    public class FIProportionController : Controller
    {
        //
        // GET: /FIProportion/
        /// <summary>
        /// Display the list of business industries and corresponding financial index proportion
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            FBDEntities FBDModel = new FBDEntities();

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
                viewModel = BusinessFinancialIndexProportion.CreateViewModelByIndustry(FBDModel, lstIndustries[0].IndustryID);
            }
            catch (Exception)
            {
                // Display error message
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_INDUSTRY);
                return View(viewModel);
            }

            return View(viewModel);
        }

        /// <summary>
        /// Perform actions posted from View
        /// There are two actions available:
        /// 1. Choose an industry to display information
        /// 2. Saving information
        /// </summary>
        /// <param name="formCollection">form Collection of data posted from Client side</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            FBDEntities FBDModel = new FBDEntities();

            // If the action posted to Controller is done by selecting drop down list of industries
            try
            {
                if (formCollection["Industry"] != null)
                {
                    // Create View model with input industry selected from drop down list
                    FIProportionViewModel viewModelForSelectingIndustries = BusinessFinancialIndexProportion
                                                                            .CreateViewModelByIndustry(
                                                                                    FBDModel, 
                                                                                    formCollection["Industry"].ToString());

                    return View(viewModelForSelectingIndustries);
                }
            }
            catch (Exception)
            {
                // Display error message when displaying information
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_INDUSTRY);
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
                                                    .EditMultipleFinancialIndexProportion(FBDModel, 
                                                                                          viewModelForSavingProportion);

                    FIProportionViewModel viewModelAfterEditing = BusinessFinancialIndexProportion
                                                                            .CreateViewModelByIndustry(
                                                                            FBDModel,
                                                                            formCollection["IndustryID"].ToString());

                    // If some errors occur
                    if (errorIndex != null)
                    {
                        // Display error message when updating
                        TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_UPDATE_PROPORTION, errorIndex);
                        
                        return View(viewModelAfterEditing);
                    }
                    // If no error occurs
                    else
                    {
                        // Display successful message
                        TempData[Constants.SCC_MESSAGE] = Constants.SCC_UPDATE_PROPORTION;
                        return View(viewModelAfterEditing);
                    }
                }
            }
            catch (Exception)
            {
                // Error message when handling in Controller
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_CONTROLLER_PARSING;
                return RedirectToAction("Index");
            }

            return View(new FIProportionViewModel());
        }
    }
}
