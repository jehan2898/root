<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MRISignature.aspx.cs" Inherits="MRISignature" %>

    <script type="text/javascript" src="validation.js"></script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Signature</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C# .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Css/ModalPopUp.css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script language="Javascript">
<!--

function OnClear() {
   
    document.FORM1.SigPlusDoctor.ClearTablet(); //Clears the signature, in case of error or mistake
    document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigParentOfMinorPatient.TabletState =0;
    document.FORM1.SigPlusDoctor.TabletState = 1;
    document.FORM1.Gardian.TabletState=0;
}

function OnGardianClear()
{
        document.FORM1.Gardian.ClearTablet(); //Clears the signature, in case of error or mistake
        document.FORM1.SigPlusDoctor.TabletState = 0;
        document.FORM1.SigParentOfMinorPatient.TabletState =0;
        document.FORM1.SigPlusPatient.TabletState = 0;
         document.FORM1.Gardian.TabletState=1; 
}

function OnClearPatient() {
   
     document.FORM1.SigPlusPatient.ClearTablet(); //Clears the signature, in case of error or mistake
    document.FORM1.SigPlusDoctor.TabletState = 0;
    document.FORM1.SigParentOfMinorPatient.TabletState =0;
    document.FORM1.SigPlusPatient.TabletState = 1;
    document.FORM1.Gardian.TabletState=0;
    
}

function OnParentOfMinorPatientClear(){
    document.FORM1.SigParentOfMinorPatient.ClearTablet();
    document.FORM1.SigPlusDoctor.TabletState = 0;
    document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigParentOfMinorPatient.TabletState = 1;
    document.FORM1.Gardian.TabletState=0;
}

function OnSign() {
   document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigParentOfMinorPatient.TabletState =0;
    document.FORM1.SigPlusDoctor.TabletState = 1; //Turns tablet on
    document.FORM1.Gardian.TabletState=0;
}

function OnSignPatient() {

   document.FORM1.SigParentOfMinorPatient.TabletState = 0;
    document.FORM1.SigPlusDoctor.TabletState =0;
    document.FORM1.SigPlusPatient.TabletState = 1;
    document.FORM1.Gardian.TabletState=0;
}
function OnParentOfMinorPatient() {
    document.FORM1.SigPlusDoctor.TabletState = 0;
    document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigParentOfMinorPatient.TabletState = 1;
    document.FORM1.Gardian.TabletState=0;
}
 function OnGardian()
 {
     document.FORM1.SigPlusDoctor.TabletState = 0;
    document.FORM1.SigPlusPatient.TabletState = 0;
    document.FORM1.SigParentOfMinorPatient.TabletState = 0;
     document.FORM1.Gardian.TabletState =1;
 }


function OnSave() {
  if(document.FORM1.SigPlusDoctor.NumberOfTabletPoints > 0 && document.FORM1.SigPlusPatient.NumberOfTabletPoints>0)
     {
              
  
       document.FORM1.SigPlusDoctor.TabletState = 0; //Turns tablet off 
       document.FORM1.SigPlusDoctor.AutoKeyStart();   
       
        document.FORM1.SigPlusPatient.TabletState = 0;
        document.FORM1.SigPlusPatient.AutoKeyStart(); 
       
       document.FORM1.SigParentOfMinorPatient.TabletState = 0;
        document.FORM1.SigParentOfMinorPatient.AutoKeyStart(); 
        
         document.FORM1.Gardian.TabletState = 0;
        document.FORM1.Gardian.AutoKeyStart();
      // document.FORM1.SigPlus1.AutoKeyData = document.FORM1.SigText.value;  
       
       document.FORM1.SigPlusDoctor.AutoKeyFinish();
       document.FORM1.SigPlusDoctor.EncryptionMode = 2;
       document.FORM1.SigPlusDoctor.SigCompressionMode = 1; 
       
       document.FORM1.SigPlusPatient.AutoKeyFinish();
       document.FORM1.SigPlusPatient.EncryptionMode = 2;
       document.FORM1.SigPlusPatient.SigCompressionMode = 1; 
       
       document.FORM1.SigParentOfMinorPatient.AutoKeyFinish();
       document.FORM1.SigParentOfMinorPatient.EncryptionMode = 2;
       document.FORM1.SigParentOfMinorPatient.SigCompressionMode = 1; 
       
        document.FORM1.Gardian.AutoKeyFinish();
       document.FORM1.Gardian.EncryptionMode = 2;
       document.FORM1.Gardian.SigCompressionMode = 1; 
          
       document.FORM1.hidden.value = document.FORM1.SigPlusDoctor.SigString;
       document.FORM1.hiddenPatient.value = document.FORM1.SigPlusPatient.SigString;
       document.FORM1.hiddenParentOfMinorPatient.value = document.FORM1.SigParentOfMinorPatient.SigString;
       document.FORM1.hiddenGardian.value=document.FORM1.Gardian.SigString
       //pass the signature ASCII hex string to the hidden field,
       //so it will be automatically passed when the page is submitted
      
       document.FORM1.submit();
   }
   else
   {
        alert(" Attorney and Patient Signs are compulsary......");
        return false;
   }

}

