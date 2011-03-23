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
    //TODO: Paging for customers
    public class RNKRankBusinessController : Controller
    {
        //
        // GET: /RKNRankBusiness/

        public ActionResult Index()
        {
            List<CustomersBusinessRanking> model = null;
            try
            {
                model = CustomersBusinessRanking.SelectBusinessRankings();
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_BUSINESS_RANKING);

            }
            return View(model);
        }


        //
        // GET: /RKNRankBusiness/Create

        public ActionResult AddStep1()
        {
            //TODO: check branch
            var model = new RNKPeriodViewModel();
            try
            {
                model.ReportingPeriods = SystemReportingPeriods.SelectReportingPeriods();
                model.BusinessCustomer = CustomersBusinesses.SelectBusinesses();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_REPORTING_PERIOD);
            }
            return View(model);
        }

        //
        // Post: /RKNRankBusiness/Add
        [HttpPost]
        public ActionResult AddStep1(RNKPeriodViewModel data)
        {
            var model = new RNKPeriodViewModel();

            if (data.CustomerID>0 && !string.IsNullOrEmpty(data.PeriodID))
            {
                var ranking = CustomersBusinessRanking.SelectRankingByPeriodAndCustomer(data.PeriodID, data.CustomerID);
                RNKBusinessRankingViewModel addModel = new RNKBusinessRankingViewModel();
                if (ranking == null)
                {
                    ranking = new CustomersBusinessRanking();
                    addModel.IsNew = true;
                }
                else addModel.IsNew = false;
                addModel.BusinessRanking = ranking;
                ranking.DateModified = DateTime.Now;
                addModel.PeriodID = data.PeriodID;
                var customer = CustomersBusinesses.SelectBusinessByID(data.CustomerID);
                addModel.CIF = customer.CIF;
                return View("Add",addModel);
            }

            //else: ERROR
            model.ReportingPeriods = SystemReportingPeriods.SelectReportingPeriods();
            model.BusinessCustomer = CustomersBusinesses.SelectBusinesses();
            TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.SYSTEM_REPORTING_PERIOD);
            
            return View(model);
        } 
        //
        // POST: /RKNRankBusiness/Create

        [HttpPost]
        public ActionResult Add(RNKBusinessRankingViewModel data)
        {
            int id;
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var ranking = data.BusinessRanking;
                    ranking.BusinessIndustries = BusinessIndustries.SelectIndustryByID(data.IndustryID, entity);
                    ranking.BusinessTypes = BusinessTypes.SelectTypeByID(data.TypeID, entity);
                    ranking.CustomersLoanTerm = CustomersLoanTerm.SelectLoanTermByID(data.LoanID, entity);
                    ranking.SystemReportingPeriods = SystemReportingPeriods.SelectReportingPeriodByID(data.PeriodID, entity);
                    //line.BusinessIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessIndustries", "IndustryID", data.IndustryID);
                    CustomersBusinessRanking.AddBusinessRanking(ranking, entity);
                    id=ranking.ID;
                }
                else throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_BUSINESS_RANKING);
                return View("AddScore", ScaleList(id));
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS_RANKING);
                
                return View(data);
            }
        }

        private List<RNKScaleRow> ScaleList(int id)
        {
            var ranking=CustomersBusinessRanking.SelectBusinessRankingByID(id);

            var businessScale = BusinessScaleCriteria.SelectScaleCriteria();
            List<RNKScaleRow> scale = new List<RNKScaleRow>();
            foreach (BusinessScaleCriteria item in businessScale)
            {
                var temp = new RNKScaleRow();
                temp.CriteriaID = item.CriteriaID;
                temp.RankingID = id;
                scale.Add(temp);
            }
            return scale;
        }
        [HttpPost]
        public ActionResult AddScore(List<RNKScaleRow> model){
            return null;
        }
        //
        // GET: /RNKRank/Delete/5
        /// <summary>
        /// Delete rank
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                int result = CustomersBusinessRanking.DeleteBusinessRanking(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.CUSTOMER_BUSINESS_RANKING);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.CUSTOMER_BUSINESS_RANKING);
                return RedirectToAction("Index");
            }

        }
    }
}
