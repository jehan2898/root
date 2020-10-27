<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_DeleteVisits.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_DeleteVisits"
    Title="Untitled Page" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>

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
         
         function SetDate()
         {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID %>").value;
             var selValue = document.getElementById("<%=DropDownList1.ClientID %>").value;
            if(selValue == 0)
            {
                   document.getElementById("<%=txtToDate1.ClientID %>").value = "";
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtToDate1.ClientID %>").value = getDate('today');
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtToDate1.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtToDate1.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtToDate1.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtToDate1.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtToDate1.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtToDate1.ClientID %>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtToDate1.ClientID %>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtFromDate1.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtToDate1.ClientID %>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtFromDate1.ClientID %>").value =  quarteryear('startquaeter');
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
         
          function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdDoctorChange.ClientID %>");	
            var str = 1;
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
          function SelectAll1(ival)
       {
            var f= document.getElementById("<%= grdDeleteVisit.ClientID %>");	
            var str = 1;
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

 function confirm_delete()
		 {
		         
		        var f= document.getElementById("<%=grdDeleteVisit.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete1') !=-1)
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
		        
		 

                if(confirm("Are you sure want to Delete?")==true)
				{
				  
				   return true;
				}
				else
				{
					return false;
				}
		}
		
		function confirm_update()
		 {
		       
		        var f= document.getElementById("<%=grdDoctorChange.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
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
		       
		 

		}
     
    </script>

    <ajaxToolkit:TabContainer ID="tabcontainerAddVisit" runat="server" ActiveTabIndex="0">
        <ajaxToolkit:TabPanel runat="server" ID="tabPanelVisit" Height="100%">
            <HeaderTemplate>
                <div style="width: 150px; text-align: center;" class="lbl">
                    Delete Visits
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                    height: 100%; background-color: White">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;
                                            border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
                                            <tr>
                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                    <b class="txt3">Delete Visits</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td colspan="3" style="height: 20px">
                                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                    <contenttemplate>
                                                                        <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                                    </contenttemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-bc-search-desc-ch1">
                                                                Set Date
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
                                                            <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                                Specialty
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch1" valign="top" colspan="2">
                                                                Doctor Name
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                    Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                    Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch3" valign="top" colspan="2">
                                                                <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="240px" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <contenttemplate>
                                                    
                                                            <asp:Button ID="btnSearch" runat="server" Width="87px" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                                            </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                         <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                            <ContentTemplate>
                            <table style="vertical-align: middle; width: 100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: middle; width: 30%" id="Searchtd" runat="server" align="left">
                                            Search:&nbsp;
                                            <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                CssClass="search-input">
                                            </gridsearch:XGridSearchTextBox>
                                        </td>
                                        <td style="vertical-align: middle; width: 30%" align="left">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                <progresstemplate>
                                                    <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress" runat="Server">
                                                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....." Height="25px" Width="24px"></asp:Image>
                                                            Loading...
                                                    </div>
                                                </progresstemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="vertical-align: middle; width: 40%; text-align: right" id="Exceltd1" runat="server"
                                            align="right" colspan="2">
                                            Record Count:<%= grdDeleteVisit.RecordCount%>
                                            | Page Count:
                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                            </gridpagination:XGridPaginationDropDown>
                                            <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                Text="Export TO Excel">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete"></asp:Button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <xgrid:XGridViewControl ID="grdDeleteVisit" runat="server" Height="148px" Width="1002px"
                                CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="50"
                                DataKeyNames="I_EVENT_ID" XGridKey="Bill_Sys_Delete_Visit" GridLines="None" AllowPaging="true"
                                AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,DT_EVENT_DATE,SZ_DOCTOR_NAME,SZ_PROCEDURE_GROUP" ExportToExcelColumnNames="Case#,Event Date,Doctor Name,Specialty"
                                HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                ShowExcelTableBorder="true" ExcelFileNamePrefix="ExcelLitigation" MouseOverColor="0, 153, 153"
                                AutoGenerateColumns="false">
                                <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case#" SortExpression="(select SZ_CASE_NO from MST_CASE_MASTER where SZ_CASE_ID=TXN_CALENDAR_EVENT.SZ_CASE_ID)">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DT_EVENT_DATE" HeaderText="Event Date" SortExpression="CONVERT(NVARCHAR(20),TXN_CALENDAR_EVENT.DT_EVENT_DATE,101)">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" SortExpression="MST_DOCTOR.SZ_DOCTOR_NAME">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SZ_PROCEDURE_GROUP" HeaderText="Specialty" SortExpression="MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                        Visible="False" SortExpression="SZ_CASE_NO"></asp:BoundField>
                                    <asp:BoundField DataField="I_EVENT_ID" HeaderText="EventID" Visible="True" SortExpression="SZ_CASE_NO">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SZ_DOCTOR_ID" HeaderText="DoctorID" Visible="False"></asp:BoundField>
                                    <asp:TemplateField HeaderText="">
                                        <headertemplate>
                                            <asp:CheckBox ID="chkSelectAll1" runat="server" onclick="javascript:SelectAll1(this.checked);"  ToolTip="Select All" />
                                        </headertemplate>
                                        <itemtemplate>
                                         <asp:CheckBox ID="ChkDelete1" runat="server" />
                                    </itemtemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </xgrid:XGridViewControl>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="tabPanel1" Height="100%">
            <HeaderTemplate>
                <div style="width: 150px; text-align: center;" class="lbl">
                    Change Doctor
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <table id="Second"  cellpadding="0" cellspacing="0" style="width: 100%;
                    height: 100%; background-color: White">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;
                                            border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
                                            <tr>
                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                    <b class="txt3">Change Doctor</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td colspan="3" style="height: 20px">
                                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                                    <contenttemplate>
                                                                        <UserMessage:MessageControl runat="server" ID="usrMessage1" />
                                                                    </contenttemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-bc-search-desc-ch1">
                                                                Visit Date
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
                                                                <asp:DropDownList ID="DropDownList1" runat="Server" Width="98%">
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
                                                                <asp:TextBox ID="txtFromDate1" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                                    MaxLength="10" Width="80%"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                            </td>
                                                            <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                                <asp:TextBox ID="txtToDate1" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                                    MaxLength="10" Width="75%"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate1"
                                                                    PopupButtonID="ImageButton1">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate1"
                                                                    PopupButtonID="ImageButton2">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="td-widget-bc-search-desc-ch1" valign="top" colspan="2">
                                                                Doctor Name
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:DropDownList ID="ddldoctors" runat="server" Width="200%">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlreferringdocs" Width="97%" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <contenttemplate>--%>
                                                    <asp:Button ID="btnSearch1" runat="server" Width="87px" Text="Search" OnClick="btnSearch1_Click">
                                                    </asp:Button>
                                                    <%--</contenttemplate>
                                                    </asp:UpdatePanel>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDoctorChange" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                         <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                                            <ContentTemplate>
                            <table style="vertical-align: middle; width: 100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: middle; width: 30%" id="Td1" runat="server" align="left">
                                            Search:&nbsp;
                                            <gridsearch:XGridSearchTextBox ID="txtSearchBox1" runat="server" AutoPostBack="true"
                                                CssClass="search-input">
                                            </gridsearch:XGridSearchTextBox>
                                        </td>
                                        <td style="vertical-align: middle; width: 30%" align="left">
                                            <asp:UpdateProgress ID="UpdateProgress15" runat="server" DisplayAfter="10">
                                                <progresstemplate>
                                                    <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress" runat="Server">
                                                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....." Height="25px" Width="24px"></asp:Image>
                                                        Loading...
                                                    </div>
                                                </progresstemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td style="vertical-align: middle; width: 40%; text-align: right" id="Exceltd" runat="server"
                                            align="right" colspan="2">
                                            Record Count:<%= grdDoctorChange.RecordCount%>
                                            | Page Count:
                                            <gridpagination:XGridPaginationDropDown ID="con1" runat="server">
                                            </gridpagination:XGridPaginationDropDown>
                                            <asp:LinkButton ID="lnkExportToExcel1" OnClick="lnkExportTOExcel1_onclick" runat="server"
                                                Text="Export TO Excel">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1" valign="top">
                                        </td>
                                        <td valign="top" align="right">
                                            Doctor Name:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDoctor" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlReferringDoctor" Width="97%" runat="server" Visible="false">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <xgrid:XGridViewControl ID="grdDoctorChange" runat="server" Height="148px" Width="1002px"
                                CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="50"
                                DataKeyNames="I_EVENT_ID" XGridKey="Bill_Sys_Change_Doctor" GridLines="None"
                                AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,PATIENT_NAME,DT_EVENT_DATE,SZ_DOCTOR_NAME"
                                ExportToExcelColumnNames="Case No,Patient Name,Event Date,Doctor Name" ShowExcelTableBorder="true" ExcelFileNamePrefix="DoctorChange"
                                HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                MouseOverColor="0, 153, 153" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case No " Visible="True" SortExpression="SZ_CASE_NO">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PATIENT_NAME" HeaderText="Patient Name" Visible="True"
                                        SortExpression="MP.SZ_PATIENT_FIRST_NAME"></asp:BoundField>
                                    <asp:BoundField DataField="DT_EVENT_DATE" HeaderText="Event Date" Visible="True"
                                        SortExpression="TCE.DT_EVENT_DATE"></asp:BoundField>
                                    <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name" Visible="True"
                                        SortExpression="MPG.SZ_PROCEDURE_GROUP "></asp:BoundField>
                                    <asp:BoundField DataField="I_EVENT_ID " HeaderText="Event Id" Visible="False"></asp:BoundField>
                                    <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False"></asp:BoundField>
                                    <asp:BoundField DataField="SZ_DOCTOR_ID " HeaderText="Doctor Id" Visible="False"></asp:BoundField>
                                    <asp:TemplateField HeaderText="">
                                        <headertemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                        </headertemplate>
                                        <itemtemplate>
                                         <asp:CheckBox ID="ChkDelete" runat="server" />
                                    </itemtemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </xgrid:XGridViewControl>
                             </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch1" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        
                    </tr>
                </table>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <asp:TextBox ID="txthdnFromDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txthdnToDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txthdnDoctor" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtProcedureGroupId" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtDate1" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtDate2" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtDocId" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