//-->
    </script>

</head>
<body>
    <form id="FORM1" method="post" name="FORM1"  action="Bill_Sys_Test_Facility_Intake_Save.aspx">
        <h2>
            <b><font color="blue">Signature</font></b></h2>
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
                    Parent Of Minor Patient Signature ..
                </td>
                <td style="width: 225px">
                    Gardian Signature ....
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <object id="SigPlusDoctor" style="left: 0px; width: 300px; top: 0px; height: 180px"
                        height="75" classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusDoctor">
                        <param name="_Version" value="131095">
                        <param name="_ExtentX" value="8467">
                        <param name="_ExtentY" value="4763">
                        <param name="_StockProps" value="9">
                    </object>
                </td>
                <td style="width: 261px">
                    <object id="SigPlusPatient" style="left: 0px; width: 300px; top: 0px; height: 180px"
                        height="75" classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusPatient">
                        <param name="_Version" value="131095">
                        <param name="_ExtentX" value="8467">
                        <param name="_ExtentY" value="4763">
                        <param name="_StockProps" value="9">
                    </object>
                </td>
                <td style="width: 225px">
                    <object id="SigParentOfMinorPatient" style="left: 0px; width: 300px; top: 0px; height: 180px"
                        height="75" classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigParentOfMinorPatient">
                        <param name="_Version" value="131095">
                        <param name="_ExtentX" value="8467">
                        <param name="_ExtentY" value="4763">
                        <param name="_StockProps" value="9">
                    </object>
                </td>
                <td style="width: 225px">
                    <object id="Gardian" style="left: 0px; width: 300px; top: 0px; height: 180px" height="75"
                        classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="Gardian">
                        <param name="_Version" value="131095">
                        <param name="_ExtentX" value="8467">
                        <param name="_ExtentY" value="4763">
                        <param name="_StockProps" value="9">
                    </object>
                </td>
            </tr>
            <tr>
                <td style="height: 21px">
                    <table>
                        <tr>
                            <td style="width: 61px">
                                <input id="Button5" onclick="OnSign()" type="button" value="Sign" name="SignBtn"
                                    style="width: 58px; height: 24px" class="Buttons">
                            </td>
                            <td style="width: 57px">
                                <input id="Button6" onclick="OnClear()" type="button" value="Clear" name="ClearBtn"
                                    style="height: 24px; width: 58px;" class="Buttons">
                            </td>
                            <td style="width: 82px">
                                <input id="hidden2" type="hidden" name="hidden" style="width: 104px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="height: 21px; width: 261px;">
                    <table>
                        <tr>
                            <td style="width: 61px">
                                <input id="Button7" onclick="OnSignPatient()" type="button" value="Sign" name="SignBtn"
                                    style="width: 58px; height: 24px" class="Buttons">
                            </td>
                            <td style="width: 57px">
                                <input id="Button8" onclick="OnClearPatient()" type="button" value="Clear" name="ClearBtn"
                                    style="height: 24px; width: 58px;" class="Buttons">
                            </td>
                            <td style="width: 82px">
                                <input id="hidden3" type="hidden" name="hiddenPatient" style="width: 91px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="height: 21px; width: 225px;">
                    <table>
                        <tr>
                            <td style="width: 61px; height: 26px;">
                                <input id="Button9" onclick="OnParentOfMinorPatient()" type="button" value="Sign"
                                    name="SignBtn" style="width: 58px; height: 24px" class="Buttons">
                            </td>
                            <td style="width: 57px; height: 26px;" align="left">
                                <input id="Button10" onclick="OnParentOfMinorPatientClear()" type="button" value="Clear"
                                    name="ClearBtn" style="height: 24px; width: 58px;" class="Buttons">
                            </td>
                            <td style="width: 82px; height: 26px;">
                                <input id="hidden4" type="hidden" name="hiddenParentOfMinorPatient" style="width: 71px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="height: 21px; width: 225px;">
                    <table>
                        <tr>
                            <td style="width: 61px; height: 26px;">
                                <input id="Button11" onclick="OnGardian()" type="button" value="Sign" name="SignBtn"
                                    style="width: 58px; height: 24px" class="Buttons">
                            </td>
                            <td style="width: 57px; height: 26px;" align="left">
                                <input id="Button12" onclick="OnGardianClear()()" type="button" value="Clear" name="ClearBtn"
                                    style="height: 24px; width: 58px;" class="Buttons">
                            </td>
                            <td style="width: 82px; height: 26px;">
                                <input id="hidden5" type="hidden" name="hiddenGardian" style="width: 71px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="10" align="center">
                    <input id="btnSubmit" onclick="OnSave()" type="button" value="Submit" name="Submit"
                        style="width: 58px" class="Buttons">
                </td>
            </tr>
        </table>
       
    </form>
</body>
</html>
