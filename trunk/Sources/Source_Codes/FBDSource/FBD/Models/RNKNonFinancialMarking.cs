using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects;
using FBD.CommonUtilities;

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

            ranking.NonFinancialScore = finalScore/100;
            entities.SaveChanges();
            return finalScore/100;


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

    }
}