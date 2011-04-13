using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBD.Models
{
    public interface RPTIIndividualReportService
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
        int FillGeneralInfo(FBDEntities FBDModel, int ID, RPTIndividualReportModel individualInfo);

        /// <summary>
        /// 1. Select in table [CustomersIndividualBasicIndex] all basic information with inputted RankingID
        /// 2. Fill information to [IndividualInfo].[BasicInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="RankingID">Ranking ID of individual Ranking</param>
        /// <param name="individualInfo">individual information to fill data</param>
        /// <returns>integer indicates result</returns>
        int FillBasicInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo);

        /// <summary>
        /// 1. Select in table [CustomersIndividualCollateralIndex] all collateral information with inputted RankingID
        /// 2. Fill information to [IndividualInfo].[CollateralInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="ID">ID of individual Ranking</param>
        /// <param name="individualInfo">individual information to fill data</param>
        /// <returns>integer indicates result</returns>
        int FillCollateralInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo);

        /// <summary>
        /// 1. Create RPTIndividualReportModel [IndividualInfo]
        /// 2. Fill information to [IndividualInfo] with inputted ID
        /// 3. Return [IndividualInfo]
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="ID">ID of individual ranking</param>
        /// <returns>individual Report model containing items to be displayed</returns>
        RPTIndividualReportModel SelectIndividualInfo(FBDEntities FBDModel, int ID);
    }
}
