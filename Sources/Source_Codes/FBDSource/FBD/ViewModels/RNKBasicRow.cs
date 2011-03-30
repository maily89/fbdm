using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKBasicRow
    {
        public int RankingID { get; set; }
        
        public IndividualBasicIndex Index { get; set; }
        public List<IndividualBasicIndexScore> ScoreList { get; set; }
        public int ScoreID { get; set; }
        public Nullable<decimal> Score { get; set; }
        public int CustomerBasicID { get; set; }
        public bool LeafIndex { get; set; }
    }
}
