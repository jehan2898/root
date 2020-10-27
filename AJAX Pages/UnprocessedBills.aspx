<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UnprocessedBills.aspx.cs" Inherits="AJAX_Pages_UnprocessedBills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <link rel="Stylesheet" href="../Css/admin.css" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="width: 100%;">
    <table width="100%" style="background-color: white">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 100%;">

                            <div style="width: 100%; vertical-align: top;">
    <table id="page-title" style="width: 100%;">
        <tr style="width: 100%;">
            <td>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <b>Search Parameters</b>
            </td>
        </tr>
    </table>
    <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top;">
                                            <table style="width: 100%;">
                                                <tr style="width: 100%;">
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            Name
                                                        </label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            Address</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                            City</label>
                                                    </td>
                                                    <td class="manage-member-lable-td">
                                                        <label>
                                                        </label>
                                                    </td>
                                                </tr>
                                                </table>
                                            </td>
                                        </tr>
        </table>
                                </div>
                            </td>
                        </tr>
        </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

