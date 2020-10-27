<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientInformation.aspx.cs" Inherits="PatientInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Billing System111</title>

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
         
          function checkvalidate_chkmale()
         {
            var chkvalgedermale=document.getElementById('chkGenderMale');
            var ChkvalGenderFemale=document.getElementById('ChkGenderFemale');
            document.getElementById('txtchkmale').value="Male";
            
            if(chkvalgedermale.checked==true)
            {
                ChkvalGenderFemale.checked=false;
            }
         }
         
          function checkvalidate_chkfemale()
         {
            var chkvalgedermale=document.getElementById('chkGenderMale');
            var ChkvalGenderFemale=document.getElementById('ChkGenderFemale');
            document.getElementById('txtchkmale').value="Female";
            
            if(ChkvalGenderFemale.checked==true)
            {
                chkvalgedermale.checked=false;
            }
         }
         
         function ConfirmUpdate()
        {
             var msg = "Do you wants to sync that data with the actual data.?";
             
    
             if(confirm(msg))
             {
                document.getElementById('btnhidden').value="YES";
            
                return true;
             }
             else
             {   
             document.getElementById('btnhidden').value="NO";
            
                return true;
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
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
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
                                                            <li><a href="PatientInformation.aspx">Patient Information </a></li>
                                                            <li><a href="WorkerTemplate.aspx">Employer Information </a></li>
                                                            <li><a href="DoctorsInformation.aspx">Doctor's Information </a></li>
                                                            <li><a href="BillingInformation.aspx">Billing Information </a></li>
                                                            <li><a href="History.aspx">History </a></li>
                                                            <li><a href="ExamInformation.aspx">Exam Information </a></li>
                                                            <li><a href="DoctorOpinion.aspx">Doctor's Opinion </a></li>
                                                            <li><a href="PlanOfCare.aspx">Plan Of Care </a></li>
                                                            <li><a href="WorkStatus.aspx">Work Status </a>
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
                                        Patient Information
                                        <div class="div_blockcontent">
                                            <table>
                                                <tr>
                                                    <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                                        <div align="left">
                                                            <table>
                                                                <tr>
                                                                    <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                                                        <div align="left">
                                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            1.</div>
                                                                                    </td>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            Patient Name:</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtPatientFirstName" runat="Server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPatientMiddleName" runat="Server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPatientLastName" runat="Server" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                    </td>
                                                                                    <td style="width: 93px">
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <div class="lbl">
                                                                                            First Name</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Middle Name</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Last Name</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            2.
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Soc. Sec. #</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtSocialSecurity" runat="server"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            3. Home Phone #</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtHomePhone" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            4.
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            WCB Case #</div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtWCBCase" runat="server"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            5. CARRIER Case #</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtCarrierCaseNo" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            6.</div>
                                                                                    </td>
                                                                                    <td style="width: 200px">
                                                                                        <div class="lbl">
                                                                                            Mailing Address</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 58px">
                                                                                    </td>
                                                                                    <td style="width: 93px; height: 58px;">
                                                                                        <div class="lbl">
                                                                                            Number and Street</div>
                                                                                    </td>
                                                                                    <td style="width: 156px; height: 58px;">
                                                                                        <asp:TextBox ID="txtPatientStreet" runat="server"></asp:TextBox></td>
                                                                                    <td style="height: 58px">
                                                                                        <div class="lbl">
                                                                                            City</div>
                                                                                    </td>
                                                                                    <td style="height: 58px">
                                                                                        <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td style="width: 93px">
                                                                                        <div class="lbl">
                                                                                            State</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <cc1:ExtendedDropDownList ID="txtPatientState" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                                            Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                                        </cc1:ExtendedDropDownList>
                                                                                    </td>
                                                                                    <td style="width: 93px">
                                                                                        <div class="lbl">
                                                                                            Zip Code</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            7.
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            Date of injury/ Illness</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <div class="lbl">
                                                                                            <asp:TextBox ID="txtDateOfInjury" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                                            <asp:ImageButton ID="imgbtnDateOfInjury" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfInjury"
                                                                                                PopupButtonID="imgbtnDateOfInjury" />
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="lbl">
                                                                                            8.Date of Birth</div>
                                                                                    </td>
                                                                                    <td style="width: 80px">
                                                                                        <div class="lbl">
                                                                                            <asp:TextBox ID="txtDateOfBirth" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                                            <asp:ImageButton ID="imgbtnDateOfBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOfBirth"
                                                                                                PopupButtonID="imgbtnDateOfBirth" />
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 52px">
                                                                                        <div class="lbl">
                                                                                            9.</div>
                                                                                    </td>
                                                                                    <td style="width: 93px; height: 52px;">
                                                                                        <div class="lbl">
                                                                                            Gender</div>
                                                                                    </td>
                                                                                    <td style="width: 156px; height: 52px;">
                                                                                        <asp:CheckBox ID="chkGenderMale" runat="server" Text="Male" Font-Bold="false" Font-Size="Small" />
                                                                                        <asp:CheckBox ID="ChkGenderFemale" runat="server" Text="Female" Font-Bold="false"
                                                                                            Font-Size="Small" />
                                                                                    </td>
                                                                                    <td style="height: 52px;">
                                                                                        <div class="lbl">
                                                                                            10.Patient Account #.</div>
                                                                                    </td>
                                                                                    <td style="width: 156px; height: 52px;">
                                                                                        <asp:TextBox ID="txtCaseID" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                                                    <td style="height: 52px">
                                                                                    </td>
                                                                                    <td style="height: 52px">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            11.</div>
                                                                                    </td>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            On date of injury/illness what was Patient's job and description:</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtJobTitle" runat="Server" TextMode="MultiLine" 
                                                                                            Width="300px" onkeyDown="return checkTextAreaMaxLength(this,event,'10');"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            12.</div>
                                                                                    </td>
                                                                                    <td style="vertical-align: top;">
                                                                                        <div class="lbl">
                                                                                            On date of injury/illness what were Patient's usual work activities:</div>
                                                                                    </td>
                                                                                    <td style="width: 156px">
                                                                                        <asp:TextBox ID="txtWorkActivities" runat="Server"  TextMode="MultiLine"
                                                                                            Width="300px" MaxLength="233"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
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
                                                                                        <asp:TextBox ID="txtchkmale" runat="server" Width="10px" Style="visibility: hidden;"></asp:TextBox>
                                                                                        <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px"
                                                                                            CssClass="btn-gray" OnClick="btnSaveAndGoToNext_Click" />
                                                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" />
                                                                                        <asp:TextBox ID="txtBillNumber" runat="server" Width="10px" Visible="False"></asp:TextBox></td>
                                                                                    <asp:HiddenField ID="btnhidden" runat="server"  />
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
                                <%-- <td width="16%" valign="top" scope="col">
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
    </form>
</body>
</html>
