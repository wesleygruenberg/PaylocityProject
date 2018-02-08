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

        public decimal TotalAnnualPrice { get; set; }
        public decimal TotalPaycheckPrice { get; set; }
        public decimal TotalAnnualTakehome { get; set; }
        public decimal TotalPaycheckTakehome { get; set; }

        

        public BusinessLogic()
        {
            this.TotalAnnualPrice = 0.00m;
            this.TotalPaycheckPrice = 0.00m;
            this.TotalAnnualTakehome = 0.00m;
            this.TotalPaycheckTakehome = 0.00m;

        }

        public void InitializeEmployee(string firstName, string lastName)
        {
            decimal discount = CalculateDiscount(firstName, lastName);
            decimal unitPrice = CalculateUnitPrice(EmployeeCost, discount);
            decimal periodPrice = CalculatePeriodPrice(unitPrice);

            employee = new Models.Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Dependents = new List<Dependent>(),
                PaycheckAmount = this.GetPaycheckAmount(),
                NumPayPeriods = this.GetNumPayPeriods(),
                Discount = discount,
                UnitPrice = unitPrice,
                PeriodPrice = periodPrice

            };

        }

        public void AddDependent(string firstName, string lastName)
        {
            decimal discount = CalculateDiscount(firstName, lastName);
            decimal unitPrice = CalculateUnitPrice(DependentCost, discount);


            Dependent dependent = new Dependent
            {
                FirstName = firstName,
                LastName = lastName,
                Discount = discount,
                UnitPrice = unitPrice,
                PeriodPrice = CalculatePeriodPrice(unitPrice)

            };

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

        public List<Dependent> GetDependentsList()
        {

            return null;
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
            return employee.Dependents.Count;
        }

        public decimal GetEmployeeSalary()
        {
            return employee.GetAnnualSalary();
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

        public Boolean isEmployeeSet()
        {
            Boolean isSet = false;
            if (employee != null)
            {
                isSet = true;
            }

            return isSet;
        }

        public void ResetTotals()
        {

            TotalAnnualPrice = 0.00m;
            TotalPaycheckPrice = 0.00m;
            TotalAnnualTakehome = 0.00m;
            TotalPaycheckTakehome = 0.00m;

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