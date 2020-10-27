<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_DBProcedureReport.aspx.cs" Inherits="Bill_Sys_DBProcedureReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
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
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td style="text-align: left; height: 25px;" colspan="4">
                                                    <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server">Dash board</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        &nbsp;
                                        <asp:DataGrid ID="grdAllReports" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                            CssClass="GridTable" OnPageIndexChanged="grdAllReports_PageIndexChanged" PagerStyle-Mode="NumericPages"
                                            PageSize="10" Width="100%">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" DataFormatString="{0:MM/dd/yyyy}"
                                                    HeaderText="Date Of Service"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE" HeaderText="Procedure code"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Procedure code description">
                                                </asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid></td>
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
    <div id="divDashBoard" visible="false" style="position: absolute; width: 600px; height: 475px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;" title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 430">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <td class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="Center" valign="top" width="45%">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                <tr>
                                                    <td class="TDHeading" style="width: 100%">
                                                        Today's Appointment</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="TDPart">
                                                        <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="Center" width="45%" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                <tr>
                                                    <td class="TDHeading" style="width: 100%">
                                                        Weekly &nbsp;Appointment</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="TDPart">
                                                        <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Center" valign="top" width="45%">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                <tr>
                                                    <td class="TDHeading" style="width: 100%">
                                                        Bill Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="TDPart">
                                                        You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="Center" width="45%" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                <tr>
                                                    <td class="TDHeading" style="width: 100%">
                                                        Desk</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="TDPart">
                                                        You have &nbsp;
                                                        <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Center" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                <tr>
                                                    <td class="TDHeading" style="width: 100%">
                                                        Missing Information</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="TDPart">
                                                        You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
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
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px" visible="false"></iframe>
    </div>
</asp:Content>
