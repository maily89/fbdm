function Check(){
	var total=0;
	for (int i=0;i<$("#NumberOfProportionRows").val();i++){
		if ($("#ProportionRows_"+i+"__Proportion") !=null){
		total=total+$("#ProportionRows_"+i+"__Proportion").val();
		}
	}
	return total;
}