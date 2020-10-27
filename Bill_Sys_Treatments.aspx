<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Treatments.aspx.cs"
    Inherits="Bill_Sys_Treatments" MasterPageFile="~/MasterPage.master"  %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript" src="validation.js"></script>

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


    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
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
                                         <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server" AutoGenerateColumns="False">
                                                    <HeaderStyle CssClass="GridHeader"/>
                            <ItemStyle CssClass="GridRow"/>
                                                            <Columns>
                                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                
                                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                                <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                <a href="#" onclick="return openTypePage('a')">
                                                                <img src="Images/actionEdit.gif" style="border:none;"/>
                                                                </a> 
                                                                </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                  </asp:DataGrid>		
                                    </td> 
                                    </tr> 
                                      <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>                                           
  
                                            <tr>
                                            
                                                <td class="ContentLabel" style="width: 15%">
                                                    No of treatment:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblNoOfTreatment" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Last treatment:
                                                </td>
                                                <td style="width: 35%">
                                                   <asp:Label ID="lblLastTreatment" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Total Billed Count:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblTotalBilledCount" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Total Un-Billed Count:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblTotalUnBilledCount" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
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
                        Latest Treatment
                        </td> 
                        </tr> 
                    <tr>
                        <td style="width: 100%" class="TDPart">
                            <asp:DataGrid ID="grdLatestTreatment"  Width="100%"  CssClass="GridTable" AutoGenerateColumns="false" runat="server">
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                        <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="Treatment ID" Visible="false">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Doctor Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoctorName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="DT_DATE" HeaderText="Treatment Date" DataFormatString="{0:MM/dd/yyyy}"
                                            ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Treatment"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Treatment Description">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="Procedure Code ID"
                                            Visible="false"></asp:BoundColumn>
                                    </Columns>
                                <HeaderStyle CssClass="GridHeader" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" class="SectionDevider">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" class="TDPart">
                            <ajaxToolkit:TabContainer ID="tabcontainerPatientTreatment" runat="Server" ActiveTabIndex="0" CssClass="ajax__tab_theme">
                                                                            <ajaxToolkit:TabPanel runat="server" TabIndex="0" ID="tabPanelScheduledtreatments">
                                                                                <ContentTemplate>
                                                                                    <div style="overflow-y: scroll; height: 300px">
                                                                                        <asp:DataGrid ID="grdScheduledTreatments" runat="server" OnItemCommand="grdScheduledVisits_ItemCommand" Width="100%"  CssClass="GridTable" AutoGenerateColumns="false">
                                                                                         <HeaderStyle CssClass="GridHeader" />
                                                     <ItemStyle CssClass="GridRow" />
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="False"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="False"></asp:BoundColumn>
                                                                                                <asp:TemplateColumn HeaderText="Doctor Name">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDocName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                                                                        <asp:LinkButton ID="lnkAdd" runat="server" CommandName="Add" Text="Add" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>'></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:BoundColumn DataField="DT_DATE" HeaderText="Treatment Date" DataFormatString="{0:MM/dd/yyyy}">
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Treatment"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Treatment Description">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="Treatment Code" Visible="False">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Treatment Time"></asp:BoundColumn>
                                                                                            </Columns>
                                                                                        </asp:DataGrid>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    <div style="width: 150px; text-align: center;" class="lbl">
                                                                                        Scheduled Treatments</div>
                                                                                </HeaderTemplate>
                                                                            </ajaxToolkit:TabPanel>
                                                                            <ajaxToolkit:TabPanel runat="server" ID="tabpanelBilledTreatment" TabIndex="1">
                                                                                <ContentTemplate>
                                                                                    <div style="overflow-y: scroll; height: 300px;">
                                                                                        <asp:DataGrid ID="grdBilledTreatment" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="false">
                                                                                           <HeaderStyle CssClass="GridHeader" />
                                                     <ItemStyle CssClass="GridRow" />
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="Treatment ID" Visible="false">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="false"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                                                                                <asp:TemplateColumn HeaderText="Doctor Name">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:BoundColumn DataField="DT_DATE" HeaderText="Treatment Date" DataFormatString="{0:MM/dd/yyyy}"
                                                                                                    ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Treatment"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Treatment Description">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="Procedure Code" Visible="false">
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                            
                                                                                        </asp:DataGrid>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    <div style="width: 150px; text-align: center;" class="lbl">
                                                                                        View Billed Treatment</div>
                                                                                </HeaderTemplate>
                                                                            </ajaxToolkit:TabPanel>
                                                                            <ajaxToolkit:TabPanel runat="server" TabIndex="2" ID="tabpnlViewTreatment">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <table width="53%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                            <tr>
                                                                                                <td class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        Doctor Name</div>
                                                                                                </td>
                                                                                                <td class="tablecellSpace">
                                                                                                </td>
                                                                                                <td class="tablecellControl">
                                                                                                    <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"
                                                                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        No of treatment</div>
                                                                                                </td>
                                                                                                <td class="tablecellSpace">
                                                                                                </td>
                                                                                                <td class="tablecellControl">
                                                                                                    <div class="lbl">
                                                                                                        <asp:Label ID="lblTotalTreatment" runat="server"></asp:Label></div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="tablecellLabel">
                                                                                                    <div class="lbl">
                                                                                                        Last treatment</div>
                                                                                                </td>
                                                                                                <td class="tablecellSpace">
                                                                                                </td>
                                                                                                <td class="tablecellControl">
                                                                                                    <div class="lbl">
                                                                                                        <asp:Label ID="lblLastTreatmentDate" runat="server"></asp:Label></div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="tablecellLabel" valign="top">
                                                                                                    <div class="lbl">
                                                                                                        Treatment List</div>
                                                                                                </td>
                                                                                                <td class="tablecellSpace">
                                                                                                </td>
                                                                                                <td class="tablecellControl">
                                                                                                    <asp:ListBox ID="lstTreatmentList" runat="server" SelectionMode="multiple" Width="300px">
                                                                                                    </asp:ListBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    <div style="width: 150px; text-align: center;" class="lbl">
                                                                                        View Un-Billed Treatment</div>
                                                                                </HeaderTemplate>
                                                                            </ajaxToolkit:TabPanel>
                                                                            <ajaxToolkit:TabPanel runat="server" TabIndex="3" ID="tabpnlAddTreatment">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <table cellspacing="3" cellpadding="0" width="53%" align="center" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Doctor Name</div>
                                                                                                    </td>
                                                                                                    <td class="tablecellSpace">
                                                                                                    </td>
                                                                                                    <td class="tablecellControl">
                                                                                                        <extddl:ExtendedDropDownList ID="extddlDocName" runat="server" Width="200px" OnextendDropDown_SelectedIndexChanged="extddlDocName_extendDropDown_SelectedIndexChanged"
                                                                                                            Selected_Text="---Select---" Connection_Key="Connection_String" Flag_Key_Value="GETDOCTORLIST"
                                                                                                            AutoPost_back="True" OldText="" StausText="False" Procedure_Name="SP_MST_DOCTOR"></extddl:ExtendedDropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                            Treatment Date</div>
                                                                                                    </td>
                                                                                                    <td class="tablecellSpace">
                                                                                                    </td>
                                                                                                    <td class="tablecellControl">
                                                                                                        <asp:RadioButton ID="rdoOn" runat="server" OnCheckedChanged="rdoOn_CheckedChanged"
                                                                                                            Checked="True" Text="On" AutoPostBack="True" GroupName="OnFromTO">
                                                                                                        </asp:RadioButton>
                                                                                                        <asp:RadioButton ID="rdoFromTo" runat="server" OnCheckedChanged="rdoFromTo_CheckedChanged"
                                                                                                            Text="From To" AutoPostBack="True" GroupName="OnFromTO">
                                                                                                        </asp:RadioButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="tablecellLabel">
                                                                                                        <div class="lbl">
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="tablecellSpace">
                                                                                                    </td>
                                                                                                    <td class="tablecellControl">
                                                                                                        <asp:Label ID="lblDateOfService" runat="server" Visible="False" Text="From" Font-Bold="False"></asp:Label>
                                                                                                        <asp:TextBox ID="txtTreatmentDateFrom" onkeypress="return CheckForInteger(event,'/')"
                                                                                                            runat="server" CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                                                                        <asp:ImageButton ID="imgbtnTreatmentDateFrom" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnTreatmentDateFrom"
                                                                                                            Enabled="True" TargetControlID="txtTreatmentDateFrom">
                                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                                        <asp:Label ID="lblTo" runat="server" Visible="False" Text="To"></asp:Label>
                                                                                                        <asp:TextBox ID="txtTreatmentDateTo" onkeypress="return CheckForInteger(event,'/')"
                                                                                                            runat="server" Visible="False" CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                                                                        <asp:ImageButton ID="imgbtnTreatmentDateTo" runat="server" Visible="False" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgbtnTreatmentDateTo"
                                                                                                            Enabled="True" TargetControlID="txtTreatmentDateTo">
                                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="tablecellLabel" valign="top">
                                                                                                        <div class="lbl">
                                                                                                            Treatment
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="tablecellSpace">
                                                                                                    </td>
                                                                                                    <td class="tablecellControl">
                                                                                                        <asp:ListBox ID="lstDoctorTreatment" runat="server" Width="200px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <asp:Button ID="btnSaveTreatment" OnClick="btnSaveTreatment_Click" runat="server"
                                                                                                            CssClass="Buttons" Width="80px" Text="Add"></asp:Button>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    <div style="width: 150px; text-align: center;" class="lbl">
                                                                                        Add Treatment</div>
                                                                                </HeaderTemplate>
                                                                            </ajaxToolkit:TabPanel>
                                                                            <ajaxToolkit:TabPanel runat="server" TabIndex="4" ID="tabpnlSummary">
                                                                                <ContentTemplate>
                                                                                    <div style="overflow-y: scroll; height: 300px;">
                                                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <asp:DataGrid ID="grdSummaryTreatmentGrid" Width="100%"  CssClass="GridTable" AutoGenerateColumns="false" runat="server" ShowFooter="True" OnItemCommand="grdSummaryTreatmentGrid_ItemCommand">
                                                                                                        <HeaderStyle CssClass="GridHeader" />
                                                     <ItemStyle CssClass="GridRow" />
                                                                                                        <Columns>
                                                                                                            <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="Treatment ID" Visible="false">
                                                                                                            </asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="false"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                                                                                            <asp:TemplateColumn HeaderText="Doctor Name">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblDocName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                                                                                     <asp:LinkButton ID="lnkAdd" runat="server" CommandName="Add" Text="Add" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>'></asp:LinkButton>
                                                                                                            <asp:LinkButton ID="lnkView" runat="server" CommandName="View" Text="View" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>'></asp:LinkButton>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateColumn>
                                                                                                            <asp:BoundColumn DataField="DT_DATE" HeaderText="Treatment Date" DataFormatString="{0:MM/dd/yyyy}"
                                                                                                                ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Treatment"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Treatment Description">
                                                                                                            </asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="Procedure Code" Visible="false">
                                                                                                            </asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="BILLED_STATUS" HeaderText="Bill Status"></asp:BoundColumn>
                                                                                                        </Columns>
                                                                                                       
                                                                                                    </asp:DataGrid>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                                <HeaderTemplate>
                                                                                    <div style="width: 150px; text-align: center;" class="lbl">
                                                                                        Summary Treatment</div>
                                                                                </HeaderTemplate>
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
    </td> </tr> </table>
    
      <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"></iframe>
    </div>
</asp:Content>


