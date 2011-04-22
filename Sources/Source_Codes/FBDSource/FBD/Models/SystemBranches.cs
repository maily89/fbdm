using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType(typeof(SystemBranchesMetaData))]
    public partial class SystemBranches
    {
        /// <summary>
        /// Select all Branches 
        /// in the table [System.Branches]
        /// </summary>
        /// <returns>List of all branches</returns>
        public static List<SystemBranches> SelectBranches()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemBranches.ToList();
        }

        /// <summary>
        /// Select a single Branch with specific ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>A Branch with id = ID</returns>
        public static SystemBranches SelectBranchByID(string id)
        {
            try
            {
                FBDEntities entities = new FBDEntities();
                var Branch = entities.SystemBranches.First(i => i.BranchID == id);
                return Branch;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// Select a single Branch with specific ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="entities">The Model of Entities Framework</param>
        /// <returns>A Branch with id = ID</returns>
        public static SystemBranches SelectBranchByID(string id, FBDEntities entities)
        {
            try
            {
                var Branch = entities.SystemBranches.First(i => i.BranchID == id);
                return Branch;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new Branch into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="branch">Infor of new Branch</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int AddBranch(SystemBranches branch)
        {
            FBDEntities entities = new FBDEntities();

            entities.AddToSystemBranches(branch);

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate Branch with ID 
        /// in [System.Branches] table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="Branch">Infor of updated Branch</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int EditBranch(SystemBranches branch)
        {
            FBDEntities entities = new FBDEntities();

            var temp = SystemBranches.SelectBranchByID(branch.BranchID, entities);
            temp.BranchName = branch.BranchName;
            temp.Active = branch.Active;
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Branch with selected ID 
        /// from [System.Branches] table
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int DeleteBranch(string id)
        {
            FBDEntities entities = new FBDEntities();

            var branch = SystemBranches.SelectBranchByID(id, entities);
            entities.DeleteObject(branch);
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
                bool check = entities.SystemBranches.Where(i => i.BranchID == id).Any();
                return check ? 1 : 0;
            }
            catch (Exception)
            {
                return 2;
            }
        }


        public class SystemBranchesMetaData
        {
            [DisplayName("Branch ID")]
            [Required(ErrorMessage = "Branch ID is required")]
            [StringLength(10)]
            public string BranchID { get; set; }

            [DisplayName("Branch Name")]
            [Required(ErrorMessage = "Branch Name is required")]
            [StringLength(255)]
            public string BranchName { get; set; }
        }
    }
}
