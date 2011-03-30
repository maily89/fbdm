using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;

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
            var result = entities.CustomersBusinessRanking.Include("SystemReportingPeriods")
                .Include("CustomersBusinesses")
                .Where(i => i.CustomersBusinesses.BusinessID == customerID && i.SystemReportingPeriods.PeriodID == periodID).Any();
            if (result)
            {
                return entities.CustomersBusinessRanking
                                .Include("SystemReportingPeriods")
                                .Include("CustomersBusinesses")
                                .Where(i => i.CustomersBusinesses.BusinessID == customerID && i.SystemReportingPeriods.PeriodID == periodID).First();
            }
            return null;
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
