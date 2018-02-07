using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class Benefactor
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal PeriodPrice
        {
            get; set;
        }

        public override string ToString()
        {
            // choose any format that suits you and display what you like
            return String.Format("" + LastName + ", " + FirstName);
        }

    }
}