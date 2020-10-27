<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUserforAgent.aspx.cs" Inherits="FrmUserforAgent" %>

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

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 100%" valign="top" id="grdid" runat="server">
                    <div style="height: 300px; background-color: Gray; overflow-y: scroll;">
                     <dx:ASPxGridView ID="grdoffice" ClientInstanceName="grdoffice" runat="server"
                        Width="100%" KeyFieldName="description" AutoGenerateColumns="False">
                        <Columns>
                            <dx:GridViewDataColumn Caption="chk" VisibleIndex="0" Width="30px">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <DataItemTemplate>
                                    <asp:CheckBox ID="chkall" runat="server" />
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="CODE" Caption="Office ID" Visible="False">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="DESCRIPTION" Caption="Decsription" VisibleIndex="1">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                       </Columns> 
                         <SettingsPager Visible="False" Mode="ShowAllRecords">
                         </SettingsPager>
                    </dx:ASPxGridView>   
                    </div>       
                </td>
            </tr>
            <tr>
                <td align ="center" >
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
