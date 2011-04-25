using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.CommonUtilities;

namespace FBD.Controllers
{
    public class INVMiningByStepController : Controller
    {
        //
        // GET: /INVMiningByStep/
        /// <summary>
        /// this code use for mining by step of individual to cluster Rank
        /// </summary>
        static int numOfCentroid = IndividualClusterRanks.SelectClusterRank().Count;
        static List<Vector>[] result = new List<Vector>[numOfCentroid];
        static List<Vector> vList = new List<Vector>();
        static Vector[] centroidArr = new Vector[numOfCentroid];
        static double sumDistance = 999999999999;
        public ActionResult Index()
        {
            return View();
            
        }
        /// <summary>
        /// initialize
        /// </summary>
        /// <returns></returns>
        public ActionResult Initialize()
        {
            vList = CustomersIndividualRanking.SelectIndividualRankingToVector();
            numOfCentroid = 1;//because it is initialize
            ViewData["cluster"] = numOfCentroid.ToString();
            //b. Create list result to save result=

            List<Vector> centroidList = new List<Vector>();
            ViewData["0"] = vList;
            ViewData["newCentroid"] = centroidList;
            ViewData["oldCentroid"] = centroidList;
            return View("NextStep"); 
        }

        public ActionResult NextStep()
        {
            //vList = CustomersIndividualRanking.SelectIndividualRankingToVector();
            //if, this is the 1st click on next step, numOfCentroid =1
            //numOfCentroid = int.Parse(ViewData["cluster"].ToString());
            //List<Vector> centroidList = (List<Vector>) ViewData["centroidList"];
            if (numOfCentroid == 1)
            {
                numOfCentroid = IndividualClusterRanks.SelectClusterRank().Count;
                //check conditional for mining
                //if (numOfCentroid > vList.Count)
                //    throw new Exception();
                
                //intitialzie centroid for first time, in this situation, there are no previous centroidList
                
                for (int i = 0; i < numOfCentroid; i++)
                {
                    centroidArr[i]= vList.ElementAt(i);
                    result[i] = new List<Vector>();

                }
            }
            //store number of cluster
           
            
            //b. Create list result to save result


      //      result = KMean.Clustering(numOfCentroid, vList, null);
            
            #region Cluster
            for (int i = 0; i < numOfCentroid; i++)
            {
                result[i].Clear();
            }
            //2. bring each vector in V to k list.
            foreach (Vector v in vList)
            {
                int min = Caculator.minDistant(v, centroidArr);
                result[min].Add(v);
            }

            //3.recaculator centroid
            //System.Console.WriteLine("Centroid:");
            //--> new solution, using sumCentroid to check  stop conditional
            #endregion

            if (result == null)
            {
                ViewData["cluster"] = "0";
                return View();

            }
            //this is old centroid
            ViewData["oldCentroid"] = centroidArr.ToList();
            List<Vector> newCentroidList = new List<Vector>();
            for (int i = 0; i < numOfCentroid; i++)
            {
                Vector vCentroid = Caculator.centroid(result[i]);
                centroidArr[i] = new Vector(vCentroid);
                newCentroidList.Add(vCentroid);
                ViewData[i.ToString()] = result[i];
            }
            if (sumDistance > Caculator.sumDistance(result))
            {
                sumDistance = Caculator.sumDistance(result);
            }
            else
            {
                ViewData["done"] = "true";
            }
            ViewData["sumDistance"] = sumDistance;
            ViewData["newCentroid"] = newCentroidList;

            ViewData["cluster"] = numOfCentroid.ToString();//store for next step
            return View();
        }
        public ActionResult Run()
        {
           return RedirectToAction("Cluster", "INVMining", new { fromDate = "2010-1-1", toDate = "2012-1-1" });
        }
    }
}
