<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bills_Sys_UploadDocPopup.aspx.cs"
    Inherits="Bills_Sys_UploadDocPopup" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
    <link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />
    <script type="text/javascript">

        function CheckUploadFile() {
            var DocType = document.getElementById('carTabPage_extDocType').value;
            if (DocType == 'NA') {
                alert('Please Select FileType to upload');
                return false;

            } else {
                var DocPath = document.getElementById('carTabPage_fuUploadReport').value;
                if (DocPath == '') {
                    alert('Please Select File to upload');
                    return false;
                }
                else {
                    return true;
                }
            }

        }
        function SelectAll(ival) {

            var f = document.getElementById('carTabPage_grdViewDocuments');
            //alert("<%# grdViewDocuments.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function confirm_delete_document() {
            var f = document.getElementById("carTabPage_grdViewDocuments");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {
                        if (confirm("Are you sure to continue for delete?"))
                            return true;
                        return false;
                    }
                }
            }
            alert('Please select docuemnt');
            return false;
        }



        function showPateintFrame(filename, url) {


            window.open(url);

        }
    </script>
    <link href="AJAX Pages/Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td style="width: 100%">
                    <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                        Width="100%" Height="100%" AutoPostBack="true">
                        <TabPages>
                            <dx:TabPage Text="Upload" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0">
                                <%--   <ActiveTabStyle BackColor="White" Font-Bold="True">
                                </ActiveTabStyle>--%>
                                <TabStyle Width="100%" BackColor="#B1BEE0">
                                </TabStyle>
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <asp:panel id="pnl_CaseDetails1" runat="server" width="100%">
                                            <table style="width: 100%" class="ContentTable" border="0">
                                                <tr>
                                                    <td>
                                                        Document :
                                                    </td>
                                                    <td>
                                                        <cc1:ExtendedDropDownList ID="extDocType" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_GET_DOC_TYPE_TO_UPLOAD" Selected_Text="---Select---" Width="140px"
                                                            AutoPost_back="true"></cc1:ExtendedDropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Upload File :
                                                    </td>
                                                    <td>
                                                        <asp:fileupload id="fuUploadReport" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:button id="btnUploadFile" runat="server" text="Upload File" onclick="btnUploadFile_Click" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:panel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Delete" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0">
                                <TabStyle Width="100%" BackColor="#B1BEE0">
                                </TabStyle>
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <%--<div>--%>
                                        <asp:datagrid id="grdViewDocuments" width="100%" runat="Server" autogeneratecolumns="False"
                                            cssclass="GridTable">
                                            <headerstyle cssclass="GridHeader1" />
                                            <itemstyle cssclass="GridViewHeader" />
                                            <columns>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                    ToolTip="Select All" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="File Name" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                  <a id="A1" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("sz_file_name") + "\""+ ",\"" + Eval("URL")  +"\");" %>' ><%# DataBinder.Eval(Container,"DataItem.sz_file_name")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="sz_file_path" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="sz_file_name" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="i_image_id" HeaderText="i_image_id" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DOC_Type" HeaderText="Document Type" Visible="true"></asp:BoundColumn>
                                    </columns>
                                        </asp:datagrid>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:button id="btnDelete" runat="server" text="Delete File" onclick="btnDelete_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                                <%-- </div>--%>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:textbox id="txtCompanyID" runat="server" visible="False" width="10px" />
                    <asp:textbox id="txtCaseID" runat="server" visible="False" width="10px" />
                    <asp:textbox id="txtPGID" runat="server" visible="False" width="10px" />
                    <asp:textbox id="txtPname" runat="server" visible="False" width="10px" />
                    <asp:textbox id="txtDocID" runat="server" visible="False" width="10px" />
                    <asp:textbox id="txtEID" runat="server" visible="False" width="10px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
