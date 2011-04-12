using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.CommonUtilities;

namespace FBD.Models
{
    public class RPTIIndividualReportServiceImpl: RPTIIndividualReportService
    {
        public int FillGeneralInfo(FBDEntities FBDModel, int ID, RPTIndividualReportModel individualInfo)
        {
            try
            {
                var generalInfo = CustomersIndividualRanking.SelectIndividualRankingByID(ID, FBDModel);

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

        public int FillBasicInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo)
        {
            try
            {
                var basicInfo = CustomersIndividualBasicIndex.SelectBasicIndexByRankingID(RankingID, FBDModel);

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

        public int FillCollateralInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo)
        {
            try
            {
                var collateralInfo = CustomersIndividualCollateralIndex.SelectCollateralIndexByRankingID(RankingID, FBDModel);

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
