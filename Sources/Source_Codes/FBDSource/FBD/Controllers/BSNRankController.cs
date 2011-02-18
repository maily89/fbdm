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
    //TODO: check rank name and id unique
    public class BSNRankController : Controller
    {
        //
        // GET: /BSNRank/
        /// <summary>
        /// Display list of Rank
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var ranks = BusinessRanks.SelectRanks();

            return View(ranks);
        }

        //
        // GET: /BSNRank/Details/5
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
        // GET: /BSNRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /BSNRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(BusinessRanks rank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessRanks.AddRank(rank);
                }
                else throw new Exception();
                TempData["Message"] = "Rank ID " + rank.RankID + " have been added sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(rank);
            }
        }

        //
        // GET: /BSNRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            var model = BusinessRanks.SelectRankByID(id);
            return View(model);
        }

        //
        // POST: /BSNRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessRanks rank)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    BusinessRanks.EditRank(rank);

                }
                else throw new ArgumentException();
                TempData["Message"] = "RankID " + rank.RankID + " have been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = ex.Message;
                return View(rank);
            }
        }

        //
        // GET: /BSNRank/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            BusinessRanks.DeleteRank(id);
            TempData["Message"] = "Rank ID " + id + " have been deleted sucessfully";
            return RedirectToAction("Index");
        }


    }
}
