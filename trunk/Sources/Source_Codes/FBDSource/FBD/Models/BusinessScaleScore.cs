using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessScaleScoreMetaData))]
    public partial class BusinessScaleScore
    {
        /// <summary>
        /// list of businessScaleScore
        /// </summary>
        /// <returns>list of businessScaleScore</returns>
        public static List<BusinessScaleScore> SelectScaleScore()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessScaleScore.ToList();
        }

        public static List<BusinessScaleScore> SelectScaleScore(string industryID, string criteriaID)
        {
            if (industryID == null || criteriaID == null) return null;

            FBDEntities entities = new FBDEntities();
            var scores = entities.BusinessScaleScore
                                .Include("BusinessIndustries")
                                .Include("BusinessScaleCriteria")
                                .Where(score=>score.BusinessIndustries.IndustryID == industryID && score.BusinessScaleCriteria.CriteriaID == criteriaID).ToList();


            return scores;
            
        }

        /// <summary>
        /// return the scaleScore specified by id
        /// </summary>
        /// <param name="id">id of the scaleScore</param>
        /// <returns>scaleScore</returns>
        public static BusinessScaleScore SelectScaleScoreByID(int id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                var scaleScore = entities.BusinessScaleScore.First(i => i.ScoreID == id);
                return scaleScore;

            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// return the scaleScore specified by id
        /// </summary>
        /// <param name="id">id of the scaleScore</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>scaleScore</returns>
        public static BusinessScaleScore SelectScaleScoreByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            try
            {
                var scaleScore = entities.BusinessScaleScore.First(i => i.ScoreID == id);
                return scaleScore;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// delete the scaleScore with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteScaleScore(int id)
        {
            FBDEntities entities = new FBDEntities();
            var scaleScore = BusinessScaleScore.SelectScaleScoreByID(id, entities);
            entities.DeleteObject(scaleScore);
            return entities.SaveChanges() <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the scaleScore
        /// </summary>
        /// <param name="scaleScore">update the scaleScore</param>
        public static int EditScaleScore(BusinessScaleScore scaleScore)
        {
            if (scaleScore == null) return 0;
            FBDEntities entities = new FBDEntities();
            var temp = BusinessScaleScore.SelectScaleScoreByID(scaleScore.ScoreID, entities);

            //temp.CriteriaID = scaleScore.CriteriaID;
            temp.FromValue = scaleScore.FromValue;
            //temp.IndustryID = scaleScore.IndustryID;
            temp.ToValue = scaleScore.ToValue;
            temp.Score = scaleScore.Score;

            return entities.SaveChanges() <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new scaleScore
        /// </summary>
        /// <param name="scaleScore">the scaleScore to add</param>
        public static int AddScaleScore(BusinessScaleScore scaleScore)
        {
            if (scaleScore == null) return 0;
            //if (scaleScore. == null || scaleScore.CriteriaID == null) return 0;
            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessScaleScore(scaleScore);
            int result=entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new scaleScore
        /// </summary>
        /// <param name="scaleScore">the scaleScore to add</param>
        public static int AddScaleScore(BusinessScaleScore scaleScore,FBDEntities entities)
        {
            if (scaleScore == null) return 0;
            //if (scaleScore. == null || scaleScore.CriteriaID == null) return 0;
            
            entities.AddToBusinessScaleScore(scaleScore);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        public class BusinessScaleScoreMetaData
        {
        		
        	[DisplayName("Score ID")]
        	[Required]
            public int ScoreID { get; set; }
        		
            [Required]
        	[DisplayName("From Value")]
            public Nullable<decimal> FromValue { get; set; }
        		
            [Required]
        	[DisplayName("To Value")]
            public Nullable<decimal> ToValue { get; set; }
        		
            [Required]
        	[DisplayName("Score")]
            public Nullable<decimal> Score { get; set; }
        }
    }
}
