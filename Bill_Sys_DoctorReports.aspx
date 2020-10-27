<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_DoctorReports.aspx.cs" Inherits="Bill_Sys_DoctorReports" %>


<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/WUC_QuickLinks.ascx" TagName="WUC_QuickLinks" TagPrefix="QuickLinksBox" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">  </asp:ScriptManager>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <title>Billing System</title>
    <script src="calendarPopup.js"></script>

    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();
    </script>

    <script type="text/javascript">
     	function ascii_value(c){
        	c = c . charAt (0);
			var i;
			for (i = 0; i < 256; ++ i)
			{
				var h = i . toString (16);
				if (h . length == 1)
				h = "0" + h;
				h = "%" + h;
				h = unescape (h);
				
				if (h == c)
					break;
			}
             return i;
        }
    function CheckForInteger(e,charis)
        {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
                         if (e.charCode < 48 || e.charCode > 57)
                         {             
                                return false;
                         } 
                     }
                 }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
               if(charis!="")
               {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
                }
            }
         }
    </script>
	<style>
	 	.blocktitle_ql{
			width:98%; 
			height:auto;
			text-align:left;
			background-color:#d8dfea;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			line-height:2em;		
			margin-left:0px;
		}
	
		TABLE#content_table{
			border-color:#8ba1ca;
			text-align:left;
			font:Arial, Helvetica, sans-serif;
			font-family:Arial, Helvetica, sans-serif;
			font-size:13px;
			font-weight:normal;
			height: 100%; 
			width: 100%;
		}
	
		.lcss_maintable{
			width:100%;
			height:2%;
			cellpadding:0; 
			cellspacing:0; 
			border: 1px solid #D8D8D8;
		}

		.blocktitle{
			width:98%; 
			height:auto;
			text-align:left;
			background-color:#d8dfea;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			line-height:2em;		
			margin-left:10px;
			font-size:12px;
			font-family:Arial, Helvetica, sans-serif;
		}
		.div_blockcontent{
			width:100%; 
			vertical-align:middle; 
			background-color:#ffffff;
		}
		
		.blocktitle_adv{
			width:100%;
			height: 20px;
			text-align:left;
			background-color:#CCCCCC;
			border-color:#8ba1ca;
			border-style:solid;
			border-width:1px;
			font-family:Arial, Helvetica, sans-serif;
			font-size:12px;
			font-weight:bold;
			padding-top:2px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
        <div align="center">
            <table cellpadding="0px" cellspacing="0px" class="lcss_maintable">
                <tr>
                    <td background="Images/header-bg-gray.jpg" colspan="2">
                        <div align="right">
                            <span class="top-menu"></span></div>
                    </td>
                </tr>
                <tr>
                    <td background="Images/header-bg-gray.jpg" class="top-menu" colspan="2">&nbsp;
					</td>
                </tr>
                <tr>
                    <td background="Images/header-bg-gray.jpg" colspan="2">&nbsp;
					</td>
                </tr>
                <tr>
                    <td background="Images/header-bg-gray.jpg" colspan="2">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="100%" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
                </tr>
                <tr>
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
                </tr>
                <tr>
                    <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center">
                        <span class="message-text"></span>
                    </td>
                </tr>
                <tr>
                    <td height="18" align="right" background="Images/sub-menu-bg.jpg" colspan="2">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th height="29" width="19%" scope="col">
                                    <div align="left">
                                        <a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div>
                                </th>
                                <th height="29" width="81%" scope="col">
                                    <div align="right">
                                        <span class="sub-menu">
                                        </span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="409" colspan="4">
                        <table width="100%" id="content_table"  border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td valign="top" scope="col">
							  	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
									<tr>
									  <td valign="top" scope="col">&nbsp;</td>
									  <td valign="top" scope="col">&nbsp;</td>
									  <td valign="top" scope="col">&nbsp;</td>
								  </tr>
									<tr>
									  <td width="15%" valign="top" scope="col">
											<div class="blocktitle_ql">
													<div align="left" class="blocktitle_adv">Report list</div>
														<div class="div_blockcontent">
															<table width="100%">
																<tr>
																	<td>
																		<ul style="font-family:Arial, Helvetica, sans-serif;font-size:12px;">
																			<li>
																				<a href="Bill_Sys_Reports.aspx">
																					Insurance Carrier Reports
																				</a>
																			</li>
														
																			<li>
																				<a href="Bill_Sys_ProviderReports.aspx">
																					Provider Reports
																				</a>
																			</li>
													
																			<li>
																				<a href="Bill_Sys_DoctorReports.aspx">
																					Doctor Reports
																				</a>
																			</li>
																			<%--<li>
																				<a href="Bill_Sys_Reports.aspx">
																					Patient History
																				</a>
																			</li>--%>
																		</ul>										
																	</td>
																</tr>
															</table>
														</div>
													</div>									  
									  
									  </td>
									  <td width="69%" valign="top" scope="col">
											<div class="blocktitle">
									  			Report Parameters -Doctors Reports
									  			  <div class="div_blockcontent">
											<table width="84%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr valign="top">
                                                <td width="13%" scope="col">
                                                    <div class="lbl">
                                                        <div align="right">
                                                            Doctor</div>
                                                    </div>
                                                </td>
                                                <td width="38%" scope="col">
                                               <extddl:ExtendedDropDownList  ID="extddlDoctor" runat="server" Width="200px" Connection_Key="Connection_String" Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---"  /></td> 
                                                <td width="17%" scope="col">
                                                    <div class="lbl">
                                                        <div align="right">Bill Status</div>
                                                    </div>
                                                </td>
                                                <td scope="col" style="width: 184px">
                                                    <div align="left">
                                                        &nbsp;<asp:DropDownList ID="ddlBillStatus" runat="server" Width="180px">
                                                            <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Paid</asp:ListItem>
                                                            <asp:ListItem Value="2">Un-Paid</asp:ListItem>
                                                            <asp:ListItem Value="3">First Denial</asp:ListItem>
                                                            <asp:ListItem Value="4">Second Denial</asp:ListItem>
                                                        </asp:DropDownList></div>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td scope="col">
                                                    <div class="lbl">
                                                        <div align="right">Date From </div>
                                                    </div>
                                                </td>
                                                <td valign="top" scope="col">
                                                    <div align="left">
                                                      <asp:TextBox ID="txtFromDate" runat="server" Width="120px" MaxLength="50" ReadOnly="false" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                      <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />     
                                                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate" PopupButtonID="imgbtnFromDate" />                                                 
                                                    </div>
                                                </td>
                                            
                                                <td scope="col">
                                                    <div class="lbl">
                                                      <div align="right">Date To </div>
                                                    </div>
                                                </td>
                                                <td valign="top" scope="col" style="width: 184px"><asp:TextBox ID="txtToDate" runat="server" MaxLength="50" onkeypress="return CheckForInteger(event,'/')"
											ReadOnly="false" Width="120px" Visible="true"></asp:TextBox>
                                                  <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" Visible="true"/> 
                                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" PopupButtonID="imgbtnToDate" />
                                                    
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td scope="col">
                                                    <div class="lbl">                                                        </div>
                                                </td>
                                                <td scope="col">
                                                </td>
                                           
                                                <td scope="col" style="height: 11px">
                                                    <div class="lbl">
                                                    </div>
                                                </td>
                                                <td scope="col" style="height: 11px; width: 184px;">
													&nbsp;
                                                </td>
                                             </tr>
                                            <tr valign="top">
                                                <td colspan="4" scope="col" style="text-align: center">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Get Report"
                                                        Width="80px" CssClass="btn-gray"  OnClick="btnSearch_Click"/>
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px"
                                                        CssClass="btn-gray" OnClick="btnClear_Click" />
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox></td>
                                            </tr>
                                        </table>
											  </div>
											</div>
									  </td>
									  <td width="16%" valign="top" scope="col">
                                            <QuickLinksBox:WUC_QuickLinks ID="WUC_QuickLinks1" runat="server" />
									  </td>
									</tr>
									<tr>
									  <td valign="top" scope="col">&nbsp;</td>
									  <td scope="col">&nbsp;</td>
									  <td valign="top" scope="col">&nbsp;</td>
								  </tr>
									<tr>
									  <td valign="top" scope="col">&nbsp;</td>
									  <td scope="col">
											<div class="blocktitle">
									  			Output 
									  			  <div class="div_blockcontent">
													<table width="100%">
														<tr>
															<td height="683" valign="top" scope="col">
                                                                <asp:DataGrid ID="grdBillSearch" runat="Server" OnItemCommand="grdBillSearch_ItemCommand"
                                                                    OnItemDataBound="grdBillSearch_ItemDataBound" OnPageIndexChanged="grdBillSearch_PageIndexChanged">
                                                                    <Columns>
                                                                       
                                                                        <asp:TemplateColumn HeaderText="Bill Number">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkSelectCase" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                                    CommandName="Edit" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="false"></asp:BoundColumn>
                                                                        <%--<asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>--%>
                                                                        <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CASE ID" Visible="False"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="DT_BILL_DATE" DataFormatString="{0:dd MMM yyyy}" HeaderText="Bill Date"
                                                                            ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="FLT_WRITE_OFF" DataFormatString="{0:0.00}" HeaderText="Write Off"
                                                                            ItemStyle-HorizontalAlign="Right" Visible="True"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="false"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="FLT_BILL_AMOUNT" DataFormatString="{0:0.00}" HeaderText="Bill Amount"
                                                                            ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="PAID_AMOUNT" DataFormatString="{0:0.00}" HeaderText="Paid Amount"
                                                                            ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="FLT_BALANCE" DataFormatString="{0:0.00}" HeaderText="Balance"
                                                                            ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                                        <%-- <asp:TemplateColumn HeaderText="Add Service">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkIC9Code" runat="server" Text="Add Service" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>'
                                            CommandName="Add IC9 Code"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                                                        <asp:TemplateColumn HeaderText="Make Payment" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkPayment" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                                    CommandName="Make Payment" Text="Make Payment"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Mark / View" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDeniel" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                                    CommandName="Deniel" Text="Denial"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Generate bill" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkTemplateManager" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                                    CommandName="Generate bill" Text="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Delete" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False"></asp:BoundColumn>
                                                                    </Columns>
                                                                </asp:DataGrid></td>
														</tr>
													</table>
													</div>
											</div>													
									  </td>
									  <td valign="top" scope="col">&nbsp;</td>
								  </tr>
                              </table>
                              </td>
							
                            </tr>
                            <tr>
                                <td valign="top" align="right" colspan="2">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" CssClass="btn" Visible="false"/>
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