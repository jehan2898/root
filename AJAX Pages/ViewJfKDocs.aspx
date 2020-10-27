<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewJfKDocs.aspx.cs" Inherits="AJAX_Pages_ViewJfKDocs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function OpenDocumentManager(Path) {
            window.open(Path, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
        }

        function SelectAll(ival) {
            var f = document.getElementById('grdDocs');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {




                    f.getElementsByTagName("input").item(i).checked = ival;




                }


            }
        }

        function confirm_delete() {
            var f = document.getElementById('grdDocs');
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {

                            bfFlag = true;
                        }
                    }
                }
            }
            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }



            if (confirm("Are you sure want to Delete?") == true) {

                return true;
            }
            else {
                return false;
            }
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%;" border="0">
                            <tbody>
                                <tr>
                                    <td align="left">
                                    </td>
                                    <td align="right" colspan="3">
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="4">
                                <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Red" Font-Italic="true" ></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                </tr>
                            </tbody>
                        </table>
                        <div style="height: 450px; overflow: scroll;">
                            <asp:DataGrid Width="100%" ID="grdDocs" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
                                <AlternatingItemStyle BackColor="#EEEEEE"></AlternatingItemStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="I_IMAGE_ID" HeaderText="I_IMAGE_ID" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="left" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="left" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="left" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="i_visit_id" HeaderText="i_visit_id" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="left" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="dt_uploded_date" HeaderText="Uploaded Date" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="left" Visible="true">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                         <asp:TemplateColumn>
                                         <ItemTemplate>
                                           <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "OpenDocumentManager(" + "\""+ Eval("File_Path") +"\");" %>' >'<%# DataBinder.Eval(Container, "DataItem.File_Name")%>'</a>
                                         </ItemTemplate>
                                         </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                ToolTip="Select All" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkDelete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle BackColor="#b5df82" Font-Bold="true" />
                            </asp:DataGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtvisitId" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
