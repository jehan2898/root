<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_UpdateVisit.aspx.cs"
    Inherits="Bill_Sys_UpdateVisit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" src="Registration/validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    
    function ascii_value(c)
        {
            c = c . charAt (0);
            var i;
            for (i = 0; i < 256; ++ i)
            {
                var h = i . toString (16);
                if (h . length == 1)
                    h = "0" + h;
                    h = "%" + h;
                    h = unescape (h);
                if (h == c)
                    break;
            }
            return i;
        }
        
        
     function clickButton1(e,charis)
        {
            var keychar;
            if(navigator.appName.indexOf("Netscape")>(-1))
            {    
                var key = ascii_value(charis);
                if(e.charCode == key || e.charCode==0)
                {
                    return true;
                }
                else
                {
                    if (e.charCode < 48 || e.charCode > 57)
                     {             
                            return false;
                     } 
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
                if(charis!="")
                {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                    if (event.keyCode < 48 || event.keyCode > 57)
                    {             
                        return false;
                    }
                }
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
                <tbody>
                    <tr>
                        <td style="height: 8px; text-align: left" class="ContentLabel" colspan="2">
                            <div style="color: red" id="ErrorDiv" visible="true">
                            </div>
                            <asp:Label Style="color: blue" ID="lblMsg" runat="server" CssClass="message-text"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                Doctor Name:</div>
                        </td>
                        <td width="70%">
                            <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Selected_Text="---Select---"
                                Procedure_Name="GET_SPECIALITY_DOCTOR_LIST" Flag_Key_Value="GETDOCTORLIST" Connection_Key="Connection_String"
                                 ></extddl:ExtendedDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                Date:</div>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="txtAppointmentDate" onkeypress="return clickButton1(event,'/')"
                                runat="server" Width="120px" CssClass="cinput" MaxLength="10"></asp:TextBox><span
                                    style="color: #ff0000">*</span>
                            <asp:ImageButton ID="imgbtnAppointmentDate" Visible="false" runat="server" ImageUrl="~/Images/cal.gif">
                            </asp:ImageButton>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2"   runat="server"
                                TargetControlID="txtAppointmentDate" PopupButtonID="imgbtnAppointmentDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                Time :
                            </div>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlHours" runat="server" Width="60px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlMinutes" runat="server" Width="60px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlTime" runat="server" Width="60px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblVisitType" Visible="false">Visit Type:</asp:Label>
                            </div>
                        </td>
                        <td width="70%">
                            <extddl:ExtendedDropDownList ID="extddlVisitType" runat="server" Width="200px" AutoPost_back="false"
                                Selected_Text="---Select---" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE"
                                Connection_Key="Connection_String" Visible="false"></extddl:ExtendedDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                Visit Status:</div>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Schedule</asp:ListItem>
                                <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                                <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                                <%--<asp:ListItem Value="3">No Show</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblReDate" Visible="false">Re-Schedule  Date :</asp:Label>
                            </div>
                        </td>
                        <td width="70%">
                            <asp:TextBox ID="txtReDate" onkeypress="return clickButton1(event,'/')" runat="server"
                                Width="120px" CssClass="cinput" MaxLength="10" Visible="false"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" Visible="false" runat="server" ImageUrl="~/Images/cal.gif">
                            </asp:ImageButton>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                TargetControlID="txtReDate" PopupButtonID="ImageButton1">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    >
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblReTIme" Visible="false">Re-Schedule  Time :</asp:Label>
                            </div>
                        </td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlReSchHours" runat="server" Width="45px" Visible="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="45px" Visible="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlReSchTime" runat="server" Width="45px" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                <asp:Label runat="server" ID="lblProcedure" Visible="false">Procedure :</asp:Label>
                            </div>
                        </td>
                        <td width="70%">
                            <asp:ListBox ID="ddlTestNames" runat="server" Width="100%" Height="120px" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" OnClick="btnUpdate_Click" runat="server" Width="80px" CssClass="Buttons"
                                Text="Update"></asp:Button>
                            <asp:TextBox ID="txtVisitStatus" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtPatientID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtReEventID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            <asp:ListBox ID="ddlOldTestNames" runat="server" Width="0%" Height="0px" SelectionMode="Multiple"
                                Visible="false"></asp:ListBox>
                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
