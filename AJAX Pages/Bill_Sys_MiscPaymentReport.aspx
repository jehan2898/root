<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_MiscPaymentReport.aspx.cs" Inherits="Bill_Sys_MiscPaymentReport" Title="Untitled Page" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %> 
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
 
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
                return('12/01/'+y) ;
              
             }
            else
              {
                var m=t_mon-1;
                if(m<10)
                {
                  return('0'+m+'/01/'+t_year);
                }
                else
                {
                  return(m+'/01/'+t_year);
                }
                
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
                   if(m<10)
                   {
                     return('0'+m+'/'+d+'/'+t_year);
                   }
                   else
                   {
                     return(m+'/'+d+'/'+t_year);   
                   }
                  
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
                return('10/01/'+y);
              }
              else if(t_mon==4||t_mon==5||t_mon==6)
              { 
                 return('01/01/'+t_year);
                
              }else if(t_mon==7||t_mon==8||t_mon==9)
              {
                return('04/01/'+t_year);
                
              
              }else if(t_mon==10||t_mon==11||t_mon==12)
              {
                return('07/01/'+t_year);
              
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
              {return('03/31/'+t_year);
               
              }else if(t_mon==7||t_mon==8||t_mon==9)
              {
               return('06/30/'+t_year);
               
              
              }else if(t_mon==10||t_mon==11||t_mon==12)
              {
                return('09/30/'+t_year);
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
                 return('01/01/'+y);
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
           // alert("Hi");
            
            if(day<10 && (month+1)<10)
            {
              return '0'+(month+1)+'/0'+day+'/'+year;
            }
            if(day<10 && (month+1)>=10)
            {
               return ''+(month+1)+'/0'+day+'/'+year;
            }
            if(day>=10 && (month+1)<10)
            {
              return '0'+(month+1)+'/'+day+'/'+year;
            }
            if(day>=10 && (month+1)>=10)
            {
               return ''+(month+1)+'/'+day+'/'+year; 
            }
            
            
            
         }
         
         function getFormattedDateForMonth(day,month,year)
         {
            if(day<10 && month<10)
            {
              return '0'+(month)+'/0'+day+'/'+year;
            }
            if(day<10 && month>10)
            {
              return ''+(month)+'/0'+day+'/'+year;
            }
            if(day>10 && month<10)
            {
              return '0'+(month)+'/'+day+'/'+year;
            }
            if(day>10 && month>10)
            {
               return ''+(month)+'/'+day+'/'+year;
            }  
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
         
         function CheckSetDate()
         {
            getWeek();
            var selValue = document.getElementById("<%=ddlChkDateValues.ClientID %>").value;
            if(selValue == 0)
            {
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = "";
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = getDate('today');
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtChkToDate.ClientID %>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtChkToDate.ClientID %>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtChkFromDate.ClientID %>").value =  quarteryear('startquaeter');
            }
         }
         
         
         
         // Start :   Download Date function 
         
         
         
         // End
         
    </script>
    
     <script type="text/javascript" language="javascript">
      function FromDateValidation()
      {
         var year1="";
         year1=document.getElementById("<%=txtFromDate.ClientID%>").value; 
         
         //alert(year1);
        // var aryyear = new Array();
        //alert(year1.charAt(6)+"     "+year1.charAt(6));
         //if(year.indexOf)
        //alert(year1);
        
         if(year1.charAt(0)=='_' && year1.charAt(1)=='_' && year1.charAt(2)=='/' && year1.charAt(3)=='_' && year1.charAt(4)=='_' && year1.charAt(5)=='/' && year1.charAt(6)=='_' && year1.charAt(7)=='_' && year1.charAt(8)=='_' && year1.charAt(9)=='_')
         {
             return true;   
         }
         
         
         if((year1.charAt(6)=='_' && year1.charAt(7)=='_') || (year1.charAt(8)=='_' && year1.charAt(9)=='_') || (year1.charAt(6)=='0' && year1.charAt(7)=='0') || (year1.charAt(6)=='0') || (year1.charAt(9)=='_'))
         {
            //alert("Invalide 'From Date'");
               
            //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').innerText = 'Invalide F Date';
            //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').style.visibility = 'visible';
            
          // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = 'Date is Invalid';
            document.getElementById("<%=lblValidator1.ClientID %>").innerText = 'Date is Invalid';
          
          // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').style.display = "block";
         
            document.getElementById("<%=txtFromDate.ClientID%>").focus();
            return false; 
         }
         if((year1.charAt(6)!='_' && year1.charAt(7)!='_') || (year1.charAt(8)!='_' && year1.charAt(9)!='_') || (year1.charAt(6)!='0' && year1.charAt(7)!='0'))
         {
          //  document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = '';
             document.getElementById("<%=lblValidator1.ClientID %>").innerText = '';
            return true;
         }
        
      }
      
      function ToDateValidation()
      {
         var year2="";
         year2=document.getElementById("<%=txtToDate.ClientID%>").value; 
         
         if(year2.charAt(0)=='_' && year2.charAt(1)=='_' && year2.charAt(2)=='/' && year2.charAt(3)=='_' && year2.charAt(4)=='_' && year2.charAt(5)=='/' && year2.charAt(6)=='_' && year2.charAt(7)=='_' && year2.charAt(8)=='_' && year2.charAt(9)=='_')
         {
             return true;   
         }
         if((year2.charAt(6)=='_' && year2.charAt(7)=='_') || (year2.charAt(8)=='_' && year2.charAt(9)=='_') || (year2.charAt(6)=='0' && year2.charAt(7)=='0') || (year2.charAt(6)=='0') || (year2.charAt(9)=='_'))
         {
            //alert("Invalide 'To Date'");
          //  document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator2').innerText = 'Date is Invalid';
            document.getElementById("<%=lblValidator2.ClientID %>").innerText = 'Date is Invalid';
            document.getElementById("<%=txtToDate.ClientID%>").focus();
            return false; 
         }
         if((year2.charAt(6)!='_' && year2.charAt(7)!='_') || (year2.charAt(8)!='_' && year2.charAt(9)!='_') || (year2.charAt(6)!='0' && year2.charAt(7)!='0'))
         {
            //document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator2').innerText = '';
            document.getElementById("<%=lblValidator2.ClientID %>").innerText = '';
            return true;
         }
         
      }      
      
       function CheckFromDateValidation()
      {
         var year1="";
         year1=document.getElementById("<%=txtChkFromDate.ClientID%>").value; 
         
         //alert(year1);
        // var aryyear = new Array();
        //alert(year1.charAt(6)+"     "+year1.charAt(6));
         //if(year.indexOf)
        //alert(year1);
        
         if(year1.charAt(0)=='_' && year1.charAt(1)=='_' && year1.charAt(2)=='/' && year1.charAt(3)=='_' && year1.charAt(4)=='_' && year1.charAt(5)=='/' && year1.charAt(6)=='_' && year1.charAt(7)=='_' && year1.charAt(8)=='_' && year1.charAt(9)=='_')
         {
             return true;   
         }
         
         
         if((year1.charAt(6)=='_' && year1.charAt(7)=='_') || (year1.charAt(8)=='_' && year1.charAt(9)=='_') || (year1.charAt(6)=='0' && year1.charAt(7)=='0') || (year1.charAt(6)=='0') || (year1.charAt(9)=='_'))
         {
            //alert("Invalide 'From Date'");
               
           
            //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').innerText = 'Invalide F Date';
            //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').style.visibility = 'visible';
            
          // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator3').innerText = 'Date is Invalid';
           document.getElementById("<%=lblValidator3.ClientID %>").innerText = 'Date is Invalid';
          
          // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').style.display = "block";
         
            document.getElementById("<%=txtChkFromDate.ClientID%>").focus();
            return false; 
         }
         if((year1.charAt(6)!='_' && year1.charAt(7)!='_') || (year1.charAt(8)!='_' && year1.charAt(9)!='_') || (year1.charAt(6)!='0' && year1.charAt(7)!='0'))
         {
            //document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator3').innerText = '';
             document.getElementById("<%=lblValidator3.ClientID %>").innerText = '';
            return true;
         }
        
      }
      
      function CheckToDateValidation()
      {
         var year2="";
         year2=document.getElementById("<%=txtChkToDate.ClientID%>").value; 
         
         if(year2.charAt(0)=='_' && year2.charAt(1)=='_' && year2.charAt(2)=='/' && year2.charAt(3)=='_' && year2.charAt(4)=='_' && year2.charAt(5)=='/' && year2.charAt(6)=='_' && year2.charAt(7)=='_' && year2.charAt(8)=='_' && year2.charAt(9)=='_')
         {
             return true;   
         }
         if((year2.charAt(6)=='_' && year2.charAt(7)=='_') || (year2.charAt(8)=='_' && year2.charAt(9)=='_') || (year2.charAt(6)=='0' && year2.charAt(7)=='0') || (year2.charAt(6)=='0') || (year2.charAt(9)=='_'))
         {
            //alert("Invalide 'To Date'");
            //document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator4').innerText = 'Date is Invalid';
             document.getElementById("<%=lblValidator4.ClientID %>").innerText = 'Date is Invalid';
            document.getElementById("<%=txtChkToDate.ClientID%>").focus();
            return false; 
         }
         if((year2.charAt(6)!='_' && year2.charAt(7)!='_') || (year2.charAt(8)!='_' && year2.charAt(9)!='_') || (year2.charAt(6)!='0' && year2.charAt(7)!='0'))
         {
           // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator4').innerText = '';
            document.getElementById("<%=lblValidator4.ClientID %>").innerText = '';
            return true;
         }
         
      }
</script>
    
    
<div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 81%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                         
                    </tr>
                    
                    <tr>
                            
                         <td style="width: 100%; height: 185px;" class="TDPart">
                                    
                                           <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" colspan="1" style="height: 25px; text-align: left">
                                                    <strong>Miscellaneous Payment Report</strong></td>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <b> </b>
                                                    <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                              
                                            </tr>
                                           
                                            <tr>
                                                <td class="ContentLabel" style="width: 16%; height: 12px">
                                                    Invoice&nbsp;<asp:DropDownList ID="ddlDateValues" runat="Server">
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
                                                    </asp:DropDownList></td>
                                                   <td class="ContentLabel" style="width: 15%; height: 12px;">
                                                    From&nbsp;Date&nbsp; &nbsp;</td>
                                                    <td>
                                                <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator> 
                                                </td>
                                              <td class="ContentLabel" style="width: 14%; height: 12px;">
                                                    To Date&nbsp;
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                     <asp:Label ID="lblValidator2" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 14px;">
                                                    Check&nbsp;<asp:DropDownList ID="ddlChkDateValues" runat="server">
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
                                                    </asp:DropDownList></td>
                                                <td class="ContentLabel" style="width: 15%; height: 12px;">
                                                    From&nbsp; Date&nbsp; &nbsp;</td>
                                                <td style="width: 27%; height: 12px;">
                                                    <asp:TextBox ID="txtChkFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgChkFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                     <asp:Label ID="lblValidator3" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtChkFromDate" runat="server" TargetControlID="txtChkFromDate"
                                                        PopupButtonID="imgChkFromDate" />
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtChkFromDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtChkFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator> 
                                                        
                                                        
                                                     
                                                </td>
                                                 <td class="ContentLabel" style="width: 14%; height: 12px;">
                                                    To Date&nbsp;
                                                </td>
                                                 <td style="width: 35%; height: 12px;">
                                                    <asp:TextBox ID="txtChkToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                          <asp:ImageButton ID="imgChkToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                     <asp:Label ID="lblValidator4" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtChkToDate" runat="server" TargetControlID="txtChkToDate"
                                                        PopupButtonID="imgChkToDate" />
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtChkToDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4" ControlToValidate="txtChkToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator> 
                                                  
                                                </td>
                                            </tr>
                                       
                                               <tr>
                                            <td class="ContentLabel" style="width: 15%; height: 11px;">
                                                    </td>
                                              <td class="ContentLabel" style="width: 15%; height: 11px;">
                                                 Check No. &nbsp;&nbsp;</td>
                                                <td style="width: 27%; height: 11px;">
                                                   <asp:TextBox ID="txtChkNumber" runat="server"></asp:TextBox>
                                                    &nbsp;
                                                </td>
                                                 <td class="ContentLabel" style="width: 14%; height: 11px;">
                                                     Check Amount&nbsp;
                                                </td>
                                                 <td style="width: 35%; height: 11px;">
                                                    <asp:TextBox ID="txtChkAmount" runat="server"></asp:TextBox><asp:Label ID="lbldollar" runat="server" Text="$" CssClass="lbl"></asp:Label>
                                                     &nbsp;
                                                </td>
                                            </tr>
                                            
                                            
                                                <tr>
                                            <td class="ContentLabel" style="width: 15%; height: 11px;">
                                                    </td>
                                              <td class="ContentLabel" style="width: 15%; height: 11px;">
                                               Patient Name &nbsp;&nbsp;</td>
                                                <td style="width: 27%; height: 11px;">
                                                   <asp:TextBox ID="txtPatientName" runat="server"></asp:TextBox>
                                                    &nbsp;
                                                </td>
                                                 <td class="ContentLabel" style="width: 14%; height: 11px;">
                                                    Case No&nbsp;
                                                </td>
                                                 <td style="width: 35%; height: 11px;">
                                                   <asp:TextBox ID="txtCaseNo" runat="server"></asp:TextBox>
                                                     &nbsp;
                                                </td>
                                            </tr>

                                             <tr>
                                            <td class="ContentLabel" style="width: 15%; height: 11px;">
                                                    </td>
                                              <td class="ContentLabel" style="width: 15%; height: 9px;">
                                                  User
                                              </td>
                                                <td style="width: 27%; height: 11px;" align="left">
                                                    <cc1:ExtendedDropDownList ID="extddlUser" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETUSERLIST" Procedure_Name="SP_MST_USERS" Selected_Text="---Select---"
                                                        Width="170px" />
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="ContentLabel" colspan="5">
                                                  <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />&nbsp;
                                                         <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                                 <asp:TextBox ID="txtSort" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                  <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                             </tr>
                                </table>
                                    </td>
                            </tr>                                                         
                          <tr>
                                    <td style="width: 100%; height: 105px;" class="TDPart" valign="top">
                                        <asp:Label ID="lblChkAmount" runat="server" Text="Total Check Amount" Font-Bold="True" Visible="False" CssClass="lbl"></asp:Label>&nbsp
                                        <asp:Label ID= "lblChkAmountvalue" runat="server" Visible="False" CssClass="lbl"></asp:Label>
                                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <contenttemplate>
                                        <asp:DataGrid ID="grdPayment" runat="Server" AutoGenerateColumns="False" CssClass="GridTable"
                                            Width="100%" OnItemCommand="grdMiscPayment_RowCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                              
                                                <%--1--%>                                                                        
                                                                      <asp:BoundColumn DataField="I_INVOICE_DATE" HeaderText="Invoice Date" DataFormatString="{0:MM/dd/yyyy}">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>                                                                            
                                                                        </asp:BoundColumn> 
                                                                
                                                                         <%--2--%>                                                                                                                                                  
                                                                      <asp:TemplateColumn HeaderText="Invoice ID" >                                                                                               
                                                                     <ItemTemplate>
                                                                         <asp:LinkButton ID="lnkInvoice_Id" runat="server" CommandName = "Invoice Id" CommandArgument='<%#(((DataGridItem) Container).ItemIndex)%>' Text='<%# DataBinder.Eval(Container, "DataItem.I_INVOICE_ID")%>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateColumn>                                                                        
                                                                        
                                                                         <%--3--%>
                                                                           <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No"  >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>                                                                            
                                                                        </asp:BoundColumn> 
                                                                           <%--4--%>    
                                                                          <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>                                                                            
                                                                        </asp:BoundColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                                                                        
                                                                          <%--5--%>
                                                                         <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>                                                                            
                                                                        </asp:BoundColumn> 
                                                                        
                                                                        <%--6--%>
                                                                         <asp:BoundColumn DataField="BILLED" HeaderText="Billed"   DataFormatString="{0:C}">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>                                                                            
                                                                        </asp:BoundColumn>  
                                                                        
                                                                        <%--7--%>
                                                                         <asp:BoundColumn DataField="RECEIVED" HeaderText="Received"  DataFormatString="{0:C}">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>                                                                            
                                                                        </asp:BoundColumn>  
                                                                        
                                                                         <%--8--%>
                                                                          <asp:BoundColumn DataField="OUTSTANDING" HeaderText="Outstanding"  DataFormatString="{0:C}">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Right"></itemstyle>                                                                            
                                                                        </asp:BoundColumn>  
                                             </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        </contenttemplate> 
                                        </asp:UpdatePanel>
                                        
                                           <asp:DataGrid ID="grdExcelMiscPayment" runat="Server" AutoGenerateColumns="False" CssClass="GridTable"
                                            Width="98%" Visible="false" AllowPaging="false">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="I_INVOICE_DATE" HeaderText="Invoice Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="I_INVOICE_ID" HeaderText="Invoice ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE" HeaderText="Case Type" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BILLED" HeaderText="Billed" DataFormatString="{0:N2}">
                                                  <ItemStyle HorizontalAlign="Right" />      
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="RECEIVED" HeaderText="Reiceived" DataFormatString="{0:N2}">                                                
                                                    <ItemStyle HorizontalAlign="Right" />                                              
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="OUTSTANDING" HeaderText="OutStanding"   DataFormatString="{0:N2}">
                                                <ItemStyle HorizontalAlign="Right" />     
                                                </asp:BoundColumn> 
                                                              
                                             </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                              </td>
                                </tr>
                                <tr>
                                    <td width="100%" style="text-align:left;" class="TDPart">
                                        <table width="100%">
                                            <tr>
                                                <td width="10%">
                                                    <asp:Label ID="lblTotalBillAmount" runat="server" ></asp:Label>
                                                   <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="5%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                  <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                        Visible="False" Width="94px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td width="10%">
                                                    <asp:Label ID="lblTotalPaidAmount" runat="server" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <asp:Label ID="lblTotalOutstandingAmount" runat="server" ></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>    
                                </tr>
                            </table>                                                                                         
    </div>
 
 
 
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <contenttemplate>
 <DIV style="DISPLAY: none">
                <asp:LinkButton id="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana" >
                </asp:LinkButton>
             </DIV>
<ajaxToolkit:ModalPopupExtender id="ModalPopupExtender" runat="server" PopupControlID="Panel3" TargetControlID="lbn_job_det" BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="divHeader" > 
                </ajaxToolkit:ModalPopupExtender> 
                <asp:Panel style="DISPLAY: none" id="Panel3" runat="server" Width="70%" CssClass="modalPopup">
                 <div id="divHeader" style="left: 0%; vertical-align: text-bottom; width: 100%; position: absolute;
                    top: 0px; height: 15px; background-color: lightgrey; text-align: left"
                    colspan="" rowspan="">
                   
                    <asp:Button Style="left: 95%; position: absolute; top: 5px" ID="btnClose" OnClick="btnClose_Click"
                        runat="server" CssClass="Buttons" Text="X"></asp:Button>
                </div>
             <table width="100%">
                <tr>
                    <td class="TDPart" width="100%">
                        <table width="100%">
                        <tr>
                        <td style="height: 4px" width="15%">
                        <asp:Label ID="lblBillNoText" text="Bill No:-" runat="server"  font-bold="true"></asp:Label>  
                        <asp:Label ID="lblBillNo" runat="server" ></asp:Label>                        
                        </td>
                        <td style="height: 4px" width="20%">
                        <asp:Label ID="lblBillDateText" text="Bill Date:-" runat="server" font-bold="true"></asp:Label> 
                        <asp:Label ID="lblBillDate" runat="server" ></asp:Label>
                        </td>
                        <td style="height: 4px" width="40%">
                         <asp:Label ID="lblBillNameText" text="Patient Name:-" runat="server" font-bold="true"></asp:Label> 
                        <asp:Label ID="lblPatientName" runat="server" ></asp:Label>
                        </td>
                        <td style="height: 4px" width="23%">
                         <asp:Label ID="lblCaseNoText" text="Case No:-" runat="server" font-bold="true"></asp:Label> 
                        <asp:Label ID="lblCaseNo" runat="server" ></asp:Label>
                        </td>
                         <td style="height: 4px"  width="2%">
                        <%--<asp:Button id="btnClose" onclick="btnClose_Click" runat="server" Width="100%" CssClass="Buttons" Text="X" align="RIGHT"></asp:Button>--%>
                    </td> 
                        </tr>
                        </table>
                        </td>
                        </tr>
                        <tr>
                        <td class="TDPart" width="100%">
                        <asp:DataGrid ID="grdListOfPayment" AutoGenerateColumns="false" runat="server" Width="100%"
                            CssClass="GridTable" >
                            <ItemStyle CssClass="GridRow" />
                            <Columns>                                
                                <asp:BoundColumn DataField="SZ_CHECK_NUMBER" HeaderText="Check Number"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DT_CHECK_DATE" HeaderText="Check Date"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FLT_CHECK_AMOUNT" HeaderText="Check Amount" DataFormatString="{0:C}"></asp:BoundColumn>
                                 <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>                                              
                </asp:Panel> 
                </contenttemplate> 
                </asp:UpdatePanel> 
</asp:Content>

