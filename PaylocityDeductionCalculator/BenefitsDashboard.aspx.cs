using PaylocityDeductionCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PaylocityDeductionCalculator
{


    public partial class BenefitsDashboard : System.Web.UI.Page
    {

        private static Employee NewEmployee;
        private decimal A_DISCOUNT = 0.10m;
        private decimal DEPENDENT_COST = 500.00m;
        private decimal EMPLOYEE_COST = 1000.00m;
        private int NUM_PAY_PERIODS = 26;
        private decimal BIWEEKLY_PAYCHECK = 2000.00m;
        private decimal totalAnnualPrice = 0.00m;
        private decimal totalPaycheckPrice = 0.00m;
        private decimal totalAnnualTakehome = 0.00m;
        private decimal totalPaycheckTakehome = 0.00m;



        protected void Page_Load(object sender, EventArgs e)
        {
            //Wipe out old info and hide divs if employee name boxes are empty
            
            if(EE_Last_TextBox.Text== "" || EE_First_TextBox.Text ==  "")
            {
                ModDependentsDiv.Visible = false;
                Summary_Container.Visible = false;
                NewEmployee = null;
                EE_Info_Div.Attributes.CssStyle.Add("opacity", "0.9");
            }

            
            SetDefaultPayInfo();

            UpdateResultsPanel();
            
        }




        public List<Benefactor> LoadCart()
        {

            List<Benefactor> BenefitCart = new List<Benefactor>();
           
            if(NewEmployee != null)
            {
                BenefitCart.Add(NewEmployee);

                foreach(Dependent d in NewEmployee.Dependents)
                {
                    BenefitCart.Add(d);
                }
            }
            
            
            return BenefitCart;
        }


        #region event handlers
        ////////////////////////////////
        /// EVENT HANDLERS /////////////
        ////////////////////////////////


        protected void Restart_Button_Click(object sender, EventArgs e)
        {
            NewEmployee = null;
            EE_First_TextBox.Text = "";
            EE_Last_TextBox.Text = "";

            EE_First_TextBox.Enabled = true;
            EE_Last_TextBox.Enabled = true;
            EE_Error_Label.Text = "";

            ModDependentsDiv.Visible = false;
            DepFirst_TextBox.Enabled = false;
            DepLast_TextBox.Enabled = false;
            ListBox1.Enabled = false;
            AddDependent_Button.Enabled = false;
            Summary_Container.Visible = false;
            Summary_Header.InnerText = "Summary";

            EE_Info_Div.Attributes.CssStyle.Add("opacity", "0.9");

            UpdateResultsPanel();


        }

        protected void SubmitEmployee_Button_Click(object sender, EventArgs e)
        {
            string First = EE_First_TextBox.Text.Trim();
            string Last = EE_Last_TextBox.Text.Trim();

            string Error = NameValidator(First, Last);

            if (Error.Length > 0)
            {
                EE_Error_Label.Text = Error;
            }
            else
            {
                decimal CheckDiscount = CalcDiscount(First, Last);
                decimal CheckUnitPrice = CalcUnitPrice(EMPLOYEE_COST, CheckDiscount);

                NewEmployee = new Employee()
                {
                    FirstName = First,
                    LastName = Last,
                    Dependents = new List<Dependent>(),
                    BiWeeklySalary = BIWEEKLY_PAYCHECK,
                    NumPayPeriods = NUM_PAY_PERIODS,
                    Discount = CheckDiscount,
                    UnitPrice = CheckUnitPrice,
                    PeriodPrice = CalcPeriodPrice(CheckUnitPrice)
                };
                
                EE_First_TextBox.Enabled = false;
                EE_Last_TextBox.Enabled = false;
                EE_Error_Label.Text = "";

                Summary_Container.Visible = true;
                ModDependentsDiv.Visible = true;
                DepFirst_TextBox.Enabled = true;
                DepLast_TextBox.Enabled = true;
                ListBox1.Enabled = true;
                AddDependent_Button.Enabled = true;
                Summary_Header.InnerText = "Summary for " + First + " " + Last;

                EE_Info_Div.Attributes.CssStyle.Add("opacity", "0.25");


                UpdateResultsPanel();
            }

        }


        protected void AddDependent_Button_Click(object sender, EventArgs e)
        {

            string First = DepFirst_TextBox.Text.Trim();
            string Last = DepLast_TextBox.Text.Trim();

            string Error = NameValidator(First, Last);
            if (Error.Length > 0)
            {
                Dep_Error_Label.Text = Error;
            }
            else
            {

                decimal CheckDiscount = CalcDiscount(First, Last);
                decimal CheckUnitPrice = CalcUnitPrice(DEPENDENT_COST, CheckDiscount);


                Dependent NewDependent = new Dependent
                {
                    FirstName = First,
                    LastName = Last,
                    Discount = CheckDiscount,
                    UnitPrice = CheckUnitPrice,
                    PeriodPrice = CalcPeriodPrice(CheckUnitPrice)

                };


                NewEmployee.Dependents.Add(NewDependent);

                RemoveDependent_Button.Enabled = true;

                totalAnnualPrice = 0.00m;
                totalPaycheckPrice = 0.00m;
                totalAnnualTakehome = 0.00m;
                totalPaycheckTakehome = 0.00m;

                UpdateListBox();
                UpdateResultsPanel();
                ClearDependentBoxes();
            }

        }
        
        protected void RemoveDependent_Button_Click(object sender, EventArgs e)
        {

            int selectedIndex = ListBox1.SelectedIndex;
            int ListBox1Size = ListBox1.Items.Count;

            try
            {
                for (int i = 0; i < ListBox1Size; i++)
                {
                    if (ListBox1.Items[i].Selected)
                    {
                        NewEmployee.Dependents.RemoveAt(i);
                    }
                }
            }
            catch
            {

       
            }

            if (NewEmployee.Dependents.Count <= 0)
            {
                RemoveDependent_Button.Enabled = false;
            }

            UpdateResultsPanel();
            UpdateListBox();

        }



        public void CartList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Benefactor b = (Benefactor)(e.Row.DataItem);
            //Increment Running totals
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalAnnualPrice += b.UnitPrice;
                totalPaycheckPrice += b.PeriodPrice;

            }
            //Display running totals in footer
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                totalPaycheckTakehome = BIWEEKLY_PAYCHECK - totalPaycheckPrice;
                totalAnnualTakehome = totalPaycheckTakehome * NUM_PAY_PERIODS;
                e.Row.Cells[0].Text = "TOTALS:";
                e.Row.Cells[1].Text = "" + totalAnnualPrice.ToString("C");
                e.Row.Cells[2].Text = "" + totalPaycheckPrice.ToString("C");

                GrossSalary_Label.Text = (BIWEEKLY_PAYCHECK * NUM_PAY_PERIODS).ToString("C");
                AnnualDeductions_Label.Text = totalAnnualPrice.ToString("C");
                AnnualTakehome_Label.Text = totalAnnualTakehome.ToString("C");

                GrossPayPeriod_Label.Text = BIWEEKLY_PAYCHECK.ToString("C");
                PayPeriodDeductions_Label.Text = totalPaycheckPrice.ToString("C");
                PayPeriodTakehome_Label.Text = totalPaycheckTakehome.ToString("C");
            }

        }

