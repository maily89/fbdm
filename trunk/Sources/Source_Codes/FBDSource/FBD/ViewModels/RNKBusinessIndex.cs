using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class RNKBusinessIndex
    {
        public List<CustomersBusinessRanking> CustomerRanking { get; set; }
        public string PeriodID { get; set; }
        public string BranchID { get; set; }
        public string Cif { get; set; }
    }
}