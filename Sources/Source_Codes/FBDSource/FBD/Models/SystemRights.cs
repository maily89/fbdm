using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    public partial class SystemRights
    {
        /// <summary>
        /// Select all the Rights (RightID, Right) 
        /// in the table [System.Rights]
        /// </summary>
        /// <returns></returns>
        public static List<SystemRights> SelectRights()
        {
            FBDEntities entities = new FBDEntities();
            List<SystemRights> lstRights = null;
            try
            {
                lstRights = entities.SystemRights.ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return lstRights;
        }

        /// <summary>
        /// Select the Right (RightID, Right) 
        /// in the table [System.Rights] with input ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SystemRights SelectRightsByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            SystemRights right = null;
            try
            {
                //select 1 Right by [id] param
                right = entities.SystemRights.First(i => i.RightID.Equals(id));
            }
            catch (Exception)
            {
                return null;
            }
            return right;
        }
        
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new Right into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="right">contains information of the new right</param>
        /// <returns>If successful, return 1
        /// otherwise return 0</returns>
        public static int AddRight(SystemRights right)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                entities.AddToSystemRights(right);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate Right (RightID, Right) 
        /// with ID selected in [System.Rights] table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="right">contain new information of the right</param>
        /// <returns>success: return 1 
        /// otherwise return 0</returns>
        public static int EditRight(SystemRights right)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                var temp = entities.SystemRights.First(i => i.RightID.Equals(right.RightID));
                temp.RightName = right.RightName;
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Right with selected ID from the [System.Rights] table
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id"></param>
        /// <returns>success: return 1 
        /// otherwise return 0</returns>
        public static int DeleteRight(string id)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                var temp = entities.SystemRights.First(i => i.RightID.Equals(id));
                entities.DeleteObject(temp);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        public class SystemRightsMetadata
        {
            [DisplayName("RightID")]
            [Required(ErrorMessage = "Right ID is required")]
            [StringLength(20)]
            public string IndexID { get; set; }

            [DisplayName("Right")]
            [Required(ErrorMessage = "Right is required")]
            [StringLength(255)]
            public string IndexName { get; set; }
        }
    }
}