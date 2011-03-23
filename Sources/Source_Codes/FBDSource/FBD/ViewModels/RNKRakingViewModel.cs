using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public static class RNKRankingViewModel
    {
        public static List<BusinessIndustries> BusinessIndustries
        {

            get
            {
                try
                {
                    var temp = FBD.Models.BusinessIndustries.SelectIndustries();
                    return temp;
                }
                catch
                {
                    return null;
                }
            }

        }
        public static List<SystemReportingPeriods> ReportingPeriods 
        {
            get
            {
                try
                {
                    var temp= SystemReportingPeriods.SelectReportingPeriods();
                    return temp;
                }
                catch
                {
                    return null;
                }
            }
        }
        
        public static List<BusinessLines> BusinessLines
        {
            get
            {
                try
                {
                    var temp = FBD.Models.BusinessLines.SelectLines();
                    return temp;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static List<BusinessTypes> BusinessTypes
        {
            get
            {
                try
                {
                    var temp = FBD.Models.BusinessTypes.SelectTypes();
                    return temp;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static List<CustomersLoanTerm> LoanTerm
        {
            get
            {
                try
                {
                    var temp = FBD.Models.CustomersLoanTerm.SelectLoanTerms();
                    return temp;
                }
                catch
                {
                    return new List<CustomersLoanTerm>();
                }
            }
        }
        //What the hell is this?
        public static List<SystemCustomerTypes> CustomerType
        {
            get
            {
                return new List<SystemCustomerTypes>();
            }
        }

    }
}