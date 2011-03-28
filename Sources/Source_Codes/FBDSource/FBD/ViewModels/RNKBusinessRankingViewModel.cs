using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class RNKBusinessRankingViewModel
    {
        public CustomersBusinessRanking BusinessRanking { get; set; }
        public int CustomerID { get; set; }
        public string PeriodID { get; set; }
        public string IndustryID { get; set; }
        public string LineID { get; set; }
        public string TypeID { get; set; }
        public string LoanID { get; set; }
        public string CustomerTypeID { get; set; }
        public string CIF { get; set; }
        public bool IsNew { get; set; }
       
    }
}