<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_BulkPayment.aspx.cs" Inherits="Bill_Sys_BulkPayment" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Billing System</title>
         <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="validation.js" ></script>
         <script src="calendarPopup.js"></script>
    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();
	</script>
	    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
	    <script language="javascript" type="text/javascript" >
            	
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
         function test()
            {
                if(formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType')==undefined)
                {    
                    if (confirm('Cilck ok to write off, Click cancel to continue adding more bills'))
                                  {
                                      document.getElementById('hiddenconfirmBox').value=1; 
                                  }
                                  else
                                  {
                                     document.getElementById('hiddenconfirmBox').value=0;
                                  }
                 }
                 else
                 {      
                        return false;
                 }
            }
         </script>
</head>
<body  topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
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
		              <td class="top-menu" >&nbsp;</td>
	              </tr>
		          <tr>
		              <td class="top-menu">&nbsp;</td>
		              <td colspan="2" background="Images/header-bg-gray.jpg">&nbsp;</td>
		              <td class="top-menu">&nbsp;</td>
	              </tr>
	            
	            
                  <tr>
		              <td width="9%" class="top-menu">&nbsp;</td>
	                  <td colspan="2" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1"  DynamicMenuItemStyleCSS="sublevel1" StaticMenuItemStyleCSS="parentlevel1"  Height="24px"></cc2:WebCustomControl1>
                    </td>
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
		  <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center"><span class="message-text"><asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label></span></td>
		  <td class="top-menu">&nbsp;</td>
	  </tr>  
	  	<tr>
		  <td class="top-menu">&nbsp;</td>
		  <td height="18" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
		  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <th width="19%" scope="col" style="height: 29px">
              <div align="left"><a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div></th>
              <th width="81%" scope="col" style="height: 29px"><div align="right"><span class="sub-menu">
               
              </span></div></th>
            </tr>
          </table>
     </td>
		  <td class="top-menu" colspan="3">&nbsp;</td>
	  </tr>
	   <tr>
	    <td colspan="4">
		  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
		    <tr>
                    <td colspan="6" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
		  
	        <tr>
              <th width="9%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
              <th scope="col" style="height: 20px"><div align="left" class="band">Bulk Payment</div></th>
              <th width="8%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
            </tr>            
             <tr>
                    <td colspan="6" class="usercontrol">
                        <asp:TextBox ID="txtCompanyID" runat="server" visible="false" Width="10px"></asp:TextBox>
                    </td>
                </tr>
            </table> 
            </td> 
            </tr> 
               
            
          
            <tr>
                <td colspan="6">
                    <asp:GridView ID="grvBulkPaymentTransaction" runat="server" AutoGenerateColumns="false"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Bill Number">
                                <ItemTemplate >
                                    <asp:Label ID="lblBillNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_ID") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Number" Visible="false">
                                <ItemTemplate >
                                    <asp:Label ID="lblPaymentID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SZ_PAYMENT_ID") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Balance" ItemStyle-HorizontalAlign="Right" >
                                <ItemTemplate >
                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("FLT_BALANCE", "{0:00.00}") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check Number">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtgrdCheckNumber" runat="server" CssClass="box4" Text='<%# DataBinder.Eval(Container, "DataItem.SZ_CHECK_NUMBER") %>' ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Check Date"  >
                                <ItemTemplate >
                                    <asp:TextBox ID="txtgrdCheckDate" runat="server" CssClass="box4" Text='<%# Bind("DT_CHECK_DATE", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Check Amount">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtgrdCheckAmount" runat="server" CssClass="box4" Text='<%# Bind("FLT_CHECK_AMOUNT", "{0:00.00}") %>' ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Payment Type">
                                <ItemTemplate >
                                    <asp:DropDownList runat="server" ID="drpgrdpaymentType"  SelectedValue='<%# DataBinder.Eval(Container, "DataItem.I_PAYMENT_STATE") %>'> 
                                        <asp:ListItem Text="Litigation" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Write Off" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Cancel" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtgrdPaymentType" runat="server" CssClass="box4" Text='<%# DataBinder.Eval(Container, "DataItem.I_PAYMENT_STATE") %>' ></asp:TextBox>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comment" >
                                <ItemTemplate >
                                    <asp:TextBox ID="txtgrdComment" runat="server" CssClass="box4" Text='<%# DataBinder.Eval(Container, "DataItem.SZ_COMMENT") %>' MaxLength="10" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

               
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height: 26px" colspan="6"> 
                    <asp:Button ID="btnUpdateGridRecord" Text="Update" runat="server" OnClick="btnUpdateGridRecord_Click" cssclass="btn-gray"/>
                    <asp:Button ID="Button1" Text="Cancel" runat="server" OnClick="Button1_Click" cssclass="btn-gray"/>
                    </td>
            </tr>
        </table>
     <input type="hidden"  id="hiddenconfirmBox" name="hiddenconfirmBox" />
    </div>
    </form>
</body>
</html>
