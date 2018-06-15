$(function () {
    // create the image rotator
    setInterval("rotateImages()", 4000);
    slide();
    //datepicker
    $("#admin_startdate").datepicker({
        dateFormat: "yy-mm-dd"
    });
    $("#admin_enddate").datepicker({
        dateFormat: "yy-mm-dd"
    });
    $("#cust_dob").datepicker({
        dateFormat: "yy-mm-dd"
    });
    $("#traveldate").datepicker({
        //dateFormat: "yy-mm-dd"
    });
    $("#cust_registration_date").datepicker({
        dateFormat: "yy-mm-dd"
    });

    $("#start_date_craft").datepicker({
        dateFormat: "yy-mm-dd"
    });
    $("#pi_startdate").datepicker({
        dateFormat: "yy-mm-dd"
    });
    $("#pi_enddate").datepicker({
        dateFormat: "yy-mm-dd"
    });
    $("#st_startdate").datepicker({
        dateFormat: "yy-mm-dd"
    });
    $("#st_enddate").datepicker({
        dateFormat: "yy-mm-dd"
    });

    $("#search_by_date").datepicker({
        dateFormat: "yy-mm-dd"
    });

    $("#confirm").click(function (e) {
        e.preventDefault();
        $("#confirm_reservation_modal").fadeIn();
    });
    $("#cancel_update_res").click(function (e) {
        e.preventDefault();
        $("#confirm_reservation_modal").fadeOut();
    })
    var label_1 = $("#first_label").text();
    var label_2 = $("#second_label").text();
    var label_5 = $("#fith_label").text();

    if (label_1 == "Start Date") {
        $("#first_var").datepicker({
            dateFormat: "yy-mm-dd"
        });
    }

    if (label_2 == "End Date") {
        $("#second_var").datepicker({
            dateFormat: "yy-mm-dd"
        });
    }

    if (label_5 == "Receiver Address") {
        google.maps.event.addDomListener(window, 'load', InitializePlaces);
    }

    $("#add_admin").click(function (e) {
        e.preventDefault();
        $("#my_modal").fadeIn();
    });

    $("#cancel").click(function (e) {
        e.preventDefault();
        $("#my_modal").fadeOut();
        $("#add_customer_modal").fadeOut();
    });

    $("#open_update_customer").click(function (e) {
        e.preventDefault();
        $("#update_customer_modal").fadeIn();
    });

    $("#cancel_update").click(function (e) {
        e.preventDefault();
        $("#update_customer_modal").fadeOut();
    });
    $("#open_add_customer").click(function (e) {
        e.preventDefault();
        $("#add_customer_modal").fadeIn();
    });

    $("#add_schedule").click(function (e) {
        e.preventDefault();
        $("#add_schedule_modal").fadeIn();
    });
    
    $("#cancel_add").click(function (e) {
        e.preventDefault();
        $("#add_schedule_modal").fadeOut();
    });

    

    //clean

    $("#clear_fields").click(function (e) {
        e.preventDefault();
        $("#first_var").val("");
        $("#second_var").val("");
        $("#third_var").val("");
        $("#fourth_var").val("");
        $("#fith_var").val("");
        $("#amount_paid").val("");
        $("#traveldate").val("");
    });

    //tabs
    $(".all_services .services_links li a").on("click", function (e) {
        e.preventDefault();
        var currentAttrValue = $(this).attr("href");
        //alert(currentAttrValue);
        // Show/Hide Tabs
        $(".contents " + currentAttrValue).show().siblings().hide();

        // Change/remove current tab to active
        $(this).parent("li").addClass("active").siblings().removeClass("active");
    });

    //filter customers table

    $("#customer_list_filter").keyup(function () {
        var rows = $("#customers_list").find("tr").hide();
        $("#customers_list").find("th").parent("tr").show();
        if (this.value.length) {
            var data = this.value.split(" ");
            $.each(data, function (i, v) {
                rows.filter(":contains('" + v + "')").show();
            });
        } else rows.show();
    });

    //picture capture
    var video = document.getElementById("my_video_cap"),
        canvas = document.getElementById("my_image_canvas"),
        context = canvas.getContext("2d"),
        photo = document.getElementById("image_captured"),
        
        videoUrl = window.URL || window.webkitUrl;
    $("#my_video_cap").hide();
    $("#cap_start").click(function (e) {
        e.preventDefault();
        $("#my_video_cap").show();
        $("#image_captured").hide();
        navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;

        navigator.getUserMedia({
            video: true,
            audio: false
        }, function (stream) {
            video.src = videoUrl.createObjectURL(stream);
            video.play();
        }, function (error) {

        });
    });
    $("#my_video_cap").click(function (e) {
        e.preventDefault();
        $("#my_video_cap").hide();
        context.drawImage(video, 0, 0, 780, 585);
        photo.setAttribute("src", canvas.toDataURL("image/png"));
        //downloader.setAttribute("href", canvas.toDataURL("image/png"));
        //downloader.setAttribute("download", "image.png");
        $("#image_captured").show();
    });
    $("#cap_save").click(function (e) {
        e.preventDefault();
        //window.open(canvas.toDataURL('image/png'));
        var a = document.createElement('a');
        a.href = canvas.toDataURL("png");
        a.download = 'image.png';

        a.click();
    });

    //preview image

    $("#else_upload").change(function () {
        imapePreview(this.value);
    });

});

