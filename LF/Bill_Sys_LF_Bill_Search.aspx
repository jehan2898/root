<%@ page language="C#" autoeventwireup="true" inherits="LF_Bill_Sys_LF_Bill_Search" masterpagefile="~/LF/MasterPage.master" CodeFile="Bill_Sys_LF_Bill_Search.aspx.cs" title="Bill Search" %>
<%--"--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000" >
    </asp:ScriptManager>

<script language="javascript" type="text/javascript">
function checkSelected() {

    var f = document.getElementById("<%= grdBillSearch.ClientID %>");
    var str = 1;
    var iflag = false;
    for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
        if (f.getElementsByTagName("input").item(i).type == "checkbox") {
            if (f.getElementsByTagName("input").item(i).checked) {
                iflag = true;
                break;
            }
        }

    }

    if (iflag == false) {
        alert('Please select record.');
        return false;
    }
    else {
        return true;
    }
}


function SetTransferDate() {
    alert('hi');
}

function SelectAll(ival) {
    var f = document.getElementById("<%= grdBillSearch.ClientID %>");
    var str = 1;
    for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
        if (f.getElementsByTagName("input").item(i).type == "checkbox") {
            if (f.getElementsByTagName("input").item(i).disabled == false) {
                f.getElementsByTagName("input").item(i).checked = ival;
            }
        }
    }
}


function doClick(buttonName, e) {
    //the purpose of this function is to allow the enter key to 
    //point to the correct button to click.
    var key;

    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;     //firefox


    if (key == 13) {
        //Get the button the user wants to have clicked
        var btn = document.getElementById(buttonName);
        alert("btn" + btn);
        alert('hi');
        if (btn != null) { //If we find the button click it
            btn.click();
            event.keyCode = 0
        }
    }
}

