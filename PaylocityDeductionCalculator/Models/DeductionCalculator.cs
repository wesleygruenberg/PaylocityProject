using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaylocityDeductionCalculator.Models
{
    public class DeductionCalculator
    {

        /*
         * Constants from project description
         * 
         */
        private const decimal Discount = 0.10m;
        private const decimal DependentCost = 500.00m;
        private const decimal EmployeeCost = 1000.00m;
        private const int NumPayPeriods = 26;
        private const decimal Paycheck = 2000.00m;

        /* Employee object for the session */

        private Employee employee;
      
        /* Constructor */
        public DeductionCalculator()
        {

            employee = null;
        }

        /* Initializes a new employee object for calculator to access */
        public void InitializeEmployee(string firstName, string lastName)
        {
            
            employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Dependents = new List<Dependent>(),
                PaycheckAmount = this.GetPaycheckAmount(),
                NumPayPeriods = this.GetNumPayPeriods(),
                Discount = CalculateDiscount(firstName, lastName),
                UnitPrice = EmployeeCost
            };

            employee.UpdatePrices();

        }

        /*Adds a dependent to this calculator's employee based on this
         * particular problem's inputs and constants */
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

        /*Wrapper Method: Removes a dependent based on index number from this DeductionCalculator's Employee object */
        public void RemoveDependentAt(int i)
        {
            employee.RemoveDependentAt(i);
        }

        /*Wrapper method: Removes all dependents from the DeductionCalculator's Employee object */
        public void RemoveAllDependents()
        {
            employee.RemoveAllDependents();
        }

        /*Wrapper method: to return a List of the Employee's Dependents */
        public List<Dependent> GetDependentsList()
        {
            List<Dependent> dependents = employee.Dependents;
            return employee.Dependents;
        }

        /*Creates and returns a list of both Employees and Dependents which are Benefactors 
         */
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

        /* Wrapper method to count number of dependents, catches uninitialized list of dependents */
        public int GetDependentCount()
        {
            int dependentCount = 0;

            try
            {
                dependentCount = employee.Dependents.Count;
            }catch (Exception ex)
            {
                //employee not initialized
                //return zero 
            }
            return dependentCount;
        }

        #region wrapper getters
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

        #endregion

        /* Name gets changed, set those names and recalculate discount */
        public void SetEmployeeName(string firstName, string lastName)
        {
            employee.FirstName = firstName;
            employee.LastName = lastName;

            employee.Discount = CalculateDiscount(firstName, lastName);

        }

        //MOVED LOGIC WITHIN EMPLOYEE CLASS -- TO DELETE
        /*calculates pay per period based on number of pay periods and unit price 
        private decimal CalculatePeriodPrice(decimal UnitPrice)
        {

            return UnitPrice / NumPayPeriods;

        }
        */

        /* Calculates discount: right now based on A name logic */
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