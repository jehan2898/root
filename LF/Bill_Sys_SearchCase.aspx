<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LF/MasterPage.master"
    CodeFile="Bill_Sys_SearchCase.aspx.cs" Inherits="PatientList" Title="Search Case" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000" />

    <script type="text/javascript">
        function DownloadBatch(path) {
            alert(path);
            window.open(path)
        }
        function OpenDocumentManager(CaseNo, CaseId, cmpid) {
            window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid=' + CaseId + '&caseno=' + CaseNo + '&cmpid=' + cmpid, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
        }

        function Test() {

        }
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
        function OpenDocManager(path) {
            window.open(path, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
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

        function CloseButton() {
            alert('hi');
        }

        function ValidateALL() {

            var hD = document.getElementById("<%= hDownlaodNumber.ClientID %>");

            var hC = document.getElementById("<%= hCount.ClientID %>");

            if (confirm('Downloading large number of bills with case documents \n might take some time and It might  generate Multipal zip files  \n Do you want to continue? ?')) {
                hC.value = "YES";

                var drd = document.getElementById('ctl00_ContentPlaceHolder1_drdBatchStatus');
                var hB = document.getElementById("<%= hBatch.ClientID %>");
                var hS = document.getElementById("<%= hStatus.ClientID %>");
                hB.value = "";
                hS.value = "";
                var hD = document.getElementById("<%= hDnl.ClientID %>");
                hD.value = "dnl";
                if (confirm('Do you want to create batch')) {
                    hB.value = "Y";
                    hS.value = "ONE";
                    return true;
                } else {
                    hB.value = "N";
                    hS.value = "ONE";
                    return true;
                }



            } else {

                return false;
            }




        }




        function Validate() {
            var flag = 0;
            var cnt = 0


            var f = document.getElementById("<%= grid.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        flag = 1;
                        cnt++;
                    }
                }
            }
            if (flag == 0) {
                alert('Please Select At Least One Record');
                return false;
            } else {
                var hI = document.getElementById("<%= hIsCount.ClientID %>");

                var hD = document.getElementById("<%= hDownlaodNumber.ClientID %>");
                //alert(hD.value);
                var c = parseInt(hD.value);
                // alert(c);
                if (cnt > c) {
                    hI.value = "Y";

                    //alert(cnt);
                    var hC = document.getElementById("<%= hCount.ClientID %>");
                    if (confirm('batch size cannot exceed ' + c + ' patients. Only the first ' + c + ' patients will be downloaded. Do you want to continue?')) {
                        hC.value = "YES";
                        hI.value = "N";
                    } else {
                        //hc.value="NO";
                        return false;
                    }
                } else {
                    hI.value = "N";
                }
                var drd = document.getElementById('ctl00_ContentPlaceHolder1_drdBatchStatus');
                var hB = document.getElementById("<%= hBatch.ClientID %>");
                var hS = document.getElementById("<%= hStatus.ClientID %>");
                hB.value = "";
                hS.value = "";
                if (confirm('Do you want to create batch')) {
                    var hD = document.getElementById("<%= hDnl.ClientID %>");
                    hD.value = "dnl";
                    if (drd.value == "1") {

                        hB.value = "Y";
                        hS.value = "ONE";



                    }

                    else if (drd.value == "3") {
                        hB.value = "Y";
                        hS.value = "THREE";
                    }

                    else if (drd.value == "0") {
                        hB.value = "Y";
                        hS.value = "ALL";
                    }

                } else {
                    var hD = document.getElementById("<%= hDnl.ClientID %>");
                    hD.value = "dnl";

                    if (drd.value == "1") {
                        hB.value = "N";
                        hS.value = "ONE";
                    }
                    else if (drd.value == "3") {
                        hB.value = "N";
                        hS.value = "THREE";
                    }

                    else if (drd.value == "0") {
                        hB.value = "N";
                        hS.value = "ALL";
                    }
                }

            }
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= grid.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    f.getElementsByTagName("input").item(i).checked = ival;

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

        function OnCallbackComplete(s, e) {
            expLoadingPanel.Hide();
            var url = "../LF/DownloadFiles.aspx";
            IFrame_DownloadFiles.SetContentUrl(url);
            IFrame_DownloadFiles.Show();
            return false;
        }

        function ExportToGBBHeader() {
            expLoadingPanel.Show();
            Callback1.PerformCallback();
        }

    </script>

    <script language="javascript">


        function click() {
            var hD = document.getElementById("<%= hDnl.ClientID %>");

            if (hD.value == '') {//alert(hD.value);
                return true;
            }
            if (hD.value == 'dnl') {
                // alert(hD.value);
                alert("Please wait. Download in Progress");
                return false;
            } else {
                return true;
            }

        }
        document.onmousedown = click
    </script>

    <!-- empty line -->
    <div id="psearch-search" style="height: 250px; position: relative; z-index: 0;">
        <div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" id="table-inner-menu-diplay">
                <tr>
                    <td width="40%" align="left">
                        <div id="divsub" style="width: 100%;" onkeypress="javascript:return WebForm_FireDefaultButton(event,'ctl00_ContentPlaceHolder1_btnSearch')">
                            <table>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" class="border" style="width: 97%">
                                <tr>
                                   <%-- <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                        DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsurance"
                                        ServiceMethod="GetInsurance" ServicePath="LF_INS.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                    </ajaxcontrol:AutoCompleteExtender>--%>
                                    <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajAutoClaim" EnableCaching="true"
                                        DelimiterCharacters=";, :" MinimumPrefixLength="1" CompletionInterval="1000"
                                        TargetControlID="txtClaimNo" ServiceMethod="GetClaimNo" ServicePath="LF_ClaimNo.asmx"
                                        UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                    </ajaxcontrol:AutoCompleteExtender>
                                    <ajaxcontrol:AutoCompleteExtender runat="server" ID="ajPatientName" EnableCaching="true"
                                        MinimumPrefixLength="1" CompletionInterval="500" DelimiterCharacters="" TargetControlID="txtPatient"
                                        ServiceMethod="GetPatientName" ServicePath="LF_PateintSearch.asmx" UseContextKey="true"
                                        ContextKey="SZ_COMPANY_ID">
                                    </ajaxcontrol:AutoCompleteExtender>
                                    <%-- <ajaxToolkit:AutoCompleteExtender runat = "server" id="AutoCompleteExtender1" EnableCaching="true" DelimiterCharacters=";, :" MinimumPrefixLength="2" CompletionInterval="1000" TargetControlID="txtPatient" ServiceMethod="GetPatientName" ServicePath="ClaimNumber.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID"  ></ajaxToolkit:AutoCompleteExtender>--%>
                                    <td height="20" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">&nbsp;&nbsp;<b class="txt3">Search Parameters</b>
                                    </td>
                                    <td width="0px" align="left" valign="middle" bgcolor="#B5DF82" class="txt2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" width="100%">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="TABLE1"
                                            onclick="return TABLE1_onclick()">
                                            <tr>
                                                <td align="center">Case Type
                                                </td>
                                                <td align="center">Claim No.
                                                </td>
                                                <td align="center">Patient
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" Width="90px" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GO" Procedure_Name="SP_GET_ABBRIVATION" OnextendDropDown_SelectedIndexChanged="extddlCaseType_extendDropDown_SelectedIndexChanged" />
                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtClaimNo" runat="server" Width="89px"></asp:TextBox>
                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtPatient" Width="90px" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">Insurance
                                                </td>
                                                <td align="center">SSN #
                                                </td>
                                                <td align="center">Date Of Birth
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <asp:TextBox ID="txtInsurance" runat="server" Width="90px" autocomplete="off" Style="position: relative; left: 0px; top: 0px;"></asp:TextBox>
                                                </td>
                                                <td style="position: relative" align="center" valign="top">
                                                    <asp:TextBox ID="txtSSNNo" runat="server" Width="90px"></asp:TextBox>
                                                    <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2">
                                                        <ProgressTemplate>

                                                            <div id="DivStatus21" style="position: absolute; left: 160px; top: 160px; z-index: 1; text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img21" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Loading...
                                                            </div>


                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtBirth" runat="server" Width="50%" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <%--  <asp:ImageButton
                                                        ID="imgBirth" runat="server" runat="server"  ImageUrl="~/Images/cal.gif"  />--%>
                                                    <asp:LinkButton ID="lnkBirth" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/LF/Images/cal.gif" />
                                                    </asp:LinkButton>
                                                    <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txtBirth" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxcontrol:MaskedEditValidator>
                                                    <ajaxcontrol:CalendarExtender ID="calext" runat="server" PopupButtonID="lnkBirth"
                                                        TargetControlID="txtBirth" PopupPosition="TopLeft">
                                                    </ajaxcontrol:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">Date Of Accident
                                                </td>
                                                <td align="center">Medical Facility
                                                </td>
                                                <td align="center">Batch Status
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:TextBox ID="txtAccidentDate" runat="server" Width="50%" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <%-- <asp:ImageButton
                                                        ID="imgbtnDateofAccident"  runat="server" ImageUrl="~/Images/cal.gif" />--%>
                                                    <asp:LinkButton ID="lnkbtnDateofAccident" runat="server">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/LF/Images/cal.gif" />
                                                    </asp:LinkButton>
                                                    <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender2"
                                                        ControlToValidate="txtAccidentDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxcontrol:MaskedEditValidator>
                                                    <ajaxcontrol:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="lnkbtnDateofAccident"
                                                        TargetControlID="txtAccidentDate" PopupPosition="TopLeft">
                                                    </ajaxcontrol:CalendarExtender>
                                                    <div style="position: absolute; z-index: 0; top: 195px; left: 350px; width: 4px;">
                                                        &nbsp;
                                                    </div>
                                                </td>
                                                <td align="center" valign="top">
                                                    <extddl:ExtendedDropDownList ID="extCompanyName" Width="90px" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GO" Procedure_Name="SP_GET_COMPANY_NAME" style="height: 4px" />
                                                </td>
                                                <td align="center" valign="top">
                                                    <asp:DropDownList ID="drdBatchStatus" runat="server" Width="80%">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="New Files" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Fully Downloaded" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">Bill No
                                                </td>
                                                
                                                <td align="center"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <asp:TextBox ID="txtBillNumber" Width="90px" runat="server"></asp:TextBox>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCaseType" runat="server" Width="90px" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtLawFirmID" runat="server" Width="90px" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtMedicalFacility" runat="server" Width="90px" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtBatchStatus" runat="server" Width="90px" Visible="false"></asp:TextBox>
                                                    <%--  <ajaxcontrol:CalendarExtender ID="calAccidentdate" runat="server" PopupButtonID="imgbtnDateofAccident"
                                                        TargetControlID="txtAccidentDate" PopupPosition="TopLeft">
                                                    </ajaxcontrol:CalendarExtender>--%>
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" Text="Search" ID="btnSearch" OnClick="btnSearch_Click"
                                                                Visible="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="width: 14%" align="right">
                        <div style="width: 100%; overflow: scroll; height: 200px">
                            <table style="width: 400px; border: 1px solid #B5DF82;" align="right" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgress13" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel3">
                                            <ProgressTemplate>


                                                <asp:Image ID="img212" Style="position: fixed; left: 80%; top: 30%; z-index: 1;" runat="server" ImageUrl="~/LF/Images/2simple-loading2.gif" AlternateText="Loading....."></asp:Image>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle" bgcolor="#B5DF82" style="width: 50%">
                                        <b class="txt3">Account Summary</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 400px; text-align: center; vertical-align: top; padding: 2px;">
                                        <table style="vertical-align: middle" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <gridpagination:XGridPaginationDropDown ID="con1" runat="server" Visible="false">
                                                        </gridpagination:XGridPaginationDropDown>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>

                                                <xgrid:XGridViewControl ID="grdLitigationCompanyWise" runat="server"
                                                    Width="400px" CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153"
                                                    ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true" EnableRowClick="false"
                                                    ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    AllowPaging="true" GridLines="None" XGridKey="ProviderWise" 
                                                    PageRowCount="10" PagerStyle-CssClass="pgr"
                                                    AllowSorting="true" OnRowCommand="grdLitigationCompanyWise_RowCommand" 
                                                    DataKeyNames="OFF_ID" 
                                                   >
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="SZ_OFFICE" HeaderText="Title">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="total" DataFormatString="{0:C}" HeaderText="Amount($)">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPlus" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS" Font-Size="15px"
                                                                    Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkMinus" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Font-Size="15px" Visible="false"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td colspan="100%" align="left">
                                                                        <div id="div<%# Eval("OFF_ID") %>" style="display: none;">
                                                                            <asp:GridView ID="grdVerification" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="" SortExpression="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblToralAmount" runat="server" Text="Amount($)" Font-Bold="true" Font-Size="x-Small"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="In litigation" SortExpression="">
                                                                                        <ItemTemplate>

                                                                                            <asp:Label ID="lbltrf" Text=' <%# DataBinder.Eval(Container,"DataItem.trf")%> ' runat="server" Font-Size="Small"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Sold" SortExpression="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblsol" Text=' <%# DataBinder.Eval(Container,"DataItem.sld")%> ' runat="server" Font-Size="Small"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loaned" SortExpression="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbllnd" Text=' <%# DataBinder.Eval(Container,"DataItem.lnd")%> ' runat="server" Font-Size="Small"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>



                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="width: 36%" valign="top" align="right">
                        <table width="100%" border="0">
                            <tr>
                                <td style="width: 100%;" valign="top">
                                    <div style="overflow: scroll; height: 230px">
                                        <asp:UpdatePanel ID="BatchView" runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%; vertical-align: top;" border="0" cellpadding="0" cellspacing="0">
                                                    <asp:DataList ID="DtlView" runat="server" BorderWidth="0px" BorderStyle="None" BorderColor="#DEBA84"
                                                        RepeatColumns="1" Width="100%" OnItemCommand="DtlView_ItemCommand" OnItemDataBound="DtlView_ItemDataBound">


                                                        <HeaderTemplate>
                                                            <tr style="background-color: #B5DF82">
                                                                <td class="td-widget-lf-desc-ch">Batch Date
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">UserName
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">Batch Name
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">Downloads
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center" class="lbl">
                                                                    <%# DataBinder.Eval(Container.DataItem, "dt_batch_date")%>
                                                                </td>
                                                                <td align="center">
                                                                    <%# DataBinder.Eval(Container.DataItem, "sz_user_name")%>
                                                                </td>
                                                                <td align="center">
                                                                    <%# DataBinder.Eval(Container.DataItem, "sz_batch_name")%>
                                                                </td>
                                                                <td align="center">
                                                                
                                                                 
                                                                 
                                                                   <asp:LinkButton ID="lnkOpen" Text="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.sz_batch_path")%>'
                                                                        runat="server" CommandName="lnkOpen"></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDownloads" Text="Re-Generate" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.sz_batch_name")%>'
                                                                        runat="server" CommandName="lnkDwn"></asp:LinkButton>

                                                                </td>

                                                                <td>
                                                                    <asp:LinkButton ID="lnkxls" Text="Download" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.sz_batch_name")%>'
                                                                        runat="server" CommandName="dnlxls"><img src="Images/Excel.jpg" style="border: none;" title="Fully Downloaded" Height = "20px", width="20px"/></asp:LinkButton>
                                                                </td>
                                                            </tr>

                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                        </FooterTemplate>





                                                    </asp:DataList>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="10%" valign="top">&nbsp;
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="10">
                            <ProgressTemplate>
                                <div style="vertical-align: bottom; text-align: left" id="DivStatus5" class="PageUpdateProgress"
                                    runat="Server">
                                    <asp:Image ID="img5" runat="server" Height="10px" Width="24px" ImageUrl="~/Images/rotation.gif"
                                        AlternateText="Downloading....."></asp:Image>
                                    Processing wait...
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="width: 100%; padding-bottom: 10px; height: 100%">
        &nbsp; &nbsp; &nbsp;
    </div>
    <table width="100%">
            <tr style="width:100%;text-align:right">
                <td style="width:50%;text-align:left">
                    </td>
                <td style="width:50%;text-align:right">
                    <asp:FileUpload  ID="ReportUpload" runat="server" />
        <asp:LinkButton ID="lnk_Download_status" OnClick="lnk_Download_status_Click" Text="Mark Downloaded" runat="server"/>
        <asp:Label ID="lbl_downloaded_status" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        
    <div id="psearch-list">
        <table width="100%" align="left" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                                    <tr>
                                        <td align="left" valign="middle" bgcolor="#B5DF82" class="txt2">&nbsp;&nbsp;<b class="txt3">Patients in my account</b>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="10"
                                                AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <div style="vertical-align: bottom; text-align: left" id="DivStatus2" class="PageUpdateProgress"
                                                        runat="Server">
                                                        <asp:Image ID="img2" runat="server" Height="25px" Width="24px" ImageUrl="~/Images/rotation.gif"
                                                            AlternateText="Downloading....."></asp:Image>
                                                        Processing. Please wait...
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td width="198" align="left" valign="middle" bgcolor="#B5DF82" class="txt2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" valign="top">
                                            <div>
                                                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="2">
                                                    <tr>
                                                        <td align="left" valign="top">
                                                            <div style="width: 100%; text-align: center;">
                                                                <div id="grdOuter" runat="server" style="width: 100%; text-align: center; left: 0px; top: 0px;">
                                                                    <div style="text-align: left;">
                                                                        <cMenu:ContextMenu CssClass="cmenu" ID="ContextMenu1" runat="server" BackColor="#99CCCC"
                                                                            ForeColor="Maroon" OnItemCommand="ContextMenu1_ItemCommand" RolloverColor="#009999"
                                                                            Width="150" />
                                                                    </div>
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <table style="vertical-align: middle" border="0" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 20%; text-align: left">Search:
                                                                                            <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                                                                                AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                                                                        </td>
                                                                                        <td style="width: 30%; text-align: right">
                                                                                            
                                                                                            
                                                                                            
                                                                                        </td>
                                                                                        <td style="width: 24%; text-align: right">
                                                                                            <dx:ASPxHyperLink 
                                                                                                Text="Export to GBB Header Format"
                                                                                                runat="server" 
                                                                                                ID="xExcel">
                                                                                                <ClientSideEvents Click="ExportToGBBHeader" />
                                                                                            </dx:ASPxHyperLink>
                                                                                            <asp:Label ID="lbl_export" runat="server" Text=""></asp:Label>
                                                                                            <dx:ASPxCallback 
                                                                                                ID="ASPxCallback1" 
                                                                                                runat="server" 
                                                                                                ClientInstanceName="Callback1"
                                                                                                OnCallback="ASPxCallback1_Callback">
                                                                                                <ClientSideEvents CallbackComplete="OnCallbackComplete" />
                                                                                            </dx:ASPxCallback>
                                                                                            <dx:ASPxLoadingPanel Text="Exporting..." runat="server" ID="expLoadingPanel" ClientInstanceName="expLoadingPanel"></dx:ASPxLoadingPanel>
                                                                                        </td>

                                                                                        <td style="width: 26%;" align="right">Record Count:
                                                                                            <%= this.grid.RecordCount %>
                                                                                            |
                                                                                            Page Count:
                                                                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                            </gridpagination:XGridPaginationDropDown>
                                                                                            <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                                Text="Export TO Excel">
                                                                                    <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <xgrid:XGridViewControl ID="grid" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                                                AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="50" XGridKey="PatientSearch"
                                                                                AllowPaging="true" ExportToExcelColumnNames="BillNo,Case #,Patient Name, Date Of Accident,Case Type,Insurance,Date Open,Medical  Facility"
                                                                                ShowExcelTableBorder="true" ExportToExcelFields="SZ_BILL_NUMBER,SZ_CASE_NO,Patient Name,DT_DATE_OF_ACCIDENT,Case Type,Insurance,OPEN_DATE,SZ_COMPANY_NAME"
                                                                                AlternatingRowStyle-BackColor="#EEEEEE" HeaderStyle-CssClass="GridViewHeader"
                                                                                ContextMenuID="ContextMenu1" EnableRowClick="false" MouseOverColor="0, 153, 153"
                                                                                DataKeyNames="SZ_CASE_ID,SZ_COMPANY_ID,SZ_CASE_NO,Patient Name,SZ_SPECIALITY_ID" OnRowCommand="grid_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        HeaderText="Case Id" DataField="SZ_CASE_ID" Visible="false" />
                                                                                    <xlink:XGridHyperlinkField SortExpression="CAST(SZ_CASE_NO as int)" HeaderText="Case #" DataNavigateUrlFields="SZ_CASE_ID,SZ_COMPANY_ID,Patient Name,SZ_CASE_NO"
                                                                                        DataNavigateUrlFormatString="WorkAreaWidget.aspx?csid={0}&cmp={1}&pname={2}&caseno={3}" DataTextField="SZ_CASE_NO">
                                                                                    </xlink:XGridHyperlinkField>

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        SortExpression="TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER"
                                                                                        HeaderText="Bill No" DataField="SZ_BILL_NUMBER" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        SortExpression="MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP"
                                                                                        HeaderText="Speciality" DataField="SZ_PROCEDURE_GROUP" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        HeaderText="SZ_SPECIALITY_ID" DataField="SZ_SPECIALITY_ID" Visible="false" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME  + ' '  + MST_PATIENT.SZ_PATIENT_LAST_NAME"
                                                                                        HeaderText="Patient" DataField="Patient Name" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        SortExpression="MST_CASE_TYPE.SZ_CASE_TYPE_ABBRIVATION" HeaderText="Case Type"
                                                                                        DataField="Case Type" Visible="true" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        HeaderText="Insurance"
                                                                                        DataField="Insurance" Visible="true" SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                        SortExpression="MST_CASE_MASTER.DT_DATE_OF_ACCIDENT" HeaderText="Accident Date"
                                                                                        DataField="Date Of Accident" DataFormatString="{0:MM/dd/yyyy}" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                        SortExpression="MST_CASE_MASTER.DT_CREATED_DATE" HeaderText="Open Date" DataField="OPEN_DATE"
                                                                                        DataFormatString="{0:MM/dd/yyyy}" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        SortExpression="MST_BILLING_COMPANY.SZ_COMPANY_NAME " HeaderText="Medical  Facility"
                                                                                        DataField="SZ_COMPANY_NAME" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        Visible="false" HeaderText="Case ID" DataField="SZ_CASE_ID" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                        Visible="false" HeaderText="Company ID" DataField="SZ_COMPANY_ID" />

                                                                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                        HeaderText="Status"
                                                                                        DataField="SZ_BILL_STATUS_NAME" Visible="true" SortExpression="mst_bill_status.SZ_BILL_STATUS_NAME" />

                                                                                    <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="center">
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="ChkPatientList" runat="server"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>


                                                                                    <asp:TemplateField HeaderText="Docs" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <img alt="" onclick="javascript:OpenDocumentManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');" src="Images/grid-doc-mng.gif" style="border: none; cursor: pointer;" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                     
                                                                                </Columns>
                                                                            </xgrid:XGridViewControl>
                                                                            <table width="100%" align="left" border="0" cellpadding="0" cellspacing="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 100%; text-align: center; vertical-align: middle; position: relative; height: 24px;">
                                                                                            <asp:UpdatePanel ID="asd">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Button runat="server" Text="Download" ID="btnDownload" Visible="true" OnClick="btnDownload_Click" />
                                                                                                    <asp:Button runat="server" Text="Download All" ID="btnDownloadAll" Visible="false" OnClick="btnDownloadALL_Click" />
                                                                                                    <asp:Button runat="server" Text="Excel Only" ID="btnGenerate" Visible="true" OnClick="btnGenerate_Click" />
                                                                                                    <asp:HiddenField ID="hDnl" runat="server" />
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>

                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <asp:TextBox ID="txtCompanyId" runat="server"></asp:TextBox><asp:HiddenField ID="hBatch"
                                runat="server" />
                            <asp:HiddenField ID="hStatus" runat="server" />
                            <asp:HiddenField ID="hCount" runat="server" />
                            <asp:HiddenField ID="hIsCount" runat="server" />
                            <asp:HiddenField ID="hDownlaodNumber" runat="server" />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:CheckBox ID="chkJmpCaseDetails" runat="server" Text="Jump to Case" Visible="false" />
    <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtBirth" PromptCharacter="_" AutoComplete="true">
    </ajaxcontrol:MaskedEditExtender>
    <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtAccidentDate" PromptCharacter="_" AutoComplete="true">
    </ajaxcontrol:MaskedEditExtender>
    <%--<asp:Button runat="server" Text="Download All" ID="Button1" Visible="true" OnClick="btnDownloadALL_Click"  />--%>
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
            <ajaxcontrol:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel3" TargetControlID="lbn_job_det" BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="divHeader">
            </ajaxcontrol:ModalPopupExtender>
            <asp:Panel Style="display: none" ID="Panel3" runat="server" Width="70%" CssClass="modalPopup">
                <div id="divHeader" style="left: 0%; vertical-align: text-bottom; width: 100%; position: absolute; top: 0px; height: 15px; background-color: lightgrey; text-align: left"
                    colspan="" rowspan="">
                </div>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <dx:ASPxPopupControl ID="IFrame_DownloadFiles" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="IFrame_DownloadFiles" HeaderText="Data Export" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="600px" ToolTip="Download File(s)"
        PopupHorizontalOffset="0" PopupVerticalOffset="0"  AutoUpdatePosition="true"
        ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="200px">
        <contentstyle>
            <Paddings PaddingBottom="5px" />
        </contentstyle>
    </dx:ASPxPopupControl>
</asp:Content>
