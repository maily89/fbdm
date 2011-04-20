using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class RNKCustomerInfo
    {
        public string CIF { set; get; }
        public string CustomerName { set; get; }
        public string ReportingPeriod { set; get; }
        public string Branch { set; get; }
        public DateTime Date { get; set; }
        public static RNKCustomerInfo GetBusinessRankingInfo(int id)
        {

            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
            if (ranking == null) return new RNKCustomerInfo();
            ranking.CustomersBusinessesReference.Load();
            var customer = ranking.CustomersBusinesses;

            var temp = new RNKCustomerInfo();
            temp.CIF = customer.CIF;
            temp.CustomerName = customer.CustomerName;

            ranking.SystemReportingPeriodsReference.Load();
            if(ranking.SystemReportingPeriods!=null) 
            temp.ReportingPeriod = ranking.SystemReportingPeriods.PeriodName;

            customer.SystemBranchesReference.Load();
            if(customer.SystemBranches!=null)
            temp.Branch = customer.SystemBranches.BranchName;

            return temp;
        }
        public static RNKCustomerInfo GetIndividualRankingInfo(int id)
        {

            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);
            if (ranking == null) return new RNKCustomerInfo();
            ranking.CustomersIndividualsReference.Load();
            var customer = ranking.CustomersIndividuals;

            var temp = new RNKCustomerInfo();
            temp.CIF = customer.CIF;
            temp.CustomerName = customer.CustomerName;

            if(ranking.Date!=null)
            temp.Date = ranking.Date.Value;

            customer.SystemBranchesReference.Load();
            if(customer.SystemBranches!=null)
            temp.Branch = customer.SystemBranches.BranchName;

            return temp;
        }
    }
    public static class RNKRankingViewModel
    {
        public static List<SystemBranches> SystemBranch
        {
            get
            {
                try
                {
                    var temp = FBD.Models.SystemBranches.SelectBranches();
                    return temp;
                }
                catch
                {
                    return null;
                }
            }
        }
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
        
        public static List<SystemCustomerTypes> CustomerType
        {
            get
            {
                try
                {
                    var temp = FBD.Models.SystemCustomerTypes.SelectTypes();
                    return temp;
                }
                catch
                {
                    return new List<SystemCustomerTypes>();
                }
            }
        }

        public static List<IndividualBorrowingPurposes> BorrowingPurpose
        {
            get
            {
                try
                {
                    var temp = IndividualBorrowingPurposes.SelectBorrowingPPList();
                    return temp;
                }
                catch
                {
                    return new List<IndividualBorrowingPurposes>();
                }
            }
        }

        
    }
}