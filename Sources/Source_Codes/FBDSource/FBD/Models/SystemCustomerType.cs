using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(SystemCustomerTypesMetadata))]
    public partial class SystemCustomerTypes
    {
        /// <summary>
        /// Select all Customer types (TypeID, TypeName) 
        /// in the table [System.CustomerTypes]
        /// </summary>
        /// <returns>List of all SystemCustomerTypes</returns>
        public static List<SystemCustomerTypes> SelectTypes()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemCustomerTypes.ToList();
        }

        /// <summary>
        /// Select Customer Type (TypeID, TypeName) 
        /// in the table [System.CustomerTypes] with input ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>A Customer type with id = ID</returns>
        public static SystemCustomerTypes SelectTypeByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemCustomerTypes.First(i => i.TypeID.Equals(id));
        }

        /// <summary>
        /// Select Customer Type (TypeID, TypeName) 
        /// in the table [System.CustomerTypes] with input ID, entities
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <returns>A customer type with id = ID</returns>
        public static SystemCustomerTypes SelectTypeByID(string id, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(id) || entities==null) return null;
            return entities.SystemCustomerTypes.First(i => i.TypeID.Equals(id));
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new type into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="type">Infor of the new type</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int AddType(SystemCustomerTypes type)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToSystemCustomerTypes(type);
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate Type (TypeID, TypeName) 
        /// with ID selected in [System.CustomerTypes] table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="type">Infor of the edited Type</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int EditType(SystemCustomerTypes type)
        {
            FBDEntities entities = new FBDEntities();
            var temp = entities.SystemCustomerTypes.First(i => i.TypeID.Equals(type.TypeID));
            temp.TypeName = type.TypeName;
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Type with selected ID 
        /// from the [System.CustomerTypes] table
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int DeleteType(string id)
        {
            FBDEntities entities = new FBDEntities();
            var temp = entities.SystemCustomerTypes.First(i => i.TypeID.Equals(id));
            entities.DeleteObject(temp);
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Check ID dupplication
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// 1: if true (dupplication is occuring)
        /// 0: if false (no dupplication, the ID is available
        /// 2: if there is any exception
        /// </returns>
        public static int IsIDExist(string id)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                bool check = entities.SystemCustomerTypes.Where(i => i.TypeID == id).Any();
                return check ? 1 : 0;
            }
            catch (Exception)
            {
                return 2;
            }
        }

        public class SystemCustomerTypesMetadata
        {
            [DisplayName("Type ID")]
            [Required(ErrorMessage = "Type ID is required")]
            [StringLength(10)]
            public string TypeID { get; set; }

            [DisplayName("Type Name")]
            [Required(ErrorMessage = "Type name is required")]
            [StringLength(255)]
            public string TypeName { get; set; }
        }
    }
}