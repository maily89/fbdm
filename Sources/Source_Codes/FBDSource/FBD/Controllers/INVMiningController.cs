using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.CommonUtilities;
using FBD.Models;

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

            List<Vector> vList = CustomersIndividualRanking.SelectIndividualRankingToVector();
            numOfCentroid = IndividualClusterRanks.SelectClusterRank().Count;
            //List<Vector> centroidList = new List<Vector>();
            ////Se co cach xac dinh la centroid list sau va nhat dinh, se tim hieu ki cai nay!!!
            ////check success!!!
            ////1 mining
            ////a. Create initial centroid vector
            //for (int i = 0; i < Constants.level.Length - 1; i++)
            //{
            //    double centroidX = (Constants.level[i] + Constants.level[i + 1]) / 2;
            //    Vector v = new Vector(centroidX, 0);
            //    centroidList.Add(v);
            //}
            // Another way to decide the number of centroid
            /*
            List<BusinessClusterRanks> bcrl = BusinessClusterRanks.SelectClusterRank();
            int numOfCentroid = bcrl.Count();
            */

            //int numOfCentroid = CommonUtilities.Constants.NumberOfInvCentroid;
            ViewData["cluster"] = numOfCentroid.ToString();
            //b. Create list result to save result
            

            result = KMean.Clustering(numOfCentroid, vList, null);
            if (result == null)
            {
                ViewData["cluster"] = "0";
                return View();
                
            }
            result = Caculator.bubbleSort(result);
            //2. updating
            //this process will process by user. They can choose update or not

            /*    for (int i = 0; i < centroidList.Count; i++)
                {
                    foreach (Vector v in result[i])
                    {
                        CustomersIndividualRanking.UpdateIndividualRanking(v.ID, i.ToString());
                    }
                }
             * 
             * */
            //3. Copy list vector to Individualranking viewModel with nesseccary information???


            //4. Store list ranking into a session

            //I'm using another method: load again from DB
            //  List<CustomersIndividualRanking> ListView = CustomersIndividualRanking.SelectIndividualRankings();

            
            for (int i = 0; i < numOfCentroid; i++)
            {
                List<Vector> listV = Caculator.bubbleSort(result[i]);
                ViewData[i.ToString()] = listV;
            }

            return View();
            //return View(result);
        }
        public ActionResult Save()
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
                    CustomersIndividualRanking.UpdateIndividualRanking(v.ID, i.ToString(), icrList[i]);
                }
                List<Vector> listV = Caculator.bubbleSort(result[i]);
                ViewData[i.ToString()] = listV;
            }
            ViewData["cluster"] = numOfCentroid.ToString();
            return View();
        }
    }
}
