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
                TempData["Message"] = string.Format(Constants.ERR_INDEX,Constants.SYSTEM_BRANCH);
                return View(branches);
            }
            return View(branches);
        }


        //
        // GET: /SYSBranches/Add

        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /SYSBranches/Create

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
                        TempData["Message"] = string.Format(Constants.SCC_ADD,Constants.SYSTEM_BRANCH);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_ADD_POST, Constants.SYSTEM_BRANCH);
                return View(branch);
            }
        }

        //
        // GET: /SYSBranches/Edit/5

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
                TempData["Message"] = string.Format(Constants.ERR_EDIT,Constants.SYSTEM_BRANCH);
                return View(branch);
            }

            return View(branch);
        }

        //
        // POST: /SYSBranches/Edit/5

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
                        TempData["Message"] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_BRANCH, id);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_BRANCH);
                return View(branch);
            }
        }

        //
        // GET: /SYSBranches/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                int result = SystemBranches.DeleteBranch(id);
                if (result == 1)
                {
                    TempData["Message"] = string.Format(Constants.SCC_DELETE,Constants.SYSTEM_BRANCH);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_DELETE, Constants.SYSTEM_BRANCH);
                return RedirectToAction("Index");
            }
        }
    }
}
