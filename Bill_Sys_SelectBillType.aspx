<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_SelectBillType.aspx.cs" Inherits="Bill_Sys_SelectBillType" %>

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

		.blocktitle_lm{
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
		
		.block{
			width:100%; 
			height: 200px;
			vertical-align:middle;
		}
		
		DIV#m_content_adv1{
			position:absolute;
			left:1000px;
			top:105px;
			width:270px;
		}

		DIV#m_content_adv2{
			position:absolute;
			left:1000px;
			top:290px;
			width:270px;
		}

		.m_content{
			position:absolute;
			left:225px;
			top:100px;
			border:1px solid;
			width:inherit;
		}
		
		.div_messages{
			font:Arial, Helvetica, sans-serif;
			font-family:Arial, Helvetica, sans-serif;
			font-size:12px;
			font-weight:bold;
			width:100%;
			height:20px;
			padding-top:10px;
			text-align:center;
			/*visibility:hidden*/
		}
		
		.div_blockcontent{
			width:100%; 
			vertical-align:middle; 
			background-color:#ffffff;

			/*padding-right:10%;*/
		}
		
		.div_blockcontent_field_label{
		
		}
		
		DIV#div_content_adv{
			border-color:#8ba1ca;
			background-color:#FFFFFF;
			height:200px;
			border:1px;
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
		INPUT#selectbox{
			width:inherit;
		}
		DIV#div_contect_field_buttons{
			position:inherit;
			border:0px;
			height:40px;
			text-align:center;
			background-color:#F0F0F0;
			padding-top:5px;
		}
		.btnsignin{
			background-color:#CCCCCC;
			color:#000000;
			font-size:12px;
			font-weight:bold;
			height:22px;
			border:1px solid;
			text-align:center;
			padding-bottom:4px;
		}
		
		.lcss_maintable{
			width:100%;
			height:2%;
			cellpadding:0; 
			cellspacing:0; 
			border: 1px solid #D8D8D8;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td width="432%" colspan="6" class="usercontrol">
                       
                    </td>
                </tr>
                <tr>
                
                  
                <td colspan="6" style="height: 454px; vertical-align:top;">
				  <table width="100%" id="content_table"  border="0" cellspacing="0" cellpadding="0">
              
                
                <tr>
                 <td width="15%" valign="top" scope="col" style="height: 229px">&nbsp;						  
					  </td>
		
		     <td width="69%" scope="col">
						<div class="blocktitle">
                        	Select Case Type
                        <div class="div_blockcontent">
                        <table width="100%">
                            <tr>
                            
                            <th width="100%" height="60" colspan="6" align="center" valign="top" style="height: 60px" scope="col">
                                    <div align="left">
                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <!-- Start : Data Entry -->
                                            <tr>
                                                <td colspan="6" height="26">
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                              </td>
                                            </tr>
                                            <tr>
                                             <td height="80" colspan="6" valign="top" style="height: 80px; vertical-align:top; text-align:center;">
                            
                               <div class="lbl" style="text-align:center;"> Select Case Type&nbsp;
                                   <asp:DropDownList ID="drpSelectBill" runat="server" Width="170px" AutoPostBack="true" OnSelectedIndexChanged="drpSelectBill_SelectedIndexChanged">
                                       <asp:ListItem Value="00">--Select--</asp:ListItem>
                                       <asp:ListItem Value="01">Doctor's Initial Report</asp:ListItem>
                                       <asp:ListItem Value="02">Doctor's Progress Report</asp:ListItem>
                                       <asp:ListItem Value="03">Doctor's Report Of MMI</asp:ListItem>
                                   </asp:DropDownList></td>
                                            </tr> 
                                      </table> 
                              </div> 
                              </th> 
                            
                            
                            </tr> 
                            </table> 
                            </div> 
                            </div> 
                            </td> 			  
                   
                     <td width="16%" valign="top" scope="col" style="height: 229px">&nbsp;
                        
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
