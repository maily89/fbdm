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
    public class INVBasicRankController : Controller
    {
        //
        // GET: /INVBasicRank/
        /// <summary>
        /// Display list of Rank
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<IndividualBasicRanks> ranks = null;
            try
            {
                ranks = IndividualBasicRanks.SelectRanks();
                if (ranks == null) throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_BASIC_RANK);
            }

            return View(ranks);
        }

        //
        // GET: /INVBasicRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /INVBasicRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(IndividualBasicRanks BasicRank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IndividualBasicRanks.AddRank(BasicRank) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.INV_BASIC_RANK);
                        return RedirectToAction("Index");
                    }
                }
               
                    throw new Exception();
              
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.INV_BASIC_RANK);
                return View(BasicRank);
            }
        }

        //
        // GET: /INVBasicRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            IndividualBasicRanks model = null;
            try
            {
                model = IndividualBasicRanks.SelectRankByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_BASIC_RANK);
            }

            return View(model);
        }

        //
        // POST: /INVBasicRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, IndividualBasicRanks basicRank)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (IndividualBasicRanks.EditRank(basicRank) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_BASIC_RANK, id);
                        return RedirectToAction("Index");
                    }

                }
                throw new ArgumentException();

            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_BASIC_RANK);
                return View(basicRank);
            }
        }

        //
        // GET: /INVBasicRank/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            try
            {
                int result = IndividualBasicRanks.DeleteRank(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.INV_BASIC_RANK);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.INV_BASIC_RANK);
                return RedirectToAction("Index");

            }
        }


    }
}
