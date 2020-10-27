<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="AddNewVisit.aspx.cs" Inherits="AddNewVisit" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divChkCaseno">
        <asp:Panel ID="pnlChkCaseno" runat="server">
            <table style="width:100%;text-align:center;">
                <tr id="trInvalid" runat="server">
                    <td style="vertical-align: top" class="tablecellLabel">
                        &nbsp;
                    </td>
                    <td style="font-family:Arial;font-size:12px;font-weight:normal;">
                        <asp:Label ID="lblMsgInvalid" Text="Visits could not be added to these cases: " runat="server"
                            Style="color: Red;"></asp:Label>
                        <asp:Label ID="lblInvalidCaseNo" Text="" runat="server" Style="color: Red;"></asp:Label>
                    </td>
                    <td style="text-align:left;">
                        &nbsp;
                    </td>
                </tr>
                <tr id="trValid" runat="server">
                    <td style="vertical-align: top" class="tablecellLabel">
                    </td>
                    <td style="font-family:Arial;font-size:12px;font-weight:normal;">
                        <asp:Label ID="lblMsgValid" Text="Visits added to these cases: " runat="server" Style="color: Red;"></asp:Label>
                        <asp:Label ID="lblValidCaseNo" Text="" runat="server" Style="color: Red;"></asp:Label>
                    </td>
                    <td style="text-align:left;">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <table style="width:100%;">
        <tr>
            <td colspan="2" style="text-align:center;">
                <div id="ErrorDiv" style="color: red" visible="true">
                </div>
                <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false" Style="color: Blue;"></asp:Label>            
            </td>
        </tr>
        <tr>
            <td>
                Doctor Name:
            </td>
            <td>
                <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged" />
            </td>            
        </tr>
        <tr>
            <td>
                Procedure Code:
            </td>
            <td>
                <asp:ListBox ID="lstProcedureCode" runat="server" Width="349px" SelectionMode="Multiple">
                </asp:ListBox>            
            </td>
        </tr>
        <tr>
            <td>
                Date:
            </td>
            <td>
                <span>                
                    <asp:TextBox Width="120px" ID="txtAddDate" runat="server" onkeypress="return clickButton1(event,'/')"
                    MaxLength="10" CssClass="cinput"></asp:TextBox><span style="color: #ff0000">*</span>
                    <asp:ImageButton ID="imgbtnDateofAppt" runat="server" ImageUrl="~/Images/cal.gif" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAddDate"
                    PopupButtonID="imgbtnDateofAppt" />
                </span>
            </td>
        </tr>
        <tr>
            <td>
                Visit Type:
            </td>
            <td>
                <extddl:ExtendedDropDownList ID="extddlVisitType" runat="server" Width="200px" Connection_Key="Connection_String"
                Flag_Key_Value="GET_VISIT_TYPE" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Selected_Text="---Select---"
                AutoPost_back="false" />
            </td>
        </tr>
        <tr>
            <td>
                Case No:
            </td>
            <td>
                <asp:TextBox ID="txtCaseNo" runat="server" Visible="true" Width="100%" TextMode="MultiLine" Height="140px"></asp:TextBox>            
            </td>
        </tr>
        <tr>
            <td colspan ="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <asp:Button ID="btnSave" runat="server" Width="80px" Text="Add" OnClick="btnSave_Click"></asp:Button>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>            
            </td>
        </tr>
        
    </table>        
    </div>
    
</asp:Content>
