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

        private static Boolean isEmployeeSet = false;
        private static BusinessLogic session = null;
        private static int listBoxIndex = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if(session == null || !session.IsEmployeeSet())
            {
                EnableEmployeePanel();
                DisableDependentsPanel();
                DisableSummaryPanel();                
                session = new BusinessLogic();
            }else
            {
                if (isEmployeeSet)
                {
                    EE_First_TextBox.Text = session.GetEmployeeFirstName();
                    EE_Last_TextBox.Text = session.GetEmployeeLastName();
                    
                }


            }

            if (!IsPostBack)
            {
                SetDefaultPayInfo();
                UpdateDependentsPanel();
                UpdateSummaryPanel();
            }
            
            
            
            
        }

        public List<Benefactor> LoadGridView()
        {
            return session.GetBenefactorsList();
        }
        
        #region event handlers
        ////////////////////////////////
        /// EVENT HANDLERS /////////////
        ////////////////////////////////

        protected void Restart_Button_Click(object sender, EventArgs e)
        {
            session = null;
            session = new BusinessLogic();
            isEmployeeSet = false;
     
            ResetEmployeePanel();
            EnableEmployeePanel();
            DisableDependentsPanel();
            DisableSummaryPanel();
            UpdateSummaryPanel();


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
                if (!isEmployeeSet)
                {
                    session.InitializeEmployee(First, Last);
                    isEmployeeSet = true;                
                }
                else
                {
                    session.SetEmployeeName(First, Last);
                }

                DisableEmployeePanel();
                EnableDependentsPanel();
                EnableSummaryPanel();
                UpdateDependentsPanel();
                UpdateSummaryPanel();
            }

        }

        protected void EditEmployeeInfo_Button_Click(object sender, EventArgs e)
        {
            EnableEmployeePanel();
        }

        protected void AddDependent_Button_Click(object sender, EventArgs e)
        {
            string firstName = DependentFirst_TextBox.Text.Trim();
            string lastName = DependentLast_TextBox.Text.Trim();
            string Error = NameValidator(firstName, lastName);

            if (Error.Length > 0)
            {
                DependentError_Label.Text = Error;
            }
            else
            {
                
                session.AddDependent(firstName, lastName);
                UpdateDependentsPanel();
                UpdateSummaryPanel();
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
                        session.RemoveDependentAt(i);

                    }
                }
            }
            catch (Exception ex)
            {
                DependentError_Label.Text = ex.Message;

            }
          

            UpdateSummaryPanel();
            UpdateDependentsPanel();
        }

        protected void RemoveAllDependents_Button_Click(object sender, EventArgs e)
        {
            try
            {
                session.RemoveAllDependents();
            }
            catch
            {

            }

            UpdateDependentsPanel();
            UpdateSummaryPanel();
            
        }

        public void CartList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
               
                e.Row.Cells[0].Text = "TOTALS:";
                e.Row.Cells[2].Text = "" + session.GetAnnualDeductions().ToString("C");
                e.Row.Cells[3].Text = "" + session.GetPaycheckDeductions().ToString("C");
                
            }

        }

#endregion

        #region helper methods
        ////////////////////////////////
        /// HELPER METHODS /////////////
        ////////////////////////////////
        
        private void ResetEmployeePanel()
        {
            EE_First_TextBox.Text = "";
            EE_Last_TextBox.Text = "";
            SetDefaultPayInfo();
        }
        /*
         * Autofills Textboxes with information from Business Logic class
         */
        private void SetDefaultPayInfo()
        {

            PayAmount_TB.Text = session.GetPaycheckAmount().ToString("C");
            NumPeriods_TB.Text = session.GetNumPayPeriods().ToString();
            EECost_TB.Text = session.GetEmployeeCost().ToString("C");
            DepCost_TB.Text = session.GetDependentCost().ToString("C");
            Discount1_TB.Text = session.GetDiscount().ToString("P");

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
            DependentFirst_TextBox.Text = "";
            DependentLast_TextBox.Text = "";
        }

        private void ClearEmployeeBoxes()
        {
            EE_First_TextBox.Text = "";
            EE_Last_TextBox.Text = "";

        }


        private void UpdateSummaryPanel()
        {
            if (session.IsEmployeeSet())
            {

                Summary_Header.InnerText = "Summary for " + session.GetEmployeeFirstName() + " " + session.GetEmployeeLastName();

                GrossSalary_Label.Text = session.GetAnnualGross().ToString("C");
                AnnualDeductions_Label.Text = session.GetAnnualDeductions().ToString("C");
                AnnualTakehome_Label.Text = session.GetAnnualNet().ToString("C");

                GrossPayPeriod_Label.Text = session.GetPaycheckGross().ToString("C");
                PayPeriodDeductions_Label.Text = session.GetPaycheckDeductions().ToString("C");
                PayPeriodTakehome_Label.Text = session.GetPaycheckNet().ToString("C");
            }

            CartList.DataSource = LoadGridView();
            CartList.DataBind();

        }

        private void UpdateDependentsPanel()
        {
            UpdateListBox();

            DependentError_Label.Text = "";

            if (session.GetDependentCount() <= 0)
            {
                RemoveDependent_Button.Enabled = false;
                RemoveAllDependents_Button.Enabled = false;
            }
            else
            {
                RemoveDependent_Button.Enabled = true;
                RemoveAllDependents_Button.Enabled = true;
            }
            
        }

        private void UpdateListBox()
        {
            Dependents_ListBox.DataSource = null;
            if (isEmployeeSet)
            {
                Dependents_ListBox.DataSource = session.GetDependentsList();
                Dependents_ListBox.DataBind();
            }
                
            
            
        }

        private void EnableEmployeePanel()
        {
            
            EE_First_TextBox.Enabled = true;
            EE_Last_TextBox.Enabled = true;
            EE_Error_Label.Text = "";
            EE_Info_Div.Attributes.CssStyle.Add("opacity", "1.0");
            SubmitEmployee_Button.Enabled = true;
        }

        private void DisableEmployeePanel()
        {
            EE_First_TextBox.Enabled = false;
            EE_Last_TextBox.Enabled = false;
            EE_Error_Label.Text = "";
            SubmitEmployee_Button.Enabled = false;
            EE_Info_Div.Attributes.CssStyle.Add("opacity", "0.50");
        }

        private void EnableDependentsPanel()
        {
            ModDependentsDiv.Visible = true;
            DependentFirst_TextBox.Enabled = true;
            DependentLast_TextBox.Enabled = true;
            Dependents_ListBox.Enabled = true;
            AddDependent_Button.Enabled = true;
            
        }

        private void DisableDependentsPanel()
        {
            ModDependentsDiv.Visible = false;
            DependentFirst_TextBox.Enabled = false;
            DependentLast_TextBox.Enabled = false;
            Dependents_ListBox.Enabled = false;
            AddDependent_Button.Enabled = false;
        }

        private void EnableSummaryPanel()
        {
            Summary_Container.Visible = true;
            
        }

        private void DisableSummaryPanel()
        {
            Summary_Container.Visible = false;
            Summary_Header.InnerText = "Summary";
        }




        #endregion

        protected void Dependents_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DependentError_Label.Text = listBoxIndex.ToString();
            listBoxIndex = Dependents_ListBox.SelectedIndex;
            DependentError_Label.Text += " " + listBoxIndex.ToString();
        }
    }

}
 