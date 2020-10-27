	function SameAs(){
		
		//alert(window.document.forms[3].elements[4].name);
		var tempLName,tempFName,tempA,tempC,tempS,tempZ
		tempLName = window.document.forms[3].elements[4].value;
		tempFName = window.document.forms[3].elements[5].value;
		tempA = window.document.forms[3].elements[6].value;
		tempC = window.document.forms[3].elements[7].value;
		tempS = window.document.forms[3].elements[8].value;
		tempZ= window.document.forms[3].elements[9].value;		
		
		
		if (window.document.forms[3].elements[11].checked == true){
			window.document.forms[3].elements[12].value = tempLName;
			window.document.forms[3].elements[13].value = tempFName;
			window.document.forms[3].elements[14].value = tempA;
			window.document.forms[3].elements[15].value = tempC;
			window.document.forms[3].elements[16].value = tempS;
			window.document.forms[3].elements[17].value = tempZ;
			
			return true;
		}
		if (window.document.forms[3].elements[11].checked == false){
			window.document.forms[3].elements[12].value = "";
			window.document.forms[3].elements[13].value = "";
			window.document.forms[3].elements[14].value = "";
			window.document.forms[3].elements[15].value = "";
			window.document.forms[3].elements[16].value = "";
			window.document.forms[3].elements[17].value = "";							
		}
		
		else{
		
			return false;
		}

	}