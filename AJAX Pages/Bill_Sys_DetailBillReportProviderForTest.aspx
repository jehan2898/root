<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="Bill_Sys_DetailBillReportProviderForTest.aspx.cs" Inherits="Bill_Sys_DetailBillReportProviderForTest"  Title="Untitled Page" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"  TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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
         
         
         
         
         // Start :   Download Date function 
         
         
         
         // End
         
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top; background-color:White;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                     <tr>
                       <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                          <asp:Label ID="Label1" runat="server"></asp:Label>
                          </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%"  >
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                    <b>Detail Provider Bill Report</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                   
                                <tr>
                                    <td style="width: 100%;">
                                      <div style="width: 100%;">
                                                <table style=" width: 100%; border: 1px solid #B5DF82;" class="txt2"
                                                    align="left" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                            <b class="txt3">Bill list</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                      <td style="width: 100%;height: 100%;">
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                       <ContentTemplate>
                                          <table style="vertical-align: middle; width: 100%;" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="vertical-align: middle; width: 30%" align="left">
                                                                                      <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true" Visible="false"
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
                                                                                    Record Count:<%= this.grdPayment.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server" >
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                        Text="Export TO Excel">
                                                                                   <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                          </table>      
                                      </ContentTemplate>
                                     </asp:UpdatePanel>                
                                                                              
                                         <xgrid:XGridViewControl ID="grdPayment" runat="server" Height="0px" Width="1002px"
                                                                        CssClass="mGrid"  AutoGenerateColumns="false"
                                                                        MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelPacketing" ShowExcelTableBorder="true"
                                                                        EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                        ExportToExcelColumnNames="Bill #,Case #,Patient Name,Bill Date,Visit Date,Billed,Received,Outstanding" 
                                                                        ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,SZ_PATIENT_NAME,DT_BILL_DATE,DT_FIRST_LAST_VISIT_DATE,TOTAL_BILL_AMOUNT,TOTAL_PAID_AMOUNT,TOTAL_OUTSTANDING_AMOUNT" 
                                                                        AlternatingRowStyle-BackColor="#EEEEEE"
                                                                        AllowPaging="true" GridLines="None" XGridKey="BILL_SYS_DETAIL_BILL_REPORT_BY_PROVIDER_FOR_TEST" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                        AllowSorting="true">
                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                        <Columns>
                                                                 <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill #"></asp:BoundField>
                                                                 <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundField>
                                                                 <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundField>
                                                                 <asp:BoundField DataField="DT_BILL_DATE" HeaderText="Bill Date"></asp:BoundField>
                                                                 <asp:TemplateField   HeaderText="Visit Date">
                                                                           <itemtemplate>                                                                          
                                                                                <%# DataBinder.Eval(Container,"DataItem.DT_FIRST_LAST_VISIT_DATE")%>                                                                          
                                                                           </itemtemplate>
                                                                         </asp:TemplateField> 
                                                                <%-- <asp:BoundField DataField="DT_FIRST_LAST_VISIT_DATE" HeaderText="Visit Date"></asp:BoundField>--%>
                                                                 <asp:BoundField DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible="false"></asp:BoundField>
                                                                 <asp:TemplateField   HeaderText="Billed">
                                                                           <itemtemplate>                                                                          
                                                                                <%# DataBinder.Eval(Container,"DataItem.TOTAL_BILL_AMOUNT")%>                                                                          
                                                                           </itemtemplate>
                                                                         </asp:TemplateField>                
                                                                <%-- <asp:BoundField DataField="TOTAL_BILL_AMOUNT" HeaderText="Billed" ItemStyle-HorizontalAlign="Right"></asp:BoundField>--%>
                                                                   <asp:TemplateField   HeaderText="Received">
                                                                           <itemtemplate>                                                                          
                                                                                <%# DataBinder.Eval(Container,"DataItem.TOTAL_PAID_AMOUNT")%>                                                                          
                                                                           </itemtemplate>
                                                                         </asp:TemplateField>  
                                                                 <%--<asp:BoundField DataField="TOTAL_PAID_AMOUNT" HeaderText="Received" ItemStyle-HorizontalAlign="Right"></asp:BoundField>--%>
                                                                  <asp:TemplateField   HeaderText="Outstanding">
                                                                           <itemtemplate>                                                                          
                                                                                <%# DataBinder.Eval(Container,"DataItem.TOTAL_OUTSTANDING_AMOUNT")%>                                                                          
                                                                           </itemtemplate>
                                                                         </asp:TemplateField>  
                                                                <%-- <asp:BoundField DataField="TOTAL_OUTSTANDING_AMOUNT" HeaderText="Outstanding" ItemStyle-HorizontalAlign="Right"></asp:BoundField>--%>
                                                                 <asp:BoundField DataField="SZ_COMMENT" HeaderText="Comment"></asp:BoundField>
                                                                      </Columns>
                                                                    </xgrid:XGridViewControl>
                                                            </td>     
                                                    </tr>
                                                </table>
                                            </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtOfficeId" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtFromDate" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtToDate" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtOffice" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtLocationId" runat="server" Visible="false"></asp:TextBox>
                         <td class="RightCenter" style="width: 10px; height: 100%;">     
                        <asp:DataGrid ID="grdExportToExcel" Width="100%" runat="Server" CssClass="GridTable" AutoGenerateColumns="False" visible="false">
                                                                             <Columns>
                                                                        <%--0--%>
                                                                        
                                                                         <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Provider Name" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn>
                                                                                                                                            
                                                                        
                                                                        <%--1--%>
                                                                          <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Bill Amount" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn>                                                                                                                                                                                                                           
                                                                          
                                                                        
                                                                        <%--3--%>
                                                                        <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Received">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn> 
                                                                         
                                                                          
                                                                        
                                                                        <%--4--%>
                                                                          <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Pending" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn> 
                                                                         
                                                                         <%--4--%>
                                                                          <asp:BoundColumn DataField="DT_FIRST_LAST_VISIT_DATE" HeaderText="Pending" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn> 
                                                                        
                                                                         <%--5--%>
                                                                          <asp:BoundColumn DataField="TOTAL_PAID_AMOUNT" HeaderText="Pending" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn> 
                                                                        
                                                                          <%--6--%>
                                                                          <asp:BoundColumn DataField="TOTAL_OUTSTANDING_AMOUNT" HeaderText="Pending" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn> 
                                                                        
                                                                        <%--7--%>
                                                                          <asp:BoundColumn DataField="SZ_COMMENT" HeaderText="Pending" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>
                                                                        </asp:BoundColumn> 
                                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                                        </Columns>
                                                                      </asp:DataGrid>                    
                          <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>                           
                         </td>
                        </td>
                       
                    </tr>
                   
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
