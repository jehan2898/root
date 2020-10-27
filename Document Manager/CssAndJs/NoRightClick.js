msg = "You can't right click on this web page!";
bV  = parseInt(navigator.appVersion)
bNS = navigator.appName=="Netscape"
bIE = navigator.appName=="Microsoft Internet Explorer"
function nrc(e) {
if (bNS && e.which > 1){
alert(msg)
return false
} else if (bIE && (event.button >1)) {
alert(msg)
return false;
}
}
document.onmousedown = nrc;
if (document.layers) window.captureEvents(Event.MOUSEDOWN);
if (bNS && bV<5) window.onmousedown = nrc;


function stopPropagation(e) 
{ 
e = e¦¦event; /* get IE event ( not passed ) */ 
alert(e.keyCode); 
e.stopPropagation? e.stopPropagation() : e.cancelBubble = true; 
} 

var x = false; 
function checkShortcut(e) 
{ 

stopPropagation(e); 
if(event.keyCode==8 ¦¦ event.keyCode==13) 
{ 
alert("backspace"); 
if( x ) 
return true; 
return false; 
} 
} 
function attachHandlerStart() 
{ 
var dlist = document.getElementsByTagName('html'); 
for( var i = 0; i < dlist.length; i++ ) 
{ 
attachHandler(dlist.item(i)); 
} 
} 
function attachHandler(node) 
{ 
// Iterate the dom, attaching the onkeypress event to all of the items 
if( node.nodeType == 1 ) // Element 
{ 
node.onkeydown = checkShortcut; 
alert('Attached checkShortcut to '+node.nodeName); 

// Iterate the children of this node 
var children = node.childNodes; 
for( var i = 0; i < children.length; i++ ) 
{ 
attachHandler(children.item(i)); 
} 
} 
} 


