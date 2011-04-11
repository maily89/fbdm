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
    [MetadataType(typeof(CustomersFinancialIndexMetaData))]
    public partial class CustomersBusinessFinancialIndex
    {
        /// <summary>
        /// list of businessFinancialIndex
        /// </summary>
        /// <returns>list of businessFinancialIndex</returns>
        public static List<CustomersBusinessFinancialIndex> SelectFinancialIndex()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersBusinessFinancialIndex.ToList();
        }



        /// <summary>
        /// return the financialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the financialIndex</param>
        /// <returns>financialIndex</returns>
        public static CustomersBusinessFinancialIndex SelectFinancialIndexByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var financialIndex = entities.CustomersBusinessFinancialIndex.First(i => i.ID == id);
            return financialIndex;
        }

        /// <summary>
        /// return the financialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the financialIndex</param>
        /// <returns>financialIndex</returns>
        public static List<CustomersBusinessFinancialIndex> SelectFinancialIndexByRankingID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var financialIndex = entities
                                .CustomersBusinessFinancialIndex
                                .Include(Constants.TABLE_CUSTOMERS_BUSINESS_RANKING)
                                .Include(Constants.TABLE_BUSINESS_FINANCIAL_INDEX)
                                .Include(Constants.TABLE_BUSINESS_FINANCIAL_INDEX_LEVELS)
                                .Where(i => i.CustomersBusinessRanking.ID == id).ToList();
            return financialIndex;
        }

        public static List<CustomersBusinessFinancialIndex> SelectFinancialIndexByRankingID(int id, FBDEntities entities)
        {
            var financialIndex = entities
                                .CustomersBusinessFinancialIndex
                                .Include(Constants.TABLE_CUSTOMERS_BUSINESS_RANKING)
                                .Include(Constants.TABLE_BUSINESS_FINANCIAL_INDEX)
                                .Include(Constants.TABLE_BUSINESS_FINANCIAL_INDEX_LEVELS)
                                .Where(i => i.CustomersBusinessRanking.ID == id).ToList();
            return financialIndex;
        }

        /// <summary>
        /// return the financialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the financialIndex</param>
        /// <returns>financialIndex</returns>
        public static CustomersBusinessFinancialIndex SelectFinancialIndexByRankingIDAndIndexID(int rankingID, string indexID)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                var financialIndex = entities
                                    .CustomersBusinessFinancialIndex
                                    .Include(Constants.TABLE_CUSTOMERS_BUSINESS_RANKING)
                                    .Include(Constants.TABLE_BUSINESS_FINANCIAL_INDEX)
                                    .First(i => (i.CustomersBusinessRanking.ID == rankingID && i.BusinessFinancialIndex.IndexID == indexID));
                return financialIndex;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// return the financialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the financialIndex</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>financialIndex</returns>
        public static CustomersBusinessFinancialIndex SelectFinancialIndexByID(int id, FBDEntities entities)
        {
            if (entities == null || id<=0) return null;
            var financialIndex = entities.CustomersBusinessFinancialIndex.First(i => i.ID== id);
            return financialIndex;
        }



        /// <summary>
        /// edit the financialIndex
        /// </summary>
        /// <param name="financialIndex">update the financialIndex</param>
        public static int EditFinancialIndex(CustomersBusinessFinancialIndex financialIndex, FBDEntities entities)
        {
            if (financialIndex == null || entities==null) return 0;

            
            DatabaseHelper.AttachToOrGet<CustomersBusinessFinancialIndex>(entities,financialIndex.GetType().Name,ref financialIndex);

            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(financialIndex);
            stateEntry.SetModified();

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }



        /// <summary>
        /// add new financialIndex
        /// </summary>
        /// <param name="financialIndex">the financialIndex to add</param>
        public static int AddFinancialIndex(CustomersBusinessFinancialIndex financialIndex, FBDEntities entities)
        {
            if (financialIndex == null) return 0;
            //if (financialIndex. == null || financialIndex.CriteriaID == null) return 0;

            entities.AddToCustomersBusinessFinancialIndex(financialIndex);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        #region RNKFinancialRow Handle
        public static List<RNKFinancialRow> LoadFinancialIndex(int id, bool isAdd)
        {
            FBDEntities entities = new FBDEntities();
            CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);
            ranking.BusinessIndustriesReference.Load();
            ranking.BusinessScalesReference.Load();
            if (ranking.BusinessScales == null)
                RNKScaleMarking.SaveScaleScore(id, entities);
            ranking.BusinessScalesReference.Load();
            if (ranking.BusinessScales == null) return null;
            string scale = ranking.BusinessScales.ScaleID;

            string IndustryID = ranking.BusinessIndustries.IndustryID;

            List<BusinessFinancialIndex> indexList = BusinessFinancialIndex.SelectFinancialIndex(entities);

            List<RNKFinancialRow> financial = new List<RNKFinancialRow>();
            RNKFinancialRow temp = null;
            foreach (BusinessFinancialIndex item in indexList)
            {
                if (isAdd)
                    temp = GetNewFinancialRow(id, entities, scale, IndustryID, item);
                else
                    temp = LoadExistingFinancialRow(id, entities, scale, IndustryID, item);
                financial.Add(temp);
            }
            return financial;
        }

        public static RNKFinancialRow GetNewFinancialRow(int rankingID, FBDEntities entities, string scale, string IndustryID, BusinessFinancialIndex item)
        {
            var temp = new RNKFinancialRow();
            temp.RankingID = rankingID;
            temp.Index = item;
            temp.LeafIndex = item.LeafIndex;
            if (!item.LeafIndex) return temp;
            item.BusinessFinancialIndexScore.Load();
            if(temp.Index.ValueType=="C")
            temp.ScoreList = BusinessFinancialIndexScore.SelectScoreByIndustryByScaleByFinancialIndex(entities, IndustryID, scale, item.IndexID);
            return temp;
        }

        public static RNKFinancialRow LoadExistingFinancialRow(int rankingID, FBDEntities entities, string scale, string IndustryID, BusinessFinancialIndex item)
        {
            if (item.LeafIndex)
            {
                var customerFinancial = CustomersBusinessFinancialIndex.SelectFinancialIndexByRankingIDAndIndexID(rankingID, item.IndexID);

                if (customerFinancial == null)
                {
                    customerFinancial = new CustomersBusinessFinancialIndex();

                    customerFinancial.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID, entities);
                    customerFinancial.BusinessFinancialIndex = item;

                    CustomersBusinessFinancialIndex.AddFinancialIndex(customerFinancial, entities);
                }
                var temp = new RNKFinancialRow();

                temp.CustomerFinancialID = customerFinancial.ID;
                temp.RankingID = rankingID;
                temp.Index = item;
                temp.LeafIndex = true;
                item.BusinessFinancialIndexScore.Load();
                if (temp.Index.ValueType == "C")
                {
                    temp.ScoreList = BusinessFinancialIndexScore.SelectScoreByIndustryByScaleByFinancialIndex(entities, IndustryID, scale, item.IndexID);
                    foreach(BusinessFinancialIndexScore score in temp.ScoreList)
                    {
                        if (score.FixedValue.Equals(customerFinancial.Value))
                        {
                            temp.ScoreID = score.ScoreID;
                            break;
                        }
                    }
                }
                else
                    try
                    {
                        temp.Score = System.Convert.ToDecimal(customerFinancial.Value);
                    }
                    catch
                    {
                        temp.Score = 0;
                    }
                return temp;
            }
            else
            {
                var temp = new RNKFinancialRow();
                temp.LeafIndex = false;
                temp.RankingID = rankingID;
                temp.Index = item;
                return temp;
            }
        }
        public static CustomersBusinessFinancialIndex ConvertFinancialRowToModel(FBDEntities entities, RNKFinancialRow item, CustomersBusinessFinancialIndex indexScore)
        {
            int rankID = item.RankingID;


            indexScore.BusinessFinancialIndex = BusinessFinancialIndex.SelectFinancialIndexByID(entities, item.Index.IndexID);

            indexScore.Value = item.Score.ToString();
            indexScore.CustomersBusinessRanking = CustomersBusinessRanking.SelectBusinessRankingByID(rankID, entities);

            if (item.ScoreID > 0)
            {
                BusinessFinancialIndexScore score = BusinessFinancialIndexScore.SelectBusinessFinancialIndexScoreByScoreID(entities, item.ScoreID);
                score.BusinessFinancialIndexLevelsReference.Load();
                indexScore.Value = score.FixedValue;
                indexScore.BusinessFinancialIndexLevels = score.BusinessFinancialIndexLevels;
            }
            return indexScore;
        }
        #endregion
        public class CustomersFinancialIndexMetaData
        {
        		
        	[DisplayName("ID")]
        	[Required]
            public int ID { get; set; }
        		
        	[DisplayName("Value")]
        	[StringLength(255)]
            public string Value { get; set; }
        }

        internal static void Reload(List<RNKFinancialRow> rnkFinancialRow)
        {
            if(rnkFinancialRow.Count<=0) return;
            FBDEntities entities = new FBDEntities();
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rnkFinancialRow[0].RankingID);
            ranking.BusinessScalesReference.Load();
            ranking.BusinessIndustriesReference.Load();
            string industryID = ranking.BusinessIndustries.IndustryID;
            string scale = ranking.BusinessScales.ScaleID;
            foreach (RNKFinancialRow item in rnkFinancialRow)
            {
                item.Index = BusinessFinancialIndex.SelectFinancialIndexByID(entities,item.Index.IndexID);
                item.ScoreList = BusinessFinancialIndexScore.SelectScoreByIndustryByScaleByFinancialIndex(entities, industryID, scale, item.Index.IndexID);
            }
        }
    }
}
