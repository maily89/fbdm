using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.ViewModels
{
    public class RNKScaleRow
    {
        public int RankingID { get; set; }
        public string CriteriaID { get; set; }
        public string CriteriaName { get; set; }
        public Nullable<decimal> Value { get; set; }
        public int CustomerScaleID { get; set; }
        public Nullable<decimal> Score { get; set; }
    }
}
