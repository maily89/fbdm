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

        public ActionResult Index()
        {
            var branch = SystemBranches.SelectBranches();
            var model = new SYSUsersIndexViewModel();
            model.Branches = branch;
            var temp = SystemUsers.SelectUsers();
            model.Users = temp;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string BranchID)
        {

            var model = new SYSUsersIndexViewModel();
            try
            {
                var branches = SystemBranches.SelectBranches();
                model.Branches = branches;

                if (BranchID != null)
                {
                    if (BranchID == "")
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
                TempData["Message"] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_BRANCH + " or " + Constants.SYSTEM_USER);
            }
            return View(model);
        }

        //
        // GET: /SYSUsers/Add

        public ActionResult Add()
        {
            var model = new SYSUsersViewModel();
            model.SystemUserGroups = SystemUserGroups.SelectUserGroups();
            model.SystemBranches = SystemBranches.SelectBranches();
            return View(model);
        }

        //
        // POST: /SYSUsers/Create

        [HttpPost]
        public ActionResult Add(SYSUsersViewModel data)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    //If dupplicated
                    if (SystemUsers.IsIDExist(data.SystemUsers.UserID) == 1) 
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                        data.SystemBranches = SystemBranches.SelectBranches();
                        return View(data);
                    }
                    //If there is any exception
                    if (SystemUsers.IsIDExist(data.SystemUsers.UserID) == 2) 
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_UNABLE_CHECK;
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
                        TempData["Message"] = string.Format(Constants.SCC_ADD, Constants.SYSTEM_USER) ;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_ADD_POST, Constants.SYSTEM_USER);
                data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /SYSUsers/Edit/5

        public ActionResult Edit(string id)
        {
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
                TempData["Message"] = string.Format(Constants.ERR_EDIT,Constants.SYSTEM_USER);
                return View(model);
            }
            return View(model);
        }

        //
        // POST: /SYSUsers/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, SYSUsersViewModel data)
        {
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
                TempData["Message"] = string.Format(Constants.SCC_EDIT_POST,Constants.SYSTEM_USER,id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_EDIT_POST,Constants.SYSTEM_USER);
                data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /SYSUsers/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                int result = SystemUsers.DeleteUser(id);
                if (result == 1)
                {
                    TempData["Message"] = string.Format(Constants.SCC_DELETE, Constants.SYSTEM_USER);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_DELETE, Constants.SYSTEM_USER);
                return RedirectToAction("Index");
            }
        }
    }
}
