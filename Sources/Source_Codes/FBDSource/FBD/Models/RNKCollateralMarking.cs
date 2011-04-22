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

    public class RNKCollateralMarking
    {
        public static IndividualCollateralRanks GetRank(decimal collateralScore)
        {
            List<IndividualCollateralRanks> rankList = IndividualCollateralRanks.SelectRanks();
            foreach (IndividualCollateralRanks item in rankList)
            {
                if (collateralScore >= item.FromValue.Value && collateralScore <= item.ToValue)
                {
                    return item;
                }
            }
            return null;
        }
        public static decimal CalculateCollateralScore(int rankingID,bool keepExistingLevel, FBDEntities entities)
        {
            //Step1: Load all collateral score saved.
            CustomersIndividualRanking ranking=CustomersIndividualRanking.SelectIndividualRankingByID(rankingID,entities);
            ranking.CustomersIndividualCollateralIndex.Load();
           
            decimal finalScore = 0;
            //Step2: calculate LevelID for each collateral score.
            foreach (CustomersIndividualCollateralIndex indexScore in ranking.CustomersIndividualCollateralIndex)
            {
                //get level
                if (indexScore.IndividualCollateralIndexLevelsReference.EntityKey != null && keepExistingLevel)
                {
                    indexScore.IndividualCollateralIndexLevelsReference.Load();
                    indexScore.IndividualCollateralIndexReference.Load();
                }
                else
                    GetLevel(indexScore, ranking, entities);

                //calculate score
                if (indexScore.IndividualCollateralIndexLevels != null)
                {

                    Nullable<decimal> score=indexScore.IndividualCollateralIndexLevels.Score;
                    if (score != null )
                    {
                        finalScore += score.Value;
                    }

                }
                
            }
            ranking.CollateralIndexScore = finalScore;
            entities.SaveChanges();
            return finalScore;


        }

        public static IndividualCollateralIndexLevels GetLevel(CustomersIndividualCollateralIndex indexScore,CustomersIndividualRanking ranking,FBDEntities entities)
        {
            indexScore.IndividualCollateralIndexReference.Load();
            var index=indexScore.IndividualCollateralIndex;
            if (index == null)
            {
                indexScore.IndividualCollateralIndexLevels = null;
                return null;
            }

            List<IndividualCollateralIndexScore> scoreList = IndividualCollateralIndexScore.SelectScoreByCollateral(entities,index.IndexID);

            foreach (IndividualCollateralIndexScore item in scoreList)
            {
                if (index.ValueType == FBD.CommonUtilities.Constants.INDEX_NUMERIC) //numeric type
                {
                    decimal score;
                    try
                    {
                        score = System.Convert.ToDecimal(indexScore.Value);
                    }
                    catch
                    {
                        score = 0;
                    }
                    if (score >= item.FromValue && score <= item.ToValue)
                    {
                        item.IndividualCollateralIndexLevelsReference.Load();

                        indexScore.IndividualCollateralIndexLevels = item.IndividualCollateralIndexLevels;

                        return indexScore.IndividualCollateralIndexLevels;
                    }
                }
                else // character type
                {
                    if (indexScore.Value == null) break;
                    if (indexScore.Value.Equals(item.FixedValue))
                    {
                        item.IndividualCollateralIndexLevelsReference.Load();
                        indexScore.IndividualCollateralIndexLevels = item.IndividualCollateralIndexLevels;
                        return indexScore.IndividualCollateralIndexLevels;
                    }
                }
            }

            indexScore.IndividualCollateralIndexLevels = null;
            return null;
        }


        internal static decimal CalculateTempCollateral(int rankingID, List<RNKCollateralRow> rnkCollateralRow)
        {
            //Step1: Load all collateral score saved.
            FBDEntities entities = new FBDEntities();
            CustomersIndividualRanking ranking = CustomersIndividualRanking.SelectIndividualRankingByID(rankingID, entities);

            decimal finalScore = 0;
            //Step2: calculate LevelID for each collateral score.
            foreach (RNKCollateralRow indexScore in rnkCollateralRow)
            {

                    GetLevel(indexScore, ranking, entities);

                //calculate score
                    
                    finalScore += indexScore.CalculatedScore;

            }
            ranking.CollateralIndexScore = finalScore ;
            entities.SaveChanges();
            return finalScore ;
        }

        private static void GetLevel(RNKCollateralRow indexScore, CustomersIndividualRanking ranking, FBDEntities entities)
        {
            
            var index = indexScore.Index;

            if (index == null) return;

            if (index.ValueType == FBD.CommonUtilities.Constants.INDEX_CHARACTER)
            {
                GetScoreForCharacter(indexScore, entities);
                return;
            }
            else
            {
                GetScoreForNumeric(indexScore, entities, index);
            }
            
        }

        private static void GetScoreForCharacter(RNKCollateralRow indexScore, FBDEntities entities)
        {
            IndividualCollateralIndexScore score = IndividualCollateralIndexScore.SelectIndividualCollateralIndexScoreByScoreID(entities, indexScore.ScoreID);
            if (score == null) return;
            score.IndividualCollateralIndexLevelsReference.Load();
            if (score.IndividualCollateralIndexLevels == null) return;
            indexScore.CalculatedScore = score.IndividualCollateralIndexLevels.Score;
            indexScore.Value = score.FixedValue;
            return;
        }

        private static void GetScoreForNumeric(RNKCollateralRow indexScore, FBDEntities entities, IndividualCollateralIndex index)
        {
            List<IndividualCollateralIndexScore> scoreList = IndividualCollateralIndexScore.SelectScoreByCollateral(entities, index.IndexID);
            if (indexScore.Score == null) return;
            decimal score = indexScore.Score.Value;

            foreach (IndividualCollateralIndexScore item in scoreList)
            {

                
                if (score >= item.FromValue && score <= item.ToValue)
                {
                    item.IndividualCollateralIndexLevelsReference.Load();

                    if (item.IndividualCollateralIndexLevels != null)
                        indexScore.CalculatedScore = item.IndividualCollateralIndexLevels.Score;
                    else
                    {
                        indexScore.CalculatedScore = 0;
                    }
                    return;
                }

            }
        }
    }
}