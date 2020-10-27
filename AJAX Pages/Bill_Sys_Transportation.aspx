<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Transportation.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Transportation"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Src="~/UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>

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
    
        
       function Clear()
       {
        //alert("call");
       
        document.getElementById("<%=txtToDate.ClientID %>").value ='';
        document.getElementById("<%=txtFromDate.ClientID %>").value ='';
        document.getElementById("<%=txtCaseNo.ClientID %>").value ='';
       
        document.getElementById("ctl00_ContentPlaceHolder1_extddlTransport").value = 'NA';
    
       
        document.getElementById("<%=ddlDateValues.ClientID %>").value =0;
       }
        
           
             
             
    </script>

    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;">
        <tr>
            <td align="left">
                <table width="42%" style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82;
                    border-bottom: 1px solid #B5DF82">
                    <tr>
                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                            <b class="txt3">Search Parameters</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        Transport Date
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        From
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1">
                                        To
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="100%">
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
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="80%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="imgbtnFromDate" />
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3">
                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="80%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="imgbtnToDate" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                        <asp:Label ID="lblLocation" runat="server" Text="Transport Name" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                        Case No
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch3" valign="top">
                                        <extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="100%" Selected_Text="---Select---"
                                            Procedure_Name="SP_MST_TRANSPOTATION" Flag_Key_Value="GET_TRANSPORT_LIST" Connection_Key="Connection_String">
                                        </extddl:ExtendedDropDownList>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch3" valign="top">
                                        <asp:TextBox ID="txtCaseNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Width="80px"
                                        Text="Search"></asp:Button>
                                    <input style="width: 80px" id="btnClear1" type="button" onclick="Clear();" value="Clear"
                                        runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UP_grdTransport" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                    <b class="txt3">Transport Information</b>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdTransport"
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
                                        <%= this.grdTransport.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="btnExportToExcel" runat="server" Text="Export TO Excel" OnClick="btnExportToExcel_onclick">
                                 <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdTransport" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="" MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                        ExportToExcelFields="TIME,SZ_CASE_NO,SZ_PATIENT_NAME,SZ_PATIENT_ADDRESS,SZ_PATIENT_PHONE,SZ_TARNSPOTATION_COMPANY_NAME,DT_TRANS_DATE"
                                        ShowExcelTableBorder="true" ExportToExcelColumnNames="Time,Case #,Patient Name,Patient Address,Patient Phone,Transport Name,Transport Date"
                                        AllowPaging="true" XGridKey="GET_TRANSPORTATION_INFO" PageRowCount="50" PagerStyle-CssClass="pgr"
                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="" headertext="Case ID" DataField="SZ_CASE_ID" visible="false" />
                                                
                                                 <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="CONVERT(time ,SZ_TRANS_TIME+' '+SZ_TRANS_TIME_TYPE)" headertext="Time"
                                                DataField="TIME" />
                                            <%--2--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_NO)" headertext="Case #"
                                                DataField="SZ_CASE_NO" />
                                            <%--3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" headertext="Patient Name"
                                                DataField="SZ_PATIENT_NAME" />
                                            <%--4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_PATIENT.SZ_PATIENT_ADDRESS" headertext="Patient Address"
                                                DataField="SZ_PATIENT_ADDRESS" />
                                            <%--5--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                               SortExpression="MST_PATIENT.SZ_PATIENT_PHONE"  headertext="Patient Phone" DataField="SZ_PATIENT_PHONE" />
                                            
                                            <%--6--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="MST_TRANSPOTATION.SZ_TARNSPOTATION_COMPANY_NAME" headertext="Transport Name"
                                                DataField="SZ_TARNSPOTATION_COMPANY_NAME" />
                                                
                                                
                                               <%--7--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="TXN_TRANSPORTATION_INFO.DT_TRANS_DATE"
                                                DataFormatString="{0:MM/dd/yyyy}" headertext="Transport Date" DataField="DT_TRANS_DATE"  />
                                            <%--8--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                SortExpression="" headertext="I_TRANS_ID" DataField="I_TRANS_ID" visible="false" />
                                            <%--9--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Patient ID" DataField="SZ_PATIENT_ID" visible="false" />
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
</asp:Content>
