using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public class FIFinancialIndexLogic
    {
        static FBDEntities FBDModel = new FBDEntities();

        /// <summary>
        /// Select all the Financial Index in the table Business.FinancialIndex
        /// </summary>
        /// <returns>FIFinancialIndexViewModel</returns>
        public static FIFinancialIndexViewModel SelectFinancialIndex()
        {
            FIFinancialIndexViewModel viewModel = null;
            try 
            {
                var financialIndexes = FBDModel.BusinessFinancialIndex;

                viewModel.FinancialIndexes = financialIndexes.ToList();
            }
            catch (Exception)
            {
                return null;        
            }
            return viewModel;
        }

        /// <summary>
        /// Select the Financial Index in the table Business.FinancialIndex with input ID
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>BusinessFinancialIndex</returns>
        public static BusinessFinancialIndex SelectFinancialIndexByID(string id)
        {
            BusinessFinancialIndex businessFinancialIndex = null;
            try
            {
                businessFinancialIndex = FBDModel.BusinessFinancialIndex.First(index => index.IndexID.Equals(id));
            }
            catch (Exception)
            {
                return null;
            }
            return businessFinancialIndex;
        }

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new index into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="businessFinancialIndex">A new FinancialIndex information</param>
        /// <returns>int</returns>
        public static int AddFinancialIndex(BusinessFinancialIndex businessFinancialIndex)
        {
            try
            {
                FBDModel.AddToBusinessFinancialIndex(businessFinancialIndex);
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
        /// <param name="id">ID of the FinancialIndex editted</param>
        /// <param name="formValues">The information of the FinancialIndex editted</param>
        /// <returns>int</returns>
        //public static int EditFinancialIndex(string id, FormCollection formValues)
        //{
            
        //}

        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the Financial Index with selected ID from database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID of the Financial Index selected</param>
        /// <returns>int</returns>
        public static int DeleteFinancialIndex(string id)
        {
            try
            {
                var financialIndex = FBDModel.BusinessFinancialIndex.First(index => index.IndexID.Equals(id));

                FBDModel.DeleteObject(financialIndex);
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
