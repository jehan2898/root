<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_DownloadDesk.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_DownloadDesk"
    Title="Untitled Page" %>

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
    
    function OpenDocManager(CaseNo,CaseId,cmpid)
    {
        window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid='+CaseId+'&caseno='+CaseNo+'&cmpid='+cmpid ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
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
                                                                                    <td  class="td-widget-bc-search-desc-ch">
                                                                                        Case No
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Patient Name
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Case Status
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" class="td-widget-bc-search-desc-ch2">
                                                                                        <asp:TextBox ID="txtCaseNumber" runat="server" Width="78%"></asp:TextBox>&nbsp;
                                                                                    </td>
                                                                                    <td style="width: 33%;" align="center" class="td-widget-bc-search-desc-ch2">
                                                                                        <asp:TextBox ID="txtPatientName" runat="server" Width="80%"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 148px;" align="center">
                                                                                        <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="CASESTATUS_LIST" Procedure_Name="SP_MST_CASE_STATUS"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                              <%--  <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                       &nbsp; Downloaded Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        To
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="82%">
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
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                       <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="70%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator></td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="70%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                            ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                    </td>
                                                                                </tr>--%>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3"  style="width:500px">
                                                                                        Carrier
                                                                                    </td>
                                                                                    
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="3" valign="top">
                                                                                    
                                                                                        <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="490px" Selected_Text="---Select---"
                                                                                             Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST" 
                                                                                            Procedure_Name="SP_MST_INSURANCE_COMPANY"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td align="center" style="height: 19px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td  class="td-widget-bc-search-desc-ch" colspan="3" style="width:500px">
                                                                                        Denial Reason
                                                                                    </td>
                                                                                   
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch"  colspan="3" valign="top">
                                                                                      <extddl:ExtendedDropDownList ID="extdlitigate" Width="490px" runat="server" Connection_Key="Connection_String"
                                                                                            Procedure_Name="SP_MST_DENIAL" Flag_Key_Value="DENIAL_LIST" Selected_Text="--- Select ---"
                                                                                            Visible="true" />
                                                                                    </td>
                                                                                   
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                &nbsp;<asp:Button ID="btnSearch" OnClick="btnSearch_Click"  runat="server" Text="Search"></asp:Button>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="text-align: right; width: 50%; vertical-align: text-top;">
                                                                <table style="width: 80%; border: 1px solid #B5DF82;" class="txt2" align="right"
                                                                    cellpadding="0" cellspacing="0" visible="false">
                                                               <%-- <tr >
                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 50%" visible="false">
                                                                            <b class="txt3">Account Summary</b>
                                                                        </td>
                                                                </tr>--%>
                                                                      <tr>
                                                                <%--<td style="width: 45%; text-align:center;vertical-align:top;padding:2px;">
                                                                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                    <contenttemplate>
                                                                   <xgrid:XGridViewControl ID="grdDownloadCompanyWise" runat="server" Height="148px"
                                                                        Width="385px" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153"
                                                                        ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true" EnableRowClick="false"
                                                                        ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"  AlternatingRowStyle-BackColor="#EEEEEE"
                                                                        AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_DownloadCompanyWise" PageRowCount="10" PagerStyle-CssClass="pgr"
                                                                        AllowSorting="true">
                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Text" HeaderText="Title">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="amount" DataFormatString="{0:C}" HeaderText="Amount($)">
                                                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                                                <itemstyle horizontalalign="Right"></itemstyle>
                                                                            </asp:BoundField>                                                                        
                                                                        </Columns>
                                                                    </xgrid:XGridViewControl>
                                                                    <table width="100%">
                                                                                <tr>
                                                                                    <td style="width: 100%;">
                                                                                        <asp:DataList ID="DtlView" runat="server" BorderWidth="0px" BorderStyle="None" BorderColor="#DEBA84"
                                        RepeatColumns="1" Width="100%"  >
                                        <HeaderTemplate>
                                            <table style="width: 100%; vertical-align: top;" border="0" cellpadding="0" cellspacing="0">
                                                <tr style="background-color: #B5DF82">
                                                    <td class="td-widget-lf-desc-ch">
                                                      
                                                        <b class="txt3">Title</b>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch2" align="right">
                                                  
                                                      <b class="txt3">Amount($)</b>
                                                      
                                                    </td>
                                                  
                                                    
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td align="center" class="lbl">
                                                 <b class="txt3">  <%# DataBinder.Eval(Container.DataItem, "Text")%></b>
                                                </td>
                                                <td align="right">
                                                  <b class="txt3">  <%# DataBinder.Eval(Container.DataItem, "amount","{0:C}")%></b>
                                                </td>
                                               
                                            </tr>
                                          
                                        </ItemTemplate>
                                        <FooterTemplate>
                                              </table>
                                        </FooterTemplate>
                                    </asp:DataList>
                                                                                    </td>--%>
                                                                                </tr>
                                                                            </table>
                                                                    </contenttemplate> 
                                                                    </asp:UpdatePanel> 
                                                                </td>
                                                            </tr>
                                                                    <tr>
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
                                                                    <b class="txt3">Case list</b>
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
                                                                                            Record Count:<%= this.grdDownLoadDesk.RecordCount%>
                                                                                            | Page Count:
                                                                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                            </gridpagination:XGridPaginationDropDown>
                                                                                            <asp:LinkButton ID="lnkExportToExcel" runat="server" OnClick="lnkExportTOExcel_onclick"  Text="Export TO Excel">
                                                                                 <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <xgrid:XGridViewControl ID="grdDownLoadDesk" runat="server" Height="148px" Width="1002px"
                                                                                CssClass="mGrid" AllowSorting="true" DataKeyNames="SZ_CASE_ID,SZ_BILL_NUMBER" PagerStyle-CssClass="pgr"
                                                                                PageRowCount="10" XGridKey="Bill_Sys_DownloadDesk" GridLines="None" AllowPaging="true"
                                                                                AlternatingRowStyle-BackColor="#EEEEEE" 
                                                                                ExportToExcelFields="SZ_CASE_NO,PATIENT NAME,INSURANCE_COMPANY,BILL_AMOUNT,PAID_AMOUNT,LITIGATION_AMOUNT,SZ_LEGAL_FIRM"
                                                                                ExportToExcelColumnNames="Case #,Patient Name,Insurance Company,Total Bill Amount,Total Paid Amount,Total Litigation Amount,Assigned Firm"
                                                                                HeaderStyle-CssClass="GridViewHeader" 
                                                                                ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                                                ShowExcelTableBorder="true" ExcelFileNamePrefix="ExcelDonloadedBills" MouseOverColor="0, 153, 153"
                                                                                AutoGenerateColumns="false" OnRowCommand="grdDownLoadDesk_RowCommand">
                                                                                <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                                <PagerStyle CssClass="pgr"></PagerStyle>
                                                                                <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                                <Columns>
                                                                                      <%--  0--%>
                                                                                    <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False">
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                    
                                                                                     <%--  1--%>
                                                                                    <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case#" SortExpression="convert(Integer,SZ_CASE_NO)">
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                     <%--  2--%>
                                                                                    <asp:TemplateField HeaderText="Bill No" SortExpression="convert(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)">
                                                                                        <itemtemplate>
                                                                            <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS"  font-size="15px" 
                                                                                Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                                <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>                                                                                  
                                                                                <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>                                                                                
                                                                            </itemtemplate>
                                                                                    </asp:TemplateField>
                                                                                     <%--  3--%>
                                                                                    <asp:BoundField DataField="PATIENT NAME" HeaderText="Patient Name" SortExpression="SZ_PATIENT_FIRST_NAME">
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                     <%--  4--%>
                                                                                    <asp:BoundField DataField="INSURANCE_COMPANY" HeaderText="Insurance Company" SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME">
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                     <%--  5--%>
                                                                                    <asp:BoundField DataField="BILL_AMOUNT" DataFormatString="{0:C}" HeaderText="Billed ($)" SortExpression="SUM(FLT_BILL_AMOUNT)" >
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Right"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                     <%--  6--%>
                                                                                    <asp:BoundField DataField="PAID_AMOUNT" DataFormatString="{0:C}" HeaderText="Paid ($)"
                                                                                    SortExpression= "cast(sum(FLT_BILL_AMOUNT) as numeric(10,2))- cast(sum(TXN_BILL_TRANSACTIONS.FLT_BALANCE) as numeric(10,2))">
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Right"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                     <%--  7--%>
                                                                                    <asp:BoundField DataField="LITIGATION_AMOUNT" DataFormatString="{0:C}" HeaderText="Litigated ($)" SortExpression="SUM(TXN_BILL_TRANSACTIONS.FLT_BALANCE)">
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Right"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                     <%--  8--%>
                                                                                    <asp:BoundField DataField="SZ_LEGAL_FIRM" HeaderText="Assigned Firm" SortExpression="SZ_COMPANY_NAME">
                                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                                    </asp:BoundField>
                                                                                    <%--  9--%>
                                                                                    <asp:TemplateField HeaderText="Docs">
                                                                                        <itemtemplate>
                                                                                <img alt="" onclick="javascript:OpenDocManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');"  src="Images/grid-doc-mng.gif" style="border:none;cursor:pointer;"/>
                                                                            </itemtemplate>
                                                                                        <itemstyle horizontalalign="Left"></itemstyle>
                                                                                    </asp:TemplateField>
                                                                               
                                                                                    <asp:TemplateField visible="false">
                                                                                        <itemtemplate>                                            
                                                         <tr>
                                                    <td colspan="100%">
                                                    <%--  10--%>
                                                        <div id="div<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;left: 25px;">
                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" 
                                                                EmptyDataText="No Record Found" Width="80%" CellPadding="4" ForeColor="#333333"
                                                                GridLines="None">
                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Bill No" HeaderText="Bill No." ItemStyle-Width="25px"></asp:BoundField>
                                                                    <asp:BoundField DataField="Total Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Billed ($)">
                                                                     <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Total Paid Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Paid ($)">
                                                                     <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Total Litigation Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Litigated ($)">
                                                                     <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Lawfirm Claim" ItemStyle-Width="105px" HeaderText="Lawfirm Claim"></asp:BoundField>                                                                    
                                                                </Columns>
                                                          </asp:GridView>
                                                        </div>
                                                   </td>
                                                </tr> 
                                                 </itemtemplate>
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
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="height: auto; width: 100%; border: 0px solid #B5DF82; background-color: White;">
                                            <tr>
                                                <td align="right" style="width: 50%; height: 20px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" style="width: 50%; height: 20px">
                                                    &nbsp;<asp:TextBox ID="txtCompanyId" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                    <asp:TextBox ID="Flag" runat="server" Visible="false" Text="1" Width="18px"></asp:TextBox>
                                                    <asp:TextBox ID="txtInsuranceCompany" runat="server" Width="31px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtDenialReason" runat="server" Width="31px" Visible="False"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtStatusID" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                  <%--  <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>--%>
                                                   <%-- <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>--%>
                                                    <%--<ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />--%>
                                                  <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10px; height: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
