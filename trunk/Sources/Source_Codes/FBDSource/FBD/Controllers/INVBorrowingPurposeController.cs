using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    //to do: need catch specific exeption. For example: insert the same ID 
    public class INVBorrowingPurposeController : Controller
    {
        #region  Index()
        // GET: /INVBorrowingPurpose/     
        /// <summary>
        /// Create view for index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            List<IndividualBorrowingPurposes> lstBorrowingPP = null;
            try
            {
                // Select the list of financial indexes
                lstBorrowingPP = IndividualBorrowingPurposes.SelectBorrowingPPList();
                // If error occurs
                if (lstBorrowingPP == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Dispay error message when displaying financial indexes
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX,Constants.BORROWING_PURPOSE);
                return View(lstBorrowingPP);
            }
            // If there is no error, displaying the list of financial index
            return View(lstBorrowingPP);
        }
        #endregion
       
        #region Create
        //
        // GET: /INVBorrowingPurpose/Create
        public ActionResult Create()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            TempData[Constants.SCC_MESSAGE] = null;
            return View();
        } 
        //
        // POST: /INVBorrowingPurpose/Create

        [HttpPost]
        public ActionResult Create(IndividualBorrowingPurposes IndividualBorrowingPP)
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
                    int result = IndividualBorrowingPurposes.AddBorrowingPP(IndividualBorrowingPP);
                    if (result == 1)
                    {
                        // Display successful message when adding new financial index
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.BORROWING_PURPOSE);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                // Display error message when adding new financial index
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BORROWING_PURPOSE);
                return View(IndividualBorrowingPP);
            }
        }
        #endregion

        #region Edit
        //
        // GET: /INVBorrowingPurpose/Edit/5
         public ActionResult Edit(string id)
        {
            IndividualBorrowingPurposes model = null;
            try
            {
                model = IndividualBorrowingPurposes.SelectBorrowingPPByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BORROWING_PURPOSE);
            }
            return View(model);
        }

        //
        // POST: /INVBorrowingPurpose/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, IndividualBorrowingPurposes individualBorrowingPP)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = IndividualBorrowingPurposes.EditBorowingPurpose(individualBorrowingPP);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.BORROWING_PURPOSE, individualBorrowingPP.PurposeID);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BORROWING_PURPOSE);
                return View(individualBorrowingPP);
            }
        }

        #endregion
      
        //
        // GET: /INVBorrowingPurpose/Delete/5
 
        public ActionResult Delete(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                int result=IndividualBorrowingPurposes.DeleteBorrowingPurpose(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BORROWING_PURPOSE);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BORROWING_PURPOSE);
                return RedirectToAction("Index");
            }
        }
    }
}
