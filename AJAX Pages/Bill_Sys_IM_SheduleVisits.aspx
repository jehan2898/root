<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_IM_SheduleVisits.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_IM_SheduleVisits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Schedule Visits</title>

    <script language="Javascript" type="text/javascript">
    </script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td align="center" style="font-size: 16px;" valign="middle" class="form-head" height="40">
                        <strong>Schedule Visits</strong></td>
                </tr>
                <tr>
                    <td class="TDPart">
                        <table id="tblTestInformation" runat="server" style="width: 100%">
                            <tr>
                                <td align="center" valign="top">
                                    <asp:DataGrid ID="grdTestInformation" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" Width="100%">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditItemStyle BackColor="#2461BF" />
                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <%--0--%>
                                            <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="true"
                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                            <%--1--%>
                                            <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" ReadOnly="true"
                                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                            <%--2--%>
                                            <asp:BoundColumn DataField="SZ_TREATMENT DATE" HeaderText="Treatment Date" ItemStyle-HorizontalAlign="Left"
                                                DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#B5DF82" Font-Bold="True" ForeColor="White" />
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
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
