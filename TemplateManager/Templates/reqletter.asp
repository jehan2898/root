<HTML>
<HEAD>
<!--#include file = "includes/adovbs.inc"-->
<!--#include file = "includes/dsn.asp"-->
<!--#include file = "includes/functions.asp"-->
<!--#include file = "sessionHeader.asp"-->
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<link rel="stylesheet" href="styles/webstyles.css">
</HEAD>

<body>
<!--#include file = "includes/header.asp"-->
<!--#include file = "upNav.asp"-->
<div id="overDiv" style="position:absolute; visibility:hidden; z-index:1000;"></div>
<script language="JavaScript" src="overlib.js"></script> 
<table width="100%" border="0" cellspacing="0" cellpadding="0" ID="Table2">
<tr> 
	<td width="100%" align="center" valign="top"> 
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="backcolor13" ID="Table3">				
		<tr align="center" valign="top"> 
			<td> 
				<table width="100%" border="0" cellspacing="1" cellpadding="0" height=400 ID="Table6" align="center">
					<!--#include file = "globalSearch.asp"-->							
				<tr align="left" valign="top" class="backWhite"> 
					<td width="100%" align="left" class="regText" style="padding-left:0px;padding-right:5px;">

<%
'-###################################-start reqletter routine######################
%>					
	
<script language="Javascript">
function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}

</script>

<script language=javascript>

function trim(a){
	var tmp=new Array();
	for(j=0;j<a.length;j++)
		if(a[j]!='')
			tmp[tmp.length]=a[j];
	a.length=tmp.length;
	for(j=0;j<tmp.length;j++)
		a[j]=tmp[j];
	return a;
}

