<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderSign.aspx.cs" Inherits="AJAX_Pages_ProviderSign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Save Signature</title>
</head>
<script language="javascript" type="text/javascript">
    //    function ShowDocSignaturePopup()
    //    {
    //      
    //        document.getElementById('divDocSignature').style.zIndex = 1;
    //        document.getElementById('divDocSignature').style.position = 'fixed';
    //        document.getElementById('divDocSignature').style.left= '350px'; 
    //        document.getElementById('divDocSignature').style.top= '100px';      
    //        document.getElementById('divDocSignature').style.border= '10px';             
    //        document.getElementById('divDocSignature').style.visibility='visible'; 
    //        document.getElementById('frmDocSignature').src ='Bill_Sys_CO_CH_Notes_Signature.aspx'; 
    //        return false;
    //    }
    //     function CloseDocSignaturePopup()
    //    {
    //        document.getElementById('divDocSignature').style.visibility='hidden';
    //        document.getElementById('divDocSignature').style.zIndex = -1;  
    //    }
    //    function ShowPatientSignaturePopup()
    //    {
    //      
    //        document.getElementById('divPatientSignature').style.zIndex = 1;
    //        document.getElementById('divPatientSignature').style.position = 'fixed';
    //        document.getElementById('divPatientSignature').style.left= '350px'; 
    //        document.getElementById('divPatientSignature').style.top= '100px';      
    //        document.getElementById('divPatientSignature').style.border= '10px';             
    //        document.getElementById('divPatientSignature').style.visibility='visible'; 
    //        document.getElementById('frmPatientSignature').src ='Bill_Sys_CO_CH_Notes_Signature_Patient.aspx'; 
    //        return false;
    //    }   
    //    
    //     function ClosePatientSignaturePopup()
    //    {
    //        document.getElementById('divPatientSignature').style.visibility='hidden';
    //        document.getElementById('divPatientSignature').style.zIndex = -1;  
    //    }
    function ClosePopup() {
        parent.document.getElementById('divPatientSignature').style.visibility = 'hidden';
        parent.document.getElementById('divPatientSignature').style.zIndex = -1;
        parent.document.getElementById('divDocSignature').style.visibility = 'hidden';
        parent.document.getElementById('divDocSignature').style.zIndex = -1;
    }
</script>
<body>
     <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    &nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="Back" 
                                CssClass="Buttons" OnClick="btnBack_Click" />
                </td>
            </tr>
            
            
        </table>
        
    </div>
    <%--<div id="divDocSignature" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseDocSignaturePopup()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frmDocSignature" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignature" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePatientSignaturePopup()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frmPatientSignature" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>--%>
    </form>
</body>
</html>
