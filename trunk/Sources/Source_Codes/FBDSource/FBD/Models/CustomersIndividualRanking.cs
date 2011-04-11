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
    [MetadataType(typeof(CustomersIndividualRankingMetaData))]
    public partial class CustomersIndividualRanking
    {
        /// <summary>
        /// list of CustomersIndividualRanking
        /// </summary>
        /// <returns>list of CustomersIndividualRanking</returns>
        public static List<CustomersIndividualRanking> SelectIndividualRankings()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersIndividualRanking.OrderBy(m => m.ID).ToList();
        }

        /// <summary>
        /// return the Individual specified by id
        /// </summary>
        /// <param name="id">id of the Individual</param>
        /// <returns>Individual</returns>
        public static CustomersIndividualRanking SelectIndividualRankingByID(int id)
        {
            if (id <= 0) return null;
            FBDEntities entities = new FBDEntities();
            var Individual = entities.CustomersIndividualRanking.First(i => i.ID == id);

            return Individual;
        }

        public static CustomersIndividualRanking SelectRankingByDateAndCustomer(DateTime date, int customerID)
        {
            
            FBDEntities entities = new FBDEntities();
            var result = entities.CustomersIndividualRanking
                        .Include("CustomersIndividual")
                        .Include("Date")
                        .Where(i => i.CustomersIndividuals.IndividualID == customerID && i.Date.Value == date)
                        .Any();
            if (result)
            {
                return  entities.CustomersIndividualRanking
                        .Include("CustomersIndividual")
                        .Include("Date")
                        .First(i => i.CustomersIndividuals.IndividualID == customerID && i.Date.Value == date);
            }
            return null;
        }

        public static List<CustomersIndividualRanking> SelectRankingByDateAndCifAndBranch(DateTime date, string Cif, string BranchID)
        {
            FBDEntities entities = new FBDEntities();
            var result = entities.CustomersIndividualRanking
                .Include("CustomersIndividuals")
                .Include("CustomersIndividuals.SystemBranches")
                .Where(i => i.CustomersIndividuals.CIF.StartsWith(Cif) && i.CustomersIndividuals.SystemBranches.BranchID == BranchID && i.Date == date).ToList();
            return result;
        }

        /// <summary>
        /// return the Individual specified by id
        /// </summary>
        /// <param name="id">id of the Individual</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>Individual</returns>
        public static CustomersIndividualRanking SelectIndividualRankingByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var individual = entities.CustomersIndividualRanking.First(i => i.ID == id);
            return individual;
        }


        /// <summary>
        /// add new Individual
        /// </summary>
        /// <param name="Individual">the Individual to add</param>
        public static int AddIndividualRanking(CustomersIndividualRanking ranking)
        {
            if (ranking == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToCustomersIndividualRanking(ranking);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new Individual
        /// </summary>
        /// <param name="Individual">the Individual to add</param>
        public static int AddIndividualRanking(CustomersIndividualRanking ranking, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;


            entities.AddToCustomersIndividualRanking(ranking);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit new individual ranking
        /// </summary>
        /// <param name="individual">the individual to add</param>
        public static int EditIndividualRanking(CustomersIndividualRanking ranking, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;

            DatabaseHelper.AttachToOrGet<CustomersIndividualRanking>(entities, ranking.GetType().Name, ref ranking);


            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(ranking);
            stateEntry.SetModified();

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// delete the Ranking with the specified rankingID
        /// </summary>
        /// <param name="rankingID"> the rankingID deleted</param>
        public static int DeleteIndividualRanking(int id)
        {

            FBDEntities entities = new FBDEntities();
            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id, entities);
            entities.DeleteObject(ranking);
            int temp = entities.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }
        public class CustomersIndividualRankingMetaData
        {
        		
        	[DisplayName("ID")]
        	[Required]
            public int ID { get; set; }
        		
        	[DisplayName("Date")]
            public Nullable<System.DateTime> Date { get; set; }
        		
        	[DisplayName("Credit Department")]
        	[StringLength(255)]
            public string CreditDepartment { get; set; }
        		
        	[DisplayName("Total Debt")]
            public Nullable<decimal> TotalDebt { get; set; }
        		
        	[DisplayName("Basic Index Score")]
            public Nullable<decimal> BasicIndexScore { get; set; }
        		
        	[DisplayName("Collateral Index Score")]
            public Nullable<decimal> CollateralIndexScore { get; set; }
        		
        	[DisplayName("User ID")]
        	[StringLength(10)]
            public string UserID { get; set; }
        		
        	[DisplayName("Date Modified")]
            public Nullable<System.DateTime> DateModified { get; set; }
        }

        internal void LoadAll()
        {
            this.CustomersIndividualsReference.Load();
            this.CustomersLoanTermReference.Load();
            this.IndividualBorrowingPurposesReference.Load();
            this.IndividualSummaryRanksReference.Load();
            
        }
    }
}
