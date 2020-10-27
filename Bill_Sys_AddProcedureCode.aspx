<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AddProcedureCode.aspx.cs"
    Inherits="Bill_Sys_AddProcedureCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="masterhead">
    <title>Untitled Page</title>
    <style type="text/css">
.ajax__tab_theme .ajax__tab_header 
{
    font-family:verdana,tahoma,helvetica;
    font-size:11px;
    
}
</style>
</head>
<body>
        <script type="text/javascript" src="validation.js"></script>
    <form id="form1" runat="server">
        <table width="100%">
            <tr style="width: 100%">
                <td class="TDPart">
                    <asp:Panel ID="pnlShowNotes" runat="server" BackColor="white" Style="height: 400px;
                        width: 100%;">
                        <asp:Label ID="lblDateOfService" runat="server" Text="Date Of Service" Font-Bold="False"></asp:Label>
                        <asp:TextBox ID="txtDateOfservice" runat="server" Width="240px" ReadOnly="false"
                            onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                        <a id="A1" href="#">
                            <input type="image" name="mgbtnDateofService" id="Image1" runat="server" src="Images/cal.gif"
                                border="0" /></a>
                                <asp:Button ID="btnProceCode" Text="ADD" runat="server" OnClick="btnProceCode_Click" CssClass="Buttons" />&nbsp;<%--  <input type="button" value="Add" class="Buttons" onclick="onOk()" id="btnService" />--%>
                        <div style="overflow: scroll; height: 100%; width: 99%;">
                            <asp:DataGrid ID="grdProcedure" runat="server" AllowPaging="false" Width="99%" CssClass="GridTable"
                                AutoGenerateColumns="false">
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkselect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="PROCEDURE ID" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="SZ_TYPE_CODE_ID ID" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" Visible="false"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="GridHeader" />
                            </asp:DataGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
