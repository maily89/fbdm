using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;

namespace FBD.Controllers
{
    //TODO: check Rights
    //TODO: check scaleScore name and id unique
    public class BSNScaleScoreController : Controller
    {
        //
        // GET: /BSNScaleScore/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var scaleScores = BusinessScaleScore.SelectScaleScore();

            return View(scaleScores);
        }

        //
        // GET: /BSNScaleScore/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {

            return View();
        }

        //
        // GET: /BSNScaleScore/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /BSNScaleScore/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleScore"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(BusinessScaleScore scaleScore)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessScaleScore.AddScaleScore(scaleScore);
                }
                else throw new Exception();
                TempData["Message"] = "ScaleScore ID " + scaleScore.ScoreID + " have been added sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(scaleScore);
            }
        }

        //
        // GET: /BSNScaleScore/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = BusinessScaleScore.SelectScaleScoreByID(id);
            return View(model);
        }

        //
        // POST: /BSNScaleScore/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scaleScore"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessScaleScore scaleScore)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    BusinessScaleScore.EditScaleScore(scaleScore);

                }
                else throw new ArgumentException();
                TempData["Message"] = "ScaleScoreID " + scaleScore.ScoreID + " have been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = ex.Message;
                return View(scaleScore);
            }
        }

        //
        // GET: /BSNScaleScore/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            BusinessScaleScore.DeleteScaleScore(id);
            TempData["Message"] = "ScaleScore ID " + id + " have been deleted sucessfully";
            return RedirectToAction("Index");
        }


    }
}
