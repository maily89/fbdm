using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace FBD.Models
{
    public class SystemUserGroupsRights
    {
        public static List<SystemUserGroupsRights> SelectUserGroupRights()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemUserGroupsRights.ToList();
        }

        public static SystemUserGroupsRights SelectUserGroupRightByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemUserGroupsRights.First(i => i.ID == id);
        }

        public static SystemUserGroupsRights SelectUserGroupRightByID(int id, FBDEntities entities)
        {
            return entities.SystemUserGroupsRights.First(i => i.ID == id);
        }

        public static int AddUserGroupRight(SystemUserGroupsRights userGroupRight, FBDEntities entities)
        {
            entities.AddToSystemUserGroupsRights(userGroupRight);
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        public static int EditUserGroupRight(SystemUserGroupsRights userGroupRight)
        {
            FBDEntities entities = new FBDEntities();
            var temp = SystemUserGroupsRights.SelectUserGroupRightByID(userGroupRight.ID, entities);
            
        }

        [Bind (Exclude="ID")]
        public class SystemUserGroupRightMetadata
        {
            [ScaffoldColumn(false)]
            public int ID { get; set; }
            
            [DisplayName("Group ID")]
            [Required(ErrorMessage="Group ID is required")]
            [StringLength()]
            public s
        }
    }
}
