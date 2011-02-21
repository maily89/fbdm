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
                TempData["Message"] = Constants.ERR_INDEX_SYS_BRANCHES;
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
                    int result = SystemBranches.AddBranch(branch);

                    if (result == 1)
                    { 
                       //TempData["Message"] = Constants.
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_EDIT_SYS_BRANCHES;
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
                TempData["Message"] = Constants.ERR_EDIT_SYS_BRANCHES;
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
                        TempData["Message"] = Constants.SCC_EDIT_POST_SYS_BRANCHES_1 + id 
                                              + Constants.SCC_EDIT_POST_SYS_BRANCHES_2;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = Constants.ERR_EDIT_POST_SYS_BRANCHES;
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
                    TempData["Message"] = Constants.SCC_DELETE_SYS_BRANCHES;
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_DELETE_SYS_BRANCHES;
                return RedirectToAction("Index");
            }
        }
    }
}
