using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace FBD.Models
{
    [MetadataType(typeof(SystemUserGroupRightMetadata))]
    public partial class SystemUserGroupsRights 
    {
        public static List<SystemUserGroupsRights> SelectUserGroupRights()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemUserGroupsRights.ToList();
        }

        public static SystemUserGroupsRights SelectUserGroupRightByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var temp = entities.SystemUserGroupsRights.First(i => i.ID == id);
            return temp;
        }

        public static bool CheckUserGroupRight()
        {
            FBDEntities entities = new FBDEntities();
            var temp = entities.SystemUserGroupsRights.First(m => (m.SystemRights.RightID == "1") && (m.SystemUserGroups.GroupID == "2"));
            if (temp == null) return false;
            return true;
        }

        public static SystemUserGroupsRights SelectUserGroupRightByID(int id, FBDEntities entities)
        {
            var temp = entities.SystemUserGroupsRights.First(i => i.ID == id);
            return temp;
        }


        public static void EditUserGroupsRight(SystemUserGroupsRights userGroupRight)
        {
            FBDEntities entities = new FBDEntities();
            
            
        }

        [Bind (Exclude="RightID")]
        public class SystemUserGroupRightMetadata
        {
            [ScaffoldColumn(false)]
            public int ID { get; set; }

            [DisplayName("Group ID")]
            [Required(ErrorMessage = "Group ID is required")]
            public string GroupID { get; set; }

            [DisplayName("Right ID")]
            [Required(ErrorMessage = "Right ID is required")]
            public string RightID { get; set; }
        }
    }
}
