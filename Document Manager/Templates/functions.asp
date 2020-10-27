
<script language=javascript>
<!--
function showClient(a){
MM_openBrWindow('clientWin.asp?Cid='+a,'win1','scrollbars=yes,width=300,height=300')
}

function showNotes(a,b){
//alert("NotesWin.asp?nid="+a+"&cid="+b);
MM_openBrWindow('NotesWin.asp?nid='+a+'&cid='+b,'win1','scrollbars=yes,width=500,height=700')
}

//--------------------------end function-----------------------------

function showIns(a){
MM_openBrWindow('InsWin.asp?Iid='+a,'win1','scrollbars=yes,width=300,height=300')
}
//-->
function showdocs(a){
	//alert("hi"+a);
	if (a=="pom"){
		//alert("i am in pom");
		MM_openBrWindow('ShowDocsPOM.asp','win1','toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
	}
	else{
		if (a=="scases"){
			//alert("i am in scases");
			MM_openBrWindow('ShowDocsScases.asp','win1','toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
		}
		else{
			MM_openBrWindow('ShowDocs.asp','win1','toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
		}
	}
}

function showpdocs(a){
	//alert("hi"+a);
	MM_openBrWindow('ShowDocs.asp','win1','toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
}

function popupWindow(url)
{
	var new_window = window.open(url, '','status=no,resizable=no,scrollbars=yes,top=180,left=275,width=450,height=400');
	new_window.focus();
}
function popupWindowSmall(url)
{
	var new_window = window.open(url, '','status=no,resizable=no,scrollbars=yes,top=180,left=275,width=450,height=420');
	new_window.focus();
}

function popupWindow2(url)
{
	var new_window = window.open(url, '','status,resizable=yes,scrollbars=yes,top=180,left=275,width=600,height=480');
	new_window.focus();
}

function popupWindowSizeable(url)
{
	var new_window = window.open(url, '','status,resizable=yes,scrollbars');
	new_window.focus();
}

function currency( num ) 
{ 
   var prefix = ""; 
   var suffix = ""; 
   if ( num < 0 ) 
   { 
       prefix = "("; 
       suffix = ")"; 
       num = - num; 
   } 
       var temp = Math.round( num * 100.0 ); // convert to pennies! 
       if ( temp < 10 ) return prefix + "0.0" + temp + suffix; 
       if ( temp < 100 ) return prefix + "0." + temp + suffix; 
       temp = prefix + temp; // convert to string! 
       return temp.substring(0,temp.length-2) + "." + temp.substring(temp.length-2) + suffix; 
} 


function suycDateDiff( start, end, interval, rounding ) {

    var iOut = 0;
    
    // Create 2 error messages, 1 for each argument. 
    var startMsg = "Check the Start Date and End Date\n"
        startMsg += "must be a valid date format.\n\n"
        startMsg += "Please try again." ;
		
    var intervalMsg = "Sorry the dateAdd function only accepts\n"
        intervalMsg += "d, h, m OR s intervals.\n\n"
        intervalMsg += "Please try again." ;

    var bufferA = Date.parse( start ) ;
    var bufferB = Date.parse( end ) ;
    	
    // check that the start parameter is a valid Date. 
    if ( isNaN (bufferA) || isNaN (bufferB) ) {
        alert( startMsg ) ;
        return null ;
    }
	
    // check that an interval parameter was not numeric. 
    if ( interval.charAt == 'undefined' ) {
        // the user specified an incorrect interval, handle the error. 
        alert( intervalMsg ) ;
        return null ;
    }
    
    var number = bufferB-bufferA ;
    
    // what kind of add to do? 
    switch (interval.charAt(0))
    {
        case 'd': case 'D': 
            iOut = parseInt(number / 86400000) ;
            if(rounding) iOut += parseInt((number % 86400000)/43200001) ;
            break ;
        case 'h': case 'H':
            iOut = parseInt(number / 3600000 ) ;
            if(rounding) iOut += parseInt((number % 3600000)/1800001) ;
            break ;
        case 'm': case 'M':
            iOut = parseInt(number / 60000 ) ;
            if(rounding) iOut += parseInt((number % 60000)/30001) ;
            break ;
        case 's': case 'S':
            iOut = parseInt(number / 1000 ) ;
            if(rounding) iOut += parseInt((number % 1000)/501) ;
            break ;
        default:
        // If we get to here then the interval parameter
        // didn't meet the d,h,m,s criteria.  Handle
        // the error. 		
        alert(intervalMsg) ;
        return null ;
    }
    
    return iOut ;
}


function dateAdd( start, interval, number ) {
	
    // Create 3 error messages, 1 for each argument. 
    var startMsg = "Sorry the start parameter of the dateAdd function\n"
        startMsg += "must be a valid date format.\n\n"
        startMsg += "Please try again." ;
		
    var intervalMsg = "Sorry the dateAdd function only accepts\n"
        intervalMsg += "d, h, m OR s intervals.\n\n"
        intervalMsg += "Please try again." ;

    var numberMsg = "Sorry the number parameter of the dateAdd function\n"
        numberMsg += "must be numeric.\n\n"
        numberMsg += "Please try again." ;
		
    // get the milliseconds for this Date object. 
    var buffer = Date.parse( start ) ;
	
    // check that the start parameter is a valid Date. 
    if ( isNaN (buffer) ) {
        alert( startMsg ) ;
        return null ;
    }
	
    // check that an interval parameter was not numeric. 
    if ( interval.charAt == 'undefined' ) {
        // the user specified an incorrect interval, handle the error. 
        alert( intervalMsg ) ;
        return null ;
    }

    // check that the number parameter is numeric. 
    if ( isNaN ( number ) )	{
        alert( numberMsg ) ;
        return null ;
    }

    // so far, so good...
    //
    // what kind of add to do? 
    switch (interval.charAt(0))
    {
        case 'd': case 'D': 
            number *= 24 ; // days to hours
            // fall through! 
        case 'h': case 'H':
            number *= 60 ; // hours to minutes
            // fall through! 
        case 'm': case 'M':
            number *= 60 ; // minutes to seconds
            // fall through! 
        case 's': case 'S':
            number *= 1000 ; // seconds to milliseconds
            break ;
        default:
        // If we get to here then the interval parameter
        // didn't meet the d,h,m,s criteria.  Handle
        // the error. 		
        alert(intervalMsg) ;
        return null ;
    }
    return new Date( buffer + number ) ;
}


function youSure(dowhat)
{
	if (dowhat == "updateClaims")
	{
		if (confirm("Are you sure you would like to update this record?"))
		{
			return true;
		}
		else
		{
			return false;
		}
	}	
	
}	

//-------------------------------------------------------------------
// Trim functions
//   Returns string with whitespace trimmed
//-------------------------------------------------------------------
function LTrim(str) {
	for (var i=0; str.charAt(i)==" "; i++);
	return str.substring(i,str.length);
	}
function RTrim(str) {
	for (var i=str.length-1; str.charAt(i)==" "; i--);
	return str.substring(0,i+1);
	}
function Trim(str) {
	return LTrim(RTrim(str));
	}

//-------------------------------------------------------------------
// isNull(value)
//   Returns true if value is null
//-------------------------------------------------------------------
function isNull(val) {
	if (val == null) { return true; }
	return false;
	}

function isEmpty(s)
{   return ((s == null) || (s.length == 0))
}

//-------------------------------------------------------------------
// isBlank(value)
//   Returns true if value only contains spaces
//-------------------------------------------------------------------
function isBlank(val) {
	if (val == null) { return true; }
	for (var i=0; i < val.length; i++) {
		if ((val.charAt(i) != ' ') && (val.charAt(i) != "\t") && (val.charAt(i) != "\n")) { return false; }
		}
	return true;
	}

//-------------------------------------------------------------------
// isInteger(value)
//   Returns true if value contains all digits
//-------------------------------------------------------------------
function isInteger(val) {
	for (var i=0; i < val.length; i++) {
		if (!isDigit(val.charAt(i))) { return false; }
		}
	return true;
	}

//-------------------------------------------------------------------
// isNumeric(value)
//   Returns true if value contains a positive float value
//-------------------------------------------------------------------
function isNumeric(val) {
	var dp = false;
	for (var i=0; i < val.length; i++) {
		if (!isDigit(val.charAt(i))) { 
			if (val.charAt(i) == '.') {
				if (dp == true) { return false; } // already saw a decimal point
				else { dp = true; }
				}
			else {
				return false; 
				}
			}
		}
	return true;
	}
	
//-------------------------------------------------------------------
// isDigit(value)
//   Returns true if value is a 1-character digit
//-------------------------------------------------------------------
function isDigit(num) {
	var string="1234567890";
	if (string.indexOf(num) != -1) {
		return true;
		}
	return false;
	}

//-------------------------------------------------------------------
// isMonth(string)
//   Returns true if string is either a full month name or a month
//   abbreviation.
//-------------------------------------------------------------------
function isMonth(val) {
	val = val+"";
	val = val.toLowerCase();
	if ((val=="jan") || (val=="feb") || (val=="mar") || (val=="apr") || (val=="may") || (val=="jun") ||
	    (val=="jul") || (val=="aug") || (val=="sep") || (val=="oct") || (val=="nov") || (val=="dec")) {
			return true;
			}
	if ((val=="january") || (val=="february") || (val=="march") || (val=="april") || (val=="may") ||
	    (val=="june") || (val=="july") || (val=="august") || (val=="september") || (val=="october") ||
	    (val=="november") || (val=="december")) {
	    	return true;
	    	}
	return false;
	}


//-------------------------------------------------------------------
// setNullIfBlank(input_object)
//   Sets a form field to "" if it isBlank()
//-------------------------------------------------------------------
function setNullIfBlank(obj) {
	if (isBlank(obj.value)) {
		obj.value = "";
		}
	}

//-------------------------------------------------------------------
// setFieldsToUpperCase(input_object)
//   Sets value of form field toUpperCase() for all fields passed
//-------------------------------------------------------------------
function setFieldsToUpperCase() {
	for (var i=0; i<arguments.length; i++) {
		var obj = arguments[i];
		obj.value = obj.value.toUpperCase();
		}
	}

//-------------------------------------------------------------------
// disallowBlank(input_object[,message[,true]])
//   Checks a form field for a blank value. Optionally alerts if 
//   blank and focuses
//-------------------------------------------------------------------
function disallowBlank(obj,msg,a){
	var msg;
	var dofocus;
	if (arguments.length>1) { msg = arguments[1]; }
	if (arguments.length>2) { dofocus = arguments[2]; } else { dofocus=false; }
	alert(obj.value);
	if (isBlank(obj.value)) {
		if (!isBlank(msg)) {
			alert(msg);
			}
		if (dofocus) {
			obj.select();
			obj.focus();
			}
		return true;
		}
	return false;
	}

//-------------------------------------------------------------------
// disallowModify(input_object[,message[,true]])
//   Checks a form field for a value different than defaultValue. 
//   Optionally alerts and focuses
//-------------------------------------------------------------------
function disallowModify(obj) {
	var msg;
	var dofocus;
	if (arguments.length>1) { msg = arguments[1]; }
	if (arguments.length>2) { dofocus = arguments[2]; } else { dofocus=false; }
	if (getInputValue(obj) != getInputDefaultValue(obj)) {
		if (!isBlank(msg)) {
			alert(msg);
			}
		if (dofocus) {
			obj.select();
			obj.focus();
			}
		setInputValue(obj,getInputDefaultValue(obj));
		return true;
		}
	return false;
	}

//-------------------------------------------------------------------
// isChanged(input_object)
//   Returns true if input object's state has changed since it was
//   created.
//-------------------------------------------------------------------
function isChanged(obj) {
	if ((typeof obj.type != "string") && (obj.length > 0) && (obj[0] != null) && (obj[0].type=="radio")) {
		for (var i=0; i<obj.length; i++) {
			if (obj[i].checked != obj[i].defaultChecked) { return true; }
			}
		return false;
		}
	if ((obj.type=="text") || (obj.type=="hidden") || (obj.type=="textarea"))
		{ return (obj.value != obj.defaultValue); }
	if (obj.type=="checkbox") {
		return (obj.checked != obj.defaultChecked);
		}
	if (obj.type=="select-one") { 
		if (obj.options.length > 0) {
			var x=0;
			for (var i=0; i<obj.options.length; i++) {
				if (obj.options[i].defaultSelected) { x++; }
				}
			if (x==0 && obj.selectedIndex==0) { return false; }
			for (var i=0; i<obj.options.length; i++) {
				if (obj.options[i].selected != obj.options[i].defaultSelected) {
					return true;
					}
				}
			}
		return false;
		}
	if (obj.type=="select-multiple") {
		if (obj.options.length > 0) {
			for (var i=0; i<obj.options.length; i++) {
				if (obj.options[i].selected != obj.options[i].defaultSelected) {
					return true;
					}
				}
			}
		return false;
		}
	// return false for all other input types (button, image, etc)
	return false;
	}

//-------------------------------------------------------------------
// getInputValue(input_object)
//   Get the value of any form input field
//   Multiple-select fields are returned as comma-separated values
//   (Doesn't support input types: button,file,password,reset,submit)
//-------------------------------------------------------------------
function getInputValue(obj) {
	if ((typeof obj.type != "string") && (obj.length > 0) && (obj[0] != null) && (obj[0].type=="radio")) {
		for (var i=0; i<obj.length; i++) {
			if (obj[i].checked == true) { return obj[i].value; }
			}
		return "";
		}
	if (obj.type=="text") 
		{ return obj.value; }
	if (obj.type=="hidden") 
		{ return obj.value; }
	if (obj.type=="textarea") 
		{ return obj.value; }
	if (obj.type=="checkbox") {
		if (obj.checked == true) {
			return obj.value;
			}
		return "";
		}
	if (obj.type=="select-one") { 
		if (obj.options.length > 0) {
			return obj.options[obj.selectedIndex].value; 
			}
		else {
			return "";
			}
		}
	if (obj.type=="select-multiple") { 
		var val = "";
		for (var i=0; i<obj.options.length; i++) {
			if (obj.options[i].selected) {
				val = val + "" + obj.options[i].value + ",";
				}
			}
		if (val.length > 0) {
			val = val.substring(0,val.length-1); // remove trailing comma
			}
		return val;
		}
	return "";
	}

//-------------------------------------------------------------------
// getInputDefaultValue(input_object)
//   Get the default value of any form input field when it was created
//   Multiple-select fields are returned as comma-separated values
//   (Doesn't support input types: button,file,password,reset,submit)
//-------------------------------------------------------------------
function getInputDefaultValue(obj) {
	if ((typeof obj.type != "string") && (obj.length > 0) && (obj[0] != null) && (obj[0].type=="radio")) {
		for (var i=0; i<obj.length; i++) {
			if (obj[i].defaultChecked == true) { return obj[i].value; }
			}
		return "";
		}
	if (obj.type=="text") 
		{ return obj.defaultValue; }
	if (obj.type=="hidden") 
		{ return obj.defaultValue; }
	if (obj.type=="textarea") 
		{ return obj.defaultValue; }
	if (obj.type=="checkbox") {
		if (obj.defaultChecked == true) {
			return obj.value;
			}
		return "";
		}
	if (obj.type=="select-one") {
		if (obj.options.length > 0) {
			for (var i=0; i<obj.options.length; i++) {
				if (obj.options[i].defaultSelected) {
					return obj.options[i].value;
					}
				}
			}
		return "";
		}
	if (obj.type=="select-multiple") { 
		var val = "";
		for (var i=0; i<obj.options.length; i++) {
			if (obj.options[i].defaultSelected) {
				val = val + "" + obj.options[i].value + ",";
				}
			}
		if (val.length > 0) {
			val = val.substring(0,val.length-1); // remove trailing comma
			}
		return val;
		}
	return "";
	}
	
//-------------------------------------------------------------------
// setInputValue()
//   Set the value of any form field. In cases where no matching value
//   is available (select, radio, etc) then no option will be selected
//   (Doesn't support input types: button,file,password,reset,submit)
//-------------------------------------------------------------------
function setInputValue(obj,val) {
	if ((typeof obj.type != "string") && (obj.length > 0) && (obj[0] != null) && (obj[0].type=="radio")) {
		for (var i=0; i<obj.length; i++) {
			if (obj[i].value == val) { 
				obj[i].checked = true;
				}
			else {
				obj[i].checked = false;
				}
			}
		}
	if (obj.type=="text") 
		{ obj.value = val; }
	if (obj.type=="hidden") 
		{ obj.value = val; }
	if (obj.type=="textarea") 
		{ obj.value = val; }
	if (obj.type=="checkbox") {
		if (obj.value == val) { obj.checked = true; }
		else { obj.checked = false; }
		}
	if ((obj.type=="select-one") || (obj.type=="select-multiple")) {
		for (var i=0; i<obj.options.length; i++) {
			if (obj.options[i].value == val) {
				obj.options[i].selected = true;
				}
			else {
				obj.options[i].selected = false;
				}
			}
		}
	}
	
//-------------------------------------------------------------------
// isFormModified(form_object,hidden_fields,ignore_fields)
//   Check to see if anything in a form has been changed. By default
//   it will check all visible form elements and ignore all hidden 
//   fields. 
//   You can pass a comma-separated list of field names to check in
//   addition to visible fields (for hiddens, etc).
//   You can also pass a comma-separated list of field names to be
//   ignored in the check.
//-------------------------------------------------------------------
function isFormModified(theform, hidden_fields, ignore_fields) {
	if (hidden_fields == null) { hidden_fields = ""; }
	if (ignore_fields == null) { ignore_fields = ""; }
	
	var hiddenFields = new Object();
	var ignoreFields = new Object();
	var i,field;
	
	var hidden_fields_array = hidden_fields.split(',');
	for (i=0; i<hidden_fields_array.length; i++) {
		hiddenFields[Trim(hidden_fields_array[i])] = true;
		}
	var ignore_fields_array = ignore_fields.split(',');
	for (i=0; i<ignore_fields_array.length; i++) {
		ignoreFields[Trim(ignore_fields_array[i])] = true;
		}
	for (i=0; i<theform.elements.length; i++) {
		var changed = false;
		var name = theform.elements[i].name;
		if (!isBlank(name)) {
			var type = theform[name].type;
			if (!ignoreFields[name]) {
				if (type=="hidden" && hiddenFields[name]) {
					changed = isChanged(theform[name]);
					}
				else if (type=="hidden") {
					changed = false;
					}
				else {
					changed = isChanged(theform[name]);
					}
				}
			}
		if (changed) { 
			return true;
			}
		}
		return false;
	}

	// ------------------------------------------------------------------
// These functions use the same 'format' strings as the 
// java.text.SimpleDateFormat class, with minor exceptions.
// The format string consists of the following abbreviations:
// 
// Field        | Full Form          | Short Form
// -------------+--------------------+-----------------------
// Year         | yyyy (4 digits)    | yy (2 digits), y (2 or 4 digits)
// Month        | MMM (name or abbr.)| MM (2 digits), M (1 or 2 digits)
// Day of Month | dd (2 digits)      | d (1 or 2 digits)
// Hour (1-12)  | hh (2 digits)      | h (1 or 2 digits)
// Hour (0-23)  | HH (2 digits)      | H (1 or 2 digits)
// Hour (0-11)  | KK (2 digits)      | K (1 or 2 digits)
// Hour (1-24)  | kk (2 digits)      | k (1 or 2 digits)
// Minute       | mm (2 digits)      | m (1 or 2 digits)
// Second       | ss (2 digits)      | s (1 or 2 digits)
// AM/PM        | a                  |
//
// NOTE THE DIFFERENCE BETWEEN MM and mm! Month=MM, not mm!
// Examples:
//  "MMM d, y" matches: January 01, 2000
//                      Dec 1, 1900
//                      Nov 20, 00
//  "M/d/yy"   matches: 01/20/00
//                      9/2/00
//  "MMM dd, yyyy hh:mm:ssa" matches: "January 01, 2000 12:30:45AM"
// ------------------------------------------------------------------

var MONTH_NAMES=new Array('January','February','March','April','May','June','July','August','September','October','November','December','Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec');
function LZ(x) {return(x<0||x>9?"":"0")+x}

// ------------------------------------------------------------------
// isDate ( date_string, format_string )
// Returns true if date string matches format of format string and
// is a valid date. Else returns false.
// It is recommended that you trim whitespace around the value before
// passing it to this function, as whitespace is NOT ignored!
// ------------------------------------------------------------------
function isDate(val,format) {
	alert(val);
	var date=getDateFromFormat(val,format);
	if (date==0) { 
		alert("Please Enter Date in format mm/dd/yyyy");
		return false;
	}
	return true;
	}

// -------------------------------------------------------------------
// compareDates(date1,date1format,date2,date2format)
//   Compare two date strings to see which is greater.
//   Returns:
//   1 if date1 is greater than date2
//   0 if date2 is greater than date1 of if they are the same
//  -1 if either of the dates is in an invalid format
// -------------------------------------------------------------------
function compareDates(date1,dateformat1,date2,dateformat2) {
	var d1=getDateFromFormat(date1,dateformat1);
	var d2=getDateFromFormat(date2,dateformat2);
	//alert(d1);
	//alert(d2);
	if (d1==0 || d2==0) {
		return -1;
		}
	else if (d1 > d2) {
		return 1;
		}
	return 0;
	}

// ------------------------------------------------------------------
// formatDate (date_object, format)
// Returns a date in the output format specified.
// The format string uses the same abbreviations as in getDateFromFormat()
// ------------------------------------------------------------------
function formatDate(date,format) {
	format=format+"";
	var result="";
	var i_format=0;
	var c="";
	var token="";
	var y=date.getYear()+"";
	var M=date.getMonth()+1;
	var d=date.getDate();
	var H=date.getHours();
	var m=date.getMinutes();
	var s=date.getSeconds();
	var yyyy,yy,MMM,MM,dd,hh,h,mm,ss,ampm,HH,H,KK,K,kk,k;
	// Convert real date parts into formatted versions
	var value=new Object();
	if (y.length < 4) {y=""+(y-0+1900);}
	value["y"]=""+y;
	value["yyyy"]=y;
	value["yy"]=y.substring(2,4);
	value["M"]=M;
	value["MM"]=LZ(M);
	value["MMM"]=MONTH_NAMES[M-1];
	value["d"]=d;
	value["dd"]=LZ(d);
	value["H"]=H;
	value["HH"]=LZ(H);
	if (H==0){value["h"]=12;}
	else if (H>12){value["h"]=H-12;}
	else {value["h"]=H;}
	value["hh"]=LZ(value["h"]);
	if (H>11){value["K"]=H-12;} else {value["K"]=H;}
	value["k"]=H+1;
	value["KK"]=LZ(value["K"]);
	value["kk"]=LZ(value["k"]);
	if (H > 11) { value["a"]="PM"; }
	else { value["a"]="AM"; }
	value["m"]=m;
	value["mm"]=LZ(m);
	value["s"]=s;
	value["ss"]=LZ(s);
	while (i_format < format.length) {
		c=format.charAt(i_format);
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		if (value[token] != null) { result=result + value[token]; }
		else { result=result + token; }
		}
	return result;
	}
	
// ------------------------------------------------------------------
// Utility functions for parsing in getDateFromFormat()
// ------------------------------------------------------------------
function _isInteger(val) {
	var digits="1234567890";
	for (var i=0; i < val.length; i++) {
		if (digits.indexOf(val.charAt(i))==-1) { return false; }
		}
	return true;
	}
function _getInt(str,i,minlength,maxlength) {
	for (var x=maxlength; x>=minlength; x--) {
		var token=str.substring(i,i+x);
		if (token.length < minlength) { return null; }
		if (_isInteger(token)) { return token; }
		}
	return null;
	}
	
// ------------------------------------------------------------------
// getDateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// ------------------------------------------------------------------
function getDateFromFormat(val,format) {
	val=val+"";
	format=format+"";
	//alert("format="+format)
	var i_val=0;
	var i_format=0;
	var c="";
	var token="";
	var token2="";
	var x,y;
	var now=new Date();
	var year=now.getYear();
	var month=now.getMonth()+1;
	var date=now.getDate();
	var hh=now.getHours();
	var mm=now.getMinutes();
	var ss=now.getSeconds();
	var ampm="";
	
	while (i_format < format.length) {
		// Get next token from format string
		c=format.charAt(i_format);
		//alert("c="+c)
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		// Extract contents of value based on format token
		if (token=="yyyy" || token=="yy" || token=="y") {
			if (token=="yyyy") { x=4;y=4; }
			if (token=="yy")   { x=2;y=2; }
			if (token=="y")    { x=2;y=4; }
			year=_getInt(val,i_val,x,y);
			if (year==null) { return 0; }
			i_val += year.length;
			if (year.length==2) {
				if (year > 70) { year=1900+(year-0); }
				else { year=2000+(year-0); }
				}
			}
		else if (token=="MMM"){
			month=0;
			for (var i=0; i<MONTH_NAMES.length; i++) {
				var month_name=MONTH_NAMES[i];
				if (val.substring(i_val,i_val+month_name.length).toLowerCase()==month_name.toLowerCase()) {
					month=i+1;
					if (month>12) { month -= 12; }
					i_val += month_name.length;
					break;
					}
				}
			if ((month < 1)||(month>12)){return 0;}
			}
		else if (token=="MM"||token=="M") {
			month=_getInt(val,i_val,token.length,2);
			if(month==null||(month<1)||(month>12)){return 0;}
			i_val+=month.length;}
		else if (token=="dd"||token=="d") {
			date=_getInt(val,i_val,token.length,2);
			if(date==null||(date<1)||(date>31)){return 0;}
			i_val+=date.length;}
		else if (token=="hh"||token=="h") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>12)){return 0;}
			i_val+=hh.length;}
		else if (token=="HH"||token=="H") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>23)){return 0;}
			i_val+=hh.length;}
		else if (token=="KK"||token=="K") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>11)){return 0;}
			i_val+=hh.length;}
		else if (token=="kk"||token=="k") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>24)){return 0;}
			i_val+=hh.length;hh--;}
		else if (token=="mm"||token=="m") {
			mm=_getInt(val,i_val,token.length,2);
			if(mm==null||(mm<0)||(mm>59)){return 0;}
			i_val+=mm.length;}
		else if (token=="ss"||token=="s") {
			ss=_getInt(val,i_val,token.length,2);
			if(ss==null||(ss<0)||(ss>59)){return 0;}
			i_val+=ss.length;}
		else if (token=="a") {
			if (val.substring(i_val,i_val+2).toLowerCase()=="am") {ampm="AM";}
			else if (val.substring(i_val,i_val+2).toLowerCase()=="pm") {ampm="PM";}
			else {return 0;}
			i_val+=2;}
		else {
			if (val.substring(i_val,i_val+token.length)!=token) {return 0;}
			else {i_val+=token.length;}
			}
		}
	// If there are any trailing characters left in the value, it doesn't match
	if (i_val != val.length) { return 0; }
	// Is date valid for month?
	if (month==2) {
		// Check for leap year
		if ( ( (year%4==0)&&(year%100 != 0) ) || (year%400==0) ) { // leap year
			if (date > 29){ return false; }
			}
		else { if (date > 28) { return false; } }
		}
	if ((month==4)||(month==6)||(month==9)||(month==11)) {
		if (date > 30) { return false; }
		}
	// Correct hours value
	if (hh<12 && ampm=="PM") { hh+=12; }
	else if (hh>11 && ampm=="AM") { hh-=12; }
	var newdate=new Date(year,month-1,date,hh,mm,ss);
	return newdate.getTime();
	}

