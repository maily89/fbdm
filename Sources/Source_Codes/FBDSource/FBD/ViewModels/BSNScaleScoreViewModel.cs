using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class BSNScaleScoreViewModel
    {
        public List<BusinessScaleCriteria> Criteria;
        public List<BusinessIndustries> Industry;
        public List<BusinessScaleScore> ScaleScore;
        public string CriteriaID;
        public string IndustryID;
    }
}
