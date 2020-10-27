<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_PrintPOM.aspx.cs" Inherits="Bill_Sys_PrintPOM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager id="scriptmanager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript" src="validation.js">   </script>
    <script language="javascript" type="text/javascript">
    function confirm_check()
		 {
		 
		 if(document.getElementById("_ctl0_ContentPlaceHolder1_extddlOffice").value == 'NA') 
                {
                    alert('Please select provider');
                  
                    return false;
                }
		        var f= document.getElementById('_ctl0_ContentPlaceHolder1_grdUnsentNF2');
		        
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkSent') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    bfFlag = true;
			                }
			            }
			        }			
		        }
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
		         else
		        {
		            return true;
		        }
		   }     
      </script>
 

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;background-color:White;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="updfate4" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%">
                                <tr>
                                    <td style="width: 100%;height:50%;"  >
                                        <table border="0" cellspacing="0"  style="width: 100%">
                                         
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; " colspan="4">
                                                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="lblMsg" runat="server" CssClass="message-text" Visible="false"></asp:Label>
                                        <div id="ErrorDiv" style="color: red" visible="true">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                     <td   style="width: 100%; height: 30%">
                                          <table style="width: 100%; height: 30%">
                                          <tr>
                                          <td style="width: 40%; height: 30%">
                                                  <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                    height: 50%; border: 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="height: 0px;" align="center" colspan="2">
                                                               
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height:15px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                   <td class="td-widget-bc-search-desc-ch" style="height: 18px;width:40%;">
                                                                                        Case No
                                                                                    </td>
                                                                                   <td class="td-widget-bc-search-desc-ch" style="height: 40px;width:60%;">
                                                                                      
                                                                                         <asp:TextBox ID="txtCaseNo" Height="80px" runat="server" Width="92%"   TextMode ="MultiLine"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px;width:40%;">
                                                                                        Patient Name
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px;width:60%;">
                                                                                         <asp:TextBox ID="txtPatientName" runat="server" Width="92%" autocomplete="off"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="vertical-align: middle; text-align: center; width: 527px; height: 40px;">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                        <asp:Button id="btnSearch" onclick="btnSearch_Click" runat="server" CssClass="Buttons" Text="Search" __designer:wfdid="w4"></asp:Button> 
</ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                         
                                                                         </td>
                                                                    </tr>
                                                                </table>
                                                                </td>
                                                                <td style="width:60%; height: 30%"></td>
                                                                </tr>
                                        </table>                            
                                    </td>             
                                                                                                                                                                                          
                                </tr>                                
                                <tr>
                                    <td style="width: 100%"  >
                                      <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%; height: 41px;" align="left">
                                                <asp:Label ID="lblprovider" runat="server" CssClass="lbl" Text="Provider Name"></asp:Label>
                                                &nbsp; &nbsp;
                                            <cc1:ExtendedDropDownList ID="extddlOffice" Width="200px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---"/>
                                                <asp:Button ID="btnSendMail" runat="server" Text="Print POM" Width="80px" CssClass="Buttons" OnClick="btnSendMail_Click"/>
                                                 <asp:Button ID="Button1" runat="server" Text="Print Envelope" Width="93px" CssClass="Buttons" OnClick="btnPrintEnvelop_Click" />
                                                 <asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" CausesValidation="False" />
                                                 <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; 
                                            
                                                
                                                    
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:DataGrid ID="grdUnsentNF2" Width="100%" runat="Server" CssClass="GridTable"
                                                    AutoGenerateColumns="False" OnItemCommand="grdUnsentNF_ItemCommand" >
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Sent">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkSent" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                       
                                                        <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lbl_Location_Id" Text="Location" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkSelectCase2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>' CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Select"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Insurance Details" ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtInsDetails" runat="server" Width="100%" ToolTip="Enter insurance company name"></asp:TextBox>
                                                                    <asp:TextBox ID="txtInsAddress" runat="server" Width="100%" ToolTip="Enter insurance company address/street"></asp:TextBox>
                                                                    <asp:TextBox ID="txtInsState" runat="server" Width="100%" ToolTip="Enter insurance company city, state zip"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtInsDescritpion" runat="server" TextMode="MultiLine" style="height:60px;width:200px;" Text='<%# DataBinder.Eval(Container,"DataItem.Description")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Accident Date"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DAYS" HeaderText="Days"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="CLAIM_NO" HeaderText="Claim Number" Visible="false">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address"
                                                            Visible="false"></asp:BoundColumn>
                                                             <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID"
                                                            Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="SZ_CASE_NO"
                                                            Visible="false"></asp:BoundColumn>
                                                            
                                                            
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                      </table>
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
    <div id="divDashBoard" visible="false" style="position: absolute; width: 600px; height: 475px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;" title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 430; float:left; position:relative;">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
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
                           <td style="width: 97%"  >
                               <table border="0"  id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0" style="width: 49%;height:140px; float:left;position:relative;" visible="false" >
                                <tr>
                                    <td class="TDHeading" style="width: 99%" valign="top">
                                        Today's Appointment</td>
                                </tr>
                                <tr>
                                    <td style="width: 99%"   valign="top">
                                        <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                    </td>
                                </tr>
                            </table>
                            
                            <table  id="tblWeeklyAppointment" runat="server"  border="0" cellpadding="0" cellspacing="0" style="width: 49%;height:140px;float:left;position:relative;" visible="false">
                                <tr>
                                    <td class="TDHeading" style="width: 99%">
                                        Weekly &nbsp;Appointment</td>
                                </tr>
                                <tr>
                                    <td style="width: 99%"  >
                                        <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                    </td>
                                </tr>
                            </table>                      
                                           
                                           
                                              <table id="tblDesk" runat="server"  border="0" cellpadding="0" cellspacing="0" style="width: 49%; height:140px; float:left;position:relative;" visible="false">
                                <tr>
                                    <td class="TDHeading" style="width: 99%" valign="top">
                                        Desk</td>
                                </tr>
                                <tr>
                                    <td style="width: 99%"   valign="top">
                                        You have&nbsp;
                                        <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                    </td>
                                </tr>
                            </table>
                            
                            
                            <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 49%; height:140px; float:left;position:relative;" visible="false">
                                <tr>
                                    <td class="TDHeading" style="width: 99%" valign="top">
                                        Bill Status</td>
                                </tr>
                                <tr >
                                    <td style="width: 99%"   valign="top">
                                        You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                         <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                    </td>
                                </tr>                               
                            </table>
                            
                        
                            
                            <table id="tblMissingInfo" runat="server"   border="0" cellpadding="0" cellspacing="0" style="width: 49%; height:140px; float:left;position:relative;" visible="false">
                                <tr>
                                    <td class="TDHeading" style="width: 99%" valign="top">
                                        Missing Information</td>
                                </tr>
                                <tr>
                                    <td style="width: 99%;"   valign="top">
                                        You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                    </td>
                                </tr>                              
                           </table>
                           
                           <table id="tblReportSection" runat="server"  border="0" cellpadding="0" cellspacing="0" style="width: 49%;height:140px;  float:left;position:relative;" visible="false">
                                  <tr>
                                    <td class="TDHeading" style="width: 99%" valign="top">
                                        Report Section</td>
                                </tr>
                                <tr>
                                    <td style="width: 99%"   valign="top" >
                                        You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                    </td>
                                </tr>
                            </table>
                            
                            
                            <table id="tblBilledUnbilledProcCode" runat="server"  border="0" cellpadding="0" cellspacing="0" style="width: 49%;height:140px;  float:left;position:relative;" visible="false">
                                  <tr>
                                    <td class="TDHeading" style="width: 99%" valign="top">
                                        Procedure Status</td>
                                </tr>
                                <tr>
                                    <td style="width: 99%"   valign="top" >
                                        You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
                                    </td>
                                </tr>
                            </table>
                            <table id="tblVisits" runat="server"  border="0" cellpadding="0" cellspacing="0" style="width: 49%;height:140px;  float:left;position:relative; left: 0px; top: 0px;" visible="false">
                                  <tr>
                                    <td class="TDHeading" style="width: 99%" valign="top">
                                        Visits</td>
                                </tr>
                                <tr>
                                    <td style="width: 99%"   valign="top" >
                                        You have &nbsp;<asp:Label ID="lblVisits" runat="server"></asp:Label>
                                       
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 99%" class="SectionDevider">
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
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px" visible="false"></iframe>
         <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
                    ServiceMethod="GetPatient" ServicePath="AJAX Pages/PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                </ajaxToolkit:AutoCompleteExtender>
    </div>
   
</asp:Content>
