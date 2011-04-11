using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using FBD.ViewModels;

namespace FBD.Models
{
    [MetadataType(typeof(CustomersIndividualBasicIndexMetaData))]
    public partial class CustomersIndividualBasicIndex
    {
        /// <summary>
        /// list of individualBasicIndex
        /// </summary>
        /// <returns>list of individualBasicIndex</returns>
        public static List<CustomersIndividualBasicIndex> SelectBasicIndex()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersIndividualBasicIndex.ToList();
        }



        /// <summary>
        /// return the basicIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the basicIndex</param>
        /// <returns>basicIndex</returns>
        public static CustomersIndividualBasicIndex SelectBasicIndexByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var basicIndex = entities.CustomersIndividualBasicIndex.First(i => i.ID == id);
            return basicIndex;
        }

        /// <summary>
        /// return the basicIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the basicIndex</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>basicIndex</returns>
        public static CustomersIndividualBasicIndex SelectBasicIndexByID(int id, FBDEntities entities)
        {
            if (entities == null || id <= 0) return null;
            var basicIndex = entities.CustomersIndividualBasicIndex.First(i => i.ID == id);
            return basicIndex;
        }

        /// <summary>
        /// return the basicIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the basicIndex</param>
        /// <returns>basicIndex</returns>
        public static List<CustomersIndividualBasicIndex> SelectBasicIndexByRankingID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var basicIndex = entities
                                .CustomersIndividualBasicIndex
                                .Include("CustomersIndividualRanking")
                                .Where(i => i.CustomersIndividualRanking.ID == id).ToList();
            return basicIndex;
        }

        /// <summary>
        /// return the basicIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the basicIndex</param>
        /// <returns>basicIndex</returns>
        public static CustomersIndividualBasicIndex SelectBasicIndexByRankingIDAndIndexID(int rankingID, string indexID)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                var basicIndex = entities
                                    .CustomersIndividualBasicIndex
                                    .Include("CustomersIndividualRanking")
                                    .Include("IndividualBasicIndex")
                                    .First(i => (i.CustomersIndividualRanking.ID == rankingID && i.IndividualBasicIndex.IndexID == indexID));
                return basicIndex;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// edit the basicIndex
        /// </summary>
        /// <param name="basicIndex">update the basicIndex</param>
        public static int EditBasicIndex(CustomersIndividualBasicIndex basicIndex, FBDEntities entities)
        {
            if (basicIndex == null || entities == null) return 0;


            DatabaseHelper.AttachToOrGet<CustomersIndividualBasicIndex>(entities, basicIndex.GetType().Name, ref basicIndex);

            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(basicIndex);
            stateEntry.SetModified();

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }



        /// <summary>
        /// add new basicIndex
        /// </summary>
        /// <param name="basicIndex">the basicIndex to add</param>
        public static int AddBasicIndex(CustomersIndividualBasicIndex basicIndex, FBDEntities entities)
        {
            if (basicIndex == null) return 0;
            //if (basicIndex. == null || basicIndex.CriteriaID == null) return 0;

            entities.AddToCustomersIndividualBasicIndex(basicIndex);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        #region RNKBasicRow Handle
        public static List<RNKBasicRow> LoadBasicIndex(int id, bool isAdd)
        {
            FBDEntities entities = new FBDEntities();
            CustomersIndividualRanking ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id);
            ranking.IndividualBorrowingPurposesReference.Load();
            if (ranking.IndividualBorrowingPurposes == null)
            {
                return null;
            }
            string purposeID = ranking.IndividualBorrowingPurposes.PurposeID;
            List<IndividualBasicIndex> indexList = IndividualBasicIndex.SelectBasicIndex(entities);

            List<RNKBasicRow> basic = new List<RNKBasicRow>();
            RNKBasicRow temp = null;
            foreach (IndividualBasicIndex item in indexList)
            {
                if (isAdd)
                    temp = GetNewBasicRow(id, entities,purposeID, item);
                else
                    temp = LoadExistingBasicRow(id, entities,purposeID,item);
                basic.Add(temp);
            }
            return basic;
        }

        public static RNKBasicRow GetNewBasicRow(int rankingID, FBDEntities entities,string purposeID, IndividualBasicIndex item)
        {
            var temp = new RNKBasicRow();
            temp.RankingID = rankingID;
            temp.Index = item;
            temp.LeafIndex = item.LeafIndex;
            if (!item.LeafIndex) return temp;
            item.IndividualBasicIndexScore.Load();
            if (temp.Index.ValueType == "C")
                temp.ScoreList = IndividualBasicIndexScore.SelectScoreByBasicAndPurposeIndex(entities, item.IndexID,purposeID);
            return temp;
        }

        public static RNKBasicRow LoadExistingBasicRow(int rankingID, FBDEntities entities, string purposeID, IndividualBasicIndex item)
        {
            if (item.LeafIndex)
            {
                var customerBasic = CustomersIndividualBasicIndex.SelectBasicIndexByRankingIDAndIndexID(rankingID, item.IndexID);

                if (customerBasic == null)
                {
                    customerBasic = new CustomersIndividualBasicIndex();

                    customerBasic.CustomersIndividualRanking = CustomersIndividualRanking.SelectIndividualRankingByID(rankingID, entities);
                    customerBasic.IndividualBasicIndex = item;

                    CustomersIndividualBasicIndex.AddBasicIndex(customerBasic, entities);
                }
                var temp = new RNKBasicRow();

                temp.CustomerScoreID = customerBasic.ID;
                temp.RankingID = rankingID;
                temp.Index = item;
                temp.LeafIndex = true;
                item.IndividualBasicIndexScore.Load();
                if (temp.Index.ValueType == "C")
                {
                    temp.ScoreList = IndividualBasicIndexScore.SelectScoreByBasicAndPurposeIndex(entities, item.IndexID, purposeID);
                    foreach (IndividualBasicIndexScore score in temp.ScoreList)
                    {
                        if (score.FixedValue.Equals(customerBasic.Value))
                        {
                            temp.ScoreID = score.ScoreID;
                            break;
                        }
                    }
                }
                else
                    try
                    {
                        temp.Score = System.Convert.ToDecimal(customerBasic.Value);
                    }
                    catch
                    {
                        temp.Score = 0;
                    }
                return temp;
            }
            else
            {
                var temp = new RNKBasicRow();
                temp.LeafIndex = false;
                temp.RankingID = rankingID;
                temp.Index = item;
                return temp;
            }
        }
        public static CustomersIndividualBasicIndex ConvertBasicRowToModel(FBDEntities entities, RNKBasicRow item, CustomersIndividualBasicIndex indexScore)
        {
            int rankID = item.RankingID;


            indexScore.IndividualBasicIndex = IndividualBasicIndex.SelectBasicIndexByID(item.Index.IndexID,entities);

            indexScore.Value = item.Score.ToString();
            indexScore.CustomersIndividualRanking = CustomersIndividualRanking.SelectIndividualRankingByID(rankID, entities);

            if (item.ScoreID > 0)
            {
                IndividualBasicIndexScore score = IndividualBasicIndexScore.SelectIndividualBasicIndexScoreByScoreID(entities, item.ScoreID);
                score.IndividualBasicIndexLevelsReference.Load();
                indexScore.Value = score.FixedValue;
                indexScore.IndividualBasicIndexLevels = score.IndividualBasicIndexLevels;
            }
            return indexScore;
        }
        #endregion
        internal static List<RNKBasicRow> Reload(List<RNKBasicRow> rnkBasicRow)
        {
            if (rnkBasicRow.Count <= 0) return rnkBasicRow;
            FBDEntities entities = new FBDEntities();
            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(rnkBasicRow[0].RankingID);

            ranking.IndividualBorrowingPurposesReference.Load();
            if (ranking.IndividualBorrowingPurposes == null) return rnkBasicRow;
            string purpose = ranking.IndividualBorrowingPurposes.PurposeID;

            foreach (RNKBasicRow item in rnkBasicRow)
            {
                item.Index = IndividualBasicIndex.SelectBasicIndexByID(item.Index.IndexID,entities);
                item.ScoreList = IndividualBasicIndexScore.SelectScoreByBasicAndPurposeIndex(entities, item.Index.IndexID,purpose);
            }

            return rnkBasicRow;
        }
        public class CustomersIndividualBasicIndexMetaData
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
