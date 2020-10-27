<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Packeting.aspx.cs" Inherits="Bill_Sys_Packeting" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <script type="text/javascript" src="Registration/validation.js"></script>

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
        
        function CancelExistPatient()
        {
            document.getElementById('divid2').style.visibility='hidden';
        }
        
        

        function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '360px'; 
            document.getElementById('divid2').style.top= '250px'; 
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
            var selValue = document.getElementById('_ctl0_ContentPlaceHolder1_ddlDateValues').value;
            if(selValue == 0)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = "";
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('today');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getWeek('endweek');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('monthend');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('quarterend');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('yearend');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getLastWeek('endweek');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = lastmonth('endmonth');
                   
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value =lastyear('endyear');
                   
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = quarteryear('endquaeter');
                   
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value =  quarteryear('startquaeter');
            }
         }
         
        function SetServiceDate()
         {
            getWeek();
            var selValue = document.getElementById('_ctl0_ContentPlaceHolder1_ddlServiceDateValues').value;
            if(selValue == 0)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = "";
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = getDate('today');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = getWeek('endweek');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = getDate('monthend');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = getDate('quarterend');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = getDate('yearend');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = getLastWeek('endweek');
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = lastmonth('endmonth');
                   
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value =lastyear('endyear');
                   
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtToServiceDate').value = quarteryear('endquaeter');
                   
                   document.getElementById('_ctl0_ContentPlaceHolder1_txtFromServiceDate').value =  quarteryear('startquaeter');
            }
         } 
         
    </script>
