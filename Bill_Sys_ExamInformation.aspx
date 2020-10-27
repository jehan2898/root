<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ExamInformation.aspx.cs"
    Inherits="Bill_Sys_ExamInformation" %>

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

        function validate_examinfordl()
        {
            var value = "";
            debugger;
            
            var rdllist = document.getElementById('rdlistExamInfo');
            var rdlyes = document.getElementById('rdlistExamInfo_0');
            var rdlno = document.getElementById('rdlistExamInfo_1');
            var rdlnone = document.getElementById('rdlistExamInfo_2');
            var rdltext = document.getElementById('txtExamInfo');
            if(rdlyes.checked)
            {
                value = "0";
               
            }
            if(rdlno.checked)
            {
                value = "1";
                document.getElementById('txtExamInfo').value = "";
            }
            if (rdlnone.checked)
            {
                value = "2";
                document.getElementById('txtExamInfo').value = "";
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
    </script>

</head>
<body>
    <form id="frmHistory" runat="server">
     <asp:Panel ID="pnlExamInformation" runat="server" Width="100%">
        <div align="center">
           
<asp:ScriptManager ID="ScriptManager1" runat="server">  </asp:ScriptManager>
      
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px" width="100%">
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
													<a href="Bill_Sys_PatientInformation.aspx">
														Patient Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_WorkerTemplate.aspx">
														Employer Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_DoctorsInformation.aspx">
														Doctor's Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_BillingInformation.aspx">
														Billing Information
													</a>
												</li>

												<li>
													<a href="Bill_Sys_History.aspx">
														History
													</a>
												</li>
												<li>
													<a href="Bill_Sys_ExamInformation.aspx">
														Exam Information
													</a>
												</li>
												<li>
													<a href="Bill_Sys_DoctorOpinion.aspx">
														Doctor's Opinion
													</a>
												</li>
												<li>
													<a href="Bill_Sys_PlanOfCare.aspx">
														Plan Of Care
													</a>
												</li>
												<li>
													<a href="Bill_Sys_WorkStatus.aspx">
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
		<td scope="col" style="width: 74%">
						<div class="blocktitle">
                            Exam Information
                        <div class="div_blockcontent">			  
		   <table>
                            <tr>
                            
                                  <th width="100%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>			  
                       
                                    <td colspan="3" scope="col" style="width: 652px">
                                      <div align="left">
                                                <table width="80%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                    <tr>
                                                        <td width="25%">
                                                        <div class="lbl">1. Date(s) of Examination:</div>    
                                                        </td>
                                                        <td width="75%" colspan="3">
                                                            <asp:TextBox ID="txtDateOfExam" runat="server"></asp:TextBox>
                                                            &nbsp;&nbsp;
                                                            <asp:ImageButton ID="imgbtnDateOfExam" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfExam" PopupButtonID="imgbtnDateOfExam" />
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" width="100%">
                                                          <div class="lbl">  2. Patient's subjective complaints: Check all that apply and identify specific affected
                                                            body part(s).</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkNumbnessTingling" Text="Numbness/Tingling" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtNumbnessTingling" runat="server" MaxLength="46"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkSwelling" Text="Swelling" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtSwelling" runat="server" MaxLength="60"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkPain" Text="Pain" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtPain" runat="server" MaxLength="64"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkWeakness" Text="Weakness" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtWeakness" runat="server" MaxLength="61"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkStiffness" Text="Stiffness" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtStiffness" runat="server" MaxLength="61"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkOther" Text="Other(Specify)" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtOther" runat="server" MaxLength="60"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" width="100%">
                                                           <div class="lbl"> 3. Type/nature of injury: Check all that apply and identify specific affected body
                                                            part(s).</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                         <div class="lbl">   <asp:CheckBox ID="chkAbrasion" Text="Abrasion" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtAbrasion" runat="server" MaxLength="52"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkInfectiousDisease" Text="Infectious Disease" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtInfectiousDisease" runat="server" maxlengh="54"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkAmputation" Text="Amputation" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtAmputation" runat="server" MaxLength="47"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkInhalationExposure" Text="Inhalation Exposure" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtInhalationExposure" runat="server" MaxLength="51"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkAvulsion" Text="Avulsion" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtAvulsion" runat="server" MaxLength="52"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkLaceration" Text="Laceration" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtLaceration" runat="server" MaxLength="58"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkBite" Text="Bite" runat="server" MaxLength="62" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtBite" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkNeedleStick" Text="Needle Stick" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtNeedleStick" runat="server" MaxLength="56"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <div class="lbl"><asp:CheckBox ID="chkBurn" Text="Burn" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtBurn" runat="server" MaxLength="51"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkPoisoningToxicEffects" Text="Poisoning/Toxic Effects" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtPoisoningToxicEffects" runat="server" MaxLength="46"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <div class="lbl"><asp:CheckBox ID="chkContusionHematoma" Text="Contusion/Hematoma" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtContusionHematoma" runat="server" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                            <div class="lbl"><asp:CheckBox ID="chkPsychological" Text="Psychological" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtPsychological" runat="server" MaxLength="53"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkCrushInjury" Text="Crush Injury" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtCrushInjury" runat="server" MaxLength="49"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkPunctureWound" Text="Puncture Wound" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtPunctureWound" runat="server" MaxLength="44"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkDermatitis" Text="Dermatitis" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtDermatitis" runat="server" MaxLength="53"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkRepetitiveStrainInjury" Text="Repetitive Strain Injury" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtRepetitiveStrainInjury" runat="server" MaxLength="47"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkDislocation" Text="Dislocation" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtDislocation" runat="server" MaxLength="55"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkSpinalCordInjury" Text="Spinal Cord Injury" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtSpinalCordInjury" runat="server" MaxLength="52"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkFracture" Text="Fracture" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtFracture" runat="server" MaxLength="58"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkSprainStrain" Text="Sprain/Strain" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtSprainStrain" runat="server" MaxLength="56"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                         <div class="lbl">   <asp:CheckBox ID="chkHearingLoss" Text="Hearing Loss" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtHearingLoss" runat="server" MaxLength="47"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                         <div class="lbl">   <asp:CheckBox ID="chkTornLigamentTendonOrMuscle" Text="Torn Ligament Tendon or Muscle"
                                                                runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtTornLigamentTendonOrMuscle" runat="server" MaxLength="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkHernia" Text="Hernia" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtHernia" runat="server" MaxLength="54"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkVisionLoss" Text="Vision Loss" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtVisionLoss" runat="server" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%" valign="top">
                                                          <div class="lbl">  <asp:CheckBox ID="chkOtherNatureOfInjury" Text="Other(Specify) :" runat="server" /></div> 
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtOtherNatureOfInjury" runat="server" TextMode="MultiLine" Width="420px"
                                                                Height="80px" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" width="100%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" width="100%">
                                                           <div class="lbl"> 4. Physical examination: Check all relevant objective findings and identify specific
                                                            affected body part(s).</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkNoneAtPresent" Text="None at present" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                        </td>
                                                        <%--<td width="25%">
                                                            <asp:CheckBox ID="chkNeuromuscularFindings" Text="Neuromuscular &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Findings:"
                                                                runat="server" />
                                                        </td>--%>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkNeuromuscularFindings" Text="Neuromuscular Findings:"
                                                                runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkBruising" Text="Bruising" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtBruising" runat="server" MaxLength="59"></asp:TextBox>
                                                        </td>
                                                       <%-- <td width="25%">
                                                            &nbsp;&nbsp;<asp:CheckBox ID="chkAbnormalRestrictedROM" Text="Abnormal/Restricted &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;  ROM"
                                                                runat="server" />
                                                        </td>--%>
                                                         <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkAbnormalRestrictedROM" Text="Abnormal/Restricted ROM"
                                                                runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkBurns" Text="Burns" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtBurns" runat="server" MaxLength="54"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                            <div class="lbl"><asp:CheckBox ID="chkActiveROM" Text="Active ROM"
                                                                runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtActiveROM" runat="server" MaxLength="37"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkCrepitation" Text="Crepitation" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtCrepitation" runat="server" MaxLength="58"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkPassiveROM" Text="Passive ROM"
                                                                runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtPassiveROM" runat="server" MaxLength="36"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkDeformity" Text="Deformity" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtDeformity" runat="server" MaxLength="51"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkGait" Text="Gait" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtGait" runat="server" MaxLength="62"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                         <div class="lbl">   <asp:CheckBox ID="chkEdema" Text="Edema" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtEdema" runat="server" MaxLength="45"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkPalpableMuscleSpasm" Text="Palpable Muscle Spasm" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtPalpableMuscleSpasm" runat="server" MaxLength="42"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkHematomaLumpSwelling" Text="Hematoma/Lump/Swelling" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtHematomaLumpSwelling" runat="server" MaxLength="31"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkReflexes" Text="Reflexes" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtReflexes" runat="server" MaxLength="57"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <div class="lbl"><asp:CheckBox ID="chkJointEffusion" Text="Joint Effusion" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtJointEffusion" runat="server" MaxLength="57"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                            <div class="lbl"><asp:CheckBox ID="chkSensation" Text="Sensation" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtSensation" runat="server" MaxLength="54"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkLacerationSutures" Text="Laceration/Sutures" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtLacerationSutures" runat="server" MaxLength="47"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkStrengthWeakness" Text="Strength (Weakness)" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtStrengthWeakness" runat="server" MaxLength="47"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkPainTenderness" Text="Pain/Tenderness" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtPainTenderness" runat="server" MaxLength="49"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                           <div class="lbl"> <asp:CheckBox ID="chkWastingMuscleAtrophy" Text="Wasting/Muscle Atrophy" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtWastingMuscleAtrophy" runat="server" MaxLength="40"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                          <div class="lbl">  <asp:CheckBox ID="chkScar" Text="Scar" runat="server" /></div> 
                                                        </td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtScar" runat="server" MaxLength="59"></asp:TextBox>
                                                        </td>
                                                        <td width="25%">
                                                        </td>
                                                        <td width="25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%" valign="top">
                                                           <div class="lbl"> <asp:CheckBox ID="chkOtherPhysicalInfo" Text="Other(Specify) ::" runat="server" /></div> 
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtOtherPhysicalInfo" runat="server" TextMode="MultiLine" Width="420px"
                                                                Height="80px" MaxLength="138"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%" colspan="4" valign="top">
                                                           <div class="lbl"> 5. Describe any diagnostic test(s) rendered at this visit:</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%" colspan="4" valign="top">
                                                            <asp:TextBox ID="txtDiagnosticTest" runat="server" TextMode="MultiLine" Width="420px"
                                                                Height="80px" MaxLength="254" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%" colspan="4" valign="top">
                                                           <div class="lbl"> 6. Describe any treatment(s) rendered at this visit:</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%" colspan="4">
                                                            <asp:TextBox ID="txtTreatment" runat="server" TextMode="MultiLine" Width="420px"
                                                                Height="80px" MaxLength="270"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%" colspan="4" valign="top">
                                                           <div class="lbl"> 7. Describe prognosis for recovery:</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="50%" colspan="4">
                                                            <asp:TextBox ID="txtPrognosis" runat="server" TextMode="MultiLine" Width="420px"
                                                                Height="80px" MaxLength="330"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%" colspan="4">
                                                          <div class="lbl">  8. Does the patient's medical history reveal any pre-existing condition(s) that
                                                            may affect the treatment and/or prognosis?</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                         <div class="lbl">   <asp:RadioButtonList ID="rdlistExamInfo" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="None" Value="2" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            If yes, list and describe:</div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txtExamInfo" runat="server" TextMode="MultiLine" Width="420px" Height="80px" MaxLength="300"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                           
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
                                                                CssClass="btn-gray" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                       </div>
                                    </td>
                                
                   </tr>
                                        </table>
                                    </th>
                                   
                                   </tr>
                        </table>
                             </div>
                        </div>					  
					  </td>
			         <%-- <td valign="top" scope="col" style="width: 16%">
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
                    </tr>
                                </table> 
                                </td> 
                                </tr> 
                </table>
           
        </div>
         </asp:Panel>
    </form>
</body>
</html>
