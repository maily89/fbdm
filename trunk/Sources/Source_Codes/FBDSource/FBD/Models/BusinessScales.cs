using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FBD.Models
{
    [MetadataType(typeof(BusinessScalesMetaData))]
    public partial class BusinessScales
    {
        public static List<BusinessScales> SelectScales()
        {
            FBDEntities entities = new FBDEntities();
            return entities.BusinessScales.ToList();
        }
        public static BusinessScales SelectScaleByID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            FBDEntities entities = new FBDEntities();
            var scale = entities.BusinessScales.First(i => i.ScaleID == id);
            return scale;
        }

        public static BusinessScales SelectScaleByID(string id, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(id) || entities == null) return null;
            var scale = entities.BusinessScales.First(i => i.ScaleID == id);
            return scale;
        }

        public static int DeleteScale(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;

            FBDEntities entities = new FBDEntities();
            var scale = BusinessScales.SelectScaleByID(id, entities);
            entities.DeleteObject(scale);

            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public static int EditScale(BusinessScales scale)
        {
            if (scale == null) return 0;

            FBDEntities entities = new FBDEntities();
            var temp = BusinessScales.SelectScaleByID(scale.ScaleID, entities);
            temp.Scale = scale.Scale;
            temp.FromValue = scale.FromValue;
            temp.ToValue = scale.ToValue;

            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        public static int AddScale(BusinessScales scale)
        {
            if (scale == null) return 0;

            FBDEntities entities = new FBDEntities();
            entities.AddToBusinessScales(scale);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        public class BusinessScalesMetaData
        {
        		
        	[DisplayName("Scale ID")]
        	[Required]
        	[StringLength(1)]
            public string ScaleID { get; set; }
        		
        	[DisplayName("From Value")]
            public Nullable<decimal> FromValue { get; set; }
        		
        	[DisplayName("To Value")]
            public Nullable<decimal> ToValue { get; set; }
        		
        	[DisplayName("Scale")]
        	[StringLength(50)]
            public string Scale { get; set; }
        }
    }
}
