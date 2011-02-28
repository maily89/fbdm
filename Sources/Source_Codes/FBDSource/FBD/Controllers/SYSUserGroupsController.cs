using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class SYSUserGroupsController : Controller
    {
        //
        // GET: /SYSUserGroups/

        public ActionResult Index()
        {
            List<SystemUserGroups> groups = null;

            try
            {
                groups = SystemUserGroups.SelectUserGroups();
                if (groups == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_INDEX_SYS_USER_GROUPS;
                return View(groups);
            }
            return View(groups);
        }


        //
        // GET: /SYSUserGroups/Add

        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /SYSUserGroups/Create

        [HttpPost]
        public ActionResult Add(SystemUserGroups group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemUserGroups.AddUserGroup(group);

                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_ADD_POST_SYS_USER_GROUPS;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_EDIT_SYS_USER_GROUPS;
                return View(group);
            }
        }

        //
        // GET: /SYSUserGroups/Edit/5

        public ActionResult Edit(string id)
        {
            SystemUserGroups group = null;

            try
            {
                group = SystemUserGroups.SelectUserGroupByID(id);

                if (group == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_EDIT_SYS_USER_GROUPS;
                return View(group);
            }

            return View(group);
        }

        //
        // POST: /SYSUserGroups/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, SystemUserGroups group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemUserGroups.EditUserGroup(group);

                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_EDIT_POST_SYS_USER_GROUPS_1 + id 
                                              + Constants.SCC_EDIT_POST_SYS_USER_GROUPS_2;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = Constants.ERR_EDIT_POST_SYS_USER_GROUPS;
                return View(group);
            }
        }

        //
        // GET: /SYSUserGroups/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                int result = SystemUserGroups.DeleteUserGroup(id);
                if (result == 1)
                {
                    TempData["Message"] = Constants.SCC_DELETE_SYS_USER_GROUPS;
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_DELETE_SYS_USER_GROUPS;
                return RedirectToAction("Index");
            }
        }
    }
}