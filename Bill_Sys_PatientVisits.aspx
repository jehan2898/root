<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_PatientVisits.aspx.cs" Inherits="Bill_Sys_PatientVisits" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
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
                                                    Total Visits:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblTotalPatientVisitCount" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Last Visit Date:
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:Label ID="lblPatientLastVisitDate" runat="server"></asp:Label>
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
                        Latest Visit
                        </td> 
                        </tr> 
                    <tr>
                        <td style="width: 100%" class="TDPart">
                            <asp:DataGrid ID="grdPatientLatestVisit"  Width="100%"  CssClass="GridTable" AutoGenerateColumns="false" runat="server">
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="Visit ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Doctor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoctorName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="DT_DATE" HeaderText="Visit Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Visit"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Visit Description"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="Procedure Code ID" Visible="false"></asp:BoundColumn>
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
                            <ajaxToolkit:TabContainer ID="tabcontainerPatientVisit" runat="Server" ActiveTabIndex="0" CssClass="ajax__tab_theme">
                                <ajaxToolkit:TabPanel runat="server" ID="tabPanelScheduledVisit" TabIndex="0">
                                    <HeaderTemplate>
                                        <div style="width: 150px; text-align: center;" class="lbl">
                                            Scheduled Visits</div>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="overflow-y: scroll; height: 300px;">
                                            <asp:DataGrid ID="grdScheduledVisits" runat="server"  Width="100%"  CssClass="GridTable" AutoGenerateColumns="false" OnItemCommand="grdScheduledVisits_ItemCommand">
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
                                                    <asp:BoundColumn DataField="DT_DATE" HeaderText="Visit Date" DataFormatString="{0:MM/dd/yyyy}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Visit"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Visit Description"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="Visit Code" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Visit Time" Visible="true"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" ID="tabpanelBilledVisit" TabIndex="1">
                                    <HeaderTemplate>
                                        <div style="width: 150px; text-align: center;" class="lbl">
                                            View Billed Visits</div>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="overflow-y: scroll; height: 300px;">
                                            <asp:DataGrid ID="grdBilledVisits" Width="100%"  CssClass="GridTable" AutoGenerateColumns="false"  runat="server">
                                              <HeaderStyle CssClass="GridHeader" />
                                                     <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="Visit ID" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Doctor Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="DT_DATE" HeaderText="Visit Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Visit"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Visit Description"></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle />
                                            </asp:DataGrid>
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" ID="tabpanelViewVisit" TabIndex="2">
                                    <HeaderTemplate>
                                        <div style="width: 150px; text-align: center;" class="lbl">
                                            View Un-Billed Visits</div>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="53%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td colspan="3" height="30">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Doctor Name</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <extddl:ExtendedDropDownList ID="extddlViewDoctor" runat="server" Width="200px" Connection_Key="Connection_String" Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlViewDoctor_extendDropDown_SelectedIndexChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Total Visits</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:Label ID="lblTotalVisits" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Last Visit Date</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:Label ID="lblLastDate" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel" valign="top" style="height: 24px">
                                                    <div class="lbl">
                                                        Visits</div>
                                                </td>
                                                <td class="tablecellSpace" style="height: 24px">
                                                </td>
                                                <td class="tablecellControl" style="height: 24px">
                                                    <asp:ListBox ID="lstViewDoctorVisit" runat="server" Height="200px" Width="300px"></asp:ListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlAddNewVisit" TabIndex="3">
                                    <HeaderTemplate>
                                        <div style="width: 150px; text-align: center;" class="lbl">
                                            Add New Visits</div>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="53%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td colspan="3" height="30">
                                                    <div id="Div1" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Doctor Name</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="200px" Connection_Key="Connection_String" Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" AutoPost_back="true" Selected_Text="---Select---" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Visit Date</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:RadioButton ID="rdoOn" runat="server" Visible="true" Text="On" Checked="True" GroupName="OnFromTO" AutoPostBack="true" OnCheckedChanged="rdoOn_CheckedChanged" />
                                                    <asp:RadioButton ID="rdoFromTo" runat="server" Visible="true" Text="From To" GroupName="OnFromTO" AutoPostBack="true" OnCheckedChanged="rdoFromTo_CheckedChanged" />
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
                                                    <asp:Label ID="lblDateOfService" runat="server" Text="From" Font-Bold="False" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtVisitDateFrom" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnVisitDateFrom" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtVisitDateFrom" PopupButtonID="imgbtnVisitDateFrom" />
                                                    <asp:Label ID="lblTo" runat="server" Text="To" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtVisitDateTo" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Visible="false"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnVisitDateTo" runat="server" ImageUrl="~/Images/cal.gif" Visible="false" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtVisitDateTo" PopupButtonID="imgbtnVisitDateTo" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel" valign="top" style="height: 24px">
                                                    <div class="lbl">
                                                        Visits</div>
                                                </td>
                                                <td class="tablecellSpace" style="height: 24px">
                                                </td>
                                                <td class="tablecellControl" style="height: 24px">
                                                    <asp:ListBox ID="lstPatientVisits" runat="server" Height="200px" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center">
                                                    <asp:Button ID="btnVisitSave" runat="server" Text="Add" Width="80px" CssClass="Buttons" OnClick="btnVisitSave_Click" />&nbsp;
                                                    <asp:Button ID="btnVisitClear" runat="server" Text="Clear" Width="80px" CssClass="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" ID="tabpnlSummary" TabIndex="4">
                                    <HeaderTemplate>
                                        <div style="width: 150px; text-align: center;" class="lbl">
                                            Summary</div>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="overflow-y: scroll; height: 300px;">
                                            <asp:DataGrid ID="grdPatientVisitList" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  runat="server" OnItemCommand="grdPatientVisitList_ItemCommand">
                                            <HeaderStyle CssClass="GridHeader" />
                                                     <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="Visit ID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Doctor Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                            <asp:LinkButton ID="lnkAdd" runat="server" CommandName="Add" Text="Add" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>'></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandName="View" Text="View" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_ID") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="DT_DATE" HeaderText="Visit Date" DataFormatString="{0:MM/dd/yyyy}">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_CODE" HeaderText="Visit"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_DESCRIPTION" HeaderText="Visit Description"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="Procedure Code" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BILLED_STATUS" HeaderText="Bill Status"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
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

