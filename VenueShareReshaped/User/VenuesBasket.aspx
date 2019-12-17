<%@ Page Title="Your Basket" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="VenuesBasket.aspx.cs" Inherits="VenueShareReshaped.User.VenuesBasket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="basket-details text-center">
            <asp:UpdatePanel ID="ListPanel" runat="server">
                <ContentTemplate>
                     <asp:Calendar runat="server" ID="ProposalDateInput"
                          CssClass="mx-auto" ClientIDMode="Static"/>      
                    <asp:ListView ID="BasketList" runat="server"
                        OnPreRender="BasketList_PreRender"
                        OnItemDeleting="BasketList_ItemDeleting"
                        OnSelectedIndexChanging="BasketList_SelectedIndexChanging"
                        DataKeyNames="VenueID">
                        <EmptyItemTemplate>
                            You have no items in your basket
                        </EmptyItemTemplate>
                        <EmptyDataTemplate>
                            You have no items in your basket
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <div class="row align-items-center mx-1 my-3" runat="server">
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
                                    Owner: <%#Eval("CustomerName") %>
                                    <br />
                                </div>
                                <div class="row w-100 justify-content-center 
                                        align-content-center border-bottom m-0">
                                    <asp:LinkButton runat="server" ID="ProposeButton"
                                        Text="Propose"
                                        CssClass="btn btn-primary loginBtn my-2 mx-1"
                                        CommandName="Select" />
                                    <asp:LinkButton runat="server" ID="RemoveButton"
                                        Text="Remove"
                                        CssClass="btn btn-primary alert-danger my-2 mx-1"
                                        CommandName="Delete" />
                                </div>
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
