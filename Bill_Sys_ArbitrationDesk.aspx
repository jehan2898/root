<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ArbitrationDesk.aspx.cs" Inherits="Bill_Sys_ArbitrationDesk" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
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
                        <td class="TDPart" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td class="TDPart" style="text-align: center; height: 25px;" colspan="4">
                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" align="right">
                                        <table width="100%">
                                            <tr>
                                                <td width="50%" class="ContentLabel" style="text-align: left;">
                                                    <b>Arbitaration Desk</b></td>
                                                <td width="50%" align="right">
                                                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                        OnClick="btnExportToExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DataGrid ID="grdArbitarationDesk" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="I_DENIEL" HeaderText="Deniel"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_REMARK" HeaderText="Remark"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="false"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" Visible="false">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" DataFormatString="{0:0.00}"
                                                    ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="I_DENIEL" HeaderText="Deniel"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_REMARK" HeaderText="Remark"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="false"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" colspan="4">
                                        <asp:Button ID="btnMake2ndrequest" runat="server" Text="Make 2 nd Request" Width="130px"
                                            OnClick="btnMake2ndrequest_Click" CssClass="Buttons" />
                                        <asp:Button ID="btnArbitrate" runat="server" Text="Arbitrate" Width="80px" CssClass="Buttons" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
