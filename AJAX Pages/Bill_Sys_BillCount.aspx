<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_BillCount.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_BillCount"
    Title="Untitled Page" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>

    <script type="text/javascript">
    
     function Clear()
       {
       document.getElementById("ctl00_ContentPlaceHolder1_ddlBilldate").value=0;
       document.getElementById("<%=txtBilltodate.ClientID%>").value='';
       document.getElementById("<%=txtBillfromdate.ClientID%>").value='';
       document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value ='NA';
       
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
            var selValue = document.getElementById("<%=ddlBilldate.ClientID %>").value;
        
            if(selValue == 0)
            {
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = "";
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = getDate('today');
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtBilltodate.ClientID %>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtBilltodate.ClientID %>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtBillfromdate.ClientID %>").value =  quarteryear('startquaeter');
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
         
         
    </script>

    <table width="100%" border="0">
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="background-color: White;">
                <table>
                    <tr>
                        <td>
                            <%--       <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">--%>
                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnSearch.ClientID %>)">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                        <b class="txt3">Search Parameters</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Bill Date
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    From Date
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    To Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:DropDownList ID="ddlBilldate" runat="Server" Width="98%">
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
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtBillfromdate" onkeypress="return CheckForInteger(event,'/')"
                                                        runat="server" MaxLength="10" Width="80%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnBillfromdate" runat="server" ImageUrl="~/Images/cal.gif">
                                                    </asp:ImageButton>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtBilltodate" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                        MaxLength="10" Width="75%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnBilltodate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBillfromdate"
                                                        PopupButtonID="imgbtnBillfromdate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBilltodate"
                                                        PopupButtonID="imgbtnBilltodate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Case Type
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" style="height: 10px;">
                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100%" Selected_Text="---Select---"
                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                                        CssClass="search-input"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSearch" runat="server" Width="80px" Text="Search" OnClick="btnSearch_Click">
                                                </asp:Button>
                                                <input type="button" id="btnClear" style="width: 80px" value="Clear" onclick="Clear();" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-color: White;">
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                    <b class="txt3"></b>
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
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                runat="Server">
                                                <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
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
                                        <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                            CssClass="search-input" Visible="False">
                                        </gridsearch:XGridSearchTextBox>
                                        <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                    </td>
                                    <td style="width: 60%" align="right">
                                        Record Count:
                                        <%= this.grdBillCount.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                            Text="Export TO Excel">
                                        <img src="Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                            title="Export TO Excel" /></asp:LinkButton>
                                        <%--  <asp:Button ID="btninvoicegenerate" runat="server" Visible="true" Text="Paid" OnClick="btninvoice_Click">
                                        </asp:Button>--%>
                                        <%--  <asp:Button ID="btnExportToExcel" runat="server" Text="Export Bills" OnClick="btnExportToExcel_Click" />--%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdBillCount" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="SZ_CASE_TYPE_ID" MouseOverColor="0, 153, 153" EnableRowClick="false"
                                        ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                        ExportToExcelFields="BILL_COUNT,BillAmount,BALANCE_AMOUNT,PAID_AMOUNT,SZ_CASE_TYPE_NAME"
                                        ShowExcelTableBorder="true" ExportToExcelColumnNames="Bill Count,Bill Amount,Outstanding Amount,Paid Amount,Case Type"
                                        AllowPaging="true" XGridKey="Bill_Sys_BillCount" PageRowCount="40" PagerStyle-CssClass="pgr"
                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                headertext="Bill Count" DataField="BILL_COUNT" />
                                            <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Bill Amount" DataField="BillAmount" DataFormatString="{0:C}" />
                                            <%--2--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Outstanding Amount" DataField="BALANCE_AMOUNT" DataFormatString="{0:C}"  />
                                            <%--3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Paid Amount" DataField="PAID_AMOUNT" DataFormatString="{0:C}" />
                                            <%--4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                headertext="CASE TYPE ID" DataField="SZ_CASE_TYPE_ID" visible="false" />
                                            <%--5--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                headertext="Case Type" DataField="SZ_CASE_TYPE_NAME" />
                                        </Columns>
                                    </xgrid:XGridViewControl>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="con" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnExportToExcel" EventName="Click" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
