<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CO_Signs.aspx.cs"
    Inherits="Bill_Sys_CO_Signs" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cc1" Namespace="RealSignature" Assembly="WebSignature" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlPatientSign" runat="server">
                        <div>
                            <table>
                                <tr>
                                    <td width="50%">
                                        Patient's Signature
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:WebSignature ID="WebSignature1" runat="server">
                                        </cc1:WebSignature>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <asp:Button Text="btnGetDoctor's Sign" runat="server" ID="btnGetDoctorSign" OnClick="btnGetDoctorSign_Click" />
                                </tr>--%>
                            </table>
                        </div>
                    </asp:Panel>
                    <%--<asp:Panel ID="pnlDoctorSign" runat="server" Visible="false" >
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
                                
                            </table>
                        </div>
                    </asp:Panel>--%>
                </td>
                <td>
                    <%-- <ajaxToolkit:TabContainer ID="tabSignature" runat="server">
                        <ajaxToolkit:TabPanel ID="tabPatientSign" runat="server" HeaderText="Sample" TabIndex="0">
                            <HeaderTemplate>
                                <asp:Label ID="lblHeadOne" runat="server" Text="Patient Sign" class="lbl"></asp:Label>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td width="50%">
                                            <cc1:WebSignature ID="WebSignature1" runat="server">
                                            </cc1:WebSignature>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabDoctorSign" runat="server" HeaderText="Sample" TabIndex="1">
                            <HeaderTemplate>
                                <asp:Label ID="lblHeadtwo" runat="server" Text="Doctor Sign" class="lbl"></asp:Label>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td width="50%">
                                            <cc1:WebSignature ID="WebSignature2" runat="server">
                                            </cc1:WebSignature>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>--%>
                </td>
                <%-- <td width="50%">
            Patient's Signature
         </td>
        <td width="50%">
            Doctor's Signature
        </td>--%>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSumbit" runat="server" Text="Sumbit" OnClick="btnSumbit_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
