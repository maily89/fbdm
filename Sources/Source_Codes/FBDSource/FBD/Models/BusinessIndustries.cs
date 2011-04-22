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
            return entities.BusinessIndustries.OrderBy(m => m.IndustryID).ToList();
        }

        /// <summary>
        /// return the industry specified by id
        /// </summary>
        /// <param name="id">id of the industry</param>
        /// <returns>industry</returns>
        public static BusinessIndustries SelectIndustryByID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            try
            {
                FBDEntities entities = new FBDEntities();
                var industry = entities.BusinessIndustries.First(i => i.IndustryID == id);

                return industry;
            }
            catch 
            {
                
                return null;
            }
        }

        public static bool IsIDExist(string id)
        {
            FBDEntities entities = new FBDEntities();

            return entities.BusinessIndustries.Where(i => i.IndustryID == id).Any();
        }

        /// <summary>
        /// return the industry specified by id
        /// </summary>
        /// <param name="id">id of the industry</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>industry</returns>
        public static BusinessIndustries SelectIndustryByID(string id,FBDEntities entities)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || entities == null) return null;
                var industry = entities.BusinessIndustries.First(i => i.IndustryID == id);
                return industry;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// delete the industry with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteIndustry(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;
            FBDEntities entities = new FBDEntities();
            var industry = BusinessIndustries.SelectIndustryByID(id,entities);
            entities.DeleteObject(industry);
            int temp=entities.SaveChanges();

            return temp<=0?0:1;
        }

        /// <summary>
        /// edit the industry
        /// </summary>
        /// <param name="industry">update the industry</param>
        public static int EditIndustry(BusinessIndustries industry)
        {
            if (industry == null) return 0;
            FBDEntities entities = new FBDEntities();
            var temp = BusinessIndustries.SelectIndustryByID(industry.IndustryID,entities);
            temp.IndustryName = industry.IndustryName;
            int result=entities.SaveChanges();
            return result<=0?0:1;
        }

        /// <summary>
        /// add new industry
        /// </summary>
        /// <param name="industry">the industry to add</param>
        public static int AddIndustry(BusinessIndustries industry)
        {
            if (industry == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToBusinessIndustries(industry);

            int temp= entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
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
