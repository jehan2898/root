<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="ListDashboard.aspx.cs" Inherits="ListDashboard" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Repeater ID="List" runat="server" onitemdatabound="List_ItemDataBound">
    <HeaderTemplate>
    <ul> 
    </HeaderTemplate>
    <ItemTemplate>
    <li>
        <asp:HyperLink Target="_blank" runat="server" ID="link"></asp:HyperLink>
    </li>
    </ItemTemplate>
    <FooterTemplate>
      </ul>
    </FooterTemplate>
    
    </asp:Repeater>
</asp:Content>