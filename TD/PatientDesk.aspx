<%@ Page Title="" Language="C#" MasterPageFile="TreatingDoctorMasterPage.master"
    AutoEventWireup="true" CodeFile="PatientDesk.aspx.cs" Inherits="TreatingDoctor_Patient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function Export() {
            expLoadingPanel.Show();
            Callback1.PerformCallback();
        }
        function OnCallbackComplete(s, e) {
            expLoadingPanel.Hide();
            var url = "DownloadFiles.aspx";
            IFrame_DownloadFiles.SetContentUrl(url);
            IFrame_DownloadFiles.Show();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
    <div style="padding-top:10px;">
        <table width="100%;padding-left:0px;height:30px;" border="0">
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
                        <tr runat="server" id="row2label">
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
                        <tr runat="server" id="row2control">
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
                    </table>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
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
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100%;text-align:right;">
                                <dx:ASPxHyperLink text = "[Excel]" runat="server" ID="xExcel">
                                    <ClientSideEvents Click="Export" />
                                </dx:ASPxHyperLink>
                                <dx:ASPxCallback ID="ASPxCallback1" 
                                    runat="server" ClientInstanceName="Callback1" OnCallback="ASPxCallback1_Callback">
                                    <ClientSideEvents CallbackComplete="OnCallbackComplete" />
                                </dx:ASPxCallback>
                                <dx:ASPxLoadingPanel 
                                    Text = "Exporting..."
                                    runat="server" ID="expLoadingPanel" ClientInstanceName="expLoadingPanel">
                                </dx:ASPxLoadingPanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="height: 400px; width: 100%; overflow: scroll;">
                                     <dx:ASPxGridView 
                                        ID="grdVisits" runat="server" KeyFieldName="CaseID" 
                                        AutoGenerateColumns="false"
                                        Width="100%" SettingsPager-PageSize="20" 
                                        SettingsCustomizationWindow-Height="330"
                                        Settings-VerticalScrollableHeight="330">
                                        <Columns>
                                            <%--0--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="CaseNumber" 
                                                Width="3%"
                                                Caption="Case #" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--1--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="PatientName" 
                                                Width="15%"
                                                Caption="Patient" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--3--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="DOA" 
                                                Caption="Accident Date" 
                                                Width="7%"
                                                HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--4--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="CaseOpenedDate" 
                                                Caption="Opened" 
                                                Width="7%"
                                                HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--5--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="CarrierName" 
                                                Caption="Insurance" 
                                                Width="15%"
                                                HeaderStyle-HorizontalAlign="Center"
                                                Visible="true" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--6--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="ClaimNumber" 
                                                Caption="Claim Number" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--7--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="PolicyNumber" Caption="Policy Number" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--8--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="Phone" Caption="Phone Number" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--9--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="CaseType" Caption="Case Type" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--10--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="CaseStatus" Caption="Case Status" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--11--%>
                                            <dx:GridViewDataColumn 
                                                FieldName="Provider" Caption="Office" 
                                                HeaderStyle-HorizontalAlign="Center"
                                                Width="10%"
                                                Settings-AllowSort="False" HeaderStyle-Font-Bold="true">
                                            </dx:GridViewDataColumn>
                                            <%--11--%>
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
                                        <SettingsBehavior AllowFocusedRow="False" AllowSort="False" />
                                        <SettingsPager PageSize="20">
                                        </SettingsPager>
                                        <Styles>
                                            <AlternatingRow Enabled="True"></AlternatingRow>
                                            <Footer BackColor="#F0F0F0" Font-Bold="True"></Footer>
                                        </Styles>
                                        <Settings ShowFooter="True"/>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="CaseNumber" SummaryType="Count" DisplayFormat="Total Patients: {0}" />
                                        </TotalSummary>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="grdVisitsExport" runat="server" GridViewID="grdVisits"></dx:ASPxGridViewExporter>
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
            </table>

            <dx:ASPxPopupControl 
                ID="IFrame_DownloadFiles" 
                runat="server" CloseAction="CloseButton"
                Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ClientInstanceName="IFrame_DownloadFiles"
                HeaderText="Data Export"
                HeaderStyle-HorizontalAlign="Left"
                HeaderStyle-ForeColor="White"
                HeaderStyle-BackColor="#000000" 
                AllowDragging="True" 
                EnableAnimation="False"
                EnableViewState="True" Width="600px" ToolTip="Download File(s)" PopupHorizontalOffset="0"
                PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
                RenderIFrameForPopupElements="Default" Height="200px">
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
        </dx:ASPxPopupControl>
    </div>
</asp:Content>
