using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualBasicIndexProportionMetaData))]
    public partial class IndividualBasicIndexProportion
    {
        /// <summary>
        /// Select all the basic index proportion filtered by specified Borrowing purpose
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmBorrowingPP">The selected Borrowing purpose</param>
        /// <returns>List of basic index proportion</returns>
        public static List<IndividualBasicIndexProportion> SelectBasicIndexProportionByBorrowingPP(
                                                                    FBDEntities FBDModel, string prmBorrowingPPID)
        {
            List<IndividualBasicIndexProportion> lstINVProportionByBorrowingPP = FBDModel
                                                                                .IndividualBasicIndexProportion
                                                                               // .Include("IndividualBasicIndex")
                                                                                .Where(p => p.IndividualBorrowingPurposes
                                                                                             .PurposeID
                                                                                             .Equals(prmBorrowingPPID))
                                                                                             .ToList();
            return lstINVProportionByBorrowingPP;
        }

        /// <summary>
        /// Select all the basic index proportion filtered by specified Borrowing purpose and Index ID
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmBorrowingPP">The selected Borrowing purpose</param>
        /// <returns>List of basic index proportion</returns>
        public static IndividualBasicIndexProportion SelectBasicIndexProportionByBorrowingPPAndBasicIndex(
                                                                    FBDEntities FBDModel, string prmBorrowingPPID,string indexID)
        {
            try
            {
                IndividualBasicIndexProportion lstINVProportionByBorrowingPP = FBDModel
                                                                                    .IndividualBasicIndexProportion
                                                                                   // .Include("IndividualBasicIndex")
                                                                                    //.Include("IndividualBasicIndex")
                                                                                    .First(p => p.IndividualBorrowingPurposes
                                                                                                 .PurposeID
                                                                                                 .Equals(prmBorrowingPPID)
                                                                                           && p.IndividualBasicIndex.IndexID.Equals(indexID)
                                                                                           );
                return lstINVProportionByBorrowingPP;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Select a single record in IndividualBasicIndexProportion with specified proportion id
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ProportionID">The proportion ID as primary key</param>
        /// <returns>a record in IndividualBasicIndexProportion</returns>
        public static IndividualBasicIndexProportion SelectBasicIndexProportionByProportionID(FBDEntities FBDModel,
                                                                                                    int ProportionID)
        {
            //to: recheck about the exeption: null = null. however, we can't use .equal in null case9
            try
            {
                IndividualBasicIndexProportion BasicIndexProportion = FBDModel
                                                                     .IndividualBasicIndexProportion
                                                                     .First(p => p.ProportionID.Equals(ProportionID));

                return BasicIndexProportion;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// Add new a record to IndividualBasicIndexProportion
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The View Model containing data</param>
        /// <param name="row">The row to insert</param>
        /// <returns></returns>
        public static int AddBasicIndexProportion(FBDEntities FBDModel, INVProportionViewModel viewModel,
                                                                        INVProportionRowViewModel row)
        {
            IndividualBasicIndexProportion BasicIndexProportion = new IndividualBasicIndexProportion();

            // Select the business Borrowing purpose with specified Borrowing purpose ID
            IndividualBorrowingPurposes borrowingPP = IndividualBorrowingPurposes
                                                    .SelectBorrowingPPByID(viewModel.BorrowingPPID, FBDModel);
            if (borrowingPP == null)
            {
                throw new Exception();
            }

            // Select the basic index with specified index ID
            IndividualBasicIndex BasicIndex = IndividualBasicIndex
                                                        .SelectBasicIndexByID(row.IndexID, FBDModel);
            if (BasicIndex == null)
            {
                throw new Exception();
            }

            // Add new a row the IndividualBasicIndexProportion table
            BasicIndexProportion.IndividualBorrowingPurposes = borrowingPP;
            BasicIndexProportion.IndividualBasicIndex = BasicIndex;
            BasicIndexProportion.Proportion = row.Proportion;

            FBDModel.AddToIndividualBasicIndexProportion(BasicIndexProportion);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit a single record of IndividualBasicIndexProportion
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="row">The row to be edited</param>
        /// <returns></returns>
        public static int EditBasicIndexProportion(FBDEntities FBDModel, INVProportionRowViewModel row)
        {
            IndividualBasicIndexProportion BasicIndexProportion = SelectBasicIndexProportionByProportionID(
                                                                                FBDModel, row.ProportionID);
            BasicIndexProportion.Proportion = row.Proportion;
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a single record of IndividualBasicIndexProportion
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ProportionID">The Proportion id as primary key</param>
        /// <returns></returns>
        public static int DeleteBasicIndexProportion(FBDEntities FBDModel, int ProportionID)
        {
            IndividualBasicIndexProportion deletedBasicIndexProportion = new IndividualBasicIndexProportion();
            deletedBasicIndexProportion = SelectBasicIndexProportionByProportionID(FBDModel, ProportionID);
            FBDModel.DeleteObject(deletedBasicIndexProportion);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Save information about proportion changes to the database
        /// </summary>
        /// <param name="viewModel">The View model containing data to be updated</param>
        /// <returns>
        /// The string indicates the basic index gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditMultipleBasicIndexProportion(FBDEntities FBDModel, INVProportionViewModel viewModel)
        {
            // The error index is initially set to be empty, but not null;
            string errorIndex = "";

            try
            {
                // With each row in the list basic index of the view model got from View
                foreach (var row in viewModel.ProportionRows)
                {
                    // Error index is temporarily assigned to the index id of the row
                    // to display the error is got at which basic index if some
                    // exceptions occur
                    errorIndex = row.IndexID;

                    // If the row is checked in the checkbox
                    if (row.Checked == true)
                    {
                        row.Proportion = decimal.Parse(row.strProportion);
                        if(row.Proportion>100)
                            throw new Exception();
                        // The proportion id less than 0 means that row does not
                        // exist in the existing table of database.
                        // With this situation, we add new a row to the table
                        if (row.ProportionID < 0)
                        {
                            AddBasicIndexProportion(FBDModel, viewModel, row);
                        }
                        // If the row exists in the IndividualBasicIndexProportion table...
                        else
                        {
                            // ...then update the row
                            EditBasicIndexProportion(FBDModel, row);
                        }
                    }
                    // If the row is not checked in checkbox...
                    else
                    {
                        // ...and it exists in the table...
                        if (row.ProportionID >= 0)
                        {
                            // ...then delete it
                            DeleteBasicIndexProportion(FBDModel, row.ProportionID);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // If there is exception, return the string specifying the basic index gets error
                return errorIndex;
            }

            // If update successfully, return null
            return null;
        }

        /// <summary>
        /// Create a view model used to exchange data between Controller and View of INVProportion 
        /// </summary>
        /// <param name="prmBorrowingPP">the selected Borrowing purpose chosen from drop down list of View</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static INVProportionViewModel CreateViewModelByBorrowingPP(FBDEntities FBDModel, string prmBorrowingPP)
        {
            // The list of propotion got from IndividualBasicIndexProportion table with specified Borrowing purpose
            List<IndividualBasicIndexProportion> lstINVProportionByBorrowingPP = new List<IndividualBasicIndexProportion>();

            // The list of all basic indexes available in the system 
            List<IndividualBasicIndex> lstBasicIndexes = new List<IndividualBasicIndex>();

            // The view model that will be returned value
            INVProportionViewModel viewModelResult = new INVProportionViewModel();

            // Select all the basic index propotion with the specified Borrowing purpose
            lstINVProportionByBorrowingPP = SelectBasicIndexProportionByBorrowingPP(FBDModel, prmBorrowingPP);

            // Select all the basic indexes
            lstBasicIndexes = FBDModel.IndividualBasicIndex.OrderBy(index => index.IndexID).ToList();

            // With each basic indexes got from above process...
            foreach (var index in lstBasicIndexes)
            {
                // ...Create each row for the returned view model
                INVProportionRowViewModel viewModelRow = new INVProportionRowViewModel();

                // Set value for each row...
                viewModelRow.IndexID = index.IndexID;
                viewModelRow.IndexName = index.IndexName;
                viewModelRow.LeafIndex = index.LeafIndex;

                // ...Then add the row to the returned view model
                viewModelResult.ProportionRows.Add(viewModelRow);
            }

            // With each financial idnex that is enabled with the specified Borrowing purpose...
            foreach (var item in lstINVProportionByBorrowingPP)
            {
                // ...And with each row in the returned view model...
                foreach (var row in viewModelResult.ProportionRows)
                {
                    // ...Mark the row as 'Checked' if the basic index of the row
                    // exists in the proportion of the specified Borrowing purpose
                    if (item.IndividualBasicIndex.IndexID.Equals(row.IndexID))
                    {
                        row.Checked = true;
                        row.Proportion = (decimal)item.Proportion;
                        row.ProportionID = item.ProportionID;

                        break;
                    }
                }
            }

            // Get all the industries in the system
            viewModelResult.BorrowingPPs = FBDModel.IndividualBorrowingPurposes.ToList();
            viewModelResult.BorrowingPPID = prmBorrowingPP;

            return viewModelResult;
        }
        class IndividualBasicIndexProportionMetaData
        {
            [Required(ErrorMessage="Proportion can't be empty")]
            [StringLength(5)]
            [RegularExpression("[0-9]*.",ErrorMessage="Propotion must be a numberic")]
            public decimal Proportion { get; set; }
        }
    }
   
}
