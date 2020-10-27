<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightExtraLinks.ascx.cs" Inherits="RightExtraLinks" %>

    <a href="#" class="toplinktxt">Privacy Policy</a> | 
    <a href="#" class="toplinktxt">Security</a> | 
    <a href="javascript:OnSupportNewClick()" class="toplinktxt">New Ticket</a> | 
    <a href="javascript:OnSupportViewClick()" class="toplinktxt">View Ticket</a> | 
    <a href="../Bill_Sys_Logout.aspx" class="toplinktxt"><b>Logoff</b></a>

    <script language="javascript" type="text/javascript">
        function OnSupportNewClick() {
            var url = '../NewTicket.aspx';
            IFrame_NewTicket.SetContentUrl(url);
            IFrame_NewTicket.Show();
            return false;
        }
        function OnSupportViewClick() {
            var url = '../ViewTickets.aspx';
            IFrame_ViewTickets.SetContentUrl(url);
            IFrame_ViewTickets.Show();
            return false;
        }
    </script>

    <div>
        <dx:ASPxPopupControl 
            ID="IFrame_NewTicket" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="IFrame_NewTicket" 
            HeaderText="Open New Ticket"
            HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-ForeColor="White"
            HeaderStyle-BackColor="#000000" 
            AllowDragging="True" 
            EnableAnimation="False"
            EnableViewState="True" Width="800px" ToolTip="Open New Ticket" PopupHorizontalOffset="0"
            PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
            RenderIFrameForPopupElements="Default" Height="500px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <dx:ASPxPopupControl 
            ID="IFrame_ViewTickets" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="IFrame_ViewTickets" 
            HeaderText="View Tickets"
            HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-ForeColor="White"
            HeaderStyle-BackColor="#000000" 
            AllowDragging="True" 
            EnableAnimation="False"
            EnableViewState="True" Width="800px" ToolTip="Open New Ticket" PopupHorizontalOffset="0"
            PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
            RenderIFrameForPopupElements="Default" Height="500px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
    </div>