<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_UpdateOutScheduleVisit.aspx.cs"
    Inherits="Bill_Sys_UpdateOutScheduleVisit" %>

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
                        <td colspan="2">
                            <asp:Label ID="lblMsg" runat="server" CssClass="message-text" Style="color: blue"
                                Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <div class="lbl">
                                <asp:Label ID="lblTestFacility" runat="server" Text="Test Facility"></asp:Label></div>
                        </td>
                        <td width="70%">
                            <extddl:ExtendedDropDownList ID="extddlReferringFacility" runat="server" 
                                Connection_Key="Connection_String" Flag_Key_Value="REFERRING_FACILITY_LIST" 
                                Procedure_Name="SP_TXN_REFERRING_FACILITY" Selected_Text="--- Select ---" 
                                Width="200px" OnextendDropDown_SelectedIndexChanged="extddlReferringFacility_extendDropDown_SelectedIndexChanged"/>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="30%">
                            Room
                        </td>
                        <td width="70%">
                            <extddl:ExtendedDropDownList ID="extddlRoom" runat="server" 
                                Connection_Key="Connection_String" Flag_Key_Value="ROOM_LIST" 
                                Procedure_Name="SP_MST_ROOM" Selected_Text="--- Select ---"  Width="200px" OnextendDropDown_SelectedIndexChanged="extddlRoom_extendDropDown_SelectedIndexChanged" AutoPost_back="True"/>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            Referring Doctor</td>
                        <td width="70%">
                            <extddl:ExtendedDropDownList ID="extddlReferringDoctor" runat="server" Connection_Key="Connection_String"
                                Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                                Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            Date:</td>
                        <td width="70%">
                            <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="cinput" MaxLength="80"
                                Width="40%"></asp:TextBox><span style="color: #ff0000">*</span>
                            <%--<asp:ImageButton ID="imgbtnAppointmentDate" Visible="false" runat="server" ImageUrl="~/Images/cal.gif">
                            </asp:ImageButton>--%>
                            <a id="trigger" href="#">
                                <input id="imgbtnDateofService" runat="server" border="0" name="mgbtnDateofService"
                                    src="Images/cal.gif" type="image" /></a>
                                     <ajaxToolkit:CalendarExtender ID="calBirthdate" runat="server" PopupButtonID="imgbtnDateofService"
                                                                TargetControlID="txtAppointmentDate" >
                                                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <asp:Label ID="lblTIme" runat="server">Time :</asp:Label></td>
                        <td width="70%">
                            <asp:DropDownList ID="ddlHours" runat="server" Width="60px">
                            </asp:DropDownList><asp:DropDownList ID="ddlMinutes" runat="server" 
                                Width="60px">
                            </asp:DropDownList><asp:DropDownList ID="ddlTime" runat="server" 
                                Width="60px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <asp:Label ID="lblTransport" runat="server" Text="Transport"></asp:Label></td>
                        <td width="70%">
                           <div style="height:auto; width:193px; float:left; ">
                               <asp:CheckBox ID="chkTransportation" runat="server" Style="float: left; height: auto;
                                   width: auto;" TextAlign="Left" AutoPostBack="true" OnCheckedChanged="chkTransportation_CheckedChanged"
                                   Text=" "></asp:CheckBox>
                               <extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="150px" Connection_Key="Connection_String"
                                   Visible="false" Procedure_Name="SP_MST_TRANSPOTATION" Selected_Text="---Select---"
                                   Flag_Key_Value="GET_TRANSPORT_LIST" Flag_ID="txtCompanyID.Text.ToString();" />
                           </div></td>
                    </tr>
                    <tr>
                        <td width="30%" valign="top">
                            Procedure</td>
                        <td width="70%">
                            <asp:ListBox ID="ddlTestNames" runat="server" SelectionMode="Multiple" Width="350px">
                            </asp:ListBox></td>
                    </tr>
                    <tr>
                        <td width="30%" valign="top">
                            Notes</td>
                        <td width="70%">
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td width="30%">
                        </td>
                        <td width="70%">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="Buttons" 
                                Text="Update" Width="62px" OnClick="btnUpdate_Click" />
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtEventID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
