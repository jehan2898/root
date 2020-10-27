<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CO_CH_Notes_Signature_Patient.aspx.cs" Inherits="Bill_Sys_CO_CH_Notes_Signature_Patient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Signature</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C# .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="Javascript">
<!--

function OnClear() { 
    document.FORM1.SigPlusPatient.TabletState = 0; 
}

function OnClearPatient() {
    document.FORM1.SigPlusPatient.ClearTablet();     
    document.FORM1.SigPlusPatient.TabletState = 1;
}


function OnSign() {
   document.FORM1.SigPlusPatient.TabletState = 0;
}

function OnSignPatient() { 
    document.FORM1.SigPlusPatient.TabletState = 1; 
}


function OnSave() {
 
   if(document.FORM1.SigPlusPatient.NumberOfTabletPoints > 0)   
    {
       
        document.FORM1.SigPlusPatient.TabletState = 0;
        document.FORM1.SigPlusPatient.AutoKeyStart();       
       
       document.FORM1.SigPlusPatient.AutoKeyFinish();
       document.FORM1.SigPlusPatient.EncryptionMode = 2;
       document.FORM1.SigPlusPatient.SigCompressionMode = 1; 
          
       document.FORM1.hiddenPatient.value = document.FORM1.SigPlusPatient.SigString;
       
       document.FORM1.submit();
   }
   else
   {
        alert("Please Patient Sign Before Continuing...");
        return false;
   }
 }

//-->
		</SCRIPT>
</head>
<body>
    <FORM id="FORM1" method="post" name="FORM1" action="Bill_Sys_CO_CH_Notes_Doctor.aspx?Sign=Patient">
			<h2><b><font color="blue">Signature</font></b></h2>			 
			<table border="1" cellpadding="0">
			<tr>			   
			    <td>
			        Patient Signature..
			    </td>
			</tr>
				<tr>					 					
					<td>
					   	<OBJECT id="SigPlusPatient" style="LEFT: 0px; WIDTH: 329px; TOP: 0px; HEIGHT: 180px" height="75"
							classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusPatient">
							<PARAM NAME="_Version" VALUE="131095">
							<PARAM NAME="_ExtentX" VALUE="8467">
							<PARAM NAME="_ExtentY" VALUE="4763">
							<PARAM NAME="_StockProps" VALUE="9">
						</OBJECT>
					</td>
				</tr>
			</table>
			<table>
			<tr>			    
		        <td style="width: 61px">
			        <INPUT id="btnSignPatient" onclick="OnSignPatient()" type="button" value="Sign" name="SignBtn" style="width: 58px; height: 24px">
			    </td>
			    <td style="width: 57px">
			        <INPUT id="btnClearPatient" onclick="OnClearPatient()" type="button" value="Clear" name="ClearBtn" style="height: 24px; width: 58px;"> 
			    </td> 
                <td style="width: 82px">
				    <INPUT id="hiddenPatient" type="hidden" name="hiddenPatient" style="width: 132px">		
			    </td>	    
			</tr>
			<tr align="center">
			    <td>
			         <INPUT id="btnSubmit" onclick="OnSave()" type="button" value="Submit" name="Submit" style="width: 58px">
			    </td>
			    <td>
                    &nbsp;</tr>
			</table>
		</FORM>
</body>
</html>
			
			



