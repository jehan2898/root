<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CheckoutPopup.aspx.cs"
    Inherits="Bill_Sys_CheckoutPopup" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
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
                        <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="80px" CssClass="Buttons"
                            OnClick="btnRemove_Click" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <div style="width: 100%;">
                            <asp:DataGrid ID="grdDoctorList" runat="Server" AutoGenerateColumns="False" CssClass="GridTable"
                                Width="95%" OnItemCommand="grdDoctorList_ItemCommand"   >
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <%--0--%>
                                    <asp:TemplateColumn HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <%--1--%>
                                    <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                    <%--2--%>
                                    <asp:BoundColumn DataField="VISIT_TYPE" HeaderText="Visit Type"></asp:BoundColumn>
                                    <%--3--%>
                                    <asp:BoundColumn DataField="STATUS" HeaderText="Status"></asp:BoundColumn>
                                    <%--4--%>
                                    <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="Event ID" Visible="False"></asp:BoundColumn>
                                    <%--Added bound colomns to get checkbox value--%>
                                    <%--5--%>
                                    <asp:BoundColumn DataField="CH_IE_REFERRAL" HeaderText="CH_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--6--%>
                                    <asp:BoundColumn DataField="OT_IE_REFERRAL" HeaderText="OT_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--7--%>
                                    <asp:BoundColumn DataField="PT_IE_REFERRAL" HeaderText="PT_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--8--%>
                                    <asp:BoundColumn DataField="VSNCT_IE_REFERRAL" HeaderText="VSNCT_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--9--%>
                                    <asp:BoundColumn DataField="SUPPLIES_IE_REFERRAL" HeaderText="SUPPLIES_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--10--%>
                                    <asp:BoundColumn DataField="CH_FU_REFERRAL" HeaderText="CH_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--11--%>
                                    <asp:BoundColumn DataField="OT_FU_REFERRAL" HeaderText="OT_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--12--%>
                                    <asp:BoundColumn DataField="PT_FU_REFERRAL" HeaderText="PT_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--13--%>
                                    <asp:BoundColumn DataField="VSNCT_FU_REFERRAL" HeaderText="VSNCT_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--14--%>
                                    <asp:BoundColumn DataField="SUPPLIES_FU_REFERRAL" HeaderText="SUPPLIES_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--15--%>
                                    <%--Added linkbuttons in the grid--%>
                                    <asp:TemplateColumn HeaderText="Link">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkChIEReferal" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CH_IE_REFERRAL_LINK")%>' Text='CH Referal' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkOTIEReferal" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.OT_IE_REFERRAL_LINK")%>' Text='OT Referal' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkPTIEReferal" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.PT_IE_REFERRAL_LINK")%>' Text='PT Referal' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkIEEMG" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.VSNCT_IE_REFERRAL_LINK")%>' Text='EMG' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkIESupplies" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SUPPLIES_IE_REFERRAL_LINK")%>' Text='Supplies' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            </br>
                                            <asp:LinkButton ID="lnkChFUReferal" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CH_FU_REFERRAL_LINK")%>' Text='CH Referal' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkOTFUReferal" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.OT_FU_REFERRAL_LINK")%>' Text='OT Referal' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkPTFUReferal" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.PT_FU_REFERRAL_LINK")%>' Text='PT Referal' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkFUEMG" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.VSNCT_FU_REFERRAL_LINK")%>' Text='EMG' CommandName="Open PDF">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkFUSupplies" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SUPPLIES_FU_REFERRAL_LINK")%>' Text='Supplies' CommandName="Open PDF">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                     <%--16--%>
                                    <asp:BoundColumn DataField="CH_IE_REFERRAL" HeaderText="CH_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--17--%>
                                    <asp:BoundColumn DataField="OT_IE_REFERRAL" HeaderText="OT_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--18--%>
                                    <asp:BoundColumn DataField="PT_IE_REFERRAL" HeaderText="PT_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--19--%>
                                    <asp:BoundColumn DataField="VSNCT_IE_REFERRAL" HeaderText="VSNCT_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--20--%>
                                    <asp:BoundColumn DataField="SUPPLIES_IE_REFERRAL" HeaderText="SUPPLIES_IE_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--21--%>
                                    <asp:BoundColumn DataField="CH_FU_REFERRAL" HeaderText="CH_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--22--%>
                                    <asp:BoundColumn DataField="OT_FU_REFERRAL" HeaderText="OT_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--23--%>
                                    <asp:BoundColumn DataField="PT_FU_REFERRAL" HeaderText="PT_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--24--%>
                                    <asp:BoundColumn DataField="VSNCT_FU_REFERRAL" HeaderText="VSNCT_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    <%--25--%>
                                    <asp:BoundColumn DataField="SUPPLIES_FU_REFERRAL" HeaderText="SUPPLIES_FU_REFERRAL"  Visible="False"></asp:BoundColumn>
                                    
                                </Columns>
                            </asp:DataGrid>
                            &nbsp;
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
