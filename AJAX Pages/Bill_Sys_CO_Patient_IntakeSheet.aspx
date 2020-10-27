<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CO_Patient_IntakeSheet.aspx.cs" Inherits="Bill_Sys_CO_Patient_IntakeSheet"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        function ShowAOBDPROVIDERSignaturePopup() {

            document.getElementById('divDocSignatureAob').style.zIndex = 1;
            document.getElementById('divDocSignatureAob').style.position = 'fixed';
            document.getElementById('divDocSignatureAob').style.left = '350px';
            document.getElementById('divDocSignatureAob').style.top = '100px';
            document.getElementById('divDocSignatureAob').style.border = '10px';
            document.getElementById('divDocSignatureAob').style.visibility = 'visible';
            document.getElementById('frmDocSignatureAob').src = 'Bill_Sys_AOB_Provider_Sign.aspx';
            return false;
        }

        function ShowPVTDPROVIDERSignaturePopup() {

            document.getElementById('divDocSignaturePVT').style.zIndex = 1;
            document.getElementById('divDocSignaturePVT').style.position = 'fixed';
            document.getElementById('divDocSignaturePVT').style.left = '350px';
            document.getElementById('divDocSignaturePVT').style.top = '100px';
            document.getElementById('divDocSignaturePVT').style.border = '10px';
            document.getElementById('divDocSignaturePVT').style.visibility = 'visible';
            document.getElementById('frmDocSignaturePVT').src = 'Bill_Sys_Intake_PVT_Guardian_Sign.aspx';
            return false;
        }
        function ClosePVTDPROVIDERSignaturePopup() {
            document.getElementById('divDocSignaturePVT').style.visibility = 'hidden';
            document.getElementById('divDocSignaturePVT').style.zIndex = -1;
        }



        function ShowIntakeDPROVIDERSignaturePopup() {

            document.getElementById('divDocSignatureIntake').style.zIndex = 1;
            document.getElementById('divDocSignatureIntake').style.position = 'fixed';
            document.getElementById('divDocSignatureIntake').style.left = '350px';
            document.getElementById('divDocSignatureIntake').style.top = '100px';
            document.getElementById('divDocSignatureIntake').style.border = '10px';
            document.getElementById('divDocSignatureIntake').style.visibility = 'visible';
            document.getElementById('frmDocSignatureIntake').src = 'Bill_Sys_Intake_Provider.aspx';
            return false;
        }

        function ShowLienDPROVIDERSignaturePopup() {
            document.getElementById('divDocSignatureLien').style.zIndex = 1;
            document.getElementById('divDocSignatureLien').style.position = 'fixed';
            document.getElementById('divDocSignatureLien').style.left = '350px';
            document.getElementById('divDocSignatureLien').style.top = '100px';
            document.getElementById('divDocSignatureLien').style.border = '10px';
            document.getElementById('divDocSignatureLien').style.visibility = 'visible';
            document.getElementById('frmDocSignatureLien').src = 'Bill_Sys_Lien_Attorney_Sign.aspx';
            return false;
        }

        function CloseAOBDPROVIDERSignaturePopup() {
            document.getElementById('divDocSignatureAob').style.visibility = 'hidden';
            document.getElementById('divDocSignatureAob').style.zIndex = -1;
        }

        function CloseIntakeDPROVIDERSignaturePopup() {
            document.getElementById('divDocSignatureIntake').style.visibility = 'hidden';
            document.getElementById('divDocSignatureIntake').style.zIndex = -1;
        }

        function CloseLienDPROVIDERSignaturePopup() {
            document.getElementById('divDocSignatureLien').style.visibility = 'hidden';
            document.getElementById('divDocSignatureLien').style.zIndex = -1;
        }

        function ShowPatientAOBSignaturePopup() {

            document.getElementById('divPatientSignatureAOB').style.zIndex = 1;
            document.getElementById('divPatientSignatureAOB').style.position = 'fixed';
            document.getElementById('divPatientSignatureAOB').style.left = '350px';
            document.getElementById('divPatientSignatureAOB').style.top = '100px';
            document.getElementById('divPatientSignatureAOB').style.border = '10px';
            document.getElementById('divPatientSignatureAOB').style.visibility = 'visible';
            document.getElementById('frmPatientSignatureAOB').src = 'Bill_Sys_AOB_Patient_Sign.aspx';
            return false;
        }

        function ShowPatientIntakeSignaturePopup() {

            document.getElementById('divPatientSignatureIntake').style.zIndex = 1;
            document.getElementById('divPatientSignatureIntake').style.position = 'fixed';
            document.getElementById('divPatientSignatureIntake').style.left = '350px';
            document.getElementById('divPatientSignatureIntake').style.top = '100px';
            document.getElementById('divPatientSignatureIntake').style.border = '10px';
            document.getElementById('divPatientSignatureIntake').style.visibility = 'visible';
            document.getElementById('frmPatientSignatureIntake').src = 'Bill_Sys_Inatke_Patient_Sign.aspx';
            return false;
        }

        function ShowPatientHippaSignaturePopup() {

            document.getElementById('divPatientSignatureHippa').style.zIndex = 1;
            document.getElementById('divPatientSignatureHippa').style.position = 'fixed';
            document.getElementById('divPatientSignatureHippa').style.left = '350px';
            document.getElementById('divPatientSignatureHippa').style.top = '100px';
            document.getElementById('divPatientSignatureHippa').style.border = '10px';
            document.getElementById('divPatientSignatureHippa').style.visibility = 'visible';
            document.getElementById('frmPatientSignatureHippa').src = 'Bill_Sys_Hippa_Patient_Sign.aspx';
            return false;
        }

        function ShowPatientLienSignaturePopup() {

            document.getElementById('divPatientSignatureLien').style.zIndex = 1;
            document.getElementById('divPatientSignatureLien').style.position = 'fixed';
            document.getElementById('divPatientSignatureLien').style.left = '350px';
            document.getElementById('divPatientSignatureLien').style.top = '100px';
            document.getElementById('divPatientSignatureLien').style.border = '10px';
            document.getElementById('divPatientSignatureLien').style.visibility = 'visible';
            document.getElementById('frmPatientSignatureLien').src = 'Bill_Sys_Lien_Patient_Sign.aspx';
            return false;
        }

        function ShowPatientPVTSignaturePopup() {

            document.getElementById('divPatientSignaturePVT').style.zIndex = 1;
            document.getElementById('divPatientSignaturePVT').style.position = 'fixed';
            document.getElementById('divPatientSignaturePVT').style.left = '350px';
            document.getElementById('divPatientSignaturePVT').style.top = '100px';
            document.getElementById('divPatientSignaturePVT').style.border = '10px';
            document.getElementById('divPatientSignaturePVT').style.visibility = 'visible';
            document.getElementById('frmPatientSignaturePVT').src = 'Bill_Sys_Intake_PVT_Patient_Sign.aspx';
            return false;
        }

        function ShowWitnessPVTSignaturePopup() {

            document.getElementById('divPatientSignaturePVTWT').style.zIndex = 1;
            document.getElementById('divPatientSignaturePVTWT').style.position = 'fixed';
            document.getElementById('divPatientSignaturePVTWT').style.left = '350px';
            document.getElementById('divPatientSignaturePVTWT').style.top = '100px';
            document.getElementById('divPatientSignaturePVTWT').style.border = '10px';
            document.getElementById('divPatientSignaturePVTWT').style.visibility = 'visible';
            document.getElementById('frmPatientSignaturePVTWT').src = 'Bill_Sys_Intake_PVT_Witness_Sign.aspx';
            return false;
        }
         function ShowPatientGuarantorSignaturePopup() {
            document.getElementById('divDocSignaturePatientGuarantor').style.zIndex = 1;
            document.getElementById('divDocSignaturePatientGuarantor').style.position = 'fixed';
            document.getElementById('divDocSignaturePatientGuarantor').style.left = '350px';
            document.getElementById('divDocSignaturePatientGuarantor').style.top = '100px';
            document.getElementById('divDocSignaturePatientGuarantor').style.border = '10px';
            document.getElementById('divDocSignaturePatientGuarantor').style.visibility = 'visible';
            document.getElementById('frmPatientGuarantor').src = 'Bill_Sys_Intake_Patient_Guarantor_Billing_Sign.aspx';
            return false;
        }
       //sunil
        function ShowPatientDeclapaignaturePopup() {
            document.getElementById('divPatientSignatureDCHQ').style.zIndex = 1;
            document.getElementById('divPatientSignatureDCHQ').style.position = 'fixed';
            document.getElementById('divPatientSignatureDCHQ').style.left = '350px';
            document.getElementById('divPatientSignatureDCHQ').style.top = '100px';
            document.getElementById('divPatientSignatureDCHQ').style.border = '10px';
            document.getElementById('divPatientSignatureDCHQ').style.visibility = 'visible';
            document.getElementById('frmPatientSignatureDCHQ').src = 'Bill_Sys_Declaration_hippa_queeen_Sign.aspx';

            return false;
        }
         function ShowPatientDeclaLegalGuardianPopup() {
            document.getElementById('divLegalGuardianDCHQ').style.zIndex = 1;
            document.getElementById('divLegalGuardianDCHQ').style.position = 'fixed';
            document.getElementById('divLegalGuardianDCHQ').style.left = '350px';
            document.getElementById('divLegalGuardianDCHQ').style.top = '100px';
            document.getElementById('divLegalGuardianDCHQ').style.border = '10px';
            document.getElementById('divLegalGuardianDCHQ').style.visibility = 'visible';
            document.getElementById('frmLegalGuardianDCHQ').src = 'Bill_Sys_Decl_Hippa_Legal_Guardian_Sign.aspx';
            return false;
        }
       //end
        function ClosePatientGuarantorSignaturePopup() {
            document.getElementById('divDocSignaturePatientGuarantor').style.visibility = 'hidden';
            document.getElementById('divDocSignaturePatientGuarantor').style.zIndex = -1;
        }
       function CloseDCHQLegalGuardianPopup() {
            document.getElementById('divLegalGuardianDCHQ').style.visibility = 'hidden';
            document.getElementById('divLegalGuardianDCHQ').style.zIndex = -1;
        }
       function CloseDCHQignaturePopup() {
            document.getElementById('divPatientSignatureDCHQ').style.visibility = 'hidden';
            document.getElementById('divPatientSignatureDCHQ').style.zIndex = -1;
        }
        function CloseWitnessPVTSignaturePopup() {
            document.getElementById('divPatientSignaturePVTWT').style.visibility = 'hidden';
            document.getElementById('divPatientSignaturePVTWT').style.zIndex = -1;
        }

        function ClosePatientPVTSignaturePopup() {
            document.getElementById('divPatientSignaturePVT').style.visibility = 'hidden';
            document.getElementById('divPatientSignaturePVT').style.zIndex = -1;
        }

        function ClosePatientAOBSignaturePopup() {
            document.getElementById('divPatientSignatureAOB').style.visibility = 'hidden';
            document.getElementById('divPatientSignatureAOB').style.zIndex = -1;
        }

        function ClosePatientIntakeSignaturePopup() {
            document.getElementById('divPatientSignatureIntake').style.visibility = 'hidden';
            document.getElementById('divPatientSignatureIntake').style.zIndex = -1;
        }

        function ClosePatientHippaSignaturePopup() {
            document.getElementById('divPatientSignatureHippa').style.visibility = 'hidden';
            document.getElementById('divPatientSignatureHippa').style.zIndex = -1;
        }

        function ClosePatientLienSignaturePopup() {
            document.getElementById('divPatientSignatureLien').style.visibility = 'hidden';
            document.getElementById('divPatientSignatureLien').style.zIndex = -1;
        }
        //Nirmalkumar
        function showSecondaryInsurance() 
        {

            var flag = "fromIntake";
            var url = "SecondaryInsuraceViewFrame.aspx?link=" + flag;
           // var url = "SecondaryInsuraceViewFrame.aspx";

            SecInsuracePop.SetContentUrl(url);
            SecInsuracePop.Show();
            return false;

        }
        function ClosePopup() 
        {
            SecInsuracePop.Hide();
            var sURL = unescape(window.location.pathname);
           // sURL = sURL;
            window.location.href = sURL;
           // var button = document.getElementById('<%=btnCls.ClientID%>');
            //button.click();
        }
        //END
    </script>

    <table style="width: 99%; border: 0px;">
        <tr>
            <td colspan="2">
                <UserMessage:MessageControl runat="server" ID="usrMessage" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%;">
                <!-- The patient box goes here -->
                <table border="0" align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                    background-color: White; height: 28;">
                    <tr>
                        <td width="530" height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2">
                            &nbsp;&nbsp;<b class="txt3">Patient Information</b>
                        </td>
                    </tr>
                    <tr>
                        <!-- this td is a part of the parent table -->
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        Name
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        <asp:Label ID="txtPatientFirstName" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ReadOnly="true"></asp:Label>
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        Address
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        <asp:Label ID="txtAddress" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:Label>
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        D.O.B / Age
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        <asp:Label ID="txtDOB" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true" Style="float: left"></asp:Label>
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 20px;">
                                        <asp:Label ID="TXT_PATIENT_AGE" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true" Style="float: left"></asp:Label>
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        Accident
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        <asp:Label ID="txtDateOfInjury" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:Label>
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        Sex
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        <asp:Label ID="txtGendar" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:Label>
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        Carrier
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        <asp:Label runat="server" ID="TXT_INSURANCE_COMPANY_NAME1" Style="float: left;"></asp:Label>
                                        <asp:Label ID="TXT_INSURANCE_COMPANY_NAME" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        SSN
                                    </td>
                                    <td style="font-family: Arial; font-size: 11px; font-weight: bold; width: 60px;">
                                        <asp:Label ID="txtSocialSecurity" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 30%;">
                &nbsp;
            </td>
        </tr>
    </table>
    <div style="width: 99%; padding-bottom: 10px; padding-top: 10px; text-align: -moz-right;">
        <span style="font-family: Sans-Serif; font-size: 10px;">Other documents</span>
        <asp:LinkButton ID="lnkIntake" Text="INTAKE" runat="server" OnClick="lnkIntake_Click"
            class="toplinktxt"></asp:LinkButton>
        |
        <asp:LinkButton ID="lnkAOB" Text="AOB" runat="server" OnClick="lnkAOB_Click" class="toplinktxt"></asp:LinkButton>
        |
        <asp:LinkButton ID="lnkHippa" Text="HIPPA" runat="server" OnClick="lnkHippa_Click"
            class="toplinktxt"></asp:LinkButton>
        |
        <asp:LinkButton ID="lnkLIEN" Text="LIEN" runat="server" OnClick="lnkLIEN_Click" class="toplinktxt"></asp:LinkButton>
        |
        <asp:LinkButton ID="LinkButton1" Text="PATIENT INTAKE PRIVATE" runat="server" OnClick="lnkPatientIntakeprivate_Click"
            class="toplinktxt"></asp:LinkButton>
        |
        <asp:LinkButton ID="lnkPaymentGuarantee" Text="PAYMENT GUARANTEE" runat="server"
            OnClick="lnkPaymentGuarantee_Click" class="toplinktxt"></asp:LinkButton>
        |
        <asp:LinkButton ID="LinkButton2" Text="DECLARATION AND HIPPA QUEENS" runat="server"
            OnClick="lnkDeclaration_hippa_Click" class="toplinktxt"></asp:LinkButton>
        |
        <asp:LinkButton ID="lnkPrintAll" Text="PRINT ALL" runat="server" OnClick="lnklnkPrintAll_Click"
            class="toplinktxt"></asp:LinkButton>
    </div>
    <asp:Wizard ID="Wizard1" runat="server" Width="100%" ActiveStepIndex="0" DisplaySideBar="False">
        <WizardSteps>
            <asp:WizardStep runat="server" ID="IntakeStep1" StepType="Finish" Title="1:">
                <%--new table inserted--%>
                <div>
                    <table width="100%" style="border: 1px solid; background-color: White" border="1"
                        class="border">
                        <tr>
                            <td style="font-size: 15px" width="99%" height="28" align="center"  valign="middle"
                                bgcolor="#b5df82">
                                <b><u>INTAKE FORM</u></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="4" cellspacing="4" width="100%" class="border" border="0">
                                    <tr>
                                        <td>
                                            Today's Date:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtTodayDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Date of Accident:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDOA" runat="server" onkeypress="return clickButton1(event,'/')"
                                                ReadOnly="true"></asp:TextBox>
                                            <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton5"></asp:ImageButton>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender7"
                                                ControlToValidate="txtDOA" EmptyValueMessage="Date is required" ErrorMessage="MaskedEditValidator3"
                                                InvalidValueMessage="Date is invalid" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator>
                                            <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton5" Enabled="True"
                                                PopupPosition="TopRight" TargetControlID="txtDOA" ID="CalendarExtender6">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
                                                MaskType="Date" TargetControlID="txtDOA" PromptCharacter="_" AutoComplete="true">
                                            </ajaxToolkit:MaskedEditExtender>

                                           <%-- <ajaxToolkit:MaskedEditExtender runat="server" Mask="99/99/9999" 
                                                MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                                                CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                                                CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDOA" 
                                                ID="MaskedEditExtender7"></ajaxToolkit:MaskedEditExtender>
