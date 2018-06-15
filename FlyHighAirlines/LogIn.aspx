<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="FlyHighAirlines.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Login</title>
    <meta charset="utf-8" />
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="jquery-ui.css" rel="stylesheet"/>
    <script type="text/javascript" src="jquery-3.1.1.js"></script>
    <script type="text/javascript" src="Scripts.js"></script>
    <script type="text/javascript" src="jquery-ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="fade_images">
                <div class="current"><img src="slide1.jpg" /></div>
                <div><img src="slide2.jpg" /></div>
                <div><img src="slide3.jpg" /></div>
            </div>
            <div class="logo"><img src="logo.png" /></div>
            <div class="loggin_form">
                <h2>Log In Form</h2>
                <p id="registration_success" class="success" runat="server"></p>
                <p id="registration_failed" class="failure" runat="server"></p>
                <asp:TextBox ID="Username" CssClass="input_text" placeholder="Enter Username" runat="server"></asp:TextBox>
                <asp:TextBox ID="Password" CssClass="input_text" TextMode="Password" placeholder="Enter Password" runat="server"></asp:TextBox>
                <p id="login_error" class="error" runat="server"></p>
                <asp:Button ID="login_button" CssClass="submit" runat="server" Text="Submit" OnClick="Login_User" />
                <input id="add_admin" type="button" class="add" value="Add Admin" />
            </div>
        </div>
        <div class="my_modal" id="my_modal">
            <div class="add_admin">
                <h3>Add New Administrator</h3>
                <input type="text" id="admin_id" placeholder="Enter ID" runat="server"/>
                <input type="text" id="admin_fname" placeholder="Enter First Name" runat="server" />
                <input type="text" id="admin_lname" placeholder="Enter Last Name" runat="server" />
                <input type="text" id="admin_username" placeholder="Enter Username" runat="server"/>
                <input type="text" id="admin_function" placeholder="Admin Function" runat="server"/>
                <input type="text" id="admin_address" placeholder="Admin Address" runat="server"/>
                <input type="text" id="admin_number" placeholder="Admin Phone Number" runat="server"/>
                <input type="text" id="admin_startdate" placeholder="Admin Start Date" runat="server"/>
                <input type="text" id="admin_enddate" placeholder="Admin End Date" runat="server"/>
                <input type="password" id="admin_password" placeholder="Enter Password" runat="server"/>
                <asp:FileUpload ID="admin_avatar" CssClass="file_upload" runat="server" />
                <div class="add_avatar">Add Administrator Avatar</div>
                <asp:Button ID="add_new_admin" runat="server" CssClass="submit" Text="Submit" OnClick="add_new_admin_Click" />
                <a href="#" id="cancel">Cancel</a>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
