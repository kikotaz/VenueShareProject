<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="UnknownError.aspx.cs" Inherits="VenueShareReshaped.ErrorPages.UnknownError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Unknown Error</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center">
            <div>
                <img class="img-404" src="../Content/error-unknown.png" alt="unknown error image" />
            </div>
            <div class="text-center">
                <h4>Sorry, unknown error has occured. Could be a server problem.</h4>
                <br />
                <h4>Please come back later, or <a href="../default.aspx">click here</a></h4>
                <h4> To go to home page</h4>
            </div>
        </div>
    </div>
</asp:Content>