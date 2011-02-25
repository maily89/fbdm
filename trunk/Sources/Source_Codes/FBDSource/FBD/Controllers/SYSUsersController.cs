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
            List<SystemUsers> users = null;

            try
            {
                users = SystemUsers.SelectUsers();
                if (users == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_INDEX_SYS_USERS;
                return View(users);
            }
            return View(users);
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
                    var entity = new FBDEntities();
                    var user = data.SystemUsers;
                    user.SystemUserGroups = SystemUserGroups.SelectUserGroupByID(data.GroupID, entity);
                    user.SystemBranches = SystemBranches.SelectBranchByID(data.BranchID, entity);
                    int result = SystemUsers.AddUser(user);

                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_ADD_POST_SYS_USERS;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_ADD_POST_SYS_USERS;
                data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
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
                TempData["Message"] = Constants.ERR_EDIT_SYS_USERS;
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

                    int result = SystemUsers.EditUser(user);
                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_EDIT_POST_SYS_USERS_1 + id + Constants.SCC_EDIT_POST_SYS_USERS_2;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_EDIT_POST_SYS_USERS;
                data.SystemUserGroups = SystemUserGroups.SelectUserGroups();
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
                    TempData["Message"] = Constants.SCC_DELETE_SYS_USERS;
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_DELETE_SYS_USERS;
                return RedirectToAction("Index");
            }
        }
    }
}
