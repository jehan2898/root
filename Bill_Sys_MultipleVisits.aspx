<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_MultipleVisits.aspx.cs" Inherits="Bill_Sys_MultipleVisits" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
    <script type="text/javascript">
        function validation() {
            if (document.getElementById("<%= txtAppointdate.ClientID %>").value == '' || document.getElementById("<%= ddlDoctor.ClientID %>").value == '---select---') {
                return false;
            } else {
                return true;
            }
        }

        function ShowConfirmation() {
            var grdProc = document.getElementById('_ctl0_ContentPlaceHolder1_grdCaseMaster');
            var count = 0;
            var countVisitType = 0;
            var idstatus = 0;

            if (grdProc.rows.length > 0) {
                var count = 0;
                for (var i = 1; i < grdProc.rows.length; i++) {
                    var cell = grdProc.rows[i].cells[0];
                    for (j = 0; j < cell.childNodes.length; j++) {
                        if (cell.childNodes[j].type == "checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report") {
                            if (cell.childNodes[j].checked) {
                                count = count + 1;
                            }
                        }
                    }
                }
            }

            if (grdProc.rows.length > 0) {
                for (var i = 1; i < grdProc.rows.length; i++) {
                    var cell = grdProc.rows[i].cells[0];
                    var cell7 = grdProc.rows[i].cells[1];

                    //alert(navigator.appName);

                    if (navigator.appName == "Microsoft Internet Explorer") {
                        cell7.childNodes[0].style.backgroundColor = "";
                        cell7.childNodes[0].style.backgroundColor = "";

                        for (j = 0; j < cell.childNodes.length; j++) {
                            if (cell.childNodes[j].type == "checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report") {
                                if (cell.childNodes[j].checked) {
                                    if (cell7.childNodes[0].rows[0].cells[0].childNodes[0].checked == true || cell7.childNodes[0].rows[1].cells[0].childNodes[0].checked == true || cell7.childNodes[0].rows[2].cells[0].childNodes[0].checked == true) {
                                        cell7.childNodes[0].style.backgroundColor = "";
                                    }
                                    else {
                                        cell7.childNodes[0].style.backgroundColor = "Yellow";
                                        countVisitType = countVisitType + 1;
                                        idstatus = 1;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        cell7.childNodes[1].style.backgroundColor = "";
                        cell7.childNodes[1].style.backgroundColor = "";

                        for (j = 0; j < cell.childNodes.length; j++) {
                            if (cell.childNodes[j].type == "checkbox" && grdProc.rows[i].cells[4].innerHTML != "Received Report") {
                                if (cell.childNodes[j].checked) {
                                    if (cell7.childNodes[1].rows[0].cells[0].childNodes[0].checked == true || cell7.childNodes[1].rows[1].cells[0].childNodes[0].checked == true || cell7.childNodes[1].rows[2].cells[0].childNodes[0].checked == true) {
                                        cell7.childNodes[1].style.backgroundColor = "";
                                    }
                                    else {
                                        cell7.childNodes[1].style.backgroundColor = "Yellow";
                                        countVisitType = countVisitType + 1;
                                    }
                                }
                            }
                        }
                    }
                }

                if (count > 0) {
                    if (idstatus == 0) {

                        //var frmValid = formValidator('form1', 'txtAppointdate,ddlDoctor');
                        var frmValid = validation();


                        if (frmValid != false) {
                            if (confirm('You have selected ' + count + ' patients for adding their visit(s), Do you want to proceed?') == true) {
                                return true;
                            }
                            else {
                                return false;
                            }

                            if (countVisitType > 0) {
                                alert("Select a visit type");
                                return false;
                            }
                        }
                        else {
                            if (document.getElementById("<%= txtAppointdate.ClientID %>").value == '') {
                                alert('Enter visit date');
                            }
                            else if (document.getElementById("<%= ddlDoctor.ClientID %>").value == '---select---') {
                                alert('Select Doctor');
                            }
                            return false;
                        }
                    }
                    else {
                        alert('Select a visit type');
                        return false;
                    }
                }
                else {
                    alert('Select a visit type');
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function SelectAll(id) {
            var frm = document.forms[0];
            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox") {
                    if (frm.elements[i].id != '_ctl0_ContentPlaceHolder1_Chkmult') {
                        frm.elements[i].checked = true;
                    }
                }
            }
        }

        function SelectAll1(ival) {
            var f = document.getElementById("<%= grdCaseMaster.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSave')">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;height: 100%; background-color: White;">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updfate4" runat="server">
                        <contenttemplate>
                            <UserMessage:MessageControl runat="server" ID="usrMessage" />
                        </contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;padding-top: 3px; height: 100%; vertical-align: top;">
                    <table style="width: 100%; height: 30%">
                        <tr>
                            <td align="center" colspan="3" style="height: 30%">
                                <asp:Label CssClass="message-text" ID="lblMsg" runat="server"></asp:Label>
                                </br>
                                <asp:Label CssClass="message-text" ID="lblDateMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; height: 30%">
                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height:0px; border: 1px solid #B5DF82;">
                                    <tr>
                                        <td style="height: 0px;" align="center" colspan="2">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 0px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearchPArameter')">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                        <b class="txt3">Search Parameters</b>
                                                    </td>
                                                </tr>
                                                <tr id="RowLocation" runat="server">
                                                    <td class="td-widget-bc-search-desc-ch" style="width: 40%;text-align: left;">
                                                    Location                                                       
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="width: 55%;">
                                                        <extddl:ExtendedDropDownList 
                                                            ID="extddlLocation" 
                                                            runat="server" 
                                                            Width="97%" 
                                                            Connection_Key="Connection_String"
                                                            Flag_Key_Value="LOCATION_LIST" 
                                                            Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---"
                                                            AutoPost_back="true" 
                                                            OnextendDropDown_SelectedIndexChanged="extddlLocation_extendDropDown_SelectedIndexChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 40%;text-align: left;">
                                                        Case No
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 40px; width: 60%;">
                                                        <asp:TextBox ID="txtCaseNo" Height="80px" runat="server" Width="92%" TextMode="MultiLine" ></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" visible="true" id="trChartNumber">
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 40%;text-align: left;">
                                                        Chart #
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 40px; width: 60%;">
                                                        <asp:TextBox ID="txtChartNumber" Height="80px" runat="server" Width="92%" TextMode="MultiLine" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 40%;text-align: left;">
                                                        Patient Name
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 60%;">
                                                        <asp:TextBox ID="txtPatientName" runat="server" Width="92%" autocomplete="off"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                <td colspan="2" align="right">
                                                <asp:CheckBox  ID="Chkmult" runat="server" Text="Search with Previous" />
                                                </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: center; height: 40px;">
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdoISActivePatient" runat="server" RepeatDirection="Horizontal"
                                                            OnSelectedIndexChanged="rdoISActivePatient_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Active Patient" Selected="True" Value="ACTIVE PATIENT"></asp:ListItem>
                                                            <asp:ListItem Text="All Patient" Value="All Patient"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: center; height: 40px;" colspan="2">
                                                    <asp:Label  id="lblNote" text="* Enter comma separated case-numbers. Note: Patients in inactive status will not appear if 'Active' patients is selected." style="color:Red;Font-Size:10px;" runat="server"></asp:Label>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <contenttemplate>                                                            
                                                            <asp:Button id="btnSearchPArameter" onclick="btnSearchPArameter_Click" runat="server" CssClass="Buttons" Text="Search" __designer:wfdid="w7"></asp:Button> 
                                                            </contenttemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 10%; height: 30%">
                            </td>
                            <td style="width: 50%; height: 30%" valign="top">
                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;border: 1px solid #B5DF82;">
                                    <tr>
                                        <td  align="center" colspan="2">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;vertical-align:top;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearchPArameter')">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                        <b class="txt3">Save Parameters</b>
                                                    </td>
                                                </tr>                                                
                                                <tr>
                                                    <td class="td-widget-bc-search-desc-ch" style="width: 40%;text-align: left;">
                                                        Doctor Name :
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="width: 60%; text-align: left;">
                                                        <asp:DropDownList ID="ddlDoctor" runat="server" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged"
                                                            AutoPostBack="True" Width="250px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 40%;text-align: left;">
                                                        Date :
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 60%; text-align: left;">
                                                        <asp:TextBox ID="txtAppointdate" runat="server"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnAppointdate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnAppointdate"
                                                            TargetControlID="txtAppointdate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr id="RowVisitStatus" runat="server">
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 40%;text-align: left;">
                                                        Visit Status :
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 60%; text-align: left;">
                                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                                            <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                            <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                            <asp:ListItem Value="2">Completed</asp:ListItem>
                                                            <asp:ListItem Value="3">No-show</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="RowProcedureCode" runat="server">
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 40%; text-align: left;">
                                                        Procedure Code :
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 60%;">
                                                        <asp:ListBox ID="lstProcedureCode" runat="server" Width="349px" SelectionMode="Multiple">
                                                        </asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="vertical-align: middle; text-align: center; width: 527px;
                                                        height: 40px;">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <contenttemplate>
                                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Buttons" OnClick="btnSave_Click" />
                                                        </contenttemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; height: 30%">
                            </td>
                            <td style="width: 10%; height: 30%">
                            </td>
                            <td style="width: 50%; height: 30%;vertical-align:top;" align="right">
                                <asp:LinkButton ID="Button1" Style="visibility: hidden;" runat="server" OnClick="Button1_Click"></asp:LinkButton>
                                <a id="hlnkShowSearch"  style="cursor: pointer; height: 240px; vertical-align: top;
                                    text-align: left;" class="Buttons" runat="server" title="Quick Search" visible="false">Quick Search</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%; vertical-align: top;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%; height: 566px;">
                        <tr>
                            <td class="LeftCenter" style="height: 231px">
                            </td>
                            <td class="Center" valign="top" style="height: 231px">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td style="width: 100%">
                                            <table border="0" cellpadding="3" cellspacing="5" class="ContentTable" style="width: 100%">                                                                                                
                                                <tr>
                                                    <td class="ContentLabel" colspan="4">
                                                        <asp:DataGrid Width="100%" ID="grdCaseMaster" CssClass="GridTable" runat="server"
                                                            AutoGenerateColumns="False" >
                                                            <ItemStyle CssClass="GridRow" />
                                                            <Columns>
                                                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                                                <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll1(this.checked);"  ToolTip="Select All" />
                                                                </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:TemplateColumn>
                                                                <%--<asp:TemplateColumn HeaderText="Visit Type">
                                                                    <HeaderStyle HorizontalAlign="Left" Width="50px"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:RadioButtonList ID="listVisitType" BorderColor="" runat="server">
                                                                            <asp:ListItem>IE</asp:ListItem>
                                                                            <asp:ListItem>C</asp:ListItem>
                                                                            <asp:ListItem>FU</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>--%>
                                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case no." ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-HorizontalAlign="left" Visible="true">
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case Id" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-HorizontalAlign="left" Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Case Id" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-HorizontalAlign="left" Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient name" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-HorizontalAlign="left">
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-HorizontalAlign="left">
                                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date Of Accident" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DT_CREATED_DATE" HeaderText="Date Open" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="sz_chart_number" HeaderText="Chart #" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="i_rfo_chart_no" HeaderText="Chart #" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundColumn>
                                                            </Columns>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="4">
                                                        <asp:TextBox ID="txtChartSearch" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtCaseIDSearch" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtPatientLNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtPatientFNameSearch" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="RightCenter" style="width: 10px; height: 231px;">
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftBottom" style="height: 36px">
                            </td>
                            <td class="CenterBottom" style="height: 36px">
                                <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtIsActivePatient" runat="server" Visible="false" Width="2%"></asp:TextBox></td>
                            <td class="RightBottom" style="width: 10px; height: 36px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowSearch"
            PopupControlID="pnlShowSearch" Position="Center" OffsetX="-600" OffsetY="0" />
        <asp:Panel ID="pnlShowSearch" runat="server" Style="left: 200px; top: 300px; width: 300px;
            height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
            border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;
            vertical-align: middle; text-align: center;" DefaultButton="btnQuickSearch">
            <br />
            <table style="text-align: center; vertical-align: middle;">
                <tr>
                    <td>
                        <asp:Label ID="lblCaseIDSearch" runat="server" Text="Case No."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVisitSearch" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPatientLNameSearch" runat="server" Text="Patient Last Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLNameSearch" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPatientFNameSearch" runat="server" Text="Patient First Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFNameSearch" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:Button 
                            ID="btnQuickSearch" 
                            runat="server" 
                            Text="Search" 
                            OnClick="btnQuickSearch_Click"
                            Width="80px" 
                            CssClass="Buttons" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <ajaxToolkit:AutoCompleteExtender 
            runat="server" 
            ID="ajAutoName" 
            EnableCaching="true"
            DelimiterCharacters="" 
            MinimumPrefixLength="1" 
            CompletionInterval="500" 
            TargetControlID="txtPatientName"
            ServiceMethod="GetPatient" 
            ServicePath="AJAX Pages/PatientService.asmx" 
            UseContextKey="true" ContextKey="SZ_COMPANY_ID">
        </ajaxToolkit:AutoCompleteExtender>
    </div>
</asp:Content>

