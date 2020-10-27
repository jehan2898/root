<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Reminder.ascx.cs" Inherits="UserControl_Reminder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<script language="javascript" type="text/javascript">

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
        
       function btnsave()     {
            
       }
        
        function ReminderTypeBasedVisible()
        {
    
             var d= document.getElementById("<%=rbtnDaily.ClientID %>");
             var w= document.getElementById("<%=rbtnWeekly.ClientID %>");
             var m= document.getElementById("<%=rbtnMonthly.ClientID %>");
             //var y= document.getElementById("ctl00_ContentPlaceHolder1_rbtnYearly");
             
             if(d.checked == true)
             {                
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="block";	
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";	
                //document.getElementById('ctl00_ContentPlaceHolder1_pnlYearly').style.display="none";
                
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
                
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYEvery').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonth').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtYDay').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYThe').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYTerm').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYDayRecur').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYof').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonthRecur').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYDay').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYThe').style.display="none";	
                            
             }             
             
             if(w.checked == true)
             {
              
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="block";	
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";                
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="none";	
                //document.getElementById('ctl00_ContentPlaceHolder1_pnlYearly').style.display="none";	
                
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
                
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYEvery').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonth').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtYDay').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYThe').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYTerm').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYDayRecur').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYof').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonthRecur').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYDay').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYThe').style.display="none";	
             }            
             
             if(m.checked == true)
             {
                
                
                
                document.getElementById('<%=pnlMonthly.ClientID %>').style.display="block";	                
                document.getElementById('<%=pnlWeekly.ClientID %>').style.display="none";	
                document.getElementById('<%=pnlDaily.ClientID %>').style.display="none";
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
                
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYEvery').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonth').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtYDay').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYThe').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYTerm').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYDayRecur').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYof').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonthRecur').style.display="none"; 
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYDay').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYThe').style.display="none";	             
             }
                          
//             if(y.checked == true)
//             {
//                document.getElementById('ctl00_ContentPlaceHolder1_pnlYearly').style.display="block";
//                document.getElementById('ctl00_ContentPlaceHolder1_pnlMonthly').style.display="none";	                
//                document.getElementById('ctl00_ContentPlaceHolder1_pnlWeekly').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_pnlDaily').style.display="none"; 
//                
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnDEvery').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtDDays').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblDDays').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnDEveryWeekday').style.display="none";  
//                document.getElementById('ctl00_ContentPlaceHolder1_lblDEvery').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblDEveryweekday').style.display="none"; 
//                                
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWRecurEvery').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_txtWRecur').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWWeeksOn').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_chkWSunday').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_chkWMonday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_chkWTuesday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_chkWWednesday').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_chkWThursday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_chkWFriday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_chkWSaturday').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWSunday').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWMonday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWTuesday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWWednesday').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWThursday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWFriday').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblWSaturday').style.display="none";	
//                
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnMDay').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtMDay').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblMofEvery').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_txtMMonths').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblMMonths').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnMThe').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_drpMTerm').style.display="none";                
//                document.getElementById('ctl00_ContentPlaceHolder1_drpMDayRecur').style.display="none";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblMEvery').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtMEveryMonths').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblMEveryMonths').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblMDay').style.display="none";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblMThe').style.display="none";	
//                
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYEvery').style.display="block";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonth').style.display="block";
//                document.getElementById('ctl00_ContentPlaceHolder1_txtYDay').style.display="block";	
//                document.getElementById('ctl00_ContentPlaceHolder1_rbtnYThe').style.display="block";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYTerm').style.display="block";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYDayRecur').style.display="block";	
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYof').style.display="block";
//                document.getElementById('ctl00_ContentPlaceHolder1_drpYMonthRecur').style.display="block";  
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYDay').style.display="block";
//                document.getElementById('ctl00_ContentPlaceHolder1_lblYThe').style.display="block";	            
//             }              
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
            document.getElementById('tabcontainerAddVisit_tabPanelAddReminder_reminderVisits_extddlRType').value='NA';	                  
            document.getElementById("<%=txtStartDate.ClientID %>").value =  now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
            document.getElementById("<%=txtReminderDesc.ClientID %>").value = "";             
            document.getElementById("<%=lsbAssignTo.ClientID %>").selectedIndex = 0;                
            document.getElementById("<%=txtEndByDate.ClientID %>").value = now1.getMonth()+1+"/"+now1.getDate()+"/"+now1.getFullYear();
                               
            document.getElementById("<%=chkRecurrence.ClientID %>").checked = 0;                
            document.getElementById("<%=rbtnDaily.ClientID %>").checked = 1;
            document.getElementById("<%=rbtnWeekly.ClientID %>").checked = 0;
            document.getElementById("<%=rbtnMonthly.ClientID %>").checked = 0;
            //document.getElementById("ctl00_ContentPlaceHolder1_rbtnYearly").checked = 0;
                                                             
            document.getElementById("<%=chkRecurrence.ClientID %>").checked = 0;
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
            
