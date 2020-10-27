<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_BillReportSpecialityForBilling.aspx.cs" Inherits="Bill_Sys_BillReportSpecialityForBilling" Title="Green Your Bills - Bill Report" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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
         
         
         
         
         
          function SetServiceDate()
         {
            getWeek();
            var selValue = document.getElementById('_ctl0_ContentPlaceHolder1_ddlServiseDate').value;
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
<div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
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
                                                <td class="ContentLabel" colspan="1" style="height: 25px; text-align: left">
                                                    <strong>Bill Report By Speciality</strong></td>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <b> </b>
                                                    <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%"></td>
                                                <td style="width: 24%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 12px">
                                                    Bill &nbsp; &nbsp;<asp:DropDownList ID="ddlDateValues" runat="Server">
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
                                                <td class="ContentLabel" style="width: 15%; height: 12px;">
                                                    From&nbsp; Date&nbsp; &nbsp;</td>
                                                <td style="width: 24%; height: 12px;">
                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;
                                                </td>
                                                <td class="ContentLabel" style="width: 14%; height: 12px;">
                                                    To Bill Date&nbsp;
                                                </td>
                                                <td style="width: 35%; height: 12px;">
                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 14px;">
                                                    Service<asp:DropDownList ID="ddlServiseDate" runat="server">
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
                                                <td class="ContentLabel" style="width: 15%; height: 14px;">
                                                    From&nbsp; Date</td>
                                                <td style="width: 24%; height: 14px;">
                                                    <asp:TextBox ID="txtFromServiceDate" runat="server" CssClass="text-box" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;
                                                </td>
                                                <td class="ContentLabel" style="width: 14%; height: 14px;">
                                                    To Service Date</td>
                                                <td style="width: 35%; height: 14px;">
                                                    <asp:TextBox ID="txtToServiceDate" runat="server" CssClass="text-box" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" />&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="2">
                                                    Bill Status&nbsp;</td>
                                                <td style="width: 24%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="170px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_STATUS_LIST" Procedure_Name="SP_MST_BILL_STATUS" Selected_Text="---Select---" />
                                                    &nbsp;
                                                </td>
                                                <td class="ContentLabel" style="width: 14%; height: 18px;">
                                                    Speciality
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="170px"></cc1:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td colspan="1"></td>
                                                 <td colspan="2" class="ContentLabel" style="width: 15%; height: 18px;" >
                                                    <asp:CheckBox ID="chkVerificationsent" runat="Server" Text="VerificationSent" />
                                                    <asp:CheckBox ID="chkVerificationreceived" runat="Server" Text="VerificationReceived" />
                                                    <asp:CheckBox ID="chkdenail" runat="Server" Text="Denial" />
                                                    <asp:CheckBox ID="chkPaidfull" runat="Server" Text="PaidInFull" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="2">
                                                    User
                                                </td>
                                                <td style="width: 24%">
                                                    <cc1:ExtendedDropDownList ID="extddlUser" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_USERS" Flag_Key_Value="GETUSERLIST"
                                                        Selected_Text="---Select---" Width="170px"></cc1:ExtendedDropDownList>
                                                    <%--<asp:DropDownList ID="ddlUserList" runat="server" >
                                                       <asp:ListItem Selected="True" Value = "NA">Select User</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 14%">
                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                        Visible="False" Width="94px"></asp:Label></td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="59%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="5">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                             
                                
                               <%-- <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="text-align: right;">
                                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                        </div>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        Total Amount : <asp:Label ID="lblTotal" runat="server" /> <br />
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" OnItemCommand = "grdAllReports_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Status" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CommandName = "View" Text='<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%>' Visible="false"></asp:LinkButton>
                                                         <a target="_self" href='AJAX Pages/Bill_Sys_BillReportForTest.aspx?flag=View&Status=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_ID")%>&speciality=<%# DataBinder.Eval(Container,"DataItem.SZ_SPECIALITY_ID")%>'><%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Special1" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Speciality"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BILL COUNT" HeaderText="Count Of Bills"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SUM" HeaderText="Sum Of Amount" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILL_STATUS_ID" HeaderText="StatusID" Visible ="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_SPECIALITY_ID" HeaderText="SpecialityID" Visible = "false"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromServiceDate" runat="server" TargetControlID="txtFromServiceDate"
                                                        PopupButtonID="ImageButton1" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtToServiceDate" runat="server" TargetControlID="txtToServiceDate"
                                                        PopupButtonID="ImageButton2" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
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
</asp:Content>
