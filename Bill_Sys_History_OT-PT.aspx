<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_History_OT-PT.aspx.cs"
    Inherits="Bill_Sys_History_OT_PT" %>

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
    
    function checkvalidate_a()
         {
           
            var chkWithoutLimitation=document.getElementById('chkPatientReturnToWorkWithoutLimitation');
            var chkWithLimitation=document.getElementById('chkPatientReturnToWorkWithlimitation');
            
            var chkLimitationAllReason=document.getElementById('chklstPatientLimitationAllReason');
            var chkBoxCount= chkLimitationAllReason.getElementsByTagName("input");



            if(chkWithoutLimitation.checked==true)
            {
           
            for(var i=0;i<chkBoxCount.length;i++) 

            {

             chkBoxCount[i].checked = false;

            }
                chkWithLimitation.checked=false;
                chkLimitationAllReason.checked=false;
            }
             if(chkWithoutLimitation.checked==true)
             {
             document.getElementById('txtPatientWorkWithLimitaion').value="";
             document.getElementById('txtQuantifyTheLimitaion').value="";
             document.getElementById('txtOtherLimitation').value="";
             }
            
            
         }
         
         function checkvalidate_b()
         {
           
            var chkWithoutLimitation=document.getElementById('chkPatientReturnToWorkWithoutLimitation');
            var chkWithLimitation=document.getElementById('chkPatientReturnToWorkWithlimitation');
            
            var chkLimitationAllReason=document.getElementById('chklstPatientLimitationAllReason');
                 var chkBoxCount= chkLimitationAllReason.getElementsByTagName("input");
            if(chkWithLimitation.checked==true)
            {
                chkWithoutLimitation.checked=false;
            }
            
             if(chkWithLimitation.checked==false)
            {
         
               for(var i=0;i<chkBoxCount.length;i++) 

                {

                 chkBoxCount[i].checked = false;

                }
            }
            if(chkWithLimitation.checked==true)
             {
             document.getElementById('txtPatientWorkWithoutLimitaion').value="";
             
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
    <form id="frmWorkStatus" runat="server">
        <div align="center">
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
                                        Patient's Injury History
                                        <div class="div_blockcontent">
                                            <table>
                                                <tr>
                                                    <th width="83%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                                        <div align="left">
                                                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                            </asp:ScriptManager>
                                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                <tr>
                                                                    <td colspan="4" style="height: 30px">
                                                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                              
                                                                <tr>
                                                                    <td style="width: 6px">
                                                                    </td>
                                                                    <td style="height: 24px; vertical-align: top;">
                                                                        <div class="lbl">
                                                                            1. Diagnosis of referring physician/podiatrist.</div>
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 6px">
                                                                    </td>
                                                                    <td colspan="3" style="height: 24px; text-align: left">
                                                                        <asp:TextBox ID="txtDiagnosis" runat="server" Width="100%" MaxLength="200" TextMode="MultiLine"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td colspan="2" style="height: 24px; text-align: left;">
                                                                        <div class="lbl">
                                                                            2.If patient has given any history of pre-existing injury, disease or physical impairment,
                                                                            describe specifically.</div>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td colspan="3" style="height: 24px; text-align: left">
                                                                        <asp:TextBox ID="txtprehistroyinjury" runat="server" Width="100%" MaxLength="200" TextMode="MultiLine"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="div_contect_field_buttons">
                                                            <table>
                                                                <tr>
                                                                    <td colspan="4" width="100%" align="center">
                                                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                        <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                        <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save" Width="80px" OnClick="btnSaveAndGoToNext_Click"
                                                                            CssClass="btn-gray" />
                                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" />
                                                                        <asp:TextBox ID="txtWorkStatusID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </th>
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
