<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Location.aspx.cs" Inherits="Bill_Sys_Location" Title="Untitled Page" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 100%;
                padding-top: 2px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%; height: 177px;" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    Location
                                                </td>
                                                <td class="ContentLabel" style="width: 35%; height: 22px;">
                                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    Address</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 22px">
                                                    State</td>
                                                <td class="ContentLabel" style="width: 35%; height: 22px">
                                                    <cc1:ExtendedDropDownList ID="extddlOfficeState" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Selected_Text="--- Select ---"
                                                        Width="90%"></cc1:ExtendedDropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 22px">
                                                    City</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Zip
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtZip" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <asp:Label ID="lblDisPlayName" runat="server" Text="Bill Display Name" Font-Size="Small"></asp:Label>
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtDisPlayName" runat="server" MaxLength="100" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <asp:Label ID="lblShowDisPlayName" runat="server" Text="Is Display On Bill" Font-Size="Small"></asp:Label>
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:CheckBox ID="chkDispayName" runat="server"></asp:CheckBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:TextBox ID="txtShowDisplayName" runat="server" MaxLength="50" CssClass="textboxCSS"
                                                        Visible="false"></asp:TextBox>&nbsp;
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
                                        <asp:DataGrid ID="grdLocation" runat="server" OnPageIndexChanged="grdLocation_PageIndexChanged"
                                            OnSelectedIndexChanged="grdLocation_SelectedIndexChanged" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_LOCATION_ID" HeaderText="SZ LOCATION ID" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_LOCATION_NAME" HeaderText="Location Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ADDRESS" HeaderText="Address" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CITY" HeaderText="City" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_STATE_ID" HeaderText="STATE" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ZIP" HeaderText="Zip" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILL_DISPLAY_NAME" HeaderText="BILL DISPLAY NAME"
                                                    Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BT_BILL_DISPLAY_NAME" HeaderText="IS DISPLAY ON BILL"
                                                    Visible="False"></asp:BoundColumn>
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
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
