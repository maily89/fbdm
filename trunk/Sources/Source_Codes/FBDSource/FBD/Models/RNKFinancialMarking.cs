using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;

namespace FBD.Models
{

    public class RNKFinancialMarking
    {
        public static decimal CalculateFinancialScore(int rankingID,bool keepExistingLevel, FBDEntities entities)
        {
            //Step1: Load all financial score saved.
            CustomersBusinessRanking ranking=CustomersBusinessRanking.SelectBusinessRankingByID(rankingID,entities);
            ranking.BusinessIndustriesReference.Load();
            ranking.BusinessScalesReference.Load();

            ranking.CustomersBusinessFinancialIndex.Load();
            decimal finalScore = 0;
            //Step2: calculate LevelID for each financial score.
            foreach (CustomersBusinessFinancialIndex indexScore in ranking.CustomersBusinessFinancialIndex)
            {
                //get level
                if (indexScore.BusinessFinancialIndexLevelsReference.EntityKey != null && keepExistingLevel)
                {
                    indexScore.BusinessFinancialIndexLevelsReference.Load();
                    indexScore.BusinessFinancialIndexReference.Load();
                }
                else
                    GetLevel(indexScore, ranking, entities);

                //calculate score
                if (indexScore.BusinessFinancialIndexLevels != null)
                {
                    var proportion=BusinessFinancialIndexProportion.SelectFinancialIndexProportionByIndustryAndIndex
                        (entities, ranking.BusinessIndustries.IndustryID, indexScore.BusinessFinancialIndex.IndexID);

                    Nullable<decimal> score=indexScore.BusinessFinancialIndexLevels.Score;
                    if (score != null && proportion != null && proportion.Proportion!=null)
                    {
                        decimal prop = proportion.Proportion.Value;
                        finalScore += score.Value * prop;
                    }

                }
                
            }
            ranking.FinancialScore = finalScore/100;
            entities.SaveChanges();
            return finalScore/100;


        }

        public static BusinessFinancialIndexLevels GetLevel(CustomersBusinessFinancialIndex indexScore,CustomersBusinessRanking ranking,FBDEntities entities)
        {
            indexScore.BusinessFinancialIndexReference.Load();
            var index=indexScore.BusinessFinancialIndex;


            List<BusinessFinancialIndexScore> scoreList = BusinessFinancialIndexScore
                .SelectScoreByIndustryByScaleByFinancialIndex(entities, ranking.BusinessIndustries.IndustryID, ranking.BusinessScales.ScaleID, index.IndexID);

            foreach (BusinessFinancialIndexScore item in scoreList)
            {
                if (index.ValueType == "N") //numeric type
                {
                    decimal score = System.Convert.ToDecimal(indexScore.Value);
                    if (score >= item.FromValue && score <= item.ToValue)
                    {
                        item.BusinessFinancialIndexLevelsReference.Load();
                        indexScore.BusinessFinancialIndexLevels = item.BusinessFinancialIndexLevels;
                        return indexScore.BusinessFinancialIndexLevels;
                    }
                }
                else // character type
                {
                    if (indexScore.Value.Equals(item.FixedValue))
                    {
                        item.BusinessFinancialIndexLevelsReference.Load();
                        indexScore.BusinessFinancialIndexLevels = item.BusinessFinancialIndexLevels;
                        return indexScore.BusinessFinancialIndexLevels;
                    }
                }
            }
            return null;
        }

    }
}