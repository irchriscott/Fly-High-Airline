<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyRecord.aspx.cs" Inherits="FlyHighAirlines.DailyRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Daily Record</title>
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
            <span style="margin-top:-55px; right:0; margin-right:100px; position:absolute;"><a href="#" id="print_todayschedule"><img src="printer.png" /></a></span>
        </div>
        <div class="customers_list_div">
            <h2>Daily Record</h2>
            <div class="search_date">
                <input type="text" runat="server" id="search_by_date" placeholder="&#xF002; Search By Date" />
                <asp:Button ID="search_now" runat="server" CssClass="search_button" Text="&#xF002;" OnClick="search_now_Click" />
            </div>
            <br />
            <div>
                <asp:GridView ID="daily_record_table" CssClass="customers_table"  runat="server"></asp:GridView>
            </div>
        </div>
        <div class="add_details">
            <h3>Daily Record</h3>
            <span id="cust_ok" runat="server"></span>
            <span id="cust_fail" runat="server"></span>
            <span id="shedule_id" runat="server" style="display:none;"></span>
            <span id="customer_document" runat="server" style="display:none;"></span>
            <div class="slide_in">
                <ul>
                    <li><img src="slide1.jpg" /></li>
                    <li><img src="slide2.jpg" /></li>
                    <li><img src="slide3.jpg" /></li>
                </ul>
            </div>
            <p><strong>Name : </strong><span runat="server" id="air_name"></span></p>
            <p><strong>From : </strong><span id="air_from" runat="server"></span></p>
            <p><strong>To : </strong><span id="air_to" runat="server"></span></p>
            <p><strong>Time : </strong><span id="air_time" runat="server"></span></p>
            <p><strong>Travel : <span runat="server" id="air_travel"></span></strong></p>
            <p><strong>Tour : <span runat="server" id="air_tour"></span></strong></p>
            <p><strong>Transfert : <span runat="server" id="air_transfert"></span></strong></p>
            <p><strong>Business : <span runat="server" id="air_business"></span></strong></p>
        </div>
        <!--<div class="customers_report" id="airchaft_schedule_print">
            <p style="float:right; font-size:15px;"><strong>Date : </strong><span runat="server" id="printed_time"></span></p>
            <h2 style="text-align:center;">Aircrafts Schedules</h2>
            <asp:GridView ID="aircraft_sch" runat="server" CssClass="cust_rep" Width="100%"></asp:GridView>
        </div>-->
    </div>
    </form>
</body>
</html>
