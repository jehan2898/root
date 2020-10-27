<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_UpcomingEvents.aspx.cs"
    Inherits="Bill_Sys_UpcomingEvents" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    <%@ Register Src="UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <style>
		.css-calendar-grid{
			width: 100%;
			background-color:#999999;
		}
		
		.css-calendar-grid-td{
			width: 14%;
			height: 60px;
			background-color:#CCCCCC;
			text-align:center;
			/*width='14%' height='60px' bgcolor='blue' align='center' style='color:White' */
		}
		
		.css-cal-date{
			color:#000000;
		}
	</style>
	
	<script language="javascript" type="text/javascript">
   
    function OpenDayViewCalender(p_szDate)
    {
        
        document.getElementById("hdnSession").value = p_szDate;
    
        document.getElementById("btnSetSession").click();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="simple-table">
                <tr>
                    <td width="9%" height="18">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <div align="right">
                            <span class="top-menu"></span></div>
                    </td>
                    <td width="8%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg" class="top-menu">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="9%" class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
                    <td width="8%" class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="35px" bgcolor="#000000">
                        <div align="left">
                        </div>
                        <div align="left">
                            <span class="pg-bl-usr">Billing company name</span></div>
                    </td>
                    <td width="12%" height="35px" bgcolor="#000000">
                        <div align="right">
                            <span class="usr">Admin</span></div>
                    </td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
                            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label></span></td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="18" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th width="19%" scope="col" style="height: 29px">
                                    <div align="left">
                                        <span class="pg">&gt;&gt; Home >> Upcoming Events</span></div>
                                </th>
                                <th width="81%" scope="col" style="height: 29px">
                                    <div align="right">
                                        <span class="sub-menu"></span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                    <td class="top-menu" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="4" class="usercontrol">
                                    <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                            <asp:TextBox ID="hdnSession" runat="server" Width="0px" BorderStyle="None" BorderWidth="0px" ></asp:TextBox></td>
                            </tr>
                            <tr>
                    <td width="9%">&nbsp;</td>
                    <td align="left" style="background-color:#D2D2D6;"> <CI:Bill_Sys_Case ID="UCCaseInfo" runat="server"></CI:Bill_Sys_Case></td>
                    <td width="8%">&nbsp;</td>
               </tr> 
                            <tr>
                                <th width="9%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                                <th scope="col" style="height: 20px">
                                    <div align="left" class="band">
                                        Upcoming Events</div>
                                </th>
                                <th width="8%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td id="tdMonthCalender" runat="server" >
                                          
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                            <td>
                            <asp:Button ID="btnSetSession" runat="server" Width="1px"  OnClick="btnSetSession_Click" Text="setsession"/>&nbsp;
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                            </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
