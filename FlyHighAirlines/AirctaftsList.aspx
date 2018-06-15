<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AirctaftsList.aspx.cs" Inherits="FlyHighAirlines.AirctaftsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Aircraft List</title>
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
            <h2>Aircrafts List</h2>
            <div>
                <asp:GridView ID="aircr_list" CssClass="customers_table" OnSelectedIndexChanged="aircr_list_SelectedIndexChanged" autogenerateselectbutton="true" runat="server"></asp:GridView>
            </div>
        </div>
        <div class="add_details">
            <h3>Aircraft Detail</h3>
            <span id="cust_ok" runat="server"></span>
            <span id="cust_fail" runat="server"></span>
            <span id="aircra_id" runat="server" style="display:none;"></span>
            <span id="customer_document" runat="server" style="display:none;"></span>
            <div class="slide_in">
                <ul>
                    <li><img src="slide1.jpg" /></li>
                    <li><img src="slide2.jpg" /></li>
                    <li><img src="slide3.jpg" /></li>
                </ul>
            </div>
            <p><strong>Name : </strong><span runat="server" id="air_name"></span></p>
            <p><strong>Tot. Sits : </strong><span id="air_sits" runat="server"></span></p>
            <p><strong>VIP : </strong><span id="air_vip" runat="server"></span></p>
            <p><strong>Business : </strong><span id="air_busi" runat="server"></span></p>
            <p><strong>Economy : </strong><span id="air_eco" runat="server"></span></p>
            <p><strong>Started Date : </strong><span id="air_date" runat="server"></span></p>
            <div class="add_new">
                <a href="#" id="add_schedule">Add</a>
                <asp:Button ID="delete_airplaine" CssClass="delete_btn" runat="server" Text="Delete" OnClick="delete_airplaine_Click" />
            </div>
        </div>
        <div class="customers_report" id="airchaft_schedule_print">
            <p style="float:right; font-size:15px;"><strong>Date : </strong><span runat="server" id="printed_time"></span></p>
            <h2 style="text-align:center;">Aircrafts List</h2>
            <asp:GridView ID="aircraft_list" runat="server" CssClass="cust_rep" Width="100%"></asp:GridView>
        </div>
        <div class="my_modal" id="add_schedule_modal">
            <div class="add_schedule">
                <h3>Add New Aircraft</h3>
                <input type="text" id="id_craft" runat="server" placeholder="Enter Aircraft ID" />
                <input type="text" id="name_craft" runat="server" placeholder="Enter Aircraft Name" />
                <input type="text" id="sits_craft" runat="server" placeholder="Enter Total Sits" />
                <input type="text" id="vip_craft" runat="server" placeholder="Enter VIP Sits (from 0 to)" />
                <input type="text" id="business_craft" runat="server" placeholder="Enter Business Sits (from VIP to)" />
                <input type="text" id="economy_craft" runat="server" placeholder="Enter Economy Sits (from Business to)" />
                <input type="text" runat="server" id="start_date_craft" placeholder="Enter Started Date" />
                <div class="buttons">
                    <asp:Button ID="save_airplaine" runat="server" CssClass="add_schedule_btn" Text="Save" OnClick="save_airplaine_Click" />
                    <a href="#" id="cancel_add">Cancel</a>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
