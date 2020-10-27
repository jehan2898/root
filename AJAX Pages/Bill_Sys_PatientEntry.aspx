<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_PatientEntry.aspx.cs" Inherits="Bill_Sys_PatientEntry" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"  AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <script type="text/javascript" src="validation.js"></script>  

    <script language="javascript" type="text/javascript">

  function OpenPopup()
  {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute'; 
            document.getElementById('div1').style.left= '350px'; 
            document.getElementById('div1').style.top= '200px'; 
            document.getElementById('div1').style.visibility='visible';            
            return false;  
  }
  
 function CloseSource()
       {
            document.getElementById('div1').style.visibility='hidden';
            document.getElementById('iframeAddDiagnosis').src='';   
       }
    function ViewDetails(obj1,obj2,obj3)
       {     
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute'; 
            document.getElementById('div1').style.left= '350px'; 
            document.getElementById('div1').style.top= '200px'; 
            document.getElementById('div1').style.visibility='visible'; 
            document.getElementById('iframeAddDiagnosis').src="../Bill_Sys_CO_ViewDeatails.aspx?CaseID="+obj1+"&ProcID="+obj2+"&CompID="+obj3+"";
            return false;            
       }
       
        function Clear()
        {            
             
             document.getElementById('ctl00_ContentPlaceHolder1_extddlCaseType').value="NA";                        
             document.getElementById('ctl00_ContentPlaceHolder1_extddlPatientState').value="NA";
             document.getElementById("<%=txtPatientFName.ClientID%>").value="";
             document.getElementById("<%=txtPatientLName.ClientID%>").value="";
             document.getElementById("<%=txtDateOfBirth.ClientID%>").value="";
             document.getElementById("<%=txtPatientAddress.ClientID%>").value="";
             document.getElementById("<%=txtPatientCity.ClientID%>").value="";
             document.getElementById("<%=txtDateofAccident.ClientID%>").value="";
             document.getElementById('ctl00_ContentPlaceHolder1_extddlLocation').value="NA";
        }
        
        function SaveExistPatient()
        {            
             
            document.getElementById("<%=btnOK.ClientID%>").click(); 
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


    function setReadOnly(obj)
    {
        var chk = document.getElementById('_ctl0_ContentPlaceHolder1_chkAutoIncr');
         
        if(chk.checked)
        {
            
            document.getElementById('_ctl0_ContentPlaceHolder1_txtRefChartNumber').readOnly = true;
   
          }
        else
        {
          
            document.getElementById('_ctl0_ContentPlaceHolder1_txtRefChartNumber').readOnly = false;
         
        }
    }
    
    </script>

    
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;height: 100%">
        <tr>
        <td>
                           <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
        </td>
        </tr>
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 99%;
                padding-top: 3px; height: 100%; vertical-align: top; background-color:White;" colspan="6">
                <table width="100%" id="MainBodyTable" border="0" cellspacing="0" cellpadding="0">                   
                    <tr>
                        <td class="TDHeading" style="text-align: left; height: 25px; font-weight: bold;"
                            colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Small">Patient Entry</asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <span style="color: Red;">* -&nbsp; Mandatory fields.</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: auto;">
                        </td>
                        <td width="100%" scope="col"   style="height: auto;">
                            <table width="100%">
                                <tr>
                                    <th colspan="6" align="center" valign="top" scope="col" style="height: auto;">
                                        <div align="left">
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                <!-- Start : Data Entry -->
                                                
                                                <tr>
                                                    <td   style="width: 100%;" class="lbl">                                                        
                                                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>       
                                                            <div id="ErrorDiv" style="color: red" visible="true">
                                                            </div>                                                                                                              
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="vertical-align: top; width:30%">
                                                                </td>
                                                                <td style="width: 70%">
                                                            <asp:Label ID="lblPchartno" Text="Prev. Chart No. : " runat="server" class="lbl"
                                                                Visible="False" ForeColor="white" BackColor="red"></asp:Label>
                                                             <asp:Label ID="lblPreChartNo" runat="server" Visible="False" BackColor="Red" class="lbl" ForeColor="White"
                                                                Font-Bold="true"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 30%; vertical-align:top;">
                                                                 
                                                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align:top;border: 1px solid #B5DF82;" 
                                                            onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
                                                            <tr>
                                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                    <b class="txt3">Search Parameters</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td   align="left" colspan="2">
                                                                    Patient Name</td>
                                                                    <td style="width: 50%; height: 24px;" align="left">  
                                                                     <asp:TextBox ID="txtPatientName" runat="server" Width="98%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                                                                        
                                                                   </td>   
                                                            </tr>
                                                           
                                                            <tr>                                                        
                                                                <td align="left" style="height: 24px;" colspan="2"  >Location</td>
                                                                <td style="width: 50%; height: 24px;" align="left">     
                                                                   <cc1:ExtendedDropDownList ID="extddlPatientLocation" runat="server" Width="100%" Selected_Text="---Select---"
                                                                    Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Connection_Key="Connection_String" ></cc1:ExtendedDropDownList>                                                           
                                                                 </td>                                                                     
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>                                                                            
                                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="20%"   OnClick="btnSearch_OnClick"/>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                      
                                                                </td>
                                                                <td style="width: 70%">
                                                                   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align:top;border: 1px solid #B5DF82;">
                                                             <tr>
                                                                <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="4">
                                                                    <b class="txt3">Save Parameters</b>
                                                                </td>
                                                            </tr>
                                                                        <tr>
                                                                            <td style="width: 15%; height: 35px;" class="lbl">
                                                            <asp:Label ID="lblChart" Text="Chart No." runat="server" class="lbl"></asp:Label>
                                                            <asp:Label ID="lblChartNo" Text="Chart No." runat="server" class="lbl"></asp:Label></td>
                                                                            <td style="width: 30%; height: 35px;" class="lbl">
                                                        <asp:TextBox ID="txtChartNo" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtRefChartNumber" runat="server" Style="float: left;" CssClass="cinput"
                                                            Width="147px" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                        </td>
                                                                            <td style="width: 15%; height: 35px;" class="lbl">
                                                        <asp:Label ID="lblLocation" Text="Location" runat="server" class="lbl" Visible="false"></asp:Label> 
                                                        </td>
                                                                            <td style="width: 30%; height: 35px;" class="lbl">
                                                                     <cc1:ExtendedDropDownList ID="extddlLocation" Width="140px"
                                                            runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_LOCATION"
                                                            Flag_Key_Value="LOCATION_LIST" Selected_Text="--- Select ---" CssClass="cinput"  
                                                            Visible="False" />
                                                        <asp:Label ID="lextddlLocation" runat="server"   Visible="False"></asp:Label> 
                                                        </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 15%;" class="lbl">
                                                                                 First Name 
                                                                            </td>
                                                                            <td style="width: 30%;" class="lbl">
                                                                    <asp:TextBox ID="txtPatientFName" runat="server" MaxLength="50" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                        <asp:Label ID="ltxtPatientFName" runat="server" ForeColor="#FF8000" Text="*" Visible="True"></asp:Label></td>
                                                                            <td style="width: 15%;" class="lbl">
                                                                             Last Name
                                                                            </td>
                                                                            <td style="width: 30%;" class="lbl">
                                                                    <asp:TextBox ID="txtPatientLName" runat="server" MaxLength="50" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPatientLName" runat="server" ForeColor="#FF8000" Text="*" Visible="True"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 15%;" class="lbl">
                                                       
                                                            Date of Birth</td>
                                                                            <td style="width: 30%;" class="lbl">
                                                                        <asp:TextBox Width="82px" ID="txtDateOfBirth" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                            MaxLength="10" CssClass="cinput"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                        <asp:Label ID="ltxtDateOfBirth" runat="server" ForeColor="#FF8000" Text="*" Visible="False" Width="4px"></asp:Label>
                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1"
                                                                            ControlToValidate="txtDateOfBirth" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator></td>
                                                                            <td style="width: 15%;" class="lbl">
                                                                              Address
                                                                            </td>
                                                                            <td style="width: 30%;" class="lbl">
                                                                    <asp:TextBox Width="148px" ID="txtPatientAddress" MaxLength="50" runat="server"
                                                                        CssClass="cinput" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox><asp:Label
                                                                            ID="ltxtPatientAddress" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 15%; height: 25px;" class="lbl">
                                                                            City
                                                                            </td>
                                                                            <td style="width: 30%;; height: 25px;" class="lbl">
                                                                    <asp:TextBox ID="txtPatientCity" runat="server" MaxLength="50" Width="150px" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox><asp:Label ID="ltxtPatientCity"
                                                                            runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                            </td>
                                                                            <td style="width:15%; height: 25px;" class="lbl">
                                                                    State</td>
                                                                            <td style="width:30%; height: 25px;" class="lbl">
                                                                    <cc1:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="80%" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---">
                                                                    </cc1:ExtendedDropDownList>
                                                                    <asp:Label ID="lextddlPatientState" runat="server" ForeColor="#FF8000" Text="*" Visible="False"
                                                                        Width="4px"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 15%; height: 33px;" class="lbl">
                                                            Date Of Accident</td>
                                                                            <td style="width: 30%; height: 33px;" class="lbl">
                                                        <asp:TextBox Width="81px" ID="txtDateofAccident" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10" CssClass="cinput"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <asp:Label
                                                                ID="ltxtDateofAccident" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                                <ajaxToolkit:MaskedEditValidator
                                                            ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                                            ControlToValidate="txtDateofAccident" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator></td>
                                                                            <td style="width: 15%; height: 33px;" class="lbl">
                                                                    Case Type</td>
                                                                            <td style="width: 30%; height: 33px;" class="lbl">
                                                                    <cc1:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="150px" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---" />
                                                                    <asp:Label ID="lextddlCaseType" runat="server" ForeColor="#FF8000" Text="*" Visible="True"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 15%;">
                                                                            </td>
                                                                            <td style="width:30%;">
                                                                            </td>
                                                                            <td style="width: 15%;">
                                                                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate> 
                                                             <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"/>
                                                                       </ContentTemplate> 
                                                                       </asp:UpdatePanel>                                                                        
                                                                            </td>
                                                                            <td style="width: 130%;">                                                                         
                                            <input type="button" runat="server" id="btnClear1" onclick="Clear();" style="width: 80px" value="Clear"   />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:CheckBox ID="chkAutoIncr" runat="server" Style="float: right;" Text="Auto Increment"
                                                            Visible="false" Width="126px" Font-Bold="False" onclick="setReadOnly();" /></td>
                                                </tr>
                                                <!-- End : Data Entry -->
                                            </table>
                                        </div>
                                                                    <cc1:ExtendedDropDownList ID="extddlProvider" Width="10px" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---"
                                                                        Visible="false" />
                                                                    <cc1:ExtendedDropDownList ID="extddlCaseStatus" Width="10px" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                        Flag_ID="txtCompanyID.Text.ToString();" Visible="false" />
                                                                    <asp:TextBox ID="txtAssociateDiagnosisCode" runat="server" Visible="False" Width="10px" />
                                                                    <asp:TextBox ID="txtJobTitle" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtWorkActivites" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox Visible="false" ID="txtPatientStreet" runat="server" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtPatientAge" runat="server" onkeypress="return clickButton1(event,'')"
                                                                            MaxLength="10" Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCarrierCaseNo" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox Width="10px" ID="txtDateOfInjury" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                            MaxLength="10" Visible="False"></asp:TextBox>
                                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                            Width="80px" CssClass="Buttons" Visible="false" />
                                        <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtProcedureGroupId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtAccidentID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtUserId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtState" runat="server" Width="60px" CssClass="cinput" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtDoctorId" runat="server" Visible="False" Width="10px"></asp:TextBox> 
                                        <asp:TextBox ID="txtLocationId" runat="server" Visible="False" Width="10px"></asp:TextBox>                                           
                                        <asp:CheckBox ID="chkAssociateCode" runat="server" Text="Associate Diagnosis Code" Visible="False" Width="200px" /></th>
                                </tr>
                                <tr>
                                    <th align="center" colspan="6" scope="col" style="height: auto" valign="top">
                                    </th>
                                </tr>
                                <tr>
                                    <th align="center" colspan="6" scope="col" style="height: auto" valign="top">
                                    </th>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: auto">
                        </td>
                    </tr>
                    <tr>
                        <td scope="col">
                            &nbsp;</td>
                        <td scope="col" id="anch_Patient">
                               <table style=" width: 100%; border: 1px solid #B5DF82;" class="txt2"
                                                    align="left" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                            <b class="txt3">Patient list</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1017px;">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <table style="vertical-align: middle; width: 100%;" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="vertical-align: middle; width: 30%" align="left">
                                                                                    <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true" Visible="false"
                                                                                        CssClass="search-input">
                                                                                    </gridsearch:XGridSearchTextBox>
                                                                                </td>
                                                                                <td style="vertical-align: middle; width: 30%" align="left">
                                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                                                        <ProgressTemplate>
                                                                                            <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                                runat="Server">
                                                                                                <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                                Loading...</div>
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                </td>
                                                                                <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                                                                    Record Count:<%= this.grdPatientList.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server" OnSelectedIndexChanged="con_OnSelectedIndexChanged" SelectedIndexproperty="true">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                        Text="Export TO Excel">
                                                                                   <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <xgrid:XGridViewControl ID="grdPatientList" runat="server" Height="0px" Width="1002px" OnRowCommand ="grdPatientList_OnRowCommand"
                                                                        CssClass="mGrid"   AutoGenerateColumns="false"
                                                                        MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelPatientList" ShowExcelTableBorder="true"
                                                                        EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                        ExportToExcelColumnNames="Case #,Patient Name,Insurance Company,Visit Date,Date Of Accident,Last Date Of Visit,Visit Type,Treatment Status,Signature Status" 
                                                                        ExportToExcelFields="SZ_CASE_NO,SZ_PATIENT_NAME,SZ_INSURANCE_COMPANY,DT_EVENT_DATE,DT_ACCIDENT_DATE,DT_LAST_DATE_OF_VISIT,SZ_TYPE,TREATMENT_STATUS,SIGNATURE_STATUS" 
                                                                        AlternatingRowStyle-BackColor="#EEEEEE"
                                                                        AllowPaging="true" GridLines="None" XGridKey="DoctorPatientList" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                                        AllowSorting="true" DataKeyNames="SZ_PROCEDURE_GROUP_ID,SZ_PROCEDURE_GROUP,I_EVENT_ID,SZ_TYPE,SZ_CASE_ID,SZ_PATIENT_NAME,SZ_PATIENT_ID">
                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                        
                                                                        <Columns>
                                                <%--0--%>
                                                <asp:BoundField DataField="SZ_CASE_ID" HeaderText="CaseID" Visible="false">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                <%--1--%>
                                                
                                                <asp:TemplateField HeaderText="Case #">
                                                    <itemtemplate>
                                                        <asp:Label id="lblOpenForm" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>' visible="false"  style="font-weight: bold;"></asp:Label>
                                                        <asp:LinkButton ID="lnkOpenForms" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Open Form" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'></asp:LinkButton>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                
                                                <%--2--%>
                                                <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Patient ID" Visible ="false">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                <%--3--%>
                                                <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ReadOnly="True">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                <%--4--%>
                                                <asp:BoundField DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company" ReadOnly="True">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                <%--5--%>
                                                <asp:BoundField DataField="DT_EVENT_DATE" HeaderText="Visit Date"  ReadOnly="True">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                <%--6--%>
                                                <asp:BoundField DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" ReadOnly="True">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                <%--7--%>
                                                <asp:BoundField DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit" ReadOnly="True" visible ="false">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                <%--8--%>                                                
                                                <asp:TemplateField HeaderText="Visit Type">
                                                    <itemtemplate>                                                    
                                                        <asp:Label id="lblVisitType" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_TYPE")%>'   style="font-weight: bold;"></asp:Label>
                                                        <asp:LinkButton ID="lnkAddVisit" runat="server" Text="Add Visit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  CommandName="Add Visit" visible="false"> </asp:LinkButton>                                                                                
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                <%--9--%>
                                                <asp:TemplateField HeaderText="Previous Treatment">
                                                    <itemtemplate>
                                                        <a href="#" id="hlnkShowDetails" onclick='<%# "ViewDetails(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_PROCEDURE_GROUP_ID") +"\",\""+ Eval("SZ_COMPANY_ID") +"\");" %>'>
                                                            View Details</a>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                                <%--10--%>
                                                <asp:BoundField DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure ID" ReadOnly="True" Visible="false">
                                                    <itemstyle horizontalalign="Left" />
                                                </asp:BoundField>
                                               <%--  <%--11--%>
                                                 <asp:BoundField DataField="TREATMENT_STATUS" HeaderText="Treatment Status" ReadOnly="True" Visible="true">
                                                    <itemstyle horizontalalign="Left" />
                                                </asp:BoundField> 
                                                 <%--12--%>
                                                 <asp:BoundField DataField="SIGNATURE_STATUS" HeaderText="Signature Status" ReadOnly="True"  Visible="true">
                                                    <itemstyle horizontalalign="Left" />
                                                </asp:BoundField>
                                                <%--13--%>
                                                <asp:BoundField DataField="SZ_COMPANY_ID" HeaderText="Company ID" ReadOnly="True" Visible="false">
                                                    <itemstyle horizontalalign="Left" />
                                                </asp:BoundField>
                                                 <%--14--%>
                                                <asp:BoundField DataField="I_EVENT_ID" HeaderText="Event ID" ReadOnly="True" Visible="false">
                                                    <itemstyle horizontalalign="Left" />
                                                </asp:BoundField>
                                                <%--15--%>
                                                <asp:BoundField DataField="SZ_PROCEDURE_GROUP" HeaderText="Procedure Group" ReadOnly="True" Visible="false">
                                                    <itemstyle horizontalalign="Left" />
                                                </asp:BoundField>
                                                
                                                <%--16--%>
                                                <asp:BoundField DataField="SZ_TYPE" HeaderText="Visit Type" ReadOnly="True" visible="false">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                
                                                 <%--17--%>
                                                <asp:BoundField DataField="SZ_PATIENT_ID" HeaderText="Patient Id" ReadOnly="True" visible="false">
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Center" />
                                                </asp:BoundField>
                                                
                                                 <asp:TemplateField HeaderText="Tempalate Manager">
                                                    <itemtemplate>                                                    
                                                        <a href="TemplateManager.aspx?fromCase=true&caseId=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_ID") %>&CaseNo=<%#DataBinder.Eval(Container,"DataItem.SZ_CASE_NO") %>&PName=<%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>&PId=<%#DataBinder.Eval(Container,"DataItem.SZ_PATIENT_ID") %>">
                                                            Tempalate Manager</a>
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                            </Columns>                                                              
                                            </xgrid:XGridViewControl>
                                            
                                            
                       <asp:GridView ID="grdPatientListExportToExcel" runat="server" visible="False">                                               
                        <Columns>                             
                            <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case #" ></asp:BoundField>
                            <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ></asp:BoundField>
                            <asp:BoundField DataField="SZ_INSURANCE_COMPANY" HeaderText="Insurance Company"></asp:BoundField>
                            <asp:BoundField DataField="DT_EVENT_DATE" HeaderText="Visit Date"></asp:BoundField>
                            <asp:BoundField DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident"></asp:BoundField>
                           <%-- <asp:BoundField DataField="DT_LAST_DATE_OF_VISIT" HeaderText="Last Date Of Visit" ></asp:BoundField>--%>
                            <asp:BoundField DataField="SZ_TYPE" HeaderText="Visit Type" ></asp:BoundField>
                            <asp:BoundField DataField="TREATMENT_STATUS" HeaderText="Traetment Status" ></asp:BoundField>
                            <asp:BoundField DataField="SIGNATURE_STATUS" HeaderText="Signature Status"></asp:BoundField>                            
                        </Columns>                      
                    </asp:GridView>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                          </td>
                                        </tr>
                                      </table>                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>     
    <asp:Button ID="btnOK" runat="server" Style="visibility: hidden;" CssClass="Buttons"
            Text="OK" OnClick="btnOK_Click" />
            
             <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <ContentTemplate>
    <div id="divid2" style="position: absolute; left: 428px; top: 920px; width: 300px;
        height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4;">
            <a onclick="CancelExistPatient();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="msgPatientExists" runat="server"></span>
        </div>
        <br />
        <div style="text-align: center;">
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />
        </div>
    </div>   
            </ContentTemplate> 
            </asp:UpdatePanel> 
    <table style="width: 1053px">
        <tr>
            <td class="lbl" style="width: 118px;">       
   <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoName" EnableCaching="true"
                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtPatientName"
                    ServiceMethod="GetPatient" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                </ajaxToolkit:AutoCompleteExtender>
    <asp:RadioButtonList ID="rdolstPatientType" runat="server" RepeatDirection="Horizontal"
        Visible="false" Width="411px">
        <asp:ListItem Value="0">Bicyclist Driver</asp:ListItem>
        <asp:ListItem Value="1">Driver</asp:ListItem>
        <asp:ListItem Value="2">Passenger</asp:ListItem>
        <asp:ListItem Value="3">Pedestrian</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="lrdolstPatientType" Width="1%" Visible="false" runat="server" ForeColor="#FF8000"
        Text="*"></asp:Label>
    <asp:TextBox ID="txtPatientType" runat="server" Visible="false" Width="1%"></asp:TextBox>
    <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red" Width="37px" ></asp:Label></td>
            <td style="width: 54px;">
                
    <asp:HiddenField ID="hidIsSaved" Value="0" runat="server" />
    <asp:Label ID="lblValidator3" runat="server" Font-Bold="False" ForeColor="Red" ></asp:Label></td>
            <td style="width: 100px;">
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtDateofAccident" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>
    <asp:Label ID="lblValidator9" runat="server" Font-Bold="False" ForeColor="Red" Width="1px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 118px; height: 13px;" align="left">
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtDateOfBirth" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>
            </td>
            <td style="width: 54px; height: 13px;" align="left">
    <ajaxToolkit:CalendarExtender ID="calDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
        PopupButtonID="imgbtnDateofBirth" />
    </td>
            <td style="width: 100px; height: 13px;">
    <ajaxToolkit:CalendarExtender ID="calDateOfAccident" runat="server" TargetControlID="txtDateofAccident"
        PopupButtonID="imgbtnDateofAccident" />
        
        
            </td>
            
        </tr>
    </table> 
     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                <ContentTemplate>
      <div id="div1" style="position: absolute; left: 100px; top: 100px; width: 500px;
        height: 280px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="CloseSource();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="iframeAddDiagnosis" src="" frameborder="0" height="380" width="500"></iframe>
    </div>  
    </ContentTemplate> 
    </asp:UpdatePanel> 
    
    
     <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lbn_job_det"
                PopupDragHandleControlID="pnlSaveDescriptionHeader" PopupControlID="pnlSaveDescription"
                DropShadow="true">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel Style="display:none; width: 450px;background-color: #dbe6fa;"
                ID="pnlSaveDescription" runat="server">
                <table style="width: 100%; height: 100%" id="Table1" cellspacing="0" cellpadding="0"
                    border="0">
                    <tbody>
                        <tr>
                            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; vertical-align: top;
                                width: 100%; padding-top: 3px; height: 100%">
                                <table style="width: 100%" id="Table2" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td class="LeftTop">
                                            </td>
                                            <td style="width: 446px" class="CenterTop">
                                                                 <table style="width: 100%" class="ContentTable" cellspacing="0" cellpadding="3" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="ContentLabel" colspan="3">
                                                                <div style="left: 0px; width: 100%; position: relative; top: 0px; background-color: #8babe4;
                                                                    text-align: left" id="pnlSaveDescriptionHeader">
                                                                    <asp:Label ID="lblHeader" runat="server" Width="173px" Font-Bold="True" Text="Add Visit"
                                                                        CssClass="lbl"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td style="width: 10%" align="right">
                                                                <asp:Button ID="btnClose" OnClick="btnClose_Click" runat="server" Height="21px" Width="50px"
                                                                    Text="Close" CssClass="Buttons"></asp:Button></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" class="LeftCenter">
                                            </td>
                                            <td style="width: 446px" class="Center" valign="top">
                                                <table style="width: 100%; height: 100%" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 100%" class="TDPart">
                                                                <table style="width: 100%">
                                                                    <tbody>                                                                                                                                                                                                                     
                                                                          <tr>
                                                                            <td>
                                                             <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                                 <contenttemplate>
                                                                       <UserMessage:MessageControl runat="server" ID="MessageControl1" />
                                                                 </contenttemplate>
                                                              </asp:UpdatePanel>
                                                                             </td>
                                                                           </tr>
                                                                        <tr>
                                                                            <td style="text-align: left" class="ContentLabel">
                                                                                <b>Add Visit</b>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <table style="width: 100%" class="ContentTable" cellspacing="0" cellpadding="3" border="0">
                                                                    <tbody>                                                                        
                                                                <tr>
                                                                            <td style="width: 25%" class="td-widget-bc-search-desc-ch"  align="left" style="font-weight:bold;">
                                                                                Visit Type</td>
                                                                            <td style="width: 12%" align="left">
                                                                             <asp:RadioButtonList ID="listVisitType" BorderColor="" runat="server">
                                                                            <asp:ListItem>IE</asp:ListItem>
                                                                            <asp:ListItem>C</asp:ListItem>
                                                                            <asp:ListItem>FU</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                               </td>
                                                                            <td style="width: 10%" class="td-widget-bc-search-desc-ch" align="left" style="font-weight:bold;">
                                                                                Visit Date</td>
                                                                            <td style="width: 53%" align="left">
                                                                            <asp:TextBox Width="70%" ID="txtVisitDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                            MaxLength="10" CssClass="cinput"></asp:TextBox>
                                                                        <asp:ImageButton ID="imgbtnVisitDate" runat="server" ImageUrl="~/Images/cal.gif" />                                                                         
                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender4"
                                                                            ControlToValidate="txtVisitDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                                                 </td>
                                                                        </tr>                                                                                                                                                     
                                                                <tr id="RowVisitStatus" runat="server">
                                                           <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 40%;text-align: left;">
                                                                Visit Status :
                                                           </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; text-align: left;">
                                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                                            <asp:ListItem Value="0">Scheduled</asp:ListItem>
                                                            <asp:ListItem Value="1">Re-Scheduled</asp:ListItem>
                                                            <asp:ListItem Value="2">Completed</asp:ListItem>
                                                            <asp:ListItem Value="3">No-show</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>              
                                                                <tr id="RowProcedureCode" runat="server">
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; text-align: left;" colspan="2">
                                                        Procedure Code :
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px;">
                                                       
                                                    </td>
                                                </tr>   
                                                                <tr id="RowProcedureCodeList" runat="server">                                                       
                                                    <td class="td-widget-bc-search-desc-ch" style="height: 18px; width: 60%;" colspan="4">
                                                        <asp:ListBox ID="lstProcedureCode" runat="server" Width="349px" SelectionMode="Multiple">
                                                        </asp:ListBox>
                                                    </td>
                                                </tr>                   
                                                                <tr>
                                                                            <td style="width: 25%;font-weight:bold;"   align="center"  colspan="4">
                                                                            <asp:Button id="btnAddVisit" runat="Server" Text="Add Visit"  cssClass="Buttons" onClick="btnAddVisit_onClick"/>
                                                                                 </td>                                                                            
                                                                        </tr>                                                                                                                                                                                                                                                        
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                </td>
                                            <td style="height: 100%" class="RightCenter">
                                            </td>
                                        </tr>
                                         
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                         <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtVisitDate" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>
    
     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtVisitDate"
        PopupButtonID="imgbtnVisitDate" />
                    </tbody>
                </table>
            </asp:Panel>
            <div style="display: none">
                <asp:LinkButton ID="lbn_job_det" runat="server" Text="View Job Details" Font-Names="Verdana">
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