function SSNValidation(ssn) {
var matchArr = ssn.match(/^(\d{3})-?\d{2}-?\d{4}$/);
var numDashes = ssn.split('-').length - 1;
if (matchArr == null || numDashes == 1) {
alert('Invalid SSN. Must be 9 digits or in the form NNN-NN-NNNN.');
msg = "does not appear to be valid";
}
else 
if (parseInt(matchArr[1],10)==0) {
alert("Invalid SSN: SSN's can't start with 000.");
msg = "does not appear to be valid";
}
else {
msg = "appears to be valid";
alert(ssn + "\r\n\r\n" + msg + " Social Security Number.");
   }
}


	function ValidatePayment(a){
	
		if (isBlank(a.amount.value)) {
			alert("Please Select Amount");
			//a.amount.select();
			a.amount.focus();
			return false;
		}
		else{
			if(isBlank(a.Status.value)){
				alert("Please Change Status");
				//a.Status.select();
				a.Status.focus();
				return false;
			}
			else{
				if (isBlank(a.PaymentType.value)){
					alert("Please Select Payment Type");
					//a.PaymentType.select();
					a.PaymentType.focus();
					return false;
				}
				else{
					if (!isDate(a.ADate.value,"mm/dd/yyyy")){
						//a.ADate.select();
						a.ADate.focus();
						return false;
					}
					else{
						if(!isNumeric(a.amount.value)){
							alert("Please Enter Valid Amount");
							a.ADate.select();
							a.amount.focus();
							return false;
						}
						else{
							alert("hello");
							return ConfirmSub();
						}
					}
				}
			}
		}
	}	