function rotateImages() {
    var oCurPhoto = $('.fade_images div.current');
    var oNxtPhoto = oCurPhoto.next();
    if (oNxtPhoto.length == 0)
        oNxtPhoto = $('.fade_images div:first');

    oCurPhoto.removeClass('current').addClass('previous');
    oNxtPhoto.css({ opacity: 0.0 }).addClass('current').animate({ opacity: 1.0 }, 1000,
    function () {
        oCurPhoto.removeClass('previous');
    });
}

function imapePreview(image) {
    if (image.files && image.files[0]) {
        var fileReader = new FileReader();
        fileReader.onload = function (e) {
            $("#image_captured").show().attr("src", e.target.result);
        }
        fileReader.readAsDataURL(image.files[0]);
    }
}

function printReceipt() {
    var getReceipt = document.getElementById("customer_receipt");
    var receipt = window.open('', '', 'height=500', 'width=300');
    receipt.document.write('<html><head><title>Print Receipt</title>');
    receipt.document.write('</head></body>');
    receipt.document.write(getReceipt.innerHTML);
    receipt.document.write('</body></html>');
    receipt.document.close();
    setTimeout(function () {
        receipt.print();
    });
    return false;
}

function slide() {
    var currentSlide = 0;
    var all_slides = $(".slide_in ul").find("li").length;
    var margin = 218;
    setInterval(function(){
        $(".slide_in ul").animate({ marginLeft: '-=' + margin }, 500, function () {
            currentSlide++;
            if (currentSlide == all_slides) {
                currentSlide = 0;
                var zero = -40;
                $(".slide_in ul").css('margin-left', zero);
            }
        });
    }, 3000);
}

function printCustReport() {
    var customersList = document.getElementById("customers_report");
    var customersReport = window.open('', '', 'height=500', 'width=1200');
    customersReport.document.write('<html><head><title>Print Customers List</title>');
    customersReport.document.write('</head></body><img src="logo.png" style="width:190px; height:80px;" />');
    customersReport.document.write(customersList.innerHTML);
    customersReport.document.write('</body></html>');
    customersReport.document.close();
    setTimeout(function () {
        customersReport.print();
    });
    return false;
}

function printPassengerAir() {
    var customersList = document.getElementById("todayschedule_report");
    var todayFlight = window.open('', '', 'height=500', 'width=1200');
    todayFlight.document.write('<html><head><title>Today Flight</title>');
    todayFlight.document.write('</head></body><img src="logo.png" style="width:190px; height:80px;" />');
    todayFlight.document.write(customersList.innerHTML);
    todayFlight.document.write('</body></html>');
    todayFlight.document.close();
    setTimeout(function () {
        todayFlight.print();
    });
    return false;
}

function printAircraftSchedules() {
    var airSchedule = document.getElementById("airchaft_schedule_print");
    var aircraftSchedule = window.open('', '', 'height=500', 'width=1200');
    aircraftSchedule.document.write('<html><head><title>Aircraft Schedules</title>');
    aircraftSchedule.document.write('</head></body><img src="logo.png" style="width:190px; height:80px;" />');
    aircraftSchedule.document.write(airSchedule.innerHTML);
    aircraftSchedule.document.write('</body></html>');
    aircraftSchedule.document.close();
    setTimeout(function () {
        aircraftSchedule.print();
    });
    return false;
}

function InitializePlaces() {
    var autocomplete = new google.maps.places.Autocomplete(document.getElementById('fith_var'));
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var place = autocomplete.getPlace();
    });
}

//Gmap = new GMap2(document.getElementById("my_map"));
//Gmap.setCenter(new google.maps.LatLng(0.3189261, 32.5727795), 14);
//directionPanel = document.getElementById("route");
//direction = new GDirections(Gmap, directionPanel);
//var locations = [];
//locations.push({ name: "Aptech Computer Education, Kampala", latlng: new google.maps.LatLng(0.3105855, 32.5790057) });
//var Gmap;
//var directionPanel;
//var direction;

//function initMap() {
//    var center = { lat: 0.3189261, lng: 32.5727795 };
//    var loc = {lat: 0.3105855, lng: 32.5790057};
//    var map = new google.maps.Map(document.getElementById('my_map'), {
//        zoom: 14,
//        center: center
//    });
//}

//function Get_Direction() {
//    var geocoder = new google.maps.Geocoder();
//    //var from = "Aptech Computer Education, Kampala";
//    var center = { lat: 0.3189261, lng: 32.5727795 };
//    var loc = { lat: 0.3105855, lng: 32.5790057 };
//    var map = new google.maps.Map(document.getElementById('my_map'), {
//        zoom: 14,
//        center: center
//    });
//    var address = $("#address").text();
//    geocoder.geocode({ 'address' : address }, function (results, status) {
//        if (status == google.maps.GeocoderStatus.OK) {
//            var from_lat = results[0].geometry.location.lat();
//            var from_long = results[0].geometry.location.lng();
//            var locations = [];
//            locations.push({ name: "Aptech Computer Education, Kampala", latlng: new google.maps.LatLng(0.3105855, 32.5790057) });
//            locations.push({ name: address, latlng: new google.maps.LatLng(from_lat, from_long) });

//            var bounds = new google.maps.LatLngBounds();
//            for (var i = 0; i < locations.length; i++) {
//                var marker = new google.maps.Marker({ position: locations[i].latlng, map: map, title: locations[i].name });
//                bounds.extend(locations[i].latlng);
//            }
//            map.fitBounds(bounds);
//        }
//    });
//}


