<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_CO_HIPPA.aspx.cs" Inherits="Bill_Sys_CO_HIPPA" Title="Untitled Page" %>

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
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" align="center" valign="top">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblFormHeader" runat="server" Text="HIPPA" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                <table width="90%">
                    <tbody>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Patient_Name" runat="server" Text="Patient Name" Width="89px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lblColon1" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="302px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lblDateofBirth" runat="server" Text="Date of Birth" Width="86px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                            <asp:Label ID="lblColon2" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_DOB" runat="server" Width="87px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                        <td align="left" valign="middle" style="width: 310px">
                            <asp:Label ID="lbl_SSN" runat="server" Text="Social Security Number" Width="151px"></asp:Label></td>
                        <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon3" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                        <td align="left" valign="middle" style="width: 360px">
                            <asp:TextBox ID="TXT_SSN" runat="server" Width="146px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Address" runat="server" Text="Patient Address" Width="106px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon4" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_PATIENT_ADDRESS" runat="server" Width="413px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 22px; width: 310px;">
                                <asp:Label ID="lbl_Authorization_Date" runat="server" Text="Authorization Date" Width="139px"></asp:Label></td>
                            <td align="center" valign="middle" style="height: 22px; width: 9px;">
                                <asp:Label ID="lbl_Colon5" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="height: 22px; width: 360px;">
                                <asp:TextBox ID="TXT_AUHORIZATION_DATE" runat="server" Width="87px" onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                 <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" /></td>
                                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="TXT_AUHORIZATION_DATE"
                                                                                                        PopupButtonID="imgbtnFromDate" />
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_AddressofHealthProvider" runat="server" Text="Name and Address of Health Provider or Entity" Width="317px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon6" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_PROVIDER_NAME_ADDESS" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                    BorderStyle="None" CssClass="textboxCSS" ForeColor="Black" ReadOnly="true" Width="413px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px; height: 68px">
                                <asp:Label ID="lbl_NameandAddressofPerson" runat="server" Text="Name and Address of Person or Category of Person to whom this information will be sent" Width="305px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 68px">
                                <asp:Label ID="lbl_Colon7" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px; height: 68px">
                                <asp:TextBox ID="TXT_NAME_ADDRESS_OF_PERSON" runat="server" Height="64px" TextMode="MultiLine"
                                    Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_MedicalRecord" runat="server" Text="Medical Record" Width="136px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon8" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:CheckBox ID="CHK_MEDICAL_RECORD_DATE_FROM_TO" runat="server" Text=" " /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Medical_recordDateFrom" runat="server" Text="Medical Record Date From" Width="169px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon9" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_MEDICAL_RECORD_FROM" runat="server" Width="87px" onkeypress="return clickButton1(event,'/')"></asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" /></td>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_MEDICAL_RECORD_FROM"
                                                                                                        PopupButtonID="ImageButton1" />
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 22px; width: 310px;">
                                <asp:Label ID="lbl_MedicalrecordToDate" runat="server" Text="Medical Record Date To" Width="151px"></asp:Label>
                            </td>
                            <td align="center" valign="middle" style="height: 22px; width: 9px;">
                                <asp:Label ID="lbl_Colon10" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="height: 22px; width: 360px;">
                                <asp:TextBox ID="TXT_MEDICAL_RECORD_TO" runat="server" Width="87px" onkeypress="return clickButton1(event,'/')"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" /></td>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_MEDICAL_RECORD_TO"
                                                                                                        PopupButtonID="ImageButton2" />
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Health_Care_Providers" runat="server" Text="Entire Medical Record, including patient histories, office notes (except psychotherapy notes), test results, radiology studies,films, referrals, consults, billing records, insurance records, and records sent to you by other health care providers." Width="315px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon12" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:CheckBox ID="CHK_HEALTH_CARE_PROVIDERS" runat="server" Text=" " /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Other" runat="server" Text="Other Specific Information to be Released"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon13" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:CheckBox ID="CHK_OTHER_PROVIDERS" runat="server" Text=" " /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Other_SpecificInformation" runat="server" Text="Other Specific Information" Width="173px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon14" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_OTHER_PROVIDERS" runat="server" Height="73px" TextMode="MultiLine"
                                    Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" valign="middle">
                                <asp:Label ID="lbl_Include" runat="server" Font-Bold="True" Text="Include:"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lblAlcohol_DrugTreatment" runat="server" Text="Alcohol/Drug Treatment" Width="169px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon15" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_ALOCOHOL_DRUG_TREATMENT" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_MentalHealthInformation" runat="server" Text="Mental Health Information" Width="167px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon16" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_MENTAL_HEALTH_INFORMATION" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_HIVRelatedInformation" runat="server" Text="HIV Related Information" Width="170px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon17" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_HIV_RELATED_INFORMATION" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" valign="middle">
                                <asp:Label ID="lbl_AuthorizationDiscussInformation" runat="server" Font-Bold="True"
                                    Text="Authorization to Discuss Health Information:"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_ByInitialinghere" runat="server" Text="By Initialing" Width="89px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon18" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:CheckBox ID="CHK_BY_INITILING_HERE" runat="server" Text=" " /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Initialing_here" runat="server" Text="By Initialing here" Width="109px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon19" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_BY_INITILING_HERE" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Authorize_Name" runat="server" Text="Authorize Name" Width="105px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon20" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_AUTHORIZE" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" valign="middle">
                                <asp:Label ID="lbl_ReasonforReleaseofInformation" runat="server" Font-Bold="True"
                                    Text="Reason for Release of Information"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Individual_Request" runat="server" Text="Individual Request" Width="130px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon21" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:CheckBox ID="CHK_AT_REQUEST_OF_INDIVIDUAL" runat="server" Text=" " /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Other_Release_Information" runat="server" Text="Other Release Information" Width="171px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon22" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:CheckBox ID="CHK_OTHER_RELEASE_INFORMATION" runat="server" Text=" " /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 91px; width: 310px;">
                                <asp:Label ID="lbl_Text_Other_ReleaseInformation" runat="server" Text="Other Release Information" Width="168px"></asp:Label></td>
                            <td align="center" valign="middle" style="height: 91px; width: 9px;">
                                <asp:Label ID="lbl_Colon23" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="height: 91px; width: 360px;">
                                <asp:TextBox ID="TXT_OTHER_RELEASE_INFORMATION" runat="server" Height="75px" TextMode="MultiLine"
                                    Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_AuthorizationExpire_date" runat="server" Text="Date or Event on which this authorization will expire" Width="318px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon24" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_AUTHORIZATION_EXPIRE_DATE_OR_EVENT" runat="server" Width="87px" onkeypress="return clickButton1(event,'/')"></asp:TextBox><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/cal.gif" /></td>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TXT_AUTHORIZATION_EXPIRE_DATE_OR_EVENT"
                                                                                                        PopupButtonID="ImageButton3" />
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Nameofpersonsigningform" runat="server" Text="Name of person signing form" Width="197px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon25" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_IF_NOT_PATIENT_NAME_OF_PERSON_SIGNING_FORM" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 310px">
                                <asp:Label ID="lbl_Authority_Sign" runat="server" Text="Authority to sign  on behalf of patient" Width="230px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon26" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 360px">
                                <asp:TextBox ID="TXT_AUTHORITY_TO_SIGN_ON_BEHALF_OF_PATIENT" runat="server" Width="302px"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TDPart" valign="top">
                <table style="width: 100%">
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="TXT_I_EVENT" runat="server" Visible="false">1</asp:TextBox>
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                            <asp:Button ID="css_btnSave" runat="server" CssClass="Buttons" OnClick="css_btnSave_Click"
                                Text="Save" />&nbsp;
                                <asp:Button ID="btnSavePrint" runat="server" CssClass="Buttons" Text="Save & Print" OnClick="btnSavePrint_Click"/>
                            <asp:TextBox ID="txtEventID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        </asp:Content>
   

