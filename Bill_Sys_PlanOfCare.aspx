<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_PlanOfCare.aspx.cs"
    Inherits="Bill_Sys_PlanOfCare" %>

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

        function checkvalidate_medicationRestriction_none() {
            debugger;
           
            var value = "";
            var rdllist = document.getElementById('rdlstMedicationRestrictions');
            var rdlnone = document.getElementById('rdlstMedicationRestrictions_0');
            var rdlmayaffect = document.getElementById('rdlstMedicationRestrictions_1');
            var rdlmayaffect_text = document.getElementById('txtMedicationRestrictions');
           
            if (rdlnone.checked) {
                value = "0";
                document.getElementById('txtMedicationRestrictions').value = "";
            }
           
                if (rdlmayaffect.checked)
                {
                    value = "1";
                   
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
                                                        <ul style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;">
                                                            <li><a href="Bill_Sys_PatientInformation.aspx">Patient Information </a></li>
                                                            <li><a href="Bill_Sys_WorkerTemplate.aspx">Employer Information </a></li>
                                                            <li><a href="Bill_Sys_DoctorsInformation.aspx">Doctor's Information </a></li>
                                                            <li><a href="Bill_Sys_BillingInformation.aspx">Billing Information </a></li>
                                                            <li><a href="Bill_Sys_History.aspx">History </a></li>
                                                            <li><a href="Bill_Sys_ExamInformation.aspx">Exam Information </a></li>
                                                            <li><a href="Bill_Sys_DoctorOpinion.aspx">Doctor's Opinion </a></li>
                                                            <li><a href="Bill_Sys_PlanOfCare.aspx">Plan Of Care </a></li>
                                                            <li><a href="Bill_Sys_WorkStatus.aspx">Work Status </a>
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
                                        Plan Of Care
                                        <div class="div_blockcontent">
                                            <table>
                                                <tr>
                                                    <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td colspan="3" scope="col">
                                                                    <asp:Panel ID="pnlDevicePrescribe" runat="server" Width="100%">
                                                                        <table width="80%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                            <tr>
                                                                                <td colspan="4" style="height: 30px">
                                                                                    <div id="Div2" visible="true" style="color: Red;">
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; vertical-align: top;">
                                                                                    1.</td>
                                                                                <td style="height: 24px">
                                                                                    <div class="lbl">
                                                                                        What is your proposed treatment?</div>
                                                                                </td>
                                                                                <td style="height: 24px" colspan="2">
                                                                                    <asp:TextBox ID="txtProposedWorkTreatment" runat="server" TextMode="MultiLine" Width="224px"
                                                                                        onkeyDown="return checkTextAreaMaxLength(this,event,'443');"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; vertical-align: top;">
                                                                                    2.</td>
                                                                                <td colspan="3">
                                                                                    Medication(s):</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px">
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <div class="lbl">
                                                                                        a) list medications prescribed:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMedicationsPrescribe" runat="server" MaxLength="106"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px">
                                                                                </td>
                                                                                <td colspan="2" style="height: 24px; text-align: left;">
                                                                                    <div class="lbl">
                                                                                        (b) list over-the-counter medications advised:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMedicationAdvised" runat="server" MaxLength="89"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; height: 40px">
                                                                                </td>
                                                                                <td colspan="2" style="height: 40px; text-align: left;">
                                                                                    Medication restrictions:</td>
                                                                                <td style="height: 40px">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; height: 40px">
                                                                                </td>
                                                                                <td colspan="3" style="height: 40px; text-align: left">
                                                                                    <div class="lbl">
                                                                                        <%-- <asp:CheckBoxList ID="chklstMedicationRestrictions" runat="server">
                                                                <asp:ListItem Value="0">None</asp:ListItem>
                                                                <asp:ListItem Value="1">May affect patient's ability to return to work, make patient drowsy, or other issue. Explain</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                                        <asp:RadioButtonList ID="rdlstMedicationRestrictions" runat="server">
                                                                                            <asp:ListItem Value="0" Selected="True">None</asp:ListItem>
                                                                                            <asp:ListItem Value="1">May affect patient's ability to return to work, make patient drowsy, or other issue. Explain</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; height: 40px">
                                                                                </td>
                                                                                <td colspan="2" style="height: 40px; text-align: left">
                                                                                    <asp:TextBox ID="txtMedicationRestrictions" runat="server" Height="67px" TextMode="MultiLine"
                                                                                        Width="390px" onkeyDown="return checkTextAreaMaxLength(this,event,'237');"></asp:TextBox></td>
                                                                                <td style="height: 40px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; height: 40px">
                                                                                </td>
                                                                                <td colspan="2" style="height: 40px; text-align: left">
                                                                                    <div class="lbl">
                                                                                        Patient's Name:</div>
                                                                                </td>
                                                                                <td style="height: 40px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px;">
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:TextBox ID="txtMediPatientFirstName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                                                <td style="text-align: left">
                                                                                    <asp:TextBox ID="txtMediPatientMiddleName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMediPatientLastName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px;">
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <div class="lbl">
                                                                                        First Name</div>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <div class="lbl">
                                                                                        Middle Name</div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="lbl">
                                                                                        Last Name</div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px">
                                                                                </td>
                                                                                <td style="text-align: left" colspan="2">
                                                                                    <div class="lbl">
                                                                                        Date of injury/onset of illness:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDateOfInjury" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                        ReadOnly="True"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; vertical-align: top;">
                                                                                    3.</td>
                                                                                <td style="text-align: left; vertical-align: top;" colspan="2">
                                                                                    <div class="lbl">
                                                                                        Does the patient need diagnostic tests or referrals? If yes, check all that apply.</div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="lbl">
                                                                                        <%--   <asp:CheckBoxList ID="chklstPatientNeedDiagnosisTest" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                                        <asp:RadioButtonList ID="rdlstPatientNeedDiagnosisTest" runat="server" RepeatDirection="Horizontal">
                                                                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                                                            <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                                        </asp:RadioButtonList></div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px">
                                                                                </td>
                                                                                <td colspan="3">
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
                                                                                            <td style="height: 24px; width: 15%;">
                                                                                                <asp:CheckBox ID="chkCTScans" runat="server" Text="CT Scan" /></td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                                                <asp:CheckBox ID="chkReferralsChiropractor" runat="server" Text="Chiropractor" /></td>
                                                                                            <td class="tablecellControl" style="height: 24px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 24px; width: 15%;">
                                                                                                <asp:CheckBox ID="chkEMGNCS" runat="server" Text="EMG/NCS" /></td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                                                <asp:CheckBox ID="chkReferralsInternist" runat="server" Text="Internist/Family Physician" /></td>
                                                                                            <td class="tablecellControl" style="height: 24px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 39px; width: 15%;">
                                                                                                <asp:CheckBox ID="chkMRI" runat="server" Text="MRI (specify):" /></td>
                                                                                            <td style="height: 39px">
                                                                                                <asp:TextBox ID="txtMri" runat="server" MaxLength="42"></asp:TextBox></td>
                                                                                            <td class="tablecellSpace" style="width: 17%; height: 39px">
                                                                                                <asp:CheckBox ID="chkReferralsOccupationalTherapist" runat="server" Text="Occupational Therapist" /></td>
                                                                                            <td class="tablecellControl" style="height: 39px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 24px; width: 15%;">
                                                                                                <asp:CheckBox ID="chkLabs" runat="server" Text="Labs (specify):" /></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtLabs" runat="server" MaxLength="45"></asp:TextBox></td>
                                                                                            <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                                                <asp:CheckBox ID="chkReferralsPhysicalTherapist" runat="server" Text="Physical Therapist" /></td>
                                                                                            <td class="tablecellControl" style="height: 24px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 24px; width: 15%;">
                                                                                                <asp:CheckBox ID="chkXRay" runat="server" Text="X-rays (specify):" /></td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtXRay" runat="server" MaxLength="43"></asp:TextBox></td>
                                                                                            <td class="tablecellSpace" style="width: 17%; height: 24px">
                                                                                                <asp:CheckBox ID="chkReferralsSpecialistIn" runat="server" Text="Specialist in" /></td>
                                                                                            <td class="tablecellControl" style="height: 24px">
                                                                                                <asp:TextBox ID="txtReferralsSpecialistIn" runat="server" MaxLength="46"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 26px; width: 15%;">
                                                                                                <asp:CheckBox ID="chkTestOthers" runat="server" Text="Other (specify):" /></td>
                                                                                            <td style="height: 26px">
                                                                                                <asp:TextBox ID="txtTestOthers" runat="server" MaxLength="47"></asp:TextBox></td>
                                                                                            <td class="tablecellSpace" style="width: 17%; height: 26px">
                                                                                                <asp:CheckBox ID="chkReferralsOthers" runat="server" Text="Other (specify)" /></td>
                                                                                            <td class="tablecellControl" style="height: 26px">
                                                                                                <asp:TextBox ID="txtReferralsOthers" runat="server" MaxLength="49"></asp:TextBox></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <asp:TextBox ID="txtDescription" Width="10px" runat="server" Visible="False"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtTransactionName" runat="server" Width="10px" Visible="False"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td style="width: 56px">
                                                                                                4.</td>
                                                                                            <td style="text-align: left">
                                                                                                <div class="lbl">
                                                                                                    Assistive devices prescribed for this patient:</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <div class="lbl">
                                                                                                    &nbsp;</div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 56px">
                                                                                            </td>
                                                                                            <td style="text-align: left">
                                                                                                <div class="lbl">
                                                                                                    <asp:CheckBox ID="chkCane" runat="server" Text="Cane" /></div>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 56px">
                                                                                            </td>
                                                                                            <td style="text-align: left">
                                                                                                <div class="lbl">
                                                                                                    <asp:CheckBox ID="chkCrutches" runat="server" Text="Crutches" /></div>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 56px; height: 31px;">
                                                                                            </td>
                                                                                            <td style="text-align: left; height: 31px;">
                                                                                                <div class="lbl">
                                                                                                    <asp:CheckBox ID="chkOrthotics" runat="server" Text="Orthotics" /></div>
                                                                                            </td>
                                                                                            <td style="height: 31px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 56px">
                                                                                            </td>
                                                                                            <td style="text-align: left">
                                                                                                <div class="lbl">
                                                                                                    <asp:CheckBox ID="chkWalker" runat="server" Text="Walker" /></div>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 56px; height: 28px;">
                                                                                            </td>
                                                                                            <td style="text-align: left; height: 28px;">
                                                                                                <div class="lbl">
                                                                                                    <asp:CheckBox ID="chkWheelchair" runat="server" Text="Wheelchair" /></div>
                                                                                            </td>
                                                                                            <td style="height: 28px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 56px">
                                                                                            </td>
                                                                                            <td style="text-align: left">
                                                                                                <div class="lbl">
                                                                                                    <asp:CheckBox ID="chkAssistiveOthers" runat="server" Text="Other-(specify):" /></div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtAssistiveOthers" runat="server" MaxLength="120"></asp:TextBox></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <asp:TextBox ID="txtAssistiveDeviceName" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtAssistiveDesc" runat="server" Visible="False" Width="10px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    Important:<span class="lbl"> You must fill out form C-4 AUTH to request any special
                                                                                        medical service over $1000 that is not on the pre-authorized procedures list.</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; vertical-align: top;">
                                                                                    5.</td>
                                                                                <td colspan="2" style="text-align: left; vertical-align: top;">
                                                                                    <div class="lbl">
                                                                                        When is the patient's next follow-up appointment?</div>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="lbl">
                                                                                        <%-- <asp:CheckBoxList ID="chklstPatientFollowUpAppoint" runat="server">
                                                                <asp:ListItem Value="0">1-2 weeks</asp:ListItem>
                                                                <asp:ListItem Value="1">3-4 weeks</asp:ListItem>
                                                                <asp:ListItem Value="2">5-6 weeks</asp:ListItem>
                                                                <asp:ListItem Value="3">7-8 weeks</asp:ListItem>
                                                                <asp:ListItem Value="4">____Months</asp:ListItem>
                                                                <asp:ListItem Value="5">Return as needed</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                                        <asp:RadioButtonList ID="rdlstPatientFollowUpAppoint" runat="server">
                                                                                            <asp:ListItem Value="0" Selected="True">Within Week</asp:ListItem>
                                                                                            <asp:ListItem Value="1">1-2 weeks</asp:ListItem>
                                                                                            <asp:ListItem Value="2">3-4 weeks</asp:ListItem>
                                                                                            <asp:ListItem Value="3">5-6 weeks</asp:ListItem>
                                                                                            <asp:ListItem Value="4">7-8 weeks</asp:ListItem>
                                                                                            <asp:ListItem Value="5">____Months</asp:ListItem>
                                                                                            <asp:ListItem Value="6">Return as needed</asp:ListItem>
                                                                                            <asp:ListItem Value="7" Selected="True">None</asp:ListItem>
                                                                                        </asp:RadioButtonList></div>
                                                                                    <asp:TextBox ID="txtPatientAppointment" runat="server" Width="10px" Visible="False"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; vertical-align: top; visibility:hidden" >
                                                                                    6.</td>
                                                                                <td colspan="2" style="text-align: left; visibility:hidden">
                                                                                    <div class="lbl">
                                                                                        Did you adhere to the New York Treatment Guidelines for your evaluation and treatment
                                                                                        of this injury/illness?</div>
                                                                                </td>
                                                                                <td style="visibility:hidden">
                                                                                    <div class="lbl">
                                                                                        <%--   <asp:CheckBoxList ID="chklstTreatmentGuidelines" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                                        <asp:RadioButtonList ID="rdlstTreatmentGuidelines" runat="server" RepeatDirection="Horizontal">
                                                                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                                                            <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px; height: 26px;">
                                                                                </td>
                                                                                <td colspan="2" style="text-align: left; height: 26px; visibility:hidden">
                                                                                    <div class="lbl">
                                                                                        If yes, identify applicable sections of Treatment Guidelines:</div>
                                                                                </td>
                                                                                <td style="height: 26px">
                                                                                    <asp:TextBox ID="txtTreatmentGuidelines" runat="server" MaxLength="61" Visible="false"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 56px">
                                                                                </td>
                                                                                <td colspan="2" style="text-align: left; visibility:hidden">
                                                                                    <div class="lbl">
                                                                                        If no, explain why not, including the basis for any variance from the Guidelines:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtVarianceGuideline" runat="server" MaxLength="233" Visible="false"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
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
                                                                                    <asp:TextBox ID="txtBillNumber" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                                    <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px"
                                                                                        OnClick="btnSaveAndGoToNext_Click" CssClass="btn-gray" />
                                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                                                                                        CssClass="btn-gray" />
                                                                                    <asp:TextBox ID="txtPlanofCareID" runat="server" Visible="False" Width="10px"></asp:TextBox></td>
                                                                                <asp:TextBox ID="txtTreatmentGuideYesNo" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                        </div>
                                </td>
                            </tr>
                        </table>
                    </th>
                </tr>
            </table>
        </div>
        </div> </td>
        <%--<td width="16%" valign="top" scope="col">
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
					  </td>--%>
        </tr> </table> </td> </tr> </table> </div>
    </form>
</body>
</html>
