<%@ Page Language="C#" MasterPageFile="~/ATT/AttMaster.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ATT_WorkArea.aspx.cs" Inherits="ATT_Bill_Sys_ATT_WorkArea"
    Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
function showPateintFrame(objCaseID,objflag,objCompanyID)
        {
	    // alert(objCaseID + ' ' + objCompanyID);
	        var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute'; 
            document.getElementById('divfrmPatient').style.left= '400px'; 
            document.getElementById('divfrmPatient').style.top= '250px'; 
            document.getElementById('divfrmPatient').style.visibility='visible'; 
            document.getElementById('frmpatient').src="../CaseDetailsPopup.aspx?CaseID="+objCaseID+"&cmpId="+ objCompanyID+"&flag="+objflag+"";
            return false;   
        }
        function ClosePatientFramePopup()
               {
                 //   alert("");
                   //document.getElementById('divfrmPatient').style.height='0px';
                    document.getElementById('divfrmPatient').style.visibility='hidden';
                   document.getElementById('divfrmPatient').style.top='-10000px';
                    document.getElementById('divfrmPatient').style.left='-10000px';
             }
    </script>

    <table width="100%">
        <tr>
            <td style="width: 100%;">
                <table width="98%">
                    <tr>
                        <td align="right">
                            <asp:LinkButton ID="lnkBack" runat="server" Text="Back" OnClick="lnkBack_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <table style="width: 1260px;">
                    <tr>
                        <td style="width: 1260px;">
                            <asp:DataList ID="DtlPatientDetails" runat="server" BorderWidth="0px" BorderStyle="None"
                                CssClass="TDPart" BorderColor="#DEBA84" Width="100%">
                                <ItemTemplate>
                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 98%; border: #8babe4 1px solid #B5DF82;">
                                        <tr>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Case#</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                                <b>Chart No</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Patient Name</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Insurance Name</b>
                                            </td>
                                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                <b>Accident Date</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_ID")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CHART_NO")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME")%>
                                            </td>
                                            <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT", "{0:MM/dd/yyyy}")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <asp:DataList ID="DtlView" runat="server" BorderWidth="0px" BorderStyle="None" CssClass="TDPart"
                                BorderColor="#DEBA84" Width="100%">
                                <ItemTemplate>
                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: #8babe4 1px solid #B5DF82;">
                                        <tr>
                                            <td class="td-widget-lf-top-holder-division-ch">
                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                    border: 1px solid #B5DF82;">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 490px;">
                                                            &nbsp;<b class="txt3">Personal Information</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 490px;">
                                                            <!-- outer table to hold 2 child tables -->
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                <tr>
                                                                    <td class="td-widget-lf-holder-ch">
                                                                        <!-- Table 1 - to hold the actual data -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    First Name
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Middle Name
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    <asp:Label ID="lblViewMiddleName" runat="server" class="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Last Name
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    D.O.B
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_DOB") %>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Gender
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    <asp:Label runat="server" ID="lblViewGender" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Address
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    City
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    State
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <asp:Label runat="server" ID="lblViewPatientState" class="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Home Phone
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Work
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    ZIP
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    &nbsp;
                                                                                    <asp:CheckBox ID="chkViewWrongPhone" Visible="False" Enabled="False" runat="server"
                                                                                        Text="Wrong Phone" TextAlign="Left" />
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Email
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Extn.
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Attorney
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblViewAttorney" runat="server" class="lbl"></asp:Label>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Case Type
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblViewCasetype" runat="server" class="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Case Status
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <asp:Label runat="server" ID="lblViewCaseStatus" class="lbl"></asp:Label>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    SSN
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    <asp:Label ID="lblLocation" Text="Location" runat="server" class="lbl" Style="font-weight: bold;"></asp:Label>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    <asp:Label runat="server" ID="lblVLocation1" class="lbl"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkPatientTransport" Visible="False" Enabled="False" runat="server"
                                                                                        Text="Transport" TextAlign="Left"></asp:CheckBox></td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="td-widget-lf-top-holder-division-ch" valign="top">
                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                    border: 1px solid #B5DF82;">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                            &nbsp;<b class="txt3">Insurance Information</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 490px;">
                                                            <!-- outer table to hold 2 child tables -->
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                <tr>
                                                                    <td class="td-widget-lf-holder-ch">
                                                                        <!-- Table 1 - to hold the actual data -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Policy Holder
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem,"SZ_POLICY_HOLDER") %>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Name
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME") %>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Ins. Address
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    <asp:Label ID="lblViewInsuranceAddress" runat="server" class="lbl"></asp:Label>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Address
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ADDRESS") %>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    City
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_CITY") %>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    State
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_STATE") %>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    ZIP
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ZIP") %>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Phone
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_PHONE")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                    FAX
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX_NUMBER")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                    Contact Person
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_CONTACT_PERSON")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Claim File#
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp; <a id="lnkViewInsuranceClaimNumber" style="text-decoration: underline; color: Blue;"
                                                                                        runat="server" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+claimno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%>
                                                                                    </a>
                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtViewInsuranceClaimNumber" runat="server"  TargetControlID="lnkViewInsuranceClaimNumber"
                                                                                                                   PopupControlID="pnlClaimNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID=""  />--%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Policy #
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp; <a id="lnkViewPolicyNumber" style="text-decoration: underline; color: Blue;"
                                                                                        runat="server" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+policyno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%>
                                                                                    </a>
                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtViewPolicyNumber" runat="server" TargetControlID="lnkViewPolicyNumber" 
                                                                                                                    PopupControlID="pnlPolicyNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td-widget-lf-top-holder-division-ch">
                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                    border: 1px solid #B5DF82;">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                            &nbsp;<b class="txt3">Accident Information</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 490px;">
                                                            <!-- outer table to hold 2 child tables -->
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                <tr>
                                                                    <td class="td-widget-lf-holder-ch">
                                                                        <!-- Table 1 - to hold the actual data -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Accident Date
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp; <a id="lnkDateOfAccList" style="text-decoration: underline; color: Blue;"
                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+dtaccident+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%>
                                                                                    </a>
                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtDateOfAccList" runat="server" TargetControlID="lnkDateOfAccList" 
                                                                                                                    PopupControlID="pnlDateOfAccList" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Plate Number
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp; <a id="lnkViewAccidentPlatenumber" style="text-decoration: underline; color: Blue;"
                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+plateno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%>
                                                                                    </a>
                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentPlatenumber" runat="server" TargetControlID="lnkViewAccidentPlatenumber" 
                                                                                                                    PopupControlID="pnlPlateNo" OffsetX="-100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Report Number
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp; <a id="lnkViewAccidentReportNumber" style="text-decoration: underline; color: Blue;"
                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+accidentreportno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%>
                                                                                    </a>
                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentReportNumber" runat="server" TargetControlID="lnkViewAccidentReportNumber" 
                                                                                                                PopupControlID="pnlReportNO" OffsetX="-300" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Address
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_ADDRESS")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    City
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_CITY")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    State
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <asp:Label runat="server" ID="lblViewAccidentState" class="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Hospital Name
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_NAME")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Hospital Address
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_ADDRESS")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Date Of Admission
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_ADMISSION_DATE")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Additional Patient
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FROM_CAR")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Describe Injury
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_DESCRIBE_INJURY")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Patient Type
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <asp:Label runat="server" ID="lblPatientType" class="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="td-widget-lf-top-holder-division-ch" valign="top">
                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                    border: 1px solid #B5DF82;">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                            &nbsp;<b class="txt3">Employer Information</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 490px;">
                                                            <!-- outer table to hold 2 child tables -->
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                <tr>
                                                                    <td class="td-widget-lf-holder-ch">
                                                                        <!-- Table 1 - to hold the actual data -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Name
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_NAME")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Address
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ADDRESS")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    City
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_CITY")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    State
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <asp:Label runat="server" ID="lblViewEmployerState" class="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    ZIP
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ZIP")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Phone
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_PHONE")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Date Of First Treatment
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_FIRST_TREATMENT")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    <asp:Label ID="lblView" runat="server" Text="Chart No." class="lbl" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    <asp:Label ID="lblViewChartNo" runat="server" class="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td-widget-lf-top-holder-division-ch">
                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 96%;
                                                    border: 1px solid #B5DF82;">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                            &nbsp;<b class="txt3">Adjuster Information</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 490px;">
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                <tr>
                                                                    <td class="td-widget-lf-holder-ch">
                                                                        <!-- Table 1 - to hold the actual data -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Name
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp; <a id="lnkViewAdjusterName" style="text-decoration: underline; color: Blue;"
                                                                                        runat="server" title="Claim List" class="lbl" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+adjustername+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                                                        <asp:Label runat="server" ID="lblViewAdjusterName" class="lbl"></asp:Label>
                                                                                        <%--<%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>--%>
                                                                                    </a>
                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAdjusterName" runat="server" TargetControlID="lnkViewAdjusterName" 
                                                                                                                    PopupControlID="pnlAdjuster" OffsetX="100" OffsetY="-300" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Phone
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PHONE")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Extension
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EXTENSION")%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    FAX
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX")%>
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                    Email
                                                                                </td>
                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                    &nbsp;
                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMAIL")%>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
        visibility: hidden; border-left: silver 1px solid; width: 700px; border-bottom: silver 1px solid;
        position: absolute; top: 682px; height: 280px; background-color: white" id="divfrmPatient">
        <div style="position: relative; background-color: #B5DF82; text-align: right">
            <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
        </div>
        <iframe id="frmpatient" src="" frameborder="0" width="700" height="370"></iframe>
    </div>
    <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtCompanyId" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtPatientId" runat="server" Width="10px" Visible="false"></asp:TextBox>
</asp:Content>
