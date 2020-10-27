<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ActivityRoles.aspx.cs"
    Inherits="Bill_Sys_ActivityRoles" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>
    <link href="Css/main.css" type="text/css" rel="Stylesheet" />

</head>
<body>
    <form id="frmActivityRole" runat="server">
        <div align="center">
            <table class="maintable">
                <tr>
                    <td class="bannerImg" colspan="3">
                        <img src="Images/page-bg.gif" width="100%" height="138px" /></td>
                </tr>
                <tr>
                    <td class="tableheader" colspan="3">
                        <span class="style6">Medical Billing System</span></td>
                </tr>
                <tr>
                    <td colspan="3" class="menucontrol">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            Height="24px"></cc2:WebCustomControl1>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellHeader" colspan="3">
                        Activity Role
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="30px">
                        <div id="ErrorDiv" visible="true" style="color: Red;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        Activity Role</td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                        <asp:TextBox ID="txtActivityRole" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                   
                </tr>
                <tr>
                    <td colspan="3" style="height: 177px">
                        <asp:DataGrid ID="grdActivityRole" runat="server" AutoGenerateColumns="False" OnPageIndexChanged="grdActivityRole_PageIndexChanged"
                            OnSelectedIndexChanged="grdActivityRole_SelectedIndexChanged">
                            <FooterStyle />
                            <SelectedItemStyle />
                            <PagerStyle />
                            <AlternatingItemStyle />
                            <ItemStyle />
                            <Columns>
                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="SZ_ACTIVITY_ROLE_ID" HeaderText="ACTIVITY ROLE ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_ROLE_NAME" HeaderText="ROLE NAME"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
