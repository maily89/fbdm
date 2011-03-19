﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    //TODO: check Rights
    //TODO: check rankingStructure name and id unique
    public class BSNRankingStructureController : Controller
    {
        //
        // GET: /BSNRankingStructure/
        /// <summary>
        /// Display list of rankingStructure
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<BusinessRankingStructure> rankingStructure = null;
            try
            {
                rankingStructure = BusinessRankingStructure.SelectRankingStructures();
                if (rankingStructure.Count!= 4)
                {
                    BusinessRankingStructure.Reset();
                    rankingStructure = BusinessRankingStructure.SelectRankingStructures();
                }
                if (rankingStructure == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_RANKING_STRUCTURE);
            }

            return View(rankingStructure);
        }


        //
        // GET: /BSNRankingStructure/Edit/5
        /// <summary>
        /// Display edit form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            BusinessRankingStructure model = null;
            try
            {
                model = BusinessRankingStructure.SelectRankingStructureByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_RANKING_STRUCTURE);
            }
            return View(model);
        }

        //
        // POST: /BSNRankingStructure/Edit/5
        /// <summary>
        /// Update edit form and display result.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rankingStructure"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, BusinessRankingStructure rankingStructure)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int result = BusinessRankingStructure.EditRankingStructure(rankingStructure);

                    if (result == 1)
                    {
                        TempData["Message"] = string.Format(Constants.SCC_EDIT_POST, Constants.BUSINESS_RANKING_STRUCTURE, rankingStructure.ID);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_RANKING_STRUCTURE);
                return View(rankingStructure);
            }
        }




    }
}