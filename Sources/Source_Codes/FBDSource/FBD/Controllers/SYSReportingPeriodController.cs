using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    //SystemReportingPeriods Controller
    //Check PeriodID unique, PeriodName
    public class SYSReportingPeriodController : Controller
    {
        //
        // GET: /SYSReportingPeriod/


        /// <summary>
        /// Use SYSReportingPeriodsLogic class to select all the periods 
        /// (periodID, periodName, fromDate, toDate, active) 
        /// in the table [System.ReportingPeriods] 
        /// then display to the [Index] View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<SystemReportingPeriods> lstPeriod = null;
            try
            {
                lstPeriod = SystemReportingPeriods.SelectReportingPeriods();
                if (lstPeriod == null) throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_INDEX_SYS_REPORTING_PERIODS;
                return View(lstPeriod);
            }
            return View(lstPeriod);
        }
        
                
        //
        // GET: /SYSReportingPeriod/Add


        /// <summary>
        /// Forward to [Add] view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /SYSReportingPeriod/Add


        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Use Logic class to insert new period
        /// 3. Redirect to [Index] View with label displaying: 
        /// "A new period has been added successfully"
        /// </summary>
        /// <param name="reportingPeriod"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(SystemReportingPeriods reportingPeriod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemReportingPeriods.AddReportingPeriod(reportingPeriod);
                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_ADD_POST_SYS_REPORTING_PERIODS;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch
            {
                TempData["Message"] = Constants.ERR_ADD_POST_SYS_REPORTING_PERIODS;
                return View(reportingPeriod);
            }
        }
        
        //
        // GET: /SYSReportingPeriod/Edit/5
 

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to select appropriate period 
        /// (periodID, periodName, fromDate, toDate, Active) 
        /// from [System.ReportingPeriods] table
        /// 3. Display in [Edit] view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            SystemReportingPeriods reportingPeriod = null;

            try
            {
                reportingPeriod = SystemReportingPeriods.SelectReportingPeriodByID(id);
                if (reportingPeriod == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData["Message"] = Constants.ERR_EDIT_POST_SYS_REPORTING_PERIODS;
                return View(reportingPeriod);
            }
            return View(reportingPeriod);
        }


        //
        // POST: /SYSReportingPeriod/Edit/5


        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Use Logic class to update appropriate period 
        /// (periodID, periodName, fromDate, toDate, Active) 
        /// with ID selected in [System.ReportingPeriods] table in DB
        /// 3. Display in [Index] view with label displaying: 
        /// "PeriodID xyz has been editted successfully"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reportingPeriod"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, SystemReportingPeriods reportingPeriod)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SystemReportingPeriods.EditReportingPeriod(reportingPeriod);
                    if (result == 1)
                    {
                        TempData["Message"] = Constants.SCC_EDIT_POST_SYS_REPORTING_PERIODS_1;
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = "Period ID " + reportingPeriod.PeriodID + " has been updated sucessfully";
                return View(reportingPeriod);
            }
        }

        //
        // GET: /SYSReportingPeriod/Delete/5
 
        public ActionResult Delete(string id)
        {
            try
            {
                int result = SystemReportingPeriods.DeleteReportingPeriod(id);
                if (result == 1)
                {
                    TempData["Message"] = Constants.SCC_DELETE_SYS_REPORTING_PERIODS;
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData["Message"] = Constants.ERR_DELETE_SYS_REPORTING_PERIODS;
                return RedirectToAction("Index");
            }
        }
    }
}
