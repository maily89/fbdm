using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;

namespace FBD.Models
{

    public class RNKScaleMarking
    {
        public static int SaveScaleScore(int rankingID,FBDEntities entities)
        {
            
            CustomersBusinessRanking ranking=CustomersBusinessRanking.SelectBusinessRankingByID(rankingID,entities);
            

            ranking.BusinessIndustriesReference.Load();
            string industryID=ranking.BusinessIndustries.IndustryID;

            ranking.BusinessScales= ScaleMarking(rankingID, industryID,entities);
            

            int result= entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }


        public static BusinessScales ScaleMarking(int id,string industryID, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(industryID)) return null;
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id,entities);// get ranking item

            List<CustomersBusinessScale> scale = CustomersBusinessScale.SelectBusinessScaleByRankingID(id,entities); // get scale lsit

            decimal sum = 0;

            foreach (CustomersBusinessScale item in scale)
            {
                var temp = GetScaleScore(item,industryID);
                if (temp != null) sum += temp.Value;
            }

            
            return GetScale(sum, entities);
        }

        public static BusinessScales GetScale(decimal score, FBDEntities entities)
        {
            var scaleList = entities.BusinessScales.ToList();
            foreach (BusinessScales item in scaleList)
            {
                if (score >= item.FromValue && score <= item.ToValue)
                {
                    return item;
                }
            }
            return null;
        }
        
        public static Nullable<decimal> GetScaleScore(CustomersBusinessScale scale,string industryID)
        {
            if (scale == null || string.IsNullOrEmpty(industryID)) return null;
            // get criteria and industry rankingID
            scale.BusinessScaleCriteriaReference.Load();
            string criteriaID = scale.BusinessScaleCriteria.CriteriaID;

            //scale.CustomersBusinessRanking.BusinessIndustriesReference.Load();
            //string industryID = scale.CustomersBusinessRanking.BusinessIndustries.IndustryID;

            //get all scale score
            List<BusinessScaleScore> scaleList = BusinessScaleScore.SelectScaleScore(industryID, criteriaID);
            decimal value=System.Convert.ToDecimal(scale.Value);
            foreach (BusinessScaleScore item in scaleList)
            {
                if (value >= item.FromValue && value <= item.ToValue)
                {
                    Nullable<decimal> scoreValue = item.Score;
                    scale.Score = scoreValue;
                    return scale.Score;
                }

            }
            scale.Score = null;
            return null;
        }

    }
}