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
                TempData["Message"] = string.Format(Constants.ERR_INDEX,Constants.SYSTEM_USER_GROUP);
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
                    if (SystemUserGroups.IsIDExist(group.GroupID) == 1) //If dupplicated
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        return View(group);
                    }
                    if (SystemUserGroups.IsIDExist(group.GroupID) == 2) //If there is any exception
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_UNABLE_CHECK;
                        return View(group);
                    }
                    //else IsIDExist(right.RightID) == 0 //Means the ID is available
                    int result = SystemUserGroups.AddUserGroup(group);

                    if (result == 1)
                    {
                        TempData["Message"] = string.Format(Constants.SCC_ADD, Constants.SYSTEM_USER_GROUP);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_USER_GROUP);
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
                TempData["Message"] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_USER_GROUP);
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
                        TempData["Message"] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_USER_GROUP, id);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_USER_GROUP);
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
                    TempData["Message"] = string.Format(Constants.SCC_DELETE, Constants.SYSTEM_USER_GROUP);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_DELETE, Constants.SYSTEM_USER_GROUP);
                return RedirectToAction("Index");
            }
        }
    }
}