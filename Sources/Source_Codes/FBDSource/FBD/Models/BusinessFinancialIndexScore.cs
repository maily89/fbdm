using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class BusinessFinancialIndexScore
    {
        /// <summary>
        /// Select all the financial index score filtered by specified score id
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmScoreID">The Score id as primary key</param>
        /// <returns>A single record of BusinessFinancialIndexScore</returns>
        public static BusinessFinancialIndexScore SelectBusinessFinancialIndexScoreByScoreID(FBDEntities FBDModel,
                                                                                                    int prmScoreID)
        { 
            BusinessFinancialIndexScore score = FBDModel.BusinessFinancialIndexScore
                                                        .First(s => s.ScoreID == prmScoreID);

            return score;
        }

        /// <summary>
        /// Select all the financial index proportion filtered by specified industry, scale and financial index
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmIndustryID">industry ID selected from drop down list</param>
        /// <param name="prmScaleID">scale id selected from drop down list</param>
        /// <param name="prmIndexID">financial index id selected from drop down list</param>
        /// <returns>List of BusinessFinancialIndexScore selected by industry, scale and financial index</returns>
        public static List<BusinessFinancialIndexScore> SelectScoreByIndustryByScaleByFinancialIndex(FBDEntities FBDModel,
                                                    string prmIndustryID, string prmScaleID, string prmIndexID)
        {
            List<BusinessFinancialIndexScore> lstFIScore = FBDModel
                                                            .BusinessFinancialIndexScore
                                                            .Include("BusinessIndustries")
                                                            .Include("BusinessScales")
                                                            .Include("BusinessFinancialIndex")
                                                            .Where(s => s.BusinessIndustries.IndustryID.Equals(prmIndustryID)
                                                                && s.BusinessScales.ScaleID.Equals(prmScaleID)
                                                                && s.BusinessFinancialIndex.IndexID.Equals(prmIndexID)).ToList();
            
            return lstFIScore;
        }                       

        /// <summary>
        /// Add new a record to BusinessFinancialIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <param name="row">The row to insert</param>
        /// <returns>an integer indicating result</returns>
        public static int AddFinancialIndexScore(FBDEntities FBDModel, FIScoreViewModel viewModel,
                                                    FIScoreRowViewModel row)
        {
            BusinessFinancialIndexScore financialIndexScore = new BusinessFinancialIndexScore();

            BusinessIndustries businessIndustry = BusinessIndustries.SelectIndustryByID(
                                                                                        viewModel.IndustryID, FBDModel);

            BusinessScales businessScale = BusinessScales.SelectScaleByID(viewModel.ScaleID, FBDModel);

            BusinessFinancialIndex businessFinancialIndex = BusinessFinancialIndex.SelectFinancialIndexByID(
                                                                                        FBDModel, viewModel.IndexID);

            BusinessFinancialIndexLevels businessFinancialIndexLevel = BusinessFinancialIndexLevels
                                                    .SelectFinancialIndexLevelsByID(row.LevelID, FBDModel);

            if (businessIndustry == null || businessScale == null || businessFinancialIndex == null
                || businessFinancialIndexLevel == null)
            {
                throw new Exception();
            }

            financialIndexScore.BusinessIndustries = businessIndustry;
            financialIndexScore.BusinessScales = businessScale;
            financialIndexScore.BusinessFinancialIndex = businessFinancialIndex;
            financialIndexScore.BusinessFinancialIndexLevels = businessFinancialIndexLevel;
            financialIndexScore.FromValue = row.FromValue;
            financialIndexScore.ToValue = row.ToValue;
            financialIndexScore.FixedValue = row.FixedValue;

            FBDModel.AddToBusinessFinancialIndexScore(financialIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit a single record of BusinessFinancialIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="row">The row to be edited</param>
        /// <returns>an integer indicating result</returns>
        public static int EditFinancialIndexScore(FBDEntities FBDModel, FIScoreRowViewModel row)
        {
            BusinessFinancialIndexScore scoreToBeEdited = SelectBusinessFinancialIndexScoreByScoreID(FBDModel, row.ScoreID);
            
            scoreToBeEdited.FromValue = row.FromValue;
            scoreToBeEdited.ToValue = row.ToValue;
            scoreToBeEdited.FixedValue = row.FixedValue;

            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a record of BusinessFinancialIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ScoreID">The score ID as primary key</param>
        /// <returns>an integer indicating result</returns>
        public static int DeleteFinancialIndexScore(FBDEntities FBDModel, int ScoreID)
        {
            BusinessFinancialIndexScore businessFinancialIndexScore = SelectBusinessFinancialIndexScoreByScoreID(FBDModel, ScoreID);
            FBDModel.DeleteObject(businessFinancialIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Save information about score changes to the database
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <returns>
        /// The string indicates the financial index level gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditMultipleFinancialIndexScore(FBDEntities FBDModel, FIScoreViewModel viewModel)
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
                            AddFinancialIndexScore(FBDModel, viewModel, row);
                        }
                        else
                        {
                            EditFinancialIndexScore(FBDModel, row);
                        }
                    }
                    else
                    {
                        if (row.ScoreID >= 0)
                        {
                            DeleteFinancialIndexScore(FBDModel, row.ScoreID);
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
        /// Create a view model used to exchange data between Controller and View of FIProportion business
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="prmIndustryID">industry selected from drop down list</param>
        /// <param name="prmScaleID">scale selected from drop down list</param>
        /// <param name="prmIndexID">index selected from list</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static FIScoreViewModel CreateViewModelByIndustryByScaleByFinancialIndex(FBDEntities FBDModel,
                                                string prmIndustryID, string prmScaleID, string prmIndexID)
        {
            List<BusinessFinancialIndexScore> lstFIScore = new List<BusinessFinancialIndexScore>();

            List<BusinessFinancialIndexLevels> lstFILevels = new List<BusinessFinancialIndexLevels>();

            FIScoreViewModel viewModelResult = new FIScoreViewModel();

            lstFIScore = SelectScoreByIndustryByScaleByFinancialIndex(FBDModel, prmIndustryID, prmScaleID, prmIndexID);

            lstFILevels = FBDModel.BusinessFinancialIndexLevels.OrderBy(level => level.LevelID).ToList();

            foreach (var level in lstFILevels)
            {
                FIScoreRowViewModel viewModelRow = new FIScoreRowViewModel();

                viewModelRow.LevelID = level.LevelID;

                viewModelResult.ScoreRows.Add(viewModelRow);
            }

            foreach (var item in lstFIScore)
            {
                foreach (var row in viewModelResult.ScoreRows)
                {
                    if (item.BusinessFinancialIndexLevels.LevelID.Equals(row.LevelID))
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

            viewModelResult.Industries = FBDModel.BusinessIndustries.ToList();
            viewModelResult.Scales = FBDModel.BusinessScales.ToList();
            viewModelResult.FinancialIndexes = BusinessFinancialIndex.SelectFinancialLeafIndex(FBDModel);

            viewModelResult.IndustryID = prmIndustryID;
            viewModelResult.ScaleID = prmScaleID;
            viewModelResult.IndexID = prmIndexID;

            return viewModelResult;
        }
    }
}
