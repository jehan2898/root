<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Packeting1.aspx.cs" Inherits="Bill_Sys_Packeting1" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
         
         
         
        //To Close Error Window Of Packet Request
          function CancelErrorMassage()
        {
            document.getElementById('div3').style.visibility='hidden';
        }
                

        function openErrorMessage()
        {
            document.getElementById('div3').style.zIndex = 1;
            document.getElementById('div3').style.position = 'absolute'; 
            document.getElementById('div3').style.left= '360px'; 
            document.getElementById('div3').style.top= '250px'; 
            document.getElementById('div3').style.visibility='visible';           
            return false;            
        }
         
    </script>

    <div id="diveserch">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 99%;
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
                                                    <td style="width: 23%">
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
                                                    <td class="ContentLabel" style="width: 12%">
                                                    </td>
                                                    <td style="width: 34%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%">
                                                        From Date:</td>
                                                    <td style="width: 23%">
                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10" Width="47%"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" /><br />
                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="imgbtnFromDate" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 12%">
                                                        To Date:
                                                    </td>
                                                    <td style="width: 34%">
                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                            CssClass="text-box" MaxLength="10" Width="32%"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="imgbtnToDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%; height: 12px;">
                                                        <asp:Label ID="lblLocationName" runat="server" CssClass="ContentLabel" Text="Specialty :"></asp:Label></td>
                                                    <td style="width: 23%; height: 12px;">
                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                            Selected_Text="---Select---" Width="73%"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 12%; height: 12px;">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td style="width: 34%; height: 12px;">
                                                        &nbsp;<asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="5%"></asp:TextBox>
                                                        <asp:TextBox ID="txtSort" runat="server" Visible="false" Width="5%"></asp:TextBox>
                                                        <asp:TextBox ID="txtPacketId" runat="server" Visible="False" Width="5%"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 15%; height: 8px">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajax-loader.gif"
                                                                    Visible="False" AlternateText="[image]"></asp:Image><b><asp:Label
                                                                        ID="Label15" runat="server" Text="Processing, Please wait..." Visible="false"
                                                                        Font-Bold="true"> </asp:Label>
                                                                    </b>
                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                    <ProgressTemplate>
                                                                        <div id="DivStatus11" class="PageUpdateProgress" runat="Server">
                                                                            <asp:Image ID="ajaxLoadNotificationImage1" runat="server" ImageUrl="~/Images/ajax-loader.gif" AlternateText="[image]">
                                                                            </asp:Image>
                                                                            <label id="Label2" class="PageLoadProgress">
                                                                                <b>Processing, Please wait...</b></label>
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="width: 23%; height: 8px">
                                                    </td>
                                                    <td style="width: 12%; height: 8px">
                                                    </td>
                                                    <td style="width: 34%; height: 8px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" class="ContentLabel">
                                                        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                        &nbsp; &nbsp; &nbsp;&nbsp;
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnSend" OnClick="btnSend_Click" runat="server" CssClass="Buttons"
                                                                    Text="Send Request" Width="15%"></asp:Button>
                                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="12%" CssClass="Buttons"
                                                                    Text="Search"></asp:Button>
                                                                <asp:Button ID="btnExportToExcel" OnClick="btnExportToExcel_Click" runat="server"
                                                                    Width="14%" CssClass="Buttons" Text="Export To Excel"></asp:Button>
                                                                <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick"
                                                                    Enabled="false">
                                                                </asp:Timer>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        &nbsp; &nbsp; &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 98%" class="TDPart">
                                            <div style="height: 500px; width: 100%; overflow: auto;">
                                            
                                                <asp:UpdatePanel ID="UpdateProgress2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grdPacketing" runat="server" Width="73%" CssClass="GridTable" AutoGenerateColumns="false"
                                                            PageSize="10" DataKeyNames="SZ_BILL_NUMBER,SZ_CASE_ID" OnRowCommand="grdPacketing_RowCommand"
                                                            Height="178px" >
                                                            <RowStyle CssClass="GridRow" />
                                                            <Columns>
                                                          <%--     <asp:TemplateField HeaderText="hi">
                                                        <ItemTemplate>
                                                            <a href="javascript:ShowChildGrid('div<%# Eval("SZ_BILL_NUMBER") %>');">
                                                                <img id="imgdiv<%# Eval("SZ_BILL_NUMBER") %>" alt="Click to show/hide orders" border="0"
                                                                    src="Images/arrowright.jpg" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    
                                                                    <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkP" runat="server" CausesValidation="false" CommandName="PLS"
                                                                        Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  ></asp:LinkButton><asp:LinkButton ID="lnkM"
                                                                            runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Visible="false"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                    </asp:TemplateField>
                                                    
                                                                <asp:TemplateField HeaderText="Select All">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="javascript:SelectAll(this.checked);" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bill No">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlBillNo" runat="server" CommandName="Bill No" CommandArgument="BILL_NO"
                                                                            Font-Bold="true" Font-Size="12px">Bill No</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bill Date">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlBillDate" runat="server" CommandName="Bill Date" CommandArgument="DT_BILL_DATE"
                                                                            Font-Bold="true" Font-Size="12px">Bill Date</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.DT_BILL_DATE")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Chart #">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlChartNo" runat="server" CommandName="Chart No" CommandArgument="SZ_CHART_NO"
                                                                            Font-Bold="true" Font-Size="12px">Chart No</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CHART_NO")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Case #">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlCaseNo" runat="server" CommandName="Case No" CommandArgument="SZ_CASE_NO"
                                                                            Font-Bold="true" Font-Size="12px">Case No</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Patient Name">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Reffering Office">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlRefferingOffice" runat="server" CommandName="Reffering Office"
                                                                            CommandArgument="SZ_OFFICE" Font-Bold="true" Font-Size="12px">Reffering Office</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_OFFICE")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Speciality">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlSpeciality" runat="server" CommandName="Speciality" CommandArgument="SZ_PROCEDURE_GROUP"
                                                                            Font-Bold="true" Font-Size="12px">Speciality</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Insurance Company">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlInsuranceCompany" runat="server" CommandName="Insurance Company"
                                                                            CommandArgument="SZ_INSURANCE_NAME" Font-Bold="true" Font-Size="12px">Insurance Company</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Insurance Claim No">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlInsuranceClaimNo" runat="server" CommandName="Insurance Claim No"
                                                                            CommandArgument="SZ_CLAIM_NUMBER" Font-Bold="true" Font-Size="12px">Insurance Claim No</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CLAIM_NUMBER")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bill Amount">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnlBillAmt" runat="server" CommandName="Bill Amt" CommandArgument="FLT_BILL_AMOUNT"
                                                                            Font-Bold="true" Font-Size="12px">Bill Amount</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Received Documents">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.PresentDocument")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Missing Documents">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.MissingDocument")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="SZ_CASE_ID" HeaderText="caseID" Visible="False">
                                                                    <ItemStyle Width="0px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No." Visible="False"></asp:BoundField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnCreatePacket" runat="server" Text="Create Packet" CssClass="Buttons"
                                                                            Style="width: 100%;" CommandName="CreatePacket" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                            Visible="true"></asp:Button>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                
                                                                      <asp:TemplateField>
                                                        <ItemTemplate>
                                                            </td> </tr>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <div id="div<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;left: 25px;">
                                                                      <%--  <div id="divgrd" style="display: none; position: relative;left: 25px;">--%>
                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"  DataKeyNames="SZ_BILL_NUMBER"
                                                                            EmptyDataText="No Record Found" Width="80%" CellPadding="4" ForeColor="#333333"
                                                                            GridLines="None">
                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No."></asp:BoundField>
                                                                                <asp:BoundField DataField="DT_BILL_DATE" HeaderText="Bill Date"></asp:BoundField>
                                                                                
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                        &nbsp;
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td style="width: 98%; height: 178px;" class="TDPart" >
                                            <asp:GridView ID="grdExel" runat="server" Width="99%" CssClass="GridTable" AutoGenerateColumns="false"
                                                PageSize="10">
                                                <RowStyle CssClass="GridRow" />
                                                <Columns>
                                                 
                                                    <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No."></asp:BoundField>
                                                    <asp:BoundField DataField="DT_BILL_DATE" HeaderText="Bill Date"></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_CHART_NO" HeaderText="Chart No."></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case No."></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_OFFICE" HeaderText="Referring Office"></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_PROCEDURE_GROUP" HeaderText="Specialty"></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_CLAIM_NUMBER" HeaderText="Ins. Claim Number"></asp:BoundField>
                                                    <asp:BoundField DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amt"></asp:BoundField>
                                                    <asp:BoundField DataField="PresentDocument" HeaderText="Received Documents"></asp:BoundField>
                                                    <asp:BoundField DataField="MissingDocument" HeaderText="Missing Documents"></asp:BoundField>
                                                    <asp:BoundField DataField="SZ_CASE_ID" HeaderText="caseID" Visible="False">
                                                        <ItemStyle Width="0px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No." Visible="False"></asp:BoundField>
                                              
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                            </asp:GridView>
                                            &nbsp;
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
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="divid2" style="position: absolute; left: 428px; top: 920px; width: 50%;
                height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
                border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
                text-align: center;">
                <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
                    font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px;
                    top: 0px;">
                    Msg
                </div>
                <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
                    background-color: #8babe4;">
                    <a onclick="CancelExistPatient();" style="cursor: pointer;" title="Close">X</a>
                </div>
                <br />
                <br />
                <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
                    <span id="msgPatientExists" runat="server"></span>
                </div>
                <div style="text-align: center;">
                    <asp:Button ID="btnClient" class="Buttons" Style="width: 15%;" runat="server" Text="Ok"
                        OnClick="btnOK_Click" />
                    <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                        style="width: 15%;" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--To Show Error Massage While Packet Request--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div3" style="position: absolute; left: 428px; top: 920px; width: 50%; height: 150px;
                background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
                border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
                text-align: center;">
                <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
                    font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px;
                    top: 0px;">
                    Msg
                </div>
                <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
                    background-color: #8babe4; left: 0px; top: 0px;">
                    <a onclick="CancelErrorMassage();" style="cursor: pointer;" title="Close">X</a>
                </div>
                <br />
                <br />
                <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
                    <span id="Span1" runat="server"></span>
                </div>
                <div style="text-align: center;">
                    <%--<asp:Button ID="Button1"  class="Buttons" style="width: 15%;" runat="server" Text="Ok" OnClick="btnOK_Click" />--%>
                    <input type="button" class="Buttons" value="Ok" id="Button2" onclick="CancelErrorMassage();"
                        style="width: 15%;" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
