<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_VerificationDetails.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_VerificationDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="overflow:scroll;">
                                    <dx:ASPxGridView ID="grdVER" runat="server" KeyFieldName="I_VERIFICATION_ID" AutoGenerateColumns="false"
                                        Width="100%" SettingsBehavior-AllowSort="true" SettingsPager-Mode="ShowPager">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="verification_request" Caption="Verification"
                                                HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="verification_date" Caption="Date" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="answer" Caption="Answer" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="answer_date" Caption="Answer Date" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="I_VERIFICATION_ID" Caption="I_VERIFICATION_ID"
                                                HeaderStyle-HorizontalAlign="Center" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles>
                                            <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                            </FocusedRow>
                                            <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                            </AlternatingRow>
                                        </Styles>
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsBehavior AllowSelectByRowClick="true" />
                                        <SettingsPager Position="Bottom" />
                                    </dx:ASPxGridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
