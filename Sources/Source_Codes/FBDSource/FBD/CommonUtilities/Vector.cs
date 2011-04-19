using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBD.Models;

namespace FBD.CommonUtilities
{
    public class Vector
    {
        public double x { set; get; }
        public double y { set; get; }

        //these properties just using for track id of customer who has score as this vector
        //Id of customerbusinessRanking table
        public int ID;
        //customerName in table CustomerBusinessRank
        public string CustomerName;
        //RankID of a customer in table CustomerBusinessRank
        public int RankID;
        //DateModified in table customerBusinessRank
        public DateTime modifiedDate;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector(Vector v)
        {
            this.x = v.x;
            this.y = v.y;
        }
        /// <summary>
        /// Create Vector using customerbusinessRank
        /// </summary>
        /// <param name="cbr">CustomersBusinessRanking</param>
        public Vector(CustomersBusinessRanking cbr)
        {

            this.x = Double.Parse(cbr.FinancialScore.ToString()) + Double.Parse(cbr.NonFinancialScore.ToString());
            this.y = 0;//Double.Parse(cbr.NonFinancialScore.ToString());
            this.ID = int.Parse(cbr.ID.ToString());
            this.CustomerName = cbr.CustomersBusinesses.CustomerName.ToString();
            if (cbr.BusinessRanks != null)
            {
                this.RankID = int.Parse(cbr.BusinessRanks.RankID.ToString());
            }
            if (cbr.DateModified != null)
            {
                this.modifiedDate = cbr.DateModified.Value;
            }
        }
        /// <summary>
        /// Create vector using customerIndividualRank information
        /// </summary>
        /// <param name="cir"></param>
        public Vector(CustomersIndividualRanking cir)
        {
            this.x = Convert.ToDouble(cir.CollateralIndexScore);
            this.y = Convert.ToDouble(cir.CollateralIndexScore);
            
            //more information use for display to customer.

        }

        /// <summary>
        /// vector create by business rank
        /// </summary>
        /// <param name="bcr">BusinessClusterRanks</param>
        public Vector(BusinessClusterRanks bcr)
        {
            this.x = Convert.ToDouble(bcr.Centroid);
            this.y = 0;
            this.RankID = int.Parse(bcr.RankID);
        }

        /// <summary>
        /// vector is created by individual cluster rank 
        /// </summary>
        /// <param name="icr"></param>
        public Vector(IndividualClusterRanks icr)
        {
            this.x = Convert.ToDouble(icr.CentroidX);
            this.y = Convert.ToDouble(icr.CentroidY);
            this.RankID = int.Parse(icr.RankID);
        }
        public bool Equal(Vector t)
        {
            if (t == null)
                return false;
            if (this.x == t.x && this.y == t.y)
                return true;
            else
                return false;
        }
    }
}
