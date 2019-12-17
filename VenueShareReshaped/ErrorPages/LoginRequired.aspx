<%@ Page Title="Login Required" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="LoginRequired.aspx.cs" Inherits="VenueShareReshaped.ErrorPages.LoginRequired" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login Required</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <div class="text-center">
            <div class="my-3">
                <img class="img-404" src="../Content/error-403.png" alt="403 error image" />
            </div>
            <div class="text-center">
                <h4>Sorry, the page you requested requires Login</h4>
                <br />
                <h4>To register new account or login to existing account, please <a href="../User/LoginOrRegister.aspx">click here</a></h4>
            </div>
        </div>
    </div>
</asp:Content>