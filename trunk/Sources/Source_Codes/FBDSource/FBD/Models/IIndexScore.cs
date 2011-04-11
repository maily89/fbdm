using System;
namespace FBD.Models
{
    public interface IIndexScore
    {
         string FixedValue { get; set; }
         decimal? FromValue { get; set; }
         int ScoreID { get; set; }
         decimal? ToValue { get; set; }
    }
}
