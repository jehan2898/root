<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckPageAutharization.ascx.cs"
    Inherits="UserControl_CheckPageAutharization" %>
<asp:Panel ID="pnlUserControl" runat="server" DefaultButton="btnGo">
<table width="80%" style="font-family: Tahoma;font-size: 11px;font-weight: bold;color: #000000;margin-bottom:10px;">
    <tr>
    <td width="100%" align="left" colspan="2" >
           Account : <% =((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME  %>
        </td>
   </tr>
   <tr>     
        <td width="100%" align="left" colspan="2" >
           User : <%=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME %><br/>
            <asp:LinkButton ID="lnkCaseID" runat="server" visible="false" OnClick="lnkCaseID_Click"></asp:LinkButton>
            <asp:HyperLink ID="test" runat="Server" Visible="false"></asp:HyperLink>
             <asp:TextBox ID="txtCaseSearch" runat="server" Width="140px"></asp:TextBox>
           <asp:Button ID="btnGo" runat="server" Text="Go" Width="25px" CssClass="Buttons" OnClick="btnGo_Click" />
        </td>
    </tr>
   
</table>
</asp:Panel>    
