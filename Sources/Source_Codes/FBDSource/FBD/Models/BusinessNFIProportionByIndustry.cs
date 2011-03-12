using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class BusinessNFIProportionByIndustry
    {
        /// <summary>
        /// Select all the record of BusinessNFIProportionByIndustry
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <returns>a list of BusinessNFIProportionByIndustry objects</returns>
        public static List<BusinessNFIProportionByIndustry> SelectNFIProportionByIndustry(FBDEntities FBDModel)
        {
            List<BusinessNFIProportionByIndustry> lstNFIProportionByIndustry = FBDModel
                                                                                .BusinessNFIProportionByIndustry
                                                                                .Include("BusinessNonFinancialIndex")
                                                                                .Include("BusinessIndustries")
                                                                                .ToList();
            return lstNFIProportionByIndustry;
        }

        /// <summary>
        /// Select all the non-financial index proportion filtered by specified business industry in
        /// table BusinessNFIProportionByIndustry
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmIndustry">The selected industry</param>
        /// <returns>List of non-financial index proportion</returns>
        public static List<BusinessNFIProportionByIndustry> SelectNFIProportionByIndustry(
                                                                    FBDEntities FBDModel, string prmIndustry)
        {
            List<BusinessNFIProportionByIndustry> lstNFIProportionByIndustry = FBDModel
                                                                                .BusinessNFIProportionByIndustry
                                                                                .Include("BusinessNonFinancialIndex")
                                                                                .Include("BusinessIndustries")
                                                                                .Where(p => p.BusinessIndustries
                                                                                             .IndustryID
                                                                                             .Equals(prmIndustry))
                                                                                             .ToList();
            return lstNFIProportionByIndustry;
        }

        /// <summary>
        /// Select a single record in BusinessNFIProportionByIndustry with specified proportion id
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ProportionID">The proportion ID as primary key</param>
        /// <returns>a record in BusinessNFIProportionByIndustry</returns>
        public static BusinessNFIProportionByIndustry SelectNFIProportionByIndustryByProportionID(FBDEntities FBDModel,
                                                                                                    int ProportionID)
        {
            BusinessNFIProportionByIndustry proportion = FBDModel.BusinessNFIProportionByIndustry
                                                                 .First(p => p.ProportionID == ProportionID);
            return proportion;
        }

        /// <summary>
        /// Add new a record to BusinessNFIProportionByIndustry
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The View Model containing data</param>
        /// <param name="row">The row to insert</param>
        /// <returns></returns>
        public static int AddNFIProportionByIndustry(FBDEntities FBDModel, NFIProportionViewModel viewModel,
                                                                        NFIProportionRowViewModel row)
        {
            BusinessNFIProportionByIndustry proportionByIndustry = new BusinessNFIProportionByIndustry();

            // Select the business Industry with specified Industry ID
            BusinessIndustries businessIndustry = BusinessIndustries.SelectIndustryByID(viewModel.IndustryID, FBDModel);
            if (businessIndustry == null)
            {
                throw new Exception();
            }

            // Select the non-financial index with specified index ID
            BusinessNonFinancialIndex index = BusinessNonFinancialIndex.SelectNonFinancialIndexByID(FBDModel, row.IndexID);
            if (index == null)
            {
                throw new Exception();
            }

            // Add new a row the BusinessNFIProportionByIndustry table
            proportionByIndustry.BusinessIndustries = businessIndustry;
            proportionByIndustry.BusinessNonFinancialIndex = index;
            proportionByIndustry.Proportion = row.Proportion;

            FBDModel.AddToBusinessNFIProportionByIndustry(proportionByIndustry);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit a single record of BusinessNFIProportionByIndustry
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="row">The row to be edited</param>
        /// <returns></returns>
        public static int EditNFIProportionByIndustry(FBDEntities FBDModel, NFIProportionRowViewModel row)
        {
            BusinessNFIProportionByIndustry proportionByIndustry = SelectNFIProportionByIndustryByProportionID(
                                                                                FBDModel, row.ProportionID);
            proportionByIndustry.Proportion = row.Proportion;
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a single record of BusinessNFIProportionByIndustry
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ProportionID">The Proportion id as primary key</param>
        /// <returns></returns>
        public static int DeleteNFIProportionByIndustry(FBDEntities FBDModel, int ProportionID)
        {
            BusinessNFIProportionByIndustry proportion = new BusinessNFIProportionByIndustry();
            proportion = FBDModel.BusinessNFIProportionByIndustry.First(p => p.ProportionID == ProportionID);
            FBDModel.DeleteObject(proportion);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }
        
        /// <summary>
        /// Save information about proportion changes to the database
        /// </summary>
        /// <param name="viewModel">The View model containing data to be updated</param>
        /// <returns>
        /// The string indicates the non-financial index gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditMultipleNFIProportionByIndustry(FBDEntities FBDModel, NFIProportionViewModel viewModel)
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
                    if (row.Checked == true)
                    {
                        // The proportion id less than 0 means that row does not
                        // exist in the existing table of database.
                        // With this situation, we add new a row to the table
                        if (row.ProportionID < 0)
                        {
                            AddNFIProportionByIndustry(FBDModel, viewModel, row);
                        }
                        // If the row exists in the BusinessFinancialIndexProportion table...
                        else
                        {
                            // ...then update the row
                            EditNFIProportionByIndustry(FBDModel, row);
                        }
                    }
                    // If the row is not checked in checkbox...
                    else
                    {
                        // ...and it exists in the table...
                        if (row.ProportionID >= 0)
                        {
                            // ...then delete it
                            DeleteNFIProportionByIndustry(FBDModel, row.ProportionID);
                        }
                    }
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
        /// Create a view model used to exchange data between Controller and View of FIProportion business
        /// </summary>
        /// <param name="prmType">the selected Industry chosen from drop down list of View</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static NFIProportionViewModel CreateViewModelByIndustry(FBDEntities FBDModel, string prmIndustry)
        {
            // The list of propotion got from BusinessNFIProportionByIndustry table with specified industry
            List<BusinessNFIProportionByIndustry> lstNFIProportionByIndustry = new List<BusinessNFIProportionByIndustry>();

            // The list of all non-financial indexes available in the system 
            List<BusinessNonFinancialIndex> lstNonFinancialIndexes = new List<BusinessNonFinancialIndex>();

            // The view model that will be returned value
            NFIProportionViewModel viewModelResult = new NFIProportionViewModel();

            // Select all the parent non-financial index propotion with the specified business type
            lstNFIProportionByIndustry = SelectNFIProportionByIndustry(FBDModel, prmIndustry);

            // Select all the financial indexes
            lstNonFinancialIndexes = BusinessNonFinancialIndex.SelectNonFinancialIndex(FBDModel);

            // With each non-financial indexes got from above process...
            foreach (var index in lstNonFinancialIndexes)
            {
                // ...Create each row for the returned view model
                NFIProportionRowViewModel viewModelRow = new NFIProportionRowViewModel();

                // Set value for each row...
                viewModelRow.IndexID = index.IndexID;
                viewModelRow.IndexName = index.IndexName;
                viewModelRow.LeafIndex = index.LeafIndex;

                // ...Then add the row to the returned view model
                viewModelResult.ProportionRows.Add(viewModelRow);
            }

            // With each non-financial index proportion
            foreach (var item in lstNFIProportionByIndustry)
            {
                // ...And with each row in the returned view model...
                foreach (var row in viewModelResult.ProportionRows)
                {
                    if (item.BusinessNonFinancialIndex.IndexID.Equals(row.IndexID))
                    {
                        row.Proportion = (decimal)item.Proportion;
                        row.ProportionID = item.ProportionID;
                        row.Checked = true;

                        break;
                    }
                }
            }

            // Get all the business industries in the system
            viewModelResult.Industries = FBDModel.BusinessIndustries.ToList();
            viewModelResult.IndustryID = prmIndustry;

            return viewModelResult;
        }
    }
}
