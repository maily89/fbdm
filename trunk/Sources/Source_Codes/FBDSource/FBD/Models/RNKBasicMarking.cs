using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;

namespace FBD.Models
{

    public class RNKBasicMarking
    {
        public static IndividualBasicRanks GetRank(decimal basicScore)
        {
            List<IndividualBasicRanks> rankList = IndividualBasicRanks.SelectRanks();
            foreach (IndividualBasicRanks item in rankList)
            {
                if (basicScore >= item.FromValue.Value && basicScore <= item.ToValue)
                {
                    return item;
                }
            }
            return null;
        }
        public static decimal CalculateBasicScore(int rankingID,bool keepExistingLevel, FBDEntities entities)
        {
            //Step1: Load all basic score saved.
            CustomersIndividualRanking ranking=CustomersIndividualRanking.SelectIndividualRankingByID(rankingID,entities);
            ranking.IndividualBorrowingPurposesReference.Load();
            
            if (ranking.IndividualBorrowingPurposes == null)
            {
                return 0;
            }

            string purposeID = ranking.IndividualBorrowingPurposes.PurposeID;

            ranking.CustomersIndividualBasicIndex.Load();
           
            decimal finalScore = 0;
            //Step2: calculate LevelID for each basic score.
            foreach (CustomersIndividualBasicIndex indexScore in ranking.CustomersIndividualBasicIndex)
            {
                //get level
                if (indexScore.IndividualBasicIndexLevelsReference.EntityKey != null && keepExistingLevel)
                {
                    indexScore.IndividualBasicIndexLevelsReference.Load();
                    indexScore.IndividualBasicIndexReference.Load();
                }
                else
                    GetLevel(indexScore, ranking, entities);

                //calculate score
                if (indexScore.IndividualBasicIndexLevels != null)
                {
                    var proportion = IndividualBasicIndexProportion.SelectBasicIndexProportionByBorrowingPPAndBasicIndex(entities,purposeID, indexScore.IndividualBasicIndex.IndexID);

                    Nullable<decimal> score=indexScore.IndividualBasicIndexLevels.Score;
                    if (score != null && proportion != null && proportion!=null)
                    {
                        decimal prop = proportion.Proportion.Value;
                        finalScore += score.Value * prop;
                    }

                }
                
            }
            ranking.BasicIndexScore = finalScore/100;
            entities.SaveChanges();
            return finalScore/100;


        }

        public static IndividualBasicIndexLevels GetLevel(CustomersIndividualBasicIndex indexScore,CustomersIndividualRanking ranking,FBDEntities entities)
        {
            indexScore.IndividualBasicIndexReference.Load();
            var index=indexScore.IndividualBasicIndex;


            List<IndividualBasicIndexScore> scoreList = IndividualBasicIndexScore.SelectScoreByBasicAndPurposeIndex(entities,index.IndexID,ranking.IndividualBorrowingPurposes.PurposeID);

            foreach (IndividualBasicIndexScore item in scoreList)
            {
                if (index.ValueType == "N") //numeric type
                {
                    decimal score = System.Convert.ToDecimal(indexScore.Value);
                    if (score >= item.FromValue && score <= item.ToValue)
                    {
                        item.IndividualBasicIndexLevelsReference.Load();
                        indexScore.IndividualBasicIndexLevels = item.IndividualBasicIndexLevels;
                        return indexScore.IndividualBasicIndexLevels;
                    }
                }
                else // character type
                {
                    if (indexScore.Value.Equals(item.FixedValue))
                    {
                        item.IndividualBasicIndexLevelsReference.Load();
                        indexScore.IndividualBasicIndexLevels = item.IndividualBasicIndexLevels;
                        return indexScore.IndividualBasicIndexLevels;
                    }
                }
            }
            return null;
        }

    }
}