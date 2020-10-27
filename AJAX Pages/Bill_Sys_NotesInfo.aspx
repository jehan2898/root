<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_NotesInfo.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_NotesInfo"
    Title="Notes Info" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function Clear()
       {
       
      
        document.getElementById("<%=txtFromDate.ClientID%>").value='';
        document.getElementById("<%=txtToDate.ClientID%>").value='';
        document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value=0;
        document.getElementById("ctl00_ContentPlaceHolder1_exddluserlist").value ='NA';
        document.getElementById("ctl00_ContentPlaceHolder1_extddlNType").value ='NA';
      
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
         
             function SetDate1()
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
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
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
                                                    Date
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
                                                    <asp:DropDownList ID="ddlDateValues" runat="Server" Width="98%">
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
                                                    <asp:TextBox ID="txtFromDate" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                        MaxLength="10" Width="80%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif">
                                                    </asp:ImageButton>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtToDate" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                        MaxLength="10" Width="75%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnDateofAccident">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnDateofBirth">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    User Name
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Notes Type
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="exddluserlist" runat="server" Width="125px" Selected_Text="---Select---"
                                                        Procedure_Name="SP_USER_LIST" Flag_Key_Value="USER_LIST" Connection_Key="Connection_String">
                                                    </extddl:ExtendedDropDownList>
                                                </td>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="extddlNType" runat="server" Width="150px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="LIST" Procedure_Name="SP_MST_NOTES_TYPE" Selected_Text="---Select---" />
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
    </table>
    <table width="100%">
        <tr>
            <td>
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
                                        Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                            CssClass="search-input">
                                        </gridsearch:XGridSearchTextBox>
                                        <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                    </td>
                                    <td style="width: 60%" align="right">
                                        Record Count:
                                        <%= this.grdUserlist.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                            Text="Export TO Excel">
                                        <img src="Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                            title="Export TO Excel" /></asp:LinkButton>
                                        <%--  <asp:Button ID="btnExportToExcel" runat="server" Text="Export Bills" OnClick="btnExportToExcel_Click" />--%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdUserlist" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="" MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                        ExportToExcelFields="SZ_USER_NAME,SZ_USER_ID,SZ_CASE_ID,SZ_CASE_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,SZ_NOTE_DESCRIPTION,SZ_NOTES_TYPE,DT_ADDED"
                                        ShowExcelTableBorder="true" ExportToExcelColumnNames="User Name,User Id,Case ID,Case No,Patient Name,Insurance Name,Note Description,Note Type,Date"
                                        AllowPaging="true" XGridKey="Bill_Sys_NotesInfo" PageRowCount="40" PagerStyle-CssClass="pgr"
                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_CASE_MASTER.SZ_CASE_ID" visible="false" headertext="Case ID"
                                                DataField="SZ_CASE_ID" />
                                            <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_CASE_MASTER.SZ_CASE_NO" headertext="Case No" DataField="SZ_CASE_NO" />
                                            <%--2--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_PATIENT.SZ_PATIENT_LAST_NAME   + ' '  + MST_PATIENT.SZ_PATIENT_FIRST_NAME"
                                                headertext="Patient Name" DataField="SZ_PATIENT_NAME" />
                                            <%--3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME" headertext="Insurance Name"
                                                DataField="SZ_INSURANCE_NAME" />
                                            <%--4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="TXN_NOTES.SZ_NOTE_DESCRIPTION" headertext="Note Description"
                                                DataField="SZ_NOTE_DESCRIPTION" />
                                            <%--5--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_NOTES_TYPE.SZ_DESCRIPTION" headertext="Note Type" DataField="SZ_NOTES_TYPE" />
                                            <%--6--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                SortExpression="TXN_NOTES.DT_ADDED" headertext="Date" DataField="DT_ADDED" />
                                            <%--7--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_USERS.SZ_USER_NAME" headertext="User Name" DataField="SZ_USER_NAME" />
                                            <%--8--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="User Id" DataField="SZ_USER_ID" visible="false" />
                                            <%--9--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                 SortExpression="MST_LOCATION.SZ_LOCATION_NAME" headertext="Location" DataField="Location"/>
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
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
</asp:Content>
