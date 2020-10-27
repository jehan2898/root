<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs"
    Inherits="History" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
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
         
function Count(text,long)
 
{
 
      var maxlength = new Number(long); // Change number to your max length.
 
if(document.getElementById('txtInjuryHappen').value.length > maxlength){
 
            text.value = text.value.substring(0,maxlength);
 
            alert(" Only " + long + " chars");
 
}
    </script>

</head>
<body>
    <form id="frmHistory" runat="server">
        <div align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">  </asp:ScriptManager>
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td colspan="6" bgcolor="#EAEAEA" align="center" style="height: 20px">
                        <span class="message-text">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                        </span>
                    </td>
                </tr>
                
                
                     <tr>
                    <td colspan="6" style="height: 454px">
                        <table width="100%" id="Table1" border="0" cellspacing="0" cellpadding="0">
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
                        	History 
                        <div class="div_blockcontent">
                   <table>
                            <tr>
                  <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                        
                              <td colspan="6" style="height: 454px; vertical-align:top;">
                        <table width="100%" id="content_table" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                
                                <td colspan="3" scope="col" >                               
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                
                                                <tr>
                                                    <td valign="top" colspan="3">
                                                        1. How did the injury/illness happen
                                                    </td>
                                                    
                                                   
                                                </tr>
                                                <tr >
                                                     <td colspan="3" >
                                                        <asp:TextBox ID="txtInjuryHappen" runat="server"  TextMode="MultiLine" Height="80px"
                                                            Width="420px" CssClass="lbl"  onKeyUp="javascript:Count(this,391);" onChange="javascript:Count(this,391);"  ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        2. How did you learn about the injury/illness (check one):
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 47%">
                                                                   <%-- <asp:CheckBoxList ID="chklstInjury" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Patient" Value="Patient"></asp:ListItem>
                                                                        <asp:ListItem Text="Medical Records" Value="Medical Records"></asp:ListItem>
                                                                        <asp:ListItem Text="Other(specify)" Value="Other(specify)"></asp:ListItem>
                                                                    </asp:CheckBoxList>--%>
                                                                     <asp:RadioButtonList ID="rdlstInjury" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="Patient" Selected="True">Patient</asp:ListItem>
                                                            <asp:ListItem Value="Medical Records">Medical Records</asp:ListItem>
                                                            <asp:ListItem Value="Other(specify)">Other(specify)</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                                  <asp:TextBox ID="txthdnInjury" runat="server" Visible="false"></asp:TextBox>  
                                                                </td>
                                                                <td width="60%" valign="top">
                                                                    <asp:TextBox ID="txtLearnSourceDescription" runat="server" MaxLength="33"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr >
                                                    <td colspan="3" style="float:left; position:relative; text-align:left;">
                                                        3. Did another health provider treat this injury/illness including hospitalizaton
                                                        and/or surgery?</td>
                                                    
                                                   
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 16%; height: 52px;">
                                                                  <%--  <asp:CheckBoxList ID="chklstHospitalization" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:CheckBoxList>--%>
                                                                     <asp:RadioButtonList ID="rdlstHospitalization" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" >Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                            <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                                    
                                                                    <asp:TextBox ID="txthdnHospitalization" runat="server" Visible="false"></asp:TextBox>
                                                                </td>
                                                                <td width="70%" valign="top" style="height: 52px">
                                                                    If Yes Give Details :
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        
                                                        <asp:TextBox ID="txtHospitalization" runat="server" TextMode="MultiLine" Width="420px" Height="80px" MaxLength="165"></asp:TextBox>
                                                   
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        4. Have you previously treated this patient for a similar work-related injury/illness?
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 16%">
                                                                   <%-- <asp:CheckBoxList ID="chkPreviouslyTreated" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                         <asp:RadioButtonList ID="rdlstPreviouslyTreated" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" >Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                             <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:TextBox ID="txthdnPreviouslyTreated" runat="server" Visible="false"></asp:TextBox>
                                                                </td>
                                                                <td width="70%" valign="top">
                                                                    If yes, when:
                                                        <asp:TextBox ID="txtPreviouslyTreated" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ID="imgbtnPreviouslyTreatedDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPreviouslyTreated"  PopupButtonID="imgbtnPreviouslyTreatedDate" />
                                                        
                                                        
                                                                </td>
                                                            </tr>
                                                            
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                  
                                        <div id="div_contect_field_buttons">
                                            <table>
                                                <tr>
                                                    <td colspan="4" width="100%" align="center">
                                                       <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                       <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="false"></asp:TextBox>
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
                    </td>
                    
                    </tr>
                                        </table>
                                    </th>
                  </tr>
                        </table>
                    
                    </div>
                        </div>					  
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
