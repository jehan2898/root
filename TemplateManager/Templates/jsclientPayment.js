function currencyFormat2(fld, milSep, decSep, e) {
var sep = 0;
	var key = '';
	var i = j = 0;
	var len = len2 = 0;
	var strCheck = '0123456789';
	var aux = aux2 = '';
	var fldval;
	var whichCode = (window.Event) ? e.which : e.keyCode;
	fldval=fld.value;
	//alert(whichCode);
	if (whichCode == 8) {
		//alert(fld.value);
		if (fld.value==''){
			callclear(fld);
			return false;
		}
		//alert(fld.value.substr(0, fld.value.length-1))
		fld.value = fld.value.substr(0, fld.value.length-1) ;
		callme(fld.value,fld);		return false;
	}  // Enter
	key = String.fromCharCode(whichCode);  // Get key value from key code
	//alert(key);

	//if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
	len = fld.value.length;
	for(i = 0; i < len; i++)
		if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep)) break;
		aux = '';
		for(; i < len; i++)
		if (strCheck.indexOf(fld.value.charAt(i))!=-1) aux += fld.value.charAt(i);
		aux += key;
		len = aux.length;
		if (len == 0) fld.value = '';
		if (len == 1) fld.value = '0'+ decSep + '0' + aux;
		if (len == 2) fld.value = '0'+ decSep + aux;
		if (len > 2) {
			aux2 = '';
			for (j = 0, i = len - 3; i >= 0; i--) {
				if (j == 3) {
					aux2 += milSep;
					j = 0;
				}
				aux2 += aux.charAt(i);
				j++;
			}
			fld.value = '';
			len2 = aux2.length;
			for (i = len2 - 1; i >= 0; i--)
			fld.value += aux2.charAt(i);
			fld.value += decSep + aux.substr(len - 2, len);
			//fld.value + = fldval;
		}
callme(fld.value,fld);
return false;
}

function callme(a,b){
	var getlast,c,outstanding,outstandingval,balanceval,z,myobj,objname,getStr,getLastnum,getfirst,getstrlast
	var strCheck = '0123456789';
	c=b.name;
	//alert("c="+c);
	//alert(a.length);
	getlast=c.substring(c.indexOf("@")-1,c.length);
	//alert("getlast="+getlast);
	getfirst=c.substring(0,c.indexOf("@")-1);
	getfirstnum=getlast.substring(getfirst.length-1,getfirst.length);
	//alert("getfirstnum="+getfirstnum);
	//alert(strCheck.indexOf(getfirstnum));
	if (strCheck.indexOf(getfirstnum) > 0){
		getlast=c.substring(c.indexOf("@")-2,c.length);
	}
	myobj="txtDue"+getlast;
	//alert(myobj);
	for(i=0;i<document.forms.length;i++){
		if (i==5){
			for(j=0;j<document.forms[i].elements.length;j++){
				z=eval("document.forms["+i+"].elements["+j+"].name");
				getStr=z.substring(z.indexOf("@")-1,z.length);
				getstrfirst=z.substring(0,z.indexOf("@")-1)
				if (strCheck.indexOf(getstrfirst) > 0){
					getStr=z.substring(z.indexOf("@")-2,z.length);
				}
				//alert("getStr="+ getStr + "getlast="+getlast);
				if (getStr==getlast){
					//alert(getlast);
					objname=eval("document.forms["+i+"].elements["+j+"].name");
					objname=objname.substring(0,objname.indexOf("@")-1);
					if (strCheck.indexOf(objname.substring(objname.length-1,objname.length)) > 0){
						objname=objname.substring(0,objname.length-1);
					}
					//alert("objname="+objname);
					if (objname=='txtDue'){
						outstanding=eval("document.forms["+i+"].elements["+j+"].value");
						//alert(outstanding);
						outstanding=outstanding.replace(',','');
						//alert(parseFloat(outstanding.substring(1,outstanding.length)));
						outstandingval=parseFloat(outstanding.substring(1,outstanding.length)) - parseFloat(a) ;
						document.forms[i].elements[j+2].value=outstandingval;
						//document.forms[i].payment.value= parseFloat(a);
					}
				}
			}
		}
	}
    //for(j=0;j<document.forms[i].elements.length;j++){

}


function callclear(b){
	var getlast,c,outstanding,outstandingval,balanceval,z,myobj,objname
	c=b.name;
	//alert("c="+c);
	getlast=c.substring(c.indexOf("@")-1,c.length);
	myobj="txtDue"+getlast;
	for(i=0;i<document.forms.length;i++){
		if (i==5){
			for(j=0;j<document.forms[i].elements.length;j++){
				z=eval("document.forms["+i+"].elements["+j+"].name");
				if (z.substring(z.indexOf("@")-1,z.length)==getlast){
					objname=eval("document.forms["+i+"].elements["+j+"].name");
					if (objname.substring(0,objname.indexOf("@")-1)=='txtDue'){
						document.forms[i].elements[j+2].value='';
					}
				}
			}
		}
	}
}

