<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Assign_Attorny.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Assign_Attorny"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Src="~/UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
     function Clear()
       {
        document.getElementById("ctl00_ContentPlaceHolder1_extddlAttorney").value = 'NA';
        document.getElementById("ctl00_ContentPlaceHolder1_extddlAssignAttorny").value = 'NA';
       }
        function checkvalue()
        {
            if(document.getElementById("ctl00_ContentPlaceHolder1_extddlAttorney").value=='NA' )
		  {
			alert('Please Select Attorney');
			return false;
		  }
         if( document.getElementById("ctl00_ContentPlaceHolder1_extddlAssignAttorny").value=='NA')

		  {

			alert('Please Select Assign User Value');
			return false;
		  }
        }
    </script>

    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;">
        <tr>
            <td align="left">
                <table width="35%" style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82;
                    border-bottom: 1px solid #B5DF82">
                    <tr>
                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                            <b class="txt3">Search Parameters</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <b>Attorney</b>
                                    </td>
                                    <td>
                                        <b>Assign To </b>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <extddl:ExtendedDropDownList ID="extddlAttorney" Width="100%" runat="server" Connection_Key="Connection_String"
                                            Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                            OldText="" StausText="False" />
                                    </td>
                                    <td>
                                        <extddl:ExtendedDropDownList ID="extddlAssignAttorny" runat="server" Width="100%"
                                            Selected_Text="---Select---" Procedure_Name="SP_MST_ATTORNEY_USER" Flag_Key_Value="ATTORNEY_LIST_USER"
                                            Connection_Key="Connection_String" CssClass="search-input"></extddl:ExtendedDropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Width="80px" Text="Save">
                                    </asp:Button>
                                    <input style="width: 80px" id="btnClear1" onclick="Clear();" type="button" value="Clear"
                                        runat="server" />
                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Width="80px"
                                        Text="Update"></asp:Button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UP_grdAttornyUser" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                    <b class="txt3">Attorney Users</b>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdAttornyUser"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                 <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                         Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                             </progresstemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                 <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                         Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                             </progresstemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <table style="vertical-align: middle; width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: middle; width: 30%" align="left">
                                        Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                            CssClass="search-input">
                                        </gridsearch:XGridSearchTextBox>
                                        <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                    </td>
                                    <td style="width: 60%" align="right">
                                        Record Count:
                                        <%= this.grdAttornyUser.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="btnExportToExcel" runat="server" Text="Export TO Excel" OnClick="btnExportToExcel_onclick">
                                 <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdAttornyUser" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="" MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                        ExportToExcelFields="SZ_USER_ID,SZ_USER_NAME,SZ_ATTORNY_ID,SZ_ATTORNEY_NAME,ID"
                                        ShowExcelTableBorder="true" ExportToExcelColumnNames="User ID,User Name,Attorny ID,Attorny Name,ID"
                                        AllowPaging="true" XGridKey="Get_Attory_User" PageRowCount="50" PagerStyle-CssClass="pgr"
                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:TemplateField Visible="false">
                                                <itemtemplate>
                                                <asp:LinkButton ID="lnkSelectattuser" runat="server" Text="Select" Visible="false" ></asp:LinkButton>
                                             </itemtemplate>
                                            </asp:TemplateField>
                                            <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="User ID" DataField="SZ_USER_ID" Visible="false" />
                                            <%--2--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="User Name" DataField="SZ_USER_NAME" />
                                            <%--3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Attorney ID" DataField="SZ_ATTORNY_ID" visible="false" />
                                            <%--4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="Attorney Name" DataField="SZ_ATTORNEY_NAME" />
                                            <%--5--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                headertext="ID" DataField="ID" visible="false" />
                                        </Columns>
                                    </xgrid:XGridViewControl>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="con" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
