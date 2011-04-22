using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(CustomersLoanTermMetaData))]
    public partial class CustomersLoanTerm
    {
        /// <summary>
        /// list of CustomersLoanTerm
        /// </summary>
        /// <returns>list of CustomersLoanTerm</returns>
        public static List<CustomersLoanTerm> SelectLoanTerms()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersLoanTerm.OrderBy(m => m.LoanTermID).ToList();
        }

        /// <summary>
        /// return the loanTerm specified by id
        /// </summary>
        /// <param name="id">id of the loanTerm</param>
        /// <returns>loanTerm</returns>
        public static CustomersLoanTerm SelectLoanTermByID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            try
            {
                FBDEntities entities = new FBDEntities();
                var loanTerm = entities.CustomersLoanTerm.First(i => i.LoanTermID == id);

                return loanTerm;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        public static bool IsIDExist(string id)
        {
            FBDEntities entities = new FBDEntities();

            return entities.CustomersLoanTerm.Where(i => i.LoanTermID == id).Any();
        }

        /// <summary>
        /// return the loanTerm specified by id
        /// </summary>
        /// <param name="id">id of the loanTerm</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>loanTerm</returns>
        public static CustomersLoanTerm SelectLoanTermByID(string id, FBDEntities entities)
        {
            if (string.IsNullOrEmpty(id) || entities == null) return null;
            try
            {
                var loanTerm = entities.CustomersLoanTerm.First(i => i.LoanTermID == id);
                return loanTerm;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        /// delete the loanTerm with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteLoanTerm(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;
            FBDEntities entities = new FBDEntities();
            var loanTerm = CustomersLoanTerm.SelectLoanTermByID(id, entities);
            entities.DeleteObject(loanTerm);
            int temp = entities.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the loanTerm
        /// </summary>
        /// <param name="loanTerm">update the loanTerm</param>
        public static int EditLoanTerm(CustomersLoanTerm loanTerm)
        {
            if (loanTerm == null) return 0;
            FBDEntities entities = new FBDEntities();
            var temp = CustomersLoanTerm.SelectLoanTermByID(loanTerm.LoanTermID, entities);
            temp.LoanTermName = loanTerm.LoanTermName;
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new loanTerm
        /// </summary>
        /// <param name="loanTerm">the loanTerm to add</param>
        public static int AddLoanTerm(CustomersLoanTerm loanTerm)
        {
            if (loanTerm == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToCustomersLoanTerm(loanTerm);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }
        public class CustomersLoanTermMetaData
        {
        		
        	[DisplayName("Loan Term ID")]
        	[Required]
        	[StringLength(10)]
            public string LoanTermID { get; set; }
        		
        	[DisplayName("Loan Term Name")]
        	[StringLength(255)]
            public string LoanTermName { get; set; }
        }
    }
}
