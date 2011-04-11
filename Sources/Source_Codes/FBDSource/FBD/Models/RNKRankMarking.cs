using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;

namespace FBD.Models
{
    public class RNKRankMarking
    {
        public static IndividualBasicRanks GetBasicRank(decimal score)
        {

            var rankList = IndividualBasicRanks.SelectRanks();
            foreach (IndividualBasicRanks item in rankList)
            {
                if (IsInRank(score,item)) return item;
            }
            return null;
        }

        public static IndividualCollateralRanks GetCollateralRank(decimal score)
        {

            var rankList = IndividualCollateralRanks.SelectRanks();
            foreach (IndividualCollateralRanks item in rankList)
            {
                if (IsInRank(score, item)) return item;
            }
            return null;
        }

        public static int RemarkAll(int id)
        {
            FBDEntities entities=new FBDEntities();
            RNKScaleMarking.SaveScaleScore(id,entities);
            RNKFinancialMarking.CalculateFinancialScore(id, false, entities);
            RNKNonFinancialMarking.CalculateNonFinancialScore(id, false, entities);

            return SaveBusinessRank(id);
        }

        public static int SaveBusinessRank(int id)
        {
            FBDEntities entities=new FBDEntities();
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id, entities);
            ranking.BusinessRanks = GetBusinessRank(ranking.FinancialScore.Value + ranking.NonFinancialScore.Value, entities);
            return entities.SaveChanges() == 1 ? 0 : 1;
        }

        public static BusinessRanks GetBusinessRank(decimal score, FBDEntities entities)
        {
            var rankList = BusinessRanks.SelectRanks(entities);
            foreach (BusinessRanks item in rankList)
            {
                if (IsInRank(score, item)) return item;
            }
            return null;
        }
        public static IndividualSummaryRanks SaveIndividualRank(int id, FBDEntities entities)
        {
            
            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id,entities);

            if (ranking.BasicIndexScore == null || ranking.CollateralIndexScore == null) return null;
            var basicRank = GetBasicRank(ranking.BasicIndexScore.Value);
            var collateralRank = GetCollateralRank(ranking.CollateralIndexScore.Value);

            var rankValid = IndividualSummaryRanks.selectSummaryRankByBasicAndCollateral(entities, basicRank.RankID, collateralRank.RankID);

            if (rankValid.Count <= 0) return null;
            else
            {
                ranking.IndividualSummaryRanks = rankValid[0];
                entities.SaveChanges();
                return rankValid[0];
            }
        
        }




        private static bool IsInRank(decimal score, IRanks item)
        {
            if (item.FromValue.Value != null && item.ToValue != null)
            {
                if (score >= item.FromValue.Value && score <= item.ToValue.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}