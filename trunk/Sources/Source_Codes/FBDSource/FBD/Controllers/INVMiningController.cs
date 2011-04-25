using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.CommonUtilities;
using FBD.Models;
using Telerik.Web.Mvc;

namespace FBD.Controllers
{
    public class INVMiningController : Controller
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
        static int numOfCentroid = IndividualClusterRanks.SelectClusterRank().Count;
        static List<Vector>[] result = new List<Vector>[numOfCentroid];
        public ActionResult Index()
        {

            return View();

            //return View(result);
        }

        public ActionResult Cluster(string fromDate,string toDate)
        {
           
            string[] arrFr = fromDate.Split('-');
            string[] arrTo = toDate.Split('-');
            DateTime dFromDate = new DateTime(int.Parse(arrFr[0]),int.Parse(arrFr[1]),int.Parse(arrFr[2]));
            DateTime dToDate = new DateTime(int.Parse(arrTo[0]), int.Parse(arrTo[1]), int.Parse(arrTo[2]));
            List<Vector> vList = CustomersIndividualRanking.SelectIndividualRankingToVector(dFromDate, dToDate);
            numOfCentroid = IndividualClusterRanks.SelectClusterRank().Count;
            ViewData["cluster"] = numOfCentroid.ToString();
            //b. Create list result to save result


            result = KMean.Clustering(numOfCentroid, vList, null);
            if (result == null)
            {
                ViewData["cluster"] = "0";
                return View();

            }
            //3. Copy list vector to Individualranking viewModel with nesseccary information???


            //4. Store list ranking into a session

            //I'm using another method: load again from DB
            //  List<CustomersIndividualRanking> ListView = CustomersIndividualRanking.SelectIndividualRankings();

            List<Vector> centroidList = new List<Vector>();
            for (int i = 0; i < numOfCentroid; i++)
            {
                //List<Vector> listV = Caculator.bubbleSort(result[i]);
                Vector vCentroid = Caculator.centroid(result[i]);
                centroidList.Add(vCentroid);
                ViewData[i.ToString()] = result[i];
            }
            ViewData["centroidList"] = centroidList;
            return View();
        }
        /// <summary>
        /// implement save ajax function
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save()
        {
            if (Request.IsAjaxRequest())
            {
                FBDEntities entities = new FBDEntities();
                //2. updating
                // this process will process by user. They can choose update or not
                List<IndividualClusterRanks> icrList = IndividualClusterRanks.SelectClusterRank(entities);
                for (int i = 0; i < numOfCentroid; i++)
                {
                    string RankID = icrList[i].RankID;
                    Vector u = CommonUtilities.Caculator.centroid(result[i]);
                    IndividualClusterRanks.updateCentroid(RankID, u, entities);
                    foreach (Vector v in result[i])
                    {
                        if(!RankID.Equals(v.RankID.ToString()))
                        CustomersIndividualRanking.UpdateIndividualRanking(v.ID, RankID, icrList[i], entities);
                    }
                   // List<Vector> listV = Caculator.bubbleSort(result[i]);
                    ViewData[i.ToString()] = result[i];
                }
                ViewData["cluster"] = numOfCentroid.ToString();
                return Content("DONE");
            }
            else
                return RedirectToAction("Cluster");
        }
        public ActionResult ListCustomer(int ID)
        {
            List<Vector> listVector = result[ID];
            return View(listVector);
        }

        [GridAction]
        public ActionResult _ListCustomer(int ID)
        {
            return View(new GridModel(result[ID]));
        }
    }
}