function CheckForInteger(e, charis) {

    var keychar;
    if (navigator.appName.indexOf("Netscape") > (-1)) {
        var key = ascii_value(charis);

        if (e.charCode == key || e.charCode == 0) {
            return true;
        }
        else {
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

function ValidateALL() {
    if (confirm('Downloading large number of bills with case documents \n might take some time and It might  generate Multipal zip files  \n Do you want to continue? ?')) {
        return true;
    } else {
        //hc.value=="NO";
        return false;
    }
}
function OpenDocumentManager(CaseNo, CaseId, cmpid) {
    window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid=' + CaseId + '&caseno=' + CaseNo + '&cmpid=' + cmpid, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
}
</script>

    <div>
        <table width="100%">
            <tr>
                <td align="right" colspan="3">
                    <%-- <asp:LinkButton ID="lnklogout" runat="server" OnClick="lnklogout_Click">Logout</asp:LinkButton>--%>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table width="42%" style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82;
                        border-bottom: 1px solid #B5DF82">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                <b class="txt3">Search Parameters</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Search:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtsearch" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                Transfer Date
                            </td>
                            <td style="width: 5%">
                               &nbsp; &nbsp; &nbsp;  From &nbsp; &nbsp&nbsp; &nbsp&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlDateValues" runat="Server" Width="100%">
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
                            <td style="width: 5%">
                                <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                    MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                    PopupButtonID="imgbtnFromDate" />
                                &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                    MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                    PopupButtonID="imgbtnToDate" />
                            </td>
                        </tr>
                        <tr>
                            <%--<td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                            DisplayAfter="10">
                                            <ProgressTemplate>
                                                <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px;
                                                    left: 600px" runat="Server">
                                                    <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                        Height="25px" Width="24px"></asp:Image>
                                                    Loading...</div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        &nbsp;
                                        <asp:Button Style="width: 80px" ID="btnsearch" OnClick="btnsearch_Click" runat="server"
                                            Text="Search"></asp:Button>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>--%>
                            <td colspan="2" align="center">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="80px"
                                            Text="Search"></asp:Button>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="vertical-align: middle; width: 100%">
                        <tr>
                            <td align="right">
                                <%-- <asp:Button ID="Button2" runat="server" Text="Update" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UP_grdBillSearch" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                        <b class="txt3">Bills</b>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdBillSearch"
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
                                    <tr id="trSearch" runat="server">
                                        <td style="vertical-align: middle; width: 80%" align="left">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Search:" Font-Size="Small"></asp:Label>
                                            <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="True"
                                                CssClass="search-input">
                                            </gridsearch:XGridSearchTextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="vertical-align: middle; width: 20%; text-align: right" align="right" colspan="2">
                                            Record Count:<%= this.grdBillSearch.RecordCount %>| Page Count:
                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                            </gridpagination:XGridPaginationDropDown>
                                            <asp:LinkButton ID="lnkExportToExcel" runat="server" Text="Export TO Excel" 
                                                onclick="lnkExportToExcel_Click">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                    
                                        <td align="right">
                                            <asp:CheckBox ID="chkCaseDoc" runat="server" Text="Include Case Documents" />
                                        </td>
                                        <td align="right">
                                            <asp:Button runat="server" Text="Download" ID="btnDownload" OnClick="btnDownload_Click"
                                                Visible="true" />
                                            <asp:Button runat="server" Text="Download All"  ID="btnDownloadAll" Visible="false" OnClick="btnDownloadAll_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <xgrid:XGridViewControl ID="grdBillSearch" runat="server" Width="100%" CssClass="mGrid"
                                            ExportToExcelFields="SZ_CASE_ID,SZ_BILL_NUMBER,DT_BILL_DATE,PATIENT_NAME,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,mn_transferred_amount,SZ_BILL_STATUS_NAME,SPECIALITY,dt_transferred_on,SZ_VISIT_DATE,PROVIDER,PAYMENT_DATE"
                                            ExportToExcelColumnNames="Case ID,BillNo,Bill Date,Patient Name,Bill Amount,Paid,Outstanding,Transferred amount,Status,Specialty,Transfer Date,Visit Date,Provider,Payment Date"
                                            AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="BillSearch"
                                            ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                            HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                            AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_LF_Desk_Office_Info_Search"
                                            DataKeyNames="PATIENT_NAME,SZ_COMPANY_ID,SPECIALITY_ID,SZ_VISIT_DATE,PROVIDER,PAYMENT_DATE"
                                            PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true">
                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                            <%--,,,INS_NAME,,Transfer_Date,Index_No,LAW_FIRM_CASE_ID,Purchase_Date,sz_location_name,SZ_VISIT_DATE,--%>
                                            <%--,,,,,,Transfer Date,Index Number,Law Firm ID,Purchase Date,Location,Event Date,--%>
                                            <Columns>
                                                <%--0--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    Visible="true" HeaderText="Case ID" DataField="SZ_CASE_ID" SortExpression="TBT.SZ_CASE_ID" />
                                                    <%--1--%>
                                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    Visible="true" HeaderText="Case #" DataField="SZ_CASE_NO" SortExpression="MCM.sz_case_no" />
                                                <%--2--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    Visible="true" HeaderText="Patient Name" DataField="PATIENT_NAME" SortExpression="PATIENT_NAME" />
                                                <%--3--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    HeaderText="Bill No" DataField="SZ_BILL_NUMBER" SortExpression="rtrim(ltrim(TBT.SZ_BILL_NUMBER))" />
                                                <%--4--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    HeaderText="Bill Date" DataField="DT_BILL_DATE" SortExpression="TBT.DT_BILL_DATE" />
                                                <%--5--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    Visible="true" HeaderText="Visit Date" DataField="SZ_VISIT_DATE" />
                                                <%--  6--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    Visible="true" HeaderText="Provider Name" DataField="PROVIDER" SortExpression="MO.SZ_OFFICE" />
                                                <%-- 7--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    SortExpression="convert(nvarchar(10),tbt.dt_transferred_on,101)" HeaderText="Transfer Date"
                                                    DataField="dt_transferred_on" />
                                                <%--  8--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    HeaderText="Bill Status" DataField="SZ_BILL_STATUS_NAME" SortExpression="MBS.SZ_BILL_STATUS_NAME" />
                                                <%--  9--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                    SortExpression="CAST(TBT.FLT_BILL_AMOUNT  as float)" HeaderText="Bill Amount"
                                                    DataFormatString="{0:C}" DataField="FLT_BILL_AMOUNT" />
                                                <%-- 10--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                    SortExpression="CAST(ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TBT.SZ_BILL_NUMBER),0)as float)"
                                                    HeaderText="Paid Amount" DataFormatString="{0:C}" DataField="PAID_AMOUNT" />
                                                <%--  11--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                    SortExpression="CAST(TBT.FLT_BALANCE as float)" HeaderText="Outstanding Amount"
                                                    DataFormatString="{0:C}" DataField="FLT_BALANCE" />
                                                <%--  12--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                    SortExpression="CAST(TBT.mn_transferred_amount as float)" HeaderText="Transferred Amount"
                                                    DataFormatString="{0:C}" DataField="mn_transferred_amount" />
                                                <%--13--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                    SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME  + ' '  + MST_PATIENT.SZ_PATIENT_LAST_NAME"
                                                    HeaderText="Patient Name" DataField="PATIENT_NAME" Visible="false" />
                                                <%--14--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    Visible="false" HeaderText="Company ID" DataField="SZ_COMPANY_ID" />
                                                <%--15--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                    HeaderText="specialty" DataField="SPECIALITY_ID" Visible="false" />
                                                <%--  16--%>
                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                    Visible="true" HeaderText="Last Payment Date" DataField="PAYMENT_DATE" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <%--17--%>
                                                <%-- Adding document manager URL - FR-228 --%>
                                                <asp:TemplateField HeaderText="Docs" Visible="false">
                                                    <ItemTemplate>
                                                        <img alt="" onclick="javascript:OpenDocumentManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');"
                                                            src="Images/grid-doc-mng.gif" style="border: none; cursor: pointer;" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <%--18--%>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <center>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                ToolTip="Select All" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:CheckBox ID="ChkselectBllno" runat="server" /></center>
                                                    </ItemTemplate>
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
<asp:AsyncPostBackTrigger ControlID="con"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnSearch"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtBillNo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtAllBillno" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
