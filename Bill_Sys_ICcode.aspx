<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ICcode.aspx.cs"
    Inherits="Bill_Sys_ICcode" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

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
<body>
    <form id="frmICCode" runat="server">
        <div align="center">
            <table class="maintable" cellpadding="0px" cellspacing="0px">
                <tr>
                   
                    <td colspan="6" background="Images/header-bg-gray.jpg"><div align="right"><span class="top-menu">Home | Logout</span></div></td>
                </tr>
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg" class="top-menu">&nbsp;</td>
                </tr>
                
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg" class="top-menu">&nbsp;</td>
                </tr>
               
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 id="TreeMenuControl1" runat="server"  Procedure_Name="SP_MST_MENU" Connection_Key="Connection_String" Width="100%" Xml_Transform_File="TransformXSLT.xsl" LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1"  DynamicMenuItemStyleCSS="sublevel1" StaticMenuItemStyleCSS="parentlevel1"  Height="24px"></cc2:WebCustomControl1></td>
                </tr>
               
                <tr>
                    <td height="35px" bgcolor="#000000" colspan="5">
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
                   
                    <td height="20px" colspan="6" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                        </span>
                    </td>
                   
                </tr>
                
                
                <tr>
                    <td height="18" colspan="6" align="right" background="Images/sub-menu-bg.jpg">
                        <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th height="29" width="19%" scope="col"><div align="left"><a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div></th>
                            </tr>
                        </table>
                    </td> 
                </tr>
                 <tr>
                    <td colspan="6" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
                
                <tr>
                <td colspan="6">
                   <div align="left" class="band"> IC Code </div></td>
            </tr>
            
            <tr>
               <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col" colspan="6">
                   <div align="left">
                       <table width="55%" border="0" align="center" cellpadding="0" cellspacing="3">
                
                <!-- Start : Data Entry -->
                <tr>
                    <td colspan="3" height="30px">
                        <div id="ErrorDiv" visible="true" style="color: Red;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        <div class="lbl">IC9 Code</div></td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                        <asp:TextBox ID="txtICCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                    <td class="tablecellLabel">
                        <div class="lbl">Description</div></td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                        <asp:TextBox ID="txtICCodeDesc" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        <div class="lbl">Amount</div></td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                        <asp:TextBox ID="txtICCodeAmt" runat="server" Width="250px" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click" CssClass="btn-gray"/>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click" CssClass="btn-gray"/>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click" CssClass="btn-gray"/></td>
                </tr>
                <!-- End : Data Entry -->
                </table>
                   </div>
               </th>
           </tr>
                <tr>
                    <th scope="col" colspan="6">
                        <div align="left">
                        </div>
                        <div align="left" class="band">
                            IC Code List</div>
                    </th>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:DataGrid ID="grdICCode" runat="server" OnSelectedIndexChanged="grdICCode_SelectedIndexChanged"
                            OnPageIndexChanged="grdICCode_PageIndexChanged">
                            <FooterStyle />
                            <SelectedItemStyle />
                            <PagerStyle />
                            <AlternatingItemStyle />
                            <ItemStyle />
                            <Columns>
                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                </asp:ButtonColumn>
                                <asp:BoundColumn HeaderText="IC9 ID" DataField="SZ_IC9_ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_IC9_CODE" HeaderText="IC9 Code"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_IC9_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_IC9_AMOUNT" HeaderText="Amount" DataFormatString="{0:0.00}"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                            </Columns>
                            <HeaderStyle />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
