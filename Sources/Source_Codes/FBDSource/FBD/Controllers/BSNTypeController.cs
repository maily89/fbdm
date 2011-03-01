using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    //TODO: check Rights
    //TODO: check type name and id unique
    public class BSNTypeController : Controller
    {
        //
        // GET: /BSNType/
        
        public ActionResult Index()
        {
            List<BusinessTypes> types=null;
            try
            {
                types = BusinessTypes.SelectTypes();

                if (types == null) throw new Exception();
            }
            catch
            {
                TempData["Message"] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_TYPE);

            }
            
            return View(types);
        }



        //
        // GET: /BSNType/Add

        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /BSNType/Add

        [HttpPost]
        public ActionResult Add(BusinessTypes type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (BusinessTypes.AddType(type) == 1)
                    {
                        TempData["Message"] = string.Format(Constants.SCC_ADD, Constants.BUSINESS_TYPE);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
                
            }
            catch(Exception ex)
            {
                TempData["Message"] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_TYPE);
                return View(type);
            }
        }
        
        //
        // GET: /BSNType/Edit/5
        /// <summary>
        /// Display edit form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            BusinessTypes model = null;
            try
            {
                model = BusinessTypes.SelectTypeByID(id);
                if (model == null) throw new Exception();
            }
            catch
            {
                TempData["Message"] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_TYPE);
            }

            return View(model);
        }

        //
        // POST: /BSNType/Edit/5
        /// <summary>
        /// Update edit form and display result.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="industry"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessTypes type)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    if (BusinessTypes.EditType(type) == 1)
                    {
                        TempData["Message"] = string.Format(Constants.SCC_EDIT_POST, Constants.BUSINESS_TYPE, id);
                        return RedirectToAction("Index");
                    }
                    
                }
                throw new ArgumentException();
                
            }
            catch 
            {
                //TODO: Temporary error handle.

                TempData["Message"] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_TYPE);
                return View(type);
            }
        }

        //
        // GET: /BSNType/Delete/5
        /// <summary>
        /// Delete Industry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            try
            {
                if (BusinessTypes.DeleteType(id) == 1)
                {
                    TempData["Message"] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_TYPE);
                    return RedirectToAction("Index");
                }
                throw new Exception();
                
            }
            catch
            {
                TempData["Message"] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_TYPE);
                return RedirectToAction("Index");
            }
        }

        
    }
}
