<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Treatment_Objective_Master.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Treatment_Objective_Master"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" style="background-color: White;">
        <tr>
            <td align="center" style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px;
                width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                <asp:UpdatePanel ID="UPdatapanel" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px;">
                                    Treatment:</td>
                                <td align="left" style="width: 350px">
                                    <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                        Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="LoadSpeciality"
                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="---Select---" />
                                </td>
                                <td width="350px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50px;" valign="top">
                                    Area:</td>
                                <td style="width: 350px; vertical-align: top;">
                                    <asp:TextBox ID="txtComplaintcopy" runat="server" MaxLength="4000" Width="350px"
                                        Height="20"></asp:TextBox>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <contenttemplate>
                                          <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                          
                                          </contenttemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td valign="top" align="left" width="350px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px;">
                                </td>
                                <td align="right" style="width: 350px">
                                    <table style="width: 350px;">
                                        <tr style="width: 350px;">
                                            <td style="width: 400px;" align="right">
                                                &nbsp;
                                                <asp:GridView ID="grdTreatmentArea" runat="server" Width="650px" CssClass="mGrid"
                                                    HeaderStyle-CssClass="GridViewHeader" GridLines="None" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    PagerStyle-CssClass="pgr" EnableViewState="true" AllowSorting="true" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <%-- <asp:BoundField DataField="SZ_ID" HeaderText="Id" 
                                                             Visible="false"></asp:BoundField>--%>
                                                        <asp:BoundField DataField="sz_specialty" HeaderText="Treatment" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="Area" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl" Width="550px" runat="server" Font-Size="Small" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_Complaints")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ID" HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="50px" Visible="true"></asp:BoundField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click1"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" width="350px;">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtUserId" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="txtCompanyId" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="txtComplaint" runat="server" Style="display: none;" MaxLength="4000"
                    TextMode="MultiLine" Width="350px" Height="100"></asp:TextBox>
                <asp:TextBox ID="txtProcedureGroup" runat="server" Style="display: none;" MaxLength="4000"
                    TextMode="MultiLine" Width="350px" Height="100"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
