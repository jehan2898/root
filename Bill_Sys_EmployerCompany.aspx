<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_EmployerCompany.aspx.cs" Inherits="Bill_Sys_EmployerCompany" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="validation.js"></script>
    <script type="text/javascript" src="validation.js"></script>
    <script type="text/javascript">
        function ConfirmDelete() {
            var msg = "Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }

        function clickButton1(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }
        }
    </script>
      <asp:scriptmanager id="scriptmanager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:scriptmanager>
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
                                                <td  class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:UpdatePanel ID="UpdatePanelUserMessage" runat="server">
                                                        <ContentTemplate>
                                                            <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Name&nbsp;
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtInsuranceName" runat="server" Width="80%"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Employer Code&nbsp;
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtInsCode" runat="server" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Email&nbsp;
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtInsuranceEmail" runat="server" Width="80%"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Phone&nbsp;
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtInsurancePhone" runat="server" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Fax Number&nbsp;
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtFaxNumber" runat="server" Width="80%" MaxLength="40"></asp:TextBox>
                                                </td>
                                                <!--<td class="ContentLabel" style="width: 15%">
                                                    Zeus ID
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtZeusID" runat="server" MaxLength="10" Width="80%"></asp:TextBox>
                                                </td>-->
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Contact Person
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:TextBox ID="txtContactPerson" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
                                                </td>
<!--                                                <td class="ContentLabel" style="width: 15%">
                                                    Priority Billing
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:CheckBox ID="chkPriority" runat="server" />
                                                </td>
                                                -->
                                            </tr>
                                            <tr>
                                                <!--<td class="ContentLabel" style="width: 15%">
                                                    Paper Authorization
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:CheckBox ID="chkPaperAuthorization" runat="server" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Only 1500 Form
                                                </td>
                                                <td class="ContentLabel" style="width: 35%">
                                                    <asp:CheckBox ID="chk1500Form" runat="server" />
                                                </td>-->
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" class="SectionDevider" colspan="4">
                                                </td>
                                            </tr>
<tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                        <tr>
                                                            <td style="width: 100%;" class="TDPart">
                                                                <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                                    <tr>
                                                                        <td colspan="4" align="left">
                                                                            <b>Address Details </b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                                            Address&nbsp;
                                                                        </td>
                                                                        <td style="width: 35%; height: 22px;" class="ContentLabel">
                                                                            <asp:TextBox ID="txtInsuranceAddress" runat="server" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                                        </td>
                                                                        <td style="width: 35%; height: 22px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ContentLabel" style="width: 15%">
                                                                            Street&nbsp;
                                                                        </td>
                                                                        <td style="width: 35%" class="ContentLabel">
                                                                            <asp:TextBox ID="txtInsuranceStreet" runat="server" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="ContentLabel" style="width: 15%">
                                                                            City&nbsp;
                                                                        </td>
                                                                        <td style="width: 35%" class="ContentLabel">
                                                                            <asp:TextBox ID="txtInsuranceCity" runat="server" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ContentLabel" style="width: 15%">
                                                                            State&nbsp;
                                                                        </td>
                                                                        <td style="width: 35%" class="ContentLabel">
                                                                            <asp:TextBox ID="txtInsuranceState" runat="server" Visible="false"></asp:TextBox>
                                                                            <cc1:ExtendedDropDownList ID="extddlState" runat="server" Selected_Text="--- Select ---"
                                                                                Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                                                Width="90%"></cc1:ExtendedDropDownList>
                                                                        </td>
                                                                        <td class="ContentLabel" style="width: 15%">
                                                                            Zip &nbsp;
                                                                        </td>
                                                                        <td style="width: 35%" class="ContentLabel">
                                                                            <asp:TextBox ID="txtInsuranceZip" runat="server" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ContentLabel" style="width: 15%">
                                                                            Default
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:CheckBox ID="chkDefault" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="ContentLabel" colspan="4" style="height: 23px">
                                                                            <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                                                            <asp:TextBox ID="txtInsuranceAddressID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                            <asp:Button ID="btnAddAddress" runat="server" OnClick="btnAddAddress_Click" Text="Add"
                                                                                Width="80px" CssClass="Buttons"/>
                                                                            <asp:Button ID="btnUpdateAddress" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                                                                OnClick="btnUpdateAddress_Click" />
                                                                            <asp:Button ID="btnClearAddress" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                                                OnClick="btnClearAddress_Click" />
                                                                        </td>
                                                                    </tr>