function subForm(){

	if(ValidatePayment(document.frmPayment)){
		document.frmPayment.submit();
	}
	else{
	}
}
	
function ConfirmSub(){
		var x=window.confirm("Do you wish to submit this transaction?")
		if (x){	
			return true;
		}
		else{
			return false;
		}
}


function currencyFormat(fld, milSep, decSep, e) {
var sep = 0;
var key = '';
var i = j = 0;
var len = len2 = 0;
var strCheck = '0123456789';
var aux = aux2 = '';
var whichCode = (window.Event) ? e.which : e.keyCode;
if (whichCode == 13) return true;  // Enter
key = String.fromCharCode(whichCode);  // Get key value from key code
if (strCheck.indexOf(key) == -1) return false;  // Not a valid key
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
}
return false;
}


function checkPhone(strng) {
var stripped = strng.replace(/[\(\)\.\-\ ]/g, '');
	//strip out acceptable non-numeric characters
	if (isNaN(parseInt(stripped))) {
   		alert("The phone number contains illegal characters.");
		return false
	}	
	if (!(stripped.length == 10)) {
		alert("The phone number is the wrong length.Make sure you included an area code.");
		return false;
	}
	return true;
}

function checkZipInsurer(zip){

if(zip.value.length > 0){
   if (isInteger(zip.value)){
      document.frmInsurer.submit();
	  return true;
   }
   else{
   		alert("Zip Code is invalid");
		return false;
  }
}
else{
	alert("Please Enter five digit zip code;")
	return false;
	}
}

