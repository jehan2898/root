<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ViewDashboard.aspx.cs" Inherits="ViewDashboard" %>
<%@ Register Assembly="Kalitte.Dashboard.Framework" Namespace="Kalitte.Dashboard.Framework"
    TagPrefix="kalitte" %>
    
   <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <kalitte:ScriptManager ID="ScriptManager2" runat="server">
        </kalitte:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <kalitte:WidgetTypeList ID="WidgetTypeList1" SurfaceInstance="DashboardSurface1" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel>
    <kalitte:DashboardSurface ID="DashboardSurface1" runat="server" Scope="Container" />
    
</asp:Content>