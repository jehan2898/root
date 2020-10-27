<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Modifier.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Modifier" Title="Untitled Page" %>


<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Data" Assembly="DevExpress.Data.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
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



<%@ Register Assembly="DevExpress.XtraCharts.v16.2.Web, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="dxcharts" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.PivotGrid.v16.2.Core, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraPivotGrid" TagPrefix="temp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
</asp:ScriptManager>
<script type="text/javascript" src="../validation.js"></script>
    <script type="text/javascript">
    function Clear() 
    {
        document.getElementById("<%=txtModifier.ClientID %>").value='';
        document.getElementById("<%=txtIDCode.ClientID %>").value='';
        document.getElementById("<%=txtModifierDesc.ClientID %>").value = '';
        document.getElementById("<%=btnAdd.ClientID %>").disabled = false;
        document.getElementById("<%=btnUpdate.ClientID %>").disabled = true;
    }
     function SelectAllModifier(ival) {
            var f = document.getElementById('<%=grdModifier.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }
        function CheckModifierExists() {
            var m = document.getElementById("<%=txtModifier.ClientID %>").value;
            if (m == '') {
                alert('Please enter Modifier');
                return false;
            }
        }
        function checkModifierChecked() {
            var f = document.getElementById('<%=grdModifier.ClientID%>');
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        if (confirm("Are you sure to delete modifier?"))
                            return true;
                        return false;
                    }
                }
            }
            alert('Please select modifier.');
            return false;
        } 
    </script>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
 <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        background-color: White;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
          
                <table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
                  
                            <%--<asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2"
                                displayafter="10">
                                <progresstemplate>
                                    <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 200px;
                                        left: 600px" runat="Server">
                                        <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Searching....."
                                            Height="25px" Width="24px"></asp:Image>
                                        Processing...
                                    </div>
                                </progresstemplate>
                            </asp:updateprogress>--%>
                    <tr>
                        <td colspan="3">
                            <asp:updatepanel id="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:updatepanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="text-align: left; width: 100%; vertical-align: top;">
                                                    <table style="text-align: left; width: 50%;">
                                                        <tr>
                                                            <td style="text-align: left; width: 100%; vertical-align: top;">
                                                                <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                                                    height: 50%; border: 0px solid #B5DF82;">
                                                                    <tr>
                                                                        <td style="width: 100%; height: 0px;" align="center">
                                                                            <table border="0" cellpadding="10" cellspacing="0" style="width: 100%; border-right: 1px solid #d3d3d3;
                                                                                border-left: 1px solid #d3d3d3; border-bottom: 1px solid #d3d3d3" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" class="txt2" colspan="2">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        Modifier
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                        ID Code
                                                                                    </td>
                                                                                    
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <asp:TextBox ID="txtModifier" runat="server" Width="225px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:TextBox ID="txtIDCode" runat="server" Width="225px"></asp:TextBox>
                                                                                    </td>
                                                                                    
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" colspan="2">
                                                                                        Description
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" colspan="2">
                                                                                        <asp:TextBox ID="txtModifierDesc" runat="server" Width="450px" Height="100px" TextMode="MultiLine" ></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="width: 100%" align="center">
                                                                                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>--%>
                                                                                                <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2"
                                                                                                    displayafter="10">
                                                                                                    <progresstemplate>
                                                                                                        <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 200px;
                                                                                                            left: 600px" runat="Server">
                                                                                                            <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Searching....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                            Processing...
                                                                                                        </div>
                                                                                                    </progresstemplate>
                                                                                                </asp:updateprogress>
                                                                                                &nbsp;
                                                                                                <asp:button id="btnAdd" runat="server" text="Add" onclick="btnAdd_Click" />
                                                                                                <asp:button id="btnUpdate" runat="server" text="Update" onclick="btnUpdate_Click" />
                                                                                                <asp:button id="btnSearch" runat="server" text="Search" onclick="btnSearch_Click" />
                                                                                                <asp:button id="btnDelete" runat="server" text="Delete" onclick="btnDelete_Click" />
                                                                                                <input style="width: 60px" id="btnClear" onclick="Clear();" type="button" value="Clear"
                                                                                                    runat="server" />
                                                                                            <%--</ContentTemplate>
                                                                                            
                                                                                        </asp:UpdatePanel>--%>
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
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div style="height: 400px; width: 1050px; background-color: gray; overflow: scroll;">
                               <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                                        <dx:ASPxGridView ID="grdModifier" runat="server" KeyFieldName="i_modifier_id" AutoGenerateColumns="false"
                                            Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                            Settings-VerticalScrollableHeight="330" OnRowCommand="grdModifier_RowCommand" >
                                            <Columns>
                                             <dx:GridViewDataColumn Caption="select" VisibleIndex="0" Width="30px">
                                                    <HeaderTemplate>
                                                       
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkselect"  runat="server" CommandName="select" Text="Select"></asp:LinkButton>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            
                                                <%--0--%>
                                                <dx:GridViewDataColumn FieldName="i_modifier_id" Caption="Modifier ID" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false" >
                                                </dx:GridViewDataColumn>
                                                <%--1--%>
                                                <dx:GridViewDataColumn FieldName="sz_modifier" Caption="Modifier" HeaderStyle-HorizontalAlign="Center"
                                                    Width="25px" Settings-AllowSort="true">
                                                </dx:GridViewDataColumn>
                                                <%--2--%>
                                                <dx:GridViewDataColumn FieldName="sz_code" Caption="ID Code" HeaderStyle-HorizontalAlign="Center"
                                                    Width="25px" Settings-AllowSort="true">
                                                </dx:GridViewDataColumn>
                                                <%--3--%>
                                                <dx:GridViewDataColumn FieldName="sz_modifier_desc" Caption="Modifier Description" HeaderStyle-HorizontalAlign="Center"
                                                    Width="25px" Settings-AllowSort="true">
                                                </dx:GridViewDataColumn>
                                                <%--4--%>
                                                 <dx:GridViewDataColumn Caption="chk1" VisibleIndex="5" Width="30px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAllModifier" runat="server" onclick="javascript:SelectAllModifier(this.checked);"
                                                        ToolTip="Select All" />
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                    <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                            <Styles>
                                                <FocusedRow  cssClass="dxgvFocusedGroupRow">
                                                </FocusedRow>
                                                <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                </AlternatingRow>
                                                <SelectedRow cssClass="dxgvFocusedGroupRow">
                                                </SelectedRow>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    <%--</ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:textbox id="txtCompanyID" runat="server" visible="false">
                            </asp:textbox>
                            <asp:hiddenfield id="hdnClick" runat="server">
                            </asp:hiddenfield>
                            <asp:textbox id="txtSelectedModifierID" runat="server" visible="false">
                            </asp:textbox>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
    </table>
        </ContentTemplate>                                                  
                    </asp:UpdatePanel>
</asp:Content>

