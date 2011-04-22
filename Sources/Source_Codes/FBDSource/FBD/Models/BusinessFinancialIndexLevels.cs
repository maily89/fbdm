﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessFinancialIndexLevelsMetaData))]
    public partial class BusinessFinancialIndexLevels
    {
        /// <summary>
        /// Select all the Levels in the table Business.FinancialIndexLevels
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>List of Financial Index Levels</returns>
        public static List<BusinessFinancialIndexLevels> SelectFinancialIndexLevels(FBDEntities FBDModel)
        {
            List<BusinessFinancialIndexLevels> lstFinancialIndexLevels = null;

            // Get the business financial index levels from entities model
            lstFinancialIndexLevels = FBDModel.BusinessFinancialIndexLevels.ToList();

            return lstFinancialIndexLevels;
        }

        /// <summary>
        /// Select the Levels in the table Business.FinancialIndexLevels with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>BusinessFinancialIndexLevels</returns>
        public static BusinessFinancialIndexLevels SelectFinancialIndexLevelsByID(Decimal id, FBDEntities FBDModel)
        {
            try
            {
                BusinessFinancialIndexLevels businessFinancialIndexLevels = null;

                // Get the business financial index from the entities model with the inputted ID
                businessFinancialIndexLevels = FBDModel.BusinessFinancialIndexLevels.First(level => level.LevelID == id);

                return businessFinancialIndexLevels;
            }
            catch 
            {

                return null;
            }
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new level into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="businessFinancialIndexLevels">A new FinancialIndexLevels information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddFinancialIndexLevels(FBDEntities FBDModel, BusinessFinancialIndexLevels businessFinancialIndexLevels)
        {
            // Add new business financial index level with the inputted information to the entities
            FBDModel.AddToBusinessFinancialIndexLevels(businessFinancialIndexLevels);

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
        /// <param name="businessFinancialIndexLevels">The financial index level to be updated</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int EditFinancialIndexLevels(FBDEntities FBDModel, BusinessFinancialIndexLevels businessFinancialIndexLevels)
        {
            // Select the financial index to be updated from database
            var temp = FBDModel.BusinessFinancialIndexLevels.First(level => 
                                            level.LevelID == businessFinancialIndexLevels.LevelID);

            // Update the financial index to the entities
            temp.Score = businessFinancialIndexLevels.Score;

            // Save changes to the database
            int result = FBDModel.SaveChanges();

            return result <= 0 ? 0 : 1;

        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Financial Index Level with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="id">ID of the Financial Index Level selected</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int DeleteFinancialIndexLevels(FBDEntities FBDModel, Decimal id)
        {
            var financialIndexLevels = FBDModel.BusinessFinancialIndexLevels.First(level => level.LevelID == id);

            // Delete business financial index from entities
            FBDModel.DeleteObject(financialIndexLevels);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class BusinessFinancialIndexLevelsMetaData
        {
            [DisplayName("Level ID")]
            [Required(ErrorMessage = "Level ID is required")]
            public Decimal LevelID { get; set; }

            [DisplayName("Score")]
            [Required(ErrorMessage = "Score is required")]
            public Decimal Score { get; set; }
        }
    }
}
