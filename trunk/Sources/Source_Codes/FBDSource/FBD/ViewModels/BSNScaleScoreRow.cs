using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
using System.ComponentModel.DataAnnotations;

namespace FBD.ViewModels
{
    [MetadataType(typeof(FBD.Models.BusinessScaleScore.BusinessScaleScoreMetaData))]
    public class BSNScaleScoreRow
    {
        public int ScoreID { get; set; }

        public Nullable<decimal> FromValue { get; set; }

        public Nullable<decimal> ToValue { get; set; }

        public Nullable<decimal> Score { get; set; }
    }
}