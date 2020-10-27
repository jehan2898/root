<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ViewNotes.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_ViewNotes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
<link href="Css/UI.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
<link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
<link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
<link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
<link rel="stylesheet" href="CSS/style.css" type="text/css" />
<link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td style="width: 100%" class="SectionDevider" colspan="4">
                    <table>
                        <tr>
                            <td style="width: 5px;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="grdViewNotes" runat="server" Width="800px" CssClass="GridTable"
                                    AutoGenerateColumns="false">
                                    <FooterStyle />
                                    <SelectedItemStyle />
                                    <PagerStyle />
                                    <AlternatingItemStyle />
                                    <ItemStyle CssClass="GridRow" />
                                    <Columns>
                                        <%--0--%>
                                        <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                        <%--1--%>
                                        <asp:BoundColumn DataField="NOTE_TYPE" HeaderText="Note Type"></asp:BoundColumn>
                                        <%--2--%>
                                        <asp:BoundColumn DataField="ADDED_ON" HeaderText="Added On"></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="GridHeader1" />
                                </asp:DataGrid>
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
