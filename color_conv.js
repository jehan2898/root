//////////////////////////////////////////////////
//Calling the dialog
function fnProcessReturnValue(retVal){
	if(retVal){
		if(retVal.substr(0,3)=='OK!'){
			return retVal.substring(3,retVal.length);
		}
	}
	return "";
}

function fnShowChooseColorDlg(color,param,path,controlid,i_id){

	if (document.all) { //IE4 and up
		var args = new Array(5);
		args[0] = color;
		args[1] = window;
		args[2] = param;
		args[3] = controlid;
		args[4] = i_id
		retVal = window.showModalDialog(
				path+'ColorDialog.html',args,
				'dialogHeight: 380px; dialogWidth: 245px; center: yes; scroll: No; help:  No; resizable: No; status:no;');				
				
//                if(document.getElementById('g_controlid')!=null)
//                {
//                    document.getElementById('g_controlid').value ="";
//                    document.getElementById('g_controlid').value = controlid;
//                }
//                else
//                {
//                    document.getElementById('g_controlid').value = controlid;
//                }
		return fnProcessReturnValue(retVal);
	} else if (document.layers) {
	
	} else if (document.getElementById) { //mozilla
		var winRef;
		
		winRef = window.open(
				path+'ColorDialog.html?'+escape(color)+'&'+param+'&'+controlid+'&'+i_id,"_blank",
				'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=245, height=325');
				
			
//                if(document.getElementById('g_controlid')!=null)
//                {

//                    document.getElementById('g_controlid').value ="";
//                    document.getElementById('g_controlid').value = controlid;
//                }
//                else
//                {
//                    document.getElementById('g_controlid').value = controlid;
//                }
	}
}

/////////////////////////////////////////////////////////////////////////////////
//Coockies
function Get_Cookie(name){ 
   var start = document.cookie.indexOf(name+"="); 
   var len = start+name.length+1; 
   if ((!start) && (name != document.cookie.substring(0,name.length))) return null; 
   if (start == -1) return null; 
   var end = document.cookie.indexOf(";",len); 
   if (end == -1) end = document.cookie.length; 
   return unescape(document.cookie.substring(len,end)); 
} 

function Set_Cookie(name,value,expires,path,domain,secure) { 
    var cookieString = name + "=" +escape(value) + 
       ( (expires) ? ";expires=" + expires.toGMTString() : "") + 
       ( (path) ? ";path=" + path : "") + 
       ( (domain) ? ";domain=" + domain : "") + 
       ( (secure) ? ";secure" : ""); 
    document.cookie = cookieString; 
} 

function Delete_Cookie(name,path,domain) { 
   if (Get_Cookie(name)) document.cookie = name + "=" + 
      ( (path) ? ";path=" + path : "") + 
      ( (domain) ? ";domain=" + domain : "") + 
      ";expires=Sun, 01-Jan-70 00:00:01 GMT"; 
} 


function setCookieColor(colorName,value){
	var myDate=new Date();
	myDate.setFullYear(2040,0,1);
	Set_Cookie(colorName,value,myDate,"","","");
}


