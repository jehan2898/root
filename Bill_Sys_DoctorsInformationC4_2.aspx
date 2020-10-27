<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_DoctorsInformationC4_2.aspx.cs"
    Inherits="Bill_Sys_DoctorsInformationC4_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
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

    <script type="text/javascript">
        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }
        function CheckForInteger(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }


        }

        function CopySameAddress() {
            var chk = document.getElementById('chkCopySameAddress');

            if (chk.checked) {
                document.getElementById('txtBillingAdd').value = document.getElementById('txtOfficeAdd').value;
                document.getElementById('txtBillingCity').value = document.getElementById('txtOfficeCity').value;
                document.getElementById('extddlOfficeState').value = document.getElementById('txtOfficeState').value;
                document.getElementById('txtBillingZip').value = document.getElementById('txtOfficeZip').value;
            }
            else {
                document.getElementById('txtBillingAdd').value = "";
                document.getElementById('txtBillingCity').value = "";
                document.getElementById('extddlOfficeState').value = "NA";
                document.getElementById('txtBillingZip').value = "";
            }

        }
    </script>

</head>
<body>
    <form id="frmPlanOfCare" runat="server">
        <div>
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
                 <tr>
                    <td colspan="6" align="center">
                        <asp:Panel ID="pnlmsg" runat="server">
                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                        <UserMessage:MessageControl runat="server" id="usrMessage" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="height: 454px">
                        <table width="100%" id="Table1" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="6" style="height: 454px; vertical-align: top;">
                                    <table width="100%" id="content_table" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="83%" scope="col">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                                                                                    <li><a href="Bill_Sys_PatientInformationC4_2.aspx">Patient Information </a></li>
                                                                                    <li><a href="Bill_Sys_DoctorsInformationC4_2.aspx">Doctor Information </a></li>
                                                                                    <li><a href="Bill_Sys_BillingInformationC4_2.aspx">Billing Information </a></li>
                                                                                    <li><a href="Bill_Sys_ExaminationandTreatmentC4_2.aspx">Examination and Treatment </a>
                                                                                    </li>
                                                                                    <li><a href="Bill_Sys_DoctorsOpinionC4_2.aspx">Doctor's Opinion </a></li>
                                                                                    <li><a href="Bill_Sys_ReturnToWorkC4_2.aspx">Return To Work </a>
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
                                                                Doctor Information
                                                                <div class="div_blockcontent">
                                                                    <table border="0">
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
                                                                                            <td style="vertical-align: top; width: 13px;">
                                                                                                1.</td>
                                                                                            <td style="vertical-align: top; width: 141px;">
                                                                                                <div class="lbl">
                                                                                                    Doctor Name:</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                                <asp:TextBox ID="txtDoctorName" runat="server" MaxLength="50" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td style="width: 208px">
                                                                                                &nbsp;</td>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top; width: 13px;">
                                                                                                2.</td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    WCB Authorization #</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                                <asp:TextBox ID="txtWCBAuth" runat="server" MaxLength="14"></asp:TextBox></td>
                                                                                            <td style="width: 208px">
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px;">
                                                                                                3.
                                                                                            </td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    WCB Rating Code</div>
                                                                                            </td>
                                                                                            <td style="width: 144px;">
                                                                                                <div class="lbl">
                                                                                                    <asp:TextBox ID="txtWCBRatingCode" runat="server" MaxLength="20"></asp:TextBox></div>
                                                                                            </td>
                                                                                            <td style="width: 208px;">
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                                4.</td>
                                                                                            <td colspan="4">
                                                                                                <div class="lbl">
                                                                                                    Office Address(if changed previous report)</div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px;">
                                                                                            </td>
                                                                                            <td style="width: 141px;">
                                                                                                <div class="lbl">
                                                                                                    Number and Street</div>
                                                                                            </td>
                                                                                            <td style="width: 144px;">
                                                                                                <asp:TextBox ID="txtOfficeAdd" runat="server" MaxLength="50"></asp:TextBox></td>
                                                                                            <td style="width: 208px">
                                                                                                <div class="lbl">
                                                                                                    City</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOfficeCity" runat="server" MaxLength="26"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px;">
                                                                                            </td>
                                                                                            <td style="width: 141px;">
                                                                                                <div class="lbl">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td style="width: 144px;">
                                                                                                <%--<asp:TextBox ID="txtOfficeState" runat="server" MaxLength="50"></asp:TextBox>--%>
                                                                                                <cc1:ExtendedDropDownList ID="txtOfficeState" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                                                </cc1:ExtendedDropDownList>
                                                                                            </td>
                                                                                            <td style="width: 208px">
                                                                                                <div class="lbl">
                                                                                                    Zip Code</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOfficeZip" runat="server" MaxLength="8"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <div class="lbl">
                                                                                                    <asp:CheckBox ID="chkCopySameAddress" runat="server" onclick="CopySameAddress();"
                                                                                                        Text="Copy Same Address for Biiling" /></div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                                5.</td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    Billing Address</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                            </td>
                                                                                            <td style="width: 208px">
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                            </td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    Number and Street</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                                <asp:TextBox ID="txtBillingAdd" runat="server" MaxLength="53"></asp:TextBox></td>
                                                                                            <td style="width: 208px">
                                                                                                <div class="lbl">
                                                                                                    City</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtBillingCity" runat="server" MaxLength="26"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                            </td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                                <%--<asp:TextBox ID="txtBillingState" runat="server" MaxLength="50"></asp:TextBox>--%>
                                                                                                <cc1:ExtendedDropDownList ID="extddlOfficeState" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                                                </cc1:ExtendedDropDownList>
                                                                                            </td>
                                                                                            <td style="width: 208px">
                                                                                                <div class="lbl">
                                                                                                    Zip Code</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtBillingZip" runat="server" MaxLength="8"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px; height: 30px">
                                                                                                6.</td>
                                                                                            <td style="width: 141px; height: 30px;">
                                                                                                <div class="lbl">
                                                                                                    Office Phone #.</div>
                                                                                            </td>
                                                                                            <td style="width: 144px; height: 30px;">
                                                                                                <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50"></asp:TextBox></td>
                                                                                            <td style="height: 30px; width: 208px;">
                                                                                            </td>
                                                                                            <td style="height: 30px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                                7.</td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    Billing Phone #.</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                                <asp:TextBox ID="txtBillingPhone" runat="server" MaxLength="50"></asp:TextBox></td>
                                                                                            <td style="width: 208px">
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                                8.</td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    NPI #.</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                                <asp:TextBox ID="txtNPI" runat="server" MaxLength="50"></asp:TextBox></td>
                                                                                            <td style="width: 208px">
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                                9.</td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    Federal Tax ID #. is the</div>
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                                <asp:TextBox ID="txtFederalTax" runat="server" MaxLength="21"></asp:TextBox></td>
                                                                                            <td style="width: 208px">
                                                                                                <div class="lbl">
                                                                                                    Tax ID #. is the(Check one)</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <div class="lbl">
                                                                                                    <%--<asp:CheckBoxList ID="chklstTaxType" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0">SSN</asp:ListItem>
                                                            <asp:ListItem Value="1">EIN</asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                                                                    <asp:RadioButtonList ID="rdlstTaxType" runat="server" RepeatDirection="Horizontal">
                                                                                                        <asp:ListItem Value="0" >SSN</asp:ListItem>
                                                                                                        <asp:ListItem Value="1">EIN</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                                10.
                                                                                            </td>
                                                                                            <td style="width: 141px">
                                                                                                <div class="lbl">
                                                                                                    Billing Group</div>
                                                                                            </td>
                                                                                            <td style="width: 144px" colspan="3">
                                                                                                <asp:TextBox ID="txtbillingGroup" runat="server" Width="64%" MaxLength="100"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </th>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div id="div_contect_field_buttons">
                                                                    <table>
                                                                        <tr>
                                                                            <td colspan="4" width="100%" align="center">
                                                                                <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtDoctorID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                                <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px"
                                                                                    CssClass="btn-gray" OnClick="btnSaveAndGoToNext_Click" />
                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" />
                                                                                <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtTaxType" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                <%-- <asp:TextBox ID="txtOfficeState" runat="server" Visible="False" Width="2px"></asp:TextBox>
                                                        <asp:TextBox ID="txtBillingState" runat="server" Visible="False" Width="2px"></asp:TextBox>--%>
                                                                            </td>
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
					  </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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