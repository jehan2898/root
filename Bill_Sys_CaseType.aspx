<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_CaseType.aspx.cs" Inherits="Bill_Sys_CaseType" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                                                    Case Type
                                                </td>
                                                <td style="width: 35%" align="left">
                                                    &nbsp;&nbsp;<asp:TextBox ID="txtCaseType" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case Type Abbreviation
                                                </td>
                                                <td style="width: 35%" align="left">
                                                    &nbsp;&nbsp;<asp:TextBox ID="txtCaseTypeAbbrivation" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr style="height:6px">
                                            <td>
                                            </td></tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Abbreviation
                                                </td>
                                                <td style="width: 35%" align="left">
                                                    &nbsp;&nbsp;<cc1:ExtendedDropDownList ID="extddlAbbrevation" width="90%" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="GETABBRIVATIONLIST" Selected_Text="--- Select ---" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%" >
                                                Include 1500 form
                                                </td>
                                                <td style="width: 35%">
                                                 &nbsp <asp:CheckBox ID="chkinclude_1500" runat="server" Text=""  />   
                                                </td>
                                            </tr>
                                            <tr style="height:6px">
                                            <td>
                                            </td></tr>
                                            <tr>
                                            <td class="ContentLabel" style="width: 20%;" valign="top">
                                           Bill without Diagnosis Code &nbsp
                                            </td>
                                            <td style="width: 250px" align="left" valign="top">
                                                <asp:CheckBox ID="chkDiagnosisCode" runat="server" />
                                            </td>

                                            <td class="ContentLabel" style="width: 20%;" valign="top">
                                            Modify Procedure Code 
                                            </td>
                                            <td style="width: 250px" align="left" valign="top">
                                               &nbsp;  <asp:CheckBox ID="chkModifyProcedureCode" runat="server" />
                                            </td></tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
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
                                        <asp:DataGrid ID="grdCaseType" runat="server" OnDeleteCommand="grdCaseType_DeleteCommand"
                                            OnPageIndexChanged="grdCaseType_PageIndexChanged" OnSelectedIndexChanged="grdCaseType_SelectedIndexChanged"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                            PageSize="10" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="Case Type ID" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CASE_TYPE_ABBRIVATION" HeaderText="Case Type Abbrevation">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_ABBRIVATION_ID" HeaderText="Abbrevation" Visible="false">
                                                </asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="bt_include_1500" HeaderText="Include_1500" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="bt_diagnosis_code" HeaderText="Diagnosis Code" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="bt_modify" HeaderText="Modify Procedure Code" Visible="false"></asp:BoundColumn>
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

