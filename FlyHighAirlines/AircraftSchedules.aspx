<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AircraftSchedules.aspx.cs" Inherits="FlyHighAirlines.AircraftSchedules" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Aircraft Schedules</title>
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
            <span style="margin-top:-55px; right:0; margin-right:100px; position:absolute;"><a href="#" id="print_todayschedule" onclick="return printAircraftSchedules();"><img src="printer.png" /></a></span>
        </div>
        <div class="customers_list_div">
            <h2>Aircrafts Schedules</h2>
            <div>
                <asp:GridView ID="aircraft_schedules" CssClass="customers_table" OnSelectedIndexChanged="aircraft_schedules_SelectedIndexChanged" autogenerateselectbutton="true" runat="server"></asp:GridView>
            </div>
        </div>
        <div class="add_details">
            <h3>Aircraft Schedule</h3>
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
            <p style="display:none;"><strong>Name : </strong><span runat="server" id="air_name"></span></p>
            <p><strong>Tot. Sits : </strong><span id="air_sits" runat="server"></span></p>
            <p><strong>Travel Day : </strong><span id="air_trav_day" runat="server"></span></p>
            <p><strong>From : </strong><span id="air_from" runat="server"></span></p>
            <p><strong>To : </strong><span id="air_to" runat="server"></span></p>
            <p><strong>Time : </strong><span id="air_time" runat="server"></span></p>
            <p><strong>Start Date : </strong><span id="air_start_day" runat="server"></span></p>
            <div class="add_new">
                <a href="#" id="add_schedule">Add</a>
                <asp:Button ID="delete_schedule" CssClass="delete_btn" runat="server" Text="Delete" OnClick="delete_schedule_Click" />
            </div>
        </div>
        <div class="customers_report" id="airchaft_schedule_print">
            <p style="float:right; font-size:15px;"><strong>Date : </strong><span runat="server" id="printed_time"></span></p>
            <h2 style="text-align:center;">Aircrafts Schedules</h2>
            <asp:GridView ID="aircraft_sch" runat="server" CssClass="cust_rep" Width="100%"></asp:GridView>
        </div>
        <div class="my_modal" id="add_schedule_modal">
            <div class="add_schedule">
                <h3>Add Aircraft Schedule</h3>
                <input type="text" id="sch_id" runat="server" placeholder="Enter Schedule ID" />
                <input type="text" id="sch_aircraft_name" runat="server" placeholder="Enter Aircraft Name" />
                <input type="text" id="sch_place_from" runat="server" placeholder="Enter Place From" />
                <input type="text" id="sch_plate_to" runat="server" placeholder="Enter Place To" />
                <label>Travel Day</label>
                <select id="sch_trav_day" runat="server">
                    <option>-- TRAVEL DAY --</option>
                    <option value="Mondays">Mondays</option>
                    <option value="Tuesdays">Tuesdays</option>
                    <option value="Wednesdays">Wednesdays</option>
                    <option value="Thursdays">Thursdays</option>
                    <option value="Fridays">Fridays</option>
                    <option value="Saturdays">Saturdays</option>
                    <option value="Sundays">Sundays</option>
                </select>
                <input type="text" runat="server" id="sch_time" placeholder="Enter Flight Time" />
                <div class="buttons">
                    <asp:Button ID="save_schedule" runat="server" CssClass="add_schedule_btn" Text="Save" OnClick="save_schedule_Click" />
                    <a href="#" id="cancel_add">Cancel</a>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
