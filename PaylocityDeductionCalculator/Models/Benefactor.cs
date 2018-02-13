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
        public decimal AnnualPrice { get; set; }
        public decimal PeriodPrice { get; set; }
        public int NumPayPeriods { get; set; }

        public Benefactor()
        {
            
           
        }

        public void UpdatePrices()
        {
            AnnualPrice = GetAnnualPrice();
            PeriodPrice = GetPeriodPrice();
        }

        public decimal GetPeriodPrice() 
        {
            if(NumPayPeriods == 0){
                throw new DivideByZeroException("number of pay periods cannot be zero");
            }
            return GetAnnualPrice() / NumPayPeriods;
        }

        public decimal GetAnnualPrice()
        {
            return UnitPrice - UnitPrice * Discount;
        }


        public override string ToString()
        {
            
            return String.Format("" + LastName + ", " + FirstName);
        }

    }
}