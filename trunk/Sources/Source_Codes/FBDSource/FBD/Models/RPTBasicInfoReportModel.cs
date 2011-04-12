using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.Models
{
    public class RPTBasicInfoReportModel
    {
        /// <summary>
        /// The individual basic index used to be ranked for the individual customer
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// The value the customer got with the index
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The score got from the value
        /// </summary>
        public decimal Score { get; set; }
    }
}
