<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUserProfile.aspx.cs" Inherits="Security_frmUserProfile" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD>
		<title>frmCreateUsers</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" Content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../CssAndJs/css.css" type="text/css" rel="stylesheet"/>
		<link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />
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
            <td height="27" bgcolor="#E9E9E9" colspan="2"><span class="black11arial">Home - Manage Users Profile</span></td>
          </tr>
          <tr>
            <td colspan="2">&nbsp;</td>
          </tr>
          
          <tr >
            <td width="5%"></td>
            <td  class="defaultText">&nbsp;
            <asp:GridView CssClass="grid" ID="gdUsers" runat="server" AllowPaging="True" Width="95%" AutoGenerateColumns="False" DataKeyNames="USERID,ISACTIVE" OnDataBound="gdUsers_DataBound"  OnRowEditing="gdUsers_RowEditing" OnSelectedIndexChanging="gdUsers_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gdUsers_PageIndexChanging" >
            <RowStyle Height="25px" BorderStyle="Solid" BackColor="#F7F6F3" ForeColor="#333333"/>
                <Columns>
                    <asp:BoundField DataField="USERID"  HeaderText="USERID" Visible="False" >
                         <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FIRSTNAME" HeaderText="First Name">
                         <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LASTNAME" HeaderText="Last Name" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOGINID" HeaderText="LOGINID" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="USERTYPE" HeaderText="USERTYPE" Visible="False" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="USERTYPENAME" HeaderText="User Type" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EMAIL" HeaderText="Email" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ROLEID" HeaderText="ROLEID" Visible="False" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ROLENAME" HeaderText="Role Name" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ISACTIVE" HeaderText="ISACTIVE" Visible="False" />
                    
                    <asp:CommandField ShowSelectButton="True" >
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" Font-Bold="True" />
                    </asp:CommandField>
                    
                    <asp:CommandField ShowEditButton="True">
                        <FooterStyle BackColor="MediumBlue" Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ControlStyle CssClass="esnav" />
                        <ItemStyle CssClass="esnav" Font-Bold="True" />
                    </asp:CommandField>
                </Columns>
                <HeaderStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" Font-Bold="True" />
                <PagerStyle ForeColor="White" Font-Size="XX-Large" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" BackColor="#284775" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                
            </asp:GridView>
           </td>
          </tr>
        
          </table>
			
					
				
		</form>
	</body>
</HTML>
