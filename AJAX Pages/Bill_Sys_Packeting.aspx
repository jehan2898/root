<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Packeting.aspx.cs" Inherits="Bill_Sys_Packeting" Title="Untitled Page"  AsyncTimeout="36000"%>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript" src="../Registration/validation.js"></script>

    <script type="text/javascript">
    
     function ClearFields()
    {
      document.getElementById("<%=txtToDate.ClientID%>").value="";
      document.getElementById("<%=txtFromDate.ClientID%>").value="";
      document.getElementById("<%=txtToServiceDate.ClientID%>").value="";
      document.getElementById("<%=txtFromServiceDate.ClientID%>").value="";
      document.getElementById("<%=ddlDateValues.ClientID%>").value="0";
      document.getElementById("<%=ddlServiceDateValues.ClientID%>").value="0";
      //document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value="NA";
      document.getElementById("<%=extddlSpeciality.ClientID%>").value="NA";
      //document.getElementById("ctl00_ContentPlaceHolder1_extddlBillStatus").value="NA";
       document.getElementById("<%=extddlBillStatus.ClientID%>").value="NA";
 
    }
    
    
    
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
        
        function CancelExistPatient()
        {
            document.getElementById('divid2').style.visibility='hidden';
        }
        
        

        function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '380px'; 
            document.getElementById('divid2').style.top= '350px'; 
            document.getElementById('divid2').style.visibility='visible';           
            return false;            
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
            var selValue = document.getElementById("<%= ddlDateValues.ClientID %>").value;
            if(selValue == 0)
            {
                   document.getElementById("<%= txtToDate.ClientID %>").value = "";
                   document.getElementById("<%= txtFromDate.ClientID %>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%= txtToDate.ClientID %>").value = getDate('today');
                   document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%= txtToDate.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%= txtFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%= txtToDate.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%= txtToDate.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%= txtToDate.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%= txtFromDate.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%= txtToDate.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%= txtFromDate.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%= txtToDate.ClientID %>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%= txtFromDate.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%= txtToDate.ClientID %>").value =lastyear('endyear');
                   
                   document.getElementById("<%= txtFromDate.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%= txtToDate.ClientID %>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%= txtFromDate.ClientID %>").value =  quarteryear('startquaeter');
            }
         }
         
        function SetServiceDate()
         {
            getWeek();
            var selValue = document.getElementById("<%= ddlServiceDateValues.ClientID %>").value;
            if(selValue == 0)
            {
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = "";
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = getDate('today');
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value =lastyear('endyear');
                   
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%= txtToServiceDate.ClientID %>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%= txtFromServiceDate.ClientID %>").value =  quarteryear('startquaeter');
            }
         } 
         
    </script>

    <%--<div id="diveserch">--%>
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
            <td>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
            </tr>
            <tr>
             
                <td style="background-color:white;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="LeftCenter" >
                            </td>
                            <td class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td style="width: 100%">
                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 50%;
                                                 border: 1px solid #B5DF82;">
                                                <tr>
                                                    <td style="width: 100%; height: 0px;" align="left">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; "
                                                            onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnSearch.ClientID %>)">
                                                            <tr>
                                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                    <b class="txt3">Search Parameters</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch" style="width: 150px">
                                                                    </td>
                                                                <td class="td-widget-bc-search-desc-ch" style="width: 319px" align="center">
                                                                    From Bill Date:</td>
                                                                <td class="td-widget-bc-search-desc-ch" style="width: 317px" align="center">
                                                                    To Bill Date:</td>
                                                            </tr>
                                                           
                                                            <tr>
                                                        
                                                                <td align="center" style="height: 24px; width: 150px;">                                                                     
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
                                                                    </asp:DropDownList>&nbsp;
                                                                    
                                                                </td>
                                                               
                                                                <td style="width: 319px; height: 24px;">                                                                  
                                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>                                                                         
                                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate" PopupButtonID="imgbtnFromDate" />
                                                                    </td>                                                                  
                                                                <td style="width: 317px; height: 24px;">                                                                
                                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>                                                                        
                                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate" PopupButtonID="imgbtnToDate" />
                                                                    </td>
                                                                     
                                                            </tr>
                                                          
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 150px">
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 319px" align="center">
                                                                    From Service Date</td>
                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 317px" align="center">
                                                                    To Service Date:</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 150px; height: 19px">
                                                                    <asp:DropDownList ID="ddlServiceDateValues" runat="server">
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
                                                                    </asp:DropDownList>&nbsp;</td>
                                                                <td class="td-widget-bc-search-desc-ch3" style="width: 319px; height: 19px">
                                                                    <asp:TextBox ID="txtFromServiceDate" runat="server" CssClass="text-box" MaxLength="10"
                                                                        onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" /></td>
                                                                <td class="td-widget-bc-search-desc-ch3" style="width: 317px; height: 19px">
                                                                    <asp:TextBox ID="txtToServiceDate" runat="server" CssClass="text-box" MaxLength="10"
                                                                        onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 317px" align="center">
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 317px" align="center">
                                                                    Specialty
                                                                    <%--<asp:Label ID="lblLocationName" runat="server" CssClass="ContentLabel" Text="Specialty :"></asp:Label>--%>
                                                                </td>
                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 317px" align="center">
                                                                    Bill Status
                                                                    <%--   <asp:Label ID="Label1" runat="server" CssClass="ContentLabel" Text="Bill Status"
                                                            Width="67px"></asp:Label>--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 150px; height: 19px">
                                                                </td>
                                                                <td align="left" class="td-widget-bc-search-desc-ch3" style="width: 319px; height: 19px">
                                                                    <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                        Selected_Text="---Select---" Width="170px"></extddl:ExtendedDropDownList>
                                                                </td>
                                                                <td align="left" class="td-widget-bc-search-desc-ch3" style="width: 317px; height: 19px">
                                                                    <cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="GET_STATUS_LIST" Procedure_Name="SP_MST_BILL_STATUS" Selected_Text="---Select---"
                                                                        Width="170px"></cc1:ExtendedDropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px">
                                                                    &nbsp;</td>
                                                                <td align="center" style="width: 319px">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            
                                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    </td>
                                                                <td style="width: 317px" align="center">
                                                                    <asp:UpdatePanel id="UpdatePanel15" runat="server">
                                                                        <contenttemplate>
                                                <asp:Button id="btnClear" onclick="btnClear_Click" runat="server" Text="Clear" Width="80px"></asp:Button> 
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
                                        <td style="width: 100%; height: 10px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <div style="width: 100%;">
                                                <table style=" width: 100%; border: 1px solid #B5DF82;" class="txt2"
                                                    align="left" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                            <b class="txt3">Case list</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1017px;">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <table style="vertical-align: middle; width: 100%;" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="vertical-align: middle; width: 30%" align="left">
                                                                                    Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                        CssClass="search-input">
                                                                                    </gridsearch:XGridSearchTextBox>
                                                                                </td>
                                                                                <td style="vertical-align: middle; width: 30%" align="left">
                                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                                                        <ProgressTemplate>
                                                                                            <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                                runat="Server">
                                                                                                <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                                Loading...</div>
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                </td>
                                                                                <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                                                                    Record Count:<%= this.grdPacketing.RecordCount %>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                        Text="Export TO Excel">
                                                                                   <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <xgrid:XGridViewControl ID="grdPacketing" runat="server" Height="0px" Width="1002px"
                                                                        CssClass="mGrid" OnRowCommand="grdPacketing_RowCommand" AutoGenerateColumns="false"
                                                                        MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelPacketing" ShowExcelTableBorder="true"
                                                                        EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                        ExportToExcelColumnNames="Bill No,Bill Date,Case No,Patient Name,Reffering Office,Specialty,Insurance Company,Insurance Claim No,Bill Amount ($),Received Documents,Missing Documents,Visite Date,Bill Status" 
                                                                        ExportToExcelFields="SZ_BILL_NUMBER,DT_BILL_DATE,SZ_CASE_NO,SZ_PATIENT_NAME,SZ_OFFICE,SZ_PROCEDURE_GROUP,SZ_INSURANCE_NAME,SZ_CLAIM_NUMBER,FLT_BILL_AMOUNT,PresentDocument,MissingDocument,VisitDate,SZ_BILL_STATUS" 
                                                                        AlternatingRowStyle-BackColor="#EEEEEE"
                                                                        AllowPaging="true" GridLines="None" XGridKey="BillPacket" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                        AllowSorting="true" DataKeyNames="SZ_CASE_ID,SZ_BILL_NUMBER">
                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                        <Columns>
                                                                        <%--0--%>
                                                                          <asp:BoundField DataField="SZ_BILL_NUMBER"  HeaderText="Bill No" SortExpression="TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                          
                                                                            <%--1--%>
                                                                             <asp:BoundField DataField="DT_BILL_DATE"  HeaderText="Bill Date"  SortExpression="convert(nvarchar,DT_BILL_DATE,101)">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                            
                                                                            <%--2--%>
                                                                              <asp:BoundField DataField="SZ_CHART_NO"  HeaderText="Chart No">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                          
                                                                            <%--3--%>
                                                                            <asp:BoundField DataField="SZ_CASE_NO"  HeaderText="Case No" SortExpression="MST_CASE_MASTER.SZ_CASE_NO">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                            
                                                                            <%--4--%>
                                                                            <asp:BoundField DataField="SZ_PATIENT_NAME"  HeaderText="Patient Name" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                           
                                                                            
                                                                            <%--5--%>
                                                                             <asp:BoundField DataField="SZ_OFFICE"  HeaderText="Reffering Office"  SortExpression="MST_OFFICE.SZ_OFFICE">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                         
                                                                            <%--6--%>
                                                                             <asp:BoundField DataField="SZ_PROCEDURE_GROUP"  HeaderText="Specialty" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                          
                                                                            <%--7--%>
                                                                              <asp:BoundField DataField="SZ_INSURANCE_NAME"  HeaderText="Insurance Company"  SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                         
                                                                            <%--8--%>
                                                                              <asp:BoundField DataField="SZ_CLAIM_NUMBER"  HeaderText="Insurance Claim No" SortExpression="MST_CASE_MASTER.SZ_CLAIM_NUMBER">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                            
                                                                            <%--9--%>
                                                                              <asp:BoundField DataField="FLT_BILL_AMOUNT" DataFormatString="{0:C}" HeaderText="Bill Amount ($)" SortExpression="FLT_BILL_AMOUNT">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                           
                                                                            <%--10--%>
                                                                              <asp:TemplateField HeaderText="Received Documents" >                                                            
                                                                              <itemtemplate>
                                                                              <asp:LinkButton ID="lnkReceivDocument" runat="server"  Text='<%# DataBinder.Eval(Container,"DataItem.PresentDocument")%>'  > </asp:LinkButton>
                                                                             </itemtemplate>                                                           
                                                                            </asp:TemplateField>
                                                                        <%--<asp:BoundField DataField="PresentDocument" HeaderText="Received Documents"></asp:BoundField>--%>
                                                                            <%--11--%>
                                                                            <asp:BoundField DataField="MissingDocument" HeaderText="Missing Documents"></asp:BoundField>
                                                                            <%--12--%>
                                                                            <asp:BoundField DataField="SZ_CASE_ID" HeaderText="caseID" Visible="false" ItemStyle-Width="0px">
                                                                            </asp:BoundField>
                                                                            <%--13--%>
                                                                            <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No." Visible="false"></asp:BoundField>
                                                                            <asp:TemplateField>
                                                                                <itemtemplate>
                                                                <asp:Button ID="btnCreatePacket" runat="server" Text="Create Packet" CssClass="Buttons"
                                                                    Style="width: 100px;" CommandName="CreatePacket"  CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:Button>
                                                            </itemtemplate>
                                                                            </asp:TemplateField>
                                                                            <%--15--%>
                                                                            <asp:BoundField DataField="VisitDate" HeaderText="Visite Date" Visible="True"></asp:BoundField>
                                                                            <%--16--%>
                                                                            <asp:BoundField DataField="SZ_BILL_STATUS" HeaderText="Bill Status" Visible="True"></asp:BoundField>
                                                                        </Columns>
                                                                    </xgrid:XGridViewControl>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <asp:TextBox ID="txtPacketId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtSort" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calExtFromServiceDate" runat="server" TargetControlID="txtFromServiceDate"
                                PopupButtonID="ImageButton1" />
                            <ajaxToolkit:CalendarExtender ID="calExtToServiceDate" runat="server" TargetControlID="txtToServiceDate"
                                PopupButtonID="ImageButton2" />
                            <td class="RightCenter" style="width: 10px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
   <%-- </div>--%>
    <asp:UpdatePanel id="updatepanle20" runat="server">
    <ContentTemplate>    
    <div id="divid2" style="position: absolute; left: 0px; top:0px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="msgPatientExists" runat="server"></span>
        </div>
        <div style="text-align:center; vertical-align:bottom;">
            <asp:Button ID="btnClient" class="Buttons" Style="width: 80px;" runat="server" Text="Ok" 
                OnClick="btnOK_Click" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
