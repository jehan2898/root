<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Billing_Report.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Billing_Report" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>



    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
    
    
//     function CloseModalPopup()
//    {
//      var modal = $find('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
//      modal.hide();
//    }
    

    
    
    
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
        
        
        function checkReportStatus(iStatus)
        {
            if(iStatus=="0")
            {  alert('your documents are not received');
                return false
            }else
            {
                return true;
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
         
         
        function OpenPopUPDisplayDiagCode(p_szCaseID,p_szSpeciality)
        {
        
            document.getElementById("divDisplayDiagCode").style.position = "absolute";
            document.getElementById("divDisplayDiagCode").style.left= "300px";
            document.getElementById("divDisplayDiagCode").style.top= "200px";
            document.getElementById('divDisplayDiagCode').style.zIndex= '1'; 
            document.getElementById("divDisplayDiagCode").style.visibility="visible";
            document.getElementById("divDisplayDiagCode").style.width= "400px";
            document.getElementById("divDisplayDiagCode").style.height= "250px";
            document.getElementById("ifrmDisplayDiagCode").style.width= "400px";
            document.getElementById("ifrmDisplayDiagCode").style.height= "250px";
            document.getElementById("ifrmDisplayDiagCode").src="Bill_Sys_Test_PopupShowDiagnosisCode.aspx?P_SZ_CASE_ID=" + p_szCaseID + "&P_SZ_Speciality="+p_szSpeciality;
           
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
            var selValue = document.getElementById('<%= ddlDateValues.ClientID %>').value;
            if(selValue == 0)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = "";
                   document.getElementById('<%= txtFromDate.ClientID %>').value = "";
                   
            }
            else if(selValue == 1)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('today');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getWeek('endweek');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('monthend');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('quarterend');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getDate('yearend');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById('<%= txtToDate.ClientID %>').value = getLastWeek('endweek');
                   document.getElementById('<%= txtFromDate.ClientID %>').value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById('<%= txtToDate.ClientID %>').value = lastmonth('endmonth');
                   
                   document.getElementById('<%= txtFromDate.ClientID %>').value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById('<%= txtToDate.ClientID %>').value =lastyear('endyear');
                   
                   document.getElementById('<%= txtFromDate.ClientID %>').value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById('<%= txtToDate.ClientID %>').value = quarteryear('endquaeter');
                   
                   document.getElementById('<%= txtFromDate.ClientID %>').value =  quarteryear('startquaeter');
            }
         }
        
        
        
         function checkExistingBills(objCaseType,objBillNumber,objSpeciality)
       {
            
              
                    if(objCaseType=="WC000000000000000001")
                    {
                        document.getElementById('<%= hdnWCPDFBillNumber.ClientID %>').value=objBillNumber
                        document.getElementById('<%= hdnSpeciality.ClientID %>').value=objSpeciality;
                        
                        showPDFWorkerComp();
                        return false;
                    }
                    else if(objCaseType=="WC000000000000000002")
                    {
                        return true;
                    }
                
                
            
         
       }
    
       
       function showPDFWorkerComp()
        {
        document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.height='180px';
        document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.visibility = 'visible';
        document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.position = "absolute";
	    document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.top = '200px';
	    document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.left ='650px';
        }
        function ClosePDFWorkerComp()
       {
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.height='0px';
            document.getElementById('<%= pnlPDFWorkerComp.ClientID %>').style.visibility = 'hidden';  
       }
        function CheckDetails(claimNo,Inscmp,InsAdd)
        {
        // alert(document.getElementById("<%=hfFlag.ClientID%>").value);
  
        document.getElementById("<%=hfFlag.ClientID%>").value = "0";
       
             if(Inscmp==''||Inscmp=='&nbsp;'||InsAdd==''||InsAdd=='&nbsp;')
             {
            
                if(claimNo==''||claimNo=='&nbsp;')
                {
              
                     
                     
                     document.getElementById('<%=hfFlag.ClientID%>').value = "0";
                     alert("You do not have an insurance company or  an insurance address or a claim number added to this patient.");                
                     
                     
                   return false;
                }else
                {
                   
                    document.getElementById('<%=hfFlag.ClientID%>').value = "0";
                     alert("You do not have an insurance company or  an insurance address to this patient.");                
                    
                  return false
                }
             }else if(claimNo==''||claimNo=='&nbsp;')
             {
                 var v_Confirm = confirm("You do not have a claim number added to this patient. Do you still want to create a bill? [Ok / Cancel]");                
                            if(v_Confirm==true)
                            {
                                document.getElementById("<%=hfFlag.ClientID%>").value = "1";
                                return true
                            }
                            else
                            {
                                document.getElementById('<%=hfFlag.ClientID%>').value = "0";
                                return false;
                            }
             }else  if(Inscmp!=''&& Inscmp!='&nbsp;' && InsAdd!='' && InsAdd!='&nbsp;'&& claimNo!=''&& claimNo!='&nbsp;')
             {
            
                document.getElementById("<%=hfFlag.ClientID%>").value = "1";
                
                return true;  
             }
        }
    </script>

    <%--
  <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                                               <contenttemplate>--%>
    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '<%= btnSearch.ClientID %>')">
        <table style="width: 100%; height: 100%" id="First" cellspacing="0" cellpadding="0"
            border="0">
            <tbody>
                <tr>
                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; vertical-align: top;
                        width: 100%; padding-top: 3px; height: 100%">
                        <table style="width: 100%" id="MainBodyTable" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td class="LeftTop">
                                    </td>
                                    <td class="CenterTop">
                                    </td>
                                    <td class="RightTop">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%" class="LeftCenter">
                                    </td>
                                    <td class="Center" valign="top">
                                        <table style="width: 100%; height: 100%" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 100%" class="TDPart">
                                                        <table style="width: 100%" class="ContentTable" cellspacing="0" cellpadding="3" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="ContentLabel" colspan="3" style="height: 25px; text-align: left">
                                                                        <asp:UpdatePanel ID="UpdatePanelp" runat="server">
                                                                            <ContentTemplate>
                                                                                <UserMessage:MessageControl runat="server" ID="usrMessage1" EnableTheming="true" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px; text-align: left" class="ContentLabel" colspan="3">
                                                                        <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                                        <div style="color: red" id="ErrorDiv" visible="true">
                                                                        </div>
                                                                        <b>Billing Report </b>
                                                                        <asp:Label ID="lblHeader" runat="server" Visible="false"></asp:Label>&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <%--    <tr>
                                                                    <td class="ContentLabel" style="width: 182px">
                                                                    </td>
                                                                    <td style="width: 38%">
                                                                        
                                                                    </td>
                                                                    <td style="width: 38%">
                                                                    </td>
                                                                    <td class="ContentLabel" style="width: 12%">
                                                                    </td>
                                                                    <td style="width: 38%">
                                                                    </td>
                                                                </tr>--%>
                                                                <tr>
                                                                    <td class="ContentLabel" style="text-align: left;" colspan="3">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="28%" class="lbl" align="left" style="height: 22px">
                                                                        Schedule Date:
                                                                        <asp:DropDownList ID="ddlDateValues" runat="Server" Width="34%">
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
                                                                    <td style="width: 32%; height: 22px;" class="lbl" align="left">
                                                                        &nbsp; From Date:<asp:TextBox ID="txtFromDate" onkeypress="return CheckForInteger(event,'/')"
                                                                            runat="server" MaxLength="10"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                    </td>
                                                                    <td style="width: 32%; height: 22px;" class="lbl" align="left">
                                                                        <%--  &nbsp; To Date: <asp:TextBox ID="txtToDate" onkeypress="return CheckForInteger(event,'/')" runat="server" MaxLength="10" CssClass="text-box"></asp:TextBox>  <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>--%>
                                                                        &nbsp; To Date: &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:TextBox ID="txtToDate" Width="60%" onkeypress="return CheckForInteger(event,'/')"
                                                                            runat="server" MaxLength="10"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 32%" class="lbl">
                                                                          Doctor Name&nbsp: &nbsp;
                                                                         <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="58%" Connection_Key="Connection_String"
                                                                            Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                                    </td>
                                                                    <td class="lbl" width="29%" align="left">
                                                                        &nbsp;Speciality &nbsp;:
                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="146px" Selected_Text="---Select---"
                                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"  Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                    </td>
                                                                    <td class="lbl" align="left" width="29%">
                                                                        &nbsp; Case Type:
                                                                        <extddl:ExtendedDropDownList ID="extddlCaseType" Width="149px" runat="server" Connection_Key="Connection_String"
                                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                            OldText="" StausText="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ContentLabel" colspan="4">
                                                                        <asp:TextBox ID="txtBillDate" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtRefCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCaseNo" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtReadingDocID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"> </asp:TextBox>
                                                                        <asp:TextBox ID="txtCaseID1" runat="server" Width="10px" Visible="False"> </asp:TextBox>
                                                                        <asp:TextBox ID="txtPGID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtSpeciality" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtEvenId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtEventProc" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtClaimInsurance" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                         <asp:TextBox ID="txtDoctorID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                         
                                                                        
                                                                        <%-- <asp:TextBox ID="txtInsuranceCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                         <asp:TextBox ID="txtInsuranceCompanyAddress" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                         <asp:TextBox ID="txtClaimNO" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                         --%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" width="100%" align="right">
                                                                        <table>
                                                                            <tr>
                                                                                <td class="ContentLabel" colspan="1">
                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="80px"
                                                                                                CssClass="Buttons" Text="Search"></asp:Button>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="btnExportToExcel" OnClick="btnExportToExcel_Click" runat="server"
                                                                                        CssClass="Buttons" Text="Export To Excel"></asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px; width: 182px;" class="lbl" align="left" colspan="1">
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                            <ContentTemplate>
                                                                                <b>Total Count:</b>
                                                                                <asp:Label ID="lblCount" runat="server"></asp:Label>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                            <ProgressTemplate>
                                                                                <div id="DivStatus2" runat="Server" class="PageUpdateProgress">
                                                                                    <asp:Image ID="ajaxLoadNotificationImage2" runat="server" AlternateText="[image]"
                                                                                        ImageUrl="~/AJAX Pages/Images/ajax-loader.gif" />
                                                                                    <label id="Label2" class="PageLoadProgress">
                                                                                        <b>Processing, Please wait...</b></label>
                                                                                </div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" class="TDPart">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div style="overflow: scroll; height: 600px">
                                                                     <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" OnItemCommand="grdAllReports_ItemCommand" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            
                                                <%-- 0 --%>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case No" Visible="false" ></asp:BoundColumn> 
                                                <%-- 1 --%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="SZ_DOCTOR_ID" Visible="false"></asp:BoundColumn>                                           
                                                <%-- 2 --%>
                                              
                                                 <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                <asp:LinkButton ID="lnkCaseSearch" runat="server" CommandName="CASENO" CommandArgument="SORT_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                              
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 
                                                <%-- 3 --%>
                                                <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PatientNameSearch" CommandArgument="PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container,"DataItem.PATIENT_NAME")%>
                                              
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <%-- 4 --%>
                                                <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                  <asp:LinkButton ID="lnkEventDateSearch" runat="server" CommandName="EventDateSearch" CommandArgument="SORT_DATE"
                                                            Font-Bold="true" Font-Size="12px">Date Of Visit</asp:LinkButton>
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container,"DataItem.DT_EVENT_DATE")%>
                                               <%-- <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>--%>
                                                </ItemTemplate>
                                                
                                                </asp:TemplateColumn>
                                                
                                                  <%-- 5 --%>     
                                                <asp:BoundColumn DataField="SZ_VISIT" HeaderText="Visit Type"></asp:BoundColumn>
                                                
                                                <%-- 6 --%>                                               
                                                <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                <asp:LinkButton ID="lnkDoctorNameSearch" runat="server" CommandName="DoctorNameSearch" CommandArgument="DOCTOR_NAME"
                                                            Font-Bold="true" Font-Size="12px">Doctor Name</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container,"DataItem.DOCTOR_NAME")%>
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <%-- 7 --%>
                                                    <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                 <asp:LinkButton ID="lnkCastTypeSearch" runat="server" CommandName="CasetypeSearch" CommandArgument="CASE_TYPE"
                                                            Font-Bold="true" Font-Size="12px">Case Type</asp:LinkButton>
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                  <%# DataBinder.Eval(Container, "DataItem.CASE_TYPE")%>
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <%-- 8 --%>
                                                <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                 <asp:LinkButton ID="lnkSpecialitySearch" runat="server" CommandName="SpecialitySearch" CommandArgument="Speciality"
                                                            Font-Bold="true" Font-Size="12px">Speciality</asp:LinkButton>
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                  <%# DataBinder.Eval(Container,"DataItem.Speciality")%>
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <%-- 9 --%>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name" Visible="false" ></asp:BoundColumn>
                                              
                                                <%-- 10 --%>
                                                <asp:BoundColumn DataField="sz_case_no" HeaderText="Case No." Visible="false"></asp:BoundColumn>
                                             
                                                <%-- 11 --%>
                                                <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="patient id" Visible="false"></asp:BoundColumn>
                                             
                                                 <%-- 12 --%>
                                             
                                                 <asp:BoundColumn DataField="NO_OF_DAYS" HeaderText="No Of Days"></asp:BoundColumn>
                                                
                                                                        <%-- 13 --%>     
                                                 <asp:TemplateColumn HeaderText="Edit Information">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkAddDiagCode" runat="server" Text="Edit" CommandName="ShowPop"> </asp:LinkButton>
                                                                                    <%--   <asp:LinkButton ID="lnkDisplayDiagCode" runat="server" Text="" CommandName="Display Diag Code"
                                                                                Visible="false">Show(<%# DataBinder.Eval(Container,"DataItem.DIAG COUNT")%>)</asp:LinkButton>
                                                                    <a href="#" onclick="javascript:OpenPopUPDisplayDiagCode('<%# DataBinder.Eval(Container,"DataItem.CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SPECIALIYT")%>')">                                                                                Show(<%# DataBinder.Eval(Container,"DataItem.DIAG COUNT")%>)</a>--%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateColumn>
                                                
                                                <%-- 14 --%>
                                                <asp:TemplateColumn HeaderText="Add Bill">
                                                     <ItemTemplate>
                                                         
                                                        <asp:LinkButton ID="lnkAddBill" runat="server" Text="Add"  CommandName="AddBill" Visible="true"  ></asp:LinkButton> 
                                                        <%-- <a runat="server" href="#"  onclick="javascript:CheckDetails('<%# DataBinder.Eval(Container,"DataItem.CLAIM_NUMBER")%>','<%# DataBinder.Eval(Container,"DataItem.Insurance_Company")%>','<%# DataBinder.Eval(Container,"DataItem.Insurance_Address")%>')">Add</a>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                
                                                 <%--15--%>
                                                <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="I_EVENT_ID" Visible="false"></asp:BoundColumn>
                                                
                                                 <%--16--%>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="DT_EVENT_DATE" Visible="false"></asp:BoundColumn>
                                                <%--17--%>
                                                <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="DOCTOR_NAME" Visible="false"></asp:BoundColumn>
                                                <%--18--%>
                                                <asp:BoundColumn DataField="Speciality" HeaderText="Speciality" Visible="false"></asp:BoundColumn>
                                                
                                                   <%--19--%>
                                                <asp:BoundColumn DataField="CLAIM_NUMBER" HeaderText="Claim Number" Visible="false"></asp:BoundColumn>
                                                
                                                   <%--20--%>
                                                <asp:BoundColumn DataField="Insurance_Company" HeaderText="Inscmp" Visible="false"></asp:BoundColumn>
                                                   <%--21--%>
                                                <asp:BoundColumn DataField="Insurance_Address" HeaderText="InsAdd" Visible="false"></asp:BoundColumn>
                                                   <%--22--%>
                                                <asp:BoundColumn DataField="Speciality_ID" HeaderText="sz_pgid" Visible="false"></asp:BoundColumn>
                                               
                                                 <%--23--%>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doct" Visible="false"></asp:BoundColumn>
                                                 <%--24--%>
                                                <asp:BoundColumn DataField="PATIENT_ID" HeaderText="patientid" Visible="false"></asp:BoundColumn>
                                               
                                                
                                                
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlPDFWorkerComp" runat="server" Style="background-color: white; border-color: ThreeDFace;
                                                                                border-width: 1px; border-style: solid; visibility: hidden;">
                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                                                                                    class="TDPart">
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <a onclick="ClosePDFWorkerComp();" style="cursor: pointer;" title="Close">X</a>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top" style="text-align: left;" class="ContentLabel">
                                                                                            <asp:RadioButtonList ID="rdbListPDFType" runat="server">
                                                                                                <asp:ListItem Value="1" Text="Doctor's Initial Report" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Value="2" Text="Doctor's Progress Report"></asp:ListItem>
                                                                                                <asp:ListItem Value="3" Text="Doctor's Report Of MMI"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top" align="center">
                                                                                            <asp:Button ID="btnGenerateWCPDF" runat="server" Text="Generate PDF" OnClick="btnGenerateWCPDF_Click"
                                                                                                CssClass="Buttons" />
                                                                                            <asp:HiddenField ID="hdnWCPDFBillNumber" runat="server" />
                                                                                            <asp:HiddenField ID="hdnSpeciality" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:DataGrid ID="grdForReport" runat="server" Width="100%" Visible="false" CssClass="GridTable"
                                                            AutoGenerateColumns="false">
                                                            <ItemStyle CssClass="GridRow" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_VISIT" HeaderText="Visite Type"></asp:BoundColumn>
                                                                
                                                                <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="Doctor Name" Visible="true">
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Speciality" HeaderText="Speciality" DataFormatString="{0:MM/dd/yyyy}">
                                                                </asp:BoundColumn>
                                                                
                                                                <asp:BoundColumn DataField="NO_OF_DAYS" HeaderText="No of days"></asp:BoundColumn>
                                                            </Columns>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                        </asp:DataGrid>
                                                        <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" PopupButtonID="imgbtnToDate"
                                                            TargetControlID="txtToDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" PopupButtonID="imgbtnFromDate"
                                                            TargetControlID="txtFromDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftBottom">
                                    </td>
                                    <td class="CenterBottom">
                                    </td>
                                    <td style="width: 10px" class="RightBottom">
                                    <asp:HiddenField ID="hfFlag" runat="server" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanelforpopup" runat="server">
        <ContentTemplate>
<DIV style="DISPLAY: none"><asp:LinkButton id="lnkbtnpopup" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton> </DIV><ajaxToolkit:ModalPopupExtender id="ModalPopupExtender1" runat="server" TargetControlID="lnkbtnpopup" PopupDragHandleControlID="divHeader" PopupControlID="ShowDiagno" X="300" Y="150">
            </ajaxToolkit:ModalPopupExtender> <asp:Panel style="BORDER-RIGHT: lightgrey thick solid; BORDER-TOP: lightgrey thick solid; DISPLAY: none; BORDER-LEFT: lightgrey thick solid; BORDER-BOTTOM: lightgrey thick solid" id="ShowDiagno" runat="server" Height="100%" Width="52%"><DIV style="VERTICAL-ALIGN: text-top; WIDTH: 100%; HEIGHT: 25px; BACKGROUND-COLOR: lightgrey; TEXT-ALIGN: right" id="divHeader"><asp:Button id="btnClose" onclick="btnClose_Click" runat="server" Width="3%" Text="X" CssClass="Buttons"></asp:Button> </DIV><DIV style="BORDER-BOTTOM-COLOR: #000000; VERTICAL-ALIGN: text-bottom; BACKGROUND-COLOR: white; BORDER-BOTTOM-STYLE: solid" id="divIns"><TABLE style="VERTICAL-ALIGN: text-bottom; WIDTH: 100%; PADDING-TOP: 20px; BACKGROUND-COLOR: white" id="ins"><TBODY><TR><TD align=left colSpan=2><asp:Label id="lblMsg" runat="server" Visible="False" CssClass="message-text" Font-Bold="True" ForeColor="Red"></asp:Label></TD></TR><TR><TD class="lbl" align=left colSpan=2><B>Insurance Section</B> </TD></TR><TR><TD class="lbl" align=left>Insurance Company </TD><TD class="lbl" align=left><extddl:ExtendedDropDownList id="extddlInsuranceCompany" runat="server" Width="90%" Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY" Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" StausText="False" OldText="" OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged" AutoPost_back="True">
                                    </extddl:ExtendedDropDownList> </TD></TR><TR><TD class="lbl" align=left width="30%">Ins. Address </TD><TD align=left width="50%"><asp:ListBox id="lstInsuranceCompanyAddress" runat="server"></asp:ListBox> </TD></TR><TR><TD class="lbl" align=left width="30%">Claim/File # </TD><TD class="lbl" align=left width="50%"><asp:TextBox id="txtCalimNumber" runat="server" Width="80%"></asp:TextBox> </TD><TD align=right><asp:Button id="btnUpdateInsurance" onclick="btnUpdateInsurance_Click" runat="server" Text="Update" CssClass="Buttons"></asp:Button> </TD></TR></TBODY></TABLE></DIV><DIV style="BORDER-BOTTOM-COLOR: #000000; VERTICAL-ALIGN: text-bottom; WIDTH: 100%; BACKGROUND-COLOR: white; BORDER-BOTTOM-STYLE: solid" id="divdiagno"><TABLE style="VERTICAL-ALIGN: text-bottom; WIDTH: 100%; PADDING-TOP: 20px; BACKGROUND-COLOR: white" id="Digno"><TBODY><TR><TD class="lbl" align=left colSpan=6><B>Diagnosis Code Section </B></TD></TR><TR><TD style="WIDTH: 100%" align=left colSpan=6><cc1:TabContainer id="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0" TabStripPlacement="Top"><ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate"><HeaderTemplate>
                                                                                                     <div style="float:left; " class="lbl">
                                                                                                          Associate Diagnosis Code</div>
                                                                                                          
                                                
                                                                                                        
</HeaderTemplate>
<ContentTemplate>
<TABLE width="100%"><TBODY><TR><TD scope="col" align=left width="100%"><DIV class="blocktitle"><DIV class="div_blockcontent"><TABLE width="100%" border=0><TBODY><TR></TR><TR></TR><TR><TD colSpan=2><TABLE style="WIDTH: 80%" class="ContentTable" cellSpacing=0 cellPadding=3 border=0><TBODY><TR id="trDoctorType"><TD id="Td1" class="ContentLabel">Diagnosis Type &nbsp;&nbsp;</TD><TD id="Td2" class="ContentLabel" ><extddl:ExtendedDropDownList id="extddlDiagnosisType" runat="server" Width="105px" Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Flag_Key_Value="DIAGNOSIS_TYPE_LIST" Selected_Text="--- Select ---" StausText="False" OldText=""></extddl:ExtendedDropDownList> </TD><TD id="Td3" class="ContentLabel" >Code &nbsp;&nbsp; </TD><TD id="Td4" ><asp:TextBox id="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox> </TD><TD id="Td5" class="ContentLabel" >Description &nbsp;&nbsp; </TD><TD id="Td6" class="ContentLabel"><asp:TextBox id="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox> </TD></TR><TR><TD align=left colSpan=6><asp:TextBox id="TextBox1" runat="server" Width="10px" Visible="False"></asp:TextBox> <asp:Button id="btnSeacrh1" onclick="btnSeacrh1_Click" runat="server" Width="80px" Text="Search" CssClass="Buttons"></asp:Button> &nbsp;<asp:Button id="btnAssign" onclick="btnAssign_Click" runat="server" Width="80px" Text="Assign" CssClass="Buttons"></asp:Button> </TD></TR></TBODY></TABLE></TD></TR><TR></TR><TR><TD colSpan=2><TABLE id="tblDiagnosisCodeFirst" width="100%" ><TBODY><TR id="Tr1" ><TD id="Td8" width="100%" ><DIV style="OVERFLOW: scroll; HEIGHT: 120px"><asp:DataGrid id="grdNormalDgCode" runat="server" Width="99%" CssClass="GridTable" AutoGenerateColumns="False" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged"><Columns>
<asp:TemplateColumn><ItemTemplate>
                                                                                                         <asp:CheckBox ID="chkSelect" runat="server" /> 
                                                                                                         
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn>
</Columns>

<HeaderStyle HorizontalAlign="Center" CssClass="GridHeader"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" CssClass="GridRow"></ItemStyle>
</asp:DataGrid> </DIV></TD></TR></TBODY></TABLE></TD></TR><TR><TD colSpan=2><asp:Label id="lblDiagnosisCount" runat="server" Visible="False" Font-Bold="True"></asp:Label> </TD></TR><TR><TD colSpan=2><DIV style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: auto">&nbsp;<asp:DataGrid id="grdSelectedDiagnosisCode" runat="server" Width="100%" Visible="False" CssClass="GridTable" AutoGenerateColumns="False"><Columns>
<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
</Columns>

<HeaderStyle CssClass="GridHeader"></HeaderStyle>

<ItemStyle CssClass="GridRow"></ItemStyle>
</asp:DataGrid> </DIV></TD></TR></TBODY></TABLE></DIV></DIV><asp:TextBox id="TextBox2" runat="server" Width="10px" Visible="False"></asp:TextBox> <asp:TextBox id="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox> </TD></TR></TBODY></TABLE>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel runat="server" TabIndex="1" ID="tabpnlDeassociate"><HeaderTemplate>
                                                    <div  class="lbl">
                                                        De-associate Diagnosis Code</div>
                                                
</HeaderTemplate>
<ContentTemplate>
<TABLE width="100%"><TBODY><TR><TD scope="col" width="100%"><DIV class="blocktitle"><DIV class="div_blockcontent"><TABLE width="100%" border=0><TBODY><TR><TD colSpan=2> <asp:DataGrid id="grdAssignedDiagnosisCode" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="False" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged" AllowPaging="True"><Columns>
<asp:TemplateColumn><ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                
</ItemTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
</Columns>

<HeaderStyle CssClass="GridHeader"></HeaderStyle>

<ItemStyle CssClass="GridRow"></ItemStyle>

<PagerStyle Mode="NumericPages"></PagerStyle>
</asp:DataGrid> </TD></TR><TR><TD><asp:Button id="btnDeassociateDiagCode" onclick="btnDeassociateDiagCode_Click" runat="server" Width="104px" Text="De-Associate" CssClass="Buttons"></asp:Button> </TD></TR></TBODY></TABLE></DIV></DIV></TD></TR></TBODY></TABLE>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</cc1:TabContainer> </TD></TR></TBODY></TABLE></DIV></asp:Panel> 
</ContentTemplate>
       
    </asp:UpdatePanel>
</asp:Content>


