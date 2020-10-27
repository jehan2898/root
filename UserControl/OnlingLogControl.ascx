<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnlingLogControl.ascx.cs" Inherits="UserControl_OnlingLogControl" %>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            text-align:right;
        }
    </style>
    <script type="text/javascript">
        function ShowOnlineLog() {
           
           
            var url = "AJAX Pages/OnlineLogDetails.aspx";
           
                OnlineLog.SetContentUrl(url);
                OnlineLog.Show();
            }
    </script>

    <table class="auto-style1">
        <tr>
            <td style="text-align:right;">
                <a onclick="ShowOnlineLog(); return false;" style="font-family:Arial;font-size:12px;color:blue;text-decoration:none;cursor:pointer;">
                    [Log Viewer]
                </a>
            </td>
        </tr>
    </table>

    <dx:ASPxPopupControl 
        ID="OnlineLog" 
        runat="server" 
        CloseAction="CloseButton"  
        Modal="true"
        HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="OnlineLog" HeaderText="Online Logger" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Font-Bold="true" AllowDragging="True" EnableAnimation="False" EnableViewState="True"
        Width="600px" 
        ToolTip="Online Logger" 
        PopupHorizontalOffset="0" 
        PopupVerticalOffset="0"
        AutoUpdatePosition="true" 
        RenderIFrameForPopupElements="Default"
        Height="500px" EnableHierarchyRecreation="True">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>          
    </dx:ASPxPopupControl>
