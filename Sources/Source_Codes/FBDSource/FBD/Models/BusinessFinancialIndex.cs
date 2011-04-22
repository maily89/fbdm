﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessFinancialIndexMetaData))]
    public partial class BusinessFinancialIndex
    {
        /// <summary>
        /// Select all the Financial Index in the table Business.FinancialIndex
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>List of Financial Index</returns>
        public static List<BusinessFinancialIndex> SelectFinancialIndex(FBDEntities FBDModel)
        {
            List<BusinessFinancialIndex> lstFinancialIndex = null;

            // Get the business financial index from entities model
            lstFinancialIndex = FBDModel.BusinessFinancialIndex.ToList();
            
            return lstFinancialIndex;
        }

        /// <summary>
        /// Select the Financial Index in the table Business.FinancialIndex with input ID
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="id">string ID</param>
        /// <returns>BusinessFinancialIndex</returns>
        public static BusinessFinancialIndex SelectFinancialIndexByID(FBDEntities FBDModel, string id)
        {
            try
            {
                BusinessFinancialIndex businessFinancialIndex = null;

                // Get the business financial index from the entities model with the inputted ID
                businessFinancialIndex = FBDModel.BusinessFinancialIndex.First(index => index.IndexID.Equals(id));

                return businessFinancialIndex;
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// Select all the financial indexes where leaf index is true, used for business handling proportion
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>All the financial leaf indexes</returns>
        public static List<BusinessFinancialIndex> SelectFinancialLeafIndex(FBDEntities FBDModel)
        {
            List<BusinessFinancialIndex> lstBusinessFinancialLeafIndexes = FBDModel
                                                                           .BusinessFinancialIndex
                                                                           .Where(index => index.LeafIndex == true)
                                                                           .ToList();

            return lstBusinessFinancialLeafIndexes;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new index into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="businessFinancialIndex">A new FinancialIndex information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddFinancialIndex(FBDEntities FBDModel, BusinessFinancialIndex businessFinancialIndex)
        {
            // Add new business financial index with the inputted information to the entities
            FBDModel.AddToBusinessFinancialIndex(businessFinancialIndex);

            // Save changes to the Database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
            
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate data to table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="businessFinancialIndex">The financial index to be updated</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int EditFinancialIndex(FBDEntities FBDModel, BusinessFinancialIndex businessFinancialIndex)
        {
            // Select the financial index to be updated from database
            try
            {

                var temp = FBDModel.BusinessFinancialIndex.First(index => index.IndexID.Equals(businessFinancialIndex.IndexID));

                // Update the financial index to the entities
                temp.IndexName = businessFinancialIndex.IndexName;
                temp.Unit = businessFinancialIndex.Unit;
                temp.Formula = businessFinancialIndex.Formula;
                temp.ValueType = businessFinancialIndex.ValueType;
                temp.LeafIndex = businessFinancialIndex.LeafIndex;

                // Save changes to the database
                int result = FBDModel.SaveChanges();

                return result <= 0 ? 0 : 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Financial Index with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="id">ID of the Financial Index selected</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int DeleteFinancialIndex(FBDEntities FBDModel, string id)
        {
            var financialIndex = FBDModel.BusinessFinancialIndex.First(index => index.IndexID.Equals(id));

            // Delete business financial index from entities
            FBDModel.DeleteObject(financialIndex);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class BusinessFinancialIndexMetaData
        {
            [DisplayName("Index ID")]
            [Required(ErrorMessage = "Index ID is required")]
            [StringLength(20)]
            public string IndexID { get; set; }

            [DisplayName("Index Name")]
            [Required(ErrorMessage = "Index Name is required")]
            [StringLength(255)]
            public string IndexName { get; set; }

            [DisplayName("Unit")]
            [StringLength(50)]
            public string Unit { get; set; }

            [DisplayName("Formula")]
            [StringLength(255)]
            public string Formula { get; set; }
        }
    }
}
