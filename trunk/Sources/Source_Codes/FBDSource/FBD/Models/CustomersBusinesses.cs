using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType(typeof(CustomersBusinessesMetaData))]
    public partial class CustomersBusinesses
    {
        public string DropdownDisplay
        {
            get { return string.Format("{0} {1}", _CIF, _CustomerName); }
        }
        /// <summary>
        /// list of CustomersBusinesses
        /// </summary>
        /// <returns>list of CustomersBusinesses</returns>
        public static List<CustomersBusinesses> SelectBusinesses()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersBusinesses.OrderBy(m => m.BusinessID).ToList();
        }

        /// <summary>
        /// return the Business specified by id
        /// </summary>
        /// <param name="id">id of the Business</param>
        /// <returns>Business</returns>
        public static CustomersBusinesses SelectBusinessByID(int id)
        {
            
            FBDEntities entities = new FBDEntities();
            var Business = entities.CustomersBusinesses.Include(Constants.TABLE_SYSTEM_BRANCHES).First(i => i.BusinessID == id);

            return Business;
        }

        /// <summary>
        /// return the Business specified by id
        /// </summary>
        /// <param name="id">id of the Business</param>
        /// <returns>Business</returns>
        public static List<CustomersBusinesses> SelectBusinessByBranchID(string branchID)
        {

            FBDEntities entities = new FBDEntities();
            var Business = entities.CustomersBusinesses.Include(Constants.TABLE_SYSTEM_BRANCHES)
                .Where(i => i.SystemBranches.BranchID == branchID).ToList();

            return Business;
        }


        /// <summary>
        /// return the Business specified by id
        /// </summary>
        /// <param name="id">id of the Business</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>Business</returns>
        public static CustomersBusinesses SelectBusinessByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var business = entities.CustomersBusinesses.Include(Constants.TABLE_SYSTEM_BRANCHES).First(i => i.BusinessID == id);
            return business;
        }

        /// <summary>
        /// delete the Business with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteBusiness(int id)
        {
            
            FBDEntities entities = new FBDEntities();
            var business = CustomersBusinesses.SelectBusinessByID(id, entities);
            entities.DeleteObject(business);
            int temp = entities.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the Business
        /// </summary>
        /// <param name="Business">update the Business</param>
        public static int EditBusiness(CustomersBusinesses business)
        {
            if (business == null) return 0;
            FBDEntities entities = new FBDEntities();
            var temp = CustomersBusinesses.SelectBusinessByID(business.BusinessID, entities);
            temp.CIF = business.CIF;
            temp.CustomerName = business.CustomerName;
            temp.SystemBranches = SystemBranches.SelectBranchByID(business.SystemBranches.BranchID, entities);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new Business
        /// </summary>
        /// <param name="Business">the Business to add</param>
        public static int AddBusiness(CustomersBusinesses business)
        {
            if (business == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToCustomersBusinesses(business);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new Business
        /// </summary>
        /// <param name="Business">the Business to add</param>
        public static int AddBusiness(CustomersBusinesses business,FBDEntities entities)
        {
            if (business == null || entities ==null) return 0;
            

            entities.AddToCustomersBusinesses(business);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }
        public class CustomersBusinessesMetaData
        {
        		
        	[DisplayName("Business ID")]
        	[Required]
            public int BusinessID { get; set; }
        		
        	[DisplayName("CIF")]
        	[Required]
            [StringLength(20)]
            public string CIF { get; set; }
        		
        	[DisplayName("Customer Name")]
        	[StringLength(255)]
            public string CustomerName { get; set; }
        }
    }
}
