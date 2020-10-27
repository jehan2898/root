<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Pre_Authsent.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Pre_Authsent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="framehead">
    <title>Untitled Page</title>
</head>

<script type="text/javascript" language="javascript">
        function spanhide()
         {
        document.getElementById('lblcharttext').style.display= 'none';
        document.getElementById('tdlchartno').style.display= 'none';
         }
       function GetInsuranceValue(source, eventArgs)
       {
        document.getElementById('hdninsurancecode').value = eventArgs.get_value();
       }
       
</script>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table runat="server" width="100%" style="background-color: White; border-color: #DEBA84;"
            border="0">
            <tr>
                <td>
                    <table class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="td-widget-lf-top-holder-division-ch">
                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                                    border: 1px solid #B5DF82;">
                                    <tr>
                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                            &nbsp;<b class="txt3">Personal Information</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 446px;">
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="td-widget-lf-holder-ch">
                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    First Name
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--  <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>--%>
                                                                    <asp:TextBox ID="txtPatientFName" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Middle Name
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%-- <%# DataBinder.Eval(Container.DataItem, "MI")%>--%>
                                                                    <asp:TextBox ID="txtMI" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Last Name
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>--%>
                                                                    <asp:TextBox ID="txtPatientLName" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    D.O.B
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "DOB") %>--%>
                                                                    <asp:TextBox Width="69%" ID="txtDateOfBirth" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                        MaxLength="10"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"
                                                                        PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Gender
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem,"SZ_GENDER") %>--%>
                                                                    <asp:DropDownList ID="ddlSex" runat="server" Width="141px">
                                                                        <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                                                        <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Address
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>--%>
                                                                    <asp:TextBox Width="90%" ID="txtPatientAddress" runat="server"></asp:TextBox>
                                                                    <asp:TextBox Visible="False" ID="txtPatientStreet" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    City
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>--%>
                                                                    <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    State
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--       <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_STATE")%>--%>
                                                                    <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
                                                                    <extddl:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                                                        OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Home Phone
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%-- &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>--%>
                                                                    <asp:TextBox ID="txthomephone" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Work
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>--%>
                                                                    <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    ZIP
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--  <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>--%>
                                                                    <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="td-widget-lf-data-ch">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Email
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>--%>
                                                                    <asp:TextBox ID="txtPatientEmail" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Extn.
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>--%>
                                                                    <asp:TextBox ID="txtExtension" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Attorney
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%-- <%# DataBinder.Eval(Container.DataItem, "SZ_ATTORNEY")%>--%>
                                                                    <extddl:ExtendedDropDownList ID="extddlAttorney" Width="95%" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                                                        OldText="" StausText="False" />
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Case Type
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%-- <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_TYPE")%>--%>
                                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                        OldText="" StausText="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    Case Status
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%--  <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_STATUS")%>--%>
                                                                    <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                        Flag_ID="txtCompanyID.Text.ToString();" OldText="" StausText="False" />
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    SSN
                                                                </td>
                                                                <td class="td-widget-lf-desc-ch">
                                                                    <%-- <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>--%>
                                                                    <asp:TextBox ID="txtSocialSecurityNumber" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td-widget-lf-top-holder-division-ch">
                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                        border: 1px solid #B5DF82;">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                &nbsp;<b class="txt3">Insurance Information</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 446px;">
                                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="td-widget-lf-holder-ch">
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Policy Holder
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox ID="txtPolicyHolder" runat="server" CssClass="text-box" Width="61%"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Name
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                            DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany"
                                                            ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true"
                                                            ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                        <asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="75%"
                                                            OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true" />
                                                        <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server"
                                                            Visible="false" Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                            Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                            OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged"
                                                            OldText="" StausText="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Ins. Address
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        <asp:ListBox Width="100%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                            OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Address
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        City
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        State
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        ZIP
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox Width="80%" ID="txtInsuranceZip" runat="server" CssClass="text-box"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Phone
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox Width="80%" ID="txtInsPhone" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                        FAX
                                                    </td>
                                                    <td class="td-widget-lf-data-ch" style="height: 33px">
                                                        &nbsp;
                                                        <asp:TextBox ID="txtInsFax" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                        Contact Person
                                                    </td>
                                                    <td class="td-widget-lf-data-ch" style="height: 33px">
                                                        &nbsp;
                                                        <asp:TextBox Width="80%" ID="txtInsContactPerson" runat="server" CssClass="text-box"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Claim File#
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Policy #
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                        <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="text-box"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td-widget-lf-top-holder-division-ch">
                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                        border: 1px solid #B5DF82;">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                &nbsp;<b class="txt3">Accident Information</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 446px;">
                                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="td-widget-lf-holder-ch">
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch" style="width: 19%;">
                                                        Accident Date
                                                    </td>
                                                    <td class="td-widget-lf-data-ch" align="left" style="width: 31%;">
                                                        <asp:TextBox Width="90%" ID="txtATAccidentDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                        <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="txtATAccidentDate"
                                                            PopupButtonID="imgbtnATAccidentDate" Enabled="True" />
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" style="width: 19%;">
                                                        Plate Number
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" style="width: 31%;">
                                                        <asp:TextBox ID="txtATPlateNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Report Number
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtATReportNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Address
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtATAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        City
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtATCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        State
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <extddl:ExtendedDropDownList ID="extddlATAccidentState" runat="server" Width="90%"
                                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                            Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Hospital Name
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Hospital Address
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtATHospitalAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Date Of Admission
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox Width="70%" ID="txtATAdmissionDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                        <asp:ImageButton ID="imgbtnATAdmissionDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                        <ajaxToolkit:CalendarExtender ID="calATAdmissionDate" runat="server" TargetControlID="txtATAdmissionDate"
                                                            PopupButtonID="imgbtnATAdmissionDate" Enabled="True" />
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Additional Patient
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtATAdditionalPatients" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Describe Injury
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtATDescribeInjury" runat="server" MaxLength="200" Width="82%"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Patient Type
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:RadioButtonList ID="rdolstPatientType" runat="server" RepeatDirection="Horizontal"
                                                            class="lbl">
                                                            <asp:ListItem Value="0">Bicyclist</asp:ListItem>
                                                            <asp:ListItem Value="1">Driver</asp:ListItem>
                                                            <asp:ListItem Value="2">Passenger</asp:ListItem>
                                                            <asp:ListItem Value="3">Pedestrian</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td-widget-lf-top-holder-division-ch">
                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                        border: 1px solid #B5DF82;">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                &nbsp;<b class="txt3">Employer Information</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 446px;">
                                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="td-widget-lf-holder-ch">
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch" style="width: 19%;">
                                                        Name
                                                    </td>
                                                    <td class="td-widget-lf-data-ch" align="left" style="width: 31%;">
                                                        <asp:TextBox Width="99%" ID="txtEmployerName" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" style="width: 19%;">
                                                        Address
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" style="width: 31%;">
                                                        <asp:TextBox ID="txtEmployerAddress" runat="server" Width="99%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        City
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtEmployerCity" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        State
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtEmployerState" runat="server" Visible="False"></asp:TextBox>
                                                        <extddl:ExtendedDropDownList ID="extddlEmployerState" runat="server" Selected_Text="--- Select ---"
                                                            Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                            Width="90%" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        ZIP
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtEmployerZip" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Phone
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtEmployerPhone" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Date Of First Treatment
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox Width="45%" ID="txtDateofFirstTreatment" runat="server" onkeypress="return clickButton1(event,'/')"
                                                            MaxLength="10"></asp:TextBox><asp:ImageButton ID="imgbtnDateofFirstTreatment" runat="server"
                                                                ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="CalendarExtender1"
                                                                    runat="server" TargetControlID="txtDateofFirstTreatment" PopupButtonID="imgbtnDateofFirstTreatment"
                                                                    Enabled="True" />
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" id="tdlcharttext" runat="server">
                                                        <asp:Label ID="lblcharttext" runat="server" Text="Chart No" class="td-widget-lf-desc-ch"
                                                            Visible="true"></asp:Label>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" id="tdlchartno" runat="Server">
                                                        <asp:TextBox ID="txtChartNo" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td-widget-lf-top-holder-division-ch">
                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 100%;
                        border: 1px solid #B5DF82;">
                        <tr>
                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                &nbsp;<b class="txt3">Adjuster Information</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 446px;">
                                <!-- outer table to hold 2 child tables -->
                                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="td-widget-lf-holder-ch">
                                            <!-- Table 1 - to hold the actual data -->
                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch" style="width: 19%;">
                                                        Name
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch" style="width: 31%;">
                                                        &nbsp;
                                                        <extddl:ExtendedDropDownList ID="extddlAdjuster" Width="100%" runat="server" Connection_Key="Connection_String"
                                                            Selected_Text="--- Select ---" Flag_Key_Value="GET_ADJUSTER_LIST" Procedure_Name="SP_MST_ADJUSTER"
                                                            AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlAdjuster_extendDropDown_SelectedIndexChanged"
                                                            OldText="" StausText="False" />
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        &nbsp;
                                                    </td>
                                                    <td class="td-widget-lf-data-ch">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Phone
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtAdjusterPhone" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Extension
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtAdjusterExtension" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-lf-desc-ch">
                                                        FAX
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox ID="txtfax" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        Email
                                                    </td>
                                                    <td class="td-widget-lf-desc-ch">
                                                        <asp:TextBox Width="98%" ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="width: 100%;" border="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnshowpdf" runat="server" Text="Print PDF" Width="10%" />
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtcaseid" runat="server" Visible="false"></asp:TextBox>
        <asp:HiddenField ID="hdninsurancecode" runat="server" />
    </form>
</body>
</html>
