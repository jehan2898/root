<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_PermanentImpairementC4_3.aspx.cs" Inherits="Bill_Sys_PermanentImpairementC4_3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>   

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" type="text/css" rel="stylesheet" />
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
 <script src="calendarPopup.js"></script>	
    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();	
	
    </script>

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

</head>
<body>
    <form id="frmWorkStatus" runat="server">
        <div align="center">
            <table cellpadding="0px" cellspacing="0px">
       <%--         <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg">
                        <div align="right">
                            <span class="top-menu"></span></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg" class="top-menu">&nbsp;
                        </td>
                </tr>
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg" class="top-menu">&nbsp;
                        </td>
                </tr>
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="100%" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
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
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th height="29" width="19%" scope="col">
                                    <div align="left">
                                        <span class="pg">&gt;&gt; Home >> Return To Work </span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization2" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="6" style="height: 37px">
                        <div align="left" class="band">
                           WC 4.3  
                        </div>
                    </td>
                </tr>
                
                
              
               <tr>
                  <td colspan="6" style="height: 454px">
				  <table width="100%" id="content_table"  border="0" cellspacing="0" cellpadding="0">
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
												
                                    
													<a href="Bill_Sys_PatientInformationC4_3.aspx">
														Patient Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_DoctorInformationC4_3.aspx">
														Doctor Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_BillingInformationC4_3.aspx">
														Billing Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_PermanentImpairementC4_3.aspx">
														Permaanent Impairment/ Work Status
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
                        	Return To Work
                        <div class="div_blockcontent">
                        <table>
                            <tr>
                                <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                    <div align="left">
                                  <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                
                                                <tr>
                                                    <td colspan="4" style="height: 30px">
                                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                        1.</td>
                                                    <td style="height: 24px; " colspan="2">
                                                        <div class="lbl">
                                                            Has patient reached Maximum Medical Improvement ?</div>
                                                    </td>
                                                  
                                                    <td style="height: 24px">
                                                        <div class="lbl">
                                                           <%-- <asp:CheckBoxList ID="chklstPatientMedicalImprovement" runat="server"
                                                                RepeatDirection="Horizontal" >
                                                                <asp:ListItem Value=1>Yes</asp:ListItem>
                                                                <asp:ListItem Value=0>No</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                        <asp:RadioButtonList ID="rdlstPatientMedicalImprovement" runat="server" RepeatDirection="Horizontal">                                                        
                                                        <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="1">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        </div>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px; height: 26px">
                                                    </td>
                                                    <td style="height: 26px; " colspan="2">
                                                      <div class="lbl">  if yes, Provide the date patient reached MMI:</div> </td>
                                                 
                                                    <td style="height: 26px">
                                                        <asp:TextBox ID="txtPatientReachedMMI" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnMMI" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtPatientReachedMMI" runat="server" TargetControlID="txtPatientReachedMMI"
                                                                                                        PopupButtonID="imgbtnMMI" />
                                                        </td>
                                                        
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px; height: 52px;">
                                                    </td>
                                                    <td style="height: 52px; "  colspan="2">
                                                       <div class="lbl"> if there permanent impairment?
                                                       </div> 
                                                       </td>
                                                   
                                                    <td style="height: 52px">
                                                       <div class="lbl"> <%--<asp:CheckBoxList ID="chkPermanentImpairement"  runat="server"
                                                                RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                           <asp:RadioButtonList ID="rdlstPermanentImpairement" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                           </asp:RadioButtonList>
                                                           
                                                           </div> 
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="height: 24px;"  colspan="3">
                                                       <div class="lbl"> if yes, Check the boxes that apply</div> </td>
                                                  
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="height: 24px" colspan="3">
                                                    <div class="lbl">    <asp:CheckBox ID="chkScheduleLoss" runat="server" Text="Schedule loss of use of member: (Identity impairment rating according to NY impairment Guidelines and attach separate sheet for additional body parts.)" /></div> </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="height: 24px; width: 221px;">
                                                      <div class="lbl">  Body Parts</div> </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtSLBodyParts" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 221px; height: 24px">
                                                       <div class="lbl"> Impairment %</div> </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtSLImpairment" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 221px; height: 24px">
                                                     <div class="lbl">   Patien Name</div> </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 221px; height: 24px">
                                                        <asp:TextBox ID="txtPatienFirstName" runat="server" ReadOnly="false" ></asp:TextBox></td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtPatienMiddleName" runat="server" ReadOnly="false"></asp:TextBox></td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtPatienLastName" runat="server" ReadOnly="false"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 221px; height: 24px">
                                                      <div class="lbl">  First Name</div> </td>
                                                    <td style="height: 24px">
                                                      <div class="lbl">  Middle Name</div> </td>
                                                    <td style="height: 24px">
                                                      <div class="lbl">  Last Name</div> </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 221px; height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 221px; height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                      <div class="lbl">  Date of injury/illness</div> </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtDateOfInjury" runat="server" ReadOnly="false" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style=" height: 24px" colspan="3">
                                                     <div class="lbl">   Describe findings and relevant diagnotic test results:</div> </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px; height: 38px;">
                                                    </td>
                                                    <td style="width: 160px; height: 38px" colspan="3">
                                                        <asp:TextBox ID="txtSLDignosticFinding" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                                 
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style=" height: 24px"  colspan="3">
                                                      <div class="lbl">  Explain how impairement % was determined:</div> </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px; height: 26px;">
                                                    </td>
                                                    <td style="width: 160px; height: 26px" colspan="3">
                                                        <asp:TextBox ID="txtSLImpairmentDetermine" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                                  
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td colspan="3">
                                                     <div class="lbl">   <asp:CheckBox ID="chkDisfigurement" runat="server" Text="Disfigurement: (Describe findings)" /></div> </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtDisfigurementDesc" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td colspan="3">
                                                     <div class="lbl">   <asp:CheckBox ID="chkNonScheduleLoss" runat="server" Text="Non-Schedule losses: (Identity impairment rating according to NY impairment Guidelines and attach separate sheet for additional body parts.)" /></div> </td>
                                                </tr>
                                                      <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="height: 24px; width: 221px;">
                                                      <div class="lbl">  Body Parts</div> </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtNSLBodyParts" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 221px; height: 24px">
                                                    <div class="lbl">    Impairment %</div> </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtNSLImpairment" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox></td>
                                                </tr>
                                                  <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="height: 24px" colspan="3">
                                                       <div class="lbl"> Describe findings and relevant diagnotic test results:</div> </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 160px; height: 24px" colspan="3">
                                                        <asp:TextBox ID="txtNSLDescribeDiagnosis" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                                 
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="height: 24px" colspan="3">
                                                      <div class="lbl">  Explain how impairement % was determined:</div> </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 160px; height: 24px" colspan="3">
                                                        <asp:TextBox ID="txtNSLImpairmentDetermine" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style="width: 160px; height: 24px" colspan="3">
                                                     <div class="lbl">  <asp:CheckBox ID="chkMultipleImpairments" runat="server" Text="For multiple impairments from an injury/illness"  /></div>  </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style=" height: 24px" colspan="2">
                                                     <div class="lbl">   a. Combined aggregate impairment: %</div> </td>
                                                  
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtCombinedAggregate" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td style=" height: 24px"  colspan="2">
                                                     <div class="lbl">   b. Explain how % was determined</div> </td>
                                              
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtExplainAggregateImpairment" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" height="20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td colspan="2" style="height: 24px">
                                                      <div class="lbl">  Is patient working now?</div> </td>
                                                    <td style="height: 24px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px; height: 26px;">
                                                    </td>
                                                    <td style="width: 160px; height: 26px" colspan="3">
                                                    <div class="lbl">
                                                   <%-- <asp:CheckBoxList ID="chklstIsPatienWorking"  runat="server" Width="296px" >
                                                        <asp:ListItem Value="0">Yes at the pre-injury job</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes at other employment</asp:ListItem>
                                                        <asp:ListItem Value="2">Not working</asp:ListItem>
                                                    </asp:CheckBoxList>--%>
                                                        <asp:RadioButtonList ID="rdlstIsPatienWorking" runat="server" RepeatDirection="Horizontal" Width="421px">
                                                            <asp:ListItem Value="0" Selected="True">Yes at the pre-injury job</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes at other employment</asp:ListItem>
                                                        <asp:ListItem Value="2">Not working</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        </div> 
                                                        </td>
                                                  
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px; height: 26px;">
                                                    </td>
                                                    <td colspan="2" style="height: 26px">
                                                      <div class="lbl">  Does Patient have work limitions?</div> </td>
                                                        <td style="height: 26px">
                                                        <div class="lbl">
                                                       <%-- <asp:CheckBoxList ID="chklstPatientLimitation"  runat="server"
                                                                RepeatDirection="Horizontal" >
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                            <asp:RadioButtonList ID="rdlstPatientLimitation" runat="server" RepeatDirection="Horizontal">                                                            
                                                            <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div> 
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td colspan="3">
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td style="width: 56px">
                                                </td> 
                                                    <td colspan="3">   <div class="lbl">
                                                        if yes, then check all of the following that apply:</div></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px;">
                                                    </td>
                                                    <td colspan="3" >
                                                        <div class="lbl">
                                                          <asp:CheckBoxList ID="chklstPatientLimitationAllReason" runat="server" RepeatColumns="3">
                                                                <asp:ListItem Value="0">Bending/twisting</asp:ListItem>
                                                                <asp:ListItem Value="1">Lifting</asp:ListItem>
                                                                <asp:ListItem Value="2">Sitting</asp:ListItem>
                                                                <asp:ListItem Value="3">Climbing stairs/ladders</asp:ListItem>
                                                                <asp:ListItem Value="4">Operating heavy equipment</asp:ListItem>
                                                                <asp:ListItem Value="5">Standing</asp:ListItem>
                                                                <asp:ListItem Value="6">Environmental conditions</asp:ListItem>
                                                                <asp:ListItem Value="7">Operation of motor vehicles</asp:ListItem>
                                                                <asp:ListItem Value="8">Use of public transportation</asp:ListItem>
                                                                <asp:ListItem Value="9">Kneeling</asp:ListItem>
                                                                <asp:ListItem Value="10">Personal protective equipment</asp:ListItem>
                                                                <asp:ListItem Value="11">Use of upper extremities</asp:ListItem>
                                                                <asp:ListItem Value="12">Other (explain):</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                
                                                </tr>
                                                <tr>
                                                 <td style="width: 56px;">
                                                    </td>
                                                    <td style="vertical-align: bottom; padding-bottom: 10px;" colspan="4">
                                                        <asp:TextBox ID="txtOtherLimitation" runat="server" TextMode="MultiLine" Height="80px" Width="420px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td colspan="2" style="height: 24px; text-align: left;">
                                                        <div class="lbl">
                                                            Describe/quantify the limitations:</div>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 56px">
                                                    </td>
                                                    <td colspan="3" style="height: 24px; text-align: left">
                                                        <asp:TextBox ID="txtQuantifyTheLimitaion" runat="server" TextMode="MultiLine" Width="421px" Height="81px"></asp:TextBox></td>
                                                  
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 56px; height: 92px;">
                                                        3.</td>
                                                    <td colspan="2" style="vertical-align: top; height: 92px;">
                                                        <div class="lbl">
                                                            With whom will you discuss the patient's return to work and/or limitations?</div>
                                                    </td>
                                                    <td style="height: 92px">
                                                        <div class="lbl">
                                                            <%--<asp:CheckBoxList ID="chklstDiscussPatientReturn" runat="server">
                                                                <asp:ListItem Value="0">with patient</asp:ListItem>
                                                                <asp:ListItem Value="1">with patient's employer</asp:ListItem>
                                                                <asp:ListItem Value="2">N/A</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                        <asp:RadioButtonList ID="rdlstDiscussPatientReturn" runat="server">
                                                          <asp:ListItem Value="0" Selected="True">with patient</asp:ListItem>
                                                                <asp:ListItem Value="1">with patient's employer</asp:ListItem>
                                                                <asp:ListItem Value="2">N/A</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        </div>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 56px; ">
                                                        4.</td>
                                                    <td colspan="2" style="vertical-align: top; ">
                                                       <div class="lbl"> Would patient benefit from vocational rehabilitation?</div> </td>
                                                    <td >
                                                    <div class="lbl">
                                                   <%-- <asp:CheckBoxList ID="chklstPatientBenefit"  runat="server"
                                                                RepeatDirection="Horizontal" >
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:CheckBoxList>--%>
                                                        <asp:RadioButtonList ID="rdlstPatientBenefit" runat="server" RepeatDirection="Horizontal">
                                                         <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="1">No</asp:ListItem>
                                                        
                                                        </asp:RadioButtonList>
                                                    </div> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 56px">
                                                    </td>
                                                    <td colspan="2" style="vertical-align: top">
                                                     <div class="lbl">   if yes, explain</div> </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 56px">
                                                    </td>
                                                    <td colspan="3" style="vertical-align: top">
                                                        <asp:TextBox ID="txtPatientBenefitDesc" runat="server" Height="81px" TextMode="MultiLine" Width="421px"></asp:TextBox></td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td colspan="4" width="100%">
                                                      <div class="lbl">  This form is signed under penalty of perjury.</div> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" width="100%">
                                                        Board Authorized <span class="lbl">Health Care Provider signature: </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 17px; width: 56px;">
                                                        <div class="lbl">
                                                            Name</div>
                                                    </td>
                                                    <td style="height: 17px; width: 221px;">
                                                        <asp:TextBox ID="txtProviderName" runat="server" ReadOnly="true" ></asp:TextBox></td>
                                                    <td style="height: 17px">
                                                        <div class="lbl">
                                                            Signature</div>
                                                    </td>
                                                    <td style="height: 17px">
                                                        <asp:TextBox ID="txtProviderSignature" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                
                                                <td>
                                               <div class="lbl"> Spciality</div> 
                                                </td>
                                                <td style="width: 221px">
                                                    <asp:TextBox ID="txtProviderSpeciality" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                    <td style="height: 17px; width: 56px;">
                                                        <div class="lbl">
                                                            Date</div>
                                                    </td>
                                                    <td style="height: 17px; width: 160px;">
                                                        <asp:TextBox ID="txtProviderDate" runat="server" ReadOnly="true" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            
                                        </div>
                
                                  
                                        <div id="div_contect_field_buttons">
                                            <table>
                                                <tr>
                                                    <td colspan="4" width="100%" align="center">
                                                       <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                       <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                       <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save" Width="80px" 
                                                            CssClass="btn-gray" OnClick="btnSaveAndGoToNext_Click"  />
                                                       <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" 
                                                            CssClass="btn-gray" />
                                                        <asp:TextBox ID="txtPermanentImpairmentID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                   
                                    </th>
                            </tr>
                        </table>
                            </div>
                        </div>					  
					  </td>
					  
                                   
                                
                             <%--  <td width="16%" valign="top" scope="col">
							<div class="blocktitle_ql">
								<div align="left" class="blocktitle_adv">
									Quick Links
								</div>
                        		<div class="div_blockcontent">
								<table width="100%">
									<tr>
										<td>
											<ul style="font-family:Arial, Helvetica, sans-serif;font-size:12px;">

												<li>
													<a href="Bill_Sys_SearchCase.aspx">
														Home
													</a>
												</li>

												<li>
													<a href="">
														Add New Patient
													</a>
												</li>

												<li>
													<a href="">
														Search Patient
													</a>
												<br />
												</li>
											</ul>										
										</td>
									</tr>
								</table>
								</div>
							</div>
					  </td> --%>
					  
                            </tr>
                        </table>
                    </td>
                   
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
