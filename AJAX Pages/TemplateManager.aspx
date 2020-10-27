<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TemplateManager.aspx.cs" Inherits="AJAX_Pages_TemplateManager" Title="Template Manager" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
     function OpenFile()
     {
   
            var bname = navigator.appName; 
	        if(bname == "Netscape")
	        {
               alert('Template manager requires Internet Explorer or another browser which supports ActiveX controls.')
               return false;
            }            
            var listbox=document.getElementById("<%=lstTemplates.ClientID%>");
            var count = 0;            
            for (var i = 0; i < listbox.options.length;i++)
            {
                if(listbox.options[i].selected)
                {                         
                    window.open('../tm/RTFEditter.aspx? path = ' + listbox.options[i].value, 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=1,status=no,scrollbars=0'); 
                    count = count + 1;
                }
            }
            if(count == 0)
            {
                alert('select one template from list.');
            }
            return true;
     }    
    </script>

    <table class="ContentTable" style="height: 100%; width: 100%;">
        <tr>
            <td style="height: 5%; width: 100%;" class="TDPart" align="left">
                <asp:Label runat="server" ID="lblPageName" Text="Template Manager" CssClass="ContentLabel"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblError" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 5%; width: 100%;" class="TDPart" align="center">
                <asp:UpdatePanel ID="UpdatePanelUserMessage" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 90%; width: 100%;" class="TDPart" align="left">
                <table>
                    <tr style="height: 100%; width: 100%;" valign="top">
                        <td style="height: 100%; width: 30%;" align="left">
                            Templates
                            <br />
                            <asp:ListBox ID="lstTemplates" AutoPostBack="true" runat="server" Height="300px" Width="250px" OnSelectedIndexChanged="lstTemplates_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td style="height: 100%; width: 70%;" align="left">
                            &nbsp;</td>
                    </tr>
                    <tr style="height: 100%; width: 100%;">
                        <td style="height: 21%; width: 30%;" align="center">
                            <asp:UpdatePanel ID="update" runat="server">
                                <ContentTemplate>
                                        <asp:Button runat="server" Text="Open Template" OnClientClick="return OpenFile();"
                                ID="btnOpenTemplate" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="height: 21%; width: 70%;" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
