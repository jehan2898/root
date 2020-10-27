<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ReturnToWorkC4_2.aspx.cs"
    Inherits="Bill_Sys_ReturnToWorkC4_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>
     <script src="js/scan/jquery.js"></script>
    <script language="javascript">
        $("#chklstPatientLimitationAllReason").live("click", function () {
            debugger;
            var selectedValues = "";
            $("[id*=chklstPatientLimitationAllReason]   :checked").each(function () {
                if (selectedValues == "") {
                    selectedValues = "Selected Values:\r\n\r\n";
                }
                selectedValues += $(this).val() + "\r\n";
            });
            if (selectedValues != "") {
                $('#chkPatientReturnToWorkWithlimitation').attr('checked', true);
                $('#chkPatientReturnToWorkWithoutLimitation').attr('checked', false);
                $('#chkPatientcannotReturn').attr('checked', false);
            } else {
                $('#chkPatientReturnToWorkWithlimitation').attr('checked', false);
            }
        });



    </script>
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
            var chkpatientcannotreturn=document.getElementById('chkPatientcannotReturn');
            
            var chkLimitationAllReason=document.getElementById('chklstPatientLimitationAllReason');
            var chkBoxCount= chkLimitationAllReason.getElementsByTagName("input");



            if(chkpatientcannotreturn.checked==true)
            {
           
            for(var i=0;i<chkBoxCount.length;i++) 

            {

             chkBoxCount[i].checked = false;

            }
                chkWithLimitation.checked=false;
                chkWithoutLimitation.checked=false;
                chkLimitationAllReason.checked=false;
            }
             if(chkpatientcannotreturn.checked==true)
             {
             document.getElementById('txtPatientWorkWithLimitaion').value="";
             document.getElementById('txtPatientWorkWithoutLimitaion').value="";
             document.getElementById('txtQuantifyTheLimitaion').value="";
             document.getElementById('txtOtherLimitation').value="";
             }
             else if(chkpatientcannotreturn.checked==false)
            {
            
             document.getElementById('txtPatientcannotReturn').value="";
            
            }
            
         }
    
    function checkvalidate_b()
         {
           
            var chkWithoutLimitation=document.getElementById('chkPatientReturnToWorkWithoutLimitation');
            var chkWithLimitation=document.getElementById('chkPatientReturnToWorkWithlimitation');
            var chkpatientcannotreturn=document.getElementById('chkPatientcannotReturn');
            var chkLimitationAllReason=document.getElementById('chklstPatientLimitationAllReason');
            var chkBoxCount= chkLimitationAllReason.getElementsByTagName("input");



            if(chkWithoutLimitation.checked==true)
            {
           
            for(var i=0;i<chkBoxCount.length;i++) 

            {

             chkBoxCount[i].checked = false;

            }
                chkWithLimitation.checked=false;
                chkpatientcannotreturn.checked=false;
                chkLimitationAllReason.checked=false;
            }
             if(chkWithoutLimitation.checked==true)
             {
             document.getElementById('txtPatientWorkWithLimitaion').value="";
             document.getElementById('txtPatientcannotReturn').value="";
             document.getElementById('txtQuantifyTheLimitaion').value="";
             document.getElementById('txtOtherLimitation').value="";
             }
             
              else if(chkWithoutLimitation.checked==false)
            {
            
            document.getElementById('txtPatientWorkWithoutLimitaion').value="";
            
            }
            
            
         }
         
         function checkvalidate_c()
         {
        
            var chkWithoutLimitation=document.getElementById('chkPatientReturnToWorkWithoutLimitation');
            var chkWithLimitation=document.getElementById('chkPatientReturnToWorkWithlimitation');
            var chkpatientcannotreturn=document.getElementById('chkPatientcannotReturn');
            var chkLimitationAllReason=document.getElementById('chklstPatientLimitationAllReason');
                 var chkBoxCount= chkLimitationAllReason.getElementsByTagName("input");
            if(chkWithLimitation.checked==true)
            {
                chkWithoutLimitation.checked=false;
                chkpatientcannotreturn.checked=false;
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
             document.getElementById('txtPatientcannotReturn').value="";
             
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
                                        Return To Work
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
                                                                    <td style="width: 56px">
                                                                        1.</td>
                                                                    <td style="height: 24px">
                                                                        <div class="lbl">
                                                                            is the patient working now?</div>
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                        <div class="lbl">
                                                                            <%--   <asp:CheckBoxList ID="chklstPatientWorking" AutoPostBack="true" runat="server"
                                                                RepeatDirection="Horizontal" >
                                                                <asp:ListItem Value=1>Yes</asp:ListItem>
                                                                <asp:ListItem Value=0>No</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                            <asp:RadioButtonList ID="rdlstPatientWorking" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="1">No</asp:ListItem>
                                                                                <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                        <div class="lbl">
                                                                            if yes, are there work restrictions?
                                                                        </div>
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                        <div class="lbl">
                                                                            <%--<asp:CheckBoxList ID="chkWorkRestriction" AutoPostBack="true" runat="server"
                                                                RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                                            <asp:RadioButtonList ID="rdlstWorkRestriction" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="1">No</asp:ListItem>
                                                                                <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                        <div class="lbl">
                                                                            if yes, Describe the work restrictions</div>
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px; height: 39px;">
                                                                    </td>
                                                                    <td colspan="3" style="height: 39px">
                                                                        <asp:TextBox ID="txtDescribeWorkRestriction" runat="server" Width="355px" MaxLength="139"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td style="height: 24px; vertical-align: top;">
                                                                        <div class="lbl">
                                                                            How long wil the work restrictions apply?</div>
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                        <div class="lbl">
                                                                            <%--<asp:CheckBoxList ID="chklstHowLongRestrictionApply" runat="server">
                                                            <asp:ListItem Value="0">1-2 days</asp:ListItem>
                                                            <asp:ListItem Value="1">3-7 days</asp:ListItem>
                                                            <asp:ListItem Value="2">8-14 days</asp:ListItem>
                                                            <asp:ListItem Value="3">15+ days</asp:ListItem>
                                                            <asp:ListItem Value="4">Unknown at this time</asp:ListItem>
                                                        </asp:CheckBoxList>--%>
                                                                            <asp:RadioButtonList ID="rdlstHowLongRestrictionApply" runat="server">
                                                                                <asp:ListItem Value="0" Selected="True">1-2 days</asp:ListItem>
                                                                                <asp:ListItem Value="1">3-7 days</asp:ListItem>
                                                                                <asp:ListItem Value="2">8-14 days</asp:ListItem>
                                                                                <asp:ListItem Value="3">15+ days</asp:ListItem>
                                                                                <asp:ListItem Value="4">Unknown at this time</asp:ListItem>
                                                                                <asp:ListItem Value="5" Selected="True">None</asp:ListItem>
                                                                            </asp:RadioButtonList></div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                        2.</td>
                                                                    <td style="height: 24px" colspan="3">
                                                                        <div class="lbl">
                                                                            Can the patient return to work? (check only one):
                                                                            <asp:TextBox ID="hdntxtPatientCanReturnWork" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                            <asp:TextBox ID="hdntxtPatientCanReturnWorkDescription" runat="server" Visible="false"
                                                                                Width="10px"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    a.
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <div class="lbl">
                                                                            <asp:CheckBox ID="chkPatientcannotReturn" runat="server" Text="The patient cannot return to work because (explain):" /></div>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtPatientcannotReturn" runat="server" TextMode="multiLine" Height="80px"
                                                                            Width="420px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                        b.</td>
                                                                    <td colspan="2" style="height: 24px; text-align: left;">
                                                                        <div class="lbl">
                                                                            <asp:CheckBox ID="chkPatientReturnToWorkWithoutLimitation" runat="server" Text="The patient can return to work without limitations on" /></div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPatientWorkWithoutLimitaion" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnPatientWorkWithoutLimitaion" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                        <ajaxToolkit:CalendarExtender ID="calextPatientWorkWithoutLimitaion" runat="server"
                                                                            TargetControlID="txtPatientWorkWithoutLimitaion" PopupButtonID="imgbtnPatientWorkWithoutLimitaion" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px; height: 40px">
                                                                        c.</td>
                                                                    <td colspan="2" style="height: 40px; text-align: left;">
                                                                        <div class="lbl">
                                                                            <asp:CheckBox ID="chkPatientReturnToWorkWithlimitation" runat="server" Text="The patient can return to work with the following limitations (check all that apply) on" /></div>
                                                                    </td>
                                                                    <td style="height: 40px">
                                                                        <asp:TextBox ID="txtPatientWorkWithLimitaion" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnPatientWorkWithLimitaion" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                        <ajaxToolkit:CalendarExtender ID="calextPatientWorkWithLimitaion" runat="server"
                                                                            TargetControlID="txtPatientWorkWithLimitaion" PopupButtonID="imgbtnPatientWorkWithLimitaion" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px;">
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <div class="lbl">
                                                                            <asp:CheckBoxList ID="chklstPatientLimitationAllReason" CssClass="check2"  runat="server" RepeatColumns="3">
                                                                                <asp:ListItem Value="0" >Bending/twisting</asp:ListItem>
                                                                                <asp:ListItem Value="1">Lifting</asp:ListItem>
                                                                                <asp:ListItem Value="2">Sitting</asp:ListItem>
                                                                                <asp:ListItem Value="3">Climbing stairs/ladders</asp:ListItem>
                                                                                <asp:ListItem Value="4">Operating heavy equipment</asp:ListItem>
                                                                                <asp:ListItem Value="5">Standing</asp:ListItem>
                                                                                <asp:ListItem Value="6">Environmental conditions</asp:ListItem>
                                                                                <asp:ListItem Value="7">Operation of motor vehicles</asp:ListItem>
                                                                                <asp:ListItem Value="8">Use of public transportation</asp:ListItem>
                                                                                <asp:ListItem Value="9">Kneeling</asp:ListItem>
                                                                                <asp:ListItem Value="10">Personal protective equipment</asp:ListItem>
                                                                                <asp:ListItem Value="11">Use of upper extremities</asp:ListItem>
                                                                                <asp:ListItem Value="12">Other (explain):</asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px;">
                                                                    </td>
                                                                    <td style="vertical-align: bottom; padding-bottom: 10px;" colspan="4">
                                                                        <asp:TextBox ID="txtOtherLimitation" runat="server" Width="420px" MaxLength="56"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td colspan="2" style="height: 24px; text-align: left;">
                                                                        <div class="lbl">
                                                                            Describe/quantify the limitations:</div>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td colspan="3" style="height: 24px; text-align: left">
                                                                        <asp:TextBox ID="txtQuantifyTheLimitaion" runat="server" Width="421px" MaxLength="58"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px">
                                                                    </td>
                                                                    <td colspan="2" style="height: 24px; text-align: left; vertical-align: top;">
                                                                        <div class="lbl">
                                                                            How long will these limitations apply?</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="lbl">
                                                                            <asp:RadioButtonList ID="rdlstHowLongLimitaionApply" runat="server">
                                                                                <asp:ListItem Value="0" Selected="True">1-2 days</asp:ListItem>
                                                                                <asp:ListItem Value="1">3-7 days</asp:ListItem>
                                                                                <asp:ListItem Value="2">8-14 days</asp:ListItem>
                                                                                <asp:ListItem Value="3">15+ days</asp:ListItem>
                                                                                <asp:ListItem Value="4">Unknown at this time</asp:ListItem>
                                                                                <asp:ListItem Value="5">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="6" Selected="True">None</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <%-- <asp:CheckBoxList ID="chklstHowLongLimitaionApply" runat="server">
                                                                <asp:ListItem Value="0">1-2 days</asp:ListItem>
                                                                <asp:ListItem Value="1">3-7 days</asp:ListItem>
                                                                <asp:ListItem Value="2">8-14 days</asp:ListItem>
                                                                <asp:ListItem Value="3">15+ days</asp:ListItem>
                                                                <asp:ListItem Value="4">Unknown at this time</asp:ListItem>
                                                                <asp:ListItem Value="5">N/A</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align: top; width: 56px; height: 92px;">
                                                                        3.</td>
                                                                    <td colspan="2" style="vertical-align: top; height: 92px;">
                                                                        <div class="lbl">
                                                                            With whom will you discuss the patient's return to work and/or limitations?</div>
                                                                    </td>
                                                                    <td style="height: 92px">
                                                                        <div class="lbl">
                                                                            <asp:RadioButtonList ID="rdlstDiscussPatientReturn" runat="server">
                                                                                <asp:ListItem Value="0" Selected="True">with patient</asp:ListItem>
                                                                                <asp:ListItem Value="1">with patient's employer</asp:ListItem>
                                                                                <asp:ListItem Value="2">N/A</asp:ListItem>
                                                                                <asp:ListItem Value="3" Selected="True">None</asp:ListItem>
                                                                            </asp:RadioButtonList><%--<asp:CheckBoxList ID="chklstDiscussPatientReturn" runat="server">
                                                                <asp:ListItem Value="0">with patient</asp:ListItem>
                                                                <asp:ListItem Value="1">with patient's employer</asp:ListItem>
                                                                <asp:ListItem Value="2">N/A</asp:ListItem>
                                                            </asp:CheckBoxList>--%></div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align: top; width: 56px;">
                                                                        4.</td>
                                                                    <td colspan="2" style="vertical-align: top;">
                                                                        Would the patient benefit from vocational rehabilitation?</td>
                                                                    <td>
                                                                        <%--<asp:CheckBoxList ID="chklstPatientBenefit" AutoPostBack="true" runat="server"
                                                                RepeatDirection="Horizontal" >
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:CheckBoxList>--%>
                                                                        <div class="lbl">
                                                                            <asp:RadioButtonList ID="rdlstPatientBenefit" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="1">No</asp:ListItem>
                                                                                <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" width="100%" style="height: 26px">
                                                                        This form is signed under penalty of perjury.
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" width="100%">
                                                                        Board Authorized <span class="lbl">Health Care Provider - Check one:</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" width="100%">
                                                                        <div class="lbl">
                                                                            <%--      <asp:CheckBoxList ID="chklstHealthCareProvider" runat="server">
                                                                <asp:ListItem Value="0">I provided the services listed above.</asp:ListItem>
                                                                <asp:ListItem Value="1">I actively supervised the health-care provider named below who provided these services.</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                            <asp:RadioButtonList ID="rdlstHealthCareProvider" runat="server">
                                                                                <asp:ListItem Value="0" Selected="True">I provided the services listed above.</asp:ListItem>
                                                                                <asp:ListItem Value="1">I actively supervised the health-care provider named below who provided these services.</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 56px; height: 34px">
                                                                        <div class="lbl">
                                                                            Provider's name</div>
                                                                    </td>
                                                                    <td style="height: 34px">
                                                                        <asp:TextBox ID="txtProviderName" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 34px">
                                                                        <div class="lbl">
                                                                            Specialty</div>
                                                                    </td>
                                                                    <td style="height: 34px">
                                                                        <asp:TextBox ID="txtProviderSpeciality" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" width="100%">
                                                                        Board Authorized <span class="lbl">Health Care Provider signature: </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 17px; width: 56px;">
                                                                        <div class="lbl">
                                                                            Name</div>
                                                                    </td>
                                                                    <td style="height: 17px">
                                                                        <asp:TextBox ID="txtAuthProviderName" runat="server" MaxLength="50"></asp:TextBox></td>
                                                                    <td style="height: 17px">
                                                                        <div class="lbl">
                                                                            Sepciality</div>
                                                                    </td>
                                                                    <td style="height: 17px">
                                                                        <asp:TextBox ID="txtAuthProviderSpeciality" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 17px; width: 56px;">
                                                                        <div class="lbl">
                                                                            Date</div>
                                                                    </td>
                                                                    <td style="height: 17px">
                                                                        <asp:TextBox ID="txtProviderDate" runat="server" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnBoardAuthProviderDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                        <ajaxToolkit:CalendarExtender ID="calextProviderDate" runat="server" TargetControlID="txtProviderDate"
                                                                            PopupButtonID="imgbtnBoardAuthProviderDate" />
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
                                                                        <asp:Button ID="btnSaveAndGoToNext" runat="server" Text="Save" Width="80px" OnClick="btnSaveAndGoToNext_Click"
                                                                            CssClass="btn-gray" />
                                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" />
                                                                        <asp:TextBox ID="txtWorkStatusID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtDoctorName" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    </td>
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
