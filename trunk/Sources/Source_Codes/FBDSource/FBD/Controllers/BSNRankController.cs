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
            List<BusinessRanks> ranks=null;
            try
            {
                ranks= BusinessRanks.SelectRanks();
                if (ranks == null) throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_RANK);
            }

            return View(ranks);
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
        public ActionResult Add(BusinessRanks businessRanks)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (BusinessRanks.AddRank(businessRanks) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.BUSINESS_RANK);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
                
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_RANK);
                return View(businessRanks);
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
            BusinessRanks model=null;
            try
            {
                model = BusinessRanks.SelectRankByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_RANK);
            }
            
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
        public ActionResult Edit(string id, BusinessRanks businessRanks)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (BusinessRanks.EditRank(businessRanks) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.BUSINESS_RANK, id);
                        return RedirectToAction("Index");
                    }

                }
                throw new ArgumentException();
                
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_RANK);
                return View(businessRanks);
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
            try
            {
                int result=BusinessRanks.DeleteRank(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_RANK);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_RANK);
                return RedirectToAction("Index");
            
            }
        }


    }
}
