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
    public class NFIProportionController : Controller
    {
        //
        // GET: /NFIProportion/
        /// <summary>
        /// Display the list of business industries and corresponding non-financial index proportion
        /// </summary>
        /// <returns>IndexByIndustry View</returns>
        public ActionResult IndexByIndustry()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            // The view model to be exchanged
            NFIProportionViewModel viewModel = new NFIProportionViewModel();
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
                viewModel = BusinessNFIProportionByIndustry.CreateViewModelByIndustry(FBDModel, lstIndustries[0].IndustryID);
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
        /// <param name="formCollection">collection of data posted to Server</param>
        /// <returns>IndexByIndustry View</returns>
        [HttpPost]
        public ActionResult IndexByIndustry(FormCollection formCollection)
        {
            FBDEntities FBDModel = new FBDEntities();

            // If the action posted to Controller is done by selecting drop down list of industries
            try
            {
                if (formCollection["Industry"] != null)
                {
                    if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
                    {
                        return RedirectToAction("Unauthorized", "SYSAuths");
                    }
                    // Create View model with input industry selected from drop down list
                    NFIProportionViewModel viewModelForSelectingIndustries = BusinessNFIProportionByIndustry
                                                                            .CreateViewModelByIndustry(
                                                                                    FBDModel,
                                                                                    formCollection["Industry"].ToString());

                    return View(viewModelForSelectingIndustries);
                }
            }
            catch (Exception)
            {
                // Display error message when displaying information
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_INDUSTRY); ;
                return RedirectToAction("IndexByIndustry");
            }

            // If the action posted to Controller is Saving information from View
            try
            {
                if (formCollection["Save"] != null)
                {
                    if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
                    {
                        return RedirectToAction("Unauthorized", "SYSAuths");
                    }
                    NFIProportionViewModel viewModelForSavingProportion = new NFIProportionViewModel();

                    // With each non-financial index row in the list posted from View
                    for (int i = 0; i < int.Parse(formCollection["NumberOfProportionRows"].ToString()); i++)
                    {
                        // Create new row
                        NFIProportionRowViewModel rowForSavingProportion = new NFIProportionRowViewModel();

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
                                throw new Exception();
                                //rowForSavingProportion.Proportion = 0;
                            }
                        }

                        // Assign the proportion ID to the row
                        // As default, the proportion ID is -1, and if the row exists in BusinessNFIProportionIndustry
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
                    string errorIndex = BusinessNFIProportionByIndustry
                                                    .EditMultipleNFIProportionByIndustry(FBDModel,
                                                                                          viewModelForSavingProportion);

                    NFIProportionViewModel viewModelAfterEditing = BusinessNFIProportionByIndustry
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
                        int temp = BusinessNFIProportionCalculated.UpdateNFIProportionCalculatedByIndustry
                                                                            (FBDModel, formCollection["IndustryID"].ToString());

                        if (temp == 0)
                        {
                            // Display error message when updating
                            TempData[Constants.ERR_MESSAGE] = Constants.ERR_UPDATE_PROPORTION_CALCULATED;

                            return View(viewModelAfterEditing);
                        }

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
                return RedirectToAction("IndexByIndustry");
            }

            return View(new NFIProportionViewModel());
        }

        //
        // GET: /FIProportion/
        /// <summary>
        /// Display the list of business types and corresponding non-financial index proportion
        /// </summary>
        /// <returns>IndexByType View</returns>
        public ActionResult IndexByType()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            // The view model to be exchanged
            NFIProportionViewModel viewModel = new NFIProportionViewModel();
            try
            {
                // The list of business types to be binded to drop down list
                List<BusinessTypes> lstTypes = new List<BusinessTypes>();

                lstTypes = BusinessTypes.SelectTypes();

                // If there is no type available in the system
                // then return empty-data View
                if (lstTypes.Count() < 1)
                    return View(viewModel);

                // Create View model with the drop down list displays the first type of the list
                viewModel = BusinessNFIProportionByType.CreateViewModelByType(FBDModel, lstTypes[0].TypeID);
            }
            catch (Exception)
            {
                // Display error message
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_TYPE); ;
                return View(viewModel);
            }

            return View(viewModel);
        }

        /// <summary>
        /// Perform actions posted from View
        /// There are two actions available:
        /// 1. Choose a type to display information
        /// 2. Saving information
        /// </summary>
        /// <param name="formCollection">collection of data posted to Server</param>
        /// <returns>IndexByType View</returns>
        [HttpPost]
        public ActionResult IndexByType(FormCollection formCollection)
        {
            FBDEntities FBDModel = new FBDEntities();

            // If the action posted to Controller is done by selecting drop down list of business types
            try
            {
                if (formCollection["Type"] != null)
                {
                    if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
                    {
                        return RedirectToAction("Unauthorized", "SYSAuths");
                    }
                    // Create View model with input type selected from drop down list
                    NFIProportionViewModel viewModelForSelectingTypes = BusinessNFIProportionByType
                                                                            .CreateViewModelByType(
                                                                                    FBDModel,
                                                                                    formCollection["Type"].ToString());

                    return View(viewModelForSelectingTypes);
                }
            }
            catch (Exception)
            {
                // Display error message when displaying information
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_TYPE); ;
                return RedirectToAction("IndexByType");
            }

            // If the action posted to Controller is Saving information from View
            try
            {
                if (formCollection["Save"] != null)
                {
                    if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
                    {
                        return RedirectToAction("Unauthorized", "SYSAuths");
                    }
                    NFIProportionViewModel viewModelForSavingProportion = new NFIProportionViewModel();

                    // With each non-financial index row in the list posted from View
                    for (int i = 0; i < int.Parse(formCollection["NumberOfProportionRows"].ToString()); i++)
                    {
                        // Create new row
                        NFIProportionRowViewModel rowForSavingProportion = new NFIProportionRowViewModel();

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
                                throw new Exception();
                                //rowForSavingProportion.Proportion = 0;
                            }
                        }

                        // Assign the proportion ID to the row
                        // As default, the proportion ID is -1, and if the row exists in BusinessNFIProportionIndustry
                        // table, the proportion ID will be assigned new integer value
                        rowForSavingProportion.ProportionID = int.Parse(
                                                        formCollection["ProportionRows[" + i + "].ProportionID"].ToString());

                        // Add the row to the view model
                        viewModelForSavingProportion.ProportionRows.Add(rowForSavingProportion);
                    }

                    // Get type ID from View
                    viewModelForSavingProportion.TypeID = formCollection["TypeID"].ToString();

                    // Saving the information with input is View Model created above
                    // then return the error index
                    string errorIndex = BusinessNFIProportionByType
                                                    .EditMultipleNFIProportionByType(FBDModel,
                                                                                          viewModelForSavingProportion);

                    NFIProportionViewModel viewModelAfterEditing = BusinessNFIProportionByType
                                                                            .CreateViewModelByType(
                                                                            FBDModel,
                                                                            formCollection["TypeID"].ToString());

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
                        int temp = BusinessNFIProportionCalculated.UpdateNFIProportionCalculatedByType(
                                                                            FBDModel, formCollection["TypeID"].ToString());

                        if (temp == 0)
                        {
                            // Display error message when updating
                            TempData[Constants.ERR_MESSAGE] = Constants.ERR_UPDATE_PROPORTION_CALCULATED;

                            return View(viewModelAfterEditing);
                        }

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
                return RedirectToAction("IndexByType");
            }

            return View(new NFIProportionViewModel());
        }
    }
}
