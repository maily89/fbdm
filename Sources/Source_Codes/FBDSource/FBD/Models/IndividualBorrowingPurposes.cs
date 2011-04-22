﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualBorrowingPurposesMetaData))]
    public partial class IndividualBorrowingPurposes
    {
        /// <summary>
        /// Select all borrowing purpose in the table IndividualBorrowingPurposes
        /// </summary>
        /// <returns>List of borrowing purpose</returns>
        public static List<IndividualBorrowingPurposes> SelectBorrowingPPList()
        {
            FBDEntities FBDModel = new FBDEntities();
            List<IndividualBorrowingPurposes> lstBorrowingPurpose = null;
            lstBorrowingPurpose = FBDModel.IndividualBorrowingPurposes.ToList();
            return lstBorrowingPurpose;
        }

        /// <summary>
        /// Select the borrowing purpose in the table IndividualBorrowingPurposes with purposeID= ID from input
        /// </summary>
        /// <param name="id">string ID</param>
        /// <returns>IndividualBorrowingPurposes object</returns>
        public static IndividualBorrowingPurposes SelectBorrowingPPByID(string id)
        {
            if (id == null) return null;
            try
            {
                FBDEntities FBDModel = new FBDEntities();
                IndividualBorrowingPurposes IndividualBorrowingPurposes = null;
                IndividualBorrowingPurposes = FBDModel.IndividualBorrowingPurposes.First(pp => pp.PurposeID.Equals(id));
                return IndividualBorrowingPurposes;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        public static IndividualBorrowingPurposes SelectBorrowingPPByID(string id, FBDEntities FBDModel)
        {
            try
            {
                IndividualBorrowingPurposes IndividualBorrowingPurposes = null;
                IndividualBorrowingPurposes = FBDModel.IndividualBorrowingPurposes.First(pp => pp.PurposeID.Equals(id));
                return IndividualBorrowingPurposes;
            }
            catch (Exception)
            {
                
                return null;
            }
        }
        /// <summary>
        /// 1. Receive information from parameter
        /// 2. Insert new index into the Database
        /// 3. If successful, return 1 otherwise return 0
        /// </summary>
        /// <param name="BorrowingPurpose">A new BorrowingPurpose information</param>
        /// <returns>Result code, 1 indicates success and 0 indicates error</returns> 
        public static int AddBorrowingPP(IndividualBorrowingPurposes IndividualBorrowingPP)
        {
            FBDEntities FBDModel = new FBDEntities();
            FBDModel.AddToIndividualBorrowingPurposes(IndividualBorrowingPP);
            int temp = FBDModel.SaveChanges();
            //it won't work for mutil-update
            return temp <= 0 ? 0 : 1;

        }

        /// <summary>
        /// Edit borrowing purpose
        /// </summary>
        /// <param name="IndividualBorrowingPP"></param>
        /// <returns></returns>
        public static int EditBorowingPurpose(IndividualBorrowingPurposes IndividualBorrowingPP)
        {
            FBDEntities entities = new FBDEntities();
            var temp = SelectBorrowingPPByID(IndividualBorrowingPP.PurposeID, entities);//entities.IndividualBorrowingPurposes.First(pp => pp.PurposeID == IndividualBorrowingPP.PurposeID);
            temp.Purpose = IndividualBorrowingPP.Purpose;
            
            int result = entities.SaveChanges();
            
            return result <= 0 ? 0 : 1;
        }
        /// <summary>
        /// check there are any borrowing purpose with the name @purposeName
        /// </summary>
        /// <param name="PurposeName">Purpose name from user</param>
        /// <returns></returns>

        public static bool IsExistPurpose(string PurposeName)
        {
            FBDEntities FBDModel = new FBDEntities();
            return FBDModel.IndividualBorrowingPurposes.Where(p => p.Purpose.Equals(PurposeName)).Any();
        }

        public static int DeleteBorrowingPurpose(string id)
        {
            FBDEntities entities = new FBDEntities();
            var borrowingPP = SelectBorrowingPPByID(id, entities);//entities.IndividualBorrowingPurposes.First(pp => pp.PurposeID == id);
            entities.DeleteObject(borrowingPP);
            int temp = entities.SaveChanges();

            return temp <= 0 ? 0 : 1;
        }

        public class IndividualBorrowingPurposesMetaData
        {
            [DisplayName("Purpose ID")]
            [Required(ErrorMessage = "Borrowing purpose ID is required")]
            [StringLength(3)]
            public string PurposeID { get; set; }

            [DisplayName("Purpose ")]
            [Required(ErrorMessage = "Purpose is required")]
            [StringLength(255)]
            [RegularExpression(".{10,}", ErrorMessage = "purpose must have at least 10 characters")]
            public string Purpose { get; set; }
        }
    }
}
