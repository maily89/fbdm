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
        /// <returns>[Index] View</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            List<SystemRights> rights = null;
            try
            {
                rights = SystemRights.SelectRights();
                if (rights == null) throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_RIGHT);
                return View(rights);
            }
            return View(rights);
        }

        //
        // GET: /SYSRights/Add

        /// <summary>
        /// Forward to Add view
        /// </summary>
        /// <returns>[Add] view</returns>
        public ActionResult Add()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
        /// <param name="right">Infor of new Right</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Add] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Add(SystemRights right)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (SystemRights.IsIDExist(right.RightID)==1) //If dupplicated
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        return View(right);
                    }
                    if (SystemRights.IsIDExist(right.RightID) == 2) //If there is any exception
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_UNABLE_CHECK;
                        return View(right);
                    }
                    //else IsIDExist(right.RightID) == 0 //Means the ID is available
                    int result = SystemRights.AddRight(right);
                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.SYSTEM_RIGHT);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST,Constants.SYSTEM_RIGHT);
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
            SystemRights right = null;
            try
            {
                right = SystemRights.SelectRightsByID(id);
                if (right == null) throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT,Constants.SYSTEM_RIGHT);
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
        /// <param name="id">ID</param>
        /// <param name="right">Infor of edited right</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Edit] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Edit(string id, SystemRights right)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemRights.EditRight(right);
                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_RIGHT, right.RightID);
                        return RedirectToAction("Index");
                    }
                    
                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_RIGHT);
                return View(right);
            }
        }

        //
        // GET: /SYSRights/Delete/5
 
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Right 
        /// with selected ID from the [System.Rights] table
        /// 3. Back to [Index] view with label displaying: 
        /// "A right has been deleted successfully"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                int result = SystemRights.DeleteRight(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.SYSTEM_RIGHT);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.SYSTEM_RIGHT);
                return RedirectToAction("Index");
            }
        }
    }
}
