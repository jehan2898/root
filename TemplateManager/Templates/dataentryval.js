	function checkImpData(){
		
		if(window.document.frm2AddClaim.client_id.options[0].selected == true){
			alert('Please select Client from the pull down menu.');
			window.document.frm2AddClaim.client_id.focus();
			return false;
		}
		if(window.document.forms[2].elements[3].value == ""){
			alert('Please Enter Injured Last Name.');
			window.document.forms[2].elements[3].focus();
			return false;
		}
		if(window.document.forms[2].elements[4].value == ""){
			alert('Please Enter Injured First Name.');
			window.document.forms[2].elements[4].focus();
			return false;
		}		
		if(window.document.forms[2].elements[21].value == ""){
			alert('Please Enter Date of Accident.');
			window.document.forms[2].elements[21].focus();
			return false;
		}
		if(window.document.frm2AddClaim.ins_id.options[0].selected == true){
			alert('Please select Insurer from the pull down menu.');
			window.document.frm2AddClaim.ins_id.focus();
			return false;
		}
		
		else{
			return true;
		}
	}
