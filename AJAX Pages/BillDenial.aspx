<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillDenial.aspx.cs" Inherits="AJAX_Pages_BillDenial" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link href="Css/buttons.css" rel="stylesheet" type="text/css" />
    <link href="Css/buttons-core.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        div.scrollable {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            overflow: auto;
        }

        .lbltext {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
        }

        .lblcontrol {
            background-color: #f6f6f6;
            padding-bottom: 2px;
            padding-left: 5px;
        }
    </style>
    <script type="text/javascript">


        function RemoveDenial() {

            if (document.getElementById("lbSelectedDenial").length <= 0) {
                alert("No Denial reason available to remove.")
                document.getElementById("lbSelectedDenial").focus();
                return false;
            }

            else if (document.getElementById("lbSelectedDenial").selectedIndex < 0) {
                alert("Please Select Denail reason to Remove.");
                document.getElementById("lbSelectedDenial").focus();
                return false;
            }
            else {

                var e = document.getElementById("lbSelectedDenial");
                var user = e.options[e.selectedIndex].value;
                //alert(user);
                document.getElementById("hfremovedenialreason").value = document.getElementById("hfremovedenialreason").value + e.options[e.selectedIndex].value + ",";
                document.getElementById("lbSelectedDenial").options[document.getElementById("lbSelectedDenial").selectedIndex] = null;

                var list = document.getElementById("lbSelectedDenial");
                var items = list.getElementsByTagName("option");
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function checkdenial() {
            if (document.getElementById("lbSelectedDenial").length <= 0) {
                alert("Add denial reason.")
                return false;
            }
            else {
                return true;
            }
        }

        function OpenBillDenialScan() {
            var caseid = document.getElementById('hdnCaseId').value;
            var specialityid = document.getElementById('hdnSpecialty').value;
            var typeId = document.getElementById('hdnStatusCode').value;
            var billNumber = document.getElementById('hdnBillNumber').value;
            scanVerificationDenial(caseid, specialityid, billNumber, typeId, '1');
        }

        function OpenSingleBillDenialScan(verificationId, billNumber, spId) {
            debugger;
            var caseid = document.getElementById('hdnCaseId').value;
            var specialityid = spId;
            billNumber = billNumber;
            scanVerificationDenial(caseid, specialityid, billNumber, verificationId, '1');
        }

        function SelectAll(ival) {
            var f = document.getElementById('grdVerificationReq');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function Clear() {
            document.getElementById('txtSaveDate').value = '';
            document.getElementById('txtSaveDescription').value = '';
            document.getElementById('extddenial').value = 'NA';
            document.getElementById('lbSelectedDenial').options.length = 0;
        }

        function confirm_bill_delete() {
            var f = document.getElementById("grdVerificationReq");
            var bfFlag = false;
            var index = document.getElementById("hfindex").value;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {

                            if (i == index) {
                                if (confirm("Bill status has been changed POM received? do you want to allow changed bill status..")) {
                                    document.getElementById("hfconfirm").value = 'yes';
                                }
                                else {
                                    document.getElementById("hfconfirm").value = 'no';
                                }
                            }
                            else {
                                document.getElementById("hfconfirm").value = 'delete';
                            }
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
                if (confirm("This will permanently delete the posted denial. Do you want to continue [Ok/Cancel]?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <script type="text/javascript">
            function closeWindow() {
                window.parent.location.reload(true);
            }

            function AddDenial() {
                var e = document.getElementById("extddenial");
                var user = e.options[e.selectedIndex].text;
                if (user == "---Select---") {
                    alert("Please select Denial Reason!");
                    return false;
                }
                var vlength = document.getElementById("<%=lbSelectedDenial.ClientID%>").length;
                var status = "0";

                var i;
                if (vlength != 0) {
                    for (i = 0; i < vlength; i++) {
                        if (document.getElementById("extddenial").value == document.getElementById("<%=lbSelectedDenial.ClientID %>").options[i].value) {
                            alert("Denial reason already exists");
                            status = "1";
                            return false;
                        }
                    }
                }
                if (status != "1") {
                    document.getElementById("<%=hfdenialReason.ClientID %>").value = document.getElementById("<%=hfdenialReason.ClientID %>").value + e.options[e.selectedIndex].value + ",";
                    var optn = document.createElement("OPTION");
                    optn.text = e.options[e.selectedIndex].text;
                    optn.value = e.options[e.selectedIndex].value;
                    document.getElementById("<%=lbSelectedDenial.ClientID %>").options.add(optn);
                    return false;
                }
            }
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 98%;">
                    <tr>
                        <td colspan="4" style="text-align: right; width: 98%">
                            <a style="font-family: Arial; font-size: 12px; color: blue; text-decoration: none; cursor: pointer;" href="#" onclick="javascript:closeWindow();">[Close Window]</a>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; background-color: White;" border="0">
                    <tr>
                        <td style="vertical-align: top; width: 100%;">

                            <table style="width: 100%; border: 0px solid; padding-left: 0px;"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="text-align: center;">
                                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                        <asp:Label ID="lblMessage" Font-Bold="true" Font-Size="Small" ForeColor="Green" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblErrorMessage" Font-Bold="true" Font-Size="Small" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div style="border: 0px solid; width: 100%;">
                                <table border="0" style="margin: 0 auto; width: 70%; padding-top: 10px; padding-bottom: 10px" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="lbltext">&nbsp;
                                        </td>
                                        <td class="lblcontrol">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">Date
                                        </td>
                                        <td class="lblcontrol">
                                            <asp:Label ID="lblSaveDate" runat="server" Text="" CssClass="lbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">Denial Date
                                        </td>
                                        <td class="lblcontrol">
                                            <asp:TextBox ID="txtSaveDate" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')">
                                            </asp:TextBox><asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                            <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red" CssClass="lbl"></asp:Label>
                                            <ajaxcontrol:CalendarExtender ID="calExtChequeDate" runat="server" TargetControlID="txtSaveDate"
                                                PopupButtonID="imgSavebtnToDate" />
                                            <ajaxcontrol:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                MaskType="Date" TargetControlID="txtSaveDate" PromptCharacter="_" AutoComplete="true">
                                            </ajaxcontrol:MaskedEditExtender>
                                            <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtSaveDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxcontrol:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">Description
                                        </td>
                                        <td class="lblcontrol">
                                            <asp:TextBox
                                                ID="txtSaveDescription"
                                                runat="server"
                                                Width="95%"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">Reason
                                        </td>
                                        <td class="lblcontrol">
                                            <extddl:ExtendedDropDownList ID="extddenial"
                                                Width="83%"
                                                runat="server"
                                                Connection_Key="Connection_String"
                                                Procedure_Name="SP_MST_DENIAL"
                                                Flag_Key_Value="DENIAL_LIST"
                                                Selected_Text="--- Select ---"
                                                CssClass="cinput" Visible="true" />
                                            <input type="button"
                                                value="+"
                                                height="5px"
                                                width="5px"
                                                id="btnAddDenial"
                                                runat="server"
                                                onclick="AddDenial();" />
                                            <input type="button" value="~" height="5px" width="5px" id="btnRemoveDenial" runat="server"
                                                onclick=" RemoveDenial();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">&nbsp;</td>
                                        <td class="lblcontrol">
                                            <asp:ListBox ID="lbSelectedDenial" Hight="60%"
                                                Width="95%" runat="server"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext"></td>
                                        <td class="lblcontrol"></td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext"></td>
                                        <td style="text-align:center";>
                                            <a href="javascript:void(0);" runat="server" id="anchorScan" visible="false"
                                                onclick="OpenBillDenialScan()">Scan / Upload</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">&nbsp;</td>
                                        <td class="lblcontrol">
                                            <label id="lblScan" runat="server" style="color:red; width:100%" visible="false"></label>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table style="width: 98%; border: 0px solid; margin-top: 20px; padding-left: 0px;"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="center">
                                        <asp:Button
                                            ID="btnSavesent"
                                            CssClass="pure-button pure-button-primary"
                                            Width="120"
                                            runat="server"
                                            Text="Save"
                                            OnClick="btnSaveDesc_Click" />
                                        <input type="button"
                                            id="btnClear"
                                            class="pure-button"
                                            style="width: 120px"
                                            value="Clear"
                                            onclick="Clear();" />
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField runat="server" ID="hdnCaseId" />
                            <asp:HiddenField runat="server" ID="hdnBillNumber" />
                            <asp:HiddenField runat="server" ID="hdnSpecialty" />
                            <asp:HiddenField runat="server" ID="hdnStatusCode" />
                            <asp:HiddenField runat="server" ID="hdnVerificationId" />
                            <asp:HiddenField runat="server" ID="hfdenialReason" />
                            <asp:HiddenField runat="server" ID="hfremovedenialreason" />
                            <asp:HiddenField runat="server" ID="hfconfirm" />
                            <asp:HiddenField runat="server" ID="hfindex" Value="no" />
                            <asp:HiddenField runat="server" ID="hfverificationId" />
                            <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCompanyID" runat="server" CssClass="btn" Visible="False" Width="10px"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                <table style="width: 100%; border: 0px solid; padding-left: 1px; padding-right: 15px;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div style="overflow: scroll; width: 99%; height: 150px">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table>
                                                <asp:DataGrid ID="grdVerificationReq" runat="server" AutoGenerateColumns="false"
                                                    Width="100%" CssClass="GridTable">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <HeaderStyle CssClass="GridHeader1" />
                                                    <Columns>
                                                        <%-- 0--%>
                                                        <asp:BoundColumn DataField="sz_bill_number" HeaderText="Bill#"></asp:BoundColumn>
                                                        <%-- 1--%>
                                                        <asp:BoundColumn DataField="TYPE" HeaderText="Type"></asp:BoundColumn>
                                                        <%-- 2--%>
                                                        <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                                        <%-- 3--%>
                                                        <asp:BoundColumn DataField="DATE" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date"></asp:BoundColumn>
                                                        <%-- 4--%>
                                                        <asp:BoundColumn DataField="USER" HeaderText="User"></asp:BoundColumn>
                                                        <%-- 5--%>
                                                        <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID" Visible="false"></asp:BoundColumn>
                                                        <%-- 6--%>
                                                        <asp:BoundColumn DataField="SZ_DENIAL_REASONS" HeaderText="Reason"></asp:BoundColumn>
                                                        <%--7--%>
                                                        <asp:TemplateColumn HeaderText="Documents" ItemStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <a href="javascript:void(0);"
                                                                    onclick="OpenSingleBillDenialScan('<%# Eval("i_transaction_id") %>','<%# Eval("sz_bill_number") %>','<%# Eval("sz_specialty_id") %>')">Scan / Upload</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%-- 8--%>
                                                        <asp:ButtonColumn CommandName="Delete" Text="Delete" DataTextField="" Visible="false">
                                                            <ItemStyle CssClass="grid-item-left" />
                                                        </asp:ButtonColumn>
                                                        <%-- 9--%>
                                                        <asp:BoundColumn DataField="I_TYPE_ID" Visible="false"></asp:BoundColumn>
                                                        <%-- 10--%>
                                                        <asp:BoundColumn DataField="sz_bill_status" Visible="false"></asp:BoundColumn>
                                                        <%--11--%>
                                                        <asp:BoundColumn DataField="DT_CREATED_DATE" Visible="false" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                        <%--12--%>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                    ToolTip="Select All" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDelete" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--13--%>
                                                    <asp:BoundColumn DataField="sz_specialty_id" Visible="false"></asp:BoundColumn>
                                                        <%--14--%>
                                                    <asp:BoundColumn DataField="i_transaction_id" Visible="false"></asp:BoundColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="width: 100%; border: 0px solid; text-align: center">
            <table style="margin: 0 auto; width: 98%" border="0">
                <tr>
                    <td style="width: 100%">
                        <asp:Button
                            ID="btnDelete"
                            CssClass="pure-button pure-button-primary"
                            runat="server"
                            Width="120"
                            Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <asp:TextBox ID="txtSpecialtyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
