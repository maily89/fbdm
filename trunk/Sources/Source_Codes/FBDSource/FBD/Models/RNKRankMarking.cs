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

        public static BusinessRanks GetBusinessRank(decimal score, FBDEntities entities)
        {
            var rankList = BusinessRanks.SelectRanks(entities);
            foreach (BusinessRanks item in rankList)
            {
                if (IsInRank(score, item)) return item;
            }
            return null;
        }
        public static IndividualSummaryRanks GetIndividualRank(int id, FBDEntities entities)
        {
            
            var ranking = CustomersIndividualRanking.SelectIndividualRankingByID(id,entities);

            if (ranking.BasicIndexScore == null || ranking.CollateralIndexScore == null) return null;
            var basicRank = GetBasicRank(ranking.BasicIndexScore.Value);
            var collateralRank = GetCollateralRank(ranking.CollateralIndexScore.Value);

            var rankValid = IndividualSummaryRanks.selectSummaryRankByBasicAndCollateral(entities, basicRank.RankID, collateralRank.RankID);

            if (rankValid.Count <= 0) return null;
            else return rankValid[0];
        
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