//Global variables
var xmlhttp;
var xmlDoc;
var regions = new Array();
var states = new Array();

function readyStates(){
	if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
		xmlhttp=new XMLHttpRequest();
	} else {// code for IE6, IE5
		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
	}
	xmlhttp.open("GET","js/accomodation.xml",false);
	xmlhttp.send();
	xmlDoc=xmlhttp.responseXML;
	var states=xmlDoc.getElementsByTagName("States");
	for(var i=0; i < states.length; i++){
	name = states[i].childNodes[1].childNodes[1].textContent
		$('#states').append(new Option(name, name));
	}
}


function readyMap(state){
	state = state.states.replace("+", " ");
	$('#state1').text(state);
	$('#state2').text(state);
	$('#state3').text(state);

	if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
		xmlhttp=new XMLHttpRequest();
	} else {// code for IE6, IE5
		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
	}
	xmlhttp.open("GET","js/accomodation.xml",false);
	xmlhttp.send();
	xmlDoc=xmlhttp.responseXML;
	getState(state);
	createRegions();
	populateRegions();
}

function getState(state){
	xmlDoc=xmlDoc.getElementsByTagName("States");
	for(var  i = 0; i < xmlDoc.length; i ++){
		if(xmlDoc[i].childNodes[1].childNodes[1].textContent == state){
			xmlDoc = xmlDoc[i].childNodes[1];
		}
	}
}

function region(name,areas,room,bed,employee, occupancy, stay) {
	this.name=name;
	this.bed=bed;
	this.room=room;
	this.employee=employee;
	this.occupancy=occupancy;
	this.stay=stay;
	this.areas = areas;
	
	this.compare = function(r){
		var occ = 0;
    	for(var i=0; i<r.occupancy.length; i++) {
    	   	if(occupancy[i] != 0 && r.occupancy[i] != 0) {
    	       	if(occupancy[i] < r.occupancy[i]) {
    	           	occ =occ-1;
    			} else if(occupancy[i] > r.occupancy[i]) {
    	           	occ = occ+1;
    	        }
    		}
    	}
		return occ;
	}
}


function area(name, room, bed, employee, occupancy, stay){
	this.name=name;
	this.bed=bed;
	this.room=room;
	this.employee=employee;
	this.occupancy=occupancy;
	this.stay=occupancy;
}

function createoccupancy(children){
	occupancy = new Array(12);
	occupancy[0] = children[0].textContent;
	occupancy[1] = children[1].textContent;
	occupancy[2] = children[2].textContent;
	occupancy[3] = children[3].textContent;
	occupancy[4] = children[4].textContent;
	occupancy[5] = children[5].textContent;
	occupancy[6] = children[6].textContent;
	occupancy[7] = children[7].textContent;
	occupancy[8] = children[8].textContent;
	occupancy[9] = children[9].textContent;
	occupancy[10] = children[10].textContent;
	occupancy[11] = children[11].textContent;
	return occupancy;
}

function createStaylength(children){
	staylength = new Array(12);
	staylength[0] = children[0].textContent;
	staylength[1] = children[1].textContent;
	staylength[2] = children[2].textContent;
	staylength[3] = children[3].textContent;
	staylength[4] = children[4].textContent;
	staylength[5] = children[5].textContent;
	staylength[6] = children[6].textContent;
	staylength[7] = children[7].textContent;
	staylength[8] = children[8].textContent;
	staylength[9] = children[9].textContent;
	staylength[10] = children[10].textContent;
	staylength[11] = children[11].textContent;
	return staylength;
}

function createAreas(areaChildren){
	areas = new Array();
	for(var t=0; t < areaChildren.length; t++){
		aName = areaChildren[t].children[0].textContent;
		aRooms = areaChildren[t].children[1].textContent;
		aBeds = areaChildren[t].children[2].textContent;
		aEmployees = areaChildren[t].children[3].textContent;
		aoccupancy = createoccupancy(areaChildren[t].children[4].children);
		aStay = createStaylength(areaChildren[t].children[5].children);
		areas.push(new area(aName, aRooms, aBeds, aEmployees, aoccupancy, aStay));
	}
	return areas;
}


function createRegions(){

	regionNodes = xmlDoc.children[1];

	for(var j=0;j<regionNodes.children.length;j++){
		name   = regionNodes.children[j].children[0].textContent;
		rAreas = createAreas(regionNodes.children[j].children[1].children);
		rooms  = regionNodes.children[j].children[2].textContent;
		beds   = regionNodes.children[j].children[3].textContent;
		employees = regionNodes.children[j].children[4].textContent;
		roccupancy = createoccupancy(regionNodes.children[j].children[5].children);
		rStay  = createStaylength(regionNodes.children[j].children[6].children);
		
		regions.push(new region(name, rAreas, rooms, beds, employees, roccupancy,rStay));
	}
}

function populateRegions(){
	var mySelect = $('#stateRegions');
	$.each(regions, function(index, value) {
		mySelect.append(new Option(value.name, value.name));
	});
	populateAreas();
}


function populateAreas(){
	var mySelect = $('#stateAreas');
	mySelect.empty();
	for(var i = 0; i< regions.length; i ++){
		if(regions[i].name == $("#stateRegions :selected").text()){
			for(var j = 0; j< regions[i].areas.length; j ++){
				mySelect.append(new Option(regions[i].areas[j].name, regions[i].areas[j].name));
			}
		}
	}
}

function emptiestRegion(){
	var empty = regions[0];
	for(var i = 1; i< regions.length; i++){
		if(empty.compare(regions[i])>0){
			empty = regions[i];
		}
	}
	$("#stateRegions").val(empty.name);
}