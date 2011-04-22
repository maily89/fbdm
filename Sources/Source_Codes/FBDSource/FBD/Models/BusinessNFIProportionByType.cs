using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class BusinessNFIProportionByType
    {
        /// <summary>
        /// Select all records of BusinessNFIProportionByType
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>a list of BusinessNFIProportionByType objects</returns>
        public static List<BusinessNFIProportionByType> SelectNFIProportionByType(FBDEntities FBDModel)
        {
            List<BusinessNFIProportionByType> lstNFIProportionByType = FBDModel
                                                                                .BusinessNFIProportionByType
                                                                                .Include("BusinessNonFinancialIndex")
                                                                                .Include("BusinessTypes")
                                                                                .ToList();
            return lstNFIProportionByType;
        }

        /// <summary>
        /// Select all the non-financial index proportion filtered by specified business type in
        /// table BusinessNFIProportionByType
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmType">The selected type</param>
        /// <returns>List of non-financial index proportion</returns>
        public static List<BusinessNFIProportionByType> SelectNFIProportionByType(
                                                                    FBDEntities FBDModel, string prmType)
        {
            List<BusinessNFIProportionByType> lstNFIProportionByType = FBDModel
                                                                                .BusinessNFIProportionByType
                                                                                .Include("BusinessNonFinancialIndex")
                                                                                .Include("BusinessTypes")
                                                                                .Where(p => p.BusinessTypes
                                                                                             .TypeID
                                                                                             .Equals(prmType))
                                                                                             .ToList();
            return lstNFIProportionByType;
        }

        internal static BusinessNFIProportionByType SelectProportionByTypeAndIndex(FBDEntities entity, string parentID, string typeID)
        {
            try
            {
                BusinessNFIProportionByType lstNFIProportionByType = entity
                                                                            .BusinessNFIProportionByType
                                                                            .Include("BusinessNonFinancialIndex")
                                                                            .Include("BusinessTypes")
                                                                            .First(p => p.BusinessTypes
                                                                                         .TypeID
                                                                                         .Equals(typeID)
                                                                                         && p.BusinessNonFinancialIndex.IndexID == parentID
                                                                                    );

                return lstNFIProportionByType;
            }
            catch (Exception)
            {
                
                return null;
            }
        }
        /// <summary>
        /// Select a single record in BusinessNFIProportionByType with specified proportion id
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ProportionID">The proportion ID as primary key</param>
        /// <returns>a record in BusinessNFIProportionByType</returns>
        public static BusinessNFIProportionByType SelectNFIProportionByTypeByProportionID(FBDEntities FBDModel,
                                                                                                    int ProportionID)
        {
            try
            {
                BusinessNFIProportionByType proportion = FBDModel.BusinessNFIProportionByType
                                                                     .First(p => p.ProportionID == ProportionID);
                return proportion;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// Add new a record to BusinessNFIProportionByType
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The View Model containing data</param>
        /// <param name="row">The row to insert</param>
        /// <returns>an integer indicating result</returns>
        public static int AddNFIProportionByType(FBDEntities FBDModel, NFIProportionViewModel viewModel,
                                                                        NFIProportionRowViewModel row)
        {
            BusinessNFIProportionByType proportionByType = new BusinessNFIProportionByType();

            // Select the business type with specified type ID
            BusinessTypes businessType = BusinessTypes.SelectTypeByID(viewModel.TypeID, FBDModel);
            if (businessType == null)
            {
                throw new Exception();
            }

            // Select the non-financial index with specified index ID
            BusinessNonFinancialIndex index = BusinessNonFinancialIndex.SelectNonFinancialIndexByID(FBDModel, row.IndexID);
            if (index == null)
            {
                throw new Exception();
            }

            // Add new a row the BusinessNFIProportionByType table
            proportionByType.BusinessTypes = businessType;
            proportionByType.BusinessNonFinancialIndex = index;
            proportionByType.Proportion = row.Proportion;

            FBDModel.AddToBusinessNFIProportionByType(proportionByType);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit a single record of BusinessNFIProportionByType
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="row">The row to be edited</param>
        /// <returns>an integer indicating result</returns>
        public static int EditNFIProportionByType(FBDEntities FBDModel, NFIProportionRowViewModel row)
        {
            BusinessNFIProportionByType proportionByType = SelectNFIProportionByTypeByProportionID(
                                                                                FBDModel, row.ProportionID);
            proportionByType.Proportion = row.Proportion;
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a single record of BusinessNFIProportionByType
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ProportionID">The Proportion id as primary key</param>
        /// <returns>an integer indicating result</returns>
        public static int DeleteNFIProportionByType(FBDEntities FBDModel, int ProportionID)
        {
            BusinessNFIProportionByType proportion = new BusinessNFIProportionByType();
            proportion = FBDModel.BusinessNFIProportionByType.First(p => p.ProportionID == ProportionID);
            FBDModel.DeleteObject(proportion);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Save information about proportion changes to the database
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="viewModel">The View model containing data to be updated</param>
        /// <returns>
        /// The string indicates the non-financial index gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditMultipleNFIProportionByType(FBDEntities FBDModel, NFIProportionViewModel viewModel)
        {
            // The error index is initially set to be empty, but not null;
            string errorIndex = "";

            try
            {
                // With each row in the list non-financial index of the view model got from View
                foreach (var row in viewModel.ProportionRows)
                {
                    // Error index is temporarily assigned to the index id of the row
                    // to display the error is got at which non-financial index if some
                    // exceptions occur
                    errorIndex = row.IndexID;

                    // If the row is checked in the checkbox
               
                    // The proportion id less than 0 means that row does not
                    // exist in the existing table of database.
                    // With this situation, we add new a row to the table
                    if (row.ProportionID < 0)
                    {
                        AddNFIProportionByType(FBDModel, viewModel, row);
                    }
                    // If the row exists in the BusinessFinancialIndexProportion table...
                    else
                    {
                        // ...then update the row
                        EditNFIProportionByType(FBDModel, row);
                    }
               
                // If the row is not checked in checkbox...
                }
            }
            catch (Exception)
            {
                // If there is exception, return the string specifying the financial index gets error
                return errorIndex;
            }

            // If update successfully, return null
            return null;
        }

        /// <summary>
        /// Create a view model used to exchange data between Controller and View of NFIProportion business
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="prmType">the selected Type chosen from drop down list of View</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static NFIProportionViewModel CreateViewModelByType(FBDEntities FBDModel, string prmType)
        {
            // The list of propotion got from BusinessNFIProportionByType table with specified industry
            List<BusinessNFIProportionByType> lstNFIProportionByType = new List<BusinessNFIProportionByType>();

            // The list of all non-financial indexes which are parent indexes available in the system 
            List<BusinessNonFinancialIndex> lstParentNonFinancialIndexes = new List<BusinessNonFinancialIndex>();

            // The view model that will be returned value
            NFIProportionViewModel viewModelResult = new NFIProportionViewModel();

            // Select all the parent non-financial index propotion with the specified business type
            lstNFIProportionByType = SelectNFIProportionByType(FBDModel, prmType);

            // Select all the financial indexes
            lstParentNonFinancialIndexes = BusinessNonFinancialIndex.SelectNonFinancialParentIndex(FBDModel);

            // With each non-financial indexes got from above process...
            foreach (var index in lstParentNonFinancialIndexes)
            {
                // ...Create each row for the returned view model
                NFIProportionRowViewModel viewModelRow = new NFIProportionRowViewModel();

                // Set value for each row...
                viewModelRow.IndexID = index.IndexID;
                viewModelRow.IndexName = index.IndexName;

                // ...Then add the row to the returned view model
                viewModelResult.ProportionRows.Add(viewModelRow);
            }

            // With each non-financial parent index
            foreach (var item in lstNFIProportionByType)
            {
                // ...And with each row in the returned view model...
                foreach (var row in viewModelResult.ProportionRows)
                {
                    if (item.BusinessNonFinancialIndex.IndexID.Equals(row.IndexID))
                    {
                        if (item.Proportion != null)
                        {
                            row.Proportion = (decimal)item.Proportion;
                        }
                        else
                        {
                            row.Proportion = 0;
                        }
                        row.ProportionID = item.ProportionID;

                        break;
                    }
                }
            }

            // Get all the business types in the system
            viewModelResult.Types = FBDModel.BusinessTypes.ToList();
            viewModelResult.TypeID = prmType;

            return viewModelResult;
        }


    }
}
