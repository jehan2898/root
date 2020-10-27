<%@ Page Language="c#" AutoEventWireup="true" Inherits="Security.frmRoleActivityMapping" CodeFile="frmRoleActivityMapping.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<%@ Register Assembly="RadTreeView.Net2" Namespace="Telerik.WebControls" TagPrefix="radt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title id=lnkTitle>Assign Privilege To Role</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../CssAndJs/css.css" type="text/css" rel="stylesheet"/>
		<link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />
		<link href='<%=Page.ResolveUrl("~/CssAndJs/DemoStyles.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/Main.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/AxpStyleXPGrid3.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/LinkSelector.css")%>' type="text/css" rel="stylesheet">
		<script language=javascript>

        function UpdateAllChildren(nodes, checked)
        {
         var i;
         for (i=0; i<nodes.length; i++)
         {
          if (checked)
          {
           nodes[i].Check();
           nodes[i].Expand();
           }
          else
          {
           nodes[i].UnCheck();
           nodes[i].Collapse();
          }
          if (nodes[i].Nodes.length > 0)
          {
           UpdateAllChildren(nodes[i].Nodes, checked);
          }
         }
        }
        function AfterCheck(node)
        {
            UpdateAllChildren(node.Nodes, node.Checked);
            if (!node.Checked)
            {
                while(node.Parent != null)
                {
                    node.Parent.UnCheck();
                    node.Collapse();
                    node = node.Parent;
                }
            }
            else
            {
                node.Expand();
            }
        }
        </script>
		
</HEAD>
	<body leftMargin="0" topMargin="0">
		
		<form id="Form2" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                  <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
                <td width="100%" height="27" colspan="3" bgcolor="#E9E9E9" ><span class="black11arial">Home - Role Activity Mapping</span></td>
              </tr>
              <tr>
                <td >&nbsp;</td>
                <td >&nbsp;</td>
                <td >&nbsp;</td>
              </tr>
              <tr>
                <td  width="20%"></td>
                <td class="defaultText">&nbsp;
					<table CssClass="grid" cellSpacing="1" cellPadding="2" width="100%" border="0" >
						<tr  id="rowRoleNameText" runat="server">
							<TD>Role Name <A style="COLOR: #ff3333">*</A></TD>
							<TD><asp:textbox id="txtRoleName" runat="server" CssClass="box" Width="250px" Visible="False" MaxLength="30"></asp:textbox><asp:requiredfieldvalidator id="RFVRoleNameText" runat="server" CssClass="Validatecontroltxt" ControlToValidate="txtRoleName" ErrorMessage="*" InitialValue="Select"></asp:requiredfieldvalidator>
                                <asp:regularexpressionvalidator id="REVRoleNameText" runat="server" CssClass="Validatecontroltxt" Width="136px" ControlToValidate="txtRoleName" Display="Dynamic" ValidationExpression="[a-z A-Z 0-9 ]*[a-z A-Z 0-9]" ErrorMessage="Role name is not valid"></asp:regularexpressionvalidator>
                            </TD>
						</tr>
						<tr id="rowRoleNameDropDown" runat="server" >
							<td >Role Name</td>
							<td><asp:dropdownlist id="ddlRoleName" runat="server" CssClass="combox" Width="250px" AutoPostBack="True" Height="24px" onselectedindexchanged="ddlRoleName_SelectedIndexChanged"></asp:dropdownlist></td>
						</tr>
						<tr>
							<td valign="top">Previleges</td>
							<td><radt:radtreeview id="TvwProductTree" runat="server" autopostback="False" autopostbackoncheck="False" imagesbasedir="~/TreeView/Img/WindowsXP/" checkboxes="True" AfterClientCheck="AfterCheck"></radt:radtreeview>&nbsp;</td>
						</tr>
						<tr>
							<td></td>
							<td></td>
						</tr>
                        <tr>
                            <td>
                            </td>
                            <td>
					    <asp:button id="btnSave" accessKey="s" runat="server" CssClass="button_text" Width="60px" ToolTip="Alt + s"
						Text="Submit" onclick="btnSave_Click"></asp:button>
                                &nbsp;<asp:button id="btnAdd" accessKey="l" runat="server" CssClass="button_text" Width="72px" ToolTip="Alt + l"
						Text="Add Role" onclick="btnAdd_Click"></asp:button>&nbsp;
                                <asp:button id="btnClose" accessKey="c" runat="server" CssClass="button_text" Width="60px" ToolTip="Alt + c"
						Text="Close" CausesValidation="False" onclick="btnClose_Click"></asp:button></td>
                        </tr>
					</table>
                </td>
                <td width="20%"></td>
            </tr>
		</table>
		</form>
	</body>
</HTML>
