<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_DisplayEvents.aspx.cs" Inherits="Bill_Sys_DisplayEvents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization" TagPrefix="CPA" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Billing System</title>
        <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="frmDisplayEvent" runat="server">
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
              <th scope="col" style="height: 20px"><div align="left" class="band">Appointments</div></th>
              <th width="8%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
            </tr>
                     <tr>
                <td >
    <asp:DataGrid ID="grdDisplayEvents" runat="server" OnItemDataBound="grdDisplayEvents_ItemDataBound" AllowPaging="True" OnPageIndexChanged="grdDisplayEvents_PageIndexChanged" >
        <Columns>
            <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Event Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
            <asp:BoundColumn DataField="DT_EVENT_TIME" HeaderText="Event Time" ></asp:BoundColumn>
            <asp:BoundColumn DataField="SZ_EVENT_NOTES" HeaderText="Event Notes"></asp:BoundColumn>
            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company"></asp:BoundColumn>
            <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </td> 
    </tr>     
    <tr>
    <td>
    <asp:TextBox ID="txtCompanyid" runat="server" Width="10px" Visible="false">
    </asp:TextBox>
    <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="false"></asp:TextBox>
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
