<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TodaySchedule.aspx.cs" Inherits="FlyHighAirlines.TodaySchedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Today Schedule</title>
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
            <span style="margin-top:-55px; right:0; margin-right:100px; position:absolute;"><a href="#" id="print_todayschedule" onclick="return printPassengerAir();"><img src="printer.png" /></a></span>
        </div>
        <div class="customers_list_div">
            <h2>Today's Flight</h2>
            <p><strong>Date : </strong><span id="today_date" runat="server"></span> | <strong>Airplaine : </strong><span id="today_airplaine" runat="server"></span></p>
            <p><strong>From : </strong><span id="today_from" runat="server"></span> | <strong>To : </strong><span id="today_to" runat="server"></span></p>
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
            <p style="display:none;"><strong>Date of Birth : </strong><span id="dob" runat="server"></span></p>
            <p><strong>Place of Birth : </strong><span id="pob" runat="server"></span></p>
            <p><strong>Service : </strong><span id="service" runat="server"></span></p>
            <p><strong>Email Address : </strong><span id="email_address" runat="server"></span></p>
            <div runat="server" id="confirm_reserv"><a href="#" id="confirm">Confirm</a></div>
        </div>
        <div class="customers_report" id="todayschedule_report">
            <p style="float:right; font-size:15px;"><strong>Date : </strong><span runat="server" id="printed_time"></span></p>
            <h2 style="text-align:center;">Today's Flight</h2>
            <p style="text-align:center;"><strong>Date : </strong><span id="rep_today" runat="server"></span> | <strong>Airplaine : </strong><span id="rep_airplaine" runat="server"></span></p>
            <p style="text-align:center;"><strong>From : </strong><span id="rep_from" runat="server"></span> | <strong>To : </strong><span id="rep_to" runat="server"></span></p>
            <asp:GridView ID="airplaine_pass" runat="server" CssClass="cust_rep" Width="100%"></asp:GridView>
        </div>
        <div class="my_modal" id="confirm_reservation_modal">
            <div class="update_customer">
                <h3>Update Customer</h3>
                <div class="cust_id">
                    <p>Document ID : <span id="update_docid" runat="server"></span></p>
                    <p>First Name : <span id="update_fname" runat="server"></span></p>
                    <p>Last Name : <span id="update_lname" runat="server"></span></p>
                    <p>Nationality : <span id="update_nationality" runat="server"></span></p>
                    <label>Amount : </label>
                    <input type="text" id="amount" runat="server" />
                    <label>Currency : </label>
                    <select id="currency" runat="server">
                        <option value="Dollars">Dollars</option>
                        <option value="Euro">Euro</option>
                        <option value="Ug Shillings">Ug Shillings</option>
                        <option value="Pound">Pound</option>
                    </select>
                    <div class="buttons_update">
                        <asp:Button ID="confirm_reservation" CssClass="update" runat="server" Text="Save" OnClick="confirm_reservation_Click" />
                        <a href="#" id="cancel_update_res">Cancel</a>
                    </div>
                </div>
                <div class="cust_img">
                    <asp:Image ID="update_image" CssClass="update_image" runat="server" />
                    <asp:FileUpload ID="update_avatar" runat="server" CssClass="file_upload" />
                    <div class="add_pic">Add Customer Avatar</div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
