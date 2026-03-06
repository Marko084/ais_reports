<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDMap.ascx.cs" Inherits="AISReports.UserControls.NCDMap" %>
 <!--<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>-->
  <script type="text/javascript" src="https://maps.google.com/maps/api/js?key=AIzaSyC3wUzcg2kcWgRpnR_D_LYb_V6EbToHu98"></script>

  <script type="text/javascript" src="../script/gmaps.js"></script>
  <!--<link href='//fonts.googleapis.com/css?family=Convergence|Bitter|Droid+Sans|Ubuntu+Mono' rel='stylesheet' type='text/css' />-->
  <link href='../css/geo-map-styles.css' rel='stylesheet' type='text/css' />
<style type="text/css">
    .map-settings { height:100% !important;}
    .chart-title-text {width:56%;}
</style>
    <script type="text/javascript">
        //var map;
        //var mapOptions = {streetViewControl: false};

        $(document).ready(function(){
            var directionsService = new google.maps.DirectionsService();

            var request = {
              origin      : 'Melbourne VIC', // a city, full address, landmark etc
              destination : 'Sydney NSW',
              travelMode  : google.maps.DirectionsTravelMode.DRIVING
            };

            directionsService.route(request, function(response, status) {
              if ( status == google.maps.DirectionsStatus.OK ) {
                alert( response.routes[0].legs[0].distance.value * 0.000621371 ); // the distance in metres
              }
              else {
                // oops, there's no route between these two locations
                // every time this happens, a kitten dies
                // so please, ensure your address is formatted properly
              }
            });
        });
    </script>
    <div class="user-control-widget" style="height:900px;">
       <div class="chart-title" style="width:100%;">
            <asp:Label runat="server" ID="lblChartTitle" CssClass="chart-title-text"  />
        </div>
        <asp:Panel runat="server" ID="pChart" class="grid-section" Height="600">
            <div runat="server" id="map" class="map-settings"></div>
        </asp:Panel>
    </div>