<tr>
                                                                        <td colspan="4" width="100%" style="text-align: left;">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:Button ID="btnDeleteInsuranceAddress" runat="server" CssClass="Buttons" Text="Delete"
                                                                                            OnClick="btnDeleteInsuranceAddress_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div style="overflow: scroll; height: 150px; width: 97%">
                                                                                            <asp:DataGrid ID="grdInsuranceAddress" CssClass="GridTable" runat="server" AutoGenerateColumns="False"
                                                                                                Width="100%" OnSelectedIndexChanged="grdInsuranceAddress_SelectedIndexChanged">
                                                                                                <ItemStyle CssClass="GridRow" />
                                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                                <Columns>
                                                                                                    <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                                                                    <asp:BoundColumn DataField="SZ_EMP_ADDRESS_ID" HeaderText="Address ID" Visible="False">
                                                                                                    </asp:BoundColumn>
                                                                                                    <asp:BoundColumn HeaderText="Address" DataField="SZ_EMPLOYER_ADDRESS"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn HeaderText="Street" DataField="SZ_EMPLOYER_STREET"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn HeaderText="City" DataField="SZ_EMPLOYER_CITY"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn HeaderText="StateName" DataField="SZ_EMPLOYER_STATE"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn HeaderText="Zip" DataField="SZ_EMPLOYER_ZIP"></asp:BoundColumn>
                                                                                                    <asp:BoundColumn HeaderText="Unique ID" DataField="I_UNIQUE_ID" Visible="false">
                                                                                                    </asp:BoundColumn>
                                                                                                    <asp:BoundColumn HeaderText="StateID" DataField="SZ_EMPLOYER_STATE_ID" Visible="false">
                                                                                                    </asp:BoundColumn>
                                                                                                    <asp:TemplateColumn>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkInsuranceAddressDelete" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateColumn>
                                                                                                    <asp:BoundColumn HeaderText="Default" DataField="I_DEFAULT" Visible="false"></asp:BoundColumn>
                                                                                                </Columns>
                                                                                                <PagerStyle Mode="NumericPages" />
                                                                                            </asp:DataGrid>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>&nbsp;
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:TextBox ID="txtInsuranceCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtDefault" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtPriorityBilling" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtPaperAuthorization" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txt1500Form" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table style="width: 100%">
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                                    <asp:ImageButton ID="btnexport" runat="server" OnClick="btnexport_Click" ImageUrl="~/Images/Excel.jpg"  style="border:none;"  height="15px" width ="15px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="grdEmployerCompanyList" runat="server" Width="100%" CssClass="GridTable"
                                                        AutoGenerateColumns="false" AllowPaging="true" PageSize="100" PagerStyle-Mode="NumericPages"
                                                        OnDeleteCommand="grdEmployerCompanyList_DeleteCommand" OnPageIndexChanged="grdEmployerCompanyList_PageIndexChanged"
                                                        OnSelectedIndexChanged="grdEmployerCompanyList_SelectedIndexChanged" OnItemCommand="grdEmployerCompanyList_ItemCommand">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_ID" HeaderText="Employer ID" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%-- <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Name"></asp:BoundColumn>--%>
                                                            <asp:TemplateColumn HeaderText="Name">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlName" runat="server" CommandName="NameSearch" CommandArgument="SZ_EMPLOYER_NAME"
                                                                        Font-Bold="true" Font-Size="12px">Name</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_EMPLOYER_NAME")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Employer Code">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlInsCompanycode" runat="server" CommandName="InsuranceCodeSearch"
                                                                        CommandArgument="SZ_EMPLOYER_COMPANY_CODE" Font-Bold="true" Font-Size="12px">Employer Code</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_EMPLOYER_COMPANY_CODE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Address">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlInsCompanyAddress" runat="server" CommandName="AddressSearch"
                                                                        CommandArgument="SZ_EMPLOYER_ADDRESS" Font-Bold="true" Font-Size="12px">Address</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_EMPLOYER_ADDRESS")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%-- 
                                                <asp:BoundColumn DataField="SZ_INSURANCE_ADDRESS" HeaderText="Address"></asp:BoundColumn>--%>
                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_STREET" HeaderText="Street" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="City">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlInsCompanyCity" runat="server" CommandName="CitySearch" CommandArgument="SZ_EMPLOYER_CITY"
                                                                        Font-Bold="true" Font-Size="12px">City</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_EMPLOYER_CITY")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%--    <asp:BoundColumn DataField="SZ_INSURANCE_CITY" HeaderText="City"></asp:BoundColumn>
                                                  <asp:BoundColumn DataField="SZ_INSURANCE_STATE" HeaderText="State" ></asp:BoundColumn>--%>
                                                            <asp:TemplateColumn HeaderText="State">
                                                                <HeaderTemplate>
                                                                    <asp:LinkButton ID="lnlInsCompanyState" runat="server" CommandName="StateSearch"
                                                                        CommandArgument="SZ_EMPLOYER_STATE" Font-Bold="true" Font-Size="12px">State</asp:LinkButton>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_EMPLOYER_STATE")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_ZIP" HeaderText="Zip" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_PHONE" HeaderText="Phone"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_EMAIL" HeaderText="Email"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Name" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="SZ_EMPLOYER_COMPANY_CODE" HeaderText="Insurance Code"
                                                                Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_ZEUS_ID" HeaderText="ZEUS ID" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CONTACT_PERSON" HeaderText="SZ_CONTACT_PERSON" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_FAX_NUMBER" HeaderText="SZ_FAX_NUMBER" Visible="false">
                                                            </asp:BoundColumn>
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

