<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Add_Reminder.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Add_Reminder"
    Title="Add Reminder" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
     function SelectAll(ival)
       {
            var f= document.getElementById("<%=grdaccountreminder.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {	
			       if(f.getElementsByTagName("input").item(i).disabled==false)
		             {					
			             f.getElementsByTagName("input").item(i).checked=ival;
			         }    
			    }			
		    }
       }


        
        function btnsave()    
        {

        }
        
        function ReminderTypeBasedVisible()
        {
             var hdl=document.getElementById('<%=hdncheck.ClientID %>');
             var d= document.getElementById("<%=rbtnDaily.ClientID %>");
             var w= document.getElementById("<%=rbtnWeekly.ClientID %>");
             var m= document.getElementById("<%=rbtnMonthly.ClientID %>");

            if(hdl.Value!='Edit')
            {
             if(d.checked == true)
             {                
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="block";	
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="block";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="block";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="block";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="block";   
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="block";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="block"; 
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
                document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
                document.getElementById('<%=drpMTerm.ClientID %>').style.display="none";                
                document.getElementById('<%=drpMDayRecur.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="none";	
             }             
             
             if(w.checked == true)
             {
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="block";	
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";                
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="none";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="none";  
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="none";  
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="block";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="block";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="block";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="block";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="block";	
                document.getElementById('<%=chkWThursday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="block";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="block";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="block";	
                var nowdate = new Date();
                var day = nowdate.getDay();                
                if(day == 0)
                {
                    document.getElementById('<%=chkWSunday.ClientID %>').checked = true;                                    
                }
                
                else if(day == 1)
                {                    
                    document.getElementById('<%=chkWMonday.ClientID %>').checked = true;                    
                }
                else if(day == 2)
                {                   
                    document.getElementById('<%=chkWTuesday.ClientID %>').checked = true;                 
                }
                else if(day == 3)
                {                   
                    document.getElementById('<%=chkWWednesday.ClientID %>').checked = true;                  
                }
                else if(day == 4)
                {                 
                    document.getElementById('<%=chkWThursday.ClientID %>').checked = true;                  
                }
                else if(day == 5)
                {
                    document.getElementById('<%=chkWFriday.ClientID %>').checked = true;                
                }
                else if(day == 6)
                {                  
                    document.getElementById('<%=chkWSaturday.ClientID %>').checked = true;  
                }
                
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
                document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
                document.getElementById('<%=drpMTerm.ClientID %>').style.display="none";                
                document.getElementById('<%=drpMDayRecur.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="none";	

             }            
             
             if(m.checked == true)
             {
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="block";	                
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";	
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="none";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="none";   
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="block";
                document.getElementById('<%=txtMDay.ClientID %>').style.display="block";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="block";	
                document.getElementById('<%=txtMMonths.ClientID %>').style.display="block";
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="block";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="block";	
                document.getElementById('<%=drpMTerm.ClientID %>').style.display="block";                
                document.getElementById('<%=drpMDayRecur.ClientID %>').style.display="block";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="block";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="block";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="block";	
                document.getElementById('<%=lblMDay.ClientID %>').style.display="block";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="block";
             }
         }
             else
             {
               
             
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
            document.getElementById('ctl00_ContentPlaceHolder1_extddlRType').value='NA';	        
            document.getElementById("<%=txtStartDate.ClientID %>").value =  now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
            document.getElementById("<%=txtReminderDesc.ClientID %>").value = "";
            document.getElementById("<%=lsbAssignTo.ClientID %>").selectedIndex = 0;                
            document.getElementById("<%=txtEndByDate.ClientID %>").value = now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
            document.getElementById("<%=chkRecurrence.ClientID %>").checked = 0;                
            document.getElementById("<%=rbtnDaily.ClientID %>").checked = 1;
            document.getElementById("<%=rbtnWeekly.ClientID %>").checked = 0;
            document.getElementById("<%=rbtnMonthly.ClientID %>").checked = 0;
            document.getElementById("<%=chkRecurrence.ClientID %>").checked = 0;
            document.getElementById('<%=pnlRecurrence.ClientID %>').style.display="none";
            document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";
            document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";
            document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";
            document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="none";
            document.getElementById('<%=txtDDays.ClientID %>').style.display="none";	
            document.getElementById('<%=lblDDays.ClientID %>').style.display="none";
            document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="none";   
            document.getElementById('<%=lblDEvery.ClientID %>').style.display="none";
            document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="none"; 
            document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
            document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
            document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
            document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
            document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";	
            document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	
            document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
            document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
            document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
            document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
            document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
            document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
            document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
            document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
            document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
            document.getElementById('<%=drpMTerm.ClientID %>').style.display="none";                
            document.getElementById('<%=drpMDayRecur.ClientID %>').style.display="none";	
            document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
            document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
            document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";
            document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
            document.getElementById('<%=lblMThe.ClientID %>').style.display="none";		
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
            
            var month = document.getElementById('<%=drpYMonth.ClientID %>').selectedIndex;	
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
           var n = document.getElementById('<%=txtYDay.ClientID %>').value;	
           var month = document.getElementById('<%=drpYMonth.ClientID %>').selectedIndex;	
            if(month ==1)
            {
                if(n != "")
                { 
                    var year = now4.getFullYear();
                    if(((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    {
                        if(n<1 || n >29)
                        { 
                            document.getElementById('<%=txtYDay.ClientID %>').value = now4.getDate();                 
                            alert("Please enter number between 1 and 29 for this Month.");  
                        }
                    }
                    else
                    {
                        if(n<1 || n >28)
                        { 
                            document.getElementById('<%=txtYDay.ClientID %>').value = now4.getDate();                 
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
                        document.getElementById('<%=txtYDay.ClientID %>').value = now4.getDate();                 
                        alert("Please enter number between 1 and 31.");    
                    }   
                }
                else
                {
                    document.getElementById('<%=txtYDay.ClientID %>').value = now4.getDate(); 
                } 
            }                
       }
       
       
       
       
       function ZeroNotAllowed(e)
       {
       
           if(document.getElementById('<%=txtEndAfter.ClientID %>').value == "0")
           {
                document.getElementById('<%=txtEndAfter.ClientID %>').value = "";
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
        var Note = document.getElementById('<%=txtReminderDesc.ClientID %>').value;	
        var Date = document.getElementById('<%=txtStartDate.ClientID %>').value;
        if(Note=='' || Date=='' )
        {
          alert("Please Enter the value for Note And Date");
          return false;
          
        }
        else
        {
         return true;
        }	
     }
     
      function ReminderType()
     {
        var Reminderttpe = document.getElementById('ctl00_ContentPlaceHolder1_extddlRType').value;	
       
        if(Reminderttpe=='NA')
        {
        alert("Please Select Reminder Type");
          return false;
          
        }else
        {
         return true;
        }	
        
        
     }
     
     
     
        
         function ReminderTypeBasedVisibleNew()
        {
             var d= document.getElementById("<%=rbtnDaily.ClientID %>");
             var w= document.getElementById("<%=rbtnWeekly.ClientID %>");
             var m= document.getElementById("<%=rbtnMonthly.ClientID %>");
             if(d.checked == true)
             {                
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="block";	
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="block";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="block";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="block";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="block";   
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="block";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="block"; 
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
                document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="none";	
             }             
             
             if(w.checked == true)
             {
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="block";	
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";                
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="none";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="none";  
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="none";  
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="block";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="block";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="block";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="block";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="block";	
                document.getElementById('<%=chkWThursday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="block";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="block";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="block";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="block";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="block";	
                var nowdate = new Date();
                var day = nowdate.getDay();                
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
                document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="none";	

             }            
             
             if(m.checked == true)
             {
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="block";	                
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";	
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="none";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="none";   
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="block";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="block";	
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="block";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="block";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="block";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="block";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="block";	
                document.getElementById('<%=lblMDay.ClientID %>').style.display="block";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="block";
             }
        }
        
        
          function ClearValues1()
        {     
            var now1 = new Date();  
            document.getElementById('ctl00_ContentPlaceHolder1_extddlRType').value='NA';	        
            document.getElementById("<%=txtStartDate.ClientID %>").value =  now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
            document.getElementById("<%=txtReminderDesc.ClientID %>").value = "";
            document.getElementById("<%=lsbAssignTo.ClientID %>").selectedIndex = 0;                
            document.getElementById("<%=txtEndByDate.ClientID %>").value = now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
            document.getElementById("<%=chkRecurrence.ClientID %>").checked = 1;                
            document.getElementById("<%=rbtnDaily.ClientID %>").checked = 1;
            document.getElementById("<%=rbtnWeekly.ClientID %>").checked = 0;
            document.getElementById("<%=rbtnMonthly.ClientID %>").checked = 0;
            document.getElementById('<%=pnlRecurrence.ClientID %>').style.display="block";
            document.getElementById('<%=pnlDaily.ClientID %>').style.display="block";
            document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";
            document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";
            document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="block";
            document.getElementById('<%=txtDDays.ClientID %>').style.display="block";	
            document.getElementById('<%=lblDDays.ClientID %>').style.display="block";
            document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="block";   
            document.getElementById('<%=lblDEvery.ClientID %>').style.display="block";
            document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="block"; 
            document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
            document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
            document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
            document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
            document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";	
            document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
            document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	
            document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
            document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
            document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
            document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
            document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
            document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
            document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
            document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
            document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
            document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
            document.getElementById('<%=drpMTerm.ClientID %>').style.display="none";                
            document.getElementById('<%=drpMDayRecur.ClientID %>').style.display="none";	
            document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
            document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
            document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";
            document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
            document.getElementById('<%=lblMThe.ClientID %>').style.display="none";
            document.getElementById("<%=rbtnDEvery.ClientID %>").checked = 1;
            document.getElementById("<%=txtDDays.ClientID %>").value = "1";		
        }
        
        
        function MakeVisibleRecurrence()
        {
                 
            
            var f= document.getElementById("<%=chkRecurrence.ClientID%>");
            
            if(f.checked == true)
            {
          
            document.getElementById("<%=trReoccurance.ClientID%>").style.visibility="visible";
                document.getElementById("<%=rbtnDaily.ClientID %>").checked = 1;
                document.getElementById("<%=rbtnWeekly.ClientID %>").checked = 0;
                document.getElementById("<%=rbtnMonthly.ClientID %>").checked = 0;
                //document.getElementById("ctl00_ContentPlaceHolder1_rbtnYearly").checked = 0;
                
                document.getElementById('<%=pnlRecurrence.ClientID %>').style.display="block";	
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="block";
                
                document.getElementById("<%=rbtnDEvery.ClientID %>").checked = 1;
                document.getElementById("<%=rbtnDEveryWeekday.ClientID %>").checked = 0;
                document.getElementById("<%=txtDDays.ClientID %>").value = "1";
                
                document.getElementById("<%=rbtnDEvery.ClientID %>").style.display="block";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="block";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="block";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="block"; 
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="block";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="block"; 
                
                document.getElementById("<%=txtWRecur.ClientID %>").value = "1";
                document.getElementById("<%=chkWSunday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWMonday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWTuesday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWWednesday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWThursday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWFriday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWSaturday.ClientID %>").checked = 0;                
                
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";
               
                 document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";		
               
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	                
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
                
                document.getElementById("<%=rbtnMDay.ClientID %>").checked = 1;
                document.getElementById("<%=rbtnMThe.ClientID %>").checked = 0;
                
                var now = new Date();                
                document.getElementById("<%=txtMDay.ClientID %>").value = now.getDate();
                document.getElementById("<%=txtMMonths.ClientID %>").value = "1";
                document.getElementById("<%=drpMTerm.ClientID %>").selectedIndex = 0;
                document.getElementById("<%=drpMDayRecur.ClientID %>").selectedIndex = 0;
                document.getElementById("<%=txtMEveryMonths.ClientID %>").value = "1";
                
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
                document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
                document.getElementById('<%=drpMTerm.ClientID %>').style.display="none";                
                document.getElementById('<%=drpMDayRecur.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="none";	
                
      	
                
                
                document.getElementById("<%=rbtnNoEndDate.ClientID %>").checked = 1;
                document.getElementById("<%=rbtnEndAfter.ClientID %>").checked = 0;
                document.getElementById("<%=rbtnEndBy.ClientID %>").checked = 0;	
                
                document.getElementById("<%=txtEndAfter.ClientID %>").value = "";	                
                document.getElementById("<%=txtEndByDate.ClientID %>").value = now.getMonth()+1+"/"+now.getDate()+"/"+now.getFullYear();

            }
            else
            {            
                     
                document.getElementById("<%=rbtnDEvery.ClientID %>").checked = 1;
                document.getElementById("<%=rbtnDEveryWeekday.ClientID %>").checked = 0;
                document.getElementById("<%=txtDDays.ClientID %>").value = "1";
            
                document.getElementById("<%=txtWRecur.ClientID %>").value = "1";
                document.getElementById("<%=chkWSunday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWMonday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWTuesday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWWednesday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWThursday.ClientID %>").checked = 0;
                document.getElementById("<%=chkWFriday.ClientID %>" ).checked = 0;
                document.getElementById("<%=chkWSaturday.ClientID %>").checked = 0;  
                
                document.getElementById("<%=rbtnMDay.ClientID %>").checked = 1;

                
                var now = new Date();                
                document.getElementById("<%=txtMDay.ClientID %>").value = now.getDate();
                document.getElementById("<%=txtMMonths.ClientID %>").value = "1";
                document.getElementById("<%=drpMTerm.ClientID %>").selectedIndex = 0;
                document.getElementById("<%=drpMDayRecur.ClientID %>").selectedIndex = 0;
                document.getElementById("<%=txtMEveryMonths.ClientID %>").value = "1";
    
            
                document.getElementById("<%=rbtnNoEndDate.ClientID %>").checked = 1;
                document.getElementById("<%=rbtnEndAfter.ClientID %>").checked = 0;
                document.getElementById("<%=rbtnEndBy.ClientID %>").checked = 0;	
                
                document.getElementById("<%=txtEndAfter.ClientID %>").value = "";	                
                document.getElementById("<%=txtEndByDate.ClientID %>").value = now.getMonth()+1+"/"+now.getDate()+"/"+now.getFullYear();
                
                document.getElementById("<%=rbtnDaily.ClientID %>").checked = 1;
                document.getElementById("<%=rbtnWeekly.ClientID %>").checked = 0;
                document.getElementById("<%=rbtnMonthly.ClientID %>").checked = 0;
                //document.getElementById("ctl00_ContentPlaceHolder1_rbtnYearly").checked = 0;
                             
                document.getElementById('<%=pnlRecurrence.ClientID %>').style.display="none";
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";
                //document.getElementById('ctl00_ContentPlaceHolder1_pnlYearly').style.display="none";
                                
                document.getElementById('<%=rbtnDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtDDays.ClientID %>').style.display="none";	
                document.getElementById('<%=lblDDays.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnDEveryWeekday.ClientID %>').style.display="none";   
                document.getElementById('<%=lblDEvery.ClientID %>').style.display="none";
                document.getElementById('<%=lblDEveryweekday.ClientID %>').style.display="none"; 
                                
                document.getElementById('<%=lblWRecurEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtWRecur.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWeeksOn.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=chkWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=chkWSaturday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWSunday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWMonday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWTuesday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWWednesday.ClientID %>').style.display="none";	
                document.getElementById('<%=lblWThursday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWFriday.ClientID %>').style.display="none";
                document.getElementById('<%=lblWSaturday.ClientID %>').style.display="none";	
                
                document.getElementById('<%=rbtnMDay.ClientID %>').style.display="none";
                document.getElementById('<%=txtMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMofEvery.ClientID %>').style.display="none";	
                document.getElementById('<%=txtMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMMonths.ClientID %>').style.display="none";
                document.getElementById('<%=rbtnMThe.ClientID %>').style.display="none";	
                document.getElementById('<%=drpMTerm.ClientID %>').style.display="none";                
                document.getElementById('<%=drpMDayRecur.ClientID %>').style.display="none";	
                document.getElementById('<%=lblMEvery.ClientID %>').style.display="none";
                document.getElementById('<%=txtMEveryMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMEveryMonths.ClientID %>').style.display="none";
                document.getElementById('<%=lblMDay.ClientID %>').style.display="none";
                document.getElementById('<%=lblMThe.ClientID %>').style.display="none";		
                
                
                
            }   
        }   
     
     
    
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
            <td align="left" valign="top" style="width: 45%;">
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
            <td align="left" valign="top" style="width: 55%;">
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
                                                <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtStartDate"
                                                    PopupButtonID="imgbtnStartDate" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="left" height="30" colspan="2">
                                                <%--<asp:Label ID="Label3" runat="server" Font-Size="Small" Font-Bold="true"></asp:Label>--%>
                                                <asp:CheckBox ID="chkRecurrence" runat="server" Text="Occurrence" Font-Size="Small"
                                                    Font-Bold="true" Visible="true"></asp:CheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" border="0" style="width: 100%; background-color: White;"
                                        id="tblrecurrance">
                                        <tr runat="server" id="trReoccurance">
                                            <td valign="top" align="left">
                                                <asp:Panel ID="pnlRecurrence" runat="server">
                                                    <table style="width: 96%; height: 100%;" cellspacing="0" cellpadding="0" border="0" id="Second">
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
                                                                                        <table cellspacing="0" cellpadding="0" border="0" id="tbldaily">
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
                                                                                        <table cellspacing="0" cellpadding="0" border="0" id="tblweekly">
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
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndByDate"
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
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                                    AcceptNegative="Left" AutoComplete="false" CultureName="en-US" DisplayMoney="Left"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true">
                                                </ajaxToolkit:MaskedEditExtender>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndByDate"
                                                    AcceptNegative="Left" AutoComplete="false" CultureName="en-US" DisplayMoney="Left"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true">
                                                </ajaxToolkit:MaskedEditExtender>
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
                    padding-top: 9px;">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" Width="6%"
                                        Height="6%" />
                                    <asp:Button ID="btnupdate" Text="Update" runat="server" OnClick="btnUpdate_Click"
                                        Width="6%" Height="6%" />
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
                                <progresstemplate>
                                        <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                            runat="Server">
                                            <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                Height="25px" Width="24px"></asp:Image>
                                            Loading...
                                        </div>
                               </progresstemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table style="width: 100%; height: 100%;">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                                <b class="txt3"></b>
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
                                                    <%= this.grdaccountreminder.RecordCount%>
                                                    | Page Count:
                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                    </gridpagination:XGridPaginationDropDown>
                                                    <asp:Button ID="btndelete" runat="server" Visible="true" Text="Delete" OnClick="btnDelete_Click">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <xgrid:XGridViewControl ID="grdaccountreminder" runat="server" Width="100%" CssClass="mGrid"
                                                    DataKeyNames="sz_reminder_type_id,SZ_REMINDER_DESC,SZ_USER_NAME,DT_REMINDER_DATE,is_recurrence,recurrence_type,I_REMINDER_ID,
                                                    d_day_option,d_day_count,d_every_weekday,w_recur_week_count,w_sunday,w_monday,w_tuesday,w_wednesday,w_thursday,w_friday,w_saturday,
                                                    m_month_option,m_day,m_month_count,m_term,m_term_week,m_every_month_count,dt_start_date,i_date_count,dt_reminder_date,SZ_REMINDER_ASSIGN_TO"
                                                    MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    ExportToExcelFields="" ShowExcelTableBorder="true" ExportToExcelColumnNames=""
                                                    AllowPaging="true" XGridKey="Bill_Sys_Account_Reminder" PageRowCount="40" PagerStyle-CssClass="pgr"
                                                    AllowSorting="true" AutoGenerateColumns="false" GridLines="None" Visible="true"
                                                    OnRowCommand="grdaccountreminder_RowCommand" OnRowEditing="grdaccountreminder_RowEditing">
                                                    <Columns>
                                                        <%--0--%>
                                                        <asp:TemplateField HeaderText="">
                                                            <itemtemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Edit"></asp:LinkButton>
                                                        </itemtemplate>
                                                        </asp:TemplateField>
                                                        <%--1--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            headertext="Reminder ID" DataField="I_REMINDER_ID" Visible="False" />
                                                        <%--2--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            headertext="Due Date" DataField="DT_REMINDER_DATE" DataFormatString="{0:dd MMM yyyy}"
                                                            SortExpression="reminder.DT_REMINDER_DATE" />
                                                        <%--3--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            headertext="Task" DataField="SZ_REMINDER_DESC" />
                                                        <%--  4--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            headertext="Assign To ID" DataField="SZ_REMINDER_ASSIGN_TO" Visible="False" />
                                                        <%--  5--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="Assigned To" DataField="SZ_USER_NAME" />
                                                        <%--  6--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="Reminder Type" DataField="sz_reminder_type" />
                                                        <%--  7--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="REMINDER_ASSIGN_BY" DataField="SZ_REMINDER_ASSIGN_BY" Visible="False" />
                                                        <%--  8--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="REMINDER_STATUS_ID" DataField="SZ_REMINDER_STATUS_ID" Visible="False" />
                                                        <%--  9--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="sz_reminder_type_id" DataField="sz_reminder_type_id" Visible="False" />
                                                        <%-- 10--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="is_recurrence" DataField="is_recurrence" Visible="False" />
                                                        <%-- 11--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="recurrence_type" DataField="recurrence_type" Visible="False" />
                                                        <%-- 12--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="occurrence_end_count" DataField="occurrence_end_count" Visible="False" />
                                                        <%-- 13--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="d_day_option" DataField="d_day_option" Visible="False" />
                                                        <%-- 14--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="d_day_count" DataField="d_day_count" Visible="False" />
                                                        <%-- 15--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="d_every_weekday" DataField="d_every_weekday" Visible="False" />
                                                        <%-- 16--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_recur_week_count" DataField="w_recur_week_count" Visible="False" />
                                                        <%-- 17--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_sunday" DataField="w_sunday" Visible="False" />
                                                        <%-- 18--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_monday" DataField="w_monday" Visible="False" />
                                                        <%-- 19--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_tuesday" DataField="w_tuesday" Visible="False" />
                                                        <%-- 20--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_wednesday" DataField="w_wednesday" Visible="False" />
                                                        <%-- 21--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_thursday" DataField="w_thursday" Visible="False" />
                                                        <%-- 22--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_friday" DataField="w_friday" Visible="False" />
                                                        <%-- 23--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="w_saturday" DataField="w_saturday" Visible="False" />
                                                        <%-- 24--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="m_month_option" DataField="m_month_option" Visible="False" />
                                                        <%-- 25--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="m_day" DataField="m_day" Visible="False" />
                                                        <%-- 26--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="m_month_count" DataField="m_month_count" Visible="False" />
                                                        <%-- 27--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="m_term" DataField="m_term" Visible="False" />
                                                        <%-- 28--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="m_term_week" DataField="m_term_week" Visible="False" />
                                                        <%-- 29--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="m_every_month_count" DataField="m_every_month_count" Visible="False" />
                                                        <%-- 30--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="y_year_option" DataField="y_year_option" Visible="False" />
                                                        <%-- 31--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="y_month" DataField="y_month" Visible="False" />
                                                        <%-- 32--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="y_day" DataField="y_day" Visible="False" />
                                                        <%-- 33--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="y_term" DataField="y_term" Visible="False" />
                                                        <%-- 34--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="y_every_month_count" DataField="y_every_month_count" Visible="False" />
                                                        <%-- 35--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="dt_start_date" DataField="dt_start_date" Visible="False" />
                                                        <%-- 36--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="i_date_count" DataField="i_date_count" Visible="False" />
                                                        <%-- 36--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            headertext="dt_reminder_date" DataField="dt_reminder_date" Visible="False" />
                                                        <%-- 37--%>
                                                        <asp:TemplateField HeaderText="">
                                                            <headertemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                            </headertemplate>
                                                            <itemtemplate>
                                                            <asp:CheckBox ID="ChkDelete" runat="server" />
                                                            </itemtemplate>
                                                            <headerstyle horizontalalign="center"></headerstyle>
                                                            <itemstyle horizontalalign="center"></itemstyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="con" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtuseridforre" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="drpterm" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="drptermweek" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:HiddenField ID="hdncheck" runat="server" />
</asp:Content>
