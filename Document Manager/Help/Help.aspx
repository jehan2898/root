<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Security_Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <TITLE>Welcome Law Allies - Support</TITLE>
    <style type="text/css">
    .sb1 {FONT-FAMILY: arial; FONT-SIZE: 11px; TEXT-DECORATION: none}
    .sb2 {FONT-FAMILY: arial; FONT-SIZE: 12px; TEXT-DECORATION: none}
    .sb3 {FONT-FAMILY: arial; FONT-SIZE: 18px; TEXT-DECORATION: none}
    A.tab {COLOR: #ffffff; FONT-FAMILY: arial; FONT-SIZE: 10px; TEXT-DECORATION: none}
    A.tab1 {COLOR: #000000; FONT-FAMILY: arial; FONT-SIZE: 11px; TEXT-DECORATION: none}
    A.tab2 {COLOR: #0066cc; FONT-FAMILY: arial; FONT-SIZE: 11px; TEXT-DECORATION: none}
    A.tab3 {COLOR: #ffffff; FONT-FAMILY: arial; FONT-SIZE: 11px; TEXT-DECORATION: none}
    A.tab4 {COLOR: #FF0000; FONT-FAMILY: arial; FONT-SIZE: 11px; TEXT-DECORATION: none}
    TD.top {BACKGROUND-COLOR: #6391d1}
    img {border: 0px none;}
    TABLE.nav{COLOR: #6391d1; FONT-FAMILY: arial; FONT-SIZE: 11px; TEXT-DECORATION: none}
    TD.l{BACKGROUND-COLOR: #EAEAEA}
    TD.l2{font-size: 1pt;font-family: arial;}
    .drpdwn {color:#FFFFFF;background-color:#06589D}
    .sbttn {background: #E5B2C1;border-bottom: 1px solid #731734;border-right: 1px solid #731734;border-left: 1px solid #731734;border-top:1px solid #731734;color:#000000;height:22px;text-decoration:none;cursor: hand}
    </style>
    <script language="JavaScript" src="../css/menu.js"></script>
    <SCRIPT LANGUAGE="JavaScript1.2" TYPE="text/javascript">
    </SCRIPT>
</head>
<body>
    <form id="form1" runat="server">
    <!-- Support Header Begin //-->
    <TABLE border=0 cellPadding=0 cellSpacing=0 width="100%">
        <TR>
            <TD COLSPAN="2" BGCOLOR="#000000" HEIGHT="1"><TABLE cellSpacing=0 cellPadding=0 border=0><TR><TD height=1></TD></TR></TABLE></TD>
        </TR>
        <TR bgColor=#E8E8E7>
            <TD rowSpan=2 vAlign=bottom width=206><IMG border=0 height=58 hspace=0 src="../images/customertop.gif" width=206><BR></TD>
            <TD width=100% height=47 ALIGN=right VALIGN=bottom><FONT CLASS=sb2 COLOR=#06589D><B>New to Customer Care?</B> <FONT CLASS=sb1><A HREF="CustomerSupport.htm" onclick="return loadIframe('MyFrame', this.href)">Click here</A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT><br></TD>
        </tr>
    </TABLE>

    <!-- Support Header End //-->
    <TABLE border=0 cellPadding=0 cellSpacing=0 width="98%">
        <TR>
            <TD vAlign=top width=137>
                <!-- Left Navigation Begin //-->
                <TABLE class=ln cellSpacing=0 cellPadding=0 width=137 border=0>
                    <TR bgColor=#000000><TD width=137 height=18><IMG height=4 hspace=10 src="../images/s_arrow.gif" width=7 align=right vspace=5 border=0><FONT class=sb1 color=#ffffff><B>&nbsp;&nbsp;Customer Care</B></FONT><BR></TD></TR>
                </TABLE>

                <TABLE cellSpacing=1 cellPadding=0 width=137 bgColor=#ffffff border=0>
                    <TR><TD class=l height=18>&nbsp; <FONT size=1 COLOR=#000000>&#149;&nbsp;</FONT> <A class=tab1 href="WelcomeHelp.htm" onclick="return loadIframe('MyFrame', this.href)">Home</A><BR></TD></TR>
                <TD class=l height=18>&nbsp; <FONT size=1 COLOR=#000000>&#149;&nbsp;</FONT> <A class=tab1 href="UserRegistration.htm" onclick="return loadIframe('MyFrame', this.href)">Customer Registration</A><BR></TD>
            </TD>              
        </TR>
        <TR><TD class=l height=18>&nbsp; <FONT size=1 COLOR=#A6A1A1>&#149;&nbsp;</FONT> <A class=tab1 href="ManageUserProfile.htm" onclick="return loadIframe('MyFrame', this.href)">Profile Management</A><BR></TD></TR>
        <TR><TD class=l height=18>&nbsp; <FONT size=1 COLOR=#000000>&#149;&nbsp;</FONT> <A class=tab1 href="RoleManagement.htm" onclick="return loadIframe('MyFrame', this.href)">Role Management</A><BR></TD></TR>
        <TR><TD class=l height=18>&nbsp; <FONT size=1 COLOR=#000000>&#149;&nbsp;</FONT> <A class=tab1 href="ForgetPassword.htm" onclick="return loadIframe('MyFrame', this.href)">Forgot Password</A><BR></TD></TR>
       
</TABLE>
<!-- Left Navigation End //-->

<TD bgColor=#000000 width=2><TABLE cellSpacing=0 cellPadding=0 border=0><TR><TD height=1></TD></TR></TABLE></TD>
<TD style="width: 1px"><TABLE cellSpacing=0 cellPadding=0 border=0 width=10><TR></TR></TABLE></TD>
<TD vAlign=top width="632">

<TABLE  WIDTH="100%" CELLSPACING=0 CELLPADDING=0 BORDER=0>
<TR>
<TD WIDTH=460 VALIGN=top>
<!-- Content Begin //-->
<TABLE WIDTH=460 CELLSPACING=0 CELLPADDING=0 BORDER=0>

<Tr>
<TD WIDTH=460 VALIGN=top COLSPAN=3 style="height: 100%">
<div id="MyDiv" align="left">
        <iframe id="MyFrame" runat="server" name="LinkPage" src="WelcomeHelp.htm" frameborder="2px"   width="615px"
            style="overflow: auto" height="510px"></iframe>
    </div>

</TD>
</tr>
</TABLE>
</tr>
</table>
        </TR></TABLE>
    </form>
</body>
</html>
