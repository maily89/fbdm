using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    public partial class SystemReportingPeriods
    {
        /// <summary>
        /// Select all reporting periods contained in the [SystemReportingPeriods] table 
        /// Then, push them to [Index] view
        /// </summary>
        /// <returns>List of [SystemReportingPeriods]</returns>
        public static List<SystemReportingPeriods> SelectReportingPeriods()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemReportingPeriods.ToList();
        }

        /// <summary>
        /// Select only the reporting period with the specify [id] param
        /// </summary>
        /// <param name="id">[id] param is ID of the returned reporting period</param>
        /// <returns>One reporting period which has ID is [id] param</returns>
        public static SystemReportingPeriods SelectReportingPeriodByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var reportingPeriod = entities.SystemReportingPeriods.First(i => i.PeriodID == id);
            return reportingPeriod;
        }

        /// <summary>
        /// Select only the reporting period with the specify [id] param
        /// </summary>
        /// <param name="id">[id] param is ID of the returned reporting period</param>
        /// <returns>One reporting period which has ID is [id] param</returns>
        public static SystemReportingPeriods SelectReportingPeriodByID(string id, FBDEntities entities)
        {
            var reportingPeriod = entities.SystemReportingPeriods.First(i => i.PeriodID == id);
            return reportingPeriod;
        }
        
        /// <summary>
        /// Delete the reporting period with the specify [id] param
        /// </summary>
        /// <param name="id">[id] param is ID of the deleted reporting period</param>
        public static void DeleteReportingPeriod(string id)
        {
            FBDEntities entities = new FBDEntities();
            var reportingPeriod = SystemReportingPeriods.SelectReportingPeriodByID(id, entities);
            entities.DeleteObject(reportingPeriod);
            entities.SaveChanges();
        }

        /// <summary>
        /// Edit a reporting period which will be transmited by the param
        /// </summary>
        /// <param name="reportingPeriod">Information in this param will be updated into database</param>
        public static void EditReportingPeriod(SystemReportingPeriods reportingPeriod)
        {
            FBDEntities entities = new FBDEntities();
            var temp = SystemReportingPeriods.SelectReportingPeriodByID(reportingPeriod.PeriodID, entities);
            temp.PeriodName = reportingPeriod.PeriodName;
            entities.SaveChanges();
        }

        /// <summary>
        /// Add new reportingPeriod
        /// </summary>
        /// <param name="reportingPeriod">Information in this param will be inserted into database</param>
        public static void AddReportingPeriod(SystemReportingPeriods reportingPeriod)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToSystemReportingPeriods(reportingPeriod);
            entities.SaveChanges();
        }

        public class SystemReportingPeriodMetadata
        {
            [DisplayName("Period ID")]
            [Required(ErrorMessage = "Period ID is required")]
            [StringLength(10)]
            public string PeriodID { get; set; }

            [DisplayName("Period Name")]
            //[Required(ErrorMessage = "Period name is required")]
            [StringLength(50)]
            public string PeriodName { get; set; }

            [DisplayName("From Date")]
            //[Required()]
            //[StringLength()]
            public DateTime? FromDate { get; set; }

            [DisplayName("To Date")]
            //[Required()]
            //[StringLength()]
            public DateTime? ToDate { get; set; }

            [DisplayName("Active")]
            //[Required()]
            //[StringLength()]
            public string Active { get; set; }
        }
    }

    
}
