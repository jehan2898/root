<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Transpotation.aspx.cs" Inherits="Bill_Sys_Transpotation" Title="Untitled Page" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                padding-top: 3px; height: 100%">
                <table width="100%">
                <tr>
                <td align="center">
                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmailID"
                        Display="None" ErrorMessage="Your email id is not valid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                </tr>
                </table>
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
                                                <td class="ContentLabel" colspan="4" style="text-align: center; height: 25px;">
                                                   
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" visible="true" style="color: Red;">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="height: 25px; text-align:left">
                                                    <b>Traspotation Comapny</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  style="width: 19%" class="ContentLabel">
                                                    Name
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="textboxCSS" MaxLength="50" Width="60%"></asp:TextBox>&nbsp;<small style ="color :Red">*</small> </td> 
                                                <td class="ContentLabel" style="width: 18%">
                                                    City&nbsp;</td>
                                                <td style="width: 30%" class="ContentLabel">
                                                                <asp:TextBox ID="txtCity" runat="server" CssClass="textboxCSS" MaxLength="50" Width="66%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 19%" valign="top">
                                                    &nbsp;Address</td>
                                                <td class="ContentLabel" style="width: 35%" valign="middle">
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textboxCSS" MaxLength="250" TextMode="MultiLine" Height="90%" Width="62%"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 50%" colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top" style="width: 20%">
                                                                State&nbsp;</td>
                                                            <td style="width: 30%">
                                                                <cc1:ExtendedDropDownList ID="extddlState" runat="server" Width="83%" Selected_Text="--- Select ---"
                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                </cc1:ExtendedDropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width: 20%">
                                                    Zip&nbsp;</td>
                                                            <td style="width: 30%">
                                                    <asp:TextBox ID="txtZip" runat="server" CssClass="textboxCSS" MaxLength="10" Width="66%"></asp:TextBox>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                               
                                            </tr>
                                           
                                           
                                            <tr>
                                                <td class="ContentLabel" style="width: 19%">
                                                    Phone No
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textboxCSS" MaxLength="12" Width="62%"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 18%">
                                                    Email ID&nbsp;</td>
                                                <td style="width: 30%" class="ContentLabel">
                                                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="textboxCSS" MaxLength="50" Width="66%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 19%" valign="top">
                                                    Fax
                                                </td>
                                                <td style="width: 35%" valign="top" class="ContentLabel">
                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="textboxCSS" MaxLength="50" valign="top" Width="62%"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 18%" valign="top">
                                                    &nbsp;</td>
                                                <td style="width: 35%" valign="top" class="ContentLabel">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="height: 23px">
                                                    <asp:TextBox ID="txtTraspotationID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                        CssClass="Buttons" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align: right;">
                                        &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                    <div style="overflow:scroll; height:600px">
                                        <asp:DataGrid ID="grdTranspotaion" AutoGenerateColumns="false" runat="server" Width="100%"
                                            CssClass="GridTable" OnPageIndexChanged="grdTranspotaion_PageIndexChanged" OnSelectedIndexChanged="grdTranspotaion_SelectedIndexChanged"
                                            AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="I_TARNSPOTATION_ID" HeaderText="TARNSPOTATION ID" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_NAME" HeaderText="Name"></asp:BoundColumn>
                                               
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_ADDRESS" HeaderText="Address" > </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_CITY" HeaderText="City"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_STATE_NAME" HeaderText="State"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_STATE_ID" HeaderText="State" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_ZIP" HeaderText="Zip"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_PHONE" HeaderText="Phone"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_EMAIL" HeaderText="Email ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_FAX" HeaderText="Fax"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY ID" Visible="false"></asp:BoundColumn>
                                              
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        </div>
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

