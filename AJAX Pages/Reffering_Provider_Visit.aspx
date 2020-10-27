<%@ Page Title="Green Bills - Reffering Provider" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="Reffering_Provider_Visit.aspx.cs" Inherits="AJAX_Pages_Reffering_Provider_Visit" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl"%>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    
    <link rel="Stylesheet" href="css/ticketing.css" type="text/css" />

       <script language="javascript" type="text/javascript">
           function validateProvider(s, e) {
               var providerId = null;
               providerId = cRefferngProvider.GetValue().toString();
               if (providerId == "NA") {
                   alert('Please Select Reffering Provider');
                   e.processOnServer = false;
               }
               var btn = document.getElementById("ctl00_ContentPlaceHolder1_ASPxCallbackPanel1_hdnBtnclick");
               btn.value = 1;
           }
           function showPopup2(CaseId, PatientName, EventId, PgId, VisitId, visitType, DoctorId) {
               var url = "Bill_Sys_Popup_For_Visit.aspx?CaseId=" + CaseId + "&PatientName=" + PatientName + "&EventId=" + EventId + "&PgId=" + PgId + "&VisitId=" + VisitId + "&visitType=" + visitType + "&DoctorId=" + DoctorId;
               IFrame_NewTicket.SetContentUrl(url);
               IFrame_NewTicket.Show();
               return false;
           }
           function SelectAllVisit(ival) {
               //alert(ival);
               var f = document.getElementById('<%=grdVisits.ClientID%>');
               var str = 1;
               for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                   if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                       if (f.getElementsByTagName("input").item(i).disabled == false) {
                           f.getElementsByTagName("input").item(i).checked = ival;
                       }

                   }
               }
           }
           
    </script>

    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server">
        <PanelCollection>
            <dx:PanelContent>
                <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                    background-color: White;">
                    <tr>
                        <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 3px; width: 100%;
                            padding-top: 3px; height: 100%; vertical-align: top;">
                            <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
                                <tr>
                                    <td colspan="3">
                                        <asp:updatepanel id="UpdatePanel10" runat="server">
                                            <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                                        </asp:updatepanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%">
                                    </td>
                                    <td valign="top">
                                        <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                            background-color: White;">
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td style="text-align: center; width: 100%; vertical-align: top;">
                                                                <table style="text-align: center; width: 50%;">
                                                                    <tr>
                                                                        <td style="text-align: center; width: 100%; vertical-align: top;">
                                                                            <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                                height: 100%; border: 0px solid #B5DF82;">
                                                                                <tr>
                                                                                    <td style="width: 100%; height: 0px;" align="center">
                                                                                        <table border="0" cellpadding="10" cellspacing="0" style="width: 100%; border-right: 1px solid #d3d3d3;
                                                                                            border-left: 1px solid #d3d3d3; border-bottom: 1px solid #d3d3d3" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                            <tr>
                                                                                                <td align="left" valign="middle" colspan="3" style="background-color: #CDCAB9; font-family: Calibri;
                                                                                                    font-size: 20px; font-weight: normal; font-style: italic;">
                                                                                                    Search Parameters
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Visit Date</label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        From
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        To
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="ddlDateValues">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="--Select--" Value="not_selected" Selected="True"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="All" Value="0"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Today" Value="1"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Week" Value="2"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Month" Value="3"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Quarter" Value="4"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Year" Value="5"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Week" Value="6"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Month" Value="7"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Quarter" Value="8"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Year" Value="9"></dxe:ListEditItem>
                                                                                                        </Items>
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" CssClass="inputBox"
                                                                                                        ID="dtfromdate">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdttodate" CssClass="inputBox"
                                                                                                        ID="dttodate">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Doctor Name
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Case#
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Billed/Un-Billed
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxComboBox runat="server" EnableSynchronization="False" ValueType="System.String"
                                                                                                        ClientInstanceName="cIssueType" CssClass="inputBox" ID="ddlDoctor">
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="true" CssClass="inputBox"
                                                                                                        ID="txtCaseNo">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="ddlBiled">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                                                                                        </Items>
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3" align="center">
                                                                                                    <dxe:ASPxButton runat="server" Text="Search" ID="BtnSearch" 
                                                                                                        OnClick="BtnSearch_Click"></dxe:ASPxButton>

                                                                                                </td>
                                                                                                <tr>
                                                                                                    <td colspan="3">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                </tr>
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
                                                            <td>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="right" valign="bottom">
                                                                            <label>
                                                                                Reffering Provider
                                                                            </label>
                                                                        </td>
                                                                        <td align="right" valign="bottom" width="200px">
                                                                             <dxe:ASPxComboBox runat="server" EnableSynchronization="False" ValueType="System.String"
                                                                                ClientInstanceName="cRefferngProvider" CssClass="inputBox" 
                                                                                 ID="ddlRefferingProvider" 
                                                                                 OnSelectedIndexChanged="ddlRefferingProvider_SelectedIndexChanged" 
                                                                                 AutoPostBack="True">
                                                                                <ItemStyle>
                                                                                    <HoverStyle BackColor="#F6F6F6">
                                                                                    </HoverStyle>
                                                                                </ItemStyle>
                                                                            </dxe:ASPxComboBox>
                                                                            
                                                                        </td>
                                                                        <td align="right" valign="bottom" width="100px" >
                                                                            <label>
                                                                                Reffering Doctor
                                                                            </label>
                                                                        </td>
                                                                        <td align="right" valign="bottom" width="200px">
                                                                             <dxe:ASPxComboBox runat="server" EnableSynchronization="False" ValueType="System.String"
                                                                                ClientInstanceName="cRefferingDoctor" CssClass="inputBox" ID="ddlRefferingDoctor">
                                                                                <ItemStyle>
                                                                                    <HoverStyle BackColor="#F6F6F6">
                                                                                    </HoverStyle>
                                                                                </ItemStyle>
                                                                            </dxe:ASPxComboBox>
                                                                            
                                                                        </td>
                                                                        <td width="125px" align="right" valign="bottom">
                                                                            <dxe:ASPxButton runat="server" Text="Attach To Visit" ID="btnAttach" ClientInstanceName="cIbtnAttach" 
                                                                                OnClick="btnAttach_Click">
                                                                                <ClientSideEvents Click="validateProvider"></ClientSideEvents>
                                                                            </dxe:ASPxButton>
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
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <div style="height: 400px; width: 100%; background-color: gray; overflow: scroll;">
                                                <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                                    Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                                    Settings-VerticalScrollableHeight="330">
                                                    <columns>
                                                    <%--0--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="Case ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--1--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="Patient ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--2--%>
                                                      <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="Event ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                    
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    
                                                    </dx:GridViewDataColumn>
                                                    <%--3--%>
                                                     <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE_ID" Caption="Visit Type ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--4--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="Doctor ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                      <%--5--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="Procedure Group ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </dx:GridViewDataColumn>
                                                    <%--6--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case#" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                       <%--7--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                       <%--8--%>
                                                    <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Visit Date" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                       <%--9--%>
                                                    <dx:GridViewDataColumn FieldName="VISIT_TYPE" Caption="Visit Type" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                     <%--10--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                     <%--11--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                    <%--12--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_REFFERING_OFFICE" Caption="Reffering Provider" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                    <%--13--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_REFFERING_DOCTOR" Caption="Reffering Doctor" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                    <%--14--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_BILL_STATUS" Caption="Bill Status" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
<Settings AllowSort="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                    </dx:GridViewDataColumn>

                                                    <%--15--%>
                                                    <dx:GridViewDataColumn Caption="chk1" Width="30px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectVisit" runat="server" onclick="javascript:SelectAllVisit(this.checked);"
                                                            ToolTip="Select All" />
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                        <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </columns>
                                                    <settingsbehavior allowfocusedrow="True" allowsort="False" />
                                                    <settingspager pagesize="20"></settingspager>
                                                    <settings verticalscrollableheight="330"></settings>
                                                    <settingscustomizationwindow height="330px"></settingscustomizationwindow>
                                                    <styles>
                                                    <FocusedRow CssClass="dxgvFocusedGroupRow">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                    <SelectedRow CssClass="dxgvFocusedGroupRow">
                                                    </SelectedRow>
                                                </styles>
                                                </dx:ASPxGridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:textbox id="txtCompanyID" runat="server" visible="false">
                </asp:textbox>
                <asp:HiddenField ID="hdnBtnclick" runat="server" />


             <dx:ASPxPopupControl 
            ID="IFrame_NewTicket" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="IFrame_NewTicket" 
            HeaderText="Upload Document"
            HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-ForeColor="White"
            HeaderStyle-BackColor="#000000" 
            AllowDragging="True" 
            EnableAnimation="False"
            EnableViewState="True" Width="800px" ToolTip="Open New Ticket" PopupHorizontalOffset="0"
            PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
            RenderIFrameForPopupElements="Default" Height="500px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>

<HeaderStyle HorizontalAlign="Left" BackColor="Black" ForeColor="White"></HeaderStyle>
<ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True"></dx:PopupControlContentControl>
</ContentCollection>
        </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>

    </dx:ASPxCallbackPanel>
</asp:content>


