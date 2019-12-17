<%@ Page Title="Browse Venues" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="ApprovedVenues.aspx.cs" Inherits="VenueShareReshaped.User.ApprovedVenues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="user-venue-item text-center">
            <asp:UpdatePanel ID="ListPanel" runat="server">
                <ContentTemplate>
                    <h5 class="text-center my-3">Please fill your search criteria and press Enter</h5>
                    <div id="SearchPanel" class="row justify-content-center my-3 mx-auto">
                        <div class="form-group col-4">
                            <asp:Label ID="SearchLocationLabel" runat="server">
                                Search by location
                            </asp:Label>
                            <asp:DropDownList ID="SearchLocationList" runat="server"
                                AutoPostBack="true" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-4">
                            <asp:Label ID="SearchVenueNameLabel" runat="server">
                                Search by venue name or description
                            </asp:Label>
                            <asp:TextBox ID="SearchVenueInfoInput" runat="server"
                                AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <asp:ListView runat="server" ID="ApprovedList"
                        OnPreRender="ApprovedList_PreRender"
                        OnSelectedIndexChanging="ApprovedList_SelectedIndexChanging"
                        DataKeyNames="VenueID">
                        <EmptyItemTemplate>
                            No approved venues to show at the moment
                        </EmptyItemTemplate>
                        <LayoutTemplate>
                            <div class="row justify-content-center my-3">
                                <span class="my-auto">Sort list by</span>
                                <div class="col-2">
                                    <asp:LinkButton ID="AlphabaticalSort" runat="server"
                                        Text="Alphabatic" OnClick="AlphabaticalSort_Click"
                                        CssClass="btn btn-outline-info"></asp:LinkButton>
                                </div>
                                <div class="col-2">
                                    <asp:LinkButton ID="CapacitySort" runat="server"
                                        Text="Capacity" OnClick="CapacitySort_Click"
                                        CssClass="btn btn-outline-info"></asp:LinkButton>
                                </div>
                                <div class="col-2">
                                    <asp:LinkButton ID="AreaSort" runat="server"
                                        Text="Area Size" OnClick="AreaSort_Click"
                                        CssClass="btn btn-outline-info"></asp:LinkButton>
                                </div>
                            </div>

                            <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
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
                    <asp:DataPager ID="ApprovedVenuesPager" runat="server" PagedControlID="ApprovedList"
                        PageSize="3">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonCssClass="btn btn-light mr-2"
                                ShowNextPageButton="false" ShowPreviousPageButton="true" />

                            <asp:NumericPagerField ButtonCount="5" />

                            <asp:NextPreviousPagerField ButtonCssClass="btn btn-light ml-2"
                                ShowNextPageButton="true" ShowPreviousPageButton="false" />
                        </Fields>
                    </asp:DataPager>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptsPlaceHolder" runat="server">
</asp:Content>
