using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessTypeMetaData))] 
    public partial class BusinessTypes
    {

        public static List<BusinessTypes> SelectTypes()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessTypes.ToList();
        }
        public static BusinessTypes SelectTypeByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var type = entities.BusinessTypes.First(i => i.TypeID == id);
            return type;
        }

        public static BusinessTypes SelectTypeByID(string id,FBDEntities entities)
        {
            if (string.IsNullOrEmpty(id) || entities == null) return null;
            var type = entities.BusinessTypes.First(i => i.TypeID == id);
            return type;
        }

        public static int DeleteType(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;
            FBDEntities entities = new FBDEntities();
            var type = BusinessTypes.SelectTypeByID(id,entities);
            entities.DeleteObject(type);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public static int EditType(BusinessTypes type)
        {
            if (type == null) return 0;
            FBDEntities entities = new FBDEntities();
            var temp = BusinessTypes.SelectTypeByID(type.TypeID,entities);
            temp.TypeName = type.TypeName;
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public static int AddType(BusinessTypes type)
        {
            if (type == null) return 0;
            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessTypes(type);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        public class BusinessTypeMetaData
        {
            [DisplayName("Type ID")]
            [Required(ErrorMessage="Type ID is required")]
            [StringLength(4)]
            public string TypeID { get; set; }

            [DisplayName("Type Name")]
            [Required(ErrorMessage="Type Name is required")]
            [StringLength(255)]
            public string TypeName { get; set; }
        }
    }
}
