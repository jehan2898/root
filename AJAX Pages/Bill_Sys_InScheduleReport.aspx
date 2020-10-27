<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_InScheduleReport.aspx.cs" Inherits="Bill_Sys_InScheduleReport" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../validation.js"></script>

    <script language="javascript" type="text/javascript">
       
       function openTypePage(obj)
       {
            
            document.getElementById('divid').style.position = 'absolute'; 
            document.getElementById('divid').style.left= '450px'; 
            document.getElementById('divid').style.top= '250px'; 
            document.getElementById('divid').style.visibility='visible'; 
            document.getElementById('frameeditexpanse').src="ViwScheduled.aspx?id=" + obj ;
       }
       
       function ShowDiv()
       {
            document.getElementById('divDashBoard').style.position = 'absolute'; 
            document.getElementById('divDashBoard').style.left= '150px'; 
            document.getElementById('divDashBoard').style.top= '100px'; 
            document.getElementById('divDashBoard').style.visibility='visible'; 
            return false;
       }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table width="100%" border="0" align="center" class="ContentTable" cellpadding="0"
                                            cellspacing="3">
                                            <tr>
                                                <td class="tablecellLabel" style="text-align: right;">
                                                     <div class="lbl">Start Date</div>
                                                </td>
                                                <td class="tablecellSpace"></td>
                                                <td class="tablecellControl">
                                                
                                                <asp:TextBox ID="txtStartDate" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                                    <asp:ImageButton ID="imgStartDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate"
                                                        PopupButtonID="imgStartDate" />
                                                </td>
                                                <td class="tablecellLabel" style="text-align: right;">
                                                <div class="lbl">End Date</div>
                                                </td>
                                                <td class="tablecellSpace"></td>
                                                <td class="tablecellControl">
                                                 <asp:TextBox ID="txtEndDate" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate"
                                                        PopupButtonID="imgbtnEndDate" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel" style="text-align: right">
                                                    <div class="lbl">Doctor</div></td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                  <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="180px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                                                        AutoPost_back="false" />
                                                </td>
                                                <td class="tablecellLabel" style="text-align: right">
                                                   <div class="lbl">Status</div></td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="180px">
                                                    <asp:ListItem Value="NA">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="0">Visit Not Completed</asp:ListItem>
                                                    <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                                                    <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                                                    <asp:ListItem Value="3">No Show</asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="6" align="right">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnGenerateReport" runat="server" Text="Show" Width="150px" OnClick="btnGenerateReport_Click"
                                                        CssClass="Buttons" />&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 100%" class="TDPart" align="right" >
                                    <asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                                    </td> 
                                    </tr> 
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdScheduleReport" runat="server" 
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                            PageSize="10" PagerStyle-Mode="NumericPages" OnPageIndexChanged="grdScheduleReport_PageIndexChanged"
                                             >
                                            <FooterStyle />
                                            <SelectedItemStyle />
                                            <PagerStyle />
                                            <AlternatingItemStyle />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case#" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PATIENT NAME" HeaderText="Patient Name" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DOCTOR NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="START TIME" HeaderText="Start Time"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="END TIME" HeaderText="End Time"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="STATUS" HeaderText="Status"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PROCEDURE CODE" HeaderText="Treated Codes"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        
                                        
                                        <asp:DataGrid ID="grdForReport" runat="server" 
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" Visible="false">
                                            <FooterStyle />
                                            <SelectedItemStyle />
                                            <PagerStyle />
                                            <AlternatingItemStyle />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case#" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PATIENT NAME" HeaderText="Patient Name" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DOCTOR NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="START TIME" HeaderText="Start Time"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="END TIME" HeaderText="End Time"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="STATUS" HeaderText="Status"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PROCEDURE CODE" HeaderText="Treated Codes"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
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
    <div id="divid" style="position: absolute; width: 350px; height: 200px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divid').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="200px" width="350px"></iframe>
    </div>
   <div id="divDashBoard" visible="false" style="position: absolute; width: 600px; height: 480px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%" valign="top">
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
                                           You have &nbsp; <asp:Label ID="lblDesk" runat="server"></asp:Label>
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
</asp:Content>
