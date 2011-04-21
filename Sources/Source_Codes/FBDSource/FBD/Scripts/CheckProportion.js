function CheckTotalFIProportion() {
    var total = 0;
	for (i=0;i<$("#NumberOfProportionRows").val();i++){
	    
	    if ($("#ProportionRows_" + i.toString() + "__Proportion").length) {
		    total=parseFloat(total)+parseFloat($("#ProportionRows_"+i.toString()+"__Proportion").val());
		}
    }
    if (total == 100) {
        return true;
    }
    else {
        alert("Not equal. Total gets " + total.toString() + "%");
        return false;
    }
    return false;
}

function CheckTotalNFIProportionByIndustry() {
    for (i = 0; i < $("#NumberOfProportionRows").val(); i++) {

        if (!$("#ProportionRows_" + i.toString() + "__Proportion").length) {
            var total = 0;
            j = i + 1;
            if ($("#ProportionRows_" + j.toString() + "__Proportion").length) {
                while ($("#ProportionRows_" + j.toString() + "__Proportion").length) {
                    total = parseFloat(total) + parseFloat($("#ProportionRows_" + j.toString() + "__Proportion").val());
                    j++;
                }
                if (total != 100) {
                    alert("One or more leaf indexes do not get total equal their parent index");
                    return false;
                }
            }
            i = j - 1;
        }
    }
    return true;
}