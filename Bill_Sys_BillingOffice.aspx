<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_BillingOffice.aspx.cs" Inherits="Bill_Sys_BillingOffice" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, 
PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



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
        
        function OnMoreInfoClick(szofficeid,szoffice,szofficeadd)
        {
         var url = 'AJAX Pages/Bill_Sys_Billing_Provider_Address.aspx?szofficeid='+szofficeid + '&szoffice='+szoffice+'&szofficeadd='+szofficeadd;
             BillngAddressPop.SetContentUrl(url);
              BillngAddressPop.Show();
               
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
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="width: 100%; height: 25px; text-align: center;" colspan="4">
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    Biling Office Name</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtBilingOfficeName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    Billing Office Address</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtBillingOfficeAddress" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    Billing Office City</td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingOfficeCity" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    Billing Office State</td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <cc1:ExtendedDropDownList ID="extddlBillingOfficeState" runat="server" Selected_Text="--- Select ---"
                                                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                        Width="90%"></cc1:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    Billing Office Zip</td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingOfficeZip" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    Billing Office Phone</td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingOfficePhone" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    NPI</td>
                                                <td class="ContentLabel" style="width: 35%" valign="top">
                                                    <asp:TextBox ID="txtNPI" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox></td>
                                                <td style="width: 15%;" class="ContentLabel">
                                                    Federal Tax</td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtFederalTax" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtBillingOfficeFlag" runat="server" Visible="False" Width="10px"
                                                        Text="1"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align: right;">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdOfficeList" CssClass="GridTable" Width="100%" runat="server"
                                            OnDeleteCommand="grdOfficeList_DeleteCommand" OnPageIndexChanged="grdOfficeList_PageIndexChanged"
                                            OnSelectedIndexChanged="grdOfficeList_SelectedIndexChanged" AutoGenerateColumns="false">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ID" HeaderText="Office ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Billing Office Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ADDRESS" HeaderText="Address" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_CITY" HeaderText="City" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_STATE_ID" HeaderText="State" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ZIP" HeaderText="Zip" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_PHONE" HeaderText="Phone" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_ADDRESS" HeaderText="Billing Address" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_CITY" HeaderText="Billing City" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_STATE_ID" HeaderText="Billing State" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_ZIP" HeaderText="Billing Zip" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BILLING_PHONE" HeaderText="Billing Phone" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_NPI" HeaderText="NPI" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_FEDERAL_TAX_ID" HeaderText="Tax ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_FAX" HeaderText="Fax" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_EMAIL" HeaderText="Email" Visible="false"></asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
                                                <asp:TemplateColumn HeaderText="" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <a href="javascript:void(0);" onclick="OnMoreInfoClick('<%# DataBinder.Eval(Container,"DataItem.SZ_OFFICE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_OFFICE")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_OFFICE_ADDRESS")%>')">
                                                            Provider Address</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
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
    <dx:ASPxPopupControl ID="BillngAddressPop" runat="server" CloseAction="CloseButton"
        HeaderStyle-BackColor="#B5DF82" ContentUrl="AJAX Pages/Bill_Sys_Billing_Provider_Address.aspx"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="BillngAddressPop"
        HeaderText="Provider" HeaderStyle-HorizontalAlign="Left" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="1000px" ToolTip="Billing Address"
        PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
        RenderIFrameForPopupElements="Default" ScrollBars="None" Height="480px" EnableHierarchyRecreation="True">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
