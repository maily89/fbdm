using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessNonFinancialIndexMetaData))]
    public partial class BusinessNonFinancialIndex
    {
        /// <summary>
        /// Select all the Non Financial Index in the table Business.NonFinancialIndex
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>List of Non Financial Index</returns>
        public static List<BusinessNonFinancialIndex> SelectNonFinancialIndex(FBDEntities FBDModel)
        {
            List<BusinessNonFinancialIndex> lstNonFinancialIndex = null;

            // Get the business non financial index from entities model
            lstNonFinancialIndex = FBDModel.BusinessNonFinancialIndex.OrderBy(m=>m.IndexID).ToList();

            return lstNonFinancialIndex;
        }

        /// <summary>
        /// Select the Non Financial Index in the table Business.NonFinancialIndex with input ID
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="id">string ID</param>
        /// <returns>BusinessNonFinancialIndex</returns>
        public static BusinessNonFinancialIndex SelectNonFinancialIndexByID(FBDEntities FBDModel, string id)
        {
            
            BusinessNonFinancialIndex businessNonFinancialIndex = null;

            // Get the business non financial index from the entities model with the inputted ID
            businessNonFinancialIndex = FBDModel.BusinessNonFinancialIndex.First(index => index.IndexID.Equals(id));

            return businessNonFinancialIndex;
        }

        /// <summary>
        /// Select all the non financial indexes where leaf index is true
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>List of NonFinancial Leaf index</returns>
        public static List<BusinessNonFinancialIndex> SelectNonFinancialLeafIndex(FBDEntities FBDModel)
        {
            List<BusinessNonFinancialIndex> lstBusinessNonFinancialLeafIndexes = FBDModel
                                                                           .BusinessNonFinancialIndex
                                                                           .Where(index => index.LeafIndex == true)
                                                                           .ToList();

            return lstBusinessNonFinancialLeafIndexes;
        }

        /// <summary>
        /// Select all the non financial indexes where leaf index is false
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>List of NonFinancial Parent index</returns>
        public static List<BusinessNonFinancialIndex> SelectNonFinancialParentIndex(FBDEntities FBDModel)
        {
            List<BusinessNonFinancialIndex> lstBusinessNonFinancialParentIndexes = FBDModel
                                                                           .BusinessNonFinancialIndex
                                                                           .Where(index => index.LeafIndex == false)
                                                                           .OrderBy(index => index.IndexID)
                                                                           .ToList();

            return lstBusinessNonFinancialParentIndexes;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new index into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="businessNonFinancialIndex">A new NonFinancialIndex information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int AddNonFinancialIndex(FBDEntities FBDModel, BusinessNonFinancialIndex businessNonFinancialIndex)
        {
            // Add new business non financial index with the inputted information to the entities
            FBDModel.AddToBusinessNonFinancialIndex(businessNonFinancialIndex);

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
        /// <param name="businessNonFinancialIndex">The non financial index to be updated</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int EditNonFinancialIndex(FBDEntities FBDModel, BusinessNonFinancialIndex businessNonFinancialIndex)
        {
            // Select the non financial index to be updated from database
            var temp = FBDModel.BusinessNonFinancialIndex.First(index => index.IndexID.Equals(businessNonFinancialIndex.IndexID));

            // Update the non financial index to the entities
            temp.IndexName = businessNonFinancialIndex.IndexName;
            temp.Unit = businessNonFinancialIndex.Unit;
            temp.Formula = businessNonFinancialIndex.Formula;
            temp.ValueType = businessNonFinancialIndex.ValueType;
            temp.LeafIndex = businessNonFinancialIndex.LeafIndex;

            // Save changes to the database
            int result = FBDModel.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Non Financial Index with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="id">ID of the Non Financial Index selected</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns>
        public static int DeleteNonFinancialIndex(FBDEntities FBDModel, string id)
        {
            var nonFinancialIndex = FBDModel.BusinessNonFinancialIndex.First(index => index.IndexID.Equals(id));

            // Delete business non financial index from entities
            FBDModel.DeleteObject(nonFinancialIndex);

            // Save changes to the database
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class BusinessNonFinancialIndexMetaData
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
            [StringLength(15)]
            public string Unit { get; set; }

            [DisplayName("Formula")]
            [StringLength(100)]
            public string Formula { get; set; }
        }
    }
}
