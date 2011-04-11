using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKCollateralRow : FBD.ViewModels.RNKIndex
    {       
        public IndividualCollateralIndex Index { get; set; }
        public List<IndividualCollateralIndexScore> ScoreList { get; set; }

    }
}
