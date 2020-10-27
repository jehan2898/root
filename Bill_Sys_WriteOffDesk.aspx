<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_WriteOffDesk.aspx.cs" Inherits="WriteOffDesk" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="TDPart" valign="top" >
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td width="100%">
                                         <table width="100%">
                                            <tr>
                                                <td width="50%" class="ContentLabel" style="text-align:left;"><b>Write Off Desk</b></td>
                                            
                                                <td width="50%" align="right">
                                                       
                                                    <asp:TextBox ID="txtltID" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                            OnClick="btnExportToExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" >
                                        <asp:DataGrid ID="grdWriteOffDesk" runat="server" AutoGenerateColumns="false" Width="100%"
                                            CssClass="GridTable" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:BoundColumn DataField="BILL NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                                <%--1--%>
                                                <asp:BoundColumn DataField="CASE ID" HeaderText="File Number" Visible="false"></asp:BoundColumn>
                                                <%--2--%>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <%--3--%>
                                                <asp:BoundColumn DataField="INSURANCE COMPANY" HeaderText="Insurance Company" ItemStyle-HorizontalAlign="Left">
                                                </asp:BoundColumn>
                                                <%--4--%>
                                                <asp:BoundColumn DataField="BILL AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <%--5--%>
                                                <asp:BoundColumn DataField="PAID AMOUNT" HeaderText="Paid Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="WRITE OFF AMOUNT" HeaderText="Write Off Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="REASON" HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                                                </asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        <asp:DataGrid ID="grdForReport" runat="server" AutoGenerateColumns="false" Width="100%"
                                            CssClass="GridTable" Visible="false">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="BILL NUMBER" HeaderText="Bill Number" ItemStyle-HorizontalAlign="Center">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASE ID" HeaderText="File Number" ItemStyle-HorizontalAlign="Center" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="INSURANCE COMPANY" HeaderText="Insurance Company" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BILL AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PAID AMOUNT" HeaderText="Paid Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="WRITE OFF AMOUNT" HeaderText="Write Off Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="REASON" HeaderText="Reason" ItemStyle-HorizontalAlign="Left">
                                                </asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
        <tr>
            <td class="LeftBottom">
            </td>
            <td class="CenterBottom">
            </td>
            <td class="RightBottom" style="width: 10px">
            </td>
        </tr>
        </table> </td> </tr>
    </table>
</asp:Content>
