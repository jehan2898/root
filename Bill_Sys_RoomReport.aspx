<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_RoomReport.aspx.cs" Inherits="Bill_Sys_RoomReport" %>
 
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

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
                                        <asp:Label ID="lblScheduleID" runat="server" Font-Size="Small"></asp:Label>
                                        <table width="100%" border="0" align="center" class="ContentTable" cellpadding="0"
                                            cellspacing="3">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <b> Schedule visits </b>
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" visible="true" style="color: Red;">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 95px">
                                                                From Date
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                                                <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:calendarextender id="calFromDate" runat="server" targetcontrolid="txtFromDate"
                                                                    popupbuttonid="imgFromDate" />
                                                            </td>
                                                            <td>
                                                                To Date
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                                                <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtToDate"
                                                                    popupbuttonid="imgToDate" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 95px">
                                                                Room
                                                            </td>
                                                            <td>
                                                                <extddl:extendeddropdownlist id="extddlVisitRoom" runat="server" width="150px" connection_key="Connection_String"
                                                                    flag_key_value="ROOM_LIST" procedure_name="SP_MST_ROOM" selected_text="---Select---" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 95px">
                                                                <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                                    Visible="False" Width="94px"></asp:Label></td>
                                                            <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="74%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                    <td style="width: 100%" class="TDPart" align="right">
                                    <asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                                    </td> 
                                    </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdScheduleReport" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <FooterStyle />
                                            <SelectedItemStyle />
                                            <PagerStyle />
                                            <AlternatingItemStyle />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left"
                                                    Visible="false"></asp:ButtonColumn>
                                                <asp:BoundColumn DataField="Date Of Visit" HeaderText="Date Of Visit" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                                               
                                                <asp:BoundColumn DataField="Name Of Patient" HeaderText="Name Of Patient"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="type" HeaderText=""></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Referred By" HeaderText="Referred By"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DOC #" HeaderText="DOC #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Office Location" HeaderText="Office Location"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Office Phone" HeaderText="Office Phone"></asp:BoundColumn>
                                                
                                                <asp:BoundColumn DataField="Remarks" HeaderText="Remarks" >
                                                </asp:BoundColumn>
                                               
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        
                                        <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" Visible="false"  >
                                            <FooterStyle />
                                            <SelectedItemStyle />
                                            <PagerStyle />
                                            <AlternatingItemStyle />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left"
                                                    Visible="false"></asp:ButtonColumn>
                                                <asp:BoundColumn DataField="Date Of Visit" HeaderText="Date Of Visit" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                                               
                                                <asp:BoundColumn DataField="Name Of Patient" HeaderText="Name Of Patient"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="type" HeaderText=""></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Referred By" HeaderText="Referred By"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DOC #" HeaderText="DOC #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Office Location" HeaderText="Office Location"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Office Phone" HeaderText="Office Phone"></asp:BoundColumn>
                                                
                                                <asp:BoundColumn DataField="Remarks" HeaderText="Remarks" >
                                                </asp:BoundColumn>
                                               
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
    
</asp:Content>
