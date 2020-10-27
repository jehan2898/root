<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Make_Invoice_Payment.aspx.cs"
    Inherits="AJAX_Pages_Make_Invoice_Payment" %>

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
    </style>
    <script type="text/javascript">

        function Clear() {
            //alert("call");
            document.getElementById('txtDate').value = '';
            document.getElementById('txtChequeNo').value = '';
            document.getElementById('txtAmount').value = '';
            document.getElementById('txtNotes').value = '';
            document.getElementById('btnSavesent').disabled = false;
            document.getElementById('btnUpdate').disabled = true;
            
            
             
         }
        function ooValidate() {
            if (document.getElementById('txtDate').value == '') {
                alert('Please select cheque date');
                return false;
            }
            if (document.getElementById('txtChequeNo').value == '') {
                alert('Please select cheque number');
                return false;
            }
            if (document.getElementById('txtAmount').value == '') {
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

        </script>
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
                                <table border="0" style="margin: 0 auto; width: 90%; height: 100%;"
                                    cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="lbltext">&nbsp;
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
                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Employer
                                        </td>
                                        <td class="style7" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblemp" runat="server"></asp:Label>
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Invoice#
                                        </td>
                                        <td class="auto-style4" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblinvoicenumber" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style3" style="text-align: left; padding-left: 8px;" nowrap="nowrap">Invoice Amount
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblinvoiceamount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbltext" style="text-align: left; padding-left: 8px">Balance
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 5px">
                                            <asp:Label ID="lblbal" runat="server"></asp:Label>
                                            
                                        </td>
                                        <td class="auto-style3" style="text-align: left; padding-left: 8px;">Cheque Date
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
                                        <td class="lbltext" style="text-align: left; padding-left: 8px; width: 30px">Cheque#
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 5px; width: 100px">
                                            <asp:TextBox ID="txtChequeNo" runat="server" Width="100%" CssClass="search-input" Style="text-align: left"></asp:TextBox>
                                        </td>
                                        
                                        
                                    </tr>
                                    <tr>
                                        <td class="lbltext" style="text-align: left; padding-left: 8px; width: 10px">Amount
                                        </td>
                                        <td class="auto-style4" style="text-align: left; padding-left: 5px;">
                                            <asp:TextBox ID="txtAmount" runat="server" Width="100%" CssClass="search-input" Style="text-align: left" ></asp:TextBox>
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 8px; width: 30px">Notes
                                        </td>
                                        <td class="lbltext" style="text-align: left; padding-left: 5px; width: 100px" colspan="3">
                                            <asp:TextBox ID="txtNotes" runat="server" Width="100%" CssClass="search-input"
                                                Style="text-align: left"></asp:TextBox>
                                        </td>
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

                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                 <tr>

                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                       <asp:Button ID="btndelete" CssClass="pure-button pure-button-primary" Width="120"
                                            runat="server" Text="Delete" OnClick="btnDelete_Click" />


                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                   <tr>
                                    <td>
                                        
                                        <div style="overflow: scroll; width: 99%; height: 400px">
                                            <table width="100%">
                                                <asp:DataGrid ID="grdMakeInvoicePayment" runat="server" AutoGenerateColumns="false"
                                                    OnItemCommand="grdMakeInvoicePayment_ItemCommand"
                                                    Width="99%" CssClass="GridTable" DataKeyField="id">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <HeaderStyle CssClass="GridHeader1" />
                                                    <Columns>
                                                            <%-- 0--%>
                                                      <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                    <%-- 1--%>
                                                        <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Employer" ></asp:BoundColumn>
                                                        <%-- 2--%>
                                                        <asp:BoundColumn DataField="sz_invoice_id" HeaderText="Invoice#" ></asp:BoundColumn>
                                                      
                                                        <%-- 3--%>
                                                        <asp:BoundColumn DataField="mn_cheque_amount" HeaderText="Cheque Amount" DataFormatString="{0:C}"></asp:BoundColumn>
                                                        <%-- 4--%>
                                                        <asp:BoundColumn DataField="dt_cheque_date" HeaderText="Date" ></asp:BoundColumn>
                                                        <%-- 5--%>
                                                        <asp:BoundColumn DataField="sz_cheque_no" HeaderText="cheque#" ></asp:BoundColumn>
                                                        
                                                        <%-- 6--%>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Select All" onclick="javascript:SelectAll(this.checked);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkRemove" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                         <%-- 7--%>
                                                            <asp:BoundColumn DataField="id" HeaderText="id" Visible="false"></asp:BoundColumn>
                                                        <%-- 8--%>
                                                            <asp:BoundColumn DataField="notes" HeaderText="notes" Visible="false"></asp:BoundColumn>
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
    </form>
</body>
</html>