--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Patient's Name:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtPatientName" runat="server" Width="80%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Social Security #:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtSSN" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mailing Address
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtMailingAddress" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            City:
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtCity" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            State:
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtState" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Zip:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtZip" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date of Birth:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDtBirth" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td style="width: 90px">
                                            Age:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAge" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Sex:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdbSex" runat="server" RepeatDirection="Horizontal" ReadOnly="true">
                                                <asp:ListItem Text="Male" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            Marital Status:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdbMStatus" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="S" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="M" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="D" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="W" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Home Phone:
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtintakeHPH" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Work/Cell Phone:
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtintakeCPH" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            For female patients only: ARE YOU PREGNANT?
                                        </td>
                                        <td colspan="6">
                                            <asp:RadioButtonList ID="rdbPregnant" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="May be" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Please check one:</b>
                                        </td>
                                        <td colspan="7">
                                            <asp:RadioButtonList ID="rblCaseType" runat="server" RepeatDirection="Horizontal"
                                                ReadOnly="true">
                                                <asp:ListItem Text="WORKER COMPENSATION" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="NOFAULT" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="OTHER" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;">
                                            Insurance Company Name:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtInsCompanyName" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
                                            Insurd's Name:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtInsurdsName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Insurance Company Address:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtInsAddress" runat="server" Width="80%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Adjuster's Phone:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtAdjusterPhn" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:RadioButtonList ID="rdlOccupation" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="DRIVER" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="PASSENGER" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="PEDESTRIAN" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="OTHER" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><u>WORKERS COMP ONLY:</u></b>
                                        </td>
                                        <td>
                                            Employer's Name:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtEmpName" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Employer's Phone:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpPhn" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Claim #(carrier Case#):
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtClaimniNo" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Policy #(Carrier Id #):
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtPolicyNo" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            WCB #:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWCBNo" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Attorney Name:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtAttorneyName" runat="server" Width="80%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            Attorney Phone:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtAttorneyPhn" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Injuries Case Established for:
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtInjury" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>How did you hear about us?</b>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkReferral" runat="server" Text="Referral" />
                                        </td>
                                        <td style="width: 90px">
                                            by Whom:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReff" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkAdvertisement" runat="server" Text="Advertisment" />
                                        </td>
                                        <td>
                                            Which one:
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtAdv" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="8" style="height: 15px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" align="center">
                                            <b>NO-FAULT AUTHORIZATION</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" Text="PATIENT: Your health provider may agree to accept payment for health services performed directly from your insurer (Authorization
to Pay Benefits) so that you are not required to make payment to the health provider at the time of service. Such agreement is
optional on the part of the health provider and must be signed by both patient and health provider. You may use the optional
authorization language provided below, by checking off the designated spot on this form." ID="Label30" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" Text="(INITIAL HERE IF YOU HAVE CHOSEN TO AUTHORIZE THE DIRECT PAYMENT OF BENEFITS)"
                                                            ID="Label31" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="height: 15px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>AUTHORIZATION TO PAY BENEFITS:</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" Text="I AUTHORIZE PAYMENT OF HEALTH BENEFITS TO THE UNDERSIGNED HEALTHCARE PROVIDER OR SUPPLIER OF
