﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_HPJ1_NotaryPublic_Signature.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_HPJ1_NotaryPublic_Signature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notary Public Signature</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C# .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<script language="Javascript">

function OnClear() {
    document.FORM1.SigPlusNotary.ClearTablet(); //Clears the signature, in case of error or mistake 
    document.FORM1.SigPlusNotary.TabletState = 1;
}

function OnClearPatient() {
    document.FORM1.SigPlusNotary.TabletState = 0;
}


function OnSign() {
    document.FORM1.SigPlusNotary.TabletState = 1; //Turns tablet on
}

function OnSignPatient() {
    document.FORM1.SigPlusNotary.TabletState = 0;
}


function OnSave() {
    if (document.FORM1.SigPlusNotary.NumberOfTabletPoints > 0) {
        document.FORM1.SigPlusNotary.TabletState = 0; //Turns tablet off 
        document.FORM1.SigPlusNotary.AutoKeyStart();

        document.FORM1.SigPlusNotary.AutoKeyFinish();
        document.FORM1.SigPlusNotary.EncryptionMode = 2;
        document.FORM1.SigPlusNotary.SigCompressionMode = 1;

        document.FORM1.hidden.value = document.FORM1.SigPlusNotary.SigString;
        //alert(document.FORM1.hidden.value);
        document.FORM1.submit();
        //alert(document.FORM1.SigPlusNotary.BitMapBufferSize());
        //parent.document.getElementById('divDocSignature').style.visibility='hidden';
        //parent.document.getElementById('divDocSignature').style.zIndex = -1;  
    }
    else {
        alert("Please Sign Before Continuing...");
        return false;
    }


}

</script>
<body>
       <form id="FORM1" method="post" name="FORM1" action="PhysicianSign.aspx?Sign=NotaryPublic">
        <h2>
            <center>
                <b><font color="blue">Signature</font></b></center>
        </h2>
        <table border="1" cellpadding="0" align="center">
            <tr>
                <td>
                    Notary Public Signature..
                </td>
            </tr>
            <tr>
                <td>
                    <object id="SigPlusNotary" style="left: 0px; width: 329px; top: 0px; height: 180px"
                        height="75" classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusNotary">
                        <param name="_Version" value="131095"/>
                        <param name="_ExtentX" value="8467"/>
                        <param name="_ExtentY" value="4763"/>
                        <param name="_StockProps" value="9"/>
                    </object>
                </td>
            </tr>
        </table>
        <table align="center" width="70%">
            <tr>
                <td style="width: 61px">
                    <input id="btnSign" onclick="OnSign()" type="button" value="Sign" name="SignBtn"
                        style="width: 58px; height: 24px"/>
                </td>
                <td style="width: 57px">
                    <input id="btnClear" onclick="OnClear()" type="button" value="Clear" name="ClearBtn"
                        style="height: 24px; width: 58px;"/>
                </td>
                <td style="width: 22%">
                </td>
                <td style="width: 82px">
                    <input id="hidden" type="hidden" name="hidden" style="width: 132px"/>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <input id="btnSubmit" onclick="OnSave()" type="button" value="Submit" name="Submit"
                        style="width: 58px"/>
                </td>
                <td></td>
                    &nbsp;</tr>
        </table>
   
    </form>
</body>
</html>