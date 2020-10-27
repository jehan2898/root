<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Nf2_Config_Setting.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Nf2_Config_Setting"
    Title="NF2 Config Setting" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Data" Assembly="DevExpress.Data.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<%@ Register Assembly="DevExpress.XtraCharts.v16.2.Web, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="dxcharts" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.PivotGrid.v16.2.Core, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraPivotGrid" TagPrefix="temp" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
     function GetInsuranceValue(object, arg) 
     {  
            document.getElementById('ctl00_ContentPlaceHolder1_hdninsurancecode').value = arg.get_value();
     }                                                                                  
     function validateCharLimit(area) 
    {
        var limit = 20;
        var iChars = area.value.length;
        if (iChars > limit) {
         alert('Only 20 characters are allowed !!!');
            return false;
        }
        return true;
    }
    </script>

    <table style="width: 100%; background-color: White">
        <tr>
            <td>
                <div style="overflow: scroll; height: 550px;">
                    <asp:UpdatePanel ID="upMain" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%; background-color: White">
                                <tr>
                                    <td valign="top">
                                        <table style="width: 100%; border-bottom: #b5df82 1px solid; border-right: #b5df82 1px solid;
                                            border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;">
                                            <tr>
                                                <td style="background-color: #b4dd82; height: 15%; font-weight: bold; font-size: small">
                                                    &nbsp;NF2 Setting
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upmsg" runat="server">
                                                        <contenttemplate>
                                                        <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                    </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Insurance Name: </b>
                                                    </td>
                                                    <td colspan="4">
                                                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                            DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany"
                                                            ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true"
                                                            ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                        <%--<dx:ASPxTextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="74%"
                                            OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true">
                                        </dx:ASPxTextBox>--%>
                                                        <asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="74%"
                                                            OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true" />
                                                        <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server"
                                                            Visible="false" Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                            Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                            OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged"
                                                            OldText="" StausText="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Insurance Address : </b>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:ListBox Width="75%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                            OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Address </b>
                                                    </td>
                                                    <td colspan="4">
                                                        <%--  <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                            ReadOnly="True"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                            ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>City : </b>
                                                    </td>
                                                    <td align="left" colspan="4" style="width: 35%">
                                                        <%--  <asp:TextBox ID="txtInsuranceCity" runat="server" ReadOnly="true">
                                        </asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtInsuranceCity" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>State : </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <%-- <asp:TextBox ID="txtInsuranceState" runat="server" ReadOnly="true">
                                        </asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtInsuranceState" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Zip : </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%--  <asp:TextBox ID="txtInsuranceZip" runat="server" ReadOnly="true"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtInsuranceZip" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Phone : </b>
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%--<asp:TextBox ID="txtInsPhone" runat="server" ReadOnly="true">
                                        </asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtInsPhone" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Fax : </b>&nbsp;
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <%--<asp:TextBox ID="txtInsFax" runat="server" ReadOnly="true"> </asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtInsFax" runat="server" ReadOnly="True">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Insurer Name </b>
                                                    </td>
                                                    <td align="left" style="width: 6%" colspan="3">
                                                        <%--<asp:TextBox ID="txtInsPhone" runat="server" ReadOnly="true">
                                        </asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtInsurername" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Address </b>
                                                    </td>
                                                    <td style="width: 6%" align="left" colspan="2">
                                                        <%--<asp:TextBox ID="txtInsFax" runat="server" ReadOnly="true"> </asp:TextBox>--%>
                                                        <dx:ASPxTextBox Width="99%" ID="txtInsureraddress" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>City </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtinsurercity" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>State </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <extddl:ExtendedDropDownList ID="extddlinsurerstate" runat="server" Width="90%" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                                            OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Zip </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtinsurerzip" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Insurer Phone </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtinsurerphone" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Claim No : </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxTextBox ID="txtClaimNumber" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Policy No : </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtPolicyNumber" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b></b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Date Of Accident</b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtdateofaccident" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Policy Holder : </b>
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtPolicyHolder" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b></b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>SSN # </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxTextBox ID="txtSocialSecurityNumber" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Gender </b>
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <asp:DropDownList ID="ddlSex" runat="server">
                                                            <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                                            <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>First name </b>&nbsp;
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxTextBox ID="txtPatientFName" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Middle </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtMI" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Last name </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtPatientLName" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtPatientLName" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Date of birth </b>&nbsp;
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtDateOfBirth" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Address </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%" colspan="3">
                                                        <dx:ASPxTextBox ID="txtPatientAddress" runat="server" Width="73%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>City </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtPatientCity" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>State </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtState" runat="server" Visible="False">
                                                        </dx:ASPxTextBox>
                                                        <extddl:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="90%" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                                            OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Zip </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtPatientZip" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Patient phone </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtpatientphone" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Home phone </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtHomePhone" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Work </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtWorkPhone" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Accident Address </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%--   <asp:TextBox ID="txtATAddress" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtATAddress" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>City </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%--<asp:TextBox ID="txtATCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtATCity" runat="server" CssClass="textboxCSS" MaxLength="50">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>State </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <extddl:ExtendedDropDownList ID="extddlATAccidentState" runat="server" Width="90%"
                                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                            Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Accident Brief Description </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%" colspan="3">
                                                        <%--<asp:TextBox ID="txtATCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtaccidentdesc" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Describe injury </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%" colspan="5">
                                                        <%--  <asp:TextBox ID="txtATDescribeInjury" runat="server"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtATDescribeInjury" runat="server" Width="90%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel" colspan="6">
                                                        <b>Identity of Vehicle You Occupied or Operated at the time of the Accident :</b>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Patient Policy Name</b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtpatientpolicyname" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Make</b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtmake" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Year</b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtyear" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>This Vehicle Was </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <asp:CheckBox ID="chkbusschool" runat="server" Text="&nbsp;<b>A Bus Or School Bus</b>" />
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <asp:CheckBox ID="chktruck" runat="server" Text="&nbsp;<b>A Truck</b>" />
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <asp:CheckBox ID="chkautomobile" runat="server" Text="&nbsp;<b>A Automobile</b>" />
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <asp:CheckBox ID="chkmotorcycle" runat="server" Text="&nbsp;<b>Or a Motorcycle</b>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2%" align="left" colspan="2">
                                                        <b>Were you the driver of the Motor Cycle?</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chkyesdrivermotor" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chknodrivermotor" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left" colspan="2">
                                                        <b>Were you a Passenger in the Motor Vehicle? </b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chkyespassengermotor" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chknopassengermotor" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left" colspan="2">
                                                        <b>Were you a Pedestrian? </b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chkyespedestrian" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chknopedestrian" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left" colspan="2">
                                                        <b>Were you a Member of our Policy Holder's HouseHold? </b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chkyespolicyholder" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chknopolicyholder" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left" colspan="2">
                                                        <b>Do You or a relative with whom you reside own a Motor Vehicle? </b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chkyesrelative" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        <asp:CheckBox ID="chknorelative" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2%" align="left" colspan="2">
                                                        <b>If your Were treated at a Hospital(s),Were you an :</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkoutpatient" runat="server" Text="&nbsp;<b>Out-Patient</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chkinpatient" runat="server" Text="&nbsp;<b>In-Patient</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Hospital name </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Hospital Address </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%--  <asp:TextBox ID="txtATHospitalAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtATHospitalAddress" runat="server" CssClass="textboxCSS" MaxLength="50">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Date of admission </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxDateEdit ID="txtATAdmissionDate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 6%">
                                                        <b>Amount of Health Bills TO Date : </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txthealthbills" runat="server" CssClass="textboxCSS" MaxLength="50">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%">
                                                        <b>Did You Lose Time from Work? </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <%-- <asp:TextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>--%>
                                                        <dx:ASPxTextBox ID="txtlosetime" runat="server" CssClass="textboxCSS" MaxLength="50">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%">
                                                        <b>Date Absence from work Begin </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxDateEdit ID="txtabsencedate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 6%">
                                                        <b>Have you return to Work: </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtreturnwork" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%">
                                                        <b>If Yes,Date returned to Work : </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxDateEdit ID="txtreturneddate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 6%">
                                                        <b>Amount of time lost from Work:</b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtlosttime" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 6%">
                                                        <b>What are your gross average weekly Earnings? </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtgrossaverage" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%">
                                                        <b>Number of Days you work per Week: </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtdaysperweek" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%">
                                                        <b>Number of Hours you work per Day:</b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txthrsperday" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Employer One Name </b>
                                                    </td>
                                                    <td align="left" style="width: 6%" colspan="3">
                                                        <%--  <dx:ASPxTextBox ID="txtemployerfirstname" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>--%>
                                                        <asp:TextBox ID="txtemployerfirstname" Width="91%" runat="server" onkeypress="return validateCharLimit(this);"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Address </b>
                                                    </td>
                                                    <td style="width: 6%" align="left" colspan="2">
                                                        <%--       <dx:ASPxTextBox Width="99%" ID="txtemployerfirstaddress" runat="server">
                                                            <ClientSideEvents KeyPress="validateCharLimit(this)" />
                                                        </dx:ASPxTextBox>--%>
                                                        <asp:TextBox ID="txtemployerfirstaddress" Width="99%" runat="server" onkeypress="return validateCharLimit(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>City </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtemployerfirstcity" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>State </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <extddl:ExtendedDropDownList ID="extddlemployerfirststate" runat="server" Width="90%"
                                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                            Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Employer Occupation </b>
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtemployerfirstoccu" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Employer From Date </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtemployerfirstfrmdate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Employer To Date </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtemployerfirsttodate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Employer Two Name </b>
                                                    </td>
                                                    <td align="left" style="width: 6%" colspan="3">
                                                        <%--<asp:TextBox ID="txtInsPhone" runat="server" ReadOnly="true">
                                        </asp:TextBox>--%>
                                                        <%--    <dx:ASPxTextBox ID="txtemployertwoname" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>--%>
                                                        <asp:TextBox ID="txtemployertwoname" Width="91%" runat="server" onkeypress="return validateCharLimit(this);"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Address </b>
                                                    </td>
                                                    <td style="width: 6%" align="left" colspan="2">
                                                        <%--<asp:TextBox ID="txtInsFax" runat="server" ReadOnly="true"> </asp:TextBox>--%>
                                                        <%--    <dx:ASPxTextBox Width="99%" ID="txtemployersecondadd" runat="server">
                                                        </dx:ASPxTextBox>--%>
                                                        <asp:TextBox ID="txtemployersecondadd" Width="99%" runat="server" onkeypress="return validateCharLimit(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>City </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtemployersecondcity" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>State </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <extddl:ExtendedDropDownList ID="extddlemployersecondstate" runat="server" Width="90%"
                                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                            Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Employer Occupation </b>
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtemployersecondoccu" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Employer From Date </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtemployersecondfrmdate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Employer To Date </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtemployersecondtodate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Employer Three Name </b>
                                                    </td>
                                                    <td align="left" style="width: 6%" colspan="3">
                                                        <%--<dx:ASPxTextBox ID="txtemployerthreename" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>--%>
                                                        <asp:TextBox ID="txtemployerthreename" Width="91%" runat="server" onkeypress="return validateCharLimit(this);"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Address </b>
                                                    </td>
                                                    <td style="width: 6%" align="left" colspan="2">
                                                        <%--      <dx:ASPxTextBox Width="99%" ID="txtemployerthreeaddress" runat="server">
                                                        </dx:ASPxTextBox>--%>
                                                        <asp:TextBox ID="txtemployerthreeaddress" Width="99%" runat="server" onkeypress="return validateCharLimit(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>City </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtemployerthreecity" runat="server">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>State </b>&nbsp;
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <extddl:ExtendedDropDownList ID="extddlemployerthreestate" runat="server" Width="90%"
                                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                            Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 6%">
                                                        <b>Employer Occupation </b>
                                                    </td>
                                                    <td align="left" style="width: 6%">
                                                        <dx:ASPxTextBox ID="txtemployerthirdoccu" runat="server" Width="91%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Employer From Date </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtemployerthreefrmdate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td style="width: 6%" class="ContentLabel">
                                                        <b>Employer To Date </b>
                                                    </td>
                                                    <td style="width: 6%" align="left">
                                                        <dx:ASPxDateEdit ID="txtemployerthreetodate" runat="server" Width="73%">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>Were you Treated by a Doctor(s) or the other person(s) Furnishing Health Services</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkyesfurshing" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chknofurshing" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>Will you have more Health Tratment(s)</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkyeshealth" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chknohealth" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>At the time of your accident were you in the course of your Employment</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkyescourseofemployment" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chknocourseofemployment" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>Were you Receiving Unemployment benefits at the time of Accident</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkyesaccident" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chknoaccident" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>As a result of your injury have you had any other Expenses?</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkyesexpenses" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chknoexpenses" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TDPart" colspan="6">
                                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>Due to this Accident have you received or are you eligble for Payments Under the
                                                            Following :</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>New York State Disability</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkyesnewyork" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chknonewyork" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2%" align="left">
                                                        <b>Worker's Compensation</b>
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkyeswc" runat="server" Text="&nbsp;<b>Yes</b>" />
                                                    </td>
                                                    <td style="width: 1%" align="left">
                                                        &nbsp;<asp:CheckBox ID="chknowc" runat="server" Text="&nbsp;<b>No</b>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="Tr2" runat="server">
                                        <td class="ContentLabel" colspan="5" style="height: 5px;">
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server">
                                        <td class="ContentLabel" colspan="5" style="height: 5px;">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                border="0">
                                <tr id="tdAddUpdate" runat="server">
                                    <td align="center" colspan="5">
                                        <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Width="80px"
                                            Text="Update"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                            <asp:HiddenField ID="hdndelete" runat="server" />
                            <asp:HiddenField ID="hdninsurancecode" runat="server" />
                            <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
