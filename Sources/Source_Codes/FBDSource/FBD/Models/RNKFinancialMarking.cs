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

    public class RNKFinancialMarking
    {
        public static void CalculateTempFinancial(int rankingID, List<RNKFinancialRow> rnkFinancial,out decimal totalScore,out decimal totalProportion)
        {
            //Step1: Load all financial score saved.
            FBDEntities entities = new FBDEntities();
            CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID, entities);
            ranking.BusinessIndustriesReference.Load();
            ranking.BusinessScalesReference.Load();

            totalScore = 0;
            totalProportion = 0;

            if (ranking.BusinessScales == null || ranking.BusinessIndustries == null) return;

            decimal total = 0;

            foreach (RNKFinancialRow indexScore in rnkFinancial)
            {
                //get Score
                GetScore(indexScore, ranking);

                //calculate score

                var proportion = BusinessFinancialIndexProportion.SelectFinancialIndexProportionByIndustryAndIndex
                    (entities, ranking.BusinessIndustries.IndustryID, indexScore.Index.IndexID);

                decimal score = indexScore.CalculatedScore;
                if (proportion != null && proportion.Proportion != null)
                {
                    decimal prop = proportion.Proportion.Value;
                    total += score * prop;
                    indexScore.Proportion = prop;
                    indexScore.Result = score* prop/100;
                }

                

            }

            totalScore = total/100;
            totalProportion = GetFinancialProportion(ranking).Percentage.Value;

        }

        public static BusinessRankingStructure GetFinancialProportion(CustomersBusinessRanking ranking)
        {
            var temp = BusinessRankingStructure.SelectRankingStructureByIndexAndAudit(Constants.RNK_STRUCTURE_FINANCIAL_INDEX, ranking.AuditedStatus);
            return temp;
        }
        public static decimal CalculateFinancialScore(int rankingID,bool keepExistingLevel, FBDEntities entities)
        {
            //Step1: Load all financial score saved.
            CustomersBusinessRanking ranking=CustomersBusinessRanking.SelectBusinessRankingByID(rankingID,entities);
            ranking.BusinessIndustriesReference.Load();
            ranking.BusinessScalesReference.Load();

            ranking.CustomersBusinessFinancialIndex.Load();

            if (ranking.BusinessIndustries == null || ranking.BusinessScales == null || ranking.CustomersBusinessFinancialIndex == null)
            {
                return 0;
            }
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

            ranking.FinancialScore = finalScore / 100 * GetFinancialProportion(ranking).Percentage.Value/100;
            entities.SaveChanges();
            return finalScore/100;
        }

        public static decimal GetScore(RNKFinancialRow indexScore, CustomersBusinessRanking ranking)
        {
            if (!indexScore.LeafIndex) return 0;
            FBDEntities entities = new FBDEntities();
            var index = BusinessFinancialIndex.SelectFinancialIndexByID(entities,indexScore.Index.IndexID);

            if (ranking.BusinessIndustries == null || ranking.BusinessScales == null) return 0;

            if (indexScore.Index.ValueType == "N")
            {

                List<BusinessFinancialIndexScore> scoreList = BusinessFinancialIndexScore
                    .SelectScoreByIndustryByScaleByFinancialIndex(entities, ranking.BusinessIndustries.IndustryID, ranking.BusinessScales.ScaleID, index.IndexID);

                return GetScoreFromViewModel(indexScore, index, scoreList);
            }
            else
            {
                BusinessFinancialIndexScore score = BusinessFinancialIndexScore.SelectBusinessFinancialIndexScoreByScoreID(entities,indexScore.ScoreID);
                score.BusinessFinancialIndexLevelsReference.Load();
                indexScore.CalculatedScore = score.BusinessFinancialIndexLevels.Score;
                indexScore.Value = score.FixedValue;
                return indexScore.CalculatedScore;
            }
        }
        public static BusinessFinancialIndexLevels GetLevel(CustomersBusinessFinancialIndex indexScore,CustomersBusinessRanking ranking,FBDEntities entities)
        {
           
            indexScore.BusinessFinancialIndexReference.Load();
            var index=indexScore.BusinessFinancialIndex;
            if (!indexScore.BusinessFinancialIndex.LeafIndex) return null;
            if (ranking.BusinessIndustries == null || ranking.BusinessScales == null) return null;
            List<BusinessFinancialIndexScore> scoreList = BusinessFinancialIndexScore
                .SelectScoreByIndustryByScaleByFinancialIndex(entities, ranking.BusinessIndustries.IndustryID, ranking.BusinessScales.ScaleID, index.IndexID);

            return GetLevelFromModel(indexScore, index, scoreList);
        }

        private static BusinessFinancialIndexLevels GetLevelFromModel(CustomersBusinessFinancialIndex indexScore, BusinessFinancialIndex index, List<BusinessFinancialIndexScore> scoreList)
        {
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
                    if (indexScore.Value == null) break;
                    if (indexScore.Value.Equals(item.FixedValue))
                    {
                        item.BusinessFinancialIndexLevelsReference.Load();
                        indexScore.BusinessFinancialIndexLevels = item.BusinessFinancialIndexLevels;
                        return indexScore.BusinessFinancialIndexLevels;
                    }
                }
            }

            indexScore.BusinessFinancialIndexLevels = null;
            return null;
        }

        private static decimal GetScoreFromViewModel(RNKFinancialRow indexScore, BusinessFinancialIndex index, List<BusinessFinancialIndexScore> scoreList)
        {
            
            foreach (BusinessFinancialIndexScore item in scoreList)
            {

                    if (indexScore.Score == null) return 0;
                    decimal score = indexScore.Score.Value;
                    if (score >= item.FromValue && score <= item.ToValue)
                    {
                        item.BusinessFinancialIndexLevelsReference.Load();
                        indexScore.CalculatedScore = item.BusinessFinancialIndexLevels.Score;
                        return indexScore.CalculatedScore;
                    }
                

            }
            return 0;
        }
    }
}