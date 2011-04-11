using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.ViewModels
{
    public class RNKRankIndividualFinal
    {

        public decimal BasicScore { get; set; }
        public string BasicRank { get; set; }

        public decimal CollateralScore { get; set; }
        public string CollateralRank { get; set; }

        public string ClassRank { get; set; }
        public string ClusterRank { get; set; }
    }
}