function Print()
{
	if(window.document.myfrm.seldoc1.options[0].selected == true){
		alert("Please select a letter from the drop down.");
		return false;
	}
	else{
		var a;
		a=trim(document.myfrm.seldoc1.value);
		pvar = document.myfrm.packetvar.value;
		//alert(document.myfrm.seldoc1.text);
		//alert(a);
		switch(a){
		case "arbreq2.htm": 
		  		window.open('arbcases2.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "Affidavit Overdue.htm": 
		  		window.open('richeditteroverdueaff.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "Delay.htm": 
		  		window.open('richeditterdelay.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "delay reminder.htm": 
		  		window.open('richeditterdelay.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break	
		case "MultipleSuitCaption.htm" : 
		  		//alert(a);
				window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "MultipleSuitCaptionSuffolk.htm" : 		
  		//alert(a);
				window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break	
		case "MultipleSuitCaptionStipulation.htm" :
				 //alert(a);
				window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "SUMMONSLIPACKET.htm" :
				//alert(a);
				window.open('richeditterCaption.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "dummy.htm" :
				//alert(a);
				window.open('richeditterMC.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "SJtemplateMultiple.htm" :
				//alert("here");
				window.open('processSJMultiple.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "Default Judgement Packeted.htm" :
				//alert("here");
				window.open('richeditterJudgement.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				return true;
				break
		case "Inter.htm" :
				//alert("here");
				if (pvar ==1){
					window.open('richeditterInter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}else{
				window.open('richeditter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break
		case "notice to admit1.htm" :
				//alert("here");
				if (pvar ==1){
					window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}else{
				window.open('richeditter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break
		case "MOTION TO PRECLUDE.htm" :
				//alert("here");
				if (pvar ==1){
					doc = "motion to perclude Bronx.htm"
					window.open('richeditterCaptionStipulation.asp?seldoc='+doc,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}else{
				window.open('richeditter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break	
		case "SJ template Queens multi.htm" :
				 //alert(a);
				window.open('richeditterSJMulti.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "DAILY CHECK PROCESSED.htm" :
				 //alert(a);
				window.open('richeditterDraft.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				return true;
				break
		case "Execution with notice to garnishee.htm" :
				 //alert(a);
				if (pvar ==1){
				window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')
				}else{
				window.open('richeditter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break
		case "bronx reargue.htm" :
				 //alert(a);
				if (pvar ==1){
				window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}else{
				window.open('richeditter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break
		case "Service by Mail.htm" :
				if (pvar ==1){
				window.open('richeditterCaptionStipulation.asp?seldoc=Service by Mail Multi.htm','win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}else{
				window.open('richeditter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break
		case "NOTICE OF TRIAL MULTI.htm" :
				window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				return true;
				break
		case "ClaimsItemized.htm" :
				if (pvar ==1){
					window.open('richeditterCaptionLoop.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}else{
					window.open('richeditterClaimItemized.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break
				 //alert(a);
		default:
		  		if (pvar ==1){
					window.open('richeditterCaptionStipulation.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}else{
				window.open('richeditter.asp?seldoc='+a,'win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600');
				}	
				return true;
				break	
		}
	}
}
</script>


<%

Dim CID

if Request("CID")="" then
	CID=Session("CID")
else
	CID=Request("CID")
end if

	Session("CID")=CID

dim rsClaim

set rsClaim=Server.CreateObject("ADODB.RecordSet")

sQuery = "Exec sp_claim @frmInput1='"&CID&"'"

'Response.Write sQuery

dim packetvar
rsClaim.open sQuery,DataConn,adOpenRecordSet,adLockOptimistic
packetvar = rsClaim("packet")

%>
<form name="frmclaims" method="post" action="collector.asp?login=true&qry=<%=Request.QueryString("qry")%>&uid=<%=Session("uid")%>&CID=<%=Session("CID")%>&manage=true" ID="frmclaims" >
<table height="100%" width="100%" border="0" cellspacing="0" cellpadding="0" ID="Table8">
	
	<tr>		
			
		<td valign = "top" width="15%">
			<!--#include file="WANav.inc"-->
		</td>
		<td bgcolor="#999999" width="1"><img src="images/pixel.gif" width=1 height="1">
		</td>
		<td width="1"><img src="images/pixel.gif" width="5" height="1"></td>
		<td>
		
		<table border="0" align="center">

			<tr>
				<td height="10" class="contentRed">
				<font size=3>Matter ID: <%=rsClaim("Claim_ID")%></font>
				<%
						if rsClaim("packet")="1" then
							'sqlpacket="Select * from tblpacket where insurance_id="&rsClaim("Ins_ID")&" and datediff(d,Convert(varchar(20),date_opened,101),'"& MYFormatDate(rsClaim("Date_Status_Changed"),"mm/dd/yyyy")&"')=0"
							'Response.Write sqlpacket
							sqlpacket="Select * from tblpacket where insurance_id="&rsClaim("Ins_ID")
							'Response.Write sqlpacket & "<br>"
							set rspacket =server.createobject("adodb.recordset")
							rspacket.open sqlpacket,dataConn,adopenstatic,adlockoptimistic
							'Response.Write rspacket.recordcount
							do while not rspacket.eof 
							
							'Response.Write rspacket("Matter_ids")
										if Instr(rspacket("Matter_ids"),rsClaim("Claim_ID")) > "0" then
										'if not rspacket.eof then
											'Session("Packet_Id")= rspacket("Packet_Id")
											'Response.Write Session("Packet_Id") & "<br>"
											'Response.Write Instr(rspacket("Matter_ids"),rsClaim("Claim_ID")) & "<br>"
										'end if
										str=""
										mids=split(rspacket("Matter_ids"),",")
										maxcounter=ubound(mids)
										FOR counter=0 TO maxcounter
											str=str & "<a href='collector.asp?qry=notes&login=true&CID="&mids(counter)&"'>"&mids(counter)&"</a>&nbsp;&nbsp;&nbsp;"
										NEXT
										else
										'Response.Write Instr(rspacket("Matter_ids"),rsClaim("Claim_ID"))
										end if
														
						rspacket.movenext
						loop
						end if
					%>
					<%=str & "(" & Session("Packet_Id") & ")"%>

				</td>
			
			</tr>
		</table>
	
</form>


<%
rsClaim.Close
Set rsClaim = Nothing
%>



<%
'-----------------------------------select document---------------
%>

<form method="post" action="" name="myfrm" onsubmit="javascript:return Print();">
	<input type="hidden" name="packetvar" value="<%=packetvar%>">
     <table width="100%" cellspacing="0" cellpadding="0" ID="Table13">
		<tr>
			<td height="10" class="contentBold" valign="top">
				Correspondence
			</td>
		</tr>
		<tr><td height="10"></td></tr>								
	</table>
     <table width="100%" border="0" cellspacing="2" cellpadding="5" align="center">
	 <tr>
		<td valign='top'>
		<table width="100%" border="0" cellspacing="2" cellpadding="5" align="center">
		<tr> 
		<%
			dim rsDocs,sqlDocs

			set rsDocs=Server.CreateObject("ADODB.RecordSet")
			sqlDocs="select * from tblDocs order by docname asc"
			rsDocs.open sqlDocs,DataConn,adOpenKeyset,adLockOptimistic,adCmdText
			'Response.Write  rsDocs.recordcount
		%>
			<td align="center">
			<select name="seldoc1" size=20>
				<option value="0" selected>Select Letters
				<%do while not rsDocs.EOF%>
				<option value="<%=rsDocs("DOCvalue")%>"><%=UCASE(rsDocs("DOCName"))%></option>
				<%
					rsDocs.MoveNext
					loop
				%>
			</select>
			</td>
		</tr>
		<tr>
			<td align="center"> 
				<input type="Button"  name="Submit" value="Print" class="buttonsizer" onClick = "javascript:return Print();">
			</td>
		</tr>
	    </table>
		</td>
		<td valign='top'>
			<input type="button" name="btnsuits" value="GET ORIGINAL SUMMONS > >" onclick="window.open('richeditterSummons.asp','win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')" class="blueback">
			<br>
			<br>
			<input type="button" name="btnResponse" value="GET Responses > >" onclick="window.open('getResponses.asp?keyword=<%=Session("CID")%>','win1','toolbar=no,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=800,height=600')" class="blueback">
		</td>
	</tr>
	</table>
<br>
</form>
<hr width="75%" align="center"></hr>
<%
'------------------------------end select module---------------------
%>


					
<%
'----##################################end req letter#################################3
%>




					</td>
				</tr>
				</table>
			</td>
		</tr>
		</table>
	</td>
</tr>
</table> 
<!--#include file = "bottom.inc"-->
<table border="0" cellspacing="0" cellpadding="0" width="100%" ID="Table9">
	<tr>
		<td>
			<img src="images/ie.gif" width="83" height="37" border="0">
		</td>						
	</tr>
</table>
</BODY>
</HTML>					
