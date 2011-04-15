using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType(typeof(CustomersBusinessRankingMetaData))]
    public partial class CustomersBusinessRanking
    {
        /// <summary>
        /// list of CustomersBusinessRanking
        /// </summary>
        /// <returns>list of CustomersBusinessRanking</returns>
        public static List<CustomersBusinessRanking> SelectBusinessRankings()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersBusinessRanking.OrderBy(m => m.ID).ToList();
        }

        public static List<CustomersBusinessRanking> SelectBusinessRankings(string OrderBy)
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersBusinessRanking.OrderBy(OrderBy).ToList();
        }

        //public static List<CustomersBusinessRanking> SelectBusinessRankingsDeOrder(string DeOrderBy, string Descending)
        //{
        //    FBDEntities entities = new FBDEntities();
        //    return entities.CustomersBusinessRanking.OrderByDescending(DeOrderBy).ToList();
        //}

        public static List<Vector> SelectBusinessRankingToVector()
        {
            FBDEntities entities = new FBDEntities();
            List<CustomersBusinessRanking> cbrList = entities.CustomersBusinessRanking
                                                             .Include(Constants.TABLE_CUSTOMERS_BUSINESSES)
                                                             .Include(Constants.TABLE_BUSINESS_RANKS).ToList();
            List<Vector> vList = new List<Vector>();
            foreach(CustomersBusinessRanking cbr in cbrList)
            {
                if (cbr.FinancialScore != null && cbr.NonFinancialScore != null)
                {
                    Vector v = new Vector(cbr);
                    vList.Add(v);
                }
            }
            return vList;
        }

        /// <summary>
        /// return the Business specified by id
        /// </summary>
        /// <param name="id">id of the Business</param>
        /// <returns>Business</returns>
        public static CustomersBusinessRanking SelectBusinessRankingByID(int id)
        {
            if (id <= 0) return null;
            FBDEntities entities = new FBDEntities();
            var Business = entities.CustomersBusinessRanking.First(i => i.ID == id);

            return Business;
        }

        public static CustomersBusinessRanking SelectRankingByPeriodAndCustomer(string periodID, int customerID)
        {
            FBDEntities entities = new FBDEntities();
            var result = entities.CustomersBusinessRanking.Include(Constants.TABLE_SYSTEM_REPORTING_PERIODS)
                .Include(Constants.TABLE_CUSTOMERS_BUSINESSES)
                .Where(i => i.CustomersBusinesses.BusinessID == customerID && i.SystemReportingPeriods.PeriodID == periodID).Any();
            if (result)
            {
                return entities.CustomersBusinessRanking
                                .Include(Constants.TABLE_SYSTEM_REPORTING_PERIODS)
                                .Include(Constants.TABLE_CUSTOMERS_BUSINESSES)
                                .Where(i => i.CustomersBusinesses.BusinessID == customerID && i.SystemReportingPeriods.PeriodID == periodID).First();
            }
            return null;
        }

        public static List<CustomersBusinessRanking> SelectRankingByPeriodAndCifAndBranch(string periodID, string cif, string branchID)
        {
            FBDEntities entities = new FBDEntities();
            bool isCifTested=false;
            bool isPeriodTested=false;
            bool isBranchTested=false;

            if (!string.IsNullOrEmpty(cif)) // neu cif khong null
            {
                isCifTested=true;
            }
            if (!string.IsNullOrEmpty(periodID)) // neu period id k0 null
            {
                isPeriodTested=true;
            }
            if (!string.IsNullOrEmpty(branchID)) // neu branchID k0 null
            {
                isBranchTested=true;
            }

            if (!isCifTested && !isBranchTested && !isPeriodTested) // neu ca 3 gia tri deu k0 dc test
                return SelectBusinessRankings();

            var result = entities.CustomersBusinessRanking
                .Include("SystemReportingPeriods")
                .Include("CustomersBusinesses")
                .Include("CustomersBusinesses.SystemBranches")
                .Where(i => (!isCifTested || i.CustomersBusinesses.CIF.StartsWith(cif)) && (!isPeriodTested || i.SystemReportingPeriods.PeriodID == periodID) && (!isBranchTested || i.CustomersBusinesses.SystemBranches.BranchID == branchID));


            return result.ToList();
        }
        /// <summary>
        /// return the Business specified by id
        /// </summary>
        /// <param name="id">id of the Business</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>Business</returns>
        public static CustomersBusinessRanking SelectBusinessRankingByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var business = entities.CustomersBusinessRanking.First(i => i.ID == id);
            return business;
        }

        public static CustomersBusinessRanking SelectBusinessRankingByIDWithReference(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var business = entities.CustomersBusinessRanking.Include(Constants.TABLE_CUSTOMERS_BUSINESSES)
                                                            .Include(Constants.TABLE_SYSTEM_REPORTING_PERIODS)
                                                            .Include(Constants.TABLE_BUSINESS_INDUSTRIES)
                                                            .Include(Constants.TABLE_BUSINESS_TYPES)
                                                            .Include(Constants.TABLE_CUSTOMERS_LOAN_TERM)
                                                            .Include(Constants.TABLE_SYSTEM_CUSTOMER_TYPE)
                                                            .Include(Constants.TABLE_BUSINESS_SCALES)
                                                            .Include(Constants.TABLE_BUSINESS_RANKS)
                                                            .First(i => i.ID == id);
            return business;
        }

        /// <summary>
        /// add new Business
        /// </summary>
        /// <param name="Business">the Business to add</param>
        public static int AddBusinessRanking(CustomersBusinessRanking ranking)
        {
            if (ranking == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToCustomersBusinessRanking(ranking);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new Business
        /// </summary>
        /// <param name="Business">the Business to add</param>
        public static int AddBusinessRanking(CustomersBusinessRanking ranking, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;


            entities.AddToCustomersBusinessRanking(ranking);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit new business ranking
        /// </summary>
        /// <param name="business">the business to add</param>
        public static int EditBusinessRanking(CustomersBusinessRanking ranking, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;

            DatabaseHelper.AttachToOrGet<CustomersBusinessRanking>(entities, ranking.GetType().Name, ref ranking);

            
            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(ranking);
            stateEntry.SetModified();

            int result=entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        //should change type of rank ID
        public static int UpdateBusinessRanking(int ID, string rankID)
        {
            
            FBDEntities entities = new FBDEntities();
            var temp = entities.CustomersBusinessRanking.First(i => i.ID == ID); ;
            var tempRank = BusinessRanks.SelectRankByID(rankID, entities);
            temp.BusinessRanks = tempRank;
            temp.DateModified = DateTime.Now ;
            
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// delete the Ranking with the specified rankingID
        /// </summary>
        /// <param name="rankingID"> the rankingID deleted</param>
        public static int DeleteBusinessRanking(int id)
        {
            
            FBDEntities entities = new FBDEntities();
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id, entities);
            entities.DeleteObject(ranking);
            int temp = entities.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public void LoadAll()
        {
            
            this.BusinessIndustriesReference.Load();
            this.BusinessRanksReference.Load();
            this.BusinessScalesReference.Load();
            this.BusinessTypesReference.Load();
            this.CustomersBusinessesReference.Load();
            this.CustomersLoanTermReference.Load();
            this.SystemCustomerTypesReference.Load();
            this.SystemReportingPeriodsReference.Load();

        }
        public class CustomersBusinessRankingMetaData
        {
        		
        	[DisplayName("ID")]
        	[Required]
            public int ID { get; set; }
        		
        	[DisplayName("Credit Department")]
        	[StringLength(255)]
            public string CreditDepartment { get; set; }
        		
        	[DisplayName("Tax Code")]
        	[StringLength(20)]
            public string TaxCode { get; set; }
        		
        	[DisplayName("Customer Group")]
        	[StringLength(1)]
            public string CustomerGroup { get; set; }
        		
        	[DisplayName("Audited Status")]
        	[StringLength(1)]
            public string AuditedStatus { get; set; }
        		
        	[DisplayName("Total Debt")]
            public Nullable<decimal> TotalDebt { get; set; }
        		
        		
        	[DisplayName("Financial Score")]
            public Nullable<decimal> FinancialScore { get; set; }
        		
        	[DisplayName("Non Financial Score")]
            public Nullable<decimal> NonFinancialScore { get; set; }
        		
        	[DisplayName("User ID")]
        	[StringLength(10)]
            public string UserID { get; set; }
        		
        	[DisplayName("Date Modified")]
            public Nullable<System.DateTime> DateModified { get; set; }
        }
    }
}
