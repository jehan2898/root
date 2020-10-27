<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Notes_Reminder.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Notes_Reminder" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/Reminder.ascx" TagName="MessageControl" TagPrefix="Reminder" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />

</head>
<body>
<form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="ErrorDiv" style="color: red;" visible="true">
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <cc1:TabContainer ID="tabcontainerAddVisit" runat="server" ActiveTabIndex="0">
                            <cc1:TabPanel runat="server" ID="tabPanelAddReminder" >
                                <HeaderTemplate>
                                    <div style="width: 150px; text-align: center;" class="lbl">
                                        Reminder
                                    </div>
                                </HeaderTemplate>
                                <ContentTemplate>
                                      <Reminder:MessageControl runat="server" id="reminderVisits" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </td>
                </tr>
            </table>
            
        </div>
    </form>
</body>
</html>
