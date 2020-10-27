<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_sys_unbilledTrasferTodoctor.aspx.cs" Inherits="AJAX_Pages_Bill_sys_unbilledTrasferTodoctor" Title="Green Your bills - Change Visits" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function OpenPopUPDisplayDiagCode(p_szCaseID,p_szDoctorID)
        {
            document.getElementById("divDisplayDiagCode").style.position = "absolute";
            document.getElementById("divDisplayDiagCode").style.left= "300px";
            document.getElementById("divDisplayDiagCode").style.top= "200px";
            document.getElementById('divDisplayDiagCode').style.zIndex= '1'; 
            document.getElementById("divDisplayDiagCode").style.visibility="visible";
            document.getElementById("divDisplayDiagCode").style.width= "400px";
            document.getElementById("divDisplayDiagCode").style.height= "250px";
            document.getElementById("ifrmDisplayDiagCode").style.width= "400px";
            document.getElementById("ifrmDisplayDiagCode").style.height= "250px";
            document.getElementById("ifrmDisplayDiagCode").src="../Bill_Sys_PopupShowDiagnosisCode.aspx?P_SZ_DOCTOR_ID=" + p_szDoctorID +  "&P_SZ_CASE_ID=" +p_szCaseID;
        }
        function closePopup()
        {
           document.getElementById('divid').style.zIndex = '-1'; 
           document.getElementById('divid').style.visibility='hidden';
        }
        function Clear() 
        {
           // alert('ca');
            document.getElementById("<%=txtScheduleDate.ClientID %>").value ='';
            document.getElementById('ctl00_ContentPlaceHolder1_extddlDoctor').value = 'NA';
          //  alert('end');
        }
        
        function Clear1() 
        {
           // alert('ca');
            document.getElementById("<%=txtUpdateDoctor.ClientID %>").value ='';
            document.getElementById('ctl00_ContentPlaceHolder1_extdupdateDoctor').value = 'NA';
           // alert('end');
        }
        function CheckValidate() 
       {
        
            if(Validate())
            {
             var spciality = document.getElementById('ctl00_ContentPlaceHolder1_extdupdateDoctor').value;
                var day = document.getElementById("<%=txtUpdateDoctor.ClientID %>").value;
               // alert(spciality + '' + day);
                if ((day == "" && spciality != "NA") || (day != "" && spciality == "NA") || (day != "" && spciality != "NA"))
                {
                    var f1= document.getElementById("<%= grdPatientList.ClientID %>");	
                    var count = 0;
                    for(var i=0; i<f1.getElementsByTagName("input").length ;i++ )
		            {		
		                if(f1.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f1.getElementsByTagName("input").item(i).checked != false)
			                {
			                    count = count + 1;
			                }
			            }			
		           }
		           
		           if (confirm('You have selected ' + count + ' patients for changes, Do you want to proceed?') == true) 
                        {
                            return true;
                        }
                        else 
                        {                                                                        
                            return false;
                        }
                }
                else 
                {
                  // alert("false");
                  alert("Please select updated date and  speciality doctor");
                    return false;
                }
            }
            return false;
            
            
       }
       
       
       
       function Validate()
       {
         
            var f= document.getElementById("<%= grdPatientList.ClientID %>");	
            if (f==null)
            {
            alert('Please select case.');
		    return false;
            }
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            return true;
			        }
			    }			
		    }
		    alert('Please select case');
		    return false;
       }
       
       function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdPatientList.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
        
        
