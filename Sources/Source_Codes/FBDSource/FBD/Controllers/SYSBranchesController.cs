using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class SYSBranchesController : Controller
    {
        //
        // GET: /SYSBranches/

        /// <summary>
        /// Use SYSBranchesLogic class to 
        ///     - select all the Branches in the table [System.Branches] 
        ///     - then display to the [Index] View
        /// </summary>
        /// <returns>[Index] view</returns>
        public ActionResult Index()
        {
            List<SystemBranches> branches = null;

            try
            {
                branches = SystemBranches.SelectBranches();
                if (branches == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX,Constants.SYSTEM_BRANCH);
                return View(branches);
            }
            return View(branches);
        }


        //
        // GET: /SYSBranches/Add

        /// <summary>
        /// Forward to [Add] View
        /// </summary>
        /// <returns>[Add] view</returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /SYSBranches/Create

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Use Logic class to insert new Branch
        /// 3. Redirect to [Index] View with label displaying: 
        /// "A new Branch has been added successfully"
        /// </summary>
        /// <param name="branch">Infor of new branch</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Add] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Add(SystemBranches branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (SystemBranches.IsIDExist(branch.BranchID) == 1) //If dupplicated
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        return View(branch);
                    }
                    if (SystemBranches.IsIDExist(branch.BranchID) == 2) //If there is any exception
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_UNABLE_CHECK;
                        return View(branch);
                    }
                    //else IsIDExist(right.RightID) == 0 //Means the ID is available
                    int result = SystemBranches.AddBranch(branch);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD,Constants.SYSTEM_BRANCH);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.SYSTEM_BRANCH);
                return View(branch);
            }
        }

        //
        // GET: /SYSBranches/Edit/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select the Branch with specific ID
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// [Edit] view: if OK
        /// [Index] view: if ERROR</returns>
        public ActionResult Edit(string id)
        {
            SystemBranches branch = null;

            try
            {
                branch = SystemBranches.SelectBranchByID(id);

                if (branch == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_BRANCH);
                return View(branch);
            }

            return View(branch);
        }

        //
        // POST: /SYSBranches/Edit/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update the Branch
        /// 3. Display in [Index] view with label displaying: 
        /// "The Branch with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="branch">Infor of edited Branch</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Edit] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Edit(string id, SystemBranches branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemBranches.EditBranch(branch);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_BRANCH, id);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_BRANCH);
                return View(branch);
            }
        }

        //
        // GET: /SYSBranches/Delete/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the selected Branch
        /// 3. Back to [Index] view with label displaying: 
        /// "A Branch has been deleted successfully"
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>[Index] view</returns>
        public ActionResult Delete(string id)
        {
            try
            {
                int result = SystemBranches.DeleteBranch(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.SYSTEM_BRANCH);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.SYSTEM_BRANCH);
                return RedirectToAction("Index");
            }
        }
    }
}
