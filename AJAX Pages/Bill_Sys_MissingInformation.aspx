<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_MissingInformation.aspx.cs" Inherits="Bill_Sys_MissingInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"  TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
    <%-- <script type="text/javascript" src="validation.js"></script>--%>
    <style type="text/css">
.menutxt1 {
	font-size: 12px;
    color: #ffffff;
    font-family: arial;
    background-color: #5998C9;
    border: 0px solid #000099;
    padding-top:2px;
    padding-bottom:0px;
    padding-left:0px;
    padding-right:0px;
}

.BackColorTab
        {
            color:#5998C9;
            background-color:white;
            border: 1px solid #808080;
        }
</style>

    <script type="text/javascript">

        function CheckBoxClick() {
            alert('Please Select Atleast One Case');
            return false;
        }


        function ShowChildGrid(obj) {
            var div = document.getElementById(obj);
            alert('hi');

            div.style.display = 'block';


        }
        //ashutosh
        function HideChildGrid(obj) {
            alert('hide');
            var div = document.getElementById(obj);
            div.style.display = 'none';
        }

        function CloseUploadFilePopup() {

        }
        // 21 April 2010 show ReceiveReport popup -- sachin
        function showReceiveDocumentPopup() {

            document.getElementById('divid').style.zIndex = 1;
            document.getElementById('divid').style.position = 'absolute';
            document.getElementById('divid').style.left = '300px';
            document.getElementById('divid').style.top = '100px';
            document.getElementById('divid').style.visibility = 'visible';
            document.getElementById('frameeditexpanse').src = 'Bill_Sys_ReceivedDocumentPopupPage.aspx';
            return false;

        }
        function ShowDiv() {
            document.getElementById('divDashBoard').style.position = 'absolute';
            document.getElementById('divDashBoard').style.left = '200px';
            document.getElementById('divDashBoard').style.top = '120px';
            document.getElementById('divDashBoard').style.visibility = 'visible';
            return false;
        }
    </script>

    <table border="1" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; background-color: White;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: White;"
                    border="0">
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 1041px">
                                        <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; height: 25px;" colspan="4">
                                                    <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server">Dash board</a>
                                                </td>
                                                <td style="width: 7px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <asp:UpdatePanel ID="Updatep1" runat="server">
                                                        <contenttemplate>
                                                            <asp:Label ID="lblHeader" runat="server" Style="font-size: small;"></asp:Label>
                                                        </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px; width: 100%;">
                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                        <contenttemplate>
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                                <ProgressTemplate>
                                                                    <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                        runat="Server">
                                                                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                            Height="25px" Width="24px"></asp:Image>
                                                                        Loading...</div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </contenttemplate>
                                                    </asp:UpdatePanel>
                                                    <%--     <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                        Style="float: right;" OnClick="btnExportToExcel_Click" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 99%" align="right">
                                                    <asp:Button ID="btnnext" runat="server" Style="float: left;" CssClass="Buttons" Text="Received Document"
                                                         Width="125px" Visible="False" />
                                                    <asp:Button ID="btnSpeciality" runat="server" Text="Export To Excel"  
                                                        Width="0px" Height="0px" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 99%" class="SectionDevider">
                                                    <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True"
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 99%">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <contenttemplate>
                                                            <ajaxToolkit:TabContainer ID="tabcontainerMissingInformation" runat="Server" ActiveTabIndex="1"   CssClass="BackColorTab" OnActiveTabChanged="tab_changedMissingInformation" AutoPostBack="True">
                                                                <ajaxToolkit:TabPanel runat="server" ID="tabPanel2">
                                                                    <HeaderTemplate> 
                                                                        <div style="height: 20px; width: 150px; text-align:center;vertical-align:bottom;"  class="menutxt1">
                                                                            Insurance Company
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <table style="border: 1px; width: 100%;">
                                                                            <tr>
                                                                                <td style="color:Black;">
                                                                                   <%-- Search:
                                                                                    <gridsearch:XGridSearchTextBox ID="XGridSearchTextBox2" runat="server" CssClass="search-input"
                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>--%>
                                                                                </td>
                                                                                <td style="vertical-align: right; width: auto; text-align: right;color:Black;">
                                                                                    Record Count:
                                                                                    <%= this.grdCaseMaster.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="XGridPaginationDropDown2" runat="server" OnSelectedIndexChanged="grdCaseMaster_SelectedIndexChanged" SelectedIndexproperty="true">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel2" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcelMissingInsuranceCompany_onclick"> 
                                                                             <img src="Images/Excel.jpg" 
                                                                                 style="border:none;"  
                                                                                 height="15px" 
                                                                                 width ="15px" 
                                                                                 title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <xgrid:XGridViewControl ID="grdCaseMaster" runat="server" Width="100%" CssClass="mGrid"
                                                                            OnRowCommand="grdCaseMaster_OnRowCommand" DataKeyNames="SZ_PATIENT_NAME" MouseOverColor="0, 153, 153"
                                                                            EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                            AlternatingRowStyle-BackColor="#EEEEEE" 
                                                                            ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_CASE_TYPE_NAME,SZ_OFFICE_NAME,SZ_DOCTOR_NAME,DT_DATE_OF_ACCIDENT"
                                                                            ShowExcelTableBorder="true" 
                                                                            ExportToExcelColumnNames="Case #,Patient Name,Case Type,OFFICE,DOCTOR,Date Of Accident"
                                                                            AllowPaging="true" XGridKey="MissingInsuranceCompany" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                            AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                                            <Columns>
                                                                                <%--1--%>
                                                                                <asp:TemplateField HeaderText="Case #" Visible="false">
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--2--%>
                                                                                <asp:TemplateField HeaderText="Case #" ItemStyle-Width="80px" >
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>' visible="false"
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Select"></asp:LinkButton>
                                                                                
                                                                                <a href="Bill_Sys_CaseDetails.aspx?Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>       

                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--3--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="false"></asp:BoundField>
                                                                                <%--4--%>
                                                                                <asp:TemplateField HeaderText="Patient" Visible="false">
                                                                                    <itemtemplate>
                                                                            <a href="PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                                                target="_blank">                                                                                 
                                                                            </a>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--5--%>
                                                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ItemStyle-Width="200px"  ></asp:BoundField>
                                                                                 
                                                                                <%--6--%>
                                                                                <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-Width="170px"></asp:BoundField>
                                                                                <%--7--%>
                                                                                <asp:BoundField DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundField>
                                                                                <%--8--%>
                                                                                <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundField>
                                                                                <%--9--%>
                                                                                <asp:BoundField DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}" ItemStyle-Width="130px"  >
                                                                                </asp:BoundField>

                                                                                <%--10--%>
                                                                                <asp:TemplateField HeaderText="Desk" ItemStyle-Width="80px">
                                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Patient Desk" visible="false"
                                                                                ToolTip="Patient Desk">																		                
                                                                            </asp:LinkButton>
                                                                             <a href="../Bill_SysPatientDesk.aspx?Flag=true&Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/></a>  
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--11--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundField>
                                                                            </Columns>
                                                                        </xgrid:XGridViewControl>
                                                                    </ContentTemplate>
                                                                </ajaxToolkit:TabPanel>
                                                                <%--Search Bill--%>
                                                                <ajaxToolkit:TabPanel runat="server" ID="tabPanel3">
                                                                    <HeaderTemplate>
                                                                        <div style="height:  20px; width: 150px; text-align: center;" class="menutxt1">
                                                                            Attorney
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <table style="border: 1px; width: 100%;">
                                                                            <tr>
                                                                                <td style="color:Black;">
                                                                                   <%-- Search:
                                                                                    <gridsearch:XGridSearchTextBox ID="XGridSearchTextBox3" runat="server" CssClass="search-input"
                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>--%>
                                                                                </td>
                                                                                <td style="vertical-align: right; width: auto; text-align: right;color:Black;">
                                                                                    Record Count:
                                                                                    <%= this.grdMissingAttorney.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown SelectedIndexproperty="true" OnSelectedIndexChanged="grdMissingAttorney_SelectedIndexChanged" ID="XGridPaginationDropDown3" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel5" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcelgrdMissingAttorney_onclick"> 
                                                                             <img src="Images/Excel.jpg" 
                                                                                 style="border:none;"  
                                                                                 height="15px" 
                                                                                 width ="15px" 
                                                                                 title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <xgrid:XGridViewControl ID="grdMissingAttorney" runat="server" Width="100%" CssClass="mGrid"
                                                                            OnRowCommand="grdMissingAttorney_OnRowCommand" DataKeyNames="SZ_PATIENT_NAME" MouseOverColor="0, 153, 153"
                                                                            EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                            AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_CASE_TYPE_NAME,SZ_OFFICE_NAME,SZ_DOCTOR_NAME,DT_DATE_OF_ACCIDENT"
                                                                            ShowExcelTableBorder="true" ExportToExcelColumnNames="Case #,Patient Name,Case Type,OFFICE,DOCTOR,Date Of Accident"
                                                                            AllowPaging="true" XGridKey="MissingAttorney" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                            AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                                            <Columns>
                                                                                <%--1--%>
                                                                                <asp:TemplateField HeaderText="Case #" Visible="false">
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--2--%>
                                                                                <asp:TemplateField HeaderText="Case #" ItemStyle-Width="80px"  >
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>' visible="false"
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Select"></asp:LinkButton>
                                                                                
                                                                                <a href="Bill_Sys_CaseDetails.aspx?Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>       

                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--3--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="false"></asp:BoundField>
                                                                                <%--4--%>
                                                                                <asp:TemplateField HeaderText="Patient" Visible="false">
                                                                                    <itemtemplate>
                                                                            <a href="PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                                                target="_blank">                                                                                 
                                                                            </a>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--5--%>
                                                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ItemStyle-Width="200px" ></asp:BoundField>
                                                                                 
                                                                                <%--6--%>
                                                                                <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-Width="170px"></asp:BoundField>
                                                                                <%--7--%>
                                                                                <asp:BoundField DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundField>
                                                                                <%--8--%>
                                                                                <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundField>
                                                                                <%--9--%>
                                                                                <asp:BoundField DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}" ItemStyle-Width="130px"    >
                                                                                </asp:BoundField>

                                                                                <%--10--%>
                                                                                <asp:TemplateField HeaderText="Desk" ItemStyle-Width="80px">
                                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Patient Desk" visible="false"
                                                                                ToolTip="Patient Desk">																		                
                                                                            </asp:LinkButton>
                                                                             <a href="../Bill_SysPatientDesk.aspx?Flag=true&Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/></a>  
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--11--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundField>
                                                                            </Columns>
                                                                        </xgrid:XGridViewControl>
                                                                    </ContentTemplate>
                                                                </ajaxToolkit:TabPanel>
                                                                <%--End--%>
                                                                <%--Grid used for Export to excel--%>
                                                                <ajaxToolkit:TabPanel runat="server" ID="tabPanel4">
                                                                    <HeaderTemplate>
                                                                        <div style="height: 20px; width: 150px; text-align: center;" class="menutxt1">
                                                                            Claim Number
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <table style="border: 1px; width: 100%;">
                                                                            <tr>
                                                                                <td style="color:Black;">
                                                                                  <%--  Search:
                                                                                    <gridsearch:XGridSearchTextBox ID="XGridSearchTextBox4" runat="server" CssClass="search-input"
                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>--%>
                                                                                </td>
                                                                                <td style="vertical-align: right; width: auto; text-align: right;color:Black;">
                                                                                    Record Count:
                                                                                    <%= this.grdMissingClaimNo.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="XGridPaginationDropDown4" runat="server" OnSelectedIndexChanged="grdMissingClaimNo_SelectedIndexChanged" SelectedIndexproperty="true">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel6" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcelgrdgrdMissingClaimNo_onclick"> 
                                                                             <img src="Images/Excel.jpg" 
                                                                                 style="border:none;"  
                                                                                 height="15px" 
                                                                                 width ="15px" 
                                                                                 title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <xgrid:XGridViewControl ID="grdMissingClaimNo" runat="server" Width="100%" CssClass="mGrid"
                                                                            OnRowCommand="grdMissingClaimNo_OnRowCommand" DataKeyNames="SZ_PATIENT_NAME" MouseOverColor="0, 153, 153"
                                                                            EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                            AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_CASE_TYPE_NAME,SZ_INSURANCE_NAME,SZ_OFFICE_NAME,SZ_DOCTOR_NAME,DT_DATE_OF_ACCIDENT"
                                                                            ShowExcelTableBorder="true" ExportToExcelColumnNames="Case #,Patient Name,Case Type,Insurance Company,OFFICE,DOCTOR,Date Of Accident"
                                                                            AllowPaging="true" XGridKey="MissingClaimNo" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                            AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                                            <Columns>
                                                                                <%--1--%>
                                                                                <asp:TemplateField HeaderText="Case #" Visible="false">
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--2--%>
                                                                                <asp:TemplateField HeaderText="Case #" ItemStyle-Width="80px"  >
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>' visible="false"
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Select"></asp:LinkButton>
                                                                                
                                                                                <a href="Bill_Sys_CaseDetails.aspx?Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>       

                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--3--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="false"></asp:BoundField>
                                                                                <%--4--%>
                                                                                <asp:TemplateField HeaderText="Patient" Visible="false">
                                                                                    <itemtemplate>
                                                                            <a href="PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                                                target="_blank"></a>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--5--%>
                                                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ItemStyle-Width="200px" ></asp:BoundField>
                                                                                <%--6--%> 
                                                                                 <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" Visible="false"  ></asp:BoundField>                                                                                 
                                                                                <%--7--%>
                                                                                <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-Width="170px"></asp:BoundField>
                                                                                <%--8--%>
                                                                                <asp:BoundField DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundField>
                                                                                <%--9--%>
                                                                                <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundField>
                                                                                <%--10--%>
                                                                                <asp:BoundField DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}" ItemStyle-Width="130px"  >
                                                                                </asp:BoundField>
                                                                                <%--11--%>
                                                                                <asp:TemplateField HeaderText="Desk" ItemStyle-Width="80px">
                                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Patient Desk" visible="false"
                                                                                ToolTip="Patient Desk">																		                
                                                                            </asp:LinkButton>
                                                                             <a href="../Bill_SysPatientDesk.aspx?Flag=true&Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/></a>  
                                                                                    </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--12--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundField>
                                                                            </Columns>
                                                                        </xgrid:XGridViewControl>
                                                                    </ContentTemplate>
                                                                </ajaxToolkit:TabPanel>
                                                                <%--End--%>
                                                                <ajaxToolkit:TabPanel runat="server" ID="tabPanel5" >
                                                                    <HeaderTemplate>
                                                                        <div style="height: 20px; width: 180px; text-align: center;" class="menutxt1">
                                                                            Report Number
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="color:Black;">
                                                                                    <%--Search:
                                                                                    <gridsearch:XGridSearchTextBox ID="XGridSearchTextBox1" runat="server" CssClass="search-input"
                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>--%>
                                                                                </td>
                                                                                <td style="vertical-align: right; width: auto; text-align: right;color:Black;">
                                                                                    Record Count:
                                                                                    <%= this.grdMissingReportNumber.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="XGridPaginationDropDown1" runat="server" OnSelectedIndexChanged="grdMissingReportNumber_SelectedIndexChanged" SelectedIndexproperty="true">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel1" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcelMissingReportNumber_onclick"> 
                                                                                    <img src="Images/Excel.jpg" 
                                                                                         style="border:none;"  
                                                                                         height="15px" 
                                                                                         width ="15px" 
                                                                                         title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                               <xgrid:XGridViewControl ID="grdMissingReportNumber" runat="server" Width="100%" CssClass="mGrid" DataKeyNames="SZ_PATIENT_NAME"
                                                                            MouseOverColor="0, 153, 153" OnRowCommand="grdMissingReportNumber_OnRowCommand" EnableRowClick="false"
                                                                            ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                            ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_CASE_TYPE_NAME,DT_DATE_OF_ACCIDENT"
                                                                            ShowExcelTableBorder="true"  ExportToExcelColumnNames="Case #,Patient Name,Case Type,Date Of Accident"
                                                                            AllowPaging="true" XGridKey="MissingReport" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                            AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                                            <Columns>
                                                                                <%--1--%>
                                                                                <asp:TemplateField HeaderText="Case #" Visible="false">
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--2--%>
                                                                                <asp:TemplateField HeaderText="Case #"  >
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument= '<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Select" visible="false"></asp:LinkButton>
                                                                          <a href="Bill_Sys_CaseDetails.aspx?Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>       
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--3--%>
                                                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"  ></asp:BoundField>
                                                                                <%--4--%>
                                                                                <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundField>
                                                                                <%--5--%>
                                                                                <asp:BoundField DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundField>
                                                                                <%--6--%>
                                                                                <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundField>
                                                                                <%--7--%>
                                                                                <asp:BoundField DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}"  >
                                                                                </asp:BoundField>
                                                                                <%--8--%>
                                                                                <asp:TemplateField HeaderText="Desk">
                                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Patient Desk"
                                                                                ToolTip="Patient Desk" visible="false">
                                                                                </asp:LinkButton>
                                                                                <a href="../Bill_SysPatientDesk.aspx?Flag=true&Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/></a>       
																		    </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--9--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundField>
                                                                            </Columns>
                                                                        </xgrid:XGridViewControl>
                                                                    </ContentTemplate>
                                                                </ajaxToolkit:TabPanel>
                                                                <ajaxToolkit:TabPanel runat="server" ID="tabPanel6">
                                                                    <HeaderTemplate>
                                                                        <div style="height: 20px; width: 170px; text-align: center;" class="menutxt1">
                                                                            Policy Holder
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <table style="border: 1px; width: 100%;">
                                                                            <tr>
                                                                                <td style="color:Black;">
                                                                                    <%--Search:
                                                                                    <gridsearch:XGridSearchTextBox ID="XGridSearchTextBox5" runat="server" CssClass="search-input"
                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>--%>
                                                                                </td>
                                                                                <td style="vertical-align: right; width: auto; text-align: right;color:Black;">
                                                                                    Record Count:
                                                                                    <%= this.grdMissingPolicyholder.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="XGridPaginationDropDown5" runat="server" OnSelectedIndexChanged="grdMissingPolicyholder_SelectedIndexChanged" SelectedIndexproperty="true">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel8" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcelgrdMissingPolicyholder_onclick"> 
                                                                             <img src="Images/Excel.jpg" 
                                                                                 style="border:none;"  
                                                                                 height="15px" 
                                                                                 width ="15px" 
                                                                                 title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <xgrid:XGridViewControl ID="grdMissingPolicyholder" runat="server" Width="100%" CssClass="mGrid"
                                                                            OnRowCommand="grdMissingPolicyholder_OnRowCommand" DataKeyNames="SZ_PATIENT_NAME" MouseOverColor="0, 153, 153"
                                                                            EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                            AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_CASE_TYPE_NAME,SZ_OFFICE_NAME,SZ_DOCTOR_NAME,DT_DATE_OF_ACCIDENT"
                                                                            ShowExcelTableBorder="true" ExportToExcelColumnNames="Case #,Patient Name,Case Type,OFFICE,DOCTOR,Date Of Accident"
                                                                            AllowPaging="true" XGridKey="MissingPolicyholder" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                            AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                                             <Columns>
                                                                                <%--1--%>
                                                                                <asp:TemplateField HeaderText="Case #" Visible="false">
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--2--%>
                                                                                <asp:TemplateField HeaderText="Case #" ItemStyle-Width="80px"  >
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>' visible="false"
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Select"></asp:LinkButton>
                                                                                
                                                                                <a href="Bill_Sys_CaseDetails.aspx?Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>       

                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--3--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="false"></asp:BoundField>
                                                                                <%--4--%>
                                                                                <asp:TemplateField HeaderText="Patient" Visible="false">
                                                                                    <itemtemplate>
                                                                            <a href="PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>"
                                                                                target="_blank">                                                                                 
                                                                            </a>
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--5--%>
                                                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ItemStyle-Width="200px"  ></asp:BoundField>
                                                                                 
                                                                                <%--6--%>
                                                                                <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-Width="170px"></asp:BoundField>
                                                                                <%--7--%>
                                                                                <asp:BoundField DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundField>
                                                                                <%--8--%>
                                                                                <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundField>
                                                                                <%--9--%>
                                                                                <asp:BoundField DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}" ItemStyle-Width="130px"  >
                                                                                </asp:BoundField>

                                                                                <%--10--%>
                                                                                <asp:TemplateField HeaderText="Desk" ItemStyle-Width="80px">
                                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((GridViewRow) Container).RowIndex %>' CommandName="Patient Desk" visible="false"
                                                                                ToolTip="Patient Desk">																		                
                                                                            </asp:LinkButton>
                                                                             <a href="../Bill_SysPatientDesk.aspx?Flag=true&Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/></a>  
                                                                        </itemtemplate>
                                                                                </asp:TemplateField>
                                                                                <%--11--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundField>
                                                                            </Columns>
                                                                        </xgrid:XGridViewControl>
                                                                    </ContentTemplate>
                                                                </ajaxToolkit:TabPanel>
                                                                <ajaxToolkit:TabPanel runat="server" ID="tabPanel7">
                                                                    <HeaderTemplate>
                                                                        <div style="height: 20px; width: 100px; text-align: center;" class="menutxt1">
                                                                            Unsent NF2
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td style="width: 100%;color:Black;" align="left">
                                                                                    Status<asp:DropDownList ID="ddlStatus" runat="server" Width="88px">
                                                                                        <asp:ListItem Value="0" Text="Un-Sent"></asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="Sent"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                                                        Visible="False" Width="94px"></asp:Label>&nbsp;
                                                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="39%" Connection_Key="Connection_String"
                                                                                        Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---"
                                                                                        Visible="false" />
                                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px"  
                                                                                        OnClick="btnSearch_Click" />
                                                                                    <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" Width="80px"  
                                                                                        OnClick="btnSendMail_Click" />&nbsp;
                                                                                </td>
                                                                            </tr>                                                                            
                                                                            <tr>
                                                                                <td style="width: 100%">
                                                                                     <table style="border: 1px; width: 100%;">
                                                                            <tr>
                                                                                <td style="width: 20%;color:Black;">
                                                                                   <%-- Search:
                                                                                    <gridsearch:XGridSearchTextBox ID="XGridSearchTextBox6" runat="server" CssClass="search-input"
                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>--%>
                                                                                </td>
                                                                                <td style="vertical-align: right; width: auto; text-align: right;color:Black;">
                                                                                    Record Count:
                                                                                    <%= this.grdUnsentNF2.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="XGridPaginationDropDown6" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel10" runat="server" Text="Export TO Excel" OnClick="lnkExportTOExcelgrdMissingUnsentNF2_onclick"> 
                                                                             <img src="Images/Excel.jpg" 
                                                                                 style="border:none;"  
                                                                                 height="15px" 
                                                                                 width ="15px" 
                                                                                 title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                                     </table>
                                                                        <xgrid:XGridViewControl ID="grdUnsentNF2" runat="server" Width="100%" CssClass="mGrid"  
                                                                            DataKeyNames="SZ_PATIENT_NAME,CLAIM_NO,SZ_INSURANCE_ADDRESS" MouseOverColor="0, 153, 153"
                                                                            EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                            AlternatingRowStyle-BackColor="#EEEEEE" 
                                                                            ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,DT_DATE_OF_ACCIDENT,DAYS,dt_first_treatment,SZ_ATTORNEY_NAME,STATUS"
                                                                            ShowExcelTableBorder="true" 
                                                                            ExportToExcelColumnNames="Case #,Patient Name,Insurance Company,Accident Date,Days,First Treatment,Attorney Name,Status"
                                                                            AllowPaging="true" XGridKey="MissingUnsentNF2" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                            AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                                            <Columns>
                                                                            <%--0--%>
                                                                            <asp:TemplateField HeaderText="Sent">
                                                                               <itemtemplate>
                                                                            <asp:CheckBox ID="ChkSent" runat="server" />
                                                                               </itemtemplate> 
                                                                            </asp:TemplateField> 
                                                                                <%--1--%>
                                                                            <asp:TemplateField HeaderText="Case #"  >
                                                                                    <itemtemplate>                                                                             
                                                                             <a href="Bill_Sys_CaseDetails.aspx?Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>"><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>
                                                                                    </itemtemplate>
                                                                            </asp:TemplateField>                                                                                
                                                                                <%--2--%>
                                                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"  ></asp:BoundField>                                                                                
                                                                                <%--3--%>
                                                                                <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"   ></asp:BoundField>
                                                                                <%--4--%>
                                                                                <asp:BoundField DataField="DT_DATE_OF_ACCIDENT" HeaderText="Accident Date"  ></asp:BoundField>
                                                                                <%--5--%>
                                                                                <asp:BoundField DataField="DAYS" HeaderText="Days"></asp:BoundField>
                                                                                <%--6--%>
                                                                                <%--change status column from 6 to 12--%>
                                                                                <%--<asp:BoundField DataField="STATUS" HeaderText="Status"></asp:BoundField>--%>
                                                                                <%--7--%>
                                                                                <asp:BoundField DataField="CLAIM_NO" HeaderText="Claim Number" Visible="false"></asp:BoundField>
                                                                                <%--8--%>
                                                                                <asp:BoundField DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address" Visible="false"></asp:BoundField>
                                                                                <%--9--%>
                                                                                <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Insurance Address" Visible="false"></asp:BoundField>
                                                                                <%--10--%>
                                                                                <asp:BoundField DataField="dt_first_treatment" HeaderText="First Treatment" ></asp:BoundField>
                                                                                <%--11--%>
                                                                                <asp:BoundField DataField="SZ_ATTORNEY_NAME" HeaderText="Attorney Name" ></asp:BoundField> 
                                                                                <%--12
                                                                                change status column from 6 to 12--%> 
                                                                                <asp:BoundField DataField="STATUS" HeaderText="Status"></asp:BoundField>                                                                                                                                                              
                                                                            </Columns>
                                                                        </xgrid:XGridViewControl>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </ajaxToolkit:TabPanel>
                                                            </ajaxToolkit:TabContainer>
                                                            <asp:DataGrid ID="grdExportToExcel" Width="100%" runat="Server" CssClass="GridTable" AutoGenerateColumns="False" visible="false">
                                                                            <Columns>
                                                                                <%--1--%>
                                                                                <asp:TemplateColumn HeaderText="Case #" Visible="false">
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                        </itemtemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--2--%>
                                                                                 
                                                                            <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                                        
                                                                                <%--3--%>
                                                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Case Name" Visible="false"></asp:BoundColumn>
                                                                                <%--4--%>
                                                                                <asp:TemplateColumn HeaderText="Patient" Visible="false">
                                                                                    <itemtemplate>
                                                                            <a href='PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' target="_blank">                                                                                 
                                                                            </a>
                                                                        </itemtemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--5--%>
                                                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ItemStyle-Width="200px"  ></asp:BoundColumn>
                                                                                 
                                                                                <%--6--%>
                                                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-Width="170px"></asp:BoundColumn>
                                                                                <%--7--%>
                                                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundColumn>
                                                                                <%--8--%>
                                                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundColumn>
                                                                                <%--9--%>
                                                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}" ItemStyle-Width="130px"  >
                                                                                </asp:BoundColumn>

                                                                                <%--10--%>
                                                                                <asp:TemplateColumn HeaderText="Desk" ItemStyle-Width="80px">
                                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((DataGridItem) Container).ItemIndex %>' CommandName="Patient Desk" visible="false"
                                                                                ToolTip="Patient Desk">																		                
                                                                            </asp:LinkButton>
                                                                             <a href="../Bill_SysPatientDesk.aspx?Flag=true&Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/></a>  
                                                                        </itemtemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--11--%>
                                                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundColumn>
                                                                                 <%--12--%>
                                                                                <asp:BoundColumn DataField="sz_remote_case_id" HeaderText="Patient Id" Visible="false"></asp:BoundColumn>
                                                                            </Columns>
                                                                      </asp:DataGrid>
                                                                      
                                                                        <asp:DataGrid ID="grdExportToExcelClaimNo" Width="100%" runat="Server" CssClass="GridTable" AutoGenerateColumns="False" visible="false">
                                                                            <Columns>
                                                                                <%--1--%>
                                                                                <asp:TemplateColumn HeaderText="Case #" Visible="false">
                                                                                    <itemtemplate>
                                                                            <asp:Label ID="lblLocationName1" runat="server" Visible="false" Text="Location" Font-Bold="true"
                                                                                Font-Size="Small"></asp:Label>
                                                                            <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                        </itemtemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--2--%>
                                                                                
                                                                            <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                                       
                                                                                <%--3--%>
                                                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Case Name" Visible="false"></asp:BoundColumn>
                                                                                <%--4--%>
                                                                                <asp:TemplateColumn HeaderText="Patient" Visible="false">
                                                                                    <itemtemplate>
                                                                            <a href="PatientHistory.aspx?CaseId=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>" target="_blank"></a>
                                                                        </itemtemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--5--%>
                                                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ItemStyle-Width="200px" ></asp:BoundColumn>
                                                                                <%--6--%> 
                                                                                 <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" Visible="true"  ></asp:BoundColumn>                                                                                 
                                                                                <%--7--%>
                                                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-Width="170px"></asp:BoundColumn>
                                                                                <%--8--%>
                                                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="OFFICE" Visible="False"></asp:BoundColumn>
                                                                                <%--9--%>
                                                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="DOCTOR" Visible="False"></asp:BoundColumn>
                                                                                <%--10--%>
                                                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" DataFormatString="{0:dd MMM yyyy}" ItemStyle-Width="130px"  >
                                                                                </asp:BoundColumn>
                                                                                <%--11--%>
                                                                                <asp:TemplateColumn HeaderText="Desk" ItemStyle-Width="80px">
                                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                                                CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")  + "," +   ((DataGridItem) Container).ItemIndex %>' CommandName="Patient Desk" visible="false"
                                                                                ToolTip="Patient Desk">																		                
                                                                            </asp:LinkButton>
                                                                             <a href="../Bill_SysPatientDesk.aspx?Flag=true&Status=Report&case=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&PName=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME") %>&csno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>&cmpid=<%#DataBinder.Eval(Container,"DataItem.SZ_Company_id") %>" ><img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/></a>  
                                                                                    </itemtemplate>
                                                                                </asp:TemplateColumn>
                                                                                <%--12--%>
                                                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundColumn>
                                                                                  <%--13--%>
                                                                                <asp:BoundColumn DataField="sz_remote_case_id" HeaderText="Patient Id" Visible="false"></asp:BoundColumn>
                                                                            </Columns>
                                                                        </asp:DataGrid>
                                                        </contenttemplate>
                                                    </asp:UpdatePanel>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 99%; height: auto; float: left;">
                                                    <div id="divid" style="position: absolute; width: 600px; height: 0px; background-color: #DBE6FA;
                                                        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                                        border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                                        <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                                            <a onclick="document.getElementById('divid').style.visibility='hidden';document.getElementById('divid').style.zIndex = -1;"
                                                                style="cursor: pointer;" title="Close">X</a>
                                                        </div>
                                                        <iframe id="frameeditexpanse" src="" frameborder="0" height="0px" width="600px"></iframe>
                                                    </div>
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
    <div id="divDashBoard" visible="false" style="position: absolute; width: 800px; height: 0px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="1" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 0; float: left; position: relative;" visible="false">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 0%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="LeftTop">
                            </td>
                            <td class="CenterTop">
                            </td>
                            <td class="RightTop">
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftCenter" style="height: 0%">
                            </td>
                            <td style="width: 97%" class="TDPart">
                                <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 99%; height: 0px; float: left; position: relative; left: 0px; top: 0px;
                                    vertical-align: top" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Missing Speciality</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                        You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMissingSpecialityText" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%; height: 10px;" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Today's Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%">
                                            Weekly &nbsp;Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart">
                                            <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; vertical-align: top; float: left; position: relative;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Bill Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%;" valign="top">
                                            Desk</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have&nbsp;
                                            <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Missing Information</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%;" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Report Section</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 0px; float: left; position: relative; left: 0px; top: 0px;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Procedure Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%;
                                    height: 0px; float: left; position: relative; left: 0px; top: 0px;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Visits</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblVisits" runat="server" Visible="true"></asp:Label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ul style="list-style-type: disc; padding-left: 60px;">
                                                            <li><a id="hlnkTotalVisit" href="#" runat="server">
                                                                <asp:Label ID="lblTotalVisit" runat="server"></asp:Label></a>&nbsp;Total Visit</li><li>
                                                                    <a id="hlnkBilledVisit" href="#" runat="server">
                                                                        <asp:Label ID="lblBilledVisit" runat="server"></asp:Label></a>&nbsp;Billed Visit
                                                                </li>
                                                            <li><a id="hlnkUnBilledVisit" href="#" runat="server">
                                                                <asp:Label ID="lblUnBilledVisit" runat="server"></asp:Label></a>&nbsp;UnBilled Visit
                                                            </li>
                                                        </ul>
                                                        <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="hlnkTotalVisit"
                                                            PopupControlID="pnlTotalVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="hlnkBilledVisit"
                                                            PopupControlID="pnlBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="hlnkUnBilledVisit"
                                                            PopupControlID="pnlUnBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 33%; height: 0px; float: left; position: relative; left: 0px; top: 0px;
                                    vertical-align: top" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">
                                            Patient Visit Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            You have &nbsp;<asp:Label ID="lblPatientVisitStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="RightCenter" style="width: 10px; height: 0%;">
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
        </table>
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 0px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="0px" width="850px" visible="false">
        </iframe>
    </div>
    <%--Total Visit--%>
    <asp:Panel ID="pnlTotalVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                        AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--Visit--%>
    <asp:Panel ID="pnlBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--UnVisit--%>
    <asp:Panel ID="pnlUnBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hdnSpeciality" runat="server" />
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtStatus" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtLocationId" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtUserId" runat="server" Visible="False"></asp:TextBox>
</asp:Content>
