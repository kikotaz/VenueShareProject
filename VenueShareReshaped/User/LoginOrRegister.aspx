<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserShared.Master" AutoEventWireup="true" CodeBehind="LoginOrRegister.aspx.cs" Inherits="VenueShareReshaped.User.LoginOrRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login or Register Customer</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center">

            <!--Registration section-->
            <div class="col-5 border-right my-3">
                <div class="mb-3">
                    <h4 class="text-center">Register</h4>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserNameLabel" runat="server"
                        AssociatedControlID="RegisterUserNameInput">Name</asp:Label>
                    <asp:TextBox ID="RegisterUserNameInput" runat="server" MaxLength="20"
                        placeholder="Insert your name" CssClass="form-control"
                        ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserMailLabel" runat="server"
                        AssociatedControlID="RegisterUserMailInput">E-mail</asp:Label>
                    <asp:TextBox ID="RegisterUserMailInput" runat="server" MaxLength="50"
                        placeholder="Insert your E-mail" TextMode="Email"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserPhoneLabel" runat="server"
                        AssociatedControlID="RegisterUserPhoneInput">Phone</asp:Label>
                    <asp:TextBox ID="RegisterUserPhoneInput" runat="server" MaxLength="12"
                        placeholder="Insert your Phone number"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserPasswordLabel" runat="server"
                        AssociatedControlID="RegisterUserPasswordInput">Password</asp:Label>
                    <asp:TextBox ID="RegisterUserPasswordInput" runat="server" MaxLength="12"
                        placeholder="Insert your desired password" TextMode="Password"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="RegisterUserRePassLabel" runat="server"
                        AssociatedControlID="RegisterUserRePassInput">Confirm password</asp:Label>
                    <asp:TextBox ID="RegisterUserRePassInput" runat="server" MaxLength="12"
                        placeholder="Confirm your desired password" TextMode="Password"
                        CssClass="form-control" ValidationGroup="RegisterValidationGroup"></asp:TextBox>
                </div>
                <div class="text-center">
                    <asp:Button ID="registerBtn" runat="server" Text="Register" 
                        CssClass="btn btn-primary col-3 loginBtn my-3" OnClick="registerBtn_Click"
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
            <div class="col-5 my-3">
                <div class="mb-3">
                    <h4 class="text-center">Login</h4>
                </div>
                <div class="form-group">
                    <asp:Label ID="LoginUserEmailLabel" runat="server"
                        AssociatedControlID="LoginUserEmailInput">E-mail</asp:Label>
                    <asp:TextBox ID="LoginUserEmailInput" runat="server" MaxLength="50"
                        TextMode="Email" placeholder="Insert your e-mail address"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="LoginUserPasswordLabel" runat="server"
                        AssociatedControlID="LoginUserPasswordInput">Password</asp:Label>
                    <asp:TextBox ID="LoginUserPasswordInput" runat="server" MaxLength="12"
                        TextMode="Password" placeholder="Insert your password"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class="text-center">
                    <asp:Button ID="loginBtn" runat="server" Text="Login" CssClass="btn btn-primary col-3 loginBtn my-3" OnClick="loginBtn_Click"
                        ValidationGroup="LoginValidationGroup" />
                </div>
                <div class="login-validation">
                    <asp:RequiredFieldValidator ID="LoginEmailValidator" runat="server" Display="None"
                        ErrorMessage="Please provide your E-mail"
                        ControlToValidate="LoginUserEmailInput" SetFocusOnError="true"
                        ValidationGroup="LoginValidationGroup"></asp:RequiredFieldValidator>

                    <asp:RequiredFieldValidator ID="LoginPasswordValidator" runat="server" Display="None"
                        ErrorMessage="Please provide password"
                        ControlToValidate="LoginUserPasswordInput" SetFocusOnError="true"
                        ValidationGroup="LoginValidationGroup"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="LoginUserPasswordInput"
                        ValidationExpression="^[\s\S]{8,12}$" ErrorMessage="Password should be minimum
                    8 characters and maximum 12 characters"
                        ID="PasswordLengthValidator" Display="None"
                        SetFocusOnError="true" ValidationGroup="LoginValidationGroup">
                    </asp:RegularExpressionValidator>

                    <asp:ValidationSummary ID="LoginValidationsSummary" runat="server" ForeColor="Red"
                        DisplayMode="List" HeaderText="Error: " 
                        ValidationGroup="LoginValidationGroup"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptsPlaceHolder" runat="server">
</asp:Content>
