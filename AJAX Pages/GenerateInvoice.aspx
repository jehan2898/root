<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="GenerateInvoice.aspx.cs" Inherits="AJAX_Pages_GenerateInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <script type="text/javascript">

        function Validate() {
            
            var ddl = document.getElementById('ctl00_ContentPlaceHolder1_extddlEmployer');            
            if (ddl.value == "NA") {
                alert('Please select theEmployer');
                return false;
            } else {
                return true;
            }

        }

        function CheckSelect() {
            var f = document.getElementById("<%=grdVisits.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;


                        }

                    }
                }
            }

            if (bfFlag == false) {
                alert('Please select atleast one Case to process.');
                return false;
            }
            else {
                return true;
            }
        }

        function SelectAll1(ival) {
            var f = document.getElementById("<%= grdVisits.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
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
    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="text-align: left; width: 50%; vertical-align: top;">
                                                    <table style="text-align: left; width: 100%;">
                                                        <tr>
                                                            <td style="text-align: left; width: 50%; vertical-align: top;">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                    height: 50%; border: 0px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width: 40%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: 1px solid #B5DF82;
                                                                                border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Visit Date
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        From
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        To
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
                                                                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="76%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                            MaxLength="10" Width="70%"></asp:TextBox>
                                                                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Employer
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="2">
                                                                                        Case#
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <extddl:ExtendedDropDownList ID="extddlEmployer" runat="server" Width="97%" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="EMPLOYER_LIST" Procedure_Name="SP_MST_EMPLOYER_COMPANY" Selected_Text="---Select---"
                                                                                            AutoPost_back="false" />
                                                                                    </td>
                                                                                    <td colspan="2" >
                                                                                    <asp:TextBox ID="txtCaseNo" runat="server" TextMode="MultiLine" Height="80" Width="94%" ></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                <td colspan="3">
                                                                                &nbsp;
                                                                                </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="vertical-align: middle; text-align: center">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                                                                    DisplayAfter="10">
                                                                                                    <ProgressTemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px;
                                                                                                            left: 600px" runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Loading...
                                                                                                        </div>
                                                                                                    </ProgressTemplate>
                                                                                                </asp:UpdateProgress>
                                                                                                &nbsp;
                                                                                                <asp:Button Style="width: 80px" ID="btnSearch" runat="server" Text="Search" 
                                                                                                    onclick="btnSearch_Click"></asp:Button>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <%-- <td>
                                                                                    
                                                                                    </td>--%>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 40%; vertical-align: top" class="td-widget-lf-holder-ch" id="td2"
                                                                            runat="server">
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
                                <tr>
                                    <td style="width: 100%">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <table width="100%" border="0">
                                                    <tr>
                                                        <td style="width: 20%" align="left">
                                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:UpdateProgress ID="UpdateProgress1233" runat="server" AssociatedUpdatePanelID="UpdatePanel13"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="Div1" style="vertical-align: bottom; position: absolute; top: 350px; left: 600px"
                                                                                runat="Server">
                                                                                <asp:Image ID="img1234" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td style="width: 80%" align="right">
                                                        <asp:Button Text="Generate Invoice" ID="btnGenerateInvoice" runat="server" 
                                                                onclick="btnGenerateInvoice_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: auto;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div style="width: 100%" id="dvSrch">
                                                    <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                                        width: 100%; border-bottom: #b5df82 1px solid; height: auto" cellspacing="0"
                                                        cellpadding="0" align="left">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 413px" class="txt2" valign="middle" align="left" bgcolor="#b5df82"
                                                                    colspan="6" height="28">
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Visits"></asp:Label>
                                                              
                                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                runat="Server">
                                                                                <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...</div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 1017px" colspan="5">
                                                                <div style="height:450px;overflow:scroll;">
                                                                 <asp:DataGrid Width="100%" ID="grdVisits" CssClass="mGrid" runat="server"
                                                        AutoGenerateColumns="False">
                                                    <AlternatingItemStyle BackColor="#EEEEEE"></AlternatingItemStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll1(this.checked);"
                                                                        ToolTip="Select All" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateColumn>
                                                    
                                                             <asp:BoundColumn DataField="CASE_ID" HeaderText="Case Id" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="EMPLOYER_ID" HeaderText="Employer Id" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="VISIT_ID" HeaderText="visit Id" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="CASE_NO" HeaderText="Case#" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left" Visible="true">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            
                                                            
                                                            <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="VISIT_DATE" HeaderText="Visit Date" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="ACCIDENT_DATE" HeaderText="DOA" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="EMPLOYER" HeaderText="Employer" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                               <asp:BoundColumn DataField="CODE" HeaderText="Code" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            
                                                        </Columns>
                                                       <HeaderStyle BackColor="#b5df82" Font-Bold="true" />
                                                    </asp:DataGrid>
                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="height: auto; width: 100%; border: 0px solid #B5DF82; background-color: White;">
                                            <tr>
                                                <td align="right" style="width: 50%; height: 20px">
                                                </td>
                                                <td align="left" style="width: 50%; height: 20px">
                                                    <asp:TextBox ID="txtRange" runat="server" Visible="false" Width="15px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtFromDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10px; height: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <%--<td style="width: 10px">
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
