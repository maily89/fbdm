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

        /// <summary>
        /// Select All BusinessRanking in a report period  To list vector
        /// </summary>
        /// <returns></returns>

        public static List<Vector> SelectBusinessRankingToVector(string periodID)
        {
            FBDEntities entities = new FBDEntities();
            List<CustomersBusinessRanking> cbrList = entities.CustomersBusinessRanking
                                                             .Include(Constants.TABLE_CUSTOMERS_BUSINESSES)
                                                             .Include(Constants.TABLE_BUSINESS_RANKS)
                                                             .Include(Constants.TABLE_BUSINESS_CLUSTER_RANKS)
                                                             .Where(cbr=>cbr.SystemReportingPeriods!=null && periodID.Equals(cbr.SystemReportingPeriods.PeriodID))
                                                             .ToList();
            List<Vector> vList = new List<Vector>();
            foreach (CustomersBusinessRanking cbr in cbrList)
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
        /// Selecr businessRanking with RankID
        /// </summary>
        /// <returns></returns>
        public static List<Vector> SelectBusinessRankingToVector(string RankID, string periodID)
        {
            FBDEntities entities = new FBDEntities();
            List<CustomersBusinessRanking> cbrList = entities.CustomersBusinessRanking
                                                             .Include(Constants.TABLE_CUSTOMERS_BUSINESSES)
                                                             .Include(Constants.TABLE_BUSINESS_CLUSTER_RANKS)
                                                             .Where(cbr =>cbr.BusinessClusterRanks!=null && RankID.Equals(cbr.BusinessClusterRanks.RankID)
                                                             && cbr.SystemReportingPeriods != null && periodID.Equals(cbr.SystemReportingPeriods.PeriodID))
                                                             .ToList();
            List<Vector> vList = new List<Vector>();
            foreach (CustomersBusinessRanking cbr in cbrList)
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

        /// <summary>
        /// return the Business specified by id with period report object
        /// </summary>
        /// <param name="id">id of the Business</param>
        /// <returns>Business</returns>
        public static CustomersBusinessRanking SelectBusinessRankingWithPRByID(int id)
        {
            if (id <= 0) return null;
            FBDEntities entities = new FBDEntities();
            var Business = entities.CustomersBusinessRanking
                                   .Include("SystemReportingPeriods")
                                   .First(i => i.ID == id);

            return Business;
        }
        /// <summary>
        /// return the Business specified by RankID
        /// </summary>
        /// <param name="id">id of the Business</param>
        /// <returns>Business</returns>
        public static List<CustomersBusinessRanking> SelectBusinessRankingByClusterRankID(string RankID,FBDEntities entities)
        {
            if (RankID.Length< 1) return null;
            List<CustomersBusinessRanking> BusinessList = new List<CustomersBusinessRanking>();
            BusinessList = entities.CustomersBusinessRanking.Where(i => i.BusinessClusterRanks != null && RankID.Equals(i.BusinessClusterRanks.RankID)).ToList();

            return BusinessList;
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
                                                            .Include(Constants.TABLE_BUSINESS_CLUSTER_RANKS)
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
        public static int EditBusinessRanking(CustomersBusinessRanking ranking,FBD.ViewModels.RNKBusinessRankingViewModel rknBusinessRankingViewModel, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;

            var temp = SelectBusinessRankingByID(ranking.ID, entities);

            if(rknBusinessRankingViewModel.CustomerID>0)
            temp.CustomersBusinesses = CustomersBusinesses.SelectBusinessByID(rknBusinessRankingViewModel.CustomerID, entities);

            if(!string.IsNullOrEmpty(rknBusinessRankingViewModel.IndustryID))
            temp.BusinessIndustries = BusinessIndustries.SelectIndustryByID(rknBusinessRankingViewModel.IndustryID, entities);
            temp.BusinessTypes = BusinessTypes.SelectTypeByID(rknBusinessRankingViewModel.TypeID, entities);
            temp.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(rknBusinessRankingViewModel.LoanID, entities);
            temp.SystemReportingPeriods = SystemReportingPeriods.SelectReportingPeriodByID(rknBusinessRankingViewModel.PeriodID, entities);
            temp.SystemCustomerTypes = SystemCustomerTypes.SelectTypeByID(rknBusinessRankingViewModel.CustomerTypeID, entities);

            temp.CreditDepartment = ranking.CreditDepartment;
            temp.TaxCode = ranking.TaxCode;
            temp.CustomerGroup = ranking.CustomerGroup;
            temp.AuditedStatus = ranking.AuditedStatus;
            temp.TotalDebt = ranking.TotalDebt;
            temp.UserID = ranking.UserID;
            temp.DateModified = ranking.DateModified;

            int result=entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// This code used for update businessRanking when we have customerbusinessRanking and new businessclusterRanks
        /// </summary>
        /// <param name="cbr"></param>
        /// <param name="bcr"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int UpdateBusinessRanking(CustomersBusinessRanking cbr, BusinessClusterRanks bcr, FBDEntities entities)
        {
            cbr.BusinessClusterRanks = bcr;
            cbr.DateModified = DateTime.Now;
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;

        }
        /// <summary>
        /// This code use for update business rank 
        /// </summary>
        /// <param name="ID">CustomerBusinessRankID: key of CustomerBusinessRank</param>
        /// <param name="rankID">RankID,key of businessClusterRank(1,...10)</param>
        /// <param name="u">Vector u, which is centroid of cluster which rank RankID</param>
        /// <returns></returns>
        public static int UpdateBusinessRanking(int ID, string rankID, BusinessClusterRanks bcr,FBDEntities entities)
        {

            int result = 1;
            //declare the temp object, which is get customer business rank by id
            var temp = SelectBusinessRankingByID(ID, entities);//entities.CustomersBusinessRanking.First(i => i.ID.Equals(ID)); ;
            if (temp.BusinessClusterRanks == null || !temp.BusinessClusterRanks.RankID.Equals(rankID))
            {
                //This solution don't check how many cluster in DB. because we base on db to process clustering.
                //Check there are this rank in db, then update
                // List<BusinessClusterRanks> bsr = BusinessClusterRanks.SelectClusterRank(entities);
                /*if (!BusinessClusterRanks.IsExistRank(rankID, entities))
                {
                    BusinessClusterRanks bcr = new BusinessClusterRanks();
                    bcr.RankID = rankID;
                    bcr.Rank = "Group" + rankID;
                    BusinessClusterRanks.AddRank(bcr, entities,false);
                
                }*/
                //should check change here

                //then, update centroid for it 

                //BusinessClusterRanks bcr2 = BusinessClusterRanks.SelectClusterRankByID(rankID, entities);
                //bcr2.Centroid = Convert.ToDecimal(u.x);// In this clustering, we just need x values
                //BusinessClusterRanks.EditRank(bcr2, entities);

                /*
                var tempRank = BusinessRanks.SelectRankByID(rankID, entities);
                temp.BusinessRanks = tempRank;
                */
                //check the businessClusterRank is change or not

                temp.DateModified = DateTime.Now;
                temp.BusinessClusterRanks = bcr;

                result = entities.SaveChanges();
            }
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

        public static string GetBranchByRankingID(int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                var ranking = entities.CustomersBusinessRanking
                .Include("CustomersBusinesses")
                .Include("CustomersBusinesses.SystemBranches").First(m => m.ID == id);
                if (ranking.CustomersBusinesses == null || ranking.CustomersBusinesses.SystemBranches==null) return null;
                return ranking.CustomersBusinesses.SystemBranches.BranchID;
            }
            catch
            {
                return null;
            }
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
        /// <summary>
        /// Check a customerbusinessrank is in which group and then compare the old centroid with new centroid
        /// (new centroid is centroid after add this vector)
        /// note: this algorythm just use for db with already have cluster.
        /// </summary>
        /// <param name="cbr">customerbusinessranking need to be cluster</param>
        /// <param name="epsilon">difference distance suggest by user</param>
        public static void cluster(CustomersBusinessRanking cbr, double epsilon,string periodID,FBDEntities entity )
        {
            //Get all businessclusterrank
            List<BusinessClusterRanks> bcrList = BusinessClusterRanks.SelectClusterRank(entity);
            List<Vector> vList = new List<Vector>();
            //convert all bcr to vector to caculate
            foreach (BusinessClusterRanks bcr in bcrList)
            {
                Vector v = new Vector(bcr);
                vList.Add(v);
            }
            //Convert customerbusinessranking to vector too.
            Vector Vcbr = new Vector(cbr);
            //Get the group customerbusinessrank which smallest distant to this cbr
            int groupNo = Caculator.minDistant(Vcbr, vList.ToArray());
            string GroupRankID = bcrList.ElementAt(groupNo).RankID;
            //Get all customer in this group
            List<Vector> ListVcbr = CustomersBusinessRanking.SelectBusinessRankingToVector(GroupRankID, periodID);
            Vector VOldCentroid = Caculator.centroid(ListVcbr);
            ListVcbr.Add(Vcbr);
            Vector VNewCentroid = Caculator.centroid(ListVcbr);

            //If distance is < epsilon
            if (Caculator.Distant(VOldCentroid, VNewCentroid) < epsilon)
            {
                //update centroid of BusinessRank to new
                BusinessClusterRanks.UpdateCentroid(GroupRankID, VNewCentroid, entity);
                //Update this customrclusterRank to this groupRankID
                CustomersBusinessRanking.UpdateBusinessRanking(cbr.ID, GroupRankID, bcrList.ElementAt(groupNo),entity);
            }
            else
            {
                //mining again and update all customerbusinessRanking
                //This process cost alot of time- I hope this not happen frequency
                List<Vector> VlistToMining = CustomersBusinessRanking.SelectBusinessRankingToVector(periodID);
                List<Vector>[] result = KMean.Clustering(bcrList.Count, VlistToMining, vList);

                for (int i = 0; i < bcrList.Count; i++)
                {
                    Vector centroid = Caculator.centroid(result[i]);
                    //update centroid list
                    BusinessClusterRanks.UpdateCentroid(bcrList[i].RankID, centroid, entity);
                    //then update all customerbusinessrank in this group
                    foreach (Vector v in result[i])
                        UpdateBusinessRanking(v.ID, v.RankID.ToString(), bcrList[i],entity);
                }

            }

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
