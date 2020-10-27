<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_Invoice_Report.aspx.cs" Inherits="InvoiceReport" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
    
   
        
               function Validate()
       {
            var check=0;
            var f= document.getElementById("<%= grdInvoiceReport.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            check=1;
			            break;
			        }
			    }			
		    }
		    if(check==1)
		    {
		       var val= ConfirmDelete();
		       
		       if(val==true)
		       {
		         return true;
		       }
		       else
		       {
		         return false;
		       }
		    }
		    else
		    {
		         alert('Please select record.');
		            return false;
		    }
		    
		   
       }
        function ConfirmDelete()
        { 
             var msg = "Do you want to proceed?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
      
        }
        
        
        function ascii_value(c)
          {
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
       function ShowChildGrid (obj)
        {
             var div = document.getElementById(obj);           
             div.style.display ='block';                       
        }

        function HideChildGrid (obj)
        {
             var div = document.getElementById(obj);
            div.style.display ='none';                       
        }      
        function LawFirmValidation()
        {
           alert('Please Select LawFirm...');
        }
        
        function CheckBoxValidation()
        {
           alert('Please Select Case...');
        }    
        
        
        function OpenInvoiceWiseItem(InvoiceID,CaseId)
        {
           window.location = "Bill_Sys_Invoice.aspx?InvoiceId="+InvoiceID+"&CaseId="+CaseId;
        }
        
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table <%--id="mainbodytable"--%> cellpadding="0" cellspacing="0" style="width: 100%"
                    border="0">
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </ContentTemplate>
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
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Case No
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Patient Name
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Provider Name
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 33px" align="center">
                                                                                        <asp:TextBox ID="txtCaseNumber" runat="server" Width="90px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 33px" align="center">
                                                                                        <asp:TextBox ID="txtPatientName" runat="server" Width="90px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 33px" align="center">
                                                                                        <extddl:ExtendedDropDownList ID="extddlProviderName" runat="server" Width="120px"
                                                                                            Connection_Key="Connection_String" Procedure_Name="SP_GET_OFFICE_LIST_FOR_INVOICE" Flag_Key_Value="OFFICE_LIST"
                                                                                            Selected_Text="--- Select ---" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Invoice Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        To
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 33px" align="center">
                                                                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="100px">
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
                                                                                    <td align="center">
                                                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="90px"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator></td>
                                                                                    <td align="center">
                                                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="90px"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Amount
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblfrom" runat="server" Text="From" Font-Size="12px" Font-Bold="true"
                                                                                                    Visible="false"></asp:Label>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Label ID="lblTo" runat="server" Text="To" Font-Size="12px" Font-Bold="true"
                                                                                                    Visible="false"></asp:Label>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 33px" align="center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="ddlAmount" runat="server" Width="100px" OnSelectedIndexChanged="ddlAmount_SelectedIndexChanged"
                                                                                                    AutoPostBack="true">
                                                                                                    <asp:ListItem>---Select---</asp:ListItem>
                                                                                                    <asp:ListItem>Range</asp:ListItem>
                                                                                                    <asp:ListItem>Greater Than</asp:ListItem>
                                                                                                    <asp:ListItem>Less Than</asp:ListItem>
                                                                                                    <asp:ListItem>Equal To</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td style="width: 33px;" align="center">
                                                                                        <asp:TextBox ID="txtAmount" runat="server" Width="90px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 33px;" align="center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:TextBox ID="txtToAmt" runat="server" Width="90px" Visible="false"></asp:TextBox>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                &nbsp;<asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search">
                                                                                                </asp:Button>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
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
                                    <td style="width: 100%; height: auto;">
                                        <div style="width: 100%;">
                                            <table style="height: auto; width: 100%; border: 1px solid #B5DF82;" class="txt2"
                                                align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                        <b class="txt3">Invoice List</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1017px;">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <table style="vertical-align: middle; width: 100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                                                Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                    CssClass="search-input">
                                                                                </gridsearch:XGridSearchTextBox>
                                                                            </td>
                                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                                            </td>
                                                                            <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                                                                Record Count:<%= this.grdInvoiceReport.RecordCount %>
                                                                                | Page Count:
                                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                </gridpagination:XGridPaginationDropDown>
                                                                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                    Text="Export TO Excel">
                                                                                <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <xgrid:XGridViewControl ID="grdInvoiceReport" runat="server" Height="148px" Width="100%"
                                                                    CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="10"
                                                                    XGridKey="InvoiceReport" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                    HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                                    ShowExcelTableBorder="true" ExcelFileNamePrefix="ExcelInvoice_Report" MouseOverColor="0, 153, 153"
                                                                    AutoGenerateColumns="false" OnRowCommand="grdInvoiceReport_RowCommand" DataKeyNames="SZ_CASE_ID"
                                                                    ExportToExcelColumnNames="Invoice ID,Case #,Patient Name,Invoice Date,Service Date,Amount,Paid,Balance"
                                                                    ExportToExcelFields="Invoice No,SZ_CASE_NO,SZ_PATIENT_NAME,Invoice Date,Service Date,FLT_TOTAL,FLT_RECIVED,FLT_PENDING">
                                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Invoice ID">
                                                                            <itemtemplate>                                                                                                                                          
                                                                   <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Invoice No")%>' CommandName="InvoiceId" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>
                                                                   </asp:LinkButton>
                                                                   </itemtemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Case No">
                                                                            <itemtemplate>                                                                              
                                                                               <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>                                                                      
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Invoice Date" HeaderText="Invoice Date">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Service Date" HeaderText="Service Date">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FLT_TOTAL" DataFormatString="{0:C}" HeaderText="Amount">
                                                                            <headerstyle horizontalalign="Right"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FLT_RECIVED" DataFormatString="{0:C}" HeaderText="Paid">
                                                                            <headerstyle horizontalalign="Right"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FLT_PENDING" DataFormatString="{0:C}" HeaderText="Balance">
                                                                            <headerstyle horizontalalign="Right"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Docs">
                                                                            <itemtemplate>
                                                                            <asp:LinkButton ID="lnkMakePayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Invoice No")%>' CommandName="Make Payment"> </asp:LinkButton>                                                                                
                                                                            </itemtemplate>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Docs">
                                                                            <itemtemplate>
                                                                            <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate Invoice" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Invoice No")%>' CommandName="Generate Invoice"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>                                                                                
                                                                            </itemtemplate>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <itemtemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                            </itemtemplate>
                                                                            <itemstyle width="10px" horizontalalign="Left"></itemstyle>
                                                                        </asp:TemplateField>
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
                                <tr>
                                    <td>
                                        <table style="height: auto; width: 100%; border: 0px solid #B5DF82; background-color: White;">
                                            <tr>
                                                <td align="right" style="width: 50%; height: 20px">
                                                </td>
                                                <td align="left" style="width: 50%; height: 20px">
                                                    <asp:TextBox ID="txtCompanyId" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                    <asp:TextBox ID="txtRange" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 3px; height: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lbn_job_det"
                PopupDragHandleControlID="pnlSaveDescriptionHeader" PopupControlID="pnlSaveDescription"
                DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display:none; width: 450px;background-color: #dbe6fa;"
                ID="pnlSaveDescription" runat="server">
                <table style="width: 100%; height: 100%" id="Table1" cellspacing="0" cellpadding="0"
                    border="0">
                    <tbody>
                        <tr>
                            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; vertical-align: top;
                                width: 100%; padding-top: 3px; height: 100%">
                                <table style="width: 100%" id="MainBodyTable" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td class="LeftTop">
                                            </td>
                                            <td style="width: 446px" class="CenterTop">
                                                <table style="width: 100%" class="ContentTable" cellspacing="0" cellpadding="3" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="ContentLabel" colspan="3">
                                                                <div style="left: 0px; width: 100%; position: relative; top: 0px; background-color: #8babe4;
                                                                    text-align: left" id="pnlSaveDescriptionHeader">
                                                                    <asp:Label ID="lblHeader" runat="server" Width="173px" Font-Bold="True" Text="Miscellaneous Payment"
                                                                        CssClass="lbl" __designer:wfdid="w1"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td style="width: 10%" align="right">
                                                                <asp:Button ID="btnClose" OnClick="btnClose_Click" runat="server" Height="21px" Width="50px"
                                                                    Text="Close" CssClass="Buttons"></asp:Button></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" class="LeftCenter">
                                            </td>
                                            <td style="width: 446px" class="Center" valign="top">
                                                <table style="width: 100%; height: 100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 100%" class="TDPart">
                                                                <table style="width: 100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="text-align: left" class="ContentLabel">
                                                                                <b>Miscellaneous Payment Details</b>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div class="lbl">
                                                                                    <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                                    <div style="color: red" id="ErrorDiv" visible="true">
                                                                                        <UserMessage:MessageControl ID="usrMessage1" runat="server"></UserMessage:MessageControl>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100%; height: 18px; text-align: right">
                                                                                <asp:Button ID="Button1" OnClick="btnPaymentDelete_Click" runat="server" Text="Delete"
                                                                                    CssClass="Buttons"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100%" class="SectionDevider">
                                                                                <asp:DataGrid ID="grdPaymentTransaction" runat="server" Width="100%" CssClass="GridTable"
                                                                                    AutoGenerateColumns="false" OnItemCommand="grdPaymentTransaction_ItemCommand">
                                                                                    <FooterStyle />
                                                                                    <SelectedItemStyle />
                                                                                    <PagerStyle />
                                                                                    <AlternatingItemStyle />
                                                                                    <ItemStyle CssClass="GridRow" />
                                                                                    <Columns>
                                                                                        <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                                                        <asp:BoundColumn DataField="I_PAYMENT_ID" HeaderText="Payment ID" Visible="false"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_CHECK_NUMBER" HeaderText="Check Number"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="DT_PAYMENT_DATE" HeaderText="Posted Date" DataFormatString="{0:MM/dd/yyyy}">
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="DT_CHECK_DATE" HeaderText="Check Date" DataFormatString="{0:MM/dd/yyyy}">
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="FLT_CHECK_AMOUNT" HeaderText="Check Amount" DataFormatString="{0:0.00}"
                                                                                            ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_DESCRIPTION" Visible="true" HeaderText="Description">
                                                                                        </asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_CASE_ID" Visible="false" HeaderText="CaseId"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SZ_USER_ID" Visible="false" HeaderText="CaseId"></asp:BoundColumn>
                                                                                        <asp:TemplateColumn HeaderText="Delete">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                </asp:DataGrid>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left" class="ContentLabel">
                                                                                <b>Miscellaneous Payment Transaction</b>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <table style="width: 100%" class="ContentTable" cellspacing="0" cellpadding="3" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="text-align: center" class="ContentLabel" colspan="4" rowspan="1">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 34%" class="ContentLabel">
                                                                                Invoice id:</td>
                                                                            <td style="width: 35%" align="center">
                                                                                <asp:Label ID="lblInvoiceId" runat="server"></asp:Label></td>
                                                                            <td style="width: 26%" class="ContentLabel">
                                                                                Balance:</td>
                                                                            <td style="width: 35%" align="center">
                                                                                <asp:Label ID="lblBalance" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 34%" class="ContentLabel">
                                                                                Posted Date :</td>
                                                                            <td style="width: 35%" align="center">
                                                                                &nbsp;<asp:Label ID="lblPosteddate" runat="server"></asp:Label></td>
                                                                            <td style="width: 26%" class="ContentLabel">
                                                                                Check Date :
                                                                            </td>
                                                                            <td style="width: 35%">
                                                                                &nbsp;<asp:TextBox ID="txtChequeDate" onkeypress="return CheckForInteger(event,'/')"
                                                                                    runat="server" Width="76px" MaxLength="10"></asp:TextBox><asp:ImageButton ID="imgbtnChequeDate"
                                                                                        runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                                <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                                                &nbsp;
                                                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" Visible="true"
                                                                                    TooltipMessage="Input a Date" IsValidEmpty="True" InvalidValueMessage="Date is invalid"
                                                                                    EmptyValueMessage="Date is required" ControlToValidate="txtChequeDate" ControlExtender="MaskedEditExtender1"></ajaxToolkit:MaskedEditValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 34%" class="ContentLabel">
                                                                                Check Number :&nbsp;</td>
                                                                            <td style="width: 35%">
                                                                                &nbsp;<asp:TextBox ID="txtChequeNumber" runat="server" Width="78%"></asp:TextBox></td>
                                                                            <td style="width: 26%" class="ContentLabel">
                                                                                Check Amount :
                                                                            </td>
                                                                            <td style="width: 35%">
                                                                                &nbsp;<asp:TextBox ID="txtChequeAmount" onkeypress="return CheckForInteger(event,'.')"
                                                                                    runat="server" Width="58%" MaxLength="50"></asp:TextBox><asp:Label ID="lbldollar"
                                                                                        runat="server" Text="$" CssClass="lbl"></asp:Label></td>
                                                                            <td style="width: 15%" class="ContentLabel">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 34%" class="ContentLabel">
                                                                                Description :
                                                                            </td>
                                                                            <td colspan="3">
                                                                                &nbsp;<asp:TextBox ID="txtDescription" runat="server" Height="60px" Width="309px"
                                                                                    TextMode="MultiLine"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tdLitti_Write" runat="server" visible="false">
                                                                            <td style="height: 22px" class="ContentLabel" colspan="4">
                                                                                <asp:Button ID="btnLitigation" runat="server" Width="80px" Text="Litigation" CssClass="Buttons">
                                                                                </asp:Button><asp:Button ID="btnWriteoff" runat="server" Width="80px" Text="Write off"
                                                                                    CssClass="Buttons"></asp:Button><asp:Button ID="btnCancel" OnClick="btnCancel_Click"
                                                                                        runat="server" Width="80px" Text="Cancel" CssClass="Buttons"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tdAddUpdate" runat="server">
                                                                            <td class="ContentLabel" colspan="4">
                                                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" Text="Add"
                                                                                    CssClass="Buttons"></asp:Button>
                                                                                <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Width="80px"
                                                                                    Text="Update" CssClass="Buttons"></asp:Button>
                                                                                <asp:Button ID="Button2" OnClick="btnCancel_Click" runat="server" Width="80px" Text="Cancel"
                                                                                    CssClass="Buttons"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <asp:TextBox ID="txtPaymentDate" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtCompanyID1" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtPaymentID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                <input id="hiddenconfirmBox" type="hidden" name="hiddenconfirmBox" />
                                                <asp:TextBox ID="txthdcheckamount" runat="server" Width="0%" Visible="false"></asp:TextBox></td>
                                            <td style="height: 100%" class="RightCenter">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="LeftBottom">
                                            </td>
                                            <td style="width: 446px" class="CenterBottom">
                                                <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtChequeDate"
                                                    PopupButtonID="imgbtnChequeDate">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AutoComplete="true"
                                                    PromptCharacter="_" TargetControlID="txtChequeDate" MaskType="Date" Mask="99/99/9999">
                                                </ajaxToolkit:MaskedEditExtender>
                                            </td>
                                            <td class="RightBottom">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
