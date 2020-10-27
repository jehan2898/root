<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_Attorney.aspx.cs" Inherits="Bill_Sys_Attorney" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
        function ConfirmDelete()
        {
             var msg = "Do you want to proceed?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <usermessage:messagecontrol runat="server" id="usrMessage"></usermessage:messagecontrol>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%" border="0">
                    <tr>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="6"  style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="text-align: center; height: 25px;">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" visible="true" style="color: Red;">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;">
                                                    Firm Name
                                                </td>
                                                <td style="width: 35%; font-size: 12px; font-family: Arial;">
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textboxCSS" MaxLength="50" Width="42%"></asp:TextBox></td>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;">
                                                    Contact Name
                                                </td>
                                                <td style="width: 35%; font-size: 12px; font-family: Arial;">
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textboxCSS" MaxLength="20" Width="42%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;" valign="top">
                                                    Attorney Address</td>
                                                <td style="width: 35%; font-size: 12px; font-family: Arial;">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxCSS" MaxLength="250"
                                                        TextMode="MultiLine" Width="82%"></asp:TextBox></td>
                                                <td style="width: 50%" colspan="2" valign="top" align="left">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td valign="top" style="width: 15%; font-size: 12px; font-family: Arial;">
                                                                Attorney City
                                                            </td>
                                                            <td style="width: 35%; font-size: 12px; font-family: Arial; padding-left:4px; padding-bottom:4px;">
                                                                <asp:TextBox ID="txtCity" runat="server" CssClass="textboxCSS" MaxLength="50" Width="42%"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width: 15%; font-size: 12px; font-family: Arial;">
                                                                Attorney State
                                                            </td>
                                                            <td style="width: 35%; font-size: 12px; font-family: Arial;padding-left:4px;">
                                                                <cc1:ExtendedDropDownList ID="extddlState" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                </cc1:ExtendedDropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;">
                                                    Attorney Zip Code
                                                </td>
                                                <td style="width: 35%; font-size: 12px; font-family: Arial;">
                                                    <asp:TextBox ID="txtZip" runat="server" CssClass="textboxCSS" MaxLength="10" Width="42%"></asp:TextBox></td>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;">
                                                    Phone No
                                                </td>
                                                <td style="width: 35%; font-size: 12px; font-family: Arial;">
                                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textboxCSS" MaxLength="12" Width="42%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;" valign="top">
                                                    Fax
                                                </td>
                                                <td style="width: 35%; font-size: 12px; font-family: Arial;" valign="top">
                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="textboxCSS" MaxLength="50" valign="top" Width="42%"></asp:TextBox></td>
                                                <td  style="width: 15%; font-size: 12px; font-family: Arial;" valign="top">
                                                    Email ID
                                                </td>
                                                <td style="width: 35%; font-size: 12px; font-family: Arial;" valign="top">
                                                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="textboxCSS" MaxLength="50" Width="60%"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtEmailID"
                                                        EnableClientScript="True" ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;">
                                                    Default Firm</td>
                                                <td style="width: 35%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:CheckBox ID="chkDefaultFirm" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 15%; font-size: 12px; font-family: Arial;">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="height: 23px">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click"
                                                        CssClass="Buttons" CausesValidation="false" />
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                        CssClass="Buttons" CausesValidation="false" />
                                                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" Width="100px" OnClick="btnExportToExcel_Click"
                                                        CssClass="Buttons" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align: right; height: 44px;">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdAttorney" AutoGenerateColumns="false" runat="server" Width="100%"
                                            CssClass="GridTable" OnPageIndexChanged="grdAttorney_PageIndexChanged" OnSelectedIndexChanged="grdAttorney_SelectedIndexChanged"
                                            AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_ID" HeaderText="ATTORNEY ID" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_FIRST_NAME" HeaderText="Firm Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_LAST_NAME" HeaderText="Contact Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_ADDRESS" HeaderText="Address"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_CITY" HeaderText="City"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_STATE_NAME" HeaderText="State"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_ZIP" HeaderText="Zip"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_PHONE" HeaderText="Phone"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_FAX" HeaderText="Fax"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_EMAIL" HeaderText="Email ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ATTORNEY_STATE_ID" HeaderText="State ID" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BT_DEFAULT" HeaderText="Default Attorney" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
