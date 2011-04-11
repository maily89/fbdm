using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class RPTBusinessReportController : Controller
    {
        // GET: /RPTBusinessReport/ExportBusinessInfo/5
        public ActionResult ExportBusinessInfo(int ID)
        {
            try
            {
                FBDEntities FBDModel = new FBDEntities();
                RPTIBusinessReportService businessReportService = new RPTIBusinessReportServiceImpl();
                RPTBusinessReportModel businessReportModel = businessReportService.SelectBusinessInfo(FBDModel, ID);

                List<RPTBusinessReportModel> mainReportDataSource = new List<RPTBusinessReportModel>();
                mainReportDataSource.Add(businessReportModel);

                BusinessGeneralReport mainReport = new BusinessGeneralReport();
                mainReport.SetDataSource(mainReportDataSource);

                mainReport.OpenSubreport("BusinessScaleReport.rpt").SetDataSource(businessReportModel.ScaleInfo);
                mainReport.OpenSubreport("BusinessFinancialReport.rpt").SetDataSource(businessReportModel.FinancialInfo);
                mainReport.OpenSubreport("BusinessNonFinancialReport.rpt").SetDataSource(businessReportModel.NonFinancialInfo);
                
                Stream stream = mainReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(stream, "application/pdf");
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_RPT_REPORT;
                return RedirectToAction("Index", "Error");
            }
        }
    }
}