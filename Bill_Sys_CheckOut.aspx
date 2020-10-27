<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CheckOut.aspx.cs" Inherits="Bill_Sys_CheckOut" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
    <script type="text/javascript">
        
        function ShowPopUp()
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlViewPatientDetails').style.height = '180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlViewPatientDetails').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlViewPatientDetails').style.position = 'absolute';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlViewPatientDetails').style.top = '355px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlViewPatientDetails').style.left = '450px';
        }
        
        function CloseGroupSource()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlGroupService').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlGroupService').style.visibility = 'hidden';  
               
       }
       
       function ViewDetails(obj1,obj2,obj3)
       {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '350px'; 
            document.getElementById('divid2').style.top= '200px'; 
            document.getElementById('divid2').style.visibility='visible'; 
            document.getElementById('iframeAddDiagnosis').src="Bill_Sys_CO_ViewDeatails.aspx?CaseID="+obj1+"&ProcID="+obj2+"&CompID="+obj3+"";
            return false;            
       }
       
       function CloseSource()
       {
            document.getElementById('divid2').style.visibility='hidden';
            document.getElementById('iframeAddDiagnosis').src='';
//            window.parent.document.location.href='Bill_Sys_CheckOut.aspx';            
       }
    </script>

    <%-- function load date according to selection --%>

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
         
         
         
         function SetDate(objDateOption,objFromDate,objToDate)
         {
            getWeek();
            var selValue = document.getElementById(objDateOption).value;
            if(selValue == 0)
            {
                   document.getElementById(objToDate).value = "";
                   document.getElementById(objFromDate).value = "";
            }
            else if(selValue == 1)
            {
                   document.getElementById(objToDate).value = getDate('today');
                   document.getElementById(objFromDate).value = getDate('today');
            }
            else if(selValue == 2)
            {
                   document.getElementById(objToDate).value = getWeek('endweek');
                   document.getElementById(objFromDate).value = getWeek('startweek');
            }
            else if(selValue == 3)
            {
                   document.getElementById(objToDate).value = getDate('monthend');
                   document.getElementById(objFromDate).value = getDate('monthstart');
            }
            else if(selValue == 4)
            {
                   document.getElementById(objToDate).value = getDate('quarterend');
                   document.getElementById(objFromDate).value = getDate('quarterstart');
            }
            else if(selValue == 5)
            {
                   document.getElementById(objToDate).value = getDate('yearend');
                   document.getElementById(objFromDate).value = getDate('yearstart');
            }
              else if(selValue == 6)
            {
                   document.getElementById(objToDate).value = getLastWeek('endweek');
                   document.getElementById(objFromDate).value = getLastWeek('startweek');
            }else if(selValue == 7)
            {     
                   document.getElementById(objToDate).value = lastmonth('endmonth');
                   document.getElementById(objFromDate).value = lastmonth('startmonth');
            }else if(selValue == 8)
            {     
                   document.getElementById(objToDate).value =lastyear('endyear');
                   document.getElementById(objFromDate).value = lastyear('startyear');
            }else if(selValue == 9)
            {     
                   document.getElementById(objToDate).value = quarteryear('endquaeter');
                   document.getElementById(objFromDate).value =  quarteryear('startquaeter');
            }
         }
         
         
         function OpenPage(objEventID,objProcedureGroup,objVisitType)
         {
         
            if(objProcedureGroup == 'PT' || objProcedureGroup == 'pt')
            {
                window.location.href = "Bill_Sys_CO_PTNotes.aspx?EID="+objEventID;
            }
            else if(objProcedureGroup == 'IM' || objProcedureGroup == 'im')
            {
                if(objVisitType=='IE')
                {
                    window.location.href = "Bill_Sys_IM_HistoryOfPresentIillness.aspx?EID="+objEventID;
                }
                else if(objVisitType=='FU')
                {
                    window.location.href = "Bill_Sys_FUIM_StartExamination.aspx?EID="+objEventID;
                }
                else
                {
                    window.location.href = "Bill_Sys_CO_PTNotes_1.aspx?EID="+objEventID;
                }
                    
            }
            else if(objProcedureGroup == 'AC' || objProcedureGroup == 'ac')
            {
                if(objVisitType=='FU')
                {
                    window.location.href = "Bill_Sys_AC_Acupuncture_Followup.aspx?EID="+objEventID;
                }
                else 
                {
                    window.location.href = "Bill_Sys_CO_PTNotes_1.aspx?EID="+objEventID;
                }
            }
            else 
            {
                window.location.href = "Bill_Sys_CO_PTNotes_1.aspx?EID="+objEventID;
            }
         }
         
    </script>

    <table id="tblTabTreatment" runat="server" style="width: 100%; vertical-align: top;"
        height="60">
        <tr>
            <td>
                <asp:TextBox ID="txtuserid" runat="server" Visible="false" Width="2%"></asp:TextBox>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="2%"></asp:TextBox>
                  <asp:TextBox ID="txtSort" runat="server" Width="10px" Visible="False"></asp:TextBox>
            </td>
            <td>
                <ajaxToolkit:TabContainer ID="tabVistInformation" runat="Server" ActiveTabIndex="0"
                    CssClass="ajax__tab_theme">
                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlOne" HeaderText="Sample" TabIndex="0"
                        Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeadOne" runat="server" Text="" class="lbl"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td  width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100px">
                                                                Show Visits :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlist1" runat="server" OnSelectedIndexChanged="rdlist1_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">
                                                                    <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Schedule" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                   <%-- <asp:ListItem Text="None" Value="0"></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td width="100px">
                                                                Date Range :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDateValues_1" runat="Server" Width="150px">
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
                                                                &nbsp;&nbsp;&nbsp;&nbsp;From Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate_1" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextFromDate1" runat="server" TargetControlID="txtFromDate_1"
                                                                    PopupButtonID="imgbtnFromDate" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;To Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate_1" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextToDate1" runat="server" TargetControlID="txtToDate_1"
                                                                    PopupButtonID="imgbtnToDate" Enabled="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="100%">
                                                                <asp:TextBox ID="txtProcedureGroupID" runat="server" Visible="False" Width="2%"></asp:TextBox>
                                                                <asp:Button ID="btnSearch_1" runat="server" Text="Search" OnClick="btnSearch_1_Click"
                                                                    CssClass="Buttons" />
                                                                <asp:Button ID="btnShow" runat="server" Text="Show All" OnClick="btnShow_Click"
                                                                    CssClass="Buttons" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DataGrid ID="grdCheckout" Width="100%" CssClass="GridTable" runat="server" AutoGenerateColumns="False" OnItemCommand="grdCheckout_ItemCommand">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--1--%>
                                                <%--<asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkOpenPage" onclick='<%# "javascript:OpenPage(" + "\""+ Eval("I_EVENT_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP") +"\",\""+ Eval("SZ_TYPE") +"\");" %>'>
                                                            <%#DataBinder.Eval(Container,"DataItem.SZ_CASE_NO") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                              <%-- <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                <asp:TemplateColumn HeaderText="Case #" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlCase" runat="server" CommandName="Case" CommandArgument="SZ_CASE_NO"
                                                                                        Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                  <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                 </asp:TemplateColumn>
                                               <%-- <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                
                                                <%--2--%>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Patient ID" Visible ="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--3--%>
                                                 <asp:TemplateColumn HeaderText="Case #" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="Patient Name" CommandArgument="SZ_PATIENT_NAME"
                                                                                        Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                                                </ItemTemplate>
                                                                                 </asp:TemplateColumn>
                                                
                                            <%--    <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>--%>
                                                <%--4--%>
                                                 <asp:TemplateColumn HeaderText="Insurance Company" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlInsuranceName" runat="server" CommandName="Insurance Company" CommandArgument="SZ_INSURANCE_COMPANY"
                                                                                        Font-Bold="true" Font-Size="12px">Insurance Company</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_INSURANCE_COMPANY")%>
                                                                                </ItemTemplate>
                                                                                 </asp:TemplateColumn>
                                                
                                                
                                                
                                              <%--  <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>--%>
                                                <%--5--%>
                                                 <asp:TemplateColumn HeaderText="Visit Date" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:LinkButton ID="lnlEventDate" runat="server" CommandName="Visit Date" CommandArgument="DT_EVENT_DATE"
                                                                                        Font-Bold="true" Font-Size="12px">Visit Date</asp:LinkButton>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.DT_EVENT_DATE")%>
                                                                                </ItemTemplate>
                                                                                 </asp:TemplateColumn>
                                                
                                             <%--   <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Visit Date"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>--%>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--8--%>
                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--9--%>
                                                <asp:TemplateColumn HeaderText="Previous Treatment">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--10--%>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                 <%--11--%>
                                                <asp:BoundColumn DataField="TREATMENT_STATUS" HeaderText="Treatment Status" ReadOnly="True"
                                                    Visible="true">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                 <%--12--%>
                                                <asp:BoundColumn DataField="SIGNATURE_STATUS" HeaderText="Signature Status" ReadOnly="True"
                                                    Visible="true">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <%--13--%>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                 <%--14--%>
                                                <asp:BoundColumn DataField="I_EVENT_ID" HeaderText="Event ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <%--15--%>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Procedure Group" ReadOnly="True"
                                                     >
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlTwo" HeaderText="Sample" TabIndex="1"
                        Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeadtwo" runat="server" Text="" class="lbl"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100px">
                                                                Show Visits :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlist2" runat="server" OnSelectedIndexChanged="rdlist2_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">
                                                                    <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Schedule" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                   <%-- <asp:ListItem Text="None" Value="0"></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td width="100px">
                                                                Date Range :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDateValues_2" runat="Server" Width="150px">
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
                                                                &nbsp;&nbsp;&nbsp;&nbsp;From Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate_2" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromDate2" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextFromDate2" runat="server" TargetControlID="txtFromDate_2"
                                                                    PopupButtonID="imgbtnFromDate2" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;To Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate_2" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToDate2" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextToDate2" runat="server" TargetControlID="txtToDate_2"
                                                                    PopupButtonID="imgbtnToDate2" Enabled="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSearch_2" runat="server" Text="Search"
                                                        OnClick="btnSearch_2_Click" CssClass="Buttons" />
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td>
                                                    Visit Type :
                                                </td>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="extddlVisitType2" runat="server" Width="200px" Selected_Text="---Select---"
                                                        Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE" Connection_Key="Connection_String"
                                                        OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td>
                                                    Visit Status :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus2" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>--%>
                                            <tr>
                                                <td colspan="4" align="right">
                                                    <%--<asp:TextBox ID="txtCompanyID2" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                    <asp:TextBox ID="txtDocIDLst2" runat="server" Visible="false" Width="2%"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtProcedureGroupID2" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DataGrid ID="grdCheckout2" Width="100%" CssClass="GridTable" runat="server" OnItemCommand="grdCheckout2_ItemCommand"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%-- <asp:TemplateColumn HeaderText="Patient ID">
                                                    <ItemTemplate>
                                                         <a href="Bill_Sys_CO_PTNotes.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID")%>"
                                                                            ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                               <%-- <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkOpenPage" onclick='<%# "javascript:OpenPage(" + "\""+ Eval("I_EVENT_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP") +"\",\""+ Eval("SZ_TYPE") +"\");" %>'>
                                                            <%#DataBinder.Eval(Container,"DataItem.SZ_CASE_NO") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                
                                                
                                                <%--<asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms1" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Visit Date"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Previous Treatment">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                        <%--<asp:LinkButton ID="lnkViewdetails" runat="server" CommandName="ShowDiagnosisDetails"
                                                            Text="View Diagnosis Details" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                               
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlThree" HeaderText="Sample" TabIndex="2"
                        Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeadthree" runat="server" Text="" class="lbl"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100px">
                                                                Show Visits :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlist3" runat="server" OnSelectedIndexChanged="rdlist3_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">
                                                                    <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Schedule" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                   <%-- <asp:ListItem Text="None" Value="0"></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td width="100px">
                                                                Date Range :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDateValues_3" runat="Server" Width="150px">
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
                                                                &nbsp;&nbsp;&nbsp;&nbsp;From Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate_3" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromDate3" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextFromDate3" runat="server" TargetControlID="txtFromDate_3"
                                                                    PopupButtonID="imgbtnFromDate3" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;To Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate_3" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToDate3" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextToDate3" runat="server" TargetControlID="txtToDate_3"
                                                                    PopupButtonID="imgbtnToDate3" Enabled="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btn_Search_3" runat="server" Text="Search"
                                                        OnClick="btn_Search_3_Click" CssClass="Buttons" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    Visit Type :
                                                </td>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="ExtendedDropDownList1" runat="server" Width="200px"
                                                        Selected_Text="---Select---" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE"
                                                        Connection_Key="Connection_String" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td>
                                                    Visit Status :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus3" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>--%>
                                            <tr>
                                                <td colspan="4" align="right">
                                                    <%-- <asp:TextBox ID="txtCompanyID3" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                    <asp:TextBox ID="txtDocIDLst3" runat="server" Visible="false" Width="2%"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtProcedureGroupID3" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DataGrid ID="grdCheckout3" Width="100%" CssClass="GridTable" runat="server" OnItemCommand="grdCheckout3_ItemCommand"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%-- <asp:TemplateColumn HeaderText="Patient ID">
                                                    <ItemTemplate>
                                                         <a href="Bill_Sys_CO_PTNotes.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID")%>"
                                                                            ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                               <%-- <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkOpenPage" onclick='<%# "javascript:OpenPage(" + "\""+ Eval("I_EVENT_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP") +"\",\""+ Eval("SZ_TYPE") +"\");" %>'>
                                                            <%#DataBinder.Eval(Container,"DataItem.SZ_CASE_NO") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Patient ID" Visible ="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />                                                    
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Visit Date"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Previous Treatment">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                        <%--<asp:LinkButton ID="lnkViewdetails" runat="server" CommandName="ShowDiagnosisDetails"
                                                            Text="View Diagnosis Details" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlFour" HeaderText="Sample" TabIndex="3"
                        Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeadfour" runat="server" Text="" class="lbl"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100px">
                                                                Show Visits :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlist4" runat="server" OnSelectedIndexChanged="rdlist4_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">
                                                                    <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Schedule" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                   <%-- <asp:ListItem Text="None" Value="0"></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td width="100px">
                                                                Date Range :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDateValues_4" runat="Server" Width="150px">
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
                                                                &nbsp;&nbsp;&nbsp;&nbsp;From Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate_4" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromDate4" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextFromDate4" runat="server" TargetControlID="txtFromDate_4"
                                                                    PopupButtonID="imgbtnFromDate4" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;To Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate_4" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToDate4" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextToDate4" runat="server" TargetControlID="txtToDate_4"
                                                                    PopupButtonID="imgbtnToDate4" Enabled="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSearch_4" runat="server" Text="Search"
                                                        OnClick="btnSearch_4_Click" CssClass="Buttons" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    Visit Type :
                                                </td>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="ExtendedDropDownList2" runat="server" Width="200px"
                                                        Selected_Text="---Select---" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE"
                                                        Connection_Key="Connection_String" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td>
                                                    Visit Status :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus4" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>--%>
                                            <tr>
                                                <td colspan="4" align="right">
                                                    <%--<asp:TextBox ID="txtCompanyID4" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                    <asp:TextBox ID="txtDocIDLst4" runat="server" Visible="false" Width="2%"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtProcedureGroupID4" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DataGrid ID="grdCheckout4" Width="100%" CssClass="GridTable" runat="server" OnItemCommand="grdCheckout4_ItemCommand"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                 <%--<asp:TemplateColumn HeaderText="Patient ID">
                                                    <ItemTemplate>
                                                         <a href="Bill_Sys_CO_PTNotes.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.I_EVENT_ID")%>"
                                                                            ><%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                <%--<asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkOpenPage" onclick='<%# "javascript:OpenPage(" + "\""+ Eval("I_EVENT_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP") +"\",\""+ Eval("SZ_TYPE") +"\");" %>'>
                                                            <%#DataBinder.Eval(Container,"DataItem.SZ_CASE_NO") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Patient ID"  Visible = "false">                                                    
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />                                                    
                                                </asp:BoundColumn>                                                
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Visit Date"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Previous Treatment">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                        <%--<asp:LinkButton ID="lnkViewdetails" runat="server" CommandName="ShowDiagnosisDetails"
                                                            Text="View Diagnosis Details" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlfive" HeaderText="Sample" TabIndex="4"
                        Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeadfive" runat="server" Text="" class="lbl"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100px">
                                                                Show Visits :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlist5" runat="server" OnSelectedIndexChanged="rdlist5_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">
                                                                    <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Schedule" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                    <%--<asp:ListItem Text="None" Value="0"></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <table>
                                                        <tr>
                                                            <td width="100px">
                                                                Date Range :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDateValues_5" runat="Server" Width="150px">
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
                                                                &nbsp;&nbsp;&nbsp;&nbsp;From Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate_5" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromDate5" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextFromDate5" runat="server" TargetControlID="txtFromDate_5"
                                                                    PopupButtonID="imgbtnFromDate5" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;To Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate_5" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToDate5" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextToDate5" runat="server" TargetControlID="txtToDate_5"
                                                                    PopupButtonID="imgbtnToDate5" Enabled="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSearch_5" runat="server" Text="Search"
                                                        OnClick="btnSearch_5_Click" CssClass="Buttons" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    Visit Type :
                                                </td>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="ExtendedDropDownList3" runat="server" Width="200px"
                                                        Selected_Text="---Select---" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE"
                                                        Connection_Key="Connection_String" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td>
                                                    Visit Status :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus5" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                                        <asp:ListItem Value="3">No-show</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>--%>
                                            <tr>
                                                <td colspan="4" align="right">
                                                    <%--<asp:TextBox ID="txtCompanyID5" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                    <asp:TextBox ID="txtDocIDLst5" runat="server" Visible="false" Width="2%"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtProcedureGroupID5" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DataGrid ID="grdCheckout5" Width="100%" CssClass="GridTable" runat="server" OnItemCommand="grdCheckout5_ItemCommand"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                              
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Patient ID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />                                                                                                                                                           												                                              
                                                </asp:BoundColumn>                                                                                                                                      
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Visit Date"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Previous Treatment">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                        <%--<asp:LinkButton ID="lnkViewdetails" runat="server" CommandName="ShowDiagnosisDetails"
                                                            Text="View Diagnosis Details" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>                                                
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlsix" HeaderText="Sample" TabIndex="5"
                        Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeadSix" runat="server" Text="" class="lbl"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100px">
                                                                Show Visits :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlist6" runat="server" OnSelectedIndexChanged="rdlist6_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">
                                                                    <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Schedule" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                   <%-- <asp:ListItem Text="None" Value="0"></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td width="100px">
                                                                Date Range :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDateValues_6" runat="Server" Width="150px">
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
                                                                &nbsp;&nbsp;&nbsp;&nbsp;From Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate_6" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnFromDate6" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextFromDate6" runat="server" TargetControlID="txtFromDate_6"
                                                                    PopupButtonID="imgbtnFromDate6" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;To Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate_6" runat="server" Width="75px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnToDate6" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:CalendarExtender ID="calextToDate6" runat="server" TargetControlID="txtToDate_6"
                                                                    PopupButtonID="imgbtnToDate6" Enabled="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSearch_6" runat="server" Text="Search"
                                                        OnClick="btnSearch_6_Click" CssClass="Buttons" />
                                                        <asp:TextBox ID="txtProcedureGroupID6" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                </td>
                                            </tr>
                                           
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DataGrid ID="grdCheckout6" Width="100%" CssClass="GridTable" runat="server" OnItemCommand="grdCheckout6_ItemCommand"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Patient ID" Visible = "false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Visit Date"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Previous Treatment">
                                                
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                        <%--<asp:LinkButton ID="lnkViewdetails" runat="server" CommandName="ShowDiagnosisDetails"
                                                            Text="View Diagnosis Details" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlseven" HeaderText="Sample" TabIndex="6"
                        Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeadSeven" runat="server" Text="" class = "lbl"></asp:Label>
                        </HeaderTemplate>
                        <ContentTemplate>
                             <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100px">
                                                                Show Visits :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlist7" runat="server" OnSelectedIndexChanged="rdlist7_SelectedIndexChanged"
                                                                        RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">
                                                                        <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Schedule" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                        <%--<asp:ListItem Text="None" Value="0"></asp:ListItem>--%>
                                                                    </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td width="100px">
                                                                Date Range :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDateValues_7" runat="Server" Width="150px">
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
                                                                &nbsp;&nbsp;&nbsp;&nbsp;From Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtFromDate_7" runat="server" Width="75px"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnFromDate7" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="calextFromDate7" runat="server" TargetControlID="txtFromDate_7"
                                                                        PopupButtonID="imgbtnFromDate7" Enabled="True" />
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;To Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtToDate_7" runat="server" Width="75px"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnToDate7" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="calextToDate7" runat="server" TargetControlID="txtToDate_7"
                                                                        PopupButtonID="imgbtnToDate7" Enabled="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnSearch_7" runat="server" Text="Search"
                                                        OnClick="btnSearch_7_Click" CssClass="Buttons" />
                                                        
                                                    <asp:TextBox ID="txtProcedureGroupID7" runat="server" Visible="false" Width="2%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:DataGrid ID="grdCheckout7" Width="100%" CssClass="GridTable" runat="server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                               <asp:TemplateColumn HeaderText="Case #">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkOpenPage" onclick='<%# "javascript:OpenPage(" + "\""+ Eval("I_EVENT_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP") +"\",\""+ Eval("SZ_TYPE") +"\");" %>'>
                                                            <%#DataBinder.Eval(Container,"DataItem.SZ_CASE_NO") %>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Visit Date"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit"
                                                    ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Previous Treatment">
                                                    <ItemTemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                        <%--<asp:LinkButton ID="lnkViewdetails" runat="server" CommandName="ShowDiagnosisDetails"
                                                            Text="View Diagnosis Details" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlViewPatientDetails" runat="server" Style="z-index: 102; width: 420px;
        height: 180px; background-color: white; border-color: ThreeDFace; border-width: 1px;
        border-style: solid; position: absolute; top: 355px; left: 450px" Visible="false">
        <asp:Label ID="lblDiagnosisCode" runat="server" Text="Diagnosis Code Details"></asp:Label>
        <a onclick="CloseGroupSource();" style="cursor: pointer;" title="Close">X</a>
        <asp:DataGrid ID="grddiagnosis" runat="server" AutoGenerateColumns="false" CssClass="GridTable">
            <HeaderStyle CssClass="GridHeader" />
            <ItemStyle CssClass="GridRow" />
            <Columns>
                <asp:BoundColumn DataField="DIAGNOSIS CODE" HeaderText="Diagnosis Code" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SZ_PROCEDURE_CODES" HeaderText="Procedure Code" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <%--<asp:BoundColumn DataField="DIAGNOSIS CODE DESCRIPTION" HeaderText = "Diagnosis Code" ReadOnly = "true">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>--%>
            </Columns>
        </asp:DataGrid>
        <%--<a onclick="CloseGroupSource();" style="cursor: pointer;" title="Close">X</a>--%>
    </asp:Panel>
    <div id="divid2" style="position: absolute; left: 100px; top: 100px; width: 500px;
        height: 280px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="CloseSource();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="iframeAddDiagnosis" src="" frameborder="0" height="380" width="500"></iframe>
    </div>
</asp:Content>
