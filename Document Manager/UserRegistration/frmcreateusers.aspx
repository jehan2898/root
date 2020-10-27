<%@ Page language="c#" SmartNavigation ="true" Inherits="UserRegistration.frmCreateUsers" CodeFile="frmCreateUsers.aspx.cs" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>frmCreateUsers</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" Content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../css/css.css" type="text/css" rel="stylesheet"/>
		<link href="../css/DocMgrCss.css" rel="stylesheet" type="text/css" />
		<link href='<%=Page.ResolveUrl("~/CssAndJs/DemoStyles.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/Main.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/AxpStyleXPGrid3.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/LinkSelector.css")%>' type="text/css" rel="stylesheet">
		
		</script>
		<script language="JavaScript" type="text/JavaScript">
              function MM_preloadImages()
               { //v3.0
                  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
                    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
                if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
               }
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
		
		
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
              <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
            <td height="27" bgcolor="#E9E9E9"><asp:label id="lblPageHeader" runat="server" CssClass="black11arial">Home - Add User</asp:label></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
          
          <tr>
            <td>&nbsp;</td>
          </tr>
          <tr>
          
          <td class="defaultText" height="49">
            <table  width="100%">
                <tr height="20px">
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td width="20%"></td>
                    <td class="defaultText">
				        <table class="grid" cellSpacing="0" cellPadding="5" width="100%" border="0" > 
				        <tr height="20px">
                            <td  colspan="1" rowspan="1" ></td>
                            <td colspan="2" rowspan="1" ></td>
                        </tr>
					    <tr>
					        <td width="20%"></td>
						    <td><asp:label id="Label100" runat="server" CssClass="esnav">First Name</asp:label><span style="color: #ff3333">*</span></td>
						    <td><asp:textbox id="txtFName" runat="server" CssClass="box" Width="170px"></asp:textbox>
							    <asp:requiredfieldvalidator id="rfvFName" runat="server" CssClass="Validatecontroltxt" ErrorMessage="*" ControlToValidate="txtFName"></asp:requiredfieldvalidator>&nbsp;
                            </td>
					    </tr>
					    <tr >
					        <td width="20%"></td>
						    <td><asp:label id="Label1" runat="server" CssClass="esnav">Last Name</asp:label><span style="color: #ff3333">*</span></td>
						    <td><asp:textbox id="txtLName" runat="server" CssClass="box" Width="170px"></asp:textbox>
							    <asp:requiredfieldvalidator id="rfvLName" runat="server" CssClass="Validatecontroltxt" ErrorMessage="*" ControlToValidate="txtLName"></asp:requiredfieldvalidator>&nbsp;
                            </td>
					    </tr>
                        <tr>
                            <td width="20%"></td>
                            <td><asp:label id="Label2" runat="server" CssClass="esnav">User Type</asp:label><span style="color: #ff3333">*</span></td>
                            <td><asp:dropdownlist id="ddlUserType" runat="server" CssClass="combox" Width="170px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="ddlUserType" CssClass="Validatecontroltxt" ErrorMessage="Please Select User Type" InitialValue="-- Select --"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
					    <TR>
					        <td width="20%"></td>
						    <TD><asp:label id="Label3" runat="server" CssClass="esnav">Email ID</asp:label><span style="color: #ff3333">*</span></TD>
						    <TD><asp:textbox id="txtEmail" runat="server" CssClass="box" Width="170px"></asp:textbox>
							    <asp:RequiredFieldValidator id="rfvEMail" runat="server" CssClass="Validatecontroltxt" ErrorMessage="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" CssClass="Validatecontroltxt" Display="Dynamic" ErrorMessage="Email is not in correct format" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Width="182px"></asp:RegularExpressionValidator>
                            </TD>
					    </TR>
					    <TR>
					        <td width="20%"></td>
						    <TD><asp:label id="Label4" runat="server" CssClass="esnav">Role</asp:label><span style="color: #ff3333">*</span></TD>
						    <TD><asp:dropdownlist id="ddlRoles" runat="server" CssClass="combox" Width="170px"></asp:dropdownlist>
							    <asp:requiredfieldvalidator id="rfvDesignation" runat="server" CssClass="Validatecontroltxt" ErrorMessage="Please Select User Role" ControlToValidate="ddlRoles" InitialValue="-- Select --"></asp:requiredfieldvalidator>
							</TD>
					    </TR>
					    <TR>
					        <td width="20%"></td>
						    <TD>&nbsp;</TD>
						    <TD></TD>
					    </TR>
					    <TR>
					        <td width="20%"></td>
						    <TD></TD>
						    <TD><input id="btnSubmit" type="submit" value="Submit" name="Submit" runat="server" class="button_text" onserverclick="btnSubmit_ServerClick"></TD>
						    <TD></TD>
					    </TR>
				</table>
			</td >
			<td width="20%"></td>
          </tr >
         </table>
		</td>
      </tr >
     </table>
	</form>
 </body>
</HTML>
