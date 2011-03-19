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
    public class INVSummaryRankController : Controller
    {
        //
        // GET: /INVSummaryRank/
        /// <summary>
        /// Display list of Rank
        /// </summary>
        /// <returns></returns>
        /// 
        FBDEntities FBDmodel = new FBDEntities();
        public ActionResult Index()
        {
            List<IndividualSummaryRanks> ranks = null;
            try
            {
                ranks = IndividualSummaryRanks.SelectRanks();
                if (ranks == null) throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.INV_SUMMARY_RANK);
            }

            return View(ranks);
        }

        //
        // GET: /INVSummaryRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            INVSummaryRankViewModel viewmodel = IndividualSummaryRanks.selectSummaryRankByBasicAndCollateral(FBDmodel, -1);
            return View(viewmodel);
        }

        //
        // POST: /INVSummaryRank/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(INVSummaryRankViewModel data)
        {

            IndividualSummaryRanks summaryRank = new IndividualSummaryRanks();
            //summaryRank.ID = int.Parse(form["summaryRanks.ID"]);
            //summaryRank.Evaluation = form["summaryRanks.Evaluation"];
            //summaryRank.IndividualBasicRanks = IndividualBasicRanks.SelectRankByID(form["Basic Rank"],FBDmodel);
            //summaryRank.IndividualCollateralRanks = IndividualCollateralRanks.SelectRankByID(form["Collateral Rank"],FBDmodel);
            summaryRank.Evaluation = data.Evaluation;
            summaryRank.IndividualBasicRanks = IndividualBasicRanks.SelectRankByID(data.basicRankID, FBDmodel);
            summaryRank.IndividualCollateralRanks = IndividualCollateralRanks.SelectRankByID(data.collateralRankID, FBDmodel);
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (IndividualSummaryRanks.AddRank(summaryRank,FBDmodel) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.INV_SUMMARY_RANK);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();

            }
            catch (Exception )
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.INV_SUMMARY_RANK);
                data.basicRanks = IndividualBasicRanks.SelectRanks();
                data.collateralRanks = IndividualCollateralRanks.SelectRanks();
                return View(data);
            }
        }

        //
        // GET: /INVSummaryRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            INVSummaryRankViewModel viewmodel = null;
            try
            {
                
                viewmodel  = IndividualSummaryRanks.selectSummaryRankByBasicAndCollateral(FBDmodel, id);
                
                if (viewmodel == null)
                {
                    throw new Exception();
                }
                //viewmodel.basicRankID = viewmodel.summaryRanks.IndividualBasicRanks.RankID;
                //viewmodel.collateralRankID = viewmodel.summaryRanks.IndividualCollateralRanks.RankID;
                
            }
            catch (Exception e)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.INV_SUMMARY_RANK);
            }
            
            return View(viewmodel);
        }

        //
        // POST: /INVSummaryRank/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, INVSummaryRankViewModel SummaryRankViewModel)
        {
            try
            {
                //IndividualSummaryRanks summaryRank = new IndividualSummaryRanks();
                

                if (ModelState.IsValid)
                {
                    if (IndividualSummaryRanks.EditRank(SummaryRankViewModel, id) == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.INV_SUMMARY_RANK, id);
                        return RedirectToAction("Index");
                    }

                }
                throw new ArgumentException();

            }
            catch (Exception)
            {
                //TODO: Temporary error handle.
                SummaryRankViewModel = IndividualSummaryRanks.selectSummaryRankByBasicAndCollateral(FBDmodel, id);
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.INV_SUMMARY_RANK);
                return View(SummaryRankViewModel);
            }
        }

        //
        // GET: /INVSummaryRank/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                int result = IndividualSummaryRanks.DeleteRank(id);
                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.INV_SUMMARY_RANK);
                    return RedirectToAction("Index");
                }
                throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.INV_SUMMARY_RANK);
                return RedirectToAction("Index");

            }
        }


    }
}
