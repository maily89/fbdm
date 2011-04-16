using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class BusinessNonFinancialIndexScore
    {
        /// <summary>
        /// Select all the non-financial index score filtered by specified score id
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmScoreID">The Score id as primary key</param>
        /// <returns>A single record of BusinessNonFinancialIndexScore</returns>
        public static BusinessNonFinancialIndexScore SelectBusinessNonFinancialIndexScoreByScoreID(FBDEntities FBDModel,
                                                                                                    int prmScoreID)
        {
            BusinessNonFinancialIndexScore score = FBDModel.BusinessNonFinancialIndexScore
                                                        .First(s => s.ScoreID == prmScoreID);

            return score;
        }

        /// <summary>
        /// Select all the non-financial index score filtered by specified industry and non-financial index
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmIndustryID">industry ID selected from drop down list</param>
        /// <param name="prmIndexID">index id selected from list</param>
        /// <returns>List of NonFinancial Index Score</returns>
        public static List<BusinessNonFinancialIndexScore> SelectScoreByIndustryByNonFinancialIndex(FBDEntities FBDModel,
                                                    string prmIndustryID, string prmIndexID)
        {
            List<BusinessNonFinancialIndexScore> lstNFIScore = FBDModel
                                                            .BusinessNonFinancialIndexScore
                                                            .Include("BusinessIndustries")
                                                            .Include("BusinessNonFinancialIndex")
                                                            .Where(s => s.BusinessIndustries.IndustryID.Equals(prmIndustryID)
                                                                && s.BusinessNonFinancialIndex.IndexID.Equals(prmIndexID)).ToList();

            return lstNFIScore;
        }

        /// <summary>
        /// Add new a record to BusinessNonFinancialIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <param name="row">The row to insert</param>
        /// <returns>an integer indicating result</returns>
        public static int AddNonFinancialIndexScore(FBDEntities FBDModel, NFIScoreViewModel viewModel,
                                                    NFIScoreRowViewModel row)
        {
            BusinessNonFinancialIndexScore nonFinancialIndexScore = new BusinessNonFinancialIndexScore();

            BusinessIndustries businessIndustry = BusinessIndustries.SelectIndustryByID(
                                                                                        viewModel.IndustryID, FBDModel);

            BusinessNonFinancialIndex businessNonFinancialIndex = BusinessNonFinancialIndex.SelectNonFinancialIndexByID(
                                                                                        FBDModel, viewModel.IndexID);

            BusinessNonFinancialIndexLevels businessNonFinancialIndexLevel = BusinessNonFinancialIndexLevels
                                                    .SelectNonFinancialIndexLevelsByID(row.LevelID, FBDModel);

            if (businessIndustry == null || businessNonFinancialIndex == null
                || businessNonFinancialIndexLevel == null)
            {
                throw new Exception();
            }

            nonFinancialIndexScore.BusinessIndustries = businessIndustry;
            nonFinancialIndexScore.BusinessNonFinancialIndex = businessNonFinancialIndex;
            nonFinancialIndexScore.BusinessNonFinancialIndexLevels = businessNonFinancialIndexLevel;
            nonFinancialIndexScore.FromValue = row.FromValue;
            nonFinancialIndexScore.ToValue = row.ToValue;
            nonFinancialIndexScore.FixedValue = row.FixedValue;

            FBDModel.AddToBusinessNonFinancialIndexScore(nonFinancialIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit a single record of BusinessNonFinancialIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="row">The row to be edited</param>
        /// <returns>an integer indicating result</returns>
        public static int EditNonFinancialIndexScore(FBDEntities FBDModel, NFIScoreRowViewModel row)
        {
            BusinessNonFinancialIndexScore scoreToBeEdited = SelectBusinessNonFinancialIndexScoreByScoreID(FBDModel, row.ScoreID);

            scoreToBeEdited.FromValue = row.FromValue;
            scoreToBeEdited.ToValue = row.ToValue;
            scoreToBeEdited.FixedValue = row.FixedValue;

            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a record of BusinessNonFinancialIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ScoreID">The score ID as primary key</param>
        /// <returns>an integer indicating result</returns>
        public static int DeleteNonFinancialIndexScore(FBDEntities FBDModel, int ScoreID)
        {
            BusinessNonFinancialIndexScore businessNonFinancialIndexScore = SelectBusinessNonFinancialIndexScoreByScoreID(FBDModel, ScoreID);
            FBDModel.DeleteObject(businessNonFinancialIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Save information about score changes to the database
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <returns>
        /// The string indicates the non-financial index level gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditMultipleNonFinancialIndexScore(FBDEntities FBDModel, NFIScoreViewModel viewModel)
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
                            AddNonFinancialIndexScore(FBDModel, viewModel, row);
                        }
                        else
                        {
                            EditNonFinancialIndexScore(FBDModel, row);
                        }
                    }
                    else
                    {
                        if (row.ScoreID >= 0)
                        {
                            DeleteNonFinancialIndexScore(FBDModel, row.ScoreID);
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
        /// Create a view model used to exchange data between Controller and View of NFIScore business
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="prmIndustryID">industry selected from drop down list</param>
        /// <param name="prmIndexID">index selected from list</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static NFIScoreViewModel CreateViewModelByIndustryByNonFinancialIndex(FBDEntities FBDModel,
                                                string prmIndustryID, string prmIndexID)
        {
            List<BusinessNonFinancialIndexScore> lstNFIScore = new List<BusinessNonFinancialIndexScore>();

            List<BusinessNonFinancialIndexLevels> lstNFILevels = new List<BusinessNonFinancialIndexLevels>();

            NFIScoreViewModel viewModelResult = new NFIScoreViewModel();

            lstNFIScore = SelectScoreByIndustryByNonFinancialIndex(FBDModel, prmIndustryID, prmIndexID);

            lstNFILevels = FBDModel.BusinessNonFinancialIndexLevels.OrderBy(level => level.LevelID).ToList();

            foreach (var level in lstNFILevels)
            {
                NFIScoreRowViewModel viewModelRow = new NFIScoreRowViewModel();

                viewModelRow.LevelID = level.LevelID;

                viewModelResult.ScoreRows.Add(viewModelRow);
            }

            foreach (var item in lstNFIScore)
            {
                foreach (var row in viewModelResult.ScoreRows)
                {
                    if (item.BusinessNonFinancialIndexLevels.LevelID.Equals(row.LevelID))
                    {
                        row.Checked = true;
                        if (item.FromValue != null)
                        {
                            row.FromValue = (decimal)item.FromValue;
                        }
                        else 
                        {
                            row.FromValue = 0;    
                        }
                        if (item.ToValue != null)
                        {
                            row.ToValue = (decimal)item.ToValue;
                        }
                        else
                        {
                            row.ToValue = 0;
                        }
                        row.FixedValue = item.FixedValue;
                        row.ScoreID = item.ScoreID;

                        break;
                    }
                }
            }

            viewModelResult.Industries = FBDModel.BusinessIndustries.ToList();
            viewModelResult.NonFinancialIndexes = BusinessNonFinancialIndex.SelectNonFinancialLeafIndex(FBDModel);

            viewModelResult.IndustryID = prmIndustryID;
            viewModelResult.IndexID = prmIndexID;

            return viewModelResult;
        }
    }
}
