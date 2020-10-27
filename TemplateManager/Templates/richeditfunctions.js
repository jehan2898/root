// This defines the scriptlets public interface.  See rte_interface.js for
// the actual interface definition.
var public_description =  new RichEditor();

// Initialise the editor as soon as the window is loaded.
window.attachEvent("onload", initEditor);

function print()
{
	//document.form1.htmlString.value =  doc.innerHtml ;
	//alert(document.body.form1.htmlString.value)
	//document.form1.action = "print.asp?HtmString=" + doc.innerHTML
	//document.form1.submit();
	strHtml = doc.innerHTML
	document.open();
	document.write("<html><Body onload='window.print();'>"); 
	document.write(strHtml);
	document.write("</Body></html>");
	document.close();
}

function fax()
{
	//alert('here am I');
	strHtml = doc.innerHTML
	var answer 
	answer = prompt("Please enter fax.", "Type fax no here");
	
	if (answer){
		document.form1.HtmString.value=strHtml;
		document.form1.phno.value=answer;
		document.form1.submit();
		return true;
	}
	else{
	
		return false;
	}
}

function mail()
{
	//alert('here am I');
	strHtml = doc.innerHTML
	var answer 
	answer = prompt("Please enter Email ID.", "Email ID");
	
	if (answer){
		document.form1.HtmString.value=strHtml;
		document.form1.mailvar.value=answer;
		document.form1.submit();
		return true;
	}
	else{
	
		return false;
	}
}


function print2()
{
  	document.body.insertAdjacentHTML("beforeEnd",
    	"<object id=\"printWB\" width=0 height=0 \ 	classid=\"clsid:8856F961-340A-11D0-A96B-00C04FD705A2\"></object>");

  	printFireEvent(window, doc, "onbeforeprint");
  	window.focus();
  	window.printHelper = printHelper;
  	setTimeout("window.printHelper()", 0);
}

function printFireEvent(frame, obj, name) {
  var handler = obj[name];
  switch ( typeof(handler) ) {
    case "string": frame.execScript(handler); break;
    case "function": handler();
  }
}
