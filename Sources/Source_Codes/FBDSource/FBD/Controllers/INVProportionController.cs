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
    public class INVProportionController : Controller
    {
        //
        // GET: /INVProportion/
        /// <summary>
        /// Display the list of borrowing purpose and corresponding basic index proportion
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            // The view model to be exchanged
            INVProportionViewModel viewModel = new INVProportionViewModel();
            try
            {
                // The list of borrowing purpose to be binded to drop down list
                List<IndividualBorrowingPurposes> lstborrowingPP = new List<IndividualBorrowingPurposes>();

                lstborrowingPP = IndividualBorrowingPurposes.SelectBorrowingPPList();

                // If there is no industries available in the system
                // then return empty-data View
                if (lstborrowingPP.Count() < 1)
                    return View(viewModel);

                // Create View model with the drop down list displays the first industry of the list
                viewModel = IndividualBasicIndexProportion.CreateViewModelByBorrowingPP(FBDModel, lstborrowingPP[0].PurposeID);
            }
            catch (Exception)
            {
                // Display error message
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_PROPORTION);
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
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            FBDEntities FBDModel = new FBDEntities();

            // If the action posted to Controller is done by selecting drop down list of industries
            try
            {
                if (formCollection["BorrowingPP"] != null)
                {
                    // Create View model with input industry selected from drop down list
                    INVProportionViewModel viewModelForSelectingBorrowingPP = IndividualBasicIndexProportion
                                                                            .CreateViewModelByBorrowingPP(
                                                                                    FBDModel,
                                                                                    formCollection["BorrowingPP"].ToString());

                    return View(viewModelForSelectingBorrowingPP);
                }
            }
            catch (Exception)
            {
                // Display error message when displaying information
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_PROPORTION); ;
                return RedirectToAction("Index");
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
                    INVProportionViewModel viewModelForSavingProportion = new INVProportionViewModel();

                    // With each basic index row in the list posted from View
                    for (int i = 0; i < int.Parse(formCollection["NumberOfProportionRows"].ToString()); i++)
                    {
                        // Create new row
                        INVProportionRowViewModel rowForSavingProportion = new INVProportionRowViewModel();

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
                            rowForSavingProportion.strProportion = formCollection["ProportionRows[" + i + "].Proportion"].ToString();
                            //try
                            //{
                            //    rowForSavingProportion.Proportion = decimal.
                            //                            Parse(formCollection["ProportionRows[" + i + "].Proportion"].ToString());

                            //}
                            //catch (Exception)
                            //{
                            //    rowForSavingProportion.Proportion = 0;
                            //}
                        }

                        // Assign the proportion ID to the row
                        // As default, the proportion ID is -1, and if the row exists in IndividualBasicIndexProportion
                        // table, the proportion ID will be assigned new integer value
                        rowForSavingProportion.ProportionID = int.Parse(
                                                        formCollection["ProportionRows[" + i + "].ProportionID"].ToString());

                        // Add the row to the view model
                        viewModelForSavingProportion.ProportionRows.Add(rowForSavingProportion);
                    }

                    // Get industry ID from View
                    viewModelForSavingProportion.BorrowingPPID = formCollection["BorrowingPPID"].ToString();

                    // Saving the information with input is View Model created above
                    // then return the error index
                    string errorIndex = IndividualBasicIndexProportion
                                                    .EditMultipleBasicIndexProportion(FBDModel,
                                                                                          viewModelForSavingProportion);

                    INVProportionViewModel viewModelAfterEditing = IndividualBasicIndexProportion
                                                                            .CreateViewModelByBorrowingPP(
                                                                            FBDModel,
                                                                            formCollection["BorrowingPPID"].ToString());

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
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_UPDATE_PROPORTION_COMMON;
                return RedirectToAction("Index");
            }

            return View(new INVProportionViewModel());
        }
    }
}
