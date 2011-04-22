using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessNonFinancialIndexLevelsMetaData))]
    public partial class BusinessNonFinancialIndexLevels
    {
        /// <summary>
        /// Select all the Levels in the table Business.NonFinancialIndexLevels
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>List of Non-Financial Index Levels</returns>
        public static List<BusinessNonFinancialIndexLevels> SelectNonFinancialIndexLevels(FBDEntities FBDModel)
        {
            List<BusinessNonFinancialIndexLevels> lstNonFinancialIndexLevels = null;

            // Get the business non-financial index levels from entities model
            lstNonFinancialIndexLevels = FBDModel.BusinessNonFinancialIndexLevels.ToList();

            return lstNonFinancialIndexLevels;
        }

        /// <summary>
        /// Select the Levels in the table Business.NonFinancialIndexLevels with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>BusinessNonFinancialIndexLevels</returns>
        public static BusinessNonFinancialIndexLevels SelectNonFinancialIndexLevelsByID(Decimal id, FBDEntities FBDModel)
        {
            try
            {
                BusinessNonFinancialIndexLevels businessNonFinancialIndexLevels = null;

                // Get the business non-financial index from the entities model with the inputted ID
                businessNonFinancialIndexLevels = FBDModel.BusinessNonFinancialIndexLevels.First(level => level.LevelID == id);

                return businessNonFinancialIndexLevels;
            }
            catch (Exception)
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
        /// <param name="businessNonFinancialIndexLevels">A new Non-FinancialIndexLevels information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddNonFinancialIndexLevels(FBDEntities FBDModel, BusinessNonFinancialIndexLevels businessNonFinancialIndexLevels)
        {
            // Add new business non-financial index level with the inputted information to the entities
            FBDModel.AddToBusinessNonFinancialIndexLevels(businessNonFinancialIndexLevels);

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
        /// <param name="businessNonFinancialIndexLevels">The financial index level to be updated</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int EditNonFinancialIndexLevels(FBDEntities FBDModel, BusinessNonFinancialIndexLevels businessNonFinancialIndexLevels)
        {
            // Select the financial index to be updated from database
            var temp = FBDModel.BusinessNonFinancialIndexLevels.First(level =>
                                            level.LevelID == businessNonFinancialIndexLevels.LevelID);

            // Update the financial index to the entities
            temp.Score = businessNonFinancialIndexLevels.Score;

            // Save changes to the database
            int result = FBDModel.SaveChanges();

            return result <= 0 ? 0 : 1;

        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Non-Financial Index Level with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="id">ID of the Non-Financial Index Level selected</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int DeleteNonFinancialIndexLevels(FBDEntities FBDModel, Decimal id)
        {
            var nonFinancialIndexLevels = FBDModel.BusinessNonFinancialIndexLevels.First(level => level.LevelID == id);

            // Delete business non-financial index from entities
            FBDModel.DeleteObject(nonFinancialIndexLevels);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class BusinessNonFinancialIndexLevelsMetaData
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
