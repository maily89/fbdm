using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class INVCollateralLevelsController : Controller
    {
        //
        // GET: /INVCollateralLevels/

        /// <summary>
        /// Use INVCollateralIndexLevels class to select all the Collateral Index levels
        /// in the table Business.CollateralIndexLevels then display to the [Index] View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<IndividualCollateralIndexLevels> lstCollateralIndexLevels = null;
            try
            {
                lstCollateralIndexLevels = IndividualCollateralIndexLevels.SelectCollateralIndexLevels();
                if (lstCollateralIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_COLL_LEVEL_INDEX); ;
                return View(lstCollateralIndexLevels);
            }
            return View(lstCollateralIndexLevels);
        }

        //
        // GET: /INVCollateralLevels/Create

        /// <summary>
        /// Forward to Add View
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            TempData[Constants.ERR_MESSAGE] = null;
            TempData[Constants.SCC_MESSAGE] = null;
            return View();
        }

        //
        // POST: /INVCollateralLevels/Create
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Check user right and input validation.
        /// 3. Use Logic class to insert new Collateral Index level
        /// 4. Redirect to [Index] View with label displaying: "A new Collateral Index Level has been added successfully"
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IndividualCollateralIndexLevels individualCollateralIndexLevels)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business Collateral Index level that has been inputted
                    int result = IndividualCollateralIndexLevels.AddCollateralIndexLevels(individualCollateralIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message when adding new Collateral Index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.INV_COLL_LEVEL_INDEX); ;
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new Collateral Index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.INV_COLL_LEVEL_INDEX);
                return View(individualCollateralIndexLevels);
            }
        }

        //
        // GET: /INVCollateralLevels/Edit/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Collateral Index Level from Business.CollateralIndexLevels table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">id of the Collateral Index level to be editted</param>
        /// <returns></returns>
        public ActionResult Edit(decimal id)
        {
            IndividualCollateralIndexLevels CollateralIndexLevels = null;

            try
            {
                // Select the Collateral Index level to be editted
                CollateralIndexLevels = IndividualCollateralIndexLevels.SelectCollateralIndexLevelsByID(id);

                if (CollateralIndexLevels == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Display error message when selecting Collateral Index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_COLL_LEVEL_INDEX);
                return View(CollateralIndexLevels);
            }

            // Display Collateral Index level to be editted
            return View(CollateralIndexLevels);
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Collateral Index Level with ID selected in DB
        /// 3. Display in [Index] view with label displaying: "The Collateral Index Level with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="IndividualCollateralIndexLevels">the Collateral Index to be editted</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(decimal id, IndividualCollateralIndexLevels individualCollateralIndexLevels)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Edit Collateral Index level that has been inputted
                    int result = IndividualCollateralIndexLevels.EditCollateralIndexLevels(individualCollateralIndexLevels);

                    if (result == 1)
                    {
                        // Display successful message after editting the Collateral Index level
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_COLL_LEVEL_INDEX, individualCollateralIndexLevels.LevelID);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch
            {
                // Display error message after editting the Collateral Index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_COLL_LEVEL_INDEX);
                return View(individualCollateralIndexLevels);
            }
        }

        //
        // GET: /INVCollateralLevels/Delete/5
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Collateral Index Level with selected ID from the Business.CollateralIndexLevels table
        /// 3. Back to [Index] view with label displaying: "A Collateral Index Level has been deleted successfully" 
        /// </summary>
        /// <param name="id">id of the Collateral Index level to be deleted</param>
        /// <returns></returns>
        public ActionResult Delete(decimal id)
        {
            try
            {
                // Delete the selected Collateral Index level
                int result = IndividualCollateralIndexLevels.DeleteCollateralIndexLevels(id);

                if (result == 1)
                {
                    // Display successful message after deleting the Collateral Index level
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.INV_COLL_LEVEL_INDEX);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message after deleting the Collateral Index level
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.INV_COLL_LEVEL_INDEX);
                return RedirectToAction("Index");
            }
        }
    }
}
