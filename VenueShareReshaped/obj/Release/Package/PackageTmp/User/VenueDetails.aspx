<%@ Page Title="Venue Details" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="VenueDetails.aspx.cs" Inherits="VenueShareReshaped.User.VenueDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="venue-details text-center">
            <asp:UpdatePanel ID="ListPanel" runat="server">
                <ContentTemplate>
                    <!--Images Section-->
                    <div class="row justify-content-center my-3" runat="server">
                        <asp:Repeater runat="server" ID="ImageRepeater"
                            OnLoad="ImageRepeater_Load">
                            <ItemTemplate>
                                <asp:HyperLink ImageUrl="<%#Container.DataItem %>"
                                    Target="_blank" runat="server" ImageWidth="200px"
                                    NavigateUrl="<%#Container.DataItem %>" CssClass="mx-auto"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!--Details Section-->
                    <asp:ListView ID="VenueDetailsView" runat="server" DataKeyNames="VenueID"
                        OnPreRender="VenueDetailsView_PreRender"
                        OnSelectedIndexChanging="VenueDetailsView_SelectedIndexChanging">
                        <EmptyItemTemplate>
                            <div class="row w-50 text-center">
                                The Venue you requested doesn't exist. It might have been deleted
                                by the Admin, or the chosen link is corrupted. Please contact Admin
                                to get further details.
                            </div>
                        </EmptyItemTemplate>
                        <ItemTemplate>
                            <div class="row justify-content-center mb-3">
                                <h4><%# Eval("Name") %></h4>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-6" style="font-size: 21px;">
                                    Address: <%#Eval("Address") %>
                                    <br />
                                    City: <%#Eval("City") %>
                                    <br />
                                    Area: <%#Eval("Area") %> m<sup>2</sup>
                                    <br />
                                    Capacity: <%#Eval("Capacity") %> persons
                                    <br />
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-8 jumbotron container-fluid jumbo my-3">
                                    <h4>Description</h4>
                                    <br />
                                    <span style="font-size:21px;">
                                        <%#Eval("Description") %>
                                    </span> 
                                </div>
                            </div>
                            <div class="row w-100 justify-content-center 
                                        align-content-center border-bottom m-0">
                                        <asp:LinkButton runat="server" ID="ToBasketButton" 
                                            Text="Add to Basket"
                                            CssClass="btn btn-primary loginBtn my-2"
                                            CommandName="Select" />
                                    </div>
                        </ItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptsPlaceHolder" runat="server">
</asp:Content>
