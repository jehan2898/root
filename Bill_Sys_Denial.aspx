<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Denial.aspx.cs" Inherits="Bill_Sys_Denial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Billing System</title>
    <script type="text/javascript" src="validation.js"></script>

   
     <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
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
    <form id="femDenial" runat="server">
    <div align="center">
    
    

				  <table width="100%" id="content_table"  border="0" cellspacing="0" cellpadding="0" >
				  <tr>
                                <td colspan="3">
                                    Denials
                                </td>
                            </tr>
                    <tr>
                    
                    <td width="83%" scope="col" style="vertical-align:top; ">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0"  >
                            
                        
                            <tr>
                                <td colspan="3" scope="col" style="height: 233px;width:310px;" >
                                
                                    
                                   
                                      
                                
                 
                                  
                                        <div id="div_contect_field_buttons">
                                            &nbsp;<asp:DataGrid ID="grdDenial" runat="server"   Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  >
                                        <FooterStyle />
                                        <SelectedItemStyle />
                                        <PagerStyle />
                                        <AlternatingItemStyle />
                                       <ItemStyle CssClass="GridRow"/>
                                        <Columns>
                                            <asp:BoundColumn DataField="I_DENIEL" HeaderText="I_DENIEL" Visible="false" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DENIAL" HeaderText="Denials" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="REMARK" HeaderText="Denial Reason" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DENIALDATE" HeaderText="Denial Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                        </Columns>
                                         <HeaderStyle CssClass="GridHeader"/>
                                    </asp:DataGrid>
                                <br />
                          <asp:Label ID="lblDenial" runat="server"  style="font-size:12px;" Text=""></asp:Label><br />
                                     <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            <table>
                                                <tr>
                                                    <td colspan="4" width="100%" align="center">
                                  <asp:Button ID="btnOK" runat="server" Text="Ok" Width="80px" OnClick="btnOK_Click" CssClass="Buttons" />
                                  <input id="Button1" type="button" value="Cancel" onclick="javascript:parent.document.getElementById('divid').style.visibility = 'hidden';" class="Buttons" />
                            
                         
                                                        </td>
                                                        
                                                </tr>
                                            </table>
                                        </div>
                                   
                                   
                                    </td></tr>
                           
                        </table>
                   
                 
    
   
    </div>
    </form>
</body>
</html>
