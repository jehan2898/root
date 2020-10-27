<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleAppointment.aspx.cs" Inherits="AJAX_Pages_ScheduleAppointment" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../validation.js"></script>

    <script type="text/javascript" src="../Registration/validation.js"></script>

    <script type="text/javascript">
        var postponedCallbackRequired = false;
        var postponedUpdateSearchCallbackRequired = false;

        function DisplayControl() {
            document.getElementById('contentSearch').style.visibility = 'visible';
            document.getElementById('griddiv').style.visibility = 'hidden';
            document.getElementById('searchbuttondiv').style.visibility = 'hidden';
            document.getElementById('txtPatientFName').value = '';
            document.getElementById('txtPatientLName').value = '';
        }
        function HideControl() {
            document.getElementById('contentSearch').style.visibility = 'hidden';
            document.getElementById('griddiv').style.visibility = 'hidden';
            document.getElementById('searchbuttondiv').style.visibility = 'visible';
        }
        function openExistsPage() {
            document.getElementById('pLoadPatient_divDisplayMsg').style.zIndex = 1;
            document.getElementById('pLoadPatient_divDisplayMsg').style.position = 'absolute';
            document.getElementById('pLoadPatient_divDisplayMsg').style.left = '300px';
            document.getElementById('pLoadPatient_divDisplayMsg').style.top = '200px';
            document.getElementById('pLoadPatient_divDisplayMsg').style.visibility = 'visible';
            return false;
        }
        function val_updateTestFacility() {
            if (document.getElementById('txtPatientFName').value == '') {
                alert('Patient First Name should not be Empty.');
                document.getElementById('txtPatientFName').focus();
                return false;
            }
            if (document.getElementById('txtPatientLName').value == '') {
                alert('Patient Last name should not be Empty.');
                document.getElementById('txtPatientLName').focus();
                return false;
            }
            if (document.getElementById('extddlDoctor').value == 'NA') {
                alert('Please Select any one Referring Doctor.');
                document.getElementById('extddlDoctor').focus();
                return false;
            }
            if (document.getElementById('chkTransportation').checked == true) {
                if (document.getElementById('extddlTransport').value == 'NA') {
                    alert('Please Select any one Transport company.');
                    document.getElementById('extddlTransport').focus();
                    return false;
                }
            }
        }
        function SearchPatient() {
            var firstName = document.getElementById('txtPatientFirstName');
            var lastName = document.getElementById('txtPatientLastName');
            document.getElementById('pUpdateSearch_hdnPatientFirstName').value = firstName.value;
            document.getElementById('pUpdateSearch_hdnPatientLastName').value = lastName.value;
            pUpdateSearch.PerformCallback();
        }
        function LoadPatient(PatientID) {
            document.getElementById('pLoadPatient_hdnPatientID').value = PatientID;
            pLoadPatient.PerformCallback();
        }
        var lastOffice = null;
        function OnMedicalOfficeChanged(cmbMedicalOffice) {
            if (cmbReferringDoctor.InCallback())
                lastOffice = cmbMedicalOffice.GetValue().toString();
            else {
                cmbReferringDoctor.PerformCallback(cmbMedicalOffice.GetValue().toString());
            }
        }
        function chkTransportation(s, e) {
            if (cmbTransport.InCallback()) {
                lastOffice = cmbMedicalOffice.GetValue().toString();
            }
            else {
                cmbTransport.PerformCallback();
            }
        }
        function SaveVisit(s, e) {
            document.getElementById('pLoadPatient_hdnOperation').value = 'save';
            if (pLoadPatient.InCallback())
                postponedCallbackRequired = true;
            else
                pLoadPatient.PerformCallback();
        }
        function UpdateVisit(s, e) {
            document.getElementById('pLoadPatient_hdnOperation').value = 'update';
            if (pLoadPatient.InCallback())
                postponedCallbackRequired = true;
            else
                pLoadPatient.PerformCallback();
        }
        function FunctionValidationDelete(s, e) {
            document.getElementById('pLoadPatient_hdnOperation').value = 'delete';
            if (confirm('Do you want to delete appointment?')) {
                if (pLoadPatient.InCallback())
                    postponedCallbackRequired = true;
                else
                    pLoadPatient.PerformCallback();
                return true;
            }
            else
                return false;
        }
        function onUpdateSearchEnd(s, e) {
            if (cbpData_postponedCallbackRequired) {
                cbpData.PerformCallback();
                cbpData_postponedCallbackRequired = false;
            }
        }
        function onUpdateSearchEnd(s, e) {
            if (postponedUpdateSearchCallbackRequired) {
                pUpdateSearch.PerformCallback();
                postponedUpdateSearchCallbackRequired = false;
            }
        }
        function OnEndLoadingPanelCallback(s, e) {
            if (document.getElementById('pLoadPatient_hdnReturnOpration').value != '') {
                if (document.getElementById('pLoadPatient_hdnReturnOpration').value == 'refresh') {
                    if (document.getElementById('pLoadPatient_hdnReturnPath').value != '') {
                        alert('Appointment saved successfully.');
                        window.parent.document.location.href = window.parent.document.location.href;
                        window.self.close();
                        window.top.parent.location = document.getElementById('pLoadPatient_hdnReturnPath').value;
                    }
                }
                else if (document.getElementById('pLoadPatient_hdnReturnOpration').value == 'check permission') {
                    openExistsPage();
                }
            }
            LoadError(pLoadPatient_hdnErrorMsg.value);
            if (postponedCallbackRequired) {
                pLoadPatient.PerformCallback();
                postponedCallbackRequired = false;
            }
        }
        function LoadError(msg) {
            pUserMessege_hdnUserMessege.value = pLoadPatient_hdnErrorMsg.value;
            pLoadPatient_hdnErrorMsg.value = '';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
      
    <div>
        <table>
            <tr>
                <td style="height: 19px" colspan="6">
                    <dx:ASPxCallbackPanel ID="pUserMessege" runat="server" 
                        ClientInstanceName="pUserMessege" OnCallback="onUserMessege_CallBack" 
                        meta:resourcekey="pUserMessegeResource1">
                        <PanelCollection>
                            <dx:PanelContent meta:resourcekey="PanelContentResource1">
                                <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
                                <asp:HiddenField id="hdnUserMessege" runat="server" />
                            </dx:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="onUpdateSearchEnd" />
                    </dx:ASPxCallbackPanel>
                </td>
            </tr>
            <tr runat="server" visible="false" id="trReminder">
                <td style="height: 19px" colspan="6">
                    <asp:Button ID="btnReminder" Text="Send Reminder" runat="server" OnClick="btnReminder_Click" />
                </td>
            </tr>
            <tr id="tdSerach" runat="server">
                <td class="ContentLabel" valign="top" align="center" colspan="6">
                    <table class="ContentTable" width="100%">
                        <tbody>
                            <tr>
                                <td style="text-align: left" class="ContentLabel" width="100%" colspan="6">
                                    <div style="left: 0px; float: left; visibility: visible; position: relative; top: 0px"
                                        id="searchbuttondiv">
                                        <input id="btnClickSearch" class="Buttons" onclick="DisplayControl()" type="button"
                                            value="Search" runat="server" />
                                        <asp:Button ID="btnAddPatient" class="Buttons" OnClick="btnAddPatient_Click" runat="server"
                                            Text="Add Patient" meta:resourcekey="btnAddPatientResource1"></asp:Button>
                                    </div>
                                    <div style="left: 0px; float: left; visibility: hidden; position: relative; top: 0px"
                                        id="contentSearch">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFirstname" runat="server" Font-Names="Verdana" 
                                                        Font-Size="12px" meta:resourcekey="lblFirstnameResource1">First Name :</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPatientFirstName" runat="server" Width="120px" 
                                                        meta:resourcekey="txtPatientFirstNameResource1"></asp:TextBox>
                                                </td>
                                                    
                                                <td>
                                                    <asp:Label ID="lblLastname" runat="server" Font-Names="Verdana" 
                                                        Font-Size="12px" meta:resourcekey="lblLastnameResource1">Last Name :</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPatientLastName" runat="server" Width="120px" 
                                                        meta:resourcekey="txtPatientLastNameResource1"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton 
                                                        ID="btnSearchPatientCallBack" 
                                                        AutoPostBack="False"
                                                        runat="server"
                                                        CssClass="Buttons" Text="Search" 
                                                        meta:resourcekey="btnSearchPatientCallBackResource1">
                                                        <ClientSideEvents Click="SearchPatient" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <input id="btnPatientCancel" class="Buttons" onclick="HideControl()" type="button"
                                                        value="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
            <dx:ASPxCallbackPanel ID="pUpdateSearch" runat="server" 
            ClientInstanceName="pUpdateSearch" OnCallback="onUpdateSearch_CallBack" 
            meta:resourcekey="pUpdateSearchResource1">
                <PanelCollection>
                    <dx:PanelContent meta:resourcekey="PanelContentResource2">
                        <table class="ContentTable" width="100%">
                            <tbody>
                                <tr>
                                    <td style="height: 20px; text-align: left" class="ContentLabel">
                                        <div style="left: 0px; float: left; position: relative; top: 0px" id="griddiv">
                                            <dx:ASPxGridView 
                                                runat="server" 
                                                ID="grdPatientList_" 
                                                AutoGenerateColumns="false" Width="100%" Visible="false" 
                                                meta:resourcekey="grdPatientList_Resource1">
                                                <Columns>
                                                    <dx:GridViewDataHyperLinkColumn 
                                                        FieldName="SZ_PATIENT_ID"
                                                        Caption=" " Width="60px" 
                                                        meta:resourcekey="GridViewDataColumnResource1">
                                                        <DataItemTemplate>
                                                            <a href="javascript:LoadPatient('<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ID") %>');">Select</a>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataHyperLinkColumn>

                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Visible="false" 
                                                        meta:resourcekey="GridViewDataColumnResource2" ></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_FIRST_NAME" Caption="First Name" 
                                                        meta:resourcekey="GridViewDataColumnResource3" ></dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_LAST_NAME" Caption="Last Name" 
                                                        meta:resourcekey="GridViewDataColumnResource4"></dx:GridViewDataColumn>
                                               </Columns>

                                                <Settings 
                                                    ShowVerticalScrollBar="true" 
                                                    VerticalScrollBarStyle="Standard" 
                                                    VerticalScrollableHeight="120" />
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                            </dx:ASPxGridView>
                                           <%-- <asp:DataGrid ID="grdPatientList" runat="server" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" OnSelectedIndexChanged="grdPatientList_SelectedIndexChanged"
                                                OnPageIndexChanged="grdPatientList_PageIndexChanged" PagerStyle-Mode="NumericPages"
                                                PageSize="3" AllowPaging="true" meta:resourcekey="grdPatientListResource1">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:ButtonColumn CommandName="Select" Text="Select" 
                                                        meta:resourcekey="ButtonColumnResource1"></asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_FIRST_NAME" HeaderText="First Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_LAST_NAME" HeaderText="Last Name"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="I_PATIENT_AGE" HeaderText="Age" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="Address" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_STREET" HeaderText="Street" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_CITY" HeaderText="City" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ZIP" HeaderText="Zip" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Phone" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_EMAIL" HeaderText="Email" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MI" HeaderText="MI" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_WCB_NO" HeaderText="WCB" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_SOCIAL_SECURITY_NO" HeaderText="Social Security No"
                                                        Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DT_DOB" HeaderText="Date Of Birth" DataFormatString="{0:MM/dd/yyyy}"
                                                        Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_GENDER" HeaderText="Gender" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DT_INJURY" HeaderText="Date of Injury" DataFormatString="{0:MM/dd/yyyy}"
                                                        Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_JOB_TITLE" HeaderText="Job Title" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_WORK_ACTIVITIES" HeaderText="Work Activities" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_STATE" HeaderText="State" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_CARRIER_CASE_NO" HeaderText="Carrier Case No" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Employer Name" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_EMPLOYER_PHONE" HeaderText="Employer Phone" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_EMPLOYER_ADDRESS" HeaderText="Employer Address" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_EMPLOYER_CITY" HeaderText="Employer City" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_EMPLOYER_STATE" HeaderText="Employer State" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_EMPLOYER_ZIP" HeaderText="Employer Zip" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_WORK_PHONE" HeaderText="WORK_PHONE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_WORK_PHONE_EXTENSION" HeaderText="WORK_PHONE" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BT_WRONG_PHONE" HeaderText="WRONG_PHONE" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="BT_TRANSPORTATION" HeaderText="TRANSPORTATION" Visible="False">
                                                    </asp:BoundColumn>
                                                    
                                                </Columns>
                                                <PagerStyle Mode="NumericPages" />
                                                 
                                            </asp:DataGrid>--%>
                                            
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <asp:HiddenField ID="hdnPatientFirstName" runat="server" />
                        <asp:HiddenField ID="hdnPatientLastName" runat="server" />
                    </dx:PanelContent>
                </PanelCollection>
                <ClientSideEvents EndCallback="onUpdateSearchEnd" />
            </dx:ASPxCallbackPanel>


             <dx:ASPxCallbackPanel 
                ID="pLoadPatient" 
                runat="server" 
                ClientInstanceName="pLoadPatient" 
                OnCallback="onLoadPatient_CallBack" 
                meta:resourcekey="pLoadPatientResource1">
                        <PanelCollection>
                            <dx:PanelContent meta:resourcekey="PanelContentResource3">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td style="height: 20px; text-align: left" class="ContentLabel" colspan="6">
                                                        <asp:Label ID="lblMsg2" runat="server" Visible="false" CssClass="message-text" 
                                                            meta:resourcekey="lblMsg2Resource1"></asp:Label>
                                                        <div style="color: red" id="Div1" visible="true">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 20px; text-align: left" class="ContentLabel" colspan="6">
                                                        <dx:ASPxLabel ID="lblMsg" runat="server" Visible="false" CssClass="message-text" Font-Size="Medium"  ForeColor="Red"
                                                            meta:resourcekey="lblMsgResource1"></dx:ASPxLabel>
                                                        <div style="color: red" id="ErrorDiv" visible="true">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 185px" align="left">
                                                        <asp:Label ID="lblChartNumber" runat="server" Text="Chart Number" Font-Names="Verdana"
                                                            Font-Size="12px" meta:resourcekey="lblChartNumberResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 201px" align="left">
                                                        <asp:TextBox ID="txtRefChartNumber" onkeypress="return CheckForInteger(event,'/')"
                                                            runat="server" CssClass="cinput" ReadOnly="true" 
                                                            meta:resourcekey="txtRefChartNumberResource1"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:LinkButton ID="lnkPatientDesk" OnClick="lnkPatientDesk_Click" runat="server"
                                                            Text='<img src="Images/clients_icon.png" style="border:none;width:25px;height:25px;"/>'
                                                            ToolTip="Patient Desk" meta:resourcekey="lnkPatientDeskResource1"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 185px" align="left">
                                                        First name</td>
                                                    <td style="width: 201px" align="left">
                                                        <dx:ASPxTextBox ID="txtPatientFName" runat="server" Width="100%" ReadOnly="true" 
                                                            meta:resourcekey="txtPatientFNameResource1"></dx:ASPxTextBox>
                                                        <%--<asp:TextBox ID="txtPatientFName" runat="server" ReadOnly="true"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="width: 20%" align="left">
                                                        Middle
                                                    </td>
                                                    <td align="left" width="20%">
                                                        <%--<asp:TextBox ID="txtMI" runat="server" MaxLength="2"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtMI" runat="server" MaxLength="2" Width="100%"
                                                            meta:resourcekey="txtMIResource1"></dx:ASPxTextBox>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        Last name
                                                    </td>
                                                    <td style="width: 20%" align="left">
                                                        <%--<asp:TextBox ID="txtPatientLName" runat="server" ReadOnly="true"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtPatientLName" runat="server" ReadOnly="true" Width="100%"
                                                            meta:resourcekey="txtPatientLNameResource1"></dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 185px" align="left">
                                                        Phone</td>
                                                    <td style="width: 201px" align="left">
                                                        <%--<asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox></td>--%>
                                                        <dx:ASPxTextBox ID="txtPatientPhone" runat="server" Width="100%"
                                                            meta:resourcekey="txtPatientPhoneResource1"></dx:ASPxTextBox></td>
                                                    <td style="width: 20%" align="left">
                                                        Address</td>
                                                    <td align="left">
                                                        <%--<asp:TextBox ID="txtPatientAddress" runat="server"></asp:TextBox></td>--%>
                                                        <dx:ASPxTextBox ID="txtPatientAddress" runat="server" Width="100%"
                                                            meta:resourcekey="txtPatientAddressResource1"></dx:ASPxTextBox></td>
                                                    <td align="left" width="10%">
                                                        City</td>
                                                    <td style="width: 20%" align="left">
                                                        <%--<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>--%>
                                                        <dx:ASPxTextBox ID="TextBox3" runat="server" Width="100%"
                                                            meta:resourcekey="TextBox3Resource1"></dx:ASPxTextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 185px" align="left">
                                                        State</td>
                                                    <td style="width: 201px" align="left">
                                                        <asp:TextBox ID="txtState" runat="server" Visible="false" 
                                                            meta:resourcekey="txtStateResource1"></asp:TextBox>
                                                        <%--<extddl:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="150px"
                                                            Connection_Key="Connection_String" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                            Selected_Text="--- Select ---" Visible="false"></extddl:ExtendedDropDownList>--%>
                                                        <dx:ASPxComboBox ID="cmbState" runat="server" ClientInstanceName="cmbState" 
                                                            meta:resourcekey="cmbStateResource1">
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td style="width: 20%" align="left">
                                                        Insurance</td>
                                                    <td align="left" width="10%">
                                                        <%--<extddl:ExtendedDropDownList ID="extddlInsuranceCompany" runat="server" Width="150px"
                                                            Connection_Key="Connection_String" Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                            Selected_Text="--- Select ---" Visible="false"></extddl:ExtendedDropDownList>--%>
                                                        <dx:ASPxComboBox ID="cmbInsurance" runat="server" 
                                                            ClientInstanceName="cmbInsurance" Enabled="false" 
                                                            meta:resourcekey="cmbInsuranceResource1">
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                        </dx:ASPxComboBox>
                                                        &nbsp;
                                                        <%--<asp:ImageButton ID="imgbtnBirthdate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <cc1:CalendarExtender ID="calBirthdate" runat="server" PopupButtonID="imgbtnBirthdate"
                                                        TargetControlID="txtBirthdate" >
                                                    </cc1:CalendarExtender>--%>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        Case Type</td>
                                                    <td style="width: 20%" align="left">
                                                        <%--<extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="150px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="CASETYPE_LIST" Procedure_Name="SP_MST_CASE_TYPE" 
                                                            Selected_Text="---Select---" Visible="false">
                                                        </extddl:ExtendedDropDownList >
                                                        <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="150px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="CASESTATUS_LIST" Procedure_Name="SP_MST_CASE_STATUS" Selected_Text="--- Select ---"
                                                        Visible="false" Enabled="False" Flag_ID="txtCompanyID.Text.ToString();"></extddl:ExtendedDropDownList>--%>

                                                        <dx:ASPxComboBox ID="cmbCaseType" runat="server" 
                                                            ClientInstanceName="cmbCaseType" Enabled="false" 
                                                            meta:resourcekey="cmbCaseTypeResource1">
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                        </dx:ASPxComboBox>
                                                        <dx:ASPxComboBox ID="cmbCaseStatus" runat="server" 
                                                            ClientInstanceName="cmbCaseStatus" Visible="false" Enabled="false" 
                                                            meta:resourcekey="cmbCaseStatusResource1">
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                        </dx:ASPxComboBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 185px" align="left">
                                                        <asp:Label ID="lblSSN" runat="server" Text="SSN #" Font-Names="Verdana" 
                                                            Font-Size="12px" Visible="false" meta:resourcekey="lblSSNResource1"></asp:Label></td>
                                                    <td style="width: 201px" align="left">
                                                        <asp:TextBox ID="txtSocialSecurityNumber" runat="server" Enabled="False" 
                                                            Visible="false" meta:resourcekey="txtSocialSecurityNumberResource1"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%" align="left">
                                                        <asp:Label ID="lblBirthdate" runat="server" Text="Birthdate" Font-Names="Verdana"
                                                            Font-Size="12px" Visible="false" meta:resourcekey="lblBirthdateResource1"></asp:Label>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox ID="txtBirthdate" runat="server" Enabled="False" Visible="false" 
                                                            meta:resourcekey="txtBirthdateResource1"></asp:TextBox>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:Label ID="lblAge" runat="server" Text="Age" Font-Names="Verdana" 
                                                            Font-Size="12px" Visible="false" meta:resourcekey="lblAgeResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%" align="left">
                                                        <asp:TextBox ID="txtPatientAge" runat="server" Enabled="False" Visible="false" 
                                                            meta:resourcekey="txtPatientAgeResource1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 185px; height: 31px" align="left">
                                                        <asp:Label ID="lblTestFacility" runat="server" Text="Test Facility" Font-Names="Verdana"
                                                            Font-Size="12px" meta:resourcekey="lblTestFacilityResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 201px; height: 31px" align="left">
                                                        <%--<extddl:ExtendedDropDownList ID="extddlMedicalOffice" runat="server" Width="150px"
                                                            Connection_Key="Connection_String" Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE"
                                                            Selected_Text="--- Select ---" Visible="false" OnextendDropDown_SelectedIndexChanged="extddlMedicalOffice_extendDropDown_SelectedIndexChanged"
                                                            AutoPost_back="true"></extddl:ExtendedDropDownList>
                                                        <extddl:ExtendedDropDownList ID="extddlReferenceFacility" runat="server" Width="150px"
                                                            Connection_Key="Connection_String" 
                                                            Flag_Key_Value="REFERRING_FACILITY_LIST" Procedure_Name="SP_TXN_REFERRING_FACILITY"
                                                            Selected_Text="--- Select ---" Visible="false" ></extddl:ExtendedDropDownList>--%>

                                                        <dx:ASPxComboBox ID="cmbMedicalOffice" runat="server" 
                                                            ClientInstanceName="cmbMedicalOffice" Visible="false" 
                                                            meta:resourcekey="cmbMedicalOfficeResource1">
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnMedicalOfficeChanged(s); }" />
                                                        </dx:ASPxComboBox>
                                                        <dx:ASPxComboBox ID="cmbReferringFacility" runat="server" 
                                                            ClientInstanceName="cmbReferringFacility" Enabled="false" 
                                                            meta:resourcekey="cmbReferringFacilityResource1">
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td style="width: 10%; height: 31px" align="left">
                                                        Referring Doctor
                                                    </td>
                                                    <td style="width: 10%; height: 31px" align="left" colspan="2">
                                                        <%--<extddl:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="100%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="newGETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" 
                                                            Selected_Text="---Select---" Visible="false">
                                                        </extddl:ExtendedDropDownList>--%>
                                                        <dx:ASPxComboBox ID="cmbReferringDoctor" runat="server" 
                                                            ClientInstanceName="cmbReferringDoctor" 
                                                            OnCallback="cmbReferringDoctor_Callback" 
                                                            meta:resourcekey="cmbReferringDoctorResource1">
                                                            <Items>
                                                                <dx:ListEditItem Text="---Select---" Value="NA" 
                                                                    meta:resourcekey="ListEditItemResource1" />
                                                            </Items>
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <%-- <td style="height: 31px" valign="top" align="left" width="10%">
                                                        </td>--%>
                                                    <td style="width: 20%; height: 31px" align="left">
                                                        Transport &nbsp;
                                                        <%--<asp:CheckBox ID="chkTransportation" runat="server" AutoPostBack="true" Text="" OnCheckedChanged="chkTransportation_CheckedChanged"
                                                            TextAlign="Left"></asp:CheckBox>--%>
                                                        <dx:ASPxCheckBox ID="chkTransportation" runat="server" Text="" 
                                                            meta:resourcekey="chkTransportationResource1">
                                                            <ClientSideEvents CheckedChanged="function(s, e) { chkTransportation(); }" />
                                                        </dx:ASPxCheckBox>
                                                        <%--<extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="110px" Connection_Key="Connection_String"
                                                            Flag_Key_Value="GET_TRANSPORT_LIST" Procedure_Name="SP_MST_TRANSPOTATION" Selected_Text="---Select---"
                                                            Visible="false"></extddl:ExtendedDropDownList>--%>
                                                            <dx:ASPxComboBox ID="cmbTransport" runat="server" 
                                                            ClientInstanceName="cmbTransport" OnCallback="cmbTransport_Callback" 
                                                            meta:resourcekey="cmbTransportResource1">
                                                            <ItemStyle>
                                                                <HoverStyle BackColor="#F6F6F6">
                                                                </HoverStyle>
                                                            </ItemStyle>
                                                        </dx:ASPxComboBox>
                                                        &nbsp;&nbsp;
                                                        <%--<asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label>--%>
                                                        <%-- <asp:TextBox ID="txtDoctorName" runat="server" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:HiddenField ID="hdnPatientID" runat="server" />
                                            <asp:HiddenField ID="hdnReturnOpration" runat="server" />
                                            <asp:HiddenField ID="hdnReturnPath" runat="server" />
                                            <asp:HiddenField ID="hdnCaseID" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" valign="top" align="left">
                                            Procedure
                                        </td>
                                        <td style="text-align: left" align="left" width="90%">
                                            <asp:ListBox ID="ddlTestNames" runat="server" Width="350px" Visible="false" CssClass="s"
                                                Rows="1" SelectionMode="Multiple" meta:resourcekey="ddlTestNamesResource1"></asp:ListBox>
                                            <div style="overflow: scroll; width: 100%; height: 200px" id="divProcedureCode" runat="server"
                                                visible="true">
                                                <asp:DataGrid ID="grdProcedureCode" runat="server" Width="95%" CssClass="GridTable"
                                                    AutoGenerateColumns="false" PagerStyle-Mode="NumericPages" PageSize="3" 
                                                    OnItemDataBound="grdProcedureCode_ItemDataBound" 
                                                    meta:resourcekey="grdProcedureCodeResource1">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        <%--0--%>
                                                        <asp:TemplateColumn>
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" 
                                                                    meta:resourcekey="chkSelectResource1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--1--%>
                                                        <asp:BoundColumn DataField="code" HeaderText="Patient ID" Visible="False" ItemStyle-Width="5%">
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <%--2--%>
                                                        <asp:BoundColumn DataField="description" HeaderText="Procedure" ItemStyle-Width="200px"
                                                            ItemStyle-HorizontalAlign="left">
                                                        <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <%--3--%>
                                                        <asp:TemplateColumn HeaderText="Status">
                                                            <ItemTemplate>
                                                                <itemstyle width="5%" />
                                                                <asp:DropDownList ID="ddlStatus" runat="server" 
                                                                    meta:resourcekey="ddlStatusResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource2">Re-Schedule</asp:ListItem>
                                                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource3">Visit Completed</asp:ListItem>
                                                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource4">No Show</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--4--%>
                                                        <asp:TemplateColumn HeaderText="Re-Schedule Date">
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReScheduleDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                    Width="70px" meta:resourcekey="txtReScheduleDateResource1"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--5--%>
                                                        <asp:TemplateColumn HeaderText="Re-Schedule Time">
                                                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlReSchHours" runat="server" Width="40px" 
                                                                    meta:resourcekey="ddlReSchHoursResource1">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlReSchMinutes" runat="server" Width="40px" 
                                                                    meta:resourcekey="ddlReSchMinutesResource1">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlReSchTime" runat="server" Width="40px" 
                                                                    meta:resourcekey="ddlReSchTimeResource1">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--6--%>
                                                        <asp:TemplateColumn HeaderText="Study No.">
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtStudyNo" runat="server" Width="80px" 
                                                                    meta:resourcekey="txtStudyNoResource1"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--7--%>
                                                        <asp:BoundColumn DataField="I_RESCHEDULE_ID" HeaderText="I_RESCHEDULE_ID" Visible="False">
                                                        </asp:BoundColumn>
                                                        <%--8--%>
                                                        <asp:BoundColumn DataField="I_EVENT_PROC_ID" HeaderText="I_EVENT_PROC_ID" Visible="False">
                                                        </asp:BoundColumn>
                                                        <%--9--%>
                                                        <asp:TemplateColumn HeaderText="Notes">
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcNotes" runat="server" Width="80px" 
                                                                    meta:resourcekey="txtProcNotesResource1"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--10--%>
                                                        <asp:TemplateColumn HeaderText="Notes" Visible="false">
                                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcBillStatus" runat="server" Width="80px" 
                                                                    meta:resourcekey="txtProcBillStatusResource1"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <%--<td width="10%">
                                        </td>
                                    <td width="10%">
                                        &nbsp; <asp:DropDownList ID="ddlTestNames" runat="server" Width="150px" Visible="false" >
                                        </asp:DropDownList>--%>
                                    </tr>
                                    <tr>
                                        <td style="width: 185px" align="left">
                                            Notes
                                        </td>
                                        <td style="text-align: left" align="left">
                                            &nbsp;<asp:TextBox ID="txtNotes" runat="server" Width="100%" Height="50px"
                                                TextMode="MultiLine" meta:resourcekey="txtNotesResource1"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 26px" align="left" colspan="2">
                                            <asp:DropDownList ID="ddlHours" runat="server" Width="10px" Visible="False" 
                                                meta:resourcekey="ddlHoursResource1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlMinutes" runat="server" Width="10px" Visible="False" 
                                                meta:resourcekey="ddlMinutesResource1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTime" runat="server" Width="10px" Visible="False" 
                                                meta:resourcekey="ddlTimeResource1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlEndHours" runat="server" Width="10px" Visible="False" 
                                                meta:resourcekey="ddlEndHoursResource1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlEndMinutes" runat="server" Width="10px" 
                                                Visible="False" meta:resourcekey="ddlEndMinutesResource1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlEndTime" runat="server" Width="10px" Visible="False" 
                                                meta:resourcekey="ddlEndTimeResource1">
                                            </asp:DropDownList>
                                            <%--<asp:TextBox id="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>--%>
                                            
                    
                                            &nbsp;&nbsp;
                                            
                                            &nbsp;
                                            &nbsp;&nbsp;&nbsp;
                                            
                                            &nbsp;
                                            &nbsp;
                                            
                    
                                            
                                            &nbsp;&nbsp;&nbsp;
                                            &nbsp;
                                            
                    
                                            <%--<asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Width="62px"
                                                Visible="false" CssClass="Buttons" Text="Update" OnClientClick="javascript:return FunctionValidationUpdate();">
                                            </asp:Button>--%>
                                            &nbsp;&nbsp;&nbsp;
                                            <%--<asp:Button ID="btnDeleteEvent" OnClick="btnDeleteEvent_Click" runat="server" Width="62px"
                                                Visible="false" CssClass="Buttons" Text="Delete" OnClientClick="javascript:return FunctionValidationDelete();">
                                            </asp:Button>--%>&nbsp;
                                            <%--<asp:Button ID="btnDuplicateSaveClick" OnClick="btnSave_Click"  runat="server" Width="62px" 
                                                CssClass="Buttons" Text="Save"></asp:Button>--%>
                    
                                            <asp:HiddenField ID="hdnOperation" runat="server" />
                                            <asp:HiddenField ID="hdnErrorMsg" runat="server" />
                                            <%--<asp:Button ID="btnSaveTemp"  runat="server" Width="62px" onclick="btnSaveTemp_Click" Text="Save" CssClass="Buttons"></asp:Button>--%>
                                            <%--<asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="62px"
                                                CssClass="Buttons" Text="Cancel" meta:resourcekey="btnCancelResource1"></asp:Button>&nbsp;--%>
                                                <%--<INPUT id="Button1" class="Buttons" onclick="javascript:parent.document.getElementById('divid').style.visibility = 'hidden';javascript:parent.document.getElementById('frameeditexpanse').src = '';" type=button value="Cancel" />--%>&nbsp;
                                            <asp:Button Style="display: none" ID="btnSave" OnClick="btnSave_Click" runat="server"
                                                Width="62px" CssClass="Buttons" Text="Save" 
                                                meta:resourcekey="btnSaveResource1"></asp:Button>
                                            <asp:Label ID="Label11" runat="server" Visible="False" Text="Study #" Font-Names="Verdana"
                                                Font-Size="12px" meta:resourcekey="Label11Resource1"></asp:Label>
                                            <asp:Label ID="lblTypetext" runat="server" Visible="False" Text="Type" Font-Names="Verdana"
                                                Font-Size="12px" meta:resourcekey="lblTypetextResource1"></asp:Label>
                                            <asp:DropDownList ID="ddlType" runat="server" Width="10px" Visible="false" 
                                                OnSelectedIndexChanged="ddlType_SelectedIndexChanged" 
                                                meta:resourcekey="ddlTypeResource1">
                                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource5"> --Select--</asp:ListItem>
                                                <asp:ListItem Value="TY000000000000000001" meta:resourcekey="ListItemResource6">Visit</asp:ListItem>
                                                <asp:ListItem Value="TY000000000000000002" meta:resourcekey="ListItemResource7">Treatment</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="TY000000000000000003" 
                                                    meta:resourcekey="ListItemResource8">Test</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtStudyNumber" runat="server" Width="2px" Visible="false" 
                                                meta:resourcekey="txtStudyNumberResource1"></asp:TextBox>
                                            <asp:HiddenField ID="txtPatientExistMsg" runat="server"></asp:HiddenField>
                                            <asp:TextBox ID="txtPatientCompany" runat="server" Width="5px" Visible="False" 
                                                meta:resourcekey="txtPatientCompanyResource1"></asp:TextBox>
                                            <asp:FileUpload ID="flUpload" runat="server" Width="10px" Visible="false" 
                                                meta:resourcekey="flUploadResource1"></asp:FileUpload>
                                            <asp:Button ID="btnLoadPageData" OnClick="btnLoadPageData_Click" runat="server" Width="62px"
                                                Visible="false" CssClass="Buttons" Text="Delete" 
                                                meta:resourcekey="btnLoadPageDataResource1"></asp:Button>&nbsp;
                                        </td>
                                    </tr>
                               
                                </table>
                                   <div style="border-right: silver 2px solid; border-top: silver 2px solid; z-index: 1500;
                                        left: 428px; visibility: hidden; border-left: silver 2px solid; width: 300px;
                                        border-bottom: silver 2px solid; position: absolute; top: 920px; height: 150px;
                                        background-color: #FFFFFF; text-align: center" id="divDisplayMsg" runat="server">
                                        <div style="float: left; width: 40%; font-family: Times New Roman; position: relative;
                                            height: 20px; text-align: left">
                                            Msg
                                        </div>
                                        <div style="float: left; width: 60%; position: relative; height: 20px; 
                                            text-align: right">
                                            <a style="cursor: pointer" title="Close" onclick="CancelExistPatient();">X</a>
                                        </div>
                                        <br />
                                        <br />
                                        <div style="width: 231px; font-family: Times New Roman; top: 50px; text-align: center"  >
                                            <span style="font-weight: bold; font-size: 12px;" id="msgPatientExists" runat="server">
                                            </span>
                                        </div>
                                        <br />
                                        <div style="text-align: center">
                                            <asp:Button ID="btnPEOk" OnClick="btnPEOk_Click" runat="server" CssClass="Buttons"
                                                Text="Ok" meta:resourcekey="btnPEOkResource1"></asp:Button>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnPECancel" OnClick="btnPECancel_Click" runat="server" CssClass="Buttons"
                                                Text="Cancel" meta:resourcekey="btnPECancelResource1"></asp:Button>
                                        </div>
                                    </div>   
                            </dx:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="OnEndLoadingPanelCallback" />
            </dx:ASPxCallbackPanel>
            <table>
                <tr>
                    <td>
                        <dx:ASPxButton 
                        ID="btnDuplicateSaveClick" 
                        AutoPostBack="False"
                        runat="server"
                        CssClass="Buttons" Text="Save" 
                            meta:resourcekey="btnDuplicateSaveClickResource1" >
                        <ClientSideEvents Click="SaveVisit" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton 
                        ID="btnUpdate" 
                        AutoPostBack="False"
                        runat="server"
                        CssClass="Buttons" Text="Update" meta:resourcekey="btnUpdateResource1" >
                        <ClientSideEvents Click="UpdateVisit" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton 
                        ID="btnDeleteEvent" 
                        AutoPostBack="False"
                        runat="server"
                        CssClass="Buttons" Text="Delete" meta:resourcekey="btnDeleteEventResource1" >
                        <ClientSideEvents Click="FunctionValidationDelete" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton 
                        ID="btnCancel" 
                        runat="server"
                        CssClass="Buttons" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClick="btnCancel_Click" >
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>

        <asp:HiddenField ID="reffCompanyID" runat="server" />
        <asp:HiddenField ID="roomID" runat="server" />
        <asp:HiddenField ID="hdnOpration" runat="server" />
        <asp:HiddenField ID="reffOfficeID" runat="server" />
        <asp:HiddenField ID="hdnAppTime" runat="server" />
        <asp:HiddenField ID="hdnAppDate" runat="server" />
        <asp:HiddenField ID="hdnEventID" runat="server" />
        <asp:TextBox ID="txtCompanyID" Visible="False" runat="server" 
            meta:resourcekey="txtCompanyIDResource1"></asp:TextBox>
        <asp:TextBox ID="txtCaseID" Visible="False" runat="server" 
            meta:resourcekey="txtCaseIDResource1"></asp:TextBox>
        <asp:TextBox ID="txtPatientID" Visible="False" runat="server" 
            meta:resourcekey="txtPatientIDResource1"></asp:TextBox>
        <asp:TextBox ID="txtUserId" Visible="False" runat="server" 
            meta:resourcekey="txtUserIdResource1"></asp:TextBox>
       <%-- <asp:Button ID="btnLoadPatient" runat="server" OnClick="btnLoadPatient_Click" />--%>
    </div>
    </form>
</body>
</html>
