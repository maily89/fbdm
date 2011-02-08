using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.ViewModels;
using FBD.Models;

namespace FBD.Controllers
{
    public class FIFinancialIndexController : Controller
    {
        //
        // GET: /FIFinancialIndex/

        public ActionResult Index()
        {
            FIFinancialIndexViewModel viewModel = FIFinancialIndexLogic.SelectFinancialIndex();

            return View(viewModel);
        }

        //
        // GET: /FIFinancialIndex/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FIFinancialIndex/Create

        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /FIFinancialIndex/Create

        [HttpPost]
        public ActionResult Add(BusinessFinancialIndex businessFinancialIndex)
        {
            try
            {
                FIFinancialIndexLogic.AddFinancialIndex(businessFinancialIndex);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /FIFinancialIndex/Edit/5
 
        public ActionResult Edit(string id)
        {
            BusinessFinancialIndex businessFinancialIndex = FIFinancialIndexLogic.SelectFinancialIndexByID(id);

            return View(businessFinancialIndex);
        }

        //
        // POST: /FIFinancialIndex/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                var financialIndex = FIFinancialIndexLogic.SelectFinancialIndexByID(id);
                UpdateModel(financialIndex, "BusinessFinancialIndex");
 
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
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                FIFinancialIndexLogic.DeleteFinancialIndex(id);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
