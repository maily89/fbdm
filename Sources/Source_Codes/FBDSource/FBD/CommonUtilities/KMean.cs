using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class KMean
    {
        
        //number of clustering
        public int K { get; set; }
        List<Vector> V { get; set; }

        public KMean(int k, List<Vector> V)
        {
            this.K = k;
            this.V = V;
        }
        //we should set k in constant
        public static List<Vector>[] Clustering(int k, List<Vector> V,List<Vector> centroidlist)
        {
            List<Vector>[] result = new List<Vector>[k];
            //to do: separe List<Vector> V in to k List.
            
            //0. check input available
            //if (k > V.Count)
            //{
            //    System.Console.WriteLine("Wrong input");
            //    return null;
            //}

            //1. initialing k List and k centroid
            Vector[] previousCentroid = new Vector[k];
            Vector[] centroid = new Vector[k];//this array contain array centroid of each list 
            for (int i = 0; i < k; i++)
            {
                result[i] = new List<Vector>();
                if (centroidlist != null)
                {
                        centroid[i] = centroidlist.ElementAt(i);
                }
                else
                {
                    //need a new choose for k first point
                    centroid[i] = V.ElementAt(i);
                }
            }
            
            while (!Caculator.areTheSame(previousCentroid, centroid))
            {
                //reset k list
                for (int i = 0; i < k; i++)
                {
                    result[i].Clear();
                    //previousCentroid[i] = new Vector();
                    //need a new choose for k first point
                    //centroid[i] = V.ElementAt(i);
                }
                //2. bring each vector in V to k list.
                foreach (Vector v in V)
                {
                    int min = Caculator.minDistant(v, centroid);
                    result[min].Add(v);
                }

                //3.recaculator centroid
                //System.Console.WriteLine("Centroid:");
                for (int i = 0; i < k; i++)
                {
                    previousCentroid[i] = new Vector(centroid[i]);
                    //previousCentroid[i].copyFrom(centroid[i]);
                    centroid[i] = Caculator.centroid(result[i]);
                    //System.Console.WriteLine(centroid[i]);
                    //foreach (Vector j in result[i])
                    //{
                    //    System.Console.WriteLine(j + "belong to cluster" + i);
                    //}
                }
            }

            return result;
        }

        public static List<Vector>[] Clustering(int k, List<Vector> V)
        {
            return Clustering(k, V, null);
        }
    }
}
