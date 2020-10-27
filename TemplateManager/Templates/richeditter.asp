<HTML><HEAD>
<!--#include file = "adovbs.inc"-->
<!--#include file = "dsn.asp"-->
<!--#include file = "functions.asp"-->

<%
Response.Expires = 60
Response.Expiresabsolute = Now() - 1
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "no-cache"
%>

<!--#include file = "FaxModule.asp"-->
<!--#include file = "SaveModule.asp"-->
<!--#include file = "MailModule.asp"-->
<!--#include file = "PacketingScript.asp"-->

<%
dim CID
CID=Request("Case_ID")
Session("UID") = Request("User_id")
Session("User_ID") = Request("User_id")
Session("CaseIDForDocs") = Request("Case_id")
Session("CompanyName")=Request("CompanyName")
Session("CompanyID")=Request("CompanyID")
dim InsSql
dim docname
docname=Request("SelDoc")
Session("FileName") = docname
'Response.Write docname
' docname="Document " & Mid(docname,1,Instr(docname,".")-1)
docname="Document " & docname
InsNotes2="Insert into tblNotes values ('Document  "&docname&" printed','G',0,'"&Cid&"','"&Now()&"','"&Session("User_Id")&"')"

'response.write dataconn.connctionstring
'response.end
 DataConn.Execute InsNotes2
 '-------------------------User Options--------------------

'dim rsClaim
'set rsUser=Server.CreateObject("ADODB.RecordSet")
'dim SWhere
'swhere = "exec sp_AllClaim @cid ='"&CID& "'"
'rsUser.open swhere,DataConn

dim rsPath
set rsPath=Server.CreateObject("ADODB.RecordSet")
dim szQuery

if Request("Type") = "S" then
	szQuery = "select parametervalue from tblApplicationSettings where parametername='SavedDocsPath'"
else
	szQuery = "select parametervalue from tblApplicationSettings where parametername='TemplateDocsPath'"
end if

rsPath.open szQuery,DataConn
Dim HtmString
Dim errorMessage 'as String
dim fpath,fso
'fpath=Server.MapPath("/casemanager/iisdocs/"&Request("SelDoc"))
fpath = rsPath("parametervalue") & Request("SelDoc") & ".htm"

'Response.Write(fpath)
'Response.End()

set fso=Server.CreateObject("Scripting.FileSystemObject")
Set TextStream=fso.OpenTextFile(fpath, 1)
HtmString=TextStream.ReadAll()
Set TextSream = Nothing
Set fso = nothing
errorMessage = "0"
HtmStringAll=""
'HtmString2 = HtmStringAll  & ReplaceAll(HtmString,rsUser)
HtmString2 = HtmString

 
 '------------------GET THE HEADER--------------------------------------
 ' HtmString = PacketingHeader(HtmString, rsUser)

%>


<META content="HTML 4.0" name="vs_targetSchema">
<META content="Microsoft FrontPage 5.0" name="GENERATOR">
<LINK rel="StyleSheet" type="text/css" href="richedit.css">
<LINK rel="StyleSheet" type="text/css" href="syntax.css">
<LINK rel="StyleSheet" type="text/css" href="custom.css">
<SCRIPT language="JavaScript" src="rte_interface.js"></SCRIPT>
<SCRIPT language="JavaScript" src="rte_debug.js"></SCRIPT>
<SCRIPT language="JavaScript" src="rte.js"></SCRIPT>
<SCRIPT language="JavaScript" src="rte_codesweep.js"></SCRIPT>
<SCRIPT language="JavaScript" src="rte_editmode.js"></SCRIPT>
<SCRIPT language="JavaScript" src="rte_history.js"></SCRIPT>
<SCRIPT language="JavaScript">
// This defines the scriptlets public interface.  See rte_interface.js for
// the actual interface definition.
var public_description =  new RichEditor();

// Initialise the editor as soon as the window is loaded.
window.attachEvent("onload", initEditor);

function print()
{
	strHtml = doc.innerHTML;
	selDoc = document.form1.selDoc.value;
	document.open();
	document.write("<html><head></head><Body onload='window.print();'>"); 
	document.write(strHtml);
	document.write("</Body></html>");
	document.close();
}

function save(){
	strHtml = doc.innerHTML
	var answer;
	answer = prompt("Please Enter Filename");
	if (answer.length > 0){
		document.form1.HtmString.value=strHtml;
		
		document.form1.filename.value=answer;
		document.form1.savevar.value="set";
		//alert(answer);
		document.form1.submit();
		return true;
	}
	else{
		return false;
	}

	document.form1.savevar.value="set"
	document.form1.submit();
	return true;
}




