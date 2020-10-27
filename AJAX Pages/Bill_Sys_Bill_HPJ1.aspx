<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Bill_HPJ1.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Bill_HPJ1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" id="Hp1">
    <title>HPJ1</title>
    <script language="javascript" type="text/javascript">
        function ShowPhySignaturePopup() {

            var billNo = document.getElementById('hdbillno').value;
            var signDt = document.getElementById('txtPhySignDt').value;
            document.getElementById('divPhySignature').style.zIndex = 1;
            document.getElementById('divPhySignature').style.position = 'fixed';
            document.getElementById('divPhySignature').style.left = '100px';
            document.getElementById('divPhySignature').style.top = '100px';
            document.getElementById('divPhySignature').style.border = '10px';
            document.getElementById('divPhySignature').style.visibility = 'visible';
            document.getElementById('frmPhySignature').src = 'Bill_Sys_HPJ1_Physician_Signature.aspx?billNo=' + billNo + '&SignDt=' + signDt;
            return false;
        }
        function ShowNotarySignaturePopup() {

            var billNo = document.getElementById('hdbillno').value;
            var signDt = document.getElementById('txtNotarySignDt').value;
            document.getElementById('divPhySignature').style.zIndex = 1;
            document.getElementById('divPhySignature').style.position = 'fixed';
            document.getElementById('divPhySignature').style.left = '100px';
            document.getElementById('divPhySignature').style.top = '100px';
            document.getElementById('divPhySignature').style.border = '10px';
            document.getElementById('divPhySignature').style.visibility = 'visible';
            document.getElementById('frmPhySignature').src = 'Bill_Sys_HPJ1_NotaryPublic_Signature.aspx?billNo=' + billNo + '&SignDt=' + signDt;
            return false;
        }
        function ShowOtherSignaturePopup() {

            var billNo = document.getElementById('hdbillno').value;
            var signDt = document.getElementById('txtOtherSignDt').value;
            document.getElementById('divPhySignature').style.zIndex = 1;
            document.getElementById('divPhySignature').style.position = 'fixed';
            document.getElementById('divPhySignature').style.left = '100px';
            document.getElementById('divPhySignature').style.top = '100px';
            document.getElementById('divPhySignature').style.border = '10px';
            document.getElementById('divPhySignature').style.visibility = 'visible';
            document.getElementById('frmPhySignature').src = 'Bill_Sys_HPJ1_Other_Signature.aspx?billNo=' + billNo + '&SignDt=' + signDt;
            return false;
        }
        function ClosePhySignaturePopup() {
            document.getElementById('divPhySignature').style.visibility = 'hidden';
            document.getElementById('divPhySignature').style.zIndex = -1;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>

<body>

<form id="form1" runat="server">
<asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

    <table style="width: 100%">
        <tr>
            <td>
                <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblmsg" runat="server" Text="You already have 1 HP-J1 document associated with the bill. Click" Visible="false" ></asp:Label>&nbsp;
                <asp:HyperLink ID="hyLinkOpenPDF" runat="server" Text="here" Target="_blank" Visible="false"></asp:HyperLink>&nbsp;
                <asp:Label ID="lblmsg2" runat="server" Text="to view" Visible="false" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td align="right">
                            <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Width="100px" 
                                onclick="btnSave_Click" ></dx:ASPxButton>
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnPrint" runat="server" Text="Print" Width="100px" 
                                onclick="btnPrint_Click"></dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
        <tr>
            <td align="center">                
                <table>
                    <tr>
                        <td align="center" >
                            <asp:Label ID="lblBillAmt" runat ="server" Text ="Bill Amount"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtBillAmt" runat ="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat ="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat ="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat ="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat ="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat ="server" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">                
                &nbsp;</td>
        </tr>
        <tr>            
            <td>
                <div style="border:2px solid">
                    <table style="width: 100%">
                        <tr style="background-color: #DCDCDC">
                            <td align="center" colspan="6" style="background-color: #B5DF82">
                                <asp:Label ID="lblHHealthProvider" runat="server" Text="HEALTH CARE PROVIDER DETAILS" Width="100%" Font-Bold="True" Font-Size="Small"></asp:Label>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblHPName1" runat="server" Text="Name 1"></asp:Label>                
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtHPName1" runat ="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblHPName2" runat="server" Text="2"></asp:Label>                
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtHPName2" runat ="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblHPAddr" runat="server" Text="Address"></asp:Label>                
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtHPAddr" runat ="server" TextMode="MultiLine" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblHPCity" runat="server" Text="City"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHPCity" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHPState" runat="server" Text="State"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHPState" runat="server" CssClass="text-box" visible="False" ></asp:TextBox>
                                <extddl:extendeddropdownlist ID="extdlHPState" runat="server" Connection_Key="Connection_String"
                                    Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                    OldText="" StausText="False"></extddl:extendeddropdownlist>
                            </td>
                            <td>
                                <asp:Label ID="lblHPZip" runat="server" Text="Zip"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHPZip" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>                
                </div>
            </td>            
        </tr>        
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <div style="border:2px solid">
                    <table style="width: 100%">
                        <tr>
                            <td align="center" colspan="6" style="background-color: #B5DF82">
                                <asp:Label ID="lblHCarrierEmployer" runat="server" Text="CARRIER/SELF-INSURED EMPLOYER DETAILS" Width="100%" Font-Bold="True" Font-Size="Small"></asp:Label>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblCEName1" runat="server" Text="Name 1"></asp:Label>                
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtCEName1" runat ="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblCEName2" runat="server" Text="2"></asp:Label>                
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtCEName2" runat ="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblCEAddress" runat="server" Text="Address"></asp:Label>                
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtCEAddress" runat ="server" TextMode="MultiLine" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblCECity" runat="server" Text="City"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCECity" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblCEAddr" runat="server" Text="State"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCEState" runat="server" CssClass="text-box" visible="False" ></asp:TextBox>
                                <extddl:extendeddropdownlist ID="extdlCEState" runat="server" Connection_Key="Connection_String"
                                    Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                    OldText="" StausText="False"></extddl:extendeddropdownlist>
                            </td>
                            <td>
                                <asp:Label ID="lblCEZip" runat="server" Text="Zip"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCEZip" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div> 
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <div style="border:2px solid">
                    <table style="width: 100%">
                        <tr>
                            <td align="center" colspan="5" style="background-color: #B5DF82">
                    <asp:Label ID="lblHOther" runat="server" Text="OTHER DETAILS" Width="100%" Font-Bold="True" 
                                    Font-Size="Small"></asp:Label>                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label1" runat="server" Text="WCB Case Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWCBCaseNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="WCB Authorization Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWCBAuthNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label3" runat="server" Text="Date of Accident or Injury"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtofAccident" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:imagebutton id="imgbtnDtofAccident" runat="server" imageurl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calExtDtofAccident" runat="server" TargetControlID="txtDtofAccident" PopupButtonID="imgbtnDtofAccident" />
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Carrier Case Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCarrierCaseNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label5" runat="server" Text="Carrier/Self-Insured Employer I.D. Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCarrierID" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="County in Which Injury Occurred"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCountyInjOcc" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label7" runat="server" Text="Employer"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtEmployer" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label8" runat="server" Text="State of New York County of"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCountyof" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="SS"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSS" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label10" runat="server" Text="License Holder"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLicensedHolder" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Subscribe & Sworn before"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSworn" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label12" runat="server" Text="Day of"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDayof1" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>                            
                            <td>
                                <asp:TextBox ID="txtDayof2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>        
        <tr>            
            <td align="center">
                <div style="border:2px solid">
                    <table width ="100%">
                        <tr>
                            <td align="center" style="background-color: #B5DF82">
                                <asp:Label ID ="lblHSign" runat ="server" Text ="SIGNATURE" Width="100%" 
                                    Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label15" runat="server" Text="Physician Signature"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPhySignDt" runat="server"></asp:TextBox>    
                            </td>
                            <td align="left">
                                <asp:imagebutton id="imgbtnDtofPhySign" runat="server" imageurl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calExtDtofPhySign" runat="server" TargetControlID="txtPhySignDt" PopupButtonID="imgbtnDtofPhySign" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnPhySign" runat="server" Text="Physician Signature" 
                                    OnClientClick=" return ShowPhySignaturePopup()" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label16" runat="server" Text="Notary Public Signature"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNotarySignDt" runat="server"></asp:TextBox>    
                            </td>
                            <td align="left">
                                <asp:imagebutton id="imgbtnDtofNotarySign" runat="server" imageurl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calExtDtofNotarySign" runat="server" TargetControlID="txtNotarySignDt" PopupButtonID="imgbtnDtofNotarySign" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnNotarySign" runat="server" Text="Notary Public Signature" OnClientClick=" return ShowNotarySignaturePopup()" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label17" runat="server" Text="Other Signature"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtOtherSignDt" runat="server"></asp:TextBox>    
                            </td>
                            <td align="left">
                                <asp:imagebutton id="imgbtnDtofOtherSign" runat="server" imageurl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calExtDtofOtherSign" runat="server" TargetControlID="txtOtherSignDt" PopupButtonID="imgbtnDtofOtherSign" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnOtherSign" runat="server" Text="Other Signature" 
                                    OnClientClick=" return ShowOtherSignaturePopup()" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdSign" runat="server" />
                <asp:HiddenField ID="hdSave" runat="server" />
                <asp:HiddenField ID="hdbillno" runat="server" />
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtBillNo" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtPhysicianSignPath" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtNotarySignPath" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtOtherSignPath" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtPhysicianSignSucc" runat="server" Visible="false" Text ="0"></asp:TextBox>
                <asp:TextBox ID="txtNotarySignSucc" runat="server" Visible="false" Text ="0"></asp:TextBox>
                <asp:TextBox ID="txtOtherSignSucc" runat="server" Visible="false" Text ="0"></asp:TextBox>
                <asp:TextBox ID="txtSpeciality" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtFileName" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div id="divPhySignature" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePhySignaturePopup()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frmPhySignature" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>

</form>
</body>
</html>

