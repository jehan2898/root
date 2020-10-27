<%@ Page Language="C#" masterpagefile="~/LF/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Reports.aspx.cs" Inherits="LF_Bill_Sys_Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 
 <ajaxcontrol:CollapsiblePanelExtender ID="cpe" runat="Server"
    TargetControlID="Panel1"
    CollapsedSize="20"
    ExpandedSize="60"
    Collapsed="false"
    CollapsedText="Show Details..."
    ExpandedText="Hide Details" 
    ExpandControlID="LinkButton1"
    CollapseControlID="LinkButton1"
  

 />
    
    <script type="text/javascript">

    function OpenReports()
    {
        var FromDate=document.getElementById('<%=txtFromDate.ClientID%>').value;
        var ToDate=document.getElementById('<%=txtToDate.ClientID %>').value;
        var str='Report.aspx?StartDate='+FromDate+'&EndDate='+ToDate;
        window.open(str,'Lawfirm','maximize=true');
    }
    
    function SetDate()
         {
              
            getWeek();
             
          
            var selValue = document.getElementById('<%= ddlDateValues.ClientID %>').value;
                
            if(selValue == 0)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = "";
                   document.getElementById('<%= txtFromDate.ClientID %>').value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('today');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('today');
            }
            else if(selValue == 2)
            {
            
                   document.getElementById('<%= txtToDate.ClientID %>').value = getWeek('endweek');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('monthend');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('quarterend');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('yearend');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getLastWeek('endweek');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById('<%= txtToDate.ClientID %>').value = lastmonth('endmonth');
                   
                   document.getElementById('<%= txtFromDate.ClientID %>').value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById('<%= txtToDate.ClientID %>').value =lastyear('endyear');
                   
                   document.getElementById('<%= txtFromDate.ClientID %>').value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById('<%= txtToDate.ClientID %>').value = quarteryear('endquaeter');
                   
                   document.getElementById('<%= txtFromDate.ClientID %>').value =  quarteryear('startquaeter');
            }
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

