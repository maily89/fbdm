using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FBD.CommonUtilities;

namespace FBD.Models
{
    [MetadataType(typeof(CustomersIndividualsMetaData))]
    public partial class CustomersIndividuals
    {
        /// <summary>
        /// list of CustomersIndividuals
        /// </summary>
        /// <returns>list of CustomersIndividuals</returns>
        public static List<CustomersIndividuals> SelectIndividuals()
        {
            FBDEntities entities = new FBDEntities();
            return entities.CustomersIndividuals.OrderBy(m => m.IndividualID).ToList();
        }

        /// <summary>
        /// return the Individual specified by id
        /// </summary>
        /// <param name="id">id of the Individual</param>
        /// <returns>Individual</returns>
        public static CustomersIndividuals SelectIndividualByID(int id)
        {

            FBDEntities entities = new FBDEntities();
            var Individual = entities.CustomersIndividuals
                                            .Include(Constants.TABLE_SYSTEM_BRANCHES)
                                            .First(i => i.IndividualID == id);

            return Individual;
        }


        /// <summary>
        /// return the Individual specified by id
        /// </summary>
        /// <param name="id">id of the Individual</param>
        /// <param name="entities">fbd entity to select</param>
        /// <returns>Individual</returns>
        public static CustomersIndividuals SelectIndividualByID(int id, FBDEntities entities)
        {
            if (entities == null) return null;
            var individual = entities.CustomersIndividuals
                                            .Include(Constants.TABLE_SYSTEM_BRANCHES)
                                            .First(i => i.IndividualID == id);
            return individual;
        }

        /// <summary>
        /// delete the Individual with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteIndividual(int id)
        {

            FBDEntities entities = new FBDEntities();
            var individual = CustomersIndividuals.SelectIndividualByID(id, entities);
            entities.DeleteObject(individual);
            int temp = entities.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the Individual
        /// </summary>
        /// <param name="Individual">update the Individual</param>
        public static int EditIndividual(CustomersIndividuals individual)
        {
            if (individual == null) return 0;
            FBDEntities entities = new FBDEntities();
            var temp = CustomersIndividuals.SelectIndividualByID(individual.IndividualID, entities);
            temp.CIF = individual.CIF;
            temp.CustomerName = individual.CustomerName;
            temp.SystemBranches = SystemBranches.SelectBranchByID(individual.SystemBranches.BranchID, entities);
            int result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new Individual
        /// </summary>
        /// <param name="Individual">the Individual to add</param>
        public static int AddIndividual(CustomersIndividuals individual)
        {
            if (individual == null) return 0;
            FBDEntities entities = new FBDEntities();

            entities.AddToCustomersIndividuals(individual);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new Individual
        /// </summary>
        /// <param name="Individual">the Individual to add</param>
        public static int AddIndividual(CustomersIndividuals individual, FBDEntities entities)
        {
            if (individual == null || entities == null) return 0;


            entities.AddToCustomersIndividuals(individual);

            int temp = entities.SaveChanges();
            // return 0 if there is error, 1 otherwise
            return temp <= 0 ? 0 : 1;
        }
        public class CustomersIndividualsMetaData
        {
        		
        	[DisplayName("Individual ID")]
        	[Required]
            public int IndividualID { get; set; }
        		
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
