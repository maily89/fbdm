using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using FBD.ViewModels;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType(typeof(CustomersNonFinancialIndexMetaData))]
    public partial class CustomersBusinessNonFinancialIndex
    {
        /// <summary>
        /// list of businessNonFinancialIndex
        /// </summary>
        /// <returns>list of businessNonFinancialIndex</returns>
        public static List<CustomersBusinessNonFinancialIndex> SelectNonFinancialIndex()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersBusinessNonFinancialIndex.ToList();
        }



        /// <summary>
        /// return the nonFinancialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the nonFinancialIndex</param>
        /// <returns>nonFinancialIndex</returns>
        public static CustomersBusinessNonFinancialIndex SelectNonFinancialIndexByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var nonFinancialIndex = entities.CustomersBusinessNonFinancialIndex.First(i => i.ID == id);
            return nonFinancialIndex;
        }

        /// <summary>
        /// return the nonFinancialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the nonFinancialIndex</param>
        /// <returns>nonFinancialIndex</returns>
        public static List<CustomersBusinessNonFinancialIndex> SelectNonFinancialIndexByRankingID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var nonFinancialIndex = entities
                                .CustomersBusinessNonFinancialIndex
                                .Include(Constants.TABLE_CUSTOMERS_BUSINESS_RANKING)
                                .Include(Constants.TABLE_BUSINESS_NONFINANCIAL_INDEX)
                                .Include(Constants.TABLE_BUSINESS_NONFINANCIAL_INDEX_LEVELS)
                                .Where(i => i.CustomersBusinessRanking.ID == id).ToList();
            return nonFinancialIndex;
        }

        public static List<CustomersBusinessNonFinancialIndex> SelectNonFinancialIndexByRankingID(int id, FBDEntities entities)
        {
            var nonFinancialIndex = entities
                                .CustomersBusinessNonFinancialIndex
                                .Include(Constants.TABLE_CUSTOMERS_BUSINESS_RANKING)
                                .Include(Constants.TABLE_BUSINESS_NONFINANCIAL_INDEX)
                                .Include(Constants.TABLE_BUSINESS_NONFINANCIAL_INDEX_LEVELS)
                                .Where(i => i.CustomersBusinessRanking.ID == id).ToList();
            return nonFinancialIndex;
        }

        /// <summary>
        /// return the nonFinancialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the nonFinancialIndex</param>
        /// <returns>nonFinancialIndex</returns>
        public static CustomersBusinessNonFinancialIndex SelectNonFinancialIndexByRankingIDAndIndexID(int rankingID, string indexID)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                var nonFinancialIndex = entities
                                    .CustomersBusinessNonFinancialIndex
                                    .Include(Constants.TABLE_CUSTOMERS_BUSINESS_RANKING)
                                    .Include(Constants.TABLE_BUSINESS_NONFINANCIAL_INDEX)
                                    .First(i => (i.CustomersBusinessRanking.ID == rankingID && i.BusinessNonFinancialIndex.IndexID == indexID));
                return nonFinancialIndex;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// return the nonFinancialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the nonFinancialIndex</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>nonFinancialIndex</returns>
        public static CustomersBusinessNonFinancialIndex SelectNonFinancialIndexByID(int id, FBDEntities entities)
        {
            if (entities == null || id <= 0) return null;
            var nonFinancialIndex = entities.CustomersBusinessNonFinancialIndex.First(i => i.ID == id);
            return nonFinancialIndex;
        }



        /// <summary>
        /// edit the nonFinancialIndex
        /// </summary>
        /// <param name="nonFinancialIndex">update the nonFinancialIndex</param>
        public static int EditNonFinancialIndex(CustomersBusinessNonFinancialIndex nonFinancialIndex, FBDEntities entities)
        {
            if (nonFinancialIndex == null || entities == null) return 0;


            DatabaseHelper.AttachToOrGet<CustomersBusinessNonFinancialIndex>(entities, nonFinancialIndex.GetType().Name, ref nonFinancialIndex);

            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(nonFinancialIndex);
            stateEntry.SetModified();

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }



        /// <summary>
        /// add new nonFinancialIndex
        /// </summary>
        /// <param name="nonFinancialIndex">the nonFinancialIndex to add</param>
        public static int AddNonFinancialIndex(CustomersBusinessNonFinancialIndex nonFinancialIndex, FBDEntities entities)
        {
            if (nonFinancialIndex == null) return 0;
            //if (nonFinancialIndex. == null || nonFinancialIndex.CriteriaID == null) return 0;

            entities.AddToCustomersBusinessNonFinancialIndex(nonFinancialIndex);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        #region RNKNonFinancialRow Handle
        public static List<RNKNonFinancialRow> LoadNonFinancialIndex(int id, bool isAdd)
        {
            FBDEntities entities = new FBDEntities();
            CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
            ranking.BusinessIndustriesReference.Load();
            

            string IndustryID = ranking.BusinessIndustries.IndustryID;

            List<BusinessNonFinancialIndex> indexList = BusinessNonFinancialIndex.SelectNonFinancialIndex(entities);

            List<RNKNonFinancialRow> nonFinancial = new List<RNKNonFinancialRow>();
            RNKNonFinancialRow temp = null;
            foreach (BusinessNonFinancialIndex item in indexList)
            {
                if (isAdd)
                    temp = GetNewNonFinancialRow(id, entities, IndustryID, item);
                else
                    temp = LoadExistingNonFinancialRow(id, entities, IndustryID, item);
                nonFinancial.Add(temp);
            }
            return nonFinancial;
        }

        public static RNKNonFinancialRow GetNewNonFinancialRow(int rankingID, FBDEntities entities,string IndustryID, BusinessNonFinancialIndex item)
        {
            var temp = new RNKNonFinancialRow();
            temp.RankingID = rankingID;
            temp.Index = item;
            temp.LeafIndex = item.LeafIndex;
            if (!item.LeafIndex) return temp;
            item.BusinessNonFinancialIndexScore.Load();
            if (temp.Index.ValueType == "C")
                temp.ScoreList = BusinessNonFinancialIndexScore.SelectScoreByIndustryByNonFinancialIndex(entities, IndustryID,  item.IndexID);
            return temp;
        }

        public static RNKNonFinancialRow LoadExistingNonFinancialRow(int rankingID, FBDEntities entities,  string IndustryID, BusinessNonFinancialIndex item)
        {
            if (item.LeafIndex)
            {
                var customerNonFinancial = CustomersBusinessNonFinancialIndex.SelectNonFinancialIndexByRankingIDAndIndexID(rankingID, item.IndexID);

                if (customerNonFinancial == null)
                {
                    customerNonFinancial = new CustomersBusinessNonFinancialIndex();

                    customerNonFinancial.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID, entities);
                    customerNonFinancial.BusinessNonFinancialIndex = item;

                    CustomersBusinessNonFinancialIndex.AddNonFinancialIndex(customerNonFinancial, entities);
                }
                var temp = new RNKNonFinancialRow();

                temp.CustomerScoreID = customerNonFinancial.ID;
                temp.RankingID = rankingID;
                temp.Index = item;
                temp.LeafIndex = true;
                item.BusinessNonFinancialIndexScore.Load();
                if (temp.Index.ValueType == "C")
                {
                    temp.ScoreList = BusinessNonFinancialIndexScore.SelectScoreByIndustryByNonFinancialIndex(entities, IndustryID,  item.IndexID);
                    foreach (BusinessNonFinancialIndexScore score in temp.ScoreList)
                    {
                        if (score.FixedValue.Equals(customerNonFinancial.Value))
                        {
                            temp.ScoreID = score.ScoreID;
                            break;
                        }
                    }
                }
                else
                    try
                    {
                        temp.Score = System.Convert.ToDecimal(customerNonFinancial.Value);
                    }
                    catch
                    {
                        temp.Score = 0;
                    }
                return temp;
            }
            else
            {
                var temp = new RNKNonFinancialRow();
                temp.LeafIndex = false;
                temp.RankingID = rankingID;
                temp.Index = item;
                return temp;
            }
        }
        public static CustomersBusinessNonFinancialIndex ConvertNonFinancialRowToModel(FBDEntities entities, RNKNonFinancialRow item, CustomersBusinessNonFinancialIndex indexScore)
        {
            int rankID = item.RankingID;


            indexScore.BusinessNonFinancialIndex = BusinessNonFinancialIndex.SelectNonFinancialIndexByID(entities, item.Index.IndexID);

            indexScore.Value = item.Score.ToString();
            indexScore.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(rankID, entities);

            if (item.ScoreID > 0)
            {
                BusinessNonFinancialIndexScore score = BusinessNonFinancialIndexScore.SelectBusinessNonFinancialIndexScoreByScoreID(entities, item.ScoreID);
                score.BusinessNonFinancialIndexLevelsReference.Load();
                indexScore.Value = score.FixedValue;
                indexScore.BusinessNonFinancialIndexLevels = score.BusinessNonFinancialIndexLevels;
            }
            return indexScore;
        }
        #endregion
        public class CustomersNonFinancialIndexMetaData
        {

            [DisplayName("ID")]
            [Required]
            public int ID { get; set; }

            [DisplayName("Value")]
            [StringLength(255)]
            public string Value { get; set; }
        }

        internal static List<RNKNonFinancialRow> Reload(List<RNKNonFinancialRow> rnkNonFinancialRow)
        {
            if (rnkNonFinancialRow.Count <= 0) return rnkNonFinancialRow;
            FBDEntities entities = new FBDEntities();
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rnkNonFinancialRow[0].RankingID);
            
            ranking.BusinessIndustriesReference.Load();
            string industryID = ranking.BusinessIndustries.IndustryID;
            
            foreach (RNKNonFinancialRow item in rnkNonFinancialRow)
            {
                item.Index = BusinessNonFinancialIndex.SelectNonFinancialIndexByID(entities, item.Index.IndexID);
                item.ScoreList = BusinessNonFinancialIndexScore.SelectScoreByIndustryByNonFinancialIndex(entities, industryID, item.Index.IndexID);
            }
            return rnkNonFinancialRow;
        }
    }
}
