<%@ Page Title="" Language="C#" MasterPageFile="~/linkLessMasterPage.master" AutoEventWireup="true"
    CodeFile="Appointment.aspx.cs" Inherits="AJAX_Pages_Appointment" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        var postponedCallbackRequired = false;
        function OnAppointmentClick(eventID, caseID, companyID, patientID, ReffComp, RoomID, Value, sRefOfficeID, appointmentTime, sEventDate) {
            //var url = 'ViewAppointment.aspx?evt=' + eventID + '&case=' + caseID + '&company=' + companyID + '&patient=' + patientID;
            var url = 'ScheduleAppointment.aspx?evt=' + eventID + '&case=' + caseID + '&company=' + companyID + '&patient=' + patientID + '&reffCompany=' + ReffComp + '&RoomID=' + RoomID + '&flag=' + Value +'&RefOffID=' + sRefOfficeID + '&appointmentTime=' + appointmentTime + '&appointmentDate=' + sEventDate;
            IFrame_Appointment.SetContentUrl(url);
            IFrame_Appointment.Show();
            //return false;
        }

        function deleteAppointment(sName,eventID) {
            if (confirm('Are you sure you want to delete the appointment for patient - ' + sName)) {
                document.getElementById("<%=hdnDeleteEvent.ClientID %>").value = eventID;
                document.getElementById("<%=btnDeleteEvent.ClientID %>").click();
                
            }
            return false;
        }
        function SendReminder(sName,eventID) {
            if (confirm('Are you sure you want to send reminder for patient - ' + sName)) {
                document.getElementById("<%=hdnDeleteEvent.ClientID %>").value = eventID;
                document.getElementById("<%=btnReminder.ClientID %>").click();
                
            }
            return false;
        }

        function AddAppointmentClick(reffCompanyID, roomID, sRefOfficeID, appointmentTime, sEventDate, sTestFacility) {
            var url = ''
            
            if (sTestFacility  == '') {
                url = 'ScheduleAppointment.aspx?reffCompany=' + reffCompanyID + '&RoomID=' + roomID + '&RefOffID=' + sRefOfficeID + '&appointmentTime=' + appointmentTime + '&appointmentDate=' + sEventDate;
            }
            else {
                url = 'ScheduleAppointment.aspx?reffCompany=' + reffCompanyID + '&RoomID=' + roomID + '&RefOffID=' + sRefOfficeID + '&appointmentTime=' + appointmentTime + '&appointmentDate=' + sEventDate + '&testFacility=' + sTestFacility;
            }
            IFrame_Appointment.SetContentUrl(url);
            IFrame_Appointment.Show();
            //return false;
        }

        function performCallback(vl) {
            alert(vl);
            if (cbpCalendar.InCallback())
                postponedCallbackRequired = true;
            else
                cbpCalendar.PerformCallback();
        }

        function OnEndCallback(s, e) {
            if (postponedCallbackRequired) {
                cbpCalendar.PerformCallback();
                postponedCallbackRequired = false;
            }
        }
        function checkForTestFacility() {
            if (cddlFacility.GetVisible()) {
                var value = cddlFacility.GetValue();
                if (value == null) {
                    alert('Please select Test Facility');
                    return false;
                }
                else {
                    return true;
                }
            }           
        }
    </script>
    <style type="text/css">
        .hldel
        {
            color: Red;
            text-decoration: none;
        }
        
        .imgsize
        {
            width: 10px;
            height: 10px;
        }
    </style>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="height: 19px">
                            <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="tblPageHolder" width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Table runat="server" ID="tblRoomHolder" Width="100%">
                            </asp:Table>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="width: 23%;">
                            <asp:Label ID="lblTestFacility" runat="server" Text="Test Facility :" Font-Size="11px"></asp:Label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbFacility" runat="server" ClientInstanceName="cddlFacility">
                                <ItemStyle>
                                    <HoverStyle BackColor="#F6F6F6">
                                    </HoverStyle>
                                </ItemStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 77%;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date:
                        </td>
                        <td>
                            <dx:ASPxDateEdit runat="server" ClientInstanceName="cntdtappointmentdate" CssClass="inputBox"
                                ID="AppointmentDate">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="width: 70%;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Time Interval:
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbInterval" runat="server" ClientInstanceName="cddlInterval">
                                <Items>
                                    <dx:ListEditItem Text="0.15" Value="0" />
                                    <dx:ListEditItem Text="0.30" Value="1" Selected="true" />
                                    <dx:ListEditItem Text="0.45" Value="2" />
                                    <dx:ListEditItem Text="0.60" Value="3" />
                                </Items>
                                <ItemStyle>
                                    <HoverStyle BackColor="#F6F6F6">
                                    </HoverStyle>
                                </ItemStyle>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 70%;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnLoadAppointment" OnClick="btnLoadAppointment_Click" runat="server"
                                Width="30px" Text="Go"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div id="DivStatus11" runat="Server">
                                        <asp:Image ID="img1" AlternateText="Loading....." runat="server" ImageUrl="~/images/rotation.gif" />
                                        Loading....
                                        <%-- <img id="img1" alt="Loading. Please wait..." ImageUrl="~/Images/rotation.gif" /> Loading...--%>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblAppointment" runat="server" Text="" Font-Size="Medium" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            Visit Status :
                        </td>
                        <td>
                            <asp:Image ID="imgRed" runat="server" ImageUrl="~/AJAX Pages/Images/red.jpg" />
                        </td>
                        <td>
                            No Show
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/AJAX Pages/Images/green.jpg" />
                        </td>
                        <td>
                            Completed
                        </td>
                        <td>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/AJAX Pages/Images/gray.jpg" />
                        </td>
                        <td style="width: 70%;">
                            Reschedule.
                        </td>
                    </tr>
                </table>
                <dx:ASPxPopupControl ID="IFrame_Appointment" runat="server" CloseAction="CloseButton"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="IFrame_Appointment" HeaderText="Patient Appointment" HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
                    EnableAnimation="False" EnableViewState="True" Width="900px" ToolTip="Patient Appointment"
                    PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
                    ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="600px">
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>
                <dx:ASPxCallbackPanel ID="cbpCalendar" ClientInstanceName="cbpCalendar" runat="server"
                    OnCallback="cbpCalendar_Callback">
                    <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent3" runat="server">
                            <asp:HiddenField runat="server" ID="txtGetDay" />
                            <asp:TextBox ID="txtEnteredDate" runat="server" Visible="false"></asp:TextBox>
                            <dx:ASPxSplitter ID="spltCalendar" runat="server" Height="600px" Width="100%" ClientInstanceName="spltCalendar">
                                <Styles>
                                    <Pane>
                                        <Paddings Padding="0px" />
                                    </Pane>
                                </Styles>
                                <Panes>
                                    <dx:SplitterPane Size="20%" Name="contCalendars" ShowCollapseBackwardButton="True">
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                                <asp:Label runat="server" ID="idLabel" Text="Select Month:"></asp:Label>
                                                <asp:DropDownList ID="ddlMonthList" runat="server" Width="55px">
                                                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="Sept"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlYearList" runat="server">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnLoad" OnClick="btnLoadCalendar_Click" runat="server" Width="50px"
                                                    Text="Load"></asp:Button>
                                                <asp:Panel ID="Panel1" runat="server">
                                                </asp:Panel>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                    <dx:SplitterPane Size="80%" Name="contScheduler" ShowCollapseBackwardButton="True"
                                        ScrollBars="Auto">
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                                <dx:ASPxGridView Width="100%" OnDataBound="grvAppointments_DataBound" OnHtmlRowCreated="grvAppointments_HtmlRowCreated"
                                                    runat="server" AutoGenerateColumns="false" ID="grvAppointments">
                                                    <Settings 
                                                        ShowVerticalScrollBar="true" 
                                                        VerticalScrollBarStyle="Standard" 
                                                        VerticalScrollableHeight="560" />
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsBehavior AllowSort="false" AllowGroup="false" />
                                                    <SettingsBehavior ColumnResizeMode="NextColumn" />
                                                </dx:ASPxGridView>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                </Panes>
                            </dx:ASPxSplitter>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnTestFacilty" runat="server" />
        <asp:HiddenField ID="hdnEventDate" runat="server" />
        <asp:HiddenField ID="hdnDate" runat="server" />
        <asp:HiddenField ID="hdnDeleteEvent" runat="server" />
        <asp:HiddenField ID="hdnInterval" runat="server" />
        <asp:Button ID="btnDeleteEvent" runat="server" OnClick="btnDeleteEvent_Click" />
        <asp:Button ID="btnReminder" runat="server" OnClick="btnReminder_Click" />
    </div>
</asp:Content>
