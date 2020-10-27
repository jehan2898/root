<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Billinvoice_report.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Billinvoice_report"
    Title="Software Invoice Report" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>

    <script type="text/javascript">
    
     function Clear()
       {
        document.getElementById("<%=txtBillNo.ClientID%>").value='';
        document.getElementById("<%=txtgenratetodate.ClientID%>").value='';
        document.getElementById("<%=txtgenratefromdate.ClientID%>").value='';
        document.getElementById("<%=txtinvoiceid.ClientID%>").value='';
        document.getElementById("ctl00_ContentPlaceHolder1_ddlsoftwarefee").value=0;
       }
       function Confirm_Delete_Code()
         {     
                var f= document.getElementById("<%=grdInvoicegenerate.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {		
			            				
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {  bfFlag = true;   
			                    
    		                    
		                    }
			                    
			                }
			            }
			        }   			
		        
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
//		        else
//		        {
//		            var msg = "Do you want to proceed?";
//                     var result = confirm(msg);
//                     if(result==true)
//                     {
//                        return true;
//                     }
//                     else
//                     {
//                        return false;
//                     }
//		            //return true;
//		        }
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
     function showInvoiceFrame(objInvoiceID,objCompanyID)
        {
	    // alert(objCaseID + ' ' + objCompanyID);
	        var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute'; 
            document.getElementById('divfrmPatient').style.left= '400px'; 
            document.getElementById('divfrmPatient').style.top= '350px'; 
            document.getElementById('divfrmPatient').style.visibility='visible'; 
            document.getElementById('frmpatient').src="Invoice_generate_popup.aspx?InvoiceID="+objInvoiceID+"&cmpId="+ objCompanyID+"";
            return false;   
        }
         function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdInvoicegenerate.ClientID %>");	
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
        function ClosePatientFramePopup()
               {
                 //   alert("");
                   //document.getElementById('divfrmPatient').style.height='0px';
                    document.getElementById('divfrmPatient').style.visibility='hidden';
                   document.getElementById('divfrmPatient').style.top='-10000px';
                    document.getElementById('divfrmPatient').style.left='-10000px';
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
            var selValue = document.getElementById("<%=ddlgeneartedate.ClientID %>").value;
        
            if(selValue == 0)
            {
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = "";
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = getDate('today');
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = lastmonth('endmonth');
                   
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value =lastyear('endyear');
                   
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtgenratetodate.ClientID %>").value = quarteryear('endquaeter');
                   
                   document.getElementById("<%=txtgenratefromdate.ClientID %>").value =  quarteryear('startquaeter');
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
         
         
             function val_CheckControls()
        {
            
               
       
              if(document.getElementById('<%=txtChequeAmount.ClientID%>').value == '')
            {
                alert('Please enter cheque amount.');
                //document.getElementById('ctl00_ContentPlaceHolder1_txtChequeAmount').value = '0';
                document.getElementById('<%=txtChequeAmount.ClientID%>').focus();
                return false;
            } 
               
                 if(document.getElementById('<%=txtpaymentdate.ClientID%>').value == '')
            {
                alert('Please Select Paymnet Date.');
                //document.getElementById('ctl00_ContentPlaceHolder1_txtpaymentdate').value = '0';
                document.getElementById('<%=txtpaymentdate.ClientID%>').focus();
                return false;
            } 
       
            if(document.getElementById('<%=txtChequeNumber.ClientID%>').value == '')
            {
                alert('Please Select Cheque Number');
               // document.getElementById('ctl00_ContentPlaceHolder1_checkdate').value = '0';
                document.getElementById('<%=txtChequeNumber.ClientID%>').focus();
                
                return false;
            }
             else
            {
            
                var amount11=parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtChequeAmount').value);
                        
                var  amount2= document.getElementById("<%=lblBalance.ClientID %>").innerHTML
                 
                   
                  if ( amount11<amount2 )
                  {
                    alert('Entered  amount must be equal to total amount?');
                               
                   return false;
                    }
               
                else
                  {
                    
                  }
            }
            
        }
        
        
          function confirm_bill_delete()
		 {
		         
		        var f= document.getElementById("<%=grdPaymentTransaction.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                    bfFlag = true;
			                }
			            }
			        }			
		        }
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
		        
		 

                if(confirm("Do You Want to delete Invoice ?\n It may cause Delete Payment against Multiple Transaction")==true)
				{
				  
				   return true;
				}
				else
				{
					return false;
				}
		}
         function showUploadFilePopup()
       {
      
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.height='100px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
	        document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.top = '261px';
	        document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.left ='350px';
	        document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.zIndex= '0';
       }
       function CloseUploadFilePopup()
       {
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.height='0px';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';  
          //  document.getElementById('ctl00_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
         
    </script>

    <table width="100%" border="0">
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="background-color: White;">
                <table>
                    <tr>
                        <td>
                            <%--       <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">--%>
                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnSearch.ClientID %>)">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                        <b class="txt3">Search Parameters</b>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Bill Date
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
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnDateofAccident">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnDateofBirth">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    Generate Date
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
                                                    <asp:DropDownList ID="ddlgeneartedate" runat="Server" Width="98%">
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
                                                    <asp:TextBox ID="txtgenratefromdate" onkeypress="return CheckForInteger(event,'/')"
                                                        runat="server" MaxLength="10" Width="80%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtngenfromdate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtgenratetodate" onkeypress="return CheckForInteger(event,'/')"
                                                        runat="server" MaxLength="10" Width="75%"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtngentodate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtgenratefromdate"
                                                        PopupButtonID="imgbtngenfromdate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtgenratetodate"
                                                        PopupButtonID="imgbtngentodate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Bill No
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Invoice Id
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch">
                                                    Software Fee
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtBillNo" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:TextBox ID="txtinvoiceid" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3">
                                                    <asp:DropDownList ID="ddlsoftwarefee" runat="server">
                                                        <asp:ListItem Text="Not Paid" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Paid" Value="1">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSearch" runat="server" Width="80px" Text="Search" OnClick="btnSearch_Click">
                                                </asp:Button>
                                                <input type="button" id="btnClear" style="width: 80px" value="Clear" onclick="Clear();" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-color: White;">
            </td>
        </tr>
    </table>
    <table width="100%">
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
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                runat="Server">
                                                <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
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
                                        <%= this.grdInvoicegenerate.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                            Text="Export TO Excel">
                                        <img src="Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                            title="Export TO Excel" /></asp:LinkButton>
                                        <asp:Button ID="btninvoicegenerate" runat="server" Visible="true" Text="Paid" OnClick="btninvoice_Click">
                                        </asp:Button>
                                        <asp:Button ID="btndelete" runat="server" Visible="true" Text="Delete" OnClick="btndelete_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdInvoicegenerate" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="i_invoice_id,PAID_STATUS,MN_INVOICE_AMOUNT" MouseOverColor="0, 153, 153"
                                        EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                        AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="i_invoice_id,INVOICE_GENERATED_DATE,PAID_STATUS,MN_INVOICE_AMOUNT"
                                        ShowExcelTableBorder="true" ExportToExcelColumnNames="INVOICE ID,Generated Date,Payment,Software Amount"
                                        AllowPaging="true" XGridKey="Bill_sys_invoice_generate" PageRowCount="40" PagerStyle-CssClass="pgr"
                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:TemplateField HeaderText="INVOICE ID" SortExpression="convert(int,i_invoice_id)">
                                                <itemtemplate>
                                                          <a id="lnkframePatient" href="#" onclick='<%# "showInvoiceFrame(" + "\""+ Eval("i_invoice_id") + "\");" %>' ><%# DataBinder.Eval(Container, "DataItem.i_invoice_id")%></a>
                                                </itemtemplate>
                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                <itemstyle horizontalalign="left" width="20%"></itemstyle>
                                            </asp:TemplateField>
                                            <%--1--%>
                                            <asp:BoundField DataField="INVOICE_GENERATED_DATE" HeaderText="Generated Date" SortExpression="dt_created"
                                                DataFormatString="{0:MM/dd/yyyy}">
                                                <headerstyle horizontalalign="Left"></headerstyle>
                                                <itemstyle horizontalalign="Left"></itemstyle>
                                            </asp:BoundField>
                                            <%--2--%>
                                            <asp:TemplateField HeaderText="INVOICE">
                                                <itemtemplate>
                                                           <%# DataBinder.Eval(Container, "DataItem.PATH")%>
                                                </itemtemplate>
                                                <headerstyle horizontalalign="center"></headerstyle>
                                                <itemstyle horizontalalign="center" width="10%"></itemstyle>
                                            </asp:TemplateField>
                                            <%--3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                headertext="Payment" DataField="PAID_STATUS" />
                                            <%--4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="mst_softwarefee_invoices.sz_total_amount" headertext="Software Amount"
                                                DataFormatString="{0:C}" DataField="MN_INVOICE_AMOUNT" />
                                            <%--5--%>
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
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnExportToExcel" EventName="Click" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <%--    <table width="100%">
            <tr>
                <td>--%>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lbnTest"
                DropShadow="false" PopupControlID="pnlSaveBillnotes" BehaviorID="modal1" PopupDragHandleControlID="Div2">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display: none; width: 500px; height: 200px; background-color: #dbe6fa;"
                ID="pnlSaveBillnotes" runat="server">
                <table width="100%" id="MainBodyTable" cellspacing="0" cellpadding="0" border="0">
                    <%--<tr>
                        <td>
                            <table>--%>
                    <tbody>
                        <tr>
                            <td style="height: 100%" class="LeftCenter">
                            </td>
                            <td style="width: 500px" class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
                                    background-color: White;">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%; background-color: White;" class="TDPart">
                                                <table style="width: 500px" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <div style="left: 1px; width: 527px; position: absolute; top: 0px; height: 18px;
                                                                    background-color: #B5DF82; text-align: left" id="Div2">
                                                                    <b>Paid</b>
                                                                    <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;
                                                                        border: 1px solid #B5DF82;">
                                                                        <asp:Button ID="btnbillnotesclose" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                                            Text="X" OnClientClick="$find('modal1').hide(); return false;"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td style="width: 10%; height: 100%" class="ContentLabel">
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 5px">
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <div class="lbl">
                                                                    <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                    <div style="color: red" id="ErrorDiv" visible="true">
                                                                        <UserMessage:MessageControl ID="usrMessage1" runat="server"></UserMessage:MessageControl>
                                                                        <asp:UpdateProgress ID="UpdatePanel15" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanel11">
                                                                            <progresstemplate>
                                                                                <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                    runat="Server">
                                                                                    <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                    Loading...</div>
                                                                            </progresstemplate>
                                                                        </asp:UpdateProgress>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; height: 18px; text-align: right" colspan="3">
                                                                <asp:Button ID="btnpayemtdelete" OnClick="btnPaymentDelete_Click" runat="server"
                                                                    Text="Delete"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%" class="SectionDevider" colspan="">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="height: 100px; overflow: scroll">
                                                                                <asp:DataGrid ID="grdPaymentTransaction" runat="server" Width="500px" CssClass="GridTable"
                                                                                    AutoGenerateColumns="false" OnItemCommand="grdPaymentTransaction_ItemCommand">
                                                                                    <FooterStyle />
                                                                                    <SelectedItemStyle />
                                                                                    <PagerStyle />
                                                                                    <AlternatingItemStyle />
                                                                                    <ItemStyle CssClass="GridRow" />
                                                                                    <Columns>
                                                                                        <%--0--%>
                                                                                        <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                                                        <%--1--%>
                                                                                        <asp:BoundColumn DataField="I_SOFTWARE_PAYMENT_ID" HeaderText="Payment ID" Visible="false">
                                                                                        </asp:BoundColumn>
                                                                                        <%--2--%>
                                                                                        <asp:BoundColumn DataField="SZ_CHECK_NO" HeaderText="Check Number"></asp:BoundColumn>
                                                                                        <%--3--%>
                                                                                        <asp:BoundColumn DataField="DT_PAYMENT_DATE" HeaderText="Payemnt Date" DataFormatString="{0:MM/dd/yyyy}">
                                                                                        </asp:BoundColumn>
                                                                                        <%--4--%>
                                                                                        <asp:BoundColumn DataField="SZ_PAYMENT_AMOUNT" HeaderText="Amount" DataFormatString="{0:C}"
                                                                                            ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                                                        <%--5--%>
                                                                                        <asp:BoundColumn DataField="SZ_NOTES" HeaderText="Description"></asp:BoundColumn>
                                                                                        <%--6--%>
                                                                                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY ID" Visible="false"></asp:BoundColumn>
                                                                                        <%--7--%>
                                                                                        <asp:TemplateColumn HeaderText="Check">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkscan" runat="server" CausesValidation="false" CommandName="Scan" OnClick="lnkscan_Click"
                                                                                                    Text="Scan">
                                                                                                </asp:LinkButton>/<asp:LinkButton ID="lnkuplaod" runat="server" CausesValidation="false"
                                                                                                    CommandName="Upload" Text="Upload" OnClick="lnkuplaod_Click"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <%--8--%>
                                                                                        <asp:BoundColumn DataField="SZ_INVOICE_PAYMENT_DOCUMENT_LINK" HeaderText="View" Visible="True">
                                                                                        </asp:BoundColumn>
                                                                                        <%--9--%>
                                                                                        <asp:TemplateColumn HeaderText="Delete">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="GridHeader1" />
                                                                                </asp:DataGrid>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;" class="GridHeader1" colspan="3">
                                                                <b>Make Payment</b>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="width: 100%" class="ContentTable" cellspacing="2" cellpadding="3" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="text-align: center" class="ContentLabel" colspan="4" rowspan="1">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Total Amount: </b>
                                                            </td>
                                                            <td style="width: 35%" align="left">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; $
                                                                <asp:Label ID="lblBalance" runat="server" Font-Size="X-Small" Font-Bold="True">
                                                                </asp:Label></td>
                                                            <td style="width: 25%" class="ContentLabel">
                                                            </td>
                                                            <td style="width: 35%" align="left">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%" class="ContentLabel">
                                                                <b>Amount : </b>
                                                            </td>
                                                            <td style="width: 35%">
                                                                &nbsp;
                                                                <asp:TextBox ID="txtChequeAmount" runat="server" Width="58%" MaxLength="50"></asp:TextBox>
                                                                <asp:Label ID="lbldollar" runat="server" CssClass="lbl" Text="$"></asp:Label></td>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Date : </b>
                                                            </td>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <asp:TextBox ID="txtpaymentdate" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                                    MaxLength="10" Width="70%"></asp:TextBox>
                                                                <asp:ImageButton ID="imgpaymnetdate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%" class="ContentLabel">
                                                                <b>Check Number : </b>&nbsp;</td>
                                                            <td style="width: 35%">
                                                                &nbsp;<asp:TextBox ID="txtChequeNumber" runat="server" Width="78%"></asp:TextBox></td>
                                                            <td style="width: 20%" class="ContentLabel">
                                                            </td>
                                                            <td style="width: 35%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtpaymentdate"
                                                                    PopupButtonID="imgpaymnetdate">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%" class="ContentLabel">
                                                                <b>Description : </b>
                                                            </td>
                                                            <td colspan="3">
                                                                &nbsp;&nbsp;<asp:TextBox ID="txtDescription" runat="server" Height="60px" Width="309px"
                                                                    TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="tdAddUpdate" runat="server">
                                                            <td class="ContentLabel" colspan="4">
                                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" Text="Add">
                                                                </asp:Button>
                                                                <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Width="80px"
                                                                    Text="Update"></asp:Button>
                                                                <asp:Button ID="Button2" OnClick="btnCancel_Click" runat="server" Width="80px" Text="Cancel">
                                                                </asp:Button>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <%-- </td>
                    </tr>
                </table>--%>
            </asp:Panel>
            <div style="display: none">
                <asp:LinkButton ID="lbnTest" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <contenttemplate>
            <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: white;
                border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
                <div style="position: relative; text-align: right; background-color: #8babe4;">
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
                                        <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons"
                                            OnClick="btnUploadFile_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </contenttemplate>
        <triggers>
            <asp:PostBackTrigger ControlID="btnUploadFile" />
        </triggers>
    </asp:UpdatePanel>
    <%-- </td>
        </tr>
    </table>--%>
    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
        visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
        position: absolute; top: 682px; height: 100px; background-color: #B5DF82" id="divfrmPatient">
        <div style="position: relative; background-color: #B5DF82; text-align: right">
            <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
        </div>
        <iframe id="frmpatient" src="" frameborder="0" width="500" height="300"></iframe>
    </div>
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtUserId" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtsoftwarefee" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="sztotalamount" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="checkdate" runat="server"></asp:HiddenField>
    <asp:TextBox ID="txtPrev" runat="server" Style="visibility: hidden" Text="0"></asp:TextBox>
    <asp:TextBox ID="txtTransInvoiceId" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtPaymentID" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtsoftwarepaymnetid" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtTransstorageId" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hdnBal" runat="server"></asp:HiddenField>
</asp:Content>
