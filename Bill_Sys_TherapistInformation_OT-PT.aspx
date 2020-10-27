<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_TherapistInformation_OT-PT.aspx.cs"
    Inherits="Bill_Sys_TherapistInformation_OT_PT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
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
         
         function CopySameAddress()
         {
            var chk = document.getElementById('chkCopySameAddress');
            
            if(chk.checked)
            {
               document.getElementById('txtBillingAdd').value= document.getElementById('txtOfficeAdd').value;
               document.getElementById('txtBillingCity').value= document.getElementById('txtOfficeCity').value;
               document.getElementById('extddlOfficeState').value= document.getElementById('txtOfficeState').value;
              document.getElementById('txtBillingZip').value= document.getElementById('txtOfficeZip').value;
            }
            else
            {
                document.getElementById('txtBillingAdd').value="";
                document.getElementById('txtBillingCity').value="";
                document.getElementById('extddlOfficeState').value="NA";
                document.getElementById('txtBillingZip').value="";
            }
            
         }
    </script>

</head>
<body>
    <form id="frmPlanOfCare" runat="server">
        <div>
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
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
                                                                                    <li><a href="Bill_Sys_PatientInformationOT-PT.aspx">Patient Information </a></li>
                                                                                    <li><a href="Bill_Sys_Report_OT-PT.aspx">Report Information </a></li>
                                                                                    <li><a href="Bill_Sys_History_OT-PT.aspx">History Information</a></li>
                                                                                    <li><a href="Bill_Sys_Treatment_OT-PT.aspx">Evaluation/Treatment </a></li>
                                                                                    <li><a href="Bill_Sys_BillingInformationOT-PT.aspx">Billing Information </a></li>
                                                                                    <li><a href="Bill_Sys_TherapistInformation_OT-PT.aspx">Therapist Information </a></li>
                                                                                </ul>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td width="69%" scope="col">
                                                            <div class="blocktitle">
                                                                Therapist Information
                                                                <div class="div_blockcontent">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <th width="100%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                                                                <div align="left">
                                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <div id="ErrorDiv" visible="true" style="color: Red;">
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top; width: 141px;" colspan="3">
                                                                                                <div class="lbl">
                                                                                                    Therapist's Name,Address,Phone No:</div>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtTheripastName" runat="server" Width="80%" ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top; width: 141px;" colspan="3">
                                                                                                <div class="lbl">
                                                                                                    Therapist's Billing Name,Address,Phone No:</div>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtbillingaddress" runat="server" Width="80%" ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 141px" colspan="3">
                                                                                                <div class="lbl">
                                                                                                    Federal Tax I.D. Number</div>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtfederaltaxidnumber" runat="server" MaxLength="50" ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 141px" colspan="3">
                                                                                                <div class="lbl">
                                                                                                    NYS License Number</div>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtnynlicenseno" runat="server" MaxLength="50" ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <div class="lbl">
                                                                                                    <asp:RadioButtonList ID="rdlstTaxType" runat="server" RepeatDirection="Horizontal"
                                                                                                        Enabled="false">
                                                                                                        <asp:ListItem Value="0" Selected="True">SSN</asp:ListItem>
                                                                                                        <asp:ListItem Value="1">EIN</asp:ListItem>
                                                                                                        <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 13px">
                                                                                            </td>
                                                                                            <td style="width: 141px">
                                                                                            </td>
                                                                                            <td style="width: 144px">
                                                                                            </td>
                                                                                            <td style="width: 208px">
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
            <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtBillNo" runat="server" Visible="false"></asp:TextBox>
        </div>
    </form>
</body>
</html>
