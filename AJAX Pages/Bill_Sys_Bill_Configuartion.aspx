<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Bill_Configuartion.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Bill_Configuartion"
    Title="Bill Config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%; background-color: White; border: 1px;">
        <tr>
            <td style="vertical-align: top; width: 100%;">
                <table style="width: 100%; border: 0px solid;">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: White;">
                            <table border="0" width="100%">
                                <tr>
                                    <td valign="top" style="width: 50%;">
                                        <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                            height: 100%; border: 1px solid #B5DF82;">
                                            <tr>
                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                    <b class="txt3">Search Parameters</b>
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
</asp:Content>
