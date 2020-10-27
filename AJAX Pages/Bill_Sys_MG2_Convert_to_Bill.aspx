<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_MG2_Convert_to_Bill.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_MG2_Convert_to_Bill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Convert to Bill</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 144px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Visit Date"></asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtVisitDt" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="ImgDate" runat="server" ImageUrl="~/Images/cal.gif" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtVisitDt"
                        PopupButtonID="ImgDate" Enabled="True" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Procedures"></asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtProcedureCodes" runat="server" TextMode="MultiLine" 
                                    Width="333px" Text ="97601 - Removal of devitalized tissues, 97112 - Balance Coord Postur"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text ="Doctor Name"></asp:Label></td>
                            <td class="style2">
                                <asp:DropDownList ID="ddlDoctor" runat="server" 
                                    Width="97%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVisitType" runat="server" Text ="Visit Type"></asp:Label>
                            </td>
                            <td class="style2">
                                <extddl:ExtendedDropDownList ID="extddlVisitType" runat="server" Width="200px" AutoPost_back="false"
                                Selected_Text="---Select---" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE"
                                Connection_Key="Connection_String"></extddl:ExtendedDropDownList></td>
                        </tr>                        
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="Button1" runat="server" Text="Add" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
