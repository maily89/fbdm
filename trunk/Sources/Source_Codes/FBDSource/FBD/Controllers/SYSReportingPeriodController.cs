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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX,Constants.SYSTEM_REPORTING_PERIOD);
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
                    if (SystemReportingPeriods.IsIDExist(reportingPeriod.PeriodID) == 1) //If dupplicated
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        return View(reportingPeriod);
                    }
                    if (SystemReportingPeriods.IsIDExist(reportingPeriod.PeriodID) == 2) //If there is any exception
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_UNABLE_CHECK;
                        return View(reportingPeriod);
                    }
                    //else IsIDExist(reportingPeriod.PeriodID) == 0 //Means the ID is available
                    int result = SystemReportingPeriods.AddReportingPeriod(reportingPeriod);
                    if (result == 2)
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_TO_DATE_LESS_THAN_FROM_DATE;
                        return View(reportingPeriod);
                    }
                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.SYSTEM_REPORTING_PERIOD);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.SYSTEM_REPORTING_PERIOD);
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.SYSTEM_REPORTING_PERIOD);
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
                        TempData[Constants.SCC_MESSAGE] = string.Format( Constants.SCC_EDIT_POST, Constants.SYSTEM_REPORTING_PERIOD,reportingPeriod.PeriodID);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.SYSTEM_REPORTING_PERIOD);
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
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.SYSTEM_REPORTING_PERIOD);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.SCC_MESSAGE] = string.Format( Constants.ERR_DELETE, Constants.SYSTEM_REPORTING_PERIOD);
                return RedirectToAction("Index");
            }
        }
    }
}
