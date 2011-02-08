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

        public static FIFinancialIndexViewModel SelectFinancialIndex()
        {
            //Select all the Financial Index in the table Business.FinancialIndex
            FIFinancialIndexViewModel viewModel = null;
            try 
            {
            var financialIndexes = from businessFinancialIndex in FBDModel.BusinessFinancialIndex
                                   select businessFinancialIndex.IndexID, businessFinancialIndex.IndexName,
                                          businessFinancialIndex.Unit, businessFinancialIndex.Formula, 
                                          businessFinancialIndex.ValueType, businessFinancialIndex.LeafIndex;

            viewModel.FinancialIndexes = financialIndexes.ToList();
            }
            catch (Exception)
            {
                return null;        
            }
            return viewModel;
        }

        public static BusinessFinancialIndex SelectFinancialIndexByID(int id)
        {
            BusinessFinancialIndex businessFinancialIndex = null;
            try
            {
                businessFinancialIndex = FBDModel.BusinessFinancialIndex.First(index => index.IndexID = id);
            }
            catch (Exception)
            {
                return null;
            }
            return businessFinancialIndex;
        }

        public static int AddFinancialIndex(BusinessFinancialIndex businessFinancialIndex)
        {

        }

        public static int EditFinancialIndex(int id, FormCollection formValues)
        {
            var financialIndex = FBDModel.BusinessFinancialIndex.First(index => index.IndexID = id);
        }

        public static int DeleteFinancialIndex(string id)
        {
            var financialIndex = FBDModel.BusinessFinancialIndex.First(index => index.IndexID.Equals(id));

            FBDModel.DeleteObject(financialIndex);
            FBDModel.SaveChanges();
        }
    }
}