SERVICES DESCRIBED BELOW. I RETAIN ALL RIGHTS, PRIVELIGES AND REMEDIES TO WHICH I AM ENTITLED
UNDER ARTICLE 51 (THE NO-FAULT PROVISION) OF THE INSURACE LAW." ID="Label32" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="height: 15px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <b>WORKERS COMPENSATION ASSIGNMENT OF BENEFITS</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" Text="I understand that if my Workers Compensation insurance denies payment for services that have been rendered to me I will be
financially liable for those services. In the event that my Workers Compensation insurance denies paying my claim, I authorize
CitiMedical I, PLLC to bill my major medical carrier for those services. I agree that if my major medical carrier refuses to pay for
those services, I will continue to be financially liable for the unpaid service." ID="Label33" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="8" style="height: 15px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" align="center">
                                            <b>MAJOR MEDICAL INSURANCE</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Insurance Company Name:
                                        </td>
                                        <%--Nirmalkumar--%>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtInsCompName" runat="server" ReadOnly="true" Width="73%" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlw1InsuranceType" runat="server" OnSelectedIndexChanged="ddlw1InsuranceType_SelectedIndexChanged" AutoPostBack="true"> 
                                            <asp:ListItem >Select</asp:ListItem>
                                            <asp:ListItem >Primary</asp:ListItem>
                                            <asp:ListItem >Secondary</asp:ListItem>
                                            <asp:ListItem >Major Medical</asp:ListItem>
                                            <asp:ListItem >Private</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2">
                                            <a id="A1" href="javascript:void(0);"  onclick="showSecondaryInsurance()" >Add Secondary Insurance</a>
                                        </td>
                                        <%--END--%>
                                    </tr>
                                    <tr>
                                        <td>
                                            Insurance Company Address
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtInsCmpAddress" runat="server" ReadOnly="true" Width="73%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Insurd's Name:
                                        </td>
                                        <td colspan="7">
                                            <asp:RadioButtonList ID="rblInsurdName" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="SELF" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="SPOUSE" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="PARENT" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Insurd's Date of Birth:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInsurdsDOB" runat="server"></asp:TextBox>
                                            <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton7" Visible="true">
                                            </asp:ImageButton>
                                            <%--<ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator9" runat="server" ControlExtender="MaskedEditExtender8"
                                                ControlToValidate="txtInsurdsDOB" EmptyValueMessage="Date is required" ErrorMessage="MaskedEditValidator3"
                                                InvalidValueMessage="Date is invalid" TooltipMessage="Input a Date"></ajaxToolkit:MaskedEditValidator>--%>
                                            <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton7" Enabled="True"
                                                PopupPosition="TopRight" TargetControlID="txtInsurdsDOB" ID="CalendarExtender8">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999"
                                                MaskType="Date" TargetControlID="txtDtBirth" PromptCharacter="_" AutoComplete="true">
                                            </ajaxToolkit:MaskedEditExtender>

                                           <%-- <ajaxToolkit:MaskedEditExtender runat="server" Mask="99/99/9999" 
                                                MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                                                CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                                                CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDtBirth" 
                                                ID="MaskedEditExtender8"></ajaxToolkit:MaskedEditExtender>--%>

                                        </td>
                                        <td style="width: 90px">
                                            Member Id #:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMemberId" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            Group #:
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtGroupNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" Text="All of the above information, to the best of my knowledge has been filled out correctly."
                                                            ID="Label34" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" align="center" >
                                            <asp:Button ID="btnIntakePatientSign" runat="server" Text="Patient Signature" BackColor="White"
                                                BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                Font-Size="1.0em" ForeColor="#284E98" OnClientClick="return ShowPatientIntakeSignaturePopup()" />
                                            <asp:Button ID="btnIntakeProviderSign" runat="server" Text="Provider Signature" BackColor="White"
                                                BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                Font-Size="1.0em" ForeColor="#284E98" OnClientClick="return ShowIntakeDPROVIDERSignaturePopup()" />
                                            <asp:Button ID="btnUpdateIntake" runat="server" Text="Update" OnClick="btnUpdateIntake_Click"
                                                BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                            <asp:Button ID="btnIntakePrintPDF" runat="server" Text="Print PDF" OnClick="btnIntakePrintPDF_Click"
                                                BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--end of table---%>
            </asp:WizardStep>
        </WizardSteps>
        <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
    </asp:Wizard>
    <asp:Wizard ID="Wizard2" runat="server" Width="100%" ActiveStepIndex="0" FinishCompleteButtonText="Save"
        DisplaySideBar="False" Font-Size="Small" Height="60px">
        <WizardSteps>
            <asp:WizardStep runat="server" ID="WizardStep2" StepType="Finish" Title="1:">
                <table border="1" align="left" cellpadding="0" cellspacing="0" class="border" width="100%">
                    <tr>
                           <td style="font-size: 15px" width="99%" height="28" align="center"  valign="middle"
                                bgcolor="#b5df82">
                                <b><u>AOB</u></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <table class="ContentTable" width="100%" style="background-color: White;" border="1">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="center">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 630" align="center">
                                                            <asp:Label runat="server" Text="NEW YORK MOTOR VEHICLE NO-FAULT INSURANCE LAW" Width="330px"
                                                                ID="Label2" Style="font-family: Arial; font-size: 11px; font-weight: bold;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 530" align="center">
                                                            <asp:Label runat="server" Text="ASSIGNMENT  OF   BENEFITS FORM" Width="182px" ID="lblFormHeader4"
                                                                Style="font-family: Arial; font-size: 11px; font-weight: bold;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 630" align="center">
                                                            <asp:Label runat="server" Text="(FOR ACCIDENTS OCCURRING ON AND AFTER 3/1/02)" Width="330px"
                                                                ID="Label1" Style="font-family: Arial; font-size: 11px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 530" align="center">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table style="width: 100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left" style="width: 2%">
                                                            <asp:Label runat="server" Text="I," ID="lbl_Provider_Company_Name" Style="font-family: Arial;
                                                                font-size: 12px;"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 24%">
                                                            <asp:TextBox runat="server" ReadOnly="True" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" CssClass="textboxCSS" Font-Bold="true" ForeColor="Black" Width="100%"
                                                                ID="TXT_AOB_PATIENT_NAME"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 19%">
                                                            <asp:Label runat="server" Text="('Assignor') hereby assign to" ID="Label3" Style="font-family: Arial;
                                                                font-size: 12px;"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 30%">
                                                            <cc1:ExtendedDropDownList ID="extddlOffice" Width="90%" runat="server" Connection_Key="Connection_String"
                                                                Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---" />
                                                        </td>
                                                        <td align="left" style="width: 20%">
                                                            <asp:Label runat="server" Text="('Assignee')" ID="Label4" Style="font-family: Arial;
                                                                font-size: 12px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="5">
                                                            <asp:Label runat="server" Text="all rights privileges and remedies to payment for health care services provided by assignee to which I am entitled under Article 51 (the No-Fault statute) of the Insurance Law."
                                                                ID="Label5" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="5" style="height: 10px;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="5">
                                                            <asp:Label runat="server" Text="The Assignee hereby certifies that they have not received any payment from or on behalf of the Assignor and
