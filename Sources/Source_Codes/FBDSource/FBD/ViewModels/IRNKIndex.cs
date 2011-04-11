using System;
namespace FBD.ViewModels
{
    public abstract class RNKIndex
    {
        public decimal CalculatedScore { get; set; }
        public int CustomerScoreID { get; set; }
        public bool LeafIndex { get; set; }
        public decimal Proportion { get; set; }
        public int RankingID { get; set; }
        public decimal Result { get; set; }
        public decimal? Score { get; set; }
        public int ScoreID { get; set; }
        public string Value { get; set; }
    }
}
