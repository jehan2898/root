<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_LineViewDayCalanderForCompany.aspx.cs" Inherits="Bill_Sys_LineViewDayCalanderForCompany" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>

<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Medical Billing - Calendaring</title>
    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
	<script type="text/javascript" src="validation.js" ></script>
	
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
	<style>
		.css-show-all{
			color:#FF0000;
			text-decoration:none;
			font-family:Arial, Helvetica, sans-serif;
			font-size:12px;
			font-weight:bold;
		}
		
		.css-cal-text{
			color:#000033;
			text-decoration:none;
			font-family:Arial, Helvetica, sans-serif;
			font-size:12px;
			font-weight:normal;
			BORDER-BOTTOM: #999966 1px solid;
			text-align:left;
			padding:5px 140px 5px 10px;
			margin: auto;
		}
		
		.css-text{
			padding:0px 90px 0px 0px;
		}
	</style>
</head>
<body topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="form1" runat="server">
	<div align="center">
	<table cellpadding="0" cellspacing="0" class="simple-table">
		<tr>
			<td width="9%" height="18" >&nbsp;</td>
		    <td colspan="2" background="Images/header-bg-gray.jpg"><div align="right"><span class="top-menu">Home | Logout</span></div></td>
		    <td width="8%" >&nbsp;</td>
		</tr>

		<tr>
			<td class="top-menu">&nbsp;</td>
			<td colspan="2" background="Images/header-bg-gray.jpg" class="top-menu">&nbsp;</td>
			<td class="top-menu">&nbsp;</td>
		</tr>
		<tr>
			<td class="top-menu">&nbsp;</td>
			<td colspan="2" background="Images/header-bg-gray.jpg">&nbsp;</td>
			<td class="top-menu">&nbsp;</td>
		</tr>

		<tr>
		  <td width="9%" class="top-menu">&nbsp;</td>
	      <td colspan="2" background="Images/header-bg-gray.jpg">
	      <cc2:WebCustomControl1 id="TreeMenuControl1" runat="server"  Procedure_Name="SP_MST_MENU" Connection_Key="Connection_String" Width="100%" Xml_Transform_File="TransformXSLT.xsl" LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1"  DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1></td>
	      <td width="8%" class="top-menu">&nbsp;</td>
	  </tr>
		<tr>
		  <td class="top-menu">&nbsp;</td>
		  <td height="35px" bgcolor="#000000"><div align="left"></div>		    
	      <div align="left"><span class="pg-bl-usr">Billing company name</span></div></td>
		  <td width="12%" height="35px" bgcolor="#000000"><div align="right"><span class="usr">Admin</span></div></td>
		  <td class="top-menu">&nbsp;</td>
	  </tr>
		<tr>
			<td class="top-menu">&nbsp;</td>
			<td height="20px" colspan="2" bgcolor="#EAEAEA" align="center"><span class="message-text">&nbsp;</span></td>
			<td class="top-menu">&nbsp;</td>
		</tr>		

		<tr>
		  <td class="top-menu">&nbsp;</td>
		  <td height="29" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
		  	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
				<tr>
				  <th height="29" width="19%" scope="col"><div align="left"><a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div></th>
				  <th height="29" width="81%" scope="col"><div align="right"><span class="sub-menu">
				  	<asp:LinkButton ID="lnkDayView" CssClass="sub-menu" runat="server" OnClick="btnDayView_Click">Day View</asp:LinkButton>
				  
