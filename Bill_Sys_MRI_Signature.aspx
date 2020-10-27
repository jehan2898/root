<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_MRI_Signature.aspx.cs"
    Inherits="Bill_Sys_MRI_Signature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signature</title>
     <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>

<script type="text/javascript" language="javascript">
   function OnSignPatient() 
   {
        document.FORM1.SigPlusPatient.TabletState = 1;
   }
   function OnClearPatient()
   {
     document.FORM1.SigPlusPatient.ClearTablet(); //Clears the signature, in case of error or mistake
     document.FORM1.SigPlusPatient.TabletState = 1;
   }
   function OnSave() 
   {
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
       alert("Patient Signs are compulsary...");
        return false;   
    }
       
   }
</script>

<body>
    <form id="FORM1" method="post" name="FORM1" action="Bill_Sys_MRI_Patient_Signature.aspx">
      <h2>
            <b><font color="blue">Signature</font></b></h2>
        <table cellpadding="0" cellspacing="0" border="0" class="TDPart">
            <tr>
                <td style="width: 261px">
                    Patient's Signature...</td>
            </tr>
            <tr>
                <td style="width: 261px">
                    <object id="SigPlusPatient" style="left: 0px; width: 300px; top: 0px; height: 180px"
                        height="75" classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusPatient">
                        <param name="_Version" value="131095">
                        <param name="_ExtentX" value="8467">
                        <param name="_ExtentY" value="4763">
                        <param name="_StockProps" value="9">
                    </object>
                </td>
            </tr>
            <tr>
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
                            <td colspan="10" align="center">
                                <input id="btnSubmit" onclick="OnSave()" type="button" value="Submit" name="Submit"
                                    style="width: 58px" class="Buttons">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </form>
</body>
</html>
