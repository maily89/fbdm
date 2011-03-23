using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class RNKCustomerBusinessViewModel
    {
        public CustomersBusinesses CustomerBusiness { get; set; }
        public List<SystemBranches> SystemBranches { get; set; }
        public string BranchID { get; set; }
    }
}
