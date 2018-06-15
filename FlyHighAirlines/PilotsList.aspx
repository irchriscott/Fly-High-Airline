<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PilotsList.aspx.cs" Inherits="FlyHighAirlines.PilotsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Pilots</title>
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
            <span style="margin-top:-54px; right:0; margin-right:180px; position:absolute;"><a href="#" id="print_todayschedule" onclick="return printAircraftSchedules();"><img src="printer.png" /></a></span>
            <span style="margin-top:-55px; right:0; margin-right:100px; position:absolute;"><a href="#" id="open_update_customer"><img src="edit_user.png" style="width:35px; height:35px;" /></a></span>
        </div>
        <div class="customers_list_div">
            <h2>Pilots List</h2>
            <div>
                <asp:GridView ID="pilot_list" CssClass="customers_table" OnSelectedIndexChanged="pilot_list_SelectedIndexChanged" autogenerateselectbutton="true" runat="server"></asp:GridView>
            </div>
        </div>
        <div class="add_details">
            <h3>Pilot Detail</h3>
            <span id="cust_ok" runat="server"></span>
            <span id="cust_fail" runat="server"></span>
            <span id="pilot_id" runat="server" style="display:none;"></span>
            <span id="customer_document" runat="server" style="display:none;"></span>
            <asp:Image ID="pilot_avatar" CssClass="sel_cus_av" runat="server" />
            <p><strong>First Name : </strong><span runat="server" id="p_fname"></span></p>
            <p><strong>Last Name : </strong><span runat="server" id="p_lastname"></span></p>
            <p><strong>Phone Number : </strong><span id="p_number" runat="server"></span></p>
            <p><strong>Address : </strong><span id="p_address" runat="server"></span></p>
            <p><strong>Aircraft : </strong><span id="p_aircraft" runat="server"></span></p>
            <p><strong>Started Date : </strong><span id="p_start" runat="server"></span></p>
            <p><strong>End Date : </strong><span id="p_end" runat="server"></span></p>
            <div class="add_new">
                <a href="#" id="add_schedule">Add</a>
                <asp:Button ID="delete_pilot" CssClass="delete_btn" runat="server" Text="Delete" OnClick="delete_pilot_Click" />
            </div>
        </div>
        <div class="customers_report" id="airchaft_schedule_print">
            <p style="float:right; font-size:15px;"><strong>Date : </strong><span runat="server" id="printed_time"></span></p>
            <h2 style="text-align:center;">Pilots List</h2>
            <asp:GridView ID="pilots_print_list" runat="server" CssClass="cust_rep" Width="100%"></asp:GridView>
        </div>
        <div class="my_modal" id="add_schedule_modal">
            <div class="add_schedule" style="margin-top:110px;">
                <h3>Add New Pilot</h3>
                <input type="text" id="pi_id" runat="server" placeholder="Enter Pilot ID" />
                <input type="text" id="pi_fname" runat="server" placeholder="Enter Pilot First Name" />
                <input type="text" id="pi_lname" runat="server" placeholder="Enter Pilot Last Name" />
                <input type="text" id="pi_number" runat="server" placeholder="Enter Pilot Phone Number" />
                <input type="text" id="pi_address" runat="server" placeholder="Enter Pilot Address" />
                <input type="text" id="pi_aicraft" runat="server" placeholder="Enter Pilot Aircraft ID" />
                <input type="text" runat="server" id="pi_startdate" placeholder="Enter Started Date" />
                <input type="text" runat="server" id="pi_enddate" placeholder="Enter End Date" />
                <asp:FileUpload ID="pilot_avatar_upload" CssClass="uploader" runat="server" />
                <div class="avat_up">Add Pilot Avatar</div>
                <div class="buttons">
                    <asp:Button ID="save_pilot" runat="server" CssClass="add_schedule_btn" Text="Save" OnClick="save_pilot_Click" />
                    <a href="#" id="cancel_add">Cancel</a>
                </div>
            </div>
        </div>
        <div class="my_modal" id="update_customer_modal">
            <div class="update_customer">
                <h3>Update Pilot</h3>
                <div class="cust_id">
                    <p>First Name : <span id="update_fname" runat="server"></span></p>
                    <p>Last Name : <span id="update_lname" runat="server"></span></p>
                    <label>Aircraft : </label>
                    <input type="text" id="update_arcraft" runat="server" />
                    <label>Address : </label>
                    <input type="text" id="update_address" runat="server" />
                    <label>Phone Number : </label>
                    <input type="text" id="update_number" runat="server" />
                    <div class="buttons_update">
                        <asp:Button ID="update_pilot" CssClass="update" runat="server" Text="Update" OnClick="update_pilot_Click" />
                        <a href="#" id="cancel_update">Cancel</a>
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
