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


        /// <summary>
        /// Use SYSRights class to select all the Rights 
        /// (RightID, Right) in the table [System.Rights] 
        /// then display to the [Index] View
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Use Logic class to insert new Right
        /// 3. Redirect to [Index] View with label displaying: 
        /// "A new Right has been added successfully"
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(SystemRights right)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemRights.AddRight(right);
                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_ADD_POST_SYS_RIGHTS;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_ADD_POST_SYS_RIGHTS;
                return View(right);
            }
        }
        
        //
        // GET: /SYSRights/Edit/5
 

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Right 
        /// (RightID, Right) 
        /// from [System.Rights] table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            SystemRights right = null;
            try
            {
                right = SystemRights.SelectRightsByID(id);
                if (right == null) throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_EDIT_SYS_RIGHTS;
                return View(right);
            }
            return View(right);
        }

        //
        // POST: /SYSRights/Edit/5


        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Right 
        /// (RightID, Right) 
        /// with ID selected in [System.Rights] table in DB
        /// 3. Display in [Index] view with label displaying: 
        /// "The Right with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, SystemRights right)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemRights.EditRight(right);
                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_EDIT_POST_SYS_RIGHTS_1
                                              + id +
                                              Constants.SCC_EDIT_POST_SYS_RIGHTS_2;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch
            {
                TempData["Message"] = Constants.ERR_EDIT_POST_SYS_RIGHTS;
                return View(right);
            }
        }

        //
        // GET: /SYSRights/Delete/5
 
        public ActionResult Delete(string id)
        {
            try
            {
                int result = SystemRights.DeleteRight(id);
                if (result == 1)
                {
                    TempData["Message"] = Constants.SCC_DELETE_SYS_RIGHTS;
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_DELETE_SYS_RIGHTS;
                return RedirectToAction("Index");
            }
        }
    }
}
