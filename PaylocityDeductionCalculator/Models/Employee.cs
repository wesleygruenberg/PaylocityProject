using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class Employee : Benefactor
    {

        public List<Dependent> Dependents { get; set; }
        public decimal PaycheckAmount { get; set; }
        public int NumPayPeriods { get; set; }

        public Employee()
        {
            Dependents = new List<Dependent>();

        }

        public void AddDependent(Dependent dependent)
        {
            Dependents.Add(dependent);
        }

        public void RemoveDependent(Dependent dependent)
        {
            Dependents.Remove(dependent);
        }

        public void RemoveDependentAt(int i)
        {
            Dependents.RemoveAt(i);
        }

        public void RemoveAllDependents()
        {
            Dependents.Clear();
        }

        public decimal GetAnnualSalary()
        {
            return PaycheckAmount * NumPayPeriods;
        }



        
    }
}