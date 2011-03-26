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
        public string UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

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
