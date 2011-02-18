using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessScaleCriteriaMetadata))]
    public partial class BusinessScaleCriteria
    {
        /// <summary>
        /// list of businessScaleCriteria
        /// </summary>
        /// <returns>list of businessScaleCriteria</returns>
        public static List<BusinessScaleCriteria> SelectScaleCriteria()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessScaleCriteria.ToList();
        }

        /// <summary>
        /// return the scaleCriteria specified by id
        /// </summary>
        /// <param name="id">id of the scaleCriteria</param>
        /// <returns>scaleCriteria</returns>
        public static BusinessScaleCriteria SelectScaleCriteriaByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var scaleCriteria = entities.BusinessScaleCriteria.First(i => i.CriteriaID == id);
            return scaleCriteria;
        }

        /// <summary>
        /// return the scaleCriteria specified by id
        /// </summary>
        /// <param name="id">id of the scaleCriteria</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>scaleCriteria</returns>
        public static BusinessScaleCriteria SelectScaleCriteriaByID(string id, FBDEntities entities)
        {

            var scaleCriteria = entities.BusinessScaleCriteria.First(i => i.CriteriaID == id);
            return scaleCriteria;
        }

        /// <summary>
        /// delete the scaleCriteria with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static void DeleteScaleCriteria(string id)
        {
            FBDEntities entities = new FBDEntities();
            var scaleCriteria = BusinessScaleCriteria.SelectScaleCriteriaByID(id, entities);
            entities.DeleteObject(scaleCriteria);
            entities.SaveChanges();
        }

        /// <summary>
        /// edit the scaleCriteria
        /// </summary>
        /// <param name="scaleCriteria">update the scaleCriteria</param>
        public static void EditScaleCriteria(BusinessScaleCriteria scaleCriteria)
        {
            FBDEntities entities = new FBDEntities();
            var temp = BusinessScaleCriteria.SelectScaleCriteriaByID(scaleCriteria.CriteriaID, entities);

            temp.CriteriaName = scaleCriteria.CriteriaName;
            temp.Formula = scaleCriteria.Formula ;
            temp.Unit = scaleCriteria.Unit;
            temp.ValueType = scaleCriteria.ValueType;

            entities.SaveChanges();
        }

        /// <summary>
        /// add new scaleCriteria
        /// </summary>
        /// <param name="scaleCriteria">the scaleCriteria to add</param>
        public static void AddScaleCriteria(BusinessScaleCriteria scaleCriteria)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessScaleCriteria(scaleCriteria);
            entities.SaveChanges();
        }
        public class BusinessScaleCriteriaMetadata
        {
            [DisplayName("Criteria ID")]
            [Required]
            [StringLength(2)]
            public string CriteriaID { get; set; }

            [DisplayName("Criteria Name")]
            [StringLength(255)]
            public string CriteriaName { get; set; }

            [DisplayName("Unit")]
            [StringLength(50)]
            public string Unit { get; set; }

            [DisplayName("Formula")]
            [StringLength(255)]
            public string Formula { get; set; }

            [DisplayName("Value Type")]
            [StringLength(1)]
            public string ValueType { get; set; }

        }
    }
}