function daysInMonth(month,year) 
         {
            var m = [31,28,31,30,31,30,31,31,30,31,30,31];
            if (month != 2) return m[month - 1];
            if (year%4 != 0) return m[1];
            if (year%100 == 0 && year%400 != 0) return m[1];
            return m[1] + 1;
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
               {
               y= t_year-1;
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
         
         
         function getFormattedDate(day,month,year)
         {
            return ''+(month+1)+'/'+day+'/'+year;
         }
         
         function getFormattedDateForMonth(day,month,year)
         {
            return ''+(month)+'/'+day+'/'+year;
         }
        
            
    </script>
    
    <table border="0" align="left" cellpadding="0" cellspacing="0" id="table-inner-menu-diplay" style="width:100%; margin-top: 10px;vertical-align:text-top;" border="0">
        <tr valign="top">
      <td style="width:25%; text-align:center; vertical-align:text-top;">
        <table id="table3" align="center" cellpadding="0" cellspacing="0" style="margin-left:auto; margin-right:auto; height: 100px; border: 2px solid #5998C9; width:75%; vertical-align:text-top;"  runat="server">
        <tr>
        <td style="vertical-align:text-top;">
            <asp:Panel ID="Panel1" runat="server" Height="58px" Width="100%">  
                <table id="table4" align="left" cellpadding="0" cellspacing="0" style="width: 100%; height:25px; background-color:#5998C9; ;vertical-align:text-top;"  runat="server">       <!-- border:1px solid #C0C0FF-->
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton1" Font-Underline="false" ToolTip="Show Reports" runat="server" OnClick="LinkButton1_Click"  BackColor="#5998C9" Font-Bold="false" Font-Size="Larger" ForeColor="#400000">Report type</asp:LinkButton>
                   
                       
                        </td>
                    </tr>
                    <tr style="background-color:White;">
                        <td>
                            &nbsp;
                        </td>               
                    </tr>
                    <tr  style="background-color:White;">
                        <td align="center"> 
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" Font-Size="Small">Lawfirm Report</asp:LinkButton><br />    
                        </td>
                    </tr>
                </table>                
            </asp:Panel>
        </td>
        </tr>
        </table>
            </td> 
           <td style="width:75%; height:auto;" align="left">           
           <asp:UpdatePanel runat="server"  ID="UpdatePanel">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Click" />
            </Triggers>
           <ContentTemplate >
           <table id="table1" border="0" align="left" cellpadding="0" cellspacing="0" class="border" style="width: 250px; height: 90px;"  runat="server">
            <tr>
                
                <%-- <ajaxToolkit:AutoCompleteExtender runat = "server" id="AutoCompleteExtender1" EnableCaching="true" DelimiterCharacters=";, :" MinimumPrefixLength="2" CompletionInterval="1000" TargetControlID="txtPatient" ServiceMethod="GetPatientName" ServicePath="ClaimNumber.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID"  ></ajaxToolkit:AutoCompleteExtender>--%>
                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 200px">
                    &nbsp;&nbsp;<b class="txt3">Search Parameters</b>
                </td>
                <td width="0px" align="left" valign="middle" bgcolor="#B5DF82" class="txt2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 400px; height: 100px;vertical-align:text-top;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 300px;vertical-align:text-top;" id="table2" >
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 100px; vertical-align:text-top;">
                                Date Range
                            </td>
                            <td align="center" style="width: 100px; vertical-align:text-top;">
                                From Date:</td>
                            <td align="center" style="width: 100px; vertical-align:text-top;">
                                To Date: 
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; vertical-align:text-top;" align="center">
                                &nbsp;<asp:DropDownList ID="ddlDateValues" runat="Server" width="90px">
                                    <asp:ListItem Value="1">Today</asp:ListItem>
                                    <asp:ListItem Value="2">This Week</asp:ListItem>
                                    <asp:ListItem Value="3">This Month</asp:ListItem>
                                    <asp:ListItem Value="4">This Quarter</asp:ListItem>
                                    <asp:ListItem Value="5">This Year</asp:ListItem>
                                    <asp:ListItem Value="6">Last Week</asp:ListItem>
                                    <asp:ListItem Value="7">Last Month</asp:ListItem>
                                    <asp:ListItem Value="9">Last Quarter</asp:ListItem>
                                    <asp:ListItem Value="8">Last Year</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="width: 100px; vertical-align:text-top;" align="center">
                                <asp:TextBox ID="txtFromDate"  runat="server" MaxLength="10" width="65px" onkeypress="return CheckForInteger(event,'/')" ></asp:TextBox>
                                <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                            </td>
                            <td style="width: 100px; vertical-align:text-top;" align="center">
                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" width="65px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 100px;">
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:Button ID="btn_Create" runat="server" Text="Show Report" CausesValidation="False" UseSubmitBehavior="False" OnClientClick="javascript:OpenReports(); return false;" /> 
                            </td>
                            <td align="center" style="width: 100px;">
                            </td>
                        </tr>
                      </table>
                        <ajaxcontrol:CalendarExtender id="calExtFromDate" runat="server" TargetControlID="txtFromDate" PopupButtonID="imgbtnFromDate"></ajaxcontrol:CalendarExtender>
                        <ajaxcontrol:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtToDate" PopupButtonID="imgbtnToDate"></ajaxcontrol:CalendarExtender>
                      </td>  
            </tr>            
        </table>
        </ContentTemplate> 
        </asp:UpdatePanel>
               <br />
               </td> 
                 <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="10"
                                                ID="UpdateProgress2">
                                                <ProgressTemplate>
                                                    <div id="DivStatus2" class="PageUpdateProgress" runat="Server">
                                                        <%--<img id="img2" alt="Loading. Please wait..." src="../Images/rotation.gif" /> Loading...--%>
                                                        <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading.....">
                                                        </asp:Image>
                                                        Loading....
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
            </tr>
        </table>
</asp:Content>

