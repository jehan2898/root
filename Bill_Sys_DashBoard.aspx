<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_DashBoard.aspx.cs" Inherits="Bill_Sys_DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
        function OpenPage(obj)
        {
           // alert(obj);
        }
        
        function OpenReport(obj)
        {
            //alert(obj);
            document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value = obj;
            //alert(document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value);
            document.getElementById('_ctl0_ContentPlaceHolder1_btnSpecial').click();
            
        }
        
      
    </script>

    <div id="divDashBoard">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;">
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
                                            <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 99%; height: 130px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Missing Speciality</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    You have
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblMissingSpecialityText" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td style="width: 99%; height: 10px;" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Today's Appointment</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%">
                                                        Weekly &nbsp;Appointment</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart">
                                                        <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; vertical-align: top; float: left; position: relative;"
                                                visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Bill Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 33%;
                                                height: 195px; float: left; position: relative;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%;" valign="top" >
                                                        Desk</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have&nbsp;
                                                        <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Missing Information</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%;" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Report Section</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Procedure Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 33%;
                                                height: 195px; float: left; position: relative; left: 0px; top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Visits</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        <asp:Label ID="lblVisits" runat="server" visible="true"></asp:Label>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    You have
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <ul style="list-style-type: disc; padding-left: 60px;">
                                                                        <li>
                                                                            <a id="hlnkTotalVisit" href="#" runat="server">
                                                                            <asp:Label ID="lblTotalVisit" runat="server"></asp:Label></a>&nbsp;Total Visit
                                                                        </li>
                                                                        <li>
                                                                            <a id="hlnkBilledVisit" href="#" runat="server">
                                                                            <asp:Label ID="lblBilledVisit" runat="server"></asp:Label></a>&nbsp;Billed Visit
                                                                        </li>
                                                                        <li><a id="hlnkUnBilledVisit" href="#" runat="server">
                                                                            <asp:Label ID="lblUnBilledVisit" runat="server"></asp:Label></a>&nbsp;UnBilled Visit
                                                                        </li>
                                                                    </ul>
                                                                    <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="hlnkTotalVisit"
                                                                        PopupControlID="pnlTotalVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                                    <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="hlnkBilledVisit"
                                                                        PopupControlID="pnlBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                                    <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="hlnkUnBilledVisit"
                                                                        PopupControlID="pnlUnBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%; height: 10px;" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                                    <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Patient Visit Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblPatientVisitStatus" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                   
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
                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                </td>
                <td class="CenterBottom">
                </td>
                <td class="RightBottom" style="width: 10px">
                </td>
            </tr>
        </table>
    </div>
    <%--Total Visit--%>
    <asp:Panel ID="pnlTotalVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                        AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--Visit--%>
    <asp:Panel ID="pnlBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--UnVisit--%>
    <asp:Panel ID="pnlUnBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="btnSpecial" runat="server" OnClick="btnSpecial_Click" Text="Special"
        Width="0px" Height="0px" />
    <asp:HiddenField ID="hdnSpeciality" runat="server" />
</asp:Content>
