<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssociateCodeToProcedure.aspx.cs"
    Inherits="AJAX_Pages_AssociateCodeToProcedure" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
//        function SelectAllDpctor(ival) {
//            var f = document.getElementById('grdProcedure');
//            var str = 1;
//            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
//                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

//                    if (f.getElementsByTagName("input").item(i).disabled == false) {
//                        f.getElementsByTagName("input").item(i).checked = ival;
//                    }

//                }
//            }
//        }

//        function confirm_payment_save() {

//            var f = document.getElementById("grdProcedure");
//            var bfFlag = false;
//            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
//                if (f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') != -1) {
//                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
//                        if (f.getElementsByTagName("input").item(i).checked != false) {

//                            bfFlag = true;
//                        }
//                    }
//                }
//            }
//            if (bfFlag == false) {
//                alert('Please select record.');
//                return false;
//            }

//            if (confirm("Do you want to Save?") == true) {

//                return true;
//            }
//            else {
//                return false;
//            }
//        }
        function confirm_payment_remove() {
            if (confirm("Do you want to Remove?") == true) {

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
    <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <div>
    <asp:updatepanel id="UpdatePanel2" runat="server">
                        <ContentTemplate>
        <table id="tb2" runat="server" width="100%">
        <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
            <tr>
                <td style="font-family: Arial; font-size: 12px; text-align: left;" valign="top" align="left">
                </td>
                <td valign="top" align="center">
                    <table width="100%" border="0">
                        <tr>
                            <td style="width: 78%">
                                &nbsp;
                            </td>
                            <%--<td>
                                <input id="btnClear" type="button" style="width: 50PX" onclick="javascript:ClearGrid();"
                                    name="Clear" value="Clear" visible="true" />
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 15%">
                </td>
                <td style="font-family: Arial; font-size: 12px; text-align: left; width: 85%;" valign="top"
                    align="left">
                    <dx:ASPxGridView ID="grdProcedure" runat="server" KeyFieldName="CODE" AutoGenerateColumns="false"
                        Width="85%" SettingsPager-PageSize="50" SettingsCustomizationWindow-Height="300"
                        Settings-VerticalScrollableHeight="300">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="SZ_CODE_DESCRIPTION" Caption="Procedure Code" HeaderStyle-HorizontalAlign="Center" Width="30%">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="FLT_AMOUNT" Caption="Amount" HeaderStyle-HorizontalAlign="Center" Width="20%">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <%--<dx:GridViewDataColumn Caption="All" Width="12%" CellStyle-HorizontalAlign="Center">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderTemplate>
                                    <asp:checkbox id="chkSelectAll" runat="server" onclick="javascript:SelectAllDpctor(this.checked);"
                                        text="All" tooltip="Select All" />
                                </HeaderTemplate>
                                <DataItemTemplate>
                                    <asp:checkbox id="chkSelect" runat="server" />
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                            </dx:GridViewDataColumn>--%>
                            <dx:GridViewDataColumn Caption="" Width="10%" CellStyle-HorizontalAlign="Center">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <DataItemTemplate>
                                    <asp:TextBox ID="txtProcedure" runat="server" Width="100%" Text="0.0"></asp:TextBox>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                            </dx:GridViewDataColumn>
                            <%--3--%>
                            <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Visible="false" Caption="CompanyID"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <%--6--%>
                            <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_CODE" Visible="false" Caption="SZ_PROCEDURE_CODE"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_PROC_CODE_ID" Visible="false" Caption="PROC_CODE_ID"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="I_EVENT_ID" Visible="false" Caption="I_EVENTID"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="I_EVENT_PROC_ID" Visible="false" Caption="I_EVENT_PROC_ID"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_PROC_CODE" Visible="false" Caption="SZ_PROC_CODE"
                                HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                        <SettingsBehavior AllowFocusedRow="false" />
                        <SettingsBehavior AllowSelectByRowClick="true" />
                        <SettingsPager Position="Bottom" />
                        <SettingsCustomizationWindow Height="100px"></SettingsCustomizationWindow>
                    </dx:ASPxGridView>
                    <asp:textbox id="txtCompanyName" runat="server" visible="false">
                    </asp:textbox>
                    <asp:textbox id="txtBillNo" runat="server" visible="false">
                    </asp:textbox>
                    <asp:textbox id="txtPaymentId" runat="server" visible="false">
                    </asp:textbox>
                    <asp:textbox id="txtTypeCodeId" runat="server" visible="false">
                    </asp:textbox>
                    <asp:TextBox ID="txtCompanyId" runat="server" Width="10px" Visible="false"></asp:TextBox>
                    <%--<asp:updatepanel id="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                            <asp:UpdateProgress ID="UpdateProgress123" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 100px;
                                        left: 450px" runat="Server">
                                        <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        Loading...
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <center>
                                
                            </center>
                        <%--</ContentTemplate>
                    </asp:updatepanel>--%>
                </td>
            </tr>
            <tr>
            <td style="font-family: Arial; font-size: 12px; text-align: left;" valign="top" align="left">
                </td>
            <td style="text-align:center">
            <asp:Button Style="width: 80px" ID="btnAdd" Text="Add" runat="Server" align="Center"
                                    OnClick="btnAdd_Click" />
                                <asp:Button Style="width: 121px" ID="Btn_Clear" Text="Clear All Payment" runat="Server" 
                                    align="Center" onclick="Btn_Clear_Click"/>
            </td></tr>
        </table>
        </ContentTemplate>
                    </asp:updatepanel>
    </div>
    </form>
</body>
</html>