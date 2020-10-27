<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Insurance_popup.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Insurance_popup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">
        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }
        function CheckForInteger(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }


        }

        function GetInsuranceValue(object, arg) {

            document.getElementById('hdninsurancecode').value = arg.get_value();
        }
        function SetDOADate() {

            if (document.getElementById('ChkOC').checked) {
                document.getElementById('txtdateofaccident').value = '';
                document.getElementById('txtdateofaccident').disabled = true;
                document.getElementById('imgbtnATAccidentDate').disabled = true;

            }
            else {
                document.getElementById('txtdateofaccident').disabled = false;
                document.getElementById('imgbtnATAccidentDate').disabled = false;
            }
        }
        
    </script>

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
    <link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />

    <script type="text/javascript">
        
        
        
       
         
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
        </asp:ScriptManager>
        <div>
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="width: 600px;">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%" id="Table3" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 600px" class="Center" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 80%; height: 100%;
                                                    background-color: White;">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 100%; background-color: White;" class="TDPart">
                                                                <table style="width: 600px" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div class="lbl">
                                                                                    <asp:Label ID="lblMsg1" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                                                    <div style="color: red" id="ErrorDiv" visible="true">
                                                                                        <UserMessage:MessageControl ID="usrMessage1" runat="server"></UserMessage:MessageControl>
                                                                                        <asp:UpdateProgress ID="UpdatePanel15" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanel11">
                                                                                            <ProgressTemplate>
                                                                                                <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                                    runat="Server">
                                                                                                    <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                                    Loading...</div>
                                                                                            </ProgressTemplate>
                                                                                        </asp:UpdateProgress>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <table style="width: 100%;" class="ContentTable" cellspacing="2" cellpadding="3"
                                                                    border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="text-align: center" class="ContentLabel" colspan="5" rowspan="1">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel">
                                                                                <b>Insurance Name: </b>
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany"
                                                                                    ServiceMethod="GetInsuranceLHR" ServicePath="PatientService.asmx" UseContextKey="true"
                                                                                    ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                                                                                </ajaxToolkit:AutoCompleteExtender>
                                                                                <asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="75%"
                                                                                    OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true" />
                                                                                <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server"
                                                                                    Visible="false" Connection_Key="Connection_String" Procedure_Name="SP_GET_SECONDARY_INS"
                                                                                    Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                                    OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged"
                                                                                    OldText="" StausText="False" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>Insurance Address : </b>
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:ListBox Width="100%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                                                    OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>Address </b>
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                                                    ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>City : </b>
                                                                            </td>
                                                                            <td align="left" colspan="4" style="width: 35%">
                                                                                <asp:TextBox ID="txtInsCity" runat="server" ReadOnly="true">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 20%" class="ContentLabel">
                                                                                <b>State : </b>
                                                                            </td>
                                                                            <td style="width: 35%" align="left">
                                                                                <asp:TextBox ID="txtInsState" runat="server" ReadOnly="true">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>Zip : </b>&nbsp;
                                                                            </td>
                                                                            <td align="left" style="width: 35%">
                                                                                <asp:TextBox ID="txtInsZip" runat="server" ReadOnly="true"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 20%">
                                                                                <b>Phone : </b>
                                                                            </td>
                                                                            <td align="left" style="width: 35%">
                                                                                <asp:TextBox ID="txtInsPhone" runat="server" ReadOnly="true">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>Fax : </b>&nbsp;
                                                                            </td>
                                                                            <td style="width: 35%" align="left">
                                                                                <asp:TextBox ID="txtInsFax" runat="server" ReadOnly="true"> </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 20%" class="ContentLabel">
                                                                                <b>Claim No : </b>&nbsp;
                                                                            </td>
                                                                            <td style="width: 35%" align="left">
                                                                                <asp:TextBox ID="txtclaimno" runat="server"> </asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>Policy No : </b>&nbsp;
                                                                            </td>
                                                                            <td align="left" style="width: 35%">
                                                                                <asp:TextBox ID="txtpoliccyno" runat="server"> </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 20%" class="ContentLabel">
                                                                                <b>Case Type : </b>&nbsp;
                                                                            </td>
                                                                            <td style="width: 35%" align="left">
                                                                                <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="90%" Selected_Text="---Select---"
                                                                                    OnextendDropDown_SelectedIndexChanged="extddlCaseType_extendDropDown_SelectedIndexChanged"
                                                                                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                                                                    AutoPost_back="true"></extddl:ExtendedDropDownList>
                                                                            </td>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>Policy Holder : </b>&nbsp;
                                                                            </td>
                                                                            <td align="left" style="width: 35%">
                                                                                <asp:TextBox ID="txtpolicyholder" runat="server"> </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 20%" class="ContentLabel">
                                                                                <b>Date Of Accident:</b>&nbsp;
                                                                            </td>
                                                                            <td style="width: 35%" align="left">
                                                                                <asp:TextBox Width="60%" ID="txtdateofaccident" runat="server" MaxLength="10" CssClass="textbox"></asp:TextBox>&nbsp;
                                                                                <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <ajaxToolkit:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="txtdateofaccident"
                                                                                    PopupButtonID="imgbtnATAccidentDate" Enabled="True" PopupPosition="TopLeft" />
                                                                                <asp:CheckBox ID="ChkOC" Text="OC" runat="server" />
                                                                            </td>
                                                                            <td style="width: 25%" class="ContentLabel">
                                                                                <b>Patient Phone: </b>&nbsp;
                                                                            </td>
                                                                            <td align="left" style="width: 35%">
                                                                                <asp:TextBox ID="txtPatientPhoneNo" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 20%" class="ContentLabel">
                                                                                <b>Location Name:</b>&nbsp;
                                                                            </td>
                                                                            <td style="width: 35%">
                                                                                <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="80%" Connection_Key="Connection_String"
                                                                                    Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" />
                                                                            </td>
                                                                            <td class="ContentLabel">
                                                                                <b>Co-Signed By:</b>
                                                                            </td>
                                                                            <td>
                                                                                <extddl:ExtendedDropDownList ID="extddlCoSignedby" runat="server" Width="213px" Connection_Key="Connection_String"
                                                                                    Procedure_Name="SP_GET_LHR_READINGDOCTORS" Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST"
                                                                                    Flag_ID="txtCompanyID.Text.ToString();" AutoPost_back="false" Enabled="false" />
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
                                                                        <tr id="tdAddUpdate" runat="server">
                                                                            <td class="ContentLabel" colspan="5">
                                                                                <asp:CheckBox ID="chkrefreshreport" runat="server" Text="Refresh received report"
                                                                                    OnCheckedChanged="OnCheckedChanged_reportrefesh" AutoPostBack="true" />
                                                                                <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Width="80px"
                                                                                    Text="Update"></asp:Button>
                                                                                <asp:Button ID="btnremove" OnClick="btnRemove_Click" runat="server" Width="80px"
                                                                                    Text="Remove"></asp:Button>
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
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdninsurancecode" runat="server" />
            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txteventid" runat="server" Width="10px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtInsuranceid" runat="server" Width="10px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtInsuranceaddid" runat="server" Width="10px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtcasetype" runat="server" Width="10px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="policyholder" runat="server" Width="10px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtEventPRocID" runat="server" Width="10px" Visible="False"></asp:TextBox>
        </div>
    </form>
</body>
</html>
