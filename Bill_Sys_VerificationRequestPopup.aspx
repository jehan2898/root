<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_VerificationRequestPopup.aspx.cs"
    Inherits="Bill_Sys_VerificationRequestPopup" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function CheckedBillStatus()
        {
            var chkStatus = document.getElementById('extBillStatus').value;
            if(chkStatus == 'NA')
            {
                alert('Select Bill Status!!!');
                return false;
            }
            else
            {
                return true;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td class="lbl" valign="top">
                        Bill Number
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtViewBillNumber" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly = "true"></asp:TextBox>
                    </td>
                    <td class="lbl" valign="top">
                        Visit Date
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtVisitDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly = "true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Buttons" OnClick="btnDelete_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table width="100%">
                            <%--<tr>
                                <td align="right" valign="top">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Buttons" OnClick="btnDelete_Click" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="height: auto" valign="top">
                                    <asp:DataGrid ID="grdVerificationReq" runat="server" AutoGenerateColumns="false"
                                        Width="100%" OnItemCommand="grdVerificationReq_ItemCommand" CssClass="GridTable">
                                        <ItemStyle CssClass="GridRow" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <Columns>
                                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                                <ItemStyle CssClass="grid-item-left" />
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="TYPE" HeaderText="Type"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATE" HeaderText="Date"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="USER" HeaderText="User"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID" Visible="false"></asp:BoundColumn>
                                            
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <%--<td class="lbl" valign ="top">
                        Bill Number
                    </td>
                    <td valign ="top">
                        <asp:TextBox ID="txtViewBillNumber" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" Enabled="false"></asp:TextBox>
                    </td>--%>
                    <%--<td class="lbl" valign ="top">
                        Visit Date
                    </td>
                    <td valign ="top">
                        <asp:TextBox ID="txtVisitDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black"></asp:TextBox>
                    </td>--%>
                    <td class="lbl" valign="top">
                        Date
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtVerificationDate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                            BorderStyle="None" ForeColor="Black" ReadOnly = "true"> </asp:TextBox>
                    </td>
                    <%--<td align="right" valign="top" colspan="2">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="Buttons" OnClick="btnDelete_Click" />
                    </td>--%>
                </tr>
                <tr>
                    <td valign="top" class="lbl">
                        Notes
                    </td>
                    <td colspan="4" valign="top">
                        <asp:TextBox ID="txtVerificationNotes" runat="server" TextMode="MultiLine" Height="80px"
                            Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="lbl">
                        Bill Status
                    </td>
                    <td colspan="3" valign="top">
                        <extddl:ExtendedDropDownList ID="extBillStatus" Width="90%" runat="server" Connection_Key="Connection_String"
                            Procedure_Name="SP_MST_BILL_STATUS" Flag_Key_Value="GET_VERIFICATION_BILL_STATUS"
                            Selected_Text="--- Select ---" />
                        <%-- <extddl:ExtendedDropDownList ID="extddlDoctorType" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="--- Select ---" Visible="false" />--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="350px" align="right">
                        <asp:Button ID="btnSave" Text="Save" runat="Server" CssClass="Buttons" OnClick="btnSave_Click" />
                    </td>
                    <td colspan="2" width="350px" align="left">
                        <asp:Button ID="btnUpdate" Text="Update" runat="Server" CssClass="Buttons" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" Text="Clear" runat="Server" CssClass="Buttons" OnClick="btnClear_Click" />
                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="2%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
