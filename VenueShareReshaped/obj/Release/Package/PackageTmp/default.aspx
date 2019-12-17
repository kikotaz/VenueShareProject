<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="VenueShareReshaped._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="welcome text-center">
            <h4 class="my-2">Welcome to Venue Share</h4>
            <div class="row justify-content-center">
                <div class="col-5 my-3">
                    <h4>For venue seekers</h4>
                    <span>Need to hold an event?
                        Need to find the right venue with the right budget?
                        You don't have experience with evenet management?
                    </span>
                    <h5 class="jumbotron my-2 mx-1">We can help you in 3 simple steps</h5>
                    <ol class="font-weight-bold">
                        <li>Register and click submit venue at the top
                        </li>
                        <li>Fill in the details for your venue and wait for approval
                        </li>
                        <li>We will contact you once we get a proposal for your venue
                        </li>
                    </ol>
                </div>
                <div class="col-5 my-3">
                    <h4>For venue owners</h4>
                    <span>Need to share your venue for money?
                        Need to find the right people to rent your place?
                        You don't have experience with event management?
                    </span>
                    <h5 class="jumbotron my-2 mx-1">We can help you in 3 simple steps</h5>
                    <ol class="font-weight-bold">
                        <li>Login and check our list of venues below or from the top
                        </li>
                        <li>Make a proposal
                        </li>
                        <li>We will get back to you in less than one day
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row justify-content-center">
                <h4>Our Promoted venues</h4>
            </div>
            <div class="user-venue-item text-center">
                <asp:UpdatePanel ID="ListPanel" runat="server">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="HomeVenues"
                            OnLoad="HomeVenues_Load"
                            DataKeyNames="VenueID"
                            OnSelectedIndexChanging="HomeVenues_SelectedIndexChanging">
                            <EmptyItemTemplate>
                                No Promoted venues at the moment
                            </EmptyItemTemplate>
                            <ItemTemplate>
                                <div class="row align-items-center mx-1" runat="server">
                                    <div class="col-3 mx-auto my-2">
                                        <asp:Image ImageUrl='<%# GetImages(Eval("Images").ToString()) %>'
                                            runat="server" CssClass="w-50 mb-1" /></td>
                                <h5><%# Eval("Name") %></h5>
                                    </div>
                                    <div class="col-4 border-left">
                                        Address: <%#Eval("Address") %>
                                        <br />
                                        City: <%#Eval("City") %>
                                        <br />
                                        Area: <%#Eval("Area") %> m<sup>2</sup>
                                        <br />
                                        Capacity: <%#Eval("Capacity") %> persons
                                    </div>
                                    <div class="col-5 border-left">
                                        <span><%#Eval("Description") %></span>
                                        <br />
                                        <br />
                                        Owner: <%#Eval("CustomerName") %>
                                    </div>
                                    <div class="row w-100 justify-content-center 
                                        align-content-center border-bottom m-0">
                                        <asp:LinkButton runat="server" ID="DetailsButton" 
                                            Text="View Details"
                                            CssClass="btn btn-primary loginBtn my-2"
                                            CommandName="Select" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptsPlaceHolder" runat="server">
</asp:Content>
