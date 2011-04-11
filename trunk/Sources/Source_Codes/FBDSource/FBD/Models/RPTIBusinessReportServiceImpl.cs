using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.CommonUtilities;

namespace FBD.Models
{
    public class RPTIBusinessReportServiceImpl: RPTIBusinessReportService
    {
        public int FillGeneralInfo(FBDEntities FBDModel, int ID, RPTBusinessReportModel businessInfo)
        {
            try
            {
                var generalInfo = CustomersBusinessRanking.SelectBusinessRankingByID(ID, FBDModel);

                businessInfo.CIFNumber = generalInfo.CustomersBusinesses.CIF;
                businessInfo.CustomerName = generalInfo.CustomersBusinesses.CustomerName;
                businessInfo.Industry = generalInfo.BusinessIndustries.IndustryName;
                businessInfo.BusinessType = generalInfo.BusinessTypes.TypeName;
                businessInfo.TaxCode = generalInfo.TaxCode;

                var businessCustomer = CustomersBusinesses.SelectBusinessByID(generalInfo.CustomersBusinesses.BusinessID);
                businessInfo.Branch = businessCustomer.SystemBranches.BranchName;

                businessInfo.ReportingPeriod = generalInfo.SystemReportingPeriods.PeriodName;
                businessInfo.Scale = generalInfo.BusinessScales.Scale;
                businessInfo.FinancialScore = (decimal)generalInfo.FinancialScore;
                businessInfo.NonFinancialScore = (decimal)generalInfo.NonFinancialScore;
                businessInfo.Rank = generalInfo.BusinessRanks.Rank;
                businessInfo.Evaluation = generalInfo.BusinessRanks.Evaluation;
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }

        public int FillScaleInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo)
        {
            try
            {
                var scaleInfo = CustomersBusinessScale.SelectBusinessScaleByRankingID(RankingID, FBDModel);

                foreach (var item in scaleInfo)
                {
                    RPTScaleReportModel scaleRow = new RPTScaleReportModel();
                    scaleRow.Index = item.BusinessScaleCriteria.CriteriaID;
                    scaleRow.Value = (decimal)item.Value;
                    scaleRow.Score = (decimal)item.Score;

                    businessInfo.ScaleInfo.Add(scaleRow);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        public int FillFinancialInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo)
        {
            try
            {
                var financialInfo = CustomersBusinessFinancialIndex.SelectFinancialIndexByRankingID(RankingID, FBDModel);

                foreach (var item in financialInfo)
                {
                    RPTFinancialReportModel financialRow = new RPTFinancialReportModel();
                    financialRow.Index = item.BusinessFinancialIndex.IndexID;
                    financialRow.Value = item.Value;
                    financialRow.Score = (decimal)item.BusinessFinancialIndexLevels.Score;

                    businessInfo.FinancialInfo.Add(financialRow);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        public int FillNonFinancialInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo)
        {
            try
            {
                var nonFinancialInfo = CustomersBusinessNonFinancialIndex.SelectNonFinancialIndexByRankingID(RankingID, FBDModel);

                foreach (var item in nonFinancialInfo)
                {
                    RPTNonFinancialReportModel nonFinancialRow = new RPTNonFinancialReportModel();
                    nonFinancialRow.Index = item.BusinessNonFinancialIndex.IndexID;
                    nonFinancialRow.Value = item.Value;
                    nonFinancialRow.Score = (decimal)item.BusinessNonFinancialIndexLevels.Score;

                    businessInfo.NonFinancialInfo.Add(nonFinancialRow);
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 1;
        }

        public RPTBusinessReportModel SelectBusinessInfo(FBDEntities FBDModel, int ID)
        {
            RPTBusinessReportModel businessInfo = new RPTBusinessReportModel();

            int resultGeneralInfo = FillGeneralInfo(FBDModel, ID, businessInfo);
            int resultScaleInfo = FillScaleInfo(FBDModel, ID, businessInfo);
            int resultFinancialInfo = FillFinancialInfo(FBDModel, ID, businessInfo);
            int resultNonFinancialInfo = FillNonFinancialInfo(FBDModel, ID, businessInfo);

            if (resultGeneralInfo == 0)
            {
                businessInfo.ErrGeneralInfo = Constants.ERR_RPT_GENERAL_INFO;
            }
            
            if (resultScaleInfo == 0)
            {
                businessInfo.ErrScaleInfo = Constants.ERR_RPT_SCALE_INFO;
            }
            
            if (resultFinancialInfo == 0)
            {
                businessInfo.ErrFinancialInfo = Constants.ERR_RPT_FINANCIAL_INFO;
            }
            
            if (resultNonFinancialInfo == 0)
            {
                businessInfo.ErrNonFinancialInfo = Constants.ERR_RPT_NONFINANCIAL_INFO;
            }

            return businessInfo;
        }
    }
}
