<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ticket.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .textbox{
            font-family: Arial;
            font-color: #808080;
            font-size: 12px;
            border: 1px solid;
        }
        
        .label{
            font-family: Arial;
            font-color: #808080;
            font-size: 12px;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div align ="center">
        
        <table id="tblTicket" runat="server" style="width: 489px; height: 309px;vertical-align:top;">
            
            <tr>
            
            <td style="width: 164px">
            
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Height="23px" Width="250px" BorderColor="Black"></asp:Label></td>
            </tr>
            
            <tr>
                <td style="width: 164px">
                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket #" CssClass="label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="textbox" ></asp:TextBox>
                </td>
            </tr>
        <tr>       
            <td style="width: 164px">
                <asp:Label ID="lblName" runat="server" Text="Name" Width="40px" CssClass="label"></asp:Label>
            </td>
        
            <td style="width: 341px">
                <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameValidator" runat="server" ControlToValidate="txtName">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td style="width: 164px">
                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="label"></asp:Label>
            </td>
            <td style="width: 341px">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Please enter valid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td style="width: 164px">
                <asp:Label ID="lblSupport" runat="server" Text="Support" CssClass="label"></asp:Label>
            </td>
            
            <td style="width: 341px">
                <asp:DropDownList ID="ddlSupport" runat="server" CssClass="textbox">
                    <asp:ListItem>&lt;--- Select ---&gt;</asp:ListItem>
                    <asp:ListItem Value="1">Technical Support</asp:ListItem>
                    <asp:ListItem Value="2">Application query</asp:ListItem>
                    <asp:ListItem Value="3">Feature request</asp:ListItem>
                    <asp:ListItem Value="4">Raise a bug</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
        <td style="width: 164px; height: 84px;">
            <asp:Label ID="lblComments" runat="server" Text="Comments" CssClass="label"></asp:Label>
        </td>
        
        <td style="width: 341px; height: 84px;">
            <asp:TextBox ID="txtAreaComments" runat="server" MaxLength="4000" TextMode="MultiLine" CssClass="textbox" Width="200px" Height="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAreaComments">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 164px; height: 42px;">
                &nbsp;
            </td>
            <td style="width: 486px; height: 42px">
                <asp:Button ID="btnUpdate" runat="server" Text="Submit" Width="86px" OnClick="btnUpdate_Click1"></asp:Button>&nbsp;
            </td>
        </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" BorderColor="Black" ForeColor="Black">
            <table id ="tblUpload" align="center" style="width: 489px; height: 309px">
          <tr>
          <td style="width: 671px; height: 20px;">
         <asp:Label ID="lblUpload" runat="server" Text="Upload File" Width="87px" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
          </td>
        </tr>
        <tr>
            <td style="width: 671px; height: 40px;">
                <asp:FileUpload ID="FileUploadControl"  runat="server" Height="27px" Width="267px"></asp:FileUpload>
                <asp:Button ID="btnupload" runat="server" Height="25px" OnClick="btnupload_Click" Text="Upload" Width="60px" />&nbsp;
            </td>
        </tr>
        <tr>
        
      <td style="width: 671px">
            <asp:GridView ID="GridView1" runat="server" Height="120px" Width="481px" AutoGenerateDeleteButton="True" AutoGenerateColumns="False" DataKeyNames="sz_file_path" DataSourceID="SqlDataSource1" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="trn_Ticket_No" HeaderText="No" InsertVisible="False"
                        ReadOnly="True" SortExpression="trn_Ticket_No" />
                    <asp:BoundField DataField="sz_ticket_id" HeaderText="Ticket ID" SortExpression="sz_ticket_id" />
                    <asp:TemplateField HeaderText="File Name" SortExpression="sz_file_path">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("sz_file_path") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("sz_file_path") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
                ConnectionString="Data Source=SERVER\SQLEXPRESS;Initial Catalog=BillingSystem;User ID=sa;Password=oct2005" DeleteCommand="DELETE FROM [trn_support_ticket] WHERE [trn_Ticket_No] = @original_trn_Ticket_No AND [sz_ticket_id] = @original_sz_ticket_id AND [sz_file_path] = @original_sz_file_path"
                InsertCommand="INSERT INTO [trn_support_ticket] ([sz_ticket_id], [sz_file_path]) VALUES (@sz_ticket_id, @sz_file_path)"
                OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [trn_support_ticket] WHERE ([sz_ticket_id] = @sz_ticket_id)"
                UpdateCommand="UPDATE [trn_support_ticket] SET [sz_ticket_id] = @sz_ticket_id, [sz_file_path] = @sz_file_path WHERE [trn_Ticket_No] = @original_trn_Ticket_No AND [sz_ticket_id] = @original_sz_ticket_id AND [sz_file_path] = @original_sz_file_path" ProviderName="System.Data.SqlClient">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtTicketNo" Name="sz_ticket_id" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="original_trn_Ticket_No" Type="Int32" />
                    <asp:Parameter Name="original_sz_ticket_id" Type="String" />
                    <asp:Parameter Name="original_sz_file_path" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="sz_ticket_id" Type="String" />
                    <asp:Parameter Name="sz_file_path" Type="String" />
                    <asp:Parameter Name="original_trn_Ticket_No" Type="Int32" />
                    <asp:Parameter Name="original_sz_ticket_id" Type="String" />
                    <asp:Parameter Name="original_sz_file_path" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="sz_ticket_id" Type="String" />
                    <asp:Parameter Name="sz_file_path" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            &nbsp;
            </td>
        </tr>
        </table>
        </asp:Panel>       
        </div>
    </form>        
</body>
</html>