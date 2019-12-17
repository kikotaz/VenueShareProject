<%@ Page Title="404 - Page Not Found" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs" Inherits="VenueShareReshaped.ErrorPages.PageNotFound" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center">
            <div>
                <img class="img-404" src="../Content/error-404.png" alt="404 error image" />
            </div>
            <div class="text-center">
                <h4>Sorry, the page you requested doesn't exist</h4>
                <br />
                <h4>Please check the provided link, or <a href="../default.aspx">click here</a></h4>
                <h4> To go to home page</h4>
            </div>
        </div>
    </div>
</asp:Content>
