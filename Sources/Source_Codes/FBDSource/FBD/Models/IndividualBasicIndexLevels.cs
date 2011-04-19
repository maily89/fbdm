using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    //to do: check levels cover all ranks
    [MetadataType(typeof(IndividualBasicIndexLevelsMetaData))]
    public partial class IndividualBasicIndexLevels
    {

        /// <summary>
        /// Select all the Levels in the table IndividualBasicIndexLevels
        /// </summary>
        /// <returns>List of Basic Index Levels</returns>
        public static List<IndividualBasicIndexLevels> SelectBasicIndexLevels()
        {
            FBDEntities FBDModel = new FBDEntities();

            List<IndividualBasicIndexLevels> lstBasicIndexLevels = null;

            // Get the business Basic index levels from entities model
            lstBasicIndexLevels = FBDModel.IndividualBasicIndexLevels.ToList();

            return lstBasicIndexLevels;
        }

        /// <summary>
        /// Select the Levels in the table IndividualBasicIndexLevels with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>IndividualBasicIndexLevels</returns>
        public static IndividualBasicIndexLevels SelectBasicIndexLevelsByID(Decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();

            IndividualBasicIndexLevels IndividualBasicIndexLevels = null;

            // Get the business Basic index from the entities model with the inputted ID
            IndividualBasicIndexLevels = FBDModel.IndividualBasicIndexLevels.First(level => level.LevelID.Equals(id));

            return IndividualBasicIndexLevels;
        }

        public static IndividualBasicIndexLevels SelectBasicIndexLevelsByID(Decimal id,FBDEntities FBDModel)
        {
            IndividualBasicIndexLevels IndividualBasicIndexLevels = null;

            // Get the business Basic index from the entities model with the inputted ID
            IndividualBasicIndexLevels = FBDModel.IndividualBasicIndexLevels.First(level => level.LevelID.Equals(id));

            return IndividualBasicIndexLevels;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new level into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="IndividualBasicIndexLevels">A new BasicIndexLevels information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddBasicIndexLevels(IndividualBasicIndexLevels IndividualBasicIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Add new business Basic index level with the inputted information to the entities
            FBDModel.AddToIndividualBasicIndexLevels(IndividualBasicIndexLevels);

            // Save changes to the Database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Update appropriate data to table in DB
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="IndividualBasicIndexLevels">The Basic index level to be updated</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int EditBasicIndexLevels(IndividualBasicIndexLevels IndividualBasicIndexLevels)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Select the Basic index to be updated from database
            var temp = SelectBasicIndexLevelsByID(IndividualBasicIndexLevels.LevelID,FBDModel);//FBDModel.IndividualBasicIndexLevels.First(level =>
                                            //level.LevelID.Equals(IndividualBasicIndexLevels.LevelID));

            // Update the Basic index to the entities
            temp.Score = IndividualBasicIndexLevels.Score;

            // Save changes to the database
            int result = FBDModel.SaveChanges();

            return result <= 0 ? 0 : 1;

        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Basic Index Level with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID of the Basic Index Level selected</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int DeleteBasicIndexLevels(Decimal id)
        {
            FBDEntities FBDModel = new FBDEntities();
            var BasicIndexLevels = SelectBasicIndexLevelsByID(id, FBDModel);//FBDModel.IndividualBasicIndexLevels.First(level => level.LevelID.Equals(id));

            // Delete business Basic index from entities
            FBDModel.DeleteObject(BasicIndexLevels);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class IndividualBasicIndexLevelsMetaData
        {
            [DisplayName("Level ID")]
            [Required(ErrorMessage = "Level ID is required")]
            [RegularExpression("[0-9]{1,10}",ErrorMessage="Level ID mus be a numberic and have at most 10 characters")]
            public decimal LevelID { get; set; }

            [DisplayName("Score")]
            [Required(ErrorMessage = "Score is required")]
            public decimal Score { get; set; }
        }
    }
}
