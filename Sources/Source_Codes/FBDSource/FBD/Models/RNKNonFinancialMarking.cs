using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using FBD.CommonUtilities;
using FBD.ViewModels;

namespace FBD.Models
{

    public class RNKNonFinancialMarking
    {
        private class ParentIndex
        {
            internal string ParentID ;
            internal decimal TotalMark;
            internal Nullable<decimal> Proportion;
        }
        
        public static decimal CalculateNonFinancialScore(int rankingID,bool keepExistingLevel, FBDEntities entities)
        {
            //Step1: Load all nonFinancial score saved.
            CustomersBusinessRanking ranking=CustomersBusinessRanking.SelectBusinessRankingByID(rankingID,entities);
            ranking.BusinessIndustriesReference.Load();
            

            ranking.CustomersBusinessNonFinancialIndex.Load();
            ranking.BusinessTypesReference.Load();
            if (ranking.BusinessTypes == null) return 0;

            string typeID = ranking.BusinessTypes.TypeID;
            List<ParentIndex> parentList = new List<ParentIndex>();
            
            decimal finalScore = 0;
            //Step2: calculate LevelID for each nonFinancial score.
            foreach (CustomersBusinessNonFinancialIndex indexScore in ranking.CustomersBusinessNonFinancialIndex)
            {
                //get level
                if (indexScore.BusinessNonFinancialIndexLevelsReference.EntityKey != null && keepExistingLevel)
                {
                    indexScore.BusinessNonFinancialIndexLevelsReference.Load();
                    indexScore.BusinessNonFinancialIndexReference.Load();
                }
                else
                    GetLevel(indexScore, ranking, entities);

                //calculate score
                if (indexScore.BusinessNonFinancialIndexLevels != null)
                {
                    var proportion = BusinessNFIProportionByIndustry.SelectNonFinancialIndexProportionByIndustryAndIndex
                        (entities, ranking.BusinessIndustries.IndustryID, indexScore.BusinessNonFinancialIndex.IndexID);

                    Nullable<decimal> score=indexScore.BusinessNonFinancialIndexLevels.Score;
                    if (score != null && proportion != null && proportion.Proportion!=null)
                    {
                        string parentID = StringHelper.FindParentIndex(indexScore.BusinessNonFinancialIndex.IndexID);
                        
                        decimal prop = proportion.Proportion.Value;
                        AddToParrent(parentID,parentList, score.Value * prop,typeID);
                    }
                    
                }
                
            }
            foreach (ParentIndex item in parentList)
            {
                if (item.Proportion != null)
                {
                    finalScore += item.Proportion.Value * item.TotalMark;
                }
            }

            decimal totalProportion = BusinessRankingStructure.SelectRankingStructureByIndexAndAudit(Constants.RNK_STRUCTURE_FINANCIAL_INDEX, ranking.AuditedStatus).Percentage.Value;

            ranking.NonFinancialScore = finalScore*totalProportion/10000;

            entities.SaveChanges();
            return finalScore/10000;


        }

        private static void AddToParrent(string parentID,List<ParentIndex> parent, decimal p,string typeID)
        {
            FBDEntities entity=new FBDEntities();
            foreach (ParentIndex item in parent)
            {
                if (item.ParentID.Equals(parentID))
                {
                    item.TotalMark += p;
                    return;
                }
            }
            BusinessNFIProportionByType index=null;
            try
            {
                 index = BusinessNFIProportionByType.SelectProportionByTypeAndIndex(entity, parentID,typeID);
            }
            catch
            {
                return;
            }
            if (index != null)
            {
                ParentIndex newParent = new ParentIndex();
                newParent.ParentID = parentID;
                newParent.Proportion = index.Proportion;
                newParent.TotalMark = p;
                parent.Add(newParent);
            }
        }

        public static BusinessNonFinancialIndexLevels GetLevel(CustomersBusinessNonFinancialIndex indexScore,CustomersBusinessRanking ranking,FBDEntities entities)
        {
            indexScore.BusinessNonFinancialIndexReference.Load();
            var index=indexScore.BusinessNonFinancialIndex;


            List<BusinessNonFinancialIndexScore> scoreList = BusinessNonFinancialIndexScore
                .SelectScoreByIndustryByNonFinancialIndex(entities, ranking.BusinessIndustries.IndustryID, index.IndexID);

            foreach (BusinessNonFinancialIndexScore item in scoreList)
            {
                if (index.ValueType == "N") //numeric type
                {
                    decimal score = System.Convert.ToDecimal(indexScore.Value);
                    if (score >= item.FromValue && score <= item.ToValue)
                    {
                        item.BusinessNonFinancialIndexLevelsReference.Load();
                        indexScore.BusinessNonFinancialIndexLevels = item.BusinessNonFinancialIndexLevels;
                        return indexScore.BusinessNonFinancialIndexLevels;
                    }
                }
                else // character type
                {
                    if (indexScore.Value.Equals(item.FixedValue))
                    {
                        item.BusinessNonFinancialIndexLevelsReference.Load();
                        indexScore.BusinessNonFinancialIndexLevels = item.BusinessNonFinancialIndexLevels;
                        return indexScore.BusinessNonFinancialIndexLevels;
                    }
                }
            }
            return null;
        }