shall not pursue payment directly from the Assignor for services provided by said Assignee for injuries sustained due to the motor vehicle accident which occurred on"
                                                                ID="Label9" Style="font-family: Arial; font-size: 12px;"></asp:Label>
                                                            <asp:TextBox runat="server" CssClass="textboxCSS" Width="87px" ID="TXT_ACCIDENT_ON_AFTER"
                                                                onkeypress="return clickButton1(event,'/')" Enabled="false"></asp:TextBox>
                                                            <asp:Label runat="server" Text=", not withstanding any other agreement to the contrary."
                                                                ID="Label10" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="5" style="height: 10px;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="5">
                                                            <asp:Label runat="server" Text="This agreement may be revoked by the assignee when benefits are not payable based upon the assignor’s lack of
coverage and/or violation of a policy condition due to the actions or conduct of the assignor." ID="Label11" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="5" style="height: 15px;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="5">
                                                            <asp:Label runat="server" Text="ANY PERSON WHO KNOWINGLY AND WITH INTENT TO DEFRAUD ANY INSURANCE COMPANY OR OTHER PERSON
FILES AN APPLICATION FOR COMMERCIAL INSURANCE OR A STATEMENT OF CLAIM FOR ANY COMMERCIAL OR
PERSONAL INSURANCE BENEFITS CONTAINING ANY MATERIALLY FALSE INFORMATION, OR CONCEALS FOR THE
PURPOSE OF MISLEADING, INFORMATION CONCERNING ANY FACT MATERIAL THERETO, AND ANY PERSON WHO,
IN CONNECTION WITH SUCH APPLICATION OR CLAIM, KNOWINGLY MAKES OR KNOWINGLY ASSISTS, ABETS,
SOLICITS OR CONSPIRES WITH ANOTHER TO MAKE A FALSE REPORT OF THE THEFT, DESTRUCTION, DAMAGE OR
CONVERSION OF ANY MOTOR VEHICLE TO A LAW ENFORCEMENT AGENCY, THE DEPARTMENT OF MOTOR
VEHICLES OR AN INSURANCE COMPANY, COMMITS A FRAUDULENT INSURANCE ACT, WHICH IS A CRIME, AND
SHALL ALSO BE SUBJECT TO A CIVIL PENALTY NOT TO EXCEED FIVE THOUSAND DOLLARS AND THE VALUE OF
THE SUBJECT MOTOR VEHICLE OR STATED CLAIM FOR EACH VIOLATION." ID="Label12" Style="font-family: Arial; font-size: 11px; font-weight: bold;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td style="width: 336px; height: 22px" valign="middle" align="left">
                                                        <asp:TextBox runat="server" CssClass="textboxCSS" Width="87px" ID="TXT_OCA_OFFICIAL_FORM_NUMBER"
                                                            Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 336px; height: 22px" valign="middle" align="left">
                                                        <asp:TextBox runat="server" CssClass="textboxCSS" Width="90%" ID="TXT_HERE_BY_ASSIGN_TO"
                                                            Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:TextBox runat="server" CssClass="textboxCSS" ID="txtCompanyID5" Visible="False"></asp:TextBox>
                                            <asp:TextBox runat="server" CssClass="textboxCSS" ID="TXT_I_EVENT5" Visible="False">1</asp:TextBox>
                                            <asp:TextBox runat="server" Width="10px" ID="txtCaseID5" Visible="False"></asp:TextBox>
                                            <asp:TextBox runat="server" Width="10px" ID="txtEventID5" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center">
                                            <table style="width: 100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 530; height: 23px" align="center">
                                                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                            <asp:Button ID="btnAOBProviderSign" runat="server" Text="Provider Signature" OnClientClick=" return ShowAOBDPROVIDERSignaturePopup()"
                                                                Width="130px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                                            <asp:Button ID="btn_AOB_Patient_Sign" runat="server" OnClientClick=" return ShowPatientAOBSignaturePopup()"
                                                                Text="Patient Signature" Width="130px" BackColor="White" BorderColor="#507CD1"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em"
                                                                ForeColor="#284E98" />
                                                            <asp:Button ID="btnAOBSave_Print" runat="server" OnClick="btnSavePrint_Click" Text="Upadte"
                                                                BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                                            <asp:Button ID="btnAOBPrintPDF" runat="server" Text="Print PDF" OnClick="btnAOBPrintPDF_Click"
                                                                BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" TargetControlID="TXT_ACCIDENT_ON_AFTER" PromptCharacter="_" AutoComplete="true">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
        </WizardSteps>
        <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
    </asp:Wizard>
    <asp:Wizard ID="Wizard3" runat="server" ActiveStepIndex="0" DisplaySideBar="False">
        <WizardSteps>
            <asp:WizardStep runat="server" ID="WizardStep3" StepType="Finish" Title="1:">
                <%--new table inserted--%>
                <table style="width: 100%; border: 1px solid; background-color: White" border="1">
                    <tr>
                        <td style="font-size: 15px" width="99%" height="28" align="center"  valign="middle"
                                bgcolor="#b5df82">
                                <b><u>Hippa</u></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label runat="server" Text="OCA Official Form No." ID="Label26" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label runat="server" Text="AUTHORIZATION FOR RELEASE OF HEALTH INFORMATION PURSUANT TO HIPPA"
                                            ID="Label27" Style="font-family: Arial; font-size: 13px; font-weight: bold;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label runat="server" Text="[This form has been approved by the New York State Deparment of Health]"
                                            ID="Label28" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0">
                                <tr>
                                    <td style="width: 10%;">
                                        <asp:Label runat="server" Text="Patient Name:" ID="Label62" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td style="width: 40%;">
                                        <asp:Label ID="hippapatient" runat="server" ReadOnly="true" Style="font-family: Arial;
                                            font-size: 11px; font-weight: bold;"></asp:Label>
                                    </td>
                                    <td style="width: 10%;">
                                        <asp:Label runat="server" Text="Date of Birth:" ID="Label102" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="hippadob" runat="server" ReadOnly="true" Style="font-family: Arial;
                                            font-size: 11px; font-weight: bold;"></asp:Label>
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:Label runat="server" Text="SSN:" ID="Label103" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="txthippassn" runat="server" ReadOnly="true" Style="font-family: Arial;
                                            font-size: 11px; font-weight: bold;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <asp:Label runat="server" Text="Patient Address:" ID="Label104" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td colspan="5">
                                        <asp:Label ID="lblhippaddress" runat="server" ReadOnly="true" Style="font-family: Arial;
                                            font-size: 11px; font-weight: bold;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td align="left" colspan="5" style="height: 15px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I or my aothorized representative request that health information regarding my care and treatment be released as set forth on this form:"
                                            ID="Label16" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 4px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;In accordance with New York State Law and the Privacy Rule of the Health Insurance
