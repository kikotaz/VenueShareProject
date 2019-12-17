<%@ Page Title="Manage Customers" Language="C#" MasterPageFile="~/Admin/AdminShared.Master" AutoEventWireup="true" CodeBehind="AdminManageCustomer.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminManageCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="customer-list">
            <asp:UpdatePanel runat="server" ID="ListPanel">
                <ContentTemplate>
                    <div id="SearchPanel" class="row justify-content-center my-3 mx-auto">
                        <div class="form-group col-4">
                            <asp:Label ID="SearchCustomerLabel" runat="server">
                                Search Customer by name or e-mail (press Enter)
                            </asp:Label>
                            <asp:TextBox ID="SearchCustomerInput" runat="server" 
                                AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <asp:GridView ID="CustomerGrid" runat="server" AllowSorting="true"
                        AutoGenerateColumns="false" DataKeyNames="CustomerID"
                        CssClass="w-75 mx-auto my-3 text-center" DataSourceID="CustomersDataSource"
                        OnPreRender="CustomerGrid_PreRender"
                        OnRowDeleted="CustomerGrid_RowDeleted"
                        OnRowUpdated="CustomerGrid_RowUpdated"
                        AllowPaging="true" PageSize="10" PagerStyle-HorizontalAlign="Center"
                        PagerStyle-Font-Size="Large" PagerSettings-Mode="NumericFirstLast">
                        <Columns>
                            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name"
                                SortExpression="CustomerName" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="CustomerMail" HeaderText="Email"
                                SortExpression="CustomerMail" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="CustomerPhone" HeaderText="Phone"
                                SortExpression="CustomerPhone" HeaderStyle-Font-Size="Large" />
                            <asp:CommandField ShowEditButton="True"
                                ControlStyle-CssClass="btn btn-secondary my-1" CausesValidation="false" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete"
                                        Text="Delete" CssClass="btn btn-primary alert-danger my-1"
                                        OnClientClick="return confirm('Are you sure you want to delete?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            There are no data to show for now
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:SqlDataSource ID="CustomersDataSource" runat="server"
                ProviderName="System.Data.SqlClient" UpdateCommand="UPDATE Customers SET 
                CustomerName = @CustomerName, CustomerMail = @CustomerMail, CustomerPhone =
                @CustomerPhone WHERE CustomerID = @CustomerID"
                DeleteCommand="DELETE FROM Customers WHERE CustomerID = @CustomerID">
                <UpdateParameters>
                    <asp:Parameter Name="CustomerName" Type="String" />
                    <asp:Parameter Name="CustomerMail" Type="String" />
                    <asp:Parameter Name="CustomerPhone" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
