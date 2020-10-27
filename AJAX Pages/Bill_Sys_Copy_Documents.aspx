<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Copy_Documents.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Copy_Documents" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function CheckSelect() {
            //debugger;
            var inputArray = document.getElementById('tvwmenu').getElementsByTagName('input');
            hdn = document.getElementById('hdn');
            hfselectedNode = document.getElementById('hfselectedNode');
           var flag=0;   
            for (i = 0; i < inputArray.length; i++) {
                if (inputArray[i].checked)
                 {
                   flag=1;
                    var str = inputArray[i].title.split("(");
                    var path = str[1];
                    var node = path.split(")");
                    var nod_id = node[0];
                    hfselectedNode.value = nod_id;
                    if (hdn.value != '') 
                    {
                        obj = document.getElementById(hdn.value);
                        obj.checked = false;
                        hdn.value = '';
                        hdn.value = inputArray[i].id;
                    }
                    else
                    {
                        hdn.value = '';
                        hdn.value = inputArray[i].id;
                    }
                    
                }
               
            }
            
            if(flag==0)
            {
              hfselectedNode.value='0';
            }
        }

        function SelectAllDiagnosis(ival) {
            var f = document.getElementById('grdDocuments');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++)
             {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") 
                {
                    if (f.getElementsByTagName("input").item(i).disabled == false)
                    {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function callforSearch() 
        {
            document.getElementById('hdnSearch').value = 'true';
             
        }
  
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <UserMessage:MessageControl ID="usrMessage1" runat="server"></UserMessage:MessageControl>
                    </td>
                </tr>
                <tr>
                    <td style="width: 75%" valign="top">
                        <dx:ASPxGridView ID="grdDocuments" ClientInstanceName="grdDocuments" runat="server"
                            Width="100%" KeyFieldName="NodeID" SettingsBehavior-AllowSort="true" SettingsPager-PageSize="330"
                            Settings-VerticalScrollableHeight="450" AutoGenerateColumns="False" SettingsPager-Visible="false">
                            <Columns>
                                <dx:GridViewDataColumn Caption="chk" Width="30px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllDiagnosis(this.checked);"
                                            ToolTip="Select All" />
                                    </HeaderTemplate>
                                    <DataItemTemplate>
                                        <asp:CheckBox ID="chkall" runat="server" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="NodeID" Caption="NodeID" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="NodeName" Caption="Node Name" HeaderStyle-HorizontalAlign="Center"
                                    CellStyle-HorizontalAlign="Left" Width="120px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Filename" Caption="File Name" HeaderStyle-HorizontalAlign="Center"
                                    CellStyle-HorizontalAlign="Left" Width="350px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FilePath" Caption="FilePath" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false" />
                            </Columns>
                            <Settings ShowVerticalScrollBar="true" ShowFilterRow="true" />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <SettingsBehavior AllowSelectByRowClick="true" />
                            <Styles>
                                <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                </FocusedRow>
                                <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                </AlternatingRow>
                            </Styles>
                        </dx:ASPxGridView>
                    </td>
                    <td>
                        <asp:Panel runat="server" ScrollBars="Both" Height="450px" Width="100%" ID="contentPanel">
                            <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="450px" Width="100%"
                                ID="tvwmenu" OnTreeNodePopulate="Node_Populate">
                                <Nodes>
                                    <asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager"
                                        Value="0"></asp:TreeNode>
                                </Nodes>
                            </asp:TreeView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <dx:ASPxButton ID="btnCopyDoc" runat="server" Text="Copy Document(s)" OnClick="btnCopyDoc_Click"
                            ValidationGroup="a" CausesValidation="true" Width="20%">
                            <ClientSideEvents Click="function(s, e) {LoadingPanel.Show();}" />
                        </dx:ASPxButton>
                        <%--<asp:Button ID="btnCopyDoc" runat="server" Text="Copy Document(s)" OnClick="btnCopyDoc_Click" />--%>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtUserName" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtCompanyIDCopy" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtNewCaseID" runat="server" Visible="false"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hfselectedNode"></asp:HiddenField>
            <input type="hidden" id="hdn" runat="server" />
            <asp:HiddenField ID="hdnSearch" runat="server" />
        </div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
    </form>
</body>
</html>
