<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FlyHighAirlines.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Home</title>
    <meta charset="utf-8" />
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="jquery-ui.css" rel="stylesheet"/>
    <script type="text/javascript" src="jquery-3.1.1.js"></script>
    <script type="text/javascript" src="Scripts.js"></script>
    <script type="text/javascript" src="jquery-ui.js"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAzEqqk_e9gvaojJrVhQiz3dh82jR9sOC4&v=3.exp&signed_in=true&libraries=places"></script>
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
            <ul>
                <li><a href="#" id="open_add_customer"><img src="add_user.png" /><p>Add Customer</p></a></li>
                <li><a href="#"><img src="delete_user.png" /><p>Delete Customer</p></a></li>
                <li><a href="#" id="open_update_customer"><img src="edit_user.png" /><p>Edit Customer</p></a></li>
                <li><a href="#"><img src="save_user.png" /><p>Save Customer</p></a></li>
                <li><a href="#"><img src="search_user.png" /><p>Search Customer</p></a></li>
                <li><a href="#"><img src="help_user.png" /><p>Help Customer</p></a></li>
                <li><a href="#"><img src="user.png" /><p>Session Admin</p></a></li>
                <li><a href="#" onclick="return printCustReport();"><img src="printer.png" /><p>Print Report</p></a></li>
            </ul>
        </div>
        <div class="customers_list_div">
            <h2>Customers List</h2>
            <div class="filter"><input type="text" id="customer_list_filter" placeholder="&#xF002; Search Customer" /></div>
            <div>
                <asp:GridView ID="customers_list" CssClass="customers_table" autogenerateselectbutton="true" OnSelectedIndexChanged="customers_list_SelectedIndexChanged" runat="server"></asp:GridView>
            </div>
        </div>
        <div class="add_details">
            <h3>Add Service</h3>
            <span id="cust_ok" runat="server"></span>
            <span id="cust_fail" runat="server"></span>
            <span id="service_id" runat="server" style="display:none;"></span>
            <span id="customer_document" runat="server" style="display:none;"></span>
            <asp:Image ID="select_customer_avatar" CssClass="sel_cus_av" runat="server" />
            <p class="select_id_p">
                <asp:Label ID="select_fname" CssClass="select_id" runat="server"></asp:Label>
                <asp:Label ID="select_lname" CssClass="select_id" runat="server"></asp:Label>
            </p>
            <label id="first_label" runat="server">Reason</label>
            <input type="text" id="first_var" runat="server" autocomplete="off" />
            <label id="second_label" runat="server">Visa Possession</label>
            <input type="text" id="second_var" runat="server" autocomplete="off"  />
            <label id="third_label" runat="server">Kid 1</label>
            <input type="text" id="third_var" runat="server"  autocomplete="off" />
            <label id="fourth_label" runat="server">Kid 2</label>
            <input type="text" id="fourth_var" runat="server" autocomplete="off"  />
            <label id="fith_label" runat="server">Sit Number</label>
            <input type="text" id="fith_var" runat="server" autocomplete="off"  />
            <input type="text" id="amount_paid" runat="server" placeholder="Enter Amount" autocomplete="off" />
            <select id="currency" runat="server">
                <option value="Dollars">Dollars</option>
                <option value="Euro">Euro</option>
                <option value="Ug Shillings">Ug Shillings</option>
                <option value="Pound">Pound</option>
            </select>
            <input id="traveldate" type="text" placeholder="Travel Date" runat="server" />
            <div class="butt_details">
                <asp:Button ID="save_service" CssClass="det_button" runat="server" Text="Save" OnClick="save_service_Click" />
                <asp:Button ID="print_receipt" CssClass="det_button" OnClientClick="return printReceipt();" runat="server" Text="Print" />
                <a href="#" id="clear_fields">Clear</a>
            </div>
        </div>
        <div class="my_modal" id="update_customer_modal">
            <div class="update_customer">
                <h3>Update Customer</h3>
                <div class="cust_id">
                    <p>Document ID : <span id="update_docid" runat="server"></span></p>
                    <p>First Name : <span id="update_fname" runat="server"></span></p>
                    <p>Last Name : <span id="update_lname" runat="server"></span></p>
                    <p>Nationality : <span id="update_nationality" runat="server"></span></p>
                    <label>Email Address : </label>
                    <input type="email" id="update_email" runat="server" />
                    <label>Service : </label>
                    <select id="update_service" runat="server">
                        <option>-- ENTER CUST. SERVICE --</option>
                        <option value="0">Travel Service</option>
                        <option value="1">Tour Service</option>
                        <option value="2">Transfert Service</option>
                        <option value="3">Business Service</option>
                    </select>
                    <div class="buttons_update">
                        <asp:Button ID="update_customer" CssClass="update" runat="server" Text="Update" OnClick="update_customer_Click" />
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
        <div class="my_modal" id="add_customer_modal" style="display:none;">
            <div class="add_Customer">
                <h2>Add Customer</h2>
                <video id="my_video_cap"></video>
                <div class="customer_info">
                    <input type="text" id="cust_fname" placeholder="Enter Cust. First Name" runat="server" />
                    <input type="text" id="cust_lname" placeholder="Enter Cust. Last Name" runat="server" />
                    <input type="text" id="cust_nname" placeholder="Enter Cust. Nick Name" runat="server" />
                    <input type="text" id="cust_document" placeholder="Enter Cust. Document ID" runat="server" />
                    <input type="text" id="cust_dob" placeholder="Enter Cust. Date of Birth" runat="server"/>
                    <input type="text" id="cust_pod" placeholder="Enter Cust. Place of Birth" runat="server" />
                    <input type="email" id="cust_email" placeholder="Enter Cust. Email Address" runat="server" />
                    <input type="text" id="cust_nationality" placeholder="Enter Cust. Nationality" runat="server" />
                    <select id="cust_gender" runat="server">
                        <option>-- ENTER CUST. GENDER --</option>
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                    </select>
                    <select id="cust_service" runat="server">
                        <option>-- ENTER CUST. SERVICE --</option>
                        <option value="0">Travel Service</option>
                        <option value="1">Tour Service</option>
                        <option value="2">Transfert Service</option>
                        <option value="3">Business Service</option>
                    </select>
                    <input type="text" id="cust_registration_date" placeholder="Enter Cust. Registration Date" runat="server" />
                </div>
                <div class="image_capture">
                    <div class="cust_avat">
                        <asp:Image ID="customer_image" CssClass="cust_avat_y" runat="server" />
                        <img src="#" id="image_captured" runat="server"/>
                        <input type="file" id="else_upload" runat="server" style="display:none;" />
                        <canvas id="my_image_canvas" style="display:none;" width="780" height="585"></canvas>
                    </div>
                    <asp:FileUpload ID="image_upload" runat="server" CssClass="file_upload" />
                    <div class="add_pic">Add Customer Avatar</div>

                    <div class="capture_butt">
                        <input type="button" id="cap_start" class="cap_but" value="Start" />
                        <input type="button" id="cap_stop" class="cap_but" value="Snap" />
                        <input type="button" id="cap_save" class="cap_but" value="Save" />
                    </div>
                    <div class="check" style="margin-top:10px;">
                        <asp:CheckBox ID="send_email" Checked="true" CssClass="send_email" runat="server" /> <span style="margin-top:0px; margin-left:10px; position:absolute; z-index:6;">Send Email Message</span>
                    </div>
                    <div class="event_butt" style="margin-top:10px;">
                        <asp:Button ID="save_customer" CssClass="s_c" runat="server" Text="Submit" OnClick="save_customer_Click" />
                        <input type="button" id="clean_fields" class="s_c" value="Clean" />
                        <a href="#" id="cancel">Cancel</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="receipt" id="customer_receipt">
            <img src="logo.png" />
            <div class="plaine_desc" style="float:right;">
                <p><strong>Date : </strong><span id="rec_date" runat="server"></span></p>
                <p><strong>Airplaine : </strong><span id="rec_airplaine" runat="server"></span></p>
                <p><strong>Time : </strong><span id="rec_time" runat="server"></span></p>
            </div>
            <h3 style="text-align:center;">Ticket - Receipt</h3>
            <div class="receipt_body">
                <p><strong>First Name : </strong><span id="rec_fname" runat="server"></span></p>
                <p><strong>Last Name : </strong><span id="rec_lname" runat="server"></span></p>
                <p><strong>Document ID : </strong><span id="rec_documentid" runat="server"></span></p>
                <p><strong>From : </strong><span id="rec_from" runat="server"></span></p>
                <p><strong>To : </strong><span id="rec_to" runat="server"></span></p>
                <p><strong>Service : </strong><span id="rec_service" runat="server"></span></p>
                <p><strong><span id="rec_class_text" runat="server">Class</span> : </strong><span id="rec_class" runat="server"></span></p>
                <p><strong><span id="rec_sit_text" runat="server">Sit Number</span> : </strong><span id="rec_sitnumber" runat="server"></span></p>
                <p><strong>Amount Paid : </strong><span id="rec_amountpaid" runat="server"></span></p>
                <p><strong>Travel Date : </strong><span id="rec_travel_date" runat="server"></span></p>
            </div>
            <p class="thanks" style="text-align:center;">THANK YOU FOR HAVING CHOOSEN FLY HIGH AIRLINES</p>
            <h2 style="text-align:center;">SAFE JOURNEY</h2>
        </div>
        <div class="customers_report" id="customers_report">
            <p style="float:right; font-size:15px;"><strong>Date : </strong><span runat="server" id="printed_time"></span></p>
            <h2 style="text-align:center;">Customers List</h2>
            <asp:GridView ID="cust_report" runat="server" CssClass="cust_rep" Width="100%"></asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
