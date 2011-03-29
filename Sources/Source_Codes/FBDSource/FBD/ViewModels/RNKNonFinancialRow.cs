using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKNonFinancialRow
    {
        public int RankingID { get; set; }
        
        public BusinessNonFinancialIndex Index { get; set; }
        public List<BusinessNonFinancialIndexScore> ScoreList { get; set; }
        public int ScoreID { get; set; }
        public Nullable<decimal> Score { get; set; }
        public int CustomerNonFinancialID { get; set; }
        public bool LeafIndex { get; set; }
    }
}
