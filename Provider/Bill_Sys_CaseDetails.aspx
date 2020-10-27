<%@ Page Language="C#" MasterPageFile="~/Provider/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CaseDetails.aspx.cs" Inherits="Provider_Bill_Sys_CaseDetails"
    Title="Patient Desk" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="Panel1"
        CollapsedSize="20" ExpandedSize="160" Collapsed="true" AutoCollapse="false" AutoExpand="false"
        TextLabelID="lblcpe" CollapsedText="(Show Details...)" ExpandedText="(Hide Details)"
        ExpandControlID="LinkButton1" CollapseControlID="LinkButton1" ImageControlID="Image1"
        ExpandedImage="../Images/up.jpg" CollapsedImage="../Images/dwn.jpg" />

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

    <script type="text/javascript"> 
        function redirect() 
        {  
            var CaseId=document.getElementById("<%=hdnCaseId.ClientID%>").value;
            var CaseNo=document.getElementById("<%=hdnCaseNo.ClientID%>").value;
            var cmpid=document.getElementById("<%=hdnCompanyId.ClientID%>").value;
            location.href = "Bill_Sys_PatientDesk.aspx?caseid="+CaseId+"&caseno="+CaseNo+"&cmpid="+cmpid; 
        } 
    </script>

    <script type="text/javascript"> 
        function redirectbillsearch() 
        {  
            var CaseId=document.getElementById("<%=hdnCaseId.ClientID%>").value;
            var CaseNo=document.getElementById("<%=hdnCaseNo.ClientID%>").value;
            var cmpid=document.getElementById("<%=hdnCompanyId.ClientID%>").value;
            location.href = "Bill_Sys_BillSearch.aspx?fromCase=true&caseid="+CaseId+"&caseno="+CaseNo+"&cmpid="+cmpid; 
        }
   function OpenDocumentManagerProvider()
    {  
    var CaseId=document.getElementById("<%=hdnCaseId.ClientID%>").value;
    var CaseNo=document.getElementById("<%=hdnCaseNo.ClientID%>").value;
    var cmpid=document.getElementById("<%=hdnCompanyId.ClientID%>").value;
    var OfficeId=document.getElementById("<%=hdnOfficeId.ClientID%>").value;
   
     window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid='+CaseId+'&caseno='+CaseNo+'&cmpid='+cmpid+'&OfficeId='+OfficeId ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
    }
    </script>

    <table width="100%" border="0">
        <tr>
            <td style="background-color: rgb(219, 230, 250); height: 100%; width: 10%;" valign="top">
                <div>
                    <table width="100%" border="0">
                        <tr>
                            <td height="28" valign="middle" style="background-image: url(images/link-roll.jpg);
                                background-position: right; height: 35px;">
                                <b class="txt3">Desk</b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="txt2">
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="redirect(); return false;">Patient Desk</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="txt2">
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClientClick="redirectbillsearch(); return false;">View Bills</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="txt2">
                                <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="OpenDocumentManagerProvider(); return false;">Document Manager</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="width: 90%;" valign="top">
                <table border="0" style="vertical-align: top;">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80%;" valign="top">
                            <asp:DataList ID="DtlView" runat="server" CssClass="TDPart" BorderWidth="0px" BorderStyle="None"
                                BorderColor="#DEBA84" RepeatColumns="1">
                                <ItemTemplate>
                                    <table id="lastTablel" runat="server" class="td-widget-lf-top-holder-ch" cellpadding="0"
                                        cellspacing="0" border="0">
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
                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_DOB")%>
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
                            <%-- <asp:UpdatePanel ID="updatepnl" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton runat="server" Text="Download Litigation" ID="lnkDownload" OnClick="lnkDownload_Click"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                     <asp:LinkButton runat="server" Text="Document Manager" ID="lnkDocmanager" onClientclick="javascript:OpenDocumentManager();"></asp:LinkButton>
                         
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
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
                            <asp:HiddenField ID="hdnOfficeId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; height: 160px" class="SectionDevider" valign="top">
                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                <table width="100%" cellpadding="0" cellspacing="0" style="width: 100%; height: 25px;
                                    background-color: #5998C9; vertical-align: text-top;">
                                    <tr style="height: 3px">
                                        <td align="center" style="width: 90%;" valign="top">
                                            <asp:LinkButton ID="LinkButton1" Font-Underline="false" ToolTip="Show Bills" runat="server"
                                                OnClick="LinkButton1_Click" BackColor="#5998C9" Font-Bold="false" Font-Size="Larger"
                                                ForeColor="white">
                                                Bills &nbsp;&nbsp;
                                                <asp:Label ID="lblcpe" runat="server" Style="color: White"></asp:Label>
                                            </asp:LinkButton>
                                        </td>
                                        <td align="right">
                                            <asp:Image ID="Image1" runat="server" />&nbsp;&nbsp;&nbsp;
                                            <%--<div id ="plusdiv" runat="server">
	                                        <img id="img2" src="../Images/Plus.JPG" alt="" style="border:none;"   runat="server"/> 
	                                        </div>
	                                       
	                                        <div id ="divimg" runat="server" style="visibility:hidden;">
	                                     <img id="img3" src="../Images/Minus.bmp" alt="" style="border:none;"  runat="server" />
	                                     </div>--%>
                                        </td>
                                    </tr>
                                </table>
                                <asp:UpdatePanel ID="Up_Bills" runat="server">
                                    <ContentTemplate>
                                        <table id="tblViewBills" runat="server" width="100%">
                                            <tr>
                                                <td>
                                                    <div style="overflow: scroll; height: 150px">
                                                        <asp:Repeater ID="rpt_ViewBills" runat="server" OnItemCommand="rptPatientDeskList_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table align="left" cellpadding="0" cellspacing="0" style="width: 98%; border: #8babe4 1px solid #B5DF82;">
                                                                    <tr>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                            <b>Bill#</b>
                                                                        </td>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                                                            <b>Speciality</b>
                                                                        </td>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                            <b>Visit Date</b>
                                                                        </td>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                            <b>Bill Date</b>
                                                                        </td>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                            <b>Bill Status</b>
                                                                        </td>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                            <b>Bill Amount</b>
                                                                        </td>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                            <b>Paid</b>
                                                                        </td>
                                                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                                                            <b>Outstanding</b>
                                                                        </td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>
                                                                    </td>
                                                                    <td bgcolor="white" class="lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                                        <%# DataBinder.Eval(Container,"DataItem.SPECIALITY")%>
                                                                    </td>
                                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82; text-align: center;">
                                                                        <%# DataBinder.Eval(Container,"DataItem.PROC_DATE")%>
                                                                    </td>
                                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82; text-align: center;">
                                                                        <%# DataBinder.Eval(Container,"DataItem.DT_BILL_DATE")%>
                                                                    </td>
                                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                                                                        <%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>
                                                                    </td>
                                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82; text-align: right;">
                                                                        $<%# DataBinder.Eval(Container,"DataItem.FLT_BILL_AMOUNT" )%>
                                                                    </td>
                                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82; text-align: right;">
                                                                        $<%# DataBinder.Eval(Container,"DataItem.PAID_AMOUNT" ) %>
                                                                    </td>
                                                                    <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82; text-align: right;">
                                                                        $<%# DataBinder.Eval(Container,"DataItem.FLT_BALANCE" )%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtcaseid" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtCompanyid" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
