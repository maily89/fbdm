using System;
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
    //TODO: check rank name and id unique
    public class INVCollateralRankController : Controller
    {
        //
        // GET: /INVCollateralRank/
        /// <summary>
        /// Display list of Rank
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<IndividualCollateralRanks> ranks = null;
            try
            {
                ranks = IndividualCollateralRanks.SelectRanks();
                if (ranks == null) throw new Exception();
            }
            catch
            {
                TempData["Message"] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_RANK);
            }

            return View(ranks);
        }

        //
        // GET: /INVCollateralRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            TempData["Message"] = null;
            return View();
        }

        //
        // POST: /INVCollateralRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(IndividualCollateralRanks CollateralRank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IndividualCollateralRanks.AddRank(CollateralRank) == 1)
                    {
                        TempData["Message"] = string.Format(Constants.SCC_ADD, Constants.BUSINESS_RANK);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();

            }
            catch (Exception e)
            {
                TempData["Message"] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_RANK);
                return View(CollateralRank);
            }
        }

        //
        // GET: /INVCollateralRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            IndividualCollateralRanks model = null;
            try
            {
                model = IndividualCollateralRanks.SelectRankByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData["Message"] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_RANK);
            }

            return View(model);
        }

        //
        // POST: /INVCollateralRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, IndividualCollateralRanks CollateralRank)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (IndividualCollateralRanks.EditRank(CollateralRank) == 1)
                    {
                        TempData["Message"] = string.Format(Constants.SCC_EDIT_POST, Constants.BUSINESS_RANK, id);
                        return RedirectToAction("Index");
                    }

                }
                throw new ArgumentException();

            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_RANK);
                return View(CollateralRank);
            }
        }

        //
        // GET: /INVCollateralRank/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            try
            {
                int result = IndividualCollateralRanks.DeleteRank(id);
                if (result == 1)
                {
                    TempData["Message"] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_RANK);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch
            {
                TempData["Message"] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_RANK);
                return RedirectToAction("Index");

            }
        }


    }
}
