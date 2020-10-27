<%@ Page Language="C#" MasterPageFile="~/Provider/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_BillSearch.aspx.cs" Inherits="Provider_Bill_Sys_BillSearch"
    AsyncTimeout="10000" Title="Untitled Page" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">
     function Clear()
       {
        
       // alert("call");                       
        document.getElementById("<%=txtPatientName.ClientID %>").value ='';                    
        //document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseStatus").value ='NA';
          
                         
            document.getElementById("<%=txtBillNo.ClientID%>").value='';
            document.getElementById("<%=txtCaseNo.ClientID %>").value ='';
            
            document.getElementById("_ctl0_ContentPlaceHolder1_extddlBillStatus").value ='NA'; 
             document.getElementById("_ctl0_ContentPlaceHolder1_extddlSpeciality").value ='NA'; 
             document.getElementById("_ctl0_ContentPlaceHolder1_extddlCaseType").value ='NA';  
             document.getElementById("_ctl0_ContentPlaceHolder1_ddlDateValues").value=0;
            document.getElementById("<%=txtFromDate.ClientID %>").value ='';
            document.getElementById("<%=txtToDate.ClientID %>").value ='';
            document.getElementById("ctl00_ContentPlaceHolder1_checkboxlst_0").checked=false;
            document.getElementById("ctl00_ContentPlaceHolder1_checkboxlst_1").checked=false;
            document.getElementById("ctl00_ContentPlaceHolder1_radioList_0").checked=false;
            document.getElementById("ctl00_ContentPlaceHolder1_radioList_1").checked=false;
            document.getElementById("ctl00_ContentPlaceHolder1_extddlBillStatus").value ='NA';
            document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value=0;
            document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value ='NA';
            
            //document.getElementById("ctl00_ContentPlaceHolder1_ddlAmount").value='---Select---';
            document.getElementById("<%=txtPatientName.ClientID %>").value='';
             if( document.getElementById("ctl00_ContentPlaceHolder1_lblfrom")!=null)
            {
               document.getElementById("ctl00_ContentPlaceHolder1_lblfrom").style.visibility='hidden';
            }
              if( document.getElementById("ctl00_ContentPlaceHolder1_lblTamt")!=null)
            {
               document.getElementById("ctl00_ContentPlaceHolder1_lblTamt").style.visibility='hidden';
            }
            if( document.getElementById("ctl00_ContentPlaceHolder1_lblFamt")!=null)
            {
               document.getElementById("ctl00_ContentPlaceHolder1_lblFamt").style.visibility='hidden';
            }
            
            
            
        }
    function SetDate()
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
        
    
    </script>

    <script type="text/javascript"> 
        function redirect() 
        {  
            var CaseId=document.getElementById("<%=hdnCaseId.ClientID%>").value;
            var CaseNo=document.getElementById("<%=hdnCaseNo.ClientID%>").value;
            var cmpid=document.getElementById("<%=hdnCompanyId.ClientID%>").value;
            location.href = "Bill_Sys_PatientDesk.aspx?caseid="+CaseId+"&caseno="+CaseNo+"&cmpid="+cmpid; 
        } 
    </script>

    <script type="text/javascript"> 
        function redirectCaseInfo() 
        {  
            var CaseId=document.getElementById("<%=hdnCaseId.ClientID%>").value;
            var CaseNo=document.getElementById("<%=hdnCaseNo.ClientID%>").value;
            var cmpid=document.getElementById("<%=hdnCompanyId.ClientID%>").value;
            location.href = "Bill_Sys_CaseDetails.aspx?caseid="+CaseId+"&caseno="+CaseNo+"&cmpid="+cmpid; 
        } 
    function OpenDocumentManagerProvider()
    {  
    var CaseId=document.getElementById("<%=hdnCaseId.ClientID%>").value;
    var CaseNo=document.getElementById("<%=hdnCaseNo.ClientID%>").value;
    var cmpid=document.getElementById("<%=hdnCompanyId.ClientID%>").value;
    var OfficeId=document.getElementById("<%=hdnOfficeId.ClientID%>").value;
   
     window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid='+CaseId+'&caseno='+CaseNo+'&cmpid='+cmpid+'&OfficeId='+OfficeId ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
    }
    </script>

    <table width="100%" border="0">
        <tr>
            <td style="background-color: rgb(219, 230, 250); height: 100%; width: 10%;" valign="top"
                id="linkbillsearch" runat="server">
                <table width="100%" border="0">
                    <tr>
                        <td height="28" valign="middle" style="background-image: url(images/link-roll.jpg);
                            background-position: right; height: 35px;">
                            <b class="txt3">Desk</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="txt2">
                            <asp:LinkButton ID="lnkpatientdesk" runat="server" Visible="false" OnClientClick="redirect(); return false;">Patient Desk</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="txt2">
                            <asp:LinkButton ID="lnkCaseInfo" runat="server" Visible="false" OnClientClick="redirectCaseInfo(); return false;">Case Info</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="txt2">
                            <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="OpenDocumentManagerProvider(); return false;">Document Manager</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 90%; height: 100%;" valign="top" align="left">
                <div id="psearch-search" style="height: 460px; position: relative; z-index: 0;">
                    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                                padding-top: 3px; height: 100%; vertical-align: top;">
                                <table id="table-inner-menu-diplay" cellpadding="0" cellspacing="0" style="width: 100%"
                                    border="0">
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
                                        <td style="height: 100%">
                                        </td>
                                        <td valign="top">
                                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                                background-color: White;">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                    <table style="text-align: left; width: 100%;">
                                                                        <tr>
                                                                            <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                                    height: 50%; border: 0px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td style="width: 40%; height: 0px;" align="center">
                                                                                            <table border="0" cellpadding="0" cellspacing="2" style="width: 100%; border-right: 1px solid #B5DF82;
                                                                                                border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                                <tr>
                                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                                        <b class="txt3">Search Parameters</b>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>Bill No</b>
                                                                                                    </td>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>Case No </b>
                                                                                                    </td>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>Bill Status </b>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtBillNo" runat="server" Width="90%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtCaseNo" runat="server" Width="90%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <extddl:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="94%" Connection_Key="Connection_String"
                                                                                                            Flag_Key_Value="GET_SELECTED_STATUS_LIST_WITHOUT_VR_VS_DEN_FBP" Procedure_Name="SP_MST_BILL_STATUS_BILL_SEARCH"
                                                                                                            Selected_Text="---Select---" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" valign="top">
                                                                                                        <asp:CheckBoxList AutoPostBack="false" RepeatColumns="2" RepeatDirection="Horizontal"
                                                                                                            runat="server" ID="checkboxlst" Visible="False">
                                                                                                            <asp:ListItem Text="Denial" Value="0"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Paid in Full" Value="3"></asp:ListItem>
                                                                                                        </asp:CheckBoxList>
                                                                                                    </td>
                                                                                                    <td colspan="2" style="text-align: left">
                                                                                                        <asp:RadioButtonList ID="radioList" runat="server" RepeatDirection="Horizontal" Visible="False">
                                                                                                            <asp:ListItem Text="Verification Sent" Value="1"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Verification Received" Value="2"></asp:ListItem>
                                                                                                        </asp:RadioButtonList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>Billing Date </b>
                                                                                                    </td>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>From</b>
                                                                                                    </td>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>To</b>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="90%">
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
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="76%"></asp:TextBox>
                                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                                                                            PopupButtonID="imgbtnFromDate">
                                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
                                                                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                                                                            PopupButtonID="imgbtnToDate">
                                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>Patient Name</b>
                                                                                                    </td>
                                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                                        <b>Specialty</b>
                                                                                                    </td>
                                                                                                    <td class="td-widget-bc-search-desc-ch1">
                                                                                                        <b>Case Type</b>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <asp:TextBox ID="txtPatientName" runat="server" Width="90%"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="96%" Selected_Text="---Select---"
                                                                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                                                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                                    </td>
                                                                                                    <td>    
                                                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="90%" Selected_Text="---Select---"
                                                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                                                                        </extddl:ExtendedDropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="height: 5px">
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                            <contenttemplate>
                                                                                                   
                                                                                                    &nbsp;
                                                                                                    <asp:Button Style="width: 80px" ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                                                                                        Text="Search"></asp:Button>
                                                                                                    <asp:Button ID="btnClearP" runat="server" Width="80px" Text="Clear"></asp:Button>
                                                                                                    &nbsp;
                                                                                                    <input style="width: 80px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                                                                                        runat="server" visible="false" />
                                                                                                    <input style="width: 80px" id="btnClear2" onclick="Clear1();" type="button" value="Clear"
                                                                                                        runat="server" visible="false" />
                                                                                                </contenttemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                    <%-- <td>
                                                                                    
                                                                                    </td>--%>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td style="width: 40%; vertical-align: top" class="td-widget-lf-holder-ch" id="td2"
                                                                                            runat="server">
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
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
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
                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                    DisplayAfter="10" DynamicLayout="true">
                                                    <progresstemplate>
                                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                runat="Server">
                                                <asp:Image ID="img3" runat="server" ImageUrl="../Images/rotation.gif" AlternateText="Loading....."
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
                                                    <%= this.grdBillSearch.RecordCount%>
                                                    | Page Count:
                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                    </gridpagination:XGridPaginationDropDown>
                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                        Text="Export TO Excel">
                                        <img src="../Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                            title="Export TO Excel" /></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <xgrid:XGridViewControl ID="grdBillSearch" runat="server" Width="100%" CssClass="mGrid"
                                                    DataKeyNames="SZ_BILL_NUMBER,SZ_CASE_ID,SZ_COMPANY_ID,SZ_CASE_NO" MouseOverColor="0, 153, 153"
                                                    EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                    AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SPECIALITY,PROC_DATE,DT_BILL_DATE,SZ_BILL_STATUS_NAME,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,SZ_PATIENT_NAME,DT_DATE_OF_ACCIDENT,DT_DATE_OPEN,SZ_INSURANCE_NAME,SZ_CLAIM_NUMBER,SZ_POLICY_NUMBER,SZ_CASE_TYPE,SZ_STATUS_NAME"
                                                    ShowExcelTableBorder="true" ExportToExcelColumnNames="Bill#,Case#,Specialty,Visit Date,Bill Date,Status,Bill Amount,Paid,Balance,Patient Name,Accident Date,Open Date,Insurance Name,Claim Number,Policy Number,Case Type,Case Status"
                                                    AllowPaging="true" XGridKey="Provider_Bill_Sys_BillSearch" PageRowCount="50"
                                                    PagerStyle-CssClass="pgr" AllowSorting="true" AutoGenerateColumns="false" OnRowCommand="grdBillSearch_RowCommand"
                                                    GridLines="None">
                                                    <Columns>
                                                        <%--0--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            headertext="Case ID" DataField="SZ_CASE_ID" visible="false" />
                                                        <%-- 1--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            visible="false" headertext="BillNo" DataField="SZ_BILL_NUMBER" />
                                                        <%--2--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="SZ_BILL_NUMBER" headertext="Bill#" DataField="SZ_BILL_NUMBER" />
                                                        <%--CAST(MCM.SZ_CASE_NO as int)"--%>
                                                        <%--  3--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="cm.SZ_CASE_NO" headertext="Case#" DataField="SZ_CASE_NO" />
                                                        <%--  4--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="(select SZ_PROCEDURE_GROUP from MST_PROCEDURE_GROUP where SZ_PROCEDURE_GROUP_ID =(select SZ_PROCEDURE_GROUP_ID  from MST_PROCEDURE_CODES where SZ_PROCEDURE_ID=(select top 1 SZ_PROCEDURE_ID from TXN_BILL_TRANSACTIONS_DETAIL  where SZ_BILL_NUMBER=TBT.SZ_BILL_NUMBER )))"
                                                            headertext="Specialty" DataField="SPECIALITY" />
                                                        <%--  5--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="convert(nvarchar(10),TBT.DT_FIRST_VISIT_DATE,101)	+ '-'+ CONVERT(nvarchar(10), TBT.DT_LAST_VISIT_DATE,101)"
                                                            headertext="Visit Date" DataField="PROC_DATE" />
                                                        <%--  6--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            DataFormatString="{0:MM/dd/yyyy}" SortExpression="TBT.DT_BILL_DATE" headertext="Bill Date"
                                                            DataField="DT_BILL_DATE" />
                                                        <%--  7--%>
                                                        <%--<asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                            SortExpression="MBS.SZ_BILL_STATUS_NAME" headertext="Status" DataField="SZ_BILL_STATUS_NAME" />--%>
                                                        <asp:TemplateField HeaderText="Bill Status" SortExpression="MBS.SZ_BILL_STATUS_NAME">
                                                            <itemtemplate>
                                                    <asp:Label    id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>'></asp:Label>
                                                </itemtemplate>
                                                        </asp:TemplateField>
                                                        <%--  8 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="TBT.FLT_WRITE_OFF" headertext="Write Off" DataFormatString="{0:C}"
                                                            DataField="FLT_WRITE_OFF" visible="true" />
                                                        <%--  9 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="CAST(TBT.FLT_BILL_AMOUNT  as float)" headertext="Bill Amount"
                                                            DataFormatString="{0:C}" DataField="FLT_BILL_AMOUNT" />
                                                        <%--  10--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="CAST(ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TBT.SZ_BILL_NUMBER),0)as float)"
                                                            headertext="Paid" DataFormatString="{0:C}" DataField="PAID_AMOUNT" />
                                                        <%--  11--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="CAST(TBT.FLT_BALANCE as float)" headertext="Outstanding" DataFormatString="{0:C}"
                                                            DataField="FLT_BALANCE" />
                                                        <%--12--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            visible="false" headertext="bill status code" DataField="SZ_BILL_STATUS_CODE" />
                                                        <%--13--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                            headertext="Balance" visible="false" DataField="BT_TRANSFER" />
                                                        <%--14--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            headertext="Company ID" DataField="SZ_COMPANY_ID" visible="false" />
                                                        <%--15--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            SortExpression="SZ_PATIENT_NAME" headertext="Patient name" DataField="SZ_PATIENT_NAME" />
                                                        <%--16--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="DT_DATE_OF_ACCIDENT" headertext="Accident Date" DataField="DT_DATE_OF_ACCIDENT"
                                                            DataFormatString="{0:MM/dd/yyyy}" />
                                                        <%--17--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="DT_CREATED_DATE" headertext="Opened" DataField="DT_DATE_OPEN"
                                                            DataFormatString="{0:MM/dd/yyyy}" />
                                                        <%--18--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            SortExpression="ltrim(SZ_INSURANCE_NAME)" headertext="Insurance" DataField="SZ_INSURANCE_NAME" />
                                                        <%--19--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            SortExpression="SZ_CLAIM_NUMBER" headertext="Claim Number" DataField="SZ_CLAIM_NUMBER" />
                                                        <%--20--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            SortExpression="SZ_POLICY_NUMBER" headertext="Policy Number" DataField="SZ_POLICY_NUMBER" />
                                                        <%--21--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            visible="true" SortExpression="SZ_PROVIDER_NAME" headertext="Provider Name" DataField="SZ_PROVIDER_NAME" />
                                                        <%--22--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                            headertext="Case Type" DataField="SZ_CASE_TYPE" />
                                                        <%--23--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                            headertext="Patient ID" DataField="SZ_PATIENT_ID" Visible="FALSE" />
                                                        <%--24--%>
                                                        <asp:TemplateField Visible="false">
                                                            <headertemplate>
                                             <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                          </headertemplate>
                                                            <itemtemplate>
                                      <asp:CheckBox ID="chkDelete" runat="server" />
                                       </itemtemplate>
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
                                </Triggers>
                            </asp:UpdatePanel>
                        </tr>
                        <tr>
                            <td>
                                <table style="height: auto; width: 100%; border: 0px solid #B5DF82; background-color: White;">
                                    <tr>
                                        <td align="right" style="width: 50%; height: 20px">
                                        </td>
                                        <td align="left" style="width: 50%; height: 20px">
                                            <asp:TextBox ID="txtRange" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                            <asp:TextBox ID="txtGroupId" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtBillStatusID" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtFromAmount" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtxToAmount" runat="server" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtCaseID" runat="server" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtPatientLNameSearch" runat="server" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtPatientFNameSearch" runat="server" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="TextBox1" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtOfficeID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hdnCaseId" runat="server" />
                                            <asp:HiddenField ID="hdnCaseNo" runat="server" />
                                            <asp:HiddenField ID="hdnCompanyId" runat="server" />
                                            <asp:HiddenField ID="hdnOfficeId" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
