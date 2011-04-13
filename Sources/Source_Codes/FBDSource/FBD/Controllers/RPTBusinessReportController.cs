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
                // Service used to implement exporting business report
                RPTIBusinessReportService businessReportService = new RPTIBusinessReportServiceImpl();
                // Model containing information to be displayed in report
                RPTBusinessReportModel businessReportModel = businessReportService.SelectBusinessInfo(FBDModel, ID);

                // DataSource of report must be a list, ienumerable or datatable, dataset... so there is a need for a list below
                // although it has only 1 element
                List<RPTBusinessReportModel> mainReportDataSource = new List<RPTBusinessReportModel>();
                mainReportDataSource.Add(businessReportModel);

                BusinessGeneralReport mainReport = new BusinessGeneralReport();
                mainReport.SetDataSource(mainReportDataSource);

                // Open three subreports: scale, financial, non-financial
                mainReport.OpenSubreport(Constants.RPT_NAME_BUSINESS_SCALE_REPORT).SetDataSource(businessReportModel.ScaleInfo);
                mainReport.OpenSubreport(Constants.RPT_NAME_BUSINESS_FINANCIAL_REPORT).SetDataSource(businessReportModel.FinancialInfo);
                mainReport.OpenSubreport(Constants.RPT_NAME_BUSINESS_NONFINANCIAL_REPORT).SetDataSource(businessReportModel.NonFinancialInfo);
                
                Stream stream = mainReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                // The report will be displayed on web interface before printing instead of downloading only
                return File(stream, Constants.RPT_DSP_OPT_IN_WEB);
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_RPT_REPORT;
                return RedirectToAction("Index", "Error");
            }
        }
    }
}