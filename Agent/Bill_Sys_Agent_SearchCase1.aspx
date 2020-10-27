<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Agent_SearchCase1.aspx.cs" Inherits="Agent_Bill_Sys_Agent_SearchCase" Title="Agent Search Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization" TagPrefix="CPA" %>
<%@ Register Src="~/UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <script type="text/javascript">
    
     function ShowDental() 
     {
//     alert('hi');
        var url = 'FrmDentalClaimForm.aspx';
        ShowPopup.SetContentUrl(url);
        ShowPopup.Show();

        return false;
     }
    
    
    function ReminderPopup()
    {
                   // alert('Hi');
                    document.getElementById('ReminderPopUP').style.zIndex = 1;
                    document.getElementById('ReminderPopUP').style.position = 'absolute'; 
                    document.getElementById('ReminderPopUP').style.left= '350px'; 
                    document.getElementById('ReminderPopUP').style.top= '200px'; 
                    document.getElementById('ReminderPopUP').style.visibility='visible'; 
                    document.getElementById('frmReminder').src="Bill_Sys_Reminder_Pop_Up.aspx";
                    return false;            
    }
        function showCheckinPopup(objCaseID,objPatientID)
               {
                  
                    var obj3 = "";
                    document.getElementById('divid2').style.zIndex = 1;
                    document.getElementById('divid2').style.position = 'absolute'; 
                    document.getElementById('divid2').style.left= '350px'; 
                    document.getElementById('divid2').style.top= '200px'; 
                    document.getElementById('divid2').style.visibility='visible'; 
                    document.getElementById('iframeCheckIn').src="./Bill_Sys_CheckinPopup.aspx?CaseID="+objCaseID+"&PatientID="+objPatientID+"&CompID="+obj3+"";
                    return false;            
               }
