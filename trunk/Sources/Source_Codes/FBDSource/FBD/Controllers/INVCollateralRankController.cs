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
        /// <returns>index view</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            List<IndividualCollateralRanks> ranks = null;
            try
            {
                ranks = IndividualCollateralRanks.SelectRanks();
                if (ranks == null) throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_COLL_RANK);
            }

            return View(ranks);
        }

        //
        // GET: /INVCollateralRank/Add
        /// <summary>
        /// create a add form 
        /// </summary>
        /// <returns>add view</returns>
        public ActionResult Add()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            TempData[Constants.SCC_MESSAGE] = null;
            TempData[Constants.SCC_MESSAGE] = null;
            return View();
        }

        //
        // POST: /INVCollateralRank/Add
        /// <summary>
        /// Insert information from add form to DB
        /// </summary>
        /// <param name="rank">rank object</param>
        /// <returns>index view</returns>
        [HttpPost]
        public ActionResult Add(IndividualCollateralRanks CollateralRank)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (IndividualCollateralRanks.AddRank(CollateralRank) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.INV_COLL_RANK);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();

            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.INV_COLL_RANK);
                return View(CollateralRank);
            }
        }

        //
        // GET: /INVCollateralRank/Edit/5
        /// <summary>
        /// create edit form
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>edit form</returns>
        public ActionResult Edit(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_COLL_RANK);
            }

            return View(model);
        }

        //
        // POST: /INVCollateralRank/Edit/5
        /// <summary>
        /// select edited information from edit page to update into db
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="rank">updated rank</param>
        /// <returns>index</returns>
        [HttpPost]
        public ActionResult Edit(string id, IndividualCollateralRanks CollateralRank)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {

                if (ModelState.IsValid)
                {
                    if (IndividualCollateralRanks.EditRank(CollateralRank) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_COLL_RANK, id);
                        return RedirectToAction("Index");
                    }

                }
                throw new ArgumentException();

            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_COLL_RANK);
                return View(CollateralRank);
            }
        }

        //
        // GET: /INVCollateralRank/Delete/5
        /// <summary>
        /// Delete a selected rank
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>index</returns>
        public ActionResult Delete(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                int result = IndividualCollateralRanks.DeleteRank(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.INV_COLL_RANK);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.INV_COLL_RANK);
                return RedirectToAction("Index");

            }
        }


    }
}
