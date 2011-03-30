using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKCollateralRow
    {
        public int RankingID { get; set; }
        
        public IndividualCollateralIndex Index { get; set; }
        public List<IndividualCollateralIndexScore> ScoreList { get; set; }
        public int ScoreID { get; set; }
        public Nullable<decimal> Score { get; set; }
        public int CustomerCollateralID { get; set; }
        public bool LeafIndex { get; set; }
    }
}
