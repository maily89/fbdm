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
            
            var type = entities.BusinessTypes.First(i => i.TypeID == id);
            return type;
        }

        public static void DeleteType(string id)
        {
            FBDEntities entities = new FBDEntities();
            var type = BusinessTypes.SelectTypeByID(id,entities);
            entities.DeleteObject(type);
            entities.SaveChanges();
        }

        public static void EditType(BusinessTypes type)
        {
            FBDEntities entities = new FBDEntities();
            var temp = BusinessTypes.SelectTypeByID(type.TypeID,entities);
            temp.TypeName = type.TypeName;
            entities.SaveChanges();
        }

        public static void AddType(BusinessTypes type)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessTypes(type);
            entities.SaveChanges();
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
