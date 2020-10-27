<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Bill_Hp1Form.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Bill_Hp1Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" id="Hp1">
    <title>HP1</title>
</head>

<body>

<script language="javascript" type="text/javascript">
 function ShowDocSignaturePopup() {
     var billNo = document.getElementById('<%= hfbillno.ClientID %>').value;
     //var billNo = document.getElementById('hfbillno');
        document.getElementById('divDocSignature').style.zIndex = 1;
        document.getElementById('divDocSignature').style.position = 'fixed';
        document.getElementById('divDocSignature').style.left= '100px'; 
        document.getElementById('divDocSignature').style.top= '100px';      
        document.getElementById('divDocSignature').style.border= '10px';             
        document.getElementById('divDocSignature').style.visibility='visible';
        document.getElementById('frmDocSignature').src = 'Bill_Sys_ProviderSign.aspx?billNo=' + billNo; 
        return false;
    }
    function CloseDocSignaturePopup() {
        document.getElementById('divDocSignature').style.visibility = 'hidden';
        document.getElementById('divDocSignature').style.zIndex = -1;
    }
    function ShowPatientSignaturePopup() {
        var billNo = document.getElementById('<%= hfbillno.ClientID %>').value;
        document.getElementById('divPatientSignature').style.zIndex = 1;
        document.getElementById('divPatientSignature').style.position = 'fixed';
        document.getElementById('divPatientSignature').style.left = '100px';
        document.getElementById('divPatientSignature').style.top = '100px';
        document.getElementById('divPatientSignature').style.border = '10px';
        document.getElementById('divPatientSignature').style.visibility = 'visible';
        document.getElementById('frmPatientSignature').src = 'Bill_Sys_ProviderSign2.aspx?billNo=' + billNo;
        return false;
    }

    function ClosePatientSignaturePopup() {
        document.getElementById('divPatientSignature').style.visibility = 'hidden';
        document.getElementById('divPatientSignature').style.zIndex = -1;
    }
    
    </script>
    <form id="form1" runat="server">
             <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%">
            <tr>
                <td style="width: 414px" align="center">
                    <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 226px">
                </td>
                <td style="height: 26px" align="center">
                    <asp:Label ID="lblmsg" runat="server" Text="You already have 1 HP1 document associated with the bill. Click"
                        Visible="False"></asp:Label>
                    <asp:HyperLink ID="hyLinkOpenPDF" runat="server" Text="here" Target="_blank" Visible="false"></asp:HyperLink>
                    <asp:Label ID="lblmsg2" runat="server" Text=" to view." Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 343px; height: 26px">
                    &nbsp;</td>
                <td style="width: 90px; height: 26px;">
                    <asp:Button ID="btnsave" runat="server" Text="Save" Width="71px" 
                        onclick="btnsave_Click" /></td>
                <td style="width: 1px; height: 26px;">
                </td>
                <td style="height: 26px; width: 103px;">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="76px" 
                        onclick="btnPrint_Click" /></td>
                <td style="height: 26px">
                </td>
            </tr>
            <tr>
                <td style="width: 343px">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table style="padding-right: 60px; width: 100%">
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; height: 26px; text-align: right">
                    <asp:CheckBox ID="chkARequestForAdmin" runat="server" Text="A. REQUEST FOR ADMINISTRATIVE AWARD" />&nbsp;</td>
                <td style="font-size: 12pt; font-family: Times New Roman; height: 26px">
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; height: 26px; text-align: right">
                    <asp:CheckBox ID="chkBRequestForArbitration" runat="server" Text="B. REQUEST FOR ARBITRATION " /><br />
                    &nbsp;<br />
                    NUMBER OF MEDICAL BILLS ATTACHED:<br />
                    FEE SUBMITTED:<br />
                    CHECK/M.O.NO:</td>
                <td style="font-size: 12pt; font-family: Times New Roman; height: 26px">
                    <br />
                    <br />
                    <asp:TextBox ID="txtNoOfMedicalBills" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtFeeSubmitted" runat="server"></asp:TextBox><br />
                    <asp:TextBox ID="txtCheckMONo" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; height: 26px; text-align: right">
                    TYPE OF CARE: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
                <td style="font-size: 12pt; font-family: Times New Roman; height: 26px">
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    <asp:CheckBox ID="chkmedical" runat="server" />Medical&nbsp; &nbsp;<asp:CheckBox ID="chkoutHos" runat="server" /><span style="font-size: 11pt; font-family: Calibri">
                            <span style="font-size: 12pt; font-family: Times New Roman">Outpatient Hospital &nbsp;
                            </span>&nbsp;</span><asp:CheckBox ID="chkInpHos" runat="server" />Inpatient
                    Hospital</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    <span style="font-size: 11pt; font-family: Calibri">&nbsp;</span><asp:CheckBox ID="chkChiropractic" runat="server" />Chiropractic &nbsp;<asp:CheckBox ID="chkPhysicalTherapy" runat="server" />Physical
                    Therapy
                    <asp:CheckBox ID="chkOccupationalTherapy" runat="server" />Occupational Therapy
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    <span style="font-size: 11pt; font-family: Calibri"></span>
                    <asp:CheckBox ID="chkPsychology" runat="server" />Psychology &nbsp; &nbsp;&nbsp;
                    &nbsp;<asp:CheckBox ID="chlPodiatry" runat="server" />Podiatry &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp;
                    <asp:CheckBox ID="chkOsteopathic" runat="server" />Osteopathic &nbsp; &nbsp; &nbsp;
                    &nbsp;&nbsp;</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    Name and Mailing Address Of&nbsp; Health Provider (MAXIMUM 30 CHARACTER)
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    Name:
                </td>
                <td>
                    <asp:TextBox ID="txtMHPName" runat="server" MaxLength="30" Width="299px"></asp:TextBox>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px; height: 26px;">
                    Line 1&amp;2 :
                </td>
                <td colspan="8" style="height: 26px">
                    <asp:TextBox ID="txtMHPLine" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px; height: 26px;">
                    Address :
                </td>
                <td style="height: 26px">
                    <asp:TextBox ID="txtMHPAddress" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td valign="top" class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px; height: 26px;">
                    City :
                </td>
                <td>
                    <asp:TextBox ID="txtMHPCity" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right" valign="top">
                    State:</td>
                <td>
                    <asp:TextBox ID="txtMHPState" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right" valign="top">
                    Zip Code:</td>
                <td>
                    <asp:TextBox ID="txtMHPZipCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right" valign="top">
                    WCB Case Number:</td>
                <td>
                    <asp:TextBox ID="txtWCBCaseNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right" valign="top">
                    WCB Authorization Number:</td>
                <td>
                    <asp:TextBox ID="txtWCBAuthoNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    Provider's WCB Rating Code :
                </td>
                <td>
                    <asp:TextBox ID="txtProWCBNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp&nbsp&nbsp Provider's Telephone Number:
                </td>
                <td>
                    <asp:TextBox ID="txtProTelNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Name and Billing Address Of Health Provider (MAXIMUM 30 CHARACTER)</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp&nbsp&nbsp Name :
                </td>
                <td>
                    <asp:TextBox ID="txtBHPName" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Line 1&amp;2:</td>
                <td>
                    <asp:TextBox ID="txtBHPLine" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Address:</td>
                <td>
                    <asp:TextBox ID="txtBHPAddress" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    City:</td>
                <td>
                    <asp:TextBox ID="txtBHPCity" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    State:</td>
                <td>
                    <asp:TextBox ID="txtBHPState" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Zip Code:</td>
                <td>
                    <asp:TextBox ID="txtBHPZipCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Carrier Case Number:</td>
                <td>
                    <asp:TextBox ID="txtCarrierCaseNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Carrier or Self-Insured Employer I.D.#:</td>
                <td>
                    <asp:TextBox ID="txtCarrierEmpId" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Date of Accident:</td>
                <td>
                    <asp:TextBox ID="txtdataofaccident" runat="server"></asp:TextBox><asp:ImageButton ID="ImageacciDate" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtdataofaccident"
                        PopupButtonID="ImageacciDate" Enabled="True" />
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Name and Mailing Address of Carrier (MAXIMUM 30 CHARACTER)</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Name:</td>
                <td>
                    <asp:TextBox ID="txtMCName" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Line 1&amp;2:</td>
                <td>
                    <asp:TextBox ID="txtMCLine" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Address:</td>
                <td>
                    <asp:TextBox ID="txtMCAddress" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    City:</td>
                <td>
                    <asp:TextBox ID="txtMCCity" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    State:</td>
                <td>
                    <asp:TextBox ID="txtCPState" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Zip Code:</td>
                <td>
                    <asp:TextBox ID="txtMCZipCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Country Where Service Was Rendered:</td>
                <td>
                    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Claimant's 
                    Social Security Number:</td>
                <td>
                    <asp:TextBox ID="txtClaimantSSNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Name of Claimant (First,
                    Middle Initial,
                    Last Name):</td>
                <td>
                    <asp:TextBox ID="txtClaimantName" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Name and Mailing Address of Employer (MAXIMUM 30 CHARACTER)</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Name:</td>
                <td>
                    <asp:TextBox ID="txtMEName" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Line 1&amp;2:</td>
                <td>
                    <asp:TextBox ID="txtMELine" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Address:</td>
                <td>
                    <asp:TextBox ID="txtMEAddress" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    City:</td>
                <td>
                    <asp:TextBox ID="txtMECity" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; height: 21px; text-align: right">
                    State:</td>
                <td style="height: 21px">
                    <asp:TextBox ID="txtMEState" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Zip Code:</td>
                <td>
                    <asp:TextBox ID="txtMEZipCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp Date(s) of Health Provider's:
                </td>
                <td>
                    <asp:TextBox ID="TxtDateofHelthProvider" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="ImgHelthProviderDate" runat="server" ImageUrl="~/Images/cal.gif" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TxtDateofHelthProvider"
                        PopupButtonID="ImgHelthProviderDate" Enabled="True" />
                    <asp:Button ID="btnSign" runat="server" Text="Provider Signature" OnClientClick=" return ShowDocSignaturePopup()" />
                  
                </td>
            </tr>

            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp 
                    ANY PERSON WHO KNOWINGLY AND WITH INTENT TO DEFRAUD PRESENTS, CAUSES TO BE PRESENTED,
                    OR PREPARES WITH KNOWLEDGE OR BELIEF THAT IT WILL BE PRESENTED TO OR BY AN INSURER OR SELFINSURER, ANY INFORMATION CONTAINING ANY FALSE MATERIAL ITATEMENT OR CONCEALS
                    ANY MATERIAL FACT SHALL BE GUILTY OF A CRIME AND SUBJECT TO SUBSTANTIAL FINES AND
                    IMPRISONMENT.
                </td>
                <td valign="top">
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    SECTION A: REQUEST FOR ADMINISTRATIVE AWARD - PLEASE COMPLETE THE FOLLOWING
                </td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    1.Federal Tax I.D. Number:<br />
                    <asp:CheckBox ID="chkSSNSectionA" runat="server" />
                    SSN<asp:CheckBox ID="chkEINSectionA" runat="server" />EIN
                </td>
                <td>
                    <asp:TextBox ID="txtFedaralTaxIdSectionA" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp2.Patient's &nbsp;&nbsp;Account No:
                </td>
                <td>
                    <asp:TextBox ID="txtPatientAccNoSectionA" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp3.Total Charges:
                </td>
                <td>
                    <asp:TextBox ID="txtTotalChargeSectionA" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp 4.Amount Paid:
                </td>
                <td>
                    <asp:TextBox ID="txtAmountPaidSectionA" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp 5.Amount in Dispute:
                </td>
                <td>
                    <asp:TextBox ID="txtAmountDisputeSectionA" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    SECTION B: REQUEST FOR ARBITRATION - PLEASE COMPLETE THE FOLLOWING</td>
                <td>
                    &nbsp</td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    1.Federal Tax I.D. Number:<br />
                    <asp:CheckBox ID="chkSSNSectionB" runat="server" />
                    SSN<asp:CheckBox ID="chkEINSectionB" runat="server" />EIN
                </td>
                <td>
                    <asp:TextBox ID="txtFedaralTaxIdSectionB" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp 2.Patient's Account No:
                </td>
                <td>
                    <asp:TextBox ID="txtPatientAccNoSectionB" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp3.Total &nbsp;&nbsp;Charges:
                </td>
                <td>
                    <asp:TextBox ID="txtTotalChargeSectionB" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    4.Amount Paid:</td>
                <td>
                    <asp:TextBox ID="txtAmountPaidSectionB" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    5.Amount in Dispute:</td>
                <td>
                    <asp:TextBox ID="txtAmountDisputeSectionB" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    6.Amount of free submited</td>
                <td>
                    <asp:TextBox ID="txtAmountFreeSubmited" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    &nbsp&nbsp Arbitration Fees:
                </td>
                <td>
                    <asp:TextBox ID="txtArbitrationFees" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    &nbsp&nbsp A.Date you first treated claimant:
                </td>
                <td>
                    <asp:TextBox ID="txtfirsttreatmentdate" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtontxtfirsttreatmentdate" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtfirsttreatmentdate"
                        PopupButtonID="ImageButtontxtfirsttreatmentdate" Enabled="True" />
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    B.Was first treatment rendered by you?<br />
                    IF 'NO,' Complete Items C.through L.<br />
                    IF 'YES,' Complete Only Items G.through L<br />
                    <asp:CheckBox ID="chkFirstTreatmentYes" runat="server" />YES
                    <asp:CheckBox ID="chkFirstTreatmentNo" runat="server" />NO.
                </td>
                <td></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    Name and Address of provider that rendered first treatment (MAXIMUM 30)
                </td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px;">
                    Name:
                </td>
                <td>
                    <asp:TextBox ID="txtPFirstTName" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Line 1&amp;2:</td>
                <td>
                    <asp:TextBox ID="txtPFirstTLine" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Address:</td>
                <td>
                    <asp:TextBox ID="txtPFirstTAddress" runat="server" MaxLength="30" Width="299px"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    City:</td>
                <td>
                    <asp:TextBox ID="txtPFirstTCity" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    State:</td>
                <td>
                    <asp:TextBox ID="txtPFirstTState" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Zip Code:</td>
                <td>
                    <asp:TextBox ID="txtPFirstTZipCode" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    D.Diagnosis rendered by provider indicated by item C:</td>
                <td>
                    <asp:TextBox ID="txtDiagnosisRenderByC" runat="server" Height="57px" TextMode="MultiLine" Width="338px" style="resize: none"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    E.Treatment rendered by provider indicated in Item C:</td>
                <td>
                    <asp:TextBox ID="txtTreatementByC" runat="server" Height="57px" TextMode="MultiLine" Width="338px" style="resize: none"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    F.Date provider in Item
                    C. last treated or discharged claimant:</td>
                <td>
                    <asp:TextBox ID="txtdateproviderinitemc" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtontxtdateproviderinitemc" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtdateproviderinitemc"
                        PopupButtonID="ImageButtontxtdateproviderinitemc" Enabled="True" />
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    G.Your diagnosis:</td>
                <td>
                    <asp:TextBox ID="txtYourDiagnosis" runat="server" Height="57px" TextMode="MultiLine" Width="338px" style="resize: none"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    H. Total number of treatments from first treatment to current date&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtTotalNoofTreatement" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    I. Are you still treating this claimant?<br />
                    F 'YES,' Move to Item J.<br />
                    IF 'NO,' Give date and condition<br />
                    of patient when discharged<br /><asp:CheckBox ID="chkAreYouStillReadingYes" runat="server" />YES
                    <asp:CheckBox ID="chkAreYouStillReadingNo" runat="server" />NO&nbsp;</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="text-align: right; width: 436px; height: 21px;">
              
                    Date:&nbsp;
                </td>
                <td style="height: 21px">
                    <asp:TextBox ID="txt_I_Date" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtontxt_I_Date" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_I_Date"
                        PopupButtonID="ImageButtontxt_I_Date" Enabled="True" />
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Condition:</td>
                <td>
                    <asp:TextBox ID="txtCondition" runat="server" Height="57px" TextMode="MultiLine" Width="338px" style="resize: none" ReadOnly="true" ></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    J.Total amount of bill(s) including amount in dispute:&nbsp;</td>
                <td>
                    $<asp:TextBox ID="txtJTotalAmount" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; height: 21px; text-align: right">
                    K.Amount previously paid to you on this case, if any:&nbsp;</td>
                <td style="height: 21px">
                    $<asp:TextBox ID="txtKAmountPreviously" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    L.Amount previously paid to You on disputed bill(s), if any:&nbsp;</td>
                <td>
                    $<asp:TextBox ID="txtLAmountPreviasly" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    I certify that the foregoing bill(s) was originally submitted on Form C-4, UB-92
                    or HCFA-1500 to the responsible carrier/self-insured employer for payment. Acceptable
                    payment has not been received, arbitration is required. In the event I fail to appear
                    at the scheduled hearing, I will abide by the decision of the Committee.</td>
                <td>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td class="td-CaseDetails-lf-desc-ch" style="width: 436px; text-align: right">
                    Helth Provider's Date</td>
                <td>
                    <asp:TextBox ID="txthelthproviderdate" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButtontxthelthproviderdate" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txthelthproviderdate"
                        PopupButtonID="ImageButtontxthelthproviderdate" Enabled="True" />
                        <asp:Button ID="btn_Patient_Sign" runat="server" Text="Provider Signature" OnClientClick=" return ShowPatientSignaturePopup()"
                                CssClass="Buttons" />
                </td>
            </tr>
        </table>
        <table width="100%" style="font-size: 12pt; font-family: Times New Roman">
            <tr>
                <td style="width: 327px; height: 25px;">
                    </td>
                <td style="width: 66px; height: 25px;">
                    <asp:Button ID="btnSaveBottom" runat="server" Text="Save"
                        Height="26px" Width="86px" onclick="btnSaveBottom_Click" /></td>
                <td style="width: 27px; height: 25px">
                </td>
                <td style="height: 25px">
                    <asp:Button ID="btnPrinBottom" runat="server" Text="Print"
                        Height="27px" Width="92px" onclick="btnPrinBottom_Click" />
                    <asp:TextBox ID="txtbillno" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtProviderSign1" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtProviderSign2" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 327px">
                </td>
            </tr>
        </table>
    <asp:HiddenField ID="hfbillno" runat="server" />
        <div id="divDocSignature" style="position: relative; width: 500px; height: 350px;
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
    </div>
        <div>
        </div>
    </form>
</body>
</html>