//               function showimcheckPopup()
//               {
//                    
//                    document.getElementById('checkimpopup').style.zIndex = 1;
//                    document.getElementById('checkimpopup').style.position = 'absolute'; 
//                    document.getElementById('checkimpopup').style.left= '350px'; 
//                    document.getElementById('checkimpopup').style.top= '200px'; 
//                    document.getElementById('checkimpopup').style.visibility='visible'; 
//                    document.getElementById('frmcheckim').src="Bill_Sys_IM_SheduleVisits.aspx";
//                    return false;            
//               } 
               
                 function CloseCheckimPopup()                                                                                                
               {
                    document.getElementById('checkimpopup').style.visibility='hidden';
                    document.getElementById('frmcheckim').src='Bill_Sys_IM_SheduleVisits.aspx';
        //            window.parent.document.location.href='Bill_Sys_CheckOut.aspx';            
               }
                function showReminderPopup()
               {
                    
                    document.getElementById('ReminderPopUP').style.zIndex = 1;
                    document.getElementById('ReminderPopUP').style.position = 'absolute'; 
                    document.getElementById('ReminderPopUP').style.left= '350px'; 
                    document.getElementById('ReminderPopUP').style.top= '200px'; 
                    document.getElementById('ReminderPopUP').style.visibility='visible'; 
                    document.getElementById('frmReminder').src="Bill_Sys_Reminder_Pop_Up.aspx";
                    return false;            
               } 
               
                 function CloseCheckoutReminderPopup()                                                                                                
               {
                    document.getElementById('ReminderPopUP').style.visibility='hidden';
                    document.getElementById('frmReminder').src='Bill_Sys_Reminder_Pop_Up.aspx';
        //            window.parent.document.location.href='Bill_Sys_CheckOut.aspx';            
               }
               
               
               function showCheckinPopup()
               {
                  
                    var obj3 = "";
                    document.getElementById('divid2').style.zIndex = 1;
                    document.getElementById('divid2').style.position = 'absolute'; 
                    document.getElementById('divid2').style.left= '350px'; 
                    document.getElementById('divid2').style.top= '200px'; 
                    document.getElementById('divid2').style.visibility='visible'; 
                    document.getElementById('iframeCheckIn').src="./Bill_Sys_CheckinPopup.aspx?CaseID="+objCaseID+"&PatientID="+objPatientID+"&CompID="+obj3+"";
                    return false;            
               }
               
               
               function CloseCheckoutPopup()                                                                                                
               {
                    document.getElementById('divid3').style.visibility='hidden';
                    document.getElementById('iframeCheckOut').src='Bill_Sys_SearchCase.aspx';
        //            window.parent.document.location.href='Bill_Sys_CheckOut.aspx';            
               }
               function CloseCheckinPopup()
               {
                    document.getElementById('divid2').style.visibility='hidden';
                    document.getElementById('iframeCheckIn').src='Bill_Sys_SearchCase.aspx';
        //            window.parent.document.location.href='Bill_Sys_CheckOut.aspx';            
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
       function Clear()
       {
       // alert("call");
        document.getElementById("<%=txtSSNNo.ClientID%>").value='';
        document.getElementById("<%=txtClaimNumber.ClientID %>").value ='';
        document.getElementById("<%=txtpatientid.ClientID %>").value ='';
        document.getElementById("<%=txtPatientName.ClientID %>").value ='';
        document.getElementById("<%=txtDateofAccident.ClientID %>").value ='';
        document.getElementById("<%=txtDateofBirth.ClientID %>").value ='';
       // var extvisible = document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation");
        var extvisible = document.getElementById("<%=extddlLocation.ClientID %>");
        if (extvisible != null)
        {
            //document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").value = 'NA';
            document.getElementById("<%=extddlLocation.ClientID %>").value = 'NA';
        }
        
       // document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value ='NA';
        document.getElementById("<%=extddlCaseType.ClientID %>").value ='NA';
        //document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseStatus").value ='NA';
          document.getElementById("<%=extddlCaseStatus.ClientID %>").value ='NA';
         document.getElementById("<%=txtInsuranceCompany.ClientID %>").value='';
         document.getElementById("<%=txtCaseNo.ClientID %>").value='';
         var visiblechart = document.getElementById("<%=txtChartNo.ClientID %>");
         if (visiblechart != null)
         {
            document.getElementById("<%=txtChartNo.ClientID %>").value='';
         }
         
       }
       function Validate()
       {
            var f= document.getElementById("<%= grdPatientList.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            
			            if (confirm("Are you sure to continue for soft delete?"))
			            return true;
			            return false;
			        }
			    }			
		    }
		    
		    alert('Please select bill no.');
		   return false;
       }
       function Close()
       {
            
                 document.getElementById("<%=pnlDataList.ClientID %>").style.height='0px';
            document.getElementById("<%=pnlDataList.ClientID %>").style.visibility = 'hidden'; 
           
       }
       function spanhide(charttext,chartvalue) {
       //alert("call for hide");
        //document.getElementById('ctl00_ContentPlaceHolder1_PatientDtlView_ctl00_lblcharttext').style.display= 'none';
        document.getElementById(charttext).style.display= 'none';
        //document.getElementById('ctl00_ContentPlaceHolder1_PatientDtlView_ctl00_lblchartvalue').style.display= 'none';
        document.getElementById(chartvalue).style.display= 'none';
        
      //  alert("hide done");
       }
       
       
       
       