function fax()
{
	//alert('here am I');
	strHtml = doc.innerHTML
	var answer 
	answer = prompt("Please enter fax.", "Type fax no here");
	if (answer.length > 0){
		document.form1.HtmString.value=strHtml;
		document.form1.phno.value=answer;
		//alert(answer);
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
  	document.body.insertAdjacentHTML("beforeEnd","<object id=\"printWB\" width=0 height=0 \ 	classid=\"clsid:8856F961-340A-11D0-A96B-00C04FD705A2\"></object>");
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
</SCRIPT>
<title><%=Request("seldoc")%></title>
</HEAD>
<BODY style="BORDER-RIGHT: 1px inset; BORDER-TOP: 1px inset; BORDER-LEFT: 1px inset; BORDER-BOTTOM: 1px inset" leftMargin="0" topMargin="0" scroll="no" onload="init()" UNSELECTABLE="on">
<table unselectable="on" height="100%" cellspacing="0" cellpadding="0" width="100%" bgcolor="buttonface" border="0">
  <tr ondragstart="handleDrag(0)" onmouseup="press(false)" onmousedown="press(true)" onmouseover="hover(true)" onmouseout="hover(false)">
    <td class="rebar"><nobr><span class="toolbar">
		<img class="spacer" src="spacer.gif" width="2">
		<span class="start"></span>
		<a href="#" onclick="javascript:return save();"><img id="btsave" alt="save" src="images/icon_save.gif" align="absMiddle" width="20" height="20"></a>
		<img id="btprint" onclick="print()" alt="print" src="images/print.gif" align="absMiddle" width="20" height="20">
		<a href="#" onclick="javascript:return fax();"><img id="btfax" alt="fax" src="images/fax.gif" align="absMiddle" width="32" height="20"></a>
		<a href="#" onclick="javascript:return mail();"><img id="btmail" alt="mail" src="images/mail.gif" align="absMiddle" width="20" height="20"></a>
		<img id="btnCut"	onclick="doStyle('Cut')" alt="Cut" src="images/icon_cut.gif" align="absMiddle" width="20" height="20">
		<img id="btnCopy"	onclick="doStyle('Copy')" alt="Copy" src="images/icon_copy.gif" align="absMiddle" width="20" height="20">
		<img id="btnPaste"	onclick="doStyle('Paste')" alt="Paste" src="images/icon_paste.gif" align="absMiddle" width="20" height="20">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		<img id="btnSpell"  onclick="" alt="Spell Check" src="images/icon_spell.gif" align="absMiddle" width="20" height="20">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		<img id="btnSelect" onclick="doStyle('SelectAll')" alt="Select All" src="images/icon_select_all.gif" align="absMiddle" width="20" height="20">
		<img id="btnRemove" onclick="doStyle('RemoveFormat')" alt="Remove Formatting" src="images/icon_rem_formatting.gif" align="absMiddle" width="20" height="20">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		<img id="btnLink"   onclick="link(true)" alt="Insert Link" src="images/icon_ins_link.gif" align="absMiddle" width="20" height="20">
		<img id="btnRemLink" onclick="link(false)" alt="Remove Link" src="images/icon_rem_link.gif" align="absMiddle" width="20" height="20">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		</span></nobr></td></tr>
  <tr ondragstart="handleDrag(0)" onmouseup="press(false)" onmousedown="press(true)" onmouseover="hover(true)" onmouseout="hover(false)">
    <td class="rebar"><nobr><span class="toolbar">
		<img class="spacer" src="spacer.gif" width="2"><span class="start"></span>
		<img id="btnBold"     onclick="doStyle('bold')" alt="Bold" src="images/icon_bold.gif" align="absMiddle" width="20" height="20">
		<img id="btnItalic"   onclick="doStyle('italic')" alt="Italic" src="images/icon_italic.gif" align="absMiddle" width="20" height="20">
		<img id="btnUnderline"  onclick="doStyle('underline')" alt="Underline" src="images/icon_underline.gif" align="absMiddle" width="20" height="20">
		<img id="btnStrikethrough"  onclick="doStyle('strikethrough')" alt="Strikethrough" src="images/icon_strikethrough.gif" align="absMiddle" width="20" height="20">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		<img id="btnLeftJustify"  onclick="doStyle('JustifyLeft')" alt="Align Left" src="images/icon_left.gif" align="absMiddle" width="20" height="20">
		<img id="btnCenter"   onclick="doStyle('JustifyCenter')" alt="Center" src="images/icon_center.gif" align="absMiddle" width="20" height="20">
		<img id="btnRightJustify"  onclick="doStyle('JustifyRight')" alt="Align Right" src="images/icon_right.gif" align="absMiddle" width="20" height="20">
		<img id="btnFullJustify" onclick="doStyle('JustifyFull')" alt="Align Block" src="images/icon_block.gif" align="absMiddle" width="20" height="20">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		<img id="btnNumList"  onclick="doStyle('InsertOrderedList')" alt="Numbered List" src="images/icon_numlist.gif" align="absMiddle" width="20" height="20">
		<img id="btnBulList"  onclick="doStyle('InsertUnorderedList')" alt="Buletted List" src="images/icon_bullist.gif" align="absMiddle" width="20" height="20">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		<img id="btnOutdent"  onmousedown="doStyle('Outdent')" alt="Decrease Indent" src="images/icon_outdent.gif" align="absMiddle" width="20" height="20">
		<img id="btnIndent"   onmousedown="doStyle('Indent')" alt="Increase Indent" src="images/icon_indent.gif" align="absMiddle" width="20" height="20">
		<span id="featureHistory">
		<img class="spacer" src="spacer.gif" width="2"><span class="sep"></span>
		<img id="btnPrev" onmousedown="goHistory(-1)" alt="History back"    src="images/icon_undo.gif" align="absMiddle" width="20" height="20">
		<img id="btnNext" onmousedown="goHistory(1)"  alt="History forward" src="images/icon_redo.gif" align="absMiddle" width="20" height="20">
		</span>
		</span></nobr></td></tr>
	<tr id="featureStyleBar" ondragstart="handleDrag(0)" onmouseup="press(false)" onmousedown="press(true)" onmouseover="hover(true)" onmouseout="hover(false)">
    <td class="rebar"><nobr><span class="toolbar">
		<img class="spacer" src="spacer.gif" width="2"><span class="start"></span>&nbsp;
		<span id="featureStyle">
			<span class="label">Style</span>
			<select name="" id="ctlStyle" class="button" onchange="addTag(this)">
			</select>
			<span class="sep"></span>
		</span>
		<span id="featureFont">
			<span class="label">Font</span>
			<select hidefocus class="button" id="ctlFont"  onchange="sel(this)">
				<option selected></option>
				<option value="Arial">Arial</option>
				<option value="Arial Black">Arial Black</option>
				<option value="Comic Sans MS">Comic Sans MS</option>
				<option value="Courier New">Courier New</option>
				<option value="Lucida Console">Lucida Console</option>
				<option value="MS Sans Serif">MS Sans Serif</option>
				<option value="Tahoma">Tahoma</option>
				<option value="Times New Roman">Times New Roman</option>
				<option value="Trebuchet MS">Trebuchet MS</option>
				<option value="Verdana">Verdana</option>
				<option value="Wingdings">Wingdings</option>
				<option value="Jokerman">Jokerman</option>
			</select>
		</span>
		<span id="featureFontSize">
			<span class="sep"></span>
			<span class="label">Size</span>
			<select hidefocus class="button" id="ctlSize"  onchange="sel(this)">
				<option selected></option>
				<option value="1">xx-small</option>
				<option value="2">x-small</option>
				<option value="3">small</option>
				<option value="4">medium</option>
				<option value="5">large</option>
				<option value="6">x-large</option>
				<option value="7">xx-large</option>
			</select>
		</span>
		<span id="featureColour">
			<span class="sep"></span>
			<img id="btnText"  onclick="pickColor('ForeColor')" alt="Text Color" src="images/icon_color_text.gif" align="absMiddle" width="36" height="20">
			<img id="btnFill"  onclick="pickColor('BackColor')" alt="Background Color" src="images/icon_color_fill.gif" align="absMiddle" width="36" height="20">
		</span>
		<span id="featureSource">
			<img class="spacer" src="spacer.gif" width="2"><span class="start"></span>&nbsp;
			<span class="label">Source</span>
			<input class="checkbox" hidefocus type="checkbox" name="switchMode" onclick="setEditMode(switchMode)">
		</span>
	</span></nobr></td>
</tr>
<!-- Fields are inserted here -->
<tr id="rebarBottom">
    <td class="spacer" height="2"><img height="1" src="spacer.gif" align="left"></td></tr>
  <tr>
    <td class="textedit" valign="top" height="100%">
		<div class="document" id="doc" onkeyup="reset()" contenteditable="false" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" onclick="reset()" height="100%" width="100%">
			<%=HtmString2%>
		</div>
	</td></tr>
</table>
<object id="color" data="colorchooser.html" type="text/x-scriptlet" VIEWASTEXT>
</object>
<SCRIPT for="color" event="onscriptletevent(name, data)">
	setColor(name, data);
</SCRIPT>
<form name = 'form1' id='form1' method='post' action="richeditter.asp">
	<input type = hidden name ='HtmString'>
	<input type = hidden name ='phno'>
	<input type = hidden name ='mailvar'>
	<input type = hidden name ='savevar'>
	<input type = hidden name ='filename'>
	<input type = hidden name ='selDoc' value='<%=Request("selDoc")%>'>
</form>
</BODY></HTML>