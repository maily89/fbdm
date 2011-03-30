using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;

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
            ranking.CollateralIndexScore = finalScore/100;
            entities.SaveChanges();
            return finalScore/100;


        }

        public static IndividualCollateralIndexLevels GetLevel(CustomersIndividualCollateralIndex indexScore,CustomersIndividualRanking ranking,FBDEntities entities)
        {
            indexScore.IndividualCollateralIndexReference.Load();
            var index=indexScore.IndividualCollateralIndex;


            List<IndividualCollateralIndexScore> scoreList = IndividualCollateralIndexScore.SelectScoreByCollateral(entities,index.IndexID);

            foreach (IndividualCollateralIndexScore item in scoreList)
            {
                if (index.ValueType == "N") //numeric type
                {
                    decimal score = System.Convert.ToDecimal(indexScore.Value);
                    if (score >= item.FromValue && score <= item.ToValue)
                    {
                        item.IndividualCollateralIndexLevelsReference.Load();
                        indexScore.IndividualCollateralIndexLevels = item.IndividualCollateralIndexLevels;
                        return indexScore.IndividualCollateralIndexLevels;
                    }
                }
                else // character type
                {
                    if (indexScore.Value.Equals(item.FixedValue))
                    {
                        item.IndividualCollateralIndexLevelsReference.Load();
                        indexScore.IndividualCollateralIndexLevels = item.IndividualCollateralIndexLevels;
                        return indexScore.IndividualCollateralIndexLevels;
                    }
                }
            }
            return null;
        }

    }
}