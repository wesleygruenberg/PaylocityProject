<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PaylocityDeductionCalculator._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>PAYLOCITY WEB APP</h1>
        <p class="lead">Welcome to the main page for this web application designed using C# and ASP.NET</p>
        <p><a href="BenefitsDashboard.aspx" class="btn btn-primary btn-lg">GO TO BENEFITS DASHBOARD</a></p>
    </div>

    <div class="row">
        <div class="col-md-12">
            <h2>About the project</h2>
            <p>
                This is my second project using Visual Studio and C# and my first creating a web application using ASP.NET. 
                Despite my lack of experience with these technologies, I chose to utilize them for a couple reasons:
            </p>
            <ul>
                <li>Pick the right tool for the job: this is an ideal medium for creating a web app with 
                    object oriented programming beneath the surface.
                </li>
                <li>Paylocity uses these tools and I want to work for Paylocity. If I spend some time learning more 
                    about the technology before starting, I will be that much more prepared as I begin my career with you.
                </li>
            </ul>
           
        </div>
        
    </div>
            <footer class="page-footer">
            <div class="footer-container">
                <ul class="footer-list">
                    <li><a href="http://www.boisestate.edu">&copy; 2018 WG3 Web Design</a></li>
                    <li><a href="mailto:wesley.gruenberg@gmail.com">Wes Gruenberg - Author</a> </li>
                    <li class="last-child"><a href="https://www.paylocity.com/">Paylocity Web App</a></li>
                </ul>
            </div>
        </footer>
</asp:Content>
