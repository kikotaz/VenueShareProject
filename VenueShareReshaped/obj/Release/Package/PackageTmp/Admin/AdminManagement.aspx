<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminShared.Master" AutoEventWireup="true" CodeBehind="AdminManagement.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="addAdmin" class="container my-3">
        <!--Adding new Administrator section starts here-->
        <div class="form-row">
            <div class="col-12 text-center">
                <h4 class="my-3">Add new Admin</h4>
            </div>
        </div>
        <div class="form-row justify-content-center">
            <div class="form-group col-5">
                <asp:Label ID="AdminNameLabel" AssociatedControlID="AdminNameInput" runat="server">Admin Name</asp:Label>
                <asp:TextBox ID="AdminNameInput" runat="server" CssClass="form-control" placeholder="Insert Admin Name" MaxLength="30"></asp:TextBox>
            </div>
            <div class="form-group col-5">
                <asp:Label ID="AdminEmailLabel" AssociatedControlID="AdminEmailInput" runat="server">Email</asp:Label>
                <asp:TextBox ID="AdminEmailInput" runat="server" CssClass="form-control" placeholder="Insert Admin E-mail" TextMode="Email" MaxLength="50"></asp:TextBox>
            </div>
        </div>
        <div class="form-row justify-content-center">
            <div class="form-group col-5">
                <asp:Label ID="AdminPasswordLabel" AssociatedControlID="AdminPasswordInput" runat="server">Password</asp:Label>
                <asp:TextBox ID="AdminPasswordInput" runat="server" CssClass="form-control" placeholder="Insert Admin Password" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-group col-5">
                <asp:RequiredFieldValidator ID="NameValidator" runat="server" Display="None"
                    ErrorMessage="Please provide the Admin's name" ControlToValidate="AdminNameInput"
                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="EmailValidator" runat="server" Display="None"
                    ErrorMessage="Please provide Admin's E-mail" ControlToValidate="AdminEmailInput" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="PasswordValidator" runat="server" Display="None"
                    ErrorMessage="Please provide password" ControlToValidate="AdminPasswordInput"
                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="AdminPasswordInput"
                    ValidationExpression="^[\s\S]{8,12}$" ErrorMessage="Password should be minimum
                    8 characters and maximum 12 characters"
                    ID="PasswordLengthValidator" Display="None"
                    SetFocusOnError="true"></asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="ValidationsSummary" runat="server" ForeColor="Red"
                    DisplayMode="List" HeaderText="Error: " />
            </div>
        </div>
        <div class="form-row justify-content-center">
            <asp:Button ID="AddAdminBtn" runat="server" Text="Submit"
                CssClass="loginBtn btn btn-primary my-3" OnClick="AddAdminBtn_Click" />
        </div>
        <!--Adding new Administrator section ends here-->

        <!--Showing the Admins list section starts here-->
        <div class="admin-list">
            <asp:UpdatePanel ID="ListPanel" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="AdminGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="AdminID" DataSourceID="AdminsDataSource" CssClass="w-75 mx-auto my-3 text-center ">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"
                                HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"
                                HeaderStyle-Font-Size="Large" />
                            <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-secondary"
                                CausesValidation="false" />
                        </Columns>
                        <EmptyDataTemplate>
                            There are no data to show for now
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:SqlDataSource ID="AdminsDataSource" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [tblAdmins]" UpdateCommand="UPDATE [tblAdmins] SET Name = @Name,
                Email = @Email WHERE AdminID = @AdminID">
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
