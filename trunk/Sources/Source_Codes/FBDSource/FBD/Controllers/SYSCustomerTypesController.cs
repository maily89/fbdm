using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class SYSCustomerTypesController : Controller
    {
        //
        // GET: /SYSCustomerTypes/


        /// <summary>
        /// Use SYSCustomerTypes class to select all the Types 
        /// (TypeID, TypeName) in the table [System.CustomerTypes] 
        /// then display to the [Index] View
        /// </summary>
        /// <returns>[Index] View</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            List<SystemCustomerTypes> types = null;
            try
            {
                types = SystemCustomerTypes.SelectTypes();
                if (types == null) throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_RIGHT);
                return View(types);
            }
            return View(types);
        }

        //
        // GET: /SYSCustomerTypes/Add

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
        // POST: /SYSCustomerTypes/Add


        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Use Logic class to insert new Type
        /// 3. Redirect to [Index] View with label displaying: 
        /// "A new Type has been added successfully"
        /// </summary>
        /// <param name="type">Infor of new Type</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Add] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Add(SystemCustomerTypes type)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (SystemCustomerTypes.IsIDExist(type.TypeID) == 1) //If dupplicated
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        return View(type);
                    }
                    if (SystemCustomerTypes.IsIDExist(type.TypeID) == 2) //If there is any exception
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_UNABLE_CHECK;
                        return View(type);
                    }
                    //else IsIDExist(type.TypeID) == 0 //Means the ID is available
                    int result = SystemCustomerTypes.AddType(type);
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.SYSTEM_RIGHT);
                return View(type);
            }
        }

        //
        // GET: /SYSCustomerTypes/Edit/5


        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate Type 
        /// (TypeID, TypeName) 
        /// from [System.CustomerTypes] table
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
            SystemCustomerTypes type = null;
            try
            {
                type = SystemCustomerTypes.SelectTypeByID(id);
                if (type == null) throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_RIGHT);
                return View(type);
            }
            return View(type);
        }

        //
        // POST: /SYSCustomerTypes/Edit/5


        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate Type 
        /// (TypeID, TypeName) 
        /// with ID selected in [System.CustomerTypes] table in DB
        /// 3. Display in [Index] view with label displaying: 
        /// "The Type with ID xyz has been editted successfully"
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">Infor of edited type</param>
        /// <returns>
        /// [Index] view: if OK
        /// [Edit] view: if ERROR</returns>
        [HttpPost]
        public ActionResult Edit(string id, SystemCustomerTypes type)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_SYSTEM_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemCustomerTypes.EditType(type);
                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.SYSTEM_RIGHT, type.TypeID);
                        return RedirectToAction("Index");
                    }

                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_RIGHT);
                return View(type);
            }
        }

        //
        // GET: /SYSCustomerTypes/Delete/5

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to delete the Type 
        /// with selected ID from the [System.CustomerTypes] table
        /// 3. Back to [Index] view with label displaying: 
        /// "A type has been deleted successfully"
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
                int result = SystemCustomerTypes.DeleteType(id);
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