function getCookieColor(colorName){
	val = Get_Cookie(colorName);
	if (val == null)
		return "#ffffff";
	return val;
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////	
//RGB HLS Color transforms

	var RANGE = 240;
	var HLSMAX = RANGE;/* H,L, and S vary over 0-HLSMAX */
   	var RGBMAX  = 255;   /* R,G, and B vary over 0-RGBMAX */
                           /* HLSMAX BEST IF DIVISIBLE BY 6 */
                           /* RGBMAX, HLSMAX must each fit in a byte. */

   /* Hue is undefined if Saturation is 0 (grey-scale) */
   /* This value determines where the Hue scrollbar is */
   /* initially set for achromatic colors */
   var UNDEFINED  = HLSMAX*2/3;

   function  RGBtoHLS(R,G,B)
   {
      cMax = Math.max( Math.max(R,G), B);
      cMin = Math.min( Math.min(R,G), B);

      L = Math.floor(( ((cMax+cMin)*HLSMAX) + RGBMAX )/(2*RGBMAX));

      if (cMax == cMin) {           /* r=g=b --> achromatic case */
         S = 0;                     /* saturation */
         H = UNDEFINED;             /* hue */
      }
      else {                        /* chromatic case */
         /* saturation */
         if (L <= (HLSMAX/2))
            S = Math.floor( ( ((cMax-cMin)*HLSMAX) + ((cMax+cMin)/2) ) / (cMax+cMin) );
         else
            S = Math.floor( ( ((cMax-cMin)*HLSMAX) + ((2*RGBMAX-cMax-cMin)/2) )/ (2*RGBMAX-cMax-cMin) );

         /* hue */
      Rdelta = Math.floor( ( ((cMax-R)*(HLSMAX/6)) + ((cMax-cMin)/2) ) / (cMax-cMin) );
      Gdelta = Math.floor( ( ((cMax-G)*(HLSMAX/6)) + ((cMax-cMin)/2) ) / (cMax-cMin) );
      Bdelta = Math.floor( ( ((cMax-B)*(HLSMAX/6)) + ((cMax-cMin)/2) ) / (cMax-cMin) );

         if (R == cMax)
            H = Bdelta - Gdelta;
         else if (G == cMax)
            H = (HLSMAX/3) + Rdelta - Bdelta;
         else /* B == cMax */
            H = ((2*HLSMAX)/3) + Gdelta - Rdelta;

         if (H < 0)
            H += HLSMAX;
         if (H > HLSMAX)
            H -= HLSMAX;
      }
	  
	  res = new Array();
	  res[0] = Math.floor(H);
	  res[1] = Math.floor(L);
	  res[2] = Math.floor(S);
	  
	  return res;
   }
   /* utility routine for HLStoRGB */
   
   function  HueToRGB(n1,n2,hue)
   {
	   
		n1 = Math.floor(n1);
		n2 = Math.floor(n2);
		hue = Math.floor(hue);
      /* range check: note values passed add/subtract thirds of range */
      if (hue < 0)
         hue += HLSMAX;

      if (hue > HLSMAX)
         hue -= HLSMAX;

      /* return r,g, or b value from this tridrant */
      if (hue < (HLSMAX/6))
          return Math.floor( n1 + Math.floor((((n2-n1)*hue+(HLSMAX/12))/(HLSMAX/6))) );
      if (hue < (HLSMAX/2))
         return ( n2 );
      if (hue < ((HLSMAX*2)/3))
         return Math.floor( n1 +    Math.floor( (((n2-n1)*(((HLSMAX*2)/3)-hue)+(HLSMAX/12)) / (HLSMAX/6)) ) );
      else
         return Math.floor( n1 );
   }

   function HLStoRGB(hue,lum,sat)
    {

      if (sat == 0) {            /* achromatic case */
         R=G=B=(lum*RGBMAX)/HLSMAX;
         if (hue != UNDEFINED) {
            /* ERROR */
          }
       }
      else  {                    /* chromatic case */
         /* set up magic numbers */
         if (lum <= (HLSMAX/2))
            Magic2 = Math.floor(	(lum*(HLSMAX + sat) + (HLSMAX/2)) / HLSMAX	);
         else
            Magic2 = (lum + sat - Math.floor( ((lum*sat) + (HLSMAX/2))/HLSMAX) );
			
         Magic1 = 2*lum-Magic2;

         /* get RGB, change units from HLSMAX to RGBMAX */
         R = (HueToRGB(Magic1,Magic2,hue+(HLSMAX/3))*RGBMAX + (HLSMAX/2))/HLSMAX;
         G = (HueToRGB(Magic1,Magic2,hue)*RGBMAX + (HLSMAX/2)) / HLSMAX;
         B = (HueToRGB(Magic1,Magic2,hue-(HLSMAX/3))*RGBMAX +  (HLSMAX/2))/HLSMAX;
      }
	  
  
	  res = new Array();
	  res[0] = Math.floor(R);
	  res[1] = Math.floor(G);
	  res[2] = Math.floor(B);
	  
	  return res;

    }

/////////////////////////////////////////////////////////////////
//drawing
var g_param = 0;
var g_controlid="";
var step = 6;
var visStep = 5;

function myParseInt(strIntNum){
	if(strIntNum.length<=0 || strIntNum=="")
		return 0;
	
	intStr = "0123456789";
	
	for(i = 0 ; i < strIntNum.length; i++){
	   if(intStr.indexOf(strIntNum.substring(i,1))<0)
			return 0;
	}
	return parseInt(strIntNum);
}

function getHex(num){
   hexStr = "0123456789ABCDEF";
   hex="";
   if (num>=16) {
      hex = hexStr.substr(parseInt(num/16),1);
      num = num%16;
   }
   hex += hexStr.substr(num,1);

   if(hex.length == 1)
		hex = "0" +  hex;
   
   return hex;
}

function getNum(hex){
	if(hex.length !=2 )	
		return 0;
	hexStr = "0123456789ABCDEFabcdef";

	for(i = 0 ; i < hex.length; i++){
	   if(hexStr.indexOf(hex.substring(i,1))<0)
			return 0;
	}

	return parseInt(hex,16);
}


function HSTable(L,parentEl){
	var table = document.createElement('TABLE');
	table.setAttribute('cellPadding',0);
	table.setAttribute('cellSpacing',0);
	table.setAttribute('border',0);

	var tBody = document.createElement('TBODY');
	table.appendChild(tBody);


	var maxS = Math.floor((HLSMAX-1)/step)*step;
	var maxH = HLSMAX-1;
	
	for(S = maxS ;S >= 0 ; S-=step)
	{
		var row = document.createElement('TR');
		for (H=0;H<maxH;H+=step)
		{
			var cell = document.createElement('TD');
			var res = HLStoRGB(H,L,S );
			cell.style.backgroundColor = "#" + getHex(res[0])+getHex(res[1]) + getHex(res[2]);
			cell.style.width = visStep + "px";
			cell.style.height = visStep + "px";
//			cell.style.width = step + "px";
//			cell.style.height = step + "px";
//			var theData = document.createTextNode();
//			cell.appendChild(theData);
			row.appendChild(cell);
		}
		tBody.appendChild(row);
	}

	document.getElementById(parentEl).appendChild(table);
//	document.body.appendChild(table);	
}


function LTable(H,S,parentEl){
	if(document.getElementById('sliderTable')){
		document.getElementById(parentEl).removeChild(document.getElementById('sliderTable'));
	}
	
	var table = document.createElement('TABLE');
	table.setAttribute('cellPadding',0);
	table.setAttribute('cellSpacing',0);
	table.setAttribute('border',0);
	table.setAttribute('id','sliderTable');

	var tBody = document.createElement('TBODY');
	table.appendChild(tBody);

	var maxL = Math.floor((HLSMAX-1)/step)*step;
	
	for(L = maxL ;L >= 0 ; L-=step)
	{
		var row = document.createElement('TR');
		
		var cell = document.createElement('TD');
		var res = HLStoRGB(H,L,S);
		cell.style.backgroundColor = "#" + getHex(res[0])+getHex(res[1]) + getHex(res[2]);
		cell.style.width = 10+ "px";
		cell.style.height = visStep + "px";

//			var theData = document.createTextNode();
//			cell.appendChild(theData);
		row.appendChild(cell);

		tBody.appendChild(row);
	}
	document.getElementById(parentEl).appendChild(table);
//	document.body.appendChild(table);	
}




var colorArr = new Array(
	new Array('#ff8080','#ffff80','#80ff80','#00ff80','#80ffff','#0080ff','#ff80c0','#ff80ff'),
	new Array('#ff0000','#ffff00','#80ff00','#00ff40','#00ffff','#0080c0','#8080c0','#ff00ff'),
	new Array('#804040','#ff8040','#00ff00','#008080','#004080','#8080ff','#800040','#ff0080'),
	new Array('#800000','#ff8000','#008000','#008040','#0000ff','#0000a0','#800080','#8000ff'),
	new Array('#400000','#804000','#004000','#004040','#000080','#000040','#400040','#400080'),
	new Array('#000000','#808000','#808040','#808080','#408080','#c0c0c0','#400040','#ffffff')
);

var curColElX = 0;
var curColElY = 0;


function fnOnColorClick(color,x,y){
	cell = document.getElementById("basic_color_"+curColElX+"_"+curColElY);
	cell.style.borderColor = "#ffffff";
	
	curColElX = x;
	curColElY = y;
	cell = document.getElementById("basic_color_"+curColElX+"_"+curColElY);
	cell.style.borderColor = "#ff0000";

	document.getElementById('R_id').value = getNum(color.substr(1,2));
	document.getElementById('G_id').value = getNum(color.substr(3,2));
	document.getElementById('B_id').value = getNum(color.substr(5,2));
	
	
	OnChangeRGB();
}

function ColorsTable(parentEl){
	var table = document.createElement('TABLE');
	table.setAttribute('cellPadding',0);
	table.setAttribute('cellSpacing',5);
	table.setAttribute('border',0);
	table.setAttribute('id','basic_color_table');
	
	var tBody = document.createElement('TBODY');
	table.appendChild(tBody);


	var hLen = 8;
	var vLen = 6;
	
	for(y = 0;y<vLen;y++)
	{
		var row = document.createElement('TR');
		for (x=0;x<hLen;x++)
		{
			var cell = document.createElement('TD');
			cell.style.backgroundColor = colorArr[y][x];
			cell.style.color = colorArr[y][x];
			cell.style.width = 20 + "px";
			cell.style.height = 15 + "px";
			cell.style.borderWidth = "1px";
			cell.style.borderColor = "#ffffff";

			cell.style.borderStyle = "solid";
			cell.setAttribute('id',"basic_color_"+x+"_"+y);
			
			onClickEvent = 
				function(col,colX,colY) {
					return (
						function (){
							fnOnColorClick(col,colX,colY);
						}
					); 
				} (colorArr[y][x],x,y);
		
			if (document.all) { //IE4 and up
				cell.attachEvent("onclick",onClickEvent);
			} else if (document.layers) {
				cell.addeventlistener("onclick",onClickEvent,false);
			} else if (document.getElementById) {
				cell.addEventListener("click",onClickEvent,false);
			} 
			
			var theData = document.createTextNode('.');
			cell.appendChild(theData);
			row.appendChild(cell);
		}
		tBody.appendChild(row);
	}

	document.getElementById(parentEl).appendChild(table);
//	document.body.appendChild(table);	
}

//////////////////////////////////////////////////////////////////////////
var curCustElX = 0;
var curCustElY = 0;

function fnAddCustColorClick(red,green,blue){
	cell = document.getElementById("custom_color_"+curCustElX+"_"+curCustElY);
	color = "#" + getHex(red)+getHex(green) + getHex(blue);
	cell.style.backgroundColor = color;
	cell.style.color = color;

	onClickEvent = 
		function(col,colX,colY) {
			return (
				function (){
					fnOnCustColorClick(col,colX,colY);
				}
			); 
		} (color,curCustElX,curCustElY);

	if (document.all) { //IE4 and up
		cell.attachEvent("onclick",onClickEvent);
	} else if (document.layers) {
		cell.addeventlistener("onclick",onClickEvent,false);
	} else if (document.getElementById) {
		cell.addEventListener("click",onClickEvent,false);
	} 

	setCookieColor("custom_color_"+curCustElX+"_"+curCustElY,color);	
}


function fnOnCustColorClick(color,x,y){
	cell = document.getElementById("custom_color_"+curCustElX+"_"+curCustElY);
	cell.style.borderColor = "#000000";
	
	curCustElX = x;
	curCustElY = y;
	cell = document.getElementById("custom_color_"+curCustElX+"_"+curCustElY);
	cell.style.borderColor = "#ff0000";
	document.getElementById('R_id').value = getNum(color.substr(1,2));
	document.getElementById('G_id').value = getNum(color.substr(3,2));
	document.getElementById('B_id').value = getNum(color.substr(5,2));
	OnChangeRGB();	
}

function CustColorsTable(parentEl){
	var table = document.createElement('TABLE');
	table.setAttribute('cellPadding',0);
	table.setAttribute('cellSpacing',5);
	table.setAttribute('border',0);
	table.setAttribute('id','cust_color_table');

	var tBody = document.createElement('TBODY');
	table.appendChild(tBody);

	var hLen = 8;
	var vLen = 2;
	
	for(y = 0;y<vLen;y++)
	{
		var row = document.createElement('TR');
		for (x=0;x<hLen;x++)
		{
			color = getCookieColor("custom_color_"+x+"_"+y);
			
			var cell = document.createElement('TD');
			cell.style.backgroundColor = color;
			cell.style.color = color;
			cell.style.width = 20 + "px";
			cell.style.height = 15 + "px";
			cell.style.borderWidth = "1px";
			cell.style.borderColor = "#000000";
			cell.style.borderStyle = "solid";
			cell.setAttribute('id',"custom_color_"+x+"_"+y);

			
			onClickEvent = 
				function(col,colX,colY) {
					return (
						function (){
							fnOnCustColorClick(col,colX,colY);
						}
					); 
				} (color,x,y);
		
			if (document.all) { //IE4 and up
				cell.attachEvent("onclick",onClickEvent);
			} else if (document.layers) {
				cell.addeventlistener("onclick",onClickEvent,false);
			} else if (document.getElementById) {
				cell.addEventListener("click",onClickEvent,false);
			} 
			
			var theData = document.createTextNode(".");
			cell.appendChild(theData);
			row.appendChild(cell);
		}
		tBody.appendChild(row);
	}

	document.getElementById(parentEl).appendChild(table);

	cell = document.getElementById("custom_color_"+curCustElX+"_"+curCustElY);
	cell.style.borderColor = "#ff0000";
	
//	document.body.appendChild(table);	
}



////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Dialog functionality
////////////////////////////////////////////////////////////////////////////////////////////////////////////

var colorsLeft = 10;
var colorsTop = 10 ;
var colorsWidth = 220;
var colorsHeight = 140;

var custColorsLeft = colorsLeft;
var custColorsTop = colorsTop + colorsHeight + 35;
var custColorsWidth = colorsWidth;
var custColorsHeight = 65;

var buttonsLeft = colorsLeft;
var buttonsTop = custColorsTop + custColorsHeight + 20;
var buttonsWidth = colorsWidth;
var buttonsHeight = 50;


var graphicsLeft = colorsLeft + colorsWidth + 20;
var graphicsTop = colorsTop;
var graphicsHeight = (HLSMAX/step)*visStep;

var valuesLeft = graphicsLeft;
var valuesTop = graphicsTop + graphicsHeight + 10;
/////////////////////////////////////////////////////
//relative to div_graphics
var selectLeft = 0;
var selectTop = 0;
var selectWidth = (HLSMAX/step)*visStep;
var selectHeight = graphicsHeight;

var selectPointerWidth = 20;
var selectPointerHeight = 20;

var sliderLeft = selectLeft + selectWidth + 10;
var sliderTop = selectTop;
var sliderWidth = 10;
var sliderHeight = selectHeight;

var sliderPointerWidth = 5;
var sliderPointerHeight = 9;

function fnVerifyNumber(strFieldId,maxVal){
	iValue = myParseInt(document.getElementById(strFieldId).value);
	if(iValue<0)
		document.getElementById(strFieldId).value = "0";
	else if(iValue>maxVal)
		document.getElementById(strFieldId).value=maxVal.toString();
	else if( iValue.toString() != document.getElementById(strFieldId).value)
		document.getElementById(strFieldId).value = myParseInt(document.getElementById(strFieldId).value).toString();
}


function fnGetColorByRelativePos(pos,relVal){
	return 	Math.round((pos/relVal)*HLSMAX);
}

function fnGetRelativePosByColor(col,relVal){
	return 	Math.round((col/HLSMAX)*relVal);
}



function getMouseXPos(e) {

	if (document.all) { //IE4 and up
		return (myParseInt(event.clientX) + myParseInt(document.body.scrollLeft));
	} else if (document.layers) {
		if(!e)
			e = window.event;
	    return myParseInt(e.pageX)
	} else if (document.getElementById) {
		if(!e)
			e = window.Event;
		return (myParseInt(e.clientX) + myParseInt(document.body.scrollLeft))
	} 

}
// <B style="color:black;background-color:#A0FFFF">Get</B> the vartical <B style="color:black;background-color:#ff9999">position</B> of the mouse
function getMouseYPos(e) {
	if (document.all) { //IE4 and up
		return (myParseInt(event.clientY) + myParseInt(document.body.scrollTop));
	} else if (document.layers) {
		if(!e)
			e = window.event;
	    return myParseInt(e.pageY);
	} else if (document.getElementById) { // mozilla
		if(!e)
			e = window.Event;
		return (myParseInt(e.clientY) + myParseInt(document.body.scrollTop));
	} 

}

function CalcSliderPos(e){
	parentLeft = document.getElementById('div_graphics').offsetLeft + document.getElementById('HSTable').offsetLeft;
	parentTop = document.getElementById('div_graphics').offsetTop + document.getElementById('HSTable').offsetTop;
	
	pos = getMouseYPos(e) - sliderTop - graphicsTop;
	Lum = fnGetColorByRelativePos(pos,sliderHeight);
	
	if(Lum<0){
		Lum = 0;
		pos = 0;
	}
	if(Lum>HLSMAX){
		Lum = HLSMAX;
		pos = sliderHeight;
	}
	
	if(Lum>HLSMAX || Lum<0)	
		return;


	slider = document.getElementById('div_slider');
	CalcLumByPos(pos);
//	slider.style.left = (getMouseXPos() - sliderPointerWidth - graphicsLeft)+ 'px';
	slider.style.top = (pos - sliderPointerHeight - 2 ) + 'px';
}

function CalcSelectPos(e){
	selectEl = document.getElementById('div_select');
	pos = getMouseXPos(e) - selectLeft - graphicsLeft;
	Hue = fnGetColorByRelativePos(pos,selectWidth);
	
	if(Hue<0){
		Hue = 0;
		pos = 0;
	}
	if(Hue>HLSMAX){
		Hue = HLSMAX;
		pos = selectWidth;
	}

	if(Hue>=0 && Hue<=HLSMAX){
		CalcHueByPos(pos);

		selectEl.style.left = (pos - selectPointerWidth/2) + 'px';
	}
	
	pos = getMouseYPos(e) - selectTop - graphicsTop;
	Sat = fnGetColorByRelativePos(pos,selectHeight);
	

	if(Sat<0){
		Sat = 0;
		pos = 0;
	}
	if(Sat>HLSMAX){
		Sat = HLSMAX;
		pos = selectWidth;
	}

	if(Sat>=0 && Sat<=HLSMAX){
		CalcSatByPos(pos);
		selectEl.style.top = (pos - selectPointerHeight/2) + 'px';
	}

}
///////////////////////////////////////////////////////////////////////
var curElementName;
function CaptureElement(elementDivName,elWidth,elHeight,parentLeft,parentTop,e){
	el = document.getElementById(elementDivName);
	if(el){
		mX = getMouseXPos(e);
		mY = getMouseYPos(e);
		if( 
			(el.offsetLeft + parentLeft <= mX && mX <= el.offsetLeft + parentLeft + elWidth) &&
			(el.offsetTop + parentTop <= mY && mY <= el.offsetTop + parentTop + elHeight) 
		){
			curElementName = elementDivName;
			return true;
		}
	}
	return false;
}

function CaptureSelectImg(e){
	parentLeft = document.getElementById('div_graphics').offsetLeft + document.getElementById('HSTable').offsetLeft;
	parentTop = document.getElementById('div_graphics').offsetTop + document.getElementById('HSTable').offsetTop;
	return CaptureElement('div_select',selectPointerWidth,selectPointerHeight,parentLeft,parentTop,e);
}

function CaptureSlideImg(e){
	parentLeft = document.getElementById('div_graphics').offsetLeft + document.getElementById('LTable').offsetLeft;
	parentTop = document.getElementById('div_graphics').offsetTop + document.getElementById('LTable').offsetTop + sliderPointerHeight/2;
	
	return CaptureElement('div_slider',sliderPointerWidth,sliderPointerHeight,parentLeft,parentTop,e);
}


function doMouseMove(e) {

	if (document.all) { //IE4 and up
		if(!e)
			e = event;
		btn = 1;	
		el = e.srcElement;
	} else if (document.layers) {
		if(!e)
			e = window.event;
		btn = 0;
		el = e.target;
	} else if (document.getElementById) {
		if(!e)
			e = window.Event;
		btn = 0;
		el = e.target;
	} 

	if (/*(e.button==btn) && */(curElementName!=null)){
		if(curElementName =='div_select'){
			CalcSelectPos(e);
			e.returnValue = false
			e.cancelBubble = true
		}else if(curElementName =='div_slider'){
			CalcSliderPos(e);
			e.returnValue = false
			e.cancelBubble = true
		}else{
		
		}
	}
}

function doDragStart(e, transferData, action) {
	if (document.all) { //IE4 and up
		e = event;
	} else if (document.layers) {
		if(!e)
			e = window.event;
	} else if (document.getElementById) {
		if(!e)
			e = window.Event;
	} 
	
	if ("IMG"==event.srcElement.tagName)
	  event.returnValue=false;
}

function doMouseDown(e) {
	if (document.all) { //IE4 and up
		if(!e)
			e = event;
		btn = 1;	
		el = e.srcElement;
	} else if (document.layers) {
		if(!e)
			e = window.event;
		btn = 0;
		el = e.target;
	} else if (document.getElementById) {
		if(!e)
			e = window.Event;
		btn = 0;
		el = e.target;
	} 

	
	if ((e.button==btn) && (el.tagName=="IMG")){
		if(!CaptureSelectImg(e)){
			if(CaptureSlideImg(e)){
				return false;
			}
		}
		else{
			return false;
		}
	}
}

document.ondragstart = doDragStart;
document.onmousedown = doMouseDown;

document.onmousemove = doMouseMove;
document.onmouseup = new Function("curElementName=null;	");


////////////////////////////////////////////////////////////////////////////////////

function fnOnLoad(){
/////////////////////////////////////
// Left part of dialog
	colorsEl = document.getElementById('div_colors');
	colorsEl.style.left = colorsLeft + 'px';
	colorsEl.style.top = colorsTop + 'px';
	colorsEl.style.width = colorsWidth  + 'px';
	colorsEl.style.height = colorsHeight + 'px';
	ColorsTable('div_colors');
	colorsEl.style.display = '';


	custColorsEl = document.getElementById('div_cust_colors');
	custColorsEl.style.left = custColorsLeft + 'px';
	custColorsEl.style.top = custColorsTop + 'px';
	custColorsEl.style.width = custColorsWidth + 'px';
	custColorsEl.style.height = custColorsHeight + 'px';
	CustColorsTable('div_cust_colors');
	custColorsEl.style.display = '';

	buttonsEl = document.getElementById('div_buttons');
	buttonsEl.style.left = buttonsLeft + 'px';
	buttonsEl.style.top = buttonsTop + 'px';
	buttonsEl.style.width = buttonsWidth + 'px';
	buttonsEl.style.height = buttonsHeight + 'px';
	buttonsEl.style.display='';
////////////////////////////////////////////////////
	selectEl = document.getElementById('HSTable');
	selectEl.style.left = selectLeft + 'px';
	selectEl.style.top = selectLeft + 'px';
	selectEl.style.width = selectWidth + 'px';
	selectEl.style.height = selectHeight + 'px';

	sliderEl = document.getElementById('LTable');
	sliderEl.style.left = sliderLeft + 'px';
	sliderEl.style.top = sliderTop + 'px';
	sliderEl.style.width = sliderWidth + 'px';
	sliderEl.style.height = sliderHeight + 'px';


	slideEl = document.getElementById('div_slider');
	slideEl.style.left = (sliderWidth + 2) + 'px';
	slideEl.style.top = (sliderHeight - sliderPointerHeight - 2) + 'px';

////////////////////////////////
	selectPointerEl = document.getElementById('div_select');
	selectPointerEl.style.left = '-' + selectPointerWidth/2 + 'px';
	selectPointerEl.style.top = '-' + selectPointerHeight/2 + 'px';
///////////////////////////////////////////////////////////
	HSTable(160,'HSTable');

	graphicsEl = document.getElementById('div_graphics');
	graphicsEl.style.left = graphicsLeft + 'px';
	graphicsEl.style.top = graphicsTop + 'px';
	graphicsEl.style.display='';

	valuesEl = document.getElementById('div_values');
	valuesEl.style.left = valuesLeft + 'px';
	valuesEl.style.top = valuesTop + 'px';
	valuesEl.style.display='';
/////////////////////////////////////////////////////////////////////
	var prevColor = ""

	if (document.all) { //IE4 and up
		prevColor = dialogArguments[0];
		window.opener = dialogArguments[1];
		g_param = dialogArguments[2];
		document.getElementById('g_controlid').value= dialogArguments[3];
		document.getElementById('g_i_id').value= dialogArguments[4];
	} else if (document.layers) {
		
	} else if (document.getElementById) { //mozilla
		var url = unescape(window.location.href);
		var parArray = url.split('?');
		if(parArray.length==2){
			var paramsArray = parArray[1].split('&');
			prevColor = paramsArray[0];
			g_param = paramsArray[1];

    document.getElementById('g_controlid').value = paramsArray[2];
    document.getElementById('g_i_id').value= paramsArray[3];
		}
	} 

	if(prevColor){
		if(prevColor.length == 7){
			if(prevColor.substr(0,1) == '#'){
				document.getElementById('R_id').value = getNum(prevColor.substr(1,2));
				document.getElementById('G_id').value = getNum(prevColor.substr(3,2));
				document.getElementById('B_id').value = getNum(prevColor.substr(5,2));
			
				
				OnChangeRGB();
			}
		}
	}
	else{
		document.getElementById('H_id').value = 0;
		document.getElementById('S_id').value = HLSMAX;
		document.getElementById('L_id').value = 0;

		RGBByHSL();
		
		LTable(0,HLSMAX,'LTable');
	}
	
}



function CalcHueByPos(pos){
	document.getElementById('H_id').value = fnGetColorByRelativePos(pos,selectWidth);

	OnChangeHS();
}

function CalcPosByHue(){
	
	Hue = document.getElementById('H_id').value;
	
	selectEl = document.getElementById('div_select');
	selectEl.style.left = fnGetRelativePosByColor(Hue,selectWidth) - selectPointerWidth/2 + 'px';

	OnChangeHS();
}

function CalcSatByPos(pos){
	document.getElementById('S_id').value = HLSMAX - fnGetColorByRelativePos(pos,selectHeight);
	
	OnChangeHS();
}

function CalcPosBySat(){
	Sat = document.getElementById('S_id').value;

	selectEl = document.getElementById('div_select');
	selectEl.style.top = fnGetRelativePosByColor(HLSMAX - Sat,selectHeight) - selectPointerHeight/2 + 'px';
	
	OnChangeHS();
}

function CalcLumByPos(pos){
	document.getElementById('L_id').value = HLSMAX - fnGetColorByRelativePos(pos,sliderHeight);
	OnChangeL();
}


function CalcPosByLum(){
	Lum = document.getElementById('L_id').value;

	slider = document.getElementById('div_slider');
	slider.style.top = fnGetRelativePosByColor(HLSMAX - Lum,sliderHeight) - sliderPointerHeight  + 'px';

	OnChangeL();
}

function RGBByHSL(){
	H = myParseInt(document.getElementById('H_id').value);
	L = myParseInt(document.getElementById('L_id').value);
	S = myParseInt(document.getElementById('S_id').value);

	res = HLStoRGB(H,L,S);

	document.getElementById('R_id').value = res[0];
	document.getElementById('G_id').value = res[1];
	document.getElementById('B_id').value = res[2];
	
	setPreviewColor();
}

function HSLByRGB(){
	R = myParseInt(document.getElementById('R_id').value);
	G = myParseInt(document.getElementById('G_id').value);
	B = myParseInt(document.getElementById('B_id').value);

	res = RGBtoHLS(R,G,B);

	document.getElementById('H_id').value = res[0];
	document.getElementById('L_id').value = res[1];
	document.getElementById('S_id').value = res[2];
	
	setPreviewColor();
}

function OnChangeHS(){
	RGBByHSL();
	
	H = myParseInt(document.getElementById('H_id').value);
	S = myParseInt(document.getElementById('S_id').value);
	
	LTable(H,S,'LTable');
}

function OnChangeL(){
	RGBByHSL();
}

function OnChangeRGB(){
	HSLByRGB();

	Hue = myParseInt(document.getElementById('H_id').value);
	Sat = myParseInt(document.getElementById('S_id').value);

	selectEl = document.getElementById('div_select');
	selectEl.style.left = (fnGetRelativePosByColor(Hue,selectWidth) - selectPointerWidth/2) + 'px';
	selectEl.style.top = (fnGetRelativePosByColor(HLSMAX - Sat,selectWidth) - selectPointerHeight/2) + 'px';
	LTable(Hue,Sat,'LTable');

	Lum = myParseInt(document.getElementById('L_id').value);

	slider = document.getElementById('div_slider');
	slider.style.top = fnGetRelativePosByColor(HLSMAX - Lum,sliderHeight) - sliderPointerHeight + 'px';
	
}

function setPreviewColor(){
	R = myParseInt(document.getElementById('R_id').value);
	G = myParseInt(document.getElementById('G_id').value);
	B = myParseInt(document.getElementById('B_id').value);
	
	prevEl = document.getElementById("previewColor");
	prevEl.style.backgroundColor = '#'+getHex(R)+getHex(G)+getHex(B);
}

function EndDialog(val){
	var retVal = "";
	if(val=="OK"){
		R = myParseInt(document.getElementById('R_id').value);
		G = myParseInt(document.getElementById('G_id').value);
		B = myParseInt(document.getElementById('B_id').value);
		retVal = "OK!" + "#" + getHex(R)+getHex(G) + getHex(B);		
		window.close();
	}
	
	if (document.all) { //IE4 and up
		window.returnValue = retVal;
		
	} else if (document.layers) {
		
	} else if (document.getElementById) { //mozilla
		if(window.opener.OnChangeColor){
			window.opener.OnChangeColor(fnProcessReturnValue(retVal),g_param,document.getElementById('g_controlid').value,document.getElementById('g_i_id').value);			
		}
		else{
			alert('No OnChangeColor function present to call!');
		}
			
	} 

	if(window.opener){
		if(window.opener.OnChangeColor){
			window.opener.OnChangeColor(fnProcessReturnValue(retVal),g_param,document.getElementById('g_controlid').value,document.getElementById('g_i_id').value);
			
		}
		else{
			alert('No OnChangeColor function present to call!');
		}
		
	}
document.getElementById('g_controlid').value="";
document.getElementById('g_i_id').value="";
	window.close();
}

var isShownCustColors = false;
function ShowHideCustColors(){
	
	if (document.all) { //IE4 and up
		if(isShownCustColors){
			window.dialogWidth = 245 + 'px';
			document.getElementById('DefineCustomColors').value = 'Define Custom Colors >>';
		}
		else{
			window.dialogWidth = 495 + 'px';
			document.getElementById('DefineCustomColors').value = 'Define Custom Colors <<';
		}
	} else if (document.layers) {
	} else if (document.getElementById) { //mozilla
		if(isShownCustColors){
			window.resizeTo(245,380);
			document.getElementById('DefineCustomColors').value = 'Define Custom Colors >>';
		}
		else{
			window.resizeTo(495,380);
			document.getElementById('DefineCustomColors').value = 'Define Custom Colors <<';
		}
		
	} 
	isShownCustColors = !isShownCustColors;
}


