<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_TreatmentReport.aspx.cs" Inherits="Bill_Sys_TreatmentReport" %>

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

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
    
       function ValidationViewReport()
		
		{
		      var f= document.getElementById("<%=grdCaseMaster.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') !=-1)
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

    
     function ClearFields()
    {
      document.getElementById("<%=txtCaseNumber.ClientID%>").value="";
      document.getElementById("<%=txtPatientName.ClientID%>").value="";
      document.getElementById("<%=txtFromDate.ClientID%>").value="";
      document.getElementById("<%=txtToDate.ClientID%>").value="";
      document.getElementById("<%=ddlDateValues.ClientID%>").value="0";
       document.getElementById("<%=ddlDateValues1.ClientID%>").value="0";
      document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value="NA"; 
       document.getElementById("<%=txtOpenFromDate.ClientID%>").value="";
      document.getElementById("<%=txtOpenToDate.ClientID%>").value="";
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
    function SetDateOpen()
    {
        getWeek();
        var selValue = document.getElementById("<%=ddlDateValues1.ClientID %>").value;
        if(selValue == 0)
        {
            document.getElementById("<%=txtOpenToDate.ClientID %>").value = "";
            document.getElementById("<%=txtOpenFromDate.ClientID %>").value = "";
        }
        else if(selValue == 1)
        {
            document.getElementById("<%=txtOpenToDate.ClientID %>").value = getDate('today');
            document.getElementById("<%=txtOpenFromDate.ClientID %>").value = getDate('today');
        }
        else if(selValue == 2)
        {
               document.getElementById("<%=txtOpenToDate.ClientID %>").value = getWeek('endweek');
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value = getWeek('startweek');
        }
        else if(selValue == 3)
        {
               document.getElementById("<%=txtOpenToDate.ClientID %>").value = getDate('monthend');
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value = getDate('monthstart');
        }
        else if(selValue == 4)
        {
               document.getElementById("<%=txtOpenToDate.ClientID %>").value = getDate('quarterend');
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value = getDate('quarterstart');
        }
        else if(selValue == 5)
        {
               document.getElementById("<%=txtOpenToDate.ClientID %>").value = getDate('yearend');
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value = getDate('yearstart');
        }
          else if(selValue == 6)
        {
               document.getElementById("<%=txtOpenToDate.ClientID %>").value = getLastWeek('endweek');
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value = getLastWeek('startweek');
        }else if(selValue == 7)
        {     
               document.getElementById("<%=txtOpenToDate.ClientID %>").value = lastmonth('endmonth');                   
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value = lastmonth('startmonth');
        }else if(selValue == 8)
        {     
               document.getElementById("<%=txtOpenToDate.ClientID %>").value =lastyear('endyear');
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value = lastyear('startyear');
        }else if(selValue == 9)
        {     
               document.getElementById("<%=txtOpenToDate.ClientID %>").value = quarteryear('endquaeter');                   
               document.getElementById("<%=txtOpenFromDate.ClientID %>").value =  quarteryear('startquaeter');
        }
    }
    
    function SetVisitDate()
    {
        getWeek();
        var selValue = document.getElementById("<%=ddlDateValues2.ClientID %>").value;
        if(selValue == 0)
        {
            document.getElementById("<%=txtVisitTODate.ClientID %>").value = "";
            document.getElementById("<%=txtVisitFromDate.ClientID %>").value = "";
        }
        else if(selValue == 1)
        {
            document.getElementById("<%=txtVisitTODate.ClientID %>").value = getDate('today');
            document.getElementById("<%=txtVisitFromDate.ClientID %>").value = getDate('today');
        }
        else if(selValue == 2)
        {
               document.getElementById("<%=txtVisitTODate.ClientID %>").value = getWeek('endweek');
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value = getWeek('startweek');
        }
        else if(selValue == 3)
        {
               document.getElementById("<%=txtVisitTODate.ClientID %>").value = getDate('monthend');
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value = getDate('monthstart');
        }
        else if(selValue == 4)
        {
               document.getElementById("<%=txtVisitTODate.ClientID %>").value = getDate('quarterend');
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value = getDate('quarterstart');
        }
        else if(selValue == 5)
        {
               document.getElementById("<%=txtVisitTODate.ClientID %>").value = getDate('yearend');
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value = getDate('yearstart');
        }
          else if(selValue == 6)
        {
               document.getElementById("<%=txtVisitTODate.ClientID %>").value = getLastWeek('endweek');
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value = getLastWeek('startweek');
        }else if(selValue == 7)
        {     
               document.getElementById("<%=txtVisitTODate.ClientID %>").value = lastmonth('endmonth');                   
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value = lastmonth('startmonth');
        }else if(selValue == 8)
        {     
               document.getElementById("<%=txtVisitTODate.ClientID %>").value =lastyear('endyear');
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value = lastyear('startyear');
        }else if(selValue == 9)
        {     
               document.getElementById("<%=txtVisitTODate.ClientID %>").value = quarteryear('endquaeter');                   
               document.getElementById("<%=txtVisitFromDate.ClientID %>").value =  quarteryear('startquaeter');
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
       function ShowConfirmation() {

           var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdCaseMaster');
           var count = 0;
           var countVisitType = 0;
            var idstatus=0;
           if (grdProc.rows.length > 0) {
               var count = 0;
               for (var i = 1; i < grdProc.rows.length; i++) {
                   var cell = grdProc.rows[i].cells[0];
                   for (j = 0; j < cell.childNodes.length; j++) {
                       if (cell.childNodes[j].type == "checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report") {
                           if (cell.childNodes[j].checked) {
                               count = count + 1;
                           }
                       }
                   }
               }
           }
           
           if (grdProc.rows.length > 0) 
           {
                               
              

                 
               


                if (count > 0) 
                {
                    
                }
                else 
                {
                    alert('Please select patient');
                    return false;
                }
            }
            else {
                return false;
            }
        }
        
        function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdCaseMaster.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        background-color: White; height: 100%">
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
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <b>Patient Visit Summary Report </b>
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                <table style="text-align: left; width: 100%;">
                                                                    <tr>
                                                                        <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                                height: 50%; border: 1px solid #B5DF82;">
                                                                                <tr>
                                                                                    <td style="width: 40%; height: 0px;" align="center">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                            <tr>
                                                                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                                    <b class="txt3">Search Parameters</b>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-widget-bc-search-desc-ch" style="width: 117px">
                                                                                                    Case No
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch" style="width: 100px">
                                                                                                    Patient Name
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch">
                                                                                                    Case Type&nbsp;</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="height: 24px; width: 117px;" align="center">
                                                                                                    <asp:TextBox ID="txtCaseNumber" runat="server" Width="49%" CssClass="search-input"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 100px; height: 24px;" align="center">
                                                                                                    &nbsp;<asp:TextBox ID="txtPatientName" runat="server" Width="95%" autocomplete="off"
                                                                                                        CssClass="search-input"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 148px; height: 24px;" align="center">
                                                                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="95%" Selected_Text="---Select---"
                                                                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String">
                                                                                                    </extddl:ExtendedDropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 117px">
                                                                                                    Date Of Accident</td>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px">
                                                                                                    From
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                                                    To
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" style="width: 117px; height: 37px;" valign="top">
                                                                                                    <asp:DropDownList ID="ddlDateValues" runat="Server" Width="80%">
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
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px; height: 37px;" align="center">
                                                                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                        CssClass="text-box" MaxLength="10" Width="71%"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                                        ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px; height: 37px;" align="center">
                                                                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                        CssClass="text-box" MaxLength="10" Width="71%"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                                                                        ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 117px">
                                                                                                    Date Open</td>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px">
                                                                                                    From
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                                                    To
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" style="width: 117px; height: 37px;" valign="top">
                                                                                                    <asp:DropDownList ID="ddlDateValues1" runat="Server" Width="80%">
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
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px; height: 37px;" align="center">
                                                                                                    <asp:TextBox ID="txtOpenFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                        CssClass="text-box" MaxLength="10" Width="71%"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                                                                                        ControlToValidate="txtOpenFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px; height: 37px;" align="center">
                                                                                                    <asp:TextBox ID="txtOpenToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                        CssClass="text-box" MaxLength="10" Width="71%"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4"
                                                                                                        ControlToValidate="txtOpenToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 117px">
                                                                                                    Visit Date</td>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px">
                                                                                                    From
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch1">
                                                                                                    To
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" style="width: 117px; height: 37px;" valign="top">
                                                                                                    <asp:DropDownList ID="ddlDateValues2" runat="Server" Width="80%">
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
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px; height: 37px;" align="center">
                                                                                                    <asp:TextBox ID="txtVisitFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                        CssClass="text-box" MaxLength="10" Width="71%"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator11" runat="server" ControlExtender="MaskedEditExtender11"
                                                                                                        ControlToValidate="txtVisitFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                                </td>
                                                                                                <td class="td-widget-bc-search-desc-ch1" style="width: 100px; height: 37px;" align="center">
                                                                                                    <asp:TextBox ID="txtVisitTODate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                        CssClass="text-box" MaxLength="10" Width="71%"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/cal.gif" Style="float: left;" />
                                                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator12" runat="server" ControlExtender="MaskedEditExtender12"
                                                                                                        ControlToValidate="txtVisitTODate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 100%;" align="center" colspan="3">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                        <contenttemplate>
                                                                                                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"></asp:Button>&nbsp;
                                                                                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear"></asp:Button>
                                                                                                            
                                                                                                        </contenttemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <%-- <td align="center" style="width: 100px">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear"></asp:Button>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>--%>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="text-align: right; width: 50%; vertical-align: text-top;">
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="height: 30px">
                                                    <asp:TextBox ID="txtChartSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseIDSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtPatientLNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtPatientFNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="TextBox1" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" class="SectionDevider" colspan="4">
                                                    <asp:Button ID="btnViewReport" OnClick="btnViewReport_Click" runat="server" Width="90px"
                                                        Text="View Report"></asp:Button>
                                                    <asp:Button ID="btn_ViewAll" runat="server" OnClick="btn_ViewAll_Click" Text="View Report For All"
                                                        Width="136px" /></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <contenttemplate>
                                                            <table style="vertical-align: middle; width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="vertical-align: middle; width: 30%" align="left">
                                                                            &nbsp;<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                        CssClass="search-input" Visible="false">
                                                    </gridsearch:XGridSearchTextBox></td>
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
                                                                            Record Count:<%= grdCaseMaster.RecordCount%>| Page Count:
                                                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server" SelectedIndexproperty="true"
                                                                                OnSelectedIndexChanged="grdCaseMaster_SelectedIndexChanged">
                                                                            </gridpagination:XGridPaginationDropDown>
                                                                            <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                Text="Export TO Excel">
                                    <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <xgrid:XGridViewControl ID="grdCaseMaster" runat="server" Height="148px" Width="100%"
                                                                CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="50"
                                                                DataKeyNames="SZ_CASE_ID" XGridKey="PatientVisitSummary" GridLines="None" AllowPaging="true"
                                                                AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_CASE_TYPE_NAME,DT_DATE_OF_ACCIDENT,DT_CREATED_DATE,DATE_OF_FIRST_VISIT,DATE_OF_LAST_VISIT,Last_PT_Date"
                                                                ExportToExcelColumnNames="Case No,Patient Name,Case Type,Date Of Accident,Date Open,DATE OF FIRST VISIT,DATE OF LAST VISIT,Date Of Last PT Visit"
                                                                HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                                ShowExcelTableBorder="true" ExcelFileNamePrefix="ExcelLitigation" MouseOverColor="0, 153, 153"
                                                                AutoGenerateColumns="false">
                                                                <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                <PagerStyle CssClass="pgr"></PagerStyle>
                                                                <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                <Columns>
                                                                    <asp:TemplateField itemstyle-horizontalalign="Left" headerstyle-horizontalalign="left">
                                                                        <headertemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                                 </headertemplate>
                                                                        <itemtemplate>
                                                                           <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </itemtemplate>
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case No" itemstyle-horizontalalign="Left"
                                                                        headerstyle-horizontalalign="left" Visible="true">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id" itemstyle-horizontalalign="Left"
                                                                        headerstyle-horizontalalign="left" Visible="false">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SZ_PATIENT_ID" HeaderText="Patient Id" itemstyle-horizontalalign="Left"
                                                                        headerstyle-horizontalalign="left" Visible="false">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" itemstyle-horizontalalign="Left"
                                                                        headerstyle-horizontalalign="left">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" itemstyle-horizontalalign="Left"
                                                                        headerstyle-horizontalalign="left">
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" itemstyle-horizontalalign="Center"
                                                                        headerstyle-horizontalalign="Center">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                        <itemstyle horizontalalign="Center"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DT_CREATED_DATE" HeaderText="Date Open" itemstyle-horizontalalign="Center"
                                                                        headerstyle-horizontalalign="Center">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                        <itemstyle horizontalalign="Center"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DATE_OF_FIRST_VISIT" HeaderText="Date Of First Visit" itemstyle-horizontalalign="Center"
                                                                        headerstyle-horizontalalign="Center">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                        <itemstyle horizontalalign="Center"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DATE_OF_LAST_VISIT" HeaderText="Date Of Last Visit" itemstyle-horizontalalign="Center"
                                                                        headerstyle-horizontalalign="Center">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                        <itemstyle horizontalalign="Center"></itemstyle>
                                                                    </asp:BoundField>
                                                                     <asp:BoundField DataField="Last_PT_Date" HeaderText="Date Of Last PT Visit" itemstyle-horizontalalign="Center"
                                                                        headerstyle-horizontalalign="Center">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                        <itemstyle horizontalalign="Center"></itemstyle>
                                                                    </asp:BoundField>
                                                                      <asp:BoundField DataField="SZ_STATUS_NAME" HeaderText="Case Status" itemstyle-horizontalalign="Center"
                                                                        headerstyle-horizontalalign="Center">
                                                                        <headerstyle horizontalalign="Center"></headerstyle>
                                                                        <itemstyle horizontalalign="Center"></itemstyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </xgrid:XGridViewControl>
                                                        </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtLocation" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
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
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtOpenFromDate" PromptCharacter="_" AutoComplete="true">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtOpenToDate" PromptCharacter="_" AutoComplete="true">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                        PopupButtonID="ImageButton1" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                        PopupButtonID="ImageButton2" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtOpenFromDate"
                        PopupButtonID="ImageButton3" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtOpenToDate"
                        PopupButtonID="ImageButton4" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtVisitFromDate" PromptCharacter="_" AutoComplete="true">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender12" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtVisitTODate" PromptCharacter="_" AutoComplete="true">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="calVisitFromDate" runat="server" TargetControlID="txtVisitFromDate"
                        PopupButtonID="ImageButton5" />
                    <ajaxToolkit:CalendarExtender ID="CalVisitTODate" runat="server" TargetControlID="txtVisitTODate"
                        PopupButtonID="ImageButton6" />
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