|
<asp:LinkButton ID="lnkMonthView" CssClass="sub-menu" runat="server" OnClick="btnMonthView_Click" >Month View</asp:LinkButton>
|
<asp:LinkButton ID="lnkPrevious" CssClass="sub-menu" runat="server" OnClick="btnPrev_Click" Text="Previous Date"></asp:LinkButton>
|
<asp:LinkButton ID="lnkNext" runat="server" CssClass="sub-menu" OnClick="btnNext_Click" Text="Next Date"></asp:LinkButton>
				  </span></div></th>
				</tr>
			</table>
		</tr>
		<tr>
			<td class="top-menu">&nbsp;</td>
			<td colspan="2" bgcolor="#EAEAEA" align="center">
					<table style="width: 100%;background-color:#E9E9E9; color:#003366; " cellspacing="0">
					<tr>
						<td colspan="3" style="border-bottom: #999966 1px solid;">
							<div align="center"> <asp:Label ID="lblDate" runat="server"></asp:Label> </div>
						</td>
					  </tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label0" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label> 
						</td>
						<td class="css-cal-text">
							<asp:Literal ID="literal0" runat="server">
							</asp:Literal>
						</td>
					   <td style="border-bottom: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn0" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="00">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center; height: 22px;">
							<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
					   <td class="css-cal-text">
							<asp:Literal ID="literal1" runat="server">
							</asp:Literal>
					  </td>
					  <td style="BORDER-BOTTOM: #999966 1px solid; height: 22px;">
						   <asp:LinkButton ID="LnkBtn1" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="01">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						 <td class="css-cal-text">
						<asp:Literal ID="literal2" runat="server">
						   </asp:Literal>
						</td>
					   <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn2" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="02">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal3" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn3" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="03">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						   <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						<asp:Literal ID="literal4" runat="server">
						  </asp:Literal>
						</td>
					   <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn4" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="04">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						<asp:Literal ID="literal5" runat="server">
						  </asp:Literal>
						</td>
						 <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn5" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="05">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						 <td class="css-cal-text">
							<asp:Literal ID="literal6" runat="server">
							</asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn6" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="06">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							 <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						<asp:Literal ID="literal7" runat="server">
						  </asp:Literal>
						</td>
					   <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn7" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="07">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						 <td class="css-cal-text">
						 <asp:Literal ID="literal8" runat="server">
						   </asp:Literal>
						</td>
					  <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn8" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="08">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal9" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn9" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="09">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal10" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn10" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="10">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							 <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
					   <td class="css-cal-text">
						 <asp:Literal ID="literal11" runat="server">
						 </asp:Literal>
					  </td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn11" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="11">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal12" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn12" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="12">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							<asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal13" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn13" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="13">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							 <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal14" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn14" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="14">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
							 <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal15" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn15" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="15">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal16" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn16" runat="server" Visible="false" OnClick="ShowAll" cssClass="css-show-all" ForeColor="SaddleBrown" CommandArgument="16">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						 <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
					   <td class="css-cal-text">
						 <asp:Literal ID="literal17" runat="server">
						 </asp:Literal>
					  </td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn17" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="17">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						 <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						 <asp:Literal ID="literal18" runat="server">
						  </asp:Literal>
						</td>
					   <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn18" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="18">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center; height: 28px;">
						 <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text" style="height: 28px">
						 <asp:Literal ID="literal19" runat="server">
						  </asp:Literal>
						</td>
					   <td style="BORDER-BOTTOM: #999966 1px solid; height: 28px;">
						   <asp:LinkButton ID="LnkBtn19" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="19">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						 <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal20" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn20" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="20">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						 <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal21" runat="server">
						  </asp:Literal>
						</td>
					   <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn21" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="21">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal22" runat="server">
						  </asp:Literal>
						</td>
					   <td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn22" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="22">(Show All)</asp:LinkButton>&nbsp;
					  </td>
					</tr>
					<tr>
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal23" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn23" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="23">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					 <tr runat="server" id="tr25" visible="false">
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal24" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn24" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="24">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr runat="server" id="tr26" visible="false">
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal25" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn25" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="25">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					 <tr runat="server" id="tr27" visible="false">
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal26" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn26" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="26">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr runat="server" id="tr28" visible="false">
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal27" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn27" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="27">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					 <tr runat="server" id="tr29" visible="false">
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal28" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn28" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="28">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr runat="server" id="tr30" visible="false">
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal29" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn29" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="29">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
					<tr runat="server" id="tr31" visible="false">
						<td style="width: 125px; border-right: #003333 thin solid; border-bottom: #999966 1px solid; text-align:center;">
						<asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Verdana" Font-Size="11pt" ForeColor="#333333"></asp:Label>  </td>
						<td class="css-cal-text">
						  <asp:Literal ID="literal30" runat="server">
						  </asp:Literal>
						</td>
						<td style="BORDER-BOTTOM: #999966 1px solid">
						   <asp:LinkButton ID="LnkBtn30" runat="server" Visible="false" OnClick="ShowAll" ForeColor="SaddleBrown" CssClass="css-show-all" CommandArgument="30">(Show All)</asp:LinkButton>&nbsp;
						</td>
					</tr>
				</table>			
			</td>
			<td class="top-menu">&nbsp;</td>
		</tr>
	</table>
	</div>
	
	<div align="center">
		<table width="83%" class="maintable">
			<tr>
				<td colspan="3" align="left" >
					<asp:Button ID="btnPrev" runat="server" Text="Prev" OnClick="btnPrev_Click" Visible="false" />
					<asp:Button Visible="false" ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
					<asp:Button Visible="false" ID="btnDayView" runat="server" Text="Day View" OnClick="btnDayView_Click" />
					<asp:Button Visible="false" ID="btnMonthView" runat="server" Text="Month View" OnClick="btnMonthView_Click" />
		        </td>
			</tr>
      </table>
        <%--<asp:GridView ID="grdDayView" runat="server" BackColor="yellow" ShowHeader="false" >
            <Columns>
                <asp:BoundField HeaderText="Time"    />
            </Columns>
            <HeaderStyle  />
            
        </asp:GridView> --%>
        
      </div>
    </form>
</body>
</html>
