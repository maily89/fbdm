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
        public int ID;
        public string CustomerName;
        public int RankID;
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
        //public override string ToString()
        //{
        //    return ("X : " + x + "   Y : " + y);
        //}
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
