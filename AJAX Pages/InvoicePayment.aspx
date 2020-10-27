<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoicePayment.aspx.cs" Inherits="AJAX_Pages_InvoicePayment" %>

<!DOCTYPE html>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link href="Css/buttons.css" rel="stylesheet" type="text/css" />
    <link href="Css/buttons-core.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
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

        .style5 {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
            width: 104px;
        }

        .style6 {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
            width: 105px;
        }

        .style7 {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
            width: 115px;
        }

        .auto-style3 {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
            width: 71px;
        }

        .auto-style4 {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
            width: 113px;
        }

        .auto-style5 {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
            width: 430px;
        }

        .auto-style6 {
            font-family: Arial;
            font-size: 13px;
            font-weight: bold;
            text-align: right;
            background-color: #f1f1f1;
            padding-bottom: 2px;
            padding-right: 5px;
            vertical-align: top;
            width: 97px;
        }
    </style>
    <script type="text/javascript">

        function Clear() {
            //alert("call");
            document.getElementById('txtCheckAmount').value = '';
            document.getElementById('txtCheckNo').value = '';
            document.getElementById('txtDate').value = '';
            document.getElementById('txtNotes').value = '';
            document.getElementById('txtPaymentID').value = '';
            document.getElementById('btnSavesent').disabled = false;
            document.getElementById('btnUpdate').disabled = true;



        }
        function ooValidate() {
            if (document.getElementById('txtDate').value == '') {
                alert('Please select cheque date');
                return false;
            }
            if (document.getElementById('txtCheckNo').value == '') {
                alert('Please select cheque number');
                return false;
            }
            if (document.getElementById('txtCheckAmount').value == '') {
                alert('Please enter amount');
                return false;
            }
        }

        function SelectAll(ival) {
            var f = document.getElementById("grdMakeInvoicePayment");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }


            }
        }


        function confirm_bill_delete() {

            var f = document.getElementById("grdMakeInvoicePayment");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkRemove') != -1) {
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



            if (confirm("Are you sure want to Delete?") == true) {

                return true;
            }
            else {


                return false;
            }
        }


        function showViewFrame(paymentid,invoiceid) {

            // alert(objCaseID + ' ' + objCompanyID);
            var obj3 = "";
            document.getElementById('divView').style.zIndex = 1;
            document.getElementById('divView').style.position = 'absolute';
            document.getElementById('divView').style.left = '100px';
            document.getElementById('divView').style.top = '100px';
            document.getElementById('divView').style.visibility = 'visible';
            document.getElementById('frmView').src = "InvoicePaymentDoc.aspx?paymentid=" + paymentid + "&invoiceid=" + invoiceid;
            return false;
        }
        function CloseViewFramePopup() {
            //   alert("");
            //document.getElementById('divView').style.height='0px';
            document.getElementById('divView').style.visibility = 'hidden';
            document.getElementById('divView').style.top = '-10000px';
            document.getElementById('divView').style.left = '-10000px';
        }

    </script>
    
    <script type="text/javascript">
        function paymentscan(nodetype, billno, paymentid) {
            debugger;
            alert(billno);
            scanPayment_For_Invoice(billno, paymentid, '1');
            
        }
    </script>
    
    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="text-align: right; width: 98%">
                            <a style="font-family: Arial; font-size: 12px; color: blue; text-decoration: none; cursor: pointer;"
                                href="#" onclick="javascript:closeWindow();"></a>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; background-color: White;" border="0">
                    <tr>
                        <td style="vertical-align: top; width: 100%;">
                            <table style="width: 100%; border: 0px solid; padding-left: 0px;" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                                <asp:Label ID="lblMessage" Font-Bold="true" Font-Size="Small" ForeColor="Green" runat="server"
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblErrorMessage" Font-Bold="true" Font-Size="Small" ForeColor="Red"
                                                    runat="server" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <div style="border-style: solid; border-color: inherit; border-width: 0px; width: 100%; height: 171px">
                                <table border="0" style="margin: 0 auto; width: 99%; height: 100%;"
                                    cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="auto-style6">&nbsp;
                                        </td>
                                        <td class="style7">&nbsp;
                                        </td>
                                        <td class="style5">&nbsp;
                                        </td>
                                        <td class="auto-style4">&nbsp;
                                        </td>
                                        <td class="auto-style3">&nbsp;
                                        </td>
                                        <td class="lbltext">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6" style="text-align: left; padding-left: 8px">Employer
                                        </td>
                                        <td class="style7" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblemp" runat="server"></asp:Label>
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Invoice#
                                        </td>
                                        <td class="auto-style4" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblInvoiceNumber" runat="server"></asp:Label>
                                        </td>

                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Amount
                                        </td>
                                        <td class="auto-style4" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td class="auto-style6" style="text-align: left; padding-left: 8px">Balance
                                        </td>
                                        <td class="auto-style4" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                        </td>

                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Check #
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 5px">
                                            <asp:TextBox runat="server" ID="txtCheckNo"></asp:TextBox>
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Check Amount
                                        </td>
                                        <td class="style7" style="text-align: left; padding-left: 5px">
                                            <asp:TextBox runat="server" ID="txtCheckAmount"></asp:TextBox>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td class="auto-style6" style="text-align: left; padding-left: 8px">Check Date
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 5px; width: 100px">
                                            <asp:TextBox ID="txtDate" runat="server" Width="80%" CssClass="search-input" Style="text-align: left"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif"
                                                valign="bottom"></asp:ImageButton>

                                            <ajaxcontrol:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtDate"
                                                PopupButtonID="imgbtnDateofBirth" />
                                            <ajaxcontrol:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtToVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="false">
                                            </ajaxcontrol:MaskedEditValidator>
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Notes
                                        </td>
                                        <td class="lbltext">
                                            <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="lbltext">
                                            <asp:CheckBox ID="chkwirteoff" runat="server" Text="WriteOff" />
                                        </td>
                                        <td class="lbltext"></td>

                                    </tr>
                                </table>
                            </div>
                            <table style="width: 98%; border: 0px solid; margin-top: 20px; padding-left: 0px;"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSavesent" CssClass="pure-button pure-button-primary" Width="120"
                                            runat="server" Text="Save" OnClick="btnSavesent_Click" />
                                        <asp:Button ID="btnUpdate" CssClass="pure-button pure-button-primary" Width="120"
                                             runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                        <input style="width: 80px" id="btnClear1" type="button" value="Clear" onclick="Clear();"
                                            runat="server" visible="true" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%; border: 0px solid; padding-left: 1px; padding-right: 15px;"
                                cellpadding="0" cellspacing="0">
                                <tr>

                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>

                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btndelete" CssClass="pure-button pure-button-primary" Width="120"
                                            runat="server" Text="Delete" OnClick="btndelete_Click" />


                                    </td>

                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <div style="overflow: scroll; width: 99%; height: 400px">
                                            <table width="100%">
                                                <asp:DataGrid ID="grdMakeInvoicePayment" runat="server" AutoGenerateColumns="false"
                                                    Width="99%" CssClass="GridTable" DataKeyField="I_ID" OnItemCommand="grdMakeInvoicePayment_ItemCommand">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <HeaderStyle CssClass="GridHeader1" />
                                                    <Columns>
                                                        <%-- 0--%>
                                                        <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                        <%-- 1--%>
                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Employer"></asp:BoundColumn>
                                                        <%-- 2--%>
                                                        <asp:BoundColumn DataField="INVOICE_NO" HeaderText="Invoice#"></asp:BoundColumn>

                                                        <%-- 3--%>
                                                        <asp:BoundColumn DataField="MN_CHECK_AMOUNT" HeaderText="Cheque Amount" DataFormatString="{0:C}"></asp:BoundColumn>
                                                        <%-- 4--%>
                                                        <asp:BoundColumn DataField="DT_CHECK_DATE" HeaderText="Date"></asp:BoundColumn>
                                                        <%-- 5--%>
                                                        <asp:BoundColumn DataField="SZ_CHECK_NO" HeaderText="cheque#"></asp:BoundColumn>

                                                        <%-- 6--%>
                                                        <asp:TemplateColumn HeaderText="Check">
                                                            <ItemTemplate>
                                                                <a id="caseDetailScan" href="#" runat="server" onclick='<%# "paymentscan(\""  + "NFPAY"  +"\""+ ",\""  + Eval("INVOICE_NO")  +"\""+ ",\""  + Eval("I_ID")  +"\""+ ");"%>'
                                                                    title="Scan/Upload">Scan/Upload</a>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                         <%-- 7--%>
                                                        <asp:TemplateColumn>
                                                                                        <ItemTemplate>
                                                                                 <a id="lnkView"  runat="server"  href="#" onclick='<%# "showViewFrame(\""+  Eval("I_ID") +"\""+ ",\""+ Eval("INVOICE_NO")  +"\");" %>' >View</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                        <%-- 8--%>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Select All" onclick="javascript:SelectAll(this.checked);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkRemove" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%-- 9--%>
                                                        <asp:BoundColumn DataField="I_ID" HeaderText="id" Visible="false"></asp:BoundColumn>
                                                        <%-- 10--%>
                                                        <asp:BoundColumn DataField="SZ_NOTES" HeaderText="notes" Visible="false"></asp:BoundColumn>
                                                        <%-- 11--%>
                                                        <asp:BoundColumn DataField="SZ_EMPOLYER_ID" HeaderText="SZ_EMPOLYER_ID" Visible="false"></asp:BoundColumn>
                                                        <%-- 12--%>
                                                        <asp:BoundColumn DataField="SZ_PAYMENT_TYPE" HeaderText="SZ_PAYMENT_TYPE" Visible="false"></asp:BoundColumn>
                                                         <%-- 13--%>
                                                        <asp:BoundColumn DataField="Count" HeaderText="Count" Visible="false"></asp:BoundColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtCompanyID" runat="server" CssClass="btn" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtPaymentID" runat="server" CssClass="btn" Visible="False" Width="10px"></asp:TextBox>

                            <asp:HiddenField ID="hdninvoiceamount" runat="server" />
                            <asp:HiddenField ID="hdnempId" runat="server" />
                            <asp:HiddenField ID="hdnemployer" runat="server" />
                            <asp:HiddenField ID="hdninvoicenumber" runat="server" />
                            <asp:HiddenField ID="hdnbal" runat="server" />

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:TextBox ID="txtSpecialtyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
         <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height:300px; background-color: #B5DF82" id="divView" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="CloseViewFramePopup();">X</a>
                </div><iframe id="frmView" src="" frameborder="0" width="500" height="300"></iframe>
            </div>    
            
    </form>
    
</body>
</html>

