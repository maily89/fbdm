using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.ViewModels
{
    public class RNKRankFinal
    {
        public decimal ScaleScore { get; set; }
        public string Scale { get; set; }
        public decimal FinancialScore { get; set; }
        public decimal FinancialProportion { get; set; }
        public decimal FinancialResult { get; set; }
        public decimal NonFinancialScore { get; set; }
        public decimal NonFinancialProportion { get; set; }
        public decimal NonFinancialResult { get; set; }

        public decimal TotalScore { get; set; }
        public string ClassRank { get; set; }
        public string ClusterRank { get; set; }
    }
}
