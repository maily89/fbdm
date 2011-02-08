using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public class NFINonFinancialIndexLogic
    {
        static FBDEntities FBDModel = new FBDEntities();

        /// <summary>
        /// Select all the Non-Financial Index in the table Business.NonFinancialIndex
        /// </summary>
        /// <returns>NFINonFinancialIndexViewModel</returns>
        public static NFINonFinancialIndexViewModel SelectNonFinancialIndex()
        {
            NFINonFinancialIndexViewModel viewModel = null;
            try
            {
                var nonFinancialIndexes = FBDModel.BusinessNonFinancialIndex;

                viewModel.NonFinancialIndexes = nonFinancialIndexes.ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return viewModel;
        }

        /// <summary>
        /// Select the Non-Financial Index in the table Business.NonFinancialIndex with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>BusinessNonFinancialIndex</returns>
        public static BusinessNonFinancialIndex SelectNonFinancialIndexByID(string id)
        {
            BusinessNonFinancialIndex BusinessNonFinancialIndex = null;
            try
            {
                BusinessNonFinancialIndex = FBDModel.BusinessNonFinancialIndex.First(index => index.IndexID.Equals(id));
            }
            catch (Exception)
            {
                return null;
            }
            return BusinessNonFinancialIndex;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new index into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="BusinessNonFinancialIndex">A new Non-FinancialIndex information</param>
        /// <returns>int</returns>
        public static int AddNonFinancialIndex(BusinessNonFinancialIndex BusinessNonFinancialIndex)
        {
            try
            {
                FBDModel.AddToBusinessNonFinancialIndex(BusinessNonFinancialIndex);
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
        /// <param name="id">ID of the Non-FinancialIndex editted</param>
        /// <param name="formValues">The information of the FinancialIndex editted</param>
        /// <returns>int</returns>
        public static int EditNonFinancialIndex(string id, FormCollection formValues)
        {
            var nonFinancialIndex = FBDModel.BusinessNonFinancialIndex.First(index => index.IndexID.Equals(id));
            UpdateModel(nonFinancialIndex, "BusinessNonFinancialIndex");
        }

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Financial Index with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID of the Non-Financial Index selected</param>
        /// <returns>int</returns>
        public static int DeleteNonFinancialIndex(string id)
        {
            try
            {
                var nonFinancialIndex = FBDModel.BusinessNonFinancialIndex.First(index => index.IndexID.Equals(id));

                FBDModel.DeleteObject(nonFinancialIndex);
                FBDModel.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }
    }
}
