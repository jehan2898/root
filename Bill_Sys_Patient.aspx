<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Patient.aspx.cs" Inherits="Bill_Sys_Patient" MaintainScrollPositionOnPostback="true"  Culture="en-US"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx"TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.maskedinput.js"></script>
    <script type="text/javascript" src="js/jquery.maskedinput.min.js"></script>

    <script language="javascript" type="text/javascript">

        jQuery(function ($) {

            $("#_ctl0_ContentPlaceHolder1_txtCellNo").mask("999-999-9999");
        });
        $(document).ready(function () {

            if ($('#<%=hdnIsempl.ClientID%>').val() == "1") {
                $('#123').hide();
            }
        });
    </script>
    <script language="javascript" type="text/javascript">

     
        function SaveExistPatient() {
            document.getElementById('_ctl0_ContentPlaceHolder1_btnOK').click();
            document.getElementById('divid2').style.visibility = 'hidden';
        }
        function CancelExistPatient() {
            document.getElementById('divid2').style.visibility = 'hidden';
        }

        function GetInsuranceValue(source, eventArgs) {
            //alert(eventArgs.get_value());
            document.getElementById("<%=hdninsurancecode.ClientID %>").value = eventArgs.get_value();
        }

        function openExistsPage() {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute';
            document.getElementById('divid2').style.left = '360px';
            document.getElementById('divid2').style.top = '250px';
            document.getElementById('divid2').style.visibility = 'visible';
            return false;
        }






        function showAdjusterPanel() {
            document.getElementById('divAdjuster').style.visibility = 'visible';
            document.getElementById('divAdjuster').style.position = 'absolute';
            document.getElementById('divAdjuster').style.left = '300px';
            document.getElementById('divAdjuster').style.top = '250px';
            document.getElementById('divAdjuster').style.zIndex = 1;
        }
        function showInsurancePanel() {
            document.getElementById('divInsurance').style.visibility = 'visible';
            document.getElementById('divInsurance').style.position = 'absolute';
            document.getElementById('divInsurance').style.left = '300px';
            document.getElementById('divInsurance').style.top = '150px';
            document.getElementById('divInsurance').style.zIndex = 1;
            document.getElementById('extddlAttorney').style.visibility = 'hidden';
        }

        function ShowInsSearchPanel() {
            document.getElementById('_ctl0_ContentPlaceHolder1_txtInsSearch').value = '';
            document.getElementById('divInsSearch').style.visibility = 'visible';
            document.getElementById('divInsSearch').style.position = 'absolute';
            document.getElementById('divInsSearch').style.left = '550px';
            document.getElementById('divInsSearch').style.top = '340px';
            document.getElementById('divInsSearch').style.zIndex = 1;

            return false;
        }

        function showAttorneyPanel() {
            document.getElementById('divAttorney').style.visibility = 'visible';
            document.getElementById('divAttorney').style.position = 'absolute';
            document.getElementById('divAttorney').style.left = '200px';
            document.getElementById('divAttorney').style.top = '150px';
            document.getElementById('divAttorney').style.zIndex = 1;
        }


        function showAdjusterPanelAddress() {
            document.getElementById('divAddress').style.visibility = 'visible';
            document.getElementById('divAddress').style.position = 'absolute';
            document.getElementById('divAddress').style.left = '300px';
            document.getElementById('divAddress').style.top = '300px';
            document.getElementById('divAddress').style.zIndex = 1;

            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value = '';

            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceCity").value = '';
            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceZip").value = '';
            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceStreet").value = '';


        }


        function lfnShowHide(p_szSource) {
            if (p_szSource == 'button') {
                document.getElementById('divNavigate').style.visibility = 'hidden';
                return;
            }

            var hid = document.getElementById('_ctl0_ContentPlaceHolder1_hidIsSaved').value;
            if (hid == '0') {
                document.getElementById('divNavigate').style.visibility = 'hidden';
            } else {
                document.getElementById('divNavigate').style.visibility = 'visible';
            }
        }

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

        function clickButton1(e, charis) {
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


        function DisableKeyValidation(control, e) {
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                if (control.charCode == 39 || control.charCode == 39) {
                    return false;
                }

            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                if (event.keyCode == 39) {
                    return false;
                }
            }

        }



        function confirmstatus() {
            //       _ctl0_ContentPlaceHolder1_extddlInsuranceCompany 

            if (document.getElementById("_ctl0_ContentPlaceHolder1_extddlInsuranceCompany").value == 'NA') {
                alert('Select Insurance Company');

            }
            else {
                document.getElementById('divAddress').style.visibility = 'visible';
                document.getElementById('divAddress').style.position = 'absolute';
                document.getElementById('divAddress').style.left = '300px';
                document.getElementById('divAddress').style.top = '300px';
                document.getElementById('divAddress').style.zIndex = 1;
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").style.backgroundColor = "";
                document.getElementById('divAddressError').innerHTML = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode")
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value = '';

                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceCityCode").value = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceZipCode").value = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceStreetCode").value = '';
                document.getElementById("_ctl0_ContentPlaceHolder1_extddlStateCode").value = 'NA'; ;
                document.getElementById("_ctl0_ContentPlaceHolder1_IDDefault").checked = false;
            }

        }
        function checkAddressDetails() {
            if (document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value == '') {
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").focus();
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").style.backgroundColor = "#ffff99";
                document.getElementById('divAddressError').innerHTML = 'Enter the mandatory field';
                return false;
            }
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





        function TABLE5_onclick() {

        }


        function setReadOnly(obj) {
            var chk = document.getElementById('_ctl0_ContentPlaceHolder1_chkAutoIncr');

            if (chk.checked) {

                document.getElementById('_ctl0_ContentPlaceHolder1_txtRefChartNumber').readOnly = true;

            }
            else {

                document.getElementById('_ctl0_ContentPlaceHolder1_txtRefChartNumber').readOnly = false;

            }
        }
    
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;" colspan="6">
                <table width="100%" id="MainBodyTable" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="TDHeading" style="text-align: left; height: 25px; font-weight: bold;"
                            colspan="4">
                            <table width="100%">
                                <tr>
                                    <td width="50%">
                                        <asp:Label ID="Label1" runat="server">Personal Information</asp:Label>
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
                        <td width="100%" scope="col" class="TDPart" style="height: auto;">
                            <table width="100%">
                                <tr>
                                    <th colspan="6" align="center" valign="top" scope="col" style="height: auto;">
                                        <div align="left">
                                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                <!-- Start : Data Entry -->
                                                <tr>
                                                    <td class="tablecellLabel">
                                                        <div class="lbl">
                                                        </div>
                                                    </td>
                                                    <td class="tablecellSpace">
                                                        &nbsp;</td>
                                                    <td colspan="3">
                                                        <asp:LinkButton ID="hlnkAssociate" runat="server" OnClick="hlnkAssociate_Click" Visible="false">Associate Diagnosis Codes</asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <a id="hlnkShowNotes" href="#" runat="server" visible="false">
                                                            <img src="Images/actionEdit.gif" style="border-style: none;" /></a>
                                                        <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowNotes"
                                                            PopupControlID="pnlShowNotes" Position="Bottom" OffsetX="-420" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="height: 10px">
                                                        <div class="lbl">
                                                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                            <div id="ErrorDiv" style="color: red" visible="true">
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td colspan="6" >
                                                        <div class="lbl">
                                                        <asp:Label ID="lblPchartno" Text="Prev. Chart No. : " 

runat="server" class="lbl"  Visible="False" ForeColor="white" BackColor="red" ></asp:Label><asp:Label ID="lblPreChartNo" 

runat="server" Visible="False" BackColor="Red" class="lbl" ForeColor="White" Font-Bold="true" ></asp:Label>
                                                           
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                
                                                
                                                    <td class="ContentLabel">
                                                        <div class="lbl">
                                                            <asp:Label ID="lblChart" Text="Chart No." runat="server" class="lbl"></asp:Label>
                                                             <asp:Label ID="lblChartNo" Text="Chart No." runat="server" class="lbl"></asp:Label>
                                                            </div>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtChartNo" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtRefChartNumber"  runat="server"  style="float:left ;"  CssClass="cinput" Width="147px"  onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                       
                                                        <asp:CheckBox ID="chkAutoIncr" runat="server" style="float:right;" Text="Auto Increment" Visible="false" Width="126px" Font-Bold="False"   onclick="setReadOnly();"/>

                                                        </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblLocation" Text="Location" runat="server" class="lbl"></asp:Label>
                                                        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<cc1:ExtendedDropDownList ID="extddlLocation" Width="140px"
                                                            runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_LOCATION"


                                                            Flag_Key_Value="LOCATION_LIST" Selected_Text="--- Select ---" CssClass="cinput" Visible="False" />
                                                       <asp:Label ID="lextddlLocation" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></span></td>

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td height="26" class="ContentLabel">
                                                        <div class="lbl">
                                                            First name</div>
                                                    </td>
                                                    <td rowspan="2" class="tablecellSpace">
                                                    </td>
                                                    <td colspan="4" rowspan="2">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="txtPatientFName" runat="server" MaxLength="50" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <span style="color: Red;">
                                                                        <asp:Label ID="ltxtPatientFName" runat="server" ForeColor="#FF8000" Text="*" Visible="True"></asp:Label></span></td>
                                                                <td width="13%" class="ContentLabel">
                                                                    <div class="lbl">
                                                                        &nbsp;&nbsp; Middle</div>
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="txtMI" runat="server" Style="width: 20px;" Width="20px" MaxLength="1"
                                                                        CssClass="cinput" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtMI" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                                <td width="5%" class="ContentLabel">
                                                                    <div class="lbl">
                                                                        Last name</div>
                                                                </td>
                                                                <td width="32%">
                                                                    <asp:TextBox ID="txtPatientLName" runat="server" MaxLength="50" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPatientLName" runat="server" ForeColor="#FF8000" Text="*" Visible="True"></asp:Label><span style="color: #ff0000"></span></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="tablecellControl">
                                                                        <asp:TextBox Width="65px" ID="txtDateOfBirth" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                            MaxLength="10" CssClass="cinput"></asp:TextBox>
                                                                        <asp:Label ID="ltxtDateOfBirth" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                        
                                                                        <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                         <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtDateOfBirth" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                
                                                                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"
                                                                            PopupButtonID="imgbtnDateofBirth" />--%>
                                                                    </span>
                                                                </td>
                                                                <td class="ContentLabel">
                                                                    <div class="lbl">
                                                                        &nbsp;&nbsp; SSN #</div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSocialSecurityNumber" MaxLength="20" runat="server" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtSocialSecurityNumber" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                                <td class="ContentLabel">
                                                                    <div class="lbl">
                                                                        Gender</div>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlSex" runat="server" Width="153px">
                                                                        <asp:ListItem Value="NA" Text="--Select--"></asp:ListItem>
                                                                        <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                                                        <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lddlSex" runat="server" ForeColor="#FF8000" Text="*" Visible="True"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="26" class="ContentLabel">
                                                        <div class="lbl">
                                                            Date of birth</div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel">
                                                        <div class="lbl">
                                                            Address</div>
                                                    </td>
                                                    <td class="tablecellSpace">
                                                        &nbsp;</td>
                                                    <td colspan="4">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 20%; height: 41px;">
                                                                    <asp:TextBox Width="120px" ID="txtPatientAddress" MaxLength="50" runat="server" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPatientAddress" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                </td>
                                                                <td style="width: 5%; height: 41px;" class="ContentLabel">
                                                                    <div class="lbl">
                                                                        City</div>
                                                                </td>
                                                                <td style="width: 20%; height: 41px;">
                                                                    <asp:TextBox ID="txtPatientCity" runat="server" MaxLength="50" Width="120px" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPatientCity" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label><span id="mCity" runat="server"  style="color: #ff0000"></span></td>
                                                                <td class="ContentLabel" style="width: 5%; height: 41px;">
                                                                    <div class="lbl">
                                                                        State</div>
                                                                </td>
                                                                <td style="width: 16%; height: 41px;">
                                                                    <asp:TextBox ID="txtState" runat="server" Width="60px" CssClass="cinput" Visible="false"></asp:TextBox>
                                                                    <cc1:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---">
                                                                    </cc1:ExtendedDropDownList>
                                                                    <asp:Label ID="lextddlPatientState" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                                <td class="ContentLabel" style="width: 5%; height: 41px;">
                                                                    <div class="lbl">
                                                                        Zip</div>
                                                                </td>
                                                                <td style="width: 20%; height: 41px;">
                                                                    <asp:TextBox ID="txtPatientZip" runat="server" MaxLength="10" Width="60px" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPatientZip" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel">
                                                        <div class="lbl">
                                                            Home phone</div>
                                                    </td>
                                                    <td class="tablecellSpace">
                                                    </td>
                                                    <td colspan="4">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtPatientPhone" MaxLength="20" runat="server" Width="120px" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPatientPhone" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label><span id="mPhone" runat="server"  style="color: #ff0000"></span></td>
                                                                <td style="width: 5%" class="ContentLabel">
                                                                    <div class="lbl">
                                                                        Work</div>
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtWorkPhone" MaxLength="50" runat="server" Width="120px" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtWorkPhone" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                                <td class="ContentLabel" style="width: 5%">
                                                                    <div class="lbl">
                                                                        Extn.</div>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <asp:TextBox ID="txtExtension" runat="server" Width="60px" MaxLength="4" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtExtension" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                                <td class="ContentLabel" style="width: 5%">
                                                                    <div class="lbl">
                                                                        Email</div>
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtPatientEmail" MaxLength="50" runat="server" Width="120px" CssClass="cinput"
                                                                        onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPatientEmail" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="12" class="ContentLabel">
                                                        <div class="lbl">
                                                            Attorney</div>
                                                    </td>
                                                    <td class="tablecellSpace">
                                                    </td>
                                                    <td style="width: 292px">
                                                        <cc1:ExtendedDropDownList ID="extddlAttorney" Width="140px" runat="server" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---" />
                                                        <asp:Label ID="lextddlAttorney" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label><a href="#" id="aArrorney" onclick="showAttorneyPanel()" style="text-decoration: none;"><img id="imgbtnAddAdjuster" src="Images/actionEdit.gif" style="border-style: none;" /></a>
                                                    </td>
                                                    <td style="width: 165px" class="ContentLabel">
                                                        <div class="lbl">
                                                            Case Type</div>
                                                    </td>
                                                    <td>
                                                        <cc1:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="150px" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---" />
                                                        <asp:Label ID="lextddlCaseType" runat="server" ForeColor="#FF8000" Text="*" Visible="True"></asp:Label></td>
                                                    <td>
                                                        <div class="lbl">
                                                            <asp:CheckBox ID="chkWrongPhone" runat="server" Text="Wrong Phone" Visible="false" />
                                                            &nbsp; &nbsp;<asp:Label ID="lchkWrongPhones" runat="server" ForeColor="#FF8000" Text="*"
                                                                Visible="False"></asp:Label>
                                                            &nbsp;<asp:CheckBox ID="chkTransportation" runat="server" Text="Transport"
                                                                Visible="false"></asp:CheckBox>
                                                            <asp:Label ID="lchkTransportation" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></div>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                   <td height="12" class="ContentLabel">
                                                        <div class="lbl">
                                                            Cell Phone #
                                                        </div>
                                                    </td>
                                                    <td class="tablecellSpace"></td>
                                                    <td style="width: 292px">
                                                      <asp:TextBox ID="txtCellNo" MaxLength="20" runat="server" Width="120px" CssClass="cinput"  ></asp:TextBox>  <%--onkeypress="return DisableKeyValidation(event,'')" --%>

                                                    </td>
                                   
                                            </tr>
                                                <tr>
                                                    <td height="12" class="ContentLabel">
                                                        <div class="lbl">
                                                            &nbsp;</div>
                                                    </td>
                                                    <td class="tablecellSpace">
                                                    </td>
                                                    <td style="width: 292px">
<%--                                                       <asp:TextBox ID="txtRefChartNumber"  runat="server"  style="float:left ;"  CssClass="cinput" Width="120px"></asp:TextBox>
                                                       
                                                        <asp:CheckBox ID="chkAutoIncr" runat="server" style="float:right;" Text="Auto Increment" Visible="true" Width="159px" Font-Bold="False"  />
--%>                                                    </td>
                                                    
                                                    <td>
                                                      
                                                       </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class="tablecellLabel">
                                                        <div class="lbl">
                                                        </div>
                                                    </td>
                                                    <td rowspan="1" class="tablecellSpace">
                                                    </td>
                                                    <td colspan="4" rowspan="1">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="25%">
                                                                    <cc1:ExtendedDropDownList ID="extddlProvider" Width="10px" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---"
                                                                        Visible="false" />
                                                                    <cc1:ExtendedDropDownList ID="extddlCaseStatus" Width="10px" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                        Flag_ID="txtCompanyID.Text.ToString();" Visible="false" />
                                                                    <asp:TextBox ID="txtAssociateDiagnosisCode" runat="server" Visible="False" Width="10px" />
                                                                    
                                                                    <asp:TextBox ID="txtJobTitle" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtWorkActivites" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox Visible="false" ID="txtPatientStreet" runat="server" Width="10px"></asp:TextBox></td>
                                                                <td width="13%" class="tablecellLabel">
                                                                    <div class="lbl">
                                                                        <asp:TextBox Width="10px" ID="txtDateOfInjury" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                            MaxLength="10" Visible="False"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCarrierCaseNo" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtPatientAge" runat="server" onkeypress="return clickButton1(event,'')"
                                                                            MaxLength="10" Visible="False" Width="10px"></asp:TextBox></div>
                                                                </td>
                                                                <td width="25%">
                                                                    <span>
                                                                        <asp:CheckBox ID="chkAssociateCode" runat="server" Text="Associate Diagnosis Code"
                                                                            Visible="False" Width="200px" /></span></td>
                                                                <td width="5%" class="tablecellLabel">
                                                                    <div class="lbl">
                                                                        &nbsp;</div>
                                                                </td>
                                                                <td width="32%">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <!-- End : Data Entry -->
                                            </table>
                                        </div>
                                    </th>
                                </tr>
                            </table>
                            </td>
                        <td class="RightCenter" style="width: 10px; height: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" class="SectionDevider" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td width="50%">
                                        <table  width="100%">
                                            <tr>
                                                <td class="TDHeading" style="text-align: left; height: 25px; font-weight: bold;"
                                                    width="100%">
                                                    <asp:Label ID="Label9" runat="server">Accident Information</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" class="TDPart">
                                                    <table  width="100%" >
                                                        <tr>
                                                            <td width="25%">
                                                                <div class="lbl">
                                                                    Date</div>
                                                            </td>
                                                            <td style="width:38%">
                                                                <asp:TextBox Width="65px" ID="txtDateofAccident" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                    MaxLength="10" CssClass="cinput"></asp:TextBox>
                                                                   <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtDateofAccident" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                               
                                                                <asp:Label ID="ltxtDateofAccident" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                
                                                            </td>
                                                            <td width="1%">
                                                                <div class="lbl">
                                                                    Plate #</div>
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txtPlatenumber" MaxLength="20" runat="server" CssClass="cinput"
                                                                    Width="115" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPlatenumber" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                    </td>
                                                                    </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Report #</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtPolicyReport" runat="server" MaxLength="20" CssClass="cinput"
                                                                    Width="70%" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtPolicyReport" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                    </td>
                                                            <td width="1%">
                                                                <div class="lbl">
                                                                    Address</div>
                                                            </td>
                                                            <td width="30%">
                                                                <asp:TextBox Width="115" MaxLength="50" ID="txtAccidentAddress" runat="server" CssClass="cinput"
                                                                    onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtAccidentAddress" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                    </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    City</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtAccidentCity" MaxLength="50" runat="server" CssClass="cinput"
                                                                    Width="70%" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtAccidentCity" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                    </td>
                                                            <td width=1%">
                                                                <div class="lbl">
                                                                    State</div>
                                                            </td>
                                                            <td width="30%">
                                                                <asp:TextBox ID="txtAccidentState" runat="server" CssClass="cinput" Width="95%" Visible="false"></asp:TextBox>
                                                                <cc1:ExtendedDropDownList ID="extddlAccidentState" runat="server" Width="98%" Connection_Key="Connection_String"
                                                                    Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---">
                                                                </cc1:ExtendedDropDownList>
                                                                <asp:Label ID="lextddlAccidentState" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Hospital name</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtHospitalName" runat="server" CssClass="cinput" MaxLength="50" onkeypress="return DisableKeyValidation(event,'')" Width="70%"></asp:TextBox>
                                                                <asp:Label ID="ltxtHospitalName" Width="3%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>    
                                                            </td>
                                                            <td width="1%">
                                                                <div class="lbl">
                                                                    Hospital Address</div>
                                                            </td>
                                                            <td width="30%">
                                                                <span style="color: #ff0000">
                                                                    <asp:TextBox ID="txtHospitalAddress" runat="server" CssClass="cinput" MaxLength="50"  onkeypress="return DisableKeyValidation(event,'')" Width="115"></asp:TextBox>
                                                                    <asp:Label ID="ltxtHospitalAddress" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>    
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Date of admission</div>
                                                            </td>
                                                            <td style="width:30%">
                                                                <asp:TextBox Width="65px" ID="txtDateofAdmission" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                    MaxLength="10" CssClass="cinput"></asp:TextBox>
                                                                  <asp:ImageButton ID="imgbtnDateofAdmission" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                 <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtDateofAdmission" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                               
                                                                   <%-- <asp:Label ID="ltxtDateofAdmission" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>    
                                                              --%> 
                                                                <%--<ajaxToolkit:CalendarExtender ID="calextDateofAdmission" runat="server" TargetControlID="txtDateofAdmission"
                                                                    PopupButtonID="imgbtnDateofAdmission" />--%>
                                                            </td>
                                                            <td width="20%">
                                                            </td>
                                                            <td width="30%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Describe Injury</div>
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                                <asp:TextBox Width="90%" ID="txtDescribeInjury" runat="server" MaxLength="250" Wrap="true"
                                                                    CssClass="cinput" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                     <asp:Label ID="ltxtDescribeInjury" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>    
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Additional patients</div>
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                                <asp:TextBox Width="90%" ID="txtListOfPatient" runat="server" MaxLength="250" Wrap="true"
                                                                    CssClass="cinput" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtListOfPatient" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                    </td>
                                                                     </tr>
                                                        <tr>
                                                            <td width = "20%">
                                                                <div class="lbl">
                                                                    Patient Type
                                                                </div>
                                                            </td>
                                                            <td colspan = "3" width="95%" class = "lbl">
                                                                <asp:RadioButtonList ID="rdolstPatientType" runat="server" RepeatDirection="Horizontal" >
                                                                    <asp:ListItem Value = "0">Bicyclist</asp:ListItem>
                                                                    <asp:ListItem Value = "1">Driver</asp:ListItem>
                                                                    <asp:ListItem Value = "2">Passenger</asp:ListItem>
                                                                    <asp:ListItem Value = "3">Pedestrian</asp:ListItem>
                                                                </asp:RadioButtonList>  <asp:Label ID="lrdolstPatientType" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>    
                                                               
                                                                <asp:TextBox ID="txtPatientType" runat="server" Visible = "false" Width="2%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="1px">
                                    </td>
                                    <td width="50%" valign="top">

                                     <table width="100%" runat="server" id="tblEmployer">
                            <tr>
                                <td class="TDHeading" style="text-align: left; height: 25px; font-weight: bold;"
                                    width="100%">
                                    &nbsp;<asp:Label ID="Label14" runat="server">Employer Information</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" class="TDPart">
                                    <table width="100%">

                                     <tr>
                                            <td style="width: 20%">
                                                <div class="lbl">
                                                    Name</div>
                                            </td>
                                            <td colspan="3">
                                                <div class="lbl">
                                                    <cc1:ExtendedDropDownList ID="extddlEmployer" Width="200px" runat="server"
                                                        CssClass="dropdown" Connection_Key="Connection_String" Procedure_Name="SP_MST_EMPLOYER_COMPANY"
                                                        Flag_Key_Value="EMPLOYER_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                        OnextendDropDown_SelectedIndexChanged="extddlEmployer_extendDropDown_SelectedIndexChanged" />
                                                   
                                                </div>
                                            </td>
                                        </tr>

                                          <tr>
                                            <td style="width: 20%">
                                                <div class="lbl">
                                                     Address</div>
                                            </td>
                                            <td colspan="3">
                                                <div class="lbl">
                                                    <asp:ListBox Width="85%" ID="lstEmployerAddress" AutoPostBack="true" runat="server"
                                                        OnSelectedIndexChanged="lstEmployerAddress_SelectedIndexChanged" Height="40px"
                                                       ></asp:ListBox>
                                                    &nbsp;<asp:Label ID="Label15" Width="1%" runat="server" ForeColor="#FF8000"
                                                        Text="*" Visible="False"></asp:Label>
                                                        <asp:TextBox ID="txtEmployerAddId" runat="server" Visible="false"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 20%">
                                                <div class="lbl">
                                                    Address</div>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox Width="95%" ID="txtEmployercmpAddress" runat="server" CssClass="text-box"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>

                                           <td width="20%">
                                                    <div class="lbl">
                                                        Street </div>
                                                </td>
                                            <td width="30%">
                                                <asp:TextBox ID="txtEmployercmpStreet" runat="server" CssClass="text-box" ReadOnly="True"
                                                    Width="95%"></asp:TextBox>
                                            </td>
                                            <td width="20%">
                                                    <div class="lbl">
                                                        City</div>
                                                </td>
                                            <td width="30%">
                                                <asp:TextBox ID="txtEmployercmpCity" runat="server" CssClass="text-box" ReadOnly="True"
                                                    Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                            
                                        <tr>
                                            <td style="width: 20%">
                                                <div class="lbl">
                                                    State</div>
                                            </td>
                                            <td style="width: 30%">
                                               
                                                <cc1:ExtendedDropDownList ID="extddlEmployercmpState" runat="server" Selected_Text="--- Select ---"
                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                    Width="90%" Enabled="false"></cc1:ExtendedDropDownList>
                                                <asp:Label ID="Label18" Width="1%" runat="server" ForeColor="#FF8000"
                                                    Text="*" Visible="False"></asp:Label>
                                            </td>
                                            <td width="20%">
                                                <div class="lbl">
                                                    Zip</div>
                                            </td>
                                            <td width="30%">
                                                <asp:TextBox Width="95%" ID="txtEmployercmpZip" runat="server" CssClass="text-box-" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <asp:Label ID="Label19" Width="1%" runat="server" ForeColor="#FF8000"
                                                Text="*" Visible="False"></asp:Label></tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                     
                                        <table width="100%" id="123"  >
                                            <tr>
                                                <td class="TDHeading" style="text-align: left; height: 25px; font-weight: bold;"
                                                    width="100%">
                                                    &nbsp;<asp:Label ID="Label3" runat="server">Insurance Information</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td width="100%" class="TDPart">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <div class="lbl">
                                                                    Name</div>
                                                            </td>
                                                            <td colspan="3">
                                                                <div class="lbl">
                                                                    <cc1:ExtendedDropDownList ID="extddlInsuranceCompany" Width="200px" runat="server" CssClass="dropdown"
                                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                        Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                        OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged" />
                                                                     <asp:Label ID="lextddlInsuranceCompany" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                    <a href="#" id="A2" onclick="showInsurancePanel()" style="text-decoration: none;">
                                                                        <img id="imgbtnAddInsuranceCompany" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company and Address Details" /></a>
                                                                    <a href="#" id="A3" onclick="confirmstatus();" style="text-decoration: none;"  >
                                                                         <img id="img3" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company Address" /></a>
                                                                    
                                                                        <%--<asp:LinkButton ID="lnkSearch" runat="server" Text="Search" OnClientClick="ShowInsSearchPanel()" ></asp:LinkButton>--%>
                                                                        <a href="#" onclick="ShowInsSearchPanel()" >Search</a>
                                                                    
                                                                        
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <div class="lbl">
                                                                    Insurance Code</div>
                                                            </td>
                                                            <td colspan="3">
                                                                <div class="lbl">
                                                                    <cc1:ExtendedDropDownList ID="extddlInsuranceCode" Width="200px" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_INSURANCE_COMPANY" Flag_Key_Value="INSURANCE_CODE_LIST" CssClass="Dropdown"
                                                                        Selected_Text="--- Select ---" AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlInsuranceCode_extendDropDown_SelectedIndexChanged" />
                                                                        
                                                                        <asp:Label ID="lextddlInsuranceCode" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                  <%-- <a href="#" id="A2" onclick="confirmstatus();" <%--onfocus="showadjusterpaneladdress()"--%>
                                                                       <%--  style="text-decoration: none;">--%>
                                                                       <%--  <img id="img1" src="Images/actionEdit.gif" style="border-style: none;" /></a>--%>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <div class="lbl">
                                                                    Ins. Address</div>
                                                            </td>
                                                            <td colspan="3">
                                                                <div class="lbl">
                                                                    <asp:ListBox Width="85%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                                        OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged" Height="40px" OnLoad="lstInsuranceCompanyAddress_Load">
                                                                        
                                                                    </asp:ListBox>&nbsp;<asp:Label ID="llstInsuranceCompanyAddress" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label> </div>
                                                                          
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <div class="lbl">
                                                                    Address</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox Width="95%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                    <asp:Label ID="ltxtInsuranceAddress" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label><td width="20%">
                                                                <div class="lbl">
                                                                    City</div>
                                                            </td>
                                                            <td width="30%">
                                                                <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True"
                                                                    Width="95%"></asp:TextBox></td>
                                                                    <asp:Label ID="ltxtInsuranceCity" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <div class="lbl">
                                                                    State</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"
                                                                    Width="95%" Visible="false"></asp:TextBox>
                                                                <cc1:ExtendedDropDownList ID="extddlInsuranceState" runat="server" Selected_Text="--- Select ---"
                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                                    Width="90%" Enabled="false"></cc1:ExtendedDropDownList>
                                                                     <asp:Label ID="lextddlInsuranceState" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                            </td>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Zip</div>
                                                            </td>
                                                            <td width="30%">
                                                                <asp:TextBox Width="95%" ID="txtInsuranceZip" runat="server" CssClass="cinput" ReadOnly="True"></asp:TextBox></td>
                                                                <asp:Label ID="lbltxtInsuranceZip" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label></tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <div class="lbl">
                                                                    Claim/File #</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="cinput" Width="85%" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                 <asp:Label ID="ltxtClaimNumber" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label><td width="20%">
                                                                 <asp:Label ID="lblWcbNumber" CssClass="lbl" runat="server"></asp:Label></td>
                                                                </td>
                                                                
                                                                
                                                                
                                                            <td width="30%">
                                                                <asp:TextBox ID="txtWCBNumber" runat="server" CssClass="cinput" Width="95%" onkeypress="return DisableKeyValidation(event,'')"
                                                                    Visible="False"></asp:TextBox>
                                                                    
                                                                    <asp:Label ID="ltxtWCBNumber" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="cinput" Width="90%" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                <asp:Label ID="ltxtPolicyNumber" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                </td>
                                                                 </tr>
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <div class="lbl">
                                                                    Policy Holder</div>
                                                            </td>
                                                            <td style="width: 30%" colspan="3">
                                                                <asp:TextBox ID="txtPolicyHolder" Width="90%" runat="server" CssClass="cinput"></asp:TextBox>
                                                                <asp:Label ID="ltxtPolicyHolder" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
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
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                   
                                   
                                    <td width="50%" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td class="TDHeading" style="text-align: left; height: 25px; font-weight: bold;"
                                                    width="50%">
                                                    &nbsp;<asp:Label ID="Label5" runat="server">Adjuster Information</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td width="100%" class="TDPart" align="top">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%" style="height: 21px">
                                                                <div class="lbl">
                                                                    Name</div>
                                                            </td>
                                                            <td colspan="3" style="height: 21px">
                                                                <div class="lbl">
                                                                    <cc1:ExtendedDropDownList ID="extddlAdjuster" Width="152px" runat="server" Connection_Key="Connection_String"
                                                                        Selected_Text="--- Select ---" Flag_Key_Value="GET_ADJUSTER_LIST" Procedure_Name="SP_MST_ADJUSTER"
                                                                        AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlAdjuster_extendDropDown_SelectedIndexChanged" />
                                                                        <asp:Label ID="lextddlAdjuster" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                    &nbsp;<a href="#" id="hlnlShowAdjuster" onclick="showAdjusterPanel()" style="text-decoration: none;">
                                                                        <img id="imgShowAdjuster" src="Images/actionEdit.gif" style="border-style: none;" /></a></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    &nbsp;Phone</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtAdjusterPhone" runat="server" ReadOnly="true" CssClass="cinput"
                                                                    Width="85%"></asp:TextBox>
                                                                    <asp:Label ID="ltxtAdjusterPhone" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                    </td>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Extension</div>
                                                            </td>
                                                            <td width="30%">
                                                                <asp:TextBox ID="txtAdjusterExtension" runat="server" ReadOnly="true" CssClass="cinput"
                                                                    Width="85%"></asp:TextBox>
                                                                    <asp:Label ID="ltxtAdjusterExtension" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                    </td>
                                                                    </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Email</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox Width="85%" ID="txtEmail" runat="server" ReadOnly="true" CssClass="cinput"></asp:TextBox>
                                                                <asp:Label ID="ltxtEmail" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                </td>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    Fax</div>
                                                            </td>
                                                            <td width="30%">
                                                                <asp:TextBox ID="txtfax" runat="server" ReadOnly="true" CssClass="cinput" Width="85%"></asp:TextBox>
                                                                 <asp:Label ID="ltxtfax" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                </td>
                                                               </tr>
                                                        <tr>
                                                            <td width="20%">    
                                                                <div class="lbl">
                                                                    &nbsp;</div>
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <asp:TextBox Visible="false" Width="20px" ID="txtInsuranceStreet" runat="server"
                                                                    CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                                    <asp:Label ID="ltxtInsuranceStreet" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                     <td width="1px">
                                    </td>
                                     <td width="50%">
                                          <table width="100%" id="tblEmployerInfo">
                                            <tr>
                                                <td class="TDHeading" style="text-align: left; height: 25px; font-weight: bold;"
                                                    width="50%">
                                                    <asp:Label ID="Label10" runat="server">Employer Information </asp:Label>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" class="TDPart" align="top">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%" style="height: 22px" runat="server" id="td1" >
                                                                <div class="lbl">
                                                                    Name</div>
                                                            </td>
                                                            <td style="width: 30%; height: 22px;">
                                                                <asp:TextBox Width="83%" ID="txtEmployerName" MaxLength="50" runat="server" CssClass="cinput"
                                                                    onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtEmployerName" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                </td>
                                                            <td width="20%" style="height: 22px" runat="server" id="td2">
                                                                <div class="lbl">
                                                                    Address</div>
                                                            </td>
                                                            <td style="height: 22px; width: 30%;">
                                                                <asp:TextBox ID="txtEmployerAddress" runat="server" MaxLength="150" Width="88%" CssClass="cinput"
                                                                    onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    
                                                                     <asp:Label ID="ltxtEmployerAddress" Width="1%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>
                                                                    </td>
                                                                   </tr>
                                                        <tr>
                                                            <td width="20%" runat="server" id="td3">
                                                                <div class="lbl">
                                                                    City</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtEmployerCity" MaxLength="50" runat="server" CssClass="cinput"
                                                                    Width="87%" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtEmployerCity" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                    </td>
                                                            <td width="20%" runat="server" id="td4">
                                                                <div class="lbl">
                                                                    State</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtEmployerState" runat="server" CssClass="cinput" Width="95%" Visible="false"></asp:TextBox>
                                                                <cc1:ExtendedDropDownList ID="extddlEmployerState" runat="server" Selected_Text="--- Select ---"
                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                                    Width="88%"></cc1:ExtendedDropDownList>
                                                                    <asp:Label ID="ltxtEmployerState" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" runat="server" id="td5">
                                                                <div class="lbl" >
                                                                    Zip</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtEmployerZip" MaxLength="50" runat="server" Width="87%" CssClass="cinput"
                                                                    onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox>
                                                                    <asp:Label ID="ltxtEmployerZip" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"></asp:Label>       
                                                                    </td>
                                                            <td width="20%">
                                                                <div class="lbl" runat="server" id="td6">
                                                                    Phone</div>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <asp:TextBox ID="txtEmployerPhone" MaxLength="20" runat="server" CssClass="cinput"
                                                                    Width="82%" onkeypress="return DisableKeyValidation(event,'')"></asp:TextBox><asp:Label ID="ltxtEmployerPhone" Width="2%" runat="server" ForeColor="#FF8000" Text="*" Visible="False"> </asp:Label></td>
                                                                    </tr>
                                                        <tr>
                                                            <td width="20%">
                                                                <div class="lbl">
                                                                    &nbsp;</div>
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" style="height: 18px">
                                                            </td>
                                                            <td colspan="3" width="80%" style="height: 18px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                            </td>
                                                            <td colspan="3" width="80%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%">
                                                            </td>
                                                            <td colspan="3" width="80%">
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
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td scope="col" class="TDPart">
                            <table width="100%">
                                <tr>
                                    <td class="ContentLabel">
                                        <asp:TextBox ID="txtAccidentID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                            CssClass="Buttons" />
                                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                            Width="80px" CssClass="Buttons" visible="false"/>
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                            CssClass="Buttons" />
                                        <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtUserId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td scope="col">
                            &nbsp;</td>
                        <td scope="col" id="anch_Patient">
                            <asp:DataGrid ID="grdPatientList" Visible="false" runat="server" OnDeleteCommand="grdPatientList_DeleteCommand"
                                OnPageIndexChanged="grdPatientList_PageIndexChanged" OnSelectedIndexChanged="grdPatientList_SelectedIndexChanged"
                                Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                PageSize="10" PagerStyle-Mode="NumericPages">
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="Patient ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_FIRST_NAME" HeaderText="First Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_LAST_NAME" HeaderText="Last Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="I_PATIENT_AGE" HeaderText="Age" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="Address" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_STREET" HeaderText="Street" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_CITY" HeaderText="City" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_ZIP" HeaderText="Zip" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Phone" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_EMAIL" HeaderText="Email" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MI" HeaderText="MI" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WCB_NO" HeaderText="WCB" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_SOCIAL_SECURITY_NO" HeaderText="Social Security No"
                                        Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DT_DOB" HeaderText="Date Of Birth" DataFormatString="{0:MM/dd/yyyy}">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_GENDER" HeaderText="Gender"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DT_INJURY" HeaderText="Date of Injury" DataFormatString="{0:MM/dd/yyyy}">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_JOB_TITLE" HeaderText="Job Title"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WORK_ACTIVITIES" HeaderText="Work Activities"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_PATIENT_STATE" HeaderText="State" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_CARRIER_CASE_NO" HeaderText="Carrier Case No" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_NAME" HeaderText="Employer Name" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_PHONE" HeaderText="Employer Phone" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_ADDRESS" HeaderText="Employer Address" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_CITY" HeaderText="Employer City" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_STATE" HeaderText="Employer State" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_EMPLOYER_ZIP" HeaderText="Employer Zip" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WORK_PHONE" HeaderText="Employer City" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_WORK_PHONE_EXTENSION" HeaderText="Employer State"
                                        Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="BT_WRONG_PHONE" HeaderText="Employer Zip" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BT_TRANSPORTATION" HeaderText="Employer Zip" Visible="false">
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <contenttemplate>
            <asp:Panel ID="pnlShowNotes" Visible="false" runat="server" Style="width: 420px;
                height: 220px; background-color: white; border-color: SteelBlue; border-width: 1px;
                border-style: solid;">
                <iframe id="Iframe2" src="Bill_Sys_PopupNotes.aspx" frameborder="0" height="220px"
                    width="420px" visible="false"></iframe>
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>
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
            <span id="msgPatientExists"  runat="server"></span></div>
        <br />
        <div style="text-align: center;">
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />
        </div>
    </div>
  
    <br />
    <div style="text-align: center;">
        <%--<input type="button" runat="server"  class="Buttons" value="OK" id="Button1" onclick="SaveExistPatient();" style="width:80px;" /> 
                    <input type="button" class="Buttons" value="Cancel" id="Button4" onclick="CancelExistPatient();" style="width:80px;"/>                  
                   --%>
    </div>
    <%--<div id="divNavigate" style="position: absolute; left: 621px; top: 270px; width: 300px;
        height: 150px; background-color: #DBE6FA; border-right: silver 2px solid; border-top: silver 2px solid;
        border-left: silver 2px solid; border-bottom: silver 2px solid; text-align: center;">
        <table style="vertical-align: middle; left: 14px; width: 278px; position: absolute;
            top: 27px;">
            <tr>
                <td id="aIncheduleTD" runat="server" style="height: 28px">
                    <a  href="Bill_Sys_ScheduleEvent.aspx?Flag=True">Schedule In</a>
                </td>
                <td  id="aOutcheduleTD" runat="server" style="height: 28px">
                    <a id="aOutcheduleLink" runat="server"  href="Bill_Sys_AppointPatientEntry.aspx">Schedule Out</a>
                </td>
            </tr>
            <tr>
                <td style="height: 25px">
                    <a href="Bill_Sys_RequiredDocuments.aspx" >Upload Documents</a>
                </td>
                <td style="height: 25px">
                    <a href="Bill_Sys_Patient.aspx">Add New Patient</a>
                </td>
            </tr>
            <tr>
                <td width="100%" style="height: 25px" colspan="2">
                    <a href="Bill_Sys_TemplateManager.aspx" target="_blank">Intake</a>
                </td>
            </tr>
        </table>
      
        <input id="btnToggle" type="button" class="Buttons" value="Cancel" onclick="javascript:lfnShowHide('button');"
            style="left: 8px; top: 120px; position: relative;" /></div>--%>
    <div id="divAttorney" style="visibility: hidden; width: 600px; left: 265px; top: 966px;
        position: absolute;">
        <asp:Panel ID="pnlAddAttorney" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table1" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="LeftCenter" style="height: 100%">
                    </td>
                    <td class="Center" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divAttorney').style.visibility='hidden';" style="cursor: pointer;"
                                                        title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="lblAttorneyMsg" runat="server" Visible="false"></asp:Label>
                                                <div id="AttorneyErrordiv" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label6" runat="server">Attorney</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Firm Name:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyFirstName" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Contact Name:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyLastName" runat="server" Width="250px" MaxLength="20"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                City:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyCity" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                State:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyState" runat="server" Width="250px" MaxLength="50" Visible="false"></asp:TextBox>
                                                <cc1:ExtendedDropDownList ID="extddlAttorneyState" runat="server" Width="255px" Selected_Text="--- Select ---"
                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                </cc1:ExtendedDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Zip:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyZip" runat="server" Width="250px" MaxLength="10"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Phone No:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyPhoneNo" runat="server" Width="250px" MaxLength="12"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Fax:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyFax" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Email ID:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAttorneyEmailID" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtAttorneyEmailID"
                                                    EnableClientScript="True" ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                            </td>
                                            <td style="width: 35%">
                                            </td>
                                            <td class="ContentLabel" style="width: 15%">
                                            </td>
                                            <td style="width: 35%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <asp:Button ID="btnAddAttorney" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                    OnClick="btnAddAttorney_Click" />
                                                <asp:Button ID="btnUpdateAttorney" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click"
                                                    CssClass="Buttons" Visible="false" />
                                                <asp:Button ID="btnClearAttorney" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                    Visible="false" CssClass="Buttons" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" class="SectionDevider">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="RightCenter" style="width: 10px; height: 100%;">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div id="divAdjuster" style="visibility: hidden; width: 600px; left: 481px; top: 883px;
        position: absolute;">
        <asp:Panel ID="pnlAddAdjuster" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="LeftCenter" style="height: 224px">
                    </td>
                    <td class="Center" valign="top" style="height: 224px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divAdjuster').style.visibility='hidden';" style="cursor: pointer;"
                                                        title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="Label7" runat="server" Visible="false"></asp:Label>
                                                <div id="AdjusterErrorDiv" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label8" runat="server">Adjuster</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Adjuster Name:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupName" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Phone Number:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupPhone" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%; height: 38px;">
                                                Extension:</td>
                                            <td style="width: 35%; height: 38px;">
                                                <asp:TextBox ID="txtAdjusterPopupExtension" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%; height: 38px;">
                                                FAX:</td>
                                            <td style="width: 35%; height: 38px;">
                                                <asp:TextBox ID="txtAdjusterPopupFax" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Email:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                            </td>
                                            <td style="width: 35%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <asp:Button ID="btnSaveAdjuster" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                    OnClick="btnSaveAdjuster_Click" />
                                                <asp:Button ID="btnUpdateAdjuster" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
                                                <asp:Button ID="btnClearAdjuster" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="RightCenter" style="width: 10px; height: 224px;">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div id="divInsurance" style="visibility: hidden; width: 600px; left: 569px; top: 811px;
        position: absolute;">
        <asp:Panel ID="pnlAddInsurance" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table3" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="Center" valign="top" style="height: 188px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 20px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divInsurance').style.visibility='hidden';document.getElementById('divInsurance').style.zIndex = -1;document.getElementById('extddlAttorney').style.visibility='visible';"
                                                        style="cursor: pointer;" title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="Label2" runat="server" Visible="false"></asp:Label>
                                                <div id="InsuranceErrorDiv" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label4" runat="server" Style="font-weight: bold;">Insurance Company</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%; height: 40px;">
                                                Insurance Company Name:</td>
                                            <td style="width: 35%; height: 40px;">
                                                <asp:TextBox ID="txtInsuranceCompanyName" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%; height: 40px;">
                                                Insurance Code
                                            </td>
                                            <td style="width: 35%; height: 40px;">
                                                <asp:TextBox ID="txtInsCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Phone Number:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsurancePhoneNumber" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Email:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td style="width: 100%;" class="TDPart">
                                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                                <tr>
                                                                    <td colspan="4" align="left">
                                                                        Address Details</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ContentLabel" style="width: 15%">
                                                                        Address</td>
                                                                    <td style="width: 35%">
                                                                        <asp:TextBox ID="txtInsuranceAddressNew" runat="server" Width="80%"></asp:TextBox></td>
                                                                    <td class="ContentLabel" style="width: 15%">
                                                                    </td>
                                                                    <td style="width: 35%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ContentLabel" style="width: 15%">
                                                                        Street</td>
                                                                    <td style="width: 35%">
                                                                        <asp:TextBox ID="txtInsuranceStreetNew" runat="server" Width="80%"></asp:TextBox></td>
                                                                    <td class="ContentLabel" style="width: 15%">
                                                                        City
                                                                    </td>
                                                                    <td style="width: 35%">
                                                                        <asp:TextBox ID="txtInsuranceCityNew" runat="server" Width="80%"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ContentLabel" style="width: 15%">
                                                                        State</td>
                                                                    <td style="width: 35%">
                                                                        <asp:TextBox ID="txtInsuranceStateNew" runat="server" Visible="false"></asp:TextBox>
                                                                        <cc1:ExtendedDropDownList ID="extddlInsuranceStateNew" runat="server" Width="80%"
                                                                            Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                                            Connection_Key="Connection_String"></cc1:ExtendedDropDownList>
                                                                    </td>
                                                                    <td class="ContentLabel" style="width: 15%">
                                                                        Zip
                                                                    </td>
                                                                    <td style="width: 35%">
                                                                        <asp:TextBox ID="txtInsuranceZipNew" runat="server" Width="80%"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <asp:Button ID="btnSaveInsurance" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                    OnClick="btnSaveInsurance_Click" />
                                                <asp:Button ID="Button2" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
                                                <asp:Button ID="Button3" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button ID="btnOK" runat="server" Style="visibility: hidden;" CssClass="Buttons"
            Text="OK" OnClick="btnOK_Click" />
    </div>
    <div id="divAddress" style="visibility: hidden; width: 600px; left: 244px; top: 999px;
        vertical-align: bottom; position: absolute;">
        <asp:Panel ID="pnlAddress" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table4" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="LeftCenter" style="height: 100%">
                    </td>
                    <td class="Center" valign="top" style="width: 782px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 77%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%" id="TABLE5" onclick="return TABLE5_onclick()">
                                    <tr>
                                            <td width="100%" colspan="4">
                                                <div id="divAddressError" style="color: red;font-size:small"  >
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divAddress').style.visibility='hidden';" style="cursor: pointer;"
                                                        title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="Label11" runat="server" Visible="false"></asp:Label>
                                                <div id="Div1" style="color: red; "   visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label12" runat="server">Address Details</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Address:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceAddressCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Street:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceStreetCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                City:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceCityCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                State:</td>
                                            <td style="width: 35%">
                                                <cc1:ExtendedDropDownList ID="extddlStateCode" runat="server" Width="80%" Selected_Text="--- Select ---" CssClass="Dropdown"
                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                </cc1:ExtendedDropDownList>
                                                <cc1:ExtendedDropDownList ID="ExtendedDropDownList2" runat="server" Width="255px"
                                                    Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                    Connection_Key="Connection_String" Visible="false"></cc1:ExtendedDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Zip:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceZipCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                               Default:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="IDDefault" runat="server" />
                                            </td>
                                            <td style="width: 35%">
                                                <%--<asp:TextBox ID="txtCompanyID1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                          --%>
                                                <asp:TextBox ID="txtInsuranceStateNewCode" runat="server" Visible="false"></asp:TextBox>
                                                <%--<cc1:ExtendedDropDownList ID="extddlInsuranceStateNew" runat="server" Width="80%"
                                                                            Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                                            Connection_Key="Connection_String"></cc1:ExtendedDropDownList>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%--<td class="ContentLabel">
                                                Default:</td>--%>
                                            <%--<td>
                                                <asp:CheckBox ID="IDDefault" runat="server" />
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <asp:Button ID="btnSaveAddress" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                    OnClick="btnSaveAddress_Click" OnClientClick =" return checkAddressDetails()" />
                                                <asp:Button ID="btnClearAddress" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="RightCenter" style="width: 10px; height: 100%;">
                    </td>
                </tr>
                <asp:TextBox ID="txtInsuranceCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox><%--<asp:TextBox id="txtInsuranceCompanyAddress" runat="server" Width="10px" Visible="False"></asp:TextBox>--%></table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hdninsurancecode" runat="server" />
    <div id="divInsSearch" style="visibility:hidden; width:300px; left:244px; top:999px; vertical-align:bottom; position:absolute; z-index:5;">
        <asp:Panel ID="pnlInsSearch" runat="server" BackColor="white" BorderColor="steelblue" Width="300px">
        <table width="100%">
            <tr>
                <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;" colspan="2" valign="top">
                    <div style="position: relative; text-align: right; background-color: #8babe4;">
                    <a onclick="document.getElementById('divInsSearch').style.visibility='hidden';" style="cursor: pointer;"
                    title="Close">X &nbsp;</a>
                    </div>
                    <asp:Label CssClass="message-text" ID="Label13" runat="server" Visible="false"></asp:Label>
                    <div id="Div2" style="color: red; "   visible="true">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width:100%" align="center" valign="top">
                    <table style="width:80%;">
                        <tr>
                            <td>
                                Insurance:
                            </td>
                            <td>
                                <asp:TextBox ID="txtInsSearch" runat="server" AutoComplete="off" ></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="aceInsSearch" runat="server" TargetControlID="txtInsSearch" UseContextKey="true" 
                                ServicePath="AJAX Pages/PatientService.asmx" ServiceMethod="GetInsurance_With_Addr" ContextKey="SZ_COMPANY_ID" CompletionSetCount="20"
                                CompletionListCssClass="autocomplete_completionListElement" OnClientItemSelected="GetInsuranceValue"
                                DelimiterCharacters="" MinimumPrefixLength="1" EnableCaching="true" ></ajaxToolkit:AutoCompleteExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="Buttons" OnClick="btnAccept_ButtonClick"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hidIsSaved" Value="0" runat="server" />
    <asp:HiddenField ID="hdnIsempl" runat="server" />
         
          <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateOfBirth" PromptCharacter="_" AutoComplete="true" ></ajaxToolkit:MaskedEditExtender>
          <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
          <ajaxToolkit:CalendarExtender ID="calDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
          PopupButtonID="imgbtnDateofBirth" />     
                                                       
         <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateofAdmission" PromptCharacter="_" AutoComplete="true" ></ajaxToolkit:MaskedEditExtender>
         <asp:Label ID="lblValidator3" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
         <ajaxToolkit:CalendarExtender ID="calDateOfAddmission" runat="server" TargetControlID="txtDateofAdmission"
         PopupButtonID="imgbtnDateofAdmission" /> 
         
         <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateofAccident" PromptCharacter="_" AutoComplete="true" ></ajaxToolkit:MaskedEditExtender>
         <asp:Label ID="lblValidator9" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
         <ajaxToolkit:CalendarExtender ID="calDateOfAccident" runat="server" TargetControlID="txtDateofAccident"
         PopupButtonID="imgbtnDateofAccident" />                                  
                                                                                             
</asp:Content>
