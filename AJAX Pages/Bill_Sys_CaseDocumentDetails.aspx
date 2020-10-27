<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CaseDocumentDetails.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_CaseDocumentDetails" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Case Document Details</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
    <link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />
    <script type="text/javascript">

        function SelectCaseAll(ival) {

            var f = document.getElementById('grdViewDocuments');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }



                }


            }

        }

        function CheckDocument() {

            var f = document.getElementById('grdViewDocuments');
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        return true;

                    }
                }
            }
            alert('Please select docuemnt');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="upCaseNO" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <UserMessage:MessageControl runat="server" ID="MessageControl1"></UserMessage:MessageControl>
                                            <asp:UpdateProgress ID="UpdatePanel19" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="upCaseNO">
                                                <ProgressTemplate>
                                                    <div id="Div3" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                        runat="Server">
                                                        <asp:Image ID="img46" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                            Height="25px" Width="24px"></asp:Image>
                                                        Loading...</div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; height: 20px;" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel101" runat="server">
                                        <ContentTemplate>
                                            <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <extddl:ExtendedDropDownList ID="extddlSpecialty" runat="server" Connection_Key="Connection_String"
                                        Flag_Key_Value="GET_SPECIALTY" AutoPost_back="true" Procedure_Name="SP_MST_SPECIALTY_LHR"
                                        OnextendDropDown_SelectedIndexChanged="extddlSpecialty_SelectedIndexChanged"
                                        Selected_Text="---Select---" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DataGrid ID="grdViewDocuments" Width="100%" runat="Server" AutoGenerateColumns="False"
                                        OnItemCommand="grdViewDocuments_ItemCommand" CssClass="GridTable">
                                        <HeaderStyle CssClass="GridHeader1" />
                                        <ItemStyle CssClass="GridViewHeader" />
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectCaseAll" runat="server" onclick="javascript:SelectCaseAll(this.checked);"
                                                        ToolTip="Select All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkView" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="File Name" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LKBView" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Filename")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="FilePath" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Filename" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                                                        <asp:ListItem Value="8" Selected="true">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="0">Report</asp:ListItem>
                                                        <asp:ListItem Value="1">Referral</asp:ListItem>
                                                        <asp:ListItem Value="2">AOB</asp:ListItem>
                                                        <asp:ListItem Value="3">Comp Authorization</asp:ListItem>
                                                        <asp:ListItem Value="4">HIPPA Consent</asp:ListItem>
                                                        <asp:ListItem Value="5">Lien Form</asp:ListItem>
                                                        <asp:ListItem Value="6">Misc</asp:ListItem>
                                                        <asp:ListItem Value="7">Additional Reports</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkrefreshpaidbills" runat="server" Text="Refresh Paid Bills" OnCheckedChanged="OnCheckedChanged_paidbillsrefesh"
                                        AutoPostBack="true" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnUPdate" runat="server" Text="Save" Width="104px" OnClick="btnUPdate_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnOpenDocument" runat="server" Text="Open Document" Width="104px"
                                        OnClick="btnOpenDocument_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCaseID" runat="server" Visible="false">
                </asp:TextBox>
                <asp:TextBox ID="txtEventProcID" runat="server" Visible="false">
                </asp:TextBox>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false">
                </asp:TextBox>
                <asp:TextBox ID="txtProcGroup" runat="server" Visible="false">
                </asp:TextBox>
                <asp:TextBox ID="txtProcGroupID" runat="server" Visible="false">
                </asp:TextBox>
                <asp:TextBox ID="hdnmdltxtSpeciality" runat="server" Visible="false">
                </asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
