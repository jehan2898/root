<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_User_With_IP_Address.aspx.cs" Inherits="AJAX_Pages_Add_User_With_IP_Address" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Registration/validation.js"></script>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        function fncLocal() {
            parent.location.reload();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<div style="overflow-y: scroll">--%>
            <table style="width:100%">
                <tr style="width:100%">
                    <td style="width:100%">
                    <div style="overflow-y:scroll;height:110px;">
                        <asp:GridView ID="grd_User_List" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SZ_USER_ID,SZ_USER_NAME"
                            Width="100%" Height="200px" >
                            <Columns>
                                <asp:BoundField DataField="SZ_USER_NAME" HeaderText="User Name" />
                                <asp:BoundField DataField="SZ_USER_ID" Visible="false"/>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkVisit" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#990000" ForeColor="White" />
                            <SelectedRowStyle BackColor="#FFCC66" ForeColor="Navy" />
                            <HeaderStyle BackColor="#DCDCDC" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr style="width:100%">
                <td style="width:100%;text-align:center">
                    <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click"/>
                </td></tr>
            </table>
            <table>
            <tr>
            <td>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
            </td></tr></table>
        <%--</div>--%>
        
    </div>
    </form>
</body>
</html>
