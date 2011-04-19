using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.ViewModels;
using FBD.Models;
using FBD.CommonUtilities;
using Telerik.Web.Mvc;


namespace FBD.Controllers
{
    public class BSNMiningController : Controller
    {
        //
        // GET: /Mining/
        /// <summary>
        /// this process include:
        /// 1. Get data from DB
        /// 2. Process mining
        /// 3. Update rank in DB( this process will process later)
        /// 4. Display it into page
        /// </summary>
        /// <returns>index view</returns>
        static int numOfCentroid = BusinessClusterRanks.SelectClusterRank().Count;
        static List<Vector>[] result = new List<Vector>[numOfCentroid];
        public ActionResult Index()
        {

            if (!AccessManager.AllowAccess(Constants.RIGHT_PARAMETERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }

            List<SystemReportingPeriods> reportPeriodList = new List<SystemReportingPeriods>();
            SystemReportingPeriods srp = new SystemReportingPeriods();
            srp.PeriodID = "-1";
            srp.PeriodName = "--Please select a period report--";
            
            try
            {
                reportPeriodList = SystemReportingPeriods.SelectReportingPeriods();
                if (reportPeriodList == null)
                    throw new Exception();
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_LINE);
            }

            reportPeriodList.Insert(0,srp);
            return View(reportPeriodList);
        }

        public ActionResult Cluster(string ID)
        {
            List<Vector> vList = CustomersBusinessRanking.SelectBusinessRankingToVector(ID);
            numOfCentroid = BusinessClusterRanks.SelectClusterRank().Count;
           

            ViewData["cluster"] = numOfCentroid.ToString();
            //b. Create list result to save result


            result = KMean.Clustering(numOfCentroid, vList, null);
            if (result == null)
            {
                ViewData["cluster"] = "0";
                return View();
            }
            result = Caculator.bubbleSort(result);

            //3. Copy list vector to businessranking viewModel with nesseccary information???


            //4. Store list ranking into a session

            //I'm using another method: load again from DB
            //  List<CustomersBusinessRanking> ListView = CustomersBusinessRanking.SelectBusinessRankings();


            for (int i = 0; i < numOfCentroid; i++)
            {
                List<Vector> listV = Caculator.bubbleSort(result[i]);
                ViewData[i.ToString()] = listV;
            }

            return View();
        }
        public ActionResult GetCustomerList(string ID)
        {
            List<Vector> vList = CustomersBusinessRanking.SelectBusinessRankingToVector(ID);
            return View(vList);
        }

        [GridAction]
        public ActionResult _GetCustomerList(string ID)
        {
            return View(new GridModel(CustomersBusinessRanking.SelectBusinessRankingToVector(ID)));
        }
        /// <summary>
        /// update centroid list and customerBusinessRanking
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save()
        {
            if (Request.IsAjaxRequest())
            {
                FBDEntities entities = new FBDEntities();
                //select list businessRank
                List<BusinessClusterRanks> bcr = BusinessClusterRanks.SelectClusterRank(entities);
                //2. updating
                // this process will process by user. They can choose update or not

                for (int i = 0; i < numOfCentroid; i++)
                {
                    string bcrID = bcr[i].RankID;
                    Vector u = CommonUtilities.Caculator.centroid(result[i]);
                    //bcr[i].Centroid = Convert.ToDecimal(u.x);
                    //BusinessClusterRanks.EditRank(bcr[i], entities);
                    BusinessClusterRanks.UpdateCentroid(bcrID, u, entities);
                    foreach (Vector v in result[i])
                    {
                        CustomersBusinessRanking.UpdateBusinessRanking(v.ID, bcrID, bcr[i],entities);
                    }
                    List<Vector> listV = Caculator.bubbleSort(result[i]);
                    ViewData[i.ToString()] = listV;
                }
                ViewData["cluster"] = numOfCentroid.ToString();
                return Content("DONE");
            }
            else
                return RedirectToAction("Cluster");
        }
        public ActionResult LoadPartial()
        {
            return PartialView("Map");
        }
  
    }
}
