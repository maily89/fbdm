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
    public class BSNMiningStepController : Controller
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
    }
}