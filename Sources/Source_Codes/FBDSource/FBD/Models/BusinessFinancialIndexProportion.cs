using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class BusinessFinancialIndexProportion
    {
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
            lstFinancialIndexes = FBDModel.BusinessFinancialIndex.ToList();

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

        public static string EditFinancialIndexProportion(FIProportionViewModel viewModel)
        {
            FBDEntities FBDModel = new FBDEntities();

            string errorIndex = "";

            try
            {
                foreach (var row in viewModel.ProportionRows)
                {
                    errorIndex = row.IndexID;

                    if (row.Checked == true)
                    {
                        BusinessFinancialIndexProportion financialIndexProportion = null;
                        if (row.ProportionID < 0)
                        {
                            financialIndexProportion = new BusinessFinancialIndexProportion();

                            BusinessIndustries businessIndustry = BusinessIndustries
                                                                    .SelectIndustryByID(viewModel.IndustryID, FBDModel);
                            if (businessIndustry == null)
                            {
                                throw new Exception();
                            }
                            BusinessFinancialIndex financialIndex = BusinessFinancialIndex
                                                                        .SelectFinancialIndexByID(row.IndexID, FBDModel);
                            if (financialIndex == null)
                            {
                                throw new Exception();
                            }

                            financialIndexProportion.BusinessIndustries = businessIndustry;
                            financialIndexProportion.BusinessFinancialIndex = financialIndex;
                            financialIndexProportion.Proportion = row.Proportion;

                            FBDModel.AddToBusinessFinancialIndexProportion(financialIndexProportion);
                            FBDModel.SaveChanges();
                        }
                        else
                        {
                            financialIndexProportion = FBDModel.BusinessFinancialIndexProportion
                                                        .First(p => p.ProportionID == row.ProportionID);
                            financialIndexProportion.Proportion = row.Proportion;
                            FBDModel.SaveChanges();
                        }
                    }
                    else
                    {
                        if (row.ProportionID >= 0)
                        {
                            BusinessFinancialIndexProportion deletedFinancialIndexProportion = new BusinessFinancialIndexProportion();
                            deletedFinancialIndexProportion = FBDModel.BusinessFinancialIndexProportion
                                                                .First(p => p.ProportionID == row.ProportionID);
                            FBDModel.DeleteObject(deletedFinancialIndexProportion);
                            FBDModel.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return errorIndex;
            }

            return null;
        }
    }
}
