using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.CommonUtilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FBD.Models;
namespace FBD.Models
{   
    [MetadataType(typeof(BusinessClusterRanksMetaData))]
    public partial class BusinessClusterRanks
    {
         //static FBDEntities FBDModel = new FBDEntities();
        
        
         
        /// <summary>
        /// select cluster rank order by rank id
        /// </summary>
        /// <returns></returns>
         public static List<BusinessClusterRanks> SelectClusterRank()
         {
             FBDEntities FBDModel = new FBDEntities();
             
             return FBDModel.BusinessClusterRanks.OrderBy(bcr=>bcr.RankID).ToList();
         }

         /// <summary>
         /// List All business cluster rank
         /// </summary>
         /// <returns></returns>
         public static List<BusinessClusterRanks> SelectClusterRank(FBDEntities FBDModel)
         {

             return FBDModel.BusinessClusterRanks.OrderBy(bcr => bcr.RankID).ToList();
         }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <returns>rank</returns>
        public static BusinessClusterRanks SelectClusterRankByID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            FBDEntities entities = new FBDEntities();
            var rank = entities.BusinessClusterRanks.First(i => i.RankID == id);
            return rank;
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>rank</returns>
        public static BusinessClusterRanks SelectClusterRankByID(string id, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(id) || entities == null) return null;
            var rank = entities.BusinessClusterRanks.First(i => i.RankID == id);
            return rank;
        }

        /// <summary>
        ///check there are cluster with id = id in db 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static bool IsExistRank(string id, FBDEntities entities)
        {
            return entities.BusinessClusterRanks.Where(cr => id.Equals(cr.RankID)).Any();
        }

        /// <summary>
        /// edit the rank
        /// </summary>
        /// <param name="rank">update the rank</param>
        public static int EditRank(BusinessClusterRanks rank,FBDEntities entities)
        {
            if (rank == null) return 0;

            var temp = SelectClusterRankByID(rank.RankID, entities);
            temp.Rank = rank.Rank;
            temp.Evaluation = rank.Evaluation;
            //this code maybe dangerous. Maybe we need another way to make it more security
            temp.Centroid = rank.Centroid;
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public static int UpdateCentroid(string id, Vector centroid,FBDEntities entities)
        {
            var temp = SelectClusterRankByID(id, entities);
            temp.Centroid = Convert.ToDecimal(centroid.x);
            var result = entities.SaveChanges();

            return result<=0?0:1;
        }

        /// <summary>
        /// edit the rank
        /// </summary>
        /// <param name="rank">update the rank</param>
        //public static int DecreaseID(BusinessClusterRanks rank,int decreaseValue, FBDEntities entities)
        //{
        //    if (rank == null) return 0;

        //    var temp = SelectClusterRankByID(rank.RankID, entities);
        //    temp.RankID = (int.Parse(temp.RankID) - decreaseValue).ToString();
            
        //    var result = entities.SaveChanges();
        //    return result <= 0 ? 0 : 1;
        //}
        /// <summary>
        /// delete the rank with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteRank(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;

            FBDEntities entities = new FBDEntities();
            //update all customerbusinessRanking have clusterRank referent to BusinessCluterRank
            List<CustomersBusinessRanking> cbrList = Models.CustomersBusinessRanking.SelectBusinessRankingByClusterRankID(id,entities);
            int finalResult = 1;
            foreach (CustomersBusinessRanking cbr in cbrList)
                finalResult*=  Models.CustomersBusinessRanking.UpdateBusinessRanking(cbr, null, entities);
            //we check if all update success
            if (finalResult == 0)
                return 0;

            var rank = BusinessClusterRanks.SelectClusterRankByID(id, entities);
            entities.DeleteObject(rank);
            var result = entities.SaveChanges();
            //then update other buslinessclusterRanks
            //be care with this exception
            //List<BusinessClusterRanks> needUpdateList = entities.BusinessClusterRanks.Where(bcr => id.CompareTo(bcr.RankID)<0).OrderBy(cbr => cbr.RankID).ToList();
            ////list list must increase by id then update them
            //foreach (BusinessClusterRanks bc in needUpdateList)
            //{
            //    DecreaseID(bc, 1, entities);
            //}
            return result <= 0 ? 0 : 1;
        }

        ///// <summary>
        ///// add new rank
        ///// </summary>
        ///// <param name="rank">the rank to add</param>
        //public static int AddRank(BusinessClusterRanks rank,FBDEntities entities)
        //{
        //    if (rank == null) return 0;

        //    entities.AddToBusinessClusterRanks(rank);
        //    var result = entities.SaveChanges();
        //    return result <= 0 ? 0 : 1;
        //}
       
        /// <summary>
        /// add new rank
        /// </summary>
        /// <param name="rank">the rank to add</param>
        public static int AddRank(BusinessClusterRanks rank,FBDEntities entities,bool autoAssignID)
        {
            if (rank == null) return 0;
            //if (autoAssignID)
            //{
            //    int ID = SelectClusterRank().Count() + 1;
            //}

            entities.AddToBusinessClusterRanks(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
            
        }

        public class BusinessClusterRanksMetaData
        {
            [DisplayName("rankID")]
            [Required(ErrorMessage = "Rank is required")]
            [RegularExpression("[0-9]{2,}", ErrorMessage = "ID must be a number and more than 2 charater example:01,02")]
            public string RankID { get; set; }

            [DisplayName("Rank")]
            [Required(ErrorMessage="Rank is required")]
            [StringLength(10,ErrorMessage="maximum 10 charater for rank")]
            public string Rank { set; get; }

            [DisplayName("Evaluation")]
            [StringLength(50,ErrorMessage="Maximum 50 charater for Evaluation")]
            public string Evaluation { set; get; }
        }
    }
}
