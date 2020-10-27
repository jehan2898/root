<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CO_ViewDeatails.aspx.cs"
    Inherits="Bill_Sys_CO_ViewDeatails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css"rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:DataGrid ID="grddiagnosis" runat="server" AutoGenerateColumns="false" CssClass="GridTable">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:BoundColumn DataField="DIAGNOSIS CODE" HeaderText="Diagnosis Code" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODES" HeaderText="Procedure Code" ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="DIAGNOSIS CODE DESCRIPTION" HeaderText = "Diagnosis Code" ReadOnly = "true">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>--%>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
