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

    public class RNKScaleMarking
    {
        /// <summary>
        /// Save ScaleScore for an Scale object
        /// </summary>
        /// <param name="rankingID"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int SaveScaleScore(int rankingID,FBDEntities entities)
        {
            
            CustomersBusinessRanking ranking=CustomersBusinessRanking.SelectBusinessRankingByID(rankingID,entities);
            

            ranking.BusinessIndustriesReference.Load();
            string industryID=ranking.BusinessIndustries.IndustryID;

            ranking.BusinessScales= ScaleMarking(rankingID, industryID,entities);
            

            int result= entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Marking Scale for rankiing id and industryID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="industryID"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static BusinessScales ScaleMarking(int id,string industryID, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(industryID)) return null;
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id,entities);// get ranking item

            List<CustomersBusinessScale> scale = CustomersBusinessScale.SelectBusinessScaleByRankingID(id,entities); // get scale lsit

            decimal temp;
            return ScaleMarking(id, industryID, entities, scale,out temp );
        }

        public static BusinessScales ScaleMarking(int id, string industryID, FBDEntities entities,List<CustomersBusinessScale> scale,out decimal sum)
        {
            sum = 0;
            if (id == 0) return null;
            if (string.IsNullOrEmpty(industryID)) return null;
            if (entities == null || scale == null) return null;
            sum = 0;

            foreach (CustomersBusinessScale item in scale)
            {
                var temp = GetScaleScore(item, industryID);
                if (temp != null) sum += temp.Value;
            }

            return GetScale(sum, entities);
        }
        /// <summary>
        /// Mark and load scale used for preview only
        /// </summary>
        /// <param name="id"></param>
        /// <param name="industryID"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static BusinessScales ScaleTempMarking(int id, string industryID,List<RNKScaleRow> scale,out decimal sum)
        {
            sum = 0;
            if (id == 0) return null;
            if (string.IsNullOrEmpty(industryID)) return null;
            if (scale == null) return null;
            FBDEntities entities=new FBDEntities();
            

            foreach (RNKScaleRow item in scale)
            {

                //get all scale score
                List<BusinessScaleScore> scaleList = BusinessScaleScore.SelectScaleScore(industryID, item.CriteriaID);
                decimal value;
                try
                {
                    value = System.Convert.ToDecimal(item.Value);
                }
                catch
                {
                    value = 0;
                }
                foreach (BusinessScaleScore scaleScore in scaleList)
                {
                    if (value >= scaleScore.FromValue && value <= scaleScore.ToValue)
                    {
                        Nullable<decimal> scoreValue = scaleScore.Score;
                        item.Score = scoreValue;
                        if (scoreValue != null)
                            sum += scoreValue.Value;
                        break;
                    }
                }
            }

            return GetScale(sum, entities);
        }
        /// <summary>
        /// Get a specific Scale for a scale score
        /// </summary>
        /// <param name="score"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static BusinessScales GetScale(decimal score, FBDEntities entities)
        {
            var scaleList = entities.BusinessScales.ToList();
            foreach (BusinessScales item in scaleList)
            {
                if ((item.FromValue!=null) && (item.ToValue != null))
                if (score >= item.FromValue && score <= item.ToValue)
                {
                    return item;
                }
            }
            return null;
        }

        
        /// <summary>
        /// Get scale Score for each criteria and industryID
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="industryID"></param>
        /// <returns></returns>
        public static Nullable<decimal> GetScaleScore(CustomersBusinessScale scale,string industryID)
        {
            if (scale == null || string.IsNullOrEmpty(industryID)) return null;
            // get criteria and industry rankingID
            scale.BusinessScaleCriteriaReference.Load();
            string criteriaID=null;
            if (scale.BusinessScaleCriteria != null)
                criteriaID = scale.BusinessScaleCriteria.CriteriaID;
            else
            {
                scale.Score = null;
                return null;
            }
            //scale.CustomersBusinessRanking.BusinessIndustriesReference.Load();
            //string industryID = scale.CustomersBusinessRanking.BusinessIndustries.IndustryID;

            //get all scale score
            List<BusinessScaleScore> scaleList = BusinessScaleScore.SelectScaleScore(industryID, criteriaID);
            decimal value = 0;
            try
            {
                value = System.Convert.ToDecimal(scale.Value);
            }
            catch
            {
                value = 0;
            }
            foreach (BusinessScaleScore item in scaleList)
            {
                if ((item.FromValue != null) && (item.ToValue != null))
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