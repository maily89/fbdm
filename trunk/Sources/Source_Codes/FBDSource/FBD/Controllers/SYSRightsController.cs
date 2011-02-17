using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class SYSRightsController : Controller
    {
        //
        // GET: /SYSRights/

        public ActionResult Index()
        {
            List<SystemRights> rights = null;
            try
            {
                rights = SystemRights.SelectRights();
                if (rights == null) throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_ADD_POST_SYS_RIGHTS;
                return View(rights);
            }
            return View(rights);
        }

        //
        // GET: /SYSRights/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SYSRights/Add

        /// <summary>
        /// Forward to Add view
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /SYSRights/Add

        [HttpPost]
        public ActionResult Add(SystemRights right)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /SYSRights/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SYSRights/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SYSRights/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SYSRights/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
