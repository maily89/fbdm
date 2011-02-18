using System;
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
        /// <returns>List of Financial Index Levels</returns>
        public static List<BusinessFinancialIndexLevels> SelectFinancialIndexLevels()
        {
            FBDEntities FBDModel = new FBDEntities();

            List<BusinessFinancialIndexLevels> lstFinancialIndexLevels = null;

            // Get the business financial index levels from entities model
            lstFinancialIndexLevels = FBDModel.BusinessFinancialIndexLevels.ToList();

            return lstFinancialIndexLevels;
        }

        /// <summary>
        /// Select the Levels in the table Business.FinancialIndexLevels with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>BusinessFinancialIndexLevels</returns>
        public static BusinessFinancialIndexLevels SelectFinancialIndexLevelsByID(Decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();

            BusinessFinancialIndexLevels businessFinancialIndexLevels = null;

            try
            {
                // Get the business financial index from the entities model with the inputted ID
                businessFinancialIndexLevels = FBDModel.BusinessFinancialIndexLevels.First(level => level.LevelID == id);
            }
            catch (Exception)
            {
                return null;
            }
            return businessFinancialIndexLevels;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new level into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="businessFinancialIndexLevels">A new FinancialIndexLevels information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddFinancialIndexLevels(BusinessFinancialIndexLevels businessFinancialIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // Add new business financial index level with the inputted information to the entities
                FBDModel.AddToBusinessFinancialIndexLevels(businessFinancialIndexLevels);

                // Save changes to the Database
                FBDModel.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate data to table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="businessFinancialIndexLevels">The financial index level to be updated</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int EditFinancialIndexLevels(BusinessFinancialIndexLevels businessFinancialIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            try
            {
                // Select the financial index to be updated from database
                var temp = FBDModel.BusinessFinancialIndexLevels.First(level => 
                                                level.LevelID == businessFinancialIndexLevels.LevelID);

                // Update the financial index to the entities
                temp.Score = businessFinancialIndexLevels.Score;

                // Save changes to the database
                FBDModel.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Financial Index Level with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID of the Financial Index Level selected</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int DeleteFinancialIndexLevels(Decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();
            try
            {
                var financialIndexLevels = FBDModel.BusinessFinancialIndexLevels.First(level => level.LevelID == id);

                // Delete business financial index from entities
                FBDModel.DeleteObject(financialIndexLevels);

                // Save changes to the database
                FBDModel.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
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
