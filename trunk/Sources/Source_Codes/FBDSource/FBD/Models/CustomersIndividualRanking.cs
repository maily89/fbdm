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
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Vector> SelectIndividualRankingToVector()
        {
            FBDEntities entities = new FBDEntities();
            //may be miss include. Should consider all flow!!!
            List<CustomersIndividualRanking> cbrList = entities.CustomersIndividualRanking
                                                                .Include("CustomersIndividuals")
                                                                .Include("IndividualClusterRanks")
                                                                .ToList();
            List<Vector> vList = new List<Vector>();
            foreach (CustomersIndividualRanking cbr in cbrList)
            {
                if (cbr.BasicIndexScore != null && cbr.CollateralIndexScore != null)
                {
                    Vector v = new Vector(cbr);
                    vList.Add(v);
                }
            }
            return vList;
        }

        /// <summary>
        /// Select to vector in a rank of time
        /// </summary>
        /// <returns></returns>
        public static List<Vector> SelectIndividualRankingToVector(DateTime fromDate,DateTime toDate)
        {
            FBDEntities entities = new FBDEntities();
            //may be miss include. Should consider all flow!!!
            List<CustomersIndividualRanking> cbrList = entities.CustomersIndividualRanking
                                                                .Include("CustomersIndividuals")
                                                                .Include("IndividualClusterRanks")
                                                                .Where(i=>i.Date> fromDate && i.Date<toDate)
                                                                .ToList();
            List<Vector> vList = new List<Vector>();
            foreach (CustomersIndividualRanking cbr in cbrList)
            {
                if (cbr.BasicIndexScore != null && cbr.CollateralIndexScore != null)
                {
                    Vector v = new Vector(cbr);
                    vList.Add(v);
                }
            }
            return vList;
        }

        /// <summary>
        ///Select all customerRank with have Rank ID = Rank ID 
        /// </summary>
        /// <param name="RankID">Rank of Customer Rank</param>
        /// <returns></returns>
        public static List<Vector> SelectIndividualRankingToVector(string RankID)
        {
            FBDEntities entities = new FBDEntities();
            //using this function only when you are sure albout IndividualClusterRanks of this kind of customer
            List<CustomersIndividualRanking> cbrList = entities.CustomersIndividualRanking
                                                                .Include(Constants.TABLE_INDIVIDUAL_CLUSTER_RANKS)
                                                                .Where(cir => cir.IndividualClusterRanks!=null && RankID.Equals(cir.IndividualClusterRanks.RankID))
                                                                .ToList();
            List<Vector> vList = new List<Vector>();
            foreach (CustomersIndividualRanking cbr in cbrList)
            {
                if (cbr.BasicIndexScore != null && cbr.CollateralIndexScore != null)
                {
                    Vector v = new Vector(cbr);
                    vList.Add(v);
                }
            }
            return vList;
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
            var Individual = entities.CustomersIndividualRanking
                                            .First(i => i.ID == id);

            return Individual;
        }
        /// <summary>
        /// Select all customer individual ranking by rankID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<CustomersIndividualRanking> SelectIndividualRankingByClusterRankID(string RankID,FBDEntities entities)
        {
            if (RankID.Length <= 0) return null;
            List<CustomersIndividualRanking> cirList = new List<CustomersIndividualRanking>();
            cirList = entities.CustomersIndividualRanking.Where(i => i.IndividualClusterRanks != null && RankID.Equals(i.IndividualClusterRanks.RankID)).ToList();
            return cirList;
        }

        public static CustomersIndividualRanking SelectRankingByDateAndCustomer(DateTime date, int customerID)
        {
            
            FBDEntities entities = new FBDEntities();
            try
            {
                return entities.CustomersIndividualRanking
                        .Include("CustomersIndividuals")
                        .First(i => i.CustomersIndividuals.IndividualID == customerID && i.Date.Value == date);
            }
            catch
            {
                return null;
            }
        }

        public static List<CustomersIndividualRanking> SelectRankingByDateAndCifAndBranch(Nullable<DateTime> fromDate, Nullable<DateTime> toDate, string Cif, string BranchID)
        {
            FBDEntities entities = new FBDEntities();
            bool isFromDateTested = fromDate != null ? true : false;
            bool isToDateTested = toDate != null ? true : false;
            bool isCifTested = !string.IsNullOrEmpty(Cif) ? true : false;
            bool isBranchIDTested = !string.IsNullOrEmpty(BranchID) ? true : false;

            if (!isFromDateTested && !isToDateTested && !isCifTested && !isBranchIDTested)
                return SelectIndividualRankings();

            var result = entities.CustomersIndividualRanking
                .Include("CustomersIndividuals")
                .Include("CustomersIndividuals.SystemBranches")
                .Where(i =>(!isCifTested || i.CustomersIndividuals.CIF.StartsWith(Cif)) 
                    && (!isBranchIDTested || i.CustomersIndividuals.SystemBranches.BranchID == BranchID) 
                    && (!isFromDateTested || i.Date >= fromDate) && (!isToDateTested || i.Date<=toDate)).ToList();
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
            var individual = entities.CustomersIndividualRanking
                            .Include("IndividualClusterRanks")
                            .First(i => i.ID == id);
            return individual;
        }

        public static CustomersIndividualRanking SelectIndividualRankingByIDWithReference(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var individual = entities.CustomersIndividualRanking
                                            .Include(Constants.TABLE_CUSTOMERS_INDIVIDUALS)
                                            .Include(Constants.TABLE_INDIVIDUAL_BORROWING_PURPOSES)
                                            .Include(Constants.TABLE_CUSTOMERS_LOAN_TERM)
                                            .Include(Constants.TABLE_INDIVIDUAL_SUMMARY_RANKS)
                                            .First(i => i.ID == id);
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
        public static int EditIndividualRanking(CustomersIndividualRanking ranking,FBD.ViewModels.RNKIndividualViewModel model, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;

            var temp = CustomersIndividualRanking.SelectIndividualRankingByID(ranking.ID,entities);
            temp.CustomersIndividuals = CustomersIndividuals.SelectIndividualByID(model.CustomerID, entities);
            temp.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(model.LoanTermID, entities);
            temp.IndividualBorrowingPurposes = IndividualBorrowingPurposes.SelectBorrowingPPByID(model.PurposeID, entities);

            temp.CreditDepartment = ranking.CreditDepartment;
            temp.TotalDebt = ranking.TotalDebt;
            temp.UserID = ranking.UserID;
            temp.DateModified = ranking.DateModified;
           

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
        /// <summary>
        /// this code use for update customer individual ranking when we have customerindividualranking and individualClusterRank
        /// </summary>
        /// <param name="cir"></param>
        /// <param name="icr"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int UpdateIndividualRanking(CustomersIndividualRanking cir, IndividualClusterRanks icr, FBDEntities entities)
        {
            cir.IndividualClusterRanks = icr;
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        /// <summary>
        /// This code use for update business rank and centroid list
        /// </summary>
        /// <param name="ID">CustomerBusinessRankID: key of CustomerBusinessRank</param>
        /// <param name="rankID">RankID,key of IndividualClusterRank(1,...10)</param>
        /// <returns></returns>
        public static int UpdateIndividualRanking(int ID, string rankID, IndividualClusterRanks icr,FBDEntities entities)
        {
            //declare the temp object, which is get customer business rank by id
            var temp = SelectIndividualRankingByID(ID, entities);//entities.CustomersIndividualRanking.First(i => i.ID.Equals(ID));
            int result = 1;
            if (temp.IndividualClusterRanks == null || !temp.IndividualClusterRanks.RankID.Equals(rankID))
            {
                //This solution don't check how many cluster in DB. because we base on db to process clustering.
                //Check there are this rank in db, then update

                //if (!IndividualClusterRanks.IsExistRank(rankID, entities))
                //{
                //    IndividualClusterRanks icr = new IndividualClusterRanks();
                //    icr.ID = rankID;
                //    icr.Rank = "Group" + rankID;
                //    IndividualClusterRanks.AddRank(icr, entities);
                //}
                //then, update centroid for it 

                //IndividualClusterRanks icr2 = IndividualClusterRanks.SelectClusterRankByID(rankID, entities);
                //icr2.CentroidX = Convert.ToDecimal(u.x);// In this clustering, we just need x values
                //icr2.CentroidY = Convert.ToDecimal(u.y);
                //IndividualClusterRanks.EditRank(icr2, entities);
                /*
                var tempRank = BusinessRanks.SelectRankByID(rankID, entities);
                temp.BusinessRanks = tempRank;
                */
                temp.DateModified = DateTime.Now;
                temp.IndividualClusterRanks = icr;

                result = entities.SaveChanges();
            }
            return result <= 0 ? 0 : 1;
        }

        public void cluster(CustomersIndividualRanking cir, double epsilon)
        {
            FBDEntities entity = new FBDEntities();
            //Get all businessclusterrank
            List<IndividualClusterRanks> icrList = IndividualClusterRanks.SelectClusterRank(entity);
            List<Vector> vList = new List<Vector>();
            //convert all bcr to vector to caculate
            foreach (IndividualClusterRanks icr in icrList)
            {
                Vector v = new Vector(icr);
                vList.Add(v);
            }
            //Convert customerbusinessranking to vector too.
            Vector Vcir = new Vector(cir);
            //Get the group customerbusinessrank which smallest distant to this cbr
            int groupNo = Caculator.minDistant(Vcir, vList.ToArray());
            string GroupRankID = icrList.ElementAt(groupNo).RankID;
            //Get all customer in this group
            List<Vector> ListVcir = CustomersIndividualRanking.SelectIndividualRankingToVector(GroupRankID.ToString());
            Vector VOldCentroid = Caculator.centroid(ListVcir);
            ListVcir.Add(Vcir);
            Vector VNewCentroid = Caculator.centroid(ListVcir);

            //If distance is < epsilon
            if (Caculator.Distant(VOldCentroid, VNewCentroid) < epsilon)
            {
                //update centroid of BusinessRank to new
                IndividualClusterRanks.updateCentroid(GroupRankID,VNewCentroid,entity);
                //Update this customrclusterRank to this groupRankID
                CustomersIndividualRanking.UpdateIndividualRanking(cir.ID, GroupRankID, icrList.ElementAt(groupNo),entity);
            }
            else
            {
                //mining again and update all customerbusinessRanking
                //This process cost alot of time- I hope this not happen frequency
                List<Vector> VlistToMining = CustomersIndividualRanking.SelectIndividualRankingToVector();
                List<Vector>[] result = KMean.Clustering(icrList.Count, VlistToMining, vList);

                for (int i = 0; i < icrList.Count; i++)
                {
                    Vector centroid = Caculator.centroid(result[i]);
                    //update centroid list
                    IndividualClusterRanks.updateCentroid(icrList[i].RankID, centroid, entity);
                    //then update all customerbusinessrank in this group
                    foreach (Vector v in result[i])
                        UpdateIndividualRanking(v.ID, v.RankID.ToString(), icrList[i],entity);
                }

            }
        }

        public static string GetBranchByRankingID(int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                var ranking = entities.CustomersIndividualRanking
                .Include("CustomersIndividuals")
                .Include("CustomersIndividuals.SystemBranches").First(m => m.ID == id);
                if (ranking.CustomersIndividuals == null || ranking.CustomersIndividuals.SystemBranches == null) return null;
                return ranking.CustomersIndividuals.SystemBranches.BranchID;
            }
            catch
            {
                return null;
            }
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
