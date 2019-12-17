<%@ Page Title="Show Proposals" Language="C#" MasterPageFile="~/Admin/AdminShared.Master" AutoEventWireup="true" CodeBehind="AdminProposals.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminProposals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="admin-proposals-item text-center">
            <asp:UpdatePanel ID="ListPanel" runat="server">
                <ContentTemplate>
                    <asp:ListView ID="ProposalsList" runat="server"
                        OnPreRender="ProposalsList_PreRender"
                        DataKeyNames="ProposalID">
                        <EmptyItemTemplate>
                            No proposals to show for now
                        </EmptyItemTemplate>
                        <ItemTemplate>
                            <div class="row align-items-center mx-1 border-bottom" runat="server">
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
                                    Proposed by: <%#Eval("CustomerName") %>
                                    <br />
                                    Proposal date: <%#Eval("ProposedDate") %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
