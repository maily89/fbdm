using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessIndustryMetaData))] 
    public partial class BusinessIndustries
    {
        /// <summary>
        /// list of businessIndustries
        /// </summary>
        /// <returns>list of businessIndustries</returns>
        public static List<BusinessIndustries> SelectIndustries()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessIndustries.ToList();
        }

        /// <summary>
        /// return the industry specified by id
        /// </summary>
        /// <param name="id">id of the industry</param>
        /// <returns>industry</returns>
        public static BusinessIndustries SelectIndustryByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var industry = entities.BusinessIndustries.First(i => i.IndustryID == id);
            return industry;
        }

        /// <summary>
        /// return the industry specified by id
        /// </summary>
        /// <param name="id">id of the industry</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>industry</returns>
        public static BusinessIndustries SelectIndustryByID(string id,FBDEntities entities)
        {
            
            var industry = entities.BusinessIndustries.First(i => i.IndustryID == id);
            return industry;
        }

        /// <summary>
        /// delete the industry with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static void DeleteIndustry(string id)
        {
            FBDEntities entities = new FBDEntities();
            var industry = BusinessIndustries.SelectIndustryByID(id,entities);
            entities.DeleteObject(industry);
            entities.SaveChanges();
        }

        /// <summary>
        /// edit the industry
        /// </summary>
        /// <param name="industry">update the industry</param>
        public static void EditIndustry(BusinessIndustries industry)
        {
            FBDEntities entities = new FBDEntities();
            var temp = BusinessIndustries.SelectIndustryByID(industry.IndustryID,entities);
            temp.IndustryName = industry.IndustryName;
            entities.SaveChanges();
        }

        /// <summary>
        /// add new industry
        /// </summary>
        /// <param name="industry">the industry to add</param>
        public static void AddIndustry(BusinessIndustries industry)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessIndustries(industry);
            entities.SaveChanges();
        }
        public class BusinessIndustryMetaData
        {
            [DisplayName("Industry ID")]
            [Required(ErrorMessage="Industry ID is required")]
            [StringLength(3)]
            public string IndustryID { get; set; }

            [DisplayName("Industry Name")]
            [Required(ErrorMessage="Industry Name is required")]
            [StringLength(255)]
            public string IndustryName { get; set; }
        }
    }
}
