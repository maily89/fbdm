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
    [MetadataType(typeof(CustomersIndividualCollateralIndexMetaData))]
    public partial class CustomersIndividualCollateralIndex 
    {
        /// <summary>
        /// list of individualCollateralIndex
        /// </summary>
        /// <returns>list of individualCollateralIndex</returns>
        public static List<CustomersIndividualCollateralIndex> SelectCollateralIndex()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersIndividualCollateralIndex.ToList();
        }



        /// <summary>
        /// return the collateralIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the collateralIndex</param>
        /// <returns>collateralIndex</returns>
        public static CustomersIndividualCollateralIndex SelectCollateralIndexByID(int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                var collateralIndex = entities.CustomersIndividualCollateralIndex.First(i => i.ID == id);
                return collateralIndex;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// return the collateralIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the collateralIndex</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>collateralIndex</returns>
        public static CustomersIndividualCollateralIndex SelectCollateralIndexByID(int id, FBDEntities entities)
        {
            try
            {
                if (entities == null || id <= 0) return null;
                var collateralIndex = entities.CustomersIndividualCollateralIndex.First(i => i.ID == id);
                return collateralIndex;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// return the collateralIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the collateralIndex</param>
        /// <returns>collateralIndex</returns>
        public static List<CustomersIndividualCollateralIndex> SelectCollateralIndexByRankingID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var collateralIndex = entities
                                .CustomersIndividualCollateralIndex
                                .Where(i => i.CustomersIndividualRanking.ID == id).ToList();
            return collateralIndex;
        }

        public static List<CustomersIndividualCollateralIndex> SelectCollateralIndexByRankingIDWithReference(int id, FBDEntities entities)
        {
            var collateralIndex = entities
                                .CustomersIndividualCollateralIndex
                                .Include(Constants.TABLE_CUSTOMERS_INDIVIDUAL_RANKING)
                                .Include(Constants.TABLE_INDIVIDUAL_COLLATERAL_INDEX)
                                .Include(Constants.TABLE_INDIVIDUAL_COLLATERAL_INDEX_LEVELS)
                                .Where(i => i.CustomersIndividualRanking.ID == id).ToList();
            return collateralIndex;
        }

        /// <summary>
        /// return the collateralIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the collateralIndex</param>
        /// <returns>collateralIndex</returns>
        public static CustomersIndividualCollateralIndex SelectCollateralIndexByRankingIDAndIndexID(int rankingID, string indexID)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                var collateralIndex = entities
                                    .CustomersIndividualCollateralIndex
                                    .Include("CustomersIndividualRanking")
                                    .Include("IndividualCollateralIndex")
                                    .First(i => (i.CustomersIndividualRanking.ID == rankingID && i.IndividualCollateralIndex.IndexID == indexID));
                return collateralIndex;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// edit the collateralIndex
        /// </summary>
        /// <param name="collateralIndex">update the collateralIndex</param>
        public static int EditCollateralIndex(CustomersIndividualCollateralIndex collateralIndex, FBDEntities entities)
        {
            if (collateralIndex == null || entities == null) return 0;


            DatabaseHelper.AttachToOrGet<CustomersIndividualCollateralIndex>(entities, collateralIndex.GetType().Name, ref collateralIndex);

            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(collateralIndex);
            stateEntry.SetModified();

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }



        /// <summary>
        /// add new collateralIndex
        /// </summary>
        /// <param name="collateralIndex">the collateralIndex to add</param>
        public static int AddCollateralIndex(CustomersIndividualCollateralIndex collateralIndex, FBDEntities entities)
        {
            if (collateralIndex == null) return 0;
            //if (collateralIndex. == null || collateralIndex.CriteriaID == null) return 0;

            entities.AddToCustomersIndividualCollateralIndex(collateralIndex);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        #region RNKCollateralRow Handle
        public static List<RNKCollateralRow> LoadCollateralIndex(int id, bool isAdd)
        {
            FBDEntities entities = new FBDEntities();
            CustomersIndividualRanking ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);

            List<IndividualCollateralIndex> indexList = IndividualCollateralIndex.SelectCollateralIndex(entities);

            List<RNKCollateralRow> collateral = new List<RNKCollateralRow>();
            RNKCollateralRow temp = null;
            foreach (IndividualCollateralIndex item in indexList)
            {
                if (isAdd)
                    temp = GetNewCollateralRow(id, entities, item);
                else
                    temp = LoadExistingCollateralRow(id, entities, item);
                collateral.Add(temp);
            }
            return collateral;
        }

        public static RNKCollateralRow GetNewCollateralRow(int rankingID, FBDEntities entities, IndividualCollateralIndex item)
        {
            var temp = new RNKCollateralRow();
            temp.RankingID = rankingID;
            temp.Index = item;
            temp.LeafIndex = item.LeafIndex;
            if (!item.LeafIndex) return temp;
            item.IndividualCollateralIndexScore.Load();
            if (temp.Index.ValueType == FBD.CommonUtilities.Constants.INDEX_CHARACTER)
                temp.ScoreList = IndividualCollateralIndexScore.SelectScoreByCollateral(entities, item.IndexID);
            return temp;
        }

        public static RNKCollateralRow LoadExistingCollateralRow(int rankingID, FBDEntities entities,  IndividualCollateralIndex item)
        {
            if (item.LeafIndex)
            {
                var customerCollateral = CustomersIndividualCollateralIndex.SelectCollateralIndexByRankingIDAndIndexID(rankingID, item.IndexID);

                if (customerCollateral == null)
                {
                    customerCollateral = new CustomersIndividualCollateralIndex();

                    customerCollateral.CustomersIndividualRanking = CustomersIndividualRanking.SelectIndividualRankingByID(rankingID, entities);
                    customerCollateral.IndividualCollateralIndex = item;

                    CustomersIndividualCollateralIndex.AddCollateralIndex(customerCollateral, entities);
                }
                var temp = new RNKCollateralRow();

                temp.CustomerScoreID = customerCollateral.ID;
                temp.RankingID = rankingID;
                temp.Index = item;
                temp.LeafIndex = true;
                item.IndividualCollateralIndexScore.Load();
                if (temp.Index.ValueType == FBD.CommonUtilities.Constants.INDEX_CHARACTER)
                {
                    temp.ScoreList = IndividualCollateralIndexScore.SelectScoreByCollateral(entities, item.IndexID);
                    foreach (IndividualCollateralIndexScore score in temp.ScoreList)
                    {
                        if (score.FixedValue.Equals(customerCollateral.Value))
                        {
                            temp.ScoreID = score.ScoreID;
                            break;
                        }
                    }
                    temp.Value = customerCollateral.Value;
                }
                else
                    try
                    {
                        temp.Score = System.Convert.ToDecimal(customerCollateral.Value);
                    }
                    catch
                    {
                        temp.Score = 0;
                    }
                return temp;
            }
            else
            {
                var temp = new RNKCollateralRow();
                temp.LeafIndex = false;
                temp.RankingID = rankingID;
                temp.Index = item;
                return temp;
            }
        }
        public static CustomersIndividualCollateralIndex ConvertCollateralRowToModel(FBDEntities entities, RNKCollateralRow item, CustomersIndividualCollateralIndex indexScore)
        {
            int rankID = item.RankingID;


            indexScore.IndividualCollateralIndex = IndividualCollateralIndex.SelectCollateralIndexByID(item.Index.IndexID, entities);

            if (indexScore.IndividualCollateralIndex.ValueType == Constants.INDEX_NUMERIC)
            {
                if (item.Score != null)
                {
                    indexScore.Value = item.Score.ToString();
                }
                else
                {
                    indexScore.Value = "0";
                }
            }
            indexScore.CustomersIndividualRanking = CustomersIndividualRanking.SelectIndividualRankingByID(rankID, entities);

            if (item.ScoreID > 0)
            {
                IndividualCollateralIndexScore score = IndividualCollateralIndexScore.SelectIndividualCollateralIndexScoreByScoreID(entities, item.ScoreID);
                score.IndividualCollateralIndexLevelsReference.Load();
                indexScore.Value = score.FixedValue;
                indexScore.IndividualCollateralIndexLevels = score.IndividualCollateralIndexLevels;
            }
            return indexScore;
        }
        #endregion
        internal static List<RNKCollateralRow> Reload(List<RNKCollateralRow> rnkCollateralRow)
        {
            if (rnkCollateralRow.Count <= 0) return rnkCollateralRow;
            FBDEntities entities = new FBDEntities();
            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(rnkCollateralRow[0].RankingID);


            foreach (RNKCollateralRow item in rnkCollateralRow)
            {
                item.Index = IndividualCollateralIndex.SelectCollateralIndexByID(item.Index.IndexID, entities);
                item.ScoreList = IndividualCollateralIndexScore.SelectScoreByCollateral(entities, item.Index.IndexID);
            }

            return rnkCollateralRow;
        }
        public class CustomersIndividualCollateralIndexMetaData
        {
        		
        	[DisplayName("ID")]
        	[Required]
            public int ID { get; set; }
        		
        	[DisplayName("Value")]
        	[StringLength(255)]
            public string Value { get; set; }
        }
    }
}
