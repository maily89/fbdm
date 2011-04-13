using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBD.Models
{
    public interface RPTIBusinessReportService
    {
        /// <summary>
        /// 1. With input ID, select the following information:
        /// CIFNumber, CustomerName, Industry, BusinessType, TaxCode, Branch, ReportingPeriod, Scale, FinancialScore,
        /// NonFinancialScore, Rank, Evaluation
        /// 2. Fill selected information to [businessInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="ID">ID of business Ranking</param>
        /// <param name="businessInfo">Business information to fill data</param>
        /// <returns>integer indicates result</returns>
        int FillGeneralInfo(FBDEntities FBDModel, int ID, RPTBusinessReportModel businessInfo);

        /// <summary>
        /// 1. Select in table [CustomersBusinessScale] all scale information with inputted RankingID
        /// 2. Fill information to [businessInfo].[ScaleInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="RankingID">Ranking ID of business Ranking</param>
        /// <param name="businessInfo">business information to fill data</param>
        /// <returns>integer indicates result</returns>
        int FillScaleInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo);

        /// <summary>
        /// 1. Select in table [CustomersBusinessFinancialIndex] all financial information with inputted RankingID
        /// 2. Fill information to [businessInfo].[FinancialInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="RankingID">Ranking ID of Business Ranking</param>
        /// <param name="businessInfo">Business Information to fill data</param>
        /// <returns>integer indicates result</returns>
        int FillFinancialInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo);

        /// <summary>
        /// 1. Select in table [CustomersBusinessNonFinancialIndex] all non financial information with inputted RankingID
        /// 2. Fill information to [businessInfo].[NonFinancialInfo]
        /// 3. Return 1 if success, otherwise return 0
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="RankingID">Ranking ID of business ranking</param>
        /// <param name="businessInfo">business information to fill data</param>
        /// <returns>integer indicates result</returns>
        int FillNonFinancialInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo);

        /// <summary>
        /// 1. Create RPTBusinessReportModel [businessInfo]
        /// 2. Fill information to [businessInfo] with inputted ID
        /// 3. Return [businessInfo]
        /// </summary>
        /// <param name="FBDModel">Model of EF</param>
        /// <param name="ID">ID of business ranking</param>
        /// <returns>Business Report model containing items to be displayed</returns>
        RPTBusinessReportModel SelectBusinessInfo(FBDEntities FBDModel, int ID);
    }
}
