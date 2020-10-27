<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_Office.aspx.cs" Inherits="Bill_Sys_Office" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
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
        
        function Copy()
        {
            var objCheckBox = document.getElementById('_ctl0_ContentPlaceHolder1_chkSameAddress');
            if(objCheckBox.checked)
            {
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingAdd').value =  document.getElementById('_ctl0_ContentPlaceHolder1_txtOfficeAdd').value;
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingCity').value =  document.getElementById('_ctl0_ContentPlaceHolder1_txtOfficeCity').value;
               document.getElementById('_ctl0_ContentPlaceHolder1_extddlBillingState').value =  document.getElementById('_ctl0_ContentPlaceHolder1_extddlOfficeState').value;
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingZip').value =  document.getElementById('_ctl0_ContentPlaceHolder1_txtOfficeZip').value;
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingPhone').value =  document.getElementById('_ctl0_ContentPlaceHolder1_txtOfficePhone').value;
            }
            else
            {
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingAdd').value =  "";
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingCity').value =  "";
               document.getElementById('_ctl0_ContentPlaceHolder1_extddlBillingState').value =  "NA";
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingZip').value =  "";
               document.getElementById('_ctl0_ContentPlaceHolder1_txtBillingPhone').value =  "";
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
                                                <td style="font-size: 15px; width: 15%; font-family: arial; text-align: right; height: 17px;">
                                                   <asp:Label id="lblLocation" class="lbl" runat="server" Text="Location"></asp:Label></td>
                                                <td class="ContentLabel" style="width: 35%; height: 17px;">
                                                    <cc1:ExtendedDropDownList ID="extddlLocation" Width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Selected_Text="--- Select ---"
                                                        OnextendDropDown_SelectedIndexChanged="extddlLocation_extendDropDown_SelectedIndexChanged" AutoPost_back="True" />
                                                </td>
                                                <td style="font-size: 15px; width: 15%; font-family: arial; text-align: right; height: 17px;">
                                                </td>
                                                <td class="ContentLabel" style="width: 35%; height: 17px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblName" Text="Provider Name" runat="server"></asp:Label></td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtOffice" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblAddress" Text="Provider Address" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtOfficeAdd" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblCity" Text="Provider City" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficeCity" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblState" Text="Provider State" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <cc1:ExtendedDropDownList ID="extddlOfficeState" runat="server" Selected_Text="--- Select ---"
                                                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                        Width="90%"></cc1:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblZip" Text="Provider Zip" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficeZip" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblPhone" Text="Provider Phone" runat="server"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right; vertical-align: top;">
                                                    <asp:Label ID="lblFax" Text="Provider Fax" runat="server"></asp:Label></td>
                                                <td class="ContentLabel" style="width: 35%" valign="top">
                                                    <asp:TextBox ID="txtFax" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox></td>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right; vertical-align: top;">
                                                    <asp:Label ID="lblEmail" Text="Provider Email" runat="server"></asp:Label></td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 15px; width: 15%; font-family: arial; text-align: right">
                                                    <asp:Label ID="lblPrefix" runat="server" Text="Prefix"></asp:Label></td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 16%">
                                                            </td>
                                                            <td width="80%" align="left">
                                                                <asp:TextBox ID="txtPrefix" runat="server" CssClass="textboxCSS" MaxLength="2" Width="32px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtEmail"
                                                        EnableClientScript="True" ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 15px; vertical-align: top; width: 15%; font-family: arial;
                                                    text-align: right">
                                                    &nbsp;</td>
                                                <td class="ContentLabel" style="width: 35%" valign="top">
                                                    <asp:CheckBox ID="chkSameAddress" runat="server" Text="Check here if same as Provider Address"
                                                        onclick="javascript:Copy();" /></td>
                                                <td style="font-size: 15px; vertical-align: top; width: 15%; font-family: arial;
                                                    text-align: right">
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblBillingAdd" runat="Server" Text="Billing Address"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingAdd" runat="server" MaxLength="250" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblBillingCity" runat="Server" Text="Billing City"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingCity" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblBillingState" runat="Server" Text="Billing State"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <cc1:ExtendedDropDownList ID="extddlBillingState" runat="server" CssClass="textboxCSS"
                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                        Selected_Text="--- Select ---" Width="90%"></cc1:ExtendedDropDownList>
                                                </td>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblBillingZip" runat="Server" Text="Billing Zip"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingZip" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblBillingPhone" runat="Server" Text="Billing Phone"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtBillingPhone" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblNPI" runat="Server" Text="NPI"></asp:Label>
                                                </td>
                                                <td style="width: 35%" align="right">
                                                    <asp:TextBox ID="txtNPI" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right;">
                                                    <asp:Label ID="lblFederalTax" runat="Server" Text="Federal Tax"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtFederalTax" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    &nbsp;</td>
                                                <td class="ContentLabel" style="width: 35%">
                                                   
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
                                                        Text="0"></asp:TextBox>
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
                                                <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Name"></asp:BoundColumn>
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
                                                <asp:BoundColumn DataField="SZ_PREFIX" HeaderText="Perfix" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_LOCATION_ID" HeaderText="Location" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_LOCATION_NAME" HeaderText="Location" Visible="false"></asp:BoundColumn>
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
