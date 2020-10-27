<%@ Page Title="" Language="C#" MasterPageFile="~/Refering provider/ProviderMasterPage.master"
    AutoEventWireup="true" CodeFile="Patient.aspx.cs" Inherits="Refering_provider_Patient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
    <div>
        <table>
            <tr>
                <td align="left" valign="middle" colspan="3" style="background-color: #CDCAB9; font-family: Calibri;
                    font-size: 20px; font-weight: normal; font-style: italic;">
                    Patient List
                </td>
            </tr>
        </table>
        <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%; vertical-align: top;">
                    <table style="width: 100%;">
                        <tr>
                            <td class="manage-member-lable-td">
                                <label>
                                    Case#</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Case Type</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Case Status</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Patient</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="registration-form-ibox">
                                <dxe:ASPxTextBox CssClass="inputBox" runat="server" ID="txtCaseNO" Width="195px" Height="30px">
                                </dxe:ASPxTextBox>
                            </td>
                            <td class="form-ibox">
                                <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                    ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                    ID="ddlcasetype">
                                    <%--<Items>
                                        <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                    </Items>
                                    <ItemStyle>
                                        <HoverStyle BackColor="#F6F6F6">
                                        </HoverStyle>
                                    </ItemStyle>--%>
                                </dxe:ASPxComboBox>
                            </td>
                            <td class="form-ibox">
                                <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                    ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                    ID="ddlcasestatus">
                                    <%--<Items>
                                        <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                    </Items>
                                    <ItemStyle>
                                        <HoverStyle BackColor="#F6F6F6">
                                        </HoverStyle>
                                    </ItemStyle>--%>
                                </dxe:ASPxComboBox>
                            </td>
                            <td class="registration-form-ibox">
                                <%--<dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                    ID="txtpatient">
                                </dxe:ASPxTextBox>--%>
                                <asp:TextBox ID="txtpatient" runat="server" Width="195px" autocomplete="off" CssClass="inputBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="manage-member-lable-td">
                                <label>
                                    Insurance</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Claim Number</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    SSN#</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Location</label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="registration-form-ibox">
                                <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                    ID="txtinsurance">
                                </dxe:ASPxTextBox>
                            </td>
                            <td class="registration-form-ibox">
                                <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                    ID="txtClaimnumber">
                                </dxe:ASPxTextBox>
                            </td>
                            <td class="registration-form-ibox">
                                <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                    ID="txtSSN">
                                </dxe:ASPxTextBox>
                            </td>
                            <td class="registration-form-ibox">
                                <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                    ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                    ID="ddllocation">
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="manage-member-lable-td">
                                <label>
                                    Date Of Birth</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Date of Accident</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Chart Number</label>
                            </td>
                        </tr>--%>
                       <%-- <tr>
                            <td class="form-ibox">
                                <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtBirthDate" CssClass="inputBox"
                                    ID="dtBirthDate">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td class="form-ibox">
                                <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtAccDate" CssClass="inputBox"
                                    ID="dtAccDate">
                                </dxe:ASPxDateEdit>
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
                            <td class="registration-form-ibox">
                                <dxe:ASPxTextBox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                    ID="txtchartno">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>--%>
                    </table>
                    <table id="Table1" style="width: 100%;">
                        <tr>
                            <td align="right" >
                                <dx:ASPxButton runat="server" Text="Search" ID="btnSearch" 
                                    onclick="btnSearch_Click">
                                </dx:ASPxButton>
                            </td>
                            <td align="left">
                                <dx:ASPxButton runat="server" Text="Reset" ID="btnReset" 
                                    onclick="btnReset_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100%">
                                <div style="height: 400px; width: 100%; background-color: gray; overflow: scroll;">
                                    <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                        Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                        Settings-VerticalScrollableHeight="330">
                                        <Columns>
                                            <%--0--%>
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case #" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--1--%>
                                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--3--%>
                                            <dx:GridViewDataColumn FieldName="DT_DATE_OF_ACCIDENT" Caption="Accident Date" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--4--%>
                                            <dx:GridViewDataColumn FieldName="DT_DATE_OPEN" Caption="Opened" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--5--%>
                                            <dx:GridViewDataColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance" HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--6--%>
                                            <dx:GridViewDataColumn FieldName="SZ_CLAIM_NUMBER" Caption="Claim Number" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--7--%>
                                            <dx:GridViewDataColumn FieldName="SZ_POLICY_NUMBER" Caption="Policy Number" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--8--%>
                                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_PHONE" Caption="Phone Number" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--9--%>
                                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE" Caption="Case Type" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--9--%>
                                            <dx:GridViewDataColumn FieldName="SZ_STATUS_NAME" Caption="Case Status" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--9--%>
                                            <dx:GridViewDataColumn FieldName="Location" Caption="Location" HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>

                                            <%--9--%>
                                            <%--<dx:GridViewDataColumn Caption="Open File" Settings-AllowSort="False" Width="25px">
                                                <HeaderTemplate>
                                                    Add Visit
                                                </HeaderTemplate>
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkAddVisit" runat="server" Text='Add Document' CommandName=""
                                                        OnClick='<%# "showPopup2(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ", " + "\""+ Eval("SZ_PATIENT_NAME") +"\""+ ", " + "\""+ Eval("I_EVENT_ID") +"\""+ ", " + "\""+ Eval("SZ_PROCEDURE_GROUP_ID") +"\""+ ", "  + "\""+ Eval("SZ_VISIT_TYPE_ID") +"\""+ ", "  + "\""+ Eval("VISIT_TYPE") +"\""+ "," + "\""+ Eval("SZ_CASE_NO") +"\""+ "," + "\""+ Eval("SZ_DOCTOR_ID") +"\"); return false;" %> '>
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                            </dx:GridViewDataColumn>--%>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                        <SettingsPager PageSize="20">
                                        </SettingsPager>
                                        <Settings VerticalScrollableHeight="330"></Settings>
                                        <SettingsCustomizationWindow Height="330px"></SettingsCustomizationWindow>
                                        <Styles>
                                            <FocusedRow CssClass="dxgvFocusedGroupRow">
                                            </FocusedRow>
                                            <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                            </AlternatingRow>
                                            <SelectedRow CssClass="dxgvFocusedGroupRow">
                                            </SelectedRow>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtpatient"
                                    ServiceMethod="GetPatient" ServicePath="~/AJAX Pages/PatientService.asmx" UseContextKey="true"
                                    ContextKey="SZ_COMPANY_ID">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                        </tr>
                    </table>
    </div>
</asp:Content>
