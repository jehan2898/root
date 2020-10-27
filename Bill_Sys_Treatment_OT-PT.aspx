<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Treatment_OT-PT.aspx.cs"
    Inherits="Bill_Sys_Treatment_OT_PT" %>

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

    

    <script type="text/javascript">
    
    function chktreatmentevent()
    {
           var headerchk = document.getElementsByName('chkTreatment');
          
            if(document.getElementById('chkTreatment').checked==true)
            {
               document.getElementById("txtTreatment").disabled = false;
               document.getElementById("txtTreatment").value="";
            }
            else if(document.getElementById('chkTreatment').checked==false)
            {
              document.getElementById("txtTreatment").disabled = true;
              document.getElementById("txtTreatment").value="";
              
            }
    }
    
      function GetReferral()
         {
            var rdlstDiagnosisTestandReferral = document.getElementsByName('rdlstDiagnosisTestandReferral');  
            var radio = document.getElementsByName('rdbreferral');  
            for (var j = 0; j < radio.length; j++)
            {
              if(j==0)
              {
                if(radio[j].checked)
                {
                    document.getElementById("txtEvaluation").disabled = false;
                    document.getElementById("txtcondition").disabled = true;
                    document.getElementById("chkTreatment").disabled = true;
                    document.getElementById("txtTreatment").disabled = true;
                    rdlstDiagnosisTestandReferral[0].disabled = true;
                    rdlstDiagnosisTestandReferral[1].disabled = true;
                    rdlstDiagnosisTestandReferral[2].disabled = true;
                    document.getElementById("txtfrequency").disabled =true; 
                    document.getElementById("txtPeriod").disabled =true;
                    document.getElementById("txtcondition").value="";
                    document.getElementById("chkTreatment").value="";
                    document.getElementById("txtTreatment").value="";
                    document.getElementById("txtfrequency").value="";
                    document.getElementById("txtPeriod").value="";
                    document.getElementById('chkTreatment').checked=false;
                    
                    
               }
            }
             if(j==1)
              {
                if(radio[j].checked)
                {
                    document.getElementById("txtEvaluation").disabled = true;
                    document.getElementById("txtcondition").disabled = false;
                    document.getElementById("chkTreatment").disabled = false;
                    document.getElementById("txtTreatment").disabled = false;
                    rdlstDiagnosisTestandReferral[0].disabled = false;
                    rdlstDiagnosisTestandReferral[1].disabled = false;
                    rdlstDiagnosisTestandReferral[2].disabled = false;
                    document.getElementById("txtfrequency").disabled = false;
                    document.getElementById("txtPeriod").disabled = false;
                    document.getElementById('txtEvaluation').value='';
                    document.getElementById('chkTreatment').checked=false;
               }
             }
             
              if(j==2)
              {
                    if(radio[j].checked)
                    {
                        document.getElementById("txtEvaluation").disabled = false; 
                        document.getElementById("txtcondition").disabled = false;
                        document.getElementById("chkTreatment").disabled = false;
                        document.getElementById("txtTreatment").disabled = false;
                        rdlstDiagnosisTestandReferral[0].disabled = false;
                        rdlstDiagnosisTestandReferral[1].disabled = false;
                        rdlstDiagnosisTestandReferral[2].disabled = false;
                        document.getElementById("txtfrequency").disabled = false;
                        document.getElementById("txtPeriod").disabled = false;
                        document.getElementById('txtEvaluation').value='';
                        document.getElementById('txtcondition').value='';
                        document.getElementById('chkTreatment').value='';
                        document.getElementById('txtTreatment').value='';
                        document.getElementById('txtfrequency').value='';
                        document.getElementById('txtPeriod').value='';
                        document.getElementById('chkTreatment').checked=false;
                   }
               }
           
            }
         }
            
            function GetrdlstDiagnosisTestandReferral()
         {
           var rdlstDiagnosisTestandReferral = document.getElementsByName('rdlstDiagnosisTestandReferral');  
            for (var k = 0; k < rdlstDiagnosisTestandReferral.length; k++)
            {
             if(k==0)
              {
                if(rdlstDiagnosisTestandReferral[k].checked)
                {
                    document.getElementById("txtfrequency").disabled = false; 
                    document.getElementById("txtPeriod").disabled = false;
                     document.getElementById('txtfrequency').value='';
                     document.getElementById('txtPeriod').value='';
                    
                }
              }
              if(k==1)
              {
                if(rdlstDiagnosisTestandReferral[k].checked)
                {
                    document.getElementById("txtfrequency").disabled = true; 
                    document.getElementById("txtPeriod").disabled = true;
                    document.getElementById('txtfrequency').value='';
                    document.getElementById('txtPeriod').value='';
                   
                }
              }
              if(k==2)
              {
                if(rdlstDiagnosisTestandReferral[k].checked)
                {
                    document.getElementById("txtfrequency").disabled = false; 
                    document.getElementById("txtPeriod").disabled = false;
                   
                }
              }

            }
         }
         function Getrdnpatientseen()
         {
         var rdnpatientseen = document.getElementsByName('rdnpatientseen');  
            for (var l = 0; l < rdnpatientseen.length; l++)
            {
            if(l==0)
              {
                if(rdnpatientseen[l].checked)
                {
                    document.getElementById("txtPatientSeen").disabled = false; 
                    
                }
              }
               if(l==1)
              {
                if(rdnpatientseen[l].checked)
                {
                    document.getElementById("txtPatientSeen").disabled = true; 
                    document.getElementById('txtPatientSeen').value='';
                    
                }
              }
              if(l==2)
              {
                if(rdnpatientseen[l].checked)
                {
                    document.getElementById("txtPatientSeen").disabled = false; 
                    document.getElementById('txtPatientSeen').value='';
                   
                }
              }

            }
          }
          
          function GetrbPatientWorking()
         {
         var rdbpatienworking =document.getElementsByName('rbPatientWorking');
             for (var m = 0; m < rdbpatienworking.length; m++)
            {
             if(m==0)
              {
                if(rdbpatienworking[m].checked)
                {
                    document.getElementById("txtlimitedwork").disabled = false; 
                    document.getElementById("txtregularwork").disabled = false; 
                    document.getElementById('txtlimitedwork').value='';
                    document.getElementById('txtregularwork').value='';
                   
                }
             }
              if(m==1)
              {
                if(rdbpatienworking[m].checked)
                {
                    document.getElementById("txtlimitedwork").disabled = true; 
                    document.getElementById("txtregularwork").disabled = true;  
                    document.getElementById('txtlimitedwork').value='';
                    document.getElementById('txtregularwork').value='';
                   
                }
              }
              if(m==2)
              {
                if(rdbpatienworking[m].checked)
                {
                    document.getElementById("txtlimitedwork").disabled = false; 
                    document.getElementById("txtregularwork").disabled = false; 
                     document.getElementById('txtlimitedwork').value='';
                    document.getElementById('txtregularwork').value=''; 
                    
                }
             }

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
    <form id="frmPlanOfCare" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div align="center">
            <table class="lcss_maintable" cellpadding="0px" cellspacing="0px">
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
                                <td width="83%" scope="col">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td colspan="3" scope="col">
                                                <div align="left" class="blocktitle">
                                                    Evaluation/Treatment
                                                    <div id="divPlanOfCare" class="div_blockcontent">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                                            <tr>
                                                                <td colspan="4" style="height: 30px">
                                                                    <div id="ErrorDiv" visible="true" style="color: Red;">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    1.
                                                                </td>
                                                                <td style="text-align: left">
                                                                    Referral was for:</td>
                                                                <td colspan="2" style="text-align: left">
                                                                    <asp:RadioButtonList ID="rdbreferral" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="0">Evaluation Only (Complete item a)</asp:ListItem>
                                                                        <asp:ListItem Value="1">Treatment Only (Complete item b-1,2,3)</asp:ListItem>
                                                                        <asp:ListItem Value="2" Selected="True">Evaluation and Treatment (Complete items a and b-1,2,3)</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    a.</td>
                                                                <td colspan="2" style="text-align: left">
                                                                    <%--<div class="lbl">--%>
                                                                    Your Evalution
                                                                    <%--</div>--%>
                                                                </td>
                                                                <td style="height: 28px">
                                                                    <asp:TextBox ID="txtEvaluation" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    b.(1)
                                                                </td>
                                                                <td colspan="3" style="text-align: left">
                                                                    Patient's condition and progress:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="text-align: left">
                                                                    <asp:TextBox ID="txtcondition" runat="server" TextMode="MultiLine" Width="91%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px" valign="top">
                                                                    b.(2)</td>
                                                                <td colspan="5" style="text-align: left;">
                                                                    Treatment and planned future treatment. If an authorization request is required
                                                                    (see items 4 & 5 on reverse),
                                                                    <asp:CheckBox ID="chkTreatment" runat="server" />
                                                                    and explain below. If additional space is necessary, please attach request</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:TextBox ID="txtTreatment" runat="server" TextMode="multiline" Width="91%"> </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                            <tr>
                                                                <td style="width: 28px" valign="top">
                                                                    b.(3)</td>
                                                                <td colspan="4" style="text-align: left">
                                                                    Was such treatment plan upon prescription or referral of claimant's attending physician
                                                                    or, in the case of physical therapy, authorized physician or podiatrist?</td>
                                                                <td>
                                                                    <%--    <asp:CheckBoxList ID="chkDiagnosisTestandReferral" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:CheckBoxList>--%>
                                                                    <asp:RadioButtonList ID="rdlstDiagnosisTestandReferral" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                        <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="4">
                                                                    <asp:Panel ID="pnlTestReferrals" runat="server">
                                                                        <table class="lbl" width="90%" border="0">
                                                                            <tr>
                                                                                <td class="tablecellLabel" style="height: 24px;" colspan="3">
                                                                                    <div class="lbl">
                                                                                        If yes, frequency of treatment ordered:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtfrequency" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td class="tablecellControl" style="height: 24px" colspan="3">
                                                                                    <div class="lbl">
                                                                                        Period of treatment ordered:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPeriod" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:TextBox ID="txtTransactionName" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtDescription" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    4.</td>
                                                                <td class="tablecellControl" style="height: 24px; width: 266px;">
                                                                    <div class="lbl">
                                                                        Dates of visits on which this report is based</div>
                                                                </td>
                                                                <td style="width: 190px">
                                                                    <asp:TextBox ID="txtVisitDate" runat="server"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnPatientDOB" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtVisitDate"
                                                                        PopupButtonID="imgbtnPatientDOB" PopupPosition="BottomRight" />
                                                                </td>
                                                                <td class="tablecellControl" style="height: 24px; width: 192px;">
                                                                    <div class="lbl">
                                                                        Date of First Visit</div>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtFirstVisitDate" runat="server"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFirstVisitDate"
                                                                        PopupButtonID="ImageButton1" PopupPosition="BottomRight" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td style="text-align: left">
                                                                    Will patient be seen again?</td>
                                                                <td colspan="3">
                                                                    <asp:RadioButtonList ID="rdnpatientseen" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                        <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Panel ID="Panel1" runat="server">
                                                                        <table class="lbl" width="100%" border="0">
                                                                            <tr>
                                                                                <td class="tablecellLabel" style="height: 24px;">
                                                                                    <div class="lbl">
                                                                                        If yes, when:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPatientSeen" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="tablecellControl" style="height: 24px">
                                                                                    <div class="lbl">
                                                                                        If no, was patient referred back to attending doctor:</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbpatientAttendingDoctor" runat="server" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                                    </asp:RadioButtonList></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:TextBox ID="TextBox3" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="TextBox4" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                    5.</td>
                                                                <td colspan="1" style="text-align: left">
                                                                    Is Patient working ?</td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rbPatientWorking" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                        <asp:ListItem Value="2" Selected="True">None</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 28px">
                                                                </td>
                                                                <td colspan="4">
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <table class="lbl" width="88%">
                                                                            <tr>
                                                                                <td class="tablecellLabel" style="height: 24px;" colspan="3">
                                                                                    <div class="lbl">
                                                                                        If yes, date(s) patient: resumed limited work of any kind</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtlimitedwork" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td class="tablecellControl" style="height: 24px" colspan="3">
                                                                                    <div class="lbl">
                                                                                        resumed regular work</div>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregularwork" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:TextBox ID="TextBox6" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="TextBox7" runat="server" Visible="false" Width="10px"></asp:TextBox>
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
                                                                        OnClick="btnSaveAndGoToNext_Click" CssClass="btn-gray" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="btn-gray" />
                                                                    <asp:TextBox ID="txtBillNumber" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtTreatmentID" runat="server" Width="10px" Visible="false">
                                                                    </asp:TextBox>
                                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
