<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_GeneratedBillDesk.aspx.cs"
    Inherits="Bill_Sys_GeneratedBillDesk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
         
         function confirm_bill_delete()
		 {
		    	if(confirm("Are you sure want to Delete?")==true)
				{
				    if(confirm("Delete visit , treatment , and test entries attached with this bill?")==true)
				    {
				        document.getElementById("lblVisitStatus").value = "DELETE";
				    }
				    else
				    {
				        document.getElementById("lblVisitStatus").value = "CHANGE_STATUS";
				    }
				    
					return true;
				}
				else
				{
					return false;
				}
		}
		
		function OpenPDF(filename)
		{
		    var szDefaultURL = document.getElementById('hdnTemplatePath').value;
		 //   alert(szDefaultURL + filename);
		    window.open(szDefaultURL + filename);
		}
    </script>

</head>
<body topmargin="0" style="text-align: center" bgcolor="#FBFBFB">
    <form id="frmBillSearch" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="simple-table">
                <tr>
                    <td width="9%" height="18">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <div align="right">
                            <span class="top-menu"></span>
                        </div>
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
		  <td class="top-menu">&nbsp;</td>
		  <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center"><span class="message-text"><asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label></span></td>
		  <td class="top-menu">&nbsp;</td>
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
                                            <asp:LinkButton ID="lnkShowPaidBills" CssClass="sub-menu" runat="server" Visible="False"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkUnpaidCases" CssClass="sub-menu" runat="server" Visible="False"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkLitigationDesk" CssClass="sub-menu" runat="server" Visible="False"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkWriteOffDesk" runat="server" CssClass="sub-menu" Visible="False"></asp:LinkButton>
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
                    <td colspan="4" height="409">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th width="9%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                                <th scope="col" style="height: 20px">
                                    <div align="left" class="band">
                                        Search Options</div>
                                </th>
                                <th width="8%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                            </tr>
                            <tr>
                                <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
                                    <div align="left">
                                    
                                        <table width="55%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td class="tablecellLabel" style="width: 15%">
                                                    <div class="lbl">
                                                        File Number
                                                    </div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:TextBox ID="txtCaseID" runat="server" Width="250px"></asp:TextBox></td>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Bill Number</div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <asp:TextBox ID="txtBillNumber" runat="server" Width="250px"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                           
                                            <tr>
                                                <td colspan="6" scope="col" style="text-align: center">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False" CssClass="btn"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click"
                                                        CssClass="btn-gray" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Width="80px"
                                                        CssClass="btn-gray" />
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
                                        Generated Bill Desk</div>
                                </th>
                            </tr>
                            <tr>
                                <th valign="top" scope="col">
                                    <asp:DataGrid ID="grdGeneratedBillDesk" runat="Server" OnPageIndexChanged="grdGeneratedBillDesk_PageIndexChanged">
                                        <Columns>
                                            <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="File Number" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_FILE_NAME" HeaderText="File Name" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DT_DATE" HeaderText="Created Date" DataFormatString="{0:dd MMM yyyy}" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </th>
                            </tr>
                           
                        </table>
                    </td>
                </tr>
             <asp:HiddenField ID="hdnTemplatePath" runat="server" />
            </table>
        </div>
    </form>
</body>
</html>
