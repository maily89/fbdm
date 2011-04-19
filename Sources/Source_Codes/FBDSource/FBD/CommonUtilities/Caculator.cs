using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class Caculator
    {
        public static double Distant(Vector a, Vector b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }

        public static Vector centroid(List<Vector> V)
        {
            double sumx = 0;
            double sumy = 0;
            //Vector fn = new Vector();
            foreach (Vector v in V)
            {
                sumx += v.x;
                sumy += v.y;
            }
            return new Vector(sumx / V.Count, sumy / V.Count);
        }
        public static int minDistant(Vector u, Vector[] V)
        {
            int min = 0;
            double mindis = Distant(u, V[0]);
            for (int i = 0; i < V.Length; i++)
            {
                if (Distant(u, V[i]) < mindis)
                {
                    min = i;
                    mindis = Distant(u, V[i]);
                }
            }

            return min;
        }

        public static bool areTheSame(Vector[] V, Vector[] U)
        {
            if (V == null || U == null || V.Length != U.Length)
                return false;
            else
            {
                for (int i = 0; i < V.Length; i++)
                {
                    if (V[i] == null || U[i] == null || !V[i].Equal(U[i]))
                        return false;
                }
                return true;
            }
        }

        public static List<Vector> bubbleSort(List<Vector> lst)
        {
            int n =lst.Count;
            //do
            //{
            //    int newn = 0;
                for (int i = n-1; i > 0; i--)
                {
                    for (int j = 0; j < i;j++ )
                        if (lst.ElementAt(j).x > lst.ElementAt(j + 1).x)
                        {
                            Vector temp = lst[j];
                            lst[j] = lst[j + 1];
                            lst[j+1] = temp;
                        }
                }
            //    n = newn;
            //}
            //while (n > 1);
            return (lst);
        }

        public static List<Vector>[] bubbleSort(List<Vector>[] lst)
        {
            int n = lst.Length;
            //do
            //{
            //    int newn = 0;
            for (int i = n - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                    if (Caculator.centroid(lst[j]).x > Caculator.centroid(lst[j+1]).x)
                    {
                        List<Vector> temp = lst[j];
                        lst[j] = lst[j + 1];
                        lst[j + 1] = temp;
                    }
            }
            //    n = newn;
            //}
            //while (n > 1);
            return (lst);
        }

        public static void swap(Vector x,  Vector y)
        {
            Vector temp = x;
            x = y;
            y = temp;
        }

        public static double sumDistance(List<Vector>[] result)
        {
            double sum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                Vector centr = centroid(result[i]);
                foreach (Vector v in result[i])
                    sum += Distant(v, centr);
            }
            return sum;
        }
    }
}
