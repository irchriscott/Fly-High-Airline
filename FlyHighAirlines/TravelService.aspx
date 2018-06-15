<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelService.aspx.cs" Inherits="FlyHighAirlines.TravelService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Travel Service</title>
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
        </div>
        <div class="customers_list_div">
            <h2>Travel Service</h2>
            <div class="search_date">
                <input type="text" runat="server" id="search_by_date" placeholder="&#xF002; Search By Date" />
                <asp:Button ID="search_now" runat="server" CssClass="search_button" Text="&#xF002;" OnClick="search_now_Click" />
            </div>
            <br />
            <div>
                <asp:GridView ID="travel_service_customers" AutoGenerateSelectButton="true" CssClass="customers_table" runat="server" OnSelectedIndexChanged="travel_service_customers_SelectedIndexChanged"></asp:GridView>
            </div>
        </div>
        <div class="add_details">
            <h3>Customer ID</h3>
            <span id="cust_ok" runat="server"></span>
            <asp:Image ID="select_customer_avatar" CssClass="sel_cus_av" runat="server" />
            <p><strong>First Name : </strong><span id="first_name" runat="server"></span></p>
            <p><strong>Last Name : </strong><span id="last_name" runat="server"></span></p>
            <p style="display:none;"><strong>Nick Name : </strong><span id="nick_name" runat="server"></span></p>
            <p><strong>Nationality : </strong><span id="nationality" runat="server"></span></p>
            <p><strong>Reason : </strong><span id="reason" runat="server"></span></p>
            <p><strong>Kid 1: </strong><span id="kid_1" runat="server"></span></p>
            <p><strong>Kid 2 : </strong><span id="kid_2" runat="server"></span></p>
            <p><strong>Confirmation : </strong><span id="conf" runat="server"></span></p>
            <p><strong>Travel Date : </strong><span id="travel_date" runat="server"></span></p>
            <p><strong>From : </strong><span id="from" runat="server"></span></p>
            <p><strong>To : </strong><span id="to" runat="server"></span></p>
        </div>
    </div>
    </form>
</body>
</html>
