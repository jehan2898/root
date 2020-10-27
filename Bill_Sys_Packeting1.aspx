<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Packeting1.aspx.cs" Inherits="Bill_Sys_Packeting1" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <script type="text/javascript" src="Registration/validation.js"></script>

    <script type="text/javascript">
    
    function  SelectAll(val)
    {
         var f=document.getElementById("<%= grdPacketing.ClientID %>");
         for( var i=0 ; i < f.getElementsByTagName("input").length; i++)
         {
           if(f.getElementsByTagName("input").item(i).type=="checkbox")
           {
              f.getElementsByTagName("input").item(i).checked=val;
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
        
        function CancelExistPatient()
        {
            document.getElementById('divid2').style.visibility='hidden';
        }
        
        

        function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '360px'; 
            document.getElementById('divid2').style.top= '250px'; 
            document.getElementById('divid2').style.visibility='visible';           
            return false;            
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
            var selValue = document.getElementById('<%= ddlDateValues.ClientID%>').value;
            if(selValue == 0)
            {
                   document.getElementById('<%= txtToDate.ClientID%>').value = "";
                   document.getElementById('<%= txtFromDate.ClientID%>').value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById('<%= txtToDate.ClientID%>').value = getDate('today');
                   document.getElementById('<%= txtFromDate.ClientID%>').value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById('<%= txtToDate.ClientID%>').value = getWeek('endweek');
                   document.getElementById('<%= txtFromDate.ClientID%>').value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById('<%= txtToDate.ClientID%>').value = getDate('monthend');
                   document.getElementById('<%= txtFromDate.ClientID%>').value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById('<%= txtToDate.ClientID%>').value = getDate('quarterend');
                   document.getElementById('<%= txtFromDate.ClientID%>').value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById('<%= txtToDate.ClientID%>').value = getDate('yearend');
                   document.getElementById('<%= txtFromDate.ClientID%>').value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById('<%= txtToDate.ClientID%>').value = getLastWeek('endweek');
                   document.getElementById('<%= txtFromDate.ClientID%>').value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById('<%= txtToDate.ClientID%>').value = lastmonth('endmonth');
                   
                   document.getElementById('<%= txtFromDate.ClientID%>').value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById('<%= txtToDate.ClientID%>').value =lastyear('endyear');
                   
                   document.getElementById('<%= txtFromDate.ClientID%>').value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById('<%= txtToDate.ClientID%>').value = quarteryear('endquaeter');
                   
                   document.getElementById('<%= txtFromDate.ClientID%>').value =  quarteryear('startquaeter');
            }
         }
         
         
         
    </script>
<div id="diveserch" >
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
                                        <td style="width: 98%" class="TDPart">
                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                <tr>
                                                    <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                        <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                        <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                    </td>
                                                </tr>
                                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 24%">
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
                                                <td class="ContentLabel" style="width: 13%">
                                                </td>
                                                <td style="width: 25%">
                                                </td>
                                            </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        From Date:</td>
                                                    <td style="width: 24%">
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" /><br />
                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="imgbtnFromDate" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 13%">
                                                        To Date:
                                                    </td>
                                                    <td style="width: 35%">
                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="imgbtnToDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                        <asp:Label ID="lblLocationName" runat="server" CssClass="ContentLabel" Text="Specialty :"
                                                           ></asp:Label></td>
                                                    <td style="width: 24%; height: 22px;">
                                                          <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="170px"></extddl:ExtendedDropDownList>
                                                        
                                                    </td>
                                                    <td style="width: 13%; height: 22px;">
                                                        &nbsp;</td>
                                                    <td style="width: 35%; height: 22px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                                                                                               
                                                    <td colspan="4" class="ContentLabel">
                                                        <asp:TextBox ID="txtPacketId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtSort" runat="server" Visible="false" Width="10px"></asp:TextBox>                                   
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                 <contenttemplate>
                                        <asp:UpdateProgress id="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <div id="DivStatus11" runat="Server" class="PageUpdateProgress">
                                                    <asp:Image ID="ajaxLoadNotificationImage1" runat="server" AlternateText="[image]"
                                                        ImageUrl="~/Images/ajax-loader.gif" />
                                                    <label id="Label2" class="PageLoadProgress">
                                                        <b>Processing, Please wait...</b></label>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress> <asp:Button id="btnSend" onclick="btnSend_Click" runat="server" CssClass="Buttons" Text="Send Request" width="100px"></asp:Button> <asp:Button id="btnSearch" onclick="btnSearch_Click" runat="server" Width="80px" CssClass="Buttons" Text="Search"></asp:Button> <asp:Button id="btnExportToExcel" onclick="btnExportToExcel_Click" runat="server" Width="104px" __designer:dtid="844424930132033" CssClass="Buttons" Text="Export To Excel" __designer:wfdid="w5"></asp:Button> 
                                                </contenttemplate>
                                                </asp:UpdatePanel>&nbsp; &nbsp; &nbsp;
                                                    </td>
                                               
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                          
                                    <tr>
                                        <td style="width: 98%" class="TDPart">
                                        <div style="height:500px; width:100%;  overflow:auto;">
                                         <asp:UpdatePanel ID="UpdateProgress2" runat="server">
                                        <ContentTemplate > 
                                            <asp:GridView ID="grdPacketing" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" PageSize="10"  DataKeyNames="SZ_CASE_ID,SZ_BILL_NUMBER" OnRowCommand="grdPacketing_RowCommand">                                               
                                              <RowStyle CssClass="GridRow" />                                             
                                                <Columns>
                                                 <%--0--%>   
                                                                                             
                                                <asp:TemplateField   HeaderText="Select All" Visible="true">
                                                <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server"  Text="Select All"  onclick="javascript:SelectAll(this.checked);"/>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--1--%>                                                   
                                                     <asp:TemplateField HeaderText="Bill No" Visible="true">
                                                    <HeaderTemplate>                                                        
                                                         <asp:LinkButton ID="lnlBillNo" runat="server"  CommandName="Bill No" CommandArgument="BILL_NO"
                                                            Font-Bold="true" Font-Size="12px">Bill No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--2--%>                                                    
                                                       <asp:TemplateField HeaderText="Bill Date" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillDate" runat="server" CommandName="Bill Date" CommandArgument="DT_BILL_DATE"
                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.DT_BILL_DATE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--3--%>                                                   
                                                     <asp:TemplateField HeaderText="Chart #" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="Chart No" CommandArgument="SZ_CHART_NO"
                                                            Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CHART_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--4--%>                                                    
                                                    <asp:TemplateField HeaderText="Case #" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlCaseNo" runat="server" CommandName="Case No" CommandArgument="SZ_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                    <%--5--%>                                                    
                                                     <asp:TemplateField HeaderText="Patient Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--6--%>                                                    
                                                      <asp:TemplateField HeaderText="Reffering Office" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlRefferingOffice" runat="server" CommandName="Reffering Office" CommandArgument="SZ_OFFICE"
                                                            Font-Bold="true" Font-Size="12px">Reffering Office</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_OFFICE")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--7--%>                                                   
                                                   <asp:TemplateField HeaderText="Speciality" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlSpeciality" runat="server" CommandName="Speciality" CommandArgument="SZ_PROCEDURE_GROUP"
                                                            Font-Bold="true" Font-Size="12px">Speciality</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--8--%>                                                    
                                                       <asp:TemplateField HeaderText="Insurance Company" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceCompany" runat="server" CommandName="Insurance Company" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Company</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--9--%>                                                    
                                                    <asp:TemplateField HeaderText="Insurance Claim No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceClaimNo" runat="server" CommandName="Insurance Claim No" CommandArgument="SZ_CLAIM_NUMBER"
                                                            Font-Bold="true" Font-Size="12px">Insurance Claim No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CLAIM_NUMBER")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--10--%>                                                    
                                                      <asp:TemplateField HeaderText="Bill Amount" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlBillAmt" runat="server" CommandName="Bill Amt" CommandArgument="FLT_BILL_AMOUNT"
                                                            Font-Bold="true" Font-Size="12px">Bill Amount</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--11--%>
                                                    <asp:BoundField DataField="PresentDocument" HeaderText="Received Documents"></asp:BoundField>
                                                    <%--12--%>
                                                    <asp:BoundField DataField="MissingDocument" HeaderText="Missing Documents"></asp:BoundField>                                                  
                                                    <%--13--%>                                                
                                                     <asp:BoundField DataField="SZ_CASE_ID" HeaderText="caseID" Visible="false" ItemStyle-Width="0px" ></asp:BoundField>
                                                    <%--14--%>                                                  
                                                     <asp:BoundField  DataField="SZ_BILL_NUMBER" HeaderText="Bill No." Visible="false"></asp:BoundField>
                                                   <asp:TemplateField >
                                                 <ItemTemplate >
                                                    <asp:Button ID="btnCreatePacket" runat="server" Text="Create Packet" CssClass="Buttons" style="width:100px;" CommandName="CreatePacket" CommandArgument ='<%# ((GridViewRow) Container).RowIndex %>'></asp:Button>
                                                 </ItemTemplate>
                                                 </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />                                                                                      
                                            </asp:GridView>&nbsp;
                                           </ContentTemplate>
                                            </asp:UpdatePanel> 
                                            </div>
                                        </td>
                                    </tr>
                                      <tr visible="false">
                                        <td style="width: 98%; height: 178px;" class="TDPart" visible="false">
                                        
                                            <asp:GridView ID="grdExel" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" PageSize="10"  Visible="false">
                                                <RowStyle CssClass="GridRow" />
                                                <Columns>
                                                    <%--0--%>
                                                    <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No."></asp:BoundField>
                                                    
                                                
                                                    <%--1--%>
                                                    <asp:BoundField DataField="DT_BILL_DATE" HeaderText="Bill Date"></asp:BoundField>
                                                     
                                                    <%--2--%>
                                                    <asp:BoundField DataField="SZ_CHART_NO" HeaderText="Chart No."></asp:BoundField>
                                                    
                                                    <%--3--%>
                                                    <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case No."></asp:BoundField>    
                                                    
                                                                                            
                                                    <%--4--%>
                                                    <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundField>
                                                    
                                               
                                                    <%--5--%>
                                                    <asp:BoundField DataField="SZ_OFFICE" HeaderText="Referring Office"></asp:BoundField>
                                                     
                                                    <%--6--%>
                                                    <asp:BoundField DataField="SZ_PROCEDURE_GROUP" HeaderText="Specialty"></asp:BoundField>
                                                  
                                                    <%--7--%>
                                                    <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundField>
                                                      
                                                    <%--8--%>
                                                    <asp:BoundField DataField="SZ_CLAIM_NUMBER" HeaderText="Ins. Claim Number"></asp:BoundField>                                               
                                                   
                                                    <%--9--%>
                                                    <asp:BoundField DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amt"></asp:BoundField>
                                                   
                                                    <%--10--%>
                                                    <asp:BoundField DataField="PresentDocument" HeaderText="Received Documents"></asp:BoundField>
                                                    <%--11--%>
                                                    <asp:BoundField DataField="MissingDocument" HeaderText="Missing Documents"></asp:BoundField>
                                                    <%--12--%>
                                                    <asp:BoundField DataField="SZ_CASE_ID" HeaderText="caseID" Visible="false" ItemStyle-Width="0px" ></asp:BoundField>
                                                    <%--13--%>
                                                    <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No." Visible="false" ></asp:BoundField>                                             
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                <%--<PagerStyle Mode="NumericPages" />--%>
                                            </asp:GridView>&nbsp;
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="RightCenter" style="height: 100%;">
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftBottom">
                            </td>
                            <td class="CenterBottom">
                            </td>
                            <td class="RightBottom">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
   <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>--%>
<div id="divid2" style="position: absolute; left: 428px; top: 920px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
            
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="msgPatientExists"  runat="server"></span></div>
    
        <div style="text-align: center;">
        <asp:Button ID="btnClient"  class="Buttons" style="width: 80px;" runat="server" Text="Ok" OnClick="btnOK_Click" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />
        </div>
    </div>
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

