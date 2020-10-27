<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDelRole.aspx.cs" Inherits="Security_frmDelRole" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<LINK href="../CssAndJs/css.css" type="text/css" rel="stylesheet"/>
		<link href="../CssAndJs/Main.css" type="text/css" rel="stylesheet"/>
		<link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />
		<link href='<%=Page.ResolveUrl("~/CssAndJs/DemoStyles.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/Main.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/AxpStyleXPGrid3.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/LinkSelector.css")%>' type="text/css" rel="stylesheet">
		
</head>
<body leftMargin="0" topMargin="0">
		
		<form id="Form2" method="post" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
              <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
            <td height="27" bgcolor="#E9E9E9" colspan="3"><span class="black11arial">Home - Manage Roles</span></td>
          </tr>
          <tr>
            <td colspan="3">&nbsp;</td>
          </tr>
          <tr >
              <td width="20%"></td>
              <td  class="defaultText">
                  <table class="grid" width="100%">
				    <tr  id="rowRoleNameDropDown" runat="server" >
					    <td>RoleName</td>
                        <td><asp:dropdownlist id="ddlRoleName" runat="server" CssClass="combox" Width="250px" Height="24px"></asp:dropdownlist>&nbsp;</td>
					    <td>&nbsp;<asp:RequiredFieldValidator ID="RFVddlRole" runat="server" ControlToValidate="ddlRoleName"
                                CssClass="Validatecontroltxt" ErrorMessage="*" InitialValue="Select"></asp:RequiredFieldValidator>
                        </td>
				    </tr>
				    <tr id ="rowRoleNameText" runat="server">
					    <td>Role Name <A style="COLOR: #ff3333">*</A></td>
                        <td><asp:textbox id="txtRoleName" runat="server" CssClass="box" Width="250px" Visible="False" MaxLength="30"></asp:textbox></td>
                        <td><asp:RequiredFieldValidator ID="RFVRoleNameText" runat="server" ControlToValidate="txtRoleName" CssClass="Validatecontroltxt" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:regularexpressionvalidator id="REVRoleNameText" runat="server" CssClass="Validatecontroltxt" Width="136px" ControlToValidate="txtRoleName" Display="Dynamic" ValidationExpression="[a-z A-Z 0-9 ]*[a-z A-Z 0-9]" ErrorMessage="Role name is not valid"></asp:regularexpressionvalidator>
                        </td>
				    </tr>
                    <tr id="Tr1" runat="server">
                        <td>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                        <td>&nbsp; &nbsp; &nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:button id="btnEdit" accessKey="s" runat="server" CssClass="button_text" Width="60px" ToolTip="Alt + s" Text="Edit" OnClick="btnEdit_Click" ></asp:button>&nbsp;
                            <asp:button id="btnDelete" accessKey="l" runat="server" CssClass="button_text" Width="72px" ToolTip="Alt + l" Text="Delete" OnClick="btnDelete_Click" ></asp:button>&nbsp;
                            <asp:button id="btnClose" accessKey="c" runat="server" CssClass="button_text" Width="60px" ToolTip="Alt + c" Text="Close" CausesValidation="False" OnClick="btnClose_Click" ></asp:button>
                        </td>
                        <td></td>
                    </tr>
			        </table >
                </td>
              <td width="20%"></td>
          </tr>
          </table>
		</form>
	</body>
</html>
