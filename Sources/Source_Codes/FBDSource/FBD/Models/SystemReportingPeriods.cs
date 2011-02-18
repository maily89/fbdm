using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType (typeof(SystemReportingPeriodMetadata))]
    public partial class SystemReportingPeriods
    {
        /// <summary>
        /// Select all reporting periods contained in the [System.ReportingPeriods] table 
        /// </summary>
        /// <returns>List of [SystemReportingPeriods]</returns>
        public static List<SystemReportingPeriods> SelectReportingPeriods()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemReportingPeriods.ToList();
        }

        /// <summary>
        /// Select the period (periodID, periodName, fromDate, toDate, active) 
        /// in the table [System.ReportingPeriods] with input ID
        /// </summary>
        /// <param name="id">[id] param is ID of the returned reporting period</param>
        /// <returns>One reporting period which has ID is [id] param</returns>
        public static SystemReportingPeriods SelectReportingPeriodByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemReportingPeriods.First(i => i.PeriodID == id); 
        }


        /// <summary>
        /// Select the period (periodID, periodName, fromDate, toDate, active) 
        /// in the table [System.ReportingPeriods] with input ID
        /// </summary>
        /// <param name="id">[id] param is ID of the returned reporting period</param>
        /// <returns>Reporting Period has ID = [id]</returns>
        public static SystemReportingPeriods SelectReportingPeriodByID(string id, FBDEntities entities)
        {
            return entities.SystemReportingPeriods.First(i => i.PeriodID == id);
        }
                

        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new period into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="reportingPeriod">Contains information for new Period</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR</returns>
        public static int AddReportingPeriod(SystemReportingPeriods reportingPeriod)
        {
            FBDEntities entities = new FBDEntities();
            entities.AddToSystemReportingPeriods(reportingPeriod);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }


        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new period into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="reportingPeriod">Contains new information of the period</param>
        /// <returns>
        /// 1: if OK, 
        /// 0: if ERROR</returns>
        public static int EditReportingPeriod(SystemReportingPeriods reportingPeriod)
        {
            FBDEntities entities = new FBDEntities();
        
            var temp = SystemReportingPeriods.SelectReportingPeriodByID(reportingPeriod.PeriodID, entities);
            temp.PeriodName = reportingPeriod.PeriodName;
            temp.FromDate = reportingPeriod.FromDate;
            temp.ToDate = reportingPeriod.ToDate;
            temp.Active = reportingPeriod.Active;
            
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        
        /// <summary>
        /// 1. Receive ID from parameter
        /// 2. Delete the period with selected ID from the [System.ReportingPeriods] table
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="id">ID of the period to be deleted</param>
        /// <returns>
        /// 1: if OK
        /// 0: of ERROR</returns>
        public static int DeleteReportingPeriod(string id)
        {
            FBDEntities entities = new FBDEntities();
            var reportingPeriod = SystemReportingPeriods.SelectReportingPeriodByID(id, entities);
            entities.DeleteObject(reportingPeriod);
            
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }


        public class SystemReportingPeriodMetadata
        {
            [DisplayName("Period ID")]
            [Required(ErrorMessage = "Period ID is required")]
            [StringLength(10)]
            public string PeriodID { get; set; }

            [DisplayName("Period Name")]
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

        }
    }

    
}
