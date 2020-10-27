<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"   CodeFile="Bill_Sys_CaseDetails.aspx.cs" Inherits="Bill_Sys_CaseDetails" Title="Green Your Bills - Workarea"%>
<%@ Register Src="~/UserControl/PatientInformation.ascx" TagName="UserPatientInfoControl"  TagPrefix="UserPatientInfo" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"  TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.js"></script>
    <script type="text/javascript" src="../js/jquery.maskedinput.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <ajaxcontrol:CollapsiblePanelExtender ID="cpe" runat="Server" 
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
.ajax__scroll_none {
        overflow:scroll;
    }
        .iframe
        {
            -moz-border-radius: 12px;
            -webkit-border-radius: 12px;
            -border-radius: 12px;

            -moz-box-shadow: 4px 4px 14px #000;
            -webkit-box-shadow: 4px 4px 14px #000;
            -box-shadow: 4px 4px 14px #000;

            -filter:progid:DXImageTransform.Microsoft.BasicImage(rotation=.2);
        }
    </style>
<style type="text/css">
        .iframe
        {
            -moz-border-radius: 12px;
            -webkit-border-radius: 12px;
            -border-radius: 12px;

            -moz-box-shadow: 4px 4px 14px #000;
            -webkit-box-shadow: 4px 4px 14px #000;
            -box-shadow: 4px 4px 14px #000;

            -filter:progid:DXImageTransform.Microsoft.BasicImage(rotation=.2);
        }
    </style>
    <script type="text/javascript" src="~/validation.js"></script>

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



        function showAdjusterFrame() {
            // alert('');
            // alert(objCaseID + ' ' + objCompanyID);
            var obj3 = "";
            var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;

            // alert(objCompanyID );
            document.getElementById('divAdjusterfrm').style.zIndex = 1;
            document.getElementById('divAdjusterfrm').style.position = 'absolute';
            document.getElementById('divAdjusterfrm').style.left = '400px';
            document.getElementById('divAdjusterfrm').style.top = '250px';
            document.getElementById('divAdjusterfrm').style.visibility = 'visible';
            document.getElementById('frmadjuster').src = "Adjuster.aspx?adjcompany=" + objCompanyID + "";
            //  alert('done');
            return false;
        }

        function CloseadjusterFramePopup() {

            //document.getElementById('divfrmPatient').style.height='0px';
            document.getElementById('divAdjusterfrm').style.visibility = 'hidden';
            document.getElementById('divAdjusterfrm').style.top = '-10000px';
            document.getElementById('divAdjusterfrm').style.left = '-10000px';
            //document.getElementById("<%= btnadjcall.ClientID %>").click();
            //  alert('');
            document.getElementById("<%= btnadjcall.ClientID %>").click();
            //    alert('done');
        }


        function showAttorneyrFrame() {
            //alert('');
            // alert(objCaseID + ' ' + objCompanyID);
            var obj3 = "";
            var objCompanyID = document.getElementById("<%= hdnattorneycode.ClientID %>").value;
            var company = document.getElementById("<%= txtCompanyID.ClientID %>").value;
            //alert(objCompanyID);
            if (objCompanyID != 'NA' && objCompanyID != '') {
                document.getElementById('divedit').style.zIndex = 1;
                document.getElementById('divedit').style.position = 'absolute';
                document.getElementById('divedit').style.left = '400px';
                document.getElementById('divedit').style.top = '250px';
                document.getElementById('divedit').style.visibility = 'visible';
                document.getElementById('frmattupdate').src = "../Bill_Sys_PopupAttorny.aspx?edit=" + objCompanyID + "&cmp=" + company + "";
                //  alert('done');
                return false;
            }
            else {
                alert('Do you not select Attorney name select first');
                return false;
            }
        }

        function CloseatternoryFramePopup() {
            //   alert("");
            //document.getElementById('divfrmPatient').style.height='0px';
            document.getElementById('divedit').style.visibility = 'hidden';
            document.getElementById('divedit').style.top = '-10000px';
            document.getElementById('divedit').style.left = '-10000px';
            //  alert('');
            document.getElementById("<%= btnattcall.ClientID %>").click();
            //   alert('');



            // __doPostBack('Button2','My Argument'); 
            //alert('');
        }

        function ShowNotes() {
            //   document.getElementById('test').value = 10;
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



        function GetInsuranceValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdninsurancecode.ClientID %>").value = eventArgs.get_value();
        }

        function GetAttoreyValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdnattorney.ClientID %>").value = eventArgs.get_value();
        }

        function GeEmployerValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdnEmployerId.ClientID %>").value = eventArgs.get_value();
        }

        function ShowReminder() {

            document.getElementById('ReminderPopUP').style.zIndex = 1;
            document.getElementById('ReminderPopUP').style.position = 'absolute';
            document.getElementById('ReminderPopUP').style.left = '350px';
            document.getElementById('ReminderPopUP').style.top = '200px';
            document.getElementById('ReminderPopUP').style.visibility = 'visible';
            document.getElementById('frmReminder').src = "Bill_Sys_Reminder_Pop_UP_For_Case.aspx";
            return false;
        }
        function CloseCheckoutReminderPopup() {
            document.getElementById('ReminderPopUP').style.visibility = 'hidden';
            document.getElementById('frmReminder').src = 'Bill_Sys_Reminder_Pop_UP_For_Case.aspx';

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

        function SelectAllAtt(ival) {
            var f = document.getElementById("<%= grdAttorney.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }



                }


            }
        }

        function AttorneyAdd() {
            if (document.getElementById('<%=txtAttorneyFirstName.ClientID%>').value == '') {
                alert('Please Add Attorney First Name');
                return false;
            }
            if (document.getElementById('<%=txtAttorneyLastName.ClientID%>').value == '') {
                alert('Please Add Attorney Last Name');
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

        function CaseValidation() {
            if ($('.validationDDl :selected').text() == "---Select---") {
                alert('Select Case type');
                return false;
            }
        }
    </script>

    <script type="text/javascript" src="validation.js"></script>

     <script language="javascript" type="text/javascript">

         jQuery(function ($) {

             $("#ctl00_ContentPlaceHolder1_tabcontainerPatientEntry_tabpnlPersonalInformation_txtCellNo").mask("999-999-9999");
         });

    </script>
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
                    <%-- <table style="width:100%" >
                           <tr>
                            <td>
                             <asp:Repeater ID="rptPatientDeskList" runat="server" OnItemCommand="rptPatientDeskList_ItemCommand">
                            <HeaderTemplate>
                                <table align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                        border: #8babe4 1px solid #B5DF82;" >
                                    <tr>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" >
                                          <b>  Case#</b>
                                        </td>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                           <b> Chart No</b>
                                        </td>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                           <b>Patient Name</b> 
                                        </td>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                            <b>Insurance Name</b>
                                        </td>
                                        <td bgcolor="#B5DF82"  class="lbl" style="font-weight: bold;">
                                           <b> Accident Date</b>
                                        </td>
                                        <td bgcolor="#B5DF82"  style="height:50%">
                                           
                                        </td>
                                        
                                    </tr>
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                        <tr>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;"> 
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_INSURANCE_NAME")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl"style="border: 1px solid #B5DF82;" >
                                                <%# DataBinder.Eval(Container,"DataItem.DT_ACCIDENT","{0:MM/dd/yyyy}")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;">
                                               <asp:LinkButton ID="lnkPatientInfoPDF" ToolTip="Summary Sheet" runat="server" CommandName="genpdf"><img src="Images/actionEdit.gif" style="border: none;"/></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate> 
                              <FooterTemplate></table></FooterTemplate>
                               </asp:Repeater>
                                </td>
                           </tr>
                        </table>--%>
                           
                           
                        </td>
                    </tr>
             
                <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table style="width: 100%; height: 100%">
                          <%--      <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False" OnItemCommand="grdPatientDeskList_ItemCommand" Visible="false">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No." HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="openTypePage('a')">
                                                            <img src="Images/actionEdit.gif" alt="" style="border: none;" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPatientInfoPDF1" ToolTip="Summary Sheet" runat="server" CommandName="genpdf"><img src="Images/actionEdit.gif" style="border: none;"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>--%>
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
                                                <td colspan="4">
                                               <asp:Label ID="lbl" runat="server" Text="Walk In:" CssClass="lbl" Font-Bold="true" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblwalkin" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <div id="divAssociateCaseID" runat="server" style="float: left; text-align: left;">
                                                    </div>
                                                    
                                                </td>
                                               
                                                 
                                                <td align="right" width="65%">
                                                <asp:ImageButton ID="ImageButtontransport" runat="server" ImageUrl="~/Images/ambulance.jpg"
                                                    OnClientClick="showTransportFrame(); return false;"  Height="40px" Width="56px" />
                                                  <asp:LinkButton ID="lnkPatientInfo" runat="server" CssClass="lbl" OnClientClick="OpenPatientInfoPdf(); return false;">Print to PDF</asp:LinkButton>
                                                   &nbsp &nbsp <asp:LinkButton ID="lnkNF2Envelope" runat="server" CssClass="lbl" OnClick="lnkNF2Envelope_Click">NF2 Envelope</asp:LinkButton>
                                                    <asp:CheckBox ID="chkStatusProc" runat="server" />
                                                    <asp:TextBox Width="70px" ID="txtNF2Date" runat="server" onkeypress="return clickButton1(event,'/')"
                                                        MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnNF2Date" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    &nbsp;
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtNF2Date"
                                                        PopupButtonID="imgbtnNF2Date" Enabled="True" />
                                                    <asp:LinkButton ID="lnkGenerateNF2" runat="server" CssClass="lbl" OnClick="lnkGenerateNF2_Click">NF2 </asp:LinkButton>
                                                    &nbsp; <a id="hlnkShowMemo" href="#" runat="server" title="Add Memo"  class="lbl">Memo</a>
                                                    &nbsp; <a id="hlnkAssignSupplies" href="#" runat="server" title="Assign Supplies"
                                                        visible="false" class="lbl">Supplies</a> <a id="hlnkShowNotes" href="#" runat="server"
                                                            title="Add Note" class="lbl">Add Note</a>
                                                            &nbsp; <a id="acase" href="#" runat="server"
                                                                title="Add Reminder" class="lbl">Add Case Reminder</a>
                                                    <asp:LinkButton ID="btnDosespot" runat="server" Text="e-Prescribe" OnClick="btnDosespot_Click" OnClientClick="aspnetForm.target ='_blank';"></asp:LinkButton>
                                                    <!--<img src="Images/actionEdit.gif" style="border-style: none;" /> -->
                                                    <ajaxcontrol:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowNotes"
                                                        PopupControlID="pnlShowNotes" Position="Center" OffsetX="-600" OffsetY="-10" />
                                                        <ajaxcontrol:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="acase"
                                                        PopupControlID="pnlShowReminder" Position="Center" OffsetX="-1100" OffsetY="-100" />
                                                    <ajaxcontrol:PopupControlExtender ID="PopupCEMemo" runat="server" TargetControlID="hlnkShowMemo"
                                                        PopupControlID="pnlMemo" Position="Center" OffsetX="-600" OffsetY="-10" />
                                                    <ajaxcontrol:PopupControlExtender ID="PopupCEAssignSupplies" runat="server" TargetControlID="hlnkAssignSupplies"
                                                        PopupControlID="pnlAssignSupplies" Position="Center" OffsetX="-600" OffsetY="-10" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 15px; text-align: left;" >
                                                    <%-- <asp:LinkButton ID="hlnkAssociate" runat="server" visible="false" OnClick="hlnkAssociate_Click">Associate Diagnosis Codes</asp:LinkButton> |        
                       <asp:LinkButton ID="hlnkPatientDesk" runat="server"   OnClick="hlnkPatientDesk_Click" >Patient Desk</asp:LinkButton>--%>
                                                    <ajaxcontrol:TabContainer ID="tabcontainerPatientEntry" runat="Server" ActiveTabIndex="0"
                                                        >
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabPnlViewAll" TabIndex="6" Height="800px">
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
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Personal Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td  class="td-widget-lf-holder-ch">
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
                                                                                                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%></td>
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
                                                                                                                   
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Cell Phone #
												                                                                    <asp:CheckBox ID="chkViewWrongPhone" Visible="False" Enabled="False" runat="server" Text="Wrong Phone" TextAlign="Left" />
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                  <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CELLNO")%> 
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
                                                                                                                    Patient  Relation
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblPatientRelation" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Patient Status
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblPatientStatus" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
										                                                <tr>
										    	                                              <td class="td-CaseDetails-lf-desc-ch">
											  	                                            <asp:Label ID="lblLocation" Text="Location" runat="server" class="lbl" Style="font-weight: bold;"></asp:Label>
											                                              </td>
											                                              <td class="td-CaseDetails-lf-data-ch">
												                                            <asp:Label runat="server" ID="lblVLocation1" class="lbl" ></asp:Label>
											                                              </td>
											                                              <td class="td-CaseDetails-lf-desc-ch"><asp:CheckBox ID="chkPatientTransport" Visible="False" Enabled="False" 														
											                                              runat="server" Text="Transport" TextAlign="Left"></asp:CheckBox></td>
											                                              <td class="td-CaseDetails-lf-data-ch">&nbsp;</td>
											                                              </tr>
											                                               <tr>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                           <%-- Copy From--%>
                                                                                                                           <asp:Label ID="lblcopyfrm" runat="server" Text ="Copy From"  Font-Names ="Verdana" Font-Bold ="true" Font-Size ="11px" ></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch" colspan="3">
                                                                                                                            <asp:Label runat="server" ID="lblcopyfrom" class="lbl"></asp:Label>
                                                                                                                        </td>
                                                                                                                       <%-- <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;</td>--%>
                                                                                                                    </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
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
                                                                                                                    &nbsp;
                                                                                                                 <a id="lnkViewInsuranceClaimNumber" style="text-decoration: underline; color:Blue;" runat="server" class="lbl"  
                                                                                                                 href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+claimno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%></a>
                                                                                                                   <%--<ajaxcontrol:PopupControlExtender ID="popExtViewInsuranceClaimNumber" runat="server"  TargetControlID="lnkViewInsuranceClaimNumber"
                                                                                                                   PopupControlID="pnlClaimNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID=""  />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Policy #
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewPolicyNumber" style="text-decoration: underline; color: Blue;" runat="server" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+policyno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%> </a>
                                                                                                                    <%--<ajaxcontrol:PopupControlExtender ID="popExtViewPolicyNumber" runat="server" TargetControlID="lnkViewPolicyNumber" 
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
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
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
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkDateOfAccList" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+dtaccident+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%></a>
                                                                                                                    <%--<ajaxcontrol:PopupControlExtender ID="popExtDateOfAccList" runat="server" TargetControlID="lnkDateOfAccList" 
                                                                                                                    PopupControlID="pnlDateOfAccList" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Plate Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewAccidentPlatenumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+plateno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' ><%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%></a>
                                                                                                                    <%--<ajaxcontrol:PopupControlExtender ID="popViewAccidentPlatenumber" runat="server" TargetControlID="lnkViewAccidentPlatenumber" 
                                                                                                                    PopupControlID="pnlPlateNo" OffsetX="-100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Report Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                <a id="lnkViewAccidentReportNumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+accidentreportno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%></a>
                                                                                                                <%--<ajaxcontrol:PopupControlExtender ID="popViewAccidentReportNumber" runat="server" TargetControlID="lnkViewAccidentReportNumber" 
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
                                                                                                                    Specialty
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblAccidentSpecialty" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Patient Type
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblPatientType" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
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
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
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
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;" id="tblF">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 500px;">
                                                                                            &nbsp;<b class="txt3">Adjuster Information</b>
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
                                                                                                                    <a id="lnkViewAdjusterName" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+adjustername+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' >
                                                                                                                    <asp:Label runat="server" ID="lblViewAdjusterName" class="lbl"></asp:Label>
                                                                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>--%></a>
                                                                                                                    <%--<ajaxcontrol:PopupControlExtender ID="popViewAdjusterName" runat="server" TargetControlID="lnkViewAdjusterName" 
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
                                                        </ajaxcontrol:TabPanel>
                                                        
                                        </table>
                                        </DIV> </ContentTemplate> </ajaxcontrol:TabPanel>
                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlPersonalInformation" TabIndex="0"
                                            Height="300px">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Personal Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch">
                                                                                First name
                                                                            </td>
                                                                          
                                                                            <td colspan="4" rowspan="2">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtPatientFName" runat="server"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel">
                                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                                Middle</div>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtMI" runat="server"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="5%" class="tablecellLabel">
                                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                                Last name
                                                                                            </div>
                                                                                        </td>
                                                                                        <td width="32%">
                                                                                            <asp:TextBox ID="txtPatientLName" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox Width="69%" ID="txtDateOfBirth" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                    MaxLength="10"></asp:TextBox>
                                                                                                <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                                <ajaxcontrol:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"
                                                                                                    PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                                SSN #
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtSocialSecurityNumber" runat="server"></asp:TextBox></span></td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                                Gender</div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlSex" runat="server" Width="153px">
                                                                                                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                                                                                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Date of birth</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    <%--Marrital Status--%>
                                                                                </div>
                                                                            </td>
                                                                            
                                                                            <td colspan="4">
                                                                                <asp:RadioButtonList id="rdlPatientMarritalStatus" runat="server" RepeatDirection="Horizontal" Visible="false">
                                                                                <asp:ListItem Value="1" Text="Single" ></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Married"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Other"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Address
                                                                                </div>
                                                                            </td>
                                                                            
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="90%" ID="txtPatientAddress" runat="server"></asp:TextBox>
                                                                                <span class="tablecellControl">
                                                                                    <asp:TextBox Visible="False" ID="txtPatientStreet" runat="server"></asp:TextBox>
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    City</div>
                                                                            </td>
                                                                           
                                                                            <td colspan="4" rowspan="3">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
                                                                                                    <extddl:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                                                                                        OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Work</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Extn.</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtExtension" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                             <td>
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtCellNo" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td>
                                                                                             <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Email
                                                                                             </div>
                                                                                                <asp:CheckBox ID="chkWrongPhone" Visible="false" runat="server" Text="Wrong Phone"
                                                                                                    TextAlign="Left" /></td>
                                                                                           
                                                                                            <td >
                                                                                                <asp:TextBox ID="txtPatientEmail" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Home phone</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                               <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Cell Phone #</div></td>
                                                                        </tr>

                                                                         <tr runat="server" id="trEmp1">
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Employer Name</div>
                                                                            </td>
                                                                            
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="2">
                                                                             <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoEmployer" EnableCaching="true"
                                                                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtEmployerCompany" 
                                                                                    ServiceMethod="GetEmployer" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GeEmployerValue">
                                                                                </ajaxcontrol:AutoCompleteExtender>
                                                                                 <asp:TextBox ID="txtEmployerCompany" runat="Server" autocomplete="off" Width="75%" OnTextChanged="txtEmployerCompany_TextChanged" AutoPostBack="true"/>
                                                                                <extddl:ExtendedDropDownList ID="extddlEmployerCompany" Width="96%" runat="server" Visible="false"
                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_EMPLOYER_COMPANY"
                                                                                    Flag_Key_Value="EMPLOYER_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                                   
                                                                                    OldText="" StausText="False" />
                                                                            </td>
                                                                            </tr>


                                                                              <tr  runat="server" id="trEmp2">
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Employer Address</div>
                                                                            </td>
                                                                            
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="2">
                                                               <asp:ListBox Width="76%" ID="lstEmployerAddress" AutoPostBack="true" runat="server" OnSelectedIndexChanged="lstEmployerAddress_SelectedIndexChanged" Height="40px"></asp:ListBox>
                                                                            </td>
                                                                            </tr>


                                                                              <tr  runat="server" id="trEmp3">
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Address</div>
                                                                            </td>
                                                                            
                                                                            <td class="tablecellSpace">
                                                                           
                                                                            </td>
                                                                            <td colspan="2">
                                                                             <asp:TextBox Width="95%" ID="txtEmployercmpAddress" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                            </tr>

                                                                               <tr>
                                                                            <td colspan="5">
                                                                            <table>
                                                                                <tr>
                                                                            <td class="tablecellLabel">
                                                                               
                                                                            </td>
                                                                           <td colspan="4">
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr  id="trEmp4" runat="server">
                                                                        <td colspan="5">
                                                                            <table>
                                                                          
                                                                            <tr>
                                                                                  <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Street
                                                                                    </div>
                                                                            </td>
                                                                            <td>
                                                                                 <asp:TextBox ID="txtEmployercmpStreet" runat="server" CssClass="text-box" ReadOnly="True"
                                                    Width="95%"></asp:TextBox>
                                                                           </td>
                                                                                      <td  class="tablecellLabel">
                                                                                          <div class="td-PatientInfo-lf-desc-ch">
                                                                                              City</div>
                                                                                      </td>
                                                                                      <td>
                                                                                         <asp:TextBox ID="txtEmployercmpCity" runat="server" CssClass="text-box" ReadOnly="True"
                                                    Width="95%"></asp:TextBox>
                                                                                          </td>
                                                                                      <td class="tablecellLabel">
                                                                                          <div class="td-PatientInfo-lf-desc-ch">
                                                                                              State</div>
                                                                                      </td>
                                                                                      <td >
                                                                                      <cc1:ExtendedDropDownList ID="extddlEmployercmpState" runat="server" Selected_Text="--- Select ---"
                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                    Width="90%" Enabled="false"></cc1:ExtendedDropDownList>
                                                                                       </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                     <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Zip
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                       <asp:TextBox Width="95%" ID="txtEmployercmpZip" runat="server" CssClass="text-box-" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                                
                                                                                <td>
                                                                                     <div class="td-PatientInfo-lf-desc-ch">
                                                                                    
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                  
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        </td>
                                                                          
                                                                                       
                                                                        </tr>  
                                                                            </table>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Attorney</div>
                                                                            </td>
                                                                            
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="2">
                                                                             <ajaxcontrol:AutoCompleteExtender runat="server" ID="Ajautoattorney" EnableCaching="true"
                                                                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtAttorneyCompany" 
                                                                                    ServiceMethod="GetAttorney" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetAttoreyValue">
                                                                                </ajaxcontrol:AutoCompleteExtender>
                                                                                 <asp:TextBox ID="txtAttorneyCompany" runat="Server" autocomplete="off" Width="75%" OnTextChanged="txtAttorneyCompany_TextChanged" AutoPostBack="true"/>
                                                                                <extddl:ExtendedDropDownList ID="extddlAttorney" Width="96%" runat="server" Visible="false"
                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_ATTORNEY"
                                                                                    Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                                    OnextendDropDown_SelectedIndexChanged="extddlAttorney_selectedIndex"
                                                                                    OldText="" StausText="False" />
                                                                                    
                                                                            </td>
                                                                            <%--<td colspan="3">
                                                                                <extddl:ExtendedDropDownList ID="extddlAttorney" Width="95%" runat="server" Connection_Key="Connection_String"
                                                                                    Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                                                                    OldText="" StausText="False" OnextendDropDown_SelectedIndexChanged="extddlAttorney_selectedIndex" AutoPost_back="true"/>
                                                                            </td>--%>
                                                                            <td>
                                                                            <a id="hlnkattedit" href="#" runat="server" title="Attorney Info" class="td-PatientInfo-lf-desc-ch" onclick="showAttorneyrFrame()">
                                                                                    Edit</a>
                                                                                    
                                                                                <a id="hlnkShowAtornyInfo" href="#" runat="server" title="Attorney Info" class="td-PatientInfo-lf-desc-ch">
                                                                                    Info</a>
                                                                                <ajaxcontrol:PopupControlExtender ID="PopupAtornyInfo" runat="server" TargetControlID="hlnkShowAtornyInfo"
                                                                                    PopupControlID="pnlShowAtornyInfo" OffsetX="-600" OffsetY="-200" DynamicServicePath=""
                                                                                    Enabled="True" ExtenderControlID="" />
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        
                                                                        <tr>
                                                                            <td colspan="5">
                                                                            <table>
                                                                                <tr>
                                                                            <td class="tablecellLabel">
                                                                               
                                                                            </td>
                                                                           <td colspan="4">
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td colspan="5">
                                                                            <table>
                                                                            <tr>
                                                                                <td class="tablecellLabel"">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Address
                                                                                    </div>
                                                                                    </td>
                                                                         
                                                                                    <td colspan="5" >
                                                                                        <asp:TextBox ID="txtattorneyaddress" Width="100%" runat="server" CssClass="text-box"
                                                                                    ReadOnly="True"></asp:TextBox>
                                                                                    </td>
                                                                            </tr>
                                                                            <tr>
                                                                                  <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    City
                                                                                    </div>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtattorneycity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                                           </td>
                                                                                      <td  class="tablecellLabel">
                                                                                          <div class="td-PatientInfo-lf-desc-ch">
                                                                                              State</div>
                                                                                      </td>
                                                                                      <td>
                                                                                          <asp:TextBox ID="txtattorneState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                                                          </td>
                                                                                      <td class="tablecellLabel">
                                                                                          <div class="td-PatientInfo-lf-desc-ch">
                                                                                              Zip</div>
                                                                                      </td>
                                                                                      <td >
                                                                                       <asp:TextBox ID="txtattorneyzip" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                     <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Phone No
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtattorneyphone" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                                
                                                                                <td>
                                                                                     <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Fax
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtattorneyfax" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        </td>
                                                                          
                                                                                       
                                                                        </tr>  
                                                                            </table>
                                                                            </td>
                                                                        </tr>
                                                                     
                                                                        
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Case Type</div>
                                                                            </td>
                                                                           
                                                                            <td colspan="4" rowspan="2">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="200px" Connection_Key="Connection_String" CssClass="validationDDl" 
                                                                                                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                                                    OldText="" StausText="False" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                                Case Status</div>
                                                                                        </td>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                                                    Flag_ID="txtCompanyID.Text.ToString();" OldText="" StausText="False" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <asp:CheckBox ID="chkTransportation" runat="server" Text="Transport" TextAlign="Left"
                                                                                                Visible="false"></asp:CheckBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                       <td></td>
                                                                       </tr>
                                                                       
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    &nbsp;<asp:Label ID="lblLocationddl" Text="Location" runat="server" class="td-PatientInfo-lf-desc-ch" ></asp:Label></div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Selected_Text="--- Select ---"
                                                                                                    OldText="" StausText="False" />
                                                                                            </span>
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
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Patient Status</div>
                                                                            </td>
                                                                           
                                                                            <td colspan="4" rowspan="2">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:RadioButtonList id="rdlPatient_Status" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1" Text="Single" ></asp:ListItem>
                                                                                                <asp:ListItem Value="2" Text="Married"></asp:ListItem>
                                                                                                <asp:ListItem Value="3" Text="Other"></asp:ListItem>
                                                                                                <asp:ListItem Value="4" Text="Employed"></asp:ListItem>
                                                                                                <asp:ListItem Value="5" Text="Full-Time Student"></asp:ListItem>
                                                                                                <asp:ListItem Value="6" Text="Part-Time Student"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                                Patient relationship</div>
                                                                                        </td>
                                                                                        <td width="30%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:RadioButtonList id="rdlPatient_relation" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1" Text="Self" ></asp:ListItem>
                                                                                                <asp:ListItem Value="2" Text="Spous"></asp:ListItem>
                                                                                                <asp:ListItem Value="3" Text="Child"></asp:ListItem>
                                                                                                <asp:ListItem Value="4" Text="Other"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Transport" TextAlign="Left"
                                                                                                Visible="false"></asp:CheckBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                            </td>
                                                                            <td class="tablecellSpace" rowspan="1">
                                                                            </td>
                                                                            <td colspan="4" rowspan="1">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">&nbsp;</span></td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <div class="lbl">
                                                                                                &nbsp;&nbsp;</div>
                                                                                        </td>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">&nbsp;&nbsp; </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox Width="70%" ID="txtDateofAccident" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                        MaxLength="10" Visible="False"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif"
                                                                                                        Visible="False" />
                                                                                                    &nbsp;
                                                                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateofAccident"
                                                                                                        PopupButtonID="imgbtnDateofAccident" Enabled="True" />
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPlatenumber" Visible="False" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPolicyReport" Visible="False" runat="server" CssClass="text-box"></asp:TextBox>&nbsp;
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server" id="tr1">
                                                                            <td colspan="2">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Employer Information
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                          <tr runat="server" id="tr2">
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Name
                                                                                </div>
                                                                            </td>
                                                                           
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtEmployerName" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                       <tr runat="server" id="tr3">
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Address
                                                                                </div>
                                                                            </td>
                                                                            
                                                                            <td colspan="4">
                                                                                <asp:TextBox ID="txtEmployerAddress" runat="server" Width="99%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                         <tr runat="server" id="tr4">
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    City</div>
                                                                            </td>
                                                                           
                                                                            <td colspan="4" rowspan="2">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerCity" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerState" runat="server" Visible="False"></asp:TextBox>
                                                                                                    <extddl:ExtendedDropDownList ID="extddlEmployerState" runat="server" Selected_Text="--- Select ---"
                                                                                                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                                                                        Width="90%" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel" width="13%">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerZip" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerPhone" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    &nbsp;<asp:Label ID="lblDateofFirstTreatment" runat="server" Text="Date of First Treatment"
                                                                                                        class="td-PatientInfo-lf-desc-ch"></asp:Label></div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">&nbsp;<asp:TextBox Width="45%" ID="txtDateofFirstTreatment"
                                                                                                    runat="server" onkeypress="return clickButton1(event,'/')" MaxLength="10"></asp:TextBox><asp:ImageButton
                                                                                                        ID="imgbtnDateofFirstTreatment" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxcontrol:CalendarExtender
                                                                                                            ID="CalendarExtender1" runat="server" TargetControlID="txtDateofFirstTreatment"
                                                                                                            PopupButtonID="imgbtnDateofFirstTreatment" Enabled="True" />
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel" style="width: 13%">
                                                                                                <div class="lbl">
                                                                                                    &nbsp;<asp:Label ID="lblChart" runat="server" Text="Chart No." class="td-PatientInfo-lf-desc-ch"></asp:Label></div>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtChartNo" runat="server"></asp:TextBox></span></td>
                                                                                            <td class="tablecellLabel">
                                                                                                &nbsp;</td>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server" id="tr5">
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Phone</div>
                                                                            </td>
                                                                        </tr>



                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>
                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlInsuranceInformation" TabIndex="2" Height="400px"> 
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Insurance Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" >
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Policy Holder
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPolicyHolder" runat="server" CssClass="text-box" Width="50%"></asp:TextBox>
                                                                                &nbsp; <a id="lnkSearchInsuranceCompany" href="#" runat="server" title="Search Insurance Company"
                                                                                    class="lbl">Search Insurance Company</a>
                                                                                    <%--Nirmalkumar--%>
                                                                                    <a id="lnkSearchSecondaryInsuranceCompany" href="javascript:void(0);"  onclick="showSecondaryInsurance()" 
                                                                                    class="lbl">Add Secondary Insurance</a>
                                                                                    <%--END--%>
                                                                                <ajaxcontrol:PopupControlExtender ID="peSearchInsuranceCompany" runat="server" TargetControlID="lnkSearchInsuranceCompany"
                                                                                    PopupControlID="pnlSearchInsuranceCompany" Position="Center" OffsetX="-220" OffsetY="-10" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Policy Holder Address
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPolicyHolderAddress" runat="server" CssClass="text-box" Width="100%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    City</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td >
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtPolicyCity" runat="server" CssClass="text-box" ></asp:TextBox></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtPolicyState" runat="server" CssClass="text-box" visible="False" ></asp:TextBox>
                                                                                                 <extddl:ExtendedDropDownList ID="extdlPolicyState" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                                                                                        OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                            </td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtPolicyZip" runat="server" CssClass="text-box"></asp:TextBox></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Phone Number</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td >
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtPolicyPhone" runat="server" CssClass="text-box" ></asp:TextBox></td>
                                                                                            <td width="25%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Relation To Patient</div>
                                                                                            </td>
                                                                                            <td width="40%">
                                                                                                <asp:RadioButtonList id="rdlPolicyHolderRelation" runat="server" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Value="1" Text="Self" ></asp:ListItem>
                                                                                                <asp:ListItem Value="2" Text="Spous"></asp:ListItem>
                                                                                                <asp:ListItem Vlaue="3" Text="Child"></asp:ListItem>
                                                                                                <asp:ListItem Vlaue="4" Text="Other"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            
                                                                                                <%--<asp:TextBox ID="txtPolicyState" runat="server" CssClass="text-box" ></asp:TextBox></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtPolicyZip" runat="server" CssClass="text-box"></asp:TextBox></td>--%>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Name
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                             <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany" 
                                                                                    ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                                                                                </ajaxcontrol:AutoCompleteExtender>
                                                                                 <asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="75%" OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>
                                                                                <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server" Visible="false"
                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                                    Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                                    OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged"
                                                                                    OldText="" StausText="False" />
                                                                                    <a href="#" id="A1" onclick="showAdjusterPanelAddress()" style="text-decoration: none;">
                                                                                    <img id="img1" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company Address" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Ins. Address
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                                &nbsp;</td>
                                                                            <td colspan="4">
                                                                            <asp:ListBox Width="100%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                                                    OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Address</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                                                    ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    City</div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtInsuranceZip" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsPhone" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Fax</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsFax" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Contact Person</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtInsContactPerson" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box"></asp:TextBox></td>
                                                                                            <td class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    <asp:Label ID="lblPolicyNumber" class="td-PatientInfo-lf-desc-ch" runat="server"> Policy #</asp:Label></div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtWCBNumber" runat="server" Visible="False"></asp:TextBox>
                                                                                                <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="td-PatientInfo-lf-desc-ch">
                                                                                                 WCB #
                                                                                               </td>
                                                                                            <td>
                                                                                             <asp:TextBox ID="txtWCBNo" runat="server"></asp:TextBox>
                                                                                                <asp:TextBox Visible="False" Width="99%" ID="txtInsuranceStreet" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                                    
                                                                                      </tr>
                                                                                      
                                                                                    </table>
                                                                                    
                                                                                   <%-- <table>
                                                                                    <tr>
                                                                                       <td class="td-PatientInfo-lf-desc-ch">
                                                                                               Carrier Code #
                                                                                               </td>
                                                                                               <td>
                                                                                               <asp:TextBox ID="txtcarriercode" runat="server" ></asp:TextBox>
                                                                                               
                                                                                               </td>
                                                                                      </tr>
                                                                                    </table>--%>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Phone<br />
                                                                                    Claim/File #
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <%--Nirmalkumar--%>          
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Secondary Insurance Information
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlInsuranceType" runat="server" OnSelectedIndexChanged="ddlInsuranceType_SelectedIndexChanged" AutoPostBack="true"> 
                                                                                <asp:ListItem >Select</asp:ListItem>
                                                                                <asp:ListItem >Secondary</asp:ListItem>
                                                                                <asp:ListItem >Major Medical</asp:ListItem>
                                                                                <asp:ListItem >Private</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Secondary Ins.Name
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                            <asp:TextBox ID="txtSecInsName" runat="Server" autocomplete="off" Width="75%" ><%--</asp:TextBox>OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>--%></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Ins. Address
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                                &nbsp;</td>
                                                                            <td colspan="4">
                                                                            <asp:TextBox ID="txtSecInsAddress" runat="Server" autocomplete="off" Width="75%" ><%--</asp:TextBox>OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>--%></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Address</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtSecInsAdd" runat="server" CssClass="text-box"
                                                                                    ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    City</div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtInsZip" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtSecInsPhone" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Fax</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtSecInsFax" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Contact Person</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtInsConatactPerson" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtClaim" runat="server" CssClass="text-box"></asp:TextBox>
                                                                            </td>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    <asp:Label ID="lblPolicy" class="td-PatientInfo-lf-desc-ch" runat="server"> Policy #</asp:Label></div>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPolicy" runat="server" CssClass="text-box"></asp:TextBox>
                                                                            </td>
                                                                            </tr> 
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Phone<br />
                                                                                    <br />
                                                                                    Claim/File #
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        
                                                                        <%--END--%>
                                                                        
                                                                                                                                      
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Adjuster Information 
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                 <a href="javascript:void(0);" onclick="showAdjusterFrame('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>')">
                                                                                 <img id="imgShowAdjuster" src="Images/actionEdit.gif" style="border-style: none;" title="Add Adjuster" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                            <td class="ContentLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Name
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <extddl:ExtendedDropDownList ID="extddlAdjuster" Width="100%" runat="server" Connection_Key="Connection_String"
                                                                                    Selected_Text="--- Select ---" Flag_Key_Value="GET_ADJUSTER_LIST" Procedure_Name="SP_MST_ADJUSTER"
                                                                                    AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlAdjuster_extendDropDown_SelectedIndexChanged"
                                                                                    OldText="" StausText="False" />
                                                                                    
                                                                            </td>
                                                                             <td>
                                                                            &nbsp;
                                                                            </td>
                                                                            <td valign="top" class="class="ContentLabel">
                                                                            <a href="javascript:void(0);" onclick="showUpdateAdjusterFrame('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>','<%# DataBinder.Eval(Container,"DataItem.txtAdjuserID")%>')">
                                                                            Update Adjuster </a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Address</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox ID="txtAddress" runat="server" Width="99%" ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    City</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" valign="top" >
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtCity" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtAdjusterState" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtZip" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                        </tr>
                                                                                     </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Phone</div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtAdjusterPhone" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Extension</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtAdjusterExtension" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                                    Fax</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtfax" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3" style="height: 37px">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox Width="98%" ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></span>
                                                                                                <div class="lbl">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td class="tablecellLabel" style="height: 37px">
                                                                                                &nbsp;</td>
                                                                                            <td style="height: 37px">
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Email</div>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        
                                                                
                                                                         <tr>
                                                                                       <td height="26" class="tablecellLabel" align="left">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                           
                                                                                   <asp:Label ID="lblcarriercode" runat="server" Text="Carrier Code #" Font-Bold="true" Font-Size="8" Font-Names="Verdana" ></asp:Label>
                                                                       
                                                                                    
                                                                                     </div>
                                                                                     
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                                             <td>
                                                                                               <asp:TextBox ID="txtcarriercode" runat="server" ></asp:TextBox>
                                                                                               
                                                                                               </td>
                                                                                      </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel">
                                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                                    Associate cases
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <span class="tablecellControl">
                                                                                    <asp:TextBox ID="txtAssociateCases" runat="server"></asp:TextBox>
                                                                                    <asp:Button ID="btnAssociate" runat="server" OnClick="btnAssociate_Click" Text="Associate Cases"
                                                                                        Width="105px" CssClass="Buttons" />
                                                                                        </span>
                                                                                        <asp:Button ID="btnDAssociate" runat="server" OnClick="btnDAssociate_Click" Text="DeAssociate Cases"
                                                                                        Width="115px" CssClass="Buttons" />
                                                                                      <asp:CheckBox ID = "btassociate" runat ="server" Text="Do not Update ins data " />
                                                                                      </td>
                                                                        </tr>
                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>
                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlAccidentInformation" TabIndex="1">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Accident Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 280px;">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Accident Date
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox Width="70%" ID="txtATAccidentDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                    MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                                                <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <ajaxcontrol:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="txtATAccidentDate"
                                                                                    PopupButtonID="imgbtnATAccidentDate" Enabled="True" />
                                                                            </td>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Plate Number
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATPlateNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Report Number
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATReportNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Address</td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                City
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                State</td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <extddl:ExtendedDropDownList ID="extddlATAccidentState" runat="server" Width="90%"
                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                                                    Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Hospital name
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Hospital Address</td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATHospitalAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Date of admission
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox Width="70%" ID="txtATAdmissionDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                    MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                                                <asp:ImageButton ID="imgbtnATAdmissionDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <ajaxcontrol:CalendarExtender ID="calATAdmissionDate" runat="server" TargetControlID="txtATAdmissionDate"
                                                                                    PopupButtonID="imgbtnATAdmissionDate" Enabled="True" />
                                                                            </td>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Specialty
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtAccidentSpecialty" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Additional Patients
                                                                            </td>
                                                                           <td style="width: 35%" class="ContentLabel">
                                                                                  <asp:TextBox ID="txtATAdditionalPatients" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                               
                                                                            
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Describe injury
                                                                            </td>
                                                                            <td style="width: 35%" colspan="3">
                                                                               
                                                                                            <asp:TextBox ID="txtATDescribeInjury" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Patient Type
                                                                            </td>
                                                                            <td colspan = "3" width="35%">
                                                                           
                                                                                        <asp:RadioButtonList ID="rdolstPatientType" runat="server" RepeatDirection="Horizontal" class = "lbl" >
                                                                                            <asp:ListItem Value = "0">Bicyclist</asp:ListItem>
                                                                                            <asp:ListItem Value = "1">Driver</asp:ListItem>
                                                                                            <asp:ListItem Value = "2">Passenger</asp:ListItem>
                                                                                            <asp:ListItem Value = "3">Pedestrian</asp:ListItem>
                                                                                            <asp:ListItem Value = "4">OD</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                        <asp:TextBox ID="txtPatientType" runat="server" Visible = "false" Width="2%"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                           
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>
                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlEmployerInformation" TabIndex="6"
                                            Visible="false">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Employer Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 280px;">
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>
                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlAdjusterInformation" TabIndex="4"
                                            Visible="false">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Adjuster Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 280px;">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>
                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlAcciInfo" TabIndex="5" Visible="false">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Accident
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    &nbsp;</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <span class="tablecellControl">
                                                                                    <extddl:ExtendedDropDownList ID="extddlProvider" Width="200px" runat="server" Connection_Key="Connection_String"
                                                                                        Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---"
                                                                                        Visible="False" OldText="" StausText="False" />
                                                                                    &nbsp; &nbsp;
                                                                                    <asp:CheckBox ID="chkAssociateCode" runat="server" Text="Associate Diagnosis Code"
                                                                                        Visible="False" />&nbsp; </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    &nbsp;</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                &nbsp;</div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox Width="70%" ID="txtDateOfInjury" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                MaxLength="10" Visible="False"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtCarrierCaseNo" runat="server" Visible="False"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtJobTitle" runat="server" Visible="False"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                &nbsp;</div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtWorkActivites" runat="server" Visible="False"></asp:TextBox>
                                                                                                <asp:TextBox ID="txtPatientAge" runat="server" onkeypress="return clickButton1(event,'')"
                                                                                                    MaxLength="10" Visible="False"></asp:TextBox></span></td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="lbl">&nbsp;<asp:TextBox ID="txtAssociateDiagnosisCode" runat="server"
                                                                                                Visible="False" /></span></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div align="left" style="height: 280px;">
                                                                    <table width="100%" height="200" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Address</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtAccidentAddress" runat="server" CssClass="text-box"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="11" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    City</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" valign="top">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" height="26">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtAccidentCity" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                State</div>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtAccidentState" runat="server" CssClass="text-box" Visible="false"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="5%" class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                &nbsp;</div>
                                                                                        </td>
                                                                                        <td width="32%">
                                                                                            <span class="tablecellControl">&nbsp;</span></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="11" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Patients from the car</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" valign="top">
                                                                                <asp:TextBox Height="100px" Width="98%" ID="txtListOfPatient" runat="server" TextMode="MultiLine"
                                                                                    MaxLength="250"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>
                                                 <ajaxcontrol:TabPanel runat="server" ID="tabpnlAttorneyInformation" TabIndex="6">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Attorney Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 280px;">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                                Attorney
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <extddl:ExtendedDropDownList ID="extddlAttorneyAssign" Width="95%" runat="server" Connection_Key="Connection_String"
                                                                                    Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                                                                    OldText="" StausText="False"/>
                                                                            </td>
                                                                            <td style="width:35%;" class="class="ContentLabel" >
                                                               
                                                                                  <asp:LinkButton ID="lnkAddAttorney" runat="server" Text="Add Attorney" Style="text-align: right;
                                                                                        font-size: 12px; vertical-align: top;" OnClick="lnkAddAttorney_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp; 
                                                                                  <asp:LinkButton ID="lnkUpdateAtt" runat="server" Text="Update Attorney" Style="text-align: right;
                                                                                font-size: 12px; vertical-align: top;" OnClick="lnkUpdateAtt_Click"></asp:LinkButton>             
                                                                            </td>
                                                                            <td >
                                                                        
                                                                            </td>
                                                                                
                                                                        </tr>
                                                                             <tr style="height:10px;">
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%" colspan="4">
                                                                             
                                                                            </td>
                                                                         
                                                                                
                                                                        </tr>
                                                                         <tr style="height:10px;">
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                             
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                              <asp:CheckBox ID="chkAttorneyAssign" runat="server" Text="Allow to Access Documents" />
                                                                            </td>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                              
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                            </td>
                                                                                
                                                                        </tr>
                                                                          <tr style="height:10px;">
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%" colspan="4">
                                                                             
                                                                            </td>
                                                                          
                                                                                
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                             
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                              <asp:Button ID="btnAttorneyAssign"  runat="server" Text="Assign" OnClick="btnAttorneyAssign_Click"/>
                                                                            </td>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                              
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                            </td>
                                                                                
                                                                        </tr>
                                                                     
                                                                         <tr>
                                                                            <td class="td-PatientInfo-lf-desc-ch" style="width: 15%" colspan="4">
                                                                            
                                                                                <asp:UpdatePanel ID="UP_grdAttorney" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table width="100%">
                                                                                        <tr>
                                                                                         <td align="right">
                                                                                          <asp:Button ID="btndeleteAtt" runat="server" OnClick="btndeleteAtt_Click"  Text="Delete" />
                                                                                         </td>
                                                                                        </tr>
                                                                                            <tr>
                                                                                                <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                                                                                    <b class="txt3">Attorney List</b>
                                                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdAttorney"
                                                                                                        DisplayAfter="10" DynamicLayout="true">
                                                                                                        <progresstemplate>
                                                                                                 <div id="Div2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                                     runat="Server">
                                                                                                     <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                         Height="25px" Width="24px"></asp:Image>
                                                                                                     Loading...</div>
                                                                                             </progresstemplate>
                                                                                                    </asp:UpdateProgress>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                            <table style="vertical-align: middle; width: 100%;">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td style="vertical-align: middle; width: 30%" align="left">
                                                                                                            Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                                                CssClass="search-input">
                                                                                                            </gridsearch:XGridSearchTextBox>
                                                                                                          
                                                                                                        </td>
                                                                                                        <td style="width: 60%" align="right">
                                                                                                            Record Count:
                                                                                                            <%= this.grdAttorney.RecordCount%>
                                                                                                            | Page Count:
                                                                                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                                            </gridpagination:XGridPaginationDropDown>
                                                                                                           
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                                                                                                                 </tbody>
                                                                                            </table>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <xgrid:XGridViewControl ID="grdAttorney" runat="server" Width="1020px" CssClass="mGrid" DataKeyNames="SZ_CASE_ID,SZ_PATIENT_ID,SZ_ATTORNEY_ID,SZ_COMPANY_ID,ID,ATTORNEY_TYPE_ID" MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                                                                            HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                                                            
                                                                                                            ShowExcelTableBorder="true"
                                                                                                            AllowPaging="true" XGridKey="GetAttorneyAssignList" PageRowCount="5" PagerStyle-CssClass="pgr"
                                                                                                            AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                                                                            <Columns>
                                                                                                                <%--0--%>
                                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_ID)" headertext="Case ID"
                                                                                                                    DataField="SZ_CASE_ID" visible="false" />
                                                                                                                 <%--1--%>    
                                                                                                                 <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_NO)" headertext="Case #"
                                                                                                                DataField="SZ_CASE_NO" />
                                                                                                                <%--2--%>
                                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" headertext="Patient Name"
                                                                                                                    DataField="PATIENT_NAME" />
                                                                                                               <%--3--%>
                                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    SortExpression="MST_ATTORNEY.SZ_ATTORNEY_FIRST_NAME" 
                                                                                                                    headertext="Attorney Name" DataField="ATTORNEY_NAME" />
                                                                                                                <%--4--%>
                                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    headertext="Patient ID" DataField="SZ_PATIENT_ID" visible="false" />
                                                                                                                <%--5--%>
                                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    headertext="Attorney ID" DataField="SZ_ATTORNEY_ID" visible="false" />
                                                                                                                <%--6--%>
                                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    SortExpression="MST_ATTORNEY.SZ_ATTORNEY_ADDRESS"
                                                                                                                    headertext="Attorney Address" DataField="SZ_ATTORNEY_ADDRESS" />
                                                                                                                <%--7--%>   
                                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    SortExpression="MST_USERS.SZ_USER_NAME"
                                                                                                                    headertext="User Name" DataField="SZ_USER_NAME" />
                                                                                                                <%--8--%>
                                                                                                                  <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    headertext="Company ID" DataField="SZ_COMPANY_ID" visible="false" />
                                                                                                                <%--9--%>
                                                                                                                  <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    headertext="ID" DataField="ID" visible="false" />
                                                                                                                <%--10--%>
                                                                                                                  <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    headertext="ATTORNEY TYPE ID" DataField="ATTORNEY_TYPE_ID" visible="false" />
                                                                                                                <%--11--%>
                                                                                                                  <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                                    headertext="ATTORNEY TYPE NAME" DataField="ATTORNEY_TYPE_NAME" />
                                                                                                                  <%--12--%>   
                                                                                                                    <asp:TemplateField>
                                                                                                                    <headertemplate>
                                                                                                                        <center> <asp:CheckBox ID="chkSelectAllAtt" runat="server" onclick="javascript:SelectAllAtt(this.checked);"  ToolTip="Select All" /> </center> 
                                                                                                                    </headertemplate>
                                                                                                                    <itemtemplate >
                                                                                                                    <center> <asp:CheckBox ID="chkDeleteAtt" runat="server" /> </center> 
                                                                                                                    </itemtemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                   
                                                                                                                   
                                                                                                               </Columns>
                                                                                                        </xgrid:XGridViewControl>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="con" />
                                                                                    
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                           
                                                                                
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>

                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlRefferingInformation" TabIndex="7" Height="400px"> 
                                            <headertemplate>
                                                <div style="width: 120px;" class="lbl">
                                                    Reffering Office
                                                </div>
                                            </headertemplate>
                                            <contenttemplate>
                                                <div align="left" style="height: 280px;" >
                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                        <tr>
                                                            <td class="tablecellLabel" style="width:100px;">
                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                    Reffering Office
                                                                </div>
                                                            </td>
                                                            <%--<td class="tablecellSpace">
                                                            </td>--%>
                                                            <td>
                                                                <extddl:ExtendedDropDownList ID="extddlRefferingOffice" Width="50%" runat="server" Connection_Key="Connection_String"
                                                                Selected_Text="--- Select ---" Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE_REFF"
                                                                AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlRefferingOffice_extendDropDown_SelectedIndexChanged"
                                                                OldText="" StausText="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tablecellLabel" style="width:100px;">
                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                    Address
                                                                </div>
                                                            </td>
                                                            <%--<td class="tablecellSpace">
                                                            </td>--%>
                                                            <td colspan="5" >
                                                                <span class="tablecellControl">
                                                                    <asp:TextBox ID="txtOfficeAddress" runat="server" CssClass="text-box" Width="52.5%" ReadOnly="true"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td class="tablecellLabel" style="width:100px;">
                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                    City
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <span class="tablecellControl">
                                                                    <asp:TextBox ID="txtOfficeCity" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                            <td class="tablecellLabel" style="width:100px;">
                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                    State
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <span class="tablecellControl">
                                                                    <asp:TextBox ID="txtOfficeState" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                            <td class="tablecellLabel" style="width:100px;">
                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                    Zip
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <span class="tablecellControl">
                                                                    <asp:TextBox ID="txtOfficeZip" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tablecellLabel">
                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                    Reffering Doctor
                                                                </div>
                                                            </td>
                                                            <%--<td class="tablecellSpace">
                                                            </td>--%>
                                                            <td>
                                                                <extddl:ExtendedDropDownList ID="extddlRefferingDoctor" Width="50%" runat="server" Connection_Key="Connection_String"
                                                                Selected_Text="--- Select ---" Flag_Key_Value="Doctor_List" Procedure_Name="sp_get_billing_reff_doc"
                                                                AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlRefferingDoctor_extendDropDown_SelectedIndexChanged"
                                                                OldText="" StausText="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="tablecellLabel" style="width:100px;">
                                                                <div class="td-PatientInfo-lf-desc-ch">
                                                                    NPI
                                                                </div>
                                                            </td>
                                                            <%--<td class="tablecellSpace">
                                                            </td>--%>
                                                            <td colspan="5" >
                                                                <span class="tablecellControl">
                                                                    <asp:TextBox ID="txtDoctorNPI" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                </div>
                                            </contenttemplate>
                                        </ajaxcontrol:TabPanel>
                                      </ajaxcontrol:TabContainer> </ajaxcontrol:TabContainer>
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

                                                
                                                
                                                <asp:Button ID="btnPatientUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                                    OnClick="btnPatientUpdate_Click"  OnClientClick="return CaseValidation();" />
                                                <asp:Button ID="btnPatientClear" runat="server" Text="Clear" Width="80px" CssClass="Buttons" />
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" colspan="6">
                                        <asp:TextBox ID="txtCompanyIDForNotes" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCompanyID" runat="server" Style =" visibility:hidden;" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Style =" visibility:hidden;" ></asp:TextBox>
                                        <asp:TextBox ID="txtDosespotPatientId" runat="server" Width="10px" Style =" visibility:hidden;" ></asp:TextBox>
                                        <asp:TextBox ID="txtClinicId" runat="server" Width="10px" Style =" visibility:hidden;" ></asp:TextBox>
                                        <asp:TextBox ID="txtDosespotUserId" runat="server" Width="10px" Style =" visibility:hidden;" ></asp:TextBox>
                                        <%--<asp:TextBox ID="txtChartNo" runat="server" Width="10px" Visible="false"></asp:TextBox>--%>

                                        <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                  <asp:TextBox ID="txtAdjusterid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!--  <tr>
                                    <td style="width: 100%; height: 24px;" class="SectionDevider">
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        Associate Diagnosis Code
                                    </td>
                                </tr>  -->
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
                    <%--<tr>
                        <td style="width: 100%" class="SectionDevider">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 2px;" class="SectionDevider">
                        </td>
                    </tr>--%>
                    
                    <tr>
                        
                        <td style="width: 100%; height:160px" class="SectionDevider" valign="top">
                         <asp:Panel ID="Panel1" runat="server"  Width="100%">
	                        <table width="100%" cellpadding="0" cellspacing="0" style="width: 100%; height:25px; background-color:#5998C9; ;vertical-align:text-top;">
	                            <tr style="height:3px">
	                                <td align="center" style="width:90%;" valign="top">
	                                    <asp:LinkButton ID="LinkButton1" Font-Underline="false" ToolTip="Show Bills" runat="server" OnClick="LinkButton1_Click" BackColor="#5998C9" Font-Bold="false" Font-Size="Larger" ForeColor="white">
	                                        Bills &nbsp;&nbsp; <asp:Label ID="lblcpe" runat="server" style="color:White"></asp:Label>
	                                    </asp:LinkButton>
	                                    
	                                </td>
	                               
	                                
	                                <td align="right">
	                                    <asp:Image ID="Image1" runat="server" />&nbsp;&nbsp;&nbsp;
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
	                        <table id="tblViewBills" runat="server" width="100%">
	                            <tr>
	                                <td>
	                                <div style="overflow:scroll; height:150px">
                                     <asp:Repeater ID="rpt_ViewBills" runat="server" OnItemCommand="rptPatientDeskList_ItemCommand">
                                        <HeaderTemplate>
                                            <table align="left" cellpadding="0" cellspacing="0" style="width: 98%; border: #8babe4 1px solid #B5DF82;" >
                                            <tr>
                                                <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" >
                                                    <b>  Bill#</b>
                                                </td>
                                                <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                                    <b> Speciality</b>
                                                </td>
                                                <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                    <b>Visit Date</b> 
                                                </td>
                                                <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                    <b>Bill Date</b>
                                                </td>
                                                <td bgcolor="#B5DF82"  class="lbl" style="font-weight: bold;">
                                                   <b> Bill Status</b>
                                                </td>
                                                <td bgcolor="#B5DF82"  class="lbl" style="font-weight: bold;">
                                                   <b>Bill Amount</b>
                                                </td>
                                                <td bgcolor="#B5DF82"  class="lbl" style="font-weight: bold;">
                                                   <b>Paid</b>
                                                </td>
                                                <td bgcolor="#B5DF82"  class="lbl" style="font-weight: bold;">
                                                   <b>Outstanding</b>
                                                </td>
                                            </tr>
                                           </HeaderTemplate>
                                           <ItemTemplate>
                                                <tr>
                                                    <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;"> 
                                                          <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>
                                                    </td>
                                                    <td bgcolor="white" class = "lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                        <%# DataBinder.Eval(Container,"DataItem.SPECIALITY")%>
                                                    </td>
                                                    <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;text-align:center;">
                                                        <%# DataBinder.Eval(Container,"DataItem.PROC_DATE")%>
                                                    </td>
                                                    <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82; text-align:center;">
                                                        <%# DataBinder.Eval(Container,"DataItem.DT_BILL_DATE")%>
                                                    </td>
                                                    <td bgcolor="white" class = "lbl"style="border: 1px solid #B5DF82;" >
                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>
                                                    </td>
                                                    <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82; text-align:right;">
                                                       $<%# DataBinder.Eval(Container,"DataItem.FLT_BILL_AMOUNT" )%></td>
                                                    <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82; text-align:right;">
                                                       $<%# DataBinder.Eval(Container,"DataItem.PAID_AMOUNT" ) %></td>
                                                    <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82; text-align:right;">
                                                       $<%# DataBinder.Eval(Container,"DataItem.FLT_BALANCE" )%></td>
                                                </tr>
                                            </ItemTemplate> 
                                      <FooterTemplate></table></FooterTemplate>
                                       </asp:Repeater>
                                      </div>
                                        </td>
	                            </tr>
	                        </table>
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
                                    Note Details</div>
                                <div style="vertical-align: top;">
                                    <extddl:ExtendedDropDownList ID="extddlFilter" runat="server" Width="200px" Connection_Key="Connection_String"
                                        Flag_Key_Value="LIST" Procedure_Name="SP_MST_NOTES_TYPE" Text="NTY0002" />
                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" Width="80px" CssClass="Buttons"
                                        OnClick="btnFilter_Click" />
                                    <asp:TextBox ID="txtAccidentID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtUserID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtNoteCode" runat="server" Visible="False" Width="10px"></asp:TextBox></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" >
                            <asp:DataGrid ID="grdNotes" Width="100%" runat="server" CssClass="mGrid" AutoGenerateColumns="False">
                            
                            <PagerStyle CssClass="pgr"/>
                            <AlternatingItemStyle BackColor="#EEEEEE"/>
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="I_NOTE_ID" HeaderText="NOTES ID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_NOTE_DESCRIPTION" HeaderText="Note Description"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DT_ADDED" HeaderText="Date Added" DataFormatString="{0:dd MMM yyyy} ">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_NOTE_TYPE" HeaderText="Note Type"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="GridViewHeader" BackColor="#B5DF82" Font-Bold="true"/>
                            </asp:DataGrid>
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
    <asp:UpdatePanel runat="server" ID="updpnlmemo" UpdateMode="Conditional">
        <contenttemplate>
            <asp:Panel ID="pnlMemo" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="IframeMemo" src="../Bill_Sys_PopupMemo.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
 </contenttemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <contenttemplate>
            <asp:Panel ID="pnlShowNotes" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="Iframe2" src="../Bill_Sys_PopupNotes.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
        
            <asp:Panel ID="pnlShowAtornyInfo" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="Iframe1" src="../Bill_Sys_PopupAttorny.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
         
         <asp:Panel ID="pnlShowReminder" runat="server" Style="width:1100px;height:420px;">
            <iframe id="Iframe3" src="Bill_Sys_Case_Reminder.aspx" height="420px" width="1100px" frameborder="0" 
            visible="false" class="iframe">
            
            
            </iframe>
                   
         </asp:Panel>
         
          <asp:Panel ID="pnlSearchInsuranceCompany" runat="server" Style="width:220px;height:100px;background-color:Blue; visibility:hidden; border-width:1px;border-style:solid;">
            <table width="100%">
                <tr>
                    <td width="30%"> <div class="lbl">Code</div></td>
                    <td width="70%"><asp:TextBox ID="txtSearchCode" runat="server" width="80%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="30%"> <div class="lbl">Name</div></td>
                    <td width="70%"><asp:TextBox ID="txtSearchName" runat="server" width="80%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearchInsCompany" runat="server" Text="Search" CssClass="Buttons" OnClick="btnSearchInsCompany_Click"/>
                    </td>
                </tr>
            </table>       
         </asp:Panel>
        
        
        </contenttemplate>
    </asp:UpdatePanel>
   
    <%--Assign Supplies--%>
    <asp:Panel ID="pnlAssignSupplies" runat="server" Style="width: 420px; height: 220px;
        background-color: white; border-color: SteelBlue; border-width: 1px; border-style: solid;"
        Visible="false">
        <table width="100%">
            <tr>
                <td width="100%" align="right">
                    <asp:Button ID="btnAssignSupplies" runat="server" Text="Assign Supplies" CssClass="Buttons"
                        OnClick="btnAssignSupplies_Click" />
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:DataGrid ID="grdAssignSupplies" Width="100%" CssClass="GridTable" runat="Server"
                        AutoGenerateColumns="False">
                        <HeaderStyle CssClass="GridHeader" />
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Assign">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAssignSupplies" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CHECK" HeaderText="CHECK" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="I_SUPPLIES_ID" HeaderText="Supplies ID" Visible="false">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_SUPPLIES_NAME" HeaderText="Supplies Name" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="divAdjuster" style="visibility: hidden; width: 600px; left: 150px; top: 400px;
        vertical-align: bottom; position: absolute;">
        <asp:Panel ID="pnlAddAdjuster" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="LeftCenter" style="height: 100%">
                    </td>
                    <td class="Center" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divAdjuster').style.visibility='hidden';" style="cursor: pointer;"
                                                        title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="Label7" runat="server" Visible="false"></asp:Label>
                                                <div id="AdjusterErrorDiv" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label8" runat="server">Adjuster</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Adjuster Name:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupName" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Phone Number:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupPhone" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Extension:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupExtension" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                FAX:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupFax" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Email:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                            </td>
                                            <td style="width: 35%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <asp:Button ID="btnSaveAdjuster" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                    OnClick="btnSaveAdjuster_Click" />
                                                <asp:Button ID="btnClearAdjuster" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="RightCenter" style="width: 10px; height: 100%;">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div id="divAddress" style="visibility: hidden; width: 600px; left: 150px; top: 550px;
        vertical-align: bottom; position: absolute;">
        <asp:Panel ID="pnlAddress" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table1" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="LeftCenter" style="height: 100%">
                    </td>
                    <td class="Center" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        <tr>
                                            <td width="100%" colspan="4">
                                                <div id="divAddressError" style="color: red; font-size: small" width="100%">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divAddress').style.visibility='hidden';" style="cursor: pointer;"
                                                        title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="Label1" runat="server" Visible="false"></asp:Label>
                                                <div id="Div1" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCDMsg" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label2" runat="server">Address Details</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Address:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceAddressCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <%--<asp:RequiredFieldValidator ID="reqFieldtxtInsuranceAddressCD" ControlToValidate="txtInsuranceAddressCD" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInsuranceAddressCD"
                                                ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>--%>
                                            <td class="ContentLabel" style="width: 15%">
                                                Street:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceStreetCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                City:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceCityCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                State:</td>
                                            <td style="width: 35%">
                                                <cc1:ExtendedDropDownList ID="extddlInsuranceStateNew" runat="server" Width="80%"
                                                    Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                    Connection_Key="Connection_String"></cc1:ExtendedDropDownList>
                                                <cc1:ExtendedDropDownList ID="extddlInsuranceState" runat="server" Width="255px"
                                                    Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                    Connection_Key="Connection_String" Visible="false"></cc1:ExtendedDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Zip:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceZipCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Default:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="IDDefault" runat="server" />
                                            </td>
                                            <td style="width: 35%">
                                                <%--<asp:TextBox ID="txtCompanyID1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                          --%>
                                                <asp:TextBox ID="txtInsuranceStateNew" runat="server" Visible="false"></asp:TextBox>
                                                <%--<cc1:ExtendedDropDownList ID="extddlInsuranceStateNew" runat="server" Width="80%"
                                                                            Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                                            Connection_Key="Connection_String"></cc1:ExtendedDropDownList>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%-- <td class="ContentLabel">
                                                Default:</td>--%>
                                            <%--<td>
                                                <asp:CheckBox ID="IDDefault" runat="server" />
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <asp:Button ID="btnSaveAddress" OnClientClick="return checkAddressDetails()" runat="server"
                                                    Text="Add" Width="80px" CssClass="Buttons" OnClick="btnSaveAddress_Click" />
                                                <asp:Button ID="btnClearAddress" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="RightCenter" style="width: 10px; height: 100%;">
                    </td>
                </tr>
                <asp:TextBox ID="txtInsuranceCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                <%--<asp:TextBox id="txtInsuranceCompanyAddress" runat="server" Width="10px" Visible="False"></asp:TextBox>--%>
            </table>
        </asp:Panel>
    </div>
   <asp:HiddenField ID="hdninsurancecode" runat="server" />
   <asp:HiddenField ID="hdnattorney" runat="server" />
    <asp:HiddenField ID="hdnEmployerId" runat="server" />
    <asp:TextBox ID="txtEmployerAddId" runat="server" Visible="false"></asp:TextBox>

   <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 700px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 280px; background-color: white" id="divfrmPatient" >
                <div style="position: relative; background-color: #B5DF82 ; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
                </div>
                <iframe id="frmpatient" src="" frameborder="0" width="700" height="370"></iframe>
            </div>
              <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 700px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 400; background-color: white" id="dvTransportation" >
                <div style="position: relative; background-color: #B5DF82 ; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="ClosetransportFramePopup();">X</a>
                </div>
                <iframe id="ifrmTransportation" src="" frameborder="0" width="700" height="400"></iframe>
            </div>
             <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 512px;
        visibility: hidden; border-left: silver 1px solid; width: 780px; border-bottom: silver 1px solid;
        position: absolute; top: 500px; height: 370px; background-color: white" id="ReminderPopUP">
        <div style="position: relative; background-color: #B5DF82; text-align: right">
            <a style="cursor: pointer" title="Close" onclick="CloseCheckoutReminderPopup();">X</a>
        </div>
        <iframe id="frmReminder" src="" frameborder="0" width="780" height="370"></iframe>
    </div>
			 <input type="hidden" runat="server" id="SessionCheck" />
			 
	<input type="hidden" id="hdlimage" value="0" />
	 <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 200px; background-color: #B5DF82" id="divAdjusterfrm" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="CloseadjusterFramePopup();">X</a>
                </div>
                <iframe id="frmadjuster" src="" frameborder="0" width="500" height="380"></iframe>
            </div>
            
            
            <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 600px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 300px; background-color: White;" id="divedit" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="CloseatternoryFramePopup();">X</a>
                </div>
            <iframe id="frmattupdate" src="" frameborder="0" height="300" width="600px"></iframe>
            </div>
            <asp:HiddenField ID="hdadjusterCode" runat="server" />
    <asp:HiddenField ID="hdacode" runat="server" />
           <asp:HiddenField ID="hdnattorneycode" runat="server" />
           <asp:HiddenField ID="hdattorneyset" runat="server" />
        <asp:Button ID="btnattcall" runat="server" OnClick="btnattcall_Click" Text="att" Style="visibility:hidden;"/>
        <asp:Button ID="btnadjcall" runat="server" OnClick="btnadjcall_Click" Text="adj" Style="visibility:hidden;"/>
         <asp:TextBox ID="txtAttorneyid" runat="server" Visible="false"></asp:TextBox>
         
           <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
        
        <ajaxcontrol:ModalPopupExtender ID="mp2" runat="server" TargetControlID="lb_1"
                DropShadow="false" PopupControlID="pnlSave" BehaviorID="modal1" PopupDragHandleControlID="Pdh">
       </ajaxcontrol:ModalPopupExtender>
       
          <asp:Panel Style="display: none; width: 555px; height: 300px; "   
                ID="pnlSave" runat="server">
                
                 <table style="width: 100%; background-color:White; " id="Table3" cellspacing="0" cellpadding="0">
                  <tbody>
                  <tr>
                            <td>
                            </td>
                             <td style="width: 446px" class="Center" valign="top">
                                <table style="width: 100%; height: 100%;" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                        <tr>
                                         <td style="width: 100%">
                                                <table style="width: 535px" border="0">
                                                    <tbody>
                                                    <tr>
                                                            <td style="text-align: left">
                                                        
                                                                   <div style="left: 0px; width:555px; position: absolute; top: 0px; height: 18px;
                                                                    background-color: #B5DF82; text-align: left" id="Pdh">
                                                                    <b>Update Attorney</b>
                                                                    <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;">
                                                                        <asp:Button ID="btnc" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                                            Text="X" OnClientClick="$find('modal1').hide(); return false;"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            <td align="center">
                                                                <div class="lbl">
                                                                    <asp:Label ID="lblMsg1" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                    <div style="color: red" id="ErrorDiv" visible="true">
                                                                        <UserMessage:MessageControl ID="usrMessage1" runat="server"></UserMessage:MessageControl>
                                                                        <asp:UpdateProgress ID="UpdatePanel15" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanel11">
                                                                            <ProgressTemplate>
                                                                                <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                    runat="Server">
                                                                                    <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                    Loading...</div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:10px">
                                                        <td>
                                                        </td>
                                                        </tr>
                                                    </tbody>
                                                 </table>
                                                 
             <table align="center" style="width:535px;height:300px; background-color:White" border="0">
                 <tr>
            <td class="ContentLabel" style="width: 15%" colspan="3">
            </td>
            </tr>
                 <tr>
                    <td class="ContentLabel" style="width: 15%">
                        First Name:</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtAttFirstName" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td class="ContentLabel" style="width: 15%">
                        Last Name:</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtAttLastName" runat="server" ></asp:TextBox></td>
                </tr>
                 <tr>
                    <td  style="width: 15%">
                        Address:</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtAttAddress" runat="server"></asp:TextBox></td>
                    <td class="ContentLabel" style="width: 15%">
                        City:</td>
                    <td style="width: 35%">
                    <asp:TextBox ID="txtAttCity" runat="server"></asp:TextBox></td>
                    
                </tr>
                <tr>
                    <td  style="width: 15%">
                        State:</td>
                    <td style="width: 35%">
                          <cc1:ExtendedDropDownList ID="extddlstateAtt" runat="server" Width="90%" Selected_Text="--- Select ---"
                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                    </cc1:ExtendedDropDownList></td>
                    <td class="ContentLabel" style="width: 15%">
                         Zip:</td>
                    <td style="width: 35%">
                           <asp:TextBox ID="txtAttZip" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                       Phone No:</td>
                    <td style="width: 35%">
                       <asp:TextBox ID="txtAttPhoneNo" runat="server"  MaxLength="12"></asp:TextBox></td>
                    <td class="ContentLabel" style="width: 15%">
                       Fax:</td>
                    <td style="width: 35%">
                     <asp:TextBox ID="txtAttFax" runat="server"  MaxLength="50" valign="top"></asp:TextBox></td>
                   
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                         Email ID:</td>
                    <td style="width: 35%">
                           <asp:TextBox ID="txtAttEmailID" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revattEmailID" runat="server" ControlToValidate="txtAttEmailID"
                        EnableClientScript="True" ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        SetFocusOnError="True"></asp:RegularExpressionValidator></td>     
                    <td class="ContentLabel" style="width: 15%">
                      Attorney Type:</td>
                    <td style="width: 35%">
                    <cc1:ExtendedDropDownList ID="extddlAttorneyType" runat="server" Width="90%" Selected_Text="--- Select ---"
                      Flag_Key_Value="GET_ATT_TYPE_LIST" Procedure_Name="SP_GET_ATTORNEY_TYPE_NAME" Connection_Key="Connection_String">
                    </cc1:ExtendedDropDownList>
                  </td>              
                </tr>                                                    
                   <tr>
                <td class="ContentLabel" style="width: 15%">
                </td>
                <td  style="width: 15%">
                    <asp:CheckBox ID="chkDefaultFirmAtt" runat="server" Visible="false" />
                </td>
                <td style="width: 15%">
                </td>
                <td  style="width: 15%">
                </td>
                </tr>
           
                <tr>
                <td class="ContentLabel" style="width: 15%">
                </td>
                <td  style="width: 15%">
                    
                </td>
                <td style="width: 15%" >
                 <asp:Button ID="btnUpdateAttorney" runat="server" Text="Update" CssClass="Buttons" OnClick="btnUpdateAttorney_Click"/>
                </td>
                <td  style="width: 15%">
                </td>
                </tr>               
            </table>
                                                    </td>
                                        </tr>
                                        </tbody>
                                        </table>
                                </td>
                            </tr>
                  </tbody>
                 </table>
                </asp:Panel>
                 <div style="display: none">
                <asp:LinkButton ID="lb_1" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        
        </ContentTemplate>
        </asp:UpdatePanel>
        
       <asp:UpdatePanel ID="UpdatePanelAtt" runat="server">
        <ContentTemplate>
        
         <ajaxcontrol:ModalPopupExtender ID="mp3" runat="server" TargetControlID="lbl_2"
                DropShadow="false" PopupControlID="pnladdAtt" BehaviorID="modal2" PopupDragHandleControlID="pp2">
          </ajaxcontrol:ModalPopupExtender>
          
          <asp:Panel Style="display: none; width: 555px; height: 300px;"
                ID="pnladdAtt" runat="server">
                   <table style="width: 100%; background-color:White;" id="Table4" cellspacing="0" cellpadding="0" border="0">
                  <tbody>
                       <tr>
                            <td>
                            </td>
                              <td style="width: 446px" class="Center" valign="top">
                                <table style="width: 100%; height: 100%" cellspacing="0" cellpadding="0" border="0">
                                  <tbody>
                                        <tr>
                                         <td style="width: 100%">
                                          <table style="width: 535px" border="0">
                                                    <tbody>
                                                       <tr>
                                                            <td style="text-align: left">
                                                          <div style="left: 0px; width:555px; position: absolute; top: 0px; height: 18px;
                                                            background-color: #B5DF82; text-align: left" id="pp2">
                                                            <b>Add Attorney</b>
                                                            <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;">
                                                                <asp:Button ID="btnclc" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                                    Text="X" OnClientClick="$find('modal2').hide(); return false;"></asp:Button>
                                                            </div>
                                                        </div>
                                                                
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            <td align="center">
                                                                <div class="lbl">
                                                                    <asp:Label ID="lblMsg3" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                    <div style="color: red" id="Div4" visible="true">
                                                                        <UserMessage:MessageControl ID="usrMessage3" runat="server"></UserMessage:MessageControl>
                                                                        <asp:UpdateProgress ID="UpdatePanel18" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanelAtt">
                                                                            <ProgressTemplate>
                                                                                <div id="Div5" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                    runat="Server">
                                                                                    <asp:Image ID="img42" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                    Loading...</div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 10px">
                                                        <td>
                                                        </td>
                                                        </tr>
                                                    </tbody>
                                                    </table>
                                          
     <table align="center" style="width:500px;height:300px; background-color:White;">
            <tr>
            <td class="ContentLabel" style="width: 15%" colspan="3">
            </td>
            </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                        First Name:</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtAttorneyFirstName" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td class="ContentLabel" style="width: 15%">
                        Last Name:</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtAttorneyLastName" runat="server" ></asp:TextBox></td>
                </tr>
                 <tr>
                    <td  style="width: 15%">
                        Address:</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtboxAttorneyAddress" runat="server"></asp:TextBox></td>
                    <td class="ContentLabel" style="width: 15%">
                        City:</td>
                    <td style="width: 35%">
                    <asp:TextBox ID="txtboxAttorneyCity" runat="server"></asp:TextBox></td>
                    
                </tr>
                <tr>
                    <td  style="width: 15%">
                        State:</td>
                    <td style="width: 35%">
                          <cc1:ExtendedDropDownList ID="extddlState" runat="server" Width="90%" Selected_Text="--- Select ---"
                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                    </cc1:ExtendedDropDownList></td>
                    <td class="ContentLabel" style="width: 15%">
                         Zip:</td>
                    <td style="width: 35%">
                           <asp:TextBox ID="txtboxAttorneyZip" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                       Phone No:</td>
                    <td style="width: 35%">
                       <asp:TextBox ID="txtAttorneyPhoneNo" runat="server"  MaxLength="12"></asp:TextBox></td>
                    <td class="ContentLabel" style="width: 15%">
                       Fax:</td>
                    <td style="width: 35%">
                     <asp:TextBox ID="txtboxAttorneyFax" runat="server"  MaxLength="50" valign="top"></asp:TextBox></td>
                   
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                         Email ID:</td>
                    <td style="width: 35%">
                           <asp:TextBox ID="txtAttorneyEmailID" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtAttorneyEmailID"
                        EnableClientScript="True" ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        SetFocusOnError="True"></asp:RegularExpressionValidator></td>     
                    <td class="ContentLabel" style="width: 15%">
                      Attorney Type:</td>
                    <td style="width: 35%">
                    <cc1:ExtendedDropDownList ID="extddlAtttype" runat="server" Width="90%" Selected_Text="--- Select ---"
                      Flag_Key_Value="GET_ATT_TYPE_LIST" Procedure_Name="SP_GET_ATTORNEY_TYPE_NAME" Connection_Key="Connection_String">
                    </cc1:ExtendedDropDownList>
                  </td>              
                </tr>                                                    
                   <tr>
                <td class="ContentLabel" style="width: 15%">
                 </td>
                <td  style="width: 15%">
                    <asp:CheckBox ID="chkDefaultFirm" runat="server" Visible="false" />
                </td>
                <td style="width: 15%">
                </td>
                <td  style="width: 15%">
                </td>
                </tr>
           
                <tr>
                <td class="ContentLabel" style="width: 15%">
                </td>
                <td  style="width: 15%">
                    
                </td>
                <td style="width: 15%">
                 <asp:Button ID="btnAddAttorney" runat="server" Text="Add" CssClass="Buttons" OnClick="btnAddAttorney_Click"/>
                </td>
                <td  style="width: 15%">
                </td>
                </tr>               
            </table>    
                                         </td>
                                         </tr>
                                         </tbody>
                                </table>
                                </td>
                            </tr>
                  </tbody>
                  </table>
                </asp:Panel>
                        <div style="display: none">
                <asp:LinkButton ID="lbl_2" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
         <asp:UpdatePanel ID="UpdatePaneladuster" runat="server">
        <contenttemplate>
        
        <ajaxcontrol:ModalPopupExtender ID="ModalPopupaduster" runat="server" TargetControlID="lb_10"
                DropShadow="false" PopupControlID="pnlupdateaduster" BehaviorID="modal15" PopupDragHandleControlID="Pad">
       </ajaxcontrol:ModalPopupExtender>
       
          <asp:Panel Style="display: none; width: 555px; height: 300px; "   
                ID="pnlupdateaduster" runat="server">
                
                 <table style="width: 100%; background-color:White; " id="Table5" cellspacing="0" cellpadding="0">
                  <tbody>
                  <tr>
                            <td>
                            </td>
                            
                             <td style="width: 446px" class="Center" valign="top">
                                <table style="width: 100%; height: 100%;" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                        <tr>
                                         <td style="width: 100%">
                                                <table style="width: 535px" border="0">
                                                    <tbody>
                                                    <tr>
                                                            <td style="text-align: left">
                                                        
                                                                   <div style="left: 0px; width:555px; position:absolute; top: 0px; height: 18px;
                                                                    background-color: #B5DF82; text-align: left" id="Pad">
                                                                    <b>Update Adjuster </b>
                                                                    <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;">
                                                                        <asp:Button ID="btnc6" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                                            Text="X" OnClientClick="$find('modal15').hide(); return false;"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                            <td align="center">
                                                                <div class="lbl">
                                                                    <asp:Label ID="lblMsg15" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                    <div style="color: red" id="Div7" visible="true">
                                                                        <UserMessage:MessageControl ID="MessageControl2" runat="server"></UserMessage:MessageControl>
                                                                        <asp:UpdateProgress ID="UpdatePanel123" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePaneladuster">
                                                                            <ProgressTemplate>
                                                                                <div id="Div8" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                    runat="Server">
                                                                                    <asp:Image ID="img24" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                    Loading...</div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:10px">
                                                        <td>
                                                        </td>
                                                        </tr>
                                                    </tbody>
                                                 </table>
                                                 
             <table align="center" style="width:535px;height:150px; background-color:White; border-right:1px red" border="0">
                 <%--<tr>
            <td class="ContentLabel" style="width: 15%" colspan="3">
            </td>
            </tr>--%>
                 <tr>
                    <td class="ContentLabel" style="width: 15%">
                       Adjuster Name:</td>
                    <td style="width: 35%">
                       <asp:TextBox ID="txtAdjusterPopupName1" runat="Server" autocomplete="off"  OnTextChanged="txtInsuranceCompany_TextChanged1" AutoPostBack="true"/></td>
                    <td class="ContentLabel" style="width: 15%">
                        Phone Number:</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtAdjusterPopupPhone1" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                 <tr>
                     <td  style="width: 15%">
                         Extension:</td>
                    <td style="width: 35%">
                       <asp:TextBox ID="txtAdjusterPopupExtension1" runat="server"  MaxLength="50"></asp:TextBox></td>
                    <td class="ContentLabel" style="width: 15%">
                        FAX:</td>
                    <td style="width: 35%"><asp:TextBox ID="txtAdjusterPopupFax1" runat="server"  MaxLength="50"></asp:TextBox></td>
                    
                </tr>
                <tr>
                    <td  style="width: 15%">
                         Email:</td>
                    <td style="width: 85%" colspan="3">
                          <asp:TextBox ID="txtAdjusterPopupEmail1" runat="server" Width="91%"  MaxLength="50"></asp:TextBox>
                          <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtAdjusterPopupEmail1"
                        EnableClientScript="True" ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                    
                   
                </tr>
                <tr>
                <%--<td class="ContentLabel" style="width: 15%">
                </td>
                <td  style="width: 15%">
                    
                </td>--%>
                <td style="width: 15%" align="center" colspan="4">
                 <asp:Button ID="btnUpdateAdjuster" runat="server" Text="Update" CssClass="Buttons" OnClick="btnUpdateAdjuster_Click"/>
                </td>
                <%--<td  style="width: 15%">
                </td>--%>
                </tr>
               
               
            </table>
                                                    </td>
                                        </tr>
                                        </tbody>
                                        </table>
                                </td>
                            </tr>
                  </tbody>
                 </table>
                </asp:Panel>
                 <div style="display: none">
                <asp:LinkButton ID="lb_10" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>

        <dx:ASPxPopupControl ID="AddAdjusterPop" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="AddAdjusterPop" HeaderText="Adjuster Information" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="525px" ToolTip="Add Adjuster" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
        RenderIFrameForPopupElements="Default" Height="400px">

           <ClientSideEvents CloseButtonClick="ClosePopup" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="SecInsuracePop" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="SecInsuracePop" HeaderText="Secondary Insurance Information" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="900px" ToolTip="Patient Information" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
        RenderIFrameForPopupElements="Default" Height="400px">

        <ClientSideEvents CloseButtonClick="ClosePopup1" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
   
   
             <%--End--%>
        
        </contenttemplate>
    </asp:UpdatePanel>
     <asp:TextBox ID="txtCaseTypeID" runat="server" Visible="false"></asp:TextBox>
</asp:Content>