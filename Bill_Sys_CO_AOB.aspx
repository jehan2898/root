<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_CO_AOB.aspx.cs" Inherits="Bill_Sys_CO_AOB" Title="Untitled Page" %>
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
                        <td align="center" style="width: 776px">
                            <asp:Label ID="lblFormHeader" runat="server" Text="ASSIGNMENT  OF   BENEFITS" Style="font-weight: bold;" Width="182px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 776px">
                        </td>
                    </tr>
                </table>
                <table style="width: 93%">
                    <tbody>
                        <tr>
                            <td align="left" valign="middle" style="width: 174px; height: 18px">
                                <asp:Label ID="lbl_Patient_Name" runat="server" Text="Patient Name" Width="94%"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 18px;">
                                <asp:Label ID="lblColon1" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 336px; height: 18px;">
                                <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="429px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox></td>
                        </tr>
                        <tr>
                        <td align="left" valign="middle" style="height: 22px; width: 174px;">
                            <asp:Label ID="lbl_Provider_Company_Name" runat="server" Text="Provider Company Name" Width="181px"></asp:Label></td>
                        <td align="center" valign="middle" style="width: 9px; height: 22px;">
                            <asp:Label ID="lblColon2" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                        <td align="left" valign="middle" style="height: 22px; width: 336px;">
                            <asp:TextBox ID="TXT_PROVIDER_COMPANY_NAME" runat="server" Width="471px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 174px">
                                <asp:Label ID="lbl_DateofAccident" runat="server" Text="Date of Accident" Width="187px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon3" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 336px">
                                <asp:TextBox ID="TXT_DOA" runat="server" Width="87px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 22px; width: 174px;">
                                <asp:Label ID="lbl_Address" runat="server" Text="Patient Address" Width="179px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 22px;">
                                <asp:Label ID="lbl_Colon4" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="height: 22px; width: 336px;">
                                <asp:TextBox ID="TXT_PATIENT_ADDRESS" runat="server" Width="459px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 18px; width: 174px;">
                                <asp:Label ID="lbl_ProviderName" runat="server" Text="Provider Name" Width="180px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 18px;">
                                <asp:Label ID="lbl_Colon6" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="height: 18px; width: 336px;">
                                <asp:TextBox ID="TXT_PROVIDER_NAME" runat="server" Width="302px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 22px; width: 174px;">
                                <asp:Label ID="lbl_ProviderAddress" runat="server" Text="Provider Address" Width="192px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 22px;">
                                <asp:Label ID="lbl_Colon7" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="height: 22px; width: 336px;">
                                <asp:TextBox ID="TXT_PROVIDER_ADDRESS" runat="server" Width="473px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 174px">
                                &nbsp;<asp:Label ID="lblAccidnet_After_Date" runat="server" Text="For Accidents on and After Date" Width="252px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px">
                                <asp:Label ID="lbl_Colon9" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="width: 336px">
                                <asp:TextBox ID="TXT_ACCIDENT_ON_AFTER" runat="server" Width="87px" CssClass="textboxCSS" onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                  <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" /></td>
                                                                        <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="TXT_ACCIDENT_ON_AFTER"
                                                                                                        PopupButtonID="imgbtnFromDate" />
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="height: 22px; width: 174px;">
                                <asp:Label ID="lbl_OCAOfficialFormNumber" runat="server" Text="OCA Official Form Number" Width="207px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 9px; height: 22px;">
                                <asp:Label ID="lbl_Colon10" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle" style="height: 22px; width: 336px;">
                                <asp:TextBox ID="TXT_OCA_OFFICIAL_FORM_NUMBER" runat="server" Width="87px" CssClass="textboxCSS"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TDPart" valign="top">
                <table style="width: 100%">
                    <tr>
                        <td align="center" style="width: 781px; height: 23px;">
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" CssClass="textboxCSS"></asp:TextBox>
                            <asp:TextBox ID="TXT_I_EVENT" runat="server" Visible="false" CssClass="textboxCSS">1</asp:TextBox>
                            <asp:Button ID="css_btnSave" runat="server" CssClass="Buttons" OnClick="css_btnSave_Click" Text="Save" />&nbsp;
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
     
