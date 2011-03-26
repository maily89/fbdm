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
            return entities.SystemUsers.Include("SystemBranches")
                                       .Include("SystemUserGroups")
                                       .ToList();
        }

        public static List<SystemUsers> SelectUsersByBranch(string branchID)
        {
            FBDEntities entities = new FBDEntities();
            List<SystemUsers> lstUsersByBranch = entities.SystemUsers.Include("SystemBranches")
                                                                     .Include("SystemUserGroups")
                                                                     .Where(i => i.SystemBranches
                                                                                 .BranchID.Equals(branchID))
                                                                     .ToList();
            return lstUsersByBranch;
        }

        public static SystemUsers SelectUserByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var user = entities.SystemUsers.Include("SystemBranches")
                                           .Include("SystemUserGroups")
                                           .First(i => i.UserID == id);
            return user;
        }

        public static SystemUsers SelectUserByID(string id, FBDEntities entities)
        {
            var user = entities.SystemUsers.Include("SystemBranches")
                                           .Include("SystemUserGroups")
                                           .First(i => i.UserID == id);
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
                bool check = entities.SystemUsers.Where(i => i.UserID == id).Any();
                return check ? 1 : 0;
            }
            catch (Exception)
            {
                return 2;
            }
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
