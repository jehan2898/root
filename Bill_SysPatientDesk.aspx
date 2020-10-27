<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_SysPatientDesk.aspx.cs" Inherits="Bill_SysPatientDesk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register TagPrefix="oem" Namespace="OboutInc.EasyMenu_Pro" Assembly="obout_EasyMenu_Pro" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script src="js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="js/scan/Scan.js" type="text/javascript"></script>
    <script src="js/scan/function.js" type="text/javascript"></script>
    <script src="js/scan/Common.js" type="text/javascript"></script>
    <link href="Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="validation.js"></script>

    <%--    <script type="text/javascript" src="Registration/validation.js"></script>--%>

 <script type="text/javascript">
         $(document).ready(function () {

             $('.scanlbl').click(function () {
                 debugger;
                 var data = $(this).attr('data-val');
                 var dataArray = data.split(',');
                 var caseid = $('[id$=hdnCaseId]').val();
                 var caseno = $('[id$=hdnCaseNo]').val();
                 var eventid = dataArray[0];
                 var specialtyID = dataArray[1];
                 var visitTypeId = dataArray[2];
                 var ScheduleType = dataArray[3];
                 var sRoomID = dataArray[4];                 
                 var visittype = "";
                 var test_facility = $('[id$=hdn_TestFacility]').val();
                 if (test_facility == "True") {
                     visittype = "Followup Report";
                 }
                 else {
                     if (ScheduleType.substr(0, 2) == "FU" || ScheduleType.substr(0, 2) == "C" || ScheduleType.substr(0, 2) == "") {
                         visittype = "Followup Report";
                     }
                     if (ScheduleType.substr(0, 2) == "IE") {
                         visittype = "Initial Report";
                     }
                 }

                 if (test_facility == "False") {
                     specialtyID = sRoomID;
                 }
                 scanVisit(caseid, caseno, eventid, specialtyID, visittype, visitTypeId, '5', sRoomID, test_facility);
             });
         });
    </script>

    <script type="text/javascript">
        function EDITSchedule(eventId) {

            document.getElementById("divid").style.position = "absolute";

            document.getElementById("divid").style.left = "250px";

            document.getElementById("divid").style.top = "150px";

            document.getElementById("divid").style.visibility = "visible";
            document.getElementById('divid').style.zIndex = '1';
            document.getElementById("divid").style.width = "500px";
            document.getElementById("divid").style.height = "350px";
            document.getElementById("frameeditexpanse").style.width = "500px";
            document.getElementById("frameeditexpanse").style.height = "350px";
            document.getElementById("frameeditexpanse").src = "Bill_Sys_UpdateVisit.aspx?eventId=" + eventId;

        }

        function EDITOutSchedule(eventId) {

            document.getElementById("divid").style.position = "absolute";

            document.getElementById("divid").style.left = "250px";

            document.getElementById("divid").style.top = "150px";

            document.getElementById("divid").style.visibility = "visible";
            document.getElementById('divid').style.zIndex = '1';
            document.getElementById("divid").style.width = "500px";
            document.getElementById("divid").style.height = "350px";
            document.getElementById("frameeditexpanse").style.width = "500px";
            document.getElementById("frameeditexpanse").style.height = "350px";
            document.getElementById("frameeditexpanse").src = "Bill_Sys_UpdateOutScheduleVisit.aspx?eventId=" + eventId;

        }
        function OpenUploadPanel() {

            document.getElementById("Uploaddiv").style.position = "absolute";

            document.getElementById("Uploaddiv").style.left = "300px";

            document.getElementById("Uploaddiv").style.top = "150px";

            document.getElementById("Uploaddiv").style.visibility = "visible";
            document.getElementById("Uploaddiv").style.zIndex = '1';

        }
        function Close_UploadPanel() {
            document.getElementById("Uploaddiv").style.visibility = "hidden";

        }

        function EDIT_MRI_OutSchedule(objQueryString) {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute';
            document.getElementById('div1').style.left = '300px';
            document.getElementById('div1').style.top = '100px';
            document.getElementById('div1').style.zindex = '1';
            document.getElementById('div1').style.visibility = 'visible';
            document.getElementById("frameUpdateOutschedule").src = "Bill_Sys_AddReferredAppointment.aspx?_date=" + objQueryString;
        }

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

        function ConfirmDelete() {
            var msg = "Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function SelectTab(itemID) {
            try {

                document.getElementById("_ctl0_ContentPlaceHolder1_Hidden1").value = itemID;
                ob_em_SelectItem(itemID);

                document.getElementById("_ctl0_ContentPlaceHolder1_btnTab").click();

                //                var sURL = unescape(window.location.pathname);
                //                sURL = document.getElementById('_ctl0_ContentPlaceHolder1_Hidden2').value;
                //                window.location.href = sURL;
            }
            catch (e) {
            }
        }
        function changeSrc() {
            document.getElementById('_ctl0_ContentPlaceHolder1_tabid').value = '1';
            document.getElementById('MpnlShowSearch').style.zIndex = 1;
            document.getElementById('MpnlShowSearch').style.position = 'absolute';
            document.getElementById('MpnlShowSearch').style.left = '400px';
            document.getElementById('MpnlShowSearch').style.top = '75px';
            document.getElementById('MpnlShowSearch').style.visibility = 'visible';
            //document.getElementById("MiframeSearch").src="Bill_Sys_PopupNewVisit.aspx" ;
            document.getElementById("MiframeSearch").src = "AJAX Pages/Bill_sys_new_VisitPopup.aspx";
            return false;
        }

        function showPateintFrame(objType, objDoc, objEvent, objPGID, grd) {

            //alert(objPGID);
            //alert(grd);
            document.getElementById('_ctl0_ContentPlaceHolder1_tabid').value = grd;
            var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute';
            document.getElementById('divfrmPatient').style.left = '400px';
            document.getElementById('divfrmPatient').style.top = '250px';
            document.getElementById('divfrmPatient').style.visibility = 'visible';
            document.getElementById('frmpatient').src = "AJAX Pages/Bill_Sys_Upload_Doc_Popup.aspx?Type=" + objType + "&Doc=" + objDoc + "&Eve=" + objEvent + "&PGID=";
            return false;
        }

        function showViewFrame(objEvent) {

            // alert(objCaseID + ' ' + objCompanyID);
            var obj3 = "";
            document.getElementById('divView').style.zIndex = 1;
            document.getElementById('divView').style.position = 'absolute';
            document.getElementById('divView').style.left = '400px';
            document.getElementById('divView').style.top = '250px';
            document.getElementById('divView').style.visibility = 'visible';
            document.getElementById('frmView').src = "AJAX Pages/Bill_Sys_ViewDocPopUp.aspx?Eve=" + objEvent + "";
            return false;
        }
        function CloseViewFramePopup() {
            //   alert("");
            //document.getElementById('divView').style.height='0px';
            document.getElementById('divView').style.visibility = 'hidden';
            document.getElementById('divView').style.top = '-10000px';
            document.getElementById('divView').style.left = '-10000px';
        }

        function ClosePatientFramePopup() {
            //   alert("");
            //document.getElementById('divfrmPatient').style.height='0px';
            document.getElementById('divfrmPatient').style.visibility = 'hidden';
            document.getElementById('divfrmPatient').style.top = '-10000px';
            document.getElementById('divfrmPatient').style.left = '-10000px';

            __doPostBack('Button2', 'My Argument');

        }

        function OpenPage(objType, objDoc, objEvent, procedureGroupId, caseId) {
            window.open('ViewNotes.aspx?Type=' + objType + '&Doc=' + objDoc + '&eid=' + objEvent + "&pgid=" + procedureGroupId + "&cid=" + caseId, 'ViewNotes', 'width=800,height=600,left=30,top=30,scrollbars=1');
        }

    </script>
    
    

    <input type="hidden" id="Hidden2" name="Text1" runat="server" />
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
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
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    Patient Information <a href="#anch_top">(Top)</a>
                                                </td>
                                                <td align="right" style="width: 50%">
                                                    <a id="hlnkShowNotes" href="#" runat="server">
                                                        <img src="Images/actionEdit.gif" style="border-style: none;" /></a>
                                                    <ajaxcontrol:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowNotes"
                                                        PopupControlID="pnlShowNotes" Position="Bottom" OffsetX="-420" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdPatientList" Width="50%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_NAME" HeaderText="Attorney Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table id="tblTreatmentInformation" runat="server" style="width: 100%; vertical-align: top;
                                            position: relative;" height="60" visible="false">
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative; height: 20px;">
                                                    Treatment Information <a href="#anch_top">(Top)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative; height: 40px;">
                                                    <asp:DataGrid ID="grdTreatment" runat="Server" Width="100%" CssClass="GridTable"
                                                        AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="TOTAL_TREATMENTS" HeaderText="No Of Treatments Till date"
                                                                ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="WEEKLY_TREATMENT" HeaderText="Treatments in this week"
                                                                ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tblVisitInformation" runat="server" style="width: 100%" visible="false">
                                            <tr>
                                                <td align="left" style="vertical-align: top; position: relative;">
                                                    Visit Information <a href="#anch_top">(Top)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 300px">
                                                    <br />
                                                    <br />
                                                    <%--<asp:UpdatePanel ID="updpnlCal" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>--%>
                                                </td>
                                                <td align="left" style="vertical-align: top; position: relative;">
                                                    <br />
                                                    <br />
                                                    <asp:DataGrid ID="grdVisitInformation" Width="100%" CssClass="GridTable" runat="Server"
                                                        AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="DT_VISIT_DATE" HeaderText="Visit Date" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR NAME"  ItemStyle-HorizontalAlign="Left">
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table id="tblTabTreatment" runat="server" style="width: 100%; vertical-align: top;"
                                            height="60">
                                            <tr>
                                                <td align="left" style="vertical-align: top; position: relative;">
                                                    Test Information <a href="#anch_top">(Top)</a>
                                                    <input type="hidden" id="Hidden1" name="Text1" runat="server" />
                                                </td>
                                                <td align="right" style="width: 50%">
                                                   <%-- <a id="hlnkShowTestNotes" href="#" runat="server">
                                                        <img src="Images/actionEdit.gif" style="border-style: none;" /></a>
                                                    <ajaxcontrol:PopupControlExtender ID="PopEx2" runat="server" TargetControlID="hlnkShowTestNotes"
                                                        PopupControlID="pnlNewVisit" Position="Bottom" OffsetX="-420" />--%>
                                                       <a id="A1" href="#" runat="server" onclick="changeSrc();">
                                                         <img src="Images/addvisit.jpg" style="border-style: none; height:20px;width:20px;"  Title="Add Visit"/>
                                                       </a>
                                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <%--<div style="border: 3px solid #c0cff0; width: auto; height: auto; text-align:right;width: 799px" id="tabIframe">--%>
                                                    <ajaxcontrol:TabContainer ID="tabVistInformation" runat="Server" ActiveTabIndex="1"
                                                        CssClass="ajax__tab_theme">
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlOne" Visible="False" TabIndex="1" >
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadOne" runat="server" Text="" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalOne" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteOne" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="1" />
                                                                            <asp:DataGrid ID="grdOne" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnItemCommand="dg_ItemCommand"  OnEditCommand="dg_EditCommand" OnUpdateCommand="dg_UpdateCommand" >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("2")+ "\");" %>' ></a>--%>
                                                                                  <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                          |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                       <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwo" TabIndex="1" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwo" runat="server" Text="" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwo" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwo" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="2" />
                                                                            <asp:DataGrid ID="grdTwo" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"   OnCancelCommand="dg_CancelCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                              <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                        <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("2")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinktwo" CommandName="Upload" Visible="false" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" />|<asp:LinkButton ID="lnkScanTwo" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| --%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                          |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlThree" TabIndex="2" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThree" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThree" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThree" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="3" />
                                                                            <asp:DataGrid ID="grdThree" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnItemCommand="dg_ItemCommand"   OnEditCommand="dg_EditCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                        <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("3")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkthree" Visible="false" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload"  />|<asp:LinkButton ID="lnkScanThree" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />|--%> 
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                          |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlFour" TabIndex="3" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadFour" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalFour" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteFour" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="4" />
                                                                            <asp:DataGrid ID="grdFour" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnItemCommand="dg_ItemCommand"   OnEditCommand="dg_EditCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                        <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("4")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkfour" Visible="false" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" />|<asp:LinkButton ID="lnkScanFour" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />|--%> 
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                         |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlFive" TabIndex="4" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadFive" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalFive" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteFive" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="5" />
                                                                            <asp:DataGrid ID="grdFive" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnItemCommand="dg_ItemCommand"   OnEditCommand="dg_EditCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                       <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("5")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton Visible="false" ID="btnLinkfive" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" />|<asp:LinkButton ID="lnkScanFive" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />|--%>
                                                                                             <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                          |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlSix" TabIndex="5" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadSix" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalSix" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteSix" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="6" />
                                                                            <asp:DataGrid ID="grdSix" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnItemCommand="dg_ItemCommand"   OnEditCommand="dg_EditCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                        <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("6")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinksix"  Visible="false" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" />|<asp:LinkButton ID="lnkScanSix" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlSeven" TabIndex="6" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadSeven" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalSeven" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteSeven" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="7" />
                                                                            <asp:DataGrid ID="grdSeven" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnItemCommand="dg_ItemCommand"  OnEditCommand="dg_EditCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                  
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("7")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkseven" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanSeven" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                          <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlEight" TabIndex="7" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadEight" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalEight" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteEight" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="8" />
                                                                            <asp:DataGrid ID="grdEight" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                      <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("8")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkeight" Visible="false" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" />|<asp:LinkButton ID="lnkScanEight" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlNine" TabIndex="8" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadNine" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalNine" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteNine" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                CommandArgument="9" />
                                                                            <asp:DataGrid ID="grdNine" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("8")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinknine" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanNine" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                           <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTen" TabIndex="9" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <div style="border: 3px solid #c0cff0; width: auto; height: auto; text-align: right;">
                                                                                <asp:Button ID="btnDeleteTen" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click"
                                                                                    CommandArgument="10" />
                                                                                <asp:DataGrid ID="grdTen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                    OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                    <ItemStyle CssClass="GridRow" />
                                                                                     <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                        <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("10")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkten" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                         <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                                </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlEleven" TabIndex="10" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadEleven" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalEleven" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteEleven" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="11" />
                                                                            <asp:DataGrid ID="grdEleven" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                       <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("11")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkeleven" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanEleven" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                           <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwelve" TabIndex="11" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwelve" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwelve" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwelve" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="12" />
                                                                            <asp:DataGrid ID="grdTwelve" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                       <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("12")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkTwelve" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTwelve" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                      <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlThirteen" TabIndex="12" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThirteen" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThirteen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThirteen" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="13" />
                                                                            <asp:DataGrid ID="grdThirteen" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                        <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("13")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkThirteen" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanThirteen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                         <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlFourteen" TabIndex="13" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadFourteen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalFourteen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteFourteen" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="14" />
                                                                            <asp:DataGrid ID="grdFourteen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                      <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("14")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkFourteen" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanFourteen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                           <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlFifteen" TabIndex="14" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadFifteen" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                       <%-- <td>
                                                                            <asp:UpdatePanel ID="updpnlCalFifteen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteFifteen" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="15" />
                                                                            <asp:DataGrid ID="grdFifteen" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("15")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkFifteen" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanFifteen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                       <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlSixteen" TabIndex="15" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadSixteen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalSixteen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteSixteen" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="16" />
                                                                            <asp:DataGrid ID="grdSixteen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("16")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinksixteen" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanSixteen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                        <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlSeventeen" TabIndex="16" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadSeventeen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalSeventeen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteSeventeen" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="17" />
                                                                            <asp:DataGrid ID="grdSeventeen" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("17")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkseventeen" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanSevenTeen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlEighteen" TabIndex="17" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadEighteen" runat="server" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalEighteen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            
</contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteEighteen" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="18" />
                                                                            <asp:DataGrid ID="grdEighteen" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                            <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("18")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkeighteen" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScaneighteen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                             <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlNineteen" TabIndex="18" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadNineteen" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalNineteen" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteNineteen" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="19" />
                                                                            <asp:DataGrid ID="grdNineteen" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("19")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinknineteen" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanNineteen" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                           <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwenty" TabIndex="19" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwenty" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                       <%-- <td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwenty" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwenty" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="20" />
                                                                            <asp:DataGrid ID="grdTwenty" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("20")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkTwenty" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false"  />|<asp:LinkButton ID="lnkScanTwenty" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                             <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                          <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentyOne" TabIndex="20" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentyOne" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentyOne" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentyOne" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="21" />
                                                                            <asp:DataGrid ID="grdTwentyOne" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                            <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("21")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkTwentyone" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false"  />|<asp:LinkButton ID="lnkScanTwentyOne" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                          <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentyTwo" TabIndex="21" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentyTwo" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentyTwo" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentyTwo" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="22" />
                                                                            <asp:DataGrid ID="grdTwentyTwo" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("22")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkTwentytwo" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTwentyTwo" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentyThree" TabIndex="22" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentyThree" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentyThree" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentyThree" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="23" />
                                                                            <asp:DataGrid ID="grdTwentyThree" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("23")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkTwentythree" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTwentyThree" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentyFour" TabIndex="23" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentyFour" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentyFour" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentyFour" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="24" />
                                                                            <asp:DataGrid ID="grdTwentyFour" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("24")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkTwentyfour" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTwentyFour" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                         <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentyFive" TabIndex="24" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentyFive" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentyFive" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentyFive" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="25" />
                                                                            <asp:DataGrid ID="grdTwentyFive" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("25")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkTwentyfive" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTwentyFive" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentySix" TabIndex="25" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentySix" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentySix" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentySix" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="26" />
                                                                            <asp:DataGrid ID="grdTwentySix" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("26")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkTwentysix" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTwentySix" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                          <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentySeven" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentySeven" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentySeven" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentySeven" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="27" />
                                                                            <asp:DataGrid ID="grdTwentySeven" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False"
                                                                                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand" OnUpdateCommand="dg_UpdateCommand"  >
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                   <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                            <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("27")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkTwentyseven" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanTwentySeven" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                        <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                    
                                                    
                                                    <%--New Tab Added - SY - 14 Apr - 2010--%>
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentyEight" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentyEight" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentyEight" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentyEight" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="28" />
                                                                            <asp:DataGrid ID="grdTwentyEight" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("28")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkTwentyeight" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload"  Visible="false" />|<asp:LinkButton ID="lnkScanTwentyEight" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                          <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        
                                                        <ajaxcontrol:TabPanel runat="server" ID="tabpnlTwentyNine" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadTwentyNine" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalTwentyNine" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteTwentyNine" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="29" />
                                                                            <asp:DataGrid ID="grdTwentyNine" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                      <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("29")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkTwentynine" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload"  Visible="false" />|<asp:LinkButton ID="lnkScanTwentyNine" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                           <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlThirty" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThirty" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThirty" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThirty" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="30" />
                                                                            <asp:DataGrid ID="grdThirty" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                               <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("30")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkThirty" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload"  Visible="false" />|<asp:LinkButton ID="lnkScanThirty" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                             <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlThirtyOne" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThirtyOne" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThirtyOne" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThirtyOne" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="31" />
                                                                            <asp:DataGrid ID="grdThirtyOne" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                            <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("31")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkThirtyOne" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanThirtyOne" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                           <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        
                                                        
                                                          <ajaxcontrol:TabPanel runat="server" ID="tabpnlThirtyTwo" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThirtyTwo" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThirtyTwo" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThirtyTwo" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="32" />
                                                                            <asp:DataGrid ID="grdThirtyTwo" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("32")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkThirtytwo" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false"  />|<asp:LinkButton ID="lnkScanThirtyTwo" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlThirtyThree" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThirtyThree" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThirtyThree" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThirtyThree" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="33" />
                                                                            <asp:DataGrid ID="grdThirtyThree" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%--<a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("33")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkThirtythree" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false" />|<asp:LinkButton ID="lnkScanThirtyThree" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                            <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                            |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                            
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        
                                                         <ajaxcontrol:TabPanel runat="server" ID="tabpnlThirtyFour" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThirtyFour" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThirtyFour" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThirtyFour" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="34" />
                                                                            <asp:DataGrid ID="grdThirtyFour" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                          <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("34")+ "\");" %>' >Upload</a>--%>
                                                                                           <%-- <asp:LinkButton ID="btnLinkThirtyfour" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload" Visible="false"  />|<asp:LinkButton ID="lnkScanThirtyFour" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                             <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                          |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                        
                                                          <ajaxcontrol:TabPanel runat="server" ID="tabpnlThirtyFive" TabIndex="26" Visible="False">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblHeadThirtyFive" runat="server" Text="" Style="width: 120px;" class="lbl"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <%--<td>
                                                                            <asp:UpdatePanel ID="updpnlCalThirtyFive" runat="server">
                                                                                <contenttemplate>
                                                                                
                                                                            </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>--%>
                                                                        <td style="text-align: right;">
                                                                            <asp:Button ID="btnDeleteThirtyFive" runat="server" CssClass="Buttons" Text="Delete"
                                                                                OnClick="btnDelete_Click" CommandArgument="35" />
                                                                            <asp:DataGrid ID="grdThirtyFive" Width="100%" CssClass="GridTable" runat="Server"
                                                                                AutoGenerateColumns="False" OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand" OnItemCommand="dg_ItemCommand"
                                                                                OnUpdateCommand="dg_UpdateCommand">
                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                <ItemStyle CssClass="GridRow" />
                                                                                 <Columns>
                                                                                    <%--0--%>
                                                                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="VISTID" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--1--%>
                                                                                    <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--2--%>
                                                                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR NAME" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--3--%>
                                                                                    <asp:BoundColumn DataField="SZ_VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--4--%>
                                                                                    <asp:BoundColumn DataField="EVENT_DATE_SHOW" HeaderText="TREATMENT DATE" ReadOnly="true"
                                                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                    <%--5--%>
                                                                                    <asp:TemplateColumn HeaderText="TREATMENT STATUS" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" Width="100px">
                                                                                                <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                                                <asp:ListItem Value="3">No-show</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--6--%>
                                                                                    <asp:BoundColumn DataField="STATUS" HeaderText="TREATMENT STATUS" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--7--%>
                                                                                    <asp:TemplateColumn HeaderText="VISIT TYPE" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--8--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="VISIT TYPE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--9--%>
                                                                                    <asp:BoundColumn DataField="SPECIALITY" HeaderText="TEST" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--10--%>
                                                                                    <asp:BoundColumn DataField="EVENT_ID" HeaderText="Eventid" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--11--%>
                                                                                    <asp:BoundColumn DataField="TYPE_CODE_ID" HeaderText="TYPE CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--12--%>
                                                                                    <asp:BoundColumn DataField="SZ_COLOR_CODE" HeaderText="COLOR_CODE" ItemStyle-HorizontalAlign="Left"
                                                                                        Visible="false"></asp:BoundColumn>
                                                                                    <%--13--%>
                                                                                    <asp:TemplateColumn HeaderText="Treatments" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES") %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--14--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--15--%>
                                                                                    <asp:EditCommandColumn ButtonType="LinkButton" EditText="Edit" CancelText="Cancel"
                                                                                        UpdateText="Update" />
                                                                                    <%--16--%>
                                                                                    <asp:BoundColumn DataField="VISIT_TYPE_ID" HeaderText="VISIT_TYPE_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--17--%>
                                                                                    <asp:BoundColumn DataField="I_STATUS" HeaderText="STATUS_ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--18--%>
                                                                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--19--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtEventType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--20--%>
                                                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--21--%>
                                                                                    <asp:BoundColumn DataField="IS_REFERRAL" HeaderText="Referral Doctor" Visible="false">
                                                                                    </asp:BoundColumn>
                                                                                    <%--22--%>
                                                                                    <asp:BoundColumn DataField="BT_STATUS" HeaderText="Bill Status" Visible="false"></asp:BoundColumn>
                                                                                    <%--23--%>
                                                                                    <%--<asp:BoundColumn DataField="SCHEDULE_TYPE" HeaderText="Schedule Type" Visible="false"></asp:BoundColumn>--%>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtScheduleType" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.SCHEDULE_TYPE") %>'>                                                                               
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                     <%--24--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="false"></asp:BoundColumn>
                                                                                    <%--25--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--26--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_TIME_TYPE" HeaderText="Event Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--27--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME" HeaderText="Event End Time" Visible="false"></asp:BoundColumn>
                                                                                    <%--28--%>
                                                                                    <asp:BoundColumn DataField="DT_EVENT_END_TIME_TYPE" HeaderText="Event End Time Type" Visible="false"></asp:BoundColumn>
                                                                                    <%--29--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID" Visible="false"></asp:BoundColumn>
                                                                                    <%--30--%>
                                                                                    <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                            <a id="A2" href="#" runat="server" data-val='<%# Eval("I_EVENT_ID")+","+ Eval("GRP_ID")+","+ Eval("VISIT_TYPE_ID")+","+ Eval("VISIT_TYPE") + "," + Eval("SZ_PROCEDURE_GROUP_ID") %>'
                                                                                                        title="Scan" class="lbl scanlbl">Scan / Upload</a>
                                                                                           <%-- <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID")  +"\""+ ",\"" + ("35")+ "\");" %>' >Upload</a>--%>
                                                                                            <%--<asp:LinkButton ID="btnLinkThirtyfive" CommandName="Upload" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Upload"  Visible="false" />|<asp:LinkButton ID="lnkScanThirtyFive" CommandName="Scan" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID") %>' runat="server" Text="Scan" />| <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>--%>
                                                                                           <asp:LinkButton ID="lnkDoctorNotes" runat="server" Text='|View Notes' CommandName="" OnClick='<%# "OpenPage(" + "\""+ Eval("TYPE") + "\""+ ",\"" + Eval("SZ_DOCTOR_ID")+ "\""+ ",\"" + Eval("I_EVENT_ID")+ "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_CODE")  +"\""+ ",\"" + Eval("SZ_CASE_ID")+ "\");" %>' Visible="false"></asp:LinkButton>
                                                                                             |<a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(" + "\""+ Eval("I_EVENT_ID")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <%--31--%>
                                                                                    <asp:BoundColumn DataField="BT_DOC" HeaderText="BT_DOC" Visible="false"></asp:BoundColumn>
                                                                                    <%--32--%>
                                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_CODE" HeaderText="Procedure Group Code" Visible="false"></asp:BoundColumn>
                                                                                    <%--33--%>
                                                                                    <asp:BoundColumn DataField="IS_ADDED_BY_DOCTOR" HeaderText="Added by Doctor" Visible="false"></asp:BoundColumn>
                                                                                    <%--34--%>
                                                                                    <asp:BoundColumn DataField="BT_FINALIZE" HeaderText="BT_FINALIZE" Visible="false"></asp:BoundColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxcontrol:TabPanel>
                                                    
                                                    
                                                    
                                                    </ajaxcontrol:TabContainer>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table id="tblTestInformation" runat="server" style="width: 100%" visible="false">
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    Test Information <a href="#anch_top">(Top)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    <asp:DataGrid ID="grdTestInformation" Width="100%" CssClass="GridTable" runat="Server"
                                                        AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="SPECIALITY" HeaderText="Test" ItemStyle-HorizontalAlign="Left">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DATE" HeaderText="TREATMENT DATE"    ItemStyle-HorizontalAlign="Center"
                                                                DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Treatment Name" ItemStyle-HorizontalAlign="Left">
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tblLastTreatment" runat="server" style="width: 100%; vertical-align: top;
                                            position: relative;" visible="false">
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    Last Treatment <a href="#anch_top">(Top)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    <asp:DataGrid ID="grdDoctorTreatment" Width="100%" CssClass="GridTable" runat="Server"
                                                        AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Last Treating Doctor">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Last Treatment Date"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Last Treatment" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tblAllTreatment" runat="server" style="width: 100%; vertical-align: top;
                                            position: relative;" visible="false">
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    All Treatments <a href="#anch_top">(Top)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    <asp:DataGrid ID="grdDoctorTreatmentList" Width="100%" CssClass="GridTable" runat="Server"
                                                        AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Treating Doctor">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDoctorName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="TREATMENT DATE"    HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Treatment Description"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tblBillingInformation" runat="server" style="width: 100%" visible="false">
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    Billing Information <a href="#anch_top">(Top)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative; height: 135px;">
                                                    <asp:DataGrid ID="grdBillingInformation" Width="100%" CssClass="GridTable" runat="Server"
                                                        AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="Total_Paid_Bills" HeaderText="No Of Paid Bills" ItemStyle-HorizontalAlign="right">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Total_Unpaid_Bills" HeaderText="No Of Unpaid Bills" ItemStyle-HorizontalAlign="right">
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <table id="tblRe_Schedule" runat="server" style="width: 100%" visible="false">
                                            <tr>
                                                <td style="float: left; vertical-align: top; position: relative;">
                                                    Re_Schedule Information <a href="#anch_top">(Top)</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" class="TDPart">
                                                    <asp:DataGrid ID="grdReSchedule" Width="100%" CssClass="GridTable" runat="Server"
                                                        AutoGenerateColumns="False">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                        <%--0--%>
                                                            <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="I_EVENT_ID" ItemStyle-HorizontalAlign="left" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%--1--%>
                                                            <asp:BoundColumn DataField="PROCEDURE CODE" HeaderText="PROCEDURE NAME" ItemStyle-HorizontalAlign="left">
                                                            </asp:BoundColumn>
                                                            <%--2--%>
                                                            <asp:BoundColumn DataField="VISIT DATE" HeaderText="VISIT DATE" ItemStyle-HorizontalAlign="left">
                                                            </asp:BoundColumn>
                                                            <%--3--%>
                                                             <asp:BoundColumn DataField="VISIT TIME" HeaderText="VISIT TIME" ItemStyle-HorizontalAlign="left">
                                                            </asp:BoundColumn>
                                                            <%--4--%>
                                                             <asp:BoundColumn DataField="RE-SCHEDULE DATE" HeaderText="RE-SCHEDULE DATE" ItemStyle-HorizontalAlign="left">
                                                            </asp:BoundColumn>
                                                            <%--5--%>
                                                              <asp:BoundColumn DataField="RE-SCHEDULE TIME" HeaderText="RE-SCHEDULE TIME" ItemStyle-HorizontalAlign="left">
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
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
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                    <tr>
                        <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
                            visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                            border-left: silver 1px solid; border-bottom: silver 1px solid; left: 160px;
                            top: -46px;" align="center">
                            <div style="position: relative; text-align: right; background-color: #8babe4;">
                                <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
                            </div>
                            <iframe id="framepatientDesk" src="" frameborder="0" height="470" width="850" visible="false">
                            </iframe>
                        </div>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <contenttemplate>
            <asp:Panel ID="pnlShowNotes" runat="server" Style="width: 420px; height: 220px; background-color: white;border-color: SteelBlue; border-width: 1px; border-style: solid;">
        
                        
                <iframe id="Iframe2" src="Bill_Sys_PopupNotes.aspx" frameborder="0" height="220px"
                    width="420px" visible="false"></iframe>
                    
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>

       

        <asp:UpdatePanel runat="server" ID="updatePnlSearch" UpdateMode="Conditional">
        <contenttemplate>
        <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 600px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 500px; background-color: #B5DF82" id="MpnlShowSearch" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
                </div>
               <iframe id="MiframeSearch" src="AJAX Pages/Bill_sys_new_VisitPopup.aspx"  frameborder="0" height="500px" width="600px" visible="false" >
            
            
            </iframe>  
               </div> 
            <asp:Panel ID="test" runat="server" Style="visibility:hidden;border-right: steelblue 1px solid; border-top: steelblue 1px solid;
        border-left: steelblue 1px solid; width: 400px; border-bottom: steelblue 1px solid;height: 400px; background-color: white;" >
                   
                     
              
            
                                       
         </asp:Panel>
         
        </contenttemplate>
    </asp:UpdatePanel>
   <ajaxcontrol:PopupControlExtender ID="MPopEx" runat="server" TargetControlID="A1"
    PopupControlID="MpnlShowSearch" Position="Left" OffsetX="-550"  />                                  
                
                <div id="divid" style="position: absolute; width: 500px; height: 350px; background-color: #DBE6FA;
        visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4; width: 500px">
            <a onclick="document.getElementById('divid').style.zIndex = '-1'; document.getElementById('divid').style.visibility='hidden';"
                style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="350" width="500"></iframe>
    </div>      
    
    <div id="Uploaddiv" style="position: absolute; width: 500px; height: 200px; background-color: white; visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4; width: 500px">
            <a onclick="document.getElementById('Uploaddiv').style.zIndex = '-1'; document.getElementById('Uploaddiv').style.visibility='hidden';"
                style="cursor: pointer;" title="Close">X</a>
        </div>&nbsp;&nbsp;&nbsp;&nbsp;
       <table cellpadding="1" cellspacing="0" align="center" style="width:100%; vertical-align:middle; font-size:small; ">
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="width:70%; color:Blue">
                        <asp:Label ID="Msglbl" runat="server" Font-Size="Small" Text="Select a File to Upload"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>
                    <td style="width:30%;" align="center">
                        Upload File:
                    </td>
                    <td style="width:70%" >
                        <asp:FileUpload  ID="ReportUpload" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
                <tr>                    
                    <td style="width:30%">
                        &nbsp;                    
                    </td>
                    <td style="width:70%" align="center">
                        <table cellpadding="0" cellspacing="0" style="width:100%">
                            <tr>
                                <td style="width:50%;" align="right">
                                    <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" />&nbsp;
                                    &nbsp;&nbsp;  
                                </td>
                                <td style="width:50%;" >
                                    <input type="button" value="Close" onclick="Close_UploadPanel();" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
               </tr>
               <tr>
                    <td colspan="2">
                        &nbsp;                    
                    </td>
                </tr>
        </table>
    </div>      
    
        
    <div id="div1" style="position: absolute; width: 740px; height: 600px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;width: 740px;">
            <a  onclick="document.getElementById('div1').style.visibility='hidden';document.getElementById('div1').style.zIndex = -1;" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameUpdateOutschedule" src="" frameborder="0" height="600px" width="740px"></iframe>
    </div>      
    
    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 150px; background-color: #B5DF82" id="divfrmPatient" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
                </div>
                <iframe id="frmpatient" src="" frameborder="0" width="500" height="150"></iframe>
            </div>   
            
            
             <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height:300px; background-color: #B5DF82" id="divView" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="CloseViewFramePopup();">X</a>
                </div>
                <iframe id="frmView" src="" frameborder="0" width="500" height="300"></iframe>
            </div>    
            
            <asp:HiddenField ID="tabid" runat="server" />  
             <asp:HiddenField  ID="hdnCaseId" runat="server" />
              <asp:HiddenField  ID="hdnCaseNo" runat="server" />
               <asp:HiddenField  ID="hdn_TestFacility" runat="server" />
    <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
</div>          
</asp:Content>
