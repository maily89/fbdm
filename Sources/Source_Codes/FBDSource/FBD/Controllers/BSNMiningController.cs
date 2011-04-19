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

            List<Vector> vList = CustomersBusinessRanking.SelectBusinessRankingToVector();
            numOfCentroid = BusinessClusterRanks.SelectClusterRank().Count;
            //List<Vector> centroidList = new List<Vector>();
            ////check success!!!
            ////1 mining
            //    //a. Create initial centroid vector
            //for (int i = 0; i < Constants.level.Length-1; i++)
            //{
            //    double centroidX = (Constants.level[i] + Constants.level[i+1])/2;
            //    Vector v = new Vector(centroidX, 0);
            //    centroidList.Add(v);
            //}

            //another way to decide the number of centroid
            /*
            List<BusinessClusterRanks> bcrl = BusinessClusterRanks.SelectClusterRank();
            int numOfCentroid = bcrl.Count();
            */
           
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
            //return View(result);
        }

        public ActionResult GetCustomerList()
        {
            List<Vector> vList = CustomersBusinessRanking.SelectBusinessRankingToVector();
            return View(vList);
        }

        [GridAction]
        public ActionResult _GetCustomerList()
        {
            return View(new GridModel(CustomersBusinessRanking.SelectBusinessRankingToVector()));
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
                return RedirectToAction("index");
        }
        public ActionResult LoadPartial()
        {
            return PartialView("Map");
        }
  
    }
}
