<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_VisitReport.aspx.cs" Inherits="Bill_Sys_VisitReport" %>


<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
     Sys.Application.add_load(function() 
        {            
           if (document.getElementById("<%=hdnLocation.ClientID%>").value=="0")
            {
                document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").style.visibility='hidden';
            }
        });
    
    
    function PerPatient_OnCheckedChanged()
    {                          
         var Location = document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").value;
         var DoctorId = document.getElementById("ctl00_ContentPlaceHolder1_extddlDoctor").value;
         var Speciality = document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value;
         var ShowDenials;
         var FromDate = document.getElementById("<%=txtFromDate.ClientID%>").value;
         var ToDate = document.getElementById("<%=txtToDate.ClientID %>").value;
         var DenialFromDate = document.getElementById("<%=txtDenialFromDate.ClientID %>").value;
         var DenialToDate = document.getElementById("<%=txtDenialToDate.ClientID %>").value;
         var ShowAmount;
         if(document.getElementById("<% =chkShowBillAmount.ClientID%>").checked==true)
         {
         ShowAmount='on';         
         }
         else
         {
         ShowAmount='no';
         }   
          if(document.getElementById("<% =chkShowDenial.ClientID%>").checked==true)
         {
         ShowDenials='1';         
         }
         else
         {
         ShowDenials='0';
         }        
         window.open('Bill_Sys_VisitReportPerPatient.aspx?Showdenials='+ShowDenials+'&LocationId='+Location+'&DoctorId='+DoctorId+'&Speciality='+Speciality+'&FromDate='+FromDate+'&ToDate='+ToDate+ '&ShowAmount='+ShowAmount+ ' &DenialFromDate='+DenialFromDate+ ' &DenialToDate='+DenialToDate ,'TotalRecords', 'menubar=yes,toolbar=yes,resizable=yes,width=1000,height=800,left=30,top=30,scrollbars=2');
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
                   
        document.getElementById("<%=txtFromDate.ClientID%>").value='';
        document.getElementById("<%=txtToDate.ClientID %>").value ='';
        document.getElementById("<%=txtDenialFromDate.ClientID %>").value ='';
        document.getElementById("<%=txtDenialToDate.ClientID %>").value ='';
        document.getElementById("<%= chkShowBillAmount.ClientID%>").checked=false;
       var extvisible = document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues");
        if (extvisible != null)
        {
            document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = "0";
        }
        var strspeciality = document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality");
      
        if (strspeciality != null && strspeciality.value != "NA")
        {
       
            document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value = "NA";
        }
        
        var strdoctor = document.getElementById("ctl00_ContentPlaceHolder1_extddlDoctor");
        if (strdoctor != null)
        {
            document.getElementById("ctl00_ContentPlaceHolder1_extddlDoctor").value = "NA";
        }
        var strlocation = document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation");
        if (strlocation != null)
        {
                document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").value = "NA";
        }
         
       }
       
       function ChkChange()
       {
            
       }
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%; background-color: White">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
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
                                                    Bill
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
                                                    <ajaxcontrol:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnDateofAccident">
                                                    </ajaxcontrol:CalendarExtender>
                                                </td>
                                                <td>
                                                    <ajaxcontrol:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnDateofBirth">
                                                    </ajaxcontrol:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    Specialty
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top" colspan="2">
                                                    Doctor Name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top" colspan="2">
                                                    <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="240px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    Denial Scan Date From
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    Denial Scan Date To
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:TextBox ID="txtDenialFromDate" ReadOnly="true" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDenialFromDate" runat="server" ImageUrl="~/Images/cal.gif" Enabled="false" />
                                                     <ajaxcontrol:CalendarExtender 
                                                        ID="CalendarExtender1" 
                                                        runat="server" 
                                                        PopupButtonID="imgbtnDenialFromDate" 
                                                        TargetControlID="txtDenialFromDate">
                                                    </ajaxcontrol:CalendarExtender>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:TextBox ID="txtDenialToDate" ReadOnly="true" runat="server" Width="70px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnDenialToDate" runat="server" ImageUrl="~/Images/cal.gif" Enabled="false" />
                                                     <ajaxcontrol:CalendarExtender 
                                                        ID="CalendarExtender2" 
                                                        runat="server" 
                                                        PopupButtonID="imgbtnDenialToDate" 
                                                        TargetControlID="txtDenialToDate">
                                                    </ajaxcontrol:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:Label ID="lblLocationName" runat="server" Text="Location Name" Visible="False"
                                                        Width="94px" CssClass="td-widget-bc-search-desc-ch1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="98%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>                                                     
                                                    <asp:CheckBox  id="chkShowBillAmount" Font-Size="small"  runat="server" Text="Show Bill Amount"/>                                                                
                                                </td>
                                                <td>                                                     
                                                    <asp:CheckBox  id="chkShowDenial" runat="server" Font-Size="Small" Text="Show Denials only" OnCheckedChanged="chkShowDenial_CheckedChange" AutoPostBack="true"/>                                                                
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtShowDenials" runat="server" style="display:none;" Width="10px"></asp:TextBox>                                        
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="87px"
                                                    Text="Show Report"></asp:Button>
                                                <input type="button" id="btn_PerPatient" onclick="PerPatient_OnCheckedChanged();"
                                                style="width: 80px" value="Per Patient"/>
                                                <input style="width: 80px" id="btnClear" onclick="Clear();" type="button" value="Clear" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <div style="text-align: right;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width:10%" align="left" valign="top">
                                                                <asp:Label ID="lblTotAmount" runat="server" Text="Total Amount:" CssClass="lbl" Font-Bold="true"></asp:Label>
                                                                
                                                            </td>
                                                            <td style="width:90%" align="left" valign="top">
                                                                <asp:Label ID="lblTotAmtValue" runat="server" Text="$0.00" CssClass="lbl"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:10%" align="left" valign="top">
                                                                <asp:Label ID="lblTotPatients" runat="server" Text="Total Patients:" CssClass="lbl" Font-Bold="true"></asp:Label>
                                                                
                                                            </td>
                                                            <td style="width:10%" style="width:90%" align="left" valign="top">
                                                                <asp:Label ID="lblTotPatientsValue" runat="server" Text="0" CssClass="lbl"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:10%" align="left" valign="top">
                                                                <asp:Label ID="lblTotVisits" runat="server" Text="Total Visits:" CssClass="lbl" Font-Bold="true"></asp:Label>
                                                                
                                                            </td>
                                                            <td style="width:90%" align="left" valign="top">
                                                                <asp:Label ID="lblTotVisitsValue" runat="server" Text="0" CssClass="lbl"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <table style="vertical-align: middle; width: 100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30%" id="Searchtd" runat="server" visible="False" align="left">
                                                                Search:&nbsp;<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox></td>
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
                                                            <td style="vertical-align: middle; width: 40%; text-align: right" id="Exceltd" runat="server" visible="False" align="right" colspan="2">
                                                                Record Count:<%= grdAllReports.RecordCount%>
                                                                | Page Count:
                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                </gridpagination:XGridPaginationDropDown >
                                                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                    Text="Export TO Excel" >
                                                                <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <xgrid:XGridViewControl ID="grdAllReports" runat="server" Height="148px" Width="1002px"
                                                    CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="50"
                                                    DataKeyNames="Total Amount" XGridKey="VisitReport" GridLines="None" AllowPaging="true"
                                                    AlternatingRowStyle-BackColor="#EEEEEE" 
                                                    ExportToExcelFields="Case No,Patient Name,Event Date,Doctor Name,Provider Name,Location,Specialty,Insurance Name,Total Amount"
                                                    ExportToExcelColumnNames="Case No,Patient Name,Date Of Visit,Doctor Name,Provider Name,Location,Specialty,Insurance Name,Billed Amount"
                                                    HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                    ShowExcelTableBorder="true" ExcelFileNamePrefix="ExcelLitigation" MouseOverColor="0, 153, 153"
                                                    AutoGenerateColumns="false">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="Case No" HeaderText="Case No" SortExpression="SZ_CASE_NO">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Patient Name" HeaderText="Patient Name" SortExpression="(SELECT sz_patient_first_name + ' ' + sz_patient_last_name FROM mst_patient WHERE sz_patient_id=txn_calendar_event.sz_patient_id)">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Event Date" HeaderText="Date Of Visit" DataFormatString="{0:MM/dd/yyyy}"
                                                            SortExpression="dt_event_date"></asp:BoundField>
                                                        <asp:BoundField DataField="Doctor Name" HeaderText="Doctor Name" SortExpression="mst_doctor.sz_doctor_name">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Provider Name" HeaderText="Provider Name" SortExpression="(SELECT sz_office FROM mst_office WHERE sz_office_id=(SELECT sz_office_id FROM mst_doctor where sz_doctor_id=txn_calendar_event.sz_doctor_id))">
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="(select SZ_LOCATION_NAME  from MST_LOCATION where SZ_LOCATION_ID =(select SZ_LOCATION_ID from MST_OFFICE where 
			                                                SZ_OFFICE_ID=(select SZ_OFFICE_ID from MST_DOCTOR where SZ_DOCTOR_ID =txn_calendar_event.sz_doctor_id)))"></asp:BoundField>
                                                        <asp:BoundField DataField="Speciality" HeaderText="Specialty" SortExpression="(SELECT sz_procedure_group FROM mst_procedure_group WHERE sz_procedure_group_id IN (SELECT sz_procedure_group_id FROM txn_doctor_speciality where sz_doctor_id = txn_calendar_event.sz_doctor_id))">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Insurance Name" HeaderText="Insurance Name" SortExpression="(select isnull(sz_insurance_name,'') from mst_insurance_company where sz_insurance_id=mst_case_master.sz_insurance_id)">
                                                        </asp:BoundField>                                                        
                                                        <asp:BoundField DataField="Total Amount" HeaderText="Billed Amount" DataFormatString="{0:C}"
                                                            ItemStyle-HorizontalAlign="Right" ></asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hdnLocation" runat="server"></asp:HiddenField>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</asp:Content>
