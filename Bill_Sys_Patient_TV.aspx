<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Patient_TV.aspx.cs" Inherits="Bill_Sys_Patient_TV" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script src="calendarPopup.js"></script>

    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();		
	
    </script>

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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align:top;">
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
                                                <td class="ContentLabel" style="text-align: left;width:94%;" rowspan="2" >
                                                    <asp:LinkButton ID="hlnkAssociate" runat="server" OnClick="hlnkAssociate_Click" Visible="false">Associate Diagnosis Codes</asp:LinkButton>
                                                </td>
                                                <td style="width:2%; text-align:right;" >
                                                 <a id="hlnkInschedule" href="Bill_Sys_ScheduleEvent.aspx?Flag=True" runat="server" visible="false">
                                                         <asp:Image ID="imgInSchedule" ImageUrl="~/Images/actionEdit.gif" runat="server"  ToolTip="In Schedule" />
                                                        </a>     
                                                </td>
                                                <td  align="right" style="width:2%;">
                                                 <a id="hlnkOutschedule" href="Bill_Sys_CalPatientEntry.aspx?Flag=True" runat="server" visible="false">
                                                 <asp:Image ID="imgOutSchedule" ImageUrl="~/Images/actionEdit.gif" runat="server"  ToolTip="Out Schedule" />
                                                        </a>     
                                                </td>
                                                <td align="right" style="width:2%;">
                                                    <a id="hlnkShowNotes" href="#" runat="server" visible="false">
                                                    <asp:Image ID="imgShowNotes" ImageUrl="~/Images/actionEdit.gif" runat="server"  ToolTip="Notes" />
                                                      </a>                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                            <td colspan="4">
                                             <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowNotes"
                                                        PopupControlID="pnlShowNotes" Position="Bottom" OffsetX="-420" Enabled="false"/>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left;" colspan="4" height="20px">
                                                    <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 100%;text-align:left; height: 31px;" colspan="4">
                                               <ajaxToolkit:TabContainer ID="tabcontainerPatientEntry" runat="Server" ActiveTabIndex="0"
                                                CssClass="ajax__tab_theme">
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlPersonalInformation" TabIndex="0">
                                                    <HeaderTemplate>
                                                        <div style="width: 120px;" class="lbl">
                                                            Personal Information
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div align="left" style="height:200px;">
                                                             <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                <!-- Start : Data Entry -->
                                                                                <tr>
                                                                                    <td height="26" class="tablecellLabel">
                                                                                        
                                                                                            First name
                                                                                    </td>
                                                                                    <td rowspan="2" class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4" rowspan="2">
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td width="25%">
                                                                                                    <span class="tablecellControl">
                                                                                                        <asp:TextBox ID="txtPatientFName" runat="server"></asp:TextBox>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td width="13%" class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        Middle</div>
                                                                                                </td>
                                                                                                <td width="25%">
                                                                                                    <span class="tablecellControl">
                                                                                                        <asp:TextBox ID="txtMI" runat="server"></asp:TextBox>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td width="5%" class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        Last name
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td width="32%">
                                                                                                    <asp:TextBox ID="txtPatientLName" runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <span class="tablecellControl">
                                                                                                        <asp:TextBox Width="69%" ID="txtDateOfBirth" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                            MaxLength="10"></asp:TextBox>
                                                                                                        <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"
                                                                                                            PopupButtonID="imgbtnDateofBirth" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        SSN #
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <span class="tablecellControl">
                                                                                                        <asp:TextBox ID="txtSocialSecurityNumber" runat="server"></asp:TextBox>
                                                                                                        <asp:TextBox ID="txtPatientAge" runat="server" onkeypress="return clickButton1(event,'')"
                                                                                                            MaxLength="10" Visible="false"></asp:TextBox>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        Gender</div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="ddlSex" runat="server" Width="153px">
                                                                                                        <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                                                                                        <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="26" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Date of birth</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Address
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="tablecellSpace">
                                                                                        &nbsp;</td>
                                                                                    <td colspan="4">
                                                                                        <asp:TextBox Width="90%" ID="txtPatientAddress" runat="server"></asp:TextBox>
                                                                                        <span class="tablecellControl">
                                                                                            <asp:TextBox Visible="false" ID="txtPatientStreet" runat="server"></asp:TextBox>
                                                                                        </span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="26" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            City</div>
                                                                                    </td>
                                                                                    <td rowspan="3" class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4" rowspan="3">
                                                                                        <div class="lbl">
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td width="25%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td width="13%" class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            State</div>
                                                                                                    </td>
                                                                                                    <td width="25%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td width="5%" class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Zip</div>
                                                                                                    </td>
                                                                                                    <td width="32%">
                                                                                                        <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Work</div>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Extn.</div>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtExtension" runat="server"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="chkWrongPhone" runat="server" Text="Wrong Phone" TextAlign="Left" /></td>
                                                                                                    <td class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Email</div>
                                                                                                    </td>
                                                                                                    <td colspan="3">
                                                                                                        <asp:TextBox ID="txtPatientEmail" runat="server"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="11" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Home phone</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="12" class="tablecellLabel">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="12" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Attorney</div>
                                                                                    </td>
                                                                                    <td class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4">
                                                                                        <cc1:ExtendedDropDownList ID="extddlAttorney" Width="95%" runat="server" Connection_Key="Connection_String"
                                                                                            Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="4" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Case Type
                                                                                        </div>
                                                                                    </td>
                                                                                    <td rowspan="3" class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4" rowspan="3">
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td width="25%">
                                                                                                    <span class="tablecellControl">
                                                                                                        <cc1:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---" />
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td width="13%" class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                         <asp:CheckBox ID="chkTransportation" runat="server" Text="Transport" TextAlign="Left">
                                                                                                        </asp:CheckBox>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td width="25%">
                                                                                                    <span class="tablecellControl">
                                                                                                        <asp:TextBox Width="70%" ID="txtDateOfInjury" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                            MaxLength="10" Visible="false"></asp:TextBox>
                                                                                                       <%-- <asp:ImageButton ID="imgbtnDateofInjury" runat="server" ImageUrl="~/Images/cal.gif" Visible="false"/>
                                                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOfInjury"
                                                                                                            PopupButtonID="imgbtnDateofInjury" Visible="false"/>--%>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td width="5%" class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td width="32%">
                                                                                                    <asp:TextBox ID="txtCarrierCaseNo" runat="server" Visible="false"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td >
                                                                                                    <span class="tablecellControl">
                                                                                                        <asp:TextBox ID="txtJobTitle" runat="server" Visible="false"></asp:TextBox>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td class="tablecellLabel" >
                                                                                                    <div class="lbl">
                                                                                                        </div>
                                                                                                </td>
                                                                                                <td >
                                                                                                    <span class="tablecellControl">
                                                                                                        <asp:TextBox ID="txtWorkActivites" runat="server" Visible="false"></asp:TextBox>
                                                                                                    </span>
                                                                                                </td>
                                                                                                <td class="tablecellLabel" >
                                                                                                    <div class="lbl">
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td >
                                                                                                    <span class="lbl">
                                                                                                       
                                                                                                    </span>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="1" class="tablecellLabel">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="1" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="5" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            </div>
                                                                                    </td>
                                                                                    <td class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4">
                                                                                        <span class="tablecellControl">
                                                                                            <cc1:ExtendedDropDownList ID="extddlProvider" Width="200px" runat="server" Connection_Key="Connection_String"
                                                                                                Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---"
                                                                                                Visible="false" />
                                                                                            
                                                                                            <cc1:ExtendedDropDownList ID="extddlCaseStatus" Width="200px" runat="server" Connection_Key="Connection_String"
                                                                                                Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                                                Flag_ID="txtCompanyID.Text.ToString();" Visible="false" />
                                                                                            <asp:TextBox ID="txtAssociateDiagnosisCode" runat="server" Visible="False" />
                                                                                            <asp:CheckBox ID="chkAssociateCode" runat="server" Text="Associate Diagnosis Code"
                                                                                                Visible="False" />
                                                                                        </span>
                                                                                    </td>
                                                                                </tr>
                                                                                <!-- End : Data Entry -->
                                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlAccidentInformation" TabIndex="1">
                                                    <HeaderTemplate>
                                                        <div style="width: 120px;" class="lbl">
                                                            Accident Information
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div align="left" style="height:280px;">
                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                <!-- Start : Data Entry -->
                                                                <tr>
                                                                    <td class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            Accident date
                                                                        </div>
                                                                    </td>
                                                                    <td class="tablecellSpace">
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <div class="lbl">
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td width="25%">
                                                                                        <span class="tablecellControl">
                                                                                            <asp:TextBox Width="70%" ID="txtDateofAccident" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                MaxLength="10"></asp:TextBox>
                                                                                            <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateofAccident"
                                                                                                PopupButtonID="imgbtnDateofAccident" />
                                                                                        </span>
                                                                                    </td>
                                                                                    <td width="13%" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Plate #
                                                                                        </div>
                                                                                    </td>
                                                                                    <td width="25%">
                                                                                        <span class="tablecellControl">
                                                                                            <asp:TextBox ID="txtPlatenumber" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                        </span>
                                                                                    </td>
                                                                                    <td width="5%" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                        </div>
                                                                                    </td>
                                                                                    <td width="32%">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="26" class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            Address</div>
                                                                    </td>
                                                                    <td class="tablecellSpace">
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:TextBox Width="99%" ID="txtAccidentAddress" runat="server" CssClass="text-box"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="11" class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            City</div>
                                                                    </td>
                                                                    <td class="tablecellSpace">
                                                                    </td>
                                                                    <td colspan="4" valign="top">
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td width="25%" height="26">
                                                                                    <span class="tablecellControl">
                                                                                        <asp:TextBox ID="txtAccidentCity" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                    </span>
                                                                                </td>
                                                                                <td width="13%" class="tablecellLabel">
                                                                                    <div class="lbl">
                                                                                        State</div>
                                                                                </td>
                                                                                <td width="25%">
                                                                                    <span class="tablecellControl">
                                                                                        <asp:TextBox ID="txtAccidentState" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                    </span>
                                                                                </td>
                                                                                <td width="5%" class="tablecellLabel">
                                                                                    <div class="lbl">
                                                                                        Report #
                                                                                    </div>
                                                                                </td>
                                                                                <td width="32%">
                                                                                    <span class="tablecellControl">
                                                                                        <asp:TextBox ID="txtPolicyReport" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                    </span>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="11" class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            Patients from the car</div>
                                                                    </td>
                                                                    <td class="tablecellSpace">
                                                                    </td>
                                                                    <td colspan="4" valign="top">
                                                                        <asp:TextBox Height="100px" Width="98%" ID="txtListOfPatient" runat="server" TextMode="MultiLine"
                                                                            MaxLength="250" Wrap="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <!-- End : Data Entry -->
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlInsuranceInformation" TabIndex="2">
                                                    <HeaderTemplate>
                                                        <div style="width: 120px;" class="lbl">
                                                            Insurance Information
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div align="left" style="height:280px;">
                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                <!-- Start : Data Entry -->
                                                                <tr>
                                                                    <td class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            Name
                                                                        </div>
                                                                    </td>
                                                                    <td class="tablecellSpace">
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <cc1:ExtendedDropDownList ID="extddlInsuranceCompany" Width="100%" runat="server"
                                                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                            Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                            OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged" />
                                                                        <%--<asp:TextBox ID="txtInsAddress" runat="server" CssClass="text-box" TextMode="MultiLine"></asp:TextBox>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            Ins. Address
                                                                        </div>
                                                                    </td>
                                                                    <td class="tablecellSpace">
                                                                        &nbsp;</td>
                                                                    <td colspan="4">
                                                                        <asp:ListBox Width="100%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                                            OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            Address</div>
                                                                    </td>
                                                                    <td class="tablecellSpace">
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            City</div>
                                                                    </td>
                                                                    <td rowspan="2" class="tablecellSpace">
                                                                    </td>
                                                                    <td colspan="4" rowspan="2">
                                                                        <div class="lbl">
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td width="25%">
                                                                                        <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                    <td width="13%" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            State</div>
                                                                                    </td>
                                                                                    <td width="25%">
                                                                                        <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                    <td width="5%" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Zip</div>
                                                                                    </td>
                                                                                    <td width="32%">
                                                                                        <asp:TextBox Width="80%" ID="txtInsuranceZip" runat="server" CssClass="text-box"
                                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box"></asp:TextBox></td>
                                                                                    <td class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            <asp:Label ID="lblWcbNumber" CssClass="lbl" runat="server"></asp:Label></div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWCBNumber" runat="server"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="text-box" Visible="False"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="tablecellLabel">
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                        <asp:TextBox Visible="false" Width="99%" ID="txtInsuranceStreet" runat="server" CssClass="text-box"
                                                                                            ReadOnly="True"></asp:TextBox></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="26" class="tablecellLabel">
                                                                        <div class="lbl">
                                                                            Claim/File #
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <!-- End : Data Entry -->
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlEmployerInformation" TabIndex="3">
                                                    <HeaderTemplate>
                                                        <div style="width: 120px;" class="lbl">
                                                            Employer Information
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div align="left" style="height:280px;">
                                                             <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                <!-- Start : Data Entry -->
                                                                                <tr>
                                                                                    <td class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Name
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4">
                                                                                        <asp:TextBox Width="99%" ID="txtEmployerName" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Address
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="tablecellSpace">
                                                                                        &nbsp;</td>
                                                                                    <td colspan="4">
                                                                                        <asp:TextBox ID="txtEmployerAddress" runat="server" Width="99%"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            City</div>
                                                                                    </td>
                                                                                    <td rowspan="2" class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4" rowspan="2">
                                                                                        <div class="lbl">
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td width="25%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtEmployerCity" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td width="13%" class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            State</div>
                                                                                                    </td>
                                                                                                    <td width="25%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtEmployerState" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td width="5%" class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Zip</div>
                                                                                                    </td>
                                                                                                    <td width="32%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtEmployerZip" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtEmployerPhone" runat="server"></asp:TextBox>
                                                                                                        </span>
                                                                                                    </td>
                                                                                                    <td class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td class="tablecellLabel">
                                                                                                        &nbsp;</td>
                                                                                                    <td>
                                                                                                        &nbsp;</td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="26" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Phone</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <!-- End : Data Entry -->
                                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlAdjusterInformation" TabIndex="4">
                                                    <HeaderTemplate>
                                                        <div style="width: 120px;" class="lbl">
                                                            Adjuster Information
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div align="left" style="height:280px;">
                                                             <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                <!-- Start : Data Entry -->
                                                                                <tr>
                                                                                    <td class="ContentLabel">
                                                                                        Name
                                                                                    </td>
                                                                                    <td class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4">
                                                                                        <cc1:ExtendedDropDownList ID="extddlAdjuster" Width="100%" runat="server" Connection_Key="Connection_String"
                                                                                            Selected_Text="--- Select ---" Flag_Key_Value="GET_ADJUSTER_LIST" Procedure_Name="SP_MST_ADJUSTER"
                                                                                            AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlAdjuster_extendDropDown_SelectedIndexChanged" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Phone</div>
                                                                                    </td>
                                                                                    <td rowspan="2" class="tablecellSpace">
                                                                                    </td>
                                                                                    <td colspan="4" rowspan="2">
                                                                                        <div class="lbl">
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td width="25%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtAdjusterPhone" runat="server" ReadOnly="true"></asp:TextBox></span></td>
                                                                                                    <td width="13%" class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Extension</div>
                                                                                                    </td>
                                                                                                    <td width="25%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtAdjusterExtension" runat="server" ReadOnly="true"></asp:TextBox></span></td>
                                                                                                    <td width="5%" class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Fax</div>
                                                                                                    </td>
                                                                                                    <td width="32%">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox ID="txtfax" runat="server" ReadOnly="true"></asp:TextBox></span></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="3">
                                                                                                        <span class="tablecellControl">
                                                                                                            <asp:TextBox Width="98%" ID="txtEmail" runat="server" ReadOnly="true"></asp:TextBox></span>
                                                                                                        <div class="lbl">
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="tablecellLabel">
                                                                                                        &nbsp;</td>
                                                                                                    <td>
                                                                                                        &nbsp;</td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="26" class="tablecellLabel">
                                                                                        <div class="lbl">
                                                                                            Email</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <!-- End : Data Entry -->
                                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                                </td>
                                            </tr>
                                                <tr>
                                                <td class="ContentLabel" style="width: 100%; text-align:right;" colspan="4">
                                         
                                            <asp:TextBox ID="txtAccidentID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                CssClass="Buttons" />
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                Width="80px" CssClass="Buttons" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                CssClass="Buttons" />
                            <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                     <asp:DataGrid ID="grdPatientList" runat="server" OnDeleteCommand="grdPatientList_DeleteCommand"
                                OnPageIndexChanged="grdPatientList_PageIndexChanged" OnSelectedIndexChanged="grdPatientList_SelectedIndexChanged"
                                Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                PageSize="10" PagerStyle-Mode="NumericPages">
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_FIRST_NAME" HeaderText="First Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_LAST_NAME" HeaderText="Last Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="I_PATIENT_AGE" HeaderText="Age" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="Address" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_STREET" HeaderText="Street" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_CITY" HeaderText="City" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_ZIP" HeaderText="Zip" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Phone" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_EMAIL" HeaderText="Email" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MI" HeaderText="MI" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WCB_NO" HeaderText="WCB" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_SOCIAL_SECURITY_NO" HeaderText="Social Security No"
                                        Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DT_DOB" HeaderText="Date Of Birth" DataFormatString="{0:MM/dd/yyyy}">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_GENDER" HeaderText="Gender"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DT_INJURY" HeaderText="Date of Injury" DataFormatString="{0:MM/dd/yyyy}">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_JOB_TITLE" HeaderText="Job Title"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WORK_ACTIVITIES" HeaderText="Work Activities"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_STATE" HeaderText="State" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_CARRIER_CASE_NO" HeaderText="Carrier Case No" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Employer Name" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_PHONE" HeaderText="Employer Phone" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_ADDRESS" HeaderText="Employer Address" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_CITY" HeaderText="Employer City" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_STATE" HeaderText="Employer State" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_ZIP" HeaderText="Employer Zip" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WORK_PHONE" HeaderText="Employer City" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WORK_PHONE_EXTENSION" HeaderText="Employer State"
                                        Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="BT_WRONG_PHONE" HeaderText="Employer Zip" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BT_TRANSPORTATION" HeaderText="Employer Zip" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                                </Columns>
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" >
        <contenttemplate>
            <asp:Panel ID="pnlShowNotes" visible="false" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="Iframe2" src="Bill_Sys_PopupNotes.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            </iframe>
          </asp:Panel>
   </contenttemplate>
    </asp:UpdatePanel>
  
</asp:Content>
