using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class INVBasicLevelsController : Controller
    {
        //
        // GET: /INVBasicLevels/

        /// <summary>
        /// Use FIBasicIndexLevels class to select all the Basic index levels
        /// in the table Business.BasicIndexLevels then display to the [Index] View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<IndividualBasicIndexLevels> lstBasicIndexLevels = null;
            try
            {
                lstBasicIndexLevels = IndividualBasicIndexLevels.SelectBasicIndexLevels();
                if (lstBasicIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_BASIC_LEVEL_INDEX); ;
                return View(lstBasicIndexLevels);
            }
            return View(lstBasicIndexLevels);
        }

        //
        // GET: /INVBasicLevels/Create

        /// <summary>
        /// Forward to Add View
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            TempData[Constants.SCC_MESSAGE] = null;
            return View();
        }

        //
        // POST: /INVBasicLevels/Create
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Check user right and input validation.
        /// 3. Use Logic class to insert new Basic Index level
        /// 4. Redirect to [Index] View with label displaying: "A new Basic Index Level has been added successfully"
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IndividualBasicIndexLevels individualBasicIndexLevels)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business Basic index level that has been inputted
                    int result = IndividualBasicIndexLevels.AddBasicIndexLevels(individualBasicIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message when adding new Basic index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.INV_BASIC_LEVEL_INDEX); ;
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new Basic index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.INV_BASIC_LEVEL_INDEX);
                return View(individualBasicIndexLevels);
            }
        }

        //
        // GET: /INVBasicLevels/Edit/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Basic Index Level from Business.BasicIndexLevels table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">id of the Basic index level to be editted</param>
        /// <returns></returns>
        public ActionResult Edit(decimal id)
        {
            IndividualBasicIndexLevels BasicIndexLevels = null;

            try
            {
                // Select the Basic index level to be editted
                BasicIndexLevels = IndividualBasicIndexLevels.SelectBasicIndexLevelsByID(id);

                if (BasicIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when selecting Basic index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_BASIC_LEVEL_INDEX);
                return View(BasicIndexLevels);
            }

            // Display Basic index level to be editted
            return View(BasicIndexLevels);
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Basic Index Level with ID selected in DB
        /// 3. Display in [Index] view with label displaying: "The Basic Index Level with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="IndividualBasicIndexLevels">the Basic index to be editted</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(decimal id, IndividualBasicIndexLevels individualBasicIndexLevels)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Edit Basic index level that has been inputted
                    int result = IndividualBasicIndexLevels.EditBasicIndexLevels(individualBasicIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message after editting the Basic index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_BASIC_LEVEL_INDEX, individualBasicIndexLevels.LevelID);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch
            {
                // Display error message after editting the Basic index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_BASIC_LEVEL_INDEX);
                return View(individualBasicIndexLevels);
            }
        }

        //
        // GET: /INVBasicLevels/Delete/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Basic Index Level with selected ID from the Business.BasicIndexLevels table
        /// 3. Back to [Index] view with label displaying: "A Basic Index Level has been deleted successfully" 
        /// </summary>
        /// <param name="id">id of the Basic index level to be deleted</param>
        /// <returns></returns>
        public ActionResult Delete(decimal id)
        {
            try
            {
                // Delete the selected Basic index level
                int result = IndividualBasicIndexLevels.DeleteBasicIndexLevels(id);

                if (result == 1)
                {
                    // Display successful message after deleting the Basic index level
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.INV_BASIC_LEVEL_INDEX);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message after deleting the Basic index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.INV_BASIC_LEVEL_INDEX);
                return RedirectToAction("Index");
            }
        }
    }
}
