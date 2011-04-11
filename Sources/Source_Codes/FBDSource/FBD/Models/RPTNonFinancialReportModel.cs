using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.Models
{
    public class RPTNonFinancialReportModel
    {
        /// <summary>
        /// The business non-financial index used to be ranked for the busines customer
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// The value the business customer got with the index
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The score got from the value
        /// </summary>
        public decimal Score { get; set; }
    }
}
