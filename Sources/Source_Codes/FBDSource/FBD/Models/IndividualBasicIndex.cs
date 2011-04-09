using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualBasicIndexMetaData))]
    public partial class IndividualBasicIndex
    {
        /// <summary>
        /// Select all the Basic Index in the table Business.BasicIndex
        /// </summary>
        /// <returns>List of Basic Index</returns>
        public static List<IndividualBasicIndex> SelectBasicIndex()
        {
            FBDEntities FBDModel = new FBDEntities();

            List<IndividualBasicIndex> lstBasicIndex = null;
            lstBasicIndex = FBDModel.IndividualBasicIndex.ToList();
            return lstBasicIndex;
        }

        /// <summary>
        /// Select all the Basic Index in the table Business.BasicIndex
        /// </summary>
        /// <returns>List of Basic Index</returns>
        public static List<IndividualBasicIndex> SelectBasicIndex(FBDEntities FBDModel)
        {

            List<IndividualBasicIndex> lstBasicIndex = null;
            lstBasicIndex = FBDModel.IndividualBasicIndex.ToList();
            return lstBasicIndex;
        }
        /// <summary>
        /// Select the Basic Index in the table Business.BasicIndex with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>IndividualBasicIndex</returns>
        public static IndividualBasicIndex SelectBasicIndexByID(string id)
        {
            FBDEntities FBDModel = new FBDEntities();

            IndividualBasicIndex IndividualBasicIndex = null;

            // Get the business Basic index from the entities model with the inputted ID
            IndividualBasicIndex = FBDModel.IndividualBasicIndex.First(index => index.IndexID.Equals(id));

            return IndividualBasicIndex;
        }

        public static IndividualBasicIndex SelectBasicIndexByID(string id, FBDEntities FBDModel)
        {
            //FBDEntities FBDModel = new FBDEntities();

            IndividualBasicIndex IndividualBasicIndex = null;

            // Get the business Basic index from the entities model with the inputted ID
            IndividualBasicIndex = FBDModel.IndividualBasicIndex.First(index => index.IndexID.Equals(id));
            return IndividualBasicIndex;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new index into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="IndividualBasicIndex">A new BasicIndex information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddBasicIndex(IndividualBasicIndex IndividualBasicIndex)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Add new business Basic index with the inputted information to the entities
            FBDModel.AddToIndividualBasicIndex(IndividualBasicIndex);

            // Save changes to the Database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;

        }

        public static int EditBasicIndex(IndividualBasicIndex individualBasicIndex)
        {
            FBDEntities FBDModel = new FBDEntities();

            // Select the Basic index to be updated from database
            var temp = SelectBasicIndexByID(individualBasicIndex.IndexID, FBDModel);//FBDModel.IndividualBasicIndex.First(index => index.IndexID.Equals(individualBasicIndex.IndexID));

            // Update the Basic index to the entities
            temp.IndexName = individualBasicIndex.IndexName;
            temp.Unit = individualBasicIndex.Unit;
            temp.Formula = individualBasicIndex.Formula;
            temp.ValueType = individualBasicIndex.ValueType;
            temp.LeafIndex = individualBasicIndex.LeafIndex;

            // Save changes to the database
            int result = FBDModel.SaveChanges();

            return result <= 0 ? 0 : 1;
        }
        public static int DeleteBasicIndex(string id)
        {
            FBDEntities FBDModel = new FBDEntities();

            var BasicIndex = SelectBasicIndexByID(id, FBDModel); //FBDModel.IndividualBasicIndex.First(index => index.IndexID.Equals(id));

            // Delete business Basic index from entities
            FBDModel.DeleteObject(BasicIndex);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }
        public class IndividualBasicIndexMetaData
        {
            [DisplayName("Index ID")]
            [Required(ErrorMessage = "Index ID is required")]
            [StringLength(20)]
            [RegularExpression("[0-9]+", ErrorMessage = "Index ID must be numberic")]
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

            [DisplayName("Is leaf index")]
            public bool LeafIndex { get; set; }
        }
    }
}
