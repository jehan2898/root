<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_BillTransactionEntry.aspx.cs"
    Inherits="Bill_Sys_BillTransactionEntry"%>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl"  %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
      <%@ Register Src="UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>
    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <script src="calendarPopup.js"></script>

    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();
    </script>
    
    <script language="javascript" type="text/javascript" >
        function Amountvalidate()
			{
			   var status=formValidator('frmBillTrans','txtBillNo,extddlIC9Code,txtUnit,txtAmount,txtWriteOff,txtDescription');
		      if (status!=false)
		      {
			   
			    if (document.getElementById('txtAmount').value > 0 )//&& document.getElementById('txtAmount')!= '' && document.getElementById('txtAmount')!= '0.00')
			    {
			        return  true;
			    }
			    else
			    {
			           
			        document.getElementById('ErrorDiv').innerHTML='Enter the amount greater than 0';							
					document.getElementById('txtAmount').focus();
			        return  false;
			    }
			  }
			  else
			  {
			    
			    return  false;
			    }
			}
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
         
         function ShowGrid()
         {
            document.getElementById('divCollapsablegrid').style.visibility="visible";
         }
         
         function off(){
            document.getElementById('divCollapsablegrid').style.visibility="visible";
         }
         
          function CalculateAmount()
            {
                var txtAmount = document.getElementById('txtAmount');
                var txtUnit = document.getElementById('txtUnit');
               var tempAmt =  document.getElementById('txtTempAmt');
                if(txtAmount.value!="")
                {
                    txtAmount.value = tempAmt.value * txtUnit.value;
                }        
            } 
            
            function clickButton1(e,charis)
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
	<style type="text/css">
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

		DIV#div_contect_field_buttons{
			position:inherit;
			border:0px;
			height:40px;
			text-align:center;
			background-color:#F0F0F0;
			padding-top:5px;
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

