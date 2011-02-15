using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;

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
            var types = BusinessTypes.SelectTypes();
            
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
                    BusinessTypes.AddType(type);
                }
                else throw new Exception();
                TempData["Message"] = "Type ID "+type.TypeID+ " have been added sucessfully";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(type);
            }
        }
        
        //
        // GET: /BSNType/Edit/5
 
        public ActionResult Edit(string id)
        {
            var model = BusinessTypes.SelectTypeByID(id);
            return View(model);
        }

        //
        // POST: /BSNType/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, BusinessTypes type)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    BusinessTypes.EditType(type);
                    
                }
                else throw new ArgumentException();
                TempData["Message"] = "TypeID "+ type.TypeID + " have been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Temporary error handle.
                
                TempData["Message"] = ex.Message;
                return View(type);
            }
        }

        //
        // GET: /BSNType/Delete/5
 
        public ActionResult Delete(string id)
        {
            BusinessTypes.DeleteType(id);
            TempData["Message"] = "Type ID "+ id+" have been deleted sucessfully";
            return RedirectToAction("Index");
        }

        
    }
}
