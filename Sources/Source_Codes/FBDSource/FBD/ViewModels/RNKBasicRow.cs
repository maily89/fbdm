using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKBasicRow : FBD.ViewModels.RNKIndex
    {
        public IndividualBasicIndex Index { get; set; }
        public List<IndividualBasicIndexScore> ScoreList { get; set; }

    }
}
