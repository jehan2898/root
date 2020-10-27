<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Intake_PVT_Patient_Sign.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Intake_PVT_Patient_Sign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Signature</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C# .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

    <script language="Javascript">
<!--

        function OnClear() {
            document.FORM2.SigPlusPatient.TabletState = 0;
        }

        function OnClearPatient() {
            document.FORM2.SigPlusPatient.ClearTablet();
            document.FORM2.SigPlusPatient.TabletState = 1;
        }


        function OnSign() {
            document.FORM2.SigPlusPatient.TabletState = 0;
        }

        function OnSignPatient() {
            document.FORM2.SigPlusPatient.TabletState = 1;
        }


        function OnSave() {



            if (document.FORM2.SigPlusPatient.NumberOfTabletPoints > 0) {
                
                document.FORM2.SigPlusPatient.TabletState = 0;
                document.FORM2.SigPlusPatient.AutoKeyStart();
                document.FORM2.SigPlusPatient.AutoKeyFinish();
                document.FORM2.SigPlusPatient.EncryptionMode = 2;
                document.FORM2.SigPlusPatient.SigCompressionMode = 1;
                document.FORM2.hiddenPatient.value = document.FORM2.SigPlusPatient.SigString;
                document.FORM2.submit();
                parent.document.getElementById('divPatientSignaturePVT').style.visibility = 'hidden';
                parent.document.getElementById('divPatientSignaturePVT').style.zIndex = -1;
                
            }
            else {
                alert("Please Patient Sign Before Continuing...");
                return false;
            }

        }

//-->
    </script>

</head>
<body>
    <form id="FORM2" method="post" name="FORM2" action="Bill_Sys_Save_Intake_PVT_Sign.aspx?Sign=Patient">
        <h2>
            <center>
                <b><font color="blue">Signature</font></b></center>
        </h2>
        <table border="1" cellpadding="0" align="center">
            <tr>
                <td>
                    Patient Signature..
                </td>
            </tr>
            <tr>
                <td>
                    <object id="SigPlusPatient" style="left: 0px; width: 329px; top: 0px; height: 180px"
                        height="75" classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusPatient">
                        <param name="_Version" value="131095">
                        <param name="_ExtentX" value="8467">
                        <param name="_ExtentY" value="4763">
                        <param name="_StockProps" value="9">
                    </object>
                </td>
            </tr>
        </table>
        <table align="center" width="70%">
            <tr>
                <td style="width: 61px">
                    <input id="btnSignPatient" onclick="OnSignPatient()" type="button" value="Sign" name="SignBtn"
                        style="width: 58px; height: 24px">
                </td>
                <td style="width: 57px">
                    <input id="btnClearPatient" onclick="OnClearPatient()" type="button" value="Clear"
                        name="ClearBtn" style="height: 24px; width: 58px;">
                </td>
                <td style="width: 82px">
                    <input id="hiddenPatient" type="hidden" name="hiddenPatient" style="width: 132px">
                </td>
            </tr>
            <tr align="left">
                <td>
                    <input id="btnSubmit" onclick="OnSave()" type="button" value="Submit" name="Submit"
                        style="width: 58px">
                </td>
                <td>
                    &nbsp;</tr>
        </table>
    </form>
</body>
</html>
