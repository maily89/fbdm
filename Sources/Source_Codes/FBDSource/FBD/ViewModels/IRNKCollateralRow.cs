using System;
namespace FBD.ViewModels
{
    interface IRNKCollateralRow
    {
        int CustomerScoreID { get; set; }
        FBD.Models.IIndex Index { get; set; }
        bool LeafIndex { get; set; }
        int RankingID { get; set; }
        decimal? Score { get; set; }
        int ScoreID { get; set; }
        System.Collections.Generic.List<FBD.Models.IIndexScore> ScoreList { get; set; }
    }
}
