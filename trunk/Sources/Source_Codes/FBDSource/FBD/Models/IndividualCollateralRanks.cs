using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualCollateralRanksMetaData))]
    public partial class IndividualCollateralRanks: IRanks
    {
        /// <summary>
        /// list of IndividualCollateralRanks
        /// </summary>
        /// <returns>list of IndividualCollateralRanks</returns>
        public static List<IndividualCollateralRanks> SelectRanks()
        {
            FBDEntities entities = new FBDEntities();
            return entities.IndividualCollateralRanks.ToList();
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <returns>rank</returns>
        public static IndividualCollateralRanks SelectRankByID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            FBDEntities entities = new FBDEntities();
            var rank = entities.IndividualCollateralRanks.First(i => i.RankID == id);
            return rank;
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>rank</returns>
        public static IndividualCollateralRanks SelectRankByID(string id, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(id) || entities == null) return null;
            var rank = entities.IndividualCollateralRanks.First(i => i.RankID == id);
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
            var rank = IndividualCollateralRanks.SelectRankByID(id, entities);
            entities.DeleteObject(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the rank
        /// </summary>
        /// <param name="rank">update the rank</param>
        public static int EditRank(IndividualCollateralRanks rank)
        {
            if (rank == null) return 0;

            FBDEntities entities = new FBDEntities();

            var temp = SelectRankByID(rank.RankID, entities);
            temp.Rank = rank.Rank;
            temp.FromValue = rank.FromValue;
            temp.ToValue = rank.ToValue;
            temp.Evaluation = rank.Evaluation;

            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new rank
        /// </summary>
        /// <param name="rank">the rank to add</param>
        public static int AddRank(IndividualCollateralRanks rank)
        {
            if (rank == null) return 0;

            FBDEntities entities = new FBDEntities();
            entities.AddToIndividualCollateralRanks(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        public class IndividualCollateralRanksMetaData
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

            [DisplayName("Evaluation")]
            [StringLength(50)]
            public string Evaluation { get; set; }
        }
    }
}
