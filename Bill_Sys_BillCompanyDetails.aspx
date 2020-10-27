<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_BillCompanyDetails.aspx.cs"
    Inherits="Bill_Sys_BillCompanyDetails" %>


<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align:top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                
                               
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        
                                        <tr>
                                                <td class="ContentLabel" style="text-align:center; height:25px;" colspan="4" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                                
                                                 <tr>
                    <td width="40%" class="ContentLabel"  align="left">
                        <strong>Account opening date</strong>
                    </td>
                    <td width="1%" class="ContentLabel" >
                        <strong>:</strong>
                    </td>
                    <td width="40%" class="ContentLabel"  align="left">
                        <asp:Label ID="lblOpenDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="40%" class="ContentLabel"  align="left">
                        <strong>Number of user licenses </strong>
                    </td>
                    <td width="1%">
                        <strong>:</strong>
                    </td>
                    <td width="40%" class="ContentLabel"  align="left">
                        <asp:Label ID="lblNumberOfUsers" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="40%" class="ContentLabel"  align="left">
                        <strong>Number of cases opened </strong>
                    </td>
                    <td width="1%">
                        <strong>:</strong>
                    </td>
                    <td width="40%" class="ContentLabel"  align="left">
                        <asp:Label ID="lblOpenCases" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="40%" class="ContentLabel"  align="left">
                        <strong>Number of Bills </strong>
                    </td>
                    <td width="1%">
                        <strong>:</strong>
                    </td>
                    <td width="40%" class="ContentLabel"  align="left">
                        <asp:Label ID="lblNumberOfBill" runat="server"></asp:Label>
                    </td>
                </tr>
                
                              <tr>
                               <td style="height: 84px" valign="top" align="left" class="ContentLabel">
                        <strong>Total Bill </strong>
                    </td>
                    <td width="1%" style="height: 84px" valign="top">
                        <strong>:</strong>
                    </td>
                    <td width="40%" align="left" style="height: 84px">
                        <table>
                            <tr>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <strong>User Cost </strong>
                                </td>
                                <td width="1%" class="ContentLabel" >
                                    <strong>:</strong>
                                </td>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <asp:Label ID="lblUserCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <strong>Open Case Cost </strong>
                                </td>
                                <td width="1%" class="ContentLabel" >
                                    <strong>:</strong>
                                </td>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <asp:Label ID="lblOpenCaseCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <strong>Bill Cost </strong>
                                </td>
                                <td width="1%" class="ContentLabel" >
                                    <strong>:</strong>
                                </td>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <asp:Label ID="lblBillCost" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <strong>Total</strong></td>
                                <td width="1%" class="ContentLabel" >
                                    <strong>:</strong>
                                </td>
                                <td width="40%" align="left" class="ContentLabel" >
                                    <asp:Label ID="lblTotalBill" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                              </tr>             
                                         
                                         
                                           
                                        </table>
                                    
                                    </td>
                                </tr>
                            
                                
                               
                               
                            </table>
    
</td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill Company Details</title>
       <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="frmBillingCompany" runat="server">
        <div align="center">          
            <table cellpadding="0" cellspacing="0" class="simple-table">
            		<tr>
			            <td width="9%" height="18" >&nbsp;</td>
		                <td colspan="2" background="Images/header-bg-gray.jpg"><div align="right"><span class="top-menu"></span></div></td>
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
		  <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center"><span class="message-text"></span></td>
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
	    <td colspan="4" >
		  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
		  
		   <tr>
                    <td class="usercontrol" colspan="3">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
		     <tr>
              <th width="9%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
              <th scope="col" style="height: 20px"><div align="left" class="band">Company Details</div></th>
              <th width="8%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
            </tr>       
                  
            <tr>
              <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
              <div align="left">              
              <table width="55%"  border="0" align="center" cellpadding="0" cellspacing="3">
                
               <tr>
                   
                </tr>
              
              </table> 
              </div> 
              </th> 
              </tr> 
              
		  </table> 
		  </td> 
		  </tr>  
                
               
              
                
               
                
               
               
            </table>
        </div>
    </form>
</body>
</html>--%>
