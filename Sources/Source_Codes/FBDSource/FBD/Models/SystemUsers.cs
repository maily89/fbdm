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
        /// <summary>
        /// Select all Users in table System.SystemUser
        /// </summary>
        /// <returns>List of all Users</returns>
        public static List<SystemUsers> SelectUsers()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemUsers.Include("SystemBranches")
                                       .Include("SystemUserGroups")
                                       .ToList();
        }

        /// <summary>
        /// Select all Users which has specific BranchID
        /// </summary>
        /// <param name="branchID">BranchID</param>
        /// <returns>A User with BranchID = ID</returns>
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

        /// <summary>
        /// Select a single User with specific ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>A User with id = ID</returns>
        public static SystemUsers SelectUserByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var user = entities.SystemUsers.Include("SystemBranches")
                                           .Include("SystemUserGroups")
                                           .First(i => i.UserID == id);
            return user;
        }

        /// <summary>
        /// Select a single User with specific ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <returns>A User with id = ID</returns>
        public static SystemUsers SelectUserByID(string id, FBDEntities entities)
        {
            var user = entities.SystemUsers.Include("SystemBranches")
                                           .Include("SystemUserGroups")
                                           .First(i => i.UserID == id);
            return user;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new User into the Database and 
        /// by default, Password field is "Password"
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="user">Infor for new User</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int AddUser(SystemUsers user, FBDEntities entity)
        { 
            entity.AddToSystemUsers(user);
            int result = entity.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate User with ID selected 
        /// in [System.Users] table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="User">Infor for updated User</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
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

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the User with selected ID 
        /// from the [System.Users] table
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
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

        /// <summary>
        /// Reset user password to default
        /// </summary>
        /// <param name="userID">id of user</param>
        /// <returns>an integer indicates result</returns>
        public static int ResetPassword(string userID)
        {
            FBDEntities entities = new FBDEntities();

            var temp = SystemUsers.SelectUserByID(userID, entities);
            temp.Password = CommonUtilities.StringHelper.Encode("password");
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
