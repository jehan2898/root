<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_sys_workers_comp_board.aspx.cs" Inherits="Bill_sys_workers_comp_board" Title="Untitled Page" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

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

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
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
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Address&nbsp;</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtAdd" runat="server" CssClass="textboxCSS" MaxLength="50" Height="45px" TextMode="MultiLine" Width="303px"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    City
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    State&nbsp;</td>
                                                <td style="width: 35%; height: 22px;" class="ContentLabel">
                                                   
                                                         <cc1:ExtendedDropDownList ID="extddlstate" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                </cc1:ExtendedDropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    Zip Code</td>
                                                <td style="width: 35%; height: 22px;" class="ContentLabel">
                                                 <asp:TextBox ID="txtpincode" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="height: 23px">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdWorkComp" runat="server" 
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" 
                                            PageSize="10" PagerStyle-Mode="NumericPages" OnSelectedIndexChanged="grdWorkComp_SelectedIndexChanged">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="I_WORK_COMP_ID" HeaderText="Work Comp ID" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ADDRESS" HeaderText="Address"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CITY" HeaderText="City">
                                                </asp:BoundColumn>                                                
                                                <asp:BoundColumn DataField="STATENAME" HeaderText="State"  >
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PINCODE" HeaderText="Zip Code" >
                                                </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="STATE" HeaderText="State" Visible="false"  >
                                                </asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
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