function checkZipInsurer2(zip){
//alert("hi");

if(zip.value.length > 0){
   if (isInteger(zip.value)){
      document.frmInsurer.submit();
	  return true;
   }
   else{
   		alert("Zip Code is invalid");
		return false;
  }
}
else{
	alert("Please Enter five digit zip code;")
	return false;
	}
}

function checkZipClient(zip){

if(zip.value.length > 0){
   if (isInteger(zip.value)){
      document.frmClient.submit();  
	  return true;
   }
   else{
   		alert("Zip Code is invalid");
		return false;
  }
}
else{
	alert("Please Enter five digit zip code;")
	return false;
	}
}

function checkZip2Client(zip){

if(zip.value.length > 0){
   if (isInteger(zip.value)){
      document.frmClient.submit();  
	  return true;
   }
   else{
   		alert("Zip Code is invalid");
		return false;
  }
}
else{
	alert("Please Enter five digit zip code;")
	return false;
	}
}

function checkZipAtt(zip){

if(zip.value.length > 0){
   if (isInteger(zip.value)){
		document.frmAttorney.method = "post"
		document.frmAttorney.action = "attorneyEntry.asp?qry=addattorney&screen=dentry"
		document.frmAttorney.submit(); 
		 
		return true;
   }
   else{
   		alert("Zip Code is invalid");
		return false;
  }
}
else{
	alert("Please Enter five digit zip code;")
	return false;
	}
}



function checkZipForAddClaim(zip){

if(zip.name == "Client_Zip_Injured"){

	window.document.frm2AddClaim.chk.value = 1;

}
if(zip.name == "Client_Zip_Insured"){

	window.document.frm2AddClaim.chk.value = 2;

}
if(zip.name == "accZip"){

	window.document.frm2AddClaim.chk.value = 3;

}
if(zip.value.length > 0){
   if (isInteger(zip.value)){
	
	document.frm2AddClaim.method = "post"
	document.frm2AddClaim.action = "dataEntry.asp?qry=addclaim&screen=dentry"
	document.frm2AddClaim.submit(); 

            
	  return true;
   }
   else{
   		alert("Zip Code is invalid");
		return false;
  }
}
else{
	alert("Please Enter five digit zip code;")
	return false;
	}
}

function focusCheckBox(){

	window.document.frm2AddClaim.sameAsInjured.focus();
}
function getkey(e)
{
if (window.event){
   //alert(window.event.keyCode);
   if(window.event.keyCode==13){
   	//alert("enter");
	return false;
   }else{
   	return true;
   }
  	
}
else if (e){
		if (e.which==13){
   			//alert("from in here"+e);
   			return false;
		}
	}
else
   return false;
}


function goodchars(e, goods){

	var key, keychar;
	key = getkey(e);
	alert(key);

	if (key == null) return true;

		// get character
	keychar = String.fromCharCode(key);
	keychar = keychar.toLowerCase();
	goods = goods.toLowerCase();
	alert(goods);

	// check goodkeys
	if (goods.indexOf(keychar) != -1){
		//return true;
		return false;
	}

	// control keys
	if ( key==null || key==0 || key==8 || key==9 || key==13 || key==27 ){
   		return false;
	}

	// else return false
	return false;
}

function ValClient(){
		var a,b;
		a=document.frmClient;


		if (isEmpty(a.Client_Id.value)){
			alert("Please Enter a Client ID.");
			a.Client_Id.focus();
			return false;
		} 
		if (isEmpty(a.Client_Name.value)){
			alert("Please Enter a Client Name.");
			a.Client_Name.focus();
			return false;
		}
		if (isEmpty(a.Client_Address.value)){
			alert("Please Enter a Client Address.");
			a.Client_Address.focus();
			return false;
		}
		
		if (isEmpty(a.Client_Zip.value)){
			alert("Please Enter a Client Zip");
			a.Client_Zip.focus();
			return false;
		}
		/*if (isEmpty(a.Client_City.value)){
			alert("Please Enter a Client City");
			a.Client_City.focus();
			return false;
		}
		if (isEmpty(a.Client_State.value)){
			alert("Please Enter a Client State");
			a.Client_State.focus();
			return false;
		}*/
		document.frmClient.action="collector.asp?qry=ac&screen=dentry&uid=<%=Request.QueryString("uid")%>&addC=true" 
		document.frmClient.method="post"
		document.frmClient.submit();
		return true;
	}

	
function imagevalue(sel)
{
	divvalue = sel.selectedIndex;
	//showpopwindow = (sel(divvalue).value);
	alert(divvalue);
}


var savepush = false;

function previewImage(htmlEle,imgValue)
{
	var swapImage =  imgValue;
	ele = eval("document.all." + htmlEle);
	strHTML = "<img id = quickView src = '"+swapImage+"'>";
	ele.innerHTML = strHTML;
}

function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}-
function updateHidden(blnValue,formName,hiddenField)
{
	frmField = eval("document."+formName+"."+hiddenField);
	frmField.value = blnValue;
}

function swapDiv(divToShow)
{
	if (divToShow == "newForm")
	{
		document.all.editForm.className = "hideMe"
		document.all.newForm.className = "showMe"
		document.images.imgToggle.src = "images/new.jpg"
		document.images.imgClick.click()
	}
	else
	{
		document.all.newForm.className = "hideMe"
		document.all.editForm.className = "showMe"
		document.images.imgToggle.src = "images/edit.jpg"
		document.images.imgClick.click()
	}
}
function popWindow(url,page)
{
	if (page == "uploader")
	{
		var new_window = window.open(url, "newImg","width=400,height=170,resizable=no,scrollbars=yes");
	}
}

