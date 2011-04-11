using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBD.Models
{
    public interface RPTIBusinessReportService
    {
        int FillGeneralInfo(FBDEntities FBDModel, int ID, RPTBusinessReportModel businessInfo);

        int FillScaleInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo);

        int FillFinancialInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo);

        int FillNonFinancialInfo(FBDEntities FBDModel, int RankingID, RPTBusinessReportModel businessInfo);

        RPTBusinessReportModel SelectBusinessInfo(FBDEntities FBDModel, int ID);
    }
}
