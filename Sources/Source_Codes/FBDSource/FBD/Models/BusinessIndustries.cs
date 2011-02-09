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
        //TODO: this is temporary error handle.
        private string error = " ";
        public string Error { get { return error; } set { error = value; } }

        public static List<BusinessIndustries> SelectIndustries()
        {
            return DatabaseAccess.Entities.BusinessIndustries.ToList();
        }
        public static BusinessIndustries SelectIndustryByID(string id)
        {
            var industry = DatabaseAccess.Entities.BusinessIndustries.First(i => i.IndustryID == id);
            return industry;
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
