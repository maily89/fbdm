using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;

namespace FBD.Models
{
    [MetadataType(typeof(CustomersFinancialIndexMetaData))]
    public partial class CustomersBusinessFinancialIndex
    {
        /// <summary>
        /// list of businessFinancialIndex
        /// </summary>
        /// <returns>list of businessFinancialIndex</returns>
        public static List<CustomersBusinessFinancialIndex> SelectFinancialIndex()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersBusinessFinancialIndex.ToList();
        }



        /// <summary>
        /// return the financialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the financialIndex</param>
        /// <returns>financialIndex</returns>
        public static CustomersBusinessFinancialIndex SelectFinancialIndexByID(int id)
        {
            FBDEntities entities = new FBDEntities();
            var financialIndex = entities.CustomersBusinessFinancialIndex.First(i => i.ID == id);
            return financialIndex;
        }

        /// <summary>
        /// return the financialIndex specified by rankingID
        /// </summary>
        /// <param name="rankingID">rankingID of the financialIndex</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>financialIndex</returns>
        public static CustomersBusinessFinancialIndex SelectFinancialIndexByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var financialIndex = entities.CustomersBusinessFinancialIndex.First(i => i.ID== id);
            return financialIndex;
        }

        /// <summary>
        /// delete the financialIndex with the specified rankingID
        /// </summary>
        /// <param name="rankingID"> the rankingID deleted</param>
        public static int DeleteFinancialIndex(int id)
        {
            FBDEntities entities = new FBDEntities();
            var financialIndex = CustomersBusinessFinancialIndex.SelectFinancialIndexByID(id, entities);
            entities.DeleteObject(financialIndex);
            return entities.SaveChanges() <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the financialIndex
        /// </summary>
        /// <param name="financialIndex">update the financialIndex</param>
        public static int EditFinancialIndex(CustomersBusinessFinancialIndex financialIndex, FBDEntities entities)
        {
            if (financialIndex == null || entities==null) return 0;

            
            DatabaseHelper.AttachToOrGet<CustomersBusinessFinancialIndex>(entities,financialIndex.GetType().Name,ref financialIndex);

            ObjectStateManager stateMgr = entities.ObjectStateManager;
            ObjectStateEntry stateEntry = stateMgr.GetObjectStateEntry(financialIndex);
            stateEntry.SetModified();

            int result = entities.SaveChanges();

            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new financialIndex
        /// </summary>
        /// <param name="financialIndex">the financialIndex to add</param>
        public static int AddFinancialIndex(CustomersBusinessFinancialIndex financialIndex)
        {
            if (financialIndex == null) return 0;
            //if (financialIndex. == null || financialIndex.CriteriaID == null) return 0;
            FBDEntities entities = new FBDEntities();
            entities.AddToCustomersBusinessFinancialIndex(financialIndex);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new financialIndex
        /// </summary>
        /// <param name="financialIndex">the financialIndex to add</param>
        public static int AddFinancialIndex(CustomersBusinessFinancialIndex financialIndex, FBDEntities entities)
        {
            if (financialIndex == null) return 0;
            //if (financialIndex. == null || financialIndex.CriteriaID == null) return 0;

            entities.AddToCustomersBusinessFinancialIndex(financialIndex);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        public class CustomersFinancialIndexMetaData
        {
        		
        	[DisplayName("ID")]
        	[Required]
            public int ID { get; set; }
        		
        	[DisplayName("Value")]
        	[StringLength(255)]
            public string Value { get; set; }
        }
    }
}
