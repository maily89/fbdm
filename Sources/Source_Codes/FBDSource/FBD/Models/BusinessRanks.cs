using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessRanksMetaData))]
    public partial class BusinessRanks
    {
        /// <summary>
        /// list of businessRanks
        /// </summary>
        /// <returns>list of businessRanks</returns>
        public static List<BusinessRanks> SelectRanks()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessRanks.ToList();
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <returns>rank</returns>
        public static BusinessRanks SelectRankByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var rank = entities.BusinessRanks.First(i => i.RankID == id);
            return rank;
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>rank</returns>
        public static BusinessRanks SelectRankByID(string id, FBDEntities entities)
        {

            var rank = entities.BusinessRanks.First(i => i.RankID == id);
            return rank;
        }

        /// <summary>
        /// delete the rank with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static void DeleteRank(string id)
        {
            FBDEntities entities = new FBDEntities();
            var rank = BusinessRanks.SelectRankByID(id, entities);
            entities.DeleteObject(rank);
            entities.SaveChanges();
        }

        /// <summary>
        /// edit the rank
        /// </summary>
        /// <param name="rank">update the rank</param>
        public static void EditRank(BusinessRanks rank)
        {
            FBDEntities entities = new FBDEntities();

            var temp = SelectRankByID(rank.RankID, entities);
            temp.Rank = rank.Rank;
            temp.FromValue = rank.FromValue;
            temp.ToValue = rank.ToValue;
            temp.DebtGroup = rank.DebtGroup;
            temp.RiskGroup = rank.RiskGroup;
            temp.Evaluation = rank.Evaluation;

            entities.SaveChanges();
        }

        /// <summary>
        /// add new rank
        /// </summary>
        /// <param name="rank">the rank to add</param>
        public static void AddRank(BusinessRanks rank)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessRanks(rank);
            entities.SaveChanges();
        }
        public class BusinessRanksMetaData
        {
        		
        	[DisplayName("Rank ID")]
        	[Required]
        	[StringLength(3)]
            public string RankID { get; set; }
        		
        	[DisplayName("From Value")]
            public Nullable<decimal> FromValue { get; set; }
        		
        	[DisplayName("To Value")]
            public Nullable<decimal> ToValue { get; set; }
        		
        	[DisplayName("Rank")]
        	[StringLength(10,ErrorMessage="Rank must be under 10 characters")]
            public string Rank { get; set; }
        		
        	[DisplayName("Evaluation")]
        	[StringLength(50)]
            public string Evaluation { get; set; }
        		
        	[DisplayName("Risk Group")]
        	[StringLength(50)]
            public string RiskGroup { get; set; }
        		
        	[DisplayName("Debt Group")]
            public Nullable<int> DebtGroup { get; set; }
        }
    }
}
