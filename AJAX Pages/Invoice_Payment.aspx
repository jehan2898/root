<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Invoice_Payment.aspx.cs" Inherits="AJAX_Pages_Invoice_Payment" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>    
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        
        function MakePayment(sz_invoice_id, mn_invoice_balance_amount, mn_invoice_amount, SZ_EMPLOYER_NAME, sz_employer_id)
        {
            var url = 'Make_Invoice_Payment.aspx?id=' + sz_invoice_id + '&bal=' + mn_invoice_balance_amount + '&amount=' + mn_invoice_amount + '&empname=' + SZ_EMPLOYER_NAME + '&empid=' + sz_employer_id;
            $("#dialog").dialog({
                autoOpen: false,
                show: "fade",
                hide: "fade",
                modal: true,
                open: function (ev, ui) {
                  $("#scanIframe").attr("src", url);
                },
                height: 600 ,
                width: 1000 ,
                resizable: true,
                zIndex: 99900000,
                title: "Make Payment"
            });
            $("#dialog").dialog("open");
           
        }
        function OpenInvoice(basepath, filepath)
        {
            window.open(basepath + filepath, 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');
        }
        function GetInsuranceValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdninsurancecode.ClientID %>").value = eventArgs.get_value();

        }
        function Clear() {
            //alert("call");
            document.getElementById("<%=txtInvoiceNo.ClientID%>").value = '';
            document.getElementById("<%=txtEmployerNO.ClientID %>").value = '';
            document.getElementById("<%=txtFromDate.ClientID %>").value = '';
            document.getElementById("<%=txtToDate.ClientID %>").value = '';
            document.getElementById("ctl00_ContentPlaceHolder1_ddlDateValues").value = 0;
        }
        function SetDate() {
            getWeek();
            var selValue = document.getElementById("<%=ddlDateValues.ClientID %>").value;
            if (selValue == 0) {
                document.getElementById("<%=txtToDate.ClientID %>").value = "";
                document.getElementById("<%=txtFromDate.ClientID %>").value = "";
            }
            else if (selValue == 1) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('today');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('today');
            }
            else if (selValue == 2) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getWeek('endweek');
            document.getElementById("<%=txtFromDate.ClientID %>").value = getWeek('startweek');
        }
        else if (selValue == 3) {
            document.getElementById("<%=txtToDate.ClientID %>").value = getDate('monthend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('monthstart');
            }
            else if (selValue == 4) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('quarterend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('quarterstart');
            }
            else if (selValue == 5) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getDate('yearend');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getDate('yearstart');
            }
            else if (selValue == 6) {
                document.getElementById("<%=txtToDate.ClientID %>").value = getLastWeek('endweek');
                document.getElementById("<%=txtFromDate.ClientID %>").value = getLastWeek('startweek');
            } else if (selValue == 7) {
                document.getElementById("<%=txtToDate.ClientID %>").value = lastmonth('endmonth');
                document.getElementById("<%=txtFromDate.ClientID %>").value = lastmonth('startmonth');
            } else if (selValue == 8) {
                document.getElementById("<%=txtToDate.ClientID %>").value = lastyear('endyear');
                document.getElementById("<%=txtFromDate.ClientID %>").value = lastyear('startyear');
            } else if (selValue == 9) {
                document.getElementById("<%=txtToDate.ClientID %>").value = quarteryear('endquaeter');
                document.getElementById("<%=txtFromDate.ClientID %>").value = quarteryear('startquaeter');
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

function OpenPage(BillNo) {

    window.open('Bill_Sys_OpenBill.aspx?bno=' + BillNo, 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');

}
function OpenVerificationPage(BillNo) {

    window.open('Bill_sys_openVerificationDocs.aspx?bno=' + BillNo + '&doctype=1', 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');

}
function OpenDenialPage(BillNo) {

    window.open('Bill_sys_openVerificationDocs.aspx?bno=' + BillNo + '&doctype=3', 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');

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
    </script>



            <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 50%;">
                <tr>
                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 100%"></td>
                                <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%; background-color: White;">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td style="text-align: left; width: 50%; height: 30%; vertical-align: top;">
                                                            <table style="text-align: left; width: 100%;">
                                                                <tr>
                                                                    <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                        <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%; border: 0px solid #B5DF82;">
                                                                            <tr>
                                                                                <td style="width: 40%; height: 0px;" align="center">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82"
                                                                                        onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                        <tr>
                                                                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                                <b class="txt3">Search Parameters</b>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="td-widget-bc-search-desc-ch">Invoice No
                                                                                            </td>
                                                                                            <td class="td-widget-bc-search-desc-ch">Employer
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="80%"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtEmployerNO" runat="server" Width="80%" autocomplete="off"></asp:TextBox>
                                                                                                <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoEmp" EnableCaching="true"
                                                                                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtEmployerNO"
                                                                                                    ServiceMethod="GetEmployer" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                                                                                                </ajaxcontrol:AutoCompleteExtender>

                                                                                            </td>


                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td class="td-widget-bc-search-desc-ch">Invoice Date
                                                                                            </td>
                                                                                            <td class="td-widget-bc-search-desc-ch">From
                                                                                            </td>
                                                                                            <td class="td-widget-bc-search-desc-ch" align="center">To
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlDateValues" runat="Server" Width="80%">
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
                                                                                                <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                    MaxLength="10" Width="71%"></asp:TextBox>

                                                                                                <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                                    MaxLength="10" Width="71%"></asp:TextBox>
                                                                                                <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>

                                                                                            <td>&nbsp;

                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                                            

                                                                                                <asp:Button Style="width: 80px" ID="btnSearch" runat="server"
                                                                                                    Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                                                                                &nbsp;
     <input style="width: 80px" id="btnClear1" type="button" value="Clear" onclick="Clear();"
         runat="server" visible="true" />

                                                                                            </td>

                                                                                        </tr>




                                                                                        <tr>
                                                                                            <td colspan="4"></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>



                                    </table>
                                </td>
                                <td style="width: 10px; height: 100%;">&nbsp;


                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                &nbsp;
                     
                           <td>
                               <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                   PopupButtonID="imgbtnFromDate" />
                               <ajaxcontrol:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                   PopupButtonID="imgbtnToDate" />

                               <table style="vertical-align: middle; width: 193%">
                                   <tbody>
                                       <tr>
                                           <td style="vertical-align: middle; width: 30%" align="left">
                                               <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Text="Search:"></asp:Label>
                                               <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                   CssClass="search-input">
                                               </gridsearch:XGridSearchTextBox>
                                           </td>
                                           <td style="vertical-align: middle; width: 30%" align="left"></td>
                                           <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">Record Count:<%= this.grdInvoiceSearch.RecordCount %>| Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                               <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                   Text="Export TO Excel">
                                                                                <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                           </td>
                                       </tr>
                                   </tbody>
                               </table>
                               <xgrid:XGridViewControl ID="grdInvoiceSearch" runat="server" Height="148px" Width="206%"
                                   CssClass="mGrid" AllowSorting="true"
                                   PagerStyle-CssClass="pgr" PageRowCount="50" DataKeyNames="sz_invoice_id,SZ_EMPLOYER_NAME,dt_invoice_date,VisitDate,mn_invoice_amount,mn_invoice_paid_amount,mn_invoice_balance_amount,sz_status_code"
                                   XGridKey="Invoice_payment" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                   HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                   ShowExcelTableBorder="true" ExcelFileNamePrefix="InvoiceSearch" MouseOverColor="0, 153, 153"
                                   AutoGenerateColumns="false" ExportToExcelColumnNames="Invoice No.#,Employer,Invoice Date,Visit Date,Invoice Amount,Paid Amount,Balance Amount,Status Code"
                                   ExportToExcelFields="sz_invoice_id,SZ_EMPLOYER_NAME,dt_invoice_date,VisitDate,mn_invoice_amount,mn_invoice_paid_amount,mn_invoice_balance_amount,sz_status_code">
                                   <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                   <PagerStyle CssClass="pgr"></PagerStyle>
                                   <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                   <Columns>

                                       <%-- 1--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                           HeaderText="Invoice No.#" DataField="sz_invoice_id" SortExpression="sz_invoice_id" />
                                       <%--  2--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                           SortExpression="SZ_EMPLOYER_NAME" HeaderText="Employer" DataField="SZ_EMPLOYER_NAME" />
                                       <%--  3--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                           SortExpression="dt_invoice_date" HeaderText="Invoice Date" DataField="dt_invoice_date" />
                                       <%--  4--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                           SortExpression="VisitDate" HeaderText="Visit Date" DataField="VisitDate" />
                                       <%--  5--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:C}"
                                           SortExpression="mn_invoice_amount" HeaderText="Invoice Amount" DataField="mn_invoice_amount" />
                                       <%--  6--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:C}"
                                           SortExpression="mn_invoice_paid_amount" HeaderText="Paid Amount" DataField="mn_invoice_paid_amount" />
                                       <%--  7--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:C}"
                                           SortExpression="mn_invoice_balance_amount" HeaderText="Balance Amount" DataField="mn_invoice_balance_amount" />
                                       <%--  8--%>
                                       <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                           SortExpression="sz_status_code" HeaderText="Status Code" DataField="sz_status_code" />
                                        <asp:TemplateField>
                                              <ItemTemplate>
                                                 <a id="lnkBill" href="#" onclick="javascript:MakePayment('<%# DataBinder.Eval(Container, "DataItem.sz_invoice_id")%> ','<%# DataBinder.Eval(Container, "DataItem.mn_invoice_balance_amount")%> ','<%# DataBinder.Eval(Container, "DataItem.mn_invoice_amount")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_EMPLOYER_NAME")%> ','<%# DataBinder.Eval(Container, "DataItem.sz_employer_id")%>');">
                                                     Make Payment</a>
                                                  </ItemTemplate>
                                       </asp:TemplateField>
                                   <asp:TemplateField>
                                              <ItemTemplate>
                                                 <a id="lnkBill" href="#" onclick="javascript:OpenInvoice('<%# DataBinder.Eval(Container, "DataItem.BasePath")%> ','<%# DataBinder.Eval(Container, "DataItem.OpenPath")%> ');"> 
                                                     View</a>
                                                  </ItemTemplate>
                                       </asp:TemplateField>
                                   </Columns>



                               </xgrid:XGridViewControl>
                           </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>


       
    <asp:TextBox ID="txtCompanyId" runat="server" Width="80%" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hdninsurancecode" runat="server" />
     <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
</div>

</asp:Content>

