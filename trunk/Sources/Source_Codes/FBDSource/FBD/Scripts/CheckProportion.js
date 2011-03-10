function CheckTotalFIProportion() {
    var total = 0;
	for (i=0;i<$("#NumberOfProportionRows").val();i++){
	    
	    if ($("#ProportionRows_" + i.toString() + "__Proportion").length) {
		    total=parseFloat(total)+parseFloat($("#ProportionRows_"+i.toString()+"__Proportion").val());
		}
    }
    if (total == 100) return true;

    return false;
}