function ConfirmUpdate(value)
{
	if (value == "editInsurer")
	{
		if (confirm("Are you sure you would like to UPDATE this Insurer Record?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	
	if (value == "addMatter")
	{
		if (confirm("Are you sure you would like to ADD this Matter?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	if (value == "updateMatter")
	{
		if (confirm("Are you sure you would like to UPDATE this Matter?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	
	if (value == "DeleteMatter")
	{
		if (confirm("Are you sure you would like to Delete this Matter? It CANNOT be UNDONE!"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	
	if (value == "updateFirm")
	{
		if (confirm("Are you sure you would like to UPDATE this Firm?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	if (value == "addInsurer")
	{
		if (confirm("Are you sure you would like to ADD this Insurer Record?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	if (value == "addNewUser")
	{
		if (confirm("Are you sure you would like to ADD this New User?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	if (value == "addNewDoc")
	{
		if (confirm("Are you sure you would like to ADD this New Document?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	
	if (value == "addFirm")
	{
		if (confirm("Are you sure you would like to ADD this New Firm?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	
	if (value == "addClaimRep")
	{
		if (confirm("Are you sure you would like to ADD this New Claim Rep.?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
	
	if (value == "updateClaimRep")
	{
		if (confirm("Are you sure you would like to Update this Claim Rep.?"))
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}
}
</script>

<%
'-----------------------Calender function start ------------------------------------------------------
%>

<style type=text/css>
TABLE.cal 	{ COLOR: #FFFFFF; WIDTH: 380px; HEIGHT:30px;Border-color:black;}
TD.cal 		{ FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: BOLD ;TEXT-ALIGN: center;Border-color:black; }
INPUT.cal 	{ COLOR: #FFFFFF; BACKGROUND:#6699cc; BORDER:1; WIDTH: 20 }
A		{ TEXT-DECORATION: none; }
A.cal:link 	{ COLOR: black; }
A.cal:visited 	{ COLOR: black; }
A.cal:active 	{ COLOR: black; }
A.cal:hover 	{ COLOR: black; }
A.tdy:link 	{ COLOR: yellow; }
A.tdy:visited 	{ COLOR: white; }
A.tdy:active 	{ COLOR: yellow; }
A.tdy:hover 	{ COLOR: gray; }
A.tdz:link 	{ COLOR: black; FONT-WEIGHT: bold; }
A.tdz:visited 	{ COLOR: black; FONT-WEIGHT: bold; }
#ppcalendar 	{ LEFT: 550px; POSITION: absolute; TOP:990px }
#pcmonth 	{ LEFT: 480px; POSITION: absolute; TOP: 200px }
</style>

<SCRIPT language=vbscript>
function mlookup(mnt)
select case mnt
	case  1 mlookup="J A N"
	case  2 mlookup="F E B"
	case  3 mlookup="M A R"
	case  4 mlookup="A P R"
	case  5 mlookup="M A Y"
	case  6 mlookup="J U N"
	case  7 mlookup="J U L"	
	case  8 mlookup="A U G"
	case  9 mlookup="S E P"
	case 10 mlookup="O C T"
	case 11 mlookup="N O V"
	case 12 mlookup="D E C"
end select
end function

function mlookup2(mnt)
select case mnt
	case  1 mlookup2="January"
	case  2 mlookup2="February"
	case  3 mlookup2="March"
	case  4 mlookup2="April"
	case  5 mlookup2="May"
	case  6 mlookup2="June"
	case  7 mlookup2="July"	
	case  8 mlookup2="August"
	case  9 mlookup2="September"
	case 10 mlookup2="October"
	case 11 mlookup2="November"
	case 12 mlookup2="December"
end select
end function

function yformat(yyr)
	for a=1 to len(yyr)
		b=b+mid(yyr,a,1)+" "
	next
	yformat=mid(b,1,len(b)-1)
end function

function dlookup(mm, yy)
	dlookup=31
	if mm<8 and mm/2=fix(mm/2) then dlookup=30
	if mm>7 and mm/2<>fix(mm/2) then dlookup=30
	
	if mm=2 then
		lpyr=leapyear(yy)
		if lpyr=1 then dlookup=29
		if lpyr=0 then dlookup=28
	end if
end function

function leapyear(yyr)
leapyear=0
	if yyr/4=fix(yyr/4) then leapyear=1
	if yyr/100=fix(yyr/100) then leapyear=0
	if yyr/400=fix(yyr/400) then leapyear=1
end function

function namedate2(mmddyy)
	ddate=mmddyy
	yys=mid(ddate,1,4)
	mms=mid(ddate,5,2)
	dds=mid(ddate,7,2) * 1
	ndate=mlookup2(mms) & " " & dds & ", " & yys
	namedate2=ndate
end function

sub init()
	document.all.mm.value=month(now())
	document.all.yy.value=year(now())
	document.all.dd.value=day(now())
	call pcalendar()
end sub

sub pcalendar()
	mnt=document.all.mm.value
	yyr=document.all.yy.value
	ddy=document.all.dd.value
	fday=weekday(mnt & "/1/" & yyr)
	lday=dlookup(mnt, yyr)

	innerHTML="<a class='tdy' href='javascript: showPcmonth()'>&nbsp; " & mlookup(mnt) &  "&nbsp;&nbsp; " & yformat(yyr) & "&nbsp;</a>"
	document.all.my.innerHTML=innerHTML

	innerHTML="<a class='tdy' href='javascript: hidePcmonth()'>&nbsp;" & yformat(yyr) & "&nbsp;</a>"
	document.all.y.innerHTML=innerHTML

	for a=1 to 6				' six weeks
		for b=1 to 7			' seven days
			c=c+1
			if c>=fday then
				d=d+1
			else 	
				d=0
			end if
			targetId=a & b
			if d<=lday then 
				if d=day(now()) and int(mnt)=month(now()) and int(yyr)=year(now()) then
					innerHTML="<a class='tdz' "
				else
					innerHTML="<a class='cal' "
				end if
				innerHTML=innerHTML & "href='javascript: outday(" & d & ")'>&nbsp;" & d & "&nbsp;</a>"
				if d=0 then innerHTML=""
				document.all(targetId).innerHTML=innerHTML
			else
				document.all(targetId).innerHTML=""
			end if
		next
		fday=1
	next	
end sub

sub prevyr()
	showpcmonth()
	document.all.yy.value=document.all.yy.value-1
	call pcalendar()
end sub

sub nextyr()
	showpcmonth()
	document.all.yy.value=document.all.yy.value+1
	call pcalendar()
end sub

sub prevmo()
	showpcal()
	document.all.mm.value=document.all.mm.value-1
	if document.all.mm.value=0 then 
		document.all.mm.value=12
		document.all.yy.value=document.all.yy.value-1
	end if
	call pcalendar()
end sub

sub nextmo()
	showpcal()
	document.all.mm.value=document.all.mm.value+1
	if document.all.mm.value=13 then 
		document.all.mm.value=1
		document.all.yy.value=document.all.yy.value+1
	end if	
	call pcalendar()
end sub

sub outday(dd)
	mms=document.all.mm.value
	yys=document.all.yy.value
	dds=document.all.dd.value
	'dout=yys & right("0" & mms, 2) & right ("0" & dd,2)
	dout= mms & "/" & dd & "/" & yys
	dtgo=document.all.cinput.value
	document.all(dtgo).value=dout	
	call hidePCal()
end sub

sub showPCalE(ddd) 
	ddate=document.all(ddd).value
	yys=year(ddate)
	mms=month(ddate)
	dds=day(ddate)

	if yys-document.all("yy").value<>0 or mms-document.all("mm").value<>0 then
		document.all("yy").value=yys
		document.all("mm").value=mms
		pcalendar()	
	end if

	showPCal()
	document.all("cinput").value=ddd
end sub


sub cmonth(mnt)
	document.all.mm.value=mnt
	hidePCmonth()
	call pcalendar()	
end sub

sub showPCmonth()
	document.all("pcmonth").style.display=""
end sub

sub hidePCmonth()
	showpcal()
	document.all("pcmonth").style.display="none"
end sub

sub showPCal()
	document.all("ppcalendar").style.display=""
end sub


sub hidePCal()	
	document.all("ppcalendar").style.display="none"
	document.all("pcmonth").style.display="none"	
end sub

</SCRIPT>

<DIV id=ppcalendar style="display: none">
<TABLE class="cal" bgColor=#0054A9 cellPadding=0 cellSpacing=0 height=280 ID="Table1">
  <TBODY>
  <TR>
    <TD class="cal" width=20><input class="cal" type="button" value="<" onclick="javascript: prevmo()" ID="Button1" NAME="Button1"></TD>
    <TD class="cal" id=my width=160>Month Year </TD>
    <TD class="cal" width=20><input class="cal" type="button" value=">" onclick="javascript: nextmo()" ID="Button2" NAME="Button2"></TD>
</TR></TBODY></TABLE>
<TABLE class="cal" bgColor="0054A9" cellPadding=0 cellSpacing=0 ID="Table2">
  <TBODY>
  <TR>
    <TD class="cal">Su</TD>
    <TD class="cal">Mo</TD>
    <TD class="cal">Tu</TD>
    <TD class="cal">We</TD>
    <TD class="cal">Th</TD>
    <TD class="cal">Fr</TD>
    <TD class="cal">Sa</TD></TR>
  <TR>
    <TD></TD></TR></TBODY></TABLE>
<TABLE class="cal" bgColor=#6699cc cellPadding=0 cellSpacing=0 height=220 ID="Table3">
  <TBODY>
  <TR>
    <TD class="cal" id=11>&nbsp;</TD>
    <TD class="cal" id=12>&nbsp;</TD>
    <TD class="cal" id=13>&nbsp;</TD>
    <TD class="cal" id=14>&nbsp;</TD>
    <TD class="cal" id=15>&nbsp;</TD>
    <TD class="cal" id=16>&nbsp;</TD>
    <TD class="cal" id=17>&nbsp;</TD></TR>
  <TR>
    <TD class="cal" id=21>&nbsp;</TD>
    <TD class="cal" id=22>&nbsp;</TD>
    <TD class="cal" id=23>&nbsp;</TD>
    <TD class="cal" id=24>&nbsp;</TD>
    <TD class="cal" id=25>&nbsp;</TD>
    <TD class="cal" id=26>&nbsp;</TD>
    <TD class="cal" id=27>&nbsp;</TD></TR>
  <TR>
    <TD class="cal" id=31>&nbsp;</TD>
    <TD class="cal" id=32>&nbsp;</TD>
    <TD class="cal" id=33>&nbsp;</TD>
    <TD class="cal" id=34>&nbsp;</TD>
    <TD class="cal" id=35>&nbsp;</TD>
    <TD class="cal" id=36>&nbsp;</TD>
    <TD class="cal" id=37>&nbsp;</TD></TR>
  <TR>
    <TD class="cal" id=41>&nbsp;</TD>
    <TD class="cal" id=42>&nbsp;</TD>
    <TD class="cal" id=43>&nbsp;</TD>
    <TD class="cal" id=44>&nbsp;</TD>
    <TD class="cal" id=45>&nbsp;</TD>
    <TD class="cal" id=46>&nbsp;</TD>
    <TD class="cal" id=47>&nbsp;</TD></TR>
  <TR>
    <TD class="cal" id=51>&nbsp;</TD>
    <TD class="cal" id=52>&nbsp;</TD>
    <TD class="cal" id=53>&nbsp;</TD>
    <TD class="cal" id=54>&nbsp;</TD>
    <TD class="cal" id=55>&nbsp;</TD>
    <TD class="cal" id=56>&nbsp;</TD>
    <TD class="cal" id=57>&nbsp;</TD></TR>
  <TR>
    <TD class="cal" id=61>&nbsp;</TD>
    <TD class="cal" id=62>&nbsp;</TD>
    <TD class="cal" id=63>&nbsp;</TD>
    <TD class="cal" id=64>&nbsp;</TD>
    <TD class="cal" id=65>&nbsp;</TD>
    <TD class="cal" id=66>&nbsp;</TD>
    <TD class="cal" id=67>&nbsp;</TD></TR></TBODY></TABLE>
</DIV>

<DIV id=pcmonth style="DISPLAY: none">
<TABLE bgColor=#EDF3F8 cellPadding=0 cellSpacing=0 height=18 ID="Table4">
  <TBODY>
  <TR>
    <TD class="cal"  width=20><input class="cal" type="button" value="<" onclick="javascript: prevyr()" ID="Button3" NAME="Button3"></TD>
    <TD class="cal"  id=y width=160>Month Year </TD>
    <TD class="cal"  width=20><input class="cal" type="button" value=">" onclick="javascript: nextyr()" ID="Button4" NAME="Button4"></TD>
  </TD></TR></TBODY></TABLE>
<TABLE class="cal" bgColor=#0054A9 cellPadding=0 cellSpacing=0 ID="Table5">
  <TBODY>
  <TR>
    <TD class="cal">Select a Month</TD></TR>
  <TR>
    <TD></TD></TR></TBODY></TABLE>
<TABLE class="cal" cellPadding=0 cellSpacing=0 height=120 ID="Table6">
  <TBODY>
  <TR>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(1)">&nbsp;Jan&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(2)">&nbsp;Feb&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(3)">&nbsp;Mar&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(4)">&nbsp;Apr&nbsp;</A></TD></TR>
  <TR>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(5)">&nbsp;May&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(6)">&nbsp;Jun&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(7)">&nbsp;Jul&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(8)">&nbsp;Aug&nbsp;</A></TD></TR>
  <TR>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(9)">&nbsp;Sep&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(10)">&nbsp;Oct&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(11)">&nbsp;Nov&nbsp;</A></TD>
    <TD class="cal" ><A class="cal" href="javascript: cmonth(12)">&nbsp;Dec&nbsp;</A></TD></TR></TBODY>
</TABLE>
</DIV>

<INPUT name=mm type=hidden value="3" ID="Hidden1"> 
<INPUT name=dd type=hidden value="10" ID="Hidden2"> 
<INPUT name=yy type=hidden value="1977" ID="Hidden3">
<INPUT name=cinput type=hidden ID="Hidden4">

<script language=vbscript>
sub ck(xt)
k=document.all(xt).value
document.all(xt).value=formatdatetime("11/12/1999",2)
end sub
</script>



<script language=javascript>
<!--
function lookupClient(){

	if(window.document.frmClient.Client_Id.value == ""){
		alert('Client ID is required to add a client.');
		window.document.frmClient.Client_Id.focus();
		return false;
	}
	else{
		document.frmClient.submit();
		return true;
	}

}

//-->
</script>
<script language = javaScript>
function toggle(e,arrowName)
	{
	ele = eval("document.all."+e)
	if (ele.style.display == 'none')
		{
		ele.style.display = 'inline'
		document.images[arrowName].src = "images/downArrow.jpg";
		}
		else
			{
			ele.style.display = 'none'
			document.images[arrowName].src = "images/leftArrow.jpg";
			}
	}
function fieldValidator(){
	if(window.frm2AddClaim.elements[0].value == ""){
		alert('Matter key is required.');
		window.frm2AddClaim.elements[0].focus();
		return false;
	}
	if(window.frm2AddClaim.elements[1].value == ""){
		alert('Client reference is required.');
		window.frm2AddClaim.elements[1].focus();
		return false;
	}
	if(window.frm2AddClaim.elements[2].value == ""){
		alert('Date of assigment is required.');
		window.frm2AddClaim.elements[2].focus();
		return false;
	}
	if(window.frm2AddClaim.elements[3].value == ""){
		alert('Claim type is required.');
		window.frm2AddClaim.elements[3].focus();
		return false;
	}
	if(window.frm2AddClaim.feetype_id.options[0].selected == true){
		alert('Fee type is required.');
		window.frm2AddClaim.elements[5].focus();
		return false;
	}
	if(window.frm2AddClaim.elements[6].value == ""){
		alert('County is required.');
		window.frm2AddClaim.elements[6].focus();
		return false;
	}
	else{
		this.frm2AddClaim.submit();
		return true;
	}
}
					
function autoComplete (field, select, property, forcematch){
	var found = false;
	for (var i = 0; i < select.options.length; i++) {
		if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) {
			found=true; 
			break;
		}
	}
	if (found) { 
		select.selectedIndex = i; 
	}
	else{
		select.selectedIndex = -1; 
	}
	if (field.createTextRange) {
		if (forcematch && !found) {
			field.value=field.value.substring(0,field.value.length-1); 
			return;
		}
		var cursorKeys ="8;46;37;38;39;40;33;34;35;36;45;";
		if (cursorKeys.indexOf(event.keyCode+";") == -1) {
			var r1 = field.createTextRange();
			var oldValue = r1.text;
			var newValue = found ? select.options[i][property] : oldValue;
				if (newValue != field.value) {
					field.value = newValue;
					var rNew = field.createTextRange();
					rNew.moveStart('character', oldValue.length) ;
					rNew.select();
				}
		}
	}

}

function sessExpire(){
	window.location.replace('login.asp?qry=lo');
}

function globalSearchClick(){
	if(window.document.globalsearching.keyID.value == ""){
		alert("Please enter [matter key] or [matter id] to search.");
		window.document.globalsearching.keyID.focus();
		return false;
	}
	else{
		CallJS('Demo()');
		this.globalsearching.submit();
		return true;
	}
}
</script>

<style>
.hide { position:absolute; visibility:hidden; }
.show { position:absolute; visibility:visible; }
</style>

<SCRIPT LANGUAGE="JavaScript">

<!-- This script and many more are available free online at -->
<!-- The JavaScript Source!! http://javascript.internet.com -->

<!-- Begin
var _progressBar = new String("");
var _progressEnd = 10;
var _progressAt = 0;
var _progressWidth = 50;	// Display width of progress bar

// Create and display the progress dialog.
// end: The number of steps to completion
function ProgressCreate(end) {
	// Initialize state variables
	_progressEnd = end;
	_progressAt = 0;

	// Move layer to center of window to show
	if (document.all) {	// Internet Explorer
		progress.className = 'show';
		progress.style.left = (document.body.clientWidth/2) - (progress.offsetWidth/2);
		progress.style.top = (document.body.clientHeight/2) - (progress.offsetHeight/2);
	} else if (document.layers) {	// Netscape
		document.progress.visibility = true;
		document.progress.left = (window.innerWidth/2) - 100;
		document.progress.top = (window.innerHeight/2) - 40;
	}

	ProgressUpdate();	// Initialize bar
}

// Hide the progress layer
function ProgressDestroy() {
	// Move off screen to hide
	if (document.all) {	// Internet Explorer
		progress.className = 'hide';
	} else if (document.layers) {	// Netscape
		document.progress.visibility = false;
	}
}

// Increment the progress dialog one step
function ProgressStepIt() {
	_progressAt++;
	if(_progressAt > _progressEnd) _progressAt = _progressAt % _progressEnd;
	ProgressUpdate();
}

// Update the progress dialog with the current state
function ProgressUpdate() {
	var n = (_progressWidth / _progressEnd) * _progressAt;
	if (document.all) {	// Internet Explorer
		var bar = dialog.bar;
 	} else if (document.layers) {	// Netscape
		var bar = document.layers["progress"].document.forms["dialog"].bar;
		n = n * 0.55;	// characters are larger
	}
	var temp = _progressBar.substring(0, n);
	bar.value = temp;
}

// Demonstrate a use of the progress dialog.
function Demo() {
	ProgressCreate(10);
	window.setTimeout("Click()", 100);	
}

function Click() {
	if(_progressAt >= _progressEnd) {
		ProgressDestroy();		
		return;
	}
	ProgressStepIt();
	window.setTimeout("Click()", 150);
	
}

function CallJS(jsStr) { //v2.0
  return eval(jsStr)
}
function x() { //v2.0
	
	
 
  
}
//  End -->
</script>

</HEAD>

<!-- STEP TWO: Copy this code into the BODY of your HTML document  -->

<BODY>

<SCRIPT LANGUAGE="JavaScript">

<!-- This script and many more are available free online at -->
<!-- The JavaScript Source!! http://javascript.internet.com -->

<!-- Begin
// Create layer for progress dialog
document.write("<span id=\"progress\" class=\"hide\">");
	document.write("<FORM name=dialog>");
	document.write("<TABLE border=2  bgcolor=\"#00287B\">");
	document.write("<TR><TD class=\"blueA\" ALIGN=\"center\">");
	document.write("Loading...<BR>");
	document.write("<input type=text name=\"bar\" size=\"" + _progressWidth/2 + "\"");
	if(document.all) 	// Microsoft
		document.write(" bar.style=\"color:navy;\">");
	else	// Netscape
		document.write(">");
	document.write("</TD></TR>");
	document.write("</TABLE>");
	document.write("</FORM>");
document.write("</span>");
ProgressDestroy();	// Hides
//  End -->

function checkFirmEntry(){
	//alert(window.document.frmAttorney.selFirms.value);
	if(window.document.frmAttorney.selFirms.value =="0"){
		alert('Please select a firm to associate with an attorney.');
		window.document.frmAttorney.selFirms.focus();
		return false;
	}
	if(window.document.frmAttorney.selFirms.value == ""){
		alert('Please select a firm to associate with an attorney.');
		window.document.frmAttorney.selFirms.focus();
		return false;
	}
	else {
		return true;
	}


}


</script>



<%
Function MyFormatDate(pvarDate, pstrFormat)
    Dim strOut
    Dim strFormatIn
    Dim strFormatIn2
    Dim varDateTemp
    Dim intHour

    strFormatIn = LCase(pstrFormat)
    Do Until Len(strFormatIn) = 0
        If Left(strFormatIn, 5) = "am/pm" Then
            varDateTemp = DateAdd("h", 12, DateSerial(Year(pvarDate),Month(pvarDate), Day(pvarDate)))
            strFormatIn2 = Mid(pstrFormat, Len(pstrFormat) - Len(strFormatIn) + 1)
            If pvarDate >= varDateTemp Then
                If Left(strFormatIn2, 1) = "A" Then
                    strOut = strOut & "PM"
                Else
                    strOut = strOut & "pm"
                End If
            Else
                If Left(strFormatIn2, 1) = "A" Then
                    strOut = strOut & "AM"
                Else
                    strOut = strOut & "am"
                End If
            End If
            strFormatIn = Mid(strFormatIn, 6)
        ElseIf Left(strFormatIn, 4) = "dddd" Then
            strOut = strOut & WeekdayName(Weekday(pvarDate, 0), False, 0)
            strFormatIn = Mid(strFormatIn, 5)
        ElseIf Left(strFormatIn, 3) = "ddd" Then
            strOut = strOut & WeekdayName(Weekday(pvarDate, 0), True, 0)
            strFormatIn = Mid(strFormatIn, 4)
        ElseIf Left(strFormatIn, 2) = "dd" Then
            strOut = strOut & Right("0" & Day(pvarDate), 2)
            strFormatIn = Mid(strFormatIn, 3)
        ElseIf Left(strFormatIn, 1) = "d" Then
            strOut = strOut & Day(pvarDate)
            strFormatIn = Mid(strFormatIn, 2)
        ElseIf Left(strFormatIn, 4) = "mmmm" Then
            strOut = strOut & MonthName(Month(pvarDate), False)
            strFormatIn = Mid(strFormatIn, 5)
        ElseIf Left(strFormatIn, 3) = "mmm" Then
            strOut = strOut & MonthName(Month(pvarDate), True)
            strFormatIn = Mid(strFormatIn, 4)
        ElseIf Left(strFormatIn, 2) = "mm" Then
            strOut = strOut & Right("0" & Month(pvarDate), 2)
            strFormatIn = Mid(strFormatIn, 3)
        ElseIf Left(strFormatIn, 1) = "m" Then
            strOut = strOut & Month(pvarDate)
            strFormatIn = Mid(strFormatIn, 2)
        ElseIf Left(strFormatIn, 4) = "yyyy" Then
            strOut = strOut & Year(pvarDate)
            strFormatIn = Mid(strFormatIn, 5)
        ElseIf Left(strFormatIn, 2) = "yy" Then
            strOut = strOut & Right(Year(pvarDate), 2)
            strFormatIn = Mid(strFormatIn, 3)
        ElseIf Left(strFormatIn, 2) = "y" Then
            varDateTemp = DateAdd("d", -1, DateSerial(Year(pvarDate), 1, 1))
            strOut = strOut & DateDiff("d", varDateTemp, pvarDate)
            strFormatIn = Mid(strFormatIn, 3)
        ElseIf Left(strFormatIn, 2) = "hh" Then
            intHour = Hour(pvarDate)
            If InStr(LCase(pstrFormat), "am/pm") Then
                If intHour > 12 Then intHour = intHour - 12
                strOut = strOut & Right("0" & intHour, 2)
            Else
                strOut = strOut & Right("0" & intHour, 2)
            End If
            strFormatIn = Mid(strFormatIn, 3)
        ElseIf Left(strFormatIn, 1) = "h" Then
            intHour = Hour(pvarDate)
            If InStr(LCase(pstrFormat), "am/pm") Then
                If intHour > 12 Then intHour = intHour - 12
                strOut = strOut & intHour
            Else
                strOut = strOut & intHour
            End If
            strFormatIn = Mid(strFormatIn, 2)
        ElseIf Left(strFormatIn, 2) = "nn" Then
            strOut = strOut & Right("0" & Minute(pvarDate), 2)
            strFormatIn = Mid(strFormatIn, 3)
        ElseIf Left(strFormatIn, 1) = "n" Then
            strOut = strOut & Minute(pvarDate)
            strFormatIn = Mid(strFormatIn, 2)
        ElseIf Left(strFormatIn, 2) = "ss" Then
            strOut = strOut & Right("0" & Second(pvarDate), 2)
            strFormatIn = Mid(strFormatIn, 3)
        ElseIf Left(strFormatIn, 1) = "s" Then
            strOut = strOut & Second(pvarDate)
            strFormatIn = Mid(strFormatIn, 2)
        Else
            strOut = strOut & Left(strFormatIn, 1)
            strFormatIn = Mid(strFormatIn, 2)
        End If
    Loop

    MyFormatDate = strOut
End Function

Function updateNotes(valueName,valueFrom,valueTo,valueClaimID,valueUID)
	
	Set rsActivityHist = Server.CreateObject("ADODB.Recordset")
	dim sqlActivityHist
	notesDesc = "The " & valueName &" was changed from " & valueFrom & " to " & valueTo
	sqlActivityHist="Insert into Notes (Notes_Desc,Notes_Type,Claim_ID,Notes_Date,User_Id,Notes_Priority) values ('" & _
					"" &notesDesc &"','A','"&valueClaimID&"','"&Now()&"','"&valueUID&"',0)"
	response.Write sqlActivityHist
	'Response.End

	DataConn.Execute (sqlActivityHist)
End Function

function isNumber(vString)
         
         if Not isnumeric(vString) Then
              isNumber = False
         Else
              
         isNumber = False
         lCard=len(vString)
         lC=right(vString,1)
         cStat=0
         For i=(lCard) To 1 step -1
         tempChar= mid(vString,i,1)
         tmp = InStr("0123456798",tempChar)
              
         if InStr("0123456798",tempChar) > 0 Then
              cStat = cStat + 1
         End if
         Next
         if lCard = cStat Then isNumber = True
         
         End if     
    End function








Function updateNotesDataEntry(valuestatus,valueClaimID,valUserID)
	
	Set rsActivityHist = Server.CreateObject("ADODB.Recordset")
	dim sqlActivityHist
	
	sqlActivityHist="select * from Notes"
	rsActivityHist.Open sqlActivityHist,DataConn,adOpenDynamic,adLockOptimistic,adCmdText
	notesDesc = "Matter opened as  " & valuestatus
	rsActivityHist.AddNew
	rsActivityHist("Notes_Desc") = notesDesc
	rsActivityHist("Notes_Type") = "A"
	rsActivityHist("Claim_ID") = valueClaimID
	rsActivityHist("Notes_Date") = Now()
	rsActivityHist("User_ID") = valUserID	
	rsActivityHist("Notes_Priority") = 0
	rsActivityHist.Update
	rsActivityHist.Close
	Set rsActivityHist = Nothing		

End Function


Function InsertClient(CLID,ClientType,ClientFeeType)
	
	Set rsInsertClient = Server.CreateObject("ADODB.Recordset")
	dim sqlClientList
	
	sqlClientList="select * from Client"
	rsInsertClient.Open sqlClientList,DataConn,adOpenDynamic,adLockOptimistic,adCmdText
	rsInsertClient.AddNew
	rsInsertClient("Client_Id") = CLID
	rsInsertClient("Client_Type") = ClientType
	rsInsertClient("Client_Rate") = ClientFeeType
	
	rsInsertClient.Update
	rsInsertClient.Close
	Set rsInsertClient = Nothing	
End Function



Function InsertPlaintiff(PID,CID,Plaintiff_Type,Plaintiff_SetUpdate,Plaintiff_Name,Plaintiff_Address,Plaintiff_State,Plaintiff_City,Plaintiff_Zip,Plaintiff_Contact,Plaintiff_phone,Plaintiff_fax,Plaintiff_email,Plaintiff_local_address,Plaintiff_local_city,Plaintiff_local_state,Plaintiff_local_zip,Plaintiff_local_phone,Plaintiff_local_fax,Plaintiff_president,Plaintiff_secretary,Plaintiff_administrator,Plaintiff_Taxid)
	
	Set rsInsertPlaintiff = Server.CreateObject("ADODB.Recordset")
	dim sqlPlaintifflist
	
	sqlPlaintiffList="Select * from Plaintiff"
	rsInsertPlaintiff.Open sqlPlaintiffList,DataConn,adOpenDynamic,adLockOptimistic,adCmdText
	rsInsertPlaintiff.AddNew
	rsInsertPlaintiff("PID") = PID
	rsInsertPlaintiff("CID") = CID
	rsInsertPlaintiff("Plaintiff_type") = plaintiff_type
	rsInsertPlaintiff("Plaintiff_SetUpdate") = plaintiff_setupdate
	rsInsertPlaintiff("Plaintiff_Name") = plaintiff_name
	rsInsertPlaintiff("Plaintiff_Address") = plaintiff_address
	rsInsertPlaintiff("Plaintiff_State") = plaintiff_state
	rsInsertPlaintiff("Plaintiff_City") = plaintiff_city
	rsInsertPlaintiff("Plaintiff_Zip") = plaintiff_zip
	rsInsertPlaintiff("Plaintiff_Contact") = plaintiff_contact
	rsInsertPlaintiff("Plaintiff_phone") = plaintiff_phone
	rsInsertPlaintiff("Plaintiff_Fax") = plaintiff_fax
	rsInsertPlaintiff("Plaintiff_Email") = plaintiff_email
	rsInsertPlaintiff("Plaintiff_local_address") = plaintiff_local_address
	rsInsertPlaintiff("Plaintiff_local_city") = plaintiff_local_city
	rsInsertPlaintiff("Plaintiff_local_State") = plaintiff_local_state
	rsInsertPlaintiff("Plaintiff_local_zip") = plaintiff_local_zip
	rsInsertPlaintiff("Plaintiff_local_phone") = plaintiff_local_phone
	rsInsertPlaintiff("Plaintiff_local_fax") = plaintiff_local_fax
	rsInsertPlaintiff("Plaintiff_president") = plaintiff_president
	rsInsertPlaintiff("Plaintiff_secretary") = plaintiff_secretary
	rsInsertPlaintiff("Plaintiff_Administrator") = plaintiff_administrator
	rsInsertPlaintiff("Plaintiff_TaxID") = plaintiff_taxid
	rsInsertPlaintiff.Update
	rsInsertPlaintiff.Close
	Set rsInsertPlaintiff = Nothing		

End Function



Function InsertDefendant(DID,CID,Defendant_Type,Defendant_SetUpdate,Defendant_Name,Defendant_Address,Defendant_State,Defendant_City,Defendant_Zip,Defendant_Contact,Defendant_phone,Defendant_fax,Defendant_email,Defendant_local_address,Defendant_local_city,Defendant_local_state,Defendant_local_zip,Defendant_local_phone,Defendant_local_fax,Defendant_president,Defendant_secretary,Defendant_administrator,Defendant_Taxid)
	
	Set rsInsertDefendant = Server.CreateObject("ADODB.Recordset")
	dim sqlDefendantList
	
	sqlDefendantList="Select * from Defendant"
	rsInsertDefendant.Open sqlDefendantList,DataConn,adOpenDynamic,adLockOptimistic,adCmdText
	rsInsertDefendant.AddNew
	rsInsertDefendant("DID") = DID
	rsInsertDefendant("CID") = CID
	rsInsertDefendant("Defendant_type") = Defendant_type
	rsInsertDefendant("Defendant_SetUpdate") = Defendant_setupdate
	rsInsertDefendant("Defendant_Name") = Defendant_name
	rsInsertDefendant("Defendant_Address") = Defendant_address
	rsInsertDefendant("Defendant_State") = Defendant_state
	rsInsertDefendant("Defendant_City") = Defendant_city
	rsInsertDefendant("Defendant_Zip") = Defendant_zip
	rsInsertDefendant("Defendant_Contact") = Defendant_contact
	rsInsertDefendant("Defendant_phone") = Defendant_phone
	rsInsertDefendant("Defendant_Fax") = Defendant_fax
	rsInsertDefendant("Defendant_Email") = Defendant_email
	rsInsertDefendant("Defendant_local_address") = Defendant_local_address
	rsInsertDefendant("Defendant_local_city") = Defendant_local_city
	rsInsertDefendant("Defendant_local_State") = Defendant_local_state
	rsInsertDefendant("Defendant_local_zip") = Defendant_local_zip
	rsInsertDefendant("Defendant_local_phone") = Defendant_local_phone
	rsInsertDefendant("Defendant_local_fax") = Defendant_local_fax
	rsInsertDefendant("Defendant_president") = Defendant_president
	rsInsertDefendant("Defendant_secretary") = Defendant_secretary
	rsInsertDefendant("Defendant_Administrator") = Defendant_administrator
	rsInsertDefendant("Defendant_TaxID") = Defendant_taxid
	rsInsertDefendant.Update
	rsInsertDefendant.Close
	Set rsInsertDefendant = Nothing		

End Function



Function InsertNotesMemo(valueMemo,valueClaimID,valUserID)
	
	Set rsMemoHist = Server.CreateObject("ADODB.Recordset")
	dim sqlMemoHist
	
	sqlMemoHist="select * from Notes"
	rsMemoHist.Open sqlMemoHist,DataConn,adOpenDynamic,adLockOptimistic,adCmdText
	notesDesc = "" & valueMemo
	rsMemoHist.AddNew
	rsMemoHist("Notes_Desc") = notesDesc
	rsMemoHist("Notes_Type") = "A"
	rsMemoHist("Claim_ID") = valueClaimID
	rsMemoHist("Notes_Date") = Now()
	rsMemoHist("User_ID") = valUserID	
	rsMemoHist("Notes_Priority") = 0
	rsMemoHist.Update
	rsMemoHist.Close
	Set rsMemoHist = Nothing		

End Function

SUB objListBox(dbObj,val,display,strName,strTarget,defaultValue)
	blnFoundTarget = false
	objRSTemp.Open dbObj,DataConn
	strLB =   ("<select class='pulldown4' name='" &strName&"' id='" & strName & "' onChange = (this.form.submit())>"&vbCRLF)
	IF defaultValue <> "" AND blnFoundTarget = false THEN
		strLB = strLB &("<OPTION selected value = ''>"  &defaultValue&"</OPTION>"&vbCRLF)
	END IF

	DO WHILE NOT objRSTemp.EOF
		IF (strTarget) = objRSTemp(val) THEN
		blnSelected = "selected"
		blnFoundTarget = true
		ELSE
		blnSelected = ""
		END IF
		strLB = strLB &("<OPTION "&blnSelected&" value = '"  &objRSTemp(val)&  "'>" &objRSTemp(display)&  "</OPTION>"&vbCRLF)
		objRSTemp.MoveNext
	LOOP
	strLB = strLB &("</select>")
	Response.Write (strLB)
	objRSTemp.Close
END SUB

SUB objListBoxZ(dbObj,val,display,display2,display3,strName,strTarget,defaultValue)
	blnFoundTarget = false
	objRSTemp.Open dbObj,DataConn
	strLB =   ("<select class='pulldown4' name='" &strName&"' id='" & strName & "' onChange=""this.form.matter_key.value=this.options[this.selectedIndex].text"">"&vbCRLF)
	IF defaultValue <> "" AND blnFoundTarget = false THEN
		strLB = strLB &("<OPTION selected value = ''>"  &defaultValue&"</OPTION>"&vbCRLF)
	END IF

	DO WHILE NOT objRSTemp.EOF
		IF (strTarget) = objRSTemp(val) THEN
		blnSelected = "selected"
		blnFoundTarget = true
		ELSE
		blnSelected = ""
		END IF
		strLB = strLB &("<OPTION "&blnSelected&" value = '"  &objRSTemp(val)&  "'>" &Ucase(objRSTemp(display))&", " & Ucase(objRSTemp(display2)) & " - "& "(" & objRSTemp(val)& ")" & " - "& "(" & objRSTemp(display3)& ")"&"</OPTION>"&vbCRLF)
		objRSTemp.MoveNext
	LOOP
	strLB = strLB &("</select>")
	Response.Write (strLB)
	objRSTemp.Close
END SUB

SUB objListBoxEI(dbObj,val,display,display2,strName,strTarget,defaultValue)
	blnFoundTarget = false
	objRSTemp.Open dbObj,DataConn
	strLB =   ("<select class='pulldown4' name='" &strName&"' id='" & strName & "' onChange=""this.form.insurer_key.value=this.options[this.selectedIndex].text"">"&vbCRLF)
	IF defaultValue <> "" AND blnFoundTarget = false THEN
		strLB = strLB &("<OPTION selected value = ''>"  &defaultValue&"</OPTION>"&vbCRLF)
	END IF

	DO WHILE NOT objRSTemp.EOF
		IF (strTarget) = objRSTemp(val) THEN
		blnSelected = "selected"
		blnFoundTarget = true
		ELSE
		blnSelected = ""
		END IF
		strLB = strLB &("<OPTION "&blnSelected&" value = '"  &objRSTemp(val)&  "'>" &Ucase(objRSTemp(display)) & " - "& "(" & objRSTemp(val)& ")" & "</OPTION>"&vbCRLF)
		objRSTemp.MoveNext
	LOOP
	strLB = strLB &("</select>")
	Response.Write (strLB)
	objRSTemp.Close
END SUB

SUB objListBoxF(dbObj,val,display,strName,strTarget,defaultValue)
	blnFoundTarget = false
	objRSTemp.Open dbObj,DataConn
	strLB =   ("<select class='pulldown4' name='" &strName&"' id='" & strName & "' onChange=""this.form.firm_key.value=this.options[this.selectedIndex].text"">"&vbCRLF)
	IF defaultValue <> "" AND blnFoundTarget = false THEN
		strLB = strLB &("<OPTION selected value = ''>"  &defaultValue&"</OPTION>"&vbCRLF)
	END IF

	DO WHILE NOT objRSTemp.EOF
		IF (strTarget) = objRSTemp(val) THEN
		blnSelected = "selected"
		blnFoundTarget = true
		ELSE
		blnSelected = ""
		END IF
		strLB = strLB &("<OPTION "&blnSelected&" value = '"  &objRSTemp(val)&  "'>" &Ucase(objRSTemp(display))& " - "& "(" & objRSTemp(val)& ")" & "</OPTION>"&vbCRLF)
		objRSTemp.MoveNext
	LOOP
	strLB = strLB &("</select>")
	Response.Write (strLB)
	objRSTemp.Close
END SUB

SUB objListBoxEC(dbObj,val,display,strName,strTarget,defaultValue)
	blnFoundTarget = false
	objRSTemp.Open dbObj,DataConn
	strLB =   ("<select class='pulldown4' name='" &strName&"' id='" & strName & "' onChange=""this.form.client_key.value=this.options[this.selectedIndex].text"">"&vbCRLF)
	IF defaultValue <> "" AND blnFoundTarget = false THEN
		strLB = strLB &("<OPTION selected value = ''>"  &defaultValue&"</OPTION>"&vbCRLF)
	END IF

	DO WHILE NOT objRSTemp.EOF
		IF (strTarget) = objRSTemp(val) THEN
		blnSelected = "selected"
		blnFoundTarget = true
		ELSE
		blnSelected = ""
		END IF
		strLB = strLB &("<OPTION "&blnSelected&" value = '"  &objRSTemp(val)&  "'>" &Ucase(objRSTemp(display))& " - "& "(" & objRSTemp(val)& ")" & "</OPTION>"&vbCRLF)
		objRSTemp.MoveNext
	LOOP
	strLB = strLB &("</select>")
	Response.Write (strLB)
	objRSTemp.Close
END SUB

SUB objListBoxClaim_REP(dbObj,val,display,strName,strTarget,defaultValue,display2)
	blnFoundTarget = false
	objRSTemp.Open dbObj,DataConn
	strLB =   ("<select class='pulldown4' name='" &strName&"' id='" & strName & "' onChange=""this.form.claim_rep_key.value=this.options[this.selectedIndex].text"">"&vbCRLF)
	IF defaultValue <> "" AND blnFoundTarget = false THEN
		strLB = strLB &("<OPTION selected value = ''>"  &defaultValue&"</OPTION>"&vbCRLF)
	END IF

	DO WHILE NOT objRSTemp.EOF
		IF (strTarget) = objRSTemp(val) THEN
		blnSelected = "selected"
		blnFoundTarget = true
		ELSE
		blnSelected = ""
		END IF
		strLB = strLB &("<OPTION "&blnSelected&" value = '"  &objRSTemp(val)&  "'>" &Ucase(objRSTemp(display))& " " & Ucase(objRSTemp(display2)) &" - "& "(" & objRSTemp(val)& ")" & "</OPTION>"&vbCRLF)
		objRSTemp.MoveNext
	LOOP
	strLB = strLB &("</select>")
	Response.Write (strLB)
	objRSTemp.Close
END SUB
SUB objListBoxB(dbObj,val,display,strName,strTarget,defaultValue)
	blnFoundTarget = false
	objRSTemp.Open dbObj,DataConn
	strLB =   ("<select class='pulldown2' name='" &strName&"' id='" & strName & "' onChange = (this.form.submit())>"&vbCRLF)
	IF defaultValue <> "" AND blnFoundTarget = false THEN
		strLB = strLB &("<OPTION selected value = ''>"  &defaultValue&"</OPTION>"&vbCRLF)
	END IF

	DO WHILE NOT objRSTemp.EOF
		IF (strTarget) = objRSTemp(val) THEN
		blnSelected = "selected"
		blnFoundTarget = true
		ELSE
		blnSelected = ""
		END IF
		strLB = strLB &("<OPTION "&blnSelected&" value = '"  &objRSTemp(val)&  "'>" &objRSTemp(display)&  "</OPTION>"&vbCRLF)
		objRSTemp.MoveNext
	LOOP
	strLB = strLB &("</select>")
	Response.Write (strLB)
	objRSTemp.Close
END SUB

'---------------------------------------------------------------------------------------------------------------
'Phone number formatting - Chetan

'---------------------------------------------------------------------------------------------------------------

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Routine:   fmtDateTime()
'
' Purpose:   Returns a Variant (String) containing an expression 
'            formatted according to instructions contained in a 
'            format expression
'
' Inputs :   Argument    : d
'            DataType    : Variant Date
'            Description : A *valid* variant date variable or an
'                        : expression that result in a variant date
'                        : variable
'
'            Argument    : pat
'            DataType    : Variant String
'            Description : An acceptable date and/or time pattern
'
' Outputs:   Argument    : None
'
' Returns:   Formatted Variant string representation of the date
'            passed in or an error message where applicable.
'
' Sample Usage :
'  strToday = fmtDateTime(Now(), "yyyy-mm-dd hh:mm:ss")
'  strYesterday = fmtDateTime(DateAdd("d", -1, Now()), "h:m:s")
'
' 
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Routine:   ZeroPad()
'
' Purpose:   Pads a Variant String with zeros to a specified number
'            of digits, 
'               e.g.  "12" padded to 4 digits --> "0012"
'
' Inputs :   Argument    : str
'            DataType    : Variant String
'            Description : The string value to be padded
'
'            Argument    : iSize
'            DataType    : Variant Integer
'            Description : The total desired size of the returned
'                          string.
'
' Outputs:   Argument    : None
'
' Returns:   A zero-padded Variant string representation of the string
'            passed in.
'
' Sample Usage :
'  strVal = ZeroPad("14", 4)  ' <--  returns "0014"
'
'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Function ZeroPad(byval str, byval iSize)
    ZeroPad = String((iSize - Len(str)), "0") & Trim(str)
End Function

Function ReplaceStr(TextIn, SearchStr, Replacement, CompMode)
Dim WorkText, Pointer 
	If IsNull(TextIn) then
		ReplaceStr = Null
	Else
		WorkText= TextIn
		Pointer = InStr(1,WorkText, SearchStr, CompMode)
		Do While Pointer>0
			WorkText= Left (WorkText, Pointer-1) & Replacement & Mid(WorkText, Pointer + Len(SearchStr))
			Pointer = InStr(Pointer+Len(Replacement), WorkText, SearchStr, CompMode)
		Loop
		ReplaceStr = WorkText
		
	End If
End Function

Function SQLFixupApos(TextIn)
	SQLFixupApos = ReplaceStr(TextIn, "'", " ",0)
	
End Function

Function SQLFixupComma(TextIn)
	SQLFixupComma = ReplaceStr(TextIn, ",", " ",0)
End Function

Function removeComma(TextIn)
	removeComma = ReplaceStr(TextIn,",","",0)
End Function
%>

