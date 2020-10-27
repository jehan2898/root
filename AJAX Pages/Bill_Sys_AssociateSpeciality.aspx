<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_AssociateSpeciality.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_AssociateSpeciality"
    Title="Untitled Page" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
       function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdAssociateSpec.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
    </script>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="40%" style="border: 1px solid #B5DF82;">
        <tr>
            <td colspan="2" style="background-color: #B5DF82; height: 40;" class="txt2">
                <b class="txt3">Associate Speciality</b></td>
        </tr>
        <tr>
            <td class="td-widget-bc-search-desc-ch1">
                Speciality</td>
            <td class="td-widget-bc-search-desc-ch1">
                Associate Speciality</td>
        </tr>
        <tr>
            <td valign="top" class="td-widget-bc-search-desc-ch3" align="center">
                <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                    Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                    Selected_Text="---Select---" Width="140px" OnextendDropDown_SelectedIndexChanged="extddlSpeciality_SelectedIndexChanged" AutoPost_back="True">
                    </extddl:ExtendedDropDownList>
            </td>
            <td class="td-widget-bc-search-desc-ch3" align="center">
                <asp:ListBox ID="lstSpeciality" runat="server" Width="180px" Height="130px" SelectionMode="Multiple">
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAssociate" runat="server" Text="Associate" OnClick="btnAssociate_Click" /></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="hdtxtCompanyId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="hdtxtAssSpec" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <div id="divgrd" runat="server" visible="false">
                <table style="vertical-align: middle; width: 100%;">
                    <tbody>
                        <tr>
                            <td style="vertical-align: middle; width: 30%" align="left">
                                Search:
                                <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                    CssClass="search-input"></gridsearch:XGridSearchTextBox>
                            </td>
                            <td style="width: 60%" align="right">
                                Record Count:
                                <%= this.grdAssociateSpec.RecordCount%>
                                | Page Count:
                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                </gridpagination:XGridPaginationDropDown>
                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server" Text="Export TO Excel">
                                <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <xgrid:XGridViewControl ID="grdAssociateSpec" runat="server" Width="100%" CssClass="mGrid"
                                DataKeyNames="Speciality_Id,Associate_Id" MouseOverColor="0, 153, 153"
                                EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields=""
                                ShowExcelTableBorder="true" ExportToExcelColumnNames=""
                                AllowPaging="true" XGridKey="Bill_Sys_AssociatedSpeciality" PageRowCount="50" PagerStyle-CssClass="pgr"
                                AllowSorting="true" AutoGenerateColumns="false" 
                                GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" headertext="Speciality_Id" DataField="Speciality_Id" visible="false" />
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" headertext="Associalted Speciality ID" DataField="Associate_Id" visible="false" />
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" headertext="Speciality" DataField="Speciality" visible="true" />
                                    <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" headertext="Associalted Speciality" DataField="Associate_Speciality" visible="true" />
                                    <asp:TemplateField Visible="true">
                                        <headertemplate>
                                             <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                          </headertemplate>
                                        <itemtemplate>
                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                       </itemtemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </xgrid:XGridViewControl>
                        </td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
