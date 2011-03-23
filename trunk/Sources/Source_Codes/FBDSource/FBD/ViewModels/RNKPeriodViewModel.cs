using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class RNKPeriodViewModel
    {
        public List<CustomersBusinesses> BusinessCustomer { get; set; }
        public List<SystemReportingPeriods> ReportingPeriods { get; set; }
        public string PeriodID { get; set; }
        public int CustomerID { get; set; }
                    
    }
}