<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminShared.Master" AutoEventWireup="true" CodeBehind="AdminEditVenues.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminEditVenues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="venue-item text-center">
            <asp:UpdatePanel ID="ListPanel" runat="server">
                <ContentTemplate>
                    <h5 class="text-center my-3">Please fill your search criteria and press Enter (Maximum 2 criterias).</h5>
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
                                Search by venue name
                            </asp:Label>
                            <asp:TextBox ID="SearchVenueNameInput" runat="server"
                                AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-4">
                            <asp:Label ID="SearchCustomerLabel" runat="server">
                                search by customer name or e-mail
                            </asp:Label>
                            <asp:TextBox ID="SearchCustomerInput" runat="server"
                                AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <asp:ListView ID="EditVenuesList" runat="server"
                        OnPreRender="EditVenuesList_PreRender"
                        OnSelectedIndexChanging="EditVenuesList_SelectedIndexChanging"
                        OnItemEditing="EditVenuesList_ItemEditing"
                        OnItemUpdating="EditVenuesList_ItemUpdating"
                        OnItemDeleting="EditVenuesList_ItemDeleting"
                        DataKeyNames="VenueID">
                        <EmptyDataTemplate>
                            No Items to show for now
                        </EmptyDataTemplate>
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
                                    <br />
                                    Promoted: <span class="alert-info">
                                        <%# Eval("IsPromoted").ToString() == "True" ? "Yes"
                                                : "No" %>
                                    </span>
                                </div>
                                <div class="col-5 border-left">
                                    <span><%#Eval("Description") %></span>
                                    <br />
                                    <br />
                                    Owner: <%#Eval("CustomerName") %>
                                    <br />
                                    E-mail: <%#Eval("CustomerMail") %>
                                    <br />
                                    Phone: <%#Eval("CustomerPhone") %>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center border-bottom m-0">
                                <asp:LinkButton runat="server" ID="PromoteButton"
                                    Text='<%# Eval("IsPromoted").ToString() == "True" ? "Demote"
                                                : "Promote" %>'
                                    CssClass="btn btn-primary mx-2 loginBtn my-2" CommandName="Select" />
                                <asp:LinkButton runat="server" ID="EditButton" Text="Edit"
                                    CssClass="btn btn-primary mx-2 alert-link my-2" CommandName="Edit" />
                            </div>
                        </ItemTemplate>
                        <SelectedItemTemplate>
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
                                    <br />
                                    Promoted: <span class="alert-info">
                                        <%# Eval("IsPromoted").ToString() == "True" ? "Yes"
                                                : "No" %>
                                    </span>
                                </div>
                                <div class="col-5 border-left">
                                    <span><%#Eval("Description") %></span>
                                    <br />
                                    <br />
                                    Owner: <%#Eval("CustomerName") %>
                                    <br />
                                    E-mail: <%#Eval("CustomerMail") %>
                                    <br />
                                    Phone: <%#Eval("CustomerPhone") %>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center m-0">
                                <asp:Label ID="PromoteLabel" CssClass="alert-danger" runat="server">
                                    <%# Eval("IsPromoted").ToString() == "True" ? 
                                            "Are you sure you want to demote this venue?"
                                                : "Are you sure you want to promote this venue?" %>
                                </asp:Label>
                            </div>
                            <div class="row justify-content-center align-items-center border-bottom m-0">
                                <asp:LinkButton runat="server" ID="PromoteVenueButton"
                                    Text='<%# Eval("IsPromoted").ToString() == "True" ? "Demote"
                                                : "Promote" %>'
                                    CssClass="btn btn-primary mx-2 loginBtn my-2" CommandName="Update"
                                    CommandArgument='<%# Eval("IsPromoted").ToString() == "True" ? "Demote"
                                                : "Promote" %>'
                                    OnCommand="PromoteVenueButton_Command" />
                                <asp:LinkButton runat="server" ID="CancelPromoteButton" Text="Cancel"
                                    CssClass="btn btn-primary mx-2 alert-danger my-2" CommandName="Cancel"
                                    OnClick="CancelPromoteButton_Click" />
                            </div>
                        </SelectedItemTemplate>
                        <EditItemTemplate>
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
                                    <br />
                                    Promoted: <span class="alert-info">
                                        <%# Eval("IsPromoted").ToString() == "True" ? "Yes"
                                                : "No" %>
                                    </span>
                                </div>
                                <div class="col-5 border-left">
                                    <span><%#Eval("Description") %></span>
                                    <br />
                                    <br />
                                    Owner: <%#Eval("CustomerName") %>
                                    <br />
                                    E-mail: <%#Eval("CustomerMail") %>
                                    <br />
                                    Phone: <%#Eval("CustomerPhone") %>
                                </div>
                            </div>
                            <div class="row justify-content-center align-items-center border-bottom m-0">
                                <asp:LinkButton runat="server" ID="EditContentButton" Text="Edit Content"
                                    CssClass="btn btn-primary mx-2 alert-link my-2"
                                    OnClick="EditContentButton_Click" />
                                <asp:LinkButton runat="server" ID="DeleteButton" Text="Delete"
                                    CssClass="btn btn-primary mx-2 alert-danger my-2"
                                    CommandName="Delete"
                                    OnClientClick="return confirm('Are you sure you want to delete?');" />
                                <asp:LinkButton runat="server" ID="CancelEditButton" Text="Cancel"
                                    CssClass="btn btn-primary mx-2 loginBtn my-2" CommandName="Cancel"
                                    OnClick="CancelEditButton_Click" />
                            </div>
                        </EditItemTemplate>
                    </asp:ListView>
                    <asp:DataPager ID="EditVenuesPager" runat="server" PagedControlID="EditVenuesList"
                        PageSize="3">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonCssClass="btn btn-light mr-2"
                                ShowNextPageButton="false" ShowPreviousPageButton="true" />

                            <asp:NumericPagerField ButtonCount="4" />

                            <asp:NextPreviousPagerField ButtonCssClass="btn btn-light ml-2"
                                ShowNextPageButton="true" ShowPreviousPageButton="false" />
                        </Fields>
                    </asp:DataPager>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
