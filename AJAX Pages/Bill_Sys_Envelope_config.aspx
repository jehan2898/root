<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Envelope_config.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Envelope_config"
    Title="Envelope Configuration" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">
    function chkcompanyname()
    {
           var headerchk = document.getElementsByName("<%=chkcompname.ClientID%>");
          
            if(document.getElementById("<%=chkcompname.ClientID%>").checked==true)
            {
               document.getElementById("<%=txtcompanyname.ClientID%>").disabled = true;
               document.getElementById("<%=txtcompanyname.ClientID%>").value="";
            }
            else if(document.getElementById("<%=chkcompname.ClientID%>").checked==false)
            {
              document.getElementById("<%=txtcompanyname.ClientID%>").disabled = false;
            }
    }
    </script>

    <table width="46%" style="background-color: white" border="0">
        <tr>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            <table style="width: 100%; border-bottom: #b5df82 1px solid; border-right: #b5df82 1px solid;
                                border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;">
                                <tr>
                                    <td style="background-color: #b4dd82; height: 15%; font-weight: bold; font-size: small">
                                        &nbsp;Envelope Configuration
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upmsg" runat="server">
                                            <contenttemplate>
                                               <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                            </contenttemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="width: 32%;">
                                                    <b>Company Name</b>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="chkcompname" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100%">
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td style="width: 32%;">
                                                                <b>Envelope Display Name</b>
                                                            </td>
                                                            <td style="width: 68%;">
                                                                <asp:TextBox ID="txtcompanyname" runat="server" Width="96%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 32%;">
                                                    <b>Company Address</b>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="chkaddress" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="width: 100%;">
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td style="width: 32.8%;">
                                                                <b>Address Street</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkaddstreet" runat="server" />
                                                            </td>
                                                            <td>
                                                                <b>City</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkcity" runat="server" />
                                                            </td>
                                                            <td>
                                                                <b>State </b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkstate" runat="server" />
                                                            </td>
                                                            <td>
                                                                <b>Zip</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="Chkzip" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnsave" runat="server" Text="Add" Width="15%" OnClick="btnsave_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="28" align="left" class="txt2" style="width: 100%">
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                            DisplayAfter="10" DynamicLayout="true">
                                            <progresstemplate>
                                 <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                         Height="25px" Width="24px"></asp:Image>
                                     Loading...</div>
                             </progresstemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hdnenvelopedispname" runat="server" />
</asp:Content>
