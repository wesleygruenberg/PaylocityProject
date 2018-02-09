﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class BusinessLogic
    {
        private const decimal Discount = 0.10m;
        private const decimal DependentCost = 500.00m;
        private const decimal EmployeeCost = 1000.00m;
        private const int NumPayPeriods = 26;
        private const decimal Paycheck = 2000.00m;

        private Employee employee;
      

        public BusinessLogic()
        {

            employee = null;
        }

        public void InitializeEmployee(string firstName, string lastName)
        {
            decimal discount = CalculateDiscount(firstName, lastName);
            

            employee = new Models.Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Dependents = new List<Dependent>(),
                PaycheckAmount = this.GetPaycheckAmount(),
                NumPayPeriods = this.GetNumPayPeriods(),
                Discount = discount,
                UnitPrice = EmployeeCost
            };
            employee.UpdatePrices();

        }

        public void AddDependent(string firstName, string lastName)
        {
            decimal discount = CalculateDiscount(firstName, lastName);
           
            Dependent dependent = new Dependent
            {
                FirstName = firstName,
                LastName = lastName,
                Discount = CalculateDiscount(firstName, lastName),
                NumPayPeriods = this.GetNumPayPeriods(),
                UnitPrice = DependentCost           
            };
            dependent.UpdatePrices();
            employee.AddDependent(dependent);
        }

        public void RemoveDependent(Dependent dependent)
        {
            employee.RemoveDependent(dependent);
        }

        public void RemoveDependentAt(int i)
        {
            employee.RemoveDependentAt(i);
        }

        public void RemoveAllDependents()
        {
            employee.RemoveAllDependents();
        }

        public List<Dependent> GetDependentsList()
        {
            List<Dependent> dependents = employee.Dependents;
            return employee.Dependents;
        }

        public List<Benefactor> GetBenefactorsList()
        {
            List<Benefactor> benefactors = null;

            if (employee != null)
            {
                benefactors = new List<Benefactor>();
                benefactors.Add(employee);

                foreach (Dependent dependent in employee.Dependents)
                {
                    benefactors.Add(dependent);
                }
            }

            return benefactors;
        }

        public int GetDependentCount()
        {
            int dependentCount = 0;

            try
            {
                dependentCount = employee.Dependents.Count;
            }catch (Exception e)
            {
                //employee not initialized
            }
            return dependentCount;
        }

        public decimal GetAnnualGross()
        {
            return employee.GetAnnualSalary();
        }

        public decimal GetAnnualDeductions()
        {
            return employee.GetAnnualDeductions();
        }

        public decimal GetAnnualNet()
        {
            return employee.GetAnnualNet();
        }

        public decimal GetPaycheckGross()
        {
            return employee.GetPaycheckGross();
        }

        public decimal GetPaycheckDeductions()
        {
            return employee.GetPaycheckDeductions();
        }

        public decimal GetPaycheckNet()
        {
            return employee.GetPaycheckNet();
        }

        public decimal GetDiscount()
        {
             return Discount;
        }

        public decimal GetEmployeeCost()
        {
            return EmployeeCost;
        }

        public decimal GetDependentCost()
        {
            return DependentCost;
        }

        public decimal GetPaycheckAmount()
        {
            return Paycheck;
        }

        public int GetNumPayPeriods()
        {
            return NumPayPeriods;
        }
      

        public string GetEmployeeFirstName()
        {
            return employee.FirstName;
        }
            public string GetEmployeeLastName()
        {
            return employee.LastName;
        }
        

        public void SetEmployeeName(string firstName, string lastName)
        {
            employee.FirstName = firstName;
            employee.LastName = lastName;

            employee.Discount = CalculateDiscount(firstName, lastName);


        }

        public Boolean IsEmployeeSet()
        {
            Boolean isSet = false;
            if (employee != null)
            {
                isSet = true;
            }

            return isSet;
        }


        private decimal CalculatePeriodPrice(decimal UnitPrice)
        {

            return UnitPrice / NumPayPeriods;

        }


        private decimal CalculateUnitPrice(decimal BasePrice, decimal Discount)
        {

            return BasePrice - BasePrice * Discount;
        }

        private decimal CalculateDiscount(string firstName, string lastName)
        {
            decimal discount = 0.00m;
            if (firstName.StartsWith("A") || firstName.StartsWith("a") ||

                lastName.StartsWith("A") || lastName.StartsWith("a"))
            {
                discount = Discount;
            }

            return discount;
        }

        

    }
}