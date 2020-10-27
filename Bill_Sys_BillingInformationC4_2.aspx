<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_BillingInformationC4_2.aspx.cs"
    Inherits="Bill_Sys_BillingInformationC4_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
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
    <form id="frmPlanOfCare" runat="server">
        <div>
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
                <td colspan="6" style="height: 454px; vertical-align: top;">
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
                            <td width="83%" scope="col">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="3" scope="col">
                                            <div align="left" class="blocktitle">
                                                Billing Information
                                                <div id="divPlanOfCare" class="div_blockcontent">
                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                        <tr>
                                                            <td colspan="5">
                                                                <div id="ErrorDiv" visible="true" style="color: Red;">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top; width: 16px;">
                                                                1.</td>
                                                            <td style="vertical-align: top; width: 225px;">
                                                                <div class="lbl">
                                                                    Employers Insurace Carrier</div>
                                                            </td>
                                                            <td style="width: 121px">
                                                                <asp:TextBox ID="txtInsuranceName" runat="Server" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top; width: 16px;">
                                                                2.</td>
                                                            <td style="width: 225px">
                                                                Carrier Code #. W</td>
                                                            <td style="width: 121px">
                                                                <asp:TextBox ID="txtcarriercode" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 16px">
                                                                3.
                                                            </td>
                                                            <td style="width: 225px">
                                                                <div class="lbl">
                                                                    Insurance Carrier's Address</div>
                                                            </td>
                                                            <td style="width: 121px">
                                                                <div class="lbl">
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 16px">
                                                            </td>
                                                            <td style="width: 225px;">
                                                                <div class="lbl">
                                                                    Number and Street</div>
                                                            </td>
                                                            <td style="width: 121px;">
                                                                <asp:TextBox ID="txtInsuranceStreet" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                            <td>
                                                                <div class="lbl">
                                                                    City</div>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtInsuranceCity" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 16px">
                                                            </td>
                                                            <td style="width: 225px">
                                                                <div class="lbl">
                                                                    State</div>
                                                            </td>
                                                            <td style="width: 121px">
                                                                <cc1:ExtendedDropDownList ID="txtInsuranceState" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Enabled="false"
                                                                    Connection_Key="Connection_String"></cc1:ExtendedDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 29px; width: 16px;">
                                                            </td>
                                                            <td style="width: 225px; height: 29px;">
                                                                <div class="lbl">
                                                                    Zip Code</div>
                                                            </td>
                                                            <td style="width: 121px; height: 29px;">
                                                                <asp:TextBox ID="txtInsuranceZip" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                            <td style="height: 29px">
                                                            </td>
                                                            <td style="height: 29px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 16px">
                                                                4.</td>
                                                            <td style="width: 225px" colspan="4">
                                                                <div class="lbl">
                                                                    Diagnosis or nature of disease or injury</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" style="height: 29px">
                                                                <asp:GridView ID="grdDignosisCode" Width="100%" runat="server" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="SZ_DIAGNOSIS_CODE" HeaderText="Enter ICD9 Code" HeaderStyle-Width="25%" />
                                                                        <asp:BoundField DataField="SZ_DESCRIPTION" HeaderText="ICD9 Description" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" height="30px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:GridView ID="grdBillingInformation" runat="server" Width="100%" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="MONTH" HeaderText="From MM"></asp:BoundField>
                                                                        <asp:BoundField DataField="DAY" HeaderText="DD"></asp:BoundField>
                                                                        <asp:BoundField DataField="YEAR" HeaderText="YY"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="TO MM"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="DD"></asp:BoundField>
                                                                        <asp:BoundField HeaderText="YY"></asp:BoundField>
                                                                        <asp:BoundField DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure, Services or supplies">
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundField>
                                                                        <asp:BoundField DataField="FL_AMOUNT" ItemStyle-HorizontalAlign="Right" HeaderText="$ Charges"></asp:BoundField>
                                                                        <asp:BoundField DataField="I_UNIT" ItemStyle-HorizontalAlign="Center" HeaderText="Days/Units"></asp:BoundField>
                                                                        <asp:BoundField DataField="BILL_AMOUNT" HeaderText="Total Amount" Visible="false"></asp:BoundField>
                                                                        <asp:BoundField DataField="PAID_AMOUNT" HeaderText="Total Paid Amount" Visible="false">
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="BALANCE" HeaderText="Total Balance" Visible="false"></asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 223px">
                                                                        </td>
                                                                        <td style="width: 223px">
                                                                        </td>
                                                                        <td style="width: 106px">
                                                                            <asp:Label ID="lblTotalCharges" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 200px">
                                                                            <asp:Label ID="lblAmountPaid" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 200px">
                                                                            <asp:Label ID="lblBalanceDue" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td style="width: 223px">
                                                                        </td>
                                                                        <td style="width: 106px">
                                                                            <asp:Label ID="lblTotalChargeAmt" runat="server" Text="Label"></asp:Label></td>
                                                                        <td style="width: 108px">
                                                                            <asp:Label ID="lblTotalPaidAmt" runat="server" Text="Label"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblTotalBalanceAmt" runat="server" Text="Label"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:CheckBox ID="chkPPO" runat="server" Text="Check here if services were provided by a WCB preferred provider organization (PPO)" /></td>
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
                                                            <td colspan="4" align="center" style="width: 100%">
                                                                <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtInsuranceID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px"
                                                                    CssClass="btn-gray" OnClick="btnSaveAndGoToNext_Click" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" />
                                                                <asp:TextBox ID="txtBillNumber" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtPPO" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                <%--<asp:TextBox ID="txtInsState" runat="server" Width="10px" Visible="false"></asp:TextBox>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
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
            </table>
        </div>
    </form>
</body>
</html>
