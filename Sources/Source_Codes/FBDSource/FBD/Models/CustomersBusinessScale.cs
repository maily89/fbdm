using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
namespace FBD.Models
{

    [MetadataType(typeof(CustomersBusinessScaleMetaData))]
    public partial class CustomersBusinessScale
    {


        /// <summary>
        /// return the business specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the business</param>
        /// <returns>business</returns>
        public static CustomersBusinessScale SelectBusinessScaleByID(int id)
        {

            FBDEntities entities = new FBDEntities();
            var business = entities.CustomersBusinessScale.First(i => i.ID == id);

            return business;
        }

        public static List<CustomersBusinessScale> SelectBusinessScaleByRankingID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var scale = entities.CustomersBusinessScale.Include("CustomersBusinessRanking").Where(s => s.CustomersBusinessRanking.ID == id).ToList();

            return scale;
        }

        public static List<CustomersBusinessScale> SelectBusinessScaleByRankingID(int id, FBDEntities entities)
        {
            
            var scale = entities.CustomersBusinessScale.Include("CustomersBusinessRanking").Where(s => s.CustomersBusinessRanking.ID == id).ToList();

            return scale;
        }
        /// <summary>
        /// return the business specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the business</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>business</returns>
        public static CustomersBusinessScale SelectBusinessScaleByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var business = entities.CustomersBusinessScale.First(i => i.ID == id);
            return business;
        }


        /// <summary>
        /// add new business
        /// </summary>
        /// <param name="business">the business to add</param>
        public static int AddBusinessScale(CustomersBusinessScale ranking)
        {
            if (ranking == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToCustomersBusinessScale(ranking);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new business
        /// </summary>
        /// <param name="business">the business to add</param>
        public static int AddBusinessScale(CustomersBusinessScale ranking, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;


            entities.AddToCustomersBusinessScale(ranking);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new business
        /// </summary>
        /// <param name="business">the business to add</param>
        public static int AddBusinessScaleList(List<CustomersBusinessScale> scale, FBDEntities entities)
        {
            foreach (CustomersBusinessScale item in scale)
            {
                entities.AddToCustomersBusinessScale(item);
            }

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit new business ranking
        /// </summary>
        /// <param name="business">the business to add</param>
        public static int EditBusinessScale(CustomersBusinessScale ranking, FBDEntities entities)
        {
            if (ranking == null || entities == null) return 0;

            DatabaseHelper.AttachToOrGet<CustomersBusinessScale>(entities, ranking.GetType().Name, ref ranking);

            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(ranking);
            stateEntry.SetModified();

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// delete the Ranking with the specified rankingID
        /// </summary>
        /// <param name="rankingID"> the rankingID deleted</param>
        public static int DeleteBusinessScale(int id)
        {

            FBDEntities entities = new FBDEntities();
            var ranking = CustomersBusinessScale.SelectBusinessScaleByID(id, entities);
            entities.DeleteObject(ranking);
            int temp = entities.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }
        public class CustomersBusinessScaleMetaData
        {
        		
        	[DisplayName("ID")]
        	[Required]
            public int ID { get; set; }
        		
        	[DisplayName("Value")]
        	[StringLength(255)]
            public string Value { get; set; }
        		
        	[DisplayName("Score")]
            public Nullable<decimal> Score { get; set; }
        }
    }
}
