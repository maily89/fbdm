using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKNonFinancialRow: FBD.ViewModels.RNKIndex 
    {
        public BusinessNonFinancialIndex Index { get; set; }
        public List<BusinessNonFinancialIndexScore> ScoreList { get; set; }
    }
}
