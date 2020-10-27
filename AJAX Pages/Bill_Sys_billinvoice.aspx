<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_billinvoice.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_billinvoice"
    Title="Software Invoice" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    
   function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdInvoice.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {	
			       if(f.getElementsByTagName("input").item(i).disabled==false)
		             {					
			             f.getElementsByTagName("input").item(i).checked=ival;
			         }    
			    }			
		    }
       }
        
 function Clear()
       {
        document.getElementById("<%=txtCaseNo.ClientID%>").value='';
        document.getElementById("<%=txtPatientName.ClientID%>").value='';
        document.getElementById("<%=txtBillNo.ClientID%>").value='';
        document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value=0;
        document.getElementById("<%=txtFromDate.ClientID%>").value='';
        document.getElementById("<%=txtToDate.ClientID%>").value='';
        document.getElementById("<%=txtFromDate1.ClientID%>").value='';
        document.getElementById("ctl00_ContentPlaceHolder1_ddlsoftwarefee").value=0;
        document.getElementById("ctl00_ContentPlaceHolder1_ddlinvoicestatus").value=0;
        
       }
       function Invoice()
        {           
           if (Check())
           {
            document.getElementById('div2').style.zIndex = 1;
            document.getElementById('div2').style.position = 'absolute'; 
            document.getElementById('div2').style.left= '360px'; 
            document.getElementById('div2').style.top= '250px'; 
            document.getElementById('div2').style.visibility='visible';  
            return false;
            }
            else
            {
                return false;
            }
        }
         function Check()
       {
        //alert('check');
        var f= document.getElementById("<%= grdInvoice.ClientID %>");	
            
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {		
			            				
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {  bfFlag = true;   
			                   
    		                    
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
        function YesMassage()
        {         
        document.getElementById('div2').style.visibility='hidden';           
        }
        function NoMassage()
        {        
        document.getElementById('div2').style.visibility='hidden'; 
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
         
             function SetDate1()
         {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID %>").value;
        
            if(selValue == 0)
            {
                   document.getElementById("<%=txtToDate.ClientID %>").value = "";
                   document.getElementById("<%=txtFromDate.ClientID %>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtToDate.ClientID %>").value = getDate('today');
                   document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtToDate.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%=txtFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtToDate.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtToDate.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtToDate.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtToDate.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtFromDate.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtToDate.ClientID %>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtFromDate.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtToDate.ClientID %>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtFromDate.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtToDate.ClientID %>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtFromDate.ClientID %>").value =  quarteryear('startquaeter');
            }
         }
          
         
          function getLastWeek(type)
         {
            var d = new Date();
            d.setDate(d.getDate() - 7);
            var day = d.getDay(); 
            d.setDate(d.getDate() - day);
            if(type=='startweek')
                return(getFormattedDate(d.getDate(),d.getMonth(),d.getFullYear()));
            if(type=='endweek')
            {
                d.setDate(d.getDate() + 6);
                return(getFormattedDate(d.getDate(),d.getMonth(),d.getFullYear()));
            }
         }
         
         function lastmonth(type)
         {
        
            var d = new Date();
            var t_date = d.getDate();      // Returns the day of the month
            var t_mon = d.getMonth()+1;      // Returns the month as a digit
            var t_year = d.getFullYear();
          
            if(type=='startmonth')
            {
             if(t_mon==1)
             { 
               var y =t_year-1;
                return('12/1/'+y) ;
              
             }
            else
              {
                var m=t_mon-1;
                return(m+'/1/'+t_year);
              }
            }
           else if(type=='endmonth')
            {
                if(t_mon==1)
                {   var y =t_year-1;
                  return('12/31/'+y);
                }else
                {
                   var m=t_mon-1;
                   var d = daysInMonth(t_mon-1,t_year);
                  return(m+'/'+d+'/'+t_year);
                }
            }
           
         }



         function quarteryear(type)
         {  var d = new Date();
            var t_date = d.getDate();      // Returns the day of the month
            var t_mon = d.getMonth()+1;      // Returns the month as a digit
            var t_year = d.getFullYear();
            
            if(type=='startquaeter')
            {
              if(t_mon==1||t_mon==2||t_mon==3)
              { 
                var y = t_year-1;
                return('10/1/'+y);
              }
              else if(t_mon==4||t_mon==5||t_mon==6)
              { 
                 return('1/1/'+t_year);
                
              }else if(t_mon==7||t_mon==8||t_mon==9)
              {
                return('4/1/'+t_year);
                
              
              }else if(t_mon==10||t_mon==11||t_mon==12)
              {
                return('7/1/'+t_year);
              
              }
            
            }else if(type=='endquaeter')
            {
             if(t_mon==1||t_mon==2||t_mon==3)
              {
               //
               var y = t_year-1;
               return('12/31/'+y);
              }
              else if(t_mon==4||t_mon==5||t_mon==6)
              {return('3/31/'+t_year);
               
              }else if(t_mon==7||t_mon==8||t_mon==9)
              {
               return('6/30/'+t_year);
               
              
              }else if(t_mon==10||t_mon==11||t_mon==12)
              {
                return('9/30/'+t_year);
              }
            
            }
            
         }
         
            function lastyear(type)
            {  
                var d = new Date();
          
            var t_year = d.getFullYear();
               if(type=='startyear')
               {  
                 y= t_year-1;
                 return('1/1/'+y);
               }
               else if(type=='endyear')
               {y= t_year-1;
                return('12/31/'+y);
               }
            }

        
         
         function getDate(type)
         {
            var d = new Date();
            var t_date = d.getDate();      // Returns the day of the month
            var t_mon = d.getMonth();      // Returns the month as a digit
            var t_year = d.getFullYear();  // Returns 4 digit year
            
                var q_start = 0;
                var q_end = 0;
                if((t_mon+1)>=1 && (t_mon+1)<=3)
                {
                    q_start = 1;
                    q_end = 3;
                }
                else if((t_mon+1)>=4 && (t_mon+1)<=6)
                {
                    q_start = 4;
                    q_end = 6;
                }
                else if((t_mon+1)>=7 && (t_mon+1)<=9)
                {
                    q_start = 7;
                    q_end = 9;
                }
                else if((t_mon+1)>=10 && (t_mon+1)<=12)
                {
                    q_start = 10;
                    q_end = 12;
                }
            
            if(type== 'today') 
                return(getFormattedDate(t_date,t_mon,t_year));
            if(type== 'monthstart')
                return(getFormattedDate(1,t_mon,t_year)); 
            if(type== 'monthend')
                return(getFormattedDate(daysInMonth(t_mon+1,t_year),t_mon,t_year)); 
            if(type== 'quarterstart')
            {
                 return(getFormattedDateForMonth(1,q_start,t_year)); 
            }
            if(type== 'quarterend')
            {
                return(getFormattedDateForMonth(daysInMonth(q_end),q_end,t_year)); 
            }    
            if(type== 'yearstart')
                return(getFormattedDate(1,0,t_year)); 
            if(type== 'yearend')
                return(getFormattedDate(31,11,t_year)); 
         }
         
         function daysInMonth(month,year) 
         {
            var m = [31,28,31,30,31,30,31,31,30,31,30,31];
            if (month != 2) return m[month - 1];
            if (year%4 != 0) return m[1];
            if (year%100 == 0 && year%400 != 0) return m[1];
            return m[1] + 1;
         }
         
         function getFormattedDate(day,month,year)
         {
            return ''+(month+1)+'/'+day+'/'+year;
         }
         
         function getFormattedDateForMonth(day,month,year)
         {
            return ''+(month)+'/'+day+'/'+year;
         }
         
         function getWeek(type)
         {
            var d = new Date();
            var day = d.getDay(); 
            d.setDate(d.getDate() - day);
            if(type=='startweek')
                return(getFormattedDate(d.getDate(),d.getMonth(),d.getFullYear()));
            if(type=='endweek')
            {
                d.setDate(d.getDate() + 6);
                return(getFormattedDate(d.getDate(),d.getMonth(),d.getFullYear()));
            }
         }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
    <table width="100%" border="0">
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="background-color: White;">
                <table>
                    <tr>
                        <td>
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
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Case#
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Patient
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Bill No
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtCaseNo" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtPatientName" runat="server" Width="98%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                    <extddl:ExtendedDropDownList ID="extddlPatient" runat="server" Width="50%" Selected_Text="--- Select ---"
                                                        Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="REF_PATIENT_LIST" Connection_Key="Connection_String"
                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlPatient_extendDropDown_SelectedIndexChanged"
                                                        Visible="false"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtBillNo" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Bill Date
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    From Date
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    To Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:DropDownList ID="ddlDateValues" runat="Server" Width="98%">
                                                        <asp:ListItem Value="0">All</asp:ListItem>
                                                        <asp:ListItem Value="1">Today</asp:ListItem>
                                                        <asp:ListItem Value="2">This Week</asp:ListItem>
                                                        <asp:ListItem Value="3">This Month</asp:ListItem>
                                                        <asp:ListItem Value="4">This Quarter</asp:ListItem>
                                                        <asp:ListItem Value="5">This Year</asp:ListItem>
                                                        <asp:ListItem Value="6">Last Week</asp:ListItem>
                                                        <asp:ListItem Value="7">Last Month</asp:ListItem>
                                                        <asp:ListItem Value="9">Last Quarter</asp:ListItem>
                                                        <asp:ListItem Value="8">Last Year</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtFromDate" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                        MaxLength="10" Width="80%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif">
                                                    </asp:ImageButton>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtToDate" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                        MaxLength="10" Width="75%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnDateofAccident">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnDateofBirth">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Service Date
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Software Fee
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Invoice Status
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtFromDate1" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                        MaxLength="10" Width="80%"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:DropDownList ID="ddlsoftwarefee" runat="server">
                                                        <asp:ListItem Text="Not Paid" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Paid" Value="1">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:DropDownList ID="ddlinvoicestatus" runat="server">
                                                        <asp:ListItem Text="Not generated" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="generated" Value="1">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate1"
                                                        PopupButtonID="ImageButton1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSearch" runat="server" Width="80px" Text="Search" OnClick="btnSearch_Click">
                                                </asp:Button>
                                                <input type="button" id="btnClear" style="width: 80px" value="Clear" onclick="Clear();" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                        DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
                        ServiceMethod="GetPatient" ServicePath="PatientService.asmx" UseContextKey="true"
                        ContextKey="SZ_COMPANY_ID">
                    </ajaxToolkit:AutoCompleteExtender>
                </table>
            </td>
            <td style="background-color: White;">
            </td>
        </tr>
    </table>
    <table width="100%">
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
                                        <%= this.grdInvoice.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                            Text="Export TO Excel">
                                        <img src="Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                            title="Export TO Excel" /></asp:LinkButton>
                                        <asp:Button ID="btninvoice" runat="server" Visible="true" Text="Add Invoice"></asp:Button>
                                        <%--  <asp:Button ID="btnExportToExcel" runat="server" Text="Export Bills" OnClick="btnExportToExcel_Click" />--%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdInvoice" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="SZ_BILL_NUMBER,SZ_CASE_ID,INVOICE_STATUS" MouseOverColor="0, 153, 153"
                                        EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                        AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_BILL_NUMBER,FLT_BILL_AMOUNT,SZ_CASE_ID,DT_BILL_DATE,SZ_VISIT_DATE,DT_RECEIVED_DATE,SZ_CODE_DESCRIPTION,PAID_STATUS,INVOICE_STATUS"
                                        ShowExcelTableBorder="true" ExportToExcelColumnNames="Case No,Patient Name,BillNo,Bill Amount,Case ID,Bill Date,Visit Date,Pom Receive Date,Description,Payment,Status"
                                        AllowPaging="true" XGridKey="Invoice" PageRowCount="40" PagerStyle-CssClass="pgr"
                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Case No" DataField="SZ_CASE_NO" />
                                            <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Patient Name" DataField="SZ_PATIENT_NAME" />
                                            <%--2--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="BillNo" DataField="SZ_BILL_NUMBER" />
                                            <%--3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="TXN_BILL_TRANSACTIONS.FLT_BILL_AMOUNT" headertext="Bill Amount"
                                                DataFormatString="{0:C}" DataField="FLT_BILL_AMOUNT" />
                                            <%--  4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                visible="false" headertext="Case ID" DataField="SZ_CASE_ID" />
                                            <%--  5--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                DataFormatString="{0:MM/dd/yyyy}" SortExpression="TXN_BILL_TRANSACTIONS.DT_BILL_DATE"
                                                headertext="Bill Date" DataField="DT_BILL_DATE" />
                                            <%--  6--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                SortExpression="TXN_BILL_TRANSACTIONS.DT_FIRST_VISIT_DATE" headertext="Visit Date"
                                                DataField="SZ_VISIT_DATE" />
                                            <%--  7--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                SortExpression="TXN_BILL_POM.DT_RECEIVED_DATE" headertext="Receive Date" DataField="DT_RECEIVED_DATE" />
                                            <%--  8--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Description" DataField="SZ_CODE_DESCRIPTION" />
                                            <%--  9--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                headertext="Payment" DataField="PAID_STATUS" />
                                            <%--  10--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                headertext="Status" DataField="INVOICE_STATUS" />
                                            <%--11--%>
                                            <asp:TemplateField HeaderText="">
                                                <headertemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                </headertemplate>
                                                <itemtemplate>
                                            <asp:CheckBox ID="ChkDelete" runat="server" />
                                                </itemtemplate>
                                                <headerstyle horizontalalign="center"></headerstyle>
                                                <itemstyle horizontalalign="center"></itemstyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </xgrid:XGridViewControl>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="con" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnExportToExcel" EventName="Click" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtCaseNo" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtPatientName" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtbillnofromdate" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtbillnotodate" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtvisitdate" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtsoftwarefee" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtbillno" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtUserId" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtinvoicestatus" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txttransactiontype" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <div id="div2" style="position: absolute; left: 50%; top: 920px; width: 30%; height: 150px;
        background-color: White; visibility: hidden; border-right: #b4dd82 2px solid;
        border-top: #b4dd82 2px solid; border-left: #b4dd82 2px solid; border-bottom: #b4dd82 2px solid;
        text-align: center;">
        <div style="position: relative; width: 100%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #b4dd82; left: 0px;
            top: 0px;">
            MSG
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2" runat="server" style="font-size: 10pt; font-family: "></span>
        </div>
        <br />
        <br />
        <div style="text-align: center;">
            <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Yes" Width="80px" />
            <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="No" Width="80px" />
        </div>
        <asp:HiddenField ID="hdnPOMValue" runat="server" />
    </div>
</asp:Content>
