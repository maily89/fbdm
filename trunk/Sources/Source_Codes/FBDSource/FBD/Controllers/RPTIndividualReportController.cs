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
    public class RPTIndividualReportController : Controller
    {
        /// GET: /RPTIndividualReport/ExportIndividualInfo/5
        public ActionResult ExportIndividualInfo(int ID)
        {
            try
            {
                FBDEntities FBDModel = new FBDEntities();
                RPTIIndividualReportService individualReportService = new RPTIIndividualReportServiceImpl();
                RPTIndividualReportModel individualReportModel = individualReportService.SelectIndividualInfo(FBDModel, ID);

                List<RPTIndividualReportModel> mainReportDataSource = new List<RPTIndividualReportModel>();
                mainReportDataSource.Add(individualReportModel);

                IndividualGeneralReport mainReport = new IndividualGeneralReport();
                mainReport.SetDataSource(mainReportDataSource);

                mainReport.OpenSubreport(Constants.RPT_NAME_INDIVIDUAL_BASIC_REPORT).SetDataSource(individualReportModel.BasicInfo);
                mainReport.OpenSubreport(Constants.RPT_NAME_INDIVIDUAL_COLLATERAL_REPORT).SetDataSource(individualReportModel.CollateralInfo);

                Stream stream = mainReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
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
