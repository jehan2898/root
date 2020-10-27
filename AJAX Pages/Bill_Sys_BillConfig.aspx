<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_BillConfig.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_BillConfig"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
    <ajaxToolkit:TabContainer ID="tabcontainerAddVisit" runat="server" ActiveTabIndex="0">
        <ajaxToolkit:TabPanel ID="tabpanelwcconfig" runat="server" Height="100%">
            <HeaderTemplate>
                <div style="width: 150px; text-align: center;" class="lbl">
                    Nf3 Config
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 24%;
                    height: 100%; background-color: White">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;
                                            border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
                                            <tr>
                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                    <b class="txt3">Nf3 Config</b>
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
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="tabPanel1" Height="100%">
            <HeaderTemplate>
                <div style="width: 150px; text-align: center;" class="lbl">
                    Wc Config
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <table id="Second" border="0" cellpadding="0" cellspacing="0" style="width: 24%;
                    height: 100%; background-color: White">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;
                                            border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
                                            <tr>
                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                    <b class="txt3">Wc Config</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td colspan="3" style="height: 20px">
                                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                                    <contenttemplate>
                                                                        <UserMessage:MessageControl runat="server" ID="usrMessage1" />
                                                                    </contenttemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                Show Case No of pdf
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkcaseno" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" colspan="2">
                                                                Show Bill No of pdf
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkbillno" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                                    CssClass="Buttons" />
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
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
