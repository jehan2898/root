<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Test_Facility_Intake.aspx.cs" Inherits="Bill_Sys_Test_Facility_Intake"
    Title="Untitled Page" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script type="text/javascript" src="validation.js"></script>

    <script language="javascript" type="text/javascript">

        function SaveExistPatient()
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_btnOK').click(); 
            document.getElementById('divid2').style.visibility='hidden';
        }
        function CancelExistPatient()
        {
            document.getElementById('divid2').style.visibility='hidden';  
        }

        function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '360px'; 
            document.getElementById('divid2').style.top= '250px'; 
            document.getElementById('divid2').style.visibility='visible';           
            return false;            
        }

        function showAdjusterPanel()
        {
            document.getElementById('divAdjuster').style.visibility='visible';
            document.getElementById('divAdjuster').style.position='absolute';
            document.getElementById('divAdjuster').style.left= '300px'; 
            document.getElementById('divAdjuster').style.top= '250px'; 
            document.getElementById('divAdjuster').style.zIndex=1; 
        }
        function showInsurancePanel()
        {
            document.getElementById('divInsurance').style.visibility='visible';
            document.getElementById('divInsurance').style.position='absolute';
            document.getElementById('divInsurance').style.left= '300px'; 
            document.getElementById('divInsurance').style.top= '150px'; 
            document.getElementById('divInsurance').style.zIndex=1; 
            document.getElementById('extddlAttorney').style.visibility='hidden'; 
        }

        function showAttorneyPanel()
        {
            document.getElementById('divAttorney').style.visibility='visible';
            document.getElementById('divAttorney').style.position='absolute';
            document.getElementById('divAttorney').style.left= '200px'; 
            document.getElementById('divAttorney').style.top= '150px'; 
            document.getElementById('divAttorney').style.zIndex=1; 
        }


         function showAdjusterPanelAddress()
        {
            document.getElementById('divAddress').style.visibility='visible';
            document.getElementById('divAddress').style.position='absolute';
            document.getElementById('divAddress').style.left= '300px'; 
            document.getElementById('divAddress').style.top= '300px'; 
            document.getElementById('divAddress').style.zIndex=1; 
            
            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value='';
			    
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceCity").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceZip").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceStreet").value='';
			    
            
        }


        function lfnShowHide(p_szSource)
        {
            if(p_szSource == 'button'){
                document.getElementById('divNavigate').style.visibility='hidden';
                return;
            }
            
            var hid = document.getElementById('_ctl0_ContentPlaceHolder1_hidIsSaved').value;
            if(hid == '0'){
                document.getElementById('divNavigate').style.visibility='hidden';
            }else{
                document.getElementById('divNavigate').style.visibility='visible';
            }
        }
        
        function ascii_value(c)
        {
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
    
        function clickButton1(e,charis)
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
  

function DisableKeyValidation(control,e)
{
        if(navigator.appName.indexOf("Netscape")>(-1))
        {  
            if(control.charCode == 39 || control.charCode==39)
            {
                return false;
            }
               
        }
        if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
        {  
            if(event.keyCode == 39)
                {
                    return false;
                } 
        }
      
} 



function confirmstatus()
         {
            //       _ctl0_ContentPlaceHolder1_extddlInsuranceCompany 
      
                if(document.getElementById("_ctl0_ContentPlaceHolder1_extddlInsuranceCompany").value == 'NA') 
                {
                    alert('Select Insurance Company');
                    
                }
                else
                {
                document.getElementById('divAddress').style.visibility='visible';
                document.getElementById('divAddress').style.position='absolute';
                document.getElementById('divAddress').style.left= '300px'; 
                document.getElementById('divAddress').style.top= '300px'; 
                document.getElementById('divAddress').style.zIndex=1; 
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").style.backgroundColor = "";
		    	document.getElementById('divAddressError').innerHTML='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode")
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value='';
			    
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceCityCode").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceZipCode").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceStreetCode").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_extddlStateCode").value='NA';; 
			      document.getElementById("_ctl0_ContentPlaceHolder1_IDDefault").checked=false; 
               }
                
         }
