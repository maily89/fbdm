﻿using System;
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
        public static List<SystemBranches> SelectBranches()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemBranches.ToList();
        }

        public static SystemBranches SelectBranchByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var Branch = entities.SystemBranches.First(i => i.BranchID == id);
            return Branch;
        }

        public static SystemBranches SelectBranchByID(string id, FBDEntities entities)
        {
            var Branch = entities.SystemBranches.First(i => i.BranchID == id);
            return Branch;
        }

        public static int AddBranch(SystemBranches branch)
        {
            FBDEntities entities = new FBDEntities();

            entities.AddToSystemBranches(branch);

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Branch"></param>
        /// <returns></returns>
        public static int EditBranch(SystemBranches branch)
        {
            FBDEntities entities = new FBDEntities();

            var temp = SystemBranches.SelectBranchByID(branch.BranchID, entities);
            temp.BranchName = branch.BranchName;
            temp.Active = branch.Active;
            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

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
            [StringLength(50)]
            public string BranchName { get; set; }

            //[DisplayName("Active")]
            //[StringLength(1)]
            //public string Active { get; set; }
        }
    }
}
