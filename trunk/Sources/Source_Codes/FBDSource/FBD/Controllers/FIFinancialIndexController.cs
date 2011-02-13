using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;

namespace FBD.Controllers
{
    public class FIFinancialIndexController : Controller
    {
        //
        // GET: /FIFinancialIndex/

        public ActionResult Index()
        {
            var lstFinancialIndex = BusinessFinancialIndex.SelectFinancialIndex();

            if (lstFinancialIndex == null)
            {
                Error error = new Error();
                // insert error properties here
                RedirectToAction("Error");
            }

            return View();
        }

        //
        // GET: /FIFinancialIndex/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FIFinancialIndex/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FIFinancialIndex/Create

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
        // GET: /FIFinancialIndex/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FIFinancialIndex/Edit/5

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
        // GET: /FIFinancialIndex/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FIFinancialIndex/Delete/5

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
