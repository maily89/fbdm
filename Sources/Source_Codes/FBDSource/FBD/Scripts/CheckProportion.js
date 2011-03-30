function CheckTotalFIProportion() {
    var total = 0;
	for (i=0;i<$("#NumberOfProportionRows").val();i++){
	    
	    if ($("#ProportionRows_" + i.toString() + "__Proportion").length) {
		    total=parseFloat(total)+parseFloat($("#ProportionRows_"+i.toString()+"__Proportion").val());
		}
    }
    if (total == 100) return "Total gets 100%";

    return "Not enough. Total gets " + total.toString() + "%";
}

function CheckTotalNFIProportionByIndustry() {
    for (i = 0; i < $("#NumberOfProportionRows").val(); i++) {

        if (!$("#ProportionRows_" + i.toString() + "__Proportion").length) {
            var total = 0;
            j = i + 1;
            while ($("#ProportionRows_" + j.toString() + "__Proportion").length) {
                total = parseFloat(total) + parseFloat($("#ProportionRows_" + j.toString() + "__Proportion").val());
                j++;
            }
            if (total != 100) return "One or more leaf indexes do not get total equal their parent index";
            i = j - 1;
        }
    }
    return "Total gets fine";
}