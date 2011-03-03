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
            FBDEntities FBDModel = new FBDEntities();

            List<BusinessFinancialIndex> lstFinancialIndex = new List<BusinessFinancialIndex>();

            try
            {
                // Select the list of financial indexes
                lstFinancialIndex = BusinessFinancialIndex.SelectFinancialIndex(FBDModel);

                // If error occurs
                if (lstFinancialIndex == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Dispay error message when displaying financial indexes
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_FINANCIAL_INDEX);
                return View(lstFinancialIndex);
            }
            // If there is no error, displaying the list of financial index
            return View(lstFinancialIndex);
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
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business financial index that has been inputted
                    int result = BusinessFinancialIndex.AddFinancialIndex(FBDModel, businessFinancialIndex);

                    if (result == 1)
                    {
                        // Display successful message when adding new financial index
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD,
                                                                        Constants.BUSINESS_FINANCIAL_INDEX);
                        return RedirectToAction("Index");
                    }
                } 
                throw new Exception();
            }
            catch(Exception)
            {
                // Display error message when adding new financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_FINANCIAL_INDEX);
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
            FBDEntities FBDModel = new FBDEntities();

            BusinessFinancialIndex financialIndex = null;

            try
            {
                // Select the financial index to be editted
                financialIndex = BusinessFinancialIndex.SelectFinancialIndexByID(FBDModel, id);

                if (financialIndex == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when selecting financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_FINANCIAL_INDEX);
                return View(new BusinessFinancialIndex());
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
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Edit financial index that has been inputted
                    int result = BusinessFinancialIndex.EditFinancialIndex(FBDModel, businessFinancialIndex);

                    if (result == 1)
                    {
                        // Display successful message after editting the financial index
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST,
                                                                        Constants.BUSINESS_FINANCIAL_INDEX,
                                                                        id);
                        return RedirectToAction("Index");
                    }
                }
                
                throw new Exception();                
            }
            catch
            {
                // Display error message after editting the financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_FINANCIAL_INDEX);
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
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // Delete the selected financial index
                int result = BusinessFinancialIndex.DeleteFinancialIndex(FBDModel, id);

                if (result == 1)
                {
                    // Display successful message after deleting the financial index
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_FINANCIAL_INDEX);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message after deleting the financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_FINANCIAL_INDEX);
                return RedirectToAction("Index");
            }
        }
    }
}
