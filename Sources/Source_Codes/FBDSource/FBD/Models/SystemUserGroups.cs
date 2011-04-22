using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(SystemUserGroupsMetaData))]
    public partial class SystemUserGroups
    {
        /// <summary>
        /// Select all systemGroups
        /// in the table [SystemUserGroups]
        /// </summary>
        /// <returns>List of all Groups</returns>
        public static List<SystemUserGroups> SelectUserGroups()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemUserGroups.ToList();
        }


        /// <summary>
        /// Select a single Group has a specific id = ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>A Group with id = ID</returns>
        public static SystemUserGroups SelectUserGroupByID(string id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                var group = entities.SystemUserGroups.First(i => i.GroupID == id);
                return group;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// Select a single Group has a specific id = ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <returns>A Group with id = ID</returns>
        public static SystemUserGroups SelectUserGroupByID(string id, FBDEntities entities)
        {
            try
            {
                var group = entities.SystemUserGroups.First(i => i.GroupID == id);
                return group;
            }
            catch (Exception)
            {
                
                return null;
            }
        }


        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new Group into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="group">Infor of new Group</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int AddUserGroup(SystemUserGroups group)
        {
            FBDEntities entities = new FBDEntities();
            
            entities.AddToSystemUserGroups(group);
            
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate Group (GroupID, GroupName) with ID selected in [System.UserGroups] table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="group">Infor of updated Group</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int EditUserGroup(SystemUserGroups group)
        {
            FBDEntities entities = new FBDEntities();

            var temp = SystemUserGroups.SelectUserGroupByID(group.GroupID, entities);
            temp.GroupName = group.GroupName;
            
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Group with selected ID 
        /// from the [System.UserGroups] table
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int DeleteUserGroup(string id)
        {
            FBDEntities entities = new FBDEntities();

            var group = SystemUserGroups.SelectUserGroupByID(id, entities);
            entities.DeleteObject(group);
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Check ID dupplication
        /// </summary>
        /// <param name="id"></param>
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
                bool check = entities.SystemUserGroups.Where(i => i.GroupID == id).Any();
                return check ? 1 : 0;
            }
            catch (Exception)
            {
                return 2;
            }
        }

        public class SystemUserGroupsMetaData
        {
            [DisplayName("Group ID")]
            [Required(ErrorMessage = "Group ID is required")]
            [StringLength(10)]
            public string GroupID { get; set; }

            [DisplayName("Group Name")]
            [Required(ErrorMessage = "Group Name is required")]
            [StringLength(255)]
            public string GroupName { get; set; }
        }
    }
}
