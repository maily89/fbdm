using System;
namespace FBD.Models
{
    interface IRanks
    {
        decimal? FromValue { get; set; }
        string Rank { get; set; }
        string RankID { get; set; }
        decimal? ToValue { get; set; }
    }
}
