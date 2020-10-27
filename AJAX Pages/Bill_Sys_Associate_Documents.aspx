<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Associate_Documents.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Associate_Documents"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

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
        
 
        function SetVisitDate()
        {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID %>").value;
            if(selValue == 0)
            {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = "";
                document.getElementById("<%=txtVisitDate.ClientID %>").value = "";
            }
            else if(selValue == 1)
            {
                document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('today');
                document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value = getWeek('endweek');
                   document.getElementById("<%=txtVisitDate.ClientID %>").value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('monthend');
                   document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('quarterend');
                   document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('yearend');
                   document.getElementById("<%=txtVisitDate.ClientID %>").value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value = getLastWeek('endweek');
                   document.getElementById("<%=txtVisitDate.ClientID %>").value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value = lastmonth('endmonth');                   
                   document.getElementById("<%=txtVisitDate.ClientID %>").value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value =lastyear('endyear');
                   document.getElementById("<%=txtVisitDate.ClientID %>").value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById("<%=txtToVisitDate.ClientID %>").value = quarteryear('endquaeter');                   
                   document.getElementById("<%=txtVisitDate.ClientID %>").value =  quarteryear('startquaeter');
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
             

        function CloseDocPopup()
        {
            document.getElementById('divid').style.visibility='hidden';
            document.getElementById('divid').style.zIndex = -1;  
        }
        function OpenReport(obj)
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value = obj;
            //alert(document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value);
            document.getElementById('_ctl0_ContentPlaceHolder1_btnSpeciality').click();
           
            
        }
        function CloseEditProcPopup()
        {
            
            document.getElementById('divid1').style.visibility='hidden';
            document.getElementById('divid1').style.zIndex = -1;
        }
        function CloseDocumentPopup()
        {
            document.getElementById('divid2').style.visibility='hidden';
            document.getElementById('divid2').style.zIndex = -1;
        }
         function showUploadFilePopup()
       {
      
           var flag= false;
           var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdAllReports');
           if(grdProc.rows.length>0)
            {           
                for (var i=1; i<grdProc.rows.length; i++)
                {
                    var cell = grdProc.rows[i].cells[0];
                    for (j=0; j<cell.childNodes.length; j++)
                    {  
                            if (cell.childNodes[j].type =="checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report")
                            {
                                if(cell.childNodes[j].checked)
                                {
                                   flag = true; 
                                   break;
                                }
                            }
                    }
                }
                if(flag==true)
                {
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='100px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'visible';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.position = "absolute";
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.top = '10px';
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.left ='200px';
	            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 
	                document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.height='0px';
                    document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.visibility = 'hidden';  
                //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
                    MA.length = 0;
                }
                else
                {
                    alert("Select procedure code ..");
                }   
            }
       }
        
       function CloseUploadFilePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
       
       
       
        function showEditPopup(caseid,Eventprocid,progroupid,patientid,eventid,speciality,descrption,code,CaseType,patientname,dateofservice,lhrcode,caseno)
       {
       
                document.getElementById("<%=lblCaseTypeEditALL.ClientID %>").innerHTML=CaseType;
                document.getElementById("<%=lblPatientNameEditALL.ClientID %>").innerHTML=patientname;
                 document.getElementById("<%=lblDateofServiceEditALL.ClientID %>").innerHTML=dateofservice; 
                document.getElementById("<%=lblLHRCodeEditALL.ClientID %>").innerHTML=lhrcode; 
                 document.getElementById("<%=lblCasenoEditALL.ClientID %>").innerHTML=caseno;   
                document.getElementById('divid4').style.zIndex = 1;
                document.getElementById('divid4').style.position = 'fixed';
                document.getElementById('divid4').style.left='250px';
                document.getElementById('divid4').style.top='60px';
                document.getElementById('divid4').style.visibility='visible'; 
                document.getElementById('frameeditexpanse1').src ='Bill_Sys_Associate_Documents_Event.aspx?CaseID='+caseid+'&Type=YES&EProcid='+Eventprocid+'&pdid='+progroupid+'&eventid='+eventid+'&spc='+speciality+'&desc='+descrption+'&code='+code+'&patientname='+patientname+'&dateofservice='+dateofservice+'&lhrcode='+lhrcode+'&caseno='+caseno; 
                
            return false;
          
       }
         function ShowDiv()
       {
            document.getElementById('divDashBoard').style.position = 'absolute'; 
            document.getElementById('divDashBoard').style.left= '200px'; 
            document.getElementById('divDashBoard').style.top= '120px'; 
            document.getElementById('divDashBoard').style.visibility='visible'; 
            return false;
       }
       
       
       function ViewDocumentPopup(caseid,Eventprocid,speciality)
       {
       
        document.getElementById('divid2').style.zIndex = 1;
        document.getElementById('divid2').style.position = 'absolute';
        document.getElementById('divid2').style.left= '300px'; 
        document.getElementById('divid2').style.top= '100px';              
        document.getElementById('divid2').style.visibility='visible'; 
         document.getElementById('frm').src ='../Bill_Sys_ViewDocuments.aspx?CaseID='+caseid+'&Type=YES&EProcid='+Eventprocid+'&spc='+speciality;
       }
       
        function Clear()
       {
       // alert("call");
        document.getElementById("<%=txtVisitDate.ClientID%>").value='';
        document.getElementById("<%=txtpatientid.ClientID%>").value='';
        document.getElementById("<%=txtappointmentid.ClientID%>").value='';
        document.getElementById("<%=txtToVisitDate.ClientID %>").value ='';
        document.getElementById("<%=txtNumberOfDays.ClientID %>").value ='';
        document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseType").value = 'NA';
        document.getElementById("<%=ddlDateValues.ClientID %>").value =0;
       }
        function SelectAll(ival)
       {
        
            var f= document.getElementById("<%= grdpaidbills.ClientID %>");		
     
            var str = 1;
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
function CheckSelect()
         {     
              
                              
                var f= document.getElementById("<%=grdpaidbills.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkReason') !=-1)
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
		        else
		        {
		            return true;
		        }
         }
       function showAddReasonPopup()
       {
            
            document.getElementById('divAddReason').style.zIndex = 1;
            document.getElementById('divAddReason').style.position = 'fixed';
            document.getElementById('divAddReason').style.left= '300px'; 
            document.getElementById('divAddReason').style.top= '150px';              
            document.getElementById('divAddReason').style.visibility='visible'; 
            document.getElementById('frameAddReason').src ='Bill_Sys_AddUnBilledReason.aspx';  
            return false;
          
       }
       function ShowNotesPopup(Eventprocid)
       {
       
        document.getElementById('divViewReason').style.zIndex = 1;
        document.getElementById('divViewReason').style.position = 'fixed';
        document.getElementById('divViewReason').style.left= '300px'; 
        document.getElementById('divViewReason').style.top= '100px';              
        document.getElementById('divViewReason').style.visibility='visible'; 
         document.getElementById('IframeViewReason').src ='Bill_Sys_ViewUnBilledReason.aspx?Eventprocid='+Eventprocid;
       }
     
    function CloseDocSignaturePopup()
    {
        document.getElementById('divViewReason').style.visibility='hidden';
        document.getElementById('divViewReason').style.zIndex = -1;  
    }

    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 60%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                    <%--<tr>
                        <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%" colspan="3">
                            <asp:Label Font-Bold="true" Font-Size="Small" ID="txtHeading" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td height="28" align="left" valign="middle" colspan="3">
                            <table width="40%" style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82;
                                border-bottom: 1px solid #B5DF82">
                                <tr>
                                    <td height="28" style="background-color: #B5DF82;" class="txt2" colspan="3">
                                        <b class="txt3">Search Parameters</b></td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblvisitdate" runat="server" Text=" Visit Date" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblfrom" runat="server" Text=" From" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblto" runat="server" Text=" To" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="90%">
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
                                    <td>
                                        <asp:TextBox ID="txtVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="76%"></asp:TextBox>
                                        <asp:ImageButton ID="imgVisit" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtVisitDate"
                                            PopupButtonID="imgVisit" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            MaxLength="10" Width="70%"></asp:TextBox>
                                        <asp:ImageButton ID="imgVisite1" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToVisitDate"
                                            PopupButtonID="imgVisite1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblCaseType" runat="server" Text="Case Type" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblNoOfDays" runat="server" Text="Number Of Days (>=)" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblpatientId" runat="server" Text="Patient Id" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100%" Selected_Text="---Select---"
                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                            CssClass="search-input"></extddl:ExtendedDropDownList>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtNumberOfDays" runat="server" onkeypress="return CheckForInteger(event,'')"
                                            MaxLength="4"></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtpatientid" runat="server" Width="98%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblappointmentid" runat="server" Text="Appointment Id" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblUnBilledReason" runat="server" Text="UnBilled Reason" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtappointmentid" runat="server" Width="98%"></asp:TextBox>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <extddl:ExtendedDropDownList ID="ExtUnBilledReason" runat="server" Width="130%" Selected_Text="---Select---"
                                            Procedure_Name="SP_GET_UNBILLIED_EXT_VALUES" Flag_Key_Value="GET_UNBILLIED_EXT"
                                            Connection_Key="Connection_String" CssClass="search-input"></extddl:ExtendedDropDownList>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                    </td>
                                </tr>

                                      <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:Label ID="lblCaseNo" runat="server" Text="Case#" CssClass="td-widget-bc-search-desc-ch"></asp:Label>
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                      
                                    </td>
                                    <td class="td-widget-bc-search-desc-ch">
                                    </td>
                                </tr>
                                  <tr>
                                    <td class="td-widget-bc-search-desc-ch">
                                        <asp:TextBox ID="txtCaseNo" runat="server" Width="98%"></asp:TextBox>
                                    </td>
                                  </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:UpdatePanel ID="pnlprogress" runat="server">
                                            <ContentTemplate>
                                                <%-- <asp:UpdateProgress ID="UpdateProgress123" runat="server" DisplayAfter="0">
                                                    <progresstemplate>
                                                        <asp:Image ID="img123" runat="server" style="position: fixed; z-index:1; left: 400px; top: 200px" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
                                                        Height="60px" Width="60px"></asp:Image>
                                                        </progresstemplate>
                                                </asp:UpdateProgress>--%>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearchvisit_Click" />
                                                <input style="width: 80px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                                    runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99%" colspan="3">
                            <table width="40%">
                            </table>
                            <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align: left; height: 25px;" colspan="4">
                                        <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server" visible="false">Dash
                                            board</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; height: 25px;" colspan="4">
                                        <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; height: 25px;" colspan="4">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="ReceivedReport" runat="server">
                        <td style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <asp:UpdatePanel ID="ReportUpdate" runat="server">
                                <contenttemplate>
                                        
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                
                              
                                <tr>
                                    <td style="width: 99%" class="SectionDevider" colspan="2">
                                        <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <table style="vertical-align: middle; width: 100%">
                                            <tbody>
                                            <tr>
                                            <td colspan="4" align="center">
                                          
                                            </td>
                                            </tr>
                                            <tr align="right">
                                             <td style="vertical-align: middle; width: 30%" align="right" colspan="4">
                                             
                                             </td>
                                            </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 30%" id="Td1" runat="server" align="left">
                                                       &nbsp;
                                                        <gridsearch:XGridSearchTextBox ID="txtSearchBox1" Visible="false" runat="server" AutoPostBack="true"
                                                            CssClass="search-input">
                                                        </gridsearch:XGridSearchTextBox>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 10%" align="right">
                                                   
                                                    </td>
                                                    <td style="vertical-align: middle; width: 60%; text-align: right" id="Exceltd" runat="server"
                                                        align="right" colspan="2">
                                                        <asp:Button ID="btnAddReason" runat="server" Text="Change Visit Status" Width="120px" OnClick="btnAddReason_Click" />
                                                 
                                                     <asp:Button ID="btnChangeCaseType" runat="server" Text="Change CaseType" Width="120px" OnClick="btnChangeCaseType_Click" />
                                                        Record Count:<%=grdpaidbills.RecordCount%>
                                                        | Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="con1" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                        <asp:LinkButton ID="lnkExportToExcel1" runat="server" OnClick="lnkExportTOExcel_onclick"
                                                            Text="Export TO Excel">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                             
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        
                                        <xgrid:XGridViewControl ID="grdpaidbills" runat="server" Height="148px" Width="100%"
                                            CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="100"
                                            DataKeyNames=" SZ_PROC_CODE,SZ_CASE_ID,SZ_PATIENT_ID,PATIENT_NAME,DT_DATE_OF_SERVICE,I_EVENT_ID,I_EVENT_PROC_ID,SZ_DOCTOR_ID,CASE_NO,DT_EVENT_DATE,SZ_PROCEDURE_GROUP_ID,SZ_CODE_DESCRIPTION,sz_procedure_group,BT_BILL_STATUS"
                                            XGridKey="Bill_Sys_Associate_Documents" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                            ExportToExcelFields="CASE_NO,PATIENT_NAME,DT_DATE_OF_SERVICE,SZ_CODE,SZ_CODE_DESCRIPTION,SZ_CASE_TYPE_NAME,SZ_LHR_CODE,I_NO_OF_DAYS,sz_remote_case_id,sz_remote_appointment_id" 
                                            ExportToExcelColumnNames="Case #,Patient Name,Date of Service,Procedure Code,Description,Case Type,LHR Code,No. of Days, Patient Id,Appointment Id" ShowExcelTableBorder="true"
                                            ExcelFileNamePrefix="DoctorChange" HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1"
                                            EnableRowClick="false" MouseOverColor="0, 153, 153" AutoGenerateColumns="false"
                                            OnRowCommand="grdpaidbills_RowCommand" OnRowEditing="grdpaidbills_RowEditing">
                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                            <Columns>
                                                <%--0--%>
                                              <asp:TemplateField Visible="false">
                                                    <itemtemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Text=" " Visible="false"/>
                                            </itemtemplate>
                                                </asp:TemplateField>
                                                <%--1--%>
                                               <asp:BoundField DataField="CHART_NO" HeaderText="Chart#" SortExpression="MST_PATIENT.I_RFO_CHART_NO" Visible="false"></asp:BoundField>
                                                <%--2--%>
                                                <asp:BoundField DataField="CASE_NO" HeaderText="Case#" SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_NO)" ></asp:BoundField>
                                               
                                                <%--3--%>
                                                <asp:BoundField DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundField>
                                                <%--4--%>
                                                <asp:BoundField DataField="SZ_PATIENT_ID" HeaderText="SZ_PATIENT_ID" Visible="false"
                                                    SortExpression=""></asp:BoundField>
                                                <%--5--%>
                                                <asp:BoundField DataField="PATIENT_NAME" HeaderText="PATIENT_NAME" Visible="false"></asp:BoundField>
                                                <%--6--%>
                                                <asp:BoundField DataField="DT_DATE_OF_SERVICE" HeaderText="DT_DATE_OF_SERVICE" Visible="false">
                                                </asp:BoundField>
                                                <%--7--%>
                                          <%--      <asp:TemplateField HeaderText="" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME"  >
                                                    <headertemplate  >
                                                           <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PATIENT_NAME" CommandArgument="PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </headertemplate>
                                                    <itemtemplate>
                                                         <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>' Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  CommandName="workarea"></asp:LinkButton>   
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                --%>
                                                <asp:BoundField DataField="PATIENT_NAME" HeaderText="Patient Name" Visible="true" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" >
                                                </asp:BoundField>
                                                
                                                
                                                    <%-- <asp:TemplateField HeaderText="Patient Name" Visible="true" SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" >
                                     <itemtemplate>
                                                 <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>' Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  CommandName="workarea"></asp:LinkButton>   
                                               
                                     </itemtemplate>
                                     </asp:TemplateField>--%>
                                                <%--8--%>
                                                <asp:BoundField DataField="DT_EVENT_DATE" HeaderText="Date Of Service" Visible="true"  SortExpression="TXN_CALENDAR_EVENT.DT_EVENT_DATE"  DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" >
                                                </asp:BoundField>
                                                <%--<asp:TemplateField HeaderText="Date Of Service" Visible="true" SortExpression="TXN_CALENDAR_EVENT.DT_EVENT_DATE" >
                                     <itemtemplate>
                                                 <asp:LinkButton ID="lnkSelectdate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>' Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  CommandName="appointment" ></asp:LinkButton>   
                                               
                                     </itemtemplate>
                                     </asp:TemplateField>--%>
                                                 <%--<asp:TemplateField HeaderText=""  SortExpression="convert(nvarchar,TXN_CALENDAR_EVENT.DT_EVENT_DATE,106)">
                                                    <headertemplate>
                                                           <asp:LinkButton ID="lnkdateservice" runat="server" CommandName="DT_DATE_OF_SERVICE" CommandArgument="DT_DATE_OF_SERVICE" Font-Bold="true" Font-Size="12px">Date Of Service</asp:LinkButton>
                                                    </headertemplate>
                                                    <itemtemplate>
                                                         <asp:LinkButton ID="lnkSelectdate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DT_DATE_OF_SERVICE")%>' Visible="true" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  CommandName="appointment" ></asp:LinkButton>   
                                                    </itemtemplate>
                                                </asp:TemplateField>--%>
                                                <%--9--%>
                                                <asp:BoundField DataField="SZ_CODE" HeaderText="Procedure code" SortExpression=" MST_BILL_PROC_TYPE.SZ_TYPE_CODE" ></asp:BoundField>
                                                <%--10--%>
                                                <asp:BoundField DataField="SZ_CODE_DESCRIPTION" HeaderText="Description" SortExpression="mst_bill_proc_type.SZ_TYPE_DESCRIPTION"></asp:BoundField>
                                                <%--11--%>
                                                <asp:BoundField DataField="SZ_STATUS" HeaderText="Status" Visible="false"></asp:BoundField>
                                                <%--12--%>
                                                <asp:BoundField DataField="SZ_COMPANY_ID" HeaderText="CompanyID" Visible="false"></asp:BoundField>
                                                <%--13--%>
                                                <asp:BoundField DataField="I_EVENT_PROC_ID" HeaderText="EventProcID" Visible="false">
                                                </asp:BoundField>
                                                <%--14--%>
                                                <asp:BoundField DataField="SZ_PROC_CODE" HeaderText="Pro Code" Visible="false" ></asp:BoundField>
                                                <%--15--%>
                                                <asp:BoundField DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false"></asp:BoundField>
                                                <%--16--%>
                                                <asp:BoundField DataField="CASE_NO" HeaderText="Case No." Visible="false"></asp:BoundField>
                                                <%--17--%>
                                                <asp:BoundField DataField="DT_EVENT_DATE" HeaderText="Case No." Visible="false"></asp:BoundField>
                                                <%--18--%>
                                                <asp:BoundField DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                                    Visible="false"></asp:BoundField>
                                                <%--19--%>
                                                <asp:BoundField DataField="sz_procedure_group" HeaderText="sz_procedure_group" Visible="false">
                                                </asp:BoundField>
                                                <%--20--%>
                                                <asp:BoundField DataField="I_EVENT_ID" HeaderText="sz_procedure_group" Visible="false">
                                                </asp:BoundField>
                                                <%--23--%>
                                                <asp:BoundField DataField="SZ_LHR_CODE" HeaderText="LHR Code" Visible="true" SortExpression="(select isnull(sz_remote_procedure_desc,'''') from txn_visit_document where i_Event_id=txn_calendar_event.i_event_id and i_event_proc_id=txn_calender_event_prpcedure.i_event_proc_id)">
                                                </asp:BoundField>
                                                <%--22--%>
                                                 <asp:TemplateField HeaderText="">
                                                    <itemtemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text='Associates Documents' CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Edit"></asp:LinkButton>
                                                        </itemtemplate>
                                                </asp:TemplateField>
                                                <%--23--%>
                                                <asp:BoundField DataField="I_NO_OF_DAYS" HeaderText="No. of Days" Visible="true"
                                                 SortExpression="(DATEDIFF(dd,txn_calendar_event.dt_event_date,getdate())+1)">
                                                </asp:BoundField>
                                                
                                                <%--24--%>
                                                <asp:BoundField DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" Visible="true"
                                                 SortExpression="(select isnull( SZ_CASE_TYPE_NAME,'''') from MST_CASE_TYPE where SZ_CASE_TYPE_ID in(select SZ_CASE_TYPE_ID from  MST_CASE_MASTER where SZ_CASE_ID= TXN_CALENDAR_EVENT.SZ_CASE_ID))">
                                                </asp:BoundField>
                                                 <%--25--%>
                                               <asp:BoundField DataField="SZ_REASON_STATUS" HeaderText="STATUS"></asp:BoundField>
                                                <%--26--%>
                                                  <asp:TemplateField HeaderText="View">
                                                    <itemtemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_UNBILLABLE_REASON")%>
                                                   </itemtemplate>
                                                </asp:TemplateField>
                                                 <%--27--%>
                                                 <asp:BoundField DataField="BT_BILL_STATUS" HeaderText="BT_BILL_STATUS" Visible="false">
                                                </asp:BoundField>
                                               <%--28--%>
                                               <asp:TemplateField HeaderText="">
                                                <headertemplate>
                                              <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                          </headertemplate>
                                                <itemtemplate>
                                             <asp:CheckBox ID="ChkReason" runat="server" />
                                            </itemtemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </xgrid:XGridViewControl>
                                        
                                        
                                    </td>
                                </tr>
                            </table>
                           
                          
                           <div id="divid4"   style="position:absolute; width: 900px; height: 400px; background-color: #DBE6FA;
                                visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
                                border-left: silver 1px solid; border-bottom: silver 1px solid;" >
                                <div style="position: relative; background-color: #B5DF82;  width: 900px; text-align:center">
                                <table width="100%">
                                <tr>
                                <td align="center">
                                  <asp:Label id="Label2" runat="server" Text="Patient Name -" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label> <asp:Label id="lblPatientNameEditALL" runat="server" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                 <asp:Label id="Label3" runat="server" Text="Date -" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label> <asp:Label id="lblDateofServiceEditALL" runat="server" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                 <asp:Label id="Label4" runat="server" Text="LHR Code -" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label> <asp:Label id="lblLHRCodeEditALL" runat="server" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                 <asp:Label id="Label1" runat="server" Text="Case Type -" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label> <asp:Label id="lblCaseTypeEditALL" runat="server" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label>&nbsp;&nbsp;<b>|</b>&nbsp;&nbsp;
                                  <asp:Label id="Label5" runat="server" Text="Case # -" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label> <asp:Label id="lblCasenoEditALL" runat="server" ForeColor="red" Font-Bold="true" Font-Size="12px"  ></asp:Label>
                                </td>
                                <td align="right">
                                 <asp:Button ID="txtUpdate4" Text="X" BackColor="#B5DF82" BorderStyle="None"  runat="server" OnClick="txtUpdate_Click" />
                                </td>
                                </tr>
                                </table>
                              
                                
                               
                            </div>
                                <iframe id="frameeditexpanse1" src="" frameborder="0" height="400px" width="900px"></iframe>
                            </div>
                          

                            </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divAddReason" style="position: absolute; width: 600px; height: 200px; background-color: white;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82; width: 600px;">
            <asp:Button ID="txtAddReason" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                OnClick="txtAddReason_Click" />
        </div>
        <iframe id="frameAddReason" src="" frameborder="0" height="200px" width="600px"></iframe>
    </div>
    <div id="divViewReason" style="position: absolute; width: 600px; height: 200px; background-color: white;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82; width: 600px;">
            <a onclick="CloseDocSignaturePopup()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="IframeViewReason" src="" frameborder="0" height="200px" width="600px"></iframe>
    </div>
    <asp:HiddenField ID="hdnSpeciality" runat="server" />
    <asp:TextBox ID="txtCompanyid" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtireortreceive" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtVisit" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtDiagnosisSetID" runat="server" Visible="false" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtAOb" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtReport" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtReferral" runat="server" Visible="false"></asp:TextBox>
    <asp:Image ID="img2" Visible="false" Style="position: absolute; left: 50%; top: 86%"
        runat="server" ImageUrl="~/AJAX Pages/Images/loading_transp.gif" AlternateText="Loading....."
        Height="60px" Width="60px"></asp:Image>
    <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0">
        <progresstemplate>
<asp:Image ID="img1" runat="server" style="position: fixed; z-index:1; left: 50%; top: 50%" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
Height="60px" Width="60px"></asp:Image>
</progresstemplate>
    </asp:UpdateProgress>
</asp:Content>
