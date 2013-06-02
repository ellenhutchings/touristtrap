var map;
var mapOptions;
var geocoder;

var regionMarker = new google.maps.Marker();
var attractionMarkers = new Array();
var pinShadow = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_shadow",
        new google.maps.Size(40, 37),
        new google.maps.Point(0, 0),
        new google.maps.Point(12, 35));var yellowIcon = "http://www.google.com/intl/en_us/mapfiles/ms/micons/green-dot.png";

var bluepinColor = "7E55FC";
var bluepinImage = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + bluepinColor,
        new google.maps.Size(21, 34),
        new google.maps.Point(0,0),
        new google.maps.Point(10, 34));
var greenpinColor = "06B32D";
var greenpinImage = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + greenpinColor,
        new google.maps.Size(21, 34),
        new google.maps.Point(0,0),
        new google.maps.Point(10, 34));


var centre = new google.maps.LatLng(-34.7386, 138.6170);
var request;

function initialize(){
    geocoder = new google.maps.Geocoder();
	request = {
		address: 'Adelaide, South Australia',
		region: "au"
	}
	
}

function mapRegion(){
	var mapOptions = {
	    zoom: 8,
	    center: centre,
	    mapTypeId: google.maps.MapTypeId.ROADMAP
	};
	map = new google.maps.Map(document.getElementById('map'), mapOptions);
}


function changeLocation(place=null){
	if(place!=null){
		if(place.indexOf('-')>0){
			place = place.split('-')[0].trim();
		}
		
		request = {
			address: place+', South Australia',
			region: "au"
		}

	}
	
	geocoder.geocode(request, function(results, status) {
      if (status == google.maps.GeocoderStatus.OK) {
      	regionMarker.setMap(null);
        map.setCenter(results[0].geometry.location);
        regionMarker = new google.maps.Marker({
            map: map,
            position: results[0].geometry.location
        });
      } else {
        alert("Geocode was not successful for the following reason: " + status);
      }
    });
}

function createMarker(title, position, accom){
	var colourIcon;
	if(accom){
		colourIcon = bluepinImage;
	} else {
		colourIcon = greenpinImage;
	}

	var location = position.split(',');
	attractionMarkers.push( new google.maps.Marker({
			map: map,
			position: new google.maps.LatLng(location[0], location[1]),
			title: title,
			icon: colourIcon,
			shadow: pinShadow
		})
	);
}

function removeMarkers(){
	for(var i=0;i<attractionMarkers.length; i++){
		attractionMarkers[i].setMap(null);
	}
}
