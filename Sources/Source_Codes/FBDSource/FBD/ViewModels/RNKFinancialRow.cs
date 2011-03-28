using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKFinancialRow
    {
        public int RankingID { get; set; }
        public int FinancialScoreID { get; set; }
        public BusinessFinancialIndex Index { get; set; }
        public List<BusinessFinancialIndexScore> ScoreList { get; set; }
        public int ScoreID { get; set; }
        public Nullable<decimal> Score { get; set; }
    }
}
