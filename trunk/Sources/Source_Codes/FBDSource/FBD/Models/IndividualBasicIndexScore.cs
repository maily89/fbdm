using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class IndividualBasicIndexScore
    {
        /// <summary>
        /// Select all the Basic index score filtered by specified score id 
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmScoreID">The Score id as primary key</param>
        /// <returns>A single record of IndividualBasicIndexScore</returns>
        public static IndividualBasicIndexScore SelectIndividualBasicIndexScoreByScoreID(FBDEntities FBDModel, int prmScoreID)
        {
            IndividualBasicIndexScore score = FBDModel.IndividualBasicIndexScore
                                                        .First(s => s.ScoreID.Equals(prmScoreID));

            return score;
        }

        /// <summary>
        /// 
        /// Select all the Basic index filtered by specified Borrowing purpose and Basic index
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmIndustryID">industry ID selected from drop down list</param>
        /// <param name="prmScaleID">scale id selected from drop down list</param>
        /// <param name="prmIndexID">Basic index id selected from drop down list</param>
        /// <returns>List<IndividualBasicIndexScore> </returns>
        public static List<IndividualBasicIndexScore> SelectScoreByBasicAndPurposeIndex(FBDEntities FBDModel,
                                                    string prmBasicIndex, string prmPurposeIndex)
        {
            List<IndividualBasicIndexScore> lstIBIScore = FBDModel
                                                            .IndividualBasicIndexScore
                                                            .Include("IndividualBasicIndex")
                                                            .Include("IndividualBorrowingPurposes")
                                                            .Where(s => s.IndividualBasicIndex.IndexID.Equals(prmBasicIndex)
                                                                && s.IndividualBorrowingPurposes.PurposeID.Equals(prmPurposeIndex)).ToList();

            return lstIBIScore;
        }

        /// <summary>
        /// Add new a record to IndividualBasicIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <param name="row">The row to insert</param>
        /// <returns></returns>
        public static int AddBasicIndexScore(FBDEntities FBDModel, INVBasicIndexScoreViewModel viewModel,
                                                    INVBasicScoreRowViewModel row)
        {
            IndividualBasicIndexScore BasicIndexScore = new IndividualBasicIndexScore();

            IndividualBasicIndex basicIndex = IndividualBasicIndex.SelectBasicIndexByID(viewModel.basicIndexID, FBDModel);

            IndividualBorrowingPurposes borrowingPurpose = IndividualBorrowingPurposes.SelectBorrowingPPByID(viewModel.BorrowingPPID, FBDModel);

            IndividualBasicIndexLevels individualBasicIndexLevel = IndividualBasicIndexLevels
                                                    .SelectBasicIndexLevelsByID(row.LevelID, FBDModel);

            if (basicIndex == null || borrowingPurpose == null || individualBasicIndexLevel == null)
            {
                throw new Exception();
            }

            BasicIndexScore.IndividualBasicIndex = basicIndex;
            BasicIndexScore.IndividualBorrowingPurposes = borrowingPurpose;
            BasicIndexScore.IndividualBasicIndexLevels= individualBasicIndexLevel;
            BasicIndexScore.FromValue = row.FromValue;
            BasicIndexScore.ToValue = row.ToValue;
            BasicIndexScore.FixedValue = row.FixedValue;

            FBDModel.AddToIndividualBasicIndexScore(BasicIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit a single record of IndividualBasicIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="row">The row to be edited</param>
        /// <returns></returns>
        public static int EditBasicIndexScore(FBDEntities FBDModel, INVBasicScoreRowViewModel row)
        {
            IndividualBasicIndexScore scoreToBeEdited = SelectIndividualBasicIndexScoreByScoreID(FBDModel, row.ScoreID);

            scoreToBeEdited.FromValue = row.FromValue;
            scoreToBeEdited.ToValue = row.ToValue;
            scoreToBeEdited.FixedValue = row.FixedValue;

            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a record of IndividualBasicIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ScoreID">The score ID as primary key</param>
        /// <returns></returns>
        public static int DeleteBasicIndexScore(FBDEntities FBDModel, int ScoreID)
        {
            IndividualBasicIndexScore IndividualBasicIndexScore = SelectIndividualBasicIndexScoreByScoreID(FBDModel, ScoreID);
            FBDModel.DeleteObject(IndividualBasicIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Save information about score changes to the database
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <returns>
        /// The string indicates the Basic index level gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditMultipleBasicIndexScore(FBDEntities FBDModel, INVBasicIndexScoreViewModel viewModel)
        {
            string errorLevel = "";

            try
            {
                foreach (var row in viewModel.ScoreRows)
                {
                    errorLevel = row.LevelID.ToString();

                    if (row.Checked == true)
                    {
                        if (row.ScoreID < 0)
                        {
                            AddBasicIndexScore(FBDModel, viewModel, row);
                        }
                        else
                        {
                            EditBasicIndexScore(FBDModel, row);
                        }
                    }
                    else
                    {
                        if (row.ScoreID >= 0)
                        {
                            DeleteBasicIndexScore(FBDModel, row.ScoreID);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return errorLevel;
            }

            return null;
        }

        /// <summary>
        /// Create a view model used to exchange data between Controller and View of BasicIndexScore 
        /// </summary>
        /// <param name="prmIndustryID">industry selected from drop down list</param>
        /// <param name="prmScaleID">scale selected from drop down list</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static INVBasicIndexScoreViewModel CreateViewModelByBasicAndPurposeIndex(FBDEntities FBDModel,
                                                string prmBasicIndexID, string prmBBorrowingPPID)
        {
            List<IndividualBasicIndexScore> lstBasicIndexScore = new List<IndividualBasicIndexScore>();

            List<IndividualBasicIndexLevels> lstBasicIndexLevels = new List<IndividualBasicIndexLevels>();

            INVBasicIndexScoreViewModel viewModelResult = new INVBasicIndexScoreViewModel();

            lstBasicIndexScore = SelectScoreByBasicAndPurposeIndex(FBDModel, prmBasicIndexID, prmBBorrowingPPID);

            lstBasicIndexLevels = FBDModel.IndividualBasicIndexLevels.OrderBy(level => level.LevelID).ToList();

            foreach (var level in lstBasicIndexLevels)
            {
                INVBasicScoreRowViewModel viewModelRow = new INVBasicScoreRowViewModel();

                viewModelRow.LevelID = level.LevelID;

                viewModelResult.ScoreRows.Add(viewModelRow);
            }

            foreach (var item in lstBasicIndexScore)
            {
                foreach (var row in viewModelResult.ScoreRows)
                {
                    if (item.IndividualBasicIndexLevels.LevelID.Equals(row.LevelID))
                    {
                        row.Checked = true;
                        row.FromValue = (decimal)item.FromValue;
                        row.ToValue = (decimal)item.ToValue;
                        row.FixedValue = item.FixedValue;
                        row.ScoreID = item.ScoreID;

                        break;
                    }
                }
            }

            viewModelResult.basicIndex = FBDModel.IndividualBasicIndex.ToList();
            viewModelResult.BorrowingPP = FBDModel.IndividualBorrowingPurposes.ToList();
           

            viewModelResult.basicIndexID = prmBasicIndexID;
            viewModelResult.BorrowingPPID = prmBBorrowingPPID;

            return viewModelResult;
        }
    }
}
