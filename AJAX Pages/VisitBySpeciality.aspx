<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VisitBySpeciality.aspx.cs" Inherits="Visit_By_Speciality" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="~/validation.js"></script>
    <script language="javascript" type="text/javascript" >
        function Export() {
            expLoadingPanel.Show();
            Callback1.PerformCallback();
        }
          function SetVisitDate() {
        getWeek();
        var selValue = document.getElementById("<%=ddlVisitDateValues.ClientID%>").value;
        if (selValue == 0) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = "";
            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = "";
        }
        else if (selValue == 1) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = getDate('today');
            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = getDate('today');
        }
        else if (selValue == 2) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = getWeek('endweek');
            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = getWeek('startweek');
        }
        else if (selValue == 3) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = getDate('monthend');
            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = getDate('monthstart');
        }
        else if (selValue == 4) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = getDate('quarterend');
            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = getDate('quarterstart');
        }
        else if (selValue == 5) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = getDate('yearend');
            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = getDate('yearstart');
        }
        else if (selValue == 6) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = getLastWeek('endweek');
            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = getLastWeek('startweek');
        } else if (selValue == 7) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = lastmonth('endmonth');

            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = lastmonth('startmonth');
        } else if (selValue == 8) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = lastyear('endyear');

            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = lastyear('startyear');
        } else if (selValue == 9) {
            document.getElementById("<%=txtToVisitDate.ClientID%>").value = quarteryear('endquaeter');

            document.getElementById("<%=txtFromVisitDate.ClientID%>").value = quarteryear('startquaeter');
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

            if (day < 10 && month == 10) {
                return '' + (month) + '/0' + day + '/' + year;
            }
            if (day > 10 && month == 10) {
                return '' + (month) + '/' + day + '/' + year;
            }
        }

         function SetDate()
    {
        getWeek();
        var selValue = document.getElementById("<%=ddlVisitDateValues.ClientID %>").value;
        if(selValue == 0)
        {
            document.getElementById("<%=txtToVisitDate.ClientID %>").value = "";
            document.getElementById("<%=txtFromVisitDate.ClientID %>").value = "";
        }
        else if(selValue == 1)
        {
            document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('today');
            document.getElementById("<%=txtFromVisitDate.ClientID %>").value = getDate('today');
        }
        else if(selValue == 2)
        {
               document.getElementById("<%=txtToVisitDate.ClientID %>").value = getWeek('endweek');
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value = getWeek('startweek');
        }
        else if(selValue == 3)
        {
               document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('monthend');
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value = getDate('monthstart');
        }
        else if(selValue == 4)
        {
               document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('quarterend');
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value = getDate('quarterstart');
        }
        else if(selValue == 5)
        {
               document.getElementById("<%=txtToVisitDate.ClientID %>").value = getDate('yearend');
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value = getDate('yearstart');
        }
          else if(selValue == 6)
        {
               document.getElementById("<%=txtToVisitDate.ClientID %>").value = getLastWeek('endweek');
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value = getLastWeek('startweek');
        }else if(selValue == 7)
        {     
               document.getElementById("<%=txtToVisitDate.ClientID %>").value = lastmonth('endmonth');                   
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value = lastmonth('startmonth');
        }else if(selValue == 8)
        {     
               document.getElementById("<%=txtToVisitDate.ClientID %>").value =lastyear('endyear');
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value = lastyear('startyear');
        }else if(selValue == 9)
        {     
               document.getElementById("<%=txtToVisitDate.ClientID %>").value = quarteryear('endquaeter');                   
               document.getElementById("<%=txtFromVisitDate.ClientID %>").value =  quarteryear('startquaeter');
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

         
         
         function Clear() {
             document.getElementById('ctl00_ContentPlaceHolder1_extddlspeciality').value = "NA";
             document.getElementById('ctl00_ContentPlaceHolder1_extddlInsurance').value = "NA";
            //document.getElementById('ctl00_ContentPlaceHolder1_extddlSpeciality').value = "NA";
            document.getElementById('<%=ddlVisitDateValues.ClientID %>').value = '0';
            document.getElementById('<%=txtFromVisitDate.ClientID %>').value = '';
            document.getElementById('<%=txtToVisitDate.ClientID %>').value = '';
            document.getElementById('<%=txtCaseNumber.ClientID %>').value = '';
        }
         


        function OnSearch() {
        
            var Speciality = document.getElementById('ctl00_ContentPlaceHolder1_extddlspeciality');
           
            if (Speciality.value == "NA") {
                alert('Please select the specialty to proceed');
                return false;
            }
        }

    </script>
<%--for data validation--%>
     <script type="text/javascript" language="javascript">
           function FromVisitDateValidation() {
            var year1 = "";
            year1 = document.getElementById("<%=txtFromVisitDate.ClientID%>").value;
            if (year1.charAt(0) == '_' && year1.charAt(1) == '_' && year1.charAt(2) == '/' && year1.charAt(3) == '_' && year1.charAt(4) == '_' && year1.charAt(5) == '/' && year1.charAt(6) == '_' && year1.charAt(7) == '_' && year1.charAt(8) == '_' && year1.charAt(9) == '_') {
                return true;
            }
            if ((year1.charAt(6) == '_' && year1.charAt(7) == '_') || (year1.charAt(8) == '_' && year1.charAt(9) == '_') || (year1.charAt(6) == '0' && year1.charAt(7) == '0') || (year1.charAt(6) == '0') || (year1.charAt(9) == '_')) {
                document.getElementById("<%=lblValidator1.ClientID%>").innerText = 'Date is Invalid';
                document.getElementById("<%=txtFromVisitDate.ClientID%>").focus();
                return false;
            }
            if ((year1.charAt(6) != '_' && year1.charAt(7) != '_') || (year1.charAt(8) != '_' && year1.charAt(9) != '_') || (year1.charAt(6) != '0' && year1.charAt(7) != '0')) {
                document.getElementById("<%=lblValidator1.ClientID%>").innerText = '';
                return true;
            }

           }
           function ToVisitDateValidation() {
            var year2 = "";
            year2 = document.getElementById("<%=txtToVisitDate.ClientID%>").value;

            if (year2.charAt(0) == '_' && year2.charAt(1) == '_' && year2.charAt(2) == '/' && year2.charAt(3) == '_' && year2.charAt(4) == '_' && year2.charAt(5) == '/' && year2.charAt(6) == '_' && year2.charAt(7) == '_' && year2.charAt(8) == '_' && year2.charAt(9) == '_') {
                return true;
            }
            if ((year2.charAt(6) == '_' && year2.charAt(7) == '_') || (year2.charAt(8) == '_' && year2.charAt(9) == '_') || (year2.charAt(6) == '0' && year2.charAt(7) == '0') || (year2.charAt(6) == '0') || (year2.charAt(9) == '_')) {
                document.getElementById("<%=lblValid1.ClientID%>").innerText = 'Date is Invalid';
                document.getElementById("<%=txtToVisitDate.ClientID%>").focus();
                return false;
            }
            if ((year2.charAt(6) != '_' && year2.charAt(7) != '_') || (year2.charAt(8) != '_' && year2.charAt(9) != '_') || (year2.charAt(6) != '0' && year2.charAt(7) != '0')) {
                document.getElementById("<%=lblValid1.ClientID%>").innerText = '';
                return true;
            }

        }
         </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
        <table border="0" style="margin-left: 10px; margin-bottom: 5px; margin-top: 5px;">
            <tr>
                <td>
                    <table style="border: 1px solid #d3d3d3;" width="650px" border="0">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" colspan="3">
                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                <b>Search Parameters</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-widget-bc-search-desc-ch1" align="left">Visit Date
                            </td>
                            <td class="td-widget-bc-search-desc-ch1" align="left">From Date
                            </td>
                            <td class="td-widget-bc-search-desc-ch1" align="left">To Date
                            </td>
                        </tr>
                        <tr>
                            <td class="td-widget-bc-search-desc-ch3">
                                <asp:DropDownList ID="ddlVisitDateValues" runat="Server" Width="90%">
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
                                <asp:TextBox ID="txtFromVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnFromVisitDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                 
                            </td>
                            <td class="td-widget-bc-search-desc-ch3">
                                <asp:TextBox ID="txtToVisitDate" runat="server" onkeypress="return CheckForInteger(event,'/')" CssClass="text-box" MaxLength="10" Width="80%"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnToVisitDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                <asp:Label ID="lblValid1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td class="td-widget-bc-search-desc-ch">Speciality
                            </td>
                            <td class="td-widget-bc-search-desc-ch">Insurance
                            </td>
                            <td class="td-widget-bc-search-desc-ch">CaseNo
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <extddl:ExtendedDropDownList ID="extddlspeciality" runat="server" Width="95%" Connection_Key="Connection_String"
                                    Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"></extddl:ExtendedDropDownList>
                            </td>
                            <td align="left">
                                <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="95%" Connection_Key="Connection_String"
                                    Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY"></extddl:ExtendedDropDownList>
                            </td>
                            <td align="left" style="height: 20px">
                                <asp:TextBox ID="txtCaseNumber" runat="server" Width="80%"></asp:TextBox>
                            </td>
                        </tr>



                        <tr>
                            <td align="center" height="40" colspan="3">
                                
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click" />
                                        <input style="width: 80px" id="btnClear" onclick="Clear();" type="button" value="Clear"
                                            runat="server" />
                                 
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       
 <asp:UpdatePanel ID="UpdatePanel5" runat="server">
<ContentTemplate>
<table style="vertical-align: middle; width: 100%;">
    <tr>
        <td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                <ProgressTemplate>
                    <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                        runat="Server">
                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                            Height="25px" Width="24px"></asp:Image>
                        Loading...</div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
</table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; text-align: right;">
                            <dx:ASPxHyperLink Text="[Excel]" runat="server" ID="xExcel">
                                <ClientSideEvents Click="Export" />
                            </dx:ASPxHyperLink>
                            <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback1">
                                <ClientSideEvents CallbackComplete="OnCallbackComplete" />
                            </dx:ASPxCallback>
                            <dx:ASPxLoadingPanel Text="Exporting..." runat="server" ID="expLoadingPanel" ClientInstanceName="expLoadingPanel">
                            </dx:ASPxLoadingPanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99%">
                            <dx:ASPxGridView ID="grdCountWiseSpeciality" runat="server"
                                Border-BorderColor="#aaaaaa" SettingsPager-PageSize="20" Width="100%" AutoGenerateColumns="False"
                                Settings-ShowHorizontalScrollBar="true" Settings-ShowGroupedColumns="true" Settings-VerticalScrollableHeight="330"
                                Settings-ShowGroupPanel="true" KeyFieldName="sz_case_no"
                                SettingsBehavior-AllowGroup="true" Settings-ShowFooter="true" SettingsBehavior-AutoExpandAllGroups="true">
                                <Columns>
                                    <dx:GridViewDataColumn Caption="Case #" VisibleIndex="1" FieldName="SZ_CASE_NO"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Patient Name" VisibleIndex="2" FieldName="PatientName"
                                        Settings-AllowSort="True" Width="200px" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Speciality" VisibleIndex="3" FieldName="SZ_SPECIALITY"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn Caption="Insurance Name" VisibleIndex="4" FieldName="SZ_INSURANCE_NAME" Settings-AllowSort="True" Width="300px"
                                        HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Phone No" VisibleIndex="5" Width="140px" FieldName="SZ_PATIENT_PHONE"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Mobile No" VisibleIndex="6" Width="140px" FieldName="sz_patient_cellno"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn Caption="First Visit Date" VisibleIndex="7" FieldName="FirstVisitDate"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Last Visit Date" VisibleIndex="8" FieldName="LastVisitDate"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn Caption="Count" VisibleIndex="9" Width="100px" FieldName="Count"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>

                                </Columns>
                                <SettingsBehavior AutoExpandAllGroups="True" />
                                <SettingsPager PageSize="20">
                                </SettingsPager>
                                <Settings ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded"
                                    ShowGroupButtons="false" />
                            </dx:ASPxGridView>
                            <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdPaymentReport">
                            </dx:ASPxGridViewExporter>
                        </td>
                    </tr>
                </table>
           </ContentTemplate>
</asp:UpdatePanel>

        <%--<ajaxToolkit:CalendarExtender ID="calExtFromBillDate" runat="server" TargetControlID="txtFromBillDate"
            PopupButtonID="imgbtnFromBillDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromBillDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtToBillDate" runat="server" TargetControlID="txtToBillDate"
            PopupButtonID="imgbtnToBillDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToBillDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>

        <ajaxToolkit:CalendarExtender ID="calExtFromPaymentDate" runat="server" TargetControlID="txtFromPaymentDate"
            PopupButtonID="imgbtnFromPaymentDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromPaymentDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtToPaymentDate" runat="server" TargetControlID="txtToPaymentDate"
            PopupButtonID="imgbtnToPaymentDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToPaymentDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>

        <ajaxToolkit:CalendarExtender ID="calExtFromVisitDate" runat="server" TargetControlID="txtFromVisitDate"
            PopupButtonID="imgbtnFromVisitDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromVisitDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="calExtToVisitDate" runat="server" TargetControlID="txtToVisitDate"
            PopupButtonID="imgbtnToVisitDate" />
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToVisitDate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>--%>
    </div>
    <dx:ASPxPopupControl ID="IFrame_DownloadFiles" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="IFrame_DownloadFiles" HeaderText="Data Export" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="600px" ToolTip="Download File(s)"
        PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
        ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="200px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>