</script>
<asp:ScriptManager ID ="ScriptManager1" runat="Server">
</asp:ScriptManager>
 <table>
 
    <tr>
    <td>
           <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
            <tr>
                <td height="28" align="left" valign="middle" bgcolor="#b5df82" class="txt2" colspan="2">
                    <b class="txt3">Search Parameters</b>
                </td>
             </tr>
                <tr>
                <td colspan ="2">
                <table>
                <tr>
                     <td class="td-widget-bc-search-desc-ch1" style="width: 146px">
                          Schdeule Date
                     </td>
                     <td class="td-widget-bc-search-desc-ch1">
                          Doctor Name
                     </td>
                </tr>
                <tr> 
                    <td class="td-widget-bc-search-desc-ch3" valign="top" style="width: 146px">
            <asp:TextBox ID="txtScheduleDate" onkeypress="return CheckForInteger(event,'/')"
                runat="server"  MaxLength="10" Width="80%" CssClass="search-input"></asp:TextBox>
            <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" valign="bottom">
            </asp:ImageButton>
            
        </td>
        <td class="td-widget-bc-search-desc-ch3" valign="top">
            <extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="100%" Selected_Text="---Select---"
                         Procedure_Name="SP_MST_DOCTOR" Flag_Key_Value="GETDOCTORLIST" Connection_Key="Connection_String" >
                        </extddl:ExtendedDropDownList>
             
        </td>
          </tr>
                    
                <tr>
                    <td class="td-widget-bc-search-desc-ch3" valign="bottom">
                            <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtScheduleDate"
                                     PopupButtonID="imgbtnDateofAccident">
                            </ajaxToolkit:CalendarExtender>
            
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txtScheduleDate" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                ControlToValidate="txtScheduleDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
            <asp:RangeValidator runat="server" id="rngDate" controltovalidate="txtScheduleDate" type="Date" minimumvalue="01/01/1901" maximumvalue="12/31/2099" errormessage="Enter a valid date " />
              
                    </td>
                  
                </tr>
               
                </table>
                   
                </td>
                
               
               </tr>
               
                  <tr>
                   <td colspan=2 align="center" >
                  <asp:UpdatePanel ID="btnpanle1" runat="server">
                  <ContentTemplate>
                      <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="80px" Text="Search" ></asp:Button>
             <%--<asp:Button ID="btnClear"  OnClientClick="Clear();"  Width="80px" Text="Clear"></asp:Button>--%>
             <input type="button" id="btnClear" onclick="Clear();" style="width:80px" value="Clear" />        
                  </ContentTemplate>
                  </asp:UpdatePanel>
     
      </td>
       </tr> 
        </table>
    </td>
    <td>
        <table>
            <tr>
                <td valign="bottom">
                   
                </td>
                
            </tr>
        </table>
    </td>
    
  
    </tr>
 </table>
 <asp:UpdatePanel ID="upnlupdate" runat="server">
 <ContentTemplate>
 <table width="100%">
 <tr>
    <td>
         <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
    </td>
 </tr>
 <tr>
    <asp:Label ID="timeid" Text ="exetime" runat="Server"></asp:Label>
 <asp:Label ID="Label1" Text ="exetime" runat="Server"></asp:Label>
 </tr>
 
        <tr>
            <td height="28" align="left"  bgcolor="#B5DF82" class="txt2" style="width: 100%">
           <%--  <b class="txt3">UnBilled Visit</b>--%>
             
             <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upnlupdate"
                 DisplayAfter="10">
                 <ProgressTemplate>
                     <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                         runat="Server">
                         <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                             Height="25px" Width="24px"></asp:Image>
                         Loading...</div>
                 </ProgressTemplate>
             </asp:UpdateProgress>
            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="btnpanle1"
                 DisplayAfter="10">
                 <ProgressTemplate>
                     <div id="Div2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                         runat="Server">
                         <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                             Height="25px" Width="24px"></asp:Image>
                         Loading...</div>
                 </ProgressTemplate>
             </asp:UpdateProgress>
             </td>
        </tr>
         
        <tr>
            <td>
                   <table style="vertical-align: middle; width: 100%;">
                    <tbody> 
                        <tr>
                           <td style="vertical-align: middle; width: 30%" align="left">
                               <gridsearch:XGridSearchTextBox id="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input" Visible="FALSE">
                                                    </gridsearch:XGridSearchTextBox>
                             
                           </td>
                           <td style="width: 60%" align="right">
                              Record Count:<%= this.grdPatientList.RecordCount%> | Page Count: <gridpagination:XGridPaginationDropDown id="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown> 
                                                 <asp:LinkButton id="lnkExportToExcel" onclick="lnkExportTOExcel_onclick" runat="server" Text="Export TO Excel">
                                             <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton> 
                                   
                            </td>                                                                                                                                   
                        </tr>
                    </tbody>
                </table>
               
            </td>
        </tr>
        <tr>
        <td>
              <xgrid:XGridViewControl ID="grdPatientList" runat="server" Width="100%" CssClass="mGrid"
                 MouseOverColor="0, 153, 153"
                 EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                 AlternatingRowStyle-BackColor="#EEEEEE" 
                 ShowExcelTableBorder="true" 
                 AllowPaging="true" XGridKey="UnbilledDoctorUpdate" PageRowCount="50" PagerStyle-CssClass="pgr"
                 AllowSorting="true" AutoGenerateColumns="false"  GridLines="None" DataKeyNames ="EVENTID,SZ_PATIENT_ID,SZ_COMPANY_ID"
                 ExportToExcelColumnNames="Case#,PatientName,VisitDate,DoctorName,Speciality"
                 ExportToExcelFields ="SZ_CASE_NO,SZ_PATIENT_NAME,DT_EVENT_DATE,DOCTOR_NAME,Speciality"
                 >
                 <Columns>
                
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                    <HeaderTemplate>
                                             <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                          </HeaderTemplate>
                         <ItemTemplate>
                             <asp:CheckBox ID="chkSelect" runat="server" />
                         </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                         <ItemStyle HorizontalAlign="Left"></ItemStyle>
                     </asp:TemplateField>
                    <asp:BoundField HeaderText="Case #" DataField="SZ_CASE_NO"  />
                                 <asp:BoundField HeaderText="PatientName" DataField="SZ_PATIENT_NAME"  />
                                 <asp:BoundField HeaderText="VisitDate" DataField="DT_EVENT_DATE"  />
                                 <asp:BoundField HeaderText="DoctorName" DataField="DOCTOR_NAME"  />
                                 <asp:BoundField HeaderText="Speciality" DataField="Speciality"  />
                                
                                <asp:BoundField HeaderText="eventid" DataField="EVENTID"  VISIBLE="FALSE"/>   
                                <asp:BoundField HeaderText="PatientID" DataField="SZ_PATIENT_ID" VISIBLE="FALSE"  />   
                                <asp:BoundField HeaderText="companyid" DataField="SZ_COMPANY_ID" VISIBLE="FALSE"  />   
                                </Columns>
                   </xgrid:XGridViewControl>
        </td>
        </tr>
    
        </table>
        <table width="100%">
        <tr>
            <td align="center">
               <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 25%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
            <tr>
                <td height="10" align="left" valign="middle" bgcolor="#b5df82" class="txt2" colspan="2">
                    
                </td>
             </tr>
                <tr>
                <td colspan ="2" align="center">
                <table>
                <tr>
                     <td class="td-widget-bc-search-desc-ch1" style="width: 146px">
                          Schdeule Date
                     </td>
                     <td class="td-widget-bc-search-desc-ch1">
                          Doctor Name
                     </td>
                </tr>
                <tr> 
                    <td class="td-widget-bc-search-desc-ch3" valign="top" style="width: 146px">
            <asp:TextBox ID="txtUpdateDoctor" onkeypress="return CheckForInteger(event,'/')"
                runat="server"  MaxLength="10" Width="80%" CssClass="search-input"></asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" valign="bottom">
            </asp:ImageButton>
            
        </td>
        <td class="td-widget-bc-search-desc-ch3" valign="top">
            <extddl:ExtendedDropDownList ID="extdupdateDoctor" runat="server" Width="100%" Selected_Text="---Select---"
                         Procedure_Name="SP_MST_DOCTOR" Flag_Key_Value="GETDOCTORLIST" Connection_Key="Connection_String" >
                        </extddl:ExtendedDropDownList>
             
        </td>
          </tr>
                    
                <tr>
                  <td class="td-widget-bc-search-desc-ch3" valign="bottom">
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtUpdateDoctor"
                                     PopupButtonID="ImageButton1">
                            </ajaxToolkit:CalendarExtender>
            
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txtUpdateDoctor" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                ControlToValidate="txtUpdateDoctor" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
            <asp:RangeValidator runat="server" id="RangeValidator1" controltovalidate="txtUpdateDoctor" type="Date" minimumvalue="01/01/1901" maximumvalue="12/31/2099" errormessage="Enter a valid date " />
              
                    </td>
                  
                </tr>
               
                </table>
                   
                </td>
                
                <td colspan=2>
                   <td colspan ="2">
          
                   
                </td>
                </td>
               </tr>
               
                  <tr>
      <td colspan=2 align="center" >
   
         <asp:Button ID="btnassign" Text ="Change" runat="server" OnClick="btnassign_Click" OnClientClick="return CheckValidate()"/>
         
            <asp:Button ID="Button1" Text ="Bucket" runat="server" OnClick="btnassign_Click1" OnClientClick="return CheckValidate()"/>
             <%--<asp:Button ID="btnClear"  OnClientClick="Clear();"  Width="80px" Text="Clear"></asp:Button>--%>
             <input type="button" id="Button2" onclick="Clear1();" style="width:80px" value="Clear" />
      
           

      </td>
       </tr> 
        </table>
            </td>
        </tr>
        </table>
        
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch"  />
        </Triggers>
        </asp:UpdatePanel>
  <div id="divDisplayDiagCode" style="position: absolute; width: 400px; height: 300px; background-color: #DBE6FA;visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4; width: 400px">
            <a onclick="document.getElementById('divDisplayDiagCode').style.zIndex = '-1'; document.getElementById('divDisplayDiagCode').style.visibility='hidden';" style="cursor: pointer;" title="Close">X</a>
         
          
        </div>
        <iframe id="ifrmDisplayDiagCode" src="" frameborder="0" height="250px" width="400px"></iframe>
    </div>
    <asp:TextBox ID="txtScheduleid" runat="server" Text=""  Visible="false" ></asp:TextBox>
     <asp:TextBox ID="txtCompanyID" runat="server" Text="" Visible="false"></asp:TextBox>
  </asp:Content>

