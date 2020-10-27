<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_ShowUnSentNF2.aspx.cs" Inherits="Bill_Sys_ShowUnSentNF2" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
        function Confirm_Send_Mail() {
            var f = document.getElementById("<%=grdUnsentNF2.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('ChkSent') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;


                        }

                    }
                }
            }

            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }
            else {
                var msg = "Do you want to proceed?";
                var result = confirm(msg);
                if (result == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function showPateintFrame(objCaseID, objCompanyID) {

            var url = "AJAX Pages/PatientViewFrame.aspx?CaseID=" + objCaseID + "&cmpId=" + objCompanyID + "";
            //alert(url);

            PatientInfoPop.SetContentUrl(url);
            PatientInfoPop.Show();
            return false;

        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="diveserch" style="vertical-align:top;" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">

                        <tr>
                            <td class="LeftCenter" style="height: 100%"></td>
                            <td class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <table style="width: 100%">
                                                     <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:UpdatePanel ID="pnlmsg" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false" />
                                                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>

                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td style="width: 100%;" align="left" colspan="2">Status<asp:DropDownList ID="ddlStatus" runat="server" Width="88px">
                                                        <asp:ListItem Value="0" Text="Un-Sent"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Sent"></asp:ListItem>
                                                    </asp:DropDownList>
                                                        &nbsp;&nbsp;<asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                            Visible="False"></asp:Label>&nbsp;<extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="100px" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false" />
                                                        <asp:Label ID="lblCaseType" runat="server" CssClass="lbl" Text="Case Type"> </asp:Label>
                                                        &nbsp;<extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100px" Connection_Key="Connection_String" Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---" Visible="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons" OnClick="btnSearch_Click" />
                                                        &nbsp;&nbsp;<asp:Button ID="btnSendMail" runat="server" Text="Send Mail" Width="80px" CssClass="Buttons" OnClick="btnSendMail_Click" />&nbsp;
                                                  &nbsp;<asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; height: 18px;">&nbsp;
                                                 <asp:Label ID="lblCount" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:DataGrid ID="grdUnsentNF2" Width="100%" runat="Server" CssClass="GridTable"
                                                            AutoGenerateColumns="False">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridRow" />
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="Sent">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkSent" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <%--<asp:TemplateColumn HeaderText="Case #">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>' CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>--%>
                                                                <asp:TemplateColumn HeaderText="#" Visible="false" SortExpression="convert(int,SZ_CASE_NO)">
                                                                    <ItemTemplate>
                                                                        <a target="_self" href='../AJAX Pages/Bill_Sys_CaseDetails.aspx?CaseID=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmp=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>'><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>
                                                                        <asp:LinkButton ID="lnkSelectCase" runat="server"  Visible="false"></asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <%--3--%>
                                                                <asp:TemplateColumn HeaderText="#" Visible="false" SortExpression="convert(int,SZ_CASE_NO)">
                                                                    <ItemTemplate>
                                                                        <a target="_self" href='../AJAX Pages/Bill_Sys_ReCaseDetails.aspx?CaseID=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmp=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>'><%# DataBinder.Eval(Container,"DataItem.SZ_RECASE_NO")%></a>
                                                                        <asp:LinkButton ID="lnkSelectRCase" runat="server" Visible="false"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPateintView" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>' Visible="false" CommandName="Patient" CommandArgument='<%# Eval("SZ_CASE_ID") + "," +  Eval("SZ_COMPANY_ID") %>'></asp:LinkButton>
                                                                        <a id="lnkframePatient" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'><%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>

                                                                <%--<asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>--%>
                                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Accident Date"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DAYS" HeaderText="Days"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="STATUS" HeaderText="Status"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="CLAIM_NO" HeaderText="Claim Number" Visible="false"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address"
                                                                    Visible="false"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Insurance Address"
                                                                    Visible="false"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id"
                                                                    Visible="false"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company Id"
                                                                    Visible="false"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_RECASE_NO" HeaderText="Case No"
                                                                    Visible="false"></asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="RightCenter" style="width: 10px; height: 100%;"></td>
                        </tr>
                        <tr>
                            <td class="LeftBottom"></td>
                            <td class="CenterBottom"></td>
                            <td class="RightBottom" style="width: 10px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divDashBoard" visible="false" style="position: absolute; width: 600px; height: 475px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
            <div style="position: relative; text-align: right; background-color: #8babe4;">
                <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;" title="Close">X</a>
            </div>
            <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 430; float: left; position: relative;">
                <tr>
                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%" valign="top">
                        <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="LeftTop"></td>
                                <td class="CenterTop"></td>
                                <td class="RightTop"></td>
                            </tr>
                            <tr>
                                <td class="LeftCenter" style="height: 100%"></td>
                                <td style="width: 97%" class="TDPart">
                                    <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%" valign="top">Today's Appointment</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="TDPart" valign="top">
                                                <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>

                                    <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%">Weekly &nbsp;Appointment</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="TDPart">
                                                <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>


                                    <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%" valign="top">Desk</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="TDPart" valign="top">You have&nbsp;
                                        <asp:Label ID="lblDesk" runat="server"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>


                                    <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%" valign="top">Bill Status</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>



                                    <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%" valign="top">Missing Information</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%;" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>

                                    <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%" valign="top">Report Section</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>


                                    <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%" valign="top">Procedure Status</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>
                                    <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height: 140px; float: left; position: relative; left: 0px; top: 0px;" visible="false">
                                        <tr>
                                            <td class="TDHeading" style="width: 99%" valign="top">Visits</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblVisits" runat="server"></asp:Label>


                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider"></td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="RightCenter" style="width: 10px; height: 100%;"></td>
                            </tr>
                            <tr>
                                <td class="LeftBottom"></td>
                                <td class="CenterBottom"></td>
                                <td class="RightBottom" style="width: 10px"></td>
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
    </div>
    <dx:ASPxPopupControl ID="PatientInfoPop" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="PatientInfoPop" HeaderText="Patient Information" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="800px" ToolTip="Patient Information" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
        RenderIFrameForPopupElements="Default" Height="500px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
