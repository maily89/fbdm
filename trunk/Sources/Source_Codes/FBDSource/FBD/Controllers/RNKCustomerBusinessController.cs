using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBD.Controllers
{
    public class RNKCustomerBusinessController : Controller
    {
        //
        // GET: /RNKCustomerBusiness/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /RNKCustomerBusiness/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RNKCustomerBusiness/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /RNKCustomerBusiness/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /RNKCustomerBusiness/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RNKCustomerBusiness/Edit/5

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
        // GET: /RNKCustomerBusiness/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RNKCustomerBusiness/Delete/5

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
