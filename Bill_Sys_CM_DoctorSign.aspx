<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CM_DoctorSign.aspx.cs" Inherits="Bill_Sys_CM_DoctorSign" %>
<%@ Register TagPrefix="cc1" Namespace="RealSignature" Assembly="WebSignature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    
</head>
<body>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlDoctorSign" runat="server">
                        <div>
                            <table>
                                <tr>
                                    <td width="50%">
                                        Doctor's Signature
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
    </div>
    </form>
</body>
</html>
