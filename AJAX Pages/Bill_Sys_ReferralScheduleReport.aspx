<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ReferralScheduleReport.aspx.cs" Inherits="Bill_Sys_ReferralScheduleReport"
    Title="Schedule Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
    
    var prm = Sys.WebForms.PageRequestManager.getInstance(); 

   prm.add_initializeRequest(InitializeRequest); 
   prm.add_endRequest(EndRequest); 
   var postBackElement; 
   function InitializeRequest(sender, args) 
   { 

      if (prm.get_isInAsyncPostBack()) 
         args.set_cancel(true); 
      postBackElement = args.get_postBackElement(); 
      if (postBackElement.id == 'ctl00_ContentPlaceHolder1_btnSearch') 
         $get('ctl00_ContentPlaceHolder1_UpdateProgress1').style.display = 'block'; 
   }  
   function EndRequest(sender, args) 
   { 
       if (postBackElement.id == 'ctl00_ContentPlaceHolder1_btnSearch') 
          $get('ctl00_ContentPlaceHolder1_UpdateProgress1').style.display = 'none'; 
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
         
         
         
         
         // Start :   Download Date function 
         
         
         
         // End
         
    </script>

    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%; vertical-align: top;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                <tr>
                                                    <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                        <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                        <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                        <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                        <b>Schedule Report</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                    <td style="width: 25%">
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
                                                    <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                    <td style="width: 25%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        From Date&nbsp; &nbsp;</td>
                                                    <td style="width: 25%">
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="imgbtnFromDate" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        To Date&nbsp;
                                                    </td>
                                                    <td style="width: 25%">
                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="imgbtnToDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%" valign="top">
                                                        Provider Name</td>
                                                    <td style="width: 25%; height: 18px">
                                                        <cc1:ExtendedDropDownList ID="extddlOffice" runat="server" Connection_Key="Connection_String"
                                                            Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_GET_OFFICE_LIST_FOR_SHCEDULE_REPORT"
                                                            Selected_Text="--- Select ---" Width="97%"></cc1:ExtendedDropDownList>
                                                    </td>
                                                    <td class="ContentLabel" style="width: 15%; height: 18px" valign="top">
                                                        Doctor&nbsp;
                                                    </td>
                                                    <td style="width: 25%; height: 18px" valign="top">
                                                        <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="GET_REFERRAL_DOCTOR_LIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%" valign="top">
                                                        Visit Status
                                                    </td>
                                                    <td style="width: 25%; height: 18px;" valign="top">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="35%">
                                                            <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                            <%--<asp:ListItem Value="1">Re-Scheduled</asp:ListItem>--%>
                                                            <asp:ListItem Value="2">Completed</asp:ListItem>
                                                             <asp:ListItem Value="4">Not Finalized </asp:ListItem>
                                                            <%--<asp:ListItem Value="3">No-show</asp:ListItem>--%>
                                                        </asp:DropDownList></td>
                                                    <td class="ContentLabel" style="width: 15%; height: 18px;" valign="top">
                                                    </td>
                                                    <td style="width: 25%; height: 18px;" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="4" valign="top">
                                                        <table style="width: 100%" border="0">
                                                            <tr>
                                                                <td style="width: 19%;" align="right" valign="top">
                                                                    Specialty
                                                                </td>
                                                                <td style="width: 12%; text-align: left;" valign="top">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <contenttemplate>
                                                                <cc1:ExtendedDropDownList id="extddlSpeciality" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_REFERRAL_PROC_GROUP" Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="---Select---" OnextendDropDown_SelectedIndexChanged="extddlSpeciality_SelectedIndexChanged" AutoPost_back="true"/>
                                                        </contenttemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 10%;" valign="top">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <contenttemplate>
                                                            <asp:Label  id="lblProcedureCode" text="Procedure Code" visible="false" runat="server" style="font-size:small;"></asp:Label>                                                            
                                                            </contenttemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 13%; text-align: left;" valign="top" align="right">
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <contenttemplate>
                                                                &nbsp;<asp:DropDownList ID="ddlProcedureCode" runat="server" OnSelectedIndexChanged="ddlProcedureCode_SelectedIndexChanged" AutoPostBack="true" Width="95%"  visible="false">
                                                                </asp:DropDownList>
                                                                </contenttemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 14%;" valign="top">
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <contenttemplate>
                                                            <asp:Label id="lblProcedureDesc" text="Procedure Description" visible="false" runat="server" style="font-size:small;"></asp:Label>
                                                            </contenttemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="width: 38%; text-align: left;" valign="top">
                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                        <contenttemplate>
                                                                <asp:ListBox ID="lstProcedureDesc" runat="server" Width="100%" visible="false"></asp:ListBox>
                                                                </contenttemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%" valign="top">
                                                        &nbsp;Patient Name</td>
                                                    <td style="width: 25%; height: 18px" valign="top">
                                                        <asp:TextBox ID="txtPatientName" runat="server"></asp:TextBox></td>
                                                    <td class="ContentLabel" style="width: 15%; height: 18px" valign="top">
                                                        Case No</td>
                                                    <td style="width: 25%; height: 18px" valign="top">
                                                        <asp:TextBox ID="txtCaseNo" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%" valign="top">
                                                        &nbsp; Is Referral </td>
                                                        <td style="width: 25%; height: 18px" valign="top">
                                                            <asp:CheckBox ID="chkRfferal" runat="server" />
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="4">
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                            <contenttemplate>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click1" /> &nbsp; <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                                </contenttemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="4" align="center">
                                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                            <contenttemplate>
                                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                            <progresstemplate>
                                                                <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                    runat="Server">
                                                                    <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....." Height="25px" Width="24px"></asp:Image>
                                                                    Loading...</div>
                                                            </progresstemplate>
                                                        </asp:UpdateProgress>
                                                        </contenttemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoPatient" EnableCaching="true"
                                                        DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
                                                        ServiceMethod="GetPatient" ServicePath="PatientService.asmx" UseContextKey="true"
                                                        ContextKey="SZ_COMPANY_ID">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;" class="TDPart" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <contenttemplate>
                                        <asp:DataGrid ID="grdPayment" runat="Server" AutoGenerateColumns="False" CssClass="GridTable"     OnItemCommand="grdPayment_ItemCommand"
                                            Width="100%">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            <asp:TemplateColumn HeaderText="Case #" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlCase" runat="server" CommandName="Case" CommandArgument="SZ_CASE_NO"
                                                                                        Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                                                                </ItemTemplate>
                                                                                 </asp:TemplateColumn>
                                                  <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlPatient" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                                                        Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>                          
                                                              <asp:TemplateColumn HeaderText="Attorney" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlAttorney" runat="server" CommandName="Attorney" CommandArgument="SZ_PATIENT_ADDRESS"
                                                                                        Font-Bold="true" Font-Size="12px">Attorney</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_ADDRESS")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>  
                                                                            
                                                                            <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Patient Phone"></asp:BoundColumn>
                                                                            
                                                                             <asp:TemplateColumn HeaderText="Accident Date" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlAccident" runat="server" CommandName="Accident Date" CommandArgument="DT_ACCIDENT_DATE"
                                                                                        Font-Bold="true" Font-Size="12px">Accident Date</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.DT_ACCIDENT_DATE")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>             
                                                                            
                                                                             <asp:TemplateColumn HeaderText="Visit Date" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlVisitDate" runat="server" CommandName="Visit Date" CommandArgument="DT_VISIT_DATE"
                                                                                        Font-Bold="true" Font-Size="12px">Visit Date</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.DT_VISIT_DATE")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>     
                                                                            
                                                                             <asp:BoundColumn DataField="SZ_SPECIALITY" HeaderText="Speciality"></asp:BoundColumn>
                                                                            
                                                                         <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="STATUS" HeaderText="Visit Status"></asp:BoundColumn>                
                                                                            
                                                <%--<asp:BoundColumn DataField="SZ_CASE_NO"  HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME"  HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="Attorney"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Patient Phone"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Accident Date"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_VISIT_DATE" HeaderText="Visit Date"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_SPECIALITY" HeaderText="Speciality"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="STATUS" HeaderText="Visit Status"></asp:BoundColumn>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        </contenttemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" style="text-align: left;" class="TDPart">
                                            <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="False"></asp:TextBox>
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
