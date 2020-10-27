<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientViewFrame.aspx.cs" Inherits="AJAX_Pages_PatientViewFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" id="framehead">
    <title>Patient View</title>
    
</head>
<script type="text/javascript" language="javascript">
        function spanhide() {
     // alert("call");
      document.getElementById('PatientDtlView_ctl00_lblcharttext').style.display= 'none';
      //  alert("ok");
        document.getElementById('PatientDtlView_ctl00_tdlchartno').style.display= 'none';
     //   
      // alert("hide done");
       }
       
    </script>
<body>

    <form id="form1" runat="server">
    
    
    <asp:DataList ID="PatientDtlView" runat="server"    BorderWidth="0px" BorderStyle="None"
                    BorderColor="#DEBA84" RepeatColumns="1"   backcolor="white" Width="100%" Visible="false">
                    <ItemTemplate>
                        
                        <!-- Holding table. This table will hold all the controls on the page -->
                        <table class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0">
                        <%--<tr>
                        
                            <td id="dvdrag" height="28%" align="right"  bgcolor="#B5DF82" class="txt2" style="width: 100%;" valign="middle">
                               <asp:Button ID="btnClose" OnClientClick="$find('modal').hide(); return false;" runat="server" Width="80px" Text="Close"></asp:Button>
                                       
                            </td>
                        
                        </tr>--%>
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
                                                <!-- outer table to hold 2 child tables -->
                                                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="td-widget-lf-holder-ch">
                                                            <!-- Table 1 - to hold the actual data -->
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
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
                                                                        <%# DataBinder.Eval(Container.DataItem, "MI")%>
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
                                                                        &nbsp;  <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
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
                                                                        <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>
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
                                                                    <td class="td-widget-lf-desc-ch" id="tdlcharttext" runat="server">
                                                                        <asp:Label ID="lblcharttext" runat="server" Text = "Chart No" class = "td-widget-lf-desc-ch" Visible="true"></asp:Label>
                                                                      </td>
                                                                    <td class="td-widget-lf-data-ch" id="tdlchartno" runat="Server">
                                                                     <asp:Label ID="lblchartvalue" runat="server" Text = '<%# DataBinder.Eval(Container.DataItem, "SZ_CHART_NO")%>' class = "td-widget-lf-data-ch" Visible="true"></asp:Label>
                                                                     
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
                                        </tr>
                                    </table>
                                </td>
                               
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
               
  </form>
</body>
</html>
