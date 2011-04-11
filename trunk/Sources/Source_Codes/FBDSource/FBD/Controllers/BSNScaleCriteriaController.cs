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
    
    //TODO: check scaleCriteria name and id unique
    public class BSNScaleCriteriaController : Controller
    {
        //
        // GET: /BSNScaleCriteria/
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

            List<BusinessScaleCriteria> scaleCriteria = null;
            try
            {
                 scaleCriteria= BusinessScaleCriteria.SelectScaleCriteria();
                 if (scaleCriteria == null)
                 {
                     throw new Exception();
                 }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_SCALECRITERIA);
            }
            return View(scaleCriteria);
        }


        //
        // GET: /BSNScaleCriteria/Add
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
        // POST: /BSNScaleCriteria/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleCriteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(BusinessScaleCriteria scaleCriteria)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result=BusinessScaleCriteria.AddScaleCriteria(scaleCriteria);
                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.BUSINESS_SCALECRITERIA);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();

            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_SCALECRITERIA);
                return View(scaleCriteria);
            }
        }

        //
        // GET: /BSNScaleCriteria/Edit/5
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
            BusinessScaleCriteria model = null;
            try
            {
                model = BusinessScaleCriteria.SelectScaleCriteriaByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_SCALECRITERIA);
            }
            return View(model);
        }

        //
        // POST: /BSNScaleCriteria/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scaleCriteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessScaleCriteria scaleCriteria)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {

                if (ModelState.IsValid)
                {
                    int result=BusinessScaleCriteria.EditScaleCriteria(scaleCriteria);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.BUSINESS_SCALECRITERIA, id);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_SCALECRITERIA);
                return View(scaleCriteria);
            }
        }

        //
        // GET: /BSNScaleCriteria/Delete/5
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
                int result= BusinessScaleCriteria.DeleteScaleCriteria(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_SCALECRITERIA);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_SCALECRITERIA);
                return RedirectToAction("Index");
            }
        }


    }
}
