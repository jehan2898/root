<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EORReceived.aspx.cs" Inherits="AJAX_Pages_EORReceived" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
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
         function AddDenial() {
             var e = $("#ddlReasons");
             var user = e.val();
             if (user == "NA") {
                 alert("Please select Reason!");
                 return false;
             }
             var vlength = document.getElementById("lbSelectedDenial").length;
             var status = "0";
         
             var i;
             if (vlength != 0) {
                 for (i = 0; i < vlength; i++) {
                     if (e.val() == document.getElementById("lbSelectedDenial").options[i].value) {
                         alert("Reason already exists");
                         status = "1";
                         return false;
                     }
                 }
             }
             if (status != "1") {
                 document.getElementById("hfEORReason").value = document.getElementById("hfEORReason").value + e.val() + ",";
                 var optn = document.createElement("OPTION");
                 optn.text = $("#ddlReasons option:selected").text();
                 optn.value = e.val();
                 document.getElementById("lbSelectedDenial").options.add(optn);
                 return false;
             }
         }
         
         function RemoveDenial() {
         
             if (document.getElementById("lbSelectedDenial").length <= 0) {
                 alert("No EOR Reason available to remove.")
                 document.getElementById("lbSelectedDenial").focus();
                 return false;
             }
         
             else if (document.getElementById("lbSelectedDenial").selectedIndex < 0) {
                 alert("Please Select EOR Reason to Remove.");
                 document.getElementById("lbSelectedDenial").focus();
                 return false;
             }
             else {
                 document.getElementById("hfEORReason").value = "";
                 var e = document.getElementById("lbSelectedDenial");
                 var user = e.options[e.selectedIndex].value;
                 //alert(user);
                 document.getElementById("hfremovedenialreason").value = document.getElementById("hfremovedenialreason").value + e.options[e.selectedIndex].value + ",";
                 document.getElementById("lbSelectedDenial").options[document.getElementById("lbSelectedDenial").selectedIndex] = null;
         
                 var list = document.getElementById("lbSelectedDenial");
                 var items = list.getElementsByTagName("option");
                 if (items.length > 0) {
                     for (i = 0; i < items.length; i++) {
                         document.getElementById("hfEORReason").value = document.getElementById("hfEORReason").value + items[i].value + ",";
                     }
                 }
                 return false;
             }
         }

        function closeWindow() {
            window.parent.location.reload(true);
        }

        function OpenBillVerificationScan() {
            debugger;
            var caseid = document.getElementById('hdnCaseId').value;
            var specialityid = document.getElementById('hdnSpecialty').value;
            var typeId = document.getElementById('hdnStatusCode').value;
            var billNumber = document.getElementById('hdnBillNumber').value;
            scanBillEOR(caseid, specialityid, typeId, billNumber, '1');
        }

        function OpenSingleBillVerificationScan(typeId, billNumber,spId) {
            debugger;
            var caseid = document.getElementById('hdnCaseId').value;
            var specialityid = spId;
            billNumber = billNumber;
            scanBillEOR(caseid, specialityid, typeId, billNumber, '1');
        }

        function Clear() {
            document.getElementById('txtSaveDate').value = '';
            document.getElementById('txtSaveDescription').value = '';
        }

        function CheckValid()
        {
            var verificationDate = document.getElementById('txtSaveDate').value;
            if(verificationDate=="")
            {
                alert('Please enter EOR Date');
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <script type="text/javascript">
            function checkedDate() {
                var year1 = "";
                year1 = document.getElementById("<%=txtSaveDate.ClientID %>").value;
            if ((year1.charAt(0) != '' && year1.charAt(1) != '' && year1.charAt(2) == '/' && year1.charAt(3) != '' && year1.charAt(4) != '' && year1.charAt(5) == '/' && year1.charAt(6) != '' && year1.charAt(7) != '' && year1.charAt(8) != '' && year1.charAt(9) != '' && year1.charAt(6) != '0')) {
                return true;
            }
            else {
                alert("Please select EOR received date.");
                return false;
            }
        }
        </script>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                                <asp:Label ID="lblMessage" Font-Bold="true" Font-Size="Small" ForeColor="Green" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblErrorMessage" Font-Bold="true" Font-Size="Small" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                                        <td class="lbltext">EOR Date
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
                                            <asp:DropDownList runat="server" ID="ddlReasons"></asp:DropDownList>
                                            <asp:Button ID="btnAddVerification" runat="server" Font-Bold="true" Font-Size="Medium" Text="+" CssClass="btn btn-primary" OnClientClick="return AddDenial();" />
                                              <asp:Button ID="btnRemoveVerification" runat="server" Font-Bold="true" Font-Size="Medium" Text="--" CssClass="btn btn-danger" OnClientClick="return RemoveDenial();" />
                                        </td>
                                    </tr>
                                        <tr>
                                           <td class="lbltext">&nbsp;</td>
                                           <td class="lblcontrol">
                                              <asp:ListBox ID="lbSelectedDenial" Hight="60%" Width="95%" runat="server" CssClass="form-control"> </asp:ListBox>
                                           </td>
                                        </tr>
                                    <tr>
                                        <td class="lbltext"></td>
                                        <td style="text-align:center";>
                                            <a href="javascript:void(0);" runat="server" id="anchorScan" visible="false"
                                                onclick="OpenBillVerificationScan()">Scan / Upload</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">&nbsp;</td>
                                        <td class="lblcontrol">
                                            <label id="lblScan" runat="server" style="color:red; width:100%" visible="false"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext">&nbsp;
                                        </td>
                                        <td class="lblcontrol">&nbsp;
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
                            <asp:HiddenField runat="server" ID="hfEORReason" />
                            <asp:HiddenField runat="server" ID="hfremovedenialreason" />
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
                            <div style="overflow: scroll; width: 99%; height: 260px">
                                <table>
                                    <asp:DataGrid ID="grdEORReceived" runat="server" AutoGenerateColumns="false"
                                        Width="99%" CssClass="GridTable" OnItemCommand="grdEORReceived_ItemCommand" DataKeyField="sz_bill_number">
                                        <ItemStyle CssClass="GridRow" />
                                        <HeaderStyle CssClass="GridHeader1" />
                                        <Columns>
                                            <%-- 0--%>
                                            <asp:BoundColumn DataField="sz_bill_number" HeaderText="Bill#" ItemStyle-Width="7%"></asp:BoundColumn>
                                            <%-- 1--%>
                                            <asp:BoundColumn DataField="TYPE" HeaderText="Type" ItemStyle-Width="15%"></asp:BoundColumn>
                                            <%-- 2--%>
                                            <asp:BoundColumn DataField="NOTES" HeaderText="Notes" ItemStyle-Width="20%"></asp:BoundColumn>
                                            <%-- 3--%>
                                            <asp:BoundColumn DataField="DATE" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date" ItemStyle-Width="10%"></asp:BoundColumn>
                                            <%-- 4--%>
                                            <asp:BoundColumn DataField="USER" HeaderText="User" ItemStyle-Width="10%"></asp:BoundColumn>
                                            <%-- 5--%>
                                            <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="sz_bill_status" Visible="true" HeaderText="Bill Status" ItemStyle-Width="20%"></asp:BoundColumn>
                                            <%-- 9--%>
                                            <asp:BoundColumn DataField="I_TYPE_ID" Visible="false" ItemStyle-Width="10%"></asp:BoundColumn>
                                            <%--7--%>
                                            <asp:TemplateColumn HeaderText="Documents" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <a href="javascript:void(0);"
                                                        onclick="OpenSingleBillVerificationScan('<%# Eval("i_transaction_id") %>','<%# Eval("sz_bill_number") %>','<%# Eval("sz_specialty_id") %>')">Scan / Upload</a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <%-- 8--%>
                                            <asp:ButtonColumn CommandName="Delete" Text="Delete" DataTextField="" Visible="true">
                                                <ItemStyle CssClass="grid-item-left" />
                                            </asp:ButtonColumn>
                                            <%--11--%>
                                            <asp:BoundColumn DataField="DT_CREATED_DATE" Visible="false"></asp:BoundColumn>
                                            <%--12--%>
                                            <asp:BoundColumn DataField="sz_specialty_id" Visible="false"></asp:BoundColumn>
                                            <%--13--%>
                                            <asp:BoundColumn DataField="i_transaction_id" Visible="false"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:TextBox ID="txtSpecialtyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
