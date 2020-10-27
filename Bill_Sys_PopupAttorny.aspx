<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_PopupAttorny.aspx.cs"
    Inherits="Bill_Sys_PopupAttorny" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divrd" runat="server">
            <table align="center" style="width: 400px; height: 200;">
                <tr>
                    <td class="ContentLabel" colspan="2">
                        Attorney Info</td>
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                        First Name:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblFirstName" runat="server" Width="250px"></asp:Label></td>
                    <td class="ContentLabel" style="width: 15%">
                        Last Name:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblLastName" runat="server" Width="250px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                        City:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblCity" runat="server" Width="250px"></asp:Label></td>
                    <td class="ContentLabel" style="width: 15%">
                        State:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblState" runat="server" Width="250px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                        Zip:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblZip" runat="server" Width="250px"></asp:Label></td>
                    <td class="ContentLabel" style="width: 15%">
                        Phone No:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblPhoneNo" runat="server" Width="250px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">
                        Fax:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblFax" runat="server" Width="250px"></asp:Label></td>
                    <td class="ContentLabel" style="width: 15%">
                        Email ID:</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblEmailID" runat="server" Width="250px"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div id="divupdate" runat="server">
            <table align="center">
                <tr>
                    <td colspan="4">
                        <UserMessage:MessageControl runat="server" id="usrMessage" />
                    </td>
                </tr>
                <tr>
                    <td class="ContentLabel" colspan="2">
                        Attorney Info</td>
                </tr>
                <tr>
                    <td class="ContentLabel">
                        First Name:</td>
                    <td style="width: 231px">
                        <asp:TextBox ID="txtFirstname" runat="server" ReadOnly="True" Width="224px" /></td>
                    <td class="ContentLabel">
                        Last Name:</td>
                    <td style="width: 208px">
                        <asp:TextBox ID="txtLastname" runat="server" ReadOnly="True" Width="199px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ContentLabel">
                        Address:
                    </td>
                    <td style="width: 231px">
                        <asp:TextBox ID="txtAddress" runat="server" Width="223px"></asp:TextBox>
                    </td>
                    <td class="ContentLabel">
                        City:</td>
                    <td style="width: 208px">
                        <asp:TextBox ID="txtCity" runat="server" Width="201px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ContentLabel">
                        State:</td>
                    <td style="width: 231px">
                        <cc1:ExtendedDropDownList ID="txtState" runat="server" Width="90%" Connection_Key="Connection_String"
                            Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---">
                        </cc1:ExtendedDropDownList></td>
                    <td class="ContentLabel">
                        Zip:</td>
                    <td style="width: 208px">
                        <asp:TextBox ID="txtZip" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ContentLabel">
                        Phone No:</td>
                    <td style="width: 231px">
                        <asp:TextBox ID="txtPhone" runat="server" Width="221px"></asp:TextBox>
                    </td>
                    <td class="ContentLabel">
                        Fax:</td>
                    <td style="width: 208px">
                        <asp:TextBox ID="txtFax" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ContentLabel">
                        Email ID:</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtEmailID" runat="server" Width="511px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdfid" runat="server" />
    </form>
</body>
</html>
