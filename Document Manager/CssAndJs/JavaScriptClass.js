//------------------------------------ Abbreviation Used ------------------------------------
//
// S - Start Comment, E - End Comment, A - Code Added, U-Code Edited, D-Code Deleted
//
//-------------------------------------- Change History -------------------------------------
//Index		Date		CallNo  Developer           Function/Procedure    Change Description
//-------------------------------------------------------------------------------------------
//			05/06/2006			ADARSH KR. BAJPAI	Developed this JavaScript to provide 
//													metthods for prevention of pressing F11
//													function Key and to retrieve Querystring
//													values client side
// 
//-------------------------------------------------------------------------------------------
//
//	How to use this Java Script File :	Adding a reference to this in HTML <HEAD> Block as
//										<script src="JavaScriptClass.js"></script>
//										will be enable you to call any method of this file
/////////////////////////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////////////////////
//	Method Name		:	PreventF11KeyPress()											   //
//																						   //
//	Description		:	This function can be used to prevent end-users from pressing F11   //
//						Function key on the page in respect to avoid viewing the address   //
//						of current web page												   //
//																						   //
//	Calling Metthod	:	Simply Add PreventF11KeyPress(); in the script block of page	   //
/////////////////////////////////////////////////////////////////////////////////////////////

function PreventF11KeyPress()
{
	if (navigator.appName == "Netscape") 
	{
		window.captureEvents(Event.KEYDOWN);
		window.onkeydown=function(evt)
		{
			if(window.event && evt.which  == 122)
			{ 
				evt.which = 505;
  			}
			if(window.event && evt.which == 505)
			{ 
				alert('F11 key can not be pressed on this page');
				return false;
			}
		}
	}
	else 
	{
		if (navigator.appName == "Microsoft Internet Explorer") 
		{
			document.onkeydown = function()
			{
				if(window.event && window.event.keyCode == 122)
				{ 
					window.event.keyCode = 505;
  				}

				if(window.event && window.event.keyCode == 505)
				{ 
					alert('F11 key can not be pressed on this page');
					return false;
				}
			}
		}
	}
}	

/////////////////////////////////////////////////////////////////////////////////////////////
//	Method Name		:	Querystring() AND Querystring_get()								   //
//																						   //
//	Description		:	Client-side access to querystring value pairs					   //
//																						   //
//	Calling Method	:	var qs = new Querystring();										   //
//						var QSTR = qs.get("QueryStringKeyName");						   //
//						Where "QueryStringKeyName" is the query string key name and		   //
//						QSTR will hold the value of query string which is represented by   //
//						Key Name "QueryStringKeyName"									   //
/////////////////////////////////////////////////////////////////////////////////////////////

function Querystring(qs) 
{ 
	// optionally pass a querystring to parse
	this.params = new Object()
	this.get=Querystring_get
	
	if (qs == null)
		qs=location.search.substring(1,location.search.length)

	if (qs.length == 0) return

	// Turn <plus> back to <space>
	qs = qs.replace(/\+/g, ' ')
	var args = qs.split('&') // parse out name/value pairs separated via &
	
	// split out each name=value pair
	for (var i=0;i<args.length;i++) 
	{
		var value;
		var pair = args[i].split('=')
		var name = unescape(pair[0])

		if (pair.length == 2)
			value = unescape(pair[1])
		else
			value = name
		this.params[name] = value
	}
}

function Querystring_get(key, default_) 
{
	// This line changes UNDEFINED to NULL
	if (default_ == null) default_ = null;
	var value=this.params[key]
	if (value==null) value=default_;
	return value
}


/////////////////////////////////////////////////////////////////////////////////////////////
//	Method Name		:	PreventRightClick()												   //
//																						   //
//	Description		:	To prevent right click from the user							   //
//																						   //
//	Calling Method	:	Simply Add PreventRightClick(); in the script block of page		   //
/////////////////////////////////////////////////////////////////////////////////////////////
	
function PreventRightClick()
{
	msg = "You can't right click on this web page!";
	//document.onselectstart=new Function ("return false");
	document.oncontextmenu=new Function("return false");
	/* Commented on 21/07/2006 By Adarsh Bajpai
	if (navigator.appName == "Netscape") 
	{
		document.onmousedown = function(e)
		{
			if (e.which > 1)
			{
				alert(msg);
				return false;
			}
		} 
		
	}
	else 
	{
		if (navigator.appName == "Microsoft Internet Explorer") 
		{
			document.onmousedown = function()
			{
				if (window.event.button > 1)
				{
						alert(msg);
					return false;
				}
			} 
		}
	}
	*/
} 

function popitup(url)
{
	newwindow=window.open(url,'Upload File','height=300,width=200');
	if (window.focus) 
	{
		newwindow.focus()
	}
}
