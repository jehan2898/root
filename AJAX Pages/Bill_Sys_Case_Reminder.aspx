<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Case_Reminder.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Case_Reminder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

function MakeVisibleRecurrence()
        {
            var f= document.getElementById('chkRecurrence');
            
            if(f.checked == true)
            {
                document.getElementById('trReoccurance').style.visibility="visible";
                document.getElementById('rbtnDaily').checked = 1;
                document.getElementById('rbtnWeekly').checked = 0;
                document.getElementById('rbtnMonthly').checked = 0;
                document.getElementById('pnlRecurrence').style.display="block";	
                document.getElementById('pnlDaily').style.display="block";
                document.getElementById('rbtnDEvery').checked = 1;
                document.getElementById('rbtnDEveryWeekday').checked = 0;
                document.getElementById('txtDDays').value = "1";
                document.getElementById('rbtnDEvery').style.display="block";
                document.getElementById('txtDDays').style.display="block";	
                document.getElementById('lblDDays').style.display="block";
                document.getElementById('rbtnDEveryWeekday').style.display="block"; 
                document.getElementById('lblDEvery').style.display="block";
                document.getElementById('lblDEveryweekday').style.display="block"; 
                document.getElementById('txtWRecur').value = "1";
                document.getElementById('chkWSunday').checked = 0;
                document.getElementById('chkWMonday').checked = 0;
                document.getElementById('chkWTuesday').checked = 0;
                document.getElementById('chkWWednesday').checked = 0;
                document.getElementById('chkWThursday').checked = 0;
                document.getElementById('chkWFriday').checked = 0;
                document.getElementById('chkWSaturday').checked = 0;                
                document.getElementById('lblWRecurEvery').style.display="none";	
                document.getElementById('txtWRecur').style.display="none";
                document.getElementById('lblWWeeksOn').style.display="none";
                document.getElementById('chkWSunday').style.display="none";	
                document.getElementById('chkWMonday').style.display="none";
                document.getElementById('chkWTuesday').style.display="none";
                document.getElementById('chkWWednesday').style.display="none";
                document.getElementById('chkWThursday').style.display="none";		
                document.getElementById('chkWFriday').style.display="none";
                document.getElementById('chkWSaturday').style.display="none";	                
                document.getElementById('lblWSunday').style.display="none";	
                document.getElementById('lblWMonday').style.display="none";
                document.getElementById('lblWTuesday').style.display="none";
                document.getElementById('lblWWednesday').style.display="none";	
                document.getElementById('lblWThursday').style.display="none";
                document.getElementById('lblWFriday').style.display="none";
                document.getElementById('lblWSaturday').style.display="none";	
                document.getElementById('rbtnMDay').checked = 1;
                document.getElementById('rbtnMThe').checked = 0;
                var now = new Date();                
                document.getElementById('txtMDay').value = now.getDate();
                document.getElementById('txtMMonths').value = "1";
                document.getElementById('drpMTerm').selectedIndex = 0;
                document.getElementById('drpMDayRecur').selectedIndex = 0;
                document.getElementById('txtMEveryMonths').value = "1";
                document.getElementById('rbtnMDay').style.display="none";
                document.getElementById('txtMDay').style.display="none";
                document.getElementById('lblMofEvery').style.display="none";	
                document.getElementById('txtMMonths').style.display="none";
                document.getElementById('lblMMonths').style.display="none";
                document.getElementById('rbtnMThe').style.display="none";	
                document.getElementById('drpMTerm').style.display="none";                
                document.getElementById('drpMDayRecur').style.display="none";	
                document.getElementById('lblMEvery').style.display="none";
                document.getElementById('txtMEveryMonths').style.display="none";
                document.getElementById('lblMEveryMonths').style.display="none";	
                document.getElementById('lblMDay').style.display="none";
                document.getElementById('lblMThe').style.display="none";	
                document.getElementById('rbtnNoEndDate').checked = 1;
                document.getElementById('rbtnEndAfter').checked = 0;
                document.getElementById('rbtnEndBy').checked = 0;	
                document.getElementById('txtEndAfter').value = "";	                
                document.getElementById('txtEndByDate').value = now.getMonth()+1+"/"+now.getDate()+"/"+now.getFullYear();
            }
            else
            {            
                     
                document.getElementById('rbtnDEvery').checked = 1;
                document.getElementById('rbtnDEveryWeekday').checked = 0;
                document.getElementById('txtDDays').value = "1";
                document.getElementById('txtWRecur').value = "1";
                document.getElementById('chkWSunday').checked = 0;
                document.getElementById('chkWMonday').checked = 0;
                document.getElementById('chkWTuesday').checked = 0;
                document.getElementById('chkWWednesday').checked = 0;
                document.getElementById('chkWThursday').checked = 0;
                document.getElementById('chkWFriday' ).checked = 0;
                document.getElementById('chkWSaturday').checked = 0;  
                document.getElementById('rbtnMDay').checked = 1;
                
                var now = new Date();                
                document.getElementById('txtMDay').value = now.getDate();
                document.getElementById('txtMMonths').value = "1";
                document.getElementById('drpMTerm').selectedIndex = 0;
                document.getElementById('drpMDayRecur').selectedIndex = 0;
                document.getElementById('txtMEveryMonths').value = "1";
                document.getElementById('rbtnNoEndDate').checked = 1;
                document.getElementById('rbtnEndAfter').checked = 0;
                document.getElementById('rbtnEndBy').checked = 0;	
                document.getElementById('txtEndAfter').value = "";	                
                document.getElementById('txtEndByDate').value = now.getMonth()+1+"/"+now.getDate()+"/"+now.getFullYear();
                document.getElementById('rbtnDaily').checked = 1;
                document.getElementById('rbtnWeekly').checked = 0;
                document.getElementById('rbtnMonthly').checked = 0;
                document.getElementById('pnlRecurrence').style.display="none";
                document.getElementById('pnlDaily').style.display="none";
                document.getElementById('pnlWeekly').style.display="none";
                document.getElementById('pnlMonthly').style.display="none";
                document.getElementById('rbtnDEvery').style.display="none";
                document.getElementById('txtDDays').style.display="none";	
                document.getElementById('lblDDays').style.display="none";
                document.getElementById('rbtnDEveryWeekday').style.display="none";   
                document.getElementById('lblDEvery').style.display="none";
                document.getElementById('lblDEveryweekday').style.display="none"; 
                document.getElementById('lblWRecurEvery').style.display="none";	
                document.getElementById('txtWRecur').style.display="none";
                document.getElementById('lblWWeeksOn').style.display="none";
                document.getElementById('chkWSunday').style.display="none";	
                document.getElementById('chkWMonday').style.display="none";
                document.getElementById('chkWTuesday').style.display="none";
                document.getElementById('chkWWednesday').style.display="none";	
                document.getElementById('chkWThursday').style.display="none";
                document.getElementById('chkWFriday').style.display="none";
                document.getElementById('chkWSaturday').style.display="none";	
                document.getElementById('lblWSunday').style.display="none";	
                document.getElementById('lblWMonday').style.display="none";
                document.getElementById('lblWTuesday').style.display="none";
                document.getElementById('lblWWednesday').style.display="none";	
                document.getElementById('lblWThursday').style.display="none";
                document.getElementById('lblWFriday').style.display="none";
                document.getElementById('lblWSaturday').style.display="none";	
                document.getElementById('rbtnMDay').style.display="none";
                document.getElementById('txtMDay').style.display="none";
                document.getElementById('lblMofEvery').style.display="none";	
                document.getElementById('txtMMonths').style.display="none";
                document.getElementById('lblMMonths').style.display="none";
                document.getElementById('rbtnMThe').style.display="none";	
                document.getElementById('drpMTerm').style.display="none";                
                document.getElementById('drpMDayRecur').style.display="none";	
                document.getElementById('lblMEvery').style.display="none";
                document.getElementById('txtMEveryMonths').style.display="none";
                document.getElementById('lblMEveryMonths').style.display="none";
                document.getElementById('lblMDay').style.display="none";
                document.getElementById('lblMThe').style.display="none";		
            }   
        }   
        
       function btnsave()    
        {
            
        }
        
        function ReminderTypeBasedVisible()
        {
             var d= document.getElementById('rbtnDaily');
             var w= document.getElementById('rbtnWeekly');
             var m= document.getElementById('rbtnMonthly');
            
             if(d.checked == true)
             {                
                document.getElementById('pnlDaily').style.display="block";	
                document.getElementById('pnlWeekly').style.display="none";
                document.getElementById('pnlMonthly').style.display="none";	
                document.getElementById('rbtnDEvery').style.display="block";
                document.getElementById('txtDDays').style.display="block";	
                document.getElementById('lblDDays').style.display="block";
                document.getElementById('rbtnDEveryWeekday').style.display="block";   
                document.getElementById('lblDEvery').style.display="block";
                document.getElementById('lblDEveryweekday').style.display="block"; 
                document.getElementById('lblWRecurEvery').style.display="none";	
                document.getElementById('txtWRecur').style.display="none";
                document.getElementById('lblWWeeksOn').style.display="none";
                document.getElementById('chkWSunday').style.display="none";	
                document.getElementById('chkWMonday').style.display="none";
                document.getElementById('chkWTuesday').style.display="none";
                document.getElementById('chkWWednesday').style.display="none";	
                document.getElementById('chkWThursday').style.display="none";
                document.getElementById('chkWFriday').style.display="none";
                document.getElementById('chkWSaturday').style.display="none";	
                document.getElementById('lblWSunday').style.display="none";	
                document.getElementById('lblWMonday').style.display="none";
                document.getElementById('lblWTuesday').style.display="none";
                document.getElementById('lblWWednesday').style.display="none";	
                document.getElementById('lblWThursday').style.display="none";
                document.getElementById('lblWFriday').style.display="none";
                document.getElementById('lblWSaturday').style.display="none";	
                document.getElementById('rbtnMDay').style.display="none";
                document.getElementById('txtMDay').style.display="none";
                document.getElementById('lblMofEvery').style.display="none";	
                document.getElementById('txtMMonths').style.display="none";
                document.getElementById('lblMMonths').style.display="none";
                document.getElementById('rbtnMThe').style.display="none";	
                document.getElementById('drpMTerm').style.display="none";                
                document.getElementById('drpMDayRecur').style.display="none";	
                document.getElementById('lblMEvery').style.display="none";
                document.getElementById('txtMEveryMonths').style.display="none";
                document.getElementById('lblMEveryMonths').style.display="none";	
                document.getElementById('lblMDay').style.display="none";
                document.getElementById('lblMThe').style.display="none";	
             }             
             
             if(w.checked == true)
             {
                document.getElementById('pnlWeekly').style.display="block";	
                document.getElementById('pnlDaily').style.display="none";                
                document.getElementById('pnlMonthly').style.display="none";	
                document.getElementById('rbtnDEvery').style.display="none";
                document.getElementById('txtDDays').style.display="none";	
                document.getElementById('lblDDays').style.display="none";
                document.getElementById('rbtnDEveryWeekday').style.display="none";  
                document.getElementById('lblDEvery').style.display="none";
                document.getElementById('lblDEveryweekday').style.display="none";  
                document.getElementById('lblWRecurEvery').style.display="block";	
                document.getElementById('txtWRecur').style.display="block";
                document.getElementById('lblWWeeksOn').style.display="block";
                document.getElementById('chkWSunday').style.display="block";	
                document.getElementById('chkWMonday').style.display="block";
                document.getElementById('chkWTuesday').style.display="block";
                document.getElementById('chkWWednesday').style.display="block";	
                document.getElementById('chkWThursday').style.display="block";
                document.getElementById('chkWFriday').style.display="block";
                document.getElementById('chkWSaturday').style.display="block";
                document.getElementById('lblWSunday').style.display="block";	
                document.getElementById('lblWMonday').style.display="block";
                document.getElementById('lblWTuesday').style.display="block";
                document.getElementById('lblWWednesday').style.display="block";	
                document.getElementById('lblWThursday').style.display="block";
                document.getElementById('lblWFriday').style.display="block";
                document.getElementById('lblWSaturday').style.display="block";	
                
                var nowdate = new Date();
                var day = nowdate.getDay();                
                if(day == 0)
                {
                    document.getElementById('chkWSunday').checked = true;                                    
                }
                
                else if(day == 1)
                {                    
                    document.getElementById('chkWMonday').checked = true;                    
                }
                else if(day == 2)
                {                   
                    document.getElementById('chkWTuesday').checked = true;                 
                }
                else if(day == 3)
                {                   
                    document.getElementById('chkWWednesday').checked = true;                  
                }
                else if(day == 4)
                {                 
                    document.getElementById('chkWThursday').checked = true;                  
                }
                else if(day == 5)
                {
                    document.getElementById('chkWFriday').checked = true;                
                }
                else if(day == 6)
                {                  
                    document.getElementById('chkWSaturday').checked = true;  
                }
                
                document.getElementById('rbtnMDay').style.display="none";
                document.getElementById('txtMDay').style.display="none";
                document.getElementById('lblMofEvery').style.display="none";	
                document.getElementById('txtMMonths').style.display="none";
                document.getElementById('lblMMonths').style.display="none";
                document.getElementById('rbtnMThe').style.display="none";	
                document.getElementById('drpMTerm').style.display="none";                
                document.getElementById('drpMDayRecur').style.display="none";	
                document.getElementById('lblMEvery').style.display="none";
                document.getElementById('txtMEveryMonths').style.display="none";
                document.getElementById('lblMEveryMonths').style.display="none";	
                document.getElementById('lblMDay').style.display="none";
                document.getElementById('lblMThe').style.display="none";	

             }            
             
             if(m.checked == true)
             {
                document.getElementById('pnlMonthly').style.display="block";	                
                document.getElementById('pnlWeekly').style.display="none";	
                document.getElementById('pnlDaily').style.display="none";
                document.getElementById('rbtnDEvery').style.display="none";
                document.getElementById('txtDDays').style.display="none";	
                document.getElementById('lblDDays').style.display="none";
                document.getElementById('rbtnDEveryWeekday').style.display="none";   
                document.getElementById('lblDEvery').style.display="none";
                document.getElementById('lblDEveryweekday').style.display="none";
                document.getElementById('lblWRecurEvery').style.display="none";	
                document.getElementById('txtWRecur').style.display="none";
                document.getElementById('lblWWeeksOn').style.display="none";
                document.getElementById('chkWSunday').style.display="none";	
                document.getElementById('chkWMonday').style.display="none";
                document.getElementById('chkWTuesday').style.display="none";
                document.getElementById('chkWWednesday').style.display="none";	
                document.getElementById('chkWThursday').style.display="none";
                document.getElementById('chkWFriday').style.display="none";
                document.getElementById('chkWSaturday').style.display="none";	
                document.getElementById('lblWSunday').style.display="none";	
                document.getElementById('lblWMonday').style.display="none";
                document.getElementById('lblWTuesday').style.display="none";
                document.getElementById('lblWWednesday').style.display="none";	
                document.getElementById('lblWThursday').style.display="none";
                document.getElementById('lblWFriday').style.display="none";
                document.getElementById('lblWSaturday').style.display="none";	
                document.getElementById('rbtnMDay').style.display="block";
                document.getElementById('txtMDay').style.display="block";
                document.getElementById('lblMofEvery').style.display="block";	
                document.getElementById('txtMMonths').style.display="block";
                document.getElementById('lblMMonths').style.display="block";
                document.getElementById('rbtnMThe').style.display="block";	
                document.getElementById('drpMTerm').style.display="block";                
                document.getElementById('drpMDayRecur').style.display="block";	
                document.getElementById('lblMEvery').style.display="block";
                document.getElementById('txtMEveryMonths').style.display="block";
                document.getElementById('lblMEveryMonths').style.display="block";	
                document.getElementById('lblMDay').style.display="block";
                document.getElementById('lblMThe').style.display="block";
                                          
             }
        }
        
         function validateDate(dtControl)
        {     
            var val = document.getElementById(dtControl).value;       
            
            if(val == "__/__/____")
            { 
                return true;
            }
            
            if(val.length == 0)
            {  
                return true;
            }
                
            if(val.length == 10)
            {
                var splits = val.split("/");
                var dt = new Date(splits[0] + "/" + splits[1] + "/" + splits[2]);
                                              
                //Validation for Dates
                if(dt.getDate()== splits[1] && dt.getMonth()+1== splits[0] && dt.getFullYear()== splits[2])
                {      
                   return true;                          
                }
                else
                {   
                    document.getElementById(dtControl).value = '';
                   document.getElementById(dtControl).focus();
                   alert('Invalid Day, Month, or Year range detected. Please correct.')                                  
                   return false;
                } 
            }            
        }
        
        function ClearValues()
        {     
                 
                 
            var now1 = new Date();                  
            document.getElementById('txtStartDate').value =  now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
            document.getElementById('extddlRType').value ='NA';
            document.getElementById('txtReminderDesc').value = "";             
            document.getElementById('lsbAssignTo').selectedIndex = 0;                
            document.getElementById('txtEndByDate').value = now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
            document.getElementById('chkRecurrence').checked = 0;                
            document.getElementById('rbtnDaily').checked = 1;
            document.getElementById('rbtnWeekly').checked = 0;
            document.getElementById('rbtnMonthly').checked = 0;
            document.getElementById('chkRecurrence').checked = 0;
            document.getElementById('pnlRecurrence').style.display="none";
            document.getElementById('pnlDaily').style.display="none";
            document.getElementById('pnlWeekly').style.display="none";
            document.getElementById('pnlMonthly').style.display="none";
            document.getElementById('rbtnDEvery').style.display="none";
            document.getElementById('txtDDays').style.display="none";	
            document.getElementById('lblDDays').style.display="none";
            document.getElementById('rbtnDEveryWeekday').style.display="none";   
            document.getElementById('lblDEvery').style.display="none";
            document.getElementById('lblDEveryweekday').style.display="none"; 
            document.getElementById('lblWRecurEvery').style.display="none";	
            document.getElementById('txtWRecur').style.display="none";
            document.getElementById('lblWWeeksOn').style.display="none";
            document.getElementById('chkWSunday').style.display="none";	
            document.getElementById('chkWMonday').style.display="none";
            document.getElementById('chkWTuesday').style.display="none";
            document.getElementById('chkWWednesday').style.display="none";	
            document.getElementById('chkWThursday').style.display="none";
            document.getElementById('chkWFriday').style.display="none";
            document.getElementById('chkWSaturday').style.display="none";	
            document.getElementById('lblWSunday').style.display="none";	
            document.getElementById('lblWMonday').style.display="none";
            document.getElementById('lblWTuesday').style.display="none";
            document.getElementById('lblWWednesday').style.display="none";	
            document.getElementById('lblWThursday').style.display="none";
            document.getElementById('lblWFriday').style.display="none";
            document.getElementById('lblWSaturday').style.display="none";	
            document.getElementById('rbtnMDay').style.display="none";
            document.getElementById('txtMDay').style.display="none";
            document.getElementById('lblMofEvery').style.display="none";	
            document.getElementById('txtMMonths').style.display="none";
            document.getElementById('lblMMonths').style.display="none";
            document.getElementById('rbtnMThe').style.display="none";	
            document.getElementById('drpMTerm').style.display="none";                
            document.getElementById('drpMDayRecur').style.display="none";	
            document.getElementById('lblMEvery').style.display="none";
            document.getElementById('txtMEveryMonths').style.display="none";
            document.getElementById('lblMEveryMonths').style.display="none";
            document.getElementById('lblMDay').style.display="none";
            document.getElementById('lblMThe').style.display="none";		
        }
    
        function ConfirmDelete()
        {
             var msg = "The court you selected to delete are already associated with cases in the system. Deleting them will erase the court information for all the above cases. Do you want to proceed?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
        }
        
       function allownumbers(e)
       {  
            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);
            var reg = new RegExp("[0-9.]")
            if (key == 8)
            {
                keychar = String.fromCharCode(key);
            }
            if (key == 13)
            {
                 key=8;
                 keychar = String.fromCharCode(key);     
            }
            return reg.test(keychar);            
       } 
       
       function DayValidation(e)
       {
            var now3 = new Date();
            var n = e.value;          
            
            var month = document.getElementById('drpYMonth').selectedIndex;	
            if(month==1)
            {
                if(n != "")
                { 
                    var year = now3.getFullYear();
                    if(((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    {
                        if(n<1 || n >29)
                        { 
                            e.value = now3.getDate();                 
                            alert("Please enter number between 1 and 29 for this Month.");  
                        }
                    }
                    else
                    {
                        if(n<1 || n >28)
                        { 
                            e.value = now3.getDate();                 
                            alert("Please enter number between 1 and 28 for this Month.");  
                        }
                    }
                }
            }
            else
            {         
                if(n != "")
                {                
                    if(n<1 || n >31)
                    {                     
                        e.value = now3.getDate();                 
                        alert("Please enter number between 1 and 31.");    
                    }   
                }
                else
                {
                    e.value = now3.getDate(); 
                } 
            }
       }
       function NewDayValidation()
       {
           var now4 = new Date();
           var n = document.getElementById('txtYDay').value;	
           var month = document.getElementById('drpYMonth').selectedIndex;	
            if(month ==1)
            {
                if(n != "")
                { 
                    var year = now4.getFullYear();
                    if(((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    {
                        if(n<1 || n >29)
                        { 
                            document.getElementById('txtYDay').value = now4.getDate();                 
                            alert("Please enter number between 1 and 29 for this Month.");  
                        }
                    }
                    else
                    {
                        if(n<1 || n >28)
                        { 
                            document.getElementById('txtYDay').value = now4.getDate();                 
                            alert("Please enter number between 1 and 28 for this Month.");  
                        }
                    }
                }
            }
            else
            {         
                if(n != "")
                {                
                    if(n<1 || n >31)
                    {                     
                        document.getElementById('txtYDay').value = now4.getDate();                 
                        alert("Please enter number between 1 and 31.");    
                    }   
                }
                else
                {
                    document.getElementById('txtYDay').value = now4.getDate(); 
                } 
            }                
       }
       
       
       
       
       function ZeroNotAllowed(e)
       {
       
           if(document.getElementById('txtEndAfter').value == "0")
           {
                document.getElementById('txtEndAfter').value = "";
           }
           else
           {
               if(e.value == "0")
               {
                    e.value = "1";
                    return;    
               }
           }
           
           
       
            
         }
     
     
     function Validate()
     {
        var Note = document.getElementById('txtReminderDesc').value;	
        var Date = document.getElementById('txtStartDate').value;
        if(Note=='' || Date=='' )
        {
        alert("Please Enter the value for Note And Date");
          return false;
          
        }else
        {
         return true;
        }	
        
        
     }
     
    function ReminderType()
     {
        var Reminderttpe = document.getElementById('extddlRType').value;	
       
        if(Reminderttpe=='NA')
        {
        alert("Please Select Reminder Type");
          return false;
          
        }else
        {
         return true;
        }	
        
        
     }
     
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;"
                runat="server">
                <tr>
                    <td style="height: 28px" bgcolor="#b5df82" class="txt2" align="center">
                        <asp:Label ID="lblHeader" runat="server" Font-Size="Medium" Font-Names="Verdana"
                            Text="Set Reminder" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; height: 100%; background-color: White;" border="0">
                <tr>
                    <td align="left" valign="top" style="width: 50%;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%; height: 100%;
                                    padding-top: 6px; background-color: White;">
                                    <tbody>
                                        <tr>
                                            <td style="padding-left: 10px; padding-top: 6px;" align="left" valign="top">
                                                <asp:Label ID="Label2" runat="server" Text="Reminder Type :" Font-Size="Small" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="padding-bottom: 10px; padding-top: 3px;" valign="top">
                                                <extddl:ExtendedDropDownList ID="extddlRType" runat="server" Width="150px" Connection_Key="Connection_String"
                                                    Flag_Key_Value="LIST" Procedure_Name="SP_MST_REMINDER_TYPE" Selected_Text="---Select---" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 10px;" align="left">
                                                <asp:Label ID="lblnotedesc" runat="server" Text="Note Description :" Font-Size="Small"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="padding-bottom: 10px;">
                                                <asp:TextBox ID="txtReminderDesc" runat="server" Height="120px" Font-Size="12px"
                                                    CssClass="DesText" Font-Names="Verdana" Width="360px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 10px; height: 108px;" align="left">
                                                <asp:Label ID="lblassignto" runat="server" Text="Assign To :" Font-Size="Small" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:ListBox ID="lsbAssignTo" Width="363px" Height="120px" runat="server" SelectionMode="Multiple">
                                                </asp:ListBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" valign="top" style="width: 50%;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%; background-color: White; height: 100%;" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <tr>
                                        <td>
                                            <table style="width: 40%; padding-top: 6px;" border="0">
                                                <tr>
                                                    <td align="left" style="padding-top: 3px;" valign="top">
                                                        <asp:Label ID="Label1" runat="server" Text="Start Date * : " Font-Size="Small" Font-Bold="true"></asp:Label>
                                                        <span class="Mandatory"></span>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="100px" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                        <ajaxcontrol:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtStartDate"
                                                            PopupButtonID="imgbtnStartDate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" align="left" height="30" colspan="2">
                                                        <asp:CheckBox ID="chkRecurrence" runat="server" Text="Re Occurrence" Font-Size="Small"
                                                            Font-Bold="true"></asp:CheckBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%; background-color: White;">
                                                <tr runat="server" style="visibility: hidden" id="trReoccurance">
                                                    <td valign="top" align="left">
                                                        <asp:Panel ID="pnlRecurrence" runat="server">
                                                            <table style="width: 99%; height: 100%;" cellspacing="0" cellpadding="0" border="0"
                                                                runat="server" id="Second">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left" class="TDPart">
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" height="30">
                                                                                            <asp:RadioButton ID="rbtnDaily" onclick="ReminderTypeBasedVisible()" runat="server"
                                                                                                Text="Daily" GroupName="RECURRENCE" Checked="True" Font-Size="Small" Font-Bold="true">
                                                                                            </asp:RadioButton>
                                                                                        </td>
                                                                                        <td style="padding-left: 30px" valign="middle" align="left" rowspan="4">
                                                                                            <asp:Panel ID="pnlDaily" runat="server">
                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td valign="top" align="left">
                                                                                                                                <asp:RadioButton ID="rbtnDEvery" runat="server" GroupName="RECURRENCE_DAILY" Checked="True"
                                                                                                                                    Font-Size="Small" Font-Bold="true"></asp:RadioButton></td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblDEvery" runat="server" Text="Every" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtDDays" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblDDays" runat="server" Text="Days" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td valign="top" align="left" colspan="4" height="20">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td valign="top" align="left" width="25%">
                                                                                                                                <asp:RadioButton ID="rbtnDEveryWeekday" runat="server" GroupName="RECURRENCE_DAILY"
                                                                                                                                    Font-Size="Small" Font-Bold="true"></asp:RadioButton></td>
                                                                                                                            <td align="left" colspan="3">
                                                                                                                                <asp:Label ID="lblDEveryweekday" runat="server" Text="Every weekday" Font-Size="Small"
                                                                                                                                    Font-Bold="true"></asp:Label>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlWeekly" runat="server">
                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align="left" colspan="8" height="20">
                                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblWRecurEvery" runat="server" Text="Recur every" Font-Size="Small"
                                                                                                                                    Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtWRecur" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblWWeeksOn" runat="server" Text="week(s) on " Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" colspan="8" height="20">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWSunday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWSunday" runat="server" Text="Sunday" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWMonday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWMonday" runat="server" Text="Monday" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWTuesday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWTuesday" runat="server" Text="Tuesday" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWWednesday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWWednesday" runat="server" Text="Wednesday" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" colspan="8" height="20">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWThursday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWThursday" runat="server" Text="Thursday" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWFriday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWFriday" runat="server" Text="Friday" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWSaturday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left" colspan="3">
                                                                                                                <asp:Label ID="lblWSaturday" runat="server" Text="Saturday" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlMonthly" runat="server">
                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td align="left">
                                                                                                                                <asp:RadioButton ID="rbtnMDay" runat="server" GroupName="RECURRENCE_MONTHLY" Checked="True"
                                                                                                                                    Font-Size="Small"></asp:RadioButton></td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblMDay" runat="server" Text="Day" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtMDay" runat="server" Width="29px" MaxLength="2" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMofEvery" runat="server" Text="of every" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtMMonths" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMMonths" runat="server" Text="month(s)" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td height="20">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td align="left">
                                                                                                                                <asp:RadioButton ID="rbtnMThe" runat="server" GroupName="RECURRENCE_MONTHLY" Font-Size="Small">
                                                                                                                                </asp:RadioButton></td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblMThe" runat="server" Text="The" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpMTerm" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpMDayRecur" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMEvery" runat="server" Text="of every" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtMEveryMonths" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMEveryMonths" runat="server" Text="month(s)" Font-Size="Small"
                                                                                                                                    Font-Bold="true"></asp:Label></td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlYearly" runat="server" Visible="False">
                                                                                                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td align="left">
                                                                                                                                <asp:RadioButton ID="rbtnYEvery" runat="server" GroupName="RECURRENCE_YEARLY" Checked="True">
                                                                                                                                </asp:RadioButton></td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblYDay" runat="server" Text="Day" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpYMonth" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtYDay" runat="server" Width="29px" MaxLength="2" CssClass="textbox"></asp:TextBox></td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td height="20">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:RadioButton ID="rbtnYThe" runat="server" GroupName="RECURRENCE_YEARLY"></asp:RadioButton>
                                                                                                                            </td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblYThe" runat="server" Text="The" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpYTerm" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpYDayRecur" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblYof" runat="server" Text="of" Font-Size="Small" Font-Bold="true"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpYMonthRecur" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" height="30">
                                                                                            <asp:RadioButton ID="rbtnWeekly" onclick="ReminderTypeBasedVisible()" runat="server"
                                                                                                Text="Weekly" GroupName="RECURRENCE" Font-Size="Small" Font-Bold="true"></asp:RadioButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" height="30">
                                                                                            <asp:RadioButton ID="rbtnMonthly" onclick="ReminderTypeBasedVisible()" runat="server"
                                                                                                Text="Monthly" GroupName="RECURRENCE" Font-Size="Small" Font-Bold="true"></asp:RadioButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" height="30">
                                                                                            <asp:RadioButton ID="rbtnYearly" onclick="ReminderTypeBasedVisible()" runat="server"
                                                                                                Text="Yearly" Visible="False" GroupName="RECURRENCE" Font-Size="Small" Font-Bold="true">
                                                                                            </asp:RadioButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" colspan="2" height="75">
                                                                                            <asp:RadioButton ID="rbtnNoEndDate" runat="server" Text="No End Date" GroupName="END_DATE"
                                                                                                Checked="True" Font-Size="Small" Font-Bold="true"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="rbtnEndAfter" runat="server" Text="End After" GroupName="END_DATE"
                                                                                                Style="display: none" Font-Size="Small" Font-Bold="true"></asp:RadioButton>
                                                                                            <asp:TextBox ID="txtEndAfter" runat="server" Width="29px" MaxLength="3" CssClass="textbox"
                                                                                                Style="display: none">
                                                                                            </asp:TextBox>&nbsp;
                                                                                            <asp:Label ID="lblOccurrence" runat="server" Text="Occurrences" Style="display: none"
                                                                                                Font-Size="Small" Font-Bold="true"></asp:Label>
                                                                                            &nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="rbtnEndBy" runat="server" Text="End by" GroupName="END_DATE"
                                                                                                Font-Size="Small" Font-Bold="true"></asp:RadioButton>&nbsp;
                                                                                            <asp:TextBox ID="txtEndByDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                                            &nbsp;
                                                                                            <asp:ImageButton ID="ImgEndByDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                                        </td>
                                                                                        <ajaxcontrol:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndByDate"
                                                                                            PopupButtonID="ImgEndByDate" />
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtReminderID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                                            AcceptNegative="Left" AutoComplete="false" CultureName="en-US" DisplayMoney="Left"
                                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true">
                                                        </ajaxcontrol:MaskedEditExtender>
                                                        <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndByDate"
                                                            AcceptNegative="Left" AutoComplete="false" CultureName="en-US" DisplayMoney="Left"
                                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true">
                                                        </ajaxcontrol:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%; background-color: White;
                            padding-top: 20px;">
                            <tr>
                                <td align="center">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" Width="6%"
                                                Height="6%" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClientClick="ClearValues();"
                                                Width="6%" Height="6%"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 35px" align="center">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                runat="Server">
                                                <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                    Height="25px" Width="24px"></asp:Image>
                                                Loading...
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
