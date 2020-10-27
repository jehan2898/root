<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_Reminders.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Reminders" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register TagPrefix="axp" Namespace="Axezz.WebControls" Assembly="AxpDBNet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
     <script type="text/javascript" src="validation.js"></script>
     
     <script language="Javascript" type="text/javascript">
     
     
     function chk()	
	{	
	    var f= document.getElementById("ctl00_ContentPlaceHolder1_tabcontainerAllInformation_tabpnlReminderOne_grdCreatedReminder");	
		for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		{		
		    if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			{						
			    if(f.getElementsByTagName("input").item(i).checked==true)
			    {	
			        document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerAllInformation_tabpnlReminderOne_btnDelete').style.visibility="visible";			        	    
			        return;
			    }	
			    else
			    {
			        document.getElementById('ctl00_ContentPlaceHolder1_tabcontainerAllInformation_tabpnlReminderOne_btnDelete').style.visibility="hidden";			        
			    }			        				
			}			
		}
	} 
     
     
     
     </script>
     
     
    <div>
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop" style="height: 10px">
                        </td>
                        <td class="CenterTop" style="height: 10px">
                        </td>
                        <td class="RightTop" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td valign="top" class="TDPart">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                                <tr>
                                    <td width="100%" colspan="6">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" width="100%" height="10px">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="6">
                                        <ajaxToolkit:TabContainer ID="tabcontainerAllInformation" runat="Server" ActiveTabIndex="0"
                                            CssClass="ajax__tab_theme" Width="900px" >
                                            <ajaxToolkit:TabPanel ID="tabpnlReminderOne" runat="server" TabIndex="0">
                                                <HeaderTemplate>
                                                    <div style="width: 120px; height: 300px;" class="lbl">
                                                        Reminder
                                                    </div>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <div style="vertical-align: top;">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: top;">
                                                            <tr>
                                                                <td style="width: 20%" align="right">
                                                                </td>
                                                                <td style="width: 13%" align="right" class="lbl">
                                                                    Reminder Status
                                                                </td>
                                                                <td style="width: 18%">
                                                                    <extddl:ExtendedDropDownList ID="extddlSearchReminderStatus" runat="server" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="GET_REMINDER_STATUS_LIST" Procedure_Name="SP_GET_LIST_MST_REMINDER_STATUS"
                                                                        Selected_Text="---Select---" Width="160px" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                </td>
                                                                <td style="width: 12%" align="right">
                                                                    Reminder Date
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtReminderDate" runat="server" MaxLength="10" Width="100px" CssClass="textbox"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnReminderDate" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" Width="80px" CssClass="Buttons"
                                                                        OnClick="btnFilter_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%; height: 5px;" colspan="6">
                                                                    <ajaxToolkit:CalendarExtender ID="calReminderDate" runat="server" TargetControlID="txtReminderDate"
                                                                        PopupButtonID="imgbtnReminderDate" Enabled="True" PopupPosition="BottomRight" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="width: 100%">
                                                                    <asp:Label ID="lblHeaderOne" runat="server" Text="Tasks / Reminders assigned to me"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="6">
                                                                    <asp:DataGrid ID="grdReminderGrid" runat="server" Width="100%" CssClass="GridTable"
                                                                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdReminderGrid_PageIndexChanged">
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="I_REMINDER_ID" HeaderText="Reminder ID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="DT_REMINDER_DATE" HeaderText="Due Date" DataFormatString="{0:dd MMM yyyy}">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_DESC" HeaderText="Task"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_TO" HeaderText="Assign To ID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText=" Assigned to"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_BY" HeaderText="REMINDER_ASSIGN_BY"
                                                                                Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_STATUS_ID" HeaderText="REMINDER_STATUS_ID"
                                                                                Visible="False"></asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <extddl:ExtendedDropDownList ID="extddlReminderStatus" runat="server" Connection_Key="Connection_String"
                                                                                        Flag_Key_Value="GET_REMINDER_STATUS_LIST" Procedure_Name="SP_GET_LIST_MST_REMINDER_STATUS"
                                                                                        Selected_Text="---Select---" Width="207px"></extddl:ExtendedDropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" align="center" colspan="6">
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                                                        OnClick="btnUpdate_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%; height: 20px;" colspan="6" class="SectionDevider">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 34%" align="right">
                                                                </td>
                                                                <td style="width: 13%" align="right">
                                                                    Reminder Status
                                                                </td>
                                                                <td style="width: 18%">
                                                                    <extddl:ExtendedDropDownList ID="extddlCReminderStatus" runat="server" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="GET_REMINDER_STATUS_LIST" Procedure_Name="SP_GET_LIST_MST_REMINDER_STATUS"
                                                                        Selected_Text="---Select---" Width="160px" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                </td>
                                                                <td style="width: 12%" align="right">
                                                                    Reminder Date
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtCReminderDate" runat="server" MaxLength="10" Width="100px" CssClass="textbox"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnCReminderDate" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;&nbsp;
                                                                    <ajaxToolkit:CalendarExtender ID="calextCReminderDate" runat="server" TargetControlID="txtCReminderDate"
                                                                        PopupButtonID="imgbtnCReminderDate" Enabled="True" PopupPosition="BottomRight" />
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <asp:Button ID="btnCFilter" runat="server" Text="Filter" Width="80px" CssClass="Buttons"
                                                                        OnClick="btnCFilter_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="6">
                                                                    <asp:Label ID="lblReminder" runat="server" Text="Tasks / Reminders I have assigned"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="6">
                                                                    <asp:DataGrid ID="grdCreatedReminder" runat="server" Width="100%" CssClass="GridTable"
                                                                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdCreatedReminder_PageIndexChanged">
                                                                        <ItemStyle CssClass="GridRow" />
                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="I_REMINDER_ID" HeaderText="Reminder ID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="DT_REMINDER_DATE" HeaderText="Due Date" DataFormatString="{0:dd MMM yyyy}">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_DESC" HeaderText="Task"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_TO" HeaderText="Assign To ID" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="Assigned to"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_BY" HeaderText="REMINDER_ASSIGN_BY"
                                                                                Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="SZ_REMINDER_STATUS_ID" HeaderText="REMINDER_STATUS_ID"
                                                                                Visible="False"></asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <extddl:ExtendedDropDownList ID="extddlCReminderStatus" Enabled="false" runat="server"
                                                                                        Connection_Key="Connection_String" Flag_Key_Value="GET_REMINDER_STATUS_LIST"
                                                                                        Procedure_Name="SP_GET_LIST_MST_REMINDER_STATUS" Selected_Text="---Select---"
                                                                                        Width="207px"></extddl:ExtendedDropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Remove">
                                                                                <%--<HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkDeleteAll" runat="server" onclick="chkall(this.checked)" Text="Select All" />
                                                                            </HeaderTemplate>--%>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="chk()" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" align="center" colspan="6">
                                                                    <asp:Button ID="btnDelete" runat="server" Text="Remove" Width="80px" CssClass="Buttons"
                                                                        OnClick="btnDelete_Click" />
                                                                    <asp:TextBox ID="txtUserID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
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
