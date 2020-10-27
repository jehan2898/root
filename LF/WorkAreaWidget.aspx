<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LF/MasterPage.master"
    CodeFile="WorkAreaWidget.aspx.cs" Inherits="WorkAreaWidget" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <script type="text/javascript">
    
     function OpenDocumentManager()
    {          
    var CaseId=document.getElementById("<%=hdnCaseId.ClientID%>").value;
    var CaseNo=document.getElementById("<%=hdnCaseNo.ClientID%>").value;
    var cmpid=document.getElementById("<%=hdnCompanyId.ClientID%>").value;
     window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid='+CaseId+'&caseno='+CaseNo+'&cmpid='+cmpid ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
    }
    
        function openURL(url, name)
        {
            if(name == "")
            {
                alert("Files not found!");
            }
            else
            {
                 var bname = navigator.appName; 
		   //check type of the browser. if browser is Firefox or chrome, load plugin otherwise load activex control.
		        if(bname == "Netscape")
		        {
                    var url1 = url;
                    window.open(url1, "","top=10,left=100,menubar=no,toolbar=no,location=no,width=750,height=600,status=no,scrollbars=no,maximize=null,resizable=1,titlebar=no;");
                }
                else
                {
                    window.location.href =url;
                }
            }
        }
    </script>

    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table border="0">
        <tr>
            <td style="width: 80%;">
                <asp:DataList ID="DtlView" runat="server" CssClass="TDPart" BorderWidth="0px" BorderStyle="None"
                    BorderColor="#DEBA84" RepeatColumns="1">
                    <ItemTemplate>
                        <div style="padding: 2px; width: auto;">
                            &nbsp;
                        </div>
                        <!-- Holding table. This table will hold all the controls on the page -->
                        <table id="lastTablel" runat="server" class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0" border="1">
                            <tr>
                                <td class="td-widget-lf-top-holder-division-ch">
                                    <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 451px;
                                        border: 1px solid #B5DF82;">
                                        <tr>
                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                &nbsp;<b class="txt3">Personal Information</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 446px;">
                                                <!-- outer table to hold 2 child tables -->
                                                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
                                                    <tr>
                                                        <td class="td-widget-lf-holder-ch">
                                                            <!-- Table 1 - to hold the actual data -->
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        First Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Middle Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Last Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        D.O.B
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "DOB") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Gender
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem,"SZ_GENDER") %>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Address
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        City
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        State
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_STATE")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Home Phone
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Work
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        ZIP
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
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
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMAIL")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Extn.
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Attorney
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_ATTORNEY")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Case Type
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_TYPE")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Case Status
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_CASE_STATUS")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        SSN
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>
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
                                <td class="td-widget-lf-top-holder-division-ch">
                                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                        border: 1px solid #B5DF82;">
                                        <tr>
                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                &nbsp;<b class="txt3">Insurance Information</b>
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
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Policy Holder
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem,"SZ_POLICY_HOLDER") %>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Ins. Address
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        -
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Address
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ADDRESS") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        City
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_CITY") %>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        State
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_STATE") %>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        ZIP
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ZIP") %>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Phone
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_PHONE")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                                        FAX
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch" style="height: 33px">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_FAX_NUMBER")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch" style="height: 33px">
                                                                        Contact Person
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch" style="height: 33px">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_CONTACT_PERSON")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Claim File#
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Policy #
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%>
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
                                <td class="td-widget-lf-division-rightmost">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-widget-lf-top-holder-division-ch">
                                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                        border: 1px solid #B5DF82;">
                                        <tr>
                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                &nbsp;<b class="txt3">Accident Information</b>
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
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Accident Date
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Plate Number
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Report Number
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Address
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_ADDRESS")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        City
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_CITY")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        State
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_INSURANCE_STATE")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Hospital Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_NAME")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Hospital Address
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_ADDRESS")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Date Of Admission
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "DT_ADMISSION_DATE")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Additional Patient
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FROM_CAR")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Describe Injury
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_DESCRIBE_INJURY")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Patient Type
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_TYPE")%>
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
                                <td class="td-widget-lf-top-holder-division-ch">
                                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                        border: 1px solid #B5DF82;">
                                        <tr>
                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                &nbsp;<b class="txt3">Employer Information</b>
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
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_NAME")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Address
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ADDRESS")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        City
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_CITY")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        State
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_STATE")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        ZIP
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ZIP")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Phone
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_PHONE")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Date Of First Treatment
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "DT_FIRST_TREATMENT")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Chart No.
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
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
                                <td class="td-widget-lf-division-rightmost">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="td-widget-lf-top-holder-division-ch">
                                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                        border: 1px solid #B5DF82;" id="tblF">
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
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>
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
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PHONE")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Extension
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EXTENSION")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        FAX
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_FAX")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Email
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_EMAIL")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                            <div id="abc" runat="server">
                                            </div>
                                            </td>
                                           
                                        </tr>
                                    </table>
                                </td>
                               <%-- <td class="td-widget-lf-top-holder-division-ch">
                                     
                           
                                    <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 451px;
                                        border: 1px solid #B5DF82;">
                                        <tr>
                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                &nbsp;<b class="txt3">Download Information</b>
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
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        Last Download Date
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "Last_date")%>
                                                                    </td>
                                                                    <td class="td-widget-lf-desc-ch">
                                                                        User Name
                                                                    </td>
                                                                    <td class="td-widget-lf-data-ch">
                                                                        &nbsp;
                                                                        <%# DataBinder.Eval(Container.DataItem, "User_Name")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                   
                                           
                                </td>--%>
                                <td class="td-widget-lf-division-rightmost">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                
            </td>
            <td style="width: 20%; vertical-align: text-top;" align="left">
                <asp:UpdatePanel ID="updatepnl" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton runat="server" Text="Download Litigation" ID="lnkDownload" OnClick="lnkDownload_Click"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                     <asp:LinkButton runat="server" Text="Document Manager" ID="lnkDocmanager" onClientclick="javascript:OpenDocumentManager();"></asp:LinkButton>
                         
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="10"
                    AssociatedUpdatePanelID="updatepnl">
                    <ProgressTemplate>
                        <div style="vertical-align: bottom; text-align: left" id="DivStatus2" class="PageUpdateProgress"
                            runat="Server">
                            <asp:Image ID="img2" runat="server" Height="25px" Width="24px" ImageUrl="~/Images/rotation.gif"
                                AlternateText="Downloading....."></asp:Image>
                            Loading...</div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:HiddenField ID="hdnCaseId" runat="server" />
                <asp:HiddenField ID="hdnCaseNo" runat="server" />
                <asp:HiddenField ID="hdnCompanyId" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
