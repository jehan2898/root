<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkerTemplate.aspx.cs"
    Inherits="WorkerTemplate" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
        <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />



    <script type="text/javascript">
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
<body topmargin="0" style="text-align: center">
    <form id="frmWorkCompension" runat="server">
        <div align="center">
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td height="20px" colspan="6" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                        </span>
                    </td>
                </tr>
                
                
                
                <tr>
                    <td colspan="6" style="height: 454px">
                        <table width="100%" id="content_table" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="15%" valign="top" scope="col">
                                    <div class="blocktitle_ql">
                                        <div align="left" class="blocktitle_adv">
                                            Jump To
                                        </div>
                                        <div class="div_blockcontent">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <ul style="font-family:Arial, Helvetica, sans-serif;font-size:12px;">
												<li>
													<a href="PatientInformation.aspx">
														Patient Information
													</a>
												</li>

												<li>
													<a href="WorkerTemplate.aspx">
														Employer Information
													</a>
												</li>

												<li>
													<a href="DoctorsInformation.aspx">
														Doctor's Information
													</a>
												</li>

												<li>
													<a href="BillingInformation.aspx">
														Billing Information
													</a>
												</li>

												<li>
													<a href="History.aspx">
														History
													</a>
												</li>
												<li>
													<a href="ExamInformation.aspx">
														Exam Information
													</a>
												</li>
												<li>
													<a href="DoctorOpinion.aspx">
														Doctor's Opinion
													</a>
												</li>
												<li>
													<a href="PlanOfCare.aspx">
														Plan Of Care
													</a>
												</li>
												<li>
													<a href="WorkStatus.aspx">
														Work Status
													</a>
												<br />
												</li>
											</ul>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                                
                               <td width="69%" scope="col">
						<div class="blocktitle">
                        	Employer Information
                        <div class="div_blockcontent">
                          <table>
                            <tr>
                             <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                    
                        <table>
                            <tr>
                            
                                <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                    <div align="left">
                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td colspan="3" scope="col">
                                                   
                                                      
                                                        <asp:Panel ID="pnlEmployerInfo" runat="server" >
                                                            
                                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                    <tr>
                                                                        <td class="tablecellLabel">
                                                                              <div class="lbl">Company/Agency Name</div> 
                                                                        </td>
                                                                        <td class="tablecellSpace">
                                                                        </td>
                                                                        <td class="tablecellControl">
                                                                            <asp:TextBox ID="txtEmployerName" runat="server" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                        <td class="tablecellLabel">
                                                                             <div class="lbl"> Phone Number</div> 
                                                                        </td>
                                                                        <td class="tablecellSpace">
                                                                        </td>
                                                                        <td class="tablecellControl">
                                                                            <asp:TextBox ID="txtEmployerPhone" runat="server" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tablecellLabel">
                                                                              <div class="lbl">Number and Street</div> 
                                                                        </td>
                                                                        <td class="tablecellSpace">
                                                                        </td>
                                                                        <td class="tablecellControl">
                                                                            <asp:TextBox ID="txtEmployerAddress" runat="server" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                        <td class="tablecellLabel">
                                                                             <div class="lbl"> City</div> 
                                                                        </td>
                                                                        <td class="tablecellSpace">
                                                                        </td>
                                                                        <td class="tablecellControl">
                                                                            <asp:TextBox ID="txtEmployerCity" runat="server" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="tablecellLabel">
                                                                             <div class="lbl"> State</div> 
                                                                        </td>
                                                                        <td class="tablecellSpace">
                                                                        </td>
                                                                        <td class="tablecellControl">
                                                                            <cc1:ExtendedDropDownList id="txtEmployerState" runat="server" Width="90%" Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---">
                                                                </cc1:ExtendedDropDownList></td>
                                                                        <td class="tablecellLabel">
                                                                              <div class="lbl">Zip Code</div> 
                                                                        </td>
                                                                        <td class="tablecellSpace">
                                                                        </td>
                                                                        <td class="tablecellControl">
                                                                            <asp:TextBox ID="txtEmployerZip" runat="server" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                          
                                                        </asp:Panel>
                                                        <div id="div_contect_field_buttons">
                                                            <table>
                                                                <tr>
                                                                    <td colspan="4" width="100%" align="center">
                                                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                        <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                        <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px"
                                                                            OnClick="btnSaveAndGoToNext_Click" CssClass="btn-gray" />
                                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                                                                            CssClass="btn-gray" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                   
                                                </td>
                                            </tr>
                                            </table> 
                                        </div> 
                                    </th>
                                                </tr> 
                          </table>                           
                                       </th>
                            </tr>
                        </table>
                            </div>
                        </div>					  
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
