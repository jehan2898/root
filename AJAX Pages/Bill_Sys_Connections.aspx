<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_Connections.aspx.cs" Inherits="Bill_Sys_Connections" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function confirm_Copy() {
            var f = document.getElementById("<%=grid.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('ChkCpy') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;
                        }
                    }
                }
            }
            if (bfFlag == false) {
                alert('Select atleast 1 patient to copy');
                return false;
            }
        }

        function OpenDocManager(CaseNo, CaseId) {

            window.open('../Document Manager/case/vb_CaseInformation.aspx?menu=false&caseid=' + CaseId + '&caseno=' + CaseNo, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
        }

        function showCopyDocPopup(caseid) {
            var e = document.getElementById("ctl00_ContentPlaceHolder1_extddlBillCompany");
            var companyid = e.value;
            var url = 'Bill_Sys_Copy_Documents.aspx?CompanyID=' + companyid + '&CaseID=' + caseid;
            CopyDocumentPopup.SetContentUrl(url);
            CopyDocumentPopup.Show();
            return false;
        }


        function ClosePopup() {
            CopyDocumentPopup.Hide();
            var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
        }
        function ShowUserPreferences() {
            var url = "PatientCopyPreferences.aspx";
            IFrame_UserPreferences.SetContentUrl(url);
            IFrame_UserPreferences.Show();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="1" cellpadding="0" cellspacing="0" style="width: 100%;
        background-color: White;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr valign="middle">
                        <asp:UpdatePanel ID="UpdatePanelFacility111" runat="server">
                            <ContentTemplate>
                                <td style="font-family: Verdana; font-size: 12px; text-align: center;" align="center">
                                    <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                                </td>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:TextBox runat="server" ID="txtCompanyId" Visible="false"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtcaseId" Visible="false"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtLoginCompanyId" Visible="false"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtProcCodeId" Visible="false"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtProcGroupId" Visible="false"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtLocationId" Visible="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnCompanyid" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: White;" align="right" valign="top">
                            <a style="vertical-align: top; font-family: Calibri; font-size: 12px;" href="javascript:void(0);"
                                onclick="ShowUserPreferences()">[My Preferences]</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--start updatepanel--%>
                            <asp:UpdatePanel ID="UpdatePanelFacility" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; width: 100%">
                                                <table border="0" style="width: 100%">
                                                    <tr>
                                                        <td style="width: 80%">
                                                            <asp:Panel runat="server" ID="pnlFacility">
                                                                Select facility
                                                                <extddl:ExtendedDropDownList ID="extddlBillCompany" runat="server" Width="280px"
                                                                    Connection_Key="Connection_String" Flag_Key_Value="flag" Procedure_Name="SP_GET_COMPANY_CONNECTIONS"
                                                                    Selected_Text="---Select---" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td id="lnkmissingProcode" runat="server" style="width: 50%">
                                                            <a id="A1" style="text-decoration: none; font-family: Verdana; font-size: 12px;"
                                                                href="Bill_Sys_ConnectionProcedures.aspx" runat="server">Missing procedure codes</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="30%" border="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btnSave" Text="Search" runat="server" Width="19%" OnClick="btnSearch_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 35px" align="center">
                                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanelFacility"
                                                                            DisplayAfter="10" DynamicLayout="true">
                                                                            <ProgressTemplate>
                                                                                <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                    runat="Server">
                                                                                    <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                    Loading...
                                                                                </div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--end updatepanel--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelSrch" runat="server">
                                <ContentTemplate>
                                    <table style="vertical-align: middle">
                                        <tbody>
                                            <tr>
                                                <asp:Panel runat="server" ID="pnlSrch" Visible="false">
                                                    <td style="width: 40%; text-align: left">
                                                        Search:
                                                        <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                                            AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td style="width: auto; text-align: right;">
                                                        Record Count:
                                                        <%= this.grid.RecordCount %>
                                                        |
                                                    </td>
                                                    <td style="vertical-align: middle; width: auto; text-align: right">
                                                        Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                            Text="Export TO Excel">
                                                        <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                    </td>
                                                </asp:Panel>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <%--Patient Grid--%>
                                            <td style="vertical-align: top;">
                                                <asp:Panel runat="server" Width="600px">
                                                    <xgrid:XGridViewControl ID="grid" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                        AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="10" XGridKey="PatientInfo"
                                                        AllowPaging="true" ExportToExcelColumnNames="Case #,Patient Name,Date Of Birth, Date Of Accident"
                                                        ShowExcelTableBorder="true" ExportToExcelFields="SZ_CASE_NO,Patient Name,DT_DOB,DT_DATE_OF_ACCIDENT"
                                                        AlternatingRowStyle-BackColor="#EEEEEE" HeaderStyle-CssClass="GridViewHeader"
                                                        ContextMenuID="ContextMenu1" EnableRowClick="false" MouseOverColor="0, 153, 153"
                                                        DataKeyNames="SZ_CASE_ID,sz_patient_id,sz_copied_from_patient,SZ_CASE_NO" OnRowCommand="grid_RowCommand">
                                                        <Columns>
                                                            <%--  0 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Case Id" DataField="SZ_CASE_ID" Visible="false" />
                                                            <%--  1 --%>
                                                            <asp:TemplateField HeaderText="Case #" Visible="true" SortExpression="cast(MST_CASE_MASTER.SZ_CASE_NO as int)">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkcaseno" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                        CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="CaseNO"
                                                                        Visible="true"> 
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:TemplateField>
                                                            <%--  2 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                ItemStyle-Width="100%" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME  + ' '  + MST_PATIENT.SZ_PATIENT_LAST_NAME"
                                                                HeaderText="Patient" DataField="Patient Name" />
                                                            <%--  3 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                ItemStyle-Width="40px" SortExpression="MST_PATIENT.DT_DOB" HeaderText="Date Of Birth"
                                                                DataField="DT_DOB" DataFormatString="{0:MM/dd/yyyy}" />
                                                            <%--  4 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                SortExpression="MST_LOCATION.SZ_LOCATION_NAME" HeaderText="Location" DataField="SZ_LOCATION_NAME" />
                                                            <%--  5 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                ItemStyle-Width="40px" SortExpression="MST_CASE_MASTER.DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident"
                                                                DataField="DT_DATE_OF_ACCIDENT" DataFormatString="{0:MM/dd/yyyy}" />
                                                            <%--  6--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                Visible="false" HeaderText="Case ID" DataField="SZ_CASE_ID" />
                                                            <%--  7 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                Visible="false" HeaderText="Company ID" DataField="SZ_COMPANY_ID" />
                                                            <%--  8 --%>
                                                            <asp:TemplateField HeaderText="Copy" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkCpy" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--  9 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                Visible="false" HeaderText="Patient" DataField="sz_patient_id" />
                                                            <%--  10 --%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                Visible="false" HeaderText="Copied" DataField="sz_copied_from_patient" />
                                                            <asp:TemplateField HeaderText="Docs">
                                                                <ItemTemplate>
                                                                    <%--<img alt="" onclick="javascript:OpenDocManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ');" title="Document Manager"  src="Images/grid-doc-mng.gif" style="border:none;cursor:pointer;"/>--%>
                                                                    <asp:ImageButton ID="ImageButton2" runat="Server" ImageUrl="Images/grid-doc-mng.gif"
                                                                        CommandName="ClientSideButton" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Copy Documents" Visible="true">
                                                                <ItemTemplate>
                                                                    <a id="aCpy" target="_self" visible="false" style="text-decoration: underline; cursor: hand;"
                                                                        runat="server" onclick='<%# "showCopyDocPopup(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ");" %>'>
                                                                        Copy Documents</a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </xgrid:XGridViewControl>
                                                    <div style="text-align: center; vertical-align: text-top; vertical-align: top;">
                                                        <asp:Button Visible="false" ID="btnCpy" runat="server" Text="Copy Patient(s)" OnClick="btnCpy_Click" />
                                                    </div>
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanelSrch"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px;
                                                                                left: 600px" runat="Server">
                                                                                <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                            <%--End of Patient Grid--%>
                                            <%-- Visit Grid--%>
                                            <td runat="server" id="tdvisit" style="width: 60%" valign="top">
                                                <div id="dvvisit" style="height: 250px; width: 80%; overflow: scroll;">
                                                    <%--    <div style="width:500px; overflow:auto; position:relative; height:200px;> --%>
                                                    <asp:Panel ID="Panel1" runat="server" Height="200px" Width="495px">
                                                        <xgrid:XGridViewControl ID="VisitGrid" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                            AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="10" XGridKey="VisitStatus"
                                                            AllowPaging="true" ExportToExcelColumnNames="Case #,Patient Name, Date Of Accident,Case Type,Date Open,Medical Facility"
                                                            ShowExcelTableBorder="true" ExportToExcelFields="SZ_CASE_NO,Patient Name,DT_DATE_OF_ACCIDENT,Case Type,OPEN_DATE,SZ_COMPANY_NAME"
                                                            AlternatingRowStyle-BackColor="#EEEEEE" HeaderStyle-CssClass="GridViewHeader"
                                                            ContextMenuID="ContextMenu1" EnableRowClick="false" MouseOverColor="0, 153, 153"
                                                            OnRowCommand="grid_RowCommand">
                                                            <Columns>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                    Visible="false" ItemStyle-Width="100%" SortExpression="I_EVENT_ID" HeaderText="I_EVENT_ID"
                                                                    DataField="I_EVENT_ID" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                    ItemStyle-Width="40px" SortExpression="TXN_CALENDAR_EVENT.DT_EVENT_DATE" HeaderText="Appointment Date"
                                                                    DataField="DT_EVENT_DATE" DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                    Visible="true" HeaderText="Office" DataField="SZ_OFFICE" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                    Visible="false" HeaderText="Company ID" DataField="SZ_COMPANY_ID" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                    Visible="true" HeaderText="doctor" DataField="SZ_DOCTOR_NAME" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                    Visible="true" HeaderText="Specialty" DataField="SZ_PROCEDURE_GROUP" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                    Visible="false" HeaderText="BT_STATUS" DataField="BT_STATUS" />
                                                                <asp:TemplateField HeaderText="Visit Type" Visible="true">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container,"DataItem.VISIT_TYPE")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Treatments" Visible="true">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_PROC_CODES")%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField>
                                                                <%--  <asp:BoundField 
                                                                    HeaderStyle-HorizontalAlign="left" 
                                                                    ItemStyle-HorizontalAlign="left"
                                                                    visible="true" 
                                                                    headertext=" Visit Type" 
                                                                    DataField="VISIT_TYPE" />--%>
                                                            </Columns>
                                                        </xgrid:XGridViewControl>
                                                    </asp:Panel>
                                                </div>
                                                <%--</div>--%>
                                            </td>
                                            <%--End Of Visit Grid--%>
                                        </tr>
                                    </table>
                                    <%--tables of widget page --%>
                                    <div runat="server" id="dvabc">
                                        <asp:DataList ID="DtlView" runat="server" BorderStyle="None" BorderColor="#DEBA84"
                                            RepeatColumns="1">
                                            <ItemTemplate>
                                                <table class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="td-widget-lf-top-holder-division-ch">
                                                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                                                border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                        &nbsp;<b class="txt3">Personal Information</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 446px;">
                                                                        <!-- outer table to hold 2 child tables -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-widget-lf-holder-ch">
                                                                                    <!-- Table 1 - to hold the actual data -->
                                                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                First Name
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Middle Name
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Last Name
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                D.O.B
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "DOB") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Gender
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem,"SZ_GENDER") %>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Address
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                City
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                State
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_STATE")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Home Phone
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Work
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                ZIP
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                SSN #
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Email
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Extn.
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Attorney
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ATTORNEY")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Case Type
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_TYPE")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Case Status
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_STATUS")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
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
                                                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                                                border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                        &nbsp;<b class="txt3">Insurance Information</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 446px;">
                                                                        <!-- outer table to hold 2 child tables -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-widget-lf-holder-ch">
                                                                                    <!-- Table 1 - to hold the actual data -->
                                                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Policy Holder
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem,"SZ_POLICY_HOLDER") %>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Name
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Ins. Address
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                -
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Address
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ADDRESS") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                City
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_CITY") %>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                State
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_STATE") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                ZIP
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ZIP") %>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Phone
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_PHONE")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                                                                FAX
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch" style="height: 33px">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_FAX_NUMBER")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                                                                Contact Person
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch" style="height: 33px">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CONTACT_PERSON")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Claim File#
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Policy #
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%>
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
                                                        <td class="td-widget-lf-division-rightmost">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-lf-top-holder-division-ch">
                                                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                                                border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                        &nbsp;<b class="txt3">Accident Information</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 446px;">
                                                                        <!-- outer table to hold 2 child tables -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-widget-lf-holder-ch">
                                                                                    <!-- Table 1 - to hold the actual data -->
                                                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Accident Date
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Plate Number
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Report Number
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Address
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_ADDRESS")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                City
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_CITY")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                State
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_INSURANCE_STATE")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Hospital Name
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_NAME")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Hospital Address
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_ADDRESS")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Date Of Admission
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "DT_ADMISSION_DATE")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Additional Patient
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FROM_CAR")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Describe Injury
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_DESCRIBE_INJURY")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Patient Type
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_TYPE")%>
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
                                                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                                                border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                        &nbsp;<b class="txt3">Employer Information</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 446px;">
                                                                        <!-- outer table to hold 2 child tables -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-widget-lf-holder-ch">
                                                                                    <!-- Table 1 - to hold the actual data -->
                                                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Name
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_NAME")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Address
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ADDRESS")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                City
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_CITY")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                State
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_STATE")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                ZIP
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ZIP")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Phone
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_PHONE")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Date Of First Treatment
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "DT_FIRST_TREATMENT")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Chart No.
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
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
                                                        <td class="td-widget-lf-division-rightmost">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-lf-top-holder-division-ch">
                                                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                                                border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                        &nbsp;<b class="txt3">Adjuster Information</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 446px;">
                                                                        <!-- outer table to hold 2 child tables -->
                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td class="td-widget-lf-holder-ch">
                                                                                    <!-- Table 1 - to hold the actual data -->
                                                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Name
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Phone
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PHONE")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Extension
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EXTENSION")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                FAX
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
                                                                                                &nbsp;
                                                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_FAX")%>
                                                                                            </td>
                                                                                            <td class="td-widget-lf-desc-ch">
                                                                                                Email
                                                                                            </td>
                                                                                            <td class="td-widget-lf-data-ch">
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
                                                        <td class="td-widget-lf-top-holder-division-ch">
                                                            &nbsp;
                                                        </td>
                                                        <td class="td-widget-lf-division-rightmost">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <%--end of tables of widget page --%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="visibility: hidden;">
        <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="txtUpdate_Click" /></div>
    <dx:ASPxPopupControl ID="CopyDocumentPopup" runat="server" CloseAction="CloseButton"
        HeaderText="Copy Documents" CloseButtonImage-ToolTip="Close" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ClientInstanceName="CopyDocumentPopup" HeaderStyle-HorizontalAlign="center"
        HeaderStyle-Font-Bold="true" HeaderStyle-BorderTop-BorderColor="DarkGreen" HeaderStyle-ForeColor="Red"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="900px"
        ToolTip="Copy Document" PopupHorizontalOffset="0" PopupVerticalOffset="0"  
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <ClientSideEvents CloseButtonClick="ClosePopup" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
        <HeaderStyle BackColor="#B5DF82"></HeaderStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="IFrame_UserPreferences" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="IFrame_UserPreferences" HeaderText="Copy Patients - My Preferences"
        HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="800px"
        ToolTip="Copy Patient - My Preferences" PopupHorizontalOffset="0" PopupVerticalOffset="0"
          AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="250px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
