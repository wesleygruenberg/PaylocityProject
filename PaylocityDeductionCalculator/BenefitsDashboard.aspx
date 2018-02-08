<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BenefitsDashboard.aspx.cs" Inherits="PaylocityDeductionCalculator.BenefitsDashboard" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
        <title></title>
    </head>

    <body>
            <form id="form1" runat="server">
                <header class="page-header">
                    <a id="logo" href="BenefitsDashboard.aspx"> 
				        <img src="Content/logo.png" alt="Apple Orange" title="Paylocity Web App"/>
				        </a>

                    <h1>Benefits Dashboard</h1>
                </header>
                <div id="app-container">
                    <asp:Button ID="Restart_Button" runat="server" Text="Start Over" Width="94px" OnClick="Restart_Button_Click" />

                <div class="row">


                    <div id="left-div" class="col-4">
                        <div id="EE_Info_Div" runat="server">
                        <h2>Submit employee&#39;s information</h2>
                        <section>
                            <asp:Label ID="FirstNameLabel" runat="server" Text="First Name: " Width="100px"></asp:Label>
                            <asp:TextBox ID="EE_First_TextBox" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="LastNameLabel" runat="server" Text="Last Name: " Width="100px"></asp:Label>
                            <asp:TextBox ID="EE_Last_TextBox" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="EE_Error_Label" runat="server" CssClass="alert-warning"></asp:Label>
                            <br />
                            <br />
                            <h3>Payroll Information</h3>

                            <asp:Label ID="PayAmount_Label" runat="server" Text="Pay Period Amount:" Width="160px"></asp:Label>
                            <asp:TextBox ID="PayAmount_TB" STYLE="text-align: right" runat="server" Width="100px"></asp:TextBox>
                            <br />
                            <asp:Label ID="PayPeriods_Label" runat="server" Text="Pay Periods Annually:" Width="160px"></asp:Label>
                            <asp:TextBox ID="NumPeriods_TB" STYLE="text-align: right" runat="server" Width="100px"></asp:TextBox>
                            <br />
                            <br />
                            <h3>Annual Insurance Costs</h3>
                            <asp:Label ID="EmployeeCost_Label" runat="server" Text="Employee:" Width="160px"></asp:Label>
                            <asp:TextBox ID="EECost_TB" STYLE="text-align: right" runat="server" Width="100px"></asp:TextBox>
                            <br />
                            <asp:Label ID="DependentsCost_Label" runat="server" Text="Dependents:" Width="160px"></asp:Label>
                            <asp:TextBox ID="DepCost_TB" STYLE="text-align: right" runat="server" Width="100px"></asp:TextBox>
                            <br />
                            <br />
                            <h3>Discounts</h3>
                            <asp:Label ID="Discount1_Label" runat="server" Text="Name Starts with &quot;A&quot;:" Width="160px"></asp:Label>
                            <asp:TextBox ID="Discount1_TB" STYLE="text-align: right" runat="server" Width="101px"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="SubmitEmployee_Button" runat="server" Text="Submit" Width="95px" OnClick="SubmitEmployee_Button_Click" />

                            <br />
                            <br />

                        </section>
                        </div>

                    </div>

                    <div id="center-div" class="col-4">
                        <div runat="server" id="ModDependentsDiv">
                            <h2>Now add any dependents</h2>
                            <section>

                                <asp:Button ID="EditEmployeeInfo_Button" runat="server" OnClick="EditEmployeeInfo_Button_Click" Text="Edit Employee Information" />
                                <br />
                                <br />

                                <asp:Label ID="FirstNameLabel0" runat="server" Text="First Name: " Width="100px"></asp:Label>
                                <asp:TextBox ID="DependentFirst_TextBox" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="LastNameLabel0" runat="server" Text="Last Name: " Width="100px"></asp:Label>
                                <asp:TextBox ID="DependentLast_TextBox" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="DependentError_Label" runat="server" CssClass="alert-warning"></asp:Label>
                                <br />
                                <asp:Button ID="AddDependent_Button" runat="server" OnClick="AddDependent_Button_Click" Text="Add Dependent" Width="134px" Enabled="False" />
                                <br />
                                <br />
                                <asp:ListBox ID="Dependents_ListBox" runat="server" Height="119px" ItemType="Dependent" Width="273px" Enabled="False"></asp:ListBox>
                                <br />
                                <br />
                                <asp:Button ID="RemoveDependent_Button" runat="server" OnClick="RemoveDependent_Button_Click" Text="Remove Dependent" Enabled="False" />
                                <asp:Button ID="RemoveAllDependents_Button" runat="server" Enabled="False" OnClick="RemoveAllDependents_Button_Click" Text="Remove All" />
                                <br />

                            </section>

                            <br />

                        </div>


                    </div>
                    <div id="right-div" class="col-4">
                        <div id="Summary_Container" runat="server">
                        <h2 id ="Summary_Header" runat="server">Summary</h2>

                        <section>
                            
                                <h3>Annual</h3>
                                <asp:Label ID="Label3" runat="server" Text="Gross Salary:" Width="200px"></asp:Label>
                                <asp:Label ID="GrossSalary_Label" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="Label5" runat="server" Text="Deductions:" Width="200px"></asp:Label>
                                <asp:Label ID="AnnualDeductions_Label" runat="server"></asp:Label>

                                <br />

                                <asp:Label ID="Label1" runat="server" Text="Net Takehome: " Width="200px"></asp:Label>
                                <asp:Label ID="AnnualTakehome_Label" runat="server"></asp:Label>
                                <br />
                                <br />
                                <h3>Pay Period</h3>
                                <asp:Label ID="Label4" runat="server" Text="Gross Period Pay:" Width="200px"></asp:Label>
                                <asp:Label ID="GrossPayPeriod_Label" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="Label6" runat="server" Text="Deductions:" Width="200px"></asp:Label>
                                <asp:Label ID="PayPeriodDeductions_Label" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="Net Takehome:" Width="200px"></asp:Label>
                                <asp:Label ID="PayPeriodTakehome_Label" runat="server"></asp:Label>
                            
                        </section>
                        <br />
                        <section>
                            <asp:GridView ID="CartList" 
                                runat="server" 
                                AutoGenerateColumns="false" 
                                ShowFooter="True" 
                                GridLines="Vertical" 
                                EmptyDataText="There is nothing in your shopping cart." 
                                CellPadding="4" onrowdatabound="CartList_RowDataBound" 
                                CssClass="table table-striped table-bordered">
                                <HeaderStyle HorizontalAlign="Left" BackColor="#3D7169" ForeColor="#FFFFFF" />
                                <FooterStyle HorizontalAlign="Right" BackColor="#6C6B66" ForeColor="#FFFFFF" />
                                <AlternatingRowStyle BackColor="#F8F8F8" />
                                <RowStyle BackColor="LightGray" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Benefactor Name">
                                        <ItemTemplate>
                                            <%# Eval("FirstName") + " " + Eval("LastName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Discount" HeaderText ="Discount Applied" DataFormatString="{0:p}" />
                                    <asp:BoundField DataField="UnitPrice" HeaderText="Annual Price" DataFormatString="{0:c}" />
                                    
                                    <asp:BoundField DataField="PeriodPrice" HeaderText="Paycheck Deduction" DataFormatString="{0:c}" />

                                </Columns>
                            </asp:GridView>
                        </section>


                    </div>
                </div>
            </div>
         </div>
        </form>
   

        <footer class="page-footer">
            <div class="footer-container">
                <ul class="footer-list">
                    <li><a href="http://www.boisestate.edu">&copy; 2018 WG3 Web Design</a></li>
                    <li><a href="mailto:wesley.gruenberg@gmail.com">Wes Gruenberg - Author</a> </li>
                    <li class="last-child"><a href="https://www.paylocity.com/">Paylocity Web App</a></li>
                </ul>
            </div>
        </footer>
    </body>

    </html>