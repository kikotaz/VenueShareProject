<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminShared.Master" AutoEventWireup="true" CodeBehind="AdminAddCustomer.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminAddCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="row justify-content-center">
            <div class="col-5 my-3">
                <div class="mb-3">
                    <h4 class="text-center">Add New Customer</h4>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserNameLabel" runat="server"
                        AssociatedControlID="RegisterUserNameInput">Name</asp:Label>
                    <asp:TextBox ID="RegisterUserNameInput" runat="server" MaxLength="20"
                        placeholder="Insert Customer name" CssClass="form-control"
                        ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserMailLabel" runat="server"
                        AssociatedControlID="RegisterUserMailInput">E-mail</asp:Label>
                    <asp:TextBox ID="RegisterUserMailInput" runat="server" MaxLength="50"
                        placeholder="Insert Customer E-mail" TextMode="Email"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserPhoneLabel" runat="server"
                        AssociatedControlID="RegisterUserPhoneInput">Phone</asp:Label>
                    <asp:TextBox ID="RegisterUserPhoneInput" runat="server" MaxLength="12"
                        placeholder="Insert Customer Phone number"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserPasswordLabel" runat="server"
                        AssociatedControlID="RegisterUserPasswordInput">Password</asp:Label>
                    <asp:TextBox ID="RegisterUserPasswordInput" runat="server" MaxLength="12"
                        placeholder="Insert Customer desired password" TextMode="Password"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserRePassLabel" runat="server"
                        AssociatedControlID="RegisterUserRePassInput">Confirm password</asp:Label>
                    <asp:TextBox ID="RegisterUserRePassInput" runat="server" MaxLength="12"
                        placeholder="Confirm Customer desired password" TextMode="Password"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="text-center">
                    <asp:Button ID="registerBtn" runat="server" Text="Add Customer" 
                        CssClass="btn btn-primary col-4 loginBtn my-3" OnClick="registerBtn_Click"
                        ValidationGroup="RegisterValidationGroup" />
                </div>

                <!--Registration Validations-->
                <div class="register-validation">
                    <asp:RequiredFieldValidator ID="RegisterNameValidator" runat="server" Display="None"
                        ErrorMessage="Please provide your Name"
                        ControlToValidate="RegisterUserNameInput" SetFocusOnError="true"
                        ValidationGroup="RegisterValidationGroup"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredMailValidator" runat="server" Display="None"
                        ErrorMessage="Please provide your E-mail"
                        ControlToValidate="RegisterUserMailInput" SetFocusOnError="true"
                        ValidationGroup="RegisterValidationGroup"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredPhoneValidator" runat="server" Display="None"
                        ErrorMessage="Please provide your Phone number"
                        ControlToValidate="RegisterUserPhoneInput" SetFocusOnError="true"
                        ValidationGroup="RegisterValidationGroup"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredPassValidator" runat="server" Display="None"
                        ErrorMessage="Please provide your Password"
                        ControlToValidate="RegisterUserPasswordInput" SetFocusOnError="true"
                        ValidationGroup="RegisterValidationGroup"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredRePassValidator" runat="server" Display="None"
                        ErrorMessage="Please confirm your password"
                        ControlToValidate="RegisterUserRePassInput" SetFocusOnError="true"
                        ValidationGroup="RegisterValidationGroup"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="CustomerPhoneFormatValidator" runat="server" ControlToValidate="RegisterUserPhoneInput" 
                        ErrorMessage="Please provide valid phone number" Display="None" SetFocusOnError="true" ValidationExpression="^([\+]?(?:00)?[0-9]{1,3}[\s.-]?[0-9]{1,12})([\s.-]?[0-9]{1,4}?)$"
                        ValidationGroup="RegisterValidationGroup"></asp:RegularExpressionValidator>

                    <asp:CompareValidator ID="PassMatchValidator" runat="server" Display="None"
                        ErrorMessage="Password confirmation doesn't match" SetFocusOnError="true"
                        ValidationGroup="RegisterValidationGroup" 
                        ControlToValidate="RegisterUserRePassInput"
                        ControlToCompare="RegisterUserPasswordInput"></asp:CompareValidator>

                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="RegisterUserPasswordInput"
                        ValidationExpression="^[\s\S]{8,12}$" ErrorMessage="Password should be minimum
                    8 characters and maximum 12 characters"
                        ID="RegisterPassFormatValidator" Display="None"
                        SetFocusOnError="true" ValidationGroup="RegisterValidationGroup">
                    </asp:RegularExpressionValidator>

                    <asp:ValidationSummary ID="RegisterValidationSummary" runat="server" ForeColor="Red"
                        DisplayMode="List" HeaderText="Error: " 
                        ValidationGroup="RegisterValidationGroup"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
