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
        


        public Employee()
        {
            
        }

        public void AddDependent(Dependent dependent)
        {
            if(Dependents == null)
            {
                Dependents = new List<Dependent>();
            }
            Dependents.Add(dependent);
        }

        public void RemoveDependent(Dependent dependent)
        {
            try {

                Dependents.Remove(dependent);

            }
            catch
            {

            }
            
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

        public decimal GetAnnualDeductions()
        {
            decimal annualDeductions = GetAnnualPrice();
            foreach(Dependent dependent in Dependents)
            {
                annualDeductions += dependent.GetAnnualPrice();
            }
            return annualDeductions;
        }

        public decimal GetAnnualNet()
        {
            return GetAnnualSalary() - GetAnnualDeductions();
        }

        public decimal GetPaycheckGross()
        {
            return PaycheckAmount;
        }

        public decimal GetPaycheckDeductions()
        {
            return GetAnnualDeductions() / NumPayPeriods;
        }

        public decimal GetPaycheckNet()
        {
            return GetPaycheckGross() - GetPaycheckDeductions();
        }

        
    }
}