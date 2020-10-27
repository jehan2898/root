<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerificationSent.aspx.cs" Inherits="AJAX_Pages_VerificationSent" %>

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
        function closeWindow() {
            window.parent.location.reload(true);
        }

        function OpenSingleBillVerificationScan(typeId, billNumber) {
            debugger;
            var caseid = document.getElementById('hdnCaseId').value;
            var specialityid = document.getElementById('hdnSpecialty').value;
            billNumber = billNumber;
            scanVerificationSent(caseid, specialityid, typeId, billNumber, '1');
        }

        function clearData() {
            var text = null;
            var emptyText;
            var grid = document.getElementById("<%= grdVerificationSend.ClientID %>");
            if (grid.rows.length > 0) {
                for (i = 0; i < grid.rows.length; i++) {
                    if (i == 0)
                    {
                        continue;
                    }
                    text = grid.rows[i].cells[4].children[0];
                    emptyText = text.innerText;
                    emptyText = emptyText.replace(/,(\s+)?$/, '');
                    grid.rows[i].cells[4].children[0].innerHTML = text;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%; background-color: White;" border="0">
            <tr>
                <td style="vertical-align: top; width: 100%;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 98%;">
                                <tr>
                                    <td colspan="4" style="text-align: right; width: 98%">
                                        <a style="font-family: Arial; font-size: 12px; color: blue; text-decoration: none; cursor: pointer;" href="#" onclick="javascript:closeWindow();">[Close Window]</a>
                                    </td>
                                </tr>
                            </table>
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
                            <asp:HiddenField runat="server" ID="hdnCaseId" />
                            <asp:HiddenField runat="server" ID="hdnBillNumber" />
                            <asp:HiddenField runat="server" ID="hdnSpecialty" />
                            <asp:HiddenField runat="server" ID="hdnStatusCode" />
                            <asp:HiddenField runat="server" ID="hdnVerificationId" />
                            <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCompanyID" runat="server" CssClass="btn" Visible="False" Width="10px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table style="width: 100%; border: 0px solid; padding-left: 1px; padding-right: 15px;"
            cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div style="overflow: scroll; height: 400px;">
                        <asp:DataGrid ID="grdVerificationSend" Width="100%" CssClass="GridTable GridTable1" runat="Server"
                            AutoGenerateColumns="False">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:BoundColumn DataField="sz_bill_number" HeaderText="Bill#" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                <asp:BoundColumn DataField="verification_request" HeaderText="Verification Request"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                <asp:BoundColumn DataField="verification_date" HeaderText="Request Date" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                <asp:BoundColumn DataField="request_user" HeaderText="Username" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="left"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Answer">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBoxAns" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container,"DataItem.answer")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="answer_date" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                <asp:BoundColumn DataField="answer_user" HeaderText="Answer User" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                <asp:BoundColumn DataField="answer_id" HeaderText="Answer User" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="I_VERIFICATION_ID" HeaderText="id" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="sz_case_id" HeaderText="case_id" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Verification">
                                    <ItemTemplate>
                                        <a href="javascript:void(0);"
                                            onclick="OpenSingleBillVerificationScan('<%# Eval("I_VERIFICATION_ID") %>','<%# Eval("sz_bill_number") %>')">Scan / Upload</a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Specialty" HeaderText="Speciality" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbltext">&nbsp;
                </td>
                <td class="lblcontrol">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button
                        ID="btnSaveSendRequest"
                        CssClass="pure-button pure-button-primary"
                        Width="120"
                        runat="server"
                        Text="Save"
                        OnClick="btnSaveSendRequest_Click" />
                    <input type="button"
                        id="btnClear"
                        class="pure-button"
                        style="width: 120px"
                        value="Clear"
                        onclick="clearData();" />
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtSpecialtyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