function checkAddressDetails()
    {
       if(document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value=='')
       {
            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").focus();
			document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").style.backgroundColor = "#ffff99";
			document.getElementById('divAddressError').innerHTML='Enter the mandatory field';
			return false;
       }
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





function TABLE5_onclick() {

}

    </script>
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" style="height: 490px">
                <table width="100%">
                    <tr>
                        <td align="center" style="height: 18px">
                            <asp:Label ID="lblHeading" runat="server"
                                Style="font-weight: bold;" Font-Size="X-Large" Width="100%" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 64px">
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle" width="12%"  >
                                            <asp:Label ID="lblCmpAddress" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td align="left"  valign="middle" width="40%">
                                            <asp:Label ID="lblAdd" runat="server" Text="Label" Width="100%"></asp:Label></td>
                                      
                                        <td align="left" valign="middle" width="6%">
                                            <asp:Label ID="lblTel" runat="server" Font-Bold="False" Text="Tel "></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" width="20%">
                                            <asp:TextBox ID="txtTelNo" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            &nbsp;</td>
                                        <td width="13%">
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblFax" runat="server" Font-Bold="False" Text=" Fax "></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="txtFax" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle" width ="15%" style="height: 32px" >
                                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Width="100%" Text="Patient's Name"></asp:Label>
                                        </td >
                                        <td align="left" valign="middle" width ="51%" style="height: 32px">
                                        
                                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="100%" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="left" valign="middle" width="14%" style="height: 32px">
                                            <asp:Label ID="lbl_DOA" runat="server" Font-Bold="False"  Width ="70%" Text="Date Of Accident"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" style="height: 32px">
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent"
                                                BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <%--   <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Text="Patient's Name"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="472px" Enabled="false"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_DOA" runat="server" Font-Bold="False" Text="Date Of Accident" ></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOE" runat="server" Font-Bold="False" Text="Date Of Examination" Enabled="false"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOE" runat="server" Width="85px" Enabled="false"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOB" runat="server" Font-Bold="False" Text="Date Of Birth"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOB" runat="server" Width="85px" Enabled="false"></asp:TextBox></td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="4" align="left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                      
                                        
                                                <td align="center" colspan="4" width="100%" style="height: 26px">
                                     <span style="font-size: 12pt; font-family: 'Times New Roman'">
                                       <strong>Acknowledgement of Receipt of Notice</strong></span></td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle" width="15%" >
                                            <asp:Label ID="lblDoctorName" runat="server" Text="Doctor  Name"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" width="25%" style="height: 22px">
                                            <asp:TextBox ID="txtDoctorName" runat="server" Width="100%"></asp:TextBox></td>
                                        <td align="left" valign="middle" width="25%" style="height: 22px">
                                            &nbsp;</td>
                                        <td align="left" valign="middle" width="25%" style="height: 22px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                    <td colspan="4" width="100%">
                                      &nbsp;
                                    </td>
                                    </tr>
                                    <tr>
                                      
                                           <td align="center" colspan="4" width="100%">
                                     <span style="font-size: 12pt; font-family: 'Times New Roman'">
                                       <strong>PLEASE CIRCLE ONE OF THE FOLLOWING</strong></span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" width="100%" style="height: 42px">
                                            <asp:RadioButtonList ID="rdoPregnant" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">I am pregnant </asp:ListItem>
                                                <asp:ListItem Value="1">I am not pregnant</asp:ListItem>
                                                <asp:ListItem Value="2">I do not know if i an pregnant</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                    <td colspan="4">
                                     &nbsp;
                                    </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle" width="25%">
                                            <asp:Label ID="lblExplainProblem" runat="server" Text="Briefly explain your problem"></asp:Label>
                                        </td>
                                        <td align="left" rowspan="2" valign="middle" width="25%">
                                            <asp:TextBox ID="txtExplainProblem" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                        <td style="width: 3px">
                                        </td>
                                        <td align="left" valign="middle" width="25%">
                                            <asp:Label ID="lblProblemYear" runat="server" Font-Bold="False" Text="How long have you had this problem ?"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" width="25%">
                                            <asp:TextBox ID="txtProblemYear" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                <tr>
                                <td align="left"  width="50%">
                                            <asp:Label ID="lblResultOf" runat="server" Font-Bold="False" Text="Is this the result of a car accident or job injury ?"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" width="50%">
                                            <asp:TextBox ID="txtResultOf" runat="server"></asp:TextBox>
                                        </td>
                                </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <tr>
                                            <td align="left" valign="middle" width="25%">
                                                <asp:Label ID="lblProblemDate" runat="server" Font-Bold="False" Text="If so, please give date:"></asp:Label>
                                            </td>
                                            <td align="left" valign="middle" width="25%">
                                               
                                                    
                                                    <asp:TextBox ID="txtProblemDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                        CssClass="text-box" MaxLength="12"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtProblemDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                            </td>
                                              <td align="left" valign="middle" width="25%">
                                            <asp:Label ID="lblmedicalillness" runat="server" Text="Which  medical illnesses do you have ?"></asp:Label>
                                        </td>
                                        <td align="left" rowspan="2" valign="middle" width="25%">
                                            <asp:TextBox ID="txtmedicalillness" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                        
                                             </tr>
                                    
                                   
                                </table>
                                 <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle" width="25%">
                                            <asp:Label ID="lblDiagnosed" runat="server" Text="Have you ever been diagnosed with a tumor or cancer ?" Width="100%"></asp:Label>
                                        </td>
                                        <td align="left" rowspan="2" valign="middle" width="25%">
                                            <asp:TextBox ID="txtDiagnosed" runat="server"></asp:TextBox></td>
                                        <td style="width: 3px">
                                        </td>
                                        <td align="left" valign="middle" width="25%">
                                            <asp:Label ID="lblListPreviousSurgeries" runat="server" Font-Bold="False" Text="List previous surgeries and operations" Width="235px"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" width="25%">
                                            <asp:TextBox ID="txtListPreviousSurgeries" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                <tr>
                                <td colspan="6">
                                &nbsp;
                                </td>
                                </tr>
                                <tr>
                                <td align="center" colspan="6" style="height: 18px">
                                     <span style="font-size: 12pt; font-family: 'Times New Roman'">
                                       <strong>TECHNOLOGISTS USE ONLY</strong></span></td>
                                </tr>
                                  <tr>
                                <td colspan="6" style="height: 18px">
                                &nbsp;
                                </td>
                                </tr>
                                <tr>
                                <td width="20%">
                                 <asp:Label ID="lblIVContrast" runat="server" Text="IV Contrast ?" Width="100%"></asp:Label> 
                                </td>
                                <td>
                                
                                            <asp:RadioButtonList ID="rdoIvContrast" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="0">YES</asp:ListItem>
                                                <asp:ListItem Value="1">NO</asp:ListItem>
                                             
                                            </asp:RadioButtonList>&nbsp;</td> 
                                <td>
                                 <asp:Label ID="lblHowMuch" runat="server" Text="How Much?" Width="100%"></asp:Label>  
                                </td>
                                <td>
                                 <asp:TextBox ID="txtIvContrast1" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                 CC
                                </td>
                                <td>
                                 <asp:TextBox ID="txtIvContrast2" runat="server"></asp:TextBox>
                                </td>
                                </tr>
                                
                                  <tr>
                                <td width="20%">
                                  <asp:Label ID="Label1" runat="server" Text="BY Whom" Width="100%"></asp:Label>         
                                </td>
                                <td>
                                &nbsp;
                                </td> 
                                <td>
                                 <asp:Label ID="lblDoctor_Name" runat="server" Text="Doctor Name" Width="100%"></asp:Label>    
                                </td>
                                <td>
                                 <asp:TextBox ID="txtDR" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                 <asp:Label ID="lbldate" runat="server" Text="Date" Width="100%"></asp:Label>    
                                </td>
                                <td>
                                 <asp:TextBox ID="txtDate" runat="server" onkeypress="return clickButton1(event,'/')"  CssClass="text-box" MaxLength="12" ></asp:TextBox>
                                 
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                                                        PopupButtonID="ImageButton2" />
                                </td>
                                </tr>
                                
                                  <tr>
                                <td width="20%">
                                  <asp:Label ID="Label2" runat="server" Text="" Width="100%"></asp:Label>         
                                </td>
                                <td>
                                &nbsp;
                                </td> 
                                <td>
                                 <asp:Label ID="Label3" runat="server" Text="MRI OF" Width="100%"></asp:Label>    
                                </td>
                                <td>
                                 <asp:TextBox ID="txtMriOf" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                 <asp:Label ID="Label4" runat="server" Text="TECH" Width="100%"></asp:Label>    
                                </td>
                                <td>
                                 <asp:TextBox ID="txtTech" runat="server"></asp:TextBox>
                                </td>
                                </tr>
                                </table>
                                <table width="100%" >
                                <tr>
                                <td colspan="4" width="100%">
                                    &nbsp;
                                </td>
                                </tr>
                                <tr>
                                
                                               <td align="center" colspan="4" width="100%" style="height: 19px">
                                     <span style="font-size: 12pt; font-family: 'Times New Roman'">
                                       <strong> GARDIAN</strong></span></td>
                               
                                
                                </tr>
                                <tr>
                                 <td width="10%">
                                 <asp:Label ID="lablGardianName" runat="server" Text="Gardian Name" Width="94px" ></asp:Label>    
                                </td>
                                <td width="40%">
                                 <asp:TextBox ID="txtGardianName"  Width="80%"   runat="server"></asp:TextBox>
                                </td>
                                <td width="10%">
                                 <asp:Label ID="lblRelation" runat="server" Text="Relation" ></asp:Label>    
                                </td>
                                <td width="40%">
                                 <asp:TextBox ID="txtRelation" Width="80%" runat="server"></asp:TextBox>
                                </td>
                                </tr>
                                
                                  <tr>
                                <td colspan="4" width="100%">
                                    &nbsp;
                                    <asp:TextBox ID="txtPregnant" runat="server" Width="10%" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtIvContrast" runat="server" Width="10%" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtId" runat="server" Width="5%" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtCmpName" runat="server" Visible="False" Width="5%"></asp:TextBox>
                                    <asp:TextBox ID="txtAdd" runat="server" Visible="False" Width="5%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <%--<asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />--%>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Buttons" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnSavePrint" runat="server" Text="Save & Print" CssClass="Buttons" OnClick="btnSavePrint_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td class="TDPart" style="height: 3px">
            </td>
        </tr>
    </table>
</asp:Content>
                               
