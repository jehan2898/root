<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ProcedureCode.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ProcedureCode"
    Title="Green Your Bills -Procedure Code Master" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
    <link href="../packages/bootstrap.3.3.7/content/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../packages/bootstrap.3.3.7/content/Scripts/bootstrap.min.js"></script>
    <link href="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
           
            prm.add_endRequest(EndRequest);
            function InitializeRequest(sender, args) {
               
            }
            function EndRequest(sender, args) {
                $('[id*=lstIns]').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    dropRight: false,
                    buttonWidth: 250,
                    nonSelectedText: "---Select---",
                });
            }
            $('[id*=lstIns]').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                dropRight: false,
                buttonWidth: 250,
                nonSelectedText: "---Select---",
            });
        })
        function Confirm_Delete_Code() {
            var f = document.getElementById("<%=grdProcedure.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;


                        }

                    }
                }
            }

            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }
            else {
                var msg = "Do you want to proceed?";
                var result = confirm(msg);
                if (result == true) {
                    return true;
                }
                else {
                    return false;
                }
                //return true;
            }
        }



        function ConfirmDelete() {
            var msg = "Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= grdProcedure.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }
        function ShowChildGrid(obj) {
            var div = document.getElementById(obj);
            div.style.display = 'block';
        }

        function HideChildGrid(obj) {
            var div = document.getElementById(obj);
            div.style.display = 'none';
        }
        function validation() {
            var code = document.getElementById("<%= txtProcedureCode.ClientID %>").value;
            var amt = document.getElementById("<%= txtProcedureAmount.ClientID %>").value;
            var des = document.getElementById("<%= txtProcedureDesc.ClientID %>").value;
            var spe = document.getElementById("ctl00_ContentPlaceHolder1_extddlProCodeGroup").value;
            if (code == "") {
                alert('You can not put code empty');
                return false;
            }
            if (amt == "") {
                alert('You can not put amount empty');
                return false;
            }
            if (des == "") {
                alert('You can not put Description empty');
                return false;
            }
            if (spe == "" || spe == "NA") {
                alert('please select specialty');
                return false;
            }
            debugger;
            var flag = 0;
            var sel = document.getElementById("<%= lstIns.ClientID %>");
            var listLength = sel.options.length;
            for (var i = 0; i < listLength; i++) {
                if (sel.options[i].selected) {
                    flag = 1;
                    break;
                }
            }

            if (flag == 1) {
                var cntamt = document.getElementById("<%= txtContractAmount.ClientID %>").value;
                    var appdate = document.getElementById("<%= txtApplyDate.ClientID %>").value;
                    if (cntamt == "") {
                        alert('You can not put emty contract Amount');
                        return false;
                    }
                    if (appdate == "") {
                        alert('You can not put empty  Contract date');
                        return false;
                    }
                }

                return true;
            }

            function showPateintFrame(procid, spec) {
                var url = "ViewContract.aspx?procid=" + procid + "";
                //alert(url);
                PatientInfoPop.SetContentUrl(url);
                PatientInfoPop.Show();
                return false;
            }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%; background-color: White">
                            <tr>
                                <td class="LeftTop"></td>
                                <td class="CenterTop"></td>
                                <td class="RightTop"></td>
                            </tr>
                            <tr>
                                <td class="LeftCenter" style="height: 100%"></td>
                                <td class="Center" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td class="ContentLabel" style="text-align: center;" colspan="3">
                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                    <ContentTemplate>
                                                        <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%; border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                            <b class="txt3">Parameters</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch">Code</td>
                                                        <td class="td-widget-bc-search-desc-ch">Specialty
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtProcedureCode" runat="server" CssClass="textboxCSS" MaxLength="50" Width="225px"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <extddl:ExtendedDropDownList ID="extddlProCodeGroup" runat="server" Connection_Key="Connection_String" Width="225px"
                                                                Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="--- Select ---" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"></extddl:ExtendedDropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch">Short Description
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch">Amount&nbsp;
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtProcedureDesc" runat="server" CssClass="textboxCSS" MaxLength="300" Width="225px"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtProcedureAmount" runat="server" CssClass="textboxCSS" MaxLength="20" Width="225px"
                                                                onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="textboxCSS" Visible="false">
                                                                <asp:ListItem Value="0"> --Select--</asp:ListItem>
                                                                <asp:ListItem Value="TY000000000000000001">Visits</asp:ListItem>
                                                                <asp:ListItem Value="TY000000000000000002">Treatments</asp:ListItem>
                                                                <asp:ListItem Value="TY000000000000000003" Selected="True">Test</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                    </tr>
                                                    <tr style="height: auto;">
                                                        <td class="td-widget-bc-search-desc-ch">Long Description
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch">Visit Type
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="center" rowspan="3">
                                                            <asp:TextBox ID="txtProcedureLongDesc" runat="server" CssClass="textboxCSS" MaxLength="1500" TextMode="MultiLine" Width="500px" Height="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:RadioButtonList ID="rdoVisitType" runat="server"
                                                                RepeatDirection="Horizontal" Width="65px">
                                                                <asp:ListItem Text="IE" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="FU" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--Style="float: right; margin-right: 90px;"--%>
                                                        </td>
                                                    </tr>

                                                    <tr style="height: auto;">
                                                        <td class="td-widget-bc-search-desc-ch" valign="bottom">Modifier
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtModifier" runat="server" Width="225px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch" style="visibility: hidden">PAS Code
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch" style="visibility: hidden">Revenue Code
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValueCode" runat="server" CssClass="textboxCSS" MaxLength="100" Width="225px" Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtRevCode" runat="server" CssClass="textboxCSS" MaxLength="100" Width="225px" Visible="false"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                    <tr>

                                                        <td class="td-widget-bc-search-desc-ch">1500 Description
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch">Add to Preferred List
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txt1500desc" runat="server" CssClass="textboxCSS" MaxLength="76" Width="500px" Height="25px"></asp:TextBox>

                                                        </td>
                                                        <td align="center">
                                                            <asp:CheckBox ID="chkPrefList" runat="server"></asp:CheckBox>
                                                        </td>

                                                    </tr>

                                                    <tr style="height: auto;">
                                                        <td class="td-widget-bc-search-desc-ch">
                                                            <asp:Label ID="lblModificationDesc" runat="server" Text="Modifier Description" Font-Bold="true" Font-Size="12.5px" Visible="false"></asp:Label>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch">
                                                            <asp:Label ID="lblLocation" runat="server" Text="Location" Font-Bold="true" Font-Size="12.5px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtModifierDesc" runat="server" TextMode="MultiLine" MaxLength="1500" Width="500px" Height="90px" Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td align="center">
                                                            <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="59%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" />
                                                        </td>
                                                    </tr>
                                                    <tr style="height: auto;">

                                                        <td class="td-widget-bc-search-desc-ch" style="visibility: hidden">RVU
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <%--<asp:TextBox ID="txt1500desc" runat="server" TextMode="MultiLine"  MaxLength="76" Width="300px" Height="50px"></asp:TextBox>--%>

                                                            <asp:TextBox ID="txtRVU" runat="server" CssClass="textboxCSS" MaxLength="1500" TextMode="MultiLine" Width="500px" Height="90px" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>

                                                        <td class="td-widget-bc-search-desc-ch">Insurance
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch">Contract Amount
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ListBox runat="server" ID="lstIns" SelectionMode="Multiple"></asp:ListBox>
                                                            <td align="center">

                                                                <asp:TextBox runat="server" ID="txtContractAmount"></asp:TextBox>
                                                            </td>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch">Contract Apply After
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox Width="65px" ID="txtApplyDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                MaxLength="10" CssClass="cinput"></asp:TextBox>
                                                            <asp:Label ID="ltxtDateOfBirth" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>

                                                            <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtApplyDate"
                                                                PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                
                                               
                                                <td></td>
                                            </tr>

                                            <tr>
                                                
                                                <td></td>
                                            </tr>--%>

                                                    <tr>
                                                        <td class="tablecellLabel"></td>
                                                        <td class="tablecellSpace"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:DataGrid ID="grdAmount" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"
                                                                AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" Visible="false">
                                                                <ItemStyle CssClass="GridRow" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_AMOUNT_ID" HeaderText="SZ_PROCEDURE_AMOUNT_ID"
                                                                        Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="SZ_CASE_TYPE_ID" Visible="false"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                                                    <asp:TemplateColumn>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="text-box" MaxLength="10" Text='<%# DataBinder.Eval(Container.DataItem, "SZ_AMOUNT") %>'
                                                                                onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="SZ_PROCEDURE_CODE_ID"
                                                                        Visible="false"></asp:BoundColumn>
                                                                </Columns>
                                                                <HeaderStyle CssClass="GridHeader" />
                                                            </asp:DataGrid></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click" />
                                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px" />
                                                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                                Width="80px" />
                                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: right; height: 44px;">
                                                <asp:Button ID="btnUpdatePre" runat="server" Text="Update Preferred List" OnClick="btnUpdatePre_Click" />
                                                <asp:Button ID="btnUpdateModifier" runat="server" Text="Update Modifier" OnClick="btnUpdateModifier_Click" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table style="vertical-align: middle; width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="vertical-align: middle; width: 30%" align="left">
                                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" Text="Search:"></asp:Label>
                                                                            <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                CssClass="search-input">
                                                                            </gridsearch:XGridSearchTextBox>
                                                                        </td>
                                                                        <td style="vertical-align: middle; width: 30%" align="left">
                                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                                DisplayAfter="10">
                                                                                <ProgressTemplate>
                                                                                    <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                        runat="Server">
                                                                                        <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                            Height="25px" Width="24px"></asp:Image>
                                                                                        Loading...
                                                                                    </div>
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </td>
                                                                        <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">Record Count:<%= this.grdProcedure.RecordCount%>| Page Count:
                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                    </gridpagination:XGridPaginationDropDown>
                                                                            <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                Text="Export TO Excel">
                                                        <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow: scroll; height: 400px; width: 99%;">
                                                                <xgrid:XGridViewControl ID="grdProcedure" runat="server" Height="148px" Width="100%"
                                                                    CssClass="mGrid" OnRowCommand="grdProcedure_RowCommand" AllowSorting="true" PagerStyle-CssClass="pgr"
                                                                    PageRowCount="50" XGridKey="Bill_Sys_ProcedureCodes" GridLines="None" AllowPaging="true"
                                                                    DataKeyNames="SZ_PROCEDURE_ID, SZ_PROCEDURE_CODE, SZ_CODE_DESCRIPTION, SZ_PROCEDURE_GROUP_ID, SZ_PROCEDURE_GROUP, I_VISIT_TYPE, SZ_TYPE_CODE_ID, SZ_TYPE_ID, FLT_AMOUNT, SZ_MODIFIER,BT_ADD_TO_PREFERRED_LIST,SZ_REV_CODE,SZ_VALUE_CODE,SZ_CODE_LONG_DESC,SZ_MODIFIER_LONG_DESC,SZ_RVU,sz_location_id,SZ_1500_DESC"
                                                                    AlternatingRowStyle-BackColor="#EEEEEE" HeaderStyle-CssClass="GridViewHeader"
                                                                    ContextMenuID="ContextMenu1" EnableRowClick="false" MouseOverColor="0, 153, 153"
                                                                    ExcelFileNamePrefix="ProcedureCode" ShowExcelTableBorder="true"
                                                                    ExportToExcelColumnNames="Procedure Code, Description,Amount,Specialty,Modifier,Rev Code,Value Code"
                                                                    ExportToExcelFields="SZ_PROCEDURE_CODE,SZ_CODE_DESCRIPTION,FLT_AMOUNT,SZ_PROCEDURE_GROUP,SZ_MODIFIER,SZ_REV_CODE,SZ_VALUE_CODE"
                                                                    AutoGenerateColumns="false">
                                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                    <Columns>
                                                                        <%--1--%>
                                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSelect" runat="server" Text="Select"
                                                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                    CommandName="Select" CausesValidation="false"> </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <%--2--%>
                                                                        <asp:BoundField DataField="SZ_PROCEDURE_CODE" HeaderStyle-HorizontalAlign="center" HeaderText="Procedure Code" Visible="true"></asp:BoundField>
                                                                        <%--<asp:TemplateField HeaderText="Procedure Code"  SortExpression="">
                                                                    <itemtemplate>
                                                                    <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS"  font-size="15px" 
                                                                        Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                        <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>                                                                                  
                                                                        <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_CODE")%>                                                                                
                                                                    </itemtemplate>
                                                                </asp:TemplateField>--%>
                                                                        <%--3--%>
                                                                        <asp:BoundField DataField="SZ_CODE_DESCRIPTION" HeaderStyle-HorizontalAlign="center" HeaderText="Description" Visible="true"></asp:BoundField>
                                                                        <%--4--%>
                                                                        <asp:BoundField DataField="FLT_AMOUNT" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right" HeaderText="Amount" Visible="True" DataFormatString="{0:C}"></asp:BoundField>
                                                                        <%--5--%>
                                                                        <asp:BoundField DataField="SZ_PROCEDURE_GROUP" HeaderStyle-HorizontalAlign="center" HeaderText="Specialty" Visible="true"></asp:BoundField>
                                                                        <%--6--%>
                                                                        <asp:BoundField DataField="SZ_MODIFIER" HeaderText="Modifier" Visible="true" HeaderStyle-HorizontalAlign="center"></asp:BoundField>
                                                                        <%--7--%>
                                                                        <asp:BoundField DataField="SZ_REV_CODE" HeaderText="Rev Code" Visible="false" HeaderStyle-HorizontalAlign="center"></asp:BoundField>
                                                                        <%--8--%>
                                                                        <asp:BoundField DataField="SZ_VALUE_CODE" HeaderText="Value Code" Visible="true" HeaderStyle-HorizontalAlign="center"></asp:BoundField>
                                                                        <%--9--%>
                                                                        <asp:BoundField HeaderText="ADD TO PREFERRED LIST" DataField="BT_ADD_TO_PREFERRED_LIST"></asp:BoundField>
                                                                        <%--10--%>
                                                                        <asp:BoundField DataField="SZ_CODE_LONG_DESC" HeaderText="Procedure Full Description"></asp:BoundField>
                                                                        <%--11--%>
                                                                        <asp:BoundField DataField="SZ_MODIFIER_LONG_DESC" HeaderText="Modifier Description" Visible="false"></asp:BoundField>
                                                                        <%--12--%>
                                                                        <asp:BoundField DataField="SZ_RVU" HeaderText="RVU" Visible="false"></asp:BoundField>
                                                                        <%--13--%>
                                                                        <asp:BoundField DataField="SZ_LOCATION_NAME" HeaderText="Location" Visible="false"></asp:BoundField>

                                                                        <%--14--%>
                                                                        <asp:BoundField DataField="SZ_1500_DESC" HeaderText="1500 Description" Visible="true"></asp:BoundField>
                                                                        <%--15--%>
                                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--16--%>
                                                                        <asp:BoundField DataField="SZ_PROCEDURE_ID" HeaderText="PROCEDURE ID" Visible="False"></asp:BoundField>
                                                                        <%--17--%>
                                                                        <asp:BoundField DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundField>
                                                                        <%--18--%>
                                                                        <asp:BoundField DataField="I_VISIT_TYPE" HeaderText="VisitType" Visible="false"></asp:BoundField>
                                                                        <%--19--%>
                                                                        <asp:BoundField DataField="SZ_TYPE_CODE_ID" HeaderText="SZ_TYPE_CODE_ID" Visible="False"></asp:BoundField>
                                                                        <%--20--%>
                                                                        <asp:BoundField DataField="SZ_TYPE_ID" HeaderText="Type" Visible="False"></asp:BoundField>
                                                                        <%--21--%>
                                                                        <asp:BoundField DataField="SZ_ROOM_ID" HeaderText="Room" Visible="False"></asp:BoundField>
                                                                        <%--22--%>
                                                                        <asp:BoundField DataField="SZ_TYPE" HeaderText="Type" Visible="False"></asp:BoundField>
                                                                        <%--23--%>
                                                                        <asp:BoundField DataField="SZ_ROOM" HeaderText="Room" Visible="False"></asp:BoundField>
                                                                        <%--24--%>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td colspan="100%">
                                                                                        <div id="div<%# Eval("SZ_PROCEDURE_ID")%>" style="display: block; position: relative; left: 25px;">
                                                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"
                                                                                                EmptyDataText="No Record Found" Width="80%" CellPadding="4" ForeColor="#333333"
                                                                                                GridLines="None">
                                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="SZ_CODE_LONG_DESC" HeaderText="Procedure Full Description" ItemStyle-Width="200px"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="SZ_MODIFIER_LONG_DESC" HeaderText="Modifier Description" ItemStyle-Width="200px"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="SZ_RVU" HeaderText="RVU" ItemStyle-Width="200px"></asp:BoundField>

                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Contract" Visible="true" >
                                                                           
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPateintView" runat="server" Text='View' Visible="false" CommandName="Patient" CommandArgument='<%# Eval("SZ_PROCEDURE_ID") + "," +  Eval("SZ_PROCEDURE_GROUP") %>'></asp:LinkButton>
                                                                                <a id="lnkframePatient" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_PROCEDURE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP")  +"\");" %>'>View</a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </xgrid:XGridViewControl>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>

                                    </table>
                                </td>
                                <td class="RightCenter" style="height: 100%;"></td>
                            </tr>
                            <tr>
                                <td class="LeftBottom"></td>
                                <td class="CenterBottom"></td>
                                <td class="RightBottom"></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:TextBox ID="txtVisitType" runat="server" Visible="False" Width="10px">0</asp:TextBox>
                <asp:TextBox ID="txtVisitID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtRoomId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txthdnCode" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txthdnDesc" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txthdnAmt" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txthdnModifier" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txthdnSpe" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txthdnVisitType" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtPreList" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <asp:TextBox ID="txtLocation" runat="server" Visible="False" Width="10px"></asp:TextBox>
            </td>
        </tr>

        <dx:ASPxPopupControl ID="PatientInfoPop" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="PatientInfoPop" HeaderText="Contract Details" HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
            EnableViewState="True" Width="800px" ToolTip="Patient Information" PopupHorizontalOffset="0"
            PopupVerticalOffset="0" AutoUpdatePosition="true" ScrollBars="Auto"
            RenderIFrameForPopupElements="Default" Height="500px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
    </table>
</asp:Content>
