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

        private static BusinessLogic calculator;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Wipe out old info and hide divs if employee name boxes are empty
            
            if(EE_Last_TextBox.Text== "" || EE_First_TextBox.Text ==  "")
            {
                ModDependentsDiv.Visible = false;
                Summary_Container.Visible = false;
                calculator = new Models.BusinessLogic();
                EE_Info_Div.Attributes.CssStyle.Add("opacity", "0.9");
            }

            SetDefaultPayInfo();
            UpdateSummary();
            
        }




        public List<Benefactor> LoadGridView()
        {
            return calculator.GetBenefactorsList();
        
        }


        #region event handlers
        ////////////////////////////////
        /// EVENT HANDLERS /////////////
        ////////////////////////////////


        protected void Restart_Button_Click(object sender, EventArgs e)
        {
            calculator = null;
            calculator = new BusinessLogic();
            EE_First_TextBox.Text = "";
            EE_Last_TextBox.Text = "";

            EE_First_TextBox.Enabled = true;
            EE_Last_TextBox.Enabled = true;
            EE_Error_Label.Text = "";

            ModDependentsDiv.Visible = false;
            DepFirst_TextBox.Enabled = false;
            DepLast_TextBox.Enabled = false;
            Dependents_ListBox.Enabled = false;
            AddDependent_Button.Enabled = false;
            Summary_Container.Visible = false;
            Summary_Header.InnerText = "Summary";

            EE_Info_Div.Attributes.CssStyle.Add("opacity", "0.9");

            UpdateSummary();


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
                calculator.InitializeEmployee(First, Last);
                
                
                EE_First_TextBox.Enabled = false;
                EE_Last_TextBox.Enabled = false;
                EE_Error_Label.Text = "";

                Summary_Container.Visible = true;
                ModDependentsDiv.Visible = true;
                DepFirst_TextBox.Enabled = true;
                DepLast_TextBox.Enabled = true;
                Dependents_ListBox.Enabled = true;
                AddDependent_Button.Enabled = true;
                Summary_Header.InnerText = "Summary for " + First + " " + Last;

                EE_Info_Div.Attributes.CssStyle.Add("opacity", "0.50");


                UpdateSummary();
            }

        }


        protected void AddDependent_Button_Click(object sender, EventArgs e)
        {

            string firstName = DepFirst_TextBox.Text.Trim();
            string lastName = DepLast_TextBox.Text.Trim();

            string Error = NameValidator(firstName, lastName);
            if (Error.Length > 0)
            {
                DependentError_Label.Text = Error;
            }
            else
            {
                calculator.AddDependent(firstName, lastName);

                RemoveDependent_Button.Enabled = true;
                UpdateListBox();
                UpdateSummary();
                ClearDependentNameBoxes();
            }

        }
        
        protected void RemoveDependent_Button_Click(object sender, EventArgs e)
        {

            int selectedIndex = Dependents_ListBox.SelectedIndex;
            int listBoxSize = Dependents_ListBox.Items.Count;

            try
            {
                for (int i = 0; i < listBoxSize; i++)
                {
                    if (Dependents_ListBox.Items[i].Selected)
                    {
                        calculator.RemoveDependentAt(i);
             
                    }
                }
            }
            catch
            {

       
            }

            if (calculator.GetDependentCount() <= 0)
            {
                RemoveDependent_Button.Enabled = false;
            }

            UpdateSummary();
            UpdateListBox();

        }



        public void CartList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
               
                e.Row.Cells[0].Text = "TOTALS:";
                e.Row.Cells[1].Text = "" + calculator.TotalAnnualPrice.ToString("C");
                e.Row.Cells[2].Text = "" + calculator.TotalPaycheckPrice.ToString("C");


            }

        }

#endregion

        #region helper methods
        ////////////////////////////////
        /// HELPER METHODS /////////////
        ////////////////////////////////

       
        /*
         * Autofills Textboxes with information from Business Logic class
         */
        private void SetDefaultPayInfo()
        {

            PayAmount_TB.Text = calculator.GetPaycheckAmount().ToString("C");
            NumPeriods_TB.Text = calculator.GetNumPayPeriods().ToString();
            EECost_TB.Text = calculator.GetEmployeeCost().ToString("C");
            DepCost_TB.Text = calculator.GetDependentCost().ToString("C");
            Discount1_TB.Text = calculator.GetDiscount().ToString("P");

            PayAmount_TB.Enabled = false;
            NumPeriods_TB.Enabled = false;
            EECost_TB.Enabled = false;
            DepCost_TB.Enabled = false;
            Discount1_TB.Enabled = false;
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


        private void ClearDependentNameBoxes()
        {
            DepFirst_TextBox.Text = "";
            DepLast_TextBox.Text = "";
        }

        private void ClearEmployeeBoxes()
        {
            EE_First_TextBox.Text = "";
            EE_Last_TextBox.Text = "";

        }


        private void UpdateSummary()
        {
            if (calculator.isEmployeeSet())
            {
                GrossSalary_Label.Text = calculator.GetEmployeeSalary().ToString("C");
                AnnualDeductions_Label.Text = calculator.TotalAnnualPrice.ToString("C");
                AnnualTakehome_Label.Text = calculator.TotalAnnualTakehome.ToString("C");

                GrossPayPeriod_Label.Text = calculator.GetPaycheckAmount().ToString("C");
                PayPeriodDeductions_Label.Text = calculator.TotalPaycheckPrice.ToString("C");
                PayPeriodTakehome_Label.Text = calculator.TotalPaycheckTakehome.ToString("C");
            }

            CartList.DataSource = LoadGridView();
            CartList.DataBind();

        }

        private void UpdateListBox()
        {
            Dependents_ListBox.DataSource = null;
            Dependents_ListBox.DataSource = calculator.GetDependentsList();
            Dependents_ListBox.DataBind();
        }

#endregion

    }

}
 