Portability and Accountability Act of 1996(HIPPA), I unserstand that:" ID="Label17" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;1. This authorization may include disclosure of information relating to ALCOHOL and DRUG ABUSE, MENTAL
HEALTH TREATMENT, except psychotherapy notes," ID="Label18" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;and CONFIDENTIAL HIV* RELATED INFORMATION only
if I place my initials on the appropriate line in Item 9(a)." ID="Label63" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;In the event the health information, and I initial the line on
the box in Item 9(a), I specifically authorize release of such information to the person(s) indicatied in Item 8." ID="Label64"
                                            Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;2. If I am authorizing the release of HIV-related, alcohol or drug treatment, or mental health treatment information,
the recipient is prohibited" ID="Label19" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;from redisclosing such information without my authorization unless permitted to do so
under federal or state law." ID="Label65" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;If I experience discrimination because of the release or disclosure
of HIV-related information, I may contact New York Division of Human Rights at (212)480-2493 or the New York
" ID="Label67" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;City Commission of Human Rights at (212)306-7450. These agencies are responsible for protecting my rights. I understand that I have the right to request a list of people who "
                                            ID="Label66" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;may receive or use my
HIV-related information without authorization." ID="Label68" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;3. I have the right to revoke this authorization at any time by writing to the health care provider listed below. I
understand that I may revoke this authorization except to" ID="Label20" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;the extent that action has already been taken based on
this authorization." ID="Label69" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;4. I understand that signing this authorization is volumtary. My treatment, payment, enrollment in health plan, or
eligibility for benefits will not be conditioned upon my " ID="Label21" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;authorization of this disclosure." ID="Label70"
                                            Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;5. Information disclosed under this authorization might be redisclosed by the recipient (except as noted above in
Item 2), and this redisclosure may no longer " ID="Label22" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;be protected by federal or state law."
                                            ID="Label71" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;6. THIS AUTHORIZATION DOES NOT AUTHORIZE YOU TO DISCUSS MY HEALTH INFORMATION OR MEDICAL CARE WITH
ANYONE OTHER THAN THE ATTORNEY " ID="Label23" Style="font-family: Arial; font-size: 13px; font-weight: bold;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 2px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;OR GOVERMENTAL AGENCY SPECIFIED IN ITEM 9(b)."
                                            ID="Label72" Style="font-family: Arial; font-size: 13px; font-weight: bold;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;7. Name And Adress of health provider or entity to release this information:"
                                            ID="Label73" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtHipaaHealthProvider" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;8. Name and address of person(s) or category of peeson to whom this information
                                        will be send:" ID="Label74" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtHipaaWhomeInfoSend" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;9(a).Specific information to be released :"
                                            ID="Label75" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="chkHipaaMRF" runat="server" Text="" />
                                        <asp:Label runat="server" Text="&nbsp;Medical Recored from (insert Date)" ID="Label76"
                                            Style="font-family: Arial; font-size: 13px"></asp:Label>
                                        <asp:TextBox ID="txtMedialRecordDtFrom" runat="server" onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                        <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="imgmedicalfrom"></asp:ImageButton>
                                        <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="imgmedicalfrom" Enabled="True"
                                            PopupPosition="TopRight" TargetControlID="txtMedialRecordDtFrom" ID="CalendarExtender10">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:Label runat="server" Text="&nbsp;to(Insert date)" ID="Label77" Style="font-family: Arial;
                                            font-size: 13px"></asp:Label>
                                        <asp:TextBox ID="txtMedialRecordDtTo" runat="server" onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                        <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="imgmedicalto"></asp:ImageButton>
                                        <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="imgmedicalto" Enabled="True"
                                            PopupPosition="TopRight" TargetControlID="txtMedialRecordDtTo" ID="CalendarExtender11">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="chkHippaMRF2" runat="server" Text="" />
                                        <asp:Label runat="server" Text="&nbsp;Entire Medical Recored, Including patient histories, office notes (except psychotheropy
                                        notes), test results, radiology studies, films, referrals, consults, billing records,
                                        Insurance Records and &nbsp;&nbsp;records sent to you by other Helth care providers."
                                            ID="Label78" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:CheckBox ID="chkHippaMRF3" runat="server" Text="" />
                                                    <asp:Label runat="server" Text="&nbsp;Other:" ID="Label79" Style="font-family: Arial;
                                                        font-size: 13px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOther1" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text="&nbsp;Include:(Indicate by Initialing)" ID="Label80"
                                                        Style="font-family: Arial; font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOther2" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIncludeAlco" runat="server"></asp:TextBox>
                                                    <asp:Label runat="server" Text="Alcohol/Drug Treatment" ID="Label81" Style="font-family: Arial;
                                                        font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIncludeMen" runat="server"></asp:TextBox>
                                                    <asp:Label runat="server" Text="Mental Helth Information" ID="Label82" Style="font-family: Arial;
                                                        font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIncludeHIV" runat="server"></asp:TextBox>
                                                    <asp:Label runat="server" Text="HIV-Related Information" ID="Label83" Style="font-family: Arial;
                                                        font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:Label runat="server" Text="Authorization to discuss Health Information" ID="Label84"
                                            Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:Label runat="server" Text="(b)" ID="Label85" Style="font-family: Arial; font-size: 13px"></asp:Label>&nbsp;&nbsp;<asp:CheckBox
                                            ID="chkini" runat="server" Text="" />&nbsp;
                                        <asp:Label runat="server" Text="By initialing here" ID="Label86" Style="font-family: Arial;
                                            font-size: 13px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtInitials" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <asp:Label runat="server" Text="I authorize" ID="Label87" Style="font-family: Arial;
                                            font-size: 13px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtHelthCareProvider" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;<asp:Label runat="server" Text="to discuss my health information with my attorney, or a govermental
                                        agency, listed here:" ID="Label88" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtAttoenry" runat="server" Width="80%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        (Attorney/Firm Name or Governmental Agency Name)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;<asp:Label runat="server" Text="10. Reason for release of information:"
                                                        ID="Label89" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text="11. Date or event on which this authorization will expire"
                                                        ID="Label90" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk10" runat="server" />
                                                    <asp:Label runat="server" Text="At request of individual" ID="Label91" Style="font-family: Arial;
                                                        font-size: 13px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExpiry" runat="server" onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton4"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton4" Enabled="True"
                                                        PopupPosition="TopRight" TargetControlID="txtExpiry" ID="CalendarExtender5">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkOther" runat="server" />
                                                    <asp:Label runat="server" Text="Other" ID="Label92" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:Label runat="server" Text="12. If not the patient, name of person signing form:"
                                                        ID="Label93" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text="13. Authority to sign on behalf of patient:" ID="Label94"
                                                        Style="font-family: Arial; font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPersonName" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAuthority" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="90%">
                                            <tr>
                                                <td style="width: 10%;">
                                                </td>
                                                <td style="width: 50%;">
                                                </td>
                                                <td align="right" style="width: 10%;">
                                                    <asp:Label ID="Label29" runat="server" Text="Date :" Style="font-family: Arial; font-size: 12px"></asp:Label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblhippatodaysdate" runat="server" Style="font-family: Arial; font-size: 12px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td align="left" colspan="5" style="height: 10px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%;" colspan="5">
                                                    <asp:Label runat="server" Text="&nbsp;&nbsp;All items on this form have been completed and my questions about this form have been answered. In addition, I
have been provided a copy of the form." ID="Label24" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="5" style="height: 4px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%;" colspan="5">
                                                    <asp:Label runat="server" Text=" &nbsp;&nbsp;*Human Immunodeficiency Virus that cause AIDS. The New York State Public Health law Protectsinformation which resonably could identify someone
