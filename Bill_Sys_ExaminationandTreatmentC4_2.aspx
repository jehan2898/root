<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ExaminationandTreatmentC4_2.aspx.cs" Inherits="Bill_Sys_ExaminationandTreatmentC4_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
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
		 
		 function checkTextAreaMaxLength(textBox, e, length) {
    var mLen = textBox["MaxLength"];
    if (null == mLen)
        mLen = length;

    var maxLength = parseInt(mLen);
    if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            if (window.event)//IE
            {
                e.returnValue = false;
                return false;
            }
            else//Firefox
                e.preventDefault();
        }
    }
}
    function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}     
        
    </script>

</head>
<body>
    <form id="frmPlanOfCare" runat="server">
        <div align="center">
           <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
              
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
													<a href="Bill_Sys_PatientInformationC4_2.aspx">
														Patient Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_DoctorsInformationC4_2.aspx">
														Doctor Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_BillingInformationC4_2.aspx">
														Billing Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_ExaminationandTreatmentC4_2.aspx">
														Examination and Treatment
													</a>
												</li>

												<li>
													<a href="Bill_Sys_DoctorsOpinionC4_2.aspx">
														Doctor's Opinion
													</a>
												</li>
												<li>
													<a href="Bill_Sys_ReturnToWorkC4_2.aspx">
														Return To Work
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
                    <td width="83%"    scope="col">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                            
                                <td colspan="3" scope="col">
                                
                                    <div align="left" class="blocktitle">
                                    Examination and Treatment
                                    <div id="divPlanOfCare" class="div_blockcontent" >
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                               
                                                <tr>
                                                    <td colspan="4" style="height: 30px">
                                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px; vertical-align: top;">
                                                        1.</td>
                                                    <td style="height: 24px;" colspan="2">
                                                        <div class="lbl">
                                                            Descripe any diagnostic test(s) rendered at this visit</div>
                                                    </td>
                                                    <td style="height: 24px" >
                                                        <asp:TextBox ID="txtDiagnosisTestRendered" runat="server" TextMode="MultiLine" Width="224px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px; height: 40px">
                                                    </td>
                                                    <td colspan="2" style="height: 40px; text-align: left">
                                                        <div class="lbl">
                                                            Patient's Name:</div>
                                                    </td>
                                                    <td style="height: 40px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px;">
                                                    </td>
                                                    <td style="text-align: left; width: 166px;">
                                                        <asp:TextBox ID="txtMediPatientFirstName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtMediPatientMiddleName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMediPatientLastName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px;">
                                                    </td>
                                                    <td style="text-align: left; width: 166px;">
                                                        <div class="lbl">
                                                            First Name</div>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <div class="lbl">
                                                            Middle Name</div>
                                                    </td>
                                                    <td>
                                                        <div class="lbl">
                                                            Last Name</div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                    </td>
                                                    <td style="text-align: left" colspan="2">
                                                        <div class="lbl">
                                                            Date of injury/onset of illness:</div>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateOfInjury" runat="server" onkeypress="return CheckForInteger(event,'/')" ReadOnly="True"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                        2.</td>
                                                    <td colspan="3" style="text-align: left">
                                                        List any changes revealed by your most recent examination in the following in the
                                                        following: area of injury, type/nature of injury, Patient or your objective findings:</td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                    </td>
                                                    <td colspan="3" style="text-align: left">
                                                        <asp:TextBox ID="txtNatureofInjury" runat="server" TextMode="MultiLine" Width="326px"></asp:TextBox></td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                        3.</td>
                                                    <td colspan="3" style="text-align: left">
                                                        List additional body parts affected by this injury, if any</td>
                                                  
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                    </td>
                                                    <td colspan="2" style="text-align: left">
                                                        <asp:TextBox ID="txtBodyPartsAffected" runat="server" TextMode="MultiLine" Width="326px"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                        4.</td>
                                                    <td colspan="3" style="text-align: left">
                                                        Based on your most recent examination, lists the changes to the original treatment
                                                        plan, prescription medication or assistive devices, if any</td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                    </td>
                                                    <td colspan="2" style="text-align: left">
                                                        <asp:TextBox ID="txtChangesintreatmentplan" MaxLength="150"  runat="server" onkeyDown="return checkTextAreaMaxLength(this,event,'150');" TextMode="MultiLine" Width="326px"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                        5.</td>
                                                    <td colspan="2" style="text-align: left">
                                                        Based on this examination, does the patient need diagnostic tests and referrals? If yes, check all that apply</td>
                                                    <td>
                                                        <%--    <asp:CheckBoxList ID="chkDiagnosisTestandReferral" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                        <asp:RadioButtonList ID="rdlstDiagnosisTestandReferral" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                      
                                                        </asp:RadioButtonList></td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                    </td>
                                                    <td colspan="3">
                                                    <asp:Panel ID="pnlTestReferrals" runat="server" >
                                                        <table class="lbl" width="100%">
                                                            <tr>
                                                                <td class="tablecellLabel" style="height: 24px;" colspan="2">
                                                                    <div class="lbl">
                                                                        Tests</div>
                                                                </td>
                                                                <td class="tablecellControl" style="height: 24px" colspan="2">
                                                                    <div class="lbl">
                                                                        Referrals:</div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height: 24px">
                                                                    <asp:CheckBox ID="chkCTScans" runat="server" Text="CT Scan" /></td>
                                                                <td>
                                                                </td>
                                                                <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                    <asp:CheckBox ID="chkReferralsChiropractor" runat="server" Text="Chiropractor" /></td>
                                                                <td class="tablecellControl" style="height: 24px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height: 24px">
                                                                    <asp:CheckBox ID="chkEMGNCS" runat="server" Text="EMG/NCS" /></td>
                                                                <td>
                                                                </td>
                                                                <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                    <asp:CheckBox ID="chkReferralsInternist" runat="server" Text="Internist/Family Physician" /></td>
                                                                <td class="tablecellControl" style="height: 24px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height: 39px">
                                                                    <asp:CheckBox ID="chkMRI" runat="server" Text="MRI (specify):" /></td>
                                                                <td style="height: 39px">
                                                                    <asp:TextBox ID="txtMRI" runat="server" MaxLength="26"></asp:TextBox></td>
                                                                <td class="tablecellSpace" style="width: 17%; height: 39px">
                                                                    <asp:CheckBox ID="chkReferralsOccupationalTherapist" runat="server" Text="Occupational Therapist" /></td>
                                                                <td class="tablecellControl" style="height: 39px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height: 24px">
                                                                    <asp:CheckBox ID="chkLabs" runat="server" Text="Labs (specify):" /></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLabs" runat="server" MaxLength="26"></asp:TextBox></td>
                                                                <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                    <asp:CheckBox ID="chkReferralsPhysicalTherapist" runat="server" Text="Physical Therapist" /></td>
                                                                <td class="tablecellControl" style="height: 24px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height: 24px">
                                                                    <asp:CheckBox ID="chkXRay" runat="server" Text="X-rays (specify):" /></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtXRay" runat="server" MaxLength="25"></asp:TextBox></td>
                                                                <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                    <asp:CheckBox ID="chkReferralsSpecialistIn" runat="server" Text="Specialist in" /></td>
                                                                <td class="tablecellControl" style="height: 24px">
                                                                    <asp:TextBox ID="txtReferralsSpecialistIn" runat="server" MaxLength="28"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td  style="height: 26px">
                                                                    <asp:CheckBox ID="chkTestOthers" runat="server" Text="Other (specify):" /></td>
                                                                <td style="height: 26px">
                                                                    <asp:TextBox ID="txtTestOthers" runat="server" MaxLength="25"></asp:TextBox></td>
                                                                <td class="tablecellSpace" style="width: 17%; height: 26px">
                                                                    <asp:CheckBox ID="chkReferralsOthers" runat="server" Text="Other (specify)" /></td>
                                                                <td class="tablecellControl" style="height: 26px">
                                                                    <asp:TextBox ID="txtReferralsOthers" runat="server" MaxLength="28"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                        </asp:Panel>
                                                        <asp:TextBox ID="txtTransactionName" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtDescription" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        Important:<span class="lbl"> You must fill out form C-4 AUTH to request any special
                                                            medical service over $1000 that is not on the pre-authorized procedures list.</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                        6.</td>
                                                    <td colspan="2" style="vertical-align: top; text-align: left">
                                                        Describe treatment &nbsp;rendered today&nbsp;</td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px">
                                                    </td>
                                                    <td colspan="2" style="vertical-align: top; text-align: left">
                                                        <asp:TextBox ID="txtTreatmentRendered" runat="server" TextMode="MultiLine" Width="326px"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 13px;vertical-align: top;">
                                                        7.</td>
                                                    <td colspan="2" style="text-align: left; vertical-align: top;">
                                                        <div class="lbl">
                                                            When is the patient's next follow-up appointment?</div>
                                                    </td>
                                                    <td>
                                                        <div class="lbl">
                                                            <%--<asp:CheckBoxList ID="chklstPatientFollowUpAppoint" runat="server">
                                                                <asp:ListItem Value="0">1-2 weeks</asp:ListItem>
                                                                <asp:ListItem Value="1">3-4 weeks</asp:ListItem>
                                                                <asp:ListItem Value="2">5-6 weeks</asp:ListItem>
                                                                <asp:ListItem Value="3">7-8 weeks</asp:ListItem>
                                                                <asp:ListItem Value="4">____Months</asp:ListItem>
                                                                <asp:ListItem Value="5">Return as needed</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                            <asp:RadioButtonList ID="rdlstPatientFollowUpAppoint" runat="server">
                                                                <asp:ListItem Value="0" Selected="True">Within a week</asp:ListItem>
                                                                <asp:ListItem Value="1">1-2 weeks</asp:ListItem>
                                                                <asp:ListItem Value="2">3-4 weeks</asp:ListItem>
                                                                <asp:ListItem Value="3">5-6 weeks</asp:ListItem>
                                                                <asp:ListItem Value="4">7-8 weeks</asp:ListItem>
                                                                <asp:ListItem Value="5">____Months</asp:ListItem>
                                                                <asp:ListItem Value="6">Return as needed</asp:ListItem>
                                                            </asp:RadioButtonList></div>
                                                        </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 13px;vertical-align: top;">
                                                    </td> 
                                                    <td style="width: 166px;vertical-align: top;">
                                                    </td> 
                                                    <td style="width: 56px;vertical-align: top;">
                                                    </td> 
                                                    <td style="width: 56px;vertical-align: top;">
                                                    <asp:TextBox ID="txtPatientAppointment" runat="server" Visible="False"></asp:TextBox>
                                                    </td> 
                                                    </tr> 
                                                <tr>
                                                    <td colspan="4">
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
                                                       <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px" OnClick="btnSaveAndGoToNext_Click"
                                                            CssClass="btn-gray" />
                                                       <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" 
                                                            CssClass="btn-gray" />
                                                        <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtTreatmentID" runat="server" Width="10px" Visible="false" ></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                   
                                   
                                    </div>
                                </td>
                                
                            </tr>
                           
                        </table>
                    </td>
                 <%--   <td width="16%" valign="top" scope="col">
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
												</li>
												<br />
											</ul>										
										</td>
									</tr>
								</table>
								</div>
							</div>
					  </td>--%>
                    </tr>
                    </table> 
                    </td> 
                    
                </tr>
            </table>
        </div>
    </form>
</body>
</html>