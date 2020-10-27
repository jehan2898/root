<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AddUnBilledReason.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_AddUnBilledReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Reason</title>

    <script type="text/javascript">
     function Clear()
       {
              
       document.getElementById("<%=txtAddReason.ClientID %>").value ='';
       document.getElementById("<%=chkReason.ClientID%>").checked=false;
       }
    function chkcompanyname()
    {
           
           var headerchk = document.getElementsByName('chkReason');
          
            if(document.getElementById('chkReason').checked==true)
            {
               document.getElementById('txtAddReason').disabled = false;
              
            }
            else if(document.getElementById('chkReason').checked==false)
            {
              document.getElementById('txtAddReason').disabled = true;
              document.getElementById('txtAddReason').value="";
            }
    }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <UserMessage:MessageControl runat="server" ID="usrMessage" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td height="28" align="left" class="txt2" style="width: 100%" colspan="3">
                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="pnlprogress"
                        DisplayAfter="10" DynamicLayout="true">
                        <ProgressTemplate>
                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                runat="Server">
                                <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                    Height="25px" Width="24px"></asp:Image>
                                Loading...</div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReason" Text="Reason" runat="server"></asp:Label>
                </td>
                <td width="80%" height="50%">
                    <asp:TextBox ID="txtAddReason" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:CheckBox ID="chkReason" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:UpdatePanel ID="pnlprogress" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnUpdate" Text="Update" runat="server" OnClick="btnUpdate_Click" />
                            <input style="width: 80px" id="btnClear" onclick="Clear();" type="button" value="Clear"
                                runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
