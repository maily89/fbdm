using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;
namespace FBD.Controllers
{
    public class BSNScaleController : Controller
    {
        //
        // GET: /BSNScale/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }

            List<BusinessScales> scales = null;
            try
            {
                scales = BusinessScales.SelectScales();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_SCALE);
            }
            return View(scales);
        }


        //
        // GET: /BSNScale/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            return View();
        }

        //
        // POST: /BSNScale/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleScore"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(BusinessScales businessScales)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result=BusinessScales.AddScale(businessScales);
                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.BUSINESS_SCALE);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_SCALE);
                return View(businessScales);
            }
        }

        //
        // GET: /BSNScale/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            BusinessScales model = null;
            try
            {
                model = BusinessScales.SelectScaleByID(id);

                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_SCALE);
            }
            return View(model);
            
        }

        //
        // POST: /BSNScale/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scaleScore"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessScales businessScales)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {

                if (ModelState.IsValid)
                {
                    int result=BusinessScales.EditScale(businessScales);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.BUSINESS_SCALE, businessScales.ScaleID);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_SCALE);
                return View(businessScales);
            }
        }

        //
        // GET: /BSNScale/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                int result = BusinessScales.DeleteScale(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_SCALE);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_SCALE);
                return RedirectToAction("Index");
            }
        }
    }
}