//            document.getElementById('ctl00_ContentPlaceHolder1_rbtnYEvery').style.display="none";
//            document.getElementById('ctl00_ContentPlaceHolder1_drpYMonth').style.display="none";
//            document.getElementById('ctl00_ContentPlaceHolder1_txtYDay').style.display="none";	
//            document.getElementById('ctl00_ContentPlaceHolder1_rbtnYThe').style.display="none";
//            document.getElementById('ctl00_ContentPlaceHolder1_drpYTerm').style.display="none";
//            document.getElementById('ctl00_ContentPlaceHolder1_drpYDayRecur').style.display="none";	
//            document.getElementById('ctl00_ContentPlaceHolder1_lblYof').style.display="none";
//            document.getElementById('ctl00_ContentPlaceHolder1_drpYMonthRecur').style.display="none";
//            document.getElementById('ctl00_ContentPlaceHolder1_lblYDay').style.display="none";
//            document.getElementById('ctl00_ContentPlaceHolder1_lblYThe').style.display="none";	 
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
          
        }else
        {
         return true;
        }	
        
        
     }
     
      function ReminderType()
     {
        var Reminderttpe =document.getElementById('tabcontainerAddVisit_tabPanelAddReminder_reminderVisits_extddlRType').value
       
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

<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
    height: 100%" runat="server">
    <tr>
        <td style="height: 28px" bgcolor="#b5df82" class="txt2" align="center">
            <asp:Label ID="lblHeader" runat="server" Font-Size="Medium" Font-Names="Verdana"
                Text="Set Reminder" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
            padding-top: 3px; height: 100%; vertical-align: top;">
            <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="Center" valign="top" align="center">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 100%" align="center" class="TDPart">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%;" class="ContentTable" cellspacing="0" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td valign="middle" align="center" height="30">
                                                        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="padding-left: 10px; height: 108px;" align="left">
                                                                        Note Description
                                                                    </td>
                                                                    <td align="center" style="height: 108px">
                                                                        :</td>
                                                                    <td align="left" style="height: 108px">
                                                                        <asp:TextBox ID="txtReminderDesc" runat="server" Height="75px" Font-Size="12px" CssClass="DesText"
                                                                            Font-Names="Verdana" Width="200px" TextMode="MultiLine" MaxLength="50"></asp:TextBox></td>
                                                                    <td style="padding-left: 10px; height: 108px;" align="left">
                                                                        Assign To
                                                                    </td>
                                                                    <td align="center" style="height: 108px">
                                                                        :&nbsp;</td>
                                                                    <td align="left" style="height: 108px">
                                                                        <asp:ListBox ID="lsbAssignTo" Width="200px" Height="75px" runat="server" SelectionMode="Multiple">
                                                                        </asp:ListBox>
                                                                        <%--<asp:DropDownList ID="extddlAssignTo" Width="50%" runat="server">
                                                                                </asp:DropDownList>--%>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 10px" align="left">
                                                                        Start Date <span class="Mandatory">*</span></td>
                                                                    <td align="center">
                                                                        :</td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="100px" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                    </td>
                                                                    <ajaxToolkit:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtStartDate"
                                                                        PopupButtonID="imgbtnStartDate" />
                                                                    <td style="padding-left: 10px; height: 108px;" align="left">
                                                                        Reminder Type
                                                                    </td>
                                                                    <td align="center" style="height: 108px">
                                                                        :&nbsp;</td>
                                                                    <td>
                                                                        <extddl:ExtendedDropDownList ID="extddlRType" runat="server" Width="150px" Connection_Key="Connection_String"
                                                                            Flag_Key_Value="LIST" Procedure_Name="SP_MST_REMINDER_TYPE" Selected_Text="---Select---" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="6">
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" CssClass="Buttons" />
                                                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Buttons" OnClientClick="ClearValues();"/>
                                                                                </asp:Button>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" align="left" height="30">
                                                        <asp:CheckBox ID="chkRecurrence" runat="server" Text="Re Occurrence"></asp:CheckBox>
                                                    </td>
                                                </tr>
                                                <tr runat="server" style="visibility: hidden" id="trReoccurance">
                                                    <td style="padding-left: 30px" valign="middle" align="center">
                                                        <asp:Panel ID="pnlRecurrence" runat="server">
                                                            <table style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
                                                                border-bottom: black thin solid" cellspacing="0" cellpadding="0" width="600"
                                                                border="1" id="Second">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" height="30">
                                                                                            <asp:RadioButton ID="rbtnDaily" onclick="ReminderTypeBasedVisible()" runat="server"
                                                                                                Text="Daily" GroupName="RECURRENCE" Checked="True"></asp:RadioButton></td>
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
                                                                                                                                <asp:RadioButton ID="rbtnDEvery" runat="server" GroupName="RECURRENCE_DAILY" Checked="True">
                                                                                                                                </asp:RadioButton></td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblDEvery" runat="server" Text="Every"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtDDays" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblDDays" runat="server" Text="Days"></asp:Label></td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td valign="top" align="left" colspan="4" height="20">
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td valign="top" align="left" width="25%">
                                                                                                                                <asp:RadioButton ID="rbtnDEveryWeekday" runat="server" GroupName="RECURRENCE_DAILY">
                                                                                                                                </asp:RadioButton></td>
                                                                                                                            <td align="left" colspan="3">
                                                                                                                                <asp:Label ID="lblDEveryweekday" runat="server" Text="Every weekday"></asp:Label></td>
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
                                                                                                                                <asp:Label ID="lblWRecurEvery" runat="server" Text="Recur every"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtWRecur" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblWWeeksOn" runat="server" Text="week(s) on "></asp:Label></td>
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
                                                                                                                <asp:Label ID="lblWSunday" runat="server" Text="Sunday"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWMonday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWMonday" runat="server" Text="Monday"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWTuesday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWTuesday" runat="server" Text="Tuesday"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWWednesday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWWednesday" runat="server" Text="Wednesday"></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" colspan="8" height="20">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWThursday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWThursday" runat="server" Text="Thursday"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWFriday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left">
                                                                                                                <asp:Label ID="lblWFriday" runat="server" Text="Friday"></asp:Label></td>
                                                                                                            <td align="left">
                                                                                                                <asp:CheckBox ID="chkWSaturday" runat="server"></asp:CheckBox></td>
                                                                                                            <td align="left" colspan="3">
                                                                                                                <asp:Label ID="lblWSaturday" runat="server" Text="Saturday"></asp:Label></td>
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
                                                                                                                                <asp:RadioButton ID="rbtnMDay" runat="server" GroupName="RECURRENCE_MONTHLY" Checked="True">
                                                                                                                                </asp:RadioButton></td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblMDay" runat="server" Text="Day"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtMDay" runat="server" Width="29px" MaxLength="2" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMofEvery" runat="server" Text="of every"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtMMonths" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMMonths" runat="server" Text="month(s)"></asp:Label></td>
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
                                                                                                                                <asp:RadioButton ID="rbtnMThe" runat="server" GroupName="RECURRENCE_MONTHLY"></asp:RadioButton></td>
                                                                                                                            <td align="left">
                                                                                                                                <asp:Label ID="lblMThe" runat="server" Text="The"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpMTerm" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpMDayRecur" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMEvery" runat="server" Text="of every"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:TextBox ID="txtMEveryMonths" runat="server" Width="29px" MaxLength="3" CssClass="textbox"></asp:TextBox></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblMEveryMonths" runat="server" Text="month(s)"></asp:Label></td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="pnlYearly" runat="server" Visible="False">
                                                                                                <table cellspacing="0" cellpadding="0" border="0">
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
                                                                                                                                <asp:Label ID="lblYDay" runat="server" Text="Day"></asp:Label></td>
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
                                                                                                                                <asp:Label ID="lblYThe" runat="server" Text="The"></asp:Label></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpYTerm" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:DropDownList ID="drpYDayRecur" runat="server">
                                                                                                                                </asp:DropDownList></td>
                                                                                                                            <td style="padding-left: 10px" align="left">
                                                                                                                                <asp:Label ID="lblYof" runat="server" Text="of"></asp:Label></td>
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
                                                                                                Text="Weekly" GroupName="RECURRENCE"></asp:RadioButton></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" height="30">
                                                                                            <asp:RadioButton ID="rbtnMonthly" onclick="ReminderTypeBasedVisible()" runat="server"
                                                                                                Text="Monthly" GroupName="RECURRENCE"></asp:RadioButton></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" height="30">
                                                                                            <asp:RadioButton ID="rbtnYearly" onclick="ReminderTypeBasedVisible()" runat="server"
                                                                                                Text="Yearly" Visible="False" GroupName="RECURRENCE"></asp:RadioButton></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px" colspan="2" height="75">
                                                                                            <asp:RadioButton ID="rbtnNoEndDate" runat="server" Text="No End Date" GroupName="END_DATE"
                                                                                                Checked="True"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnEndAfter"
                                                                                                    runat="server" Text="End After" GroupName="END_DATE" Style="display: none"></asp:RadioButton>
                                                                                            <asp:TextBox ID="txtEndAfter" runat="server" Width="29px" MaxLength="3" CssClass="textbox"
                                                                                                Style="display: none"></asp:TextBox>&nbsp;
                                                                                            <asp:Label ID="lblOccurrence" runat="server" Text="Occurrences" Style="display: none"></asp:Label>
                                                                                            &nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="rbtnEndBy" runat="server" Text="End by" GroupName="END_DATE"></asp:RadioButton>&nbsp;
                                                                                            <asp:TextBox ID="txtEndByDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                                                            &nbsp;<asp:ImageButton ID="ImgEndByDate" runat="server" ImageUrl="~/Images/cal.gif">
                                                                                            </asp:ImageButton></td>
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
                                                    <td style="height: 35px">
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                            <ProgressTemplate>
                                                                <div style="left: 0px; top: 0px" id="DivStatus3" class="PageUpdateProgress" runat="Server">
                                                                    <asp:Image ID="ajaxLoadNotificationImage3" runat="server" ImageUrl="~/Images/ajax-loader.gif"
                                                                        AlternateText="[image]"></asp:Image>
                                                                    <label style="font-size: small; font-family: Verdana" id="Label2" class="PageLoadProgress">
                                                                        <b>Processing, Please wait...</b></label>
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
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
</table>
