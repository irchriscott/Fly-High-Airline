<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministratorsList.aspx.cs" Inherits="FlyHighAirlines.AdministratorsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Administrators</title>
    <meta charset="utf-8" />
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="jquery-ui.css" rel="stylesheet"/>
    <script type="text/javascript" src="jquery-3.1.1.js"></script>
    <script type="text/javascript" src="Scripts.js"></script>
    <script type="text/javascript" src="jquery-ui.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="menu">
            <div class="menu_logo"><a href="Home.aspx"><img src="logo.png" /></a></div>
            <div class="session_user">
                <asp:Image ID="sess_avatar" CssClass="sess_avat" runat="server" />
                <p runat="server" id="sess_names"></p>
                <!--<p runat="server" id="sess_username"></p>-->
            </div>
            <div class="menu_list">
                <ul>
                    <li><img src="schedules.png" /><a href="AircraftSchedules.aspx">Aircaft Schedules</a></li>
                    <li><img src="today.png" /><a href="TodaySchedule.aspx"> Today's Flight</a></li>
                    <li><img src="service.png" /><a href="Services.aspx"> Services</a></li>
                    <li><img src="plane.png" /><a href="AirctaftsList.aspx"> Aircrafts List</a></li>
                    <li><img src="pilot.png" /><a href="PilotsList.aspx"> Pilots List</a></li>
                    <li><img src="stewardess.png" /><a href="StewardessesList.aspx"> Stewardesses List</a></li>
                    <li><img src="admin.png" /><a href="AdministratorsList.aspx"> Administrators List</a></li>
                    <li><img src="clipboard-48.png" /><a href="ReportsList.aspx"> Reports List</a>
                        <ul>
                            <li><a href="DailyRecord.aspx">Daily Record</a></li>
                            <li><a href="SingleRecord.aspx">Single Record</a></li>
                            <li><a href="TravelService.aspx">Travel Service</a></li>
                            <li><a href="TourService.aspx">Tour Service</a></li>
                            <li><a href="TransfertService.aspx">Transfert Service</a></li>
                            <li><a href="BusinessService.aspx">Business Service</a></li>
                            <li><a href="Account.aspx">Account</a></li>
                            <li><a href="DailyExpences.aspx">Daily Expences</a></li>
                            <li><a href="FeedBack.aspx">Feed Back</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="log_out"><asp:Button ID="log_out" runat="server" CssClass="logout_btn" Text="Log Out" OnClick="log_out_Click" /></div>
        </div>
        <div class="menu_top">
            <h1>Fly High Airlines</h1>
            <span style="margin-top:-54px; right:0; margin-right:100px; position:absolute;"><a href="#" id="print_todayschedule" onclick="return printAircraftSchedules();"><img src="printer.png" /></a></span>
        </div>
        <div class="customers_list_div">
            <h2>Administrators List</h2>
            <div>
                <asp:GridView ID="admin_list" CssClass="customers_table" OnSelectedIndexChanged="admin_list_SelectedIndexChanged" autogenerateselectbutton="true" runat="server"></asp:GridView>
            </div>
        </div>
        <div class="add_details">
            <h3>Administrator Detail</h3>
            <span id="cust_ok" runat="server"></span>
            <span id="cust_fail" runat="server"></span>
            <span id="adm_id" runat="server" style="display:none;"></span>
            <span id="customer_document" runat="server" style="display:none;"></span>
            <asp:Image ID="adm_avatar" CssClass="sel_cus_av" runat="server" />
            <p><strong>First Name : </strong><span runat="server" id="adm_fname"></span></p>
            <p><strong>Last Name : </strong><span runat="server" id="adm_lname"></span></p>
            <p><strong>Phone Number : </strong><span id="adm_number" runat="server"></span></p>
            <p><strong>Address : </strong><span id="adm_address" runat="server"></span></p>
            <p><strong>Function : </strong><span id="adm_function" runat="server"></span></p>
            <p><strong>Started Date : </strong><span id="adm_start" runat="server"></span></p>
            <p><strong>End Date : </strong><span id="adm_end" runat="server"></span></p>
            <div class="add_new">
                <a href="#" id="add_admin">Add</a>
                <asp:Button ID="delete_admin" CssClass="delete_btn" runat="server" Text="Delete" OnClick="delete_admin_Click" />
            </div>
        </div>
        <div class="customers_report" id="airchaft_schedule_print">
            <p style="float:right; font-size:15px;"><strong>Date : </strong><span runat="server" id="printed_time"></span></p>
            <h2 style="text-align:center;">Administrators List</h2>
            <asp:GridView ID="admin_print_list" runat="server" CssClass="cust_rep" Width="100%"></asp:GridView>
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
