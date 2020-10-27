<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_AttorneyReport.aspx.cs" Inherits="Bill_Sys_AttorneyReport"
    Title="Untitled Page" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
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
         
         
         // Start :   Download Date function 
         
         
         
         // End
         
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
                                                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                    <b>Attorney Report</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 13%; height: 21px;">
                                                    Case No&nbsp;&nbsp; &nbsp;</td>
                                                <td style="width: 25%; height: 21px;">
                                                    &nbsp;<asp:TextBox ID="txtCaseNo" runat="server" Height="14px" Width="193px"></asp:TextBox>
                                                    </td>
                                                <td class="ContentLabel" style="width: 9%; height: 21px;">
                                                    Patient Name</td>
                                                <td style="width: 25%; height: 21px;">&nbsp;<asp:TextBox ID="txtPatientName" runat="server" Height="14px" Width="82%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 13%" valign="top">
                                                    Case Type&nbsp;</td>
                                                <td style="width: 25%; height: 18px;">
                                                    &nbsp;<extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="196px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                                                    OldText="" StausText="False" /></td>
                                                <td class="ContentLabel" style="width: 9%; height: 18px;" valign="top">
                                                    Case Status&nbsp;
                                                </td>
                                                <td style="width: 25%; height: 18px;" valign="top">
                                                    &nbsp;<extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="91%" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                                                    Flag_ID="txtCompanyID.Text.ToString();" OldText="" StausText="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 13%; height: 2px;" valign="top">
                                                    Accident Date&nbsp; 
                                                </td>
                                                <td class="ContentLabel" style="width: 25%; height: 1px">
                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                        CssClass="text-box" MaxLength="10" ></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" style="float:left;"/>
                                                 <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxcontrol:MaskedEditValidator>
                                                </td>
                                                <td class="ContentLabel" style="width: 9%; height: 2px" valign="top">
                                                    Attorney &nbsp;</td>
                                                <td style="width: 25%; height: 2px" valign="top">
                                                    &nbsp;<extddl:ExtendedDropDownList ID="extddlAttorney" Width="91%" runat="server" Connection_Key="Connection_String"
                                                                                    Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                                                                    OldText="" StausText="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 13%" valign="top">
                                                    Insurance Company&nbsp;
                                                &nbsp;</td>
                                                <td style="width: 25%; height: 18px">
                                                    &nbsp;<extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="100%" runat="server"
                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                                    Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" 
                                                                                   
                                                                                    OldText="" StausText="False" />
                                                </td>
                                                <td class="ContentLabel" style="width: 9%; height: 18px" valign="top">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 25%; height: 18px" valign="top">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click1" /> &nbsp; <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                   
                                <tr>
                                    <td style="width: 100%;" class="TDPart" valign="top">

                                        <asp:DataGrid ID="grdPayment" runat="Server" AutoGenerateColumns="False" CssClass="GridTable" 
                                            Width="100%" OnItemCommand="grdPayment_ItemCommand">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            
                                             <asp:TemplateColumn HeaderText="Case No" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlCaseNo" runat="server" CommandName="Case No" CommandArgument="SZ_CASE_NO"
                                                            Font-Bold="true" Font-Size="12px">Case No</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Patient Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                            Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn HeaderText="Insurance Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlInsuranceName" runat="server" CommandName="Inasurance Name" CommandArgument="SZ_INSURANCE_NAME"
                                                            Font-Bold="true" Font-Size="12px">Insurance Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_NAME")%>
                                                    </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                     <asp:TemplateColumn HeaderText="Attorney Name" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlAttorneyName" runat="server" CommandName="Attorney Name" CommandArgument="SZ_ATTORNEY_NAME"
                                                            Font-Bold="true" Font-Size="12px">Attorney Name</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_ATTORNEY_NAME")%>
                                                    </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                     <asp:TemplateColumn HeaderText="Accident Date" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnlAccidentDate" runat="server" CommandName="Accident Date" CommandArgument="Sorting_Date"
                                                            Font-Bold="true" Font-Size="12px">Accident Date</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.DT_DATE_OF_ACCIDENT")%>
                                                    </ItemTemplate>                                                                                                
                                                </asp:TemplateColumn>
                                                
                                                <asp:BoundColumn DataField="IM_VISIT_DETAILS" HeaderText="IM Visit Details"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PT_VISIT_DETAILS" HeaderText="PT Visit Details"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MRI_VISIT_DETAILS" HeaderText="MRI Visit Details"></asp:BoundColumn> 
                                                <asp:BoundColumn DataField="MIN_DAT_OF_VISIT" HeaderText="Date Of First Visit"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MAX_DAT_OF_VISIT" HeaderText="Date Of Last Visit"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Sorting_Date" HeaderText="Sorting Date" Visible="false"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width: 100%;" class="TDPart" valign="top">

                                        <asp:DataGrid ID="grdExcel" runat="Server" AutoGenerateColumns="False" CssClass="GridTable" 
                                            Width="100%"  Visible="false" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>                                            
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_NAME" HeaderText="Attorney Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Accident Date"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="IM_VISIT_DETAILS" HeaderText="IM Visit Details"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PT_VISIT_DETAILS" HeaderText="PT Visit Details"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MRI_VISIT_DETAILS" HeaderText="MRI Visit Details"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MIN_DAT_OF_VISIT" HeaderText="Date Of First Visit"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MAX_DAT_OF_VISIT" HeaderText="Date Of Last Visit"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Sorting_Date" HeaderText="Sorting Date" Visible="false"></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" style="text-align:left;" class="TDPart">
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
                        <asp:TextBox ID="txtSort" runat="server" Visible="false" Width="24px"></asp:TextBox>
                                                      <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true" ></ajaxcontrol:MaskedEditExtender>
                                                     <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                  
                                                    <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                </td>
                        <td class="RightBottom" style="width: 10px"> &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
