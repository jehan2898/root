<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Lien_Signature.aspx.cs" Inherits="Bill_Sys_Doctor_Signature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Signature</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C# .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <LINK rel="stylesheet" type="text/css" href="Css/ModalPopUp.css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
		<SCRIPT language="Javascript">
<!--

function OnClear() {
   
    document.FORM1.SigPlusDoctor.ClearTablet(); //Clears the signature, in case of error or mistake
    document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigPlusRepresentive.TabletState =0;
    document.FORM1.SigPlusDoctor.TabletState = 1;
}

function OnClearPatient() {
   
     document.FORM1.SigPlusPatient.ClearTablet(); //Clears the signature, in case of error or mistake
    document.FORM1.SigPlusDoctor.TabletState = 0;
    document.FORM1.SigPlusRepresentive.TabletState =0;
    document.FORM1.SigPlusPatient.TabletState = 1;
    
}

function OnPatientRepresentiveClear(){
    document.FORM1.SigPlusRepresentive.ClearTablet();
    document.FORM1.SigPlusDoctor.TabletState = 0;
    document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigPlusRepresentive.TabletState = 1;
}

function OnSign() {
   document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigPlusRepresentive.TabletState =0;
    document.FORM1.SigPlusDoctor.TabletState = 1; //Turns tablet on
}

function OnSignPatient() {

   document.FORM1.SigPlusRepresentive.TabletState = 0;
    document.FORM1.SigPlusDoctor.TabletState =0;
    document.FORM1.SigPlusPatient.TabletState = 1;
}
function OnPatientRepresentive() {
    document.FORM1.SigPlusDoctor.TabletState = 0;
    document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigPlusRepresentive.TabletState = 1;
}


function OnSave() {
//if(document.FORM1.SigText.value == "")
//{
//   alert("Please Enter A Name Before Continuing...");
//   return false;
//}

if(document.FORM1.SigPlusDoctor.NumberOfTabletPoints > 0)
{
    if(document.FORM1.SigPlusPatient.NumberOfTabletPoints > 0)   
    {
       document.FORM1.SigPlusDoctor.TabletState = 0; //Turns tablet off 
       document.FORM1.SigPlusDoctor.AutoKeyStart();   
       
        document.FORM1.SigPlusPatient.TabletState = 0;
        document.FORM1.SigPlusPatient.AutoKeyStart(); 
       
       document.FORM1.SigPlusRepresentive.TabletState = 0;
        document.FORM1.SigPlusRepresentive.AutoKeyStart(); 
      // document.FORM1.SigPlus1.AutoKeyData = document.FORM1.SigText.value;  
       
       document.FORM1.SigPlusDoctor.AutoKeyFinish();
       document.FORM1.SigPlusDoctor.EncryptionMode = 2;
       document.FORM1.SigPlusDoctor.SigCompressionMode = 1; 
       
       document.FORM1.SigPlusPatient.AutoKeyFinish();
       document.FORM1.SigPlusPatient.EncryptionMode = 2;
       document.FORM1.SigPlusPatient.SigCompressionMode = 1; 
       
       document.FORM1.SigPlusRepresentive.AutoKeyFinish();
       document.FORM1.SigPlusRepresentive.EncryptionMode = 2;
       document.FORM1.SigPlusRepresentive.SigCompressionMode = 1; 
          
       document.FORM1.hidden.value = document.FORM1.SigPlusDoctor.SigString;
       document.FORM1.hiddenPatient.value = document.FORM1.SigPlusPatient.SigString;
       document.FORM1.hiddenPatientRepresentive.value = document.FORM1.SigPlusRepresentive.SigString;
       //pass the signature ASCII hex string to the hidden field,
       //so it will be automatically passed when the page is submitted
       
       document.FORM1.submit();
   }
   else
   {
        alert("Please Patient Sign Before Continuing...");
        return false;
   }
}
else
{
   alert("Please Sign Before Continuing...");
   return false;
}


}

//-->
		</SCRIPT>