as having HIV symptoms or infection and information regarding a person's contacts." ID="Label25" Style="font-family: Arial; font-size: 13px;
                                                        font-weight: bold;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnPatientSignature" Text="Patient Signature" runat="server" OnClientClick="return ShowPatientHippaSignaturePopup()"
                                            Width="130px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnUpdateHippa" runat="server" Text="Update" OnClick="btnUpdateHippa_OnClick"
                                            Width="90px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnHIPPAPDF" runat="server" Text="Print HIPPA Queen PDF" OnClick="btnHIPPAPDF_Click"
                                            BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnHippaFillable" runat="server" Text="Print HIPPA Fillable PDF"
                                            OnClick="btnHippaFillable_Click" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <%--end of table---%>
            </asp:WizardStep>
        </WizardSteps>
        <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
    </asp:Wizard>
    <asp:Wizard ID="Wizard4" runat="server" Width="100%" Font-Size="Small" DisplaySideBar="False"
        ActiveStepIndex="0" Height="60px">
        <WizardSteps>
            <asp:WizardStep runat="server" ID="WizardStep6" StepType="Finish" Title="1:">
                <table style="border: 1px solid; background-color: White" align="left" cellpadding="0"
                    cellspacing="0" class="border" width="100%">
                    <tr>
                           <td style="font-size: 15px" width="99%" height="28" align="center"  valign="middle"
                                bgcolor="#b5df82">
                                <b><u>Lien</u></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="4" cellspacing="4" width="100%" class="border">
                                <tr>
                                    <td style="height: 18px; width: 100%;" align="center" colspan="2">
                                        <asp:Label runat="server" Text="MEDICAL LIEN" Width="401px" ID="lblFormHeader7" Style="font-family: Arial;
                                            font-size: 11px; font-weight: bold;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <asp:Label runat="server" Text="To Attorney" ID="Label8" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td style="width: 90%;">
                                        <asp:Label runat="server" Text="To Attorney" ID="txtlienattorneyname" Style="font-family: Arial;
                                            font-size: 11px; font-weight: bold; width: 60%;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <asp:Label runat="server" Text="Date of Accident:" ID="lblattdoa" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td style="width: 90%" align="left">
                                        <asp:TextBox runat="server" CssClass="textboxCSS" Width="87px" ID="txtliendateofaccident"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="width: 15%;">
                                        <asp:Label runat="server" Text="RE:      Reports and Lien for:" ID="Label6" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td style="width: 85%;">
                                        <asp:Label runat="server" Text="To Attorney" ID="txtlienpatientname" Style="font-family: Arial;
                                            font-size: 12px; font-weight: bold; width: 96%;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td align="left" colspan="5" style="height: 15px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I do hereby authorize the above doctor/medical facility to furnish, you, my attorney, with a full
report, diagnosis, treatment plan,prognosis, etc. for myself in regard to " ID="Label7" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;the accident in which I
was involved." ID="Label98" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 15px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I hereby authorize and direct, you, my attorney, to pay directly to said doctor/medical facility such
sums as may be due and owing said doctor/medical facility for medical services  " ID="Label13" Style="font-family: Arial;
                                            font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;rendered to me
by reason of this accident and to withhold such sums from any settlement, judgment or verdict as
may be necessary to adequately protect said doctor/medical facility." ID="Label95" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I further give a lien on my
case to said doctor/medical facility against any proceeds of any settlement, judgment or verdict
which may be paid to you," ID="Label96" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;my attorney, or to myself, as the result of the injuries for which I have
been treated or injuries in connection therewith." ID="Label97" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 15px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I fully understand that I am directly and fully responsible to said doctor/medical facility for all
medical bills submitted by said doctor/medical facility for services rendered to me and " ID="Label14" Style="font-family: Arial;
                                            font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;that this
agreement is made solely for said doctor/medical facility's additional protection and in
consideration of said doctor/medical facility awaiting payment." ID="Label99" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I further understand that such
payment is not contingent on any settlement, judgment or verdict from which I may eventually
recover said fee." ID="Label100" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 15px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;In the case of automobile accidents, where no-fault regulations govern the medical
reimbursement,this lien will be effective only to the extent of those applicable no-fault
regulations." ID="Label15" Style="font-family: Arial; font-size: 13px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="4" cellspacing="4" width="100%" class="border">
                                <tr>
                                    <td align="center" colspan="7">
                                        <asp:Button ID="btnattPatientsign" runat="server" Text="Patient Signature" OnClientClick="return ShowPatientLienSignaturePopup()"
                                            Width="150px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnattAttorneySign" runat="server" Text="Attorney Signature" OnClientClick="return ShowLienDPROVIDERSignaturePopup()"
                                            Width="150px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnLienPDF" runat="server" Text="Print PDF" OnClick="btnLienPDF_Click"
                                            BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 213px; height: 27px" valign="middle" align="left">
                                            <asp:TextBox runat="server" Height="5px" Width="106px" ID="TXT_I_EVENT4" Visible="False">1</asp:TextBox>
                                        </td>
                                        <td style="width: 331px; height: 27px" valign="middle" align="left">
                                            &nbsp;
                                            <asp:Button runat="server" Text="Save &amp; Print" ID="btnSavePrint1" BackColor="White"
                                                BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                Font-Size="0.8em" ForeColor="#284E98" Visible="false"></asp:Button>
                                            <asp:TextBox runat="server" Width="53px" ID="txtCompanyID1" Visible="False"></asp:TextBox>
                                            <asp:TextBox runat="server" Width="10px" ID="txtEventID1" Visible="False"></asp:TextBox>
                                            <asp:TextBox runat="server" Width="10px" ID="txtCaseID1" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtliendateofaccident" PromptCharacter="_" AutoComplete="true">
                </ajaxToolkit:MaskedEditExtender>--%>
                <ajaxToolkit:MaskedEditExtender runat="server" Mask="99/99/9999" 
                    MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                    CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                    CultureAMPMPlaceholder="" Enabled="True" 
                    TargetControlID="txtliendateofaccident" ID="MaskedEditExtender6"></ajaxToolkit:MaskedEditExtender>

            </asp:WizardStep>
        </WizardSteps>
        <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
    </asp:Wizard>
    <asp:Wizard ID="Wizard5" runat="server" Width="100%" Font-Size="Small" DisplaySideBar="False"
        ActiveStepIndex="0" Height="60px">
        <WizardSteps>
            <asp:WizardStep runat="server" ID="WizardStep7" StepType="Finish" Title="1:">
                <table style="border: 1px solid; background-color: White" align="left" cellpadding="0"
                    cellspacing="4" class="border" width="87%">
                    <tr>
                       <td style="font-size: 15px" width="99%" height="28" align="center"  valign="middle"
                                bgcolor="#b5df82">
                                <b><u>Private Intake Form</u></b>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="4" cellspacing="4" width="100%" class="border">
                                <tr>
                                    <td>
                                        Patient's Name:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtw5patientname" runat="server" Width="80%" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        Social Security #:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtw5ssn" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mailing Address
                                    </td>
                                    <td colspan="6">
                                        <asp:TextBox ID="txtw5mailingaddr" runat="server" Width="51%" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtw5city" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        State:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtw5state" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        Zip:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtw5zip" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date of Birth:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtw5dob" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        Sex:
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rdbw5sex" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                            <asp:ListItem Text="Male" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        Marital Status:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbw5maritalstatus" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="S" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="M" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="D" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="W" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Home Phone:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtw5homeph" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        Work/Cell Phone:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtw5cellph" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <b><u>PRIMARY INSURANCE</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Insurance Name:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtw5priinsname" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td>
                                        Policy Holder:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtw5pripolicyholder" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date of Birth:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" ReadOnly="true" CssClass="textboxCSS" Width="87px" ID="txtpindob"
                                            onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                        <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="btnimgpindob"></asp:ImageButton>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" ControlExtender="MaskedEditExtender9"
                                            ControlToValidate="txtpindob" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="btnimgpindob" Enabled="True"
                                            TargetControlID="txtpindob" ID="CalendarExtender7">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        Relationship
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtw5prirelationship" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <b><u>SECONDARY INSURANCE</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Insurance Name:
                                    </td>
                                    <%--Nirmalkumar--%>
                                    <td>
                                        <asp:TextBox ID="txtw5secInsuranceName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInsuranceType" runat="server" OnSelectedIndexChanged="ddlInsuranceType_SelectedIndexChanged" AutoPostBack="true"> 
                                        <asp:ListItem >Select</asp:ListItem>
                                        <asp:ListItem >Secondary</asp:ListItem>
                                        <asp:ListItem >Major Medical</asp:ListItem>
                                        <asp:ListItem >Private</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    <a id="lnkSearchSecondaryInsuranceCompany" href="javascript:void(0);"  onclick="showSecondaryInsurance()" >Add Secondary Insurance</a>
                                    </td>
                                    <%--END--%>
                                    <td>
                                        Policy Holder:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtw5secpolicyholder" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date of Birth:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" ReadOnly="true" CssClass="textboxCSS" Width="87px" ID="txtsecondpindob"
                                            onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                        <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="btnimgpsecondindob">
                                        </asp:ImageButton>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator10" runat="server" ControlExtender="MaskedEditExtender10"
                                            ControlToValidate="txtsecondpindob" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                        <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="btnimgpsecondindob" Enabled="True"
                                            TargetControlID="txtsecondpindob" ID="CalendarExtender9">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        Relationship
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtw5secrelationship" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <b><u>INSURANCE ASSIGNMENT OF BENEFITS</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <table style="width: 100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td align="left" style="width: 2%">
                                                        <asp:Label runat="server" Text="I," ID="Label35" Style="font-family: Arial; font-size: 12px;"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 24%">
                                                        <asp:Label runat="server" ID="lblinprivate" Style="font-family: Arial; font-size: 12px;
                                                            font-weight: bold; width: 96%;"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 19%">
                                                        <asp:Label runat="server" Text="Member ID#" ID="Label36" Style="font-family: Arial;
                                                            font-size: 12px;"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 30%">
                                                        <asp:TextBox runat="server" CssClass="textboxCSS" Width="51%" ID="txtprivatememberid"></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5">
                                                        <asp:Label runat="server" Text="Request that payment of authorized benefits be made to CitiMedical I, PLLC, for any services furnished to
