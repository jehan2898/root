<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ViewVisit.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_ViewVisit" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">   
      function SetReValue()
         {
            if (document.getElementById("ddlStatus").selectedIndex ==1)
            {
            document.getElementById('tdReDate').style.visibility='visible';
            document.getElementById('tdReDateValue').style.visibility='visible';
            document.getElementById('tdReTime').style.visibility='visible';
            document.getElementById('tdReTimeValue').style.visibility='visible';
            }
            else
            {
            document.getElementById('tdReDate').style.visibility='hidden';
            document.getElementById('tdReDateValue').style.visibility='hidden';
            document.getElementById('tdReTime').style.visibility='hidden';
            document.getElementById('tdReTimeValue').style.visibility='hidden';
            }
         }
         
    function ChangeTime() 
    {
        if (document.getElementById('ddlchangeReSchHours').selectedIndex ==0)
        {
            alert('Please Select Change Time...!!');
            return false;
        }
    }
         
             
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <table width="100%">
                <tr>
                    <td colspan="2" align="center">
                        <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblMessage" Style="color: red;" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        Patient Name
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtPatientName" Width="90%" runat="server" Enabled="false"> </asp:TextBox></td>
                    <td>
                        Start Time
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartTime" runat="server" Enabled="false">  </asp:TextBox></td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        Doctor Name
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDocName" Width="90%" runat="server" Enabled="false"></asp:TextBox></td>
                    <td>
                        End Time
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndTime" runat="server" Enabled="false"> </asp:TextBox></td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        Status
                    </td>
                    <td style="width: 15%">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="114px" onchange="javascript:SetReValue();">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                            <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                            <asp:ListItem Value="3">No Show</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td id="tdReDate" style="width: 13%; visibility: hidden;" runat="server">
                        <asp:Label ID="lblReScheduleDAte" runat="server" Text="Reschedule Date"></asp:Label>
                    </td>
                    <td id="tdReDateValue" style="width: 15%; visibility: hidden;" runat="server">
                        <asp:TextBox ID="txtReScheduleDate" runat="server" MaxLength="10" Width="106px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                        <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReScheduleDate"
                            PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                    </td>
                    <td id="tdReTime" style="width: 11%; visibility: hidden;" runat="server">
                        <asp:Label ID="lblReScheduleTime" runat="server" Text="Reschedule Time"></asp:Label>
                    </td>
                    <td id="tdReTimeValue" style="width: 21%; visibility: hidden;" runat="server">
                        <asp:DropDownList ID="ddlReSchHours" runat="server" Width="45px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="45px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlReSchTime" runat="server" Width="45px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 5%; visibility: hidden;">
                    </td>
                    <td style="width: 5%; visibility: hidden;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        Notes
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNotes" runat="server" Width="96%" Height="100%" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td id="td1" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="Change Time"></asp:Label>
                    </td>
                    <td style="width: 21%;" runat="server">
                        <asp:DropDownList ID="ddlchangeReSchHours" runat="server" Width="45px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlchangeReSchMinutes" runat="server" Width="45px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlchangeReSchTime" runat="server" Width="45px">
                        </asp:DropDownList>
                    </td>
                   <%-- <td>
                    </td>--%>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="width: 50%;" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    </td>
                    <td style="width: 50%;" align="center" >
                        <asp:Button ID="btnchnagetime" runat="server" Text="Change Time" OnClick="btnchnagetime_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtDoctorid" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHaveLogin" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtGroupCode" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
