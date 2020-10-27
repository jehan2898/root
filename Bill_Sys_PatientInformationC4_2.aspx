<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_PatientInformationC4_2.aspx.cs" Inherits="Bill_Sys_PatientInformationC4_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>  
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
    </script>

</head>
<body>
    <form id="frmPlanOfCare" runat="server">
        <div>
         <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td colspan="6" bgcolor="#EAEAEA" align="center" style="height: 20px">
                        <span class="message-text">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label></span>
                    </td>
                </tr>
              
               <tr>
                
                  
                <td colspan="6" style="height: 454px; vertical-align:top;">
                           <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
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
                                <td colspan="3" scope="col" width="69%" valign="top" >
                                
                                    <div align="left" class="blocktitle">
                                    Patient Information
                                    <div id="divPlanOfCare" class="div_blockcontent" >
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                               
                                                <tr>
                                                    <td colspan="5" >
                                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top">
                                                    </td>
                                                    <td style="vertical-align: top; width: 199px;">
                                                      <div class="lbl">  Date's of Examination</div> </td>
                                                    <td style="width: 154px">
                                                        <asp:TextBox ID="txtDateofExam" runat="server" onkeypress="return CheckForInteger(event,'/')" MaxLength="10" Enabled="False" ReadOnly="True" Width="209px"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnDateOfExam" runat="server" ImageUrl="~/Images/cal.gif" Visible="False" />
                                                        <ajaxToolkit:CalendarExtender ID="calExtDateofExam" runat="server" TargetControlID="txtDateofExam"
                                                                                                        PopupButtonID="imgbtnDateOfExam" />
                                                        
                                                        </td>   
                                                        <td>
                                                        </td>                                                
                                                        <td>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; height: 52px;">
                                                    </td>
                                                    <td style="vertical-align: top; width: 199px; height: 52px;">
                                                       <div class="lbl"> WCB Case Number (If Known)</div> </td>
                                                    <td style="width: 154px; height: 52px;">
                                                        <asp:TextBox ID="txtWCBCaseNumber" runat="server" MaxLength="16"></asp:TextBox></td>
                                                    <td style="height: 52px">
                                                       <div class="lbl"> Carrier Case Number (if Known)</div> </td>
                                                    <td style="height: 52px">
                                                        <asp:TextBox ID="txtCaseCarrierNo" runat="server" MaxLength="16"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="vertical-align: top; height:20px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        1.</td>
                                                         <td style="vertical-align: top; width: 199px;">
                                                       <div class="lbl"> Patient Name:</div> </td>
                                                    <td style="width: 154px">
                                                    <asp:TextBox ID="txtPatientFirstName" runat="Server" ReadOnly="True" ></asp:TextBox>
                                                    </td>
                                                    <td >
                                                    <asp:TextBox ID="txtPatientMiddleName" runat="Server" ReadOnly="True" ></asp:TextBox>
                                                       </td>
                                                    <td>
                                                    <asp:TextBox ID="txtPatientLastName" runat="Server" ReadOnly="True" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        </td>
                                                        <td style="width: 199px">
                                                        </td>
                                                    <td style="width: 154px">
                                                        <div class="lbl">First Name</div> </td>
                                                        <td>
                                                        <div class="lbl">Middle Name</div> </td>
                                                        <td>
                                                        <div class="lbl">Last Name</div> </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 69px">
                                                        2. </td>
                                                        <td style="width: 199px; height: 69px;" >
                                                      <div class="lbl">Date of injury/ Illness</div>  
                                                        </td>
                                                    <td style="width: 154px; height: 69px;" >
                                                        <div class="lbl">
                                                        <asp:TextBox ID="txtDateOfInjury" runat="server" onkeypress="return CheckForInteger(event,'/')" MaxLength="10"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnDateOfInjury" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calextDateOfInjury" runat="server" TargetControlID="txtDateOfInjury"
                                                                                                        PopupButtonID="imgbtnDateOfInjury" />
                                                        </div>
                                                        
                                                    </td>
                                                    <td style="height: 69px">
                                                      <div class="lbl"> 3. Soc. Sec. #</div> </td>
                                                        <td style="height: 69px">
                                                            <asp:TextBox ID="txtSocialSecurity" runat="server" MaxLength="12"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="height: 29px">
                                                       <div class="lbl"> 4.Address(if changed previous report)</div> </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                    </td>
                                                    <td style="width: 199px; ">
                                                       <div class="lbl"> Number and Street</div> 
                                                    </td>
                                                    <td style="width: 154px; ">
                                                        <asp:TextBox ID="txtPatientStreet" runat="server"></asp:TextBox></td>
                                                    <td >
                                                       <div class="lbl"> City</div> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPatientCity" runat="server" MaxLength="12"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="width: 199px">
                                                       <div class="lbl"> State</div> </td>
                                                    <td style="width: 154px">
                                                         <cc1:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="90%" Selected_Text="--- Select ---"
                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                                </cc1:ExtendedDropDownList>  </td>
                                                        <%--<asp:TextBox ID="txtPatientState" runat="server"></asp:TextBox></td>--%>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="width: 199px">
                                                      <div class="lbl">  Zip Code</div> </td>
                                                    <td style="width: 154px">
                                                        <asp:TextBox ID="txtPatientZip" runat="server" MaxLength="8"></asp:TextBox></td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        5.</td>
                                                    <td style="width: 199px">
                                                      <div class="lbl">  Patient Account #.</div> </td>
                                                    <td style="width: 154px">
                                                        <asp:TextBox ID="txtCaseID" runat="server" MaxLength="14"></asp:TextBox></td>
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
                                                       <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save & Next" Width="80px"
                                                            CssClass="btn-gray" OnClick="btnSaveAndGoToNext_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px"
                                                            CssClass="btn-gray" />
                                                        <asp:TextBox ID="txtBillNumber" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtstateid" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                        <asp:HiddenField ID="btnhidden" runat="server" />
                                                        </td>
                                                </tr>
                                            </table>
                                        </div>
                                   
                                   
                                    </div>
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