function DTCRound(value, decimals)
{
    return(
              parseFloat(value) != "NaN" && parseInt(decimals) != "NaN" 
              ? (Math.round(value * Math.pow(10, decimals))) / Math.pow(10, decimals) 
              : "NaN"
              );
}

function adjustpay(){
	//alert("hi");
	var i=5;
	var j,z,payment,origpayment;
	var paymade = "0.00";
	for(j=0;j<document.forms[5].elements.length;j++){
		z=eval("document.forms["+i+"].elements["+j+"].name");
		getlast=z.substring(z.indexOf("@")-1,z.length);
		getfirst=z.substring(0,z.indexOf("@"));
		var strVal=getfirst.substring(0,11);
		if (strVal=="txtPayments"){
			payment=eval("document.forms[5].elements["+j+"].value");
			//alert(payment);
			if (payment!=''){
				paymade=parseFloat(paymade) + parseFloat(payment);
			}
		}
	}
	origpayment = eval("document.forms[5].elements['origpayment'].value");
	origpayment = origpayment.replace('$','');
	origpayment = origpayment.replace(',','');
	origpayment = parseFloat(origpayment);
	//alert(origpayment);
	//alert(paymade);
	if (paymade != origpayment){
		alert("The Payments made do not add up" + paymade + "<>" +  origpayment);
		return false;
	}else{
		return checkDifference();
	}
}

function checkDifference(){
	var i=5;
	var j,z,getlast,getfirst,strVal;
	var strCheck = '0123456789';
	for(j=0;j<document.forms[5].elements.length;j++){
		z=eval("document.forms["+i+"].elements["+j+"].name");
		getlast=z.substring(z.indexOf("@")-1,z.length);
		//alert("getlast=="+getlast);
		getfirst=z.substring(0,z.indexOf("@")-1);
		var strVal=getfirst.substring(0,6);
		if (strVal=="action"){
			if (strCheck.indexOf(getfirst.substring(getfirst.length-1,getfirst.length)) > 0){
				getlast=z.substring(z.indexOf("@")-2,z.length);
			}
			//alert("getlast2=="+getlast);
			payment=eval("document.forms["+i+"].elements['txtPayments"+getlast+"'].value");
			//alert(payment);
			if (payment != '') {
				iControlLength = eval("document.forms["+i+"].elements['action"+getlast+"'].length");
				//alert(iControlLength );
				strFormField = eval("document.forms["+i+"].elements['action"+getlast+"']");
				//alert(strFormField);
				bolSelected = false;
				for (count=0;count<iControlLength;count++){
					if ((strFormField[count].checked) && (payment !='')){
						bolSelected = true;
						break;
					}
				}     
				if(! bolSelected){
					alert("Please indicate whether you want to dispute or write off the amount!");
					return false;
				}else{
					return true;
				} 
			}
		}
	}
}

function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}
function showdocs(){
	MM_openBrWindow('ShowDocs.asp','win1','toolbar=yes,location=yes,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
}
function isDate(val,format) {
	//alert(val);
	var date=getDateFromFormat(val,format);
	if (date==0) { 
		alert("Please Enter Date in format mm/dd/yyyy");
		return false;
	}
	return true;
}
function LeadingZero(nr)
{
	if (nr < 10) nr = "0" + nr;
	return nr;
}

function ValidateForm(){
	var a,b,c,d,e,f,g,h,i,j,y;
	h=LeadingZero(document.frmPayment.ADatemon.value);
	y = LeadingZero(document.frmPayment.ADateYear.value);
	a= h + "/28/" + y;
	c=document.frmPayment.MonVar.value;

	if (isDate(a,"mm/dd/yyyy")){
	}else{
		alert("Please Enter a Valid Date" + a);
		return false;
	}
	document.frmPayment.paymentmade.value="set";
	document.frmPayment.ADate.value=a;
	return validateAmount();
}

function validateAmount(){
	if (document.frmPayment.amount.value==""){
		alert("Please Enter Valid Amount");
		return false ;
	}else{
		return validateDiff();
	}

}

function checkRadioControl(strFormField) 
{
    iControlLength = strFormField.length
    bolSelected = false;
    for (count=0;count<iControlLength;count++){
         if(strFormField[count].checked){
                   bolSelected = true;
                   break;
         }
    }     
     if(! bolSelected){
         alert("Please indicate whether you want to dispute or write off the amount!");
         return false;
    }else{
         return true;
    }
}


function validateDiff(){
	if (document.frmPayment.txtDiff2.value==""){
		alert("Please Indicate the Month you want to apply Payment against or Click Difference")
		return false;
	}else{
		return checkRadioControl(document.frmPayment.rdoAction);
	}
}

function Print()
{
	//'document.myfrm.action= "PrintFrame.asp" ;
	//'document.myfrm.submit();
	//alert("hi");
	var a;
	a=document.myfrm.seldoc1.value;
	MM_openBrWindow('richedit.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
}
function openedit(a){
	var x,box
	x=window.confirm("You are about to Delete Payments for this posting");
	if (x){
		document.frmTrans.delvar.value=a;
		//document.frmTrans.paymentmade.value="set";
		document.frmTrans.submit();
	}
}

