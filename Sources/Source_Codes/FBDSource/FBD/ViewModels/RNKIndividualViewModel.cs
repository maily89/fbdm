using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class RNKIndividualViewModel
    {
        public List<CustomersIndividuals> IndividualCustomer { get; set; }
        public CustomersIndividualRanking IndividualRanking { get; set; }
        public DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public string CIF { get; set; }
        public string LoanTermID { get; set; }
        public string PurposeID { get; set; }   
    }
}