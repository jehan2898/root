<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Refering_provider_Default" %>
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
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="Stylesheet" href="css/ticketing.css" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 192px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:updatepanel id="UpdatePanel2" runat="server">
        <contenttemplate>
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
                                    <td colspan="3">
                                        <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2"
                                            displayafter="10">
                                            <progresstemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px;
                                                                                                            left: 600px" runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Loading...</div>
                                                                                                    </progresstemplate>
                                        </asp:updateprogress>
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
                                                                                height: 50%; border: 0px solid #B5DF82;">
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
                                                                                                        Case#</label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Case Type
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Case Status
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="txtcasenumber">
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
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="ASPxComboBox2">
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
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                       Patient
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Insurance
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                    Claim Number
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="txtpatient">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="txtinsurance">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="txtClaimnumber">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                             <tr>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                       SSN#
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Date Of Accident
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                       Date Of Birth
                                                                                                    </label>
                                                                                                </td>
                                                                                            </tr>
                                                                                             <tr>
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="ASPxTextBox1">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdttodate" CssClass="inputBox"
                                                                                                        ID="dttodate">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdttodate" CssClass="inputBox"
                                                                                                        ID="ASPxDateEdit1">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                         
                                                                                             <tr>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                      Location
                                                                                                    </label>
                                                                                                </td>
                                                                                                <td class="new-ticket-form-lable-td" align="center">
                                                                                                    <label>
                                                                                                        Chart No.
                                                                                                    </label>
                                                                                                </td>
                                                                                              </tr>
                                                                                              <tr>
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="ASPxComboBox3">
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
                                                                                                <td class="form-ibox">
                                                                                                    <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="txtchartno">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="3" align="center">
                                                                                                    <dx:ASPxButton runat="server" Text="Search" ID="BtnSearch">
                                                                                                    </dx:ASPxButton>
                                                                                                </td>
                                                                                                 <td colspan="3" align="center">
                                                                                                    <dx:ASPxButton runat="server" Text="Clear" ID="Btnclear">
                                                                                                    </dx:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
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
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <table style="width: 100%">
                                 <tr>
                                   <td align="left" valign="middle" colspan="3" style="background-color: #CDCAB9; font-family: Calibri;
                                                                                                    font-size: 20px; font-weight: normal; font-style: italic;">
                                                                                                    Patient List
                                                                                                </td>
                                 </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <div style="height: 400px; width: 100%; background-color: gray; overflow: scroll;">
                                                <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                                    Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                                    Settings-VerticalScrollableHeight="330">
                                                    <columns>
                                                    <%--0--%>
                                                    <dx:GridViewDataColumn FieldName="#" Caption="#" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--1--%>
                                                    <dx:GridViewDataColumn FieldName="SZ_Chartno" Caption="Chart No." HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--2--%>
                                                      <dx:GridViewDataColumn FieldName="Patient" Caption="Patient" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    
                                                    </dx:GridViewDataColumn>
                                                    <%--3--%>
                                                     <dx:GridViewDataColumn FieldName="Accident_date" Caption="Accident Date" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--4--%>
                                                    <dx:GridViewDataColumn FieldName="Opened" Caption="Opened" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--5--%>
                                                    <dx:GridViewDataColumn FieldName="Insurance" Caption="Insurance" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--6--%>
                                                    <dx:GridViewDataColumn FieldName="Claim_Number" Caption="Claim Number" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--7--%>
                                                    <dx:GridViewDataColumn FieldName="Policy_no" Caption="Policy Number" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--8--%>
                                                    <dx:GridViewDataColumn FieldName="Billed" Caption="Billed" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--9--%>
                                                    <dx:GridViewDataColumn FieldName="Paid" Caption="Paid" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--10--%>
                                                    <dx:GridViewDataColumn FieldName="Outstanding" Caption="Outstanding" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--11--%>
                                                    <dx:GridViewDataColumn FieldName="Desk" Caption="Desk" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--12--%>
                                                    
                                                    <dx:GridViewDataColumn FieldName="Location" Caption="Location" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--13--%>
                                                    <dx:GridViewDataColumn FieldName="Date_of_first_treatment" Caption="Date Of First Treatment" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--14--%>
                                                    <dx:GridViewDataColumn FieldName="Bill_Summary" Caption="Bill Summary" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>
                                                    <%--15--%>
                                                    <dx:GridViewDataColumn Caption="Open File" Settings-AllowSort="False" Width="25px">
                                                    
                                                        <HeaderTemplate >
                                                         Add Visit
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                            <asp:linkbutton id="lnkAddVisit" runat="server" text='Add Document' commandname="" onclick='<%# "showPopup2(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ", " + "\""+ Eval("SZ_PATIENT_NAME") +"\""+ ", " + "\""+ Eval("I_EVENT_ID") +"\""+ ", " + "\""+ Eval("SZ_PROCEDURE_GROUP_ID") +"\""+ ", "  + "\""+ Eval("SZ_VISIT_TYPE_ID") +"\""+ ", "  + "\""+ Eval("VISIT_TYPE") +"\""+ "," + "\""+ Eval("SZ_CASE_NO") +"\""+ "," + "\""+ Eval("SZ_DOCTOR_ID") +"\"); return false;" %> '>
                                                            </asp:linkbutton>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
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
                <dx:ASPxPopupControl ID="IFrame_NewTicket" runat="server" CloseAction="CloseButton"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="IFrame_NewTicket" HeaderText="Upload Document" HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
                    EnableAnimation="False" EnableViewState="True" Width="800px" ToolTip="Open New Ticket"
                    PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
                    ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="500px">
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    </contenttemplate>
    </asp:updatepanel>
    </div>
    </form>
</body>
</html>
