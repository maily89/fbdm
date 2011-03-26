using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(SYSLoginModelMetaData))]
    public class SYSLoginModel
    {
        public string UserID { get; set; }
        public string Password { get; set; }

        public static bool VerifyLogin(string userID, string password)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            try
            {
                var user = SystemUsers.SelectUserByID(userID);

                if (user == null)
                {
                    return false;
                }

                // NEED TO DECODE PASSWORD HERE
                if (!user.Password.Equals(password))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public class SYSLoginModelMetaData
        {
            [DisplayName("User ID")]
            [Required(ErrorMessage = "User ID is required")]
            [StringLength(10)]
            public string UserID { get; set; }
            
            [DisplayName("Password")]
            [Required(ErrorMessage = "Password is required")]
            [StringLength(20)]
            public string Password { get; set; }
        }
    }
}
