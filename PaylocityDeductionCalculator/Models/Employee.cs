using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class Employee : Benefactor
    {

        public List<Dependent> Dependents { get; set; }
        public decimal BiWeeklySalary { get; set; }
        public int NumPayPeriods { get; set; }


    }
}