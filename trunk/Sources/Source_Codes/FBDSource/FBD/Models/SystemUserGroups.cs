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
        public static List<SystemUserGroups> SelectUserGroups()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemUserGroups.ToList();
        }

        public static SystemUserGroups SelectUserGroupByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var group = entities.SystemUserGroups.First(i => i.GroupID == id);
            return group;
        }

        public static SystemUserGroups SelectUserGroupByID(string id, FBDEntities entities)
        {
            var group = entities.SystemUserGroups.First(i => i.GroupID == id);
            return group;
        }

        public static int AddUserGroup(SystemUserGroups group)
        {
            FBDEntities entities = new FBDEntities();
            
            entities.AddToSystemUserGroups(group);
            
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static int EditUserGroup(SystemUserGroups group)
        {
            FBDEntities entities = new FBDEntities();

            var temp = SystemUserGroups.SelectUserGroupByID(group.GroupID, entities);
            temp.GroupName = group.GroupName;
            
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }

        public static int DeleteUserGroup(string id)
        {
            FBDEntities entities = new FBDEntities();

            var group = SystemUserGroups.SelectUserGroupByID(id, entities);
            entities.DeleteObject(group);
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }

        public class SystemUserGroupsMetaData
        {
            [DisplayName("Group ID")]
            [Required(ErrorMessage = "Group ID is required")]
            [StringLength(10)]
            public string GroupID { get; set; }

            [DisplayName("Group Name")]
            [StringLength(50)]
            public string GroupName { get; set; }
        }
    }
}
