<%@ Page Language="vb" AutoEventWireup="false" CodeFile="richeditter.aspx.vb" Inherits="richEditter" %>
<HTML>
	<HEAD>
		<title>
			<%=Request.Params.Get("seldoc")%>
		</title>
		<META content="HTML 4.0" name="vs_targetSchema">
		<META content="Microsoft FrontPage 5.0" name="GENERATOR">
		<LINK href="richedit.css" type="text/css" rel="StyleSheet">
			<LINK href="syntax.css" type="text/css" rel="StyleSheet">
				<LINK href="custom.css" type="text/css" rel="StyleSheet">
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


					</SCRIPT>
	</HEAD>
	<BODY style="BORDER-RIGHT: 1px inset; BORDER-TOP: 1px inset; BORDER-LEFT: 1px inset; BORDER-BOTTOM: 1px inset"
		leftMargin="0" topMargin="0" scroll="no" onload="init();" UNSELECTABLE="on">
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="buttonface"
			border="0" unselectable="on">
			<TR onmouseup="press(false)" onmousedown="press(true)" ondragstart="handleDrag(0)" onmouseover="hover(true)"
				onmouseout="hover(false)">
				<TD class="rebar"><NOBR><SPAN class="toolbar"><IMG class="spacer" src="spacer.gif" width="2">
							<SPAN class="start"></SPAN>
							<A onclick="javascript:return save();" href="#"><IMG id="btsave" height="20" alt="save" src="images/icon_save.gif" width="20" align="absMiddle"></A>
							<IMG id="btprint" onclick="print()" height="20" alt="print" src="images/print.gif" width="20"
								align="absMiddle"> <A onclick="javascript:return fax();" href="#"><IMG id="btfax" height="20" alt="fax" src="images/fax.gif" width="32" align="absMiddle"></A>
							<A onclick="javascript:return mail();" href="#"><IMG id="btmail" height="20" alt="mail" src="images/mail.gif" width="20" align="absMiddle"></A>
							<IMG id="btnCut" onclick="doStyle('Cut')" height="20" alt="Cut" src="images/icon_cut.gif"
								width="20" align="absMiddle"> <IMG id="btnCopy" onclick="doStyle('Copy')" height="20" alt="Copy" src="images/icon_copy.gif"
								width="20" align="absMiddle"> <IMG id="btnPaste" onclick="doStyle('Paste')" height="20" alt="Paste" src="images/icon_paste.gif"
								width="20" align="absMiddle"> <IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
							<IMG id="btnSpell" onclick="" height="20" alt="Spell Check" src="images/icon_spell.gif"
								width="20" align="absMiddle"> <IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
							<IMG id="btnSelect" onclick="doStyle('SelectAll')" height="20" alt="Select All" src="images/icon_select_all.gif"
								width="20" align="absMiddle"> <IMG id="btnRemove" onclick="doStyle('RemoveFormat')" height="20" alt="Remove Formatting"
								src="images/icon_rem_formatting.gif" width="20" align="absMiddle"> <IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
							<IMG id="btnLink" onclick="link(true)" height="20" alt="Insert Link" src="images/icon_ins_link.gif"
								width="20" align="absMiddle"> <IMG id="btnRemLink" onclick="link(false)" height="20" alt="Remove Link" src="images/icon_rem_link.gif"
								width="20" align="absMiddle"> <IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
						</SPAN></NOBR></TD>
			</TR>
			<TR onmouseup="press(false)" onmousedown="press(true)" ondragstart="handleDrag(0)" onmouseover="hover(true)"
				onmouseout="hover(false)">
				<TD class="rebar"><NOBR><SPAN class="toolbar"><IMG class="spacer" src="spacer.gif" width="2"><SPAN class="start"></SPAN>
							<IMG id="btnBold" onclick="doStyle('bold')" height="20" alt="Bold" src="images/icon_bold.gif"
								width="20" align="absMiddle"> <IMG id="btnItalic" onclick="doStyle('italic')" height="20" alt="Italic" src="images/icon_italic.gif"
								width="20" align="absMiddle"> <IMG id="btnUnderline" onclick="doStyle('underline')" height="20" alt="Underline" src="images/icon_underline.gif"
								width="20" align="absMiddle"> <IMG id="btnStrikethrough" onclick="doStyle('strikethrough')" height="20" alt="Strikethrough"
								src="images/icon_strikethrough.gif" width="20" align="absMiddle"> <IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
							<IMG id="btnLeftJustify" onclick="doStyle('JustifyLeft')" height="20" alt="Align Left"
								src="images/icon_left.gif" width="20" align="absMiddle"> <IMG id="btnCenter" onclick="doStyle('JustifyCenter')" height="20" alt="Center" src="images/icon_center.gif"
								width="20" align="absMiddle"> <IMG id="btnRightJustify" onclick="doStyle('JustifyRight')" height="20" alt="Align Right"
								src="images/icon_right.gif" width="20" align="absMiddle"> <IMG id="btnFullJustify" onclick="doStyle('JustifyFull')" height="20" alt="Align Block"
								src="images/icon_block.gif" width="20" align="absMiddle"> <IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
							<IMG id="btnNumList" onclick="doStyle('InsertOrderedList')" height="20" alt="Numbered List"
								src="images/icon_numlist.gif" width="20" align="absMiddle"> <IMG id="btnBulList" onclick="doStyle('InsertUnorderedList')" height="20" alt="Buletted List"
								src="images/icon_bullist.gif" width="20" align="absMiddle"> <IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
							<IMG onmousedown="doStyle('Outdent')" id="btnOutdent" height="20" alt="Decrease Indent"
								src="images/icon_outdent.gif" width="20" align="absMiddle"> <IMG onmousedown="doStyle('Indent')" id="btnIndent" height="20" alt="Increase Indent"
								src="images/icon_indent.gif" width="20" align="absMiddle">
							<SPAN id="featureHistory">
								<IMG class="spacer" src="spacer.gif" width="2"><SPAN class="sep"></SPAN>
								<IMG onmousedown="goHistory(-1)" id="btnPrev" height="20" alt="History back" src="images/icon_undo.gif"
									width="20" align="absMiddle"> <IMG onmousedown="goHistory(1)" id="btnNext" height="20" alt="History forward" src="images/icon_redo.gif"
									width="20" align="absMiddle">
							</SPAN>
						</SPAN></NOBR></TD>
			</TR>
			<TR onmouseup="press(false)" onmousedown="press(true)" id="featureStyleBar" ondragstart="handleDrag(0)"
				onmouseover="hover(true)" onmouseout="hover(false)">
				<TD class="rebar"><NOBR><SPAN class="toolbar"><IMG class="spacer" src="spacer.gif" width="2"><SPAN class="start"></SPAN>&nbsp; <SPAN id="featureStyle">
								<SPAN class="label">Style</SPAN>
								<SELECT class="button" id="ctlStyle" onchange="addTag(this)" name="">
								</SELECT>
								<SPAN class="sep"></SPAN>
							</SPAN><SPAN id="featureFont">
								<SPAN class="label">Font</SPAN>
								<SELECT class="button" id="ctlFont" hideFocus onchange="sel(this)">
									<OPTION selected></OPTION>
									<OPTION value="Arial">Arial</OPTION>
									<OPTION value="Arial Black">Arial Black</OPTION>
									<OPTION value="Comic Sans MS">Comic Sans MS</OPTION>
									<OPTION value="Courier New">Courier New</OPTION>
									<OPTION value="Lucida Console">Lucida Console</OPTION>
									<OPTION value="MS Sans Serif">MS Sans Serif</OPTION>
									<OPTION value="Tahoma">Tahoma</OPTION>
									<OPTION value="Times New Roman">Times New Roman</OPTION>
									<OPTION value="Trebuchet MS">Trebuchet MS</OPTION>
									<OPTION value="Verdana">Verdana</OPTION>
									<OPTION value="Wingdings">Wingdings</OPTION>
									<OPTION value="Jokerman">Jokerman</OPTION>
								</SELECT>
							</SPAN><SPAN id="featureFontSize">
								<SPAN class="sep"></SPAN>
								<SPAN class="label">Size</SPAN>
								<SELECT class="button" id="ctlSize" hideFocus onchange="sel(this)">
									<OPTION selected></OPTION>
									<OPTION value="1">xx-small</OPTION>
									<OPTION value="2">x-small</OPTION>
									<OPTION value="3">small</OPTION>
									<OPTION value="4">medium</OPTION>
									<OPTION value="5">large</OPTION>
									<OPTION value="6">x-large</OPTION>
									<OPTION value="7">xx-large</OPTION>
								</SELECT>
							</SPAN><SPAN id="featureColour">
								<SPAN class="sep"></SPAN>
								<IMG id="btnText" onclick="pickColor('ForeColor')" height="20" alt="Text Color" src="images/icon_color_text.gif"
									width="36" align="absMiddle"> <IMG id="btnFill" onclick="pickColor('BackColor')" height="20" alt="Background Color"
									src="images/icon_color_fill.gif" width="36" align="absMiddle">
							</SPAN><SPAN id="featureSource"><IMG class="spacer" src="spacer.gif" width="2"><SPAN class="start"></SPAN>&nbsp; <SPAN class="label">Source</SPAN> <INPUT class="checkbox" hideFocus onclick="setEditMode(switchMode)" type="checkbox" name="switchMode"> </SPAN></SPAN></NOBR></TD>
			</TR> <!-- Fields are inserted here -->
			<TR id="rebarBottom">
				<TD class="spacer" height="2"><IMG height="1" src="spacer.gif" align="left"></TD>
			</TR>
			<TR>
				<TD class="textedit" vAlign="top" height="100%">
					<DIV class="document" id="doc" onkeyup="reset()" contentEditable="false" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"
						onclick="reset()" width="100%" height="100%">
						<%=HtmString2%>
					</DIV>
				</TD>
			</TR>
		</TABLE>
		<OBJECT id="color" type="text/x-scriptlet" data="colorchooser.html" VIEWASTEXT>
		</OBJECT>
		<SCRIPT event="onscriptletevent(name, data)" for="color">
	setColor(name, data);
		</SCRIPT>
		<FORM id="form1" name="form1" action="richeditter.asp" method="post">
			<INPUT type="hidden" name="HtmString"> <INPUT type="hidden" name="phno"> <INPUT type="hidden" name="mailvar">
			<INPUT type="hidden" name="savevar"> <INPUT type="hidden" name="filename"> <INPUT 
type=hidden value='<%=Request.Params.Get("selDoc")%>' name=selDoc>
		</FORM>
	</BODY>
</HTML>