<body onload="off();" topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="frmBillTrans" runat="server">
         <div>
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
                            Height="24px" DynamicMenuItemStyleCSS="sublevel1" StaticMenuItemStyleCSS="parentlevel1"></cc2:WebCustomControl1>
	                  
	                  </td>
	                  <td width="8%" class="top-menu">&nbsp;</td>
	              </tr>
	             
	                 <tr>
		  <td class="top-menu" id="anch_top">&nbsp;</td>
		  <td height="35px" bgcolor="#000000"><div align="left"></div>		    
	      <div align="left"><span class="pg-bl-usr">Billing company name</span></div></td>
		  <td width="12%" height="35px" bgcolor="#000000"><div align="right"><span class="usr">Admin</span></div></td>
		  <td class="top-menu">&nbsp;</td>
	  </tr>
	<tr>
		  <td class="top-menu" style="height: 20px">&nbsp;</td>
		  <td colspan="2" bgcolor="#EAEAEA" align="center" style="height: 20px"><span class="message-text"><asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label></span></td>
		  <td class="top-menu" style="height: 20px">&nbsp;</td>
	  </tr>  
	  	<tr>
		  <td class="top-menu">&nbsp;</td>
		  <td height="18" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
		  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <th width="19%" scope="col" style="height: 29px">
              <div align="left"><span class="pg">&gt;&gt; Home >> Bill Transaction</span></div></th>
              <th width="81%" scope="col" style="height: 29px"><div align="right"><span class="sub-menu">
					<asp:LinkButton ID="lnlInitialReport" runat="server" Visible="false" OnClick="lnlInitialReport_Click">Edit W.C. 4.0 | </asp:LinkButton>
                  <%--	<asp:LinkButton ID="lnkCopyOldrogressReport" runat="server" Visible="false">Edit W.C. 4.2 |</asp:LinkButton> --%>
                  	<asp:LinkButton ID="lnlProgessReport" runat="server" Visible="false" OnClick="lnlProgessReport_Click">Edit W.C. 4.2</asp:LinkButton>
					<asp:LinkButton ID="lnkReportOfMMI" runat="server" Visible="false">Edit W.C. 4.3</asp:LinkButton>
              </span></div></th>
            </tr>
          </table>
     </td>
		  <td class="top-menu" colspan="3">&nbsp;</td>
	  </tr>
	  
	  <tr>
	    <td colspan="4" scope="col">
		  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
                    <td width="9%">&nbsp;</td>
                    <td align="left" style="background-color:#D2D2D6;"> <CI:Bill_Sys_Case ID="UCCaseInfo" runat="server"></CI:Bill_Sys_Case></td>
                    <td width="8%">&nbsp;</td>
               </tr> 
	        <tr>
              <th width="9%" rowspan="6" align="left" valign="top" scope="col">&nbsp;</th>
              <th scope="col" style="height: 20px; "><div align="left" class="band">Bill Transaction</div></th>
              <th width="8%" rowspan="6" align="left" valign="top" scope="col">&nbsp;</th>
            </tr>          
              <tr>
                <td align="left" valign="top" bgcolor="E5E5E5" scope="col">
				<table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td align="left" valign="top" scope="col">
						<div class="blocktitle">
							Associated Diagnosis Codes <a href="#anch_top">(Top)</a>
							<div class="div_blockcontent">
								<table width="80%" border="0" align="center">
									<tr>
										<td>
											<asp:DataGrid ID="grdAssociatedDiagnosisCode" runat="server" Visible="false" OnSelectedIndexChanged="grdAssociatedDiagnosisCode_SelectedIndexChanged" >
												<FooterStyle />
												<SelectedItemStyle />
												<PagerStyle />
												<AlternatingItemStyle />
												<ItemStyle />
												<Columns>
													<asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
													</asp:ButtonColumn>
													<asp:BoundColumn DataField="SZ_DIAGNOSIS_SET_ID" HeaderText="Set ID" Visible="False">
													</asp:BoundColumn>
													<asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor Code" Visible="False"></asp:BoundColumn>
													
													<asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
													<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code" >
													</asp:BoundColumn>
													<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis Code" Visible="False"></asp:BoundColumn>
												</Columns>
												<HeaderStyle />
											</asp:DataGrid>
										</td>
									</tr>
								</table>
							</div>
						</div>
					</td>
                  </tr>
                  <tr>
                    <td align="left" valign="top" scope="col">&nbsp;</td>
                  </tr>
                  <tr>
                    <td align="left" valign="top" scope="col">
					<div class="blocktitle">
						Bill Details <a href="#anch_top">(Top)</a>
						<div class="div_blockcontent">
							<table width="80%" border="0" align="center">
								<tr>
									<td valign="top"><table width="80%" border="0" align="center">
                                      <tr>
                                        <td><div align="right"><span class="lbl">Case ID</span></div></td>
                                        <td valign="middle"><asp:TextBox ID="txtCaseID" runat="server" BorderStyle="None" Style="border-top-style: none;
                            border-right-style: none; border-left-style: none; border-bottom-style: none"
                            BorderColor="Transparent" ReadOnly="True"></asp:TextBox></td>
                                      </tr>
                                      <tr>
                                        <td><div align="right">Bill Date </div></td>
                                        <td align="left" valign="top">                                            <%--<asp:LinkButton ID="btnOn" runat="server" OnClick="btnOn_Click">On</asp:LinkButton>
                                /<asp:LinkButton ID="btnFromDate" runat="server" OnClick="btnFromTo_Click">From To</asp:LinkButton>&nbsp;--%>
                                            &nbsp;
                                            <asp:TextBox ID="txtBillDate" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox> 
                                          <asp:ImageButton ID="imgbtnOpenedDate" runat="server" ImageUrl="~/Images/cal.gif" />                                                 
                                        <div   class="lbl"> </div></td></tr>
                                      <tr>
                                        <td><div align="right">Doctor </div></td>
                                        <td>
											<extddl:ExtendedDropDownList  ID="extddlDoctor" runat="server" Width="200px" Connection_Key="Connection_String" Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlDoctor_extendDropDown_SelectedIndexChanged" />
										</td>
                                      </tr>
                                      <tr>
                                        <td valign="top"><div align="right"></div></td>
                                        <td><asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtTransDetailID" runat="server" Width="10px" Visible="False"></asp:TextBox></td>
                                      </tr>
                                      <tr>
                                        <td colspan="2" valign="top">
										<div id="div_contect_field_buttons">
											<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"  cssclass="btn-gray"/>
											<asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="80px" cssclass="btn-gray"/>
											<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"  cssclass="btn-gray"/>
										    <asp:Button ID="btnForceEntry" runat="server" Visible="false" OnClick="btnForceEntry_Click" Text="Force Entry"
                                 />                                            
										</div></td>
                                      </tr>
                                    </table>
										
									</td>
								</tr>
							</table>
							</div>
						</div>
					</td>
                  </tr>
				  <tr>
				  	<td>&nbsp;
						
					</td>
				  </tr>
                  <tr>
                    <td align="left" valign="top" scope="col">
					<div class="blocktitle">
							Bill Services <a href="#anch_top">(Top)</a>
							<div class="div_blockcontent">
							<table width="80%" border="0" align="center" id="tblServices" runat="server">
								<tr>
									<td>
										<div align="right">Bill Number
								    </div></td>
									<td>
										<asp:TextBox ID="txtBillNo" runat="server" Width="250px" MaxLength="50" ReadOnly="true" ></asp:TextBox>
									</td>
								</tr>
								<tr>
								  <td><div align="right">Date of Service</div></td>
								  <td>
										<asp:RadioButton ID="rdoOn" runat="server" Text="On" Checked="True" GroupName="OnFromTO" AutoPostBack="true" OnCheckedChanged="rdoOn_CheckedChanged" />
										<asp:RadioButton ID="rdoFromTo" runat="server" Text="From To" GroupName="OnFromTO" AutoPostBack="true" OnCheckedChanged="rdoFromTo_CheckedChanged" />
                                <%--<asp:LinkButton ID="btnOn" runat="server" OnClick="btnOn_Click">On</asp:LinkButton>
                                /<asp:LinkButton ID="btnFromDate" runat="server" OnClick="btnFromTo_Click">From To</asp:LinkButton>&nbsp;--%>										 &nbsp;<div   class="lbl"> 
								  </td>
							  </tr>
								<tr>
								  <td>&nbsp;</td>
								  <td valign="top"><asp:Label ID="lblDateOfService" runat="server" Text="From" Font-Bold="False" Visible="false"></asp:Label>
							      <asp:TextBox ID="txtDateOfservice" runat="server" Width="120px" MaxLength="50" ReadOnly="false" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
							      <asp:ImageButton ID="imgbtnDateofService" runat="server" ImageUrl="~/Images/cal.gif" />
							      <asp:Label ID="lblTo" runat="server" Text="To" Visible="false"></asp:Label>
							      <asp:TextBox ID="txtDateOfServiceTo" runat="server" MaxLength="50" onkeypress="return CheckForInteger(event,'/')"
											ReadOnly="false" Width="120px" Visible="false"></asp:TextBox>
							      <asp:ImageButton ID="imgbtnFromTo" runat="server" ImageUrl="~/Images/cal.gif" Visible="false"/></td>
							  </tr>
								<tr>
								  <td><div align="right">Diagnosis Code </div></td>
								  <td><extddl:ExtendedDropDownList ID="extddlDiagnosisCode" runat="server" Width="100%" AutoPost_back="True"
                            Connection_Key="Connection_String" Flag_Key_Value="GET_DIAGNOSIS_CODE_LIST"
                            Procedure_Name="SP_MST_DIAGNOSIS_CODE" Selected_Text="---Select---" OnextendDropDown_SelectedIndexChanged="extddlIC9Code_extendDropDown_SelectedIndexChanged" /></td>
							  </tr>
								<tr>
								  <td valign="top"><div align="right">Procedure Code </div></td>
								  <td><asp:ListBox ID="lstProcedureCode" runat="server" SelectionMode="Multiple"
                                                        Width="100%" Height="160px"></asp:ListBox></td>
							  </tr>
								<tr>
								  <td valign="top"><div align="right">Unit</div></td>
								  <td><asp:TextBox ID="txtUnit" runat="server" Width="150px" onkeypress="return CheckForInteger(event,'.')" ></asp:TextBox></td>
							  </tr>
								<tr>
								  <td colspan="2" valign="top">
									  <div id="div_contect_field_buttons">
										<asp:TextBox ID="txtAmount" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                		<asp:Button ID="btnAddService" runat="server" Text="Add" Width="80px"  cssclass="btn-gray" OnClick="btnAddService_Click"/>
                                		<asp:Button ID="btnUpdateService" runat="server" Text="Update" Width="80px"  cssclass="btn-gray" OnClick="btnUpdateService_Click" />
                                		<asp:Button ID="btnClearService" runat="server" Text="Clear" Width="80px"  cssclass="btn-gray" OnClick="btnClearService_Click"/>
									  </div>								  
								  </td>
							  </tr>
							</table>
							</div>
					</div>
					</td>
                  </tr>
                  <tr>
                    <td align="left" valign="top" scope="col">&nbsp;</td>
                  </tr>
                  <tr>
                    <td align="left" valign="top" scope="col">
						<div class="blocktitle">
							Existing Services on the bill <a href="#anch_top">(Top)</a>
							<div class="div_blockcontent">
								<table width="100%" border="0" align="center" runat="server">
									<tr>
										<td>
											<asp:DataGrid ID="grdTransactionDetails" runat="server" ShowFooter="True" AllowPaging="True" PageSize="5" OnSelectedIndexChanged="grdTransactionDetails_SelectedIndexChanged" AutoGenerateColumns="true" Width="695px" OnPageIndexChanged="grdTransactionDetails_PageIndexChanged"  >
													<PagerStyle Mode="NumericPages" />
													<Columns>
															<asp:ButtonColumn CommandName="Select" Text="Select">
																<ItemStyle CssClass="grid-item-left" />
															</asp:ButtonColumn>

															<asp:BoundColumn DataField="SZ_BILL_TXN_DETAIL_ID" HeaderText="Transaction Detail ID" Visible="False">
															</asp:BoundColumn>
																			
															<asp:BoundColumn DataField="SZ_BILL_NUMBER" Visible="False" HeaderText="Bill Number">                              
															</asp:BoundColumn>

															<asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="DT_DATE_OF_SERVICE"  DataFormatString="{0:MM/dd/yyyy}">
															</asp:BoundColumn>

															<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis Code ID" Visible="False"></asp:BoundColumn>
															<asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
																			
															<asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Procedural Code ID" Visible="False"></asp:BoundColumn>
															<asp:BoundColumn DataField="SZ_PROCEDURAL_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
																		  
															<asp:BoundColumn DataField="FL_AMOUNT" HeaderText="Amount" DataFormatString="{0:0.00}"></asp:BoundColumn>
																			
															<asp:BoundColumn DataField="I_UNIT" HeaderText="Unit" ></asp:BoundColumn>																	  
														</Columns>
											</asp:DataGrid>										
										</td>
									</tr>
								</table>
							</div>
						</div>
					</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <th align="center" valign="top" bgcolor="E5E5E5" scope="col"  >&nbsp;</th>
              </tr>
              <tr>
              <th align="center" valign="top" width="83%" bgcolor="E5E5E5" scope="col"  >
              <div align="left">              </div> 
              </th> 
              </tr> 
              <tr>
            </tr>      
           <tr>
		   		<td align="left" valign="top" scope="col">
				<div class="blocktitle">
							Last 3 bills of the patient <a href="#anch_top">(Top)</a>
							<div class="div_blockcontent">
									<table width="100%" border="0" align="center" runat="server">
										<tr>
											<td>
												<asp:DataGrid ID="grdLatestBillTransaction" runat="server" OnSelectedIndexChanged="grdLatestBillTransaction_SelectedIndexChanged"
													OnItemCommand="grdLatestBillTransaction_ItemCommand" OnItemDataBound="grdLatestBillTransaction_ItemDataBound">
													<FooterStyle />
													<SelectedItemStyle />
													<PagerStyle />
													<AlternatingItemStyle />
													<ItemStyle />
													<Columns>
														<asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
														</asp:ButtonColumn>
														<%--<asp:BoundColumn DataField="SZ_BILL_ID" HeaderText="Bill ID" Visible="false"></asp:BoundColumn>--%>
														<asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number" ></asp:BoundColumn>
														<asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID"></asp:BoundColumn>
														<asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" DataFormatString="{0:MM/dd/yyyy}">
														</asp:BoundColumn>
														<asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" ItemStyle-HorizontalAlign="Right"
															DataFormatString="{0:0.00}"></asp:BoundColumn>
														<asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" ItemStyle-HorizontalAlign="Right"
															DataFormatString="{0:0.00}"></asp:BoundColumn>
														<asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
															DataFormatString="{0:0.00}"></asp:BoundColumn>
														<asp:TemplateColumn HeaderText="Make Payment">
															<ItemTemplate>
																<asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
																	CommandName="Make Payment"></asp:LinkButton>
															</ItemTemplate>
														</asp:TemplateColumn>
						
														<asp:BoundColumn DataField="BIT_WRITE_OFF_FLAG" HeaderText="WRITEOFFFLAG" Visible="false" ></asp:BoundColumn>
														
													   <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor" Visible="false" > </asp:BoundColumn>
													   
													   <asp:TemplateColumn HeaderText="">
															<ItemTemplate>
															   
																	 <asp:LinkButton ID="lnkInitialReport" runat="server" Text="Edit W.C. 4.0 "
																	CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>' CommandName="Doctor's Initial Report"> </asp:LinkButton>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" />
														</asp:TemplateColumn>
														
														<asp:TemplateColumn HeaderText="">
															<ItemTemplate>
															   
																	 <asp:LinkButton ID="lnkProgressReport" runat="server" Text="Edit W.C. 4.2  "
																	CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>' CommandName="Doctor's Progress Report"> </asp:LinkButton>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" />
														</asp:TemplateColumn>
														
														<asp:TemplateColumn HeaderText="">
															<ItemTemplate>
															   
																	 <asp:LinkButton ID="lnkMIReport" runat="server" Text="Edit W.C. 4.3 "
																	CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>' CommandName="Doctor's Report Of MMI"> </asp:LinkButton>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" />
														</asp:TemplateColumn>
													   
														<asp:TemplateColumn HeaderText="Generate bill">
															<ItemTemplate>
															   
																	 <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Add Bills"
																	CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>' CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
															</ItemTemplate>
															<ItemStyle HorizontalAlign="Center" />
														</asp:TemplateColumn>
													</Columns>
													<HeaderStyle />
												</asp:DataGrid>
											</td>
										</tr>
									</table>
								</div>
							</div>
                            </td>
                        </tr>
           <tr>
             <th align="left" valign="top" scope="col">&nbsp;</th>
             <td align="left" valign="top" scope="col">
             <table width="53%"  border="0" align="center" cellpadding="0" cellspacing="3">
               <tr>
                 <td scope="col" width="100" style="height: 30px">
                 <div id="ErrorDiv" style="color: red" visible="true" runat="Server"> &nbsp;</div></td>
               </tr>
               <tr>
                 <td scope="col" height="30" width="100">&nbsp;</td>
               </tr>
               <tr>
                 <td scope="col" height="30" width="100"><div id="Div1"  visible="false" runat="Server"><br />
                 </div></td>
               </tr>
             </table></td>
             <th align="left" valign="top" scope="col">&nbsp;</th>
           </tr>
          </table> 
        </td> 
          </tr>
            </table>
        </div>
    </form>
</body>
</html>

<!--

	                        <div id="divCollapsablegrid" visible="false"></div>
							
-->