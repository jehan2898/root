<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_VerificationSent_PrintPOM.aspx.cs" Inherits="Bill_Sys_VerificationSent_PrintPOM"
    Title="Green Your Bills - Verification PrintPOM" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
       function CheckGrid()
        {
               var f= document.getElementById('<%=grdVerReceive.ClientID %>');
	           if(f == null)
	            {		
	                bfFlag = true;			
	            }
	            if(bfFlag == true)
	            {
	                alert('Data not available!');
	                return false;
	            }
	            else
	            {		            
	                return true;
	            }    
        }        
       function openURL(url)
        {
            if(url == "")
            {
                alert("There is no bill created for the selected record!");
            }
            else
            {
                var url1 = url;
                window.open(url1, "","top=10,left=100,menubar=no,toolbar=no,location=no,width=750,height=600,status=no,scrollbars=no,maximize=null,resizable=1,titlebar=no;");
            }
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
         
         
         
         function SetDate()
         {
            getWeek();
            var selValue = document.getElementById('<%=ddlDateValues.ClientID %>').value;
            if(selValue == 0)
            {
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = "";
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('today');
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = getWeek('endweek');
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('monthend');
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('quarterend');
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = getDate('yearend');
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = getLastWeek('endweek');
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = lastmonth('endmonth');
                   
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value =lastyear('endyear');
                   
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById('<%=txtupdateToDate.ClientID %>').value = quarteryear('endquaeter');
                   
                   document.getElementById('<%=txtupdateFromDate.ClientID %>').value =  quarteryear('startquaeter');
            }
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
         
      function Validate()
       {
            var f= document.getElementById("<%= grdVerReceive.ClientID %>");	
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
		    alert('Please select bill no.');
		    return false;
       }
       
       function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdVerReceive.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
           function confirm_check()
       {
             var f= document.getElementById("<%= grdVerReceive.ClientID %>");
		        
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkverRec') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                   return true;
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
            document.getElementById('div1').style.visibility='hidden';           
        }
        function NoMassage()
        {        
            document.getElementById('div1').style.visibility='hidden'; 
        }
        
        function POMConformation()
        {           
           if (confirm_check())
           {
                document.getElementById('div1').style.zIndex = 1;
                document.getElementById('div1').style.position = 'absolute'; 
                document.getElementById('div1').style.left= '360px'; 
                document.getElementById('div1').style.top= '250px'; 
                document.getElementById('div1').style.visibility='visible';  
                return false;
           }
           else
           {
            return false;
           }
        }    
    </script>

 <asp:UpdatePanel ID="UpdatePanel112" runat="server" Visible="true">
   <contenttemplate>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table <%--id="MainBodyTable"--%> cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td <%--class="LeftTop"--%>>
                        </td>
                        <td <%--class="CenterTop"--%>>
                        </td>
                        <td <%--class="RightTop"--%>>
                        </td>
                    </tr>
                    <tr>
                        <td <%--class="LeftCenter"--%> style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td <%--class="ContentLabel"--%> style="text-align: left; width: 50%;">
                                                    <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 40%;
                                                        height: 50%; border: 1px solid #B5DF82;">
                                                        <tr>
                                                            <%--<td align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 50%;
                                                                height: 3px;">
                                                                &nbsp;&nbsp;<b class="txt3">Search Parameters</b>                                                                  
                                                            </td>--%>
                                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px"><b class="txt3">Search Parameters</td> 
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%; height: 0px;" align="center">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch">
                                                                            Bill</td>
                                                                        <td class="td-widget-bc-search-desc-ch">
                                                                           From Date</td>
                                                                           <td class="td-widget-bc-search-desc-ch">
                                                                           To Date
                                                                           </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;" align="center">
                                                                            <asp:DropDownList ID="ddlDateValues" runat="Server">
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
                                                                        <td style="width: 50%;" align="center">
                                                                            <asp:TextBox ID="txtupdateFromDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                             ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                             <ajaxToolkit:CalendarExtender 
                                                                                ID="CalendarExtender3" 
                                                                                runat="server" 
                                                                                PopupButtonID="imgbtnFromDate" 
                                                                                TargetControlID="txtupdatefromdate">
                                                                            </ajaxToolkit:CalendarExtender>

                                                                        </td>
                                                                        <td style="width: 50%;" align="center">
                                                                            <asp:TextBox ID="txtupdateToDate" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox><asp:ImageButton
                                                                                ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <ajaxToolkit:CalendarExtender 
                                                                                        ID="CalendarExtender4" 
                                                                                        runat="server" 
                                                                                        PopupButtonID="imgbtnToDate" 
                                                                                        TargetControlID="txtupdateToDate">
                                                                                 </ajaxToolkit:CalendarExtender>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="td-widget-bc-search-desc-ch">
                                                                            Case No</td>
                                                                        <td class="td-widget-bc-search-desc-ch">
                                                                            Bill No</td>
                                                                          <td class="td-widget-bc-search-desc-ch">
                                                                            Patient Name</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%;" align="center">
                                                                            <asp:TextBox ID="txtupdateCaseNo" runat="server" Width="90px"></asp:TextBox>

                                                                        </td>
                                                                        <td align="center" style="width: 50%;">
                                                                             <asp:TextBox ID="txtupdateBillNo" runat="server" Width="89px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtupdatePatientName" runat="server" Width="90px"></asp:TextBox>
                                                                         </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                            
                                                                &nbsp; <asp:Button ID="btnSearch" OnClick="btnSearch_Click1" runat="server" Width="80px"
                                                                                               Text="Search"></asp:Button>
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
                                <td>
                                     <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                            <table style="width: 900px; border: 1px solid #B5DF82;"  align="left"
                                                 border="0">
                                                <tr>
                                                 <td height="28" align="left" valign="middle" bgcolor="#B5DF82" align="center" >
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel112"
                            DisplayAfter="10">
                            <ProgressTemplate>
                                <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                    runat="Server">
                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                        Height="25px" Width="24px"></asp:Image>
                                    Loading...</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        
                                        <TABLE style="VERTICAL-ALIGN: middle;width:900px;">
                                        <TBODY>
                                            <TR>
                                                <TD style="VERTICAL-ALIGN: middle; WIDTH: 50%" align=left>
                                                    Search:<gridsearch:XGridSearchTextBox id="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                                                    </gridsearch:XGridSearchTextBox> </TD>
                                                <TD style="VERTICAL-ALIGN: middle; WIDTH: 50%; TEXT-ALIGN: right" align="right" colSpan=2>
                                                    Record Count:<%= this.grdVerReceive.RecordCount %> | Page Count: <gridpagination:XGridPaginationDropDown id="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown> 
                                                 <asp:LinkButton id="lnkExportToExcel" onclick="lnkExportTOExcel_onclick" runat="server" Text="Export TO Excel">
                                             <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton> 
                                                <asp:Button ID="btnSendMail" runat="server" Text="Print POM"/> 
                                                <asp:Button ID="btnPrintEnv" runat="server" Text="Print Envelope"  OnClick="btnPrintEnv_Click" /> 
                                    </TD></TR></TBODY></TABLE>
                                    <table style="width:900px">
                                    <tr>
                                    <td>
                                    
                                    <xgrid:XGridViewControl id="grdVerReceive" runat="server" Height="148px" Width="925px" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153" 
                                    ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" 
                                    ExportToExcelColumnNames="Bill No.,Case #,Patient Name,Insurance Name,Bill Date,Visit Date" 
                                    ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,DT_BILL_DATE,DT_VISIT_DATE"
                                    AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="VerificationSentPrintPOM" PageRowCount="10" PagerStyle-CssClass="pgr" 
                                    DataKeyNames="SZ_CASE_ID,SZ_CLAIM_NUMBER,SZ_INSURANCE_ADDRESS,SZ_SPECIALITY,SZ_CASE_TYPE_ID,SZ_COMPANY_NAME,SZ_MIN_SERVICE_DATE,SZ_MAX_SERVICE_DATE,WC_ADDRESS,FLT_BILL_AMOUNT,SZ_OFFICE_ID" AllowSorting="true">
                                                                    <HeaderStyle CssClass="GridViewHeader" ></HeaderStyle>
                                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                    <Columns>                                                                        
                                                                       <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" SortExpression="convert(int,SZ_CASE_NO)">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No" SortExpression="convert(int,substring(TXN_BILL_TRANSACTIONS.sz_Bill_number,3,len(TXN_BILL_TRANSACTIONS.sz_bill_number)))">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                         <asp:BoundField DataField="DT_BILL_DATE" HeaderText="Bill Date" SortExpression="(SELECT MAX(DT_DATE_OF_SERVICE) FROM TXN_BILL_TRANSACTIONS_DETAIL WHERE SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="left"></itemstyle>
                                                                        </asp:BoundField>
                                                                                                                                          
                                                                        <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" SortExpression="(ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name" SortExpression="SZ_INSURANCE_NAME">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DT_VISIT_DATE" HeaderText="Visit Date" SortExpression="(SELECT MAX(DT_DATE_OF_SERVICE) FROM TXN_BILL_TRANSACTIONS_DETAIL WHERE SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Insurance Details">
                                                                        <itemtemplate>
                                                                        <asp:TextBox ID="txtInsDetails" runat="server" Width="100%" ToolTip="Enter insurance company name"></asp:TextBox>
                                                                                <asp:TextBox ID="txtInsAddress" runat="server" Width="100%" ToolTip="Enter insurance company address/street"></asp:TextBox>
                                                                                <asp:TextBox ID="txtInsState" runat="server" Width="100%" ToolTip="Enter insurance company city, state zip"></asp:TextBox> 
                                                                         </itemtemplate>
                                                                         <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="left" width="20%"></itemstyle>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField HeaderText="Insurance Description">
                                                                             <itemtemplate>
                                                                                  <asp:TextBox ID="txtInsDesc" runat="server" Width="100%" ToolTip="Enter insurance company name" TextMode="MultiLine"></asp:TextBox>
                                                                             </itemtemplate>
                                                                             <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="left" width="20%" ></itemstyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                                        </HeaderTemplate>
                                                                          <itemtemplate>
                                                                              <asp:CheckBox ID="ChkverRec" runat="server" ></asp:CheckBox>
                                                                            </itemtemplate>
                                                                         </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                         <itemstyle width="10px" horizontalalign="Left"></itemstyle>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="SZ_CASE_ID" HeaderText="CaseId" visible="false"/>
                                                                        <asp:BoundField DataField="SZ_INSURANCE_ADDRESS" HeaderText="insaddess" visible="false"/>
                                                                        <asp:BoundField DataField="SZ_SPECIALITY" HeaderText="speciality" visible="false"/>
                                                                        <asp:BoundField DataField="SZ_CASE_TYPE_ID" HeaderText="casetype" visible="false"/>
                                                                        <asp:BoundField DataField="SZ_COMPANY_NAME" HeaderText="companyadd" visible="false"/>
                                                                        <asp:BoundField DataField="SZ_MIN_SERVICE_DATE" HeaderText="mindate" visible="false"/>
                                                                        <asp:BoundField DataField="SZ_MAX_SERVICE_DATE" HeaderText="maxdate" visible="false"/>
                                                                        <asp:BoundField DataField="WC_ADDRESS" HeaderText="wcaddress" visible="false"/>
                                                                        <asp:BoundField DataField="FLT_BILL_AMOUNT" HeaderText="amount" visible="false"/>
                                                                        <asp:BoundField DataField="SZ_CLAIM_NUMBER" HeaderText="claimno" visible="false"/>
                                                                         <asp:BoundField DataField="SZ_OFFICE_ID" HeaderText="claimno" visible="false"/>
                                                                  </Columns> 
                                                            </xgrid:XGridViewControl> 
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
            </td>
        </tr>
  
  </table>
 </contenttemplate>
</asp:UpdatePanel>          
    <asp:TextBox ID="txtCaseNo" runat="server" Text="" Visible="false" Width="10px"></asp:TextBox>
     <asp:TextBox ID="txtBillNo" runat="server" Text="" Visible="false" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtBillStatus" runat="server" Text="VR" Visible="false" Width="10px" />
     <asp:TextBox ID="txtDay" runat="server" Text="" Visible="false" Width="10px" />
     <asp:TextBox ID ="txtFlag" runat="server" Text="REF" Visible = "false" Width="10px"></asp:TextBox>
     <asp:TextBox ID="txtFromDate" runat="server" Text="" Visible = "false" Width="10px"></asp:TextBox>
     <asp:TextBox ID="txtToDate" runat="server" Text="" Visible="false" Width="10px"></asp:TextBox>
     <asp:TextBox ID="txtPatientName" runat="server" Text="" Visible="false" Width="10px"></asp:TextBox>
     <asp:HiddenField ID="hdnPOMValue" runat="server" />   
       
       <div id="div1" style="position: absolute; left: 50%; top: 920px; width: 30%;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px; top: 0px;">
            MSG
        </div>
       <br />
        <br />        
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2"  runat="server"></span></div>
     <br />
        <br />
        <div style="text-align: center;">
      
          <asp:Button ID="btnYes" runat="server" CssClass="Buttons" OnClick="btnYes_Click"
                                            Text="Yes" Width="80px" />
         <asp:Button ID="btnNo" runat="server" CssClass="Buttons" OnClick="btnNo_Click"
                                            Text="No" Width="80px" /> 
        </div>
    </div>
     
</asp:Content>