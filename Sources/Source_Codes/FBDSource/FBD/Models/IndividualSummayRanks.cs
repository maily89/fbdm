using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using FBD.ViewModels;

namespace FBD.Models
{
    [MetadataType(typeof(IndividualSummaryRanksMetaData))]
    public partial class IndividualSummaryRanks
    {
        /// <summary>
        /// list of IndividualSummaryRanks
        /// </summary>
        /// <returns>list of IndividualSummaryRanks</returns>
        
        public static List<IndividualSummaryRanks> SelectRanks()
        {
            FBDEntities entities = new FBDEntities();
            return entities.IndividualSummaryRanks.Include("IndividualBasicRanks").
                                                                                  Include("IndividualCollateralRanks").ToList();
        }

        /// <summary>
        /// return the rank specified by id
        /// </summary>
        /// <param name="id">id of the rank</param>
        /// <returns>rank</returns>
        public static IndividualSummaryRanks SelectRankByID(FBDEntities entities,int id)
        {

            var rank = entities.IndividualSummaryRanks.Include("IndividualBasicRanks").
                                                                                  Include("IndividualCollateralRanks").First(i => i.ID == id);
            return rank;
        }

      

        /// <summary>
        /// delete the rank with the specified id
        /// </summary>
        /// <param name="id"> the id deleted</param>
        public static int DeleteRank(int id)
        {
           // if (string.IsNullOrEmpty(id)) return 0;

            FBDEntities entities = new FBDEntities();
            var rank = IndividualSummaryRanks.SelectRankByID(entities,id);
            entities.DeleteObject(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// edit the rank
        /// </summary>
        /// <param name="rank">update the rank</param>
        public static int EditRank(INVSummaryRankViewModel rank, int ID)
        {
            if (rank == null) return 0;

            FBDEntities entities = new FBDEntities();

            var temp = SelectRankByID(entities,ID );
            temp.Evaluation = rank.Evaluation;
            temp.IndividualBasicRanks = IndividualBasicRanks.SelectRankByID(rank.basicRankID, entities);
            temp.IndividualCollateralRanks = IndividualCollateralRanks.SelectRankByID(rank.collateralRankID, entities);
            
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }

        /// <summary>
        /// add new rank
        /// </summary>
        /// <param name="rank">the rank to add</param>
        public static int AddRank(IndividualSummaryRanks rank)
        {
            if (rank == null) return 0;

            FBDEntities entities = new FBDEntities();
            entities.AddToIndividualSummaryRanks(rank);
            var result = entities.SaveChanges();
            return result <= 0 ? 0 : 1;
        }
        public static int AddRank(IndividualSummaryRanks rank, FBDEntities entities)
        {

            try
            {
                entities.AddToIndividualSummaryRanks(rank);
                var result = entities.SaveChanges();
                return result <= 0 ? 0 : 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

      //===============================================================  ------------------
        public static List<IndividualSummaryRanks> selectSummaryRankByBasicAndCollateral(FBDEntities FBDModel, string pmrBasicID, string prmCollateralID)
        {
            List<IndividualSummaryRanks> summaryRanks = FBDModel.IndividualSummaryRanks.Include("IndividualBasicRanks").
                                                                                  Include("IndividualCollateralRanks").
                                                                                  Where(s => s.IndividualBasicRanks.Equals(pmrBasicID)
                                                                                      && s.IndividualCollateralRanks.Equals(prmCollateralID)).ToList();
            return summaryRanks;
        }
        public static INVSummaryRankViewModel selectSummaryRankByBasicAndCollateral(FBDEntities FBDModel, int pmrID)
        {
            INVSummaryRankViewModel rankViewModel = new INVSummaryRankViewModel();

            List<IndividualBasicRanks> basicRanks = IndividualBasicRanks.SelectRanks();
            List<IndividualCollateralRanks> collateralRanks = IndividualCollateralRanks.SelectRanks();
            IndividualSummaryRanks summaryRank = null;
            if (pmrID >=0)
            {
                summaryRank = IndividualSummaryRanks.SelectRankByID( FBDModel,pmrID);//FBDModel.IndividualSummaryRanks.Include("IndividualBasicRanks").
                                                                                     // Include("IndividualCollateralRanks").
                                                                                      //Where(s => s.ID.Equals(pmrID)).First();
                
            }
                //rankViewModel.basicRankID = summaryRanks.IndividualBasicRanks.RankID;
            //rankViewModel.collateralRankID = summaryRanks.IndividualCollateralRanks.RankID;
            rankViewModel.basicRanks = basicRanks;
            rankViewModel.collateralRanks = collateralRanks;
            
            if (summaryRank != null)
            {
                rankViewModel.summaryRanks = summaryRank;
                rankViewModel.basicRankID = summaryRank.IndividualBasicRanks.RankID;
                rankViewModel.collateralRankID = summaryRank.IndividualCollateralRanks.RankID;
                rankViewModel.Evaluation = summaryRank.Evaluation;
            }
            return rankViewModel;
        }

        public static int AddCollateralIndexScore(FBDEntities FBDModel, INVSummaryRankViewModel viewModel)
        {
            //IndividualSummaryRanks SummaryRank = new IndividualSummaryRanks();

            //IndividualCollateralRanks CollateralRanks = new IndividualCollateralRanks();
            //IndividualBasicRanks BasicRanks = new IndividualBasicRanks();



            //SummaryRank.IndividualBasicRanks = viewModel.basicRanks();
            //SummaryRank.IndividualCollateralRanks = viewModel.

            //FBDModel.AddToIndividualCollateralIndexScore(CollateralIndexScore);
            //int temp = FBDModel.SaveChanges();

            //return temp <= 0 ? 0 : 1;
            return 1;
        }

        public class IndividualSummaryRanksMetaData
        {

            [DisplayName("Rank ID")]
            [Required]
            public int ID { get; set; }

            [DisplayName("Evaluation")]
            [StringLength(50)]
            public string Evaluation { get; set; }
        }
    }
}
