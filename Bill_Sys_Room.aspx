<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_Room.aspx.cs" Inherits="Bill_Sys_Room" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
     <script type="text/javascript">
         function ConfirmDelete() {
             var msg = "Do you want to proceed?";
             var result = confirm(msg);
             if (result == true) {
                 return true;
             }
             else {
                 return false;
             }
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
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>                                                
                                                <td class="ContentLabel">
                                                    Room Name</td>
                                                <td class="ContentLabel">
                                                    <asp:TextBox ID="txtRoomName" runat="server" CssClass="textboxCSS" 
                                                        MaxLength="50" Width="182px"></asp:TextBox></td>
                                                <td class="ContentLabel">
                                                    Speciality</td>
                                                <td class="ContentLabel">
                                                    <cc1:extendeddropdownlist id="extddlProCodeGroup" runat="server" connection_key="Connection_String"
                                                        flag_key_value="GET_PROCEDURE_GROUP_LIST" procedure_name="SP_MST_PROCEDURE_GROUP"
                                                        selected_text="--- Select ---" width="90%"></cc1:extendeddropdownlist>
                                                </td>                                               
                                                
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    &nbsp;</td>                                                
                                               
                                            </tr>
                                            <tr>                                                
                                                <td class="ContentLabel" style="font-weight: bold">
                                                    Days</td>
                                                <td class="ContentLabel" style="font-weight: bold">
                                                    Treatment Start Time</td>
                                                <td class="ContentLabel">                                                   
                                                </td>
                                                <td class="ContentLabel" style="font-weight: bold" >
                                                    Treatment End Time</td>                                               
                                               
                                            </tr>
                                            <tr>                                            
                                                
                                                <td class="ContentLabel" nowrap="nowrap"> 
                                                    <asp:Label ID="lblMon" runat="server" Text="Monday" CssClass="ContentLabel"></asp:Label></td>
                                                <td class="ContentLabel">
                                                            <asp:DropDownList ID="ddlHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         <asp:DropDownList ID="ddlEndHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlEndMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlEndTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                               
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    <asp:Label ID="lblTus" runat="server" Text="Tuesday" CssClass="ContentLabel"></asp:Label></td>
                                                <td class="ContentLabel">
                                                            <asp:DropDownList ID="ddlTuesHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTuesMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTuesTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         <asp:DropDownList ID="ddlTuesEndHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTuesEndMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTuesEndTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                               
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    <asp:Label ID="lblWen" runat="server" Text="Wednesday" CssClass="ContentLabel"></asp:Label></td>
                                                <td class="ContentLabel">
                                                            <asp:DropDownList ID="ddlWednHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlWednMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlWednTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         <asp:DropDownList ID="ddlWednEndHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlWednEndMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlWednEndTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                                
                                                <td class="ContentLabel" nowrap="nowrap">
                                                   <asp:Label ID="lblThus" runat="server" Text="Thursday" CssClass="ContentLabel"></asp:Label> </td>
                                                <td class="ContentLabel">
                                                            <asp:DropDownList ID="ddlThusHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlThusMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlThusTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         <asp:DropDownList ID="ddlThusEndHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlThusEndMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlThusEndTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                               
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    <asp:Label ID="lblFri" runat="server" Text="Friday" CssClass="ContentLabel"></asp:Label> </td>
                                                <td class="ContentLabel">
                                                            <asp:DropDownList ID="ddlFridHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlFridMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlFridTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         <asp:DropDownList ID="ddlFridEndHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlFridEndMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlFridEndTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                                
                                                <td class="ContentLabel" nowrap="nowrap">
                                                   <asp:Label ID="lblSat" runat="server" Text="Saturday" CssClass="ContentLabel"></asp:Label> </td>
                                                <td class="ContentLabel">
                                                            <asp:DropDownList ID="ddlSatHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSatMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSatTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         <asp:DropDownList ID="ddlSatEndHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSatEndMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSatEndTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                                
                                                <td class="ContentLabel" nowrap="nowrap">
                                                   <asp:Label ID="lblSun" runat="server" Text="Sunday" CssClass="ContentLabel"></asp:Label> </td>
                                                <td class="ContentLabel">
                                                            <asp:DropDownList ID="ddlSunHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSunMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSunTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         <asp:DropDownList ID="ddlSunEndHours" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSunEndMinutes" runat="server"  Width="40px">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSunEndTime" runat="server"  Width="45px">
                                                            </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>                                            
                                                
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                            &nbsp;</td>
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         &nbsp;</td>
                                            </tr>
                                              <tr>                                            
                                                
                                                <td class="ContentLabel" style="font-weight: bold">
                                                    Holidays</td>
                                                <td class="ContentLabel">
                                                            &nbsp;</td>
                                                <td class="ContentLabel" >
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         &nbsp;</td>
                                            </tr>    
                                            <tr>                                                
                                                <td class="ContentLabel" nowrap="nowrap">
                                                    Holiday Date</td>
                                                <td class="ContentLabel">
                                                    <asp:TextBox ID="txtHoliday" runat="server" ></asp:TextBox>
                                                     <asp:ImageButton ID="imgStartDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtHoliday"
                                                        PopupButtonID="imgStartDate" />
                                                   <asp:Button ID="btnAddHolidays" runat="server" Text="Add" Width="30px" 
                                                        CssClass="Buttons" onclick="btnAddHolidays_Click" /> 
                                                    </td>
                                                <td class="ContentLabel">
                                                    Holidays List</td>
                                                <td class="ContentLabel"> 
                                                <asp:ListBox ID="lstBoxHoliday" runat="server" Width="182px" Height="50px"></asp:ListBox>                                                  
                                                </td>                                               
                                                
                                            </tr> 
                                            <tr>                                            
                                                
                                                <td class="ContentLabel">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                   
                                                        </td>
                                                <td class="ContentLabel">
                                                    &nbsp;</td>
                                                <td class="ContentLabel">
                                                         
                                                </td>
                                            </tr>
                                         </table>
                                    </td>
                                    </tr>
                                    
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align:right; height: 44px;">
                                     <asp:TextBox ID="txtRoomID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                     <asp:TextBox ID="txtStartTime" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtEndTime" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click"
                                                        CssClass="Buttons" />
                                                    &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click"
                                                        CssClass="Buttons" />
                                                    &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                        CssClass="Buttons" />
                                    &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdRoomList" runat="server" OnSelectedIndexChanged="grdRoomList_SelectedIndexChanged"
                                            OnPageIndexChanged="grdRoomList_PageIndexChanged" OnItemCommand="grdRoomList_ItemCommand"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                            PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_ROOM_ID" HeaderText="ROOM ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ROOM_NAME" HeaderText="Room Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn> 
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Room Name" Visible="false"></asp:BoundColumn> 
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false" ></asp:ButtonColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="FL_START_TIME" HeaderText="STARTTIME" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="FL_END_TIME" HeaderText="ENDTIME" Visible="False"></asp:BoundColumn>
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
