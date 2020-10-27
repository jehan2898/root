<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowDocumentIntake.aspx.cs" Inherits="AJAX_Pages_ShowDocumentIntake" %>

<!DOCTYPE html>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <link rel="Stylesheet" href="../Css/admin.css" type="text/css" />
        <div style="width: 100%; vertical-align: top;">
            <table id="page-title" style="width: 100%;">
                <tr style="width: 100%;">
                    <td>Atttach Document 
                    </td>

                </tr>
            </table>
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                 <ContentTemplate>
            <table style="width: 100%; vertical-align: top;">

                <tr>
                    <td style="width: 20%;">
                        <table>
                            <tr>
                                <td align="center" class="manage-member-lable-td">
                                    <label>
                                    Case Type
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="registration-form-ibox">
                                    <cc1:ExtendedDropDownList ID="ddlCaseType" runat="server" Connection_Key="Connection_String" CssClass="te" Flag_Key_Value="CASETYPE_LIST" Maxlength="50" Procedure_Name="SP_MST_CASE_TYPE" Selected_Text="---Select---" Width="220px" AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="ddlCaseType_extendDropDown_SelectedIndexChanged" />
                                </td>
                            </tr>
                            <tr style="width: 100%;">
                                <td align="center" style="height: 19px"></td>
                                <td align="center" style="height: 19px"></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSave" runat="server" BackColor="#555555" BorderStyle="None" CausesValidation="true" ForeColor="White" Height="30px" Text="Save" ValidationGroup="a" Width="100px" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%;">
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 130%">
                                        <table style="vertical-align: middle; width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td align="left" bgcolor="#336699" class="txt2" height="28" style="width: 413px" valign="middle">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Office"></asp:Label>
                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="10">
                                                            <ProgressTemplate>
                                                                <div id="DivStatus4" runat="Server" class="PageUpdateProgress" style="text-align: center; vertical-align: bottom;">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="Loading....." Height="25px" ImageUrl="~/Images/rotation.gif" Width="24px" />
                                                                    Loading...
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <dx:ASPxGridView ID="grdShowIntakeDocument" runat="server" AutoGenerateColumns="false" keyfieldname="i_id" SettingsPager-PageSize="50" Width="70%">
                                                        <columns>
                                                            <dx:GridViewDataColumn Caption="Select" Settings-AllowSort="False" Width="40px">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Text="Select"></asp:Label>
                                                                </HeaderTemplate>
                                                                <dataitemtemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </dataitemtemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn Caption="Id" Settings-AllowSort="False" Width="50px" visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text="Id"></asp:Label>
                                                                </HeaderTemplate>
                                                                <dataitemtemplate>
                                                                    <%--  <asp:LinkButton ID="lnkid" runat="server"  </asp:LinkButton>--%>
                                                                </dataitemtemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn Caption="Company" FieldName="sz_company_name" Settings-AllowSort="False" Width="150px" visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label>
                                                                </HeaderTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn Caption="Case Type" FieldName="sz_case_type_name" Settings-AllowSort="False" Width="150px" visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text="Case Type"></asp:Label>
                                                                </HeaderTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn Caption="Document" FieldName="sz_name" Settings-AllowSort="False" Width="150px">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label10" runat="server" Text="Document"></asp:Label>
                                                                </HeaderTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn Caption="User " FieldName="sz_user_name" Settings-AllowSort="False" Width="150px" visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="Label11" runat="server" Text="Created By"></asp:Label>
                                                                </HeaderTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            </dx:GridViewDataColumn>
                                                        </columns>
                                                        <settings showverticalscrollbar="false" showfilterrow="false" showgrouppanel="false" />
                                                        <settingsbehavior allowfocusedrow="false" />
                                                        <settingsbehavior allowselectbyrowclick="false" />
                                                        <settingspager position="Bottom" />
                                                    </dx:ASPxGridView>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
                     </ContentTemplate>
                 </asp:UpdatePanel>
            

        </div>
        <asp:HiddenField ID="hdnCaseId" runat="server" />
        <asp:HiddenField ID="hdnCompanyId" runat="server" />
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnDocumentId" runat="server" />
            <asp:HiddenField ID="hdnProvidertId" runat="server" />
        <asp:HiddenField ID="hdnId" runat="server" />

    </form>
</body>
</html>
