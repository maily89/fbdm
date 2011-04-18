using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.CommonUtilities;

namespace FBD.Models
{
    public partial class BusinessNFIProportionCalculated
    {
        /// <summary>
        /// Select a single record of BusinessNFIProportionCalculated table with specified Unique key
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="type">Business Type</param>
        /// <param name="industry">Business Industry</param>
        /// <param name="indexID">Non-Financial Index</param>
        /// <returns>an object of BusinessNFIProportionCalculated</returns>
        public static BusinessNFIProportionCalculated SelectNFIProportionCalculatedByTypeByIndustryByIndex(
                                                                            FBDEntities FBDModel,
                                                                            string type,
                                                                            string industry,
                                                                            string indexID)
        {
            try
            {
                BusinessNFIProportionCalculated proportion = FBDModel.BusinessNFIProportionCalculated
                                                                     .Include("BusinessTypes")
                                                                     .Include("BusinessIndustries")
                                                                     .Include("BusinessNonFinancialIndex")
                                                                     .First(p => p.BusinessNonFinancialIndex.IndexID.Equals(indexID)
                                                                            && p.BusinessTypes.TypeID.Equals(type)
                                                                            && p.BusinessIndustries.IndustryID.Equals(industry));
                return proportion;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Add a record to BusinessNFIProportionCalculated table
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="index">Non-Financial Index</param>
        /// <param name="type">Business Type</param>
        /// <param name="industry">Business Industry</param>
        /// <param name="proportion">Corresponding proportion</param>
        /// <returns>an integer indicates whether or not adding is successful</returns>
        public static int AddNFIProportionCalculated(FBDEntities FBDModel, BusinessNonFinancialIndex index, BusinessTypes type,
                                                                           BusinessIndustries industry,
                                                                           decimal proportion)
        {
            BusinessNFIProportionCalculated proToBeAdded = new BusinessNFIProportionCalculated();
            proToBeAdded.BusinessNonFinancialIndex = index;
            proToBeAdded.BusinessTypes = type;
            proToBeAdded.BusinessIndustries = industry;
            proToBeAdded.Proportion = proportion;

            FBDModel.AddToBusinessNFIProportionCalculated(proToBeAdded);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a single record of BusinessNFIProportionCalculated
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="proToBeDeleted">The Proportion to be deleted</param>
        /// <returns>an integer indicates whether or not deleting is successful</returns>
        public static int DeleteNFIProportionCalculated(FBDEntities FBDModel, BusinessNFIProportionCalculated proToBeDeleted)
        {
            FBDModel.DeleteObject(proToBeDeleted);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Update all the NFIProportionCalculated with specified business type after updating business proportion by type
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="type">business type</param>
        /// <returns>an integer indicates whether or not updating is successful</returns>
        public static int UpdateNFIProportionCalculatedByType(FBDEntities FBDModel, string type)
        {
            try
            {
                // Select all BusinessNFIProportionByType with specified business type
                List<BusinessNFIProportionByType> lstProportionByType = BusinessNFIProportionByType
                                                                    .SelectNFIProportionByType(FBDModel, type);
                // Select all record of BusinessNFIProportionByIndustry table
                List<BusinessNFIProportionByIndustry> lstProportionByIndustries = BusinessNFIProportionByIndustry
                                                                                    .SelectNFIProportionByIndustry(FBDModel);

                // With each record of BusinessNFIProportionByIndustry
                foreach (var proByIndustry in lstProportionByIndustries)
                {                    
                    // Initialize data used to insert to BusinessNFIProportionCalculated table
                    BusinessNonFinancialIndex nonFinancialIndexToBeAdded = proByIndustry.BusinessNonFinancialIndex;
                    BusinessIndustries industryToBeAdded = proByIndustry.BusinessIndustries;
                    BusinessTypes typeToBeAdded = null;
                    decimal proportionToBeAdded = 0;

                    // With each record selected from BusinessNFIProportionByType with specified business type
                    foreach (var proByType in lstProportionByType)
                    {
                        // Find the root index (parent index) of the non-financial index to be inserted
                        if (proByType.BusinessNonFinancialIndex.IndexID
                                    .Equals(StringHelper.FindParentIndex(proByIndustry.BusinessNonFinancialIndex.IndexID)))
                        {
                            typeToBeAdded = proByType.BusinessTypes;
                            
                            // Calculate summary proportion from leaf index and parent index
                            try
                            {
                                proportionToBeAdded = (decimal)(proByIndustry.Proportion * (proByType.Proportion / 100));
                            }
                            catch (Exception)
                            {
                                proportionToBeAdded = 0;
                            }
                            break;
                        }
                    }

                    // Delete the existing object in the table
                    // BusinessNFIProportionCalculated (to avoid duplicated inserting)
                    BusinessNFIProportionCalculated proportion = SelectNFIProportionCalculatedByTypeByIndustryByIndex
                                                                    (FBDModel,
                                                                    typeToBeAdded.TypeID,
                                                                    industryToBeAdded.IndustryID,
                                                                    nonFinancialIndexToBeAdded.IndexID);
                    // If the object exists
                    if (proportion != null)
                    {
                        // Delete it
                        DeleteNFIProportionCalculated(FBDModel, proportion);          
                    }

                    // Add new record to BusinessNFIProportionCalculated
                    AddNFIProportionCalculated(FBDModel, nonFinancialIndexToBeAdded, typeToBeAdded, industryToBeAdded,
                                                                proportionToBeAdded);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// Update all the NFIProportionCalculated with specified business industry after
        /// updating business proportion by industry
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="industry">business industry</param>
        /// <returns>an integer indicates whether or not updating is successful</returns>
        public static int UpdateNFIProportionCalculatedByIndustry(FBDEntities FBDModel, string industry)
        {
            try
            {
                // Select all record of BusinessNFIProportionByType table
                List<BusinessNFIProportionByType> lstProportionByType = BusinessNFIProportionByType
                                                                    .SelectNFIProportionByType(FBDModel);

                // Select all BusinessNFIProportionByIndustry with specified business industry
                List<BusinessNFIProportionByIndustry> lstProportionByIndustry = BusinessNFIProportionByIndustry
                                                                                    .SelectNFIProportionByIndustry(FBDModel, industry);

                // With each record of BusinessNFIProportionByIndustry
                foreach (var proByIndustry in lstProportionByIndustry)
                {
                    // Initialize data used to insert to BusinessNFIProportionCalculated table
                    BusinessNonFinancialIndex nonFinancialIndexToBeAdded = proByIndustry.BusinessNonFinancialIndex;
                    BusinessIndustries industryToBeAdded = proByIndustry.BusinessIndustries;
                    BusinessTypes typeToBeAdded = null;
                    decimal proportionToBeAdded = 0;

                    // With each record selected from BusinessNFIProportionByType with specified business type
                    foreach (var proByType in lstProportionByType)
                    {
                        // Find the root index (parent index) of the non-financial index to be inserted
                        if (proByType.BusinessNonFinancialIndex.IndexID
                                    .Equals(StringHelper.FindParentIndex(proByIndustry.BusinessNonFinancialIndex.IndexID)))
                        {
                            typeToBeAdded = proByType.BusinessTypes;
                            try
                            {
                                proportionToBeAdded = (decimal)(proByIndustry.Proportion * (proByType.Proportion / 100));
                            }
                            catch (Exception)
                            {
                                proportionToBeAdded = 0;
                            }

                            // Delete the existing object in the table
                            // BusinessNFIProportionCalculated (to avoid duplicated inserting)
                            BusinessNFIProportionCalculated proportion = SelectNFIProportionCalculatedByTypeByIndustryByIndex
                                                                    (FBDModel,
                                                                    typeToBeAdded.TypeID,
                                                                    industryToBeAdded.IndustryID,
                                                                    nonFinancialIndexToBeAdded.IndexID);
                            if (proportion != null)
                            {
                                DeleteNFIProportionCalculated(FBDModel, proportion);
                            }

                            AddNFIProportionCalculated(FBDModel, nonFinancialIndexToBeAdded, typeToBeAdded, industryToBeAdded,
                                                                        proportionToBeAdded);
                        }
                    }                 
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }
    }
}
