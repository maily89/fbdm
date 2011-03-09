using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    //to do: check levels cover all ranks
    [MetadataType(typeof(IndividualCollateralIndexLevelsMetaData))]
    public partial class IndividualCollateralIndexLevels
    {

        /// <summary>
        /// Select all the Levels in the table IndividualCollateralIndexLevels
        /// </summary>
        /// <returns>List of Collateral Index Levels</returns>
        public static List<IndividualCollateralIndexLevels> SelectCollateralIndexLevels()
        {
            FBDEntities FBDModel = new FBDEntities();

            List<IndividualCollateralIndexLevels> lstCollateralIndexLevels = null;

            // Get the business Collateral Index levels from entities model
            lstCollateralIndexLevels = FBDModel.IndividualCollateralIndexLevels.ToList();

            return lstCollateralIndexLevels;
        }

        /// <summary>
        /// Select the Levels in the table IndividualCollateralIndexLevels with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>IndividualCollateralIndexLevels</returns>
        public static IndividualCollateralIndexLevels SelectCollateralIndexLevelsByID(Decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();

            IndividualCollateralIndexLevels IndividualCollateralIndexLevels = null;

            // Get the business Collateral Index from the entities model with the inputted ID
            IndividualCollateralIndexLevels = FBDModel.IndividualCollateralIndexLevels.First(level => level.LevelID == id);

            return IndividualCollateralIndexLevels;
        }

        public static IndividualCollateralIndexLevels SelectCollateralIndexLevelsByID(Decimal id, FBDEntities FBDModel)
        {
            IndividualCollateralIndexLevels IndividualCollateralIndexLevels = null;

            // Get the business Collateral Index from the entities model with the inputted ID
            IndividualCollateralIndexLevels = FBDModel.IndividualCollateralIndexLevels.First(level => level.LevelID == id);

            return IndividualCollateralIndexLevels;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new level into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="IndividualCollateralIndexLevels">A new CollateralIndexLevels information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddCollateralIndexLevels(IndividualCollateralIndexLevels IndividualCollateralIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Add new business Collateral Index level with the inputted information to the entities
            FBDModel.AddToIndividualCollateralIndexLevels(IndividualCollateralIndexLevels);

            // Save changes to the Database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate data to table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="IndividualCollateralIndexLevels">The Collateral Index level to be updated</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int EditCollateralIndexLevels(IndividualCollateralIndexLevels IndividualCollateralIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Select the Collateral Index to be updated from database
            var temp = FBDModel.IndividualCollateralIndexLevels.First(level =>
                                            level.LevelID == IndividualCollateralIndexLevels.LevelID);

            // Update the Collateral Index to the entities
            temp.Score = IndividualCollateralIndexLevels.Score;

            // Save changes to the database
            int result = FBDModel.SaveChanges();

            return result <= 0 ? 0 : 1;

        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Collateral Index Level with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID of the Collateral Index Level selected</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int DeleteCollateralIndexLevels(Decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();
            var CollateralIndexLevels = FBDModel.IndividualCollateralIndexLevels.First(level => level.LevelID == id);

            // Delete business Collateral Index from entities
            FBDModel.DeleteObject(CollateralIndexLevels);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class IndividualCollateralIndexLevelsMetaData
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
