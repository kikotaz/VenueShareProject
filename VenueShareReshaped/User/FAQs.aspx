<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="FAQs.aspx.cs" Inherits="VenueShareReshaped.User.FAQs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Frequently Asked Questions</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center my-3">
            <h4>Frequently Asked Questions</h4>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>How to post my venue?</h4>
            </div>
            <div class="col-6 text-center">
                <span>Simply click on this link <a href="SubmitVenue.aspx">Here</a>, and
                start filling down your venue details. Once submitted, your venue will
                be reviewed by our team for approval.
                </span>
            </div>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>How to show my interest in venue?</h4>
            </div>
            <div class="col-6 text-center">
                <span>You can follow this link <a href="ListVenues.aspx">Here</a>
                    and find the right venue for you. Once found, just click propose,
                    fill the small form, and we will contact you.
                </span>
            </div>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>How long it takes?</h4>
            </div>
            <div class="col-6 text-center">
                <span>Usually everything from approval to proposal review take
                    less than one day. Sometimes we even contact you on the same
                    day. We are very energetic to serve you the best way.
                </span>
            </div>
        </div>
        <div class="row justify-content-center align-items-center my-3">
            <div class="col-4">
                <h4>How much do you charge?</h4>
            </div>
            <div class="col-6 text-center">
                <span>For posting a venue or proposal we are completely <strong>FREE</strong>
                    However, we only charge the event owners when they use our services.
                </span>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptsPlaceHolder" runat="server">
</asp:Content>
