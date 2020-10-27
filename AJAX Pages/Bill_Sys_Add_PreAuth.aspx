<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Add_PreAuth.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Add_PreAuth"
    Title="Add Pre-Authorisation" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
     function showPreAuthsent()
        {   
          var url = "Bill_Sys_Pre_Authsent.aspx"
          PreAuthsentPop.SetContentUrl(url);
          PreAuthsentPop.Show();
          return false;
        }
        function showPreAuthreceived()
        {   
          var url = "Bill_Sys_Pre_Authreceived.aspx";
          PreAuthreceivedPop.SetContentUrl(url);
          PreAuthreceivedPop.Show();
          return false;
        }
        
         function showAppealsent()
        {   
          var url = "Bill_Sys_AppealSent.aspx";
          AppealSentPopup.SetContentUrl(url);
          AppealSentPopup.Show();
          return false;
        }
        
        function showAppealreceived()
        {   
          var url = "Bill_Sys_AppealReceived.aspx";
          AppealReceivedPopup.SetContentUrl(url);
          AppealReceivedPopup.Show();
          return false;
        }
        
    </script>

    <table style="width: 100%; background-color: White; height: 600px; border: 1px;">
        <tr>
            <td style="vertical-align: top; width: 100%;">
                <div id="asd" runat="server">
                    <table width="100%" border="0" id="Table1" runat="server">
                        <%-- <tr>
                            <td align="right">
                                <asp:LinkButton ID="btnXlsExport1" OnClick="btnXlsExport1_Click" runat="server" Text="Export TO Excel"
                                    Visible="true">
                                      <img 
                                           src="Images/Excel.jpg" 
                                           alt="" 
                                           style="border:none;" 
                                           height="15px" 
                                           width ="15px" 
                                           title = "Export TO Excel"/>
                                </asp:LinkButton>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="gridpreauthorisation" ClientInstanceName="gridpreauthorisation"
                                    runat="server" Width="100%" SettingsPager-PageSize="20" KeyFieldName="SZ_CASE_ID"
                                    SettingsBehavior-AllowSort="true" AutoGenerateColumns="False" SettingsPager-Mode="ShowPager">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="SZ_CASE_ID" Caption="SZ_CASE_ID" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_CASE_NO" Caption="Case No" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DT_DATE_OF_ACCIDENT" Caption="Date Of Accident"
                                            HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DATE_OPEN" Caption="Date Open" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_CLAIM_NUMBER" Caption="Claim No" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance Name"
                                            HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="true" ShowGroupPanel="true" />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <SettingsBehavior AllowSelectByRowClick="true" />
                                    <SettingsPager Position="Bottom" />
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxGridView ID="gridpreauthspecwise" runat="server" Width="100%" ClientInstanceName="gridpreauthspecwise"
                                                OnBeforePerformDataSelect="detailGrid_DataSelect" KeyFieldName="SZ_PROCEDURE_GROUP_ID">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="SZ_PROCEDURE_GROUP_ID"
                                                        HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Speciality" HeaderStyle-HorizontalAlign="Left"
                                                        Width="100px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataHyperLinkColumn Caption="" Visible="true">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="showPreAuthsent()">PreAuth Sent</a>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" />
                                                    </dx:GridViewDataHyperLinkColumn>
                                                    <dx:GridViewDataHyperLinkColumn Caption="" Visible="true">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="showPreAuthreceived()">PreAuth Received</a>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" />
                                                    </dx:GridViewDataHyperLinkColumn>
                                                    <dx:GridViewDataHyperLinkColumn Caption="" Visible="true">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="showAppealsent()">Appeal Sent</a>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" />
                                                    </dx:GridViewDataHyperLinkColumn>
                                                    <dx:GridViewDataHyperLinkColumn Caption="" Visible="true">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="showAppealreceived()">Appeal Received</a>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" />
                                                    </dx:GridViewDataHyperLinkColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <Styles>
                                                    <FocusedRow BackColor="#99ccff">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True">
                                                    </AlternatingRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
                                    <Styles>
                                        <FocusedRow BackColor="#99ccff">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True">
                                        </AlternatingRow>
                                    </Styles>
                                    <SettingsDetail ShowDetailRow="true" ExportMode="Expanded" />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                </dx:ASPxGridView>
                                <%--<table width="100%" id="tbl">
                                    <tr>
                                        <td align="right">
                                            <dx:ASPxButton ID="btnXlsExport2" runat="server" Text="Export to XLS" OnClick="btnXlsExport2_Click"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                </table>--%>
                                <%-- <dx:ASPxGridViewExporter ID="grdExportThirtyFour" runat="server" GridViewID="gridprovider">
                                </dx:ASPxGridViewExporter>
                                <dx:ASPxGridViewExporter ID="grdExport2" runat="server" GridViewID="gridproviderwise">
                                </dx:ASPxGridViewExporter>--%>
                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <dx:ASPxPopupControl ID="PreAuthsentPop" runat="server" CloseAction="CloseButton"
        HeaderStyle-BackColor="#B5DF82" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ClientInstanceName="PreAuthsentPop" HeaderText="PreAuthorisation Sent"
        HeaderStyle-HorizontalAlign="Left" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="1000px" ToolTip="PreAuthsent" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" RenderIFrameForPopupElements="Default"
        ScrollBars="None" Height="573px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="PreAuthreceivedPop" runat="server" CloseAction="CloseButton"
        HeaderStyle-BackColor="#B5DF82" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ClientInstanceName="PreAuthreceivedPop" HeaderText="PreAuthorisation Received"
        HeaderStyle-HorizontalAlign="Left" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="1000px" ToolTip="PreAuthreceived" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" RenderIFrameForPopupElements="Default"
        ScrollBars="None" Height="492px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="AppealSentPopup" runat="server" CloseAction="CloseButton"
        HeaderStyle-BackColor="#B5DF82" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ClientInstanceName="AppealSentPopup" HeaderText="Appeal Sent"
        HeaderStyle-HorizontalAlign="Left" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="1000px" ToolTip="AppealSent" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" RenderIFrameForPopupElements="Default"
        ScrollBars="None" Height="492px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="AppealReceivedPopup" runat="server" CloseAction="CloseButton"
        HeaderStyle-BackColor="#B5DF82" Modal="true" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ClientInstanceName="AppealReceivedPopup" HeaderText="Appeal Received"
        HeaderStyle-HorizontalAlign="Left" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="1000px" ToolTip="AppealReceived" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" RenderIFrameForPopupElements="Default"
        ScrollBars="None" Height="492px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <asp:HiddenField ID="hdncaseid" runat="server" />
</asp:Content>
