<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_WeekCalendarEvents.aspx.cs" Inherits="Bill_Sys_WeekCalendarEvents" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    
                    <tr>
                        <td class="Center" valign="top" width="100%" colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td style="vertical-align: top; height: 30px; width: 55%;">
                                                    &nbsp;</td>
                                                <td style="vertical-align: top; height: 30px; width: 64%;" align="right">
                                                    &nbsp;<asp:Label ID="lblReferringFacility" runat="server">Test Facility </asp:Label></td>
                                                <td style="vertical-align: top; height: 30px; width: 38%;">&nbsp;&nbsp;&nbsp;
                                                <extddl:ExtendedDropDownList id="extddlReferringFacility" runat="server" Width="150px" Selected_Text="--- Select ---" Procedure_Name="SP_TXN_REFERRING_FACILITY" Flag_Key_Value="REFERRING_FACILITY_LIST" Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extddlReferringFacility_extendDropDown_SelectedIndexChanged" AutoPost_back="true"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td style="vertical-align: top; height: 30px;" width="35%">
                                                  &nbsp;&nbsp;&nbsp;  <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="Buttons" Visible="false"/></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td style="vertical-align: top;" width="14%">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <contenttemplate>
						                                        <asp:Panel ID="Panel1" runat="server">
						                                        </asp:Panel>
					                                    </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="text-align: left;" width="86%">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <contenttemplate>
		                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
		                                                        <asp:Panel ID="Panel2" runat="server">
						
						                                        </asp:Panel>
						
	                                                    </contenttemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdateProgress ID="updateProgressOne" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                        <progresstemplate>
				                                            <img src="Images/bigrotation2.gif" />
				                                        </progresstemplate>
                                                    </asp:UpdateProgress>
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
        </tr>
    </table>
</asp:Content>

