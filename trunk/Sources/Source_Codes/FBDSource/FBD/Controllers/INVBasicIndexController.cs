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
        /// <summary>
        /// Get List for index page 
        /// </summary>
        /// <returns>index view</returns>

        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_BASIC_INDEX);
                return View(lstBasicIndex);
            }
            // If there is no error, displaying the list of financial index
            return View(lstBasicIndex);
        }

       
        //
        // GET: /INVBasicIndex/Create
        
        /// <summary>
        /// Generate create form
        /// </summary>
        /// <returns>create view</returns>
        public ActionResult Create()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            TempData[Constants.SCC_MESSAGE] = null;
            TempData[Constants.ERR_MESSAGE] = null;
            return View();
        } 

        //
        // POST: /INVBasicIndex/Create
        /// <summary>
        /// Process create object
        /// </summary>
        /// <param name="individualBasicIndex">individualBasicIndex</param>
        /// <returns>index view</returns>
        
        [HttpPost]
        public ActionResult Create(IndividualBasicIndex individualBasicIndex)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.INV_BASIC_INDEX);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.INV_BASIC_INDEX);
                return View(individualBasicIndex);
            }
        }
        
        //
        // GET: /INVBasicIndex/Edit/5
        /// <summary>
        /// Generate the edit form for user edit infor
        /// </summary>
        /// <param name="id"> ID </param>
        /// <returns> Index page if success</returns>
        public ActionResult Edit(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_BASIC_INDEX);
            }
            return View(model);
        }

        //
        // POST: /INVBasicIndex/Edit/5
        /// <summary>
        /// Process Get edit information and return the index page
        /// </summary>
        /// <param name="id"> ID </param>
        /// <returns> Index page if success</returns>
      

        [HttpPost]
        public ActionResult Edit(string id, IndividualBasicIndex individualBasicIndex)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result = IndividualBasicIndex.EditBasicIndex(individualBasicIndex);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_BASIC_INDEX, individualBasicIndex.IndexID);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_BASIC_INDEX);
                return View(individualBasicIndex);
            }
        }


        //
        // GET: /INVBasicIndex/Delete/5
        /// <summary>
        /// Delete the selected individualBasicIndex
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Index view</returns>
        
        public ActionResult Delete(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                int result = IndividualBasicIndex.DeleteBasicIndex(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.INV_BASIC_INDEX);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.INV_BASIC_INDEX);
                return RedirectToAction("Index");
            }

        }
    }
}
