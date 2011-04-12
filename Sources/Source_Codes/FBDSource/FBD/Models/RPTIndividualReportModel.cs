using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.CommonUtilities;

namespace FBD.Models
{
    public class RPTIndividualReportModel
    {
        // Error Message
        public string ErrReport = "";
        public string ErrGeneralInfo = "";
        public string ErrBasicInfo = "";
        public string ErrCollateralInfo = "";

        // General Information
        public string CIFNumber = Constants.RPT_MISSING_VALUE;
        public string CustomerName = Constants.RPT_MISSING_VALUE;
        public string BorrowingPurpose = Constants.RPT_MISSING_VALUE;
        public string Branch = Constants.RPT_MISSING_VALUE;
        public DateTime RankedDate;
        public decimal BasicScore;
        public decimal CollateralScore;
        public string Evaluation = Constants.RPT_MISSING_VALUE;

        /// <summary>
        /// Basic information of the customer
        /// </summary>
        public List<RPTBasicInfoReportModel> BasicInfo = new List<RPTBasicInfoReportModel>();

        /// <summary>
        /// Collateral information of the customer
        /// </summary>
        public List<RPTCollateralInfoReportModel> CollateralInfo = new List<RPTCollateralInfoReportModel>();
    }
}
