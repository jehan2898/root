
		//---------------- Javascript Functions -----------------------//		
		// Project Name					: 
		//------------------------------------------------------------//		
		 	
			function isNumeric(val)
			{
				//Function not allow to enter alphabets common to all browsers
				var dig="";
				var strst="";var strend="";
				var strsub="";
				for( i1=0;i1<500;i1++)
				{
					strst="";
					strend="";
					strsub="";
					dig=document.getElementById(val).value;
					for( i=0;i<dig.length;i++)
					{
						strsub=dig.substring(i,i+1);
						if(parseFloat(strsub,10)==(strsub*1))
						{
							
						}
						else
						{
							strst=dig.substring(0,i);
							strend=dig.substring(i+1,dig.length)
							dig=dig.substring(0,dig.length) ; 
							document.getElementById(val).value=strst+strend;
						}
					}
				}
			}
							
		// ------ Accept Numeric from '0' to '9' Only ----------		
			function Numeric()
			{								
				if((event.keyCode >= 48 && event.keyCode <= 57))
				{
					event.keyCode = event.keyCode ;
				} 
				else
				{
					event.keyCode = 0;
			
				}
			}
			// ------ Accept Email  ----------		
			function Email()
			{				
				if(!(((event.keyCode>=65)&&(event.keyCode<=90))||((event.keyCode>=97)&&(event.keyCode<=122))||(event.keyCode==64)||((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode == 46)||(event.keyCode == 95))))
				{					
					event.keyCode = 0;
				}
			}
				

			
			// ------ Accept Alphabets from 'A' to 'Z' & Numbers Only ----------		
			function AlphaUpperNumeric()
			{								
				if((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >=48 && event.keyCode <=57))
				{
					event.keyCode = event.keyCode ;
				} 
				else
				{
					event.keyCode = 0;
				}
			}
			
			//---Accept Numbers from '0' to '9' and '+' -----------
			function NumericPlus()
			{								
				if((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode==43))
				{
					event.keyCode = event.keyCode ;
				} 
				else
				{
					event.keyCode = 0;
				}
			}
			
			// ------ Accept Decimal from '0' to '9' and '.' Only ----------
			function Decimal()
			{				
				if((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode == 46))
				{
					event.keyCode = event.keyCode ;
				} 
				else
				{
					event.keyCode = 0;
				}
			}
			
			// ------ Accept Upper Case,lower Case and Space and numbers Only ----------
			function Alphanumeric()
			{	
				if((event.keyCode >= 48 && event.keyCode <= 57))
				{					
					event.keyCode = event.keyCode ;
				} 
				else
				{
					AlphaULS();					
				}

			}					
			
			// ------ Accept Numbers Only ----------
			function Numeric()
			{	
				if((event.keyCode >= 48 && event.keyCode <= 57))
				{					
					event.keyCode = event.keyCode ;
				} 
				else
				{
					event.keyCode= 0;
				}
			}					
			
			
			// ------ Accept Upper Case,lower Case and Space Only ----------
			function AlphaULS()
			{	
				if(!(((event.keyCode>=65)&&(event.keyCode<=90))||((event.keyCode>=97)&&(event.keyCode<=122))||(event.keyCode==32)||(event.keyCode==46)||(event.keyCode==44)||(event.keyCode==95)))
				{					
					event.keyCode = 0;
				}						
			}					
				
			//-------------- Confirm function for Delete -------------//
			function confirm_delete()
			{
				if(confirm("Are you sure want to Delete?")==true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			//-------------- Confirm function for Delete -------------//
			function confirm_revert()
			{
				if(confirm("Are you sure want to revert?")==true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			
			//-------------- Confirm function for Sign out -------------//
			function confirm_signout()
			{
				if(confirm("Are you sure, do you want to Sign out ?")==true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

	        function confirm_bckUp()
			{
				if(confirm("Are you sure, do you want to take Backup ?")==true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
					
			
			// ------ Phone Number Validation(0-9 , -  )----------		
			function PhoneValidation()
			{								
				if((event.keyCode >= 48 && event.keyCode <= 57)|| (event.keyCode==44)|| (event.keyCode==45) || (event.keyCode==32))
				{
					event.keyCode = event.keyCode ;
				} 
				else
				{
					event.keyCode = 0;
				}
			}
			// ------ Address Validation (0-9 , A - Z a - z , / - .- ,#,@ )----------		
			function Address()
			{	
				if(!(((event.keyCode>=65)&&(event.keyCode<=90))||((event.keyCode>=97)&&(event.keyCode<=122))||(event.keyCode==32)||(event.keyCode==35)||(event.keyCode==64)||((event.keyCode >= 44 && event.keyCode <= 57))))
				{					
					event.keyCode = 0;
				}

			}
			
			

				//--------------- To Check the Form Controls -----------		
			
			function formValidator(ID,strElements)
			{
		       
				    var stringArray = strElements.split(',');
				    for (var iLoop=0; iLoop < stringArray.length ; iLoop++)
				    {
					    var strCurrElement = stringArray[iLoop];				
					    var theForm = document.getElementById(ID);			
					    var allvalid = true;
					    var alertstr = "";
					    var validstr = "All form data is correct.\n\n";
					    var num_of_elements = theForm.length;
					    var radio_selected = false;
					    var checkbox_selected = false;	
			            var errormsg;
					    for (var i=1; i<num_of_elements; i++) 
					    {
						    var theElement = theForm.elements[i];
						    var element_type = theElement.type;				
						    var element_name = theElement.name;
						    var element_value = theElement.value;
    						
						    if(strCurrElement == element_name)			
						    {
						    
							    // Check Text Box ...
							    if (element_type == "text") 				
							    {											
								    if (trim(element_value).length == 0) 
								    {	
    								    
								        document.getElementById('ErrorDiv').innerHTML='Enter the mandatory field';							
									    theElement.focus();
									    theElement.style.backgroundColor = "#ffff99";
									    return false;
								    }
								    else
								    {
									    theElement.style.backgroundColor = "#ffffff";
								    }
							    }
							    //password field
							    if (element_type == "password") 				
							    {											
								    if (trim(element_value).length == 0) 
								    {					
									    //alert("Enter the mandatory field!");
									    document.getElementById('ErrorDiv').innerHTML='Enter the mandatory field';
									    theElement.focus();
									    theElement.style.backgroundColor = "#ffff99";
									    return false;
								    }
								    else
								    {
									    theElement.style.backgroundColor = "#ffffff";
								    }
							    }							
							    // Check Drop-down lists ...
							    if (element_type.indexOf("select") > -1) 
							    {						
								    var index = theElement.selectedIndex;
								    if (index == "0") 
								    {
									    //alert("Select the mandatory field !");
									    document.getElementById('ErrorDiv').innerHTML='Enter the mandatory field';
									    //alert(theElement.name);
									    //alert(document.getElementById(theElement.name).disabled);
									    if (document.getElementById(theElement.name).disabled==true)
									    {
									   
									    }
									    else
									    {
									    theElement.focus();}
									    theElement.style.backgroundColor = "#ffff99";
									    return false;
								    }
								    else
								    {
									    theElement.style.backgroundColor = "#ffffff";
								    }
							    }
							    // Check Textarea boxes ...
							    if (element_type == "textarea") 
							    {
								    if (element_value.length == 0) 
								    {					
									    //alert("Enter the mandatory field!");
									    document.getElementById('ErrorDiv').innerHTML='Enter the mandatory field';
									    theElement.focus();
									    theElement.style.backgroundColor = "#ffff99";
									    return false;
								    }
								    else
								    {
									    theElement.style.backgroundColor = "#ffffff";
								    }
							    }
    							
							    // Check List Box...
							    if (element_type.indexOf("multiple") > -1) 
							    {										
								    var index = theElement.selectedIndex;					
								    if (index < 1) 
								    {
									    //alert("Select Value!");
									    document.getElementById('ErrorDiv').innerHTML='Select Value!';
									    theElement.focus();
									    theElement.style.backgroundColor = "#ffff99";
									    return false;
								    }
								    else
								    {
									    theElement.style.backgroundColor = "#ffffff";
								    }
							    }
    							
							    // Check File Browser...
							    if (element_type.indexOf("file") > -1) 
							    {										
								    if (trim(element_value).length == 0) 
								    {					
									    //alert("Enter the mandatory field!");
									    document.getElementById('ErrorDiv').innerHTML='Enter the mandatory field!';
									    theElement.focus();
									    theElement.style.backgroundColor = "#ffff99";
									    return false;
								    }
								    else
								    {
									    theElement.style.backgroundColor = "#ffffff";
								    }
							    }
							    if(ID=='frmRegistration')
							    {
							         var pwd=document.getElementById('txtPassword');
                                   var confirmpwd=document.getElementById('txtConfirmPWD');
                                   
                                   if(pwd.value==confirmpwd.value)
                                   {
                                       
                                   }
                                   else
                                   {
                                        document.getElementById('ErrorDiv').innerHTML="Password Do not match..!";
                                         return false;
                                   }
							    }
							    // Check Radio buttons ...
							    if (element_type == "radio" ) 
							    {
								    if (theElement.checked == true) 
								    {
									    radio_selected = true;
									    validstr += "From form element '" + element_name + 
									    "' you selected the \"" + element_value + "\" button.\n\n";
								    }
							    }
    														
							    // Check Checkboxes ...
							    if (element_type == "checkbox") 
							    {
								    if (theElement.checked == true) 
								    {
									    checkbox_selected = true;
									    validstr += "From form element '" + element_name + 
									    "' you selected the \"" + element_value + "\" checkbox.\n\n";
								    }
							    }
						    }
					    }
				    }
				
			
				
			}					
			
			
			// Removes leading whitespaces
			function LTrim( value ) 
			{
				var re = /\s*((\S+\s*)*)/;
				return value.replace(re, "$1");
			}

			// Removes ending whitespaces
			function RTrim( value ) 
			{
				var re = /((\s*\S+)*)\s*/;
				return value.replace(re, "$1");
			}

			// Removes leading and ending whitespaces
			function trim( value ) 
			{
				return LTrim(RTrim(value));	

			}
			
		function ChkPerTime(ID1,ID2,ID3,ID4)
		{
			alert('test');
			// --------From and To Time Validation ------------
			var fromTime	= document.getElementById(ID1).value + ':' + document.getElementById(ID2).value 
			var toTime 		= document.getElementById(ID3).value + ':' + document.getElementById(ID4).value 

			var Status1 = CheckValid(fromTime,toTime);
			
			if(Status1 == false)
			{
				alert("To Time Should be Greater than From Time");
				return false;
			}
			return true;
		}

		function CheckValid(stTime,edTime)
		{	
			var strArryFrom  = stTime.split(':');
			var iHrsFrom = strArryFrom[0];
			var iMinFrom = strArryFrom[1];	
			var strArryTo  = edTime.split(':');
			var iHrsTo = strArryTo[0];
			var iMinTo = strArryTo[1];
			var blnStatus = "0"  ;
			if((iHrsTo == iHrsFrom) && (iMinTo > iMinFrom))
			{
				blnStatus = "1";
			}
			else if(iHrsTo > iHrsFrom) 
			{
				blnStatus = "1";
			}
			
			if(blnStatus=="0")
			{
				return false;
			}
			else if(blnStatus=="1")
			{
				return true;
			} 
			return false;
		}


		function CheckTime(stTime,edTime)
		{	
			var strArryFrom  = stTime.split(':');
			var iHrsFrom = strArryFrom[0];
			var iMinFrom = strArryFrom[1];	
			var strArryTo  = edTime.split(':');
			var iHrsTo = strArryTo[0];
			var iMinTo = strArryTo[1];
			var blnStatus = "0"  ;
			if((iHrsTo == iHrsFrom) && (iMinTo > iMinFrom))
			{
				blnStatus = "1";
			}
			else if(iHrsTo > iHrsFrom) 
			{
				blnStatus = "1";
			}
			if(blnStatus=="0")
			{
				return false;
			}
			else if(blnStatus=="1")
			{
				return true;
			} 
			return false;
			
		}
		function CheckBetweenTime(ID1,ID2,ID3,ID4,ID5,ID6,ID7,ID8)
		{
			// --------From and To Time Validation 
			var fromTime	= document.getElementById(ID1).value + ':' + document.getElementById(ID2).value 
			var toTime 		= document.getElementById(ID3).value + ':' + document.getElementById(ID4).value 
			var lunFromTime = document.getElementById(ID5).value + ':' + document.getElementById(ID6).value 
			var lunToTime   = document.getElementById(ID7).value + ':' + document.getElementById(ID8).value 

			var Status1 = CheckTime(fromTime,toTime);
			if(Status1 == false)
			{
				alert("To Time Should be Greater than From Time");
				return false;
			}
			var Status2 = CheckTime(lunFromTime,lunToTime);	
			if(Status2 == false)
			{
				alert("Lunch To Time Should be Greater than Lunch From time");
				return false;
			}	
			var Status1 = CheckTime(fromTime,lunFromTime);
			var Status2 = CheckTime(lunToTime,toTime);	
			if((Status1 == false)||(Status2 == false))
			{
				alert("Lunch Time Should be Between Working Hours");
				return false;
			}
			return true;
		}




		function isEmail(formID,ID)
		{			
					
			var EmailIdvalue = document.getElementById(ID).value;			
			var Form =  document.getElementById(formID);			
			if(EmailIdvalue == "")
			{				
			    document.getElementById(ID).style.backgroundColor = '';	
			    document.getElementById('ErrorDiv').innerText='';
				return true;
			}
			if (EmailIdvalue.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
			{
			    document.getElementById('ErrorDiv').innerText='';
			    document.getElementById(ID).style.backgroundColor = '';	
				return true;
			}
			else
			{			
				document.getElementById('ErrorDiv').innerText='Enter valid email id ...!';
				document.getElementById(ID).value=""
				document.getElementById(ID).focus();
				document.getElementById(ID).style.backgroundColor = "#ffff99";			
				return false;
			}			
		}
		function datevalid(ID)
		{
		    var DateIdvalue=document.getElementById(ID).value;
		    if(DateIdvalue=="")
		    {
                alert("Enter the mandatory field!");
				document.getElementById(ID).focus();
				document.getElementById(ID).style.backgroundColor = "#ffff99";
				return false;
			}
			else
			{
				document.getElementById(ID).style.backgroundColor = "#ffffff";
			}
		    
		}



			// ------ Accept Upper Case,lower Case, Numbers and NoSpace   ----------
			function AlphaULNS()
			{				
				if(!((event.keyCode>=65 && event.keyCode<=90)||(event.keyCode>=97 && event.keyCode<=122) ||(event.keyCode >= 48 && event.keyCode <= 57)))
				{					
					event.keyCode = event.keyCode;
				}						
			}	
			
			// ---------------Accept Lowercase uppercase Numbers and undersocre only----
			
			function UserNameValidation()
			{	
				if((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode==95 ))
				{					
					event.keyCode = event.keyCode ;
				} 
				else
				{
					AlphaULNS();					
				}

			}
				
			
			function isAlpha(val)
			{
				//Function not allowed to enter numbers common to all browsers
				escap(val); //Function not allowed to enter special characters common to all browsers
				var dig="";
				var strst="";var strend="";
				var strsub="";
				for( i1=0;i1<500;i1++)
				{
					strst="";
					strend="";
					strsub="";
					dig="";
					dig=document.getElementById(val).value;
					for( i=0;i<dig.length+1;i++)
					{
						strsub=dig.substring(i,i+1);
						if(parseFloat(strsub,10)==(strsub*1))
						{
							strst=dig.substring(0,i);
							strend=dig.substring(i+1,dig.length)
							dig=dig.substring(0,dig.length) ; 
							document.getElementById(val).value=strst+strend;							
						}
						else
						{
							
						}													
					}
				}
			}
			
			function escap(val)
			{
				//Function not allowed any special characters
				//Common to Internet Explorer and Firebox
				var dig="";							
				var sBadChars=new Array("~",
										"`",
										"!",
										"@",
										"#",
										"$",
										"%",
										"^",
										"&",
										"*",
										"(",
										 ")",
										"-",
										"_",
										"=",
										"+",										
										"|",
										"[",
										"]",
										"{"	,
										"}",
										"'",
										";",
										":",
										"/",
										"?",
										".",
										">",
										"<",	
										","										
										);
				dig=document.getElementById(val).value;	
				for (iCount = 0; iCount < sBadChars.length; iCount++)
				{
				  while (dig.indexOf(sBadChars[iCount]) != -1)
				  {
				    dig = dig.replace(sBadChars[iCount], "");
					
				  }				 				 
				}																						
				document.getElementById(val).value=dig;
			}
			
			
function datecheck(e)
{
	var whichCode = (window.Event) ? e.which : e.keyCode;
	var strCheck = '0123456789./';
	
	if (whichCode == 13) return true;  
	key = String.fromCharCode(whichCode);  // Get key value from key code
	if (strCheck.indexOf(key) == -1) return false; 
}

function schdatecheck(mobj, m)
{

	var m1 = m ;
	if (mobj.value == "" && m1 == "N" )
	{
		return true;
	}
	if (chkdate(mobj.value)== false )
	{
		alert("Please enter a valid date.");
		mobj.select();
		mobj.focus();
		return false;
	}
	mobj.value = chkdate(mobj.value);
	return true;
}

function chkdate(pdate) 
{
	var strDate = pdate;

	var strDateArray;
	var strDay;
	var strMonth;
	var strYear;
	var intday;
	var intMonth;
	var intYear;
	var booFound = false;
	var strSeparatorArray = new Array("-"," ","/",".");
	var intElementNr;
	var err = 0;

	if (strDate.length < 6) 
	{return false;}

	for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++) 
	{	if (strDate.indexOf(strSeparatorArray[intElementNr]) != -1) 
		{
			strDateArray = strDate.split(strSeparatorArray[intElementNr]);
			if (strDateArray.length != 3) 
			{	err = 1;
				return false;
			}
			else 
			{
				strDay  = strDateArray[1];
				strMonth= strDateArray[0];
				strYear = strDateArray[2];
			}
			booFound = true;
		}
	}

	if (booFound == false) 
	{	if (strDate.length > 5) 
		{
			strDay = strDate.substr(0, 2);
			strMonth = strDate.substr(2, 2);
			strYear = strDate.substr(4);
		}
	}
	
	if(strYear.length>4)
	{
		err = 2;
		return false;
	}
	
	if (strYear.length == 2) 
	{	strYear = '20' + strYear;
	}

	intday = parseInt(strDay, 10);
	if (isNaN(intday)) 
	{
			err = 2;
		return false;
	}
	intMonth = parseInt(strMonth, 10);

	if (isNaN(intMonth)) 
	{
		for (i = 0;i<12;i++) 
		{
			if (strMonth.toUpperCase() == strMonthArray[i].toUpperCase()) 
			{
				intMonth = i+1;
				strMonth = strMonthArray[i];
				i = 12;
			}
		}

		if (isNaN(intMonth)) 
		{
			err = 3;
			return false;
		}
	}
	
	intYear = parseInt(strYear, 10);
	if (isNaN(intYear)) 
	{
		err = 4;
		return false;
	}
	if (intMonth>12 || intMonth<1) 
	{
		err = 5;
		return false;
	}
	if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1)) 
	{
		err = 6;
		return false;
	}
	if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1)) 
	{
		err = 7;
		return false;
	}
	if (intMonth == 2) 
	{
		if (intday < 1) 
		{
			err = 8;
			return false;
		}
		if (LeapYear(intYear) == true) 
		{
			if (intday > 29) 
			{
				err = 9;	
				return false;
			}
		}
		else 
		{
			if (intday > 28) 
			{
				err = 10;
				return false;
			}
		}
	}

	var dt = new Date()
	if (strYear > dt.getYear() ) 
	{
		return false;
	}
		
	return ( intMonth+"/" + intday + "/" + strYear);
}

function LeapYear(intYear) 
{
	if (intYear % 100 == 0) 
	{	if (intYear % 400 == 0) 
		{ return true; }
	}
	else 
	{
		if ((intYear % 4) == 0) 
		{ return true; }
	}
return false;
}			


function AlertCount(id,length,msg,e)
	{
	    var keyASCII =BrowserCheck(e);
		var keyValue = String.fromCharCode(keyASCII); 
		
		if(document.getElementById(id).value.length > length)
		{
		    if(msg!="")
		    {  
		       alert(msg);
		      
		    }
		    document.getElementById(id).value= document.getElementById(id).value.substring(0, length)
		    return BrowserCorrection(e)
		}
	}
	
	
 function BrowserCheck(e)
	{
		var keyASCII ; 
		
		if(window.event) // IE
		{
			keyASCII = e.keyCode
		}
		else if(e.which) // Netscape/Firefox/Opera
		{
			keyASCII = e.which
		}
		return keyASCII;
	}
	
function BrowserCorrection(e)
{
	if(window.event) // IE
	{
		e.keyCode=0;
		return false;
	}
	else if(e.which) // Netscape/Firefox/Opera
	{
		if(e.which!='8')
		{
			return false;
		}
		
	}
}
function CheckPresentDays(txtID1,txtID2)
{
    if(parseFloat(document.getElementById(txtID1).value) < parseFloat(document.getElementById(txtID2).value))
    {
        alert("Enter Correct Present Days");
        document.getElementById(txtID2).select();
        return false;
    }
}

function CheckOutstanding(txtID1)
    {
        if(parseFloat(document.getElementById(txtID1).value) < 9)
        {
            alert("Enter Correct Marks");
            document.getElementById(txtID1).select();
            return false;
        }
        else if(parseFloat(document.getElementById(txtID1).value) >10)
        {
            alert("Enter Correct Marks");
            document.getElementById(txtID1).select();
            return false;
        }    
    }
    
    function CheckVeryGood(txtID1)
    {
        if(parseFloat(document.getElementById(txtID1).value) < 6)
        {
            alert("Enter Correct Marks");
            document.getElementById(txtID1).select();
            return false;
        }
        else if(parseFloat(document.getElementById(txtID1).value) >8)
        {
            alert("Enter Correct Marks");
            document.getElementById(txtID1).select();
            return false;
        }    
    }
    
    function CheckGood(txtID1)
    {
        if(parseFloat(document.getElementById(txtID1).value) < 4)
        {
            alert("Enter Correct Marks");
            document.getElementById(txtID1).select();
            return false;
        }
        else if(parseFloat(document.getElementById(txtID1).value) >5)
        {
            alert("Enter Correct Marks");
            document.getElementById(txtID1).select();
            return false;
        }    
    }
    
    function CheckUnsatisfactory(txtID1)
    {
        if(parseFloat(document.getElementById(txtID1).value) > 4)
        {
            alert("Enter Correct Marks");
            document.getElementById(txtID1).select();
            return false;
        }       
    }

function ConfirmPassword()
{
       var pwd=document.getElementById('txtPassword');
       var confirmpwd=document.getElementById('txtConfirmPWD');
       
       if(pwd.value==confirmpwd.value)
       {
           
       }
       else
       {
            document.getElementById('ErrorDiv').innerHTML="Password Does not match..!";
             return false;
       }
}

													