me by the provider. I authorize any holder of medical information about me to release to my insurance
company(s) any information needed to determine these benefits or the benefits payable for related services." ID="Label38" Style="font-family: Arial;
                                                            font-size: 13px;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5" style="height: 10px;">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <b><u>MEDICARE ASSIGNMENT OF BENEFITS</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <table style="width: 100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td align="left" style="width: 2%">
                                                        <asp:Label runat="server" Text="I," ID="Label37" Style="font-family: Arial; font-size: 12px;"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 24%">
                                                        <asp:Label runat="server" ID="lblmedicarepatient" Style="font-family: Arial; font-size: 12px;
                                                            font-weight: bold; width: 96%;"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 19%">
                                                        <asp:Label runat="server" Text="Medicare#" ID="Label40" Style="font-family: Arial;
                                                            font-size: 12px;"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 30%">
                                                        <asp:TextBox ID="txtprivatemedicare" CssClass="textboxCSS" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="width: 20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5">
                                                        <asp:Label runat="server" Text="Request that payment of authorized Medicare benefits be made to CitiMedical I, PLLC, for any services
furnished to me by the provider. I authorize any holder of medical information about me to release to my insurance company(s) any information needed to determine these benefits or the benefits payable for related
services." ID="Label41" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5" style="height: 10px;">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="7">
                                        <asp:Button ID="btnw5patientsign" runat="server" Text="Patient Signature" OnClientClick="return ShowPatientPVTSignaturePopup()"
                                            Width="150px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnlegalguardian" runat="server" Text="Legal Guardian" OnClientClick=" return  ShowPVTDPROVIDERSignaturePopup()"
                                            Width="150px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnwitness" runat="server" Text="Witness" OnClientClick=" return ShowWitnessPVTSignaturePopup() "
                                            Width="90px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnw5update" runat="server" Text="Update" OnClick="btnw5update_onclick"
                                            Width="90px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                        <asp:Button ID="btnPrivateintakePDF" runat="server" Text="Print PDF" OnClick="btnPrivateintakePDF_Click"
                                            BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtpindob" PromptCharacter="_" AutoComplete="true">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtsecondpindob" PromptCharacter="_" AutoComplete="true">
                </ajaxToolkit:MaskedEditExtender>
            </asp:WizardStep>
        </WizardSteps>
        <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
    </asp:Wizard>
    <asp:Wizard ID="Wizard6" runat="server" Width="100%" Font-Size="Small" DisplaySideBar="False"
        ActiveStepIndex="0" Height="60px">
        <WizardSteps>
            <asp:WizardStep runat="server" ID="WizardStep1" StepType="Finish" Title="1:">
                <table border="1" align="left" cellpadding="0" cellspacing="4" class="border" width="87%">
                    <tr>
                       <td style="font-size: 15px" width="99%" height="28" align="center"  valign="middle"
                                bgcolor="#b5df82">
                                <b><u>PATIENT’S GUARANTOR BILLING AGREEMENT</u></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="4" cellspacing="4" width="100%" class="ContentTable" width="100%"
                                style="background-color: White;" border="1">
                                <tr>
                                    <td>
                                        <p>
                                        - I verify that I have reviewed the information on this form, and that it is correct.<br></br>.<br></br>
                                        - I understand that if my insurance claim is denied due to incorrect personal information
                                        or. incorrect insurance information, that I have provided,<br></br> &nbsp; I will be
                                        billed and payment in full will be due immediately. 
                                            <br> </br>.<br></br> - I understand that
                                        if the patient has a billable insurance plan, and the insurance has not paidthe
                                        claim within 60 days of the date of service, 
                                            <br> </br> &nbsp; charges for that visit
                                        will become my responsibility to pay. 
                                            <br> </br>.<br></br> - I understand that, under the
                                        terms of the contract with the insurance company, co-payment deductible, and co-insurance
                                        must be paid at the time of service.<br></br> &nbsp; If the patient has no insurance
                                        coverage, I agree to pay the balance in full at the time services are provided.<br></br>
                                            <br></br>
                                        - I hereby authorize the release of medical information to the insurance company(ies)
                                        concerning any illness and treatment of the patient. 
                                            <br> </br>
                                            <br></br> - I acknowledge
                                        that I can obtain a copy of the CitiMedical I, PLLC. Privacy Practices/Patient’s
                                        Privacy Rights,<br></br> &nbsp; Patient information Letter and the Patient Financial
                                        Policy from the front desk personnel upon request<br></br>
                                            <br></br> - A service charge of
                                        $25.00 will be assessed on all returned checks.<br></br>
                                            <br></br>
                                            <br></br>
                                            <br></br>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <b>RESPONSIBILITY AGREEMENT</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p style="font-size: 10.5px;">
                                            <b>acknowledge and understand that I am financially responsible for all services rendered
                                                to me by CitiMedical I. Although CitiMedical I. may bill my insurance carrier for
                                                services 
                                            <br> </br> on my behalf, I understand that it is still my responsibility to
                                                make sure that the bill is paid within a reasonable length of time. If for any reason,
                                                there is a balance owing after the 
                                            <br> </br> insurance pays, I agree to pay the balance
                                                within 30 days of being billed. I also understand that if litigation becomes necessary
                                                to recoup any balance due to CitiMedical,<br></br>I will be held liable to any attorney’s
                                                fees and court cost that are applicable<br></br> </b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Patient/Guarantor Signature:
                                                    <asp:Image runat="server" ID="imgPatinentPaymentGuarantee" />
                                                </td>
                                                <td align="right">
                                                    Sign Date: &nbsp;
                                                    <asp:TextBox ID="txtSign" runat="server"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton8"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton8" Enabled="True"
                                                        TargetControlID="txtSign" ID="CalendarExtender13">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updates: I have reviewed the information on this form and I verify that all the
                                        information is current and unchanged.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Initials/Date
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtIntials1" runat="server"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton1"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton1" Enabled="True"
                                                        TargetControlID="txtIntials1" ID="CalendarExtender1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntials2" runat="server"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton2"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton2" Enabled="True"
                                                        TargetControlID="txtIntials2" ID="CalendarExtender3">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntials3" runat="server"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton3"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton3" Enabled="True"
                                                        TargetControlID="txtIntials3" ID="CalendarExtender4">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIntials4" runat="server"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="ImageButton6"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="ImageButton6" Enabled="True"
                                                        TargetControlID="txtIntials4" ID="CalendarExtender12">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="5">
                                                    <asp:Button ID="btnGuarantorSign" runat="server" Text="PatientSign" Width="90px"
                                                        BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" OnClientClick=" return ShowPatientGuarantorSignaturePopup(); " />
                                                    <asp:Button ID="btnGuarantorSave" runat="server" Text="Save" Width="90px" BackColor="White"
                                                        BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                        OnClick="btnGuarantorSave_Click" Font-Size="1.0em" ForeColor="#284E98" />
                                                    <asp:Button ID="btnGuarantorPrintPdf" runat="server" Text="Print PDF" BackColor="White"
                                                        BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                        Font-Size="1.0em" ForeColor="#284E98" OnClick="btnGuarantorPrint_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
    <asp:Wizard ID="Wizard8" runat="server" Width="100%" Font-Size="Small" DisplaySideBar="False"
        ActiveStepIndex="0" Height="60px">
        <WizardSteps>
            <asp:WizardStep runat="server" ID="WizardStep10" StepType="Finish" Title="1:">
                <table align="left" cellpadding="0" cellspacing="4" class="border" width="100%" style="border: 1px solid;
                    background-color: White">
                    <tr>
                       <td style="font-size: 15px" width="99%" height="28" align="center"  valign="middle"
                                bgcolor="#b5df82">
                                <b><u>DECLARATION AND HIPPA QUEENS</u></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="4" cellspacing="4" width="100%" class="border">
                                <tr>
                                    <td colspan="5" align="center">
                                        <b><u>Declaration</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 2%">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I," ID="Label39" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 24%">
                                        <asp:TextBox ID="txtdeclaraionname" runat="server" Width="96%"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 19%">
                                        <asp:Label runat="server" Text=", declare as follows:" ID="Label43" Style="font-family: Arial;
                                            font-size: 13px;"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 30%">
                                    </td>
                                    <td align="left" style="width: 20%">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 15px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;That at the time of the accident occurring on"
                                            ID="Label42" Style="font-family: Arial; font-size: 12px;"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxCSS" Width="87px" ID="txtdecdoa" onkeypress="return clickButton1(event,'/')"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;(date) I was driver/passenger/pedestrian/
