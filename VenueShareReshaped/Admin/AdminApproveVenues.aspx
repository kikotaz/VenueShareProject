<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminShared.Master" AutoEventWireup="true" CodeBehind="AdminApproveVenues.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminApproveVenues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="venue-item text-center">
            <asp:UpdatePanel ID="ListPanel" runat="server">
                <ContentTemplate>
                    <asp:ListView runat="server" ID="UnApprovedList"
                        OnPreRender="UnApprovedList_PreRender"
                        OnSelectedIndexChanging="UnApprovedList_SelectedIndexChanging"
                        OnItemEditing="UnApprovedList_ItemEditing"
                        OnItemUpdating="UnApprovedList_ItemUpdating"
                        OnItemDeleting="UnApprovedList_ItemDeleting"
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
                                <asp:LinkButton runat="server" ID="ApproveButton" Text="Approve"
                                    CssClass="btn btn-primary mx-2 loginBtn my-2" CommandName="Select" />
                                <asp:LinkButton runat="server" ID="DeleteButton" Text="Delete"
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
                                <span class="alert-danger">Are you sure you want to approve this venue?</span>
                            </div>
                            <div class="row justify-content-center align-items-center border-bottom m-0">
                                <asp:LinkButton runat="server" ID="ApproveVenueButton" Text="Approve"
                                    CssClass="btn btn-primary mx-2 loginBtn my-2" CommandName="Update" />
                                <asp:LinkButton runat="server" ID="CancelApproveButton" Text="Cancel"
                                    CssClass="btn btn-primary mx-2 alert-danger my-2" CommandName="Cancel"
                                    OnClick="CancelApproveButton_Click" />
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
                                <span class="alert-danger">Are you sure you want to delete this venue?
                                </span>
                            </div>
                            <div class="row justify-content-center align-items-center border-bottom m-0">
                                <asp:LinkButton runat="server" ID="DeleteVenueButton" Text="Delete"
                                    CssClass="btn btn-primary mx-2 alert-danger my-2" 
                                    CommandName="Delete" 
                                    OnClientClick="return confirm('Are you sure you want to delete?');"/>
                                <asp:LinkButton runat="server" ID="CancelDeleteButton" Text="Cancel"
                                    CssClass="btn btn-primary mx-2 loginBtn my-2" CommandName="Cancel"
                                    OnClick="CancelDeleteButton_Click" />
                            </div>
                        </EditItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
