<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_BillReportForTest.aspx.cs" Inherits="Bill_Sys_BillReportForTest" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
    function Validate(ival)
       {
            
            var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdAllReports");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            return true;
			        }
			    }			
		    }
		    alert('Please select record.');
		    return false;
       }
    function CloseviewBills()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.visibility = 'hidden';  
       }
     function SelectAll(ival)
       {
            var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdAllReports");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
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
<div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
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
                                                <td style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <b> Bill Report For Test Facility &nbsp;</b><asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblMessage" runat="server" Style="color: red" Visible="false"> </asp:Label></td>
                                            </tr>
                                            <%-- <tr>
                                                <td class="ContentLabel" style="width: 15%"></td>
                                                <td style="width: 35%">
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
                                            </tr>--%>
                                            <%--<tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    From Date&nbsp; &nbsp;</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" PopupButtonID="imgbtnFromDate" TargetControlID="txtFromDate"  />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    To Date&nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton  ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                </td>
                                            </tr>--%>
                                           <%-- <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    Speciality&nbsp;
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                    &nbsp;<cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox></td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                   <tr>
                                                                                                           
                                    <td style="width: 100%" class="TDPart" align="right">
                                        Bill Status<cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Connection_Key="Connection_String"
                                            Flag_Key_Value="GET_SELECTED_STATUS_LIST" Procedure_Name="SP_MST_BILL_STATUS"
                                            Selected_Text="---Select---" Width="170px" />
                                        <asp:TextBox ID="txtBillStatusdate" runat="server" onkeypress="return clickButton1(event,'/')"
                                            Visible="false"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnAppointdate" runat="server" ImageUrl="~/Images/cal.gif"
                                            Visible="false" /> <ajaxToolkit:CalendarExtender ID="calAppointdate" runat="server" PopupButtonID="imgbtnAppointdate"
                                                        TargetControlID="txtBillStatusdate" >
                                                    </ajaxToolkit:CalendarExtender>
                                        <asp:Button ID="btnUpdateStatus" runat="server" CssClass="Buttons" OnClick="btnUpdateStatus_Click"
                                            Text="Update Status" />
                                     <asp:Button ID="btnPrintPOM" runat="server" CssClass="Buttons" Text="Print POM" OnClick="btnPrintPOM_Click" />
                                    <asp:Button ID="btnPrintEnvelop" runat="server" CssClass="Buttons" Text="Print Envelop" OnClick="btnPrintEnvelop_Click" /> 
                                    <asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                                    </td> 
                                    </tr>
                                    
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                     <div style="overflow: scroll; height: 600px">
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages"
                                            OnPageIndexChanged="grdAllReports_PageIndexChanged"   OnItemCommand="grdAllReports_ItemCommand" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            <%--0--%>
                                             <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                            <%--1--%>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="CaseType Id" Visible="False"></asp:BoundColumn>
                                              <%--2--%>
                                                 <asp:TemplateColumn HeaderText="Bill No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillNo" runat="server" CommandName="Bill No" CommandArgument="SZ_BILL_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Bill No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--3--%>
                                                  <asp:TemplateColumn HeaderText="Bill Date" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillDate" runat="server" CommandName="Bill Date" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.DT_BILL_DATE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--4--%>
                                                   <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" ></asp:BoundColumn>
                                                
                                                <%--5--%>
                                                   <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="false"></asp:BoundColumn>
                                                   <%--6--%>
                                                    <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                    <%--7--%>
                                                 <asp:TemplateColumn HeaderText="Chart No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="Chart No" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--8--%>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                <%--9--%>
                                                <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--10--%>
                                                <asp:TemplateColumn HeaderText="Reffering Office" Visible="false" >
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlRefferingOffice" runat="server" CommandName="Reffering Office" CommandArgument="SZ_OFFICE"
                                                            Font-Bold="true" Font-Size="12px">Reffering Office</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_OFFICE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--11--%>
                                                <asp:TemplateColumn HeaderText="Insurance Company" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceCompany" runat="server" CommandName="Insurance Company" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Company</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--12--%>
                                                <asp:TemplateColumn HeaderText="Insurance Claim No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceClaimNo" runat="server" CommandName="Insurance Claim No" CommandArgument="SZ_CLAIM_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Insurance Claim No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CLAIM_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--13--%>
                                                <asp:TemplateColumn HeaderText="Speciality" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlSpeciality" runat="server" CommandName="Speciality" CommandArgument="SZ_PROCEDURE_GROUP"
                                                            Font-Bold="true" Font-Size="12px">Speciality</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--14--%>
                                               <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Bill Status"></asp:BoundColumn> 
                                               <%--15--%>
                                                 <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"></asp:BoundColumn>
                                                 <%--16--%>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address" Visible="false"></asp:BoundColumn>
                                                 <%--17--%>
                                                 <asp:BoundColumn DataField="MIN_DATE_OF_SERVICE" HeaderText="Min Date Of Service" Visible="false" ></asp:BoundColumn>
                                                 <%--18--%>
                                                 <asp:BoundColumn DataField="MAX_DATE_OF_SERVICE" HeaderText="Max Date Of Service" Visible="false" > </asp:BoundColumn>
                                                 
                                                 <%--   Repeated Fields  --%>
                                                 <%--19--%>
                                                  <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No" Visible="False"></asp:BoundColumn>
                                                  <%--20--%>
                                                 <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" Visible="False"></asp:BoundColumn>
                                                 <%--21--%>
                                                <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No" Visible="False"></asp:BoundColumn>
                                                 <%--22--%>                                                
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" Visible="False"></asp:BoundColumn>
                                                 <%--23--%>                                                
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Reffering Office" Visible="False"></asp:BoundColumn>
                                                 <%--24--%>                                                 
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" Visible="False"></asp:BoundColumn>
                                                 <%--25--%>                                                 
                                                 <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Insurance Claim No" Visible="False"></asp:BoundColumn>
                                                 <%--26--%>                                                 
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" Visible="False"></asp:BoundColumn>
                                                 
                                                 <%--   End Repeated Fields  --%>
                                                 <%--27--%>
                                                 
                                                 <asp:TemplateColumn HeaderText="View bill">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                    CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                 <%--28--%>
                                                 
                                             <asp:BoundColumn DataField="SZ_COMPANY_NAME" HeaderText="SZ_COMPANY_NAME" Visible="false"></asp:BoundColumn>
                                                 <%--29--%>
                                             
                                                 <asp:BoundColumn DataField="WC_ADDRESS" HeaderText="WC_ADDRESS" Visible="false"></asp:BoundColumn>
                                                <%-- <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>--%>
                                                 
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        <tr>
                                        <td>
                                        <asp:Label ID="lblTotal" Text="Total" runat="server"></asp:Label>
                                        <asp:Label ID="lblTotalVal"  runat="server"></asp:Label>
                                        </td> 
                                        </tr>
                                        </div>
                                        
                                        <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false"  Visible="false" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            <asp:TemplateColumn>
                                                 <ItemTemplate>
                                                 <asp:CheckBox ID="chkSelect" runat="server" />
                                                 </ItemTemplate>
                                                 </asp:TemplateColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="CaseType Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" ></asp:BoundColumn>
                                                  <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"> </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Reffering Office" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Insurance Claim No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Bill Status"></asp:BoundColumn> 
                                                 <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MIN_DATE_OF_SERVICE" HeaderText="Min Date Of Service" Visible="false" ></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MAX_DATE_OF_SERVICE" HeaderText="Max Date Of Service" Visible="false" > </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <%--<asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>--%>
                                                  
                                   <%--          <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Received Documents"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Missing Documents"></asp:BoundColumn>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                         <asp:DataGrid ID="grdForExcel" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false"  Visible="false" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="CaseType Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" Visible=false></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"> </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Reffering Office" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Insurance Claim No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>                                                                                                  
                                                 <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Bill Status"></asp:BoundColumn> 
                                                 <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Insurance Address" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MIN_DATE_OF_SERVICE" HeaderText="Min Date Of Service" Visible="false" ></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="MAX_DATE_OF_SERVICE" HeaderText="Max Date Of Service" Visible="false" > </asp:BoundColumn>
                                                 <%--<asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" Visible = "true"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date" Visible="true"></asp:BoundColumn>--%>
                                   <%--          <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Received Documents"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Missing Documents"></asp:BoundColumn>--%>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        <asp:TextBox ID="txtSort" runat="server" Visible="false"></asp:TextBox></td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
     <asp:Panel ID="pnlviewBills" runat="server" Style="width: 450px; height: 0px; background-color: white;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="right" valign="top">
                   <table width="100%">
                        <tr>
                            <td width="80%" align="left">
                                List of Bills
                            </td>
                            <td width="20%" align="right">
                                <a onclick="CloseviewBills();" style="cursor: pointer;" title="Close">X</a>
                            </td>
                        </tr>
                   </table>
                </td>
            </tr>
            <tr>
                <td style="width: 102%" valign="top">
                    <div style="height: 150px; overflow-y: scroll;">
                        <asp:DataGrid ID="grdViewBills" runat="server" Width="97%" CssClass="GridTable"
                            AutoGenerateColumns="false" >
                            <ItemStyle CssClass="GridRow" />
                            <HeaderStyle CssClass="GridHeader" />
                            <Columns>
                                <asp:BoundColumn DataField="VERSION" HeaderText="Version" ItemStyle-HorizontalAlign="left"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="File Path"> 
                                    <ItemTemplate>
                                        <a href="<%# DataBinder.Eval(Container,"DataItem.PATH")%>"
                                            target="_blank"><%# DataBinder.Eval(Container, "DataItem.FILE_NAME")%></a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CREATION_DATE" HeaderText="Date Created" ItemStyle-HorizontalAlign="left" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
