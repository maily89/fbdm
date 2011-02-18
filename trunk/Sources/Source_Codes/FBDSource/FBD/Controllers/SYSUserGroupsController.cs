using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;

namespace FBD.Controllers
{
    public class SYSUserGroupsController : Controller
    {
        //
        // GET: /SYSUserGroups/

        public ActionResult Index()
        {
            var groups = SystemUserGroups.SelectUserGroups();
            return View(groups);
        }

        //
        // GET: /SYSUserGroups/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SYSUserGroups/Create

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
                    SystemUserGroups.AddUserGroup(group);
                }
                else throw new Exception();
                TempData["Message"] = "User Group ID " + group.GroupID + " have been added successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(group);
            }
        }

        //
        // GET: /SYSUserGroups/Edit/5

        public ActionResult Edit(string id)
        {
            var model = SystemUserGroups.SelectUserGroupByID(id);
            return View(model);
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
                    SystemUserGroups.EditUserGroup(group);
                }
                else throw new ArgumentException();
                TempData["Message"] = "Group ID " + group.GroupID + " have been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = ex.Message;
                return View(group);
            }
        }

        //
        // GET: /SYSUserGroups/Delete/5

        public ActionResult Delete(string id)
        {
            try
            {
                SystemUserGroups.DeleteUserGroup(id);
                TempData["Message"] = "Group ID " + id + " have been deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}