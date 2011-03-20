using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class NFINonFinancialIndexController : Controller
    {
        //
        // GET: /NFINonFinancialIndex/

        /// <summary>
        /// Use NFINonFinancialIndexLogic class to select all the non financial indexes 
        /// in the table Business.NonFinancialIndex then display to the [Index] View
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            FBDEntities FBDModel = new FBDEntities();

            List<BusinessNonFinancialIndex> lstNonFinancialIndex = new List<BusinessNonFinancialIndex>();

            try
            {
                // Select the list of non financial indexes
                lstNonFinancialIndex = BusinessNonFinancialIndex.SelectNonFinancialIndex(FBDModel);

                // If error occurs
                if (lstNonFinancialIndex == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when displaying non financial indexes
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_NON_FINANCIAL_INDEX);
                return View(lstNonFinancialIndex);
            }
            // If there is no error, displaying the list of non financial index
            return View(lstNonFinancialIndex);
        }

        //
        // GET: /NFINonFinancialIndex/Create

        /// <summary>
        /// Forward to Add View
        /// </summary>
        /// <returns>Add View</returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /NFINonFinancialIndex/Create
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Check user right and input validation.
        /// 3. Use Logic class to insert new Non Financial Index
        /// 4. Redirect to [Index] View with label displaying: "A new Non Financial Index has been added successfully"
        /// </summary>
        /// <param name="businessNonFinancialIndex">The index to be inserted</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Add(BusinessNonFinancialIndex businessNonFinancialIndex)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    if (!StringHelper.IsDigitsNumber(businessNonFinancialIndex.IndexID))
                    {
                        // Display error message when new non-financial index is not valid
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_INVALID_INDEX_ID;
                        return View(businessNonFinancialIndex);
                    }
                    // Add new business non-financial index that has been inputted
                    int result = BusinessNonFinancialIndex.AddNonFinancialIndex(FBDModel, businessNonFinancialIndex);

                    if (result == 1)
                    {
                        // Display successful message when adding new non-financial index
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD,
                                                                        Constants.BUSINESS_NON_FINANCIAL_INDEX);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new non financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_NON_FINANCIAL_INDEX);
                return View(businessNonFinancialIndex);
            }
        }

        //
        // GET: /NFINonFinancialIndex/Edit/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Non Financial Index from Business.NonFinancialIndex table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">id of the non financial index to be editted</param>
        /// <returns>Edit View</returns>
        public ActionResult Edit(string id)
        {
            FBDEntities FBDModel = new FBDEntities();

            BusinessNonFinancialIndex nonFinancialIndex = null;

            try
            {
                // Select the non financial index to be editted
                nonFinancialIndex = BusinessNonFinancialIndex.SelectNonFinancialIndexByID(FBDModel, id);

                if (nonFinancialIndex == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when selecting non financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_NON_FINANCIAL_INDEX);
                return View(new BusinessNonFinancialIndex());
            }

            // Display non financial index to be editted
            return View(nonFinancialIndex);
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Non Financial Index with ID selected in DB
        /// 3. Display in [Index] view with label displaying: "The Non-Financial Index with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id">Id of the index to be edited</param>
        /// <param name="businessNonFinancialIndex">the non-financial index to be editted</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessNonFinancialIndex businessNonFinancialIndex)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Edit non-financial index that has been inputted
                    int result = BusinessNonFinancialIndex.EditNonFinancialIndex(FBDModel, businessNonFinancialIndex);

                    if (result == 1)
                    {
                        // Display successful message after editting the non-financial index
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST,
                                                                        Constants.BUSINESS_NON_FINANCIAL_INDEX,
                                                                        id);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch
            {
                // Display error message after editting the non-financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_NON_FINANCIAL_INDEX);
                return View(businessNonFinancialIndex);
            }
        }

        //
        // GET: /NFINonFinancialIndex/Delete/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Non-Financial Index with selected ID from the Business.NonFinancialIndex table
        /// 3. Back to [Index] view with label displaying: "A Non-Financial Index has been deleted successfully" 
        /// </summary>
        /// <param name="id">id of the non-financial index to be deleted</param>
        /// <returns>Index View</returns>
        public ActionResult Delete(string id)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // Delete the selected non-financial index
                int result = BusinessNonFinancialIndex.DeleteNonFinancialIndex(FBDModel, id);

                if (result == 1)
                {
                    // Display successful message after deleting the non-financial index
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_NON_FINANCIAL_INDEX);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message after deleting the non-financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_NON_FINANCIAL_INDEX);
                return RedirectToAction("Index");
            }
        }
    }
}
