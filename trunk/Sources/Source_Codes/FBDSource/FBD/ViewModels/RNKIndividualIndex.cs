﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.ViewModels
{
    public class RNKIndividualIndex
    {
        public List<CustomersIndividualRanking> CustomerRanking { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public string BranchID { get; set; }
        public string Cif { get; set; }
    }
}