present in the vehicle involved, and I sustained injuries as a result of said accident.  " ID="Label44" Style="font-family: Arial;
                                            font-size: 12px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label60" runat="server" Text="&nbsp;&nbsp;I hereby certify that I
have sought your treatment and/or service on my own and that no one solicited my coming for treatment
or promised me any remuneration or benefit for " Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label101" runat="server" Text="&nbsp;&nbsp;coming for treatment.1 further declare that prior to the
accident I had no knowledge of the driver of the other vehicle involved and to the best of my knowledge
said accident" Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label61" runat="server" Text="&nbsp;&nbsp;was not staged.I have been advised that bringing a fraudulent claim is a crime punishable by
imprisonment fine, and/or both." Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 15px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <table width="60%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label runat="server" Text="Print Name" ID="Label45" Style="font-family: Arial;
                                                        font-size: 12px; font-weight: bold;"></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="textboxCSS" Width="60%" ID="txtprtintname"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label runat="server" Text="SS#:" ID="Label46" Style="font-family: Arial; font-size: 12px;
                                                        font-weight: bold;"></asp:Label>
                                                    <asp:Label ID="lbldeclartionssn" runat="server" ReadOnly="true" Style="font-family: Arial;
                                                        font-size: 11px; font-weight: bold;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label runat="server" Text="Signature:" ID="Label55" Style="font-family: Arial;
                                                        font-size: 12px; font-weight: bold;"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <b>Sign Date :</b>
                                                    <asp:TextBox runat="server" CssClass="textboxCSS" Width="87px" ID="txtdecsigndate"
                                                        onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="btnimgprintsign"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="btnimgprintsign" Enabled="True"
                                                        TargetControlID="txtdecsigndate" ID="CalendarExtender2">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="4" cellspacing="4" width="100%" class="border">
                                <tr>
                                    <td colspan="5">
                                        <b><u>PATIENT HIPAA AWARENESS</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;With my awareness, CitiMedical I., PLLC may use and disclose protected health information (PHI) about
me to carry out treatment, payment, and healthcare operations (TPO). " ID="Label47" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;Pleased refer to CitiMedical I, PLLC 's
Notice of Privacy Practices for a more complete description of such uses and disclosures." Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I have the right to review the Notice of Privacy Practices prior to signing this consent. CitiMedical I., PLLC
reserves the right to revise its Notice of Privacy Practices at any time. " ID="Label48" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label54" runat="server" Text="&nbsp;&nbsp;A revised Notice of Privacy Practices may be
obtained by forwarding a written request to the Privacy Officer." Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;With my permission, the office of CitiMedical I, PLLC may call my home or other designated location and
leave a message on voice mail or in person in reference to " ID="Label49" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label56" runat="server" Text="&nbsp;&nbsp;any items that assist the practice in carrying out TPO, such
as appointment reminders, insurance items and any call pertaining to my clinical care, including laboratory results
among others." Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;With my permission, the office of CitiMedical I., PLLC may mail to my home or other designated location any
items that assist the practice in carrying out TPO, " ID="Label50" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label57" runat="server" Text="&nbsp;&nbsp;such as appointment reminder cards and patient statements as long
as they are marked Personal and Confidential." Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;With my permission, the office of CitiMedical I., PLLC may e-mail to my home or other designated location
any items that assist the practice in carrying out TPO 
" ID="Label51" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label58" runat="server" Text="&nbsp;&nbsp;such as appointment reminder cards and patient statements. I
have the right to request that CitiMedical I., PLLC restricts how it uses or discloses my PHI to carry out TPO." Style="font-family: Arial;
                                            font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label ID="Label59" runat="server" Text="&nbsp;&nbsp;However, the practice is not required to agree to my requested restrictions, but if it does, it is bound by this agreement."
                                            Style="font-family: Arial; font-size: 13px;">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;By signing this, I am allowing CitiMedical I., PLLC to use and disclose my PHI for TPO."
                                            ID="Label52" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5">
                                        <asp:Label runat="server" Text="&nbsp;&nbsp;I may revoke my consent in writing except to the extent that the practice has already made disclosure in reliance
upon my prior consent." ID="Label53" Style="font-family: Arial; font-size: 13px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="5" style="height: 10px;">
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td colspan="5">
                                        <table width="60%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label runat="server" Text="Signature:" ID="Label54" Style="font-family: Arial;
                                                        font-size: 12px;"></asp:Label>
                                                        
                                                </td>
                                                <td>
                                                    Sign Date:
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ReadOnly="true" CssClass="textboxCSS" Width="87px" ID="txtdecsigndate"
                                                        onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Images/cal.gif" ID="btnimgdecsigdate"></asp:ImageButton>
                                                    <ajaxToolkit:CalendarExtender runat="server" PopupButtonID="btnimgdecsigdate" Enabled="True"
                                                        TargetControlID="txtdecsigndate" ID="CalendarExtender1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>--%>
                            </table>
                            <table style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 530; height: 23px" align="center">
                                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="Button1" runat="server" OnClientClick=" return ShowPatientDeclapaignaturePopup()"
                                                Text="Signature" Width="150px" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
                                            <asp:Button ID="Button2" runat="server" Text="Signature of Patient or Legal Guardian"
                                                OnClientClick=" return ShowPatientDeclaLegalGuardianPopup()" Width="280px" BackColor="White"
                                                BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                Font-Size="1.0em" ForeColor="#284E98" />
                                            <asp:Button ID="Button3" runat="server" Text="Save" BackColor="White" BorderColor="#507CD1"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em"
                                                ForeColor="#284E98" OnClick="btnDeclHippaSave_Click" />
                                            <asp:Button ID="Button4" runat="server" Text="Print PDF" BackColor="White" BorderColor="#507CD1"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em"
                                                ForeColor="#284E98" OnClick ="btnDeclHippaPrint_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
    <div id="divDocSignatureAob" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseAOBDPROVIDERSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmDocSignatureAob" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignatureAOB" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePatientAOBSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmPatientSignatureAOB" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divDocSignatureIntake" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseIntakeDPROVIDERSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmDocSignatureIntake" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignatureIntake" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePatientIntakeSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmPatientSignatureIntake" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignatureHippa" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePatientHippaSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmPatientSignatureHippa" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divDocSignatureLien" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseLienDPROVIDERSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmDocSignatureLien" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignatureLien" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePatientLienSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmPatientSignatureLien" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divDocSignaturePVT" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePVTDPROVIDERSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmDocSignaturePVT" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignaturePVT" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePatientPVTSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmPatientSignaturePVT" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignaturePVTWT" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseWitnessPVTSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmPatientSignaturePVTWT" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divPatientSignatureDCHQ" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseDCHQignaturePopup()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frmPatientSignatureDCHQ" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divDocSignaturePatientGuarantor" style="position: relative; width: 500px;
        height: 350px; border: silver 10px solid; background-color: #B5DF82; visibility: hidden;
        border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
        border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="ClosePatientGuarantorSignaturePopup()" style="cursor: pointer;" title="Close">
                X</a>
        </div>
        <iframe id="frmPatientGuarantor" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div id="divLegalGuardianDCHQ" style="position: relative; width: 500px; height: 350px;
        border: silver 10px solid; background-color: #B5DF82; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 10px solid;">
        <div style="position: relative; text-align: right; background-color: #B5DF82;">
            <a onclick="CloseDCHQLegalGuardianPopup()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frmLegalGuardianDCHQ" src="" frameborder="1" height="350px" width="500px"
            visible="false"></iframe>
    </div>
    <div style="visibility: hidden;">
        <asp:TextBox ID="TXT_CASE_ID" runat="server"></asp:TextBox></div>
        <%--Nirmalkumar--%>
        <dx:ASPxPopupControl ID="SecInsuracePop" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="SecInsuracePop" HeaderText="Secondary Insurance Information" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="900px" ToolTip="Patient Information" PopupHorizontalOffset="0"
        PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
        RenderIFrameForPopupElements="Default" Height="400px">
         <ClientSideEvents CloseButtonClick="ClosePopup" />
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <div style="visibility: hidden;">
            <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                OnClick="txtUpdate_Click" /></div>
    <asp:HiddenField ID="secInsuranceId" runat="server" />
    <asp:TextBox runat="server"  ID="txtCaseID" Visible="false"></asp:TextBox>
    <asp:TextBox runat="server"  ID="txtCompanyID" Visible="false"></asp:TextBox>
    <%--END--%>
</asp:Content>
