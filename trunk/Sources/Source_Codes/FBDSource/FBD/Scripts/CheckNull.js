function Check() {
	var total=0;
	for (i=0;i<$("#NumberOfProportionRows").val();i++){
	    
	    if ($("#ProportionRows_" + i.toString() + "__Proportion") != null) {
	        
		    total=parseFloat(total)+parseFloat($("#ProportionRows_"+i.toString()+"__Proportion").val());
		}
    }
    if (total == 100) return true;
    alert("Total percentage is not 100%");
    return false;
}