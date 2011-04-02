using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualBasicRanksMetaData))]
    public partial class IndividualBasicRanks: IRanks
    {
        /// <summary>
        /// list of IndividualBasicRanks
        /// </summary>
        /// <returns>list of IndividualBasicRanks</returns>
        public static List<IndividualBasicRanks> SelectRanks()
        {
            FBDEntities entities = new FBDEntities();
            return entities.IndividualBasicRanks.ToList();
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <returns>rank</returns>
        public static IndividualBasicRanks SelectRankByID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            FBDEntities entities = new FBDEntities();
            var rank = entities.IndividualBasicRanks.First(i => i.RankID == id);
            return rank;
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>rank</returns>
        public static IndividualBasicRanks SelectRankByID(string id, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(id) || entities == null) return null;
            var rank = entities.IndividualBasicRanks.First(i => i.RankID == id);
            return rank;
        }

        /// <summary>
        /// delete the rank with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteRank(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;

            FBDEntities entities = new FBDEntities();
            var rank = IndividualBasicRanks.SelectRankByID(id, entities);
            entities.DeleteObject(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the rank
        /// </summary>
        /// <param name="rank">update the rank</param>
        public static int EditRank(IndividualBasicRanks rank)
        {
            if (rank == null) return 0;

            FBDEntities entities = new FBDEntities();

            var temp = SelectRankByID(rank.RankID, entities);
            temp.Rank = rank.Rank;
            temp.FromValue = rank.FromValue;
            temp.ToValue = rank.ToValue;
            temp.RiskGroup = rank.RiskGroup;

            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new rank
        /// </summary>
        /// <param name="rank">the rank to add</param>
        public static int AddRank(IndividualBasicRanks rank)
        {
            if (rank == null) return 0;

            FBDEntities entities = new FBDEntities();
            entities.AddToIndividualBasicRanks(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        public class IndividualBasicRanksMetaData
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
            [StringLength(10, ErrorMessage = "Rank must be under 10 characters")]
            public string Rank { get; set; }

            [DisplayName("Risk Group")]
            [StringLength(50)]
            public string RiskGroup { get; set; }
        }
    }
}
