using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;

namespace FBD.Controllers
{
    //SystemReportingPeriods Controller
    //Check PeriodID unique, PeriodName
    public class SYSReportingPeriodController : Controller
    {
        //
        // GET: /SYSReportingPeriod/

        public ActionResult Index()
        {
            var reportingPeriod = SystemReportingPeriods.SelectReportingPeriods();
            return View(reportingPeriod);
        }

        //
        // GET: /SYSReportingPeriod/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SYSReportingPeriod/Add

        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /SYSReportingPeriod/Add

        [HttpPost]
        public ActionResult Add(SystemReportingPeriods reportingPeriod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemReportingPeriods.AddReportingPeriod(reportingPeriod);
                }
                else throw new Exception();
                TempData["Message"] = "Reporting period " + reportingPeriod.PeriodID + "has been added successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /SYSReportingPeriod/Edit/5
 
        public ActionResult Edit(string id)
        {
            var model = SystemReportingPeriods.SelectReportingPeriodByID(id);
            return View(model);
        }

        //
        // POST: /SYSReportingPeriod/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, SystemReportingPeriods reportingPeriod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemReportingPeriods.EditReportingPeriod(reportingPeriod);
                }
                else throw new Exception();
                TempData["Message"] = "Period ID " + reportingPeriod.PeriodID + " has been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(reportingPeriod);
            }
        }

        //
        // GET: /SYSReportingPeriod/Delete/5
 
        public ActionResult Delete(string id)
        {
            SystemReportingPeriods.DeleteReportingPeriod(id);
            TempData["Message"] = "Period ID " + id + "has been deleted sucessfully";
            return RedirectToAction("Index");
        }

    }
}
