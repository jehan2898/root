	function SameAsInj(){
		//alert(window.document.forms[2].elements[10].name);
		var tempLName,tempFName,tempA,tempC,tempS,tempZ
		tempLName = window.document.forms[2].elements[3].value;
		tempFName = window.document.forms[2].elements[4].value;
		tempA = window.document.forms[2].elements[5].value;
		tempC = window.document.forms[2].elements[6].value;
		tempS = window.document.forms[2].elements[7].value;
		tempZ= window.document.forms[2].elements[8].value;		
		
		
		if (window.document.forms[2].elements[10].checked == true){
			window.document.forms[2].elements[11].value = tempLName;
			window.document.forms[2].elements[12].value = tempFName;
			window.document.forms[2].elements[13].value = tempA;
			window.document.forms[2].elements[14].value = tempC;
			window.document.forms[2].elements[15].value = tempS;
			window.document.forms[2].elements[16].value = tempZ;
			
			return true;
		}
		if (window.document.forms[2].elements[10].checked == false){
			window.document.forms[2].elements[11].value = "";
			window.document.forms[2].elements[12].value = "";
			window.document.forms[2].elements[13].value = "";
			window.document.forms[2].elements[14].value = "";
			window.document.forms[2].elements[15].value = "";
			window.document.forms[2].elements[16].value = "";							
		}
		
		else{
		
			return false;
		}

	}