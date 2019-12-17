<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="VenueShareReshaped.User.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Contact us</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center my-3">
            <h4>Contact Us</h4>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>You can call or Whatsapp</h4>
            </div>
            <div class="col-6 text-center">
                <img src="../Content/whatsapp.png" />
                <span>
                    &nbsp; 020 - 41589374
                </span>
            </div>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>You can Email us</h4>
            </div>
            <div class="col-6 text-center">
                <img src="../Content/email.png" />
                <span>
                    &nbsp; info@venueshare.com
                </span>
            </div>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>Follow our social media</h4>
            </div>
            <div class="col-6 text-center">
                <img src="../Content/facebook.png" />
                <span>
                    &nbsp; /VenueShareNz
                </span>
            </div>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>Or you can visit our office</h4>
            </div>
            <div class="col-6 text-center">
                <span>
                    Address: 8 Bankside Street, Auckland
                </span>
            </div>
        </div>
        <div class="row justify-content-center align-items-center mb-4">
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3192.7973560769447!2d174.7675022150269!3d-36.8473289871322!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x6d0d47fc5f44d09f%3A0xaf86f1c36f1fae2f!2s8+Bankside+St%2C+Auckland%2C+1010!5e0!3m2!1sen!2snz!4v1555151927983!5m2!1sen!2snz" width="600" height="450" frameborder="0" style="border:0" allowfullscreen></iframe>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptsPlaceHolder" runat="server">
</asp:Content>
