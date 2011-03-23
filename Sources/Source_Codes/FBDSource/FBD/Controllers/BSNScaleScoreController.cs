using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
using Telerik.Web.Mvc;

namespace FBD.Controllers
{
    //TODO: check Rights
    //TODO: check scaleScore name and id unique
    public class BSNScaleScoreController : Controller
    {
        #region AjaxHandler

        [GridAction]
        public ActionResult IndexAjax(string IndustryID,string CriteriaID)
        {
            try
            {
                if (string.IsNullOrEmpty(IndustryID) 
                || string.IsNullOrEmpty(CriteriaID)) 
                return View(new GridModel(new List<BSNScaleScoreRow>()));

                //var model = new BSNScaleScoreViewModel();
                var scaleScores = BusinessScaleScore.SelectScaleScore(IndustryID, CriteriaID);
                //model.ScaleScore = scaleScores;

                return View(new GridModel<BSNScaleScoreRow>(CopyScaleScore(scaleScores)));
            }
            catch (Exception )
            {
                return View(new GridModel(new List<BusinessScaleScore>()));
            }
        }

        [HttpPost]
        [GridAction]
        public ActionResult Insert(string IndustryID, string CriteriaID)
        {
            if (string.IsNullOrEmpty(IndustryID) || string.IsNullOrEmpty(CriteriaID)) return View(new GridModel());

            //Create a new instance of the EditableCustomer class.
            BusinessScaleScore scaleScore = new BusinessScaleScore();

            //Perform model binding (fill the customer properties and validate it).
            if (TryUpdateModel(scaleScore))
            {
                //scaleScore.BusinessIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessIndustries", "IndustryID", IndustryID);
                //scaleScore.BusinessScaleCriteriaReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessScaleScore", "CriteriaID", CriteriaID);

                var entities=new FBDEntities();
                scaleScore.BusinessIndustries = BusinessIndustries.SelectIndustryByID(IndustryID, entities);
                scaleScore.BusinessScaleCriteria = BusinessScaleCriteria.SelectScaleCriteriaByID(CriteriaID, entities);
                BusinessScaleScore.AddScaleScore(scaleScore,entities);

            }
            List<BusinessScaleScore> scaleScores = BusinessScaleScore.SelectScaleScore(IndustryID, CriteriaID);
            
            //Rebind the grid
            return View(new GridModel<BSNScaleScoreRow>(CopyScaleScore(scaleScores)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult Update(string IndustryID, string CriteriaID,int id)
        {
            if (string.IsNullOrEmpty(IndustryID) || string.IsNullOrEmpty(CriteriaID)) return View(new GridModel());
            var entity =new FBDEntities();
            var scaleScore = BusinessScaleScore.SelectScaleScoreByID(id, entity);

            if (scaleScore != null)
            {
                //Perform model binding (fill the customer properties and validate it).
                if (TryUpdateModel(scaleScore))
                {
                
                    BusinessScaleScore.EditScaleScore(scaleScore);
                }
            }

            var scaleScores = BusinessScaleScore.SelectScaleScore(IndustryID, CriteriaID);

            //Rebind the grid
            return View(new GridModel<BSNScaleScoreRow>(CopyScaleScore(scaleScores)));
        }

        [HttpPost]
        [GridAction]
        public ActionResult Delete(string IndustryID, string CriteriaID,int id)
        {

            if (string.IsNullOrEmpty(IndustryID) || string.IsNullOrEmpty(CriteriaID)) return View(new GridModel());
            //Delete the customer
            BusinessScaleScore.DeleteScaleScore(id);


            var scaleScores = BusinessScaleScore.SelectScaleScore(IndustryID, CriteriaID);

            //Rebind the grid
            return View(new GridModel<BSNScaleScoreRow>(CopyScaleScore(scaleScores)));
        }
        #endregion
        //
        // GET: /BSNScaleScore/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //var scaleScores = BusinessScaleScore.SelectScaleScore();
            BSNScaleScoreViewModel model = new BSNScaleScoreViewModel();
            model.ScaleScore = null;
            model.Industry = BusinessIndustries.SelectIndustries();
            model.Criteria = BusinessScaleCriteria.SelectScaleCriteria();
            return View(model);
        }

        //
        // GET: /BSNScaleScore/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string IndustryID,string CriteriaID)
        {
            var scaleScores = BusinessScaleScore.SelectScaleScore(IndustryID,CriteriaID);
            BSNScaleScoreViewModel model = new BSNScaleScoreViewModel();

            model.ScaleScore = CopyScaleScore(scaleScores);
            model.Industry = BusinessIndustries.SelectIndustries();
            model.Criteria = BusinessScaleCriteria.SelectScaleCriteria();
            model.IndustryID = IndustryID;
            model.CriteriaID = CriteriaID;
            return View(model);
        }

        
        private List<BSNScaleScoreRow> CopyScaleScore(List<BusinessScaleScore> scaleScores)
        {
            if (scaleScores == null) return null;
            List<BSNScaleScoreRow> temp = new List<BSNScaleScoreRow>();
            foreach (BusinessScaleScore item in scaleScores)
            {
                var s = new BSNScaleScoreRow();
                s.FromValue = item.FromValue;
                s.ToValue = item.ToValue;
                s.ScoreID = item.ScoreID;
                s.Score = item.Score;
                temp.Add(s);
            }
            return temp;
        }

    }
}
