using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessRankingStructureMetaData))]
    public partial class BusinessRankingStructure
    {
        
        public class BusinessRankingStructureMetaData
        {
        		
        	[DisplayName("Index Type")]
        	[Required]
        	[StringLength(10)]
            public string IndexType { get; set; }
        		
        	[DisplayName("Audited Status")]
        	[Required]
        	[StringLength(10)]
            public string AuditedStatus { get; set; }
        		
        	[DisplayName("Percentage")]
            public Nullable<decimal> Percentage { get; set; }
        }
    }
}
