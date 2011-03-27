using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType (typeof(SystemReportingPeriodMetadata))]
    public partial class SystemReportingPeriods
    {
        /// <summary>
        /// Select all reporting periods 
        /// in the [System.ReportingPeriods] table 
        /// </summary>
        /// <returns>List of [SystemReportingPeriods]</returns>
        public static List<SystemReportingPeriods> SelectReportingPeriods()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemReportingPeriods.ToList();
        }

        /// <summary>
        /// Select the period with input ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>A Period with id = ID</returns>
        public static SystemReportingPeriods SelectReportingPeriodByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemReportingPeriods.First(i => i.PeriodID == id); 
        }


        /// <summary>
        /// Select the period with input ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>A Period with id = ID</returns>
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
        /// 0: if ERROR
        /// 2: DateTime error
        /// </returns>
        public static int AddReportingPeriod(SystemReportingPeriods reportingPeriod)
        {
            FBDEntities entities = new FBDEntities();
            if (DateTimeHandler.IsToDateLaterThanFromDate(reportingPeriod.FromDate, reportingPeriod.ToDate))
                return 2;
            entities.AddToSystemReportingPeriods(reportingPeriod);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }


        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Update new information of the period into the Database
        /// 3. If successful, return 1 otherwise return 0 or 2
        /// </summary>
        /// <param name="reportingPeriod">Contains information for new Period</param>
        /// <returns>
        /// 1: if OK
        /// 0: if ERROR
        /// 2: DateTime error
        /// </returns>
        public static int EditReportingPeriod(SystemReportingPeriods reportingPeriod)
        {
            FBDEntities entities = new FBDEntities();
        
            var temp = SystemReportingPeriods.SelectReportingPeriodByID(reportingPeriod.PeriodID, entities);
            temp.PeriodName = reportingPeriod.PeriodName;
            temp.FromDate = reportingPeriod.FromDate;
            temp.ToDate = reportingPeriod.ToDate;
            temp.Active = reportingPeriod.Active;
            if (DateTimeHandler.IsToDateLaterThanFromDate(reportingPeriod.FromDate, reportingPeriod.ToDate))
                return 2;
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

        /// <summary>
        /// Check ID dupplication
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 1: if true (dupplication is occuring)
        /// 0: if false (no dupplication, the ID is available
        /// 2: if there is any other exception
        /// </returns>
        public static int IsIDExist(string id)
        {
            FBDEntities entities = new FBDEntities();
            try
            {
                bool check = entities.SystemReportingPeriods.Where(i => i.PeriodID == id).Any();
                return check ? 1 : 0;
            }
            catch (Exception)
            {
                return 2;
            }
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
            public DateTime? FromDate { get; set; }

            [DisplayName("To Date")]
            public DateTime? ToDate { get; set; }

        }
    }

    
}
