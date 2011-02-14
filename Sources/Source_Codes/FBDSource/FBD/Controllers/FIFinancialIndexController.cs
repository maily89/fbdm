using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class FIFinancialIndexController : Controller
    {
        //
        // GET: /FIFinancialIndex/

        /// <summary>
        /// Use FIFinancialIndexLogic class to select all the financial indexes 
        /// in the table Business.FinancialIndex then display to the [Index] View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Select the list of financial indexes
            var lstFinancialIndex = BusinessFinancialIndex.SelectFinancialIndex();

            // If error occurs
            if (lstFinancialIndex == null)
            {
                // Dispay error message when displaying financial indexes
                TempData["Message"] = Constants.ERR_INDEX_FI_FINANCIAL_INDEX;
                return View(lstFinancialIndex);
            }

            // If there is no error, displaying the list of financial index
            return View(lstFinancialIndex);
        }

        //
        // GET: /FIFinancialIndex/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FIFinancialIndex/Create

        /// <summary>
        /// Forward to Add View
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /FIFinancialIndex/Create
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Check user right and input validation.
        /// 3. Use Logic class to insert new Financial Index
        /// 4. Redirect to [Index] View with label displaying: "A new Financial Index has been added successfully"
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(BusinessFinancialIndex businessFinancialIndex)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business financial index that has been inputted
                    BusinessFinancialIndex.AddFinancialIndex(businessFinancialIndex);
                }
                else throw new Exception();

                // Display successful message when adding new financial index
                TempData["Message"] = Constants.SCC_ADD_FI_FINANCIAL_INDEX;
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                // Display error message when adding new financial index
                TempData["Message"] = Constants.ERR_ADD_POST_FI_FINANCIAL_INDEX;
                return View(businessFinancialIndex);
            }
        }

        //
        // GET: /FIFinancialIndex/Edit/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Financial Index from Business.FinancialIndex table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">id of the financial index to be editted</param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            BusinessFinancialIndex financialIndex = null;

            try
            {
                // Select the financial index to be editted
                financialIndex = BusinessFinancialIndex.SelectFinancialIndexByID(id);
            }
            catch (Exception)
            {
                // Display error message when selecting financial index
                TempData["Message"] = Constants.ERR_EDIT_FI_FINANCIAL_INDEX;
                return View(financialIndex);
            }

            // Display financial index to be editted
            return View(financialIndex);
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Financial Index with ID selected in DB
        /// 3. Display in [Index] view with label displaying: "The Financial Index with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="businessFinancialIndex">the financial index to be editted</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessFinancialIndex businessFinancialIndex)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Edit financial index that has been inputted
                    BusinessFinancialIndex.EditFinancialIndex(businessFinancialIndex);
                }
                else throw new Exception();

                // Display successful message after editting the financial index
                TempData["Message"] = Constants.SCC_EDIT_POST_FI_FINANCIAL_INDEX_1 + id
                                      + Constants.SCC_EDIT_POST_FI_FINANCIAL_INDEX_2;
                return RedirectToAction("Index");
            }
            catch
            {
                // Display error message after editting the financial index
                TempData["Message"] = Constants.ERR_EDIT_POST_FI_FINANCIAL_INDEX;
                return View(businessFinancialIndex);
            }
        }

        //
        // GET: /FIFinancialIndex/Delete/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Financial Index with selected ID from the Business.FinancialIndex table
        /// 3. Back to [Index] view with label displaying: "A Financial Index has been deleted successfully" 
        /// </summary>
        /// <param name="id">id of the financial index to be deleted</param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            try
            {
                // Delete the selected financial index
                BusinessFinancialIndex.DeleteFinancialIndex(id);

                // Display successful message after deleting the financial index
                TempData["Message"] = Constants.SCC_DELETE_FI_FINANCIAL_INDEX;
                RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Display error message after deleting the financial index
                TempData["Message"] = Constants.ERR_DELETE_FI_FINANCIAL_INDEX;
                RedirectToAction("Index");
            }

            return View();
        }
    }
}
