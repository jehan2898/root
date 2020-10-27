<%@ Page Title="" Language="C#" MasterPageFile="~/attorney.master" AutoEventWireup="true" CodeFile="atcasedetails.aspx.cs" Inherits="AJAX_Pages_atcasedetails" %>
<%@ Register Src="~/UserControl/PatientInformation.ascx" TagName="UserPatientInfoControl"  TagPrefix="UserPatientInfo" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharizationAttorney.ascx" TagName="CheckPageAutharization"  TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/Bill_Sys_AssociateCases.ascx" TagName="Bill_Sys_AssociateCases" TagPrefix="ASC" %>
<%@ Register Src="~/UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="../js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.min.js"></script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" 
    TargetControlID="Panel1"
    CollapsedSize="20"
    ExpandedSize="160"
    Collapsed="true"
    AutoCollapse="false"
    AutoExpand="false"
    TextLabelID="lblcpe"
    CollapsedText="(Show Details...)"
    ExpandedText="(Hide Details)" 
    ExpandControlID="LinkButton1"
    CollapseControlID="LinkButton1"
    ImageControlID="Image1"
    ExpandedImage="../Images/up.jpg"
    CollapsedImage="../Images/dwn.jpg"
    />


<style type="text/css">
    .iframe {
        -moz-border-radius: 12px;
        -webkit-border-radius: 12px;
        -border-radius: 12px;
        -moz-box-shadow: 4px 4px 14px #000;
        -webkit-box-shadow: 4px 4px 14px #000;
        -box-shadow: 4px 4px 14px #000;
        -filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=.2);
    }

    .space {
        padding-bottom: 1em;
    }
