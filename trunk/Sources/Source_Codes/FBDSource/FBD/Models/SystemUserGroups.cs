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

        public static SystemUserGroups SelecUserGroupByID(string id, FBDEntities entities)
        {
            var group = entities.SystemUserGroups.First(i => i.GroupID == id);
            return group;
        }

        public static int AddUserGroup(SystemUserGroups group)
        {
            FBDEntities entities = new FBDEntities();

            try
            {
                entities.AddToSystemUserGroups(group);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static int EditUserGroup(SystemUserGroups group)
        {
            FBDEntities entities = new FBDEntities();

            try
            {
                var temp = SystemUserGroups.SelecUserGroupByID(group.GroupID, entities);
                temp.GroupName = group.GroupName;
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        public static int DeleteUserGroup(string id)
        {
            FBDEntities entities = new FBDEntities();

            try
            {
                var group = SystemUserGroups.SelecUserGroupByID(id, entities);
                entities.DeleteObject(group);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        public class SystemUserGroupsMetaData
        {
            [DisplayName("Group ID")]
            [Required(ErrorMessage = "Industry ID is required")]
            [StringLength(10)]
            public string GroupID { get; set; }

            [DisplayName("Group Name")]
            [StringLength(50)]
            public string GroupName { get; set; }
        }
    }
}
