<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_OnlyVisitReport.aspx.cs" Inherits="Bill_Sys_OnlyVisitReport" %>
 <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }
        function CheckForInteger(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }


        }


        function SetDate() {
            getWeek();
            var selValue = document.getElementById('_ctl0_ContentPlaceHolder1_ddlDateValues').value;
            if (selValue == 0) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = "";
                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = "";

            }
            else if (selValue == 1) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('today');
                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getWeek('endweek');
                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getWeek('startweek');
            }
            else if (selValue == 3) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('monthend');
                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('quarterend');
                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getDate('yearend');
                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = getLastWeek('endweek');
                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = lastmonth('endmonth');

                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = lastyear('endyear');

                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById('_ctl0_ContentPlaceHolder1_txtToDate').value = quarteryear('endquaeter');

                document.getElementById('_ctl0_ContentPlaceHolder1_txtFromDate').value = quarteryear('startquaeter');
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
                    return ('12/1/' + y);

                }
                else {
                    var m = t_mon - 1;
                    return (m + '/1/' + t_year);
                }
            }
            else if (type == 'endmonth') {
                if (t_mon == 1) {
                    var y = t_year - 1;
                    return ('12/31/' + y);
                } else {
                    var m = t_mon - 1;
                    var d = daysInMonth(t_mon - 1, t_year);
                    return (m + '/' + d + '/' + t_year);
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
                    return ('10/1/' + y);
                }
                else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                    return ('1/1/' + t_year);

                } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                    return ('4/1/' + t_year);


                } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                    return ('7/1/' + t_year);

                }

            } else if (type == 'endquaeter') {
                if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
                    //
                    var y = t_year - 1;
                    return ('12/31/' + y);
                }
                else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                    return ('3/31/' + t_year);

                } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                    return ('6/30/' + t_year);


                } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                    return ('9/30/' + t_year);
                }

            }

        }

        function lastyear(type) {
            var d = new Date();

            var t_year = d.getFullYear();
            if (type == 'startyear') {
                y = t_year - 1;
                return ('1/1/' + y);
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
            return '' + (month + 1) + '/' + day + '/' + year;
        }

        function getFormattedDateForMonth(day, month, year) {
            return '' + (month) + '/' + day + '/' + year;
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

            document.getElementById("<%=txtFromDate.ClientID%>").value = '';
             document.getElementById("<%=txtToDate.ClientID %>").value = '';
             var extvisible = document.getElementById("_ctl0_ContentPlaceHolder1_ddlDateValues");
             if (extvisible != null) {
                 document.getElementById("_ctl0_ContentPlaceHolder1_ddlDateValues").value = "0";
             }
             var strspeciality = document.getElementById("_ctl0_ContentPlaceHolder1_extddlSpeciality");

             if (strspeciality != null && strspeciality.value != "NA") {

                 document.getElementById("_ctl0_ContentPlaceHolder1_extddlSpeciality").value = "NA";
             }

             var strdoctor = document.getElementById("_ctl0_ContentPlaceHolder1_extddlDoctor");
             if (strdoctor != null) {
                 document.getElementById("_ctl0_ContentPlaceHolder1_extddlDoctor").value = "NA";
             }
             var strlocation = document.getElementById("_ctl0_ContentPlaceHolder1_extddlLocation");
             if (strlocation != null) {
                 document.getElementById("_ctl0_ContentPlaceHolder1_extddlLocation").value = "NA";
             }

         }


    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                               <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
            <tr>
                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                    <b class="txt3">Search Parameters</b>
                </td>
             </tr>
            <tr>
                <td colspan ="2">
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
                    <td class="td-widget-bc-search-desc-ch3" valign="top" >
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
            <asp:TextBox ID="txtFromDate" onkeypress="return CheckForInteger(event,'/')"
                runat="server"  MaxLength="10" Width="80%"></asp:TextBox>
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
                    <td >
                            <ajaxcontrol:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtFromDate"
                                     PopupButtonID="imgbtnDateofAccident">
                            </ajaxcontrol:CalendarExtender>
            
            <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                ControlToValidate="txtFromDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
            <asp:RangeValidator runat="server" id="rngDate" controltovalidate="txtFromDate" type="Date" minimumvalue="01/01/1901" maximumvalue="12/31/2099" errormessage="Enter a valid date " />--%>
              
                    </td>
                    <td >
                        <ajaxcontrol:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtToDate"
                 PopupButtonID="imgbtnDateofBirth">
             </ajaxcontrol:CalendarExtender>
             <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                ControlToValidate="txtToDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
             <asp:RangeValidator runat="server" id="rngDateBirth" controltovalidate="txtToDate" type="Date" minimumvalue="01/01/1901" maximumvalue="12/31/2099" errormessage="Enter a valid date " />--%>
                    </td>
                </tr>
                <tr>
                    <td class="td-widget-bc-search-desc-ch1" valign="top" >
                        Speciality
                    </td>
                    <td class="td-widget-bc-search-desc-ch1" valign="top" colspan="2">
                         Doctor Name
                         
                    </td>
                </tr>
                <tr>
                    <td class="td-widget-bc-search-desc-ch3" valign="top" >
                         <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                        Selected_Text="---Select---" Width="140px"></cc1:ExtendedDropDownList>
                    
                    </td>
                    <td class="td-widget-bc-search-desc-ch3" valign="top" colspan="2">
                        <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="240px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                     
                    </td>
                   
                </tr>
                <tr>
                    <td class="td-widget-bc-search-desc-ch1" valign="top">
                      <asp:Label ID="lblLocationName" runat="server"  Text="Location Name"
                                                        Visible="False" Width="94px" CssClass="td-widget-bc-search-desc-ch1"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="td-widget-bc-search-desc-ch3" valign="top">
                     <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="98%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                    </td>
                </tr>
                
                </table>
                   
                </td>
               </tr>
               
                  <tr>
      <td colspan=2 align="center" >
            <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
             <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" 
             OnClick="btnSearch_Click" />
             <input type="button" id="btnClear" onclick="Clear();" style="width:80px" value="Clear" />

      </td>
       </tr> 
                </table>
                        </td>
                    </tr>
                </table>
             </td>
        </tr>
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
                                        <div style="width: 50%; text-align: left;" class="ContentLabel">
                                            <%--<asp:Label ID="lblCount" Font-Bold="true" Font-Size="10" runat="server" Visible="false"></asp:Label>
                                    &nbsp; <a id="hlnkShowCount" href="#" runat="server" title ="Total Count By Speciality" class ="lbl">Total Count By Speciality</a>                                                                                                
                                                        
                                                    <ajaxcontrol:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowCount"
                                                        PopupControlID="pnlShowCount" Position= "Center" OffsetX="100" OffsetY="10" />
                                                         <asp:Panel ID="pnlShowCount" runat="server" Style="background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
                                                                        
                                                                        
                                                                        <asp:DataGrid ID="grdCount" runat="server" Width="25px" CssClass="GridTable"
                                                                                    AutoGenerateColumns="false" >
                                                                                    <ItemStyle CssClass="GridRow" />
                                                                                    <Columns>
                                                                                    <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count"></asp:BoundColumn>
                                                                                      
                                                                                        
                                                                                    </Columns>
                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                </asp:DataGrid>
                                                             </asp:Panel> --%>
                                        </div>
                                      <%-- <div style="text-align: left; vertical-align:bottom"><asp:Label ID="lblTotalcount" runat ="server" Text="Totalcount:"></asp:Label></div>
                                      --%>  <div style="text-align: right;">
                                            <table width="100%">
                                              <tr>
                                                 <td align="left" valign="top"><asp:Label ID="lblTotalcount" runat ="server" Text="Total Count:" CssClass="lbl" Font-Bold="true"></asp:Label>
                                                     <asp:Label ID="lblCountVlaues" runat="server" Text="0" CssClass="lbl"></asp:Label>
                                                     <asp:Label ID="lblTotAmount" runat ="server" Text="Total Amount:" CssClass="lbl" Font-Bold="true"></asp:Label>
                                                     <asp:Label ID="lblTotAmtValue" runat="server" Text="$0.00" CssClass="lbl"></asp:Label>
                                                     
                                                  </td>
                                                 <td><asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" /></td>
                                              </tr>
                                            </table>
                                            <%--<asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />--%>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" Height="100%">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="insurance_name" HeaderText="Insurance Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Provider_address" HeaderText="Provider Address"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="total_Amount" HeaderText="Billed Amount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                               
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
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
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
