using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(SystemUsersMetaData))]
    public partial class SystemUsers
    {
        public static List<SystemUsers> SelectUsers()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemUsers.Include("SystemBranches").Include("SystemUserGroups").ToList();
        }

        public static SystemUsers SelectUserByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var user = entities.SystemUsers.First(i => i.UserID == id);
            return user;
        }

        public static SystemUsers SelectUserByID(string id, FBDEntities entities)
        {
            var user = entities.SystemUsers.First(i => i.UserID == id);
            return user;
        }

        public static int AddUser(SystemUsers user)
        {
            FBDEntities entities = new FBDEntities();

            entities.AddToSystemUsers(user);

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        public static int AddUser(SystemUsers user, FBDEntities entity)
        { 
            entity.AddToSystemUsers(user);
            int result = entity.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static int EditUser(SystemUsers user)
        {
            FBDEntities entities = new FBDEntities();

            var temp = SystemUsers.SelectUserByID(user.UserID, entities);
            temp.SystemUserGroups = SystemUserGroups.SelectUserGroupByID(user.SystemUserGroups.GroupID, entities);
            temp.SystemBranches = SystemBranches.SelectBranchByID(user.SystemBranches.BranchID, entities);           
            temp.FullName = user.FullName;
            temp.Password = user.Password;
            temp.Status = user.Status;
            temp.CreditDepartment = user.CreditDepartment;
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        public static int DeleteUser(string id)
        {
            FBDEntities entities = new FBDEntities();

            var user = SystemUsers.SelectUserByID(id, entities);
            entities.DeleteObject(user);
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        public class SystemUsersMetaData
        {
            [DisplayName("User ID")]
            [Required(ErrorMessage = "User ID is required")]
            [StringLength(10)]
            public string UserID { get; set; }

            [DisplayName("Full Name")]
            [StringLength(50)]
            public string FullName { get; set; }

            [DisplayName("Password")]
            [Required(ErrorMessage = "Password is required")]
            [StringLength(50)]
            public string Password { get; set; }

            [DisplayName("Status")]
            [StringLength(1)]
            public string Status { get; set; }

            [DisplayName("Credit Department")]
            [StringLength(50)]
            public string CreditDepartment { get; set; }
        }
    }
}
