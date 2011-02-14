using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    public partial class SystemReportingPeriodsLogic
    {
        public static List<SystemReportingPeriods> SelectReportingPeriods()
        {
            FBDEntities entities = new FBDEntities();
            return entities.SystemReportingPeriods.ToList();
        }

        public static SystemReportingPeriods SelectReportingPeriodByID(string id)
        {
            FBDEntities entities = new FBDEntities();
            var reportingPeriod = entities.SystemReportingPeriods.First(i => i.PeriodID == id);
            return reportingPeriod;
        }

        public static SystemReportingPeriods SelectReportingPeriodByID(string id, FBDEntities entities)
        {
            var reportingPeriod = entities.SystemReportingPeriods.First(i => i.PeriodID == id);
            return reportingPeriod;
        }

        public static void DeleteReportingPeriod(string id)
        {
            FBDEntities entities = new FBDEntities();
            var reportingPeriod = SystemReportingPeriodsLogic.SelectReportingPeriodByID(id, entities);
            entities.DeleteObject(reportingPeriod);
            entities.SaveChanges();
        }

        public static void EditReportingPeriod(SystemReportingPeriods reportingPeriod)
        {
            FBDEntities entities = new FBDEntities();
            var temp = SystemReportingPeriodsLogic.SelectReportingPeriodByID(reportingPeriod.PeriodID, entities);
            temp.PeriodName = reportingPeriod.PeriodName;
            entities.SaveChanges();
        }

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
            [Required(ErrorMessage = "Period name is required")]
            [StringLength(50)]
            public string PeriodName { get; set; }

            [DisplayName("From Date")]
            //[Required()]
            //[StringLength()]
            public string FromDate { get; set; }

            [DisplayName("To Date")]
            //[Required()]
            //[StringLength()]
            public string ToDate { get; set; }

            [DisplayName("Active")]
            //[Required()]
            //[StringLength()]
            public string Active { get; set; }
        }
    }

    
}
