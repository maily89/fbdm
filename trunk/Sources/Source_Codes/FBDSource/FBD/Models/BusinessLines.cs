using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessLinesMetaData))]
    public partial class BusinessLines
    {

        public static List<BusinessLines> SelectLines()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessLines.ToList();
        }
        public static BusinessLines SelectLineByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var line = entities.BusinessLines.First(i => i.LineID == id);
            return line;
        }

        public static BusinessLines SelectLineByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var line = entities.BusinessLines.First(i => i.LineID == id);
            return line;
        }

        public static int DeleteLine(int id)
        {
            FBDEntities entities = new FBDEntities();
            var line = BusinessLines.SelectLineByID(id, entities);
            entities.DeleteObject(line);
            int result=entities.SaveChanges();
            return result<=0?0:1;
        }

        public static int EditLine(BusinessLines line)
        {
            FBDEntities entities = new FBDEntities();
            var temp = BusinessLines.SelectLineByID(line.LineID, entities);
            temp.LineName = line.LineName;
            temp.BusinessIndustries = BusinessIndustries.SelectIndustryByID(line.BusinessIndustries.IndustryID, entities);
            int result=entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public static int AddLine(BusinessLines line,FBDEntities entities)
        {
            if (line == null || entities == null) return 0;
            entities.AddToBusinessLines(line);
            int result=entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        [Bind(Exclude="LineID")]
        public class BusinessLinesMetaData
        {
            
            [ScaffoldColumn(false)]
            public int LineID { get; set; }

            [DisplayName("Line Namexxxx")]
            [Required(ErrorMessage = "Line Name is required")]
            [StringLength(255)]
            public string LineName { get; set; }
        }
    }
}
