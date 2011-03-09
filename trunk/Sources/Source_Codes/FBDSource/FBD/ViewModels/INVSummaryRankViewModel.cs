using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class INVSummaryRankViewModel
    {
        public List<IndividualBasicRanks> basicRanks { get; set; }

        public List<IndividualCollateralRanks> collateralRanks{ get; set; }

        public IndividualSummaryRanks summaryRanks{ get; set; }

        public string basicRankID{ get; set; }
        public string collateralRankID{ get; set; }
        public string Evaluation{ get; set; }
    }
}
