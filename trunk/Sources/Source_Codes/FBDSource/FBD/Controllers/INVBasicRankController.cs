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
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
        /// Create a add form for user
        /// </summary>
        /// <returns> add view</returns>
        public ActionResult Add()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            TempData[Constants.SCC_MESSAGE] = null;
            return View();
        }

        //
        // POST: /INVBasicRank/Add
        /// <summary>
        /// Select infor from add form and insert it into db
        /// </summary>
        /// <param name="rank">basic rank object</param>
        /// <returns>index view</returns>
        [HttpPost]
        public ActionResult Add(IndividualBasicRanks BasicRank)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
        /// Create a edit form with selected basic rank
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>index view</returns>
        public ActionResult Edit(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
        /// Select edited information and update to db
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="rank">new rank</param>
        /// <returns>index view</returns>
        [HttpPost]
        public ActionResult Edit(string id, IndividualBasicRanks basicRank)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
                

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_BASIC_RANK);
                return View(basicRank);
            }
        }

        //
        // GET: /INVBasicRank/Delete/5
        /// <summary>
        /// Delete selected rank
        /// </summary>
        /// <param name="id"> ID</param>
        /// <returns>Index view</returns>
        public ActionResult Delete(string id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
