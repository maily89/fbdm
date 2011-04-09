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
    }
}
