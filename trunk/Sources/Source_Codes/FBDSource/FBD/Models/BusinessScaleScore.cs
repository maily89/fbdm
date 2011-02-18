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

        /// <summary>
        /// return the scaleScore specified by id
        /// </summary>
        /// <param name="id">id of the scaleScore</param>
        /// <returns>scaleScore</returns>
        public static BusinessScaleScore SelectScaleScoreByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var scaleScore = entities.BusinessScaleScore.First(i => i.ScoreID == id);
            return scaleScore;
        }

        /// <summary>
        /// return the scaleScore specified by id
        /// </summary>
        /// <param name="id">id of the scaleScore</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>scaleScore</returns>
        public static BusinessScaleScore SelectScaleScoreByID(int id, FBDEntities entities)
        {

            var scaleScore = entities.BusinessScaleScore.First(i => i.ScoreID == id);
            return scaleScore;
        }

        /// <summary>
        /// delete the scaleScore with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static void DeleteScaleScore(int id)
        {
            FBDEntities entities = new FBDEntities();
            var scaleScore = BusinessScaleScore.SelectScaleScoreByID(id, entities);
            entities.DeleteObject(scaleScore);
            entities.SaveChanges();
        }

        /// <summary>
        /// edit the scaleScore
        /// </summary>
        /// <param name="scaleScore">update the scaleScore</param>
        public static void EditScaleScore(BusinessScaleScore scaleScore)
        {
            FBDEntities entities = new FBDEntities();
            var temp = BusinessScaleScore.SelectScaleScoreByID(scaleScore.ScoreID, entities);

            temp.CriteriaID = scaleScore.CriteriaID;
            temp.FromValue = scaleScore.FromValue;
            temp.IndustryID = scaleScore.IndustryID;
            temp.ToValue = scaleScore.ToValue;
            temp.Score = scaleScore.Score;

            entities.SaveChanges();
        }

        /// <summary>
        /// add new scaleScore
        /// </summary>
        /// <param name="scaleScore">the scaleScore to add</param>
        public static void AddScaleScore(BusinessScaleScore scaleScore)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessScaleScore(scaleScore);
            entities.SaveChanges();
        }
        public class BusinessScaleScoreMetaData
        {
        		
        	[DisplayName("Score ID")]
        	[Required]
            public int ScoreID { get; set; }
        		
        	[DisplayName("Criteria ID")]
        	[Required]
        	[StringLength(2)]
            public string CriteriaID { get; set; }
        		
        	[DisplayName("Industry ID")]
        	[Required]
        	[StringLength(3)]
            public string IndustryID { get; set; }
        		
        	[DisplayName("From Value")]
            public Nullable<decimal> FromValue { get; set; }
        		
        	[DisplayName("To Value")]
            public Nullable<decimal> ToValue { get; set; }
        		
        	[DisplayName("Score")]
            public Nullable<decimal> Score { get; set; }
        }
    }
}
