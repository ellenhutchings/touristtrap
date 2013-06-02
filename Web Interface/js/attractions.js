function apiCall(region){

	var host = "http://govhack.atdw.com.au";
	var key = "278965474541";
	var url = host+"/productsearchservice.svc/products?key="+key+"&rg="+region+"&dsc=false";    
    
	var request = $.ajax({
       url: url,
       type: "GET",
       dataType: "xml",
       cache: true,
       success: function(data){
       		removeMarkers();
			var products = data.getElementsByTagName("product_record");
			for(var i=0; i<products.length; i++){
				title = products[i].childNodes[3].textContent;
				var accom = false;

				//
				if((products[i].childNodes[5].textContent)=="ACCOMM"){
					accom = true;
				}
				var location = products[i].childNodes[7].textContent;
				createMarker(title, location, accom);
			}
       }
   });

}

