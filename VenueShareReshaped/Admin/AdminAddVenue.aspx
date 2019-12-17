<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminShared.Master" AutoEventWireup="true" CodeBehind="AdminAddVenue.aspx.cs" Inherits="VenueShareReshaped.Admin.AdminAddVenue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-3">
        <div class="form-row w-75 mx-auto">
            <div class="form-group my-3 col-6">
                <asp:Label ID="venueNameLabel" AssociatedControlID="venueNameInput" runat="server">
                        Venue Name
                </asp:Label>
                <asp:TextBox ID="venueNameInput" runat="server" CssClass="form-control" placeholder="Please enter your desired Venue's name" MaxLength="30"></asp:TextBox>
            </div>
            <div class="form-group my-3 col-6">
                <asp:Label ID="venueAddressLabel" AssociatedControlID="venueAddressInput" runat="server">
                        Address
                </asp:Label>
                <asp:TextBox ID="venueAddressInput" runat="server" CssClass="form-control" placeholder="Please enter the Venue's address" MaxLength="30"></asp:TextBox>
            </div>
        </div>
        <div class="form-row w-75 mx-auto">
            <div class="form-group my-3 col-4">
                <asp:Label ID="venueCityLabel" AssociatedControlID="venueCityInput" runat="server">
                            City
                </asp:Label>
                <asp:DropDownList ID="venueCityInput" runat="server" AutoPostBack="false" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group my-3 col-4">
                <asp:Label ID="venueAreaLabel" AssociatedControlID="venueAreaInput" runat="server">
                </asp:Label>
                <asp:TextBox ID="venueAreaInput" runat="server" CssClass="form-control" placeholder="Please specify the area" TextMode="Number"></asp:TextBox>
            </div>
            <div class="form-group my-3 col-4">
                <asp:Label ID="venueCapacityLabel" AssociatedControlID="venueCapacityInput" runat="server">
                            Capacity (Persons)
                </asp:Label>
                <asp:TextBox ID="venueCapacityInput" runat="server" CssClass="form-control" placeholder="Maximum capacity" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="form-row w-75 mx-auto">
            <div class="form-group my-3 col-10">
                <asp:Label ID="venueDescriptionLabel" AssociatedControlID="venueDescriptionInput" runat="server">
                            Description
                </asp:Label>
                <asp:TextBox ID="venueDescriptionInput" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" MaxLength="300" onkeyup="CharCount(this)"></asp:TextBox>
                <asp:Label ID="descriptionCount" runat="server">Please describe your venue in 300 characters. Remaining </asp:Label>
                <asp:Label ID="descriptionRemaining" runat="server" ClientIDMode="Static"></asp:Label>
            </div>
        </div>
        <div class="form-row w-75 mx-auto">
            <div class="form-group my-3 col-6">
                <asp:Label ID="venueImagesLabel" AssociatedControlID="venueImagesUpload" runat="server">
                            Images
                </asp:Label>
                <asp:FileUpload ID="venueImagesUpload" runat="server" AllowMultiple="true" accept="image/jpeg,image/png" CssClass="form-control border-0 mb-2" ClientIDMode="Static" />
                <asp:Label ID="venueImagesUploaded" runat="server" ClientIDMode="Static"></asp:Label>
            </div>
        </div>
        <%if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {%>
        <div class="form-row w-75 mx-auto">
            <div class="form-group my-3 col-6">
                <asp:Label ID="CustomerEmailLabel" runat="server" AssociatedControlID="CustomerEmailInput">
                            Customer E-mail
                </asp:Label>
                <asp:TextBox ID="CustomerEmailInput" runat="server" CssClass="form-control" placeholder="Provide Customer's E-mail" onchange="callServer(this)" TextMode="Email" MaxLength="50"></asp:TextBox>
            </div>
            <div class="form-group my-3 col-6">
                <asp:Label ID="CustomerIDLabel" runat="server">Customer ID: </asp:Label>
                <asp:Label ID="CustomerIDResultLabel" runat="server" ClientIDMode="Static"></asp:Label>
            </div>
        </div>
        <%}
        %>
        <div class="form-row w-75 mx-auto my-3">
            <asp:RequiredFieldValidator ID="venueNameValidator" runat="server" ControlToValidate="venueNameInput" ErrorMessage="Please provide Venue Name" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RequiredFieldValidator ID="venueAddressValidator" runat="server" ControlToValidate="venueAddressInput" ErrorMessage="Please provide Venue Address" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RequiredFieldValidator ID="venueCityValidator" runat="server" ControlToValidate="venueCityInput" ErrorMessage="Please choose a city" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RequiredFieldValidator ID="venueAreaValidator" runat="server" ControlToValidate="venueAreaInput" ErrorMessage="Please provide the venue's area" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RequiredFieldValidator ID="venueCapaciyValidator" runat="server" ControlToValidate="venueCapacityInput" ErrorMessage="Please provide the venue's capacity" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RequiredFieldValidator ID="venueDescriptionValidator" runat="server" ControlToValidate="venueDescriptionInput" ErrorMessage="Please add some description for your venue" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RequiredFieldValidator ID="venueImagesValidaotr" runat="server" ControlToValidate="venueImagesUpload" ErrorMessage="Please upload at least one image" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RequiredFieldValidator ID="CustomerEmailValidator" runat="server"
                ControlToValidate="CustomerEmailInput" ErrorMessage="Please insert customer's e-mail"
                Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:ValidationSummary ID="validationSummary" runat="server" HeaderText="Error: " DisplayMode="List" ForeColor="Red" />
        </div>
        <div class="text-center">
            <asp:Button ID="submitVenueBtn" runat="server" Text="Submit" CssClass="btn btn-primary col-3 loginBtn my-3" OnClick="submitVenueBtn_Click" />
        </div>
    </div>
    <script src="../Content/submitvenue.js"></script>
</asp:Content>
