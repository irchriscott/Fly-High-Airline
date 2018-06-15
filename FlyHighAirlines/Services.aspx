<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="FlyHighAirlines.Services" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Services</title>
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
            <h2>Services</h2>
            <div class="all_services">
                <ul class="services_links">
                    <li style="margin-left:-40px;" class="active"><a href="#travel">Travel Service</a></li>
                    <li><a href="#tour">Tour Service</a></li>
                    <li><a href="#transfert">Transfert Service</a></li>
                    <li><a href="#business">Business Service</a></li>
                </ul>
                <div class="contents">
                    <ul>
                        <li id="travel" class="details active">
                            <asp:GridView ID="travel_service_customers" AutoGenerateSelectButton="true" CssClass="cust_list" runat="server" OnSelectedIndexChanged="travel_service_customers_SelectedIndexChanged"></asp:GridView>
                        </li>
                        <li id="tour" class="details">
                            <asp:GridView ID="tour_service_customers" AutoGenerateSelectButton="true" CssClass="cust_list" runat="server" OnSelectedIndexChanged="tour_service_customers_SelectedIndexChanged"></asp:GridView>
                        </li>
                        <li id="transfert" class="details">
                            <asp:GridView ID="transfert_service_customers" AutoGenerateSelectButton="true" runat="server" CssClass="cust_list" OnSelectedIndexChanged="transfert_service_customers_SelectedIndexChanged"></asp:GridView>
                        </li>
                        <li id="business" class="details">
                            <asp:GridView ID="business_service_customers" AutoGenerateSelectButton="true" runat="server" CssClass="cust_list" OnSelectedIndexChanged="business_service_customers_SelectedIndexChanged"></asp:GridView>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="add_details">
            <h3>Customer ID</h3>
            <span id="cust_ok" runat="server"></span>
            <asp:Image ID="select_customer_avatar" CssClass="sel_cus_av" runat="server" />
            <p><strong>First Name : </strong><span id="first_name" runat="server"></span></p>
            <p><strong>Last Name : </strong><span id="last_name" runat="server"></span></p>
            <p><strong>Nick Name : </strong><span id="nick_name" runat="server"></span></p>
            <p><strong>Gender : </strong><span id="gender" runat="server"></span></p>
            <p><strong>Nationality : </strong><span id="nationality" runat="server"></span></p>
            <p><strong>Date of Birth : </strong><span id="dob" runat="server"></span></p>
            <p><strong>Place of Birth : </strong><span id="pob" runat="server"></span></p>
            <p><strong>Service : </strong><span id="service" runat="server"></span></p>
            <p><strong>Email Address : </strong><span id="email_address" runat="server"></span></p>
        </div>
    </div>
    </form>
</body>
</html>
