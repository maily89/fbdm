using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class NFILevelsController : Controller
    {
        //
        // GET: /NFILevels/

        /// <summary>
        /// Use NFIFinancialIndexLevels class to select all the non-financial index levels
        /// in the table Business.NonFinancialIndexLevels then display to the [Index] View
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            FBDEntities FBDModel = new FBDEntities();

            List<BusinessNonFinancialIndexLevels> lstNonFinancialIndexLevels = new List<BusinessNonFinancialIndexLevels>();

            try
            {
                // Select the list of non financial index levels
                lstNonFinancialIndexLevels = BusinessNonFinancialIndexLevels.SelectNonFinancialIndexLevels(FBDModel);

                // If error occurs
                if (lstNonFinancialIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when displaying non financial index levels
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL);
                return View(lstNonFinancialIndexLevels);
            }
            // If there is no error, displaying the list of non financial index levels
            return View(lstNonFinancialIndexLevels);
        }

        //
        // GET: /NFILevels/Create

        /// <summary>
        /// Forward to Add View
        /// </summary>
        /// <returns>Add View</returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /NFILevels/Create
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Check user right and input validation.
        /// 3. Use Logic class to insert new Non-Financial Index level
        /// 4. Redirect to [Index] View with label displaying: "A new Non-Financial Index Level has been added successfully"
        /// </summary>
        /// <param name="businessNonFinancialIndexLevels">The level to be inserted</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Add(BusinessNonFinancialIndexLevels businessNonFinancialIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business non-financial index level that has been inputted
                    int result = BusinessNonFinancialIndexLevels.AddNonFinancialIndexLevels(FBDModel, businessNonFinancialIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message when adding new non-financial index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD,
                                                                        Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new non-financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL);
                return View(businessNonFinancialIndexLevels);
            }
        }

        //
        // GET: /NFILevels/Edit/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Non-Financial Index Level from Business.NonFinancialIndexLevels table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">id of the non-financial index level to be editted</param>
        /// <returns>Edit View</returns>
        public ActionResult Edit(decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();

            BusinessNonFinancialIndexLevels nonFinancialIndexLevels = null;

            try
            {
                // Select the non-financial index level to be editted
                nonFinancialIndexLevels = BusinessNonFinancialIndexLevels.SelectNonFinancialIndexLevelsByID(id, FBDModel);

                if (nonFinancialIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when selecting non-financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL);
                return View(new BusinessNonFinancialIndexLevels());
            }

            // Display non-financial index level to be editted
            return View(nonFinancialIndexLevels);
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Non-Financial Index Level with ID selected in DB
        /// 3. Display in [Index] view with label displaying: "The Non-Financial Index Level with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id">id of the level to be edited</param>
        /// <param name="businessNonFinancialIndexLevels">the non-financial index to be editted</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Edit(decimal id, BusinessNonFinancialIndexLevels businessNonFinancialIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Edit non-financial index level that has been inputted
                    int result = BusinessNonFinancialIndexLevels.EditNonFinancialIndexLevels(FBDModel, businessNonFinancialIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message after editting the non-financial index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST,
                                                                        Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL,
                                                                        id.ToString());
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch
            {
                // Display error message after editting the non-financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL);
                return View(businessNonFinancialIndexLevels);
            }
        }

        //
        // GET: /NFILevels/Delete/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Non-Financial Index Level with selected ID from the Business.NonFinancialIndexLevels table
        /// 3. Back to [Index] view with label displaying: "A Non-Financial Index Level has been deleted successfully" 
        /// </summary>
        /// <param name="id">id of the non-financial index level to be deleted</param>
        /// <returns>Index View</returns>
        public ActionResult Delete(decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // Delete the selected non-financial index level
                int result = BusinessNonFinancialIndexLevels.DeleteNonFinancialIndexLevels(FBDModel, id);

                if (result == 1)
                {
                    // Display successful message after deleting the non-financial index level
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message after deleting the non-financial index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_NON_FINANCIAL_INDEX_LEVEL);
                return RedirectToAction("Index");
            }
        }
    }
}
