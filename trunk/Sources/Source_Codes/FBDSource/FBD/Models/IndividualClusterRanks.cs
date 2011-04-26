using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualClusterRanksMetaData))]
    public partial class IndividualClusterRanks
    {
        /// <summary>
        /// List All business cluster rank
        /// </summary>
        /// <returns></returns>
        public static List<IndividualClusterRanks> SelectClusterRank()
        {
            FBDEntities FBDModel = new FBDEntities();
            return FBDModel.IndividualClusterRanks.OrderBy(icr => icr.RankID).ToList();
        }
        /// <summary>
        /// List All business cluster rank
        /// </summary>
        /// <returns></returns>
        public static List<IndividualClusterRanks> SelectClusterRank( FBDEntities FBDModel )
        {
            return FBDModel.IndividualClusterRanks.OrderBy(icr=>icr.RankID).ToList();
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <returns>rank</returns>
        public static IndividualClusterRanks SelectClusterRankByID(int id)
        {
            //if (id = null) return null;
            FBDEntities entities = new FBDEntities();
            var rank = entities.IndividualClusterRanks.First(i => id.Equals(i.RankID));
            return rank;
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>rank</returns>
        public static IndividualClusterRanks SelectClusterRankByID(string id, FBDEntities entities)
        {
            if ( entities == null) return null;
            var rank = entities.IndividualClusterRanks.First(i => id.Equals(i.RankID));
            return rank;
        }

     
        /// <summary>
        /// edit the rank
        /// </summary>
        /// <param name="rank">update the rank</param>
        public static int EditRank(IndividualClusterRanks rank, FBDEntities entities)
        {
            if (rank == null) return 0;
            var temp = SelectClusterRankByID(rank.RankID,entities);
            temp.Rank = rank.Rank;
            //temp. = rank.Evaluation;

            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        ///check there are cluster with id = id in db 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static bool IsExistRank(int id, FBDEntities entities)
        {
            return entities.IndividualClusterRanks.Where(cr => id.Equals(cr.RankID)).Any();
        }
        /// <summary>
        /// delete the rank with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteRank(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;

            FBDEntities entities = new FBDEntities();
            List<CustomersIndividualRanking> cirList = Models.CustomersIndividualRanking.SelectIndividualRankingByClusterRankID(id, entities);
            int finalResult = 1;
            foreach (CustomersIndividualRanking cbr in cirList)
                finalResult *= Models.CustomersIndividualRanking.UpdateIndividualRanking(cbr, null, entities);
            //we check if all update success
            if (finalResult == 0)
                return 0;

            var rank = IndividualClusterRanks.SelectClusterRankByID(id, entities);
            entities.DeleteObject(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new rank
        /// </summary>
        /// <param name="rank">the rank to add</param>
        public static int AddRank(IndividualClusterRanks rank, FBDEntities entities)
        {
            if (rank == null) return 0;

            entities.AddToIndividualClusterRanks(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public static int updateCentroid(string id, Vector centroid,FBDEntities entities)
        {
            IndividualClusterRanks icr = IndividualClusterRanks.SelectClusterRankByID(id, entities);
            icr.CentroidX = Convert.ToDecimal(centroid.x);
            icr.CentroidY = Convert.ToDecimal(centroid.y);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public class IndividualClusterRanksMetaData
        {
            [DisplayName("rankID")]
            [Required(ErrorMessage = "Rank is required")]
            [RegularExpression("[0-9]{2,}", ErrorMessage = "ID must be a number and more than 2 charater example:01,02")]
            public string RankID { get; set; }

            [DisplayName("Rank")]
            [Required(ErrorMessage = "Rank is required")]
            [StringLength(10, ErrorMessage = "maximum 10 charater for rank")]
            public string Rank { set; get; }

            //[DisplayName("Evaluation")]
            //[StringLength(50, ErrorMessage = "Maximum 50 charater for Evaluation")]
            //public string Evaluation { set; get; }
        }
    }
}
