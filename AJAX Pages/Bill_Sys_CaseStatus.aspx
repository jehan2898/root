<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CaseStatus.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_CaseStatus"
    Title="Untitled Page" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                    <script type="text/javascript" src="../validation.js"></script>
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
        
           function ShowChildGrid (obj)
        {
             var div = document.getElementById(obj);           
             div.style.display ='block';                       
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID ="test" runat ="server" >
    <ContentTemplate>
    
            <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                height: 100%; background-color: white;">
                <tr>
                    <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                        padding-top: 3px; height: 100%; vertical-align: top;">
                        <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%; background-color: White;">
                            <tr>
                                <td class="LeftTop" style="height: 18px">
                                </td>
                                <td class="CenterTop" style="height: 18px">
                                </td>
                                <td class="RightTop" style="height: 18px">
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftCenter">
                                </td>
                                <td class="Center" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
                                        background-color: white;">
                                        <tr>
                                            <td style="width: 100%">
                                                <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                                    border-bottom: #b5df82 1px solid">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#b5df82" class="txt2" colspan="3">
                                                            <b class="txt3">Case Status</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" valign="middle" style="height: 28px">
                                                        <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label></td>
                                                        <td align="left" colspan="1" valign="middle" style="height: 28px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 30px;" class="td-widget-bc-search-desc-ch1">
                                                            Case Status
                                                        </td>
                                                        <td style="width: 100px; height: 30px;">
                                                            <asp:TextBox ID="txtCaseStatus" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                             <ajaxToolkit:TextBoxWatermarkExtender ID="tbwe" runat="server" TargetControlID="txtCaseStatus" WatermarkText="Status Name" WatermarkCssClass ="textboxCSS"/>
                                                        <td style="width: 100px; height: 30px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px; height: 31px;" class="td-widget-bc-search-desc-ch1">
                                                            Case Type</td>
                                                        <td style="width: 100px; height: 31px;">
                                                            <asp:DropDownList ID="ddl_Case_Status" runat="server">
                                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                                <asp:ListItem Value="1">In Active</asp:ListItem>
                                                                <asp:ListItem Value="2">Unbilled</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td style="width: 100px; height: 31px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px; text-align: center;" colspan="3">
                                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px" /><asp:Button
                                                                ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="80px" /><asp:Button
                                                                    ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: right;">
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%;">
                                                <table width="100%">
                                                </table>
                                                <table style="vertical-align: middle; width: 100%;">
                                                    <tbody>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                            </td>
                                                            <td style="width: 60%" align="right">
                                                                Record Count:
                                                                <%= this.grdCaseStatusList.RecordCount%>
                                                                | Page Count:
                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                </gridpagination:XGridPaginationDropDown>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table width="100%" style="background-color: white">
                                                    <tr>
                                                        <td >
                                                            <xgrid:XGridViewControl ID="grdCaseStatusList" runat="server" CssClass="mGrid" DataKeyNames="SZ_CASE_STATUS_ID,SZ_STATUS_NAME,I_IS_ARCHIVED"
                                                                MouseOverColor="0, 153, 153" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" XGridKey="Bill_Sys_Case_Status_New"
                                                                PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true" AutoGenerateColumns="false"
                                                                GridLines="None" OnSelectedIndexChanged="grdCaseStatusList_SelectedIndexChanged"
                                                                OnRowCommand="grdCaseStatusList_RowCommand" DoNotBindGrid="False" ShowExcelTableBorder="True" Width="500">
                                                                <Columns>
                                                                    <asp:BoundField headertext="Status Id" DataField="SZ_CASE_STATUS_ID" Visible="False">
                                                                        <headerstyle horizontalalign="Left" />
                                                                        <itemstyle horizontalalign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Status Name" >
                                                                        <itemtemplate>
 								                            	            <asp:LinkButton  ID ="lnkscan" runat ="server" CausesValidation="false" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="ViewReceivedPOM" Text= '<%# DataBinder.Eval(Container, "DataItem.SZ_STATUS_NAME")%>' ></asp:LinkButton>
                                                                        
                                                                    </itemtemplate>
                                                                        <headerstyle horizontalalign="Left"></headerstyle>
                                                                        <itemstyle horizontalalign="Left" width="20%"></itemstyle>
                                                                     </asp:TemplateField>
                                                                    <asp:BoundField headertext="isArchived" DataField="I_IS_ARCHIVED" Visible="False">
                                                                        <headerstyle horizontalalign="Left" />
                                                                        <itemstyle horizontalalign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <itemtemplate>
                                                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                       
                                                                       
                                                                    </itemtemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle BackColor="#EEEEEE" />
                                                            </xgrid:XGridViewControl>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="RightCenter" style="width: 10px;">
                                </td>
                            </tr>
                           <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 280px; background-color: #B5DF82" id="divfrmPatient" >
                <div style="position: relative; background-color: #B5DF82; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
                </div>
                <iframe id="frmpatient" src="" frameborder="0" width="500" height="380"></iframe>
            </div>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <asp:TextBox ID="txtddlStatus" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseStatus" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
