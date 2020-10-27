<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_DoctorOpinion.aspx.cs"
    Inherits="Bill_Sys_DoctorOpinion" %>

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
    <form id="frmDoctorOpinion" runat="server">
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
              
                      <tr>
                    <td colspan="6" style="height: 454px">
                        <table id="content_table" border="0" cellspacing="0" cellpadding="0">
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
                            Doctor's Opinion
                            <div class="div_blockcontent">
                           
                              <table>
                            <tr>
                             <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                            <table>
                            <tr>
                              <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                <td colspan="3" scope="col">
                                
                                <div align="left">
                                
                                   
                              
                                            <table width="80%" border="0" align="center" cellpadding="0" cellspacing="3">
                                               
                                                <tr>
                                                    <td width="75%" colspan="3">
                                                       <div class="lbl"> 1. In your opinion, was the incident that the patient described the competent medical
                                                        cause of this injury/illness?</div> 
                                                    </td>
                                                    <td width="25%">
                                                       <div class="lbl"> 
                                                       
                                                      <%-- <asp:CheckBoxList ID="chkMedicalCauses" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                        <asp:RadioButtonList ID="rdlstMedicalCauses" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        
                                                        </div> 
                                                        
                                                       <%-- <asp:TextBox ID="txthdnMedicalCauses" runat="server" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="75%" colspan="3" valign="top">
                                                      <div class="lbl">  2. Are the patient's complaints consistent with his/her history of the injury/illness?</div> 
                                                    </td>
                                                    <td width="25%">
                                                       <div class="lbl"> 
                                                       
                                                       <%--<asp:CheckBoxList ID="chkHistoryOfInjury" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                        <asp:RadioButtonList ID="rdlstHistoryOfInjury" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        
                                                        </div> 
                                                        <%-- <asp:TextBox ID="txthdnHistoryOfInjury" runat="server" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%" colspan="2">
                                                       <div class="lbl"> 3. Is the patient's history of the injury/illness consistent with your objective
                                                        findings?</div> 
                                                    </td>
                                                    <td width="50%" colspan="2">
                                                       <div class="lbl"> 
                                                       
                                                      <%-- <asp:CheckBoxList ID="chkObjectiveFindings" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value=0></asp:ListItem>
                                                            <asp:ListItem Text="No" Value=1></asp:ListItem>
                                                            <asp:ListItem Text="N/A (no findings at this time)" Value=2></asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                        
                                                        <asp:RadioButtonList ID="rdlstObjectiveFindings" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                            <asp:ListItem Value="2">N/A (no findings at this time)</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        
                                                        </div> 
                                                        <asp:TextBox ID="txthdnObjectiveFindings" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%" colspan="2" style="height: 34px">
                                                      <div class="lbl">  4. What is the percentage (0-100%) of temporary impairment?</div> 
                                                    <td width="50%" colspan="2" style="height: 34px">
                                                        <asp:TextBox ID="txtPerTempImpairment" runat="server"></asp:TextBox>
                                                        %</td>
                                                </tr>
                                                <tr>
                                                    <td width="50%" colspan="4" valign="top">
                                                      <div class="lbl">  5. Describe findings and relevant diagnostic test results:</div> 
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                     <td width="50%" colspan="4">
                                                        <asp:TextBox ID="txtRelevantDiagosticTest" runat="server" TextMode="MultiLine" Height="80px" Width="420px" onkeyDown="return checkTextAreaMaxLength(this,event,'258');"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtBillNumber" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                       <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px" OnClick="btnSaveAndGoToNext_Click"
                                                            CssClass="btn-gray" />
                                                       <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                                                            CssClass="btn-gray" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                   
                                   
                                   
                                </td>
                                
                                 </tr>
                                        </table>
                                    </th>
                               </tr> 
                                                 </table> 
                               
                               </th>
                            </tr>
                        </table>
                                
                                </div>
                        </div>					  
					  </td>
		
		<%--<td valign="top" scope="col" style="width: 16%">
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
