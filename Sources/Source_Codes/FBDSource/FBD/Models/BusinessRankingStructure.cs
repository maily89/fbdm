using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using FBD.CommonUtilities;

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

        internal static int Reset()
        {
            FBDEntities entities = new FBDEntities();
            var structures = entities.BusinessRankingStructure.ToList();
            int rankingTotal=Constants.NUMBER_OF_RANKING_STRUCTURE;

            if (structures.Count() > rankingTotal)
            {
                for (int i = rankingTotal; i < structures.Count-1; i++)
                {
                    entities.DeleteObject(structures[i]);
                }
                return entities.SaveChanges();
            }
            if (structures.Count() < rankingTotal)
            {
                int sum = 0;
                for (int i = structures.Count + 1; i <= rankingTotal; i++)
                {
                    BusinessRankingStructure temp = new BusinessRankingStructure();
                    temp.IndexType = "Index " + (i+1) / 2 ;
                    temp.AuditedStatus = "Status " + (i+1) % 2;
                    using (var tempo=new FBDEntities())
                    {
                        tempo.AddToBusinessRankingStructure(temp);
                        sum+=tempo.SaveChanges();
                    }
                 
                }
                return sum;
            }
            return 0;
        }
    } 
}
