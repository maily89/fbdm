using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    public class SYSChangePassModel
    {
        /// <summary>
        /// ID of the user
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Old password of user
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// User type new password
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// User confirm their new password, it must be matched with new password above
        /// </summary>
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// Verify the change to be successful or not
        /// </summary>
        /// <param name="userID">input userID</param>
        /// <param name="oldPass">input old password</param>
        /// <param name="newPass">input new password</param>
        /// <param name="confirmNewPass">confirm the new password</param>
        /// <returns>true: success
        ///          false: fail</returns>
        public static bool VerifyChangePass(string userID, string oldPass, string newPass, string confirmNewPass)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(oldPass) 
                            || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmNewPass))
            {
                return false;
            }

            if (!SYSLoginModel.VerifyLogin(userID, oldPass))
            {
                return false;                
            }

            if (!newPass.Equals(confirmNewPass))
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Update the user's password
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="password">password</param>
        /// <returns>an integer indicates result</returns>
        public static int ChangePass(string userID, string password)
        {
            FBDEntities FBDModel = new FBDEntities();

            SystemUsers user = null;
            try
            {
                user = SystemUsers.SelectUserByID(userID, FBDModel);
            }
            catch (Exception)
            {
                return 0;    
            }

            // NEED TO ENCODE HERE
            user.Password = password;
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class SYSChangePassModelMetaData
        {
            [DisplayName("User ID")]
            [Required(ErrorMessage = "User ID is required")]
            [StringLength(10)]
            public string UserID { get; set; }

            [DisplayName("Old Password")]
            [Required(ErrorMessage = "Old Password is required")]
            [StringLength(20)]
            public string OldPassword { get; set; }

            [DisplayName("New Password")]
            [Required(ErrorMessage = "New Password is required")]
            [StringLength(20)]
            public string NewPassword { get; set; }

            [DisplayName("Confirm Password")]
            [Required(ErrorMessage = "Confirm Password is required")]
            [StringLength(20)]
            public string ConfirmNewPassword { get; set; }
        }
    }
}
