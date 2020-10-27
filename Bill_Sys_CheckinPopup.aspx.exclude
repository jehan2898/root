<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CheckinPopup.aspx.cs" Inherits="Bill_Sys_CheckinPopup" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css"rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td colspan="2" width="100%">
                        <asp:Label ID="lblCheckinMsg" runat="server" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2" align="center">
                        <table id="Table1" width="100%" runat="server">
                            <tr>
                                <td colspan="3" align="left">
                                    <asp:Label ID="lbl1" runat="server" Text="Completed" ForeColor="green" Visible="false"></asp:Label> 
                                    <asp:Label ID="lbl2" runat="server" Text="Not Completed" ForeColor="blue" Visible="false"></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left">
                                    <asp:Label ID="lblCompleted" runat="server" BackColor="Green"></asp:Label><br />
                                    <asp:Label ID="lblNotCompleted" runat="server" BackColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="50%" align="left">
                                    Date :
                                    <asp:Label ID="txtVisitDate" runat="server"></asp:Label>
                                </td>
                                <td align="left" width="50%" colspan="2">
                                    <asp:Label ID="lblSpeciality" Text="Speciality" runat="server"></asp:Label>
                                    <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                        Selected_Text="---Select---" Width="140px" StausText="true"></cc1:ExtendedDropDownList>
                                    </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnSearchSpeciality" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                            OnClick="btnSearchSpeciality_Click" />
                        <asp:Button ID="btnCheckinSave" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnCheckinSave_Click" /></td>
                </tr>
                
                <tr>
                    <td style="width: 100%">
                        <div style="width: 100%;">
                            <asp:DataGrid ID="grdDoctorList" runat="Server" AutoGenerateColumns="false" CssClass="GridTable"
                                Width="95%" OnItemDataBound="grdDoctorList_ItemDataBound">
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <%--0--%>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkVisit" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <%--1--%>
                                    <asp:BoundColumn DataField="CODE" HeaderText="SZ_DOCTOR_ID" Visible="false"></asp:BoundColumn>
                                    <%--2--%>
                                    <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Doctor Name"></asp:BoundColumn>
                                    <%--3--%>
                                    <asp:TemplateColumn HeaderText="Visit Type">
                                        <HeaderStyle HorizontalAlign="Left" Width="50px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="listVisitType" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>IE</asp:ListItem>
                                                <asp:ListItem>C</asp:ListItem>
                                                <asp:ListItem>FU</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
