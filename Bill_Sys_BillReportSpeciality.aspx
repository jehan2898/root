<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_BillReportSpeciality.aspx.cs" Inherits="Bill_Sys_BillReportSpeciality" Title="Green Your Bills - Speciality Reports" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="AJAX Pages/script/PatientList.js"></script>
    <script type="text/javascript" src="validation.js"></script>

   <%-- <script type="text/javascript">
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
    </script>--%>
    
    
     <script type="text/javascript">
         function ascii_value(c) {
             c = c.charAt(0);
             var i;
             for (i = 0; i < 256; ++i) {
                 var h = i.toString(16);
                 if (h.length == 1)
                     h = "0" + h;
                 h = "%" + h;
                 h = unescape(h);
                 if (h == c)
                     break;
             }
             return i;
         }
         function CheckForInteger(e, charis) {
             var keychar;
             if (navigator.appName.indexOf("Netscape") > (-1)) {
                 var key = ascii_value(charis);
                 if (e.charCode == key || e.charCode == 0) {
                     return true;
                 } else {
                     if (e.charCode < 48 || e.charCode > 57) {
                         return false;
                     }
                 }
             }
             if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                 var key = ""
                 if (charis != "") {
                     key = ascii_value(charis);
                 }
                 if (event.keyCode == key) {
                     return true;
                 }
                 else {
                     if (event.keyCode < 48 || event.keyCode > 57) {
                         return false;
                     }
                 }
             }


         }

         function SetDate() {
             getWeek();
             var selValue = document.getElementById('_ctl0_ContentPlaceHolder1_ddlDateValues').value;
             if (selValue == 0) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = "";
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = "";

             }
             else if (selValue == 1) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('today');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('today');
             }
             else if (selValue == 2) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getWeek('endweek');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getWeek('startweek');
             }
             else if (selValue == 3) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('monthend');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('monthstart');
             }
             else if (selValue == 4) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('quarterend');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('quarterstart');
             }
             else if (selValue == 5) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('yearend');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('yearstart');
             }
             else if (selValue == 6) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getLastWeek('endweek');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getLastWeek('startweek');
             } else if (selValue == 7) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = lastmonth('endmonth');

                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = lastmonth('startmonth');
             } else if (selValue == 8) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = lastyear('endyear');

                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = lastyear('startyear');
             } else if (selValue == 9) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = quarteryear('endquaeter');

                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = quarteryear('startquaeter');
             }
         }

         function SetDate1() {
             getWeek();
             var selValue = document.getElementById('_ctl0_ContentPlaceHolder1_ddlServDateValue').value;
             if (selValue == 0) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = "";
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = "";

             }
             else if (selValue == 1) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = getDate('today');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = getDate('today');
             }
             else if (selValue == 2) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = getWeek('endweek');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = getWeek('startweek');
             }
             else if (selValue == 3) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = getDate('monthend');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = getDate('monthstart');
             }
             else if (selValue == 4) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = getDate('quarterend');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = getDate('quarterstart');
             }
             else if (selValue == 5) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = getDate('yearend');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = getDate('yearstart');
             }
             else if (selValue == 6) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = getLastWeek('endweek');
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = getLastWeek('startweek');
             } else if (selValue == 7) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = lastmonth('endmonth');

                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = lastmonth('startmonth');
             } else if (selValue == 8) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = lastyear('endyear');

                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = lastyear('startyear');
             } else if (selValue == 9) {
                 document.getElementById('_ctl0_ContentPlaceHolder1_txtlastVisitDate').value = quarteryear('endquaeter');

                 document.getElementById('_ctl0_ContentPlaceHolder1_txtFirstVisitDate').value = quarteryear('startquaeter');
             }
         }

         function getLastWeek(type) {
             var d = new Date();
             d.setDate(d.getDate() - 7);
             var day = d.getDay();
             d.setDate(d.getDate() - day);
             if (type == 'startweek')
                 return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
             if (type == 'endweek') {
                 d.setDate(d.getDate() + 6);
                 return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
             }
         }

         function lastmonth(type) {

             var d = new Date();
             var t_date = d.getDate();      // Returns the day of the month
             var t_mon = d.getMonth() + 1;      // Returns the month as a digit
             var t_year = d.getFullYear();

             if (type == 'startmonth') {
                 if (t_mon == 1) {
                     var y = t_year - 1;
                     return ('12/01/' + y);

                 }
                 else {
                     var m = t_mon - 1;
                     if (m < 10) {
                         return ('0' + m + '/01/' + t_year);
                     }
                     else {
                         return (m + '/01/' + t_year);
                     }

                 }
             }
             else if (type == 'endmonth') {
                 if (t_mon == 1) {
                     var y = t_year - 1;
                     return ('12/31/' + y);
                 } else {
                     var m = t_mon - 1;
                     var d = daysInMonth(t_mon - 1, t_year);
                     if (m < 10) {
                         return ('0' + m + '/' + d + '/' + t_year);
                     }
                     else {
                         return (m + '/' + d + '/' + t_year);
                     }

                 }
             }

         }



         function quarteryear(type) {
             var d = new Date();
             var t_date = d.getDate();      // Returns the day of the month
             var t_mon = d.getMonth() + 1;      // Returns the month as a digit
             var t_year = d.getFullYear();

             if (type == 'startquaeter') {
                 if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
                     var y = t_year - 1;
                     return ('10/01/' + y);
                 }
                 else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                     return ('01/01/' + t_year);

                 } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                     return ('04/01/' + t_year);


                 } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                     return ('07/01/' + t_year);

                 }

             } else if (type == 'endquaeter') {
                 if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
                     //
                     var y = t_year - 1;
                     return ('12/31/' + y);
                 }
                 else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                     return ('03/31/' + t_year);

                 } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                     return ('06/30/' + t_year);


                 } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                     return ('09/30/' + t_year);
                 }

             }

         }

         function lastyear(type) {
             var d = new Date();

             var t_year = d.getFullYear();
             if (type == 'startyear') {
                 y = t_year - 1;
                 return ('01/01/' + y);
             }
             else if (type == 'endyear') {
                 y = t_year - 1;
                 return ('12/31/' + y);
             }
         }



         function getDate(type) {
             var d = new Date();
             var t_date = d.getDate();      // Returns the day of the month
             var t_mon = d.getMonth();      // Returns the month as a digit
             var t_year = d.getFullYear();  // Returns 4 digit year

             var q_start = 0;
             var q_end = 0;
             if ((t_mon + 1) >= 1 && (t_mon + 1) <= 3) {
                 q_start = 1;
                 q_end = 3;
             }
             else if ((t_mon + 1) >= 4 && (t_mon + 1) <= 6) {
                 q_start = 4;
                 q_end = 6;
             }
             else if ((t_mon + 1) >= 7 && (t_mon + 1) <= 9) {
                 q_start = 7;
                 q_end = 9;
             }
             else if ((t_mon + 1) >= 10 && (t_mon + 1) <= 12) {
                 q_start = 10;
                 q_end = 12;
             }

             if (type == 'today')
                 return (getFormattedDate(t_date, t_mon, t_year));
             if (type == 'monthstart')
                 return (getFormattedDate(1, t_mon, t_year));
             if (type == 'monthend')
                 return (getFormattedDate(daysInMonth(t_mon + 1, t_year), t_mon, t_year));
             if (type == 'quarterstart') {
                 return (getFormattedDateForMonth(1, q_start, t_year));
             }
             if (type == 'quarterend') {
                 return (getFormattedDateForMonth(daysInMonth(q_end), q_end, t_year));
             }
             if (type == 'yearstart')
                 return (getFormattedDate(1, 0, t_year));
             if (type == 'yearend')
                 return (getFormattedDate(31, 11, t_year));
         }

         function daysInMonth(month, year) {
             var m = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
             if (month != 2) return m[month - 1];
             if (year % 4 != 0) return m[1];
             if (year % 100 == 0 && year % 400 != 0) return m[1];
             return m[1] + 1;
         }

         function getFormattedDate(day, month, year) {
             // alert("Hi");

             if (day < 10 && (month + 1) < 10) {
                 return '0' + (month + 1) + '/0' + day + '/' + year;
             }
             if (day < 10 && (month + 1) >= 10) {
                 return '' + (month + 1) + '/0' + day + '/' + year;
             }
             if (day >= 10 && (month + 1) < 10) {
                 return '0' + (month + 1) + '/' + day + '/' + year;
             }
             if (day >= 10 && (month + 1) >= 10) {
                 return '' + (month + 1) + '/' + day + '/' + year;
             }



         }

         function getFormattedDateForMonth(day, month, year) {
             if (day < 10 && month < 10) {
                 return '0' + (month) + '/0' + day + '/' + year;
             }
             if (day < 10 && month > 10) {
                 return '' + (month) + '/0' + day + '/' + year;
             }
             if (day > 10 && month < 10) {
                 return '0' + (month) + '/' + day + '/' + year;
             }
             if (day > 10 && month > 10) {
                 return '' + (month) + '/' + day + '/' + year;
             }
         }

         function getWeek(type) {
             var d = new Date();
             var day = d.getDay();
             d.setDate(d.getDate() - day);
             if (type == 'startweek')
                 return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
             if (type == 'endweek') {
                 d.setDate(d.getDate() + 6);
                 return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
             }
         }
    </script>
    
    <script type="text/javascript" language="javascript">
        function FromDateValidation() {
            var year1 = "";
            year1 = document.getElementById("<%=txtFromDate.ClientID%>").value;

            //alert(year1);
            // var aryyear = new Array();
            //alert(year1.charAt(6)+"     "+year1.charAt(6));
            //if(year.indexOf)
            //alert(year1);

            if (year1.charAt(0) == '_' && year1.charAt(1) == '_' && year1.charAt(2) == '/' && year1.charAt(3) == '_' && year1.charAt(4) == '_' && year1.charAt(5) == '/' && year1.charAt(6) == '_' && year1.charAt(7) == '_' && year1.charAt(8) == '_' && year1.charAt(9) == '_') {
                return true;
            }


            if ((year1.charAt(6) == '_' && year1.charAt(7) == '_') || (year1.charAt(8) == '_' && year1.charAt(9) == '_') || (year1.charAt(6) == '0' && year1.charAt(7) == '0') || (year1.charAt(6) == '0') || (year1.charAt(9) == '_')) {
                //alert("Invalide 'From Date'");


                //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').innerText = 'Invalide F Date';
                //document.getElementById('_ctl0_ContentPlaceHolder1_MaskedEditValidator1').style.visibility = 'visible';

                document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = 'Date is Invalid';

                // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').style.display = "block";

                document.getElementById("<%=txtFromDate.ClientID%>").focus();
                return false;
            }
            if ((year1.charAt(6) != '_' && year1.charAt(7) != '_') || (year1.charAt(8) != '_' && year1.charAt(9) != '_') || (year1.charAt(6) != '0' && year1.charAt(7) != '0')) {
                document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').innerText = '';
                return true;
            }

        }

        function ToDateValidation() {
            var year2 = "";
            year2 = document.getElementById("<%=txtToDate.ClientID%>").value;

            if (year2.charAt(0) == '_' && year2.charAt(1) == '_' && year2.charAt(2) == '/' && year2.charAt(3) == '_' && year2.charAt(4) == '_' && year2.charAt(5) == '/' && year2.charAt(6) == '_' && year2.charAt(7) == '_' && year2.charAt(8) == '_' && year2.charAt(9) == '_') {
                return true;
            }
            if ((year2.charAt(6) == '_' && year2.charAt(7) == '_') || (year2.charAt(8) == '_' && year2.charAt(9) == '_') || (year2.charAt(6) == '0' && year2.charAt(7) == '0') || (year2.charAt(6) == '0') || (year2.charAt(9) == '_')) {
                //alert("Invalide 'To Date'");
                document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator2').innerText = 'Date is Invalid';
                document.getElementById("<%=txtToDate.ClientID%>").focus();
                return false;
            }
            if ((year2.charAt(6) != '_' && year2.charAt(7) != '_') || (year2.charAt(8) != '_' && year2.charAt(9) != '_') || (year2.charAt(6) != '0' && year2.charAt(7) != '0')) {
                document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator2').innerText = '';
                return true;
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
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <b>Bill Report By Specialty </b>
                                                    <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 174px"></td>
                                                <td style="width: 174px">
                                                    <asp:DropDownList ID="ddlDateValues" runat="Server" Width="174px">
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
                                            </tr>
                                            <tr>
                                               
                                                    
                                                <td class="ContentLabel" style="width: 15%">
                                                From Date&nbsp; &nbsp;</td>
                                                <td style="width: 25%">
                                                    <%--<asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtFromDate" runat="server" Width="150px" CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate" PopupButtonID="imgbtnFromDate" />
                                                    <%--<asp:Label ID="lblValidator1" runat="server" Text="" Visible="true"></asp:Label>--%>   
                                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>   
                                                    
                                                    
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    To Date&nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    <%--<asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <asp:Label ID="lblValidator2" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate" PopupButtonID="imgbtnToDate" />
                                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator> 
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 174px"></td>
                                                <td style="width: 174px">
                                                    <asp:DropDownList ID="ddlServDateValue" runat="Server" Width="174px">
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
                                            </tr>
                                            <tr>
                                               
                                                    
                                                <td class="ContentLabel" style="width: 15%">
                                                Service Date From&nbsp; &nbsp;</td>
                                                <td style="width: 25%">
                                                    <%--<asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtFirstVisitDate" runat="server" Width="150px" CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgBtnFVisitDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtVisitFromDate" runat="server" TargetControlID="txtFirstVisitDate" PopupButtonID="imgBtnFVisitDate" />
                                                    <%--<asp:Label ID="lblValidator1" runat="server" Text="" Visible="true"></asp:Label>--%>   
                                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFirstVisitDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtFirstVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>   
                                                    
                                                    
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    To Date&nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    <%--<asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtlastVisitDate" runat="server" CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgBtnLVisitDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="calExtVisitToDate" runat="server" TargetControlID="txtlastVisitDate" PopupButtonID="imgBtnLVisitDate" />
                                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtlastVisitDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4" ControlToValidate="txtlastVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator> 
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    &nbsp;Bill Status&nbsp;</td>
                                                <td style="width: 25%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="170px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_STATUS_LIST" Procedure_Name="SP_MST_BILL_STATUS" Selected_Text="---Select---" />
                                                    &nbsp;
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Specialty
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                    <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="150px"></cc1:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="ContentLabel" style="width: 15%;">
                                                    <asp:CheckBox ID="chkVerificationsent" runat="Server" Text="VerificationSent"  Visible="false"/>
                                                    <asp:CheckBox ID="chkVerificationreceived" runat="Server" Text="VerificationReceived" Visible="false"/>
                                                    <asp:CheckBox ID="chkdenail" runat="Server" Text="Denial" Visible="false"/>
                                                    <asp:CheckBox ID="chkPaidfull" runat="Server" Text="PaidInFull" Visible="false"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    User
                                                </td>
                                                <td style="width: 25%">
                                                    <cc1:ExtendedDropDownList ID="extddlUser" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_USERS" Flag_Key_Value="GETUSERLIST"
                                                        Selected_Text="---Select---" Width="170px"></cc1:ExtendedDropDownList>
                                                    <%--<asp:DropDownList ID="ddlUserList" runat="server" >
                                                       <asp:ListItem Selected="True" Value = "NA">Select User</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                        Visible="False" Width="94px"></asp:Label></td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="59%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%"> <%--Added By kapil--%>
                                                    Case Type
                                                </td>
                                                <td style="width: 25%">
                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="170px" Selected_Text="---Select---"
                                                                Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                                                CssClass="search-input"></extddl:ExtendedDropDownList>
                                                    <%--<asp:DropDownList ID="ddlUserList" runat="server" >
                                                       <asp:ListItem Selected="True" Value = "NA">Select User</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%"> <%--Added By kapil--%>
                                                    Case Number
                                                </td>
                                                   
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtCaseNo" runat="server" Width="35%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%"> <%--Added By kapil--%>
                                                    Patient Name
                                                </td>
                                                   
                                                 <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtPatientName" runat="server" Width="170px" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                    <extddl:ExtendedDropDownList ID="extddlPatient" runat="server" Width="0%" Selected_Text="--- Select ---"
                                                        Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="REF_PATIENT_LIST" Connection_Key="Connection_String"
                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlPatient_extendDropDown_SelectedIndexChanged"
                                                        Visible="false"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%"> <%--Added By kapil--%>
                                                    Bill Number
                                                </td>
                                                   
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtBillNo" runat="server" Width="35%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Insurance Name
                                                </td>
                                                <td style="width: 25%">
                                                    <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="200px" runat="server" CssClass="dropdown"
                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                        Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                             
                                
                               <%-- <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="text-align: right;">
                                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                        </div>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:Label ID="lblAmountBill" runat="server" Text="Total Amount : "/> <asp:Label ID="lblTotal" runat="server" /> <br />
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" OnItemCommand = "grdAllReports_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Status" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CommandName = "View" Text='<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%>' Visible="false"></asp:LinkButton>
                                                        <%-- <a target="_self" href='AJAX Pages/Bill_Sys_ViewBillRecordSpeciality.aspx?flag=View&Status=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_Id")%>&speciality=<%# DataBinder.Eval(Container,"DataItem.SZ_SPECIALITY_ID")%>'><%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%></a>--%>
                                                         <%--<a target="_self" href='AJAX Pages/Bill_Sys_ViewBillRecordSpeciality.aspx?flag=View&Status=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_Id")%>&speciality=<%# DataBinder.Eval(Container,"DataItem.SZ_SPECIALITY_ID")%>&CaseType=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_TYPE_ID")%>'><%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%></a>--%>
                                                         <%--Kiran--%>
                                                         <a target="_self" href='AJAX Pages/Bill_Sys_ViewBillRecordSpeciality.aspx?flag=View&Status=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_Id")%>&speciality=<%# DataBinder.Eval(Container,"DataItem.SZ_SPECIALITY_ID")%>&CaseType=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_TYPE_ID")%>&CaseNumber=<%# txtCaseNo.Text %>&BillNumber=<%# txtBillNo.Text %>&PatientName=<%# txtPatientName.Text %>'><%# DataBinder.Eval(Container, "DataItem.SZ_BILL_STATUS")%></a>
                                                         <%--kiran--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Special1" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Specialty"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BILL COUNT" HeaderText="Count Of Bills"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SUM" HeaderText="Sum Of Amount" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILL_STATUS_ID" HeaderText="StatusID" Visible ="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_SPECIALITY_ID" HeaderText="SpecialityID" Visible = "false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="CaseTypeID" Visible = "false"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        <table  width="100%">
            <tr>
                <td>
                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
                    ServiceMethod="GetPatient" ServicePath="AJAX Pages/PatientService.asmx" UseContextKey="true"
                    ContextKey="SZ_COMPANY_ID">
                    </ajaxToolkit:AutoCompleteExtender>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
