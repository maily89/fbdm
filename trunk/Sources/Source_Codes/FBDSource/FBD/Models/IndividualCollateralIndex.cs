using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualCollateralIndexMetaData))]
    public partial class IndividualCollateralIndex : IIndex
    {
        /// <summary>
        /// Select all the Individual Collateral Index in the table Business.CollateralIndex
        /// </summary>
        /// <returns>List of Individual Collateral Index</returns>
        public static List<IndividualCollateralIndex> SelectCollateralIndex()
        {
            FBDEntities FBDModel = new FBDEntities();

            List<IndividualCollateralIndex> lstCollateralIndex = null;
            lstCollateralIndex = FBDModel.IndividualCollateralIndex.ToList();
            return lstCollateralIndex;
        }

        /// <summary>
        /// Select all the Individual Collateral Index in the table Business.CollateralIndex
        /// </summary>
        /// <returns>List of Individual Collateral Index</returns>
        public static List<IndividualCollateralIndex> SelectCollateralIndex(FBDEntities FBDModel)
        {
            

            List<IndividualCollateralIndex> lstCollateralIndex = null;
            lstCollateralIndex = FBDModel.IndividualCollateralIndex.ToList();
            return lstCollateralIndex;
        }

        /// <summary>
        /// Select all the Individual Collateral Index in the table Business.CollateralIndex which is leaf
        /// </summary>
        /// <returns>List of Individual Collateral Index</returns>
        public static List<IndividualCollateralIndex> SelectCollateralLeafIndex(FBDEntities FBDModel)
        {


            List<IndividualCollateralIndex> lstCollateralIndex = null;
            lstCollateralIndex = FBDModel.IndividualCollateralIndex.Where(c=>c.LeafIndex).ToList();
            return lstCollateralIndex;
        }

        /// <summary>
        /// Select the Individual Collateral Index in the table Business.CollateralIndex with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>IndividualCollateralIndex</returns>
        public static IndividualCollateralIndex SelectCollateralIndexByID(string id)
        {
            FBDEntities FBDModel = new FBDEntities();

            IndividualCollateralIndex IndividualCollateralIndex = null;

            // Get the business Individual Collateral Index from the entities model with the inputted ID
            IndividualCollateralIndex = FBDModel.IndividualCollateralIndex.First(index => index.IndexID.Equals(id));

            return IndividualCollateralIndex;
        }

        public static IndividualCollateralIndex SelectCollateralIndexByID(string id, FBDEntities FBDModel)
        {
            //FBDEntities FBDModel = new FBDEntities();

            IndividualCollateralIndex IndividualCollateralIndex = null;

            // Get the business Individual Collateral Index from the entities model with the inputted ID
            IndividualCollateralIndex = FBDModel.IndividualCollateralIndex.First(index => index.IndexID.Equals(id));
            return IndividualCollateralIndex;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new index into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="IndividualCollateralIndex">A new CollateralIndex information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddCollateralIndex(IndividualCollateralIndex IndividualCollateralIndex)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Add new business Individual Collateral Index with the inputted information to the entities
            FBDModel.AddToIndividualCollateralIndex(IndividualCollateralIndex);

            // Save changes to the Database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;

        }

        public static int EditCollateralIndex(IndividualCollateralIndex IndividualCollateralIndex)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Select the Individual Collateral Index to be updated from database
            var temp = SelectCollateralIndexByID(IndividualCollateralIndex.IndexID, FBDModel);//FBDModel.IndividualCollateralIndex.First(index => index.IndexID.Equals(IndividualCollateralIndex.IndexID));

            // Update the Individual Collateral Index to the entities
            temp.IndexName = IndividualCollateralIndex.IndexName;
            temp.Unit = IndividualCollateralIndex.Unit;
            temp.Formula = IndividualCollateralIndex.Formula;
            temp.ValueType = IndividualCollateralIndex.ValueType;
            temp.LeafIndex = IndividualCollateralIndex.LeafIndex;

            // Save changes to the database
            int result = FBDModel.SaveChanges();

            return result <= 0 ? 0 : 1;
        }
        public static int DeleteCollateralIndex(string id)
        {
            FBDEntities FBDModel = new FBDEntities();

            var CollateralIndex = SelectCollateralIndexByID(id, FBDModel); //FBDModel.IndividualCollateralIndex.First(index => index.IndexID.Equals(id));

            // Delete business Individual Collateral Index from entities
            FBDModel.DeleteObject(CollateralIndex);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }
        public class IndividualCollateralIndexMetaData
        {
            [Required(ErrorMessage = "Index ID is require")]
            [StringLength(20, ErrorMessage = "Index ID Maximum 20 character")]
            [RegularExpression("[0-9]*",ErrorMessage="Index ID must be numberic")]
            public string IndexID { set; get; }

            [Required(ErrorMessage = "Index Name is required")]
            [StringLength(225, ErrorMessage = "Index name maximu 255 charater")]
            public string IndexName { set; get; }

            [StringLength(50, ErrorMessage = "Unit maximu 50 charater")]
            public string Unit { set; get; }

            [StringLength(255, ErrorMessage = "Unit maximu 50 charater")]
            public string Formula { set; get; }

            [Required(ErrorMessage = "Value type is require")]
             public string ValueType { set; get; }

        }
    }
}
