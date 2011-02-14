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

            var line = entities.BusinessLines.First(i => i.LineID == id);
            return line;
        }

        public static void DeleteLine(int id)
        {
            FBDEntities entities = new FBDEntities();
            var line = BusinessLines.SelectLineByID(id, entities);
            entities.DeleteObject(line);
            entities.SaveChanges();
        }

        public static void EditLine(BusinessLines line)
        {
            FBDEntities entities = new FBDEntities();
            var temp = BusinessLines.SelectLineByID(line.LineID, entities);
            temp.LineName = line.LineName;
            temp.BusinessIndustries = BusinessIndustries.SelectIndustryByID(line.BusinessIndustries.IndustryID, entities);
            entities.SaveChanges();
        }

        public static void AddLine(BusinessLines line,FBDEntities entities)
        {
            entities.AddToBusinessLines(line);
            entities.SaveChanges();
        }
        [Bind(Exclude="LineID")]
        public class BusinessLinesMetaData
        {
            
            [ScaffoldColumn(false)]
            public int LineID { get; set; }

            [DisplayName("Line Name")]
            [Required(ErrorMessage = "Line Name is required")]
            [StringLength(255)]
            public string LineName { get; set; }
        }
    }
}
