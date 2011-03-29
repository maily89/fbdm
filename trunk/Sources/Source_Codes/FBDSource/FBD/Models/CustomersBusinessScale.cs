using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using FBD.ViewModels;
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

        /// <summary>
        /// return the business specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the business</param>
        /// <returns>business</returns>
        public static CustomersBusinessScale SelectBusinessScaleByRankingIDAndCriteriaID(int rankingID, string criteriaID)
        {

            FBDEntities entities = new FBDEntities();
            CustomersBusinessScale business;
            try
            {
                business = entities.CustomersBusinessScale
                                    .Include("BusinessScaleCriteria")
                                    .Include("CustomersBusinessRanking")
                                    .First(i => i.CustomersBusinessRanking.ID == rankingID && i.BusinessScaleCriteria.CriteriaID == criteriaID);
                                    

            }
            catch
            {
                return null;
            }

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
        public static int AddBusinessScale(CustomersBusinessScale scale)
        {
            if (scale == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToCustomersBusinessScale(scale);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new business
        /// </summary>
        /// <param name="business">the business to add</param>
        public static int AddBusinessScale(CustomersBusinessScale scale, FBDEntities entities)
        {
            if (scale == null || entities == null) return 0;


            entities.AddToCustomersBusinessScale(scale);

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
        public static int EditBusinessScale(CustomersBusinessScale scale, FBDEntities entities)
        {
            if (scale == null || entities == null) return 0;

            DatabaseHelper.AttachToOrGet<CustomersBusinessScale>(entities, scale.GetType().Name, ref scale);

            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(scale);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<RNKScaleRow> ScaleList(int id)
        {
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id);

            var scaleCriteria = BusinessScaleCriteria.SelectScaleCriteria();
            List<RNKScaleRow> scale = new List<RNKScaleRow>();
            foreach (BusinessScaleCriteria item in scaleCriteria)
            {
                var temp = new RNKScaleRow();
                temp.CriteriaID = item.CriteriaID;
                temp.RankingID = id;
                temp.CriteriaName = item.CriteriaName;
                scale.Add(temp);
            }
            return scale;
        }

        public static List<RNKScaleRow> ScaleEditList(int id)
        {
            var entities = new FBDEntities();
            var ranking = CustomersBusinessRanking.SelectBusinessRankingByID(id, entities);

            var scaleCriteria = BusinessScaleCriteria.SelectScaleCriteria(entities);


            List<RNKScaleRow> scale = new List<RNKScaleRow>();
            foreach (BusinessScaleCriteria item in scaleCriteria)
            {
                var customerScale = CustomersBusinessScale.SelectBusinessScaleByRankingIDAndCriteriaID(id, item.CriteriaID);

                var temp = new RNKScaleRow();
                // if theres no current criteria, create new
                if (customerScale == null)
                {
                    customerScale = new CustomersBusinessScale();

                    customerScale.CustomersBusinessRanking = ranking;
                    customerScale.BusinessScaleCriteria = item;

                    CustomersBusinessScale.AddBusinessScale(customerScale, entities);
                }

                // copy customerScale to RNKScaleRow
                temp.CriteriaID = item.CriteriaID;
                temp.RankingID = id;
                temp.CriteriaName = item.CriteriaName;
                temp.Value = customerScale.Value;
                temp.CustomerScaleID = customerScale.ID;
                scale.Add(temp);
            }

            return scale;
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
