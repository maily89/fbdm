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
        /// <summary>
        /// Use SYSGroupsLogic class to 
        /// - select all the groups in the table [System.UserGroups] 
        /// - then display to the [Index] View
        /// </summary>
        /// <returns>[Index] view</returns>
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_USER_GROUP);
                return View(groups);
            }
            return View(groups);
        }


        //
        // GET: /SYSUserGroups/Add

        /// <summary>
        /// Forward to [Add] View
        /// </summary>
        /// <returns>[Add] view</returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /SYSUserGroups/Create

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Use Logic class to insert new Group
        /// 3. Redirect to [Index] View with label displaying: 
        /// "A new group has been added successfully"
        /// </summary>
        /// <param name="group">Infor of new Group</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Add] view: if ERROR</returns>
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
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.SYSTEM_USER_GROUP);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_USER_GROUP);
                return View(group);
            }
        }

        //
        // GET: /SYSUserGroups/Edit/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Group 
        /// from [System.UserGroups] table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// [Edit] view: if OK
        /// [Index] view: if ERROR</returns>
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_USER_GROUP);
                return View(group);
            }

            return View(group);
        }

        //
        // POST: /SYSUserGroups/Edit/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Group with ID 
        /// selected in [System.UserGroups] table in DB
        /// 3. Display in [Index] view with label displaying: 
        /// "The group with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="group">Infor of edited group</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Edit] view: if ERROR</returns>
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
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_USER_GROUP, id);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_USER_GROUP);
                return View(group);
            }
        }

        //
        // GET: /SYSUserGroups/Delete/5

        /// <summary>
        /// 1.. Receive ID from parameter
        /// 2. Use Logic class to delete the Group 
        /// with selected ID from the [System.UserGroups] table
        /// 3. Back to [Index] view with label displaying: 
        /// "A group has been deleted successfully"
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>[Index] view</returns>
        public ActionResult Delete(string id)
        {
            try
            {
                int result = SystemUserGroups.DeleteUserGroup(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.SYSTEM_USER_GROUP);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.SYSTEM_USER_GROUP);
                return RedirectToAction("Index");
            }
        }
    }
}