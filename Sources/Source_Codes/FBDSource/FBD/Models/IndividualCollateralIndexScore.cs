using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.ViewModels;

namespace FBD.Models
{
    public partial class IndividualCollateralIndexScore : FBD.Models.IIndexScore
    {
        /// <summary>
        /// Select all the Collateral index score filtered by specified score id
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmScoreID">The Score id as primary key</param>
        /// <returns>A single record of IndividualCollateralIndexScore</returns>
        public static IndividualCollateralIndexScore SelectIndividualCollateralIndexScoreByScoreID(FBDEntities FBDModel, int prmScoreID)
        {
            IndividualCollateralIndexScore score = FBDModel.IndividualCollateralIndexScore
                                                        .First(s => s.ScoreID.Equals(prmScoreID));

            return score;
        }

        /// <summary>
        /// Select all the Collateral index proportion filtered by specified industry, scale and Collateral index
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="prmIndustryID">industry ID selected from drop down list</param>
        /// <param name="prmScaleID">scale id selected from drop down list</param>
        /// <param name="prmIndexID">Collateral index id selected from drop down list</param>
        /// <returns></returns>
        public static List<IndividualCollateralIndexScore> SelectScoreByCollateral(FBDEntities FBDModel,
                                                    string prmCollateralIndex)
        {
            List<IndividualCollateralIndexScore> lstIBIScore = FBDModel
                                                            .IndividualCollateralIndexScore
                                                            .Include("IndividualCollateralIndex")
                                                            .Where(s => s.IndividualCollateralIndex.IndexID.Equals(prmCollateralIndex)).ToList();

            return lstIBIScore;
        }

        /// <summary>
        /// Add new a record to IndividualCollateralIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <param name="row">The row to insert</param>
        /// <returns></returns>
        public static int AddCollateralIndexScore(FBDEntities FBDModel, INVCollateralIndexScoreViewModel viewModel,
                                                    INVCollateralScoreRowViewModel row)
        {
            IndividualCollateralIndexScore CollateralIndexScore = new IndividualCollateralIndexScore();

            IndividualCollateralIndex CollateralIndex = IndividualCollateralIndex.SelectCollateralIndexByID(viewModel.CollateralIndexID, FBDModel);

            IndividualCollateralIndexLevels individualCollateralIndexLevel = IndividualCollateralIndexLevels
                                                    .SelectCollateralIndexLevelsByID(row.LevelID, FBDModel);

            if (CollateralIndex == null || individualCollateralIndexLevel == null)
            {
                throw new Exception();
            }

            CollateralIndexScore.IndividualCollateralIndex = CollateralIndex;
            CollateralIndexScore.IndividualCollateralIndexLevels = individualCollateralIndexLevel;
            CollateralIndexScore.FromValue = row.FromValue;
            CollateralIndexScore.ToValue = row.ToValue;
            CollateralIndexScore.FixedValue = row.FixedValue;

            FBDModel.AddToIndividualCollateralIndexScore(CollateralIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Edit a single record of IndividualCollateralIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="row">The row to be edited</param>
        /// <returns></returns>
        public static int EditCollateralIndexScore(FBDEntities FBDModel, INVCollateralScoreRowViewModel row)
        {
            IndividualCollateralIndexScore scoreToBeEdited = SelectIndividualCollateralIndexScoreByScoreID(FBDModel, row.ScoreID);

            scoreToBeEdited.FromValue = row.FromValue;
            scoreToBeEdited.ToValue = row.ToValue;
            scoreToBeEdited.FixedValue = row.FixedValue;

            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Delete a record of IndividualCollateralIndexScore
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="ScoreID">The score ID as primary key</param>
        /// <returns></returns>
        public static int DeleteCollateralIndexScore(FBDEntities FBDModel, int ScoreID)
        {
            IndividualCollateralIndexScore IndividualCollateralIndexScore = SelectIndividualCollateralIndexScoreByScoreID(FBDModel, ScoreID);
            FBDModel.DeleteObject(IndividualCollateralIndexScore);
            int temp = FBDModel.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// Save information about score changes to the database
        /// </summary>
        /// <param name="FBDModel">The Model of Entities Framework</param>
        /// <param name="viewModel">The view model containing data</param>
        /// <returns>
        /// The string indicates the Collateral index level gets error, 
        /// null value indicates successful updating
        /// </returns>
        public static string EditMultipleCollateralIndexScore(FBDEntities FBDModel, INVCollateralIndexScoreViewModel viewModel)
        {
            string errorLevel = "";

            try
            {
                foreach (var row in viewModel.ScoreRows)
                {

                    errorLevel = row.LevelID.ToString();
                    if (row.Checked == true)
                    {
                        if (row.FixedValue.Length<1)//check valid for fromvalue and to value
                        {
                            row.FromValue = decimal.Parse(row.strFromValue);
                            row.ToValue = decimal.Parse(row.strToValue);
                            if (row.FromValue > row.ToValue || row.FromValue < 0 || row.strToValue.Length > 18 || row.strFromValue.Length > 18)
                                throw new Exception();
                        }
                        if (row.ScoreID < 0)
                        {
                            AddCollateralIndexScore(FBDModel, viewModel, row);
                        }
                        else
                        {
                            EditCollateralIndexScore(FBDModel, row);
                        }
                    }
                    else
                    {
                        if (row.ScoreID >= 0)
                        {
                            DeleteCollateralIndexScore(FBDModel, row.ScoreID);
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
        /// Create a view model used to exchange data between Controller and View of INVCollateralIndex
        /// </summary>
        /// <param name="prmIndustryID">industry selected from drop down list</param>
        /// <param name="prmScaleID">scale selected from drop down list</param>
        /// <returns>The view model containing data to be displayed</returns>
        public static INVCollateralIndexScoreViewModel CreateViewModelByCollateral(FBDEntities FBDModel,
                                                string prmCollateralIndexID)
        {
            List<IndividualCollateralIndexScore> lstCollateralIndexScore = new List<IndividualCollateralIndexScore>();

            List<IndividualCollateralIndexLevels> lstCollateralIndexLevels = new List<IndividualCollateralIndexLevels>();

            INVCollateralIndexScoreViewModel viewModelResult = new INVCollateralIndexScoreViewModel();

            lstCollateralIndexScore = SelectScoreByCollateral(FBDModel, prmCollateralIndexID);

            lstCollateralIndexLevels = FBDModel.IndividualCollateralIndexLevels.OrderBy(level => level.LevelID).ToList();

            foreach (var level in lstCollateralIndexLevels)
            {
                INVCollateralScoreRowViewModel viewModelRow = new INVCollateralScoreRowViewModel();

                viewModelRow.LevelID = level.LevelID;

                viewModelResult.ScoreRows.Add(viewModelRow);
            }

            foreach (var item in lstCollateralIndexScore)
            {
                foreach (var row in viewModelResult.ScoreRows)
                {
                    if (item.IndividualCollateralIndexLevels.LevelID.Equals(row.LevelID))
                    {
                        row.Checked = true;
                        if (item.FixedValue == null)
                        {
                            row.FromValue = (decimal)item.FromValue;
                            row.ToValue = (decimal)item.ToValue;
                        }
                        row.FixedValue = item.FixedValue;
                        row.ScoreID = item.ScoreID;

                        break;
                    }
                }
            }

            viewModelResult.CollateralIndex = IndividualCollateralIndex.SelectCollateralLeafIndex(FBDModel);
            


            viewModelResult.CollateralIndexID = prmCollateralIndexID;
            

            return viewModelResult;
        }
    }
}
