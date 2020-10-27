<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Activity.aspx.cs"
    Inherits="Bill_Sys_Activity" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>

        <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="frmActivity" runat="server">
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
                            Height="24px"></cc2:WebCustomControl1></td>
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
		  <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center"><span class="message-text">Data saved successfully...</span></td>
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
                <asp:LinkButton ID="lnkShowPaidBills" CssClass="sub-menu" runat="server" >Paid Cases</asp:LinkButton>
|
<asp:LinkButton ID="lnkUnpaidCases" CssClass="sub-menu" runat="server" >Un-paid Cases</asp:LinkButton>
|
<asp:LinkButton ID="lnkLitigationDesk" CssClass="sub-menu" runat="server" >Litigation Desk</asp:LinkButton>
|
<asp:LinkButton ID="lnkWriteOffDesk" runat="server" CssClass="sub-menu" >Write-Off desk</asp:LinkButton>
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
                    <td colspan="3" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
		   <tr>
              <th width="9%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
              <th scope="col" style="height: 20px"><div align="left" class="band">Activity</div></th>
              <th width="8%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
            </tr>
		      <tr>
              <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
              <div align="left">
               <table width="55%"  border="0" align="center" cellpadding="0" cellspacing="3">
                <tr>
                    <td colspan="3" style="height: 30px">
                        <div id="ErrorDiv" visible="true" style="color: Red;">
                        </div>
                    </td>
                </tr>
               <tr>
                    <td class="tablecellLabel">
                        Actvity Name
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                        <asp:TextBox ID="txtActivityName" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                
                 <tr>
                    <td colspan="3" width="100%">
                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click" /></td>
                </tr>
               </table> 
              </div> 
              </th> 
              </tr> 
            <tr>
              <th scope="col">
              <div align="left">
               </div><div align="left" class="band">Activity List</div>
               </th>
            </tr>
            
             <tr>
              <th valign="top" scope="col">  
               <asp:DataGrid ID="grdActivity" runat="server" AutoGenerateColumns="False" OnPageIndexChanged="grdActivity_PageIndexChanged"
                            OnSelectedIndexChanged="grdActivity_SelectedIndexChanged">
                            <Columns>
                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="SZ_ACTIVITY_ID" HeaderText="ACTIVITY ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_ACTIVITY_NAME" HeaderText="ACTIVITY NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY"></asp:BoundColumn>
                            </Columns>
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
