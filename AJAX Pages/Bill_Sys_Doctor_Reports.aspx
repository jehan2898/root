<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Doctor_Reports.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Doctor_Reports"
    Title="Doctor Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
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

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <table style="width: 100%; background-color: White; border: 1px;">
        <tr>
            <td style="vertical-align: top; width: 100%;">
                <table width="100%" cellspacing="6px" border="0">
                    <tr>
                        <td style="border-bottom: 2px solid #BABABA;" colspan="3">
                            <span style="text-align: left; color: #FF820C; font-family: Arial; font-size: 22px;">
                                Search Parameters</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left; color: #626262; font-family: Arial; font-size: 14px;
                                        width: 37%">
                                        Set Date
                                    </td>
                                    <td style="text-align: left; color: #626262; font-family: Arial; font-size: 14px;
                                        width: 32%">
                                        From Date
                                    </td>
                                    <td style="text-align: left; color: #626262; font-family: Arial; font-size: 14px;
                                        width: 32%">
                                        To Date
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 37%">
                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="91%">
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
                                    <td valign="top">
                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="76%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="70%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                            ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="60%">
                                        </ajaxToolkit:MaskedEditValidator>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="imgbtnFromDate" />
                                    </td>
                                    <td colspan="2">
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                            ControlToValidate="txtToDate" CssClass="search-input" EmptyValueMessage="Date is required"
                                            InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"
                                            Visible="true" Width="60%">
                                        </ajaxToolkit:MaskedEditValidator>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="imgbtnToDate" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; border: 0px solid Gray; padding-top: 20px;" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td style="font-size: 11px; font-family: Arial; vertical-align: top;" align="center">
                            <dx:ASPxButton ClientInstanceName="" ID="btnreportsearch" runat="server" Text="Run Report"
                                OnClick="btnReportSearch_Click" Width="91px">
                                <ClientSideEvents Click="function(s, e) { LoadingPanel.Show(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <dx:ASPxPageControl ID="TabDoctor" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                        Height="250" Width="1122" EnableCallBacks="True" EnableViewState="true">
                        <TabPages>
                            <dx:TabPage Text="Summary Report">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server">
                                        <table width="100%" border="0" id="tblcom" runat="server" style="padding-top: 20px">
                                            <tr>
                                                <td align="right">
                                                    <asp:LinkButton ID="btnXlsExport1" OnClick="btnXlsExport1_Click" runat="server" Text="Export TO Excel">
                                                                <img 
                                                                src="Images/Excel.jpg" 
                                                                alt="" 
                                                                style="border:none;" 
                                                                height="15px" 
                                                                width ="15px" 
                                                                title = "Export TO Excel"/>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxGridView ID="grdsummary" ClientInstanceName="grdsummary" runat="server" Width="100%"
                                                        SettingsPager-PageSize="20" KeyFieldName="SZ_DOCTOR_NAME" AutoGenerateColumns="False"
                                                        SettingsPager-Mode="ShowPager" Settings-ShowGroupPanel="true">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_COMPANY_NAME" Caption="Company" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_OFFICE" Caption="Provider" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="FLT_BILL_AMOUNT" Caption="Total Billed($)"
                                                                UnboundType="Decimal" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="MN_PAID" Caption="Total Paid($)" HeaderStyle-HorizontalAlign="Center"
                                                                UnboundType="Decimal" CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="FLT_BALANCE" Caption=" Total Balance($)" HeaderStyle-HorizontalAlign="Center"
                                                                UnboundType="Decimal" CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="FLT_WRITE_OFF" Caption=" Write Off($)" HeaderStyle-HorizontalAlign="Center"
                                                                UnboundType="Decimal" CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="COUNT" Caption="Bill Count" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Settings ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" />
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem FieldName="COUNT" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="FLT_BILL_AMOUNT" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="MN_PAID" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="FLT_BALANCE" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="FLT_WRITE_OFF" SummaryType="Sum" />
                                                        </TotalSummary>
                                                        <GroupSummary>
                                                            <dx:ASPxSummaryItem FieldName="FLT_BILL_AMOUNT" ShowInGroupFooterColumn="FLT_BILL_AMOUNT"
                                                                SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="MN_PAID" SummaryType="Sum" ShowInGroupFooterColumn="MN_PAID" />
                                                            <dx:ASPxSummaryItem FieldName="FLT_BALANCE" SummaryType="Sum" ShowInGroupFooterColumn="FLT_BALANCE" />
                                                            <dx:ASPxSummaryItem FieldName="FLT_WRITE_OFF" SummaryType="Sum" ShowInGroupFooterColumn="FLT_WRITE_OFF" />
                                                            <dx:ASPxSummaryItem FieldName="COUNT" SummaryType="Sum" ShowInGroupFooterColumn="COUNT" />
                                                        </GroupSummary>
                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                        <Styles>
                                                            <FocusedRow BackColor="">
                                                            </FocusedRow>
                                                            <AlternatingRow Enabled="True">
                                                            </AlternatingRow>
                                                            <Header BackColor="#B5DF82">
                                                            </Header>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                    <dx:ASPxGridViewExporter ID="grdExportDoctroSummary" runat="server" GridViewID="grdsummary">
                                                    </dx:ASPxGridViewExporter>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Detailed Report">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server">
                                        <table width="100%" border="0" id="Table1" runat="server" style="padding-top: 20px">
                                            <tr>
                                                <td align="right">
                                                    <asp:LinkButton ID="LinkButtonDetails" OnClick="btnXlsExportDetails_Click" runat="server"
                                                        Text="Export TO Excel">
                                                                <img 
                                                                src="Images/Excel.jpg" 
                                                                alt="" 
                                                                style="border:none;" 
                                                                height="15px" 
                                                                width ="15px" 
                                                                title = "Export TO Excel"/>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--<div style="overflow:scroll;">--%>
                                                    <dx:ASPxGridView ID="grdDoctorDetailsReport" ClientInstanceName="grdDoctorDetailsReport"
                                                        runat="server" Width="100%" KeyFieldName="SZ_DOCTOR_NAME" AutoGenerateColumns="False"
                                                        SettingsPager-PageSize="2000" Settings-ShowGroupPanel="true" SettingsCustomizationWindow-Height="420"
                                                        Settings-VerticalScrollableHeight="420" Settings-ShowVerticalScrollBar="true">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_COMPANY_NAME" Caption="Company" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_OFFICE" Caption="Provider" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_BILL_NUMBER" Caption="Bill ID" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="DT_BILL_DATE" Caption="Bill Date" HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="SZ_DENIAL_REASONS" Caption="Denial Reasons"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="FLT_BILL_AMOUNT" Caption="Billed($)" HeaderStyle-HorizontalAlign="Center"
                                                                CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="MN_PAID" Caption="Paid($)" HeaderStyle-HorizontalAlign="Center"
                                                                CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="FLT_BALANCE" Caption="Balance($)" HeaderStyle-HorizontalAlign="Center"
                                                                CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="FLT_WRITE_OFF" Caption=" Write Off($)" HeaderStyle-HorizontalAlign="Center"
                                                                CellStyle-HorizontalAlign="Right">
                                                                <PropertiesTextEdit DisplayFormatString="c" />
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                        <Styles>
                                                            <FocusedRow BackColor="">
                                                            </FocusedRow>
                                                            <AlternatingRow Enabled="True">
                                                            </AlternatingRow>
                                                            <Header BackColor="#B5DF82">
                                                            </Header>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                    <dx:ASPxGridViewExporter ID="grdexportDetailsreport" runat="server" GridViewID="grdDoctorDetailsReport">
                                                    </dx:ASPxGridViewExporter>
                                                    <%-- </div>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Modal="True">
    </dx:ASPxLoadingPanel>
</asp:Content>
