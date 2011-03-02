using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class BusinessFinancialIndexProportion
    {
        /// <summary>
        /// Create a view model used to exchange data between Controller and View of FIProportion business
        /// </summary>
        /// <param name="prmIndustry">the selected Industry chosen from drop down list of View</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static FIProportionViewModel CreateViewModelByIndustry(string prmIndustry)
        {
            FBDEntities FBDModel = new FBDEntities();
            // The list of propotion got from BusinessFinancialIndexProportion table with specified industry
            List<BusinessFinancialIndexProportion> lstFIProportionByIndustry = new List<BusinessFinancialIndexProportion>();
            
            // The list of all financial indexes available in the system 
            List<BusinessFinancialIndex> lstFinancialIndexes = new List<BusinessFinancialIndex>();

            // The view model that will be returned value
            FIProportionViewModel viewModelResult = new FIProportionViewModel();

            // Select all the financial index propotion with the specified industry
            lstFIProportionByIndustry = FBDModel.BusinessFinancialIndexProportion
                                                .Include("BusinessFinancialIndex")
                                                .Where(p => p.BusinessIndustries.IndustryID.Equals(prmIndustry)).ToList();

            // Select all the financial indexes
            lstFinancialIndexes = FBDModel.BusinessFinancialIndex.OrderBy(index => index.IndexID).ToList();

            // With each financial indexes got from above process...
            foreach (var index in lstFinancialIndexes)
            {
                // ...Create each row for the returned view model
                FIProportionRowViewModel viewModelRow = new FIProportionRowViewModel();

                // Set value for each row...
                viewModelRow.IndexID = index.IndexID;
                viewModelRow.IndexName = index.IndexName;
                viewModelRow.LeafIndex = index.LeafIndex;

                // ...Then add the row to the returned view model
                viewModelResult.ProportionRows.Add(viewModelRow);
            }

            // With each financial idnex that is enabled with the specified industry...
            foreach (var item in lstFIProportionByIndustry)
            {
                // ...And with each row in the returned view model...
                foreach (var row in viewModelResult.ProportionRows)
                {
                    // ...Mark the row as 'Checked' if the financial index of the row
                    // exists in the proportion of the specified industry
                    if (item.BusinessFinancialIndex.IndexID.Equals(row.IndexID))
                    {
                        row.Checked = true;
                        row.Proportion = (decimal)item.Proportion;
                        row.ProportionID = item.ProportionID;

                        break;
                    }
                }
            }

            // Get all the industries in the system
            viewModelResult.Industries = FBDModel.BusinessIndustries.ToList();
            viewModelResult.IndustryID = prmIndustry;

            return viewModelResult;
        }

        /// <summary>
        /// Save information about proportion changes to the database
        /// </summary>
        /// <param name="viewModel">The View model containing data to be updated</param>
        /// <returns>
        /// The string indicates the financial index gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditFinancialIndexProportion(FIProportionViewModel viewModel)
        {
            FBDEntities FBDModel = new FBDEntities();

            // The error index is initially set to be empty, but not null;
            string errorIndex = "";

            try
            {
                // With each row in the list financial index of the view model got from View
                foreach (var row in viewModel.ProportionRows)
                {
                    // Error index is temporarily assigned to the index id of the row
                    // to display the error is got at which financial index if some
                    // exceptions occur
                    errorIndex = row.IndexID;

                    // If the row is checked in the checkbox
                    if (row.Checked == true)
                    {
                        BusinessFinancialIndexProportion financialIndexProportion = null;

                        // The proportion id less than 0 means that row does not
                        // exist in the existing table of database.
                        // With this situation, we add new a row to the table
                        if (row.ProportionID < 0)
                        {
                            financialIndexProportion = new BusinessFinancialIndexProportion();

                            // Select the business industry with specified industry ID
                            BusinessIndustries businessIndustry = BusinessIndustries
                                                                    .SelectIndustryByID(viewModel.IndustryID, FBDModel);
                            if (businessIndustry == null)
                            {
                                throw new Exception();
                            }

                            // Select the financial index with specified index ID
                            BusinessFinancialIndex financialIndex = BusinessFinancialIndex
                                                                        .SelectFinancialIndexByID(row.IndexID, FBDModel);
                            if (financialIndex == null)
                            {
                                throw new Exception();
                            }

                            // Add new a row the BusinessFinancialIndexProportion table
                            financialIndexProportion.BusinessIndustries = businessIndustry;
                            financialIndexProportion.BusinessFinancialIndex = financialIndex;
                            financialIndexProportion.Proportion = row.Proportion;

                            FBDModel.AddToBusinessFinancialIndexProportion(financialIndexProportion);
                            FBDModel.SaveChanges();
                        }
                        // If the row exists in the BusinessFinancialIndexProportion table...
                        else
                        {
                            // ...then update the row
                            financialIndexProportion = FBDModel.BusinessFinancialIndexProportion
                                                        .First(p => p.ProportionID == row.ProportionID);
                            financialIndexProportion.Proportion = row.Proportion;
                            FBDModel.SaveChanges();
                        }
                    }
                    // If the row is not checked in checkbox...
                    else
                    {
                        // ...and it exists in the table...
                        if (row.ProportionID >= 0)
                        {
                            // ...then delete it
                            BusinessFinancialIndexProportion deletedFinancialIndexProportion = new BusinessFinancialIndexProportion();
                            deletedFinancialIndexProportion = FBDModel.BusinessFinancialIndexProportion
                                                                .First(p => p.ProportionID == row.ProportionID);
                            FBDModel.DeleteObject(deletedFinancialIndexProportion);
                            FBDModel.SaveChanges();
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
    }
}