//       function showPateintFrame(objCaseID,objCompanyID)
//        {
//	    // alert(objCaseID + ' ' + objCompanyID);
//	        var obj3 = "";
//            document.getElementById('divfrmPatient').style.zIndex = 1;
//            document.getElementById('divfrmPatient').style.position = 'absolute'; 
//            document.getElementById('divfrmPatient').style.left= '400px'; 
//            document.getElementById('divfrmPatient').style.top= '250px'; 
//            document.getElementById('divfrmPatient').style.visibility='visible'; 
//            document.getElementById('frmpatient').src="PatientViewFrame.aspx?CaseID="+objCaseID+"&cmpId="+ objCompanyID+"";
//            return false;   
//        }
        
        function ClosePatientFramePopup()
               {
                 //   alert("");
                   //document.getElementById('divfrmPatient').style.height='0px';
                    document.getElementById('divfrmPatient').style.visibility='hidden';
                   document.getElementById('divfrmPatient').style.top='-10000px';
                    document.getElementById('divfrmPatient').style.left='-10000px';
             }
             
             
        function ValidateExportBill()
       {
            
            
           var f= document.getElementById("<%=grdPatientList.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                    bfFlag = true;
			                     return true;
			                }
			            }
			        }			
		        }
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }else
		        {
		            return true;
		        }
       }
       
        function showPateintFrame(objCaseID,objCompanyID)
        {   
              
             var url = "PatientViewFrame.aspx?CaseID="+objCaseID+"&cmpId="+ objCompanyID+"";
           //alert(url);
                              
              PatientInfoPop.SetContentUrl(url);
              PatientInfoPop.Show();
              return false;
            
        }
        
        function OnBillingSummary(szcaseid,szcompanyid)
        {
              var url = "Bill_Sys_Billing_Summary_Searchcase.aspx?szcaseid="+szcaseid+"&szcompanyid="+szcompanyid;
              BillngSummaryPopup.SetContentUrl(url);
              BillngSummaryPopup.Show();
               
        }
        function OnSheduaVisit(szcaseid, szpid) {
            var url = "Bill_sys_Add_Shedual_Visit.aspx?szcaseid=" + szcaseid + "&szpid=" + szpid;
            AddVisitPopUp.SetContentUrl(url);
            AddVisitPopUp.Show();

        }
    </script>

    <script type="text/javascript" src="script/PatientList.js">
    
    </script>

   <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>--%>
    <table width="100%" border="0">
        <tr>
            <td style="background-color: White;">
                <table>
                    <tr>
                        <td>
                            <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td>
                                        <div style="vertical-align: top" align="left">
                                            <asp:LinkButton Style="visibility: hidden" ID="Button1" runat="server"></asp:LinkButton>
                                            <a style="vertical-align: top; cursor: pointer; height: 240px" id="hlnkShowSearch"
                                                class="Buttons" title="Quick Search" runat="server" visible="false"></a>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <%--       <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">--%>
                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnSearch.ClientID %>)">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                        <b class="txt3">Search Parameters</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Case#
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Case Type
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Case Status
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtCaseNo" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100%" Selected_Text="---Select---"
                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                                        CssClass="search-input"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="100%" Selected_Text="OPEN"
                                                        Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Connection_Key="Connection_String"
                                                        CssClass="search-input"></extddl:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Patient
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Insurance
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Claim Number
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtPatientName" runat="server" Width="200%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                    <extddl:ExtendedDropDownList ID="extddlPatient" runat="server" Width="0%" Selected_Text="--- Select ---"
                                                        Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="REF_PATIENT_LIST" Connection_Key="Connection_String"
                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlPatient_extendDropDown_SelectedIndexChanged"
                                                        Visible="false"></extddl:ExtendedDropDownList>
                                                    <asp:CheckBox ID="chkJmpCaseDetails" runat="server" Text="Jump to Case Details" Visible="false">
                                                    </asp:CheckBox>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtInsuranceCompany" runat="server" Width="98%" autocomplete="off"
                                                        CssClass="search-input"></asp:TextBox>
                                                    <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="50%" Selected_Text="---Select---"
                                                        Procedure_Name="SP_MST_INSURANCE_COMPANY" Flag_Key_Value="INSURANCE_LIST" Connection_Key="Connection_String"
                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlInsurance_extendDropDown_SelectedIndexChanged"
                                                        Visible="false"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtClaimNumber" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                              
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Date of Accident
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Date of Birth
                                                </td>
                                                  <td class="td-widget-bc-search-desc-ch1" style="visibility:hidden">
                                                    SSN#
                                                </td>
                                            </tr>
                                            <tr>
                                               
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtDateofAccident" onkeypress="return CheckForInteger(event,'/')"
                                                        runat="server" MaxLength="10" Width="80%" CssClass="search-input"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif"
                                                        valign="bottom"></asp:ImageButton>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtDateofBirth" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                        MaxLength="10" Width="75%" CssClass="search-input"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif"
                                                        valign="bottom"></asp:ImageButton>
                                                </td>
                                                 <td class="td-widget-bc-search-desc-ch3" valign="bottom">
                                                    <asp:TextBox ID="txtSSNNo" runat="server" Width="98%" CssClass="search-input" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="bottom">
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtDateofAccident"
                                                        PopupButtonID="imgbtnDateofAccident">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtDateofAccident" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txtDateofAccident" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                    <asp:RangeValidator runat="server" ID="rngDate" ControlToValidate="txtDateofAccident"
                                                        Type="Date" MinimumValue="01/01/1901" MaximumValue="12/31/2099" ErrorMessage="Enter a valid date " />
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="bottom">
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtDateofBirth"
                                                        PopupButtonID="imgbtnDateofBirth">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtDateofBirth" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                        ControlToValidate="txtDateofBirth" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                    <asp:RangeValidator runat="server" ID="rngDateBirth" ControlToValidate="txtDateofBirth"
                                                        Type="Date" MinimumValue="01/01/1901" MaximumValue="12/31/2099" ErrorMessage="Enter a valid date " />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:Label ID="lblLocation" runat="server" Text="Location" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:Label ID="lblpatientid" runat="server" Text="Patient Id" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:Label ID="lblChart" runat="server" Text="Chart No" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="100%" Selected_Text="---Select---"
                                                        Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Connection_Key="Connection_String">
                                                    </extddl:ExtendedDropDownList>
                                                    
                                                  
                                                    
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtpatientid" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtChartNo" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="80px"
                                                    Text="Search"></asp:Button>
                                                <%--<asp:Button ID="btnClear"  OnClientClick="Clear();"  Width="80px" Text="Clear"></asp:Button>--%>
                                                <input type="button" id="btnClear" onclick="Clear();" style="width: 80px" value="Clear" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                         
                                        <a href="javascript:void(0);" onclick="ShowDental()">
                                                                                          Dental Claim Form</a>                                        
                                        
                                    </td>
                                    
                                   
                                    
                                            
           
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: White;">
                            <asp:TextBox ID="txtChartSearch" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtCaseIDSearch" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtPatientLNameSearch" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtPatientFNameSearch" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-color: White;">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <%--<b class="txt3">Patient list</b>
             <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                 DisplayAfter="10">
                 <ProgressTemplate>
                     <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                         runat="Server">
                         <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                             Height="25px" Width="24px"></asp:Image>
                         Loading...</div>
                 </ProgressTemplate>
             </asp:UpdateProgress>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                    <b class="txt3">Patient list</b>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdPatientSearch"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                 <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                         Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                             </progresstemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                 <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                         Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                             </progresstemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <table style="vertical-align: middle; width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: middle; width: 30%" align="left">
                                        Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                            CssClass="search-input">
                                        </gridsearch:XGridSearchTextBox>
                                        <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                    </td>
                                    <td style="width: 60%" align="right">
                                        Record Count:
                                      <%--  <%= this.grdPatientList.RecordCount%>--%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                            Text="Export TO Excel">
                                 <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        <asp:Button ID="btnSoftDelete" OnClick="btnSoftDelete_Click" runat="server" Visible="false"
                                            Text="Soft Delete"></asp:Button>
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export Bills" OnClick="btnExportToExcel_Click" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                
                                    <dx:ASPxGridView ID="grdPatientList" ClientInstanceName="grdPatientList" runat="server"
                                              Width="100%" KeyFieldName="description" AutoGenerateColumns="False">
                                              <Columns>
                                              <%--0--%>
                                                <dx:GridViewDataTextColumn FieldName="SZ_CASE_ID" Caption="Case ID" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                               <%--1--%>
                                               <dx:GridViewDataTextColumn FieldName="SZ_COMPANY_ID" Caption="Company ID" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <%--2--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="#"  >
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                       <a target="_self" href='../AJAX Pages/Bill_Sys_CaseDetails.aspx?CaseID=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmp=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>'><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>
                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' Visible="false" ></asp:LinkButton>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                 <%--3--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="#" >
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <a target="_self" href='../AJAX Pages/Bill_Sys_ReCaseDetails.aspx?CaseID=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmp=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>'><%# DataBinder.Eval(Container,"DataItem.SZ_RECASE_NO")%></a>
                                                         <asp:LinkButton ID="lnkSelectRCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' Visible="false"   ></asp:LinkButton>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                <%--4--%>
                                               <dx:GridViewDataTextColumn FieldName="SZ_CHART_NO" Caption="Chart No" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <%--5--%>
                                                <dx:GridViewDataColumn FieldName="MST_PATIENT.SZ_PATIENT_LAST_NAME   + ' '  + MST_PATIENT.SZ_PATIENT_FIRST_NAME" Caption="Patient" >
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                       <asp:LinkButton ID="lnkPateintView" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>' Visible="false" commandname="Patient"  CommandArgument='<%# Eval("SZ_CASE_ID") + "," +  Eval("SZ_COMPANY_ID") %>'  ></asp:LinkButton>
                                                        <a id="lnkframePatient" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' ><%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%></a>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                 <%--6--%>
                                               <dx:GridViewDataTextColumn FieldName="SZ_PATIENT_PHONE" Caption="Phone Number">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                             
                                               <%--7--%>
                                               <dx:GridViewDataTextColumn FieldName="DT_DATE_OF_ACCIDENT" Caption="Accident Date">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <%--8--%>
                                               <dx:GridViewDataTextColumn FieldName="DT_DATE_OPEN" Caption="Opened">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                 <%--9--%>
                                               <dx:GridViewDataTextColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                 <%--10--%>
                                                <dx:GridViewDataTextColumn FieldName="SZ_CLAIM_NUMBER" Caption="Claim Number">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                 <%--11--%>
                                                <dx:GridViewDataTextColumn FieldName="SZ_POLICY_NUMBER" Caption="Policy Number">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                 <%--12--%>
                                                <dx:GridViewDataTextColumn FieldName="Total" Caption="Billed">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                 <%--13--%>
                                                <dx:GridViewDataTextColumn FieldName="Paid" Caption="Paid" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                 <%--14--%>
                                                <dx:GridViewDataTextColumn FieldName="Pending" Caption="Outstanding" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <%--15--%>
                                                <dx:GridViewDataTextColumn Caption="Bills" Visible ="false" >
                                                 <DataItemTemplate>
                                                       <asp:HyperLink id="lnkNew"  runat="server" Target="_self" NavigateUrl='<%# "Bill_Sys_BillTransaction.aspx?CaseID=" + DataBinder.Eval(Container, "DataItem.SZ_CASE_ID") + "&cmpid=" + DataBinder.Eval(Container, "DataItem.SZ_Company_ID") + "&pname=" + DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME") %>' >Add | </asp:HyperLink>
                                                        <asp:HyperLink id="lnkView"  runat="server" Target="_self" NavigateUrl='<%# "Bill_Sys_BillSearch.aspx?CaseID=" + DataBinder.Eval(Container, "DataItem.SZ_CASE_ID") + "&cmpid=" + DataBinder.Eval(Container, "DataItem.SZ_Company_ID") + "&pname=" + DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME") + "&fromCase=true" %>' >View </asp:HyperLink>
                                                    </DataItemTemplate>
                                                   </dx:GridViewDataTextColumn>  
                                                 <%--16--%>
                                                <dx:GridViewDataTextColumn Caption="Desk" Visible ="true" >
                                                 <DataItemTemplate>
                                                        <a target="_self" href='../Bill_SysPatientDesk.aspx?Flag=true&CaseID=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmp=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>'>
                                                         <img alt=""   src="Images/clients_icon.png" 
                                                                 title="Patient Desk" 
                                                                 style="cursor:pointer;border:none"
                                                                 width="23px"  />
                                                       </a>
                                                    </DataItemTemplate>
                                                   </dx:GridViewDataTextColumn>
                                                 <%--17--%>   
                                               <dx:GridViewDataTextColumn FieldName="Location" Caption="Location" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn> 
                                                  <%--18--%>   
                                               <dx:GridViewDataTextColumn FieldName="DT_FIRST_TREATMENT" Caption="Date Of First Treatment" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn> 
                                                 <%--19--%>
                                                <dx:GridViewDataTextColumn Caption="Bill Summary" Visible ="true" >
                                                 <DataItemTemplate>
                                                        <a href="javascript:void(0);" onclick="OnBillingSummary('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>')">
                                                         View  </a>
                                                    </DataItemTemplate>
                                                   </dx:GridViewDataTextColumn> 
                                                    <%--20--%>
                                                <dx:GridViewDataTextColumn Caption="Add Visit" Visible ="true" >
                                                 <DataItemTemplate>
                                                        <a href="javascript:void(0);" onclick="OnSheduaVisit('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID")%>')">
                                                       <img src="Images/cal.gif" id="imgvisit" />
                                                        
                                                         </a>
                                                    </DataItemTemplate>
                                                   </dx:GridViewDataTextColumn> 
                                                     <%--21--%>
                                                    <dx:GridViewDataColumn Caption="chk" VisibleIndex="0" Width="30px">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" />
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                    </dx:GridViewDataColumn>
                                              </Columns>                       
                                     </dx:ASPxGridView> 
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="con" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnExportToExcel" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <tr>
                    <td>
                        <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" OffsetY="120" OffsetX="300"
                            Position="Center" PopupControlID="pnlShowSearch" TargetControlID="hlnkShowSearch">
                        </ajaxToolkit:PopupControlExtender>
                        <asp:Panel Style="border-right: buttonface 1px solid; border-top: buttonface 1px solid;
                            visibility: hidden; border-left: buttonface 1px solid; width: 600px; border-bottom: buttonface 1px solid;
                            height: 100%; background-color: white" ID="pnlCheckin" class="TDPart" runat="server">
                        </asp:Panel>
                        <%--Check in Pop up page--%>
                        <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                            visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                            position: absolute; top: 682px; height: 280px; background-color: #dbe6fa" id="divid2">
                            <div style="position: relative; background-color: #8babe4; text-align: right">
                                <a style="cursor: pointer" title="Close" onclick="CloseCheckinPopup();">X</a>
                            </div>
                            <iframe id="iframeCheckIn" src="" frameborder="0" width="500" height="380"></iframe>
                        </div>
                        <%--Check out Pop up page--%>
                        <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 512px;
                            visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                            position: absolute; top: 710px; height: 280px; background-color: #dbe6fa" id="divid3">
                            <div style="position: relative; background-color: #8babe4; text-align: right">
                                <a style="cursor: pointer" title="Close" onclick="CloseCheckoutPopup();">X</a>
                            </div>
                            <iframe id="iframeCheckOut" src="" frameborder="0" width="500" height="380"></iframe>
                        </div>
                        <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 512px;
                            visibility: hidden; border-left: silver 1px solid; width: 715px; border-bottom: silver 1px solid;
                            position: absolute; top: 710px; height: 280px; background-color: white" id="ReminderPopUP">
                            <div style="position: relative; background-color: #B5DF82; text-align: right">
                                <a style="cursor: pointer" title="Close" onclick="CloseCheckoutReminderPopup();">X</a>
                            </div>
                            <iframe id="frmReminder" src="" frameborder="0" width="700" height="380"></iframe>
                        </div>
                        <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 512px;
                            visibility: hidden; border-left: silver 1px solid; width: 715px; border-bottom: silver 1px solid;
                            position: absolute; top: 710px; height: 300px; background-color: white" id="checkimpopup">
                            <div style="position: relative; background-color: #B5DF82; text-align: right">
                                <a style="cursor: pointer" title="Close" onclick="CloseCheckimPopup();">X</a>
                            </div>
                            <iframe id="frmcheckim" src="" frameborder="0" width="715" height="300"></iframe>
                        </div>
                     
                          <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                            DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
                            ServiceMethod="GetPatient" ServicePath="PatientService.asmx" UseContextKey="true"
                            ContextKey="SZ_COMPANY_ID">
                        </ajaxToolkit:AutoCompleteExtender>
                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                            DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany"
                            ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true"
                            ContextKey="SZ_COMPANY_ID">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                </tr>
    </table>
    <div style="display: none" id="Pop">
        <asp:LinkButton ID="lnlpatientview" runat="server" Text=""></asp:LinkButton>
    </div>
    <ajaxToolkit:ModalPopupExtender BehaviorID="modal" ID="ModalPopupPatientView" runat="server"
        TargetControlID="lnlpatientview" PopupControlID="pnlDataList" PopupDragHandleControlID="dvdrag">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel Style="display: none;" ID="pnlDataList" runat="server" CssClass="pnl_hight"
        ScrollBars="Vertical">
        <asp:DataList ID="PatientDtlView" runat="server" BorderWidth="0px" BorderStyle="None"
            BorderColor="#DEBA84" RepeatColumns="1" BackColor="white" Width="100%">
            <ItemTemplate>
                <!-- Holding table. This table will hold all the controls on the page -->
                <table class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0">
                    <tr>
                        <td id="dvdrag" height="28%" align="right" bgcolor="#B5DF82" class="txt2" style="width: 100%;"
                            valign="middle">
                            <asp:Button ID="btnClose" OnClientClick="$find('modal').hide(); return false;" runat="server"
                                Width="80px" Text="Close"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-widget-lf-top-holder-division-ch">
                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                border: 1px solid #B5DF82;">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                        &nbsp;<b class="txt3">Personal Information</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 446px;">
                                        <!-- outer table to hold 2 child tables -->
                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="td-widget-lf-holder-ch">
                                                    <!-- Table 1 - to hold the actual data -->
                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                First Name
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Middle Name
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                <%# DataBinder.Eval(Container.DataItem, "MI")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Last Name
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                D.O.B
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "DOB") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Gender
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem,"SZ_GENDER") %>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Address
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                City
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                State
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_STATE")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Home Phone
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%></td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Work
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                ZIP
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                &nbsp;
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Email
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Extn.
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Attorney
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ATTORNEY")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Case Type
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_TYPE")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Case Status
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_STATUS")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                SSN
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-widget-lf-top-holder-division-ch">
                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                border: 1px solid #B5DF82;">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                        &nbsp;<b class="txt3">Insurance Information</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 446px;">
                                        <!-- outer table to hold 2 child tables -->
                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="td-widget-lf-holder-ch">
                                                    <!-- Table 1 - to hold the actual data -->
                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Policy Holder
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem,"SZ_POLICY_HOLDER") %>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Name
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Ins. Address
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                -
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Address
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ADDRESS") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                City
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_CITY") %>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                State
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_STATE") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                ZIP
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ZIP") %>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Phone
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_PHONE")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                                FAX
                                                            </td>
                                                            <td class="td-widget-lf-data-ch" style="height: 33px">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_FAX_NUMBER")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                                Contact Person
                                                            </td>
                                                            <td class="td-widget-lf-data-ch" style="height: 33px">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CONTACT_PERSON")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Claim File#
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Policy #
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-widget-lf-top-holder-division-ch">
                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                border: 1px solid #B5DF82;">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                        &nbsp;<b class="txt3">Accident Information</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 446px;">
                                        <!-- outer table to hold 2 child tables -->
                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="td-widget-lf-holder-ch">
                                                    <!-- Table 1 - to hold the actual data -->
                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Accident Date
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Plate Number
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Report Number
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Address
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_ADDRESS")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                City
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_CITY")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                State
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_INSURANCE_STATE")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Hospital Name
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_NAME")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Hospital Address
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_ADDRESS")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Date Of Admission
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "DT_ADMISSION_DATE")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Additional Patient
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FROM_CAR")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Describe Injury
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_DESCRIBE_INJURY")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Patient Type
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_TYPE")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-widget-lf-top-holder-division-ch">
                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                border: 1px solid #B5DF82;">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                        &nbsp;<b class="txt3">Employer Information</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 446px;">
                                        <!-- outer table to hold 2 child tables -->
                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="td-widget-lf-holder-ch">
                                                    <!-- Table 1 - to hold the actual data -->
                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Name
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_NAME")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Address
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ADDRESS")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                City
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_CITY")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                State
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_STATE")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                ZIP
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ZIP")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Phone
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_PHONE")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Date Of First Treatment
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "DT_FIRST_TREATMENT")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                <asp:Label ID="lblcharttext" runat="server" Text="Chart No" class="td-widget-lf-desc-ch"></asp:Label>
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                <asp:Label ID="lblchartvalue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SZ_CHART_NO")%>'
                                                                    class="td-widget-lf-data-ch"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-widget-lf-top-holder-division-ch">
                            <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                border: 1px solid #B5DF82;">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                        &nbsp;<b class="txt3">Adjuster Information</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 446px;">
                                        <!-- outer table to hold 2 child tables -->
                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="td-widget-lf-holder-ch">
                                                    <!-- Table 1 - to hold the actual data -->
                                                    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Name
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                &nbsp;
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Phone
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_PHONE")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Extension
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EXTENSION")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-lf-desc-ch">
                                                                FAX
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_FAX")%>
                                                            </td>
                                                            <td class="td-widget-lf-desc-ch">
                                                                Email
                                                            </td>
                                                            <td class="td-widget-lf-data-ch">
                                                                &nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "SZ_EMAIL")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
    </asp:Panel>
    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
        visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
        position: absolute; top: 682px; height: 280px; background-color: #B5DF82" id="divfrmPatient">
        <div style="position: relative; background-color: #B5DF82; text-align: right">
            <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
        </div>
        <iframe id="frmpatient" src="" frameborder="0" width="500" height="380"></iframe>
    </div>
    </contenttemplate> </asp:UpdatePanel>
    <div id="divBills" runat="server" visible="false">
        <asp:GridView ID="grdBills" runat="server">
            <Columns>
                <asp:BoundField HeaderText="Bill#" DataField="SZ_BILL_NUMBER" />
                <asp:BoundField HeaderText="Speciality" DataField="SPECIALITY" />
                <asp:BoundField HeaderText="Visit Date" DataField="PROC_DATE" />
                <asp:BoundField HeaderText="Bill Date" DataField="DT_BILL_DATE" />
                <asp:BoundField HeaderText="Bill Status" DataField="SZ_BILL_STATUS_NAME" />
                <asp:BoundField HeaderText="Bill Amount" DataField="FLT_BILL_AMOUNT" />
                <asp:BoundField HeaderText="Paid" DataField="PAID_AMOUNT" />
                <asp:BoundField HeaderText="Outstanding" DataField="FLT_BALANCE" />
            </Columns>
        </asp:GridView>
    </div>
    <dx:ASPxPopupControl ID="PatientInfoPop" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="PatientInfoPop" HeaderText="Patient Information" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="800px" ToolTip="Patient Information" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
        RenderIFrameForPopupElements="Default" Height="500px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="BillngSummaryPopup" runat="server" CloseAction="CloseButton"
        ContentUrl="Bill_Sys_Billing_Summary_Searchcase.aspx" HeaderStyle-BackColor="#B5DF82"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="BillngSummaryPopup"
        HeaderText="Bill Summary" HeaderStyle-HorizontalAlign="Left" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="1000px" ToolTip="Bill Summary"
        PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
        RenderIFrameForPopupElements="Default" ScrollBars="None" Height="600px" EnableHierarchyRecreation="True">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="AddVisitPopUp" runat="server" CloseAction="CloseButton"
        ContentUrl="Bill_sys_Add_Shedual_Visit.aspx" HeaderStyle-BackColor="#B5DF82"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="AddVisitPopUp"
        HeaderText="Add Visit" HeaderStyle-HorizontalAlign="Left" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="1000px" ToolTip="Bill Summary"
        PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
        RenderIFrameForPopupElements="Default" ScrollBars="None" Height="600px" EnableHierarchyRecreation="True">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    
       <dx:ASPxPopupControl ID="ShowPopup" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ShowPopup"
        HeaderText="Dental Claim Form" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#B5DF82"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="1100px"
        ToolTip="Select Cost Center" PopupHorizontalOffset="0" PopupVerticalOffset="0" 
          AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="600px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    

    <asp:TextBox ID="utxtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtClaimNumber" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtDateofBirth" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtDateofAccident" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtPatientName" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseType" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtCaseStatus" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtInsuranceName" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtLocation" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtSSNNo" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseNo" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtChartNo" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseId" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtUserId" runat="Server" Width="10px" Visible="false"></asp:TextBox>

</asp:Content>

