<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransfertService.aspx.cs" Inherits="FlyHighAirlines.TransfertService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fly High Airline - Transfert Service</title>
    <meta charset="utf-8" />
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="jquery-ui.css" rel="stylesheet"/>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCbU0ik8bWm0E7X7GeBA8soNimc3gcYn_8&callback=initMap" type="text/javascript"></script>
    <script type="text/javascript" src="jquery-3.1.1.js"></script>
    <script type="text/javascript" src="jquery-ui.js"></script>
    <script type="text/javascript">
        
        function mapLocation() {
            var directionsDisplay;
            var directionsService = new google.maps.DirectionsService();
            var map;

            function initialize() {
                directionsDisplay = new google.maps.DirectionsRenderer();
                var center = new google.maps.LatLng(0.3189261, 32.5727795);
                var mapOptions = {
                    zoom: 14,
                    center: center
                };
                map = new google.maps.Map(document.getElementById('my_map'), mapOptions);
                directionsDisplay.setMap(map);
                google.maps.event.addDomListener(document.getElementById('get_direction'), 'click', drawRoute);
            }

            function drawRoute() {
                var address = $("#address").text();
                
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'address' : address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var from_lat = results[0].geometry.location.lat();
                        var from_long = results[0].geometry.location.lng();

                        var start = new google.maps.LatLng(0.3105855, 32.5790057);
                        var end = new google.maps.LatLng(from_lat, from_long);

                        var bounds = new google.maps.LatLngBounds();
                        bounds.extend(start);
                        bounds.extend(end);
                        map.fitBounds(bounds);
                        var request = {
                            origin: start,
                            destination: end,
                            travelMode: google.maps.TravelMode.DRIVING
                        };
                        directionsService.route(request, function (response, status) {
                            if (status == google.maps.DirectionsStatus.OK) {
                                directionsDisplay.setDirections(response);
                                directionsDisplay.setMap(map);
                            } else {
                                alert("Directions Request from " + start.toUrlValue(6) + " to " + end.toUrlValue(6) + " failed: " + status);
                            }
                        });
                    }
                });
                
            }
            google.maps.event.addDomListener(window, 'load', initialize);
        }
        mapLocation();

        $(document).ready(function () {
            $("#show_map").click(function (e) {
                e.preventDefault();
                $("#see_location").fadeIn();
            });

            $("#hide_map").click(function (e) {
                e.preventDefault();
                $("#see_location").fadeOut();
            });
        });
    </script>
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
            <h2>Tour Service</h2>
            <div class="search_date">
                <input type="text" runat="server" id="search_by_date" placeholder="&#xF002; Search By Date" />
                <asp:Button ID="search_now" runat="server" CssClass="search_button" Text="&#xF002;" OnClick="search_now_Click" />
            </div>
            <br />
            <div>
                <asp:GridView ID="transfert_service_customers" AutoGenerateSelectButton="true" CssClass="customers_table" runat="server" OnSelectedIndexChanged="transfert_service_customers_SelectedIndexChanged"></asp:GridView>
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
            <p><strong>Sent to : </strong><span id="receiver" runat="server"></span></p>
            <p><strong>Obect Sent: </strong><span id="object_sent" runat="server"></span></p>
            <p style="display:none;"><strong>Place Visited : </strong><span id="place_visited" runat="server"></span></p>
            <p style="display:none;"><strong>Confirmation : </strong><span id="conf" runat="server"></span></p>
            <p><strong>Transfert Date : </strong><span id="travel_date" runat="server"></span></p>
            <p><strong>From : </strong><span id="to" runat="server"></span></p>
            <p><strong>To : </strong><span id="from" runat="server"></span></p>
            <div><a href="#" id="show_map">Show Address</a></div>
        </div>
        <div class="my_modal" id="see_location">
            <div class="add_Customer">
                <h2 style="text-align:center; font-size:18px;">Receiver Addresses</h2>
                <p style="font-size:15px; text-align:center;">
                    <strong>Address : </strong> <span id="address" runat="server"></span> | 
                    <strong>E-mail : </strong> <span id="email" runat="server"></span>
                    <span runat="server" id="fly_address" style="display:none;"></span>
                </p>
                <div id="my_map"></div>
                <div id="route" style="overflow:auto;"></div>
                <div style="padding-top:15px; padding-bottom:10px;"><a href="#" id="hide_map">Cancel</a><a href="#" id="get_direction">Trace Route</a></div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
