using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.CommonUtilities;

namespace FBD.Models
{
    public class RPTBusinessReportModel
    {
        // Error Message
        public string ErrReport = "";
        public string ErrGeneralInfo = "";
        public string ErrScaleInfo = "";
        public string ErrFinancialInfo = "";
        public string ErrNonFinancialInfo = "";

        // General Information
        public string CIFNumber = Constants.RPT_MISSING_VALUE;
        public string CustomerName = Constants.RPT_MISSING_VALUE;
        public string Industry = Constants.RPT_MISSING_VALUE;
        public string BusinessType = Constants.RPT_MISSING_VALUE;
        public string TaxCode = Constants.RPT_MISSING_VALUE;
        public string Branch = Constants.RPT_MISSING_VALUE;
        public string ReportingPeriod = Constants.RPT_MISSING_VALUE;
        public string Scale = Constants.RPT_MISSING_VALUE;
        public decimal FinancialScore;
        public decimal NonFinancialScore;
        public string Rank = Constants.RPT_MISSING_VALUE;
        public string Evaluation = Constants.RPT_MISSING_VALUE;
        
        /// <summary>
        /// Scale information of the customer
        /// </summary>
        public List<RPTScaleReportModel> ScaleInfo = new List<RPTScaleReportModel>();
        
        /// <summary>
        /// Financial information of the customer
        /// </summary>
        public List<RPTFinancialReportModel> FinancialInfo = new List<RPTFinancialReportModel>();

        /// <summary>
        /// Non-Financial information of the customer
        /// </summary>
        public List<RPTNonFinancialReportModel> NonFinancialInfo = new List<RPTNonFinancialReportModel>();
    }
}