</style>
    <script type="text/javascript" src="~/validation.js"></script>

    <script language="javascript" type="text/javascript">

        jQuery(function ($) {

            $("#ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_tabpnlPersonalInformation_txtCellNo").mask("999-999-9999");
            $("#ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_tabpnlPersonalInformation_txnEmergencyContact").mask("999-999-9999");
        });

    </script>

    <script language="javascript" type="text/javascript">

        document.onmousedown = function () {
            var CaseValue = document.getElementById("<%= SessionCheck.ClientID%>").value;
            if (document.getElementById("<%= SessionCheck.ClientID%>").value != "") {
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4) {
                        var SessionVar = xmlhttp.responseText;
                    }
                }
                xmlhttp.open("GET", "Bill_Sys_CaseDetails.aspx?CheckSession=" + CaseValue, true);
                xmlhttp.send("");
            }
        }

        function HideNotes() {
            //   document.getElementById('test').value = 20;
        }

        function Check(p_szname, p_fieldname) {

            if (document.getElementById("" + p_szname).value == 'NA') {
                alert('Please Select ' + p_fieldname);
                return false;
            }
            else if (document.getElementById("" + p_szname).value == '') {
                alert('Please Enter ' + p_fieldname);
                return false;
            }
            return true;
        }



    </script>

    <script type="text/javascript">
        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }
        function CheckForInteger(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }


        }

        function ShowGenerateNF2Link() {
            var chkLink = document.getElementById('_ctl0_ContentPlaceHolder1_chkStatusProc');
            if (chkLink.checked) {
                return true;
            }
            else {
                alert('Please check the status');
                return false;
            }
        }

        function showDiagnosisCodePopup() {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.height = '180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.position = "absolute";
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.top = '300px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.left = '620px';
        }

        function showAdjusterPanel() {
            document.getElementById('divAdjuster').style.visibility = 'visible';
            document.getElementById('divAdjuster').style.position = 'absolute';
            document.getElementById('divAdjuster').style.left = '300px';
            document.getElementById('divAdjuster').style.top = '300px';
            document.getElementById('divAdjuster').style.zIndex = 1;
        }

        function showAdjusterPanelAddress() {
            //  _ctl0_ContentPlaceHolder1_tabcontainerPatientEntry_tabpnlInsuranceInformation_extddlInsuranceCompany.
            var insstatus = document.getElementById("ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_tabpnlInsuranceInformation_txtInsuranceCompany").value;
            if (insstatus == '' || insstatus == 'No suggestions found for your search') {
                alert('Select Insurance Company');

            }
            else {
                document.getElementById('divAddress').style.visibility = 'visible';
                document.getElementById('divAddress').style.position = 'absolute';
                document.getElementById('divAddress').style.left = '300px';
                document.getElementById('divAddress').style.top = '370px';
                document.getElementById('divAddress').style.zIndex = 1;
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCD").value = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCD").focus();
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCD").style.backgroundColor = "";

                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceStreetCD").value = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceCityCD").value = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceZipCD").value = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_extddlInsuranceStateNew").value = 'NA';
                document.getElementById("_ctl0_ContentPlaceHolder1_IDDefault").checked = false;

                document.getElementById('divAddressError').innerHTML = '';
                //return false;
            }


        }
        function CloseDiagnosisCodePopup() {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.height = '0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.visibility = 'hidden';
            //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
        }


        function checkAddressDetails() {

            if (document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCD").value == '') {
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCD").focus();
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCD").style.backgroundColor = "#ffff99";
                document.getElementById('divAddressError').innerHTML = 'Enter the mandatory field';
                return false;
            }

        }

        function showPateintFrame(objCaseID, objflag, objCompanyID) {
            // alert(objCaseID + ' ' + objCompanyID);
            var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute';
            document.getElementById('divfrmPatient').style.left = '400px';
            document.getElementById('divfrmPatient').style.top = '250px';
            document.getElementById('divfrmPatient').style.visibility = 'visible';
            document.getElementById('frmpatient').src = "CaseDetailsPopup.aspx?CaseID=" + objCaseID + "&cmpId=" + objCompanyID + "&flag=" + objflag + "";
            return false;
        }

        function ClosePatientFramePopup() {
            //   alert("");
            //document.getElementById('divfrmPatient').style.height='0px';
            document.getElementById('divfrmPatient').style.visibility = 'hidden';
            document.getElementById('divfrmPatient').style.top = '-10000px';
            document.getElementById('divfrmPatient').style.left = '-10000px';
        }

        function showAttorney() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_tabpnlAttorneyInformation_extddlAttorneyAssign").value == 'NA') {
                alert('Please Select Attorney');
                return false;
            }
        }
        function showAdjuster() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_tabpnlInsuranceInformation_extddlAdjuster").value == 'NA') {
                alert('Please Select Adjuster ');
                return false;
            }
        }



        function OpenPatientInfoPdf() {
            var caseid = document.getElementById('<%=txtCaseID.ClientID%>').value;
            var cmpid = document.getElementById('<%=txtCompanyID.ClientID%>').value;
            window.open('Bill_Sys_ViewPatientInformation.aspx?caseid=' + caseid + '&cmpid=' + cmpid, 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');
            return false;
        }
        function showTransportFrame() {
            var caseid = document.getElementById('<%=txtCaseID.ClientID%>').value;
            var cmpid = document.getElementById('<%=txtCompanyID.ClientID%>').value;
            document.getElementById('dvTransportation').style.zIndex = 1;
            document.getElementById('dvTransportation').style.position = 'absolute';
            document.getElementById('dvTransportation').style.left = '400px';
            document.getElementById('dvTransportation').style.top = '150px';
            document.getElementById('dvTransportation').style.visibility = 'visible';
            document.getElementById('ifrmTransportation').src = "Bill_Sys_TransportationPopUP.aspx?caseid=" + caseid + "&cmpid=" + cmpid;
            return false;
        }
        function ClosetransportFramePopup() {
            document.getElementById('dvTransportation').style.visibility = 'hidden';

        }
        function showUpdateAdjusterFrame() {
            var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
            var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
            var objAdjusterID = document.getElementById("<%= hdadjusterCode.ClientID %>").value;
            var flag = "update";
            var url = "Adjuster.aspx?CaseID=" + objCaseID + "&adjcompany=" + objCompanyID + "&link=" + flag + "&objAdjusterID=" + objAdjusterID;
            AddAdjusterPop.SetContentUrl(url);
            AddAdjusterPop.Show();
            return false;
        }
        function ClosePopup() {
            AddAdjusterPop.Hide();
            var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
            var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
            var sURL = unescape(window.location.pathname);
            sURL = sURL + "?CaseID=" + objCaseID + "&cmp=" + objCompanyID;
            window.location.href = sURL;
        }

        function ClosePopup1() {
            AddAdjusterPop.Hide();
            var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
            var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
            var sURL = unescape(window.location.pathname);
            sURL = sURL + "?CaseID=" + objCaseID + "&cmp=" + objCompanyID;
            window.location.href = sURL;
        }
        function showAdjusterFrame(objCaseID, objCompanyID) {

            var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
            var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
            var url = "Adjuster.aspx?CaseID=" + objCaseID + "&adjcompany=" + objCompanyID + "";
            AddAdjusterPop.SetContentUrl(url);
            AddAdjusterPop.Show();
            return false;
        }
        //Nirmalkumar
        function showSecondaryInsurance() {

            var url = "SecondaryInsuraceViewFrame.aspx";

            SecInsuracePop.SetContentUrl(url);
            SecInsuracePop.Show();
            return false;

        }
        //End
    </script>

   

    <script type="text/javascript">
        function OpenExcuseLeters() {

            window.open('OpenExcuseLeters.aspx?', 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');
            return false;
        }
        </script>
    <script type="text/javascript" src="validation.js"></script>

    <table id="First" style="width: 100%; height: 100%" bgcolor="white">
        <tr>
        <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
            padding-top: 3px; height: 100%">
            <table id="MainBodyTable" style="width: 100%; border: 0;">
                <tr>
                    <td class="LeftTop">                       
                    </td>
                    <td class="CenterTop">                     
                    </td>
                    <td class="RightTop">
                    </td>
                </tr>
                <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        
                        <td style="width: 100%" >
                          <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                        <UserPatientInfo:UserPatientInfoControl runat="server" ID="UserPatientInfoControl" />
                            </contenttemplate>
                            </asp:UpdatePanel> 
                           
                        </td>
                    </tr>
             
                <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table style="width: 100%; height: 100%">
                        
                                   <tr>
                                    <td style="width: 100%;" >
                                        <table class="ContentTable" style="width: 100%; height: 250px;">
                                          <tr>
                                                <td colspan="6" align="center">
                                                    <asp:UpdatePanel ID="pnlmsg" runat="server">
                                                    <ContentTemplate>
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false" />
                                                    <UserMessage:MessageControl runat="server" id="usrMessage" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td colspan="6" style="height: 15px; text-align: left;" >
                                                    <%-- <asp:LinkButton ID="hlnkAssociate" runat="server" visible="false" OnClick="hlnkAssociate_Click">Associate Diagnosis Codes</asp:LinkButton> |        
                       <asp:LinkButton ID="hlnkPatientDesk" runat="server"   OnClick="hlnkPatientDesk_Click" >Patient Desk</asp:LinkButton>--%>
                                         <ajaxToolkit:TabContainer ID="tabcontainerPatientEntry" runat="Server" ActiveTabIndex="0"
                                                        >
                                                        <ajaxToolkit:TabPanel runat="server" ID="tabPnlViewAll" TabIndex="6" Height="800px">
                                                            <HeaderTemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    View All
                                                                </div>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div align="left">
                                                                <asp:DataList ID="DtlView" runat="server" CssClass="TDPart" BorderWidth="0px" BorderStyle="None" 
                                                                        BorderColor="#DEBA84" RepeatColumns="1" Width="100%">
                                                                        <ItemTemplate>
                                                                        <table id="lastTablel" runat="server" class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0" border="0" bgcolor="white">
                                                                        <tr>
                                                                            <td class="td-widget-lf-top-holder-division-ch" colspan="2">
                                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 100%;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 800px;">
                                                                                            &nbsp;<b class="txt3">Personal Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td  class="td-widget-lf-holder-ch" colspan="2">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
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
                                                                                                                     Cell Phone #
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "sz_patient_cellno")%></td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Work
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                                                                                </td>
                                                                                                                 <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    ZIP
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                   Secondary Phone
												                                                                    <asp:CheckBox ID="chkViewWrongPhone" Visible="False" Enabled="False" runat="server" Text="Wrong Phone" TextAlign="Left" />
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                     <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>
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
                                                                                                <%-- Copy From--%>
                                                                                                <asp:Label ID="lblcopyfrm" runat="server" Text ="Copy From"  Font-Names ="Verdana" Font-Bold ="true" Font-Size ="11px" ></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch" colspan="3">
                                                                                                <asp:Label runat="server" ID="lblcopyfrom" class="lbl"></asp:Label>
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                Allergies
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch" style="overflow-wrap: break-word; word-break: break-all; overflow-x: hidden; width: 200px; position: relative; text-align:right;">
                                                                                                                  
                                                                                                <%# DataBinder.Eval(Container.DataItem, "sz_allergies")%>
                                                                                                </td>

                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                Emergency Contacts
                                                                                                </td>
                                                                                                <td class="td-CaseDetails-lf-data-ch" style="overflow-wrap: break-word; word-break: break-all; overflow-x: hidden; width: 200px; position: relative; text-align:right;">
                                                                                                                  
                                                                                                <%# DataBinder.Eval(Container.DataItem, "sz_emergency_contact")%>
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
                                                                                 <td class="td-widget-lf-top-holder-division-ch" colspan="2">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 800px;">
                                                                                            &nbsp;<b class="txt3">Insurance Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 100%;">
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
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Claim File#
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                 <a id="lnkViewInsuranceClaimNumber" style="text-decoration: underline; color:Blue;" runat="server" class="lbl"  
                                                                                                                 href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+claimno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%></a>
                                                                                                                   <%--<ajaxToolkit:PopupControlExtender ID="popExtViewInsuranceClaimNumber" runat="server"  TargetControlID="lnkViewInsuranceClaimNumber"
                                                                                                                   PopupControlID="pnlClaimNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID=""  />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Policy #
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewPolicyNumber" style="text-decoration: underline; color: Blue;" runat="server" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+policyno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%> </a>
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
                                                                            <td class="td-widget-lf-top-holder-division-ch" colspan="2">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 800px;">
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
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkDateOfAccList" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+dtaccident+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%></a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtDateOfAccList" runat="server" TargetControlID="lnkDateOfAccList" 
                                                                                                                    PopupControlID="pnlDateOfAccList" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Plate Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewAccidentPlatenumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+plateno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' ><%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%></a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentPlatenumber" runat="server" TargetControlID="lnkViewAccidentPlatenumber" 
                                                                                                                    PopupControlID="pnlPlateNo" OffsetX="-100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                 <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Report Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                <a id="lnkViewAccidentReportNumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+accidentreportno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%></a>
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
                                                                          
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                              <td class="td-widget-lf-top-holder-division-ch" colspan="2">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 800px;">
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
                                                                            <td class="td-widget-lf-top-holder-division-ch" colspan="2">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                                                                    border: 1px solid #B5DF82;" id="tblF">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 100%;">
                                                                                            &nbsp;<b class="txt3">Adjuster Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch" >
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewAdjusterName" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+adjustername+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' >
                                                                                                                    <asp:Label runat="server" ID="lblViewAdjusterName" class="lbl"></asp:Label>
                                                                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>--%></a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAdjusterName" runat="server" TargetControlID="lnkViewAdjusterName" 
                                                                                                                    PopupControlID="pnlAdjuster" OffsetX="100" OffsetY="-300" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                                 <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            Address
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_ADDRESS")%>
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            City
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_CITY")%>
                                                                                                                  </td>
                                                                                                            </tr>
                                                                                                            
                                                                                                            <tr>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            State
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_STATE")%>
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            Zip
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_ZIP")%>
                                                                                                                        </td>
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
                                                                                        <td>
                                                                                        <div id="abc" runat="server">
                                                                                        </div>
                                                                                        </td>
                                                                                       
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                           
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                        
                                        </table>
                                        
                                        </DIV> </ContentTemplate> </ajaxToolkit:TabPanel>
                                      
                                      </ajaxToolkit:TabContainer> 
                                      </ajaxToolkit:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" colspan="6">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2"
                                                    displayafter="10">
                                                    <progresstemplate>
                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 550px;
                                                            left: 600px" runat="Server">
                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Searching....."
                                                                Height="40px" Width="40px"></asp:Image>
                                                            Processing...
                                                        </div>
                                                    </progresstemplate>
                                                </asp:updateprogress>
                                               
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" colspan="6">
                                        <asp:TextBox ID="txtCompanyIDForNotes" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCompanyID" runat="server" Style =" visibility:hidden;" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Style =" visibility:hidden;" ></asp:TextBox>
                                        <%--<asp:TextBox ID="txtChartNo" runat="server" Width="10px" Visible="false"></asp:TextBox>--%>

                                        <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                  <asp:TextBox ID="txtAdjusterid" runat="server" Visible="false"></asp:TextBox>
                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                                                    OldText="" StausText="False"  Visible="false"/>
                                        <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                                                    Flag_ID="txtCompanyID.Text.ToString();" OldText="" StausText="False" Visible="false" />

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
                        <td>
                            <asp:DataGrid ID="grdAssociatedDiagnosisCode" CssClass="GridTable" Width="100%" runat="server"
                                Visible="False" AutoGenerateColumns="False">
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="SZ_DIAGNOSIS_SET_ID" HeaderText="Set ID" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor Code" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <a href="Bill_Sys_AssociateDignosisCode.aspx?caseID=<%=Session["PassedCaseID"].ToString() %>&SetId=<%# DataBinder.Eval(Container.DataItem, "SZ_DIAGNOSIS_SET_ID") %>"
                                                target="_Blank">Add services </a>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Add services
                                        </HeaderTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <a href="Bill_Sys_AssociateDignosisCode.aspx?caseID=<%=Session["PassedCaseID"].ToString() %>&SetId=<%# DataBinder.Eval(Container.DataItem, "SZ_DIAGNOSIS_SET_ID") %>"
                                                target="_Blank">Delete services </a>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Delete services
                                        </HeaderTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    
                    <tr>
                        
                        <td style="width: 100%; height:160px" class="SectionDevider" valign="top">
                           <asp:Panel ID="Panel1" runat="server"  Width="100%">
	                        <table width="100%" cellpadding="0" cellspacing="0" style="width: 100%; height:25px; vertical-align:text-top;">
	                            <tr style="height:3px">
	                                <td align="center" style="width:90%;" valign="top">
	                                    <asp:LinkButton ID="LinkButton1" Font-Underline="false" ToolTip="Show Bills" runat="server"  ForeColor="white" >
	                                        Bills &nbsp;&nbsp; <asp:Label ID="lblcpe" runat="server" style="color:White" Visible="false"></asp:Label>
	                                    </asp:LinkButton>
	                                    
	                                </td>
	                               
	                                
	                                <td align="right">
	                                   <%-- <asp:Image ID="Image1" runat="server" />--%>&nbsp;&nbsp;&nbsp;
	                                    <%--<div id ="plusdiv" runat="server">
	                                        <img id="img2" src="../Images/Plus.JPG" alt="" style="border:none;"   runat="server"/> 
	                                        </div>
	                                       
	                                        <div id ="divimg" runat="server" style="visibility:hidden;">
	                                     <img id="img3" src="../Images/Minus.bmp" alt="" style="border:none;"  runat="server" />
	                                     </div>--%>
	                                </td>
	                              
	                            </tr>
	                        </table>
	                        <asp:UpdatePanel ID="Up_Bills" runat="server">
                            <ContentTemplate>
	                        </ContentTemplate>
	                        <Triggers>
	                        <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
	                        </Triggers>
	                        </asp:UpdatePanel>
                        </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div align="left" style="vertical-align: top;">
                                <div style="float: left; padding-right: 200px; text-indent: 10px;">
                                    </div>
                                <div style="vertical-align: top;">
                                    </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" >
                         </td>
                    </tr>
                </table>
            </td>
            <td class="RightCenter" style="width: 10px; height: 100%;">
            </td>
        </tr>
        <tr>
        <td class="LeftBottom">
        </td>
        <td class="CenterBottom">
        </td>
        <td class="RightBottom" style="width: 10px">
        </td>
    </tr>
    </table>
    
    </td>
    
     </tr>
    <tr>
        <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
            visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
            border-left: silver 1px solid; border-bottom: silver 1px solid; left: 176px;
            top: -70px;" align="center">
            <div style="position: relative; text-align: right; background-color: #8babe4;">
                <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
            </div>
            <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
                visible="false"></iframe>
        </div>
    </tr>
    </table>
   
   <asp:HiddenField ID="hdninsurancecode" runat="server" />
   <asp:HiddenField ID="hdnattorney" runat="server" />
  		 <input type="hidden" runat="server" id="SessionCheck" />
			 
	<input type="hidden" id="hdlimage" value="0" />
	        <asp:HiddenField ID="hdadjusterCode" runat="server" />
    <asp:HiddenField ID="hdacode" runat="server" />
           <asp:HiddenField ID="hdnattorneycode" runat="server" />
           <asp:HiddenField ID="hdattorneyset" runat="server" />
         <asp:TextBox ID="txtAttorneyid" runat="server" Visible="false"></asp:TextBox>
         
       <asp:TextBox ID="txtCaseTypeID" runat="server" Visible="false"></asp:TextBox>
</asp:Content>

