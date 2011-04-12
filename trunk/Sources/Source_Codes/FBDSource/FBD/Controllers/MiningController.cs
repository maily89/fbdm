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
    public class MiningController : Controller
    {
        //
        // GET: /Mining/
        /// <summary>
        /// this process include:
        /// 1. Get data from DB
        /// 2. Process mining
        /// 3. Update rank in DB
        /// 4. Display it into page
        /// </summary>
        /// <returns>index view</returns>
       
        public ActionResult Index()
        {
            
            List<Vector> vList = CustomersBusinessRanking.SelectBusinessRankingToVector();
            List<Vector> centroidList = new List<Vector>();
            //check success!!!
            //1 mining
                //a. Create initial centroid vector
            for (int i = 0; i < Constants.level.Length-1; i++)
            {
                double centroidX = (Constants.level[i] + Constants.level[i+1])/2;
                Vector v = new Vector(centroidX, 0);
                centroidList.Add(v);
            }
                //b. Create list result to save result
            List<Vector>[] result = new List<Vector>[centroidList.Count];

            result = KMean.Clustering(centroidList.Count, vList, null);
            result = Caculator.bubbleSort(result);
            //2. updating
            //this process will process by user. They can choose update or not

        /*    for (int i = 0; i < centroidList.Count; i++)
            {
                foreach (Vector v in result[i])
                {
                    CustomersBusinessRanking.UpdateBusinessRanking(v.ID, i.ToString());
                }
            }
         * 
         * */
            //3. Copy list vector to businessranking viewModel with nesseccary information???


            //4. Store list ranking into a session

            //I'm using another method: load again from DB
          //  List<CustomersBusinessRanking> ListView = CustomersBusinessRanking.SelectBusinessRankings();
            //List<int> chartList = new List<int>();
            //chartList.Add(1);
            //chartList.Add(2);
            //chartList.Add(6);
            //chartList.Add(5);
            //chartList.Add(4);
            int nCen =centroidList.Count;
            for (int i = 0; i < nCen; i++)
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
  
    }
}
