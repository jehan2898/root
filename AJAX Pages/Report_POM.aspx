<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Report_POM.aspx.cs" Inherits="Report_POM" Title="Green Your Bills - POM Report" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%--
<script runat="server">txtToBillDate.ClientID</script>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;
    <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="~/validation.js"></script>
     <script type="text/javascript">
        function test(pomid, pomstatus) {
            debugger;         
            if (pomstatus == 1) {
                pomstatus = "VPOMR";
            }
            else if (pomstatus == 0) {
                pomstatus = "POM";
            }         
            scanPOM(pomid, pomstatus, '4');
        }
    </script>

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
         
         
       function OpenFile(path)
       {
        window.open(path);
       }
         function SetDate()
         {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID%>").value;
            if(selValue == 0)
            {
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = "";
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('today');
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = getWeek('endweek');
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('monthend');
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('quarterend');
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = getDate('yearend');
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtToBillDate.ClientID%>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtToBillDate.ClientID%>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtFromBillDate.ClientID%>").value =  quarteryear('startquaeter');
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
            
            if(day<10 && month==10)
            {
              return ''+(month)+'/0'+day+'/'+year;
            }
            if(day>10 && month==10)
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
         
          function SetServiceDate()
         {
            getWeek();
            var selValue = document.getElementById('ctl00_ContentPlaceHolder1_ddlServiseDate').value;
            if(selValue == 0)
            {
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = "";
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = getDate('today');
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = getWeek('endweek');
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = getDate('monthend');
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = getDate('quarterend');
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = getDate('yearend');
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = getLastWeek('endweek');
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = lastmonth('endmonth');
                   
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value =lastyear('endyear');
                   
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById('ctl00_ContentPlaceHolder1_txtToServiceDate').value = quarteryear('endquaeter');
                   
                   document.getElementById('ctl00_ContentPlaceHolder1_txtFromServiceDate').value =  quarteryear('startquaeter');
            }
         }
         
          function SetPOMPrintedDate()
         {
            getWeek();
            var selValue = document.getElementById("<%=ddlPrintDate.ClientID%>").value;
            if(selValue == 0)
            {
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = "";
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = getDate('today');
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = getWeek('endweek');
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = getDate('monthend');
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                //alert("");
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = getDate('quarterend');
                  // alert(getDate('quarterstart'));
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = getDate('yearend');
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtToPrintedDate.ClientID%>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtFromPrintedDate.ClientID%>").value =  quarteryear('startquaeter');
            }
         }
         
          function SetPOMReceivedDate()
         {
            getWeek();
            var selValue = document.getElementById("<%=ddlRecDate.ClientID%>").value;
            if(selValue == 0)
            {
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = "";
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = getDate('today');
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = getWeek('endweek');
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = getDate('monthend');
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = getDate('quarterend');
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = getDate('yearend');
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtToRecDate.ClientID%>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtToRecDate.ClientID%>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtFromRecDate.ClientID%>").value =  quarteryear('startquaeter');
            }
         }
         
         
        
    </script>

    <%-- For date validation--%>

    <script type="text/javascript" language="javascript">
      function FromDateValidation()
      {
         var year1="";
         year1=document.getElementById("<%=txtFromBillDate.ClientID%>").value; 
         
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
            
           document.getElementById("<%=lblValidator1.ClientID%>").innerText = 'Date is Invalid';
          
          // document.getElementById('_ctl0_ContentPlaceHolder1_lblValidator1').style.display = "block";
         
            document.getElementById("<%=txtFromBillDate.ClientID%>").focus();
            return false; 
         }
         if((year1.charAt(6)!='_' && year1.charAt(7)!='_') || (year1.charAt(8)!='_' && year1.charAt(9)!='_') || (year1.charAt(6)!='0' && year1.charAt(7)!='0'))
         {
            document.getElementById("<%=lblValidator1.ClientID%>").innerText = '';
            return true;
         }
        
      }
      
      function ToDateValidation()
      {
         var year2="";
         year2=document.getElementById("<%=txtToBillDate.ClientID%>").value; 
         
         if(year2.charAt(0)=='_' && year2.charAt(1)=='_' && year2.charAt(2)=='/' && year2.charAt(3)=='_' && year2.charAt(4)=='_' && year2.charAt(5)=='/' && year2.charAt(6)=='_' && year2.charAt(7)=='_' && year2.charAt(8)=='_' && year2.charAt(9)=='_')
         {
             return true;   
         }
         if((year2.charAt(6)=='_' && year2.charAt(7)=='_') || (year2.charAt(8)=='_' && year2.charAt(9)=='_') || (year2.charAt(6)=='0' && year2.charAt(7)=='0') || (year2.charAt(6)=='0') || (year2.charAt(9)=='_'))
         {
            //alert("Invalide 'To Date'");
            document.getElementById("<%=lblValid1.ClientID%>").innerText = 'Date is Invalid';
            document.getElementById("<%=txtToBillDate.ClientID%>").focus();
            return false; 
         }
         if((year2.charAt(6)!='_' && year2.charAt(7)!='_') || (year2.charAt(8)!='_' && year2.charAt(9)!='_') || (year2.charAt(6)!='0' && year2.charAt(7)!='0'))
         {
            document.getElementById("<%=lblValid1.ClientID%>").innerText = '';
            return true;
         }
         
      }
      
      
      
      	function showUploadFilePopup()
       {
      
            //document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='100px';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.height='100px';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.visibility = 'visible';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.position = "absolute";
	        document.getElementById('<%=pnlUploadFile.ClientID%>').style.top = '200px';
	        document.getElementById('<%=pnlUploadFile.ClientID%>').style.left ='350px';
	        document.getElementById('<%=pnlUploadFile.ClientID%>').style.zIndex= '0';
       }
       function CloseUploadFilePopup()
       {
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.height='0px';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
       
       
       
       
        function showPOMFrame(objCaseID,objCompanyID)
        {
           var selectedvalue;
            var RB1 = document.getElementById("<%= rdbpombills.ClientID %>");
            var radio = RB1.getElementsByTagName("input");
           
             if (radio[0].checked)
             {
              selectedvalue=radio[0].value;
             }

            if (radio[1].checked)
            {
             selectedvalue=radio[1].value;
             }
         
	    // alert(objCaseID + ' ' + objCompanyID);
	        var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute'; 
            document.getElementById('divfrmPatient').style.left= '400px'; 
            document.getElementById('divfrmPatient').style.top= '350px'; 
            document.getElementById('divfrmPatient').style.visibility='visible'; 
            document.getElementById('frmpatient').src="Report_pom_popup.aspx?PomID="+objCaseID+"&cmpId="+ objCompanyID+"&selectedvalue="+selectedvalue;
            return false;   
        }
        
        function ClosePatientFramePopup()
               {
                 //   alert("");
                   //document.getElementById('divfrmPatient').style.height='0px';
                    document.getElementById('divfrmPatient').style.visibility='hidden';
                   document.getElementById('divfrmPatient').style.top='-10000px';
                    document.getElementById('divfrmPatient').style.left='-10000px';
             }
      
      
    

    </script>

    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
        <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%" border="0">
            <tr>
                <td align="center">
                </td>
            </tr>
        </table>
        <table border="0">
            <tr>
                <td>
                    <table style="border: 1px solid #B5DF82;" width="400px" border="0">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" colspan="3">
                                <b>Search Parameters</b>
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
                                        <td class="td-widget-bc-search-desc-ch3">
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
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:TextBox ID="txtFromBillDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnFromBillDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtFromBillDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:TextBox ID="txtToBillDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnToBillDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValid1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtToBillDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            Service
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            From Date
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            To Date
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:DropDownList ID="ddlServiseDate" runat="Server">
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
                                            <asp:TextBox ID="txtFromServiceDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValid3" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtFromserviceDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:TextBox ID="txtToServiceDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValid4" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtToServiceDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            Printed
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            From Date
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            To Date
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:DropDownList ID="ddlPrintDate" runat="Server">
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
                                            <asp:TextBox ID="txtFromPrintedDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValig5" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtFromPrintedDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:TextBox ID="txtToPrintedDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValid6" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtToPrintedDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            Received
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            From Date
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            To Date
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:DropDownList ID="ddlRecDate" runat="Server">
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
                                            <asp:TextBox ID="txtFromRecDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValid7" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtFromRecDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:TextBox ID="txtToRecDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValid8" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtToRecDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            Bill number
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            Patient Name
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch1">
                                            Specialty
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:TextBox ID="txtBillNo" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <asp:TextBox ID="txtPatientName" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="td-widget-bc-search-desc-ch3">
                                            <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                Selected_Text="--- Select ---" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:RadioButtonList ID="rdbpombills" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="&nbsp;Bills" Value="0" Selected="True">
                                                </asp:ListItem>
                                                <asp:ListItem Text="&nbsp;Other" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox><asp:TextBox
                                    ID="txtSort" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                <asp:TextBox ID="txtSpeciality" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                <asp:UpdatePanel ID="pnlprogress" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="updtpanel" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <table style="width: 900px; border: 1px solid #B5DF82;" align="left" border="0">
                            <tr>
                                <td>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                </td>
                            </tr>
                            <tr>
                                <td height="28" align="center" valign="middle" bgcolor="#B5DF82" align="center">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updtpanel"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div id="DivStatus2" runat="Server">
                                                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                    Height="25px" Width="24px"></asp:Image>
                                                Loading...</div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="pnlprogress"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div id="DivStatus3" runat="Server">
                                                <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                    Height="25px" Width="24px"></asp:Image>
                                                Loading...</div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="vertical-align: middle; width: 900px;">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: middle; width: 50%" align="left">
                                                    Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                        CssClass="search-input">
                                                    </gridsearch:XGridSearchTextBox>
                                                </td>
                                                <td style="vertical-align: middle; width: 50%; text-align: right" align="right" colspan="2">
                                                    Record Count:<%= this.grdPomReport.RecordCount %>
                                                    | Page Count:
                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                    </gridpagination:XGridPaginationDropDown>
                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                        Text="Export TO Excel">
                                             <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="width: 900px">
                                        <tr>
                                            <td>
                                                <xgrid:XGridViewControl ID="grdPomReport" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                    MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true"
                                                    EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                    ExportToExcelColumnNames="POMID,POM Generated Date,POM Received Date" ExportToExcelFields="POM_ID,POM_GENERATED_DATE,DT_RECEIVED_DATE"
                                                    AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" 
                                                    PageRowCount="50" PagerStyle-CssClass="pgr" DataKeyNames="PATH,POM_ID,I_VERIFICATION_POM"
                                                    AllowSorting="true">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="POM ID" SortExpression="convert(int,I_POM_ID)">
                                                            <itemtemplate>
                                                                              <a id="lnkframePatient" href="#" onclick='<%# "showPOMFrame(" + "\""+ Eval("POM_ID") + "\");" %>' ><%# DataBinder.Eval(Container,"DataItem.POM_ID")%></a>
                                                                         </itemtemplate>
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="left" width="20%"></itemstyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="POM_GENERATED_DATE" HeaderText="Generated Date" SortExpression="DT_POM_DATE"
                                                            DataFormatString="{0:MM/dd/yyyy}">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="POM">
                                                            <itemtemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.PATH")%>
                                                                        </itemtemplate>
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="center" width="10%"></itemstyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Scan/Upload Received POM">
                                                            <itemtemplate>
                                                                 <a id="caseDetailScan" href="#" runat="server"  onclick='<%# "test(" + "\""+ Eval("POM_ID")+"\""+",\"" + Eval("I_VERIFICATION_POM") +"\""+ ");"%>'
                                                                          title="Scan/Upload" >Scan / Upload</a>
 								                            	            
                                                                        </itemtemplate>
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="center" width="20%"></itemstyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Received POM">
                                                            <itemtemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.RECEIVED_PATH")%>
                                                                        </itemtemplate>
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="center" width="10%"></itemstyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DT_RECEIVED_DATE" HeaderText="Receive Date" DataFormatString="{0:MM/dd/yyyy}"
                                                            SortExpression="DT_RECEIVED_DATE">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="POM ID" DataField="POM_ID" Visible="false" />
                                                        <asp:BoundField HeaderText="pomstatus" DataField="I_VERIFICATION_POM" Visible="false" />
                                                        <asp:BoundField HeaderText="Path" DataField="PATH" Visible="false" />
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <ajaxToolkit:CalendarExtender ID="calExtFromDateofService" runat="server" TargetControlID="txtFromserviceDate"
            PopupButtonID="ImageButton1" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromserviceDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtToDateofService" runat="server" TargetControlID="txtToServiceDate"
            PopupButtonID="ImageButton2" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToServiceDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtToPOMPrintDate" runat="server" TargetControlID="txtToPrintedDate"
            PopupButtonID="ImageButton3" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToPrintedDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtFromBillDate" runat="server" TargetControlID="txtFromBillDate"
            PopupButtonID="imgbtnFromBillDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromBillDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtToBillDate" runat="server" TargetControlID="txtToBillDate"
            PopupButtonID="imgbtnToBillDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToBillDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtFromPOMRecDate" runat="server" TargetControlID="txtFromRecDate"
            PopupButtonID="ImageButton5" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromRecDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="clExtToPOMRecDate" runat="server" TargetControlID="txtToRecDate"
            PopupButtonID="ImageButton6" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToRecDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtFromPOMPrintDate" runat="server" TargetControlID="txtFromPrintedDate"
            PopupButtonID="ImageButton4" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromPrintedDate" PromptCharacter="_" AutoComplete="true">
        </ajaxToolkit:MaskedEditExtender>
        </td> </tr> </table>
    </div>
    <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: white;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseUploadFilePopup();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 98%;" valign="top">
                    <table border="0" class="ContentTable" style="width: 100%">
                        <tr>
                            <td>
                                Upload Report :
                            </td>
                            <td>
                                <asp:FileUpload ID="fuUploadReport" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" OnClick="btnUploadFile_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <contenttemplate>
             <DIV style="DISPLAY: none">
                <asp:LinkButton id="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana" >
                </asp:LinkButton>
             </DIV>
            <ajaxToolkit:ModalPopupExtender id="ModalPopupExtender1" runat="server" TargetControlID="lbn_job_det" PopupDragHandleControlID="pnlMakePayment" PopupControlID="pnlMakePayment">
        </ajaxToolkit:ModalPopupExtender>
         <asp:Panel style="DISPLAY: none" id="pnlMakePayment" runat="server" Height="200px" Width="700px" BackColor="white">
            <DIV style="TEXT-ALIGN: center">
                    <DIV style="TEXT-ALIGN: right">
                        <asp:Button id="btnClose" onclick="btnClose_Click" runat="server" Width="10%" CssClass="Buttons" Text="Close"></asp:Button>
                    </DIV>
                   
                    <asp:DataGrid id="grdPaymentTransaction" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="GridTable" >  <FooterStyle />
                        <SelectedItemStyle />
                        <PagerStyle />
                        <AlternatingItemStyle />
                        <ItemStyle CssClass="GridRow" />
                         <Columns>
                             <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill No" Visible="True"></asp:BoundColumn>
                             <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                             <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No" ></asp:BoundColumn>
                             <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid> <%--<input type="hidden"id ="hiddentxtchkamt" runat = "server" value="" />--%>
            </div>
     
    </asp:Panel> 
            <asp:HiddenField ID="hdnCaseId" runat="server" />
</contenttemplate>
    </asp:UpdatePanel>
    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
        visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
        position: absolute; top: 682px; height: 100px; background-color: #B5DF82" id="divfrmPatient">
        <div style="position: relative; background-color: #B5DF82; text-align: right">
            <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
        </div>
        <iframe id="frmpatient" src="" frameborder="0" width="500" height="300"></iframe>
    </div>
    <asp:TextBox ID="utxtCompanyID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtFromBillDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtToBillDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtFromServiceDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtToServiceDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtFromPrintedDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtToPrintedDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtFromRecDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtToRecDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtBillNo" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtPatientName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtSpeciality" runat="server" Visible="false"></asp:TextBox>
     <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
</div>
</asp:Content>
