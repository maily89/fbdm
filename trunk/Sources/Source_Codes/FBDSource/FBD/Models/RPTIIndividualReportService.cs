using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBD.Models
{
    public interface RPTIIndividualReportService
    {
        int FillGeneralInfo(FBDEntities FBDModel, int ID, RPTIndividualReportModel individualInfo);

        int FillBasicInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo);

        int FillCollateralInfo(FBDEntities FBDModel, int RankingID, RPTIndividualReportModel individualInfo);

        RPTIndividualReportModel SelectIndividualInfo(FBDEntities FBDModel, int ID);
    }
}