</head>
<body>
    <FORM id="FORM1" method="post" name="FORM1" action="Bill_Sys_Lien_Doctor_Patient_Signature.aspx">
			<h2><b><font color="blue">Signature</font></b></h2>
			<%--<P>Please Type Name Here:</P>
			<INPUT id="SigText" type="text" name="SigText"><br>
			<br>--%>
			<table border="1" cellpadding="0" class="TDPart">
			<tr>
			    <td style="width: 220px">
			        Attorney Signature..
			    </td>
			    <td style="width: 261px">
			        Patient Signature..
			    </td>
			    <td style="width: 225px">
			        Patient Representive..
			    </td>
			</tr>
				<tr>
					<td style="width: 100%">
						<OBJECT id="SigPlusDoctor" style="LEFT: 0px; WIDTH: 300px; TOP: 0px; HEIGHT: 180px" height="75"
							classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusDoctor">
							<PARAM NAME="_Version" VALUE="131095">
							<PARAM NAME="_ExtentX" VALUE="8467">
							<PARAM NAME="_ExtentY" VALUE="4763">
							<PARAM NAME="_StockProps" VALUE="9">
						</OBJECT>
					</td>
					
					<td style="width:261px">
					   	 <OBJECT id="SigPlusPatient" style="LEFT: 0px; WIDTH: 300px; TOP: 0px; HEIGHT: 180px" height="75"
							classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusPatient">
							<PARAM NAME="_Version" VALUE="131095">
							<PARAM NAME="_ExtentX" VALUE="8467">
							<PARAM NAME="_ExtentY" VALUE="4763">
							<PARAM NAME="_StockProps" VALUE="9">
						</OBJECT>
					</td>
					<td style="width:225px">
					    <OBJECT id="SigPlusRepresentive" style="LEFT: 0px; WIDTH: 300px; TOP: 0px; HEIGHT: 180px" height="75"
							classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusRepresentive">
							<PARAM NAME="_Version" VALUE="131095">
							<PARAM NAME="_ExtentX" VALUE="8467">
							<PARAM NAME="_ExtentY" VALUE="4763">
							<PARAM NAME="_StockProps" VALUE="9">
						</OBJECT>
					
					</td>
				</tr>
					<tr>
			    <td style="height: 21px">
			        <table>
			            <tr>
			                <td style="width: 61px">
			                    <INPUT id="Button5" onclick="OnSign()" type="button" value="Sign" name="SignBtn" style="width: 58px; height: 24px" class="Buttons">
			                </td>
			                <td style="width: 57px">
            			        <INPUT id="Button6" onclick="OnClear()" type="button" value="Clear" name="ClearBtn" style="height: 24px; width: 58px;" class="Buttons"> 
			                </td>
                            <td style="width: 82px">
				                <INPUT id="hidden2" type="hidden" name="hidden" style="width: 104px">		
			                </td>
			            </tr>
			        </table>
			    </td>
			    <td style="height: 21px; width: 261px;">
			        <table>
			            <tr>
			                 <td style="width: 61px">
			                     <INPUT id="Button7" onclick="OnSignPatient()" type="button" value="Sign" name="SignBtn" style="width: 58px; height: 24px" class="Buttons">
			                </td>
			                <td style="width: 57px"> 
			                     <INPUT id="Button8" onclick="OnClearPatient()" type="button" value="Clear" name="ClearBtn" style="height: 24px; width: 58px;" class="Buttons"> 
			                 </td>
			                 <td style="width: 82px">
				                 <INPUT id="hidden3" type="hidden" name="hiddenPatient" style="width: 91px">		
			                     </td>
			            </tr>
			        </table>
			    </td>
			    
			    <td style="height: 21px; width: 225px;">
			        <table>
			            <tr>
			                <td style="width: 61px; height: 26px;">
			                    <INPUT id="Button9" onclick="OnPatientRepresentive()" type="button" value="Sign" name="SignBtn" style="width: 58px; height: 24px" class="Buttons">
			                 </td>
			                 <td style="width: 57px; height: 26px;" align="left">
			                     <INPUT id="Button10" onclick="OnPatientRepresentiveClear()" type="button" value="Clear" name="ClearBtn" style="height: 24px; width: 58px;" class="Buttons"> 
			                 </td>
	                         <td style="width: 82px; height: 26px;">
				                 <INPUT id="hidden4" type="hidden" name="hiddenPatientRepresentive" style="width: 71px">		
			                 </td>
			            </tr>
			        </table>
			    </td>
			</tr>
			<tr>
			   <td colspan="10" align="center">
			         <INPUT id="btnSubmit" onclick="OnSave()" type="button" value="Submit" name="Submit" style="width: 58px" class="Buttons">
			    </td>
			</tr>
			</table>
		
			<table>
			
			
			
			<tr align="center">
			    
			   
                    &nbsp;</tr>
			</table>
		</FORM>
</body>
</html>
			
			



