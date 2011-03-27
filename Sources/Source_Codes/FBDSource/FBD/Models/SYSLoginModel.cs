using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType(typeof(SYSLoginModelMetaData))]
    public class SYSLoginModel
    {
        /// <summary>
        /// ID of the user
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Verify the user login successfully or not
        /// </summary>
        /// <param name="userID">input userId</param>
        /// <param name="password">input password</param>
        /// <returns>true: success
        ///          false: fail</returns>
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

                if (!user.Password.Equals(StringHelper.Encode(password)))
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
