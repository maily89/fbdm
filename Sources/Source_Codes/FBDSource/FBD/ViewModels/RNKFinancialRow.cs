using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;
namespace FBD.ViewModels
{
    public class RNKFinancialRow : FBD.ViewModels.RNKIndex
    {
       
        public BusinessFinancialIndex Index { get; set; }
        public List<BusinessFinancialIndexScore> ScoreList { get; set; }

    }
}
