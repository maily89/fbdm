﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class FILevelsController : Controller
    {
        //
        // GET: /FILevels/

        /// <summary>
        /// Use FIFinancialIndexLevels class to select all the financial index levels
        /// in the table Business.FinancialIndexLevels then display to the [Index] View
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            List<BusinessFinancialIndexLevels> lstFinancialIndexLevels = new List<BusinessFinancialIndexLevels>();

            try
            {
                // Select the list of financial index levels
                lstFinancialIndexLevels = BusinessFinancialIndexLevels.SelectFinancialIndexLevels(FBDModel);

                // If error occurs
                if (lstFinancialIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when displaying financial index levels
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_FINANCIAL_INDEX_LEVEL);
                return View(lstFinancialIndexLevels);
            }
            // If there is no error, displaying the list of financial index levels
            return View(lstFinancialIndexLevels);
        }

        //
        // GET: /FILevels/Create

        /// <summary>
        /// Forward to Add View
        /// </summary>
        /// <returns>Add View</returns>
        public ActionResult Add()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            return View();
        }

        //
        // POST: /FILevels/Create
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Check user right and input validation.
        /// 3. Use Logic class to insert new Financial Index level
        /// 4. Redirect to [Index] View with label displaying: "A new Financial Index Level has been added successfully"
        /// </summary>
        /// <param name="businessFinancialIndexLevels">The businessFinancialIndexLevels to be inserted</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Add(BusinessFinancialIndexLevels businessFinancialIndexLevels)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business financial index level that has been inputted
                    int result = BusinessFinancialIndexLevels.AddFinancialIndexLevels(FBDModel, businessFinancialIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message when adding new financial index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD,
                                                                        Constants.BUSINESS_FINANCIAL_INDEX_LEVEL);
                        return RedirectToAction("Index");
                    }
                }
                
                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_FINANCIAL_INDEX_LEVEL);
                return View(businessFinancialIndexLevels);
            }
        }

        //
        // GET: /FILevels/Edit/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Financial Index Level from Business.FinancialIndexLevels table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">id of the financial index level to be editted</param>
        /// <returns>Edit View</returns>
        public ActionResult Edit(decimal id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            BusinessFinancialIndexLevels financialIndexLevels = null;

            try
            {
                // Select the financial index level to be editted
                financialIndexLevels = BusinessFinancialIndexLevels.SelectFinancialIndexLevelsByID(id, FBDModel);

                if (financialIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when selecting financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_FINANCIAL_INDEX_LEVEL);
                return View(new BusinessFinancialIndexLevels());
            }

            // Display financial index level to be editted
            return View(financialIndexLevels);
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Financial Index Level with ID selected in DB
        /// 3. Display in [Index] view with label displaying: "The Financial Index Level with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id">the id of the financial index level</param>
        /// <param name="businessFinancialIndexLevels">the financial index to be editted</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Edit(decimal id, BusinessFinancialIndexLevels businessFinancialIndexLevels)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Edit financial index level that has been inputted
                    int result = BusinessFinancialIndexLevels.EditFinancialIndexLevels(FBDModel, businessFinancialIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message after editting the financial index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST,
                                                                        Constants.BUSINESS_FINANCIAL_INDEX_LEVEL,
                                                                        id.ToString());
                        return RedirectToAction("Index");
                    }
                }
                
                throw new Exception();
            }
            catch
            {
                // Display error message after editting the financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_FINANCIAL_INDEX_LEVEL);
                return View(businessFinancialIndexLevels);
            }
        }

        //
        // GET: /FILevels/Delete/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Financial Index Level with selected ID from the Business.FinancialIndexLevels table
        /// 3. Back to [Index] view with label displaying: "A Financial Index Level has been deleted successfully" 
        /// </summary>
        /// <param name="id">id of the financial index level to be deleted</param>
        /// <returns>Index View</returns>
        public ActionResult Delete(decimal id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // Delete the selected financial index level
                int result = BusinessFinancialIndexLevels.DeleteFinancialIndexLevels(FBDModel, id);

                if (result == 1)
                {
                    // Display successful message after deleting the financial index level
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_FINANCIAL_INDEX_LEVEL);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message after deleting the financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_FINANCIAL_INDEX_LEVEL);
                return RedirectToAction("Index");
            }
        }
    }
}
