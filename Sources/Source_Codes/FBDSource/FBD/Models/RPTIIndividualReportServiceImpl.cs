using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.CommonUtilities;

namespace FBD.Models
{
    public class RPTIIndividualReportServiceImpl: RPTIIndividualReportService
    {
        /// <summary>
        /// 1. With input ID, select the following information:
        /// _CIFNumber
        /// _CustomerName
        /// _BorrowingPurpose
        /// _Branch
        /// _RankedDate
        /// _BasicScore
        /// _CollateralScore
        /// _Evaluation
        /// 2. Fill selected information to [IndividualInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="ID">ID of individual Ranking</param>
        /// <param name="individualInfo">individual information to fill data</param>
        /// <returns>integer indicates result</returns>
        public int FillGeneralInfo(FBDEntities FBDModel, int ID, RPTIndividualReportModel individualInfo)
        {
            try
            {
                var generalInfo = CustomersIndividualRanking.SelectIndividualRankingByIDWithReference(ID, FBDModel);

                individualInfo.CIFNumber = generalInfo.CustomersIndividuals.CIF;
                individualInfo.CustomerName = generalInfo.CustomersIndividuals.CustomerName;
                individualInfo.BorrowingPurpose = generalInfo.IndividualBorrowingPurposes.Purpose;

                var individualCustomer = CustomersIndividuals.SelectIndividualByID(generalInfo.CustomersIndividuals.IndividualID);
                individualInfo.Branch = individualCustomer.SystemBranches.BranchName;

                individualInfo.RankedDate = (DateTime)generalInfo.Date;
                individualInfo.BasicScore = (decimal)generalInfo.BasicIndexScore;
                individualInfo.CollateralScore = (decimal)generalInfo.CollateralIndexScore;
                individualInfo.Evaluation = generalInfo.IndividualSummaryRanks.Evaluation;
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 1. Select in table [CustomersIndividualBasicIndex] all basic information with inputted RankingID
        /// 2. Fill information to [IndividualInfo].[BasicInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="RankingID">Ranking ID of individual Ranking</param>
        /// <param name="individualInfo">individual information to fill data</param>
        /// <returns>integer indicates result</returns>
        public int FillBasicInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo)
        {
            try
            {
                var basicInfo = CustomersIndividualBasicIndex.SelectBasicIndexByRankingIDWithReference(RankingID, FBDModel);

                foreach (var item in basicInfo)
                {
                    RPTBasicInfoReportModel basicInfoRow = new RPTBasicInfoReportModel();
                    basicInfoRow.Index = item.IndividualBasicIndex.IndexName;
                    basicInfoRow.Value = item.Value;
                    basicInfoRow.Score = (decimal)item.IndividualBasicIndexLevels.Score;

                    individualInfo.BasicInfo.Add(basicInfoRow);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 1. Select in table [CustomersIndividualCollateralIndex] all collateral information with inputted RankingID
        /// 2. Fill information to [IndividualInfo].[CollateralInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="ID">ID of individual Ranking</param>
        /// <param name="individualInfo">individual information to fill data</param>
        /// <returns>integer indicates result</returns>
        public int FillCollateralInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo)
        {
            try
            {
                var collateralInfo = CustomersIndividualCollateralIndex.SelectCollateralIndexByRankingIDWithReference(RankingID, FBDModel);

                foreach (var item in collateralInfo)
                {
                    RPTCollateralInfoReportModel collateralInfoRow = new RPTCollateralInfoReportModel();
                    collateralInfoRow.Index = item.IndividualCollateralIndex.IndexName;
                    collateralInfoRow.Value = item.Value;
                    collateralInfoRow.Score = (decimal)item.IndividualCollateralIndexLevels.Score;

                    individualInfo.CollateralInfo.Add(collateralInfoRow);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 1. Create RPTIndividualReportModel [IndividualInfo]
        /// 2. Fill information to [IndividualInfo] with inputted ID
        /// 3. Return [IndividualInfo]
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="ID">ID of individual ranking</param>
        /// <returns>individual Report model containing items to be displayed</returns>
        public RPTIndividualReportModel SelectIndividualInfo(FBDEntities FBDModel, int ID)
        {
            RPTIndividualReportModel individualInfo = new RPTIndividualReportModel();

            int resultGeneralInfo = FillGeneralInfo(FBDModel, ID, individualInfo);
            int resultBasicInfo = FillBasicInfo(FBDModel, ID, individualInfo);
            int resultCollateralInfo = FillCollateralInfo(FBDModel, ID, individualInfo);

            if (resultGeneralInfo == 0)
            {
                individualInfo.ErrGeneralInfo = Constants.ERR_RPT_GENERAL_INFO;
            }

            if (resultBasicInfo == 0)
            {
                individualInfo.ErrBasicInfo = Constants.ERR_RPT_BASIC_INFO;
            }

            if (resultCollateralInfo == 0)
            {
                individualInfo.ErrCollateralInfo = Constants.ERR_RPT_COLLATERAL_INFO;
            }

            return individualInfo;
        }
    }
}
