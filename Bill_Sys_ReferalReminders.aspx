<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ReferalReminders.aspx.cs" Inherits="Bill_Sys_ReferalReminders" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Billing System</title>

    <script src="validation.js"></script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

    <script src="calendarPopup.js"></script>

    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();		
	
    </script>

    <script language="javascript" type="text/javascript">
            	
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

</head>
<body topmargin="0" style="text-align: center" bgcolor="#FBFBFB">
    <form id="frmReferalReminder" runat="server">
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
                            Connection_Key="Connection_String" Width="100%" Xml_Transform_File="TransformXSLT.xsl"
                            DynamicMenuItemStyleCSS="sublevel1" StaticMenuItemStyleCSS="parentlevel1" Height="24px">
                        </cc2:WebCustomControl1>
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
                        <span class="message-text"><asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label></span>
                    </td>
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
                                        <a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div>
                                </th>
                                <th width="81%" scope="col" style="height: 29px">
                                    <div align="right">
                                        <span class="sub-menu">
                                            <asp:LinkButton ID="lnkShowPaidBills" CssClass="sub-menu" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkUnpaidCases" CssClass="sub-menu" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkLitigationDesk" CssClass="sub-menu" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkWriteOffDesk" runat="server" CssClass="sub-menu"></asp:LinkButton>
                                        </span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                    <td class="top-menu" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" >
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th width="9%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                                <th scope="col" style="height: 20px">
                                    <div align="left" class="band">
                                        Referal Reminder</div>
                                </th>
                                <th width="8%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                            </tr>
                            <tr>
                                <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
                                    <div align="left">
                                        <table width="55%" border="0" align="center" cellpadding="0" cellspacing="3">
                                        <tr>
                <td colspan="6" height="30">
                    <div id="ErrorDiv" style="color: red" visible="true">
                    </div>
                </td>
            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Referal Office</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <extddl:ExtendedDropDownList ID="extddlReferalList" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_REFERAL_OFFICE" Flag_Key_Value="REFERAL_OFFICE_LIST" Selected_Text="--- Select ---"   />
                                                </td>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Procedural Code</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                     <extddl:ExtendedDropDownList ID="extddlProceduralCode" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_CODES" Flag_Key_Value="PROCEDURE_CODE_LIST" Selected_Text="--- Select ---" />  </td>
                                            </tr>
                                  
                                         
                                            <tr>
                                                <td colspan="6" scope="col" style="text-align: center">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False" CssClass="btn" ></asp:TextBox>
                                                    <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="False" CssClass="btn"></asp:TextBox>
                                                    <asp:Button ID="btnShow" runat="server" Text="Search" Width="80px" 
                                                        CssClass="btn-gray" OnClick="btnShow_Click"  />
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </th>
                            </tr>
                            <tr>
                                <th scope="col">
                                    <div align="left">
                                    </div>
                                    <div align="left" class="band">
                                        List</div>
                                </th>
                            </tr>
                            <tr>
                                <th valign="top" scope="col">
                                    <asp:DataGrid ID="grdReferalReminder" runat="server" OnItemDataBound="grdReferalReminder_ItemDataBound" OnItemCommand="grdReferalReminder_ItemCommand" >
                                        <FooterStyle />
                                        <SelectedItemStyle />
                                        <PagerStyle />
                                        <AlternatingItemStyle />
                                        <ItemStyle />
                                        <Columns>
                                           
                                            <asp:BoundColumn DataField="SZ_SCHEDULED_REFERAL_ID" HeaderText="Referal ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_REFERAL_OFFICE_ID" HeaderText="Referal ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_REFERAL_OFFICE_NAME" HeaderText="Office Name"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURAL_CODE_ID" HeaderText="Procedural Code ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURAL_CODE" HeaderText="Procedural Code"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_REMINDER_DATE" HeaderText="Reminder Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCESSED_DATE" HeaderText="Processed Date" DataFormatString="{0:MM/dd/yyyy}" Visible="false" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_SCHEDULED_DATE" HeaderText="Schedule Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                            
                                            
                                            <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_LAST_UPDATED_USER_ID" HeaderText="Last Update User" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_CREATED_USER_ID" HeaderText="Created User"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_LAST_UPDATED" HeaderText="Last Updated" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="false"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                            <ItemTemplate>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="text-box" onkeypress="return CheckForInteger(event,'/')" MaxLength="10"></asp:TextBox>
                                            
                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn>
                                            <ItemTemplate>
                                            <asp:Button id="btnDone" runat="server" Text="Done"  CssClass="btn-gray" />
                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle />
                                    </asp:DataGrid>
                                </th>
                            </tr>
                            
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>