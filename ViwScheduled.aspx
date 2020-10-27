<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViwScheduled.aspx.cs" Inherits="ViwScheduled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">    
   
       function ascii_value(c){
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

        function CheckForInteger(e,charis)
        {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
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
         
         
           function ConfirmDelete()
            { 
                 var msg = "Are you sure to continue for delete visit";
                 var result = confirm(msg);
                 if(result==true)
                 {
                    return true;
                 }
                 else
                 {
                    return false;
                 }
          
            }
            
            
         
    </script>
    <script type="text/javascript" src="Registration/validation.js" ></script>
     <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>  
    <form id="form1" runat="server">
    
    <div>
    <table class="TDPart">
     <tr>
        <td colspan="5" >
            <div id="ErrorDiv" style="color: red;" visible="true">
    </div>
            <asp:Label ID="lblMessage"  style="color: red;" runat="server" ></asp:Label>
        </td>
     </tr>
    <tr>
            <td style="width: 17%;">
                First name</td>
            <td style="width: 111px;">
                <asp:TextBox ID="txtPatientFName" runat="server" ReadOnly="true" ></asp:TextBox></td>
            <td style="width: 7%; height: 26px;">
                Middle
            </td>
            <td style="height: 26px; width: 15%;">
                <asp:TextBox ID="txtMI" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td width="10%" style="height: 26px">
                Last name
            </td>
            <td style="height: 26px">
                <asp:TextBox ID="txtPatientLName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                Phone</td>
            <td style="width: 111px">
                <asp:TextBox ID="txtPatientPhone" runat="server" Enabled="False"></asp:TextBox></td>
            <td style="width: 7%">
                Address</td>
            <td style="width: 15%">
                <asp:TextBox ID="txtPatientAddress" runat="server" Enabled="False"></asp:TextBox></td>
            <td style="width: 10%">
                City</td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 17%">
                State</td>
            <td style="width: 111px">
                <asp:TextBox ID="txtState" runat="server" Enabled="False" ></asp:TextBox></td>
            <td style="width: 7%">
                Birthdate</td>
            <td style="width: 15%">
                <asp:TextBox ID="txtBirthdate" runat="server"  Enabled="False"></asp:TextBox>
            </td>
            <td style="width: 10%">
                Age&nbsp;</td>
            <td>
                <asp:TextBox ID="txtPatientAge" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 17%">
                SS #</td>
            <td style="width: 111px">
                <asp:TextBox ID="txtSocialSecurityNumber" runat="server" Enabled="False"></asp:TextBox></td>
            <td style="width: 7%">
                Insurance</td>
            <td style="width: 15%">
                <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" runat="server" Connection_Key="Connection_String"
                    Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY" Selected_Text="--- Select ---"
                    Width="150px" Enabled="False"  />
            </td>
            <td style="width: 10%">
                Case Type</td>
            <td>
                <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Connection_Key="Connection_String"
                    Flag_Key_Value="CASETYPE_LIST" Procedure_Name="SP_MST_CASE_TYPE" Selected_Text="---Select---"
                    Width="150px" Enabled="False" ></extddl:ExtendedDropDownList>
                <extddl:ExtendedDropDownList ID="extddlCaseStatus" Width="150px" runat="server" Connection_Key="Connection_String"
                    Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                    Flag_ID="txtCompanyID.Text.ToString();" Visible="false" Enabled="False"  />
            </td>
        </tr>
        <tr>
           
            <td  colspan="5" style="width: 100%">
                <asp:CheckBox ID="chkTransportation" runat="server" Enabled="false" Text="Transport" TextAlign="Left" Visible="false"/>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtPatientID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox></td>
           
        </tr>
        <tr>
            <td style="width: 17%">Doctor Name
             </td>
            <td  style="width: 12%"> :<asp:Label ID="lblDoctorName" CssClass="text-box-100" runat="server" ></asp:Label>  
                   <asp:Label ID="lblDoctorID" CssClass="text-box-100" runat="server" Visible="false" ></asp:Label>              
                   <asp:Label ID="lblPatientID" CssClass="text-box-100" runat="server" Visible="false" ></asp:Label>              
                   <asp:Label ID="lblDate" CssClass="text-box-100" runat="server" Visible="false" ></asp:Label> 
             </td>
            <td>
            Event Start Time</td>
            <td style="width: 25%">
                 : <asp:Label ID="lblHours" runat="server" CssClass="text-box-100" Text=""></asp:Label>
                  
                    <asp:Label ID="lblMinutes" runat="server" CssClass="text-box-100" Text=""></asp:Label>
                    <asp:Label ID="lblTime" runat="server" CssClass="text-box-100" Text=""></asp:Label></td>
            <td  style="width: 12%">
            Event End Time
                   <asp:Label ID="lblType"  CssClass="text-box-100" runat="server" Visible="false"></asp:Label>  
                <asp:Label id="lblTypeCode" runat="server" CssClass="text-box-100" Visible="false"></asp:Label> 
             </td>
             <td>
                : <asp:Label ID="lblEndHours" runat="server" CssClass="text-box-100" Text=""></asp:Label>
                   
                    <asp:Label ID="lblEndMinutes" runat="server" CssClass="text-box-100" Text=""></asp:Label>
                    <asp:Label ID="lblEndTime" runat="server" CssClass="text-box-100" Text=""></asp:Label>
                 
             </td>
        </tr>
       <%-- <tr>
            <td style="width: 17%">
                &nbsp;</td>
            <td  colspan="2" style="width: 111px">  
             </td>
            <td  style="width: 7%">
               
            </td>
            <td  colspan="2" style="width: 15%"> 
             </td>
           
        </tr>--%>
         <tr>
            <td style="width: 17%">Procedure
             </td>
            <td  colspan="4" style="width: 90%">   <asp:ListBox id="ddlTestNames" runat="server" Width="100%" SelectionMode="Multiple">
                                                            </asp:ListBox> 
             </td>
            
            
        </tr>
        <tr>
            <td style="width: 17%">Status
             </td>
            <td style="width: 15%">  <asp:DropDownList ID="ddlStatus" runat="server" Width="114px" onchange="javascript:SetReValue();">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                    <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                    <asp:ListItem Value="3">No Show</asp:ListItem>
                </asp:DropDownList>
             </td>
            <td id="tdReDate" style="width: 7%;visibility:hidden;"> 
                <asp:Label ID="lblReScheduleDAte" runat="server"  Text="Reschedule Date" ></asp:Label>
            </td>
            <td id="tdReDateValue" style="width: 15%;visibility:hidden;">  
                <asp:TextBox ID="txtReScheduleDate" runat="server" MaxLength="10" Width="106px" onkeypress="return CheckForInteger(event,'/')" ></asp:TextBox>
                <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReScheduleDate"
                    PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                 
             </td>
            <td id="tdReTime" style="width: 10%;visibility:hidden;"> 
            <asp:Label ID="lblReScheduleTime" runat="server"  Text="Reschedule Time" ></asp:Label>
            
            </td>
            <td id="tdReTimeValue" style="width: 15%;visibility:hidden;">
                  <asp:DropDownList ID="ddlReSchHours" runat="server" Width="45px" >
                    </asp:DropDownList>
                   
                    <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="45px" >
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlReSchTime" runat="server" Width="45px" >
                    </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td style="width: 17%">Notes
             </td>
            <td  colspan="4" style="width: 90%">  <asp:TextBox id="txtNotes" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox> 
             </td>
            
        </tr>
    </table>
    <table width="100%" >
        
       
        <tr>
            <td colspan="2" align="center" style="width:100%">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="62px" CssClass="Buttons" OnClick="btnSave_Click" ></asp:Button>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="62px" CssClass="Buttons" Visible="false" OnClick="btnUpdate_Click"  ></asp:Button>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Width="62px" CssClass="Buttons" ></asp:Button>
                <input id="Button1" type="button" value="Cancel" onclick="javascript:parent.document.getElementById('divid').style.visibility = 'hidden';parent.document.getElementById('divid').style.zIndex= '-1'; " class="Buttons" />
                <asp:TextBox ID="txtEventID" runat="server" Visible="false" ></asp:TextBox>
                </td>
                
        </tr>
    </table>
    
                   
                                    
                                    
    </div>
    </form>
</body>
</html>
