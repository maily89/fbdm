using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class INVBasicIndexController : Controller
    {
        //
        // GET: /INVBasicIndex/

        public ActionResult Index()
        {
            List<IndividualBasicIndex> lstBasicIndex = null;
            try
            {
                // Select the list of financial indexes
                lstBasicIndex = IndividualBasicIndex.SelectBasicIndex();

                // If error occurs
                if (lstBasicIndex == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_INDEX, Constants.INV_BASIC_INDEX);
                return View(lstBasicIndex);
            }
            // If there is no error, displaying the list of financial index
            return View(lstBasicIndex);
        }

       
        //
        // GET: /INVBasicIndex/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /INVBasicIndex/Create

        [HttpPost]
        public ActionResult Create(IndividualBasicIndex individualBasicIndex)
        {
            try
            {
                // If there is no error from client
                if (ModelState.IsValid)
                {
                    // Add new business financial index that has been inputted
                    int result = IndividualBasicIndex.AddBasicIndex(individualBasicIndex);
                    if (result == 1)
                    {
                        // Display successful message when adding new financial index
                        TempData["Message"] = string.Format(Constants.SCC_ADD, Constants.INV_BASIC_INDEX);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new financial index
                TempData["Message"] = string.Format(Constants.ERR_ADD_POST, Constants.INV_BASIC_INDEX);
                return View(individualBasicIndex);
            }
        }
        
        //
        // GET: /INVBasicIndex/Edit/5

        public ActionResult Edit(string id)
        {
            IndividualBasicIndex model = null;
            try
            {
                model = IndividualBasicIndex.SelectBasicIndexByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_EDIT, Constants.INV_BASIC_INDEX);
            }
            return View(model);
        }

        //
        // POST: /INVBasicIndex/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, IndividualBasicIndex individualBasicIndex)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = IndividualBasicIndex.EditBasicIndex(individualBasicIndex);

                    if (result == 1)
                    {
                        TempData["Message"] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_BASIC_INDEX, individualBasicIndex.IndexID);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_BASIC_INDEX);
                return View(individualBasicIndex);
            }
        }


        //
        // GET: /INVBasicIndex/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                int result = IndividualBasicIndex.DeleteBasicIndex(id);

                if (result == 1)
                {
                    TempData["Message"] = string.Format(Constants.SCC_DELETE, Constants.INV_BASIC_INDEX);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_DELETE, Constants.INV_BASIC_INDEX);
                return RedirectToAction("Index");
            }

        }
    }
}
