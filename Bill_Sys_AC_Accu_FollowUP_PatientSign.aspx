<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AC_Accu_FollowUP_PatientSign.aspx.cs" Inherits="Accu_FollowUP_DoctorSign" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="cc1" Namespace="RealSignature" Assembly="WebSignature" %>


<html>
<head id="Head1" runat="server">
   <title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body>
     <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlDoctorSign" runat="server">
                        <div>
                            <table>
                                <tr>
                                    <td width="50%">
                                        Patient's Signature
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <cc1:WebSignature ID="WebSignature2" runat="server">
                                        </cc1:WebSignature>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSumbit" runat="server" Text="Sumbit" OnClick="btnSumbit_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Image ID="picBarcode" runat="server" Visible = "false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
