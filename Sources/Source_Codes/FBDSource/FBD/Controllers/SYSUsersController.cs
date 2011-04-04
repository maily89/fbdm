using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class SYSUsersController : Controller
    {
        //
        // GET: /SYSUsers/

        /// <summary>
        /// Use SYSUsersLogic class to 
        /// - select all the Users table [System.Users] 
        /// - display to the [Index] View
        /// </summary>
        /// <returns>[Index] view</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }

            var branch = SystemBranches.SelectBranches();
            var model = new SYSUsersIndexViewModel();
            try
            {
                model.Branches = branch;
                var temp = SystemUsers.SelectUsers();
                model.Users = temp;
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_RIGHT);
            }
            return View(model);
        }

        /// <summary>
        /// 1. Receive BranchID from parameter
        /// 2. Use Logic class to select list Users
        /// 3. Redirect to [Index] View with list of Users displayed
        /// </summary>
        /// <param name="BranchID">BranchID</param>
        /// <returns>[Index] view</returns>
        [HttpPost]
        public ActionResult Index(string BranchID)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            var model = new SYSUsersIndexViewModel();
            try
            {
                var branches = SystemBranches.SelectBranches();
                model.Branches = branches;

                if (BranchID != null)
                {
                    if (BranchID.Equals(""))
                    {
                        model.Users = SystemUsers.SelectUsers();
                    }
                    else
                    {
                        var branch = SystemBranches.SelectBranchByID(BranchID);
                        model.BranchName = branch.BranchName;
                        model.BranchID = BranchID;
                        model.Users = SystemUsers.SelectUsersByBranch(BranchID);
                    }
                }
                else model.Users = SystemUsers.SelectUsers();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_BRANCH + " or " + Constants.SYSTEM_USER);
            }
            return View(model);
        }

        //
        // GET: /SYSUsers/Add

        /// <summary>
        /// Use Logic class to 
        ///     - create ViewModel 
        ///     - Forward to [Add] View
        /// </summary>
        /// <param name="BranchID">ID</param>
        /// <returns>[Add] view</returns>
        public ActionResult Add()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            var model = new SYSUsersViewModel();
            try
            {
                model.SystemUsers = new SystemUsers();
                model.SystemUsers.Password = Constants.FORM_PASSWORD;
                model.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                model.SystemBranches = SystemBranches.SelectBranches();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_RIGHT);
            }
            return View(model);
        }

        //
        // POST: /SYSUsers/Create

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Use Logic class to insert new User
        /// 3. Redirect to [Index] View with label displaying: 
        /// "A new User has been added successfully"
        /// </summary>
        /// <param name="data">Infor of new User with Group and Branch</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Add] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Add(SYSUsersViewModel data)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    //If dupplicated
                    if (SystemUsers.IsIDExist(data.SystemUsers.UserID) == 1) 
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        data.SystemUsers.Password = Constants.FORM_PASSWORD;
                        data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                        data.SystemBranches = SystemBranches.SelectBranches();
                        return View(data);
                    }
                    //If there is any exception
                    if (SystemUsers.IsIDExist(data.SystemUsers.UserID) == 2) 
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_UNABLE_CHECK;
                        data.SystemUsers.Password = Constants.FORM_PASSWORD;
                        data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                        data.SystemBranches = SystemBranches.SelectBranches();
                        return View(data);
                    }

                    //else IsIDExist(right.RightID) == 0 //Means the ID is available
                    var entity = new FBDEntities();
                    var user = new SystemUsers();
                    user.UserID = data.SystemUsers.UserID;
                    user.SystemUserGroups = SystemUserGroups.SelectUserGroupByID(data.GroupID, entity);
                    user.SystemBranches = SystemBranches.SelectBranchByID(data.BranchID, entity);
                    user.FullName = data.SystemUsers.FullName;
                    user.Password = StringHelper.Encode("password");
                    user.Status = data.SystemUsers.Status;
                    user.CreditDepartment = data.SystemUsers.CreditDepartment;
                    int result = SystemUsers.AddUser(user, entity);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.SYSTEM_USER);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.SYSTEM_USER);
                data.SystemUsers.Password = Constants.FORM_PASSWORD;
                data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /SYSUsers/Edit/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to 
        ///     - select appropriate User from [System.Users] table
        ///     - create ViewModel contains list of Groups and Branches
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// [Edit] view: if OK
        /// [Index] view: if ERROR</returns>
        public ActionResult Edit(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            var model = new SYSUsersViewModel();

            try
            {
                model.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                model.SystemBranches = SystemBranches.SelectBranches();
                model.SystemUsers = SystemUsers.SelectUserByID(id);
                model.SystemUsers.SystemUserGroupsReference.Load();
                model.SystemUsers.SystemBranchesReference.Load();
                model.GroupID = model.SystemUsers.SystemUserGroups.GroupID;
                model.BranchID = model.SystemUsers.SystemBranches.BranchID;
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_USER);
                return View(model);
            }
            return View(model);
        }

        //
        // POST: /SYSUsers/Edit/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate User 
        /// with ID in [System.Users] table in DB
        /// 3. Display in [Index] view with label displaying: 
        /// "The User with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">Infor of edited User</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Edit] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Edit(string id, SYSUsersViewModel data)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var user = SystemUsers.SelectUserByID(id, entity);
                    user.SystemUserGroups = SystemUserGroups.SelectUserGroupByID(data.GroupID, entity);
                    user.SystemBranches = SystemBranches.SelectBranchByID(data.BranchID, entity);
                    user.FullName = data.SystemUsers.FullName;
                    user.Status = data.SystemUsers.Status;
                    user.CreditDepartment = data.SystemUsers.CreditDepartment;
                    SystemUsers.EditUser(user);
                    
                }
                else throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_USER, id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_USER);
                data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /SYSUsers/Delete/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the User with selected ID
        /// from the [System.Users] table
        /// 3. Back to [Index] view with label displaying: 
        /// "A User has been deleted successfully"
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>[Index] view</returns>
        public ActionResult Delete(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                int result = SystemUsers.DeleteUser(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.SYSTEM_USER);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.SYSTEM_USER);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Reset user' password
        /// </summary>
        /// <param name="userID">the user id</param>
        /// <returns>Edit View</returns>
        public ActionResult ResetPassword(string userID)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                int result = SystemUsers.ResetPassword(userID);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = Constants.SCC_RESET_PASS;
                    return RedirectToAction("Edit", new { id = userID });
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_RESET_PASS;
                return RedirectToAction("Edit", new { id = userID });
            }
        }
    }
}
