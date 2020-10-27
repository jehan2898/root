<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_DenialDetails.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_DenialDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                                <div style="overflow: scroll;">
                                    <dx:ASPxGridView ID="grdDEN" runat="server" KeyFieldName="I_DENIAL_REASON_ID" AutoGenerateColumns="false"
                                        Width="100%" SettingsBehavior-AllowSort="true" SettingsPager-Mode="ShowPager">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="SZ_DENIAL_REASONS" Caption="Denial Reason" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="dt_denial_date" Caption="Denial Date" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="SZ_DESCRIPTION" Caption="Description" HeaderStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="I_DENIAL_REASON_ID" Caption="I_DENIAL_REASON_ID" HeaderStyle-HorizontalAlign="Center" Visible="false">
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
