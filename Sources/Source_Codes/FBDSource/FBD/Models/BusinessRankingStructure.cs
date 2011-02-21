using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessRankingStructureMetaData))]
    public partial class BusinessRankingStructure
    {
        /// <summary>
        /// list of businessRankingStructures
        /// </summary>
        /// <returns>list of businessRankingStructures</returns>
        public static List<BusinessRankingStructure> SelectRankingStructures()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessRankingStructure.ToList();
        }

        /// <summary>
        /// return the rankingStructure specified by id
        /// </summary>
        /// <param name="id">id of the rankingStructure</param>
        /// <returns>rankingStructure</returns>
        public static BusinessRankingStructure SelectRankingStructureByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var rankingStructure = entities.BusinessRankingStructure.First(i => i.ID == id);
            return rankingStructure;
        }

        /// <summary>
        /// return the rankingStructure specified by id
        /// </summary>
        /// <param name="id">id of the rankingStructure</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>rankingStructure</returns>
        public static BusinessRankingStructure SelectRankingStructureByID(int id, FBDEntities entities)
        {

            var rankingStructure = entities.BusinessRankingStructure.First(i => i.ID == id);
            return rankingStructure;
        }

        /// <summary>
        /// delete the rankingStructure with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteRankingStructure(int id)
        {
            FBDEntities entities = new FBDEntities();
            var rankingStructure = BusinessRankingStructure.SelectRankingStructureByID(id, entities);
            entities.DeleteObject(rankingStructure);
            return entities.SaveChanges()<=0?0:1;
        }

        /// <summary>
        /// edit the rankingStructure
        /// </summary>
        /// <param name="rankingStructure">update the rankingStructure</param>
        public static int EditRankingStructure(BusinessRankingStructure rankingStructure)
        {
            FBDEntities entities = new FBDEntities();
            var temp = BusinessRankingStructure.SelectRankingStructureByID(rankingStructure.ID, entities);
            temp.AuditedStatus = rankingStructure.AuditedStatus;
            temp.IndexType = rankingStructure.IndexType;
            temp.Percentage = rankingStructure.Percentage;
            return entities.SaveChanges()<=0?0:1;
        }

        /// <summary>
        /// add new rankingStructure
        /// </summary>
        /// <param name="rankingStructure">the rankingStructure to add</param>
        public static int AddRankingStructure(BusinessRankingStructure rankingStructure)
        {
            FBDEntities entities = new FBDEntities();

            entities.AddToBusinessRankingStructure(rankingStructure);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }
        public class BusinessRankingStructureMetaData
        {

            [Required]
            public int ID { get; set; }

        	[DisplayName("Index Type")]
        	[Required]
        	[StringLength(10)]
            public string IndexType { get; set; }
        		
        	[DisplayName("Audited Status")]
        	[Required]
        	[StringLength(10)]
            public string AuditedStatus { get; set; }
        		
        	[DisplayName("Percentage")]
            public Nullable<decimal> Percentage { get; set; }
        }
    }
}
