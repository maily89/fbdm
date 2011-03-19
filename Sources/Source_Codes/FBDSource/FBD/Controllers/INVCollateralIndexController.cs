using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class INVCollateralIndexController : Controller
    {
        //
        // GET: /INVCollateralIndex/

        public ActionResult Index()
        {
            List<IndividualCollateralIndex> lstCollateralIndex = null;
            try
            {
                // Select the list of financial indexes
                lstCollateralIndex = IndividualCollateralIndex.SelectCollateralIndex();

                // If error occurs
                if (lstCollateralIndex == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_COLLATERAL_INDEX);
                return View(lstCollateralIndex);
            }
            // If there is no error, displaying the list of financial index
            return View(lstCollateralIndex);
        }


        //
        // GET: /INVCollateralIndex/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /INVCollateralIndex/Create

        [HttpPost]
        public ActionResult Create(IndividualCollateralIndex individualCollateralIndex)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business financial index that has been inputted
                    int result = IndividualCollateralIndex.AddCollateralIndex(individualCollateralIndex);
                    if (result == 1)
                    {
                        // Display successful message when adding new financial index
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.INV_COLLATERAL_INDEX);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.INV_COLLATERAL_INDEX);
                return View(individualCollateralIndex);
            }
        }

        //
        // GET: /INVCollateralIndex/Edit/5

        public ActionResult Edit(string id)
        {
            IndividualCollateralIndex model = null;
            try
            {
                model = IndividualCollateralIndex.SelectCollateralIndexByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_COLLATERAL_INDEX);
            }
            return View(model);
        }

        //
        // POST: /INVCollateralIndex/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, IndividualCollateralIndex individualCollateralIndex)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = IndividualCollateralIndex.EditCollateralIndex(individualCollateralIndex);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_COLLATERAL_INDEX, individualCollateralIndex.IndexID);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_COLLATERAL_INDEX);
                return View(individualCollateralIndex);
            }
        }


        //
        // GET: /INVCollateralIndex/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                int result = IndividualCollateralIndex.DeleteCollateralIndex(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.INV_COLLATERAL_INDEX);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.INV_COLLATERAL_INDEX);
                return RedirectToAction("Index");
            }

        }
    }
}
