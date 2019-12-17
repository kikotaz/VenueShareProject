<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../Content/Styles.css?v=1" />
</head>
<body>
    <div id="loginSection" class="container w-50 col-6 mt-5">
        <div class="loginHead">
            <img src="../Content/VSLogo.png" />
        </div>
        <h4 class="text-center mt-4">Administrator Login</h4>
        <div class="row justify-content-center">
            <form id="loginForm" runat="server" method="post" class="col-8">
                <div class="form-group my-3">
                    <asp:Label ID="emailLabel" AssociatedControlID="emailInput" runat="server">E-Mail</asp:Label>
                    <asp:TextBox ID="emailInput" runat="server" CssClass="form-control" placeholder="Please enter your e-mail" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group my-3">
                    <asp:Label ID="passwordLabel" AssociatedControlID="passwordInput" runat="server">Password</asp:Label>
                    <asp:TextBox ID="passwordInput" runat="server" CssClass="form-control" placeholder="Please enter your password" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-check">
                    <asp:CheckBox ID="stayLogged" runat="server" Text="&nbspStay logged in"/>
                </div>
                
                <div class="text-center">
                    <asp:Button ID="loginBtn" runat="server" Text="Login" CssClass="btn btn-primary col-3 loginBtn my-3" OnClick="loginBtn_Click" />
                </div>

                <div class="validation mb-3">
                    <asp:RequiredFieldValidator id="emailValidator" runat="server" ControlToValidate="emailInput"
                        ErrorMessage="Please provide your E-Mail address" Display="None"></asp:RequiredFieldValidator>

                    <asp:RequiredFieldValidator id="passwordValidator" runat="server" ControlToValidate="passwordInput"
                        ErrorMessage="Please provide your password" Display="None"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator id="emailFormatValidator" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="emailInput" ErrorMessage="Please check your e-mail format" Display="None"></asp:RegularExpressionValidator>

                    <asp:RegularExpressionValidator runat="server" ControlToValidate="passwordInput"
                    ValidationExpression="^[\s\S]{8,12}$" ErrorMessage="Password should be minimum
                    8 characters and maximum 12 characters"
                    ID="PasswordLengthValidator" Display="None"
                    SetFocusOnError="true"></asp:RegularExpressionValidator>

                    <asp:ValidationSummary id="validationSummary" runat="server" HeaderText="Error: " DisplayMode="List" ForeColor="Red"></asp:ValidationSummary>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