#endregion

        #region helper methods
        ////////////////////////////////
        /// HELPER METHODS /////////////
        ////////////////////////////////

        private void Reset_Totals()
        {

            totalAnnualPrice = 0.00m;
            totalPaycheckPrice = 0.00m;
            totalAnnualTakehome = 0.00m;
            totalPaycheckTakehome = 0.00m;

        }

        private void SetDefaultPayInfo()
        {
            PayAmount_TB.Text = BIWEEKLY_PAYCHECK.ToString("C");
            NumPeriods_TB.Text = NUM_PAY_PERIODS.ToString();
            EECost_TB.Text = EMPLOYEE_COST.ToString("C");
            DepCost_TB.Text = DEPENDENT_COST.ToString("C");
            Discount1_TB.Text = A_DISCOUNT.ToString("P");

            PayAmount_TB.Enabled = false;
            NumPeriods_TB.Enabled = false;
            EECost_TB.Enabled = false;
            DepCost_TB.Enabled = false;
            Discount1_TB.Enabled = false;
        }


        private decimal CalcPeriodPrice(decimal UnitPrice)
        {

            return UnitPrice / NUM_PAY_PERIODS;

        }

        private decimal CalcUnitPrice(decimal BasePrice, decimal Discount)
        {

            return BasePrice - BasePrice * Discount;
        }

        private decimal CalcDiscount(string First, string Last)
        {
            decimal Discount = 0.00m;
            if (First.StartsWith("A") || First.StartsWith("a") ||

                Last.StartsWith("A") || Last.StartsWith("a"))
            {
                Discount = A_DISCOUNT;
            }

            return Discount;
        }


        private string NameValidator(string FirstName, string LastName)
        {
            string ErrorMessage = "";
            //Check to make sure both boxes have text
            if (FirstName == null || FirstName.Length == 0)
            {
                ErrorMessage += "First Name required \n";
            }

            if (LastName == null || LastName.Length == 0)
            {
                ErrorMessage += " Last Name required \n";
            }

            return ErrorMessage;
        }


        private void ClearDependentBoxes()
        {
            DepFirst_TextBox.Text = "";
            DepLast_TextBox.Text = "";
        }

        private void ClearEmployeeBoxes()
        {
            EE_First_TextBox.Text = "";
            EE_Last_TextBox.Text = "";

        }


        private void UpdateResultsPanel()
        {
            Reset_Totals();
            CartList.DataSource = LoadCart();
            CartList.DataBind();

        }

        private void UpdateListBox()
        {
            ListBox1.DataSource = null;
            ListBox1.DataSource = NewEmployee.Dependents;
            ListBox1.DataBind();
        }

#endregion

    }

}
 