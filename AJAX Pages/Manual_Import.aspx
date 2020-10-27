<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Manual_Import.aspx.cs" Inherits="AJAX_Pages_Manual_Import" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <script type="text/javascript">
    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        background-color: White;">
        <tr>
            <td colspan="4" id="page-title" align="center">
                <dx:ASPxLabel ID="lblHead" runat="server" Text="Import Visit" Font-Bold="true" Font-Size="Medium" ></dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:updatepanel id="pnlUpdate" runat="server">
                    <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                </asp:updatepanel>
            </td>
        </tr>
        <tr>
        <td colspan="4"> &nbsp</td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Patient First Name
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientFirstName" Width="250px" 
                    Height="25px">
                </dx:ASPxTextBox>
               <asp:RequiredFieldValidator ID="reqdPatientFirstName"   runat="server" CssClass="invalid"
                        ControlToValidate="txtPatientFirstName"  ErrorMessage="Patient first name cannot be blank">Patient first name cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Patient Last Name
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientLastName" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
                   <asp:RequiredFieldValidator ID="reqdPatientLastName"   runat="server" CssClass="invalid"
                        ControlToValidate="txtPatientLastName"  ErrorMessage="Patient last name cannot be blank">Patient last name cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Patient Middle Name
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientMiddleName" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Patient Cliam Number
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtClaimNumber" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Patient ID
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientID" Width="250px" Height="25px" >
                </dx:ASPxTextBox>
                   <asp:RequiredFieldValidator ID="reqdPatientID"   runat="server" CssClass="invalid"
                        ControlToValidate="txtPatientID"  ErrorMessage="Patient id cannot be blank">Patient id cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Date Of Birth
            </td>
            <td>
                <dx:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" 
                    CssClass="inputBox" Width="250px"
                    ID="dtDOB" >
                </dx:ASPxDateEdit>
            </td>
        </tr>
        <tr>
            <td colspan="4">
              &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Patient Address
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientAddr" Width="250px" Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Patient Address2
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientAddr2" Width="250px" Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Gender
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbGender" runat="server" ClientInstanceName="cddGender" 
                    Width="250px" ValueType="System.String">
                    <Items>
                        <dx:ListEditItem Text="Male" Value="M"  />
                        <dx:ListEditItem Text="Female" Value="F" />
                    </Items>
                    <ItemStyle>
                        <HoverStyle BackColor="#F6F6F6">
                        </HoverStyle>
                    </ItemStyle>
                </dx:ASPxComboBox>
                <asp:RequiredFieldValidator ID="reqdGender"   runat="server" CssClass="invalid"
                        ControlToValidate="cmbGender"  ErrorMessage="Patient gender cannot be blank">Patient gender cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
            <td class="td-widget-lhr-import" align="left">
                SSNO
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientSSNO" Width="250px" Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Date Of Accident
            </td>
            <td>
                <dx:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" 
                    CssClass="inputBox" Width="250px"
                    ID="dtDateOfAcci" >
                </dx:ASPxDateEdit>
            </td>
            <td class="td-widget-lhr-import" align="left">
                State
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbState" runat="server" ClientInstanceName="cddState" 
                    Width="250px" ValueType="System.String">
                    <ItemStyle>
                        <HoverStyle BackColor="#F6F6F6">
                        </HoverStyle>
                    </ItemStyle>
                </dx:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Patient City
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientCity" Width="250px" Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Patient Zip
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientZipCode" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Patient Phone
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientPhone" Width="250px" Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Policy Holder Name
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPolicyHolder" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Case Type
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbBoxCaseType" runat="server" 
                    ClientInstanceName="cddBoxCaseType" Width="250px" 
                    ValueType="System.String">
                    <Items>
                        <dx:ListEditItem Text="NO-FAULT" Value="NF" />
                        <dx:ListEditItem Text="WORKERS CO" Value="WC"  />
                        <dx:ListEditItem Text="LIEN" Value="LI" />
                    </Items>
                    <ItemStyle>
                        <HoverStyle BackColor="#F6F6F6">
                        </HoverStyle>
                    </ItemStyle>
                </dx:ASPxComboBox>
                     <asp:RequiredFieldValidator ID="reqdCaseType"   runat="server" CssClass="invalid"
                        ControlToValidate="cmbBoxCaseType"  ErrorMessage="Case type cannot be blank">Case type cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
            <td class="td-widget-lhr-import" align="left">
                AppointmentID/CaseID
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtPatientAppoinmentID" Width="250px" 
                    Height="25px">
                </dx:ASPxTextBox>
                 <asp:RequiredFieldValidator ID="reqdAppoinmentID"   runat="server" CssClass="invalid"
                        ControlToValidate="txtPatientAppoinmentID"  ErrorMessage="Patient Appoinment ID cannot be blank">Patient Appoinment ID cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Date Of Appointment
            </td>
            <td>
                <dx:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" 
                    CssClass="inputBox" Width="250px"
                    ID="dtDateOfAppointment" >
                </dx:ASPxDateEdit>
                <asp:RequiredFieldValidator ID="reqdDtAppoinment"   runat="server" CssClass="invalid"
                        ControlToValidate="dtDateOfAppointment"  ErrorMessage="Date Of appointment cannot be blank">Date Of appointment cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Visit Time
            </td>
            <td>
                <dx:ASPxTimeEdit Width="250px" runat="server" ID="timeVisitTime"
                    EditFormatString="HH:mm" >
                </dx:ASPxTimeEdit>
             
                 <asp:Label ID="lblMsg" runat="server"  Text="[Please enter time in 24 hours]" 
                    Font-Italic="True" Font-Size="Small" ForeColor="#FF3300"></asp:Label>

                 <asp:RequiredFieldValidator ID="reqdTimeVisitTime"   runat="server" CssClass="invalid"
                        ControlToValidate="timeVisitTime"  ErrorMessage="Visit time cannot be blank">Visit time cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Insurance Name
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtInsuranceName" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Insurance Address
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtInsuranceAddress" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Insurance City
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtInsuranceCity" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Insurance State
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtInsuranceState" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Insurance Zip
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtInsuranceZip" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Insurance Policy Number
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtInsurancePolicyNumber" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Procedure Code
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtProcedureCode" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
                 <asp:RequiredFieldValidator ID="reqdProcedureCode"   runat="server" CssClass="invalid"
                        ControlToValidate="txtProcedureCode"  ErrorMessage="Procedure code cannot be blank">Procedure code cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Procedure Desc
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="txtProcedureDesc" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
                 <asp:RequiredFieldValidator ID="reqdProcedureDesc"   runat="server" CssClass="invalid"
                        ControlToValidate="txtProcedureDesc"  ErrorMessage="Procedure desc cannot be blank">Procedure desc cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Referring Doctor
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="ASPxTextBox7" Width="250px" 
                    Height="25px" >
                </dx:ASPxTextBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Reading Doctor
            </td>
            <td>
                <dx:ASPxTextBox runat="server" ID="ASPxTextBox8" Width="250px" 
                    Height="25px">
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                    &nbsp;
                        </td>
        </tr>
        <tr>
            <td class="td-widget-lhr-import" align="left">
                Book Facility
            </td>
            <td>
                <dx:ASPxComboBox ID="cmdBookFacility" runat="server" 
                    ClientInstanceName="cddBookFacility" Width="250px" 
                    ValueType="System.String">
                    <ItemStyle>
                        <HoverStyle BackColor="#F6F6F6">
                        </HoverStyle>
                    </ItemStyle>
                </dx:ASPxComboBox>
            </td>
            <td class="td-widget-lhr-import" align="left">
                Modality
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbModality" runat="server" 
                    ClientInstanceName="cddModality" Width="250px" 
                    ValueType="System.String">
                    <ItemStyle>
                        <HoverStyle BackColor="#F6F6F6">
                        </HoverStyle>
                    </ItemStyle>
                </dx:ASPxComboBox>
                 <asp:RequiredFieldValidator ID="reqdModality"   runat="server" CssClass="invalid"
                        ControlToValidate="cmbModality"  ErrorMessage="Modality cannot be blank">Modality cannot be blank
                    </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <dx:ASPxButton runat="server" Text="Import" ID="btnsave" AutoPostBack="true"
                    OnClick="btnsave_Click" Width="100px" Height="25px" >
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                    &nbsp;
            </td>
        </tr>
    </table>
</asp:content>