        public static void CalculateTempNonFinancial(int rankingID, List<FBD.ViewModels.RNKNonFinancialRow> rnkNonFinancial, out decimal totalScore, out decimal totalProportion)
        {
            totalScore = 0;
            totalProportion = 0;
            //Step1: Load all nonFinancial score saved.
            FBDEntities entities = new FBDEntities();
            CustomersBusinessRanking ranking = CustomersBusinessRanking.SelectBusinessRankingByID(rankingID, entities);
            ranking.BusinessIndustriesReference.Load();
            ranking.BusinessTypesReference.Load();

            if (ranking.BusinessIndustries == null || ranking.BusinessTypes == null) return;

            RNKNonFinancialRow currentParent=null;
            //Gia dinh rang cac score duoc sap xep tu cha => con(order by ID)
            foreach (RNKNonFinancialRow indexScore in rnkNonFinancial)
            {
                //get Score
                if (!indexScore.LeafIndex)
                {
                    // do cac index dc sap xep dung, cha luon dung truoc con
                    //do do index k0 phai la la se la current Parent cua all leaf index lien sau do
                    currentParent = indexScore;
                    var parentProp = BusinessNFIProportionByType.SelectProportionByTypeAndIndex(entities, currentParent.Index.IndexID, ranking.BusinessTypes.TypeID);
                    currentParent.Proportion = parentProp.Proportion!=null?parentProp.Proportion.Value:0;
                    
                }
                else
                {
                    GetScore(indexScore, ranking);

                    var proportion = BusinessNFIProportionByIndustry.SelectNonFinancialIndexProportionByIndustryAndIndex
                            (entities, ranking.BusinessIndustries.IndustryID, indexScore.Index.IndexID);
                    
                    decimal score = indexScore.CalculatedScore;
                    if (proportion != null && proportion.Proportion != null)
                    {
                        decimal tempProp = proportion.Proportion.Value;
                        indexScore.Proportion = currentParent.Proportion * tempProp / 100;
                        
                    }
                    indexScore.Result = indexScore.Proportion * indexScore.CalculatedScore/100;
                    totalScore += indexScore.Result;
                }
            }


            totalProportion = BusinessRankingStructure.SelectRankingStructureByIndexAndAudit(Constants.RNK_STRUCTURE_FINANCIAL_INDEX, ranking.AuditedStatus).Percentage.Value;


        }
        public static decimal GetScore(RNKNonFinancialRow indexScore, CustomersBusinessRanking ranking)
        {
            if (!indexScore.LeafIndex) return 0;
            FBDEntities entities = new FBDEntities();
            var index = BusinessNonFinancialIndex.SelectNonFinancialIndexByID(entities, indexScore.Index.IndexID);
            if (indexScore.Index.ValueType == "N")
            {

                List<BusinessNonFinancialIndexScore> scoreList = BusinessNonFinancialIndexScore
                    .SelectScoreByIndustryByNonFinancialIndex(entities, ranking.BusinessIndustries.IndustryID, index.IndexID);

                return GetScoreFromViewModel(indexScore, index, scoreList);
            }
            else
            {
                BusinessNonFinancialIndexScore score = BusinessNonFinancialIndexScore.SelectBusinessNonFinancialIndexScoreByScoreID(entities, indexScore.ScoreID);
                score.BusinessNonFinancialIndexLevelsReference.Load();
                indexScore.CalculatedScore = score.BusinessNonFinancialIndexLevels.Score;
                indexScore.Value = score.FixedValue;
                return indexScore.CalculatedScore;
            }
        }
        private static decimal GetScoreFromViewModel(RNKNonFinancialRow indexScore, BusinessNonFinancialIndex index, List<BusinessNonFinancialIndexScore> scoreList)
        {

            foreach (BusinessNonFinancialIndexScore item in scoreList)
            {

                if (indexScore.Score == null) return 0;
                decimal score = indexScore.Score.Value;
                if (score >= item.FromValue && score <= item.ToValue)
                {
                    item.BusinessNonFinancialIndexLevelsReference.Load();
                    if (item.BusinessNonFinancialIndexLevels != null)
                    {
                        indexScore.CalculatedScore = item.BusinessNonFinancialIndexLevels.Score;
                    }
                    else indexScore.CalculatedScore = 0;
                        return indexScore.CalculatedScore;
                    

                }


            }
            return 0;
        }
    }
}