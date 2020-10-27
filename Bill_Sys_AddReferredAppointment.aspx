<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AddReferredAppointment.aspx.cs"
    Inherits="Bill_Sys_AddReferredAppointment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">
    
    function ConfirmDelete()
    {
        if(confirm("Are you sure you want to delete")) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
        
    function DisplayControl()
    {
        document.getElementById('contentSearch').style.visibility='visible';
        document.getElementById('griddiv').style.visibility='hidden';   
        document.getElementById('searchbuttondiv').style.visibility='hidden';   
        document.getElementById('txtPatientFirstName').value='';  
        document.getElementById('txtPatientLastName').value='';
    }
    function HideControl()
    {
        document.getElementById('contentSearch').style.visibility='hidden';
        document.getElementById('griddiv').style.visibility='hidden';   
        document.getElementById('searchbuttondiv').style.visibility='visible';   
    }
    
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
    </script>
 <script type="text/javascript" src="Registration/validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
      
        <div>
            <table width="50%">
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;vertical-align: top;">
                                        <tr>
                                            <td style="width: 100%" class="TDPart">
                                                   <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                    
                                                    <ContentTemplate>
                                               
                                                <table width="100%" border="0" align="center" class="ContentTable"
                                                    cellpadding="0" cellspacing="0">
                                                   <tr id="tdSerach" runat="server" >
                                                   <td    class="ContentLabel" colspan="6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3"  UpdateMode="Conditional">
                                                      <ContentTemplate>
                                                      <table width="100%"  class="ContentTable"   >
                                                    <tr>
                                                        <td class="ContentLabel" width="100%" colspan="6" style="text-align: left"> 
                                                        <div id="searchbuttondiv" style="visibility:visible; float:left; position:relative; left: 0px; top: 0px;">
                                                        <input id="btnClickSearch" runat="server" type="button" value="Search" class="Buttons"  onclick="DisplayControl()" />
                                                        </div>
                                                        <div id="contentSearch" style="visibility:hidden; float:left; position:relative; left: 0px; top: 0px;">
                                                        <asp:Label ID="lblFirstname" runat="server" >First Name :</asp:Label>  
                                                        <asp:TextBox ID="txtPatientFirstName" runat="server" Width="120px" ></asp:TextBox>
                                                         <asp:Label ID="lblLastname" runat="server" >Last Name :</asp:Label> 
                                                        <asp:TextBox ID="txtPatientLastName" runat="server" Width="120px" ></asp:TextBox>
                                                        <asp:Button ID="btnSearhPatientList" runat="server" Text="Submit" CssClass="Buttons" OnClick="btnSearhPatientList_Click" />
                                                        <input id="btnPatientCancel" type="button" value="Cancel" class="Buttons" onclick="HideControl()"  />                                                                                                             
                                                           </div>                                                                                                                                                            
                                                        </td>
                                                    </tr>
                                                          
                                                   </table>
                                                    </ContentTemplate>
                                                  <Triggers>
                                                  <asp:AsyncPostBackTrigger ControlID="btnSearhPatientList" EventName="Click" />
                                                  </Triggers>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4"  UpdateMode="Conditional">
                                                      <ContentTemplate>
                                                     <table width="100%"  class="ContentTable"   >
                                                    <tr>
                                                        <td class="ContentLabel" colspan="6" style="height: 20px; text-align: left">
                                                       <div id="griddiv" style="float:left; position:relative; left: 0px; top: 0px;">
                                                                <asp:DataGrid ID="grdPatientList" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                                                    PageSize="3" PagerStyle-Mode="NumericPages" OnSelectedIndexChanged="grdPatientList_SelectedIndexChanged">
                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                    <ItemStyle CssClass="GridRow" />
                                                                    <Columns>
                                                                        <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_FIRST_NAME" HeaderText="First Name"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_LAST_NAME" HeaderText="Last Name"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="I_PATIENT_AGE" HeaderText="Age" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="Address" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_STREET" HeaderText="Street" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_CITY" HeaderText="City" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_ZIP" HeaderText="Zip" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Phone" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_EMAIL" HeaderText="Email" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="MI" HeaderText="MI" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_WCB_NO" HeaderText="WCB" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_SOCIAL_SECURITY_NO" HeaderText="Social Security No" Visible="False"
                                                                           ></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="DT_DOB" HeaderText="Date Of Birth" DataFormatString="{0:MM/dd/yyyy}" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_GENDER" HeaderText="Gender" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="DT_INJURY" HeaderText="Date of Injury" DataFormatString="{0:MM/dd/yyyy}" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_JOB_TITLE" HeaderText="Job Title" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_WORK_ACTIVITIES" HeaderText="Work Activities" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_STATE" HeaderText="State" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_CARRIER_CASE_NO" HeaderText="Carrier Case No" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Employer Name" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_PHONE" HeaderText="Employer Phone" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_ADDRESS" HeaderText="Employer Address" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_CITY" HeaderText="Employer City" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_STATE" HeaderText="Employer State" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_ZIP" HeaderText="Employer Zip" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_WORK_PHONE" HeaderText="WORK_PHONE" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SZ_WORK_PHONE_EXTENSION" HeaderText="WORK_PHONE" 
                                                                            Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="BT_WRONG_PHONE" HeaderText="WRONG_PHONE" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="BT_TRANSPORTATION" HeaderText="TRANSPORTATION" Visible="False">
                                                                        </asp:BoundColumn>
                                                                        
                                                                    </Columns>
                                                                <PagerStyle Mode="NumericPages" />
                                                            </asp:DataGrid>
                                                       </div> 
                                                        </td>
                                                    </tr>
                                                    </table>
                                                    </ContentTemplate>
                                                  
                                                    </asp:UpdatePanel>
                                                    </td>
                                                    </tr>
                                                   <tr>
                                                   <td  class="ContentLabel" colspan="6" style="height: 493px">
                                                      <asp:UpdatePanel runat="server" ID="UpdatePanel2"  UpdateMode="Conditional">
                                                      <ContentTemplate>
                                                      <table width="100%">
                                                    <%--<tr>
                                                        <td class="ContentLabel" colspan="6" style="height: 20px; text-align: left">
                                                         Add Patient 
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td class="ContentLabel" style="text-align: left; height:20px;" colspan="6">
                                                            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                             <asp:TextBox ID="txtAppDate" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="6">
                                                         <asp:LinkButton ID="lnkPatientDesk" runat="server" Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'                                                            
                                                            ToolTip="Patient Desk" OnClick="lnkPatientDesk_Click">
                                                        </asp:LinkButton>
                                                         </td>
                                                    </tr>
                                                          <tr>
                                                              <td style="width: 185px">
                                                                  <asp:Label ID="lblChartNumber" runat="server" Text="Chart No."></asp:Label></td>
                                                              <td style="width: 201px">
                                                                  <asp:TextBox ID="txtChartNo" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                          </tr>
                                                    
                                                    <tr>
                                                        <td style="width: 185px">
                                                            First name</td>
                                                        <td style="width: 201px">
                                                            <asp:TextBox ID="txtPatientFName" runat="server" ReadOnly="true" ></asp:TextBox></td>
                                                        <td style="width: 20%">
                                                            Middle
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txtMI" runat="server" Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td width="10%">
                                                            Last name
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txtPatientLName" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 185px">
                                                            Phone</td>
                                                        <td style="width: 201px">
                                                            <asp:TextBox ID="txtPatientPhone" runat="server" Enabled="False"></asp:TextBox></td>
                                                        <td style="width: 20%">
                                                            Address</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPatientAddress" runat="server" Enabled="False"></asp:TextBox></td>
                                                        <td width="10%">
                                                            City</td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="TextBox3" runat="server" Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 185px">
                                                            State</td>
                                                        <td style="width: 201px">
                                                            <asp:TextBox ID="txtState" runat="server" Enabled="False"></asp:TextBox></td>
                                                        <td style="width: 20%">
                                                            Birthdate</td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txtBirthdate" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                                                            <asp:ImageButton ID="imgbtnBirthdate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <cc1:CalendarExtender ID="calBirthdate" runat="server" PopupButtonID="imgbtnBirthdate"
                                                                TargetControlID="txtBirthdate" >
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td width="10%">
                                                            Age&nbsp;</td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txtPatientAge" runat="server" Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 185px">
                                                            SS #</td>
                                                        <td style="width: 201px">
                                                            <asp:TextBox ID="txtSocialSecurityNumber" runat="server" Enabled="False"></asp:TextBox></td>
                                                        <td style="width: 20%">
                                                            Insurance</td>
                                                        <td width="10%">
                                                            <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" runat="server" Connection_Key="Connection_String"
                                                                Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY" Selected_Text="--- Select ---"
                                                                Width="150px" Enabled="False"  />
                                                        </td>
                                                        <td width="10%">
                                                            Case Type</td>
                                                        <td width="10%">
                                                            <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Connection_Key="Connection_String"
                                                                Flag_Key_Value="CASETYPE_LIST" Procedure_Name="SP_MST_CASE_TYPE" Selected_Text="---Select---"
                                                                Width="150px" Enabled="False" ></extddl:ExtendedDropDownList>
                                                            <extddl:ExtendedDropDownList ID="extddlCaseStatus" Width="150px" runat="server" Connection_Key="Connection_String"
                                                                Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                Flag_ID="txtCompanyID.Text.ToString();" Visible="false" Enabled="False"  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 185px">
                                                             <asp:Label ID="lblTestFacility" runat="server" Text="Test Facility"></asp:Label> </td>
                                                        <td style="width: 201px">
                                                             <extddl:ExtendedDropDownList id="extddlMedicalOffice" Visible="false" runat="server" Width="150px"  Selected_Text="--- Select ---" Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Connection_Key="Connection_String"  ></extddl:ExtendedDropDownList>
                                                            <extddl:ExtendedDropDownList ID="extddlReferenceFacility" runat="server" Connection_Key="Connection_String"
                                                                Flag_Key_Value="REFERRING_FACILITY_LIST" Procedure_Name="SP_TXN_REFERRING_FACILITY" Selected_Text="--- Select ---"
                                                                Width="150px" Enabled="False" />
                                                        </td>
                                                        <td style="width: 20%">
                                                         Referring Doctor
                                                           </td>
                                                        
                                                        <td width="10%" >
                                                        <asp:Label ID="lblDoctor" runat="server" Visible="False"></asp:Label><extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="150px" Connection_Key="Connection_String"
                                                                Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST">
                                                            </extddl:ExtendedDropDownList>
                                                             
                                                          </td>
                                                          <td width="10%" >
                                                          <asp:Label ID="Label1" runat="server" Text="Study #" Visible="false"></asp:Label> 
                                                          </td>
                                                        <td width="10%">
                                                         <asp:DropDownList ID="ddlType" Visible="false" runat="server" Width="150px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                                >
                                                                <asp:ListItem  Value="0"> --Select--</asp:ListItem>
                                                                <asp:ListItem Value="TY000000000000000001">Visit</asp:ListItem>
                                                                <asp:ListItem Value="TY000000000000000002">Treatment</asp:ListItem>
                                                                <asp:ListItem Value="TY000000000000000003" Selected="True">Test</asp:ListItem>
                                                            </asp:DropDownList>
                                                           <%-- <asp:TextBox ID="txtDoctorName" runat="server" Visible="false"></asp:TextBox>--%>
                                                           <asp:TextBox ID="txtStudyNumber" runat="server"  Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                   <tr>
                                                        
                                                         <td style="width: 185px">
                                                        
                                                                <asp:CheckBox ID="chkTransportation" runat="server" style="float:left;" Text="Transport" TextAlign="Left" AutoPostBack="True" OnCheckedChanged="chkTransportation_CheckedChanged" Width="80px" />
                                                            </td>
                                                            <td style="width: 201px">
                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional"  >
                                                                     <ContentTemplate >
                                                                    <extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="141px" Connection_Key="Connection_String" Visible="false"
                                                                    Procedure_Name="SP_MST_TRANSPOTATION" Selected_Text="---Select---" Flag_Key_Value="GET_TRANSPORT_LIST" Flag_ID="txtCompanyID.Text.ToString();">
                                                                    </extddl:ExtendedDropDownList>
                                                                </ContentTemplate>
                                                                <Triggers >
                                                                    <asp:AsyncPostBackTrigger ControlID="chkTransportation" EventName="OnCheckedChanged" />
                                                                </Triggers> 
                                                             </asp:UpdatePanel>
                                                            </td>
                                                            </tr>
                                                             
                                                            
                                                          
                                                        
                                                    </tr>
                                                    <tr>
                                                        
                                                        <td style="width: 20%; height: 17px;">
                                                            Procedure</td>
                                                        <td width="10%" colspan="5" style="height: 17px">
                                                            <asp:ListBox ID="ddlTestNames" runat="server" Width="00px" Height="00px" >
                                                            </asp:ListBox></td>
                                                        <%--<td width="10%">
                                                            </td>
                                                        <td width="10%">
                                                            &nbsp; <asp:DropDownList ID="ddlTestNames" runat="server" Width="150px" Visible="false" >
                                                            </asp:DropDownList>--%>
                                                    
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                             <div style="overflow: scroll; height: 150px; width: 100%;">
                                                                    <asp:DataGrid ID="grdProcedureCode" runat="server" Width="95%" CssClass="GridTable" AutoGenerateColumns="false" 
                                                                    PageSize="3" PagerStyle-Mode="NumericPages" OnItemDataBound="grdProcedureCode_ItemDataBound" >
                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                    <ItemStyle CssClass="GridRow" />
                                                                    <Columns>
                                                                         <asp:TemplateColumn>
                                                                            <ItemStyle Width="5%" />
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server"  />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="code" HeaderText="Patient ID" Visible="False" ItemStyle-Width="5%"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="description" HeaderText="Procedure" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                                           <asp:TemplateColumn HeaderText="Status">
                                                                            <ItemTemplate>
                                                                             <ItemStyle Width="5%" />
                                                                                 <asp:DropDownList ID="ddlStatus" runat="server" >
                                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Re-Schedule</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Visit Completed</asp:ListItem>
                                                                                    <asp:ListItem Value="3">No Show</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                               
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Re-Schedule Date">
                                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtReScheduleDate" runat="server"  onkeypress="return CheckForInteger(event,'/')" width="70px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Re-Schedule Time" >
                                                                            <ItemStyle Width="25%" HorizontalAlign="Center"/>
                                                                            <ItemTemplate>
                                                                                 <asp:DropDownList ID="ddlReSchHours" runat="server" Width="40px" >
                                                                                </asp:DropDownList>
                                                                               
                                                                                <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="40px">
                                                                                </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlReSchTime" runat="server" Width="40px">
                                                                                </asp:DropDownList>
                                                                           
                                                                              
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText ="Study No.">
                                                                            <ItemStyle Width="5%" HorizontalAlign="Center"/>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtStudyNo" runat="server" Width="80px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="I_RESCHEDULE_ID" HeaderText="I_RESCHEDULE_ID" Visible="False" ></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="I_EVENT_PROC_ID" HeaderText="I_EVENT_PROC_ID" Visible="False" ></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText ="Notes">
                                                                            <ItemStyle Width="5%" HorizontalAlign="Center"/>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtProcNotes" runat="server" Width="80px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <%--<asp:BoundColumn DataField = "I_STATUS" HeaderText = "I_STATUS" Visible ="false"></asp:BoundColumn>--%>
                                                                        
                                                                    </Columns>
                                                                <PagerStyle Mode="NumericPages" />
                                                            </asp:DataGrid>
                                                            </div>
                                                           </td>
                                                       
                                                            
                                                       
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 185px">
                                                            Notes
                                                        </td>
                                                        <td colspan="5" class="ContentLabel">
                                                            &nbsp;<asp:TextBox ID="txtNotes" style="float:left;" runat="server" TextMode="MultiLine" Width="584px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="right">
                                                            
                                                            <asp:TextBox ID="txtEventStatus" runat="server" Visible="False" Width="5px"></asp:TextBox>
                                                            <asp:TextBox ID="txtPatientCompany" runat="server" Visible="False" Width="5px"></asp:TextBox>
                                                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="5px"></asp:TextBox>
                                                            <asp:TextBox ID="txtPatientID" runat="server" Visible="false" Width="5px"></asp:TextBox>
                                                            <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="5px"></asp:TextBox>
                                                            
                                                           <asp:DropDownList ID="ddlHours" runat="server" Width="48px" Visible="False">
                                                            </asp:DropDownList>
                                                           
                                                            <asp:DropDownList ID="ddlMinutes" runat="server" Width="48px" Visible="False">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlTime" runat="server" Width="48px" Visible="False">
                                                            </asp:DropDownList>
                                                       
                                                            <asp:DropDownList ID="ddlEndHours" runat="server" Width="40px" Visible="False">
                                                            </asp:DropDownList>
                                                           
                                                            <asp:DropDownList ID="ddlEndMinutes" runat="server" Width="40px" Visible="False">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlEndTime" runat="server" Width="45px" Visible="False">
                                                            </asp:DropDownList>
                                                             <asp:Button
                                                                ID="btnUpdate" runat="server" Text="Update"  Width="62px"
                                                                CssClass="Buttons" OnClick="btnUpdate_Click"></asp:Button>
                                                            <asp:Button
                                                                ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="62px"
                                                                CssClass="Buttons"></asp:Button>
                                                             <asp:Button
                                                                ID="btnDelete" runat="server" Text="Delete" Width="62px"
                                                                CssClass="Buttons" OnClick="btnDelete_Click"></asp:Button>
                                                            <input id="Button1" type="button" value="Cancel" onclick="javascript:parent.document.getElementById('divid').style.visibility = 'hidden';javascript:parent.document.getElementById('frameeditexpanse').src = '';"
                                                                class="Buttons" />&nbsp;
                                                        </td>
                                                    </tr>
                                                    </table> 
                                                    </ContentTemplate> 
                                                    </asp:UpdatePanel> 
                                                   </td>
                                                   </tr> 
                                                </table>
                                                   </ContentTemplate>
                                                    </asp:UpdatePanel>
                                              
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