<div id="diveserch" >
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%; vertical-align: top;">
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
                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                <tr>
                                                    <td class="ContentLabel" colspan="5" style="height: 25px; text-align: left">
                                                        <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                        <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                    </td>
                                                </tr>
                                                            <tr>
                                                                <td class="ContentLabel" colspan="5">
                                                                    &nbsp;</td>
                                            </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
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
                                                        </asp:DropDownList></td>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        From Bill Date:</td>
                                                    <td style="width: 27%">
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" /><br />
                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="imgbtnFromDate" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 13%">
                                                        To Bill Date:
                                                    </td>
                                                    <td style="width: 31%">
                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="imgbtnToDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
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
                                                        </asp:DropDownList></td>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        From Service Date</td>
                                                    <td style="width: 27%">
                                                        <asp:TextBox ID="txtFromServiceDate" runat="server" CssClass="text-box" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtFromServiceDate" runat="server" TargetControlID="txtFromServiceDate"
                                                            PopupButtonID="ImageButton1" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 13%">
                                                        To Service Date:</td>
                                                    <td style="width: 31%">
                                                        <asp:TextBox ID="txtToServiceDate" runat="server" CssClass="text-box" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtToServiceDate" runat="server" TargetControlID="txtToServiceDate"
                                                            PopupButtonID="ImageButton2" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="2" style="height: 22px">
                                                        <asp:Label ID="lblLocationName" runat="server" CssClass="ContentLabel" Text="Specialty :"
                                                           ></asp:Label></td>
                                                    <td style="width: 27%; height: 22px;">
                                                          <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="170px"></extddl:ExtendedDropDownList>
                                                        
                                                    </td>
                                                    <td style="width: 13%; height: 22px;" class="ContentLabel">
                                                        <asp:Label ID="Label1" runat="server" CssClass="ContentLabel" Text="Bill Status"
                                                            Width="67px"></asp:Label></td>
                                                    <td style="width: 35%; height: 22px;">
                                                        <cc1:extendeddropdownlist id="extddlBillStatus" runat="server" connection_key="Connection_String"
                                                            flag_key_value="GET_STATUS_LIST" procedure_name="SP_MST_BILL_STATUS" selected_text="---Select---"
                                                            width="170px"></cc1:extendeddropdownlist>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="5">
                                                        <asp:TextBox ID="txtPacketId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtSort" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons" OnClick="btnSearch_Click"/>
                                                        <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel" 
                                            OnClick="btnExportToExcel_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                        <div style="height:500px; width:100%;  overflow:auto;">
                                            <asp:DataGrid ID="grdPacketing" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" PageSize="10" OnItemCommand="grdPacketing_ItemCommand" >
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <%--0--%>
                                                    <%--<asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No."></asp:BoundColumn>--%>
                                                     <asp:TemplateColumn HeaderText="Bill No" Visible="true">
                                                    <HeaderTemplate>
                                                        <%--<asp:LinkButton ID="lnlBillNo" runat="server" CommandName="Bill No" CommandArgument="SZ_BILL_NUMBER"--%>
                                                         <asp:LinkButton ID="lnlBillNo" runat="server" CommandName="Bill No" CommandArgument="BILL_NO"
                                                            Font-Bold="true" Font-Size="12px">Bill No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--1--%>
                                                    <%--<asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date"></asp:BoundColumn>--%>
                                                       <asp:TemplateColumn HeaderText="Bill Date" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillDate" runat="server" CommandName="Bill Date" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.DT_BILL_DATE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--2--%>
                                                   <%-- <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No."></asp:BoundColumn>--%>
                                                     <asp:TemplateColumn HeaderText="Chart #" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="Chart No" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--3--%>
                                                    <%--<asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No."></asp:BoundColumn>--%>    
                                                    <asp:TemplateColumn HeaderText="Case #" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlCaseNo" runat="server" CommandName="Case No" CommandArgument="SZ_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>                                                
                                                    <%--4--%>
                                                    <%--<asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>--%>
                                                     <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--5--%>
                                                    <%--<asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Referring Office"></asp:BoundColumn>--%>
                                                      <asp:TemplateColumn HeaderText="Reffering Office" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlRefferingOffice" runat="server" CommandName="Reffering Office" CommandArgument="SZ_OFFICE"
                                                            Font-Bold="true" Font-Size="12px">Reffering Office</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_OFFICE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--6--%>
                                                   <%-- <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Specialty"></asp:BoundColumn>--%>
                                                   <asp:TemplateColumn HeaderText="Speciality" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlSpeciality" runat="server" CommandName="Speciality" CommandArgument="SZ_PROCEDURE_GROUP"
                                                            Font-Bold="true" Font-Size="12px">Speciality</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--7--%>
                                                    <%--<asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>--%>
                                                       <asp:TemplateColumn HeaderText="Insurance Company" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceCompany" runat="server" CommandName="Insurance Company" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Company</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--8--%>
                                                    <%--<asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Ins. Claim Number"></asp:BoundColumn>                                               --%>
                                                    <asp:TemplateColumn HeaderText="Insurance Claim No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceClaimNo" runat="server" CommandName="Insurance Claim No" CommandArgument="SZ_CLAIM_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Insurance Claim No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CLAIM_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--9--%>
                                                    <%--<asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amt"></asp:BoundColumn>--%>
                                                      <asp:TemplateColumn HeaderText="Bill Amount" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillAmt" runat="server" CommandName="Bill Amt" CommandArgument="FLT_BILL_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Bill Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                    <%--10--%>
                                                    <asp:BoundColumn DataField="PresentDocument" HeaderText="Received Documents"></asp:BoundColumn>
                                                    <%--11--%>
                                                    <asp:BoundColumn DataField="MissingDocument" HeaderText="Missing Documents"></asp:BoundColumn>
                                                    <%--12--%>
                                                    <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="caseID" Visible="false" ItemStyle-Width="0px" ></asp:BoundColumn>
                                                    <%--13--%>
                                                    <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No." Visible="false" ></asp:BoundColumn>
                                                   <asp:TemplateColumn >
                                                 <ItemTemplate >
                                                    <asp:Button ID="btnCreatePacket" runat="server" Text="Create Packet" CssClass="Buttons" style="width:100px;" CommandName="CreatePacket"></asp:Button>
                                                 </ItemTemplate>
                                                 </asp:TemplateColumn>
                                                  <%--15--%>
                                                    <asp:BoundColumn DataField="VisitDate" HeaderText="Visite Date" Visible="True" ></asp:BoundColumn>
                                                    <%--16--%>
                                                    <asp:BoundColumn DataField="SZ_BILL_STATUS" HeaderText="Bill Status" Visible="True" ></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                <PagerStyle Mode="NumericPages" />
                                            </asp:DataGrid>&nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                      <tr visible="false">
                                        <td style="width: 100%" class="TDPart" visible="false">
                                        
                                            <asp:DataGrid ID="grdExel" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" PageSize="10"  Visible="false">
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <%--0--%>
                                                    <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No."></asp:BoundColumn>
                                                    
                                                
                                                    <%--1--%>
                                                    <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date"></asp:BoundColumn>
                                                     
                                                    <%--2--%>
                                                    <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No."></asp:BoundColumn>
                                                    
                                                    <%--3--%>
                                                    <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No."></asp:BoundColumn>    
                                                    
                                                                                            
                                                    <%--4--%>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                    
                                               
                                                    <%--5--%>
                                                    <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Referring Office"></asp:BoundColumn>
                                                     
                                                    <%--6--%>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Specialty"></asp:BoundColumn>
                                                  
                                                    <%--7--%>
                                                    <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                      
                                                    <%--8--%>
                                                    <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Ins. Claim Number"></asp:BoundColumn>                                               
                                                   
                                                    <%--9--%>
                                                    <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amt"></asp:BoundColumn>
                                                   
                                                    <%--10--%>
                                                    <asp:BoundColumn DataField="PresentDocument" HeaderText="Received Documents"></asp:BoundColumn>
                                                    <%--11--%>
                                                    <asp:BoundColumn DataField="MissingDocument" HeaderText="Missing Documents"></asp:BoundColumn>
                                                    <%--12--%>
                                                    <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="caseID" Visible="false" ItemStyle-Width="0px" ></asp:BoundColumn>
                                                    <%--13--%>
                                                    <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No." Visible="false" ></asp:BoundColumn>
                                             <%--      <asp:TemplateColumn >
                                                 <ItemTemplate >
                                                    <asp:Button ID="btnCreatePacket" runat="server" Text="Create Packet" CssClass="Buttons" style="width:100px;" CommandName="CreatePacket"></asp:Button>
                                                 </ItemTemplate>
                                                 </asp:TemplateColumn>--%>
                                                  <%--15--%>
                                                    <asp:BoundColumn DataField="VisitDate" HeaderText="Visite Date" Visible="True" ></asp:BoundColumn>
                                                     <%--16--%>
                                                    <asp:BoundColumn DataField="SZ_BILL_STATUS" HeaderText="Bill Status" Visible="True" ></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                <PagerStyle Mode="NumericPages" />
                                            </asp:DataGrid>&nbsp;
                                            
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
<div id="divid2" style="position: absolute; left: 428px; top: 920px; width: 300px;
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
            <span id="msgPatientExists"  runat="server"></span></div>
    
        <div style="text-align: center;">
        <asp:Button ID="btnClient"  class="Buttons" style="width: 80px;" runat="server" Text="Ok" OnClick="btnOK_Click" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />
        </div>
    </div>
</asp:Content>

