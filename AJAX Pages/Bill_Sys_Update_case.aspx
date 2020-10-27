<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Update_case.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Update_case" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Src="~/UserControl/Bill_Sys_AssociateCases.ascx" TagName="Bill_Sys_AssociateCases"
    TagPrefix="ASC" %>
<%@ Register Src="~/UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <script type="text/javascript" src="~/validation.js"></script> 
         <script language="javascript" type="text/javascript">
        function GetInsuranceValue(source, eventArgs)
    {
       //alert(eventArgs.get_value());
        document.getElementById("<%=hdninsurancecode.ClientID %>").value = eventArgs.get_value();
    }
    function showSecondaryInsurance() {
        var url = "SecondaryInsuraceViewFrame.aspx";

        SecInsuracePop.SetContentUrl(url);
        SecInsuracePop.Show();
        return false;

    }
    function showUpdateAdjusterFrame() {
        var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
        var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
        var objAdjusterID = document.getElementById("<%= hdadjusterCode.ClientID %>").value;
        var flag = "update";
        var url = "Adjuster.aspx?CaseID=" + objCaseID + "&adjcompany=" + objCompanyID + "&link=" + flag + "&objAdjusterID=" + objAdjusterID;
        AddAdjusterPop.SetContentUrl(url);
        AddAdjusterPop.Show();
        return false;
    }
    function ClosePopup() {
        AddAdjusterPop.Hide();
        var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
        var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
        var sURL = unescape(window.location.pathname);
        sURL = sURL + "?CaseID=" + objCaseID + "&cmp=" + objCompanyID;
        window.location.href = sURL;
    }
    function ClosePopup1() {
        AddAdjusterPop.Hide();
        var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
        var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
        var sURL = unescape(window.location.pathname);
        sURL = sURL + "?CaseID=" + objCaseID + "&cmp=" + objCompanyID;
        window.location.href = sURL;
    }
    function showAdjusterFrame(objCaseID, objCompanyID) {

        var objCaseID = document.getElementById("<%= txtCaseID.ClientID %>").value;
        var objCompanyID = document.getElementById("<%= txtCompanyID.ClientID %>").value;
        var url = "Adjuster.aspx?CaseID=" + objCaseID + "&adjcompany=" + objCompanyID + "";
        AddAdjusterPop.SetContentUrl(url);
        AddAdjusterPop.Show();
        return false;
    }
    </script>
    
    <div>
        <table>
            <tr>
                <td colspan="6" align="center">
                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                    <UserMessage:MessageControl runat="server" id="usrMessage" />
                </td>
            </tr>
            <tr>
                 <td style="vertical-align: top">
                    <dx:aspxpagecontrol id="carTabPage" runat="server" activetabindex="0" enablehierarchyrecreation="True"
                        width="100%" height="100%" >
                        <TabPages>
                            <dx:TabPage Text="Personal Information" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0">
<ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

<TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <asp:Panel style="background-color:white;width:1200px;height:500px;" id="pnlSaveDescription" runat="server"  >
                                            <div align="left">
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                    <!-- Start : Data Entry -->
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" >
                                                            First name
                                                        </td>
                                                        <td rowspan="2" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="2">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td width="25%">
                                                                        <span class="tablecellControl">
                                                                            <asp:TextBox ID="txtPatientFName" runat="server"></asp:TextBox>
                                                                        </span>
                                                                    </td>
                                                                    <td width="13%" class="tablecellLabel">
                                                                        <div class="td-PatientInfo-lf-desc-ch" >
                                                                            Middle</div>
                                                                    </td>
                                                                    <td width="25%">
                                                                        <span class="tablecellControl">
                                                                            <asp:TextBox ID="txtMI" runat="server"></asp:TextBox>
                                                                        </span>
                                                                    </td>
                                                                    <td width="5%" class="tablecellLabel">
                                                                        <div class="td-PatientInfo-lf-desc-ch" >
                                                                            Last name
                                                                        </div>
                                                                    </td>
                                                                    <td width="32%">
                                                                        <asp:TextBox ID="txtPatientLName" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span class="tablecellControl">
                                                                            <asp:TextBox Width="69%" ID="txtDateOfBirth" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                MaxLength="10"></asp:TextBox>
                                                                            <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"
                                                                                PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                                                                        </span>
                                                                    </td>
                                                                    <td class="tablecellLabel">
                                                                        <div class="td-PatientInfo-lf-desc-ch" >
                                                                            SSN #
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <span class="tablecellControl">
                                                                            <asp:TextBox ID="txtSocialSecurityNumber" runat="server"></asp:TextBox></span></td>
                                                                    <td class="tablecellLabel">
                                                                        <div class="td-PatientInfo-lf-desc-ch" >
                                                                            Gender</div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSex" runat="server" Width="153px">
                                                                            <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                                                            <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Date of birth</div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Address
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                            &nbsp;</td>
                                                        <td colspan="4">
                                                            <asp:TextBox Width="90%" ID="txtPatientAddress" runat="server"></asp:TextBox>
                                                            <span class="tablecellControl">
                                                                <asp:TextBox Visible="False" ID="txtPatientStreet" runat="server"></asp:TextBox>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="26" class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                City</div>
                                                        </td>
                                                        <td rowspan="3" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="3">
                                                            <div class="lbl">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox>
                                                                            </span>
                                                                        </td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                State</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
                                                                                <extddl:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                                    Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                                                                    OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                            </span>
                                                                        </td>
                                                                        <td width="5%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Zip</div>
                                                                        </td>
                                                                        <td width="32%">
                                                                            <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox>
                                                                            </span>
                                                                        </td>
                                                                        <td class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Work</div>
                                                                        </td>
                                                                        <td>
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>
                                                                            </span>
                                                                        </td>
                                                                        <td class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Extn.</div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtExtension" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkWrongPhone" Visible="false" runat="server" Text="Wrong Phone"
                                                                                TextAlign="Left" /></td>
                                                                        <td class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Email</div>
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <asp:TextBox ID="txtPatientEmail" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Home phone</div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Attorney</div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                            <td colspan="2">
                                                           <%-- <ajaxToolkit:AutoCompleteExtender runat="server" ID="Ajautoattorney" EnableCaching="true"
                                                                DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtAttorneyCompany" 
                                                                ServiceMethod="GetAttorney" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetAttoreyValue">
                                                            </ajaxToolkit:AutoCompleteExtender>--%>
                                                            <ajaxToolkit:AutoCompleteExtender runat="server" MinimumPrefixLength="1" 
                                                                    CompletionInterval="500" ServiceMethod="GetAttorney" 
                                                                    ServicePath="PatientService.asmx" ContextKey="SZ_COMPANY_ID" 
                                                                    UseContextKey="True" DelimiterCharacters="" 
                                                                    OnClientItemSelected="GetAttoreyValue" Enabled="True" 
                                                                    TargetControlID="txtAttorneyCompany" ID="Ajautoattorney"></ajaxToolkit:AutoCompleteExtender>

                                                                <asp:TextBox ID="txtAttorneyCompany" runat="Server" autocomplete="off" Width="75%" OnTextChanged="txtAttorneyCompany_TextChanged" AutoPostBack="true" Visible="false"/>
                                                            <extddl:ExtendedDropDownList ID="extddlAttorney" Width="96%" runat="server" Visible="true"
                                                                Connection_Key="Connection_String" Procedure_Name="SP_MST_ATTORNEY"
                                                                Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                OnextendDropDown_SelectedIndexChanged="extddlAttorney_selectedIndex"
                                                                OldText="" StausText="False" />
                                                                                    
                                                        </td>
                                                        <%-- <td colspan="3">
                                                            <extddl:ExtendedDropDownList ID="extddlAttorney" Width="95%" runat="server" Connection_Key="Connection_String"
                                                                Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                                                OldText="" StausText="False" OnextendDropDown_SelectedIndexChanged="extddlAttorney_selectedIndex" AutoPost_back="true" />
                                                        </td>--%>
                                                        <td>
                                                                <a id="hlnkattedit" href="#" runat="server" title="Attorney Info" class="td-PatientInfo-lf-desc-ch" visible="false" onclick="showAttorneyrFrame()">
                                                                Edit</a>
                                                            <a id="hlnkShowAtornyInfo" href="#" runat="server" title="Attorney Info" class="td-PatientInfo-lf-desc-ch" visible="false">
                                                                Info</a>
                                                            <ajaxToolkit:PopupControlExtender ID="PopupAtornyInfo" runat="server" TargetControlID="hlnkShowAtornyInfo"
                                                                PopupControlID="pnlShowAtornyInfo" OffsetX="-600" OffsetY="-200" DynamicServicePath=""
                                                                Enabled="True" ExtenderControlID="" />
                                                        </td>
                                                    </tr>
                                                                        
                                                                        
                                                        <tr>
                                                        <td colspan="5">
                                                        <table>
                                                            <tr>
                                                        <td class="tablecellLabel">
                                                                               
                                                        </td>
                                                        <td colspan="4">
                                                                                
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                   <%-- <td colspan="5">
                                                        <table>
                                                        <tr>
                                                            <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                Address
                                                                </div>
                                                                </td>
                                                                <td colspan="5">
                                                                        <asp:TextBox ID="txtattorneyaddress" Width="100%" runat="server" CssClass="text-box"
                                                                ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                                <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                City
                                                                </div>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txtattorneycity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                                    <td  class="tablecellLabel">
                                                                        <div class="td-PatientInfo-lf-desc-ch">
                                                                            State</div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtattorneState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    <td class="tablecellLabel">
                                                                        <div class="td-PatientInfo-lf-desc-ch">
                                                                            Zip</div>
                                                                    </td>
                                                                    <td >
                                                                    <asp:TextBox ID="txtattorneyzip" runat="server" CssClass="text-box"
                                                                                ReadOnly="True"></asp:TextBox></td>
                                                        </tr>
                                                                <tr>
                                                            <td>
                                                                    <div class="td-PatientInfo-lf-desc-ch">
                                                                Phone No
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtattorneyphone" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                                                
                                                            <td>
                                                                    <div class="td-PatientInfo-lf-desc-ch">
                                                                Fax
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtattorneyfax" runat="server" CssClass="text-box" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    </td>--%>
                                                                          
                                                                                       
                                                    </tr>  
                                                        </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                &nbsp;Case Type</div>
                                                        </td>
                                                        <td rowspan="2" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="2">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td width="25%" style="height: 16px">
                                                                        <span class="tablecellControl">
                                                                            <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                                OldText="" StausText="False" />
                                                                        </span>
                                                                    </td>
                                                                    <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                        <div class="td-PatientInfo-lf-desc-ch" >
                                                                            Case Status</div>
                                                                    </td>
                                                                    <td width="25%" style="height: 16px">
                                                                        <span class="tablecellControl">
                                                                            <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                                Flag_ID="txtCompanyID.Text.ToString();" OldText="" StausText="False" />
                                                                        </span>
                                                                    </td>
                                                                    <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                        <asp:CheckBox ID="chkTransportation" runat="server" Text="Transport" TextAlign="Left"
                                                                            Visible="false"></asp:CheckBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td></td>
                                                    </tr>
                                                                       
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="lbl">
                                                                &nbsp;<asp:Label ID="lblLocationddl" Text="Location" runat="server" class="td-PatientInfo-lf-desc-ch" ></asp:Label></div>
                                                        </td>
                                                        <td rowspan="2" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="2">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td width="25%" style="height: 16px">
                                                                        <span class="tablecellControl">
                                                                            <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Selected_Text="--- Select ---"
                                                                                OldText="" StausText="False" />
                                                                        </span>
                                                                    </td>
                                                                                        
                                                                                        
                                                                                        
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                        </td>
                                                        <td class="tablecellSpace" rowspan="1">
                                                        </td>
                                                        <td colspan="4" rowspan="1">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td width="25%" style="height: 16px">
                                                                        <span class="tablecellControl">&nbsp;</span></td>
                                                                    <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                        <div class="lbl">
                                                                            &nbsp;&nbsp;</div>
                                                                    </td>
                                                                    <td width="25%" style="height: 16px">
                                                                        <span class="tablecellControl">&nbsp;&nbsp; </span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="lbl">
                                                            </div>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <div class="lbl">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox Width="70%" ID="txtDateofAccident" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                    MaxLength="10" Visible="False"></asp:TextBox>
                                                                                <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif"
                                                                                    Visible="False" />
                                                                                &nbsp;
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateofAccident"
                                                                                    PopupButtonID="imgbtnDateofAccident" Enabled="True" />
                                                                            </span>
                                                                        </td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="lbl">
                                                                            </div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtPlatenumber" Visible="False" runat="server" CssClass="text-box"></asp:TextBox>
                                                                            </span>
                                                                        </td>
                                                                        <td width="5%" class="tablecellLabel">
                                                                            <div class="lbl">
                                                                            </div>
                                                                        </td>
                                                                        <td width="32%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtPolicyReport" Visible="False" runat="server" CssClass="text-box"></asp:TextBox>&nbsp;
                                                                            </span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" nowrap="nowrap">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Employer Information
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Name
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox Width="99%" ID="txtEmployerName" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Address
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                            &nbsp;</td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txtEmployerAddress" runat="server" Width="99%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                City</div>
                                                        </td>
                                                        <td rowspan="2" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="2">
                                                            <div class="lbl">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtEmployerCity" runat="server"></asp:TextBox>
                                                                            </span>
                                                                        </td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                State</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtEmployerState" runat="server" Visible="False"></asp:TextBox>
                                                                                <extddl:ExtendedDropDownList ID="extddlEmployerState" runat="server" Selected_Text="--- Select ---"
                                                                                    Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                                                    Width="90%" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                            </span>
                                                                        </td>
                                                                        <td class="tablecellLabel" width="13%">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Zip</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtEmployerZip" runat="server"></asp:TextBox>
                                                                            </span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtEmployerPhone" runat="server"></asp:TextBox>
                                                                            </span>
                                                                        </td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                &nbsp;<asp:Label ID="lblDateofFirstTreatment" runat="server" Text="Date of First Treatment" style="font-weight:bold;font-size:1.1em;"
                                                                                    class="td-PatientInfo-lf-desc-ch"></asp:Label></div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                           
                                                                            <asp:TextBox ID="txtDateofFirstTreatment" runat="server" Text=""></asp:TextBox>
                                                                            <asp:ImageButton ID="imgbtnDOFT" runat="server" ImageUrl="~/AJAX Pages/Images/cal.gif" />
                                                                            <ajaxToolkit:CalendarExtender ID="claextDOFT" runat="server" TargetControlID="txtDateofFirstTreatment" PopupButtonID="imgbtnDOFT" Enabled="true"  ></ajaxToolkit:CalendarExtender> 

                                                                            </span>
                                                                        </td>
                                                                        <td class="tablecellLabel" style="width: 13%">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                &nbsp;<asp:Label ID="lblChart" runat="server" Text="Chart No." class="td-PatientInfo-lf-desc-ch" style="font-weight:bold;font-size:1.1em;"></asp:Label></div>
                                                                        </td>
                                                                        <td style="width: 25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtChartNo" runat="server"></asp:TextBox></span></td>
                                                                        <td class="tablecellLabel">
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Phone</div>
                                                        </td>
                                                    </tr>
                                                    <!-- End : Data Entry -->
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Insurance Information" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0">
<ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

<TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <asp:Panel style="background-color:white;width:800px;height:550px;" id="Panel1" runat="server"  >
                                            <div align="left" style="height: 340px;">
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                    <!-- Start : Data Entry -->
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Policy Holder
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPolicyHolder" runat="server" CssClass="text-box" Width="61%"></asp:TextBox>
                                                            &nbsp; <a id="lnkSearchInsuranceCompany" href="#" runat="server" title="Search Insurance Company"
                                                                class="td-PatientInfo-lf-desc-ch" visible="false" >Search Insurance Company</a>
                                                            <a id="lnkSearchSecondaryInsuranceCompany" href="javascript:void(0);"  onclick="showSecondaryInsurance()" 
                                                                                class="lbl">Add Secondary Insurance</a>
                                                            <ajaxToolkit:PopupControlExtender ID="peSearchInsuranceCompany" runat="server" TargetControlID="lnkSearchInsuranceCompany"
                                                                PopupControlID="pnlSearchInsuranceCompany" Position="Center" OffsetX="-220" OffsetY="-10" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Name
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:HiddenField ID="hdninsurancecode" runat="server" />
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
                                                           <%-- <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                                DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany" 
                                                                ServiceMethod="GetInsurance" ServicePath="PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                                                            </ajaxToolkit:AutoCompleteExtender>--%>
                                                            <%--<ajaxToolkit:AutoCompleteExtender runat="server" MinimumPrefixLength="1" 
                                                                CompletionInterval="500" ServiceMethod="GetInsurance" 
                                                                ServicePath="PatientService.asmx" ContextKey="SZ_COMPANY_ID" 
                                                                UseContextKey="True" DelimiterCharacters="" 
                                                                OnClientItemSelected="GetInsuranceValue" Enabled="True" 
                                                                TargetControlID="txtInsuranceCompany" ID="ajAutoIns"></ajaxToolkit:AutoCompleteExtender>--%>

                                                                <%--<asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="75%" OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>
                                                            <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server" Visible="false"
                                                                Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged"
                                                                OldText="" StausText="False" />
                                                                <a href="#" id="A1" onclick="showAdjusterPanelAddress()" style="text-decoration: none;">--%>
                                                                <%--<img id="img1" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company Address" /></a>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Ins. Address
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                            &nbsp;</td>
                                                        <td colspan="4">
                                                        <asp:ListBox Width="100%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                                OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Address</div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                City</div>
                                                        </td>
                                                        <td rowspan="2" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="2">
                                                            <div class="lbl">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                State</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="5%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Zip</div>
                                                                        </td>
                                                                        <td width="32%">
                                                                            <asp:TextBox Width="80%" ID="txtInsuranceZip" runat="server" CssClass="text-box"
                                                                                ReadOnly="True"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtInsPhone" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Fax</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtInsFax" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="5%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Contact Person</div>
                                                                        </td>
                                                                        <td width="32%">
                                                                            <asp:TextBox Width="80%" ID="txtInsContactPerson" runat="server" CssClass="text-box"
                                                                                ReadOnly="True"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box"></asp:TextBox></td>
                                                                        <td class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                <asp:Label ID="lblPolicyNumber" class="td-PatientInfo-lf-desc-ch" style="font-weight:bold;font-size:1.1em;"  runat="server"> Policy #</asp:Label></div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtWCBNumber" runat="server" Visible="False"></asp:TextBox>
                                                                            <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="text-box"></asp:TextBox>
                                                                        </td>
                                                                        <td class="td-PatientInfo-lf-desc-ch" >
                                                                                WCB #
                                                                            </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtWCBNo" runat="server"></asp:TextBox>
                                                                            <asp:TextBox Visible="False" Width="99%" ID="txtInsuranceStreet" runat="server" CssClass="text-box"
                                                                                ReadOnly="True"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="26" class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Phone<br />
                                                                Claim/File #
                                                            </div>
                                                        </td>
                                                    </tr> 
                                                    <%--Nirmalkumar--%>   
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                Secondary Insurance Information
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlInsuranceType" runat="server" OnSelectedIndexChanged="ddlInsuranceType_SelectedIndexChanged" AutoPostBack="true"> 
                                                            <asp:ListItem >Select</asp:ListItem>
                                                            <asp:ListItem >Secondary</asp:ListItem>
                                                            <asp:ListItem >Major Medical</asp:ListItem>
                                                            <asp:ListItem >Private</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                Secondary Ins.Name
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4">
                                                        <asp:TextBox ID="txtSecInsName" runat="Server" autocomplete="off" Width="75%" ><%--</asp:TextBox>OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>--%>
                                                        </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                Ins. Address
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                            &nbsp;</td>
                                                        <td colspan="4">
                                                        <asp:TextBox ID="txtSecInsAddress" runat="Server" autocomplete="off" Width="75%" ><%--</asp:TextBox>OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>--%>
                                                        </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                Address</div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox Width="99%" ID="txtSecInsAdd" runat="server" CssClass="text-box"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                City</div>
                                                        </td>
                                                        <td rowspan="2" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="2">
                                                            <div class="lbl">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtInsCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                State</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtInsState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="5%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                Zip</div>
                                                                        </td>
                                                                        <td width="32%">
                                                                            <asp:TextBox Width="80%" ID="txtInsZip" runat="server" CssClass="text-box"
                                                                                ReadOnly="True"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtSecInsPhone" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                Fax</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <asp:TextBox ID="txtSecInsFax" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                        <td width="5%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                                Contact Person</div>
                                                                        </td>
                                                                        <td width="32%">
                                                                            <asp:TextBox Width="80%" ID="txtInsConatactPerson" runat="server" CssClass="text-box"
                                                                                ReadOnly="True"></asp:TextBox></td>
                                                                    </tr> 
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="26" class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch">
                                                                Phone<br />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                                        
                                                    <%--END--%>                                                                        
                                                    <tr>
                                                        <td colspan="2">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Adjuster Information 
                                                            </div>
                                                        </td>
                                                        <td>
                                                                <a href="#" id="hlnlShowAdjuster" onclick="showAdjusterFrame()" style="text-decoration: none;">
                                                                <img id="imgShowAdjuster" src="Images/actionEdit.gif" style="border-style: none;" title="Add Adjuster" /></a>
                                                        </td>
                                                    </tr>
                                                    <!-- Start : Data Entry -->
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Name
                                                            </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td colspan="2">
                                                            <extddl:ExtendedDropDownList ID="extddlAdjuster" Width="100%" runat="server" Connection_Key="Connection_String"
                                                                Selected_Text="--- Select ---" Flag_Key_Value="GET_ADJUSTER_LIST" Procedure_Name="SP_MST_ADJUSTER"
                                                                AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlAdjuster_extendDropDown_SelectedIndexChanged"
                                                                OldText="" StausText="False" />
                                                                                    
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                            <td valign="top" class="class="ContentLabel">
                                                        <%--<asp:LinkButton ID="lnkupdateaduster" runat="server" Text="Update Adjuster" Style="text-align: right;
                                                            font-size: 12px; vertical-align: top;" OnClick="lnkUpdateAdu_Click"></asp:LinkButton>--%>
                                                             <a href="javascript:void(0);" onclick="showUpdateAdjusterFrame('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>','<%# DataBinder.Eval(Container,"DataItem.txtAdjuserID")%>')">
                                                                            Update Adjuster </a>
                                                        </td> 
                                                    </tr>
                                                    <tr>
                                                        <td class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Phone</div>
                                                        </td>
                                                        <td rowspan="2" class="tablecellSpace">
                                                        </td>
                                                        <td colspan="4" rowspan="2">
                                                            <div class="lbl">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtAdjusterPhone" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                        <td width="13%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Extension</div>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtAdjusterExtension" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                        <td width="5%" class="tablecellLabel">
                                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                                Fax</div>
                                                                        </td>
                                                                        <td width="32%">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox ID="txtfax" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="height: 37px">
                                                                            <span class="tablecellControl">
                                                                                <asp:TextBox Width="98%" ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></span>
                                                                            <div class="lbl">
                                                                            </div>
                                                                        </td>
                                                                        <td class="tablecellLabel" style="height: 37px">
                                                                            &nbsp;</td>
                                                                        <td style="height: 37px">
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="26" class="tablecellLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                                Email</div>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td class="ContentLabel">
                                                            <div class="td-PatientInfo-lf-desc-ch" >
                                                            <asp:Label ID="lblassociate" Text="Associate cases" runat = "server" class="td-PatientInfo-lf-desc-ch" ></asp:Label>                                                                                </div>
                                                        </td>
                                                        <td class="tablecellSpace">
                                                        </td>
                                                        <td colspan="3">
                                                            <span class="tablecellControl">
                                                                <asp:TextBox ID="txtAssociateCases" runat="server"></asp:TextBox>
                                                                <asp:Button ID="btnAssociate" runat="server" OnClick="btnAssociate_Click" Text="Associate Cases"
                                                                    Width="105px" CssClass="Buttons" /></span>
                                                                    <asp:Button ID="btnDAssociate" runat="server" OnClick="btnDAssociate_Click" Text="DeAssociate Cases"
                                                                    Width="120px" CssClass="Buttons" Visible="false" />
                                                                    <asp:CheckBox ID = "btassociate" runat ="server" Text="Do not Update ins data "  Visible="false"/>
                                                                    </td>
                                                    </tr>
                                                    <!-- End : Data Entry -->
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Accident Information" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0">
<ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

<TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <asp:Panel style="background-color:white;width:800px;height:450px;" id="Panel2" runat="server"  >
                                            <div align="left" style="height: 280px;">
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Accident Date
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox Width="70%" ID="txtATAccidentDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                            <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="txtATAccidentDate"
                                                                PopupButtonID="imgbtnATAccidentDate" Enabled="True" />
                                                        </td>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Plate Number
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox ID="txtATPlateNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Report Number
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox ID="txtATReportNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Address</td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox ID="txtATAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            City
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox ID="txtATCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            State</td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <extddl:ExtendedDropDownList ID="extddlATAccidentState" runat="server" Width="90%"
                                                                Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                                Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Hospital name
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Hospital Address</td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox ID="txtATHospitalAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Date of admission
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:TextBox Width="70%" ID="txtATAdmissionDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                            <asp:ImageButton ID="imgbtnATAdmissionDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                            <ajaxToolkit:CalendarExtender ID="calATAdmissionDate" runat="server" TargetControlID="txtATAdmissionDate"
                                                                PopupButtonID="imgbtnATAdmissionDate" Enabled="True" />
                                                        </td>
                                                        <td class="ContentLabel" style="width: 15%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Additional Patients
                                                        </td>
                                                        <td style="width: 35%" colspan="3">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="7%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="93%">
                                                                        <asp:TextBox ID="txtATAdditionalPatients" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Describe injury
                                                        </td>
                                                        <td style="width: 35%" colspan="3">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="7%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td width="93%">
                                                                        <asp:TextBox ID="txtATDescribeInjury" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Patient Type
                                                        </td>
                                                        <td colspan = "3" width="35%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="7%">
                                                                &nbsp;
                                                                </td>
                                                                <td width="93%">
                                                                    <asp:RadioButtonList ID="rdolstPatientType" runat="server" RepeatDirection="Horizontal" class="td-PatientInfo-lf-desc-ch" >
                                                                        <asp:ListItem Value = "0">Bicyclist</asp:ListItem>
                                                                        <asp:ListItem Value = "1">Driver</asp:ListItem>
                                                                        <asp:ListItem Value = "2">Passenger</asp:ListItem>
                                                                        <asp:ListItem Value = "3">Pedestrian</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:TextBox ID="txtPatientType" runat="server" Visible = "false" Width="2%"></asp:TextBox>
                                                                </td>
                                                                <asp:TextBox ID="txtPatientAge" runat="server" onkeypress="return clickButton1(event,'')"
                                                                                                    MaxLength="10" Visible="False"></asp:TextBox>
                                                            </tr>
                                                        </table>
                                                                                
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Attorney Information" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0" Visible="false">
<ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

<TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <asp:Panel style="background-color:white;width:800px;height:450px;" id="Panel3" runat="server"  >
                                            <div align="left" style="height: 280px;">
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                            Attorney
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <extddl:ExtendedDropDownList ID="extddlAttorneyAssign" Width="95%" runat="server" Connection_Key="Connection_String"
                                                                Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                                                OldText="" StausText="False"/>
                                                        </td>
                                                        <td style="width:35%;" class="class="ContentLabel" >
                                                               
                                                                <asp:LinkButton ID="lnkAddAttorney" runat="server" Text="Add Attorney" Style="text-align: right;
                                                                    font-size: 12px; vertical-align: top;" OnClick="lnkAddAttorney_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp; 
                                                                <asp:LinkButton ID="lnkUpdateAtt" runat="server" Text="Update Attorney" Style="text-align: right;
                                                            font-size: 12px; vertical-align: top;" OnClick="lnkUpdateAtt_Click"></asp:LinkButton>             
                                                        </td>
                                                        <td >
                                                                        
                                                        </td>
                                                                                
                                                    </tr>
                                                            <tr style="height:10px;">
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%" colspan="4">
                                                                             
                                                        </td>
                                                                         
                                                                                
                                                    </tr>
                                                        <tr style="height:10px;">
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                             
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:CheckBox ID="chkAttorneyAssign" runat="server" Text="Allow to Access Documents" />
                                                        </td>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                              
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                        </td>
                                                                                
                                                    </tr>
                                                        <tr style="height:10px;">
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%" colspan="4">
                                                                             
                                                        </td>
                                                                          
                                                                                
                                                    </tr>
                                                    <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                             
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                            <asp:Button ID="btnAttorneyAssign"  runat="server" Text="Assign" OnClick="btnAttorneyAssign_Click"/>
                                                        </td>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%">
                                                                              
                                                        </td>
                                                        <td style="width: 35%" class="ContentLabel">
                                                        </td>
                                                                                
                                                    </tr>
                                                                     
                                                        <tr>
                                                        <td class="td-PatientInfo-lf-desc-ch" style="width: 15%" colspan="4">
                                                                            
                                                            <%--<asp:UpdatePanel ID="UP_grdAttorney" runat="server">--%>
                                                                <ContentTemplate>
                                                                    <table width="100%">
                                                                    <tr>
                                                                        <td align="right">
                                                                        <asp:Button ID="btndeleteAtt" runat="server" OnClick="btndeleteAtt_Click"  Text="Delete" />
                                                                        </td>
                                                                    </tr>
                                                                        <%--<tr>
                                                                            <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                                                                <b class="txt3">Attorney List</b>
                                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdAttorney"
                                                                                    DisplayAfter="10" DynamicLayout="true">
                                                                                    <progresstemplate>
                                                                                <div id="Div2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                    runat="Server">
                                                                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                    Loading...</div>
                                                                            </progresstemplate>
                                                                                </asp:UpdateProgress>
                                                                            </td>
                                                                        </tr>--%>
                                                                    </table>
                                                                        <table style="vertical-align: middle; width: 100%;">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="vertical-align: middle; width: 30%" align="left">
                                                                                        Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                            CssClass="search-input">
                                                                                        </gridsearch:XGridSearchTextBox>
                                                                                                          
                                                                                    </td>
                                                                                    <td style="width: 60%" align="right">
                                                                                        Record Count:
                                                                                        <%= this.grdAttorney.RecordCount%>
                                                                                        | Page Count:
                                                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                        </gridpagination:XGridPaginationDropDown>
                                                                                                           
                                                                                    </td>
                                                                                </tr>
                                                                                                                                                                                </tbody>
                                                                        </table>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <xgrid:XGridViewControl ID="grdAttorney" runat="server" Width="1020px" CssClass="mGrid" DataKeyNames="SZ_CASE_ID,SZ_PATIENT_ID,SZ_ATTORNEY_ID,SZ_COMPANY_ID,ID,ATTORNEY_TYPE_ID" MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                                                            
                                                                                        ShowExcelTableBorder="true"
                                                                                        AllowPaging="true" XGridKey="GetAttorneyAssignList" PageRowCount="5" PagerStyle-CssClass="pgr"
                                                                                        AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
<AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                                        <Columns>
                                                                                            <%--0--%>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_ID)" headertext="Case ID"
                                                                                                DataField="SZ_CASE_ID" visible="false" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                                <%--1--%>    
                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                            SortExpression="convert(int,MST_CASE_MASTER.SZ_CASE_NO)" headertext="Case #"
                                                                                            DataField="SZ_CASE_NO" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--2--%>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                SortExpression="MST_PATIENT.SZ_PATIENT_FIRST_NAME" headertext="Patient Name"
                                                                                                DataField="PATIENT_NAME" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--3--%>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                SortExpression="MST_ATTORNEY.SZ_ATTORNEY_FIRST_NAME" 
                                                                                                headertext="Attorney Name" DataField="ATTORNEY_NAME" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--4--%>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                headertext="Patient ID" DataField="SZ_PATIENT_ID" visible="false" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--5--%>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                headertext="Attorney ID" DataField="SZ_ATTORNEY_ID" visible="false" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--6--%>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                SortExpression="MST_ATTORNEY.SZ_ATTORNEY_ADDRESS"
                                                                                                headertext="Attorney Address" DataField="SZ_ATTORNEY_ADDRESS" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--7--%>   
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                SortExpression="MST_USERS.SZ_USER_NAME"
                                                                                                headertext="User Name" DataField="SZ_USER_NAME" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--8--%>
                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                headertext="Company ID" DataField="SZ_COMPANY_ID" visible="false" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--9--%>
                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                headertext="ID" DataField="ID" visible="false" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--10--%>
                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                headertext="ATTORNEY TYPE ID" DataField="ATTORNEY_TYPE_ID" 
                                                                                                visible="false" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <%--11--%>
                                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                                headertext="ATTORNEY TYPE NAME" DataField="ATTORNEY_TYPE_NAME" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                                <%--12--%>   
                                                                                                <asp:TemplateField>
                                                                                                <headertemplate>
                                                                                                    <center> <asp:CheckBox ID="chkSelectAllAtt" runat="server" onclick="javascript:SelectAllAtt(this.checked);"  ToolTip="Select All" /> </center> 
                                                                                                </headertemplate>
                                                                                                <itemtemplate >
                                                                                                <center> <asp:CheckBox ID="chkDeleteAtt" runat="server" /> </center> 
                                                                                                </itemtemplate>
                                                                                            </asp:TemplateField>
                                                                                                                   
                                                                                                                   
                                                                                            </Columns>

<HeaderStyle CssClass="GridViewHeader"></HeaderStyle>

<PagerStyle CssClass="pgr"></PagerStyle>
                                                                                    </xgrid:XGridViewControl>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                            </ContentTemplate>
                                                            <%--<Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="con" />
                                                                                    
                                                            </Triggers>
                                                        </asp:UpdatePanel>--%>
                                                    </td>
                                                                           
                                                                                
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="View All" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%" ActiveTabStyle-BackColor="White"
                                Name="case" TabStyle-BackColor="#B1BEE0" Visible="false">
                            <ActiveTabStyle BackColor="White" Font-Bold="True"></ActiveTabStyle>

                            <TabStyle Width="100%" BackColor="#B1BEE0"></TabStyle>
                                <ContentCollection>
                                    <dx:ContentControl>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DataList ID="DtlView" runat="server" CssClass="TDPart" BorderWidth="0px" BorderStyle="None" 
                                                                        BorderColor="#DEBA84" RepeatColumns="1" Width="100%">
                                                                        <ItemTemplate>
                                                                        <table id="lastTablel" runat="server" class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0" border="1" bgcolor="white">
                                                                        <tr>
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Personal Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    First Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Middle Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label ID="lblViewMiddleName" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Last Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    D.O.B
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_DOB") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Gender
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label runat="server" ID="lblViewGender" CssClass="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewPatientState" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Home Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Work
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    ZIP
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    &nbsp;
												                                                                    <asp:CheckBox ID="chkViewWrongPhone" Visible="False" Enabled="False" runat="server" Text="Wrong Phone" TextAlign="Left" />
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Email
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Extn.
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Attorney
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label ID="lblViewAttorney" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Case Type
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label ID="lblViewCasetype" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Case Status
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewCaseStatus" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    SSN
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>
                                                                                                                </td>
                                                                                                            </tr>
										                                                <tr>
										    	                                              <td class="td-CaseDetails-lf-desc-ch">
											  	                                            <asp:Label ID="lblLocation" Text="Location" runat="server" class="lbl" Style="font-weight: bold;"></asp:Label>
											                                              </td>
											                                              <td class="td-CaseDetails-lf-data-ch">
												                                            <asp:Label runat="server" ID="lblVLocation1" class="lbl" ></asp:Label>
											                                              </td>
											                                              <td class="td-CaseDetails-lf-desc-ch"><asp:CheckBox ID="chkPatientTransport" Visible="False" Enabled="False" 													
											                                              	runat="server" Text="Transport" TextAlign="Left"></asp:CheckBox></td>
											                                               <td class="td-CaseDetails-lf-data-ch">&nbsp;</td>
											                                              </tr>
											                                                     <tr>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            Copy From
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch" colspan="3">
                                                                                                                            <asp:Label runat="server" ID="lblcopyfrom" class="lbl"></asp:Label>
                                                                                                                        </td>
                                                                                                                       <%-- <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;</td>--%>
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
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Insurance Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Policy Holder
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem,"SZ_POLICY_HOLDER") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Ins. Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label ID="lblViewInsuranceAddress" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ADDRESS") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_CITY") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_STATE") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    ZIP
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ZIP") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_PHONE")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                                                    FAX
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX_NUMBER")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                                                    Contact Person
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_CONTACT_PERSON")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Claim File#
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                 <%--<a id="lnkViewInsuranceClaimNumber" style="text-decoration: underline; color:Blue;" runat="server" class="lbl"  
                                                                                                                 href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+claimno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%></a>--%>
                                                                                                                   <%--<ajaxToolkit:PopupControlExtender ID="popExtViewInsuranceClaimNumber" runat="server"  TargetControlID="lnkViewInsuranceClaimNumber"
                                                                                                                   PopupControlID="pnlClaimNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID=""  />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Policy #
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%--<a id="lnkViewPolicyNumber" style="text-decoration: underline; color: Blue;" runat="server" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+policyno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%> </a>--%>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtViewPolicyNumber" runat="server" TargetControlID="lnkViewPolicyNumber" 
                                                                                                                    PopupControlID="pnlPolicyNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
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
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Accident Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Accident Date
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%--<a id="lnkDateOfAccList" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+dtaccident+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%></a>--%>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtDateOfAccList" runat="server" TargetControlID="lnkDateOfAccList" 
                                                                                                                    PopupControlID="pnlDateOfAccList" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Plate Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%--<a id="lnkViewAccidentPlatenumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+plateno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' ><%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%></a>--%>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentPlatenumber" runat="server" TargetControlID="lnkViewAccidentPlatenumber" 
                                                                                                                    PopupControlID="pnlPlateNo" OffsetX="-100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Report Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                <%--<a id="lnkViewAccidentReportNumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+accidentreportno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%></a>--%>
                                                                                                                <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentReportNumber" runat="server" TargetControlID="lnkViewAccidentReportNumber" 
                                                                                                                PopupControlID="pnlReportNO" OffsetX="-300" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_CITY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewAccidentState" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Hospital Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_NAME")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Hospital Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Date Of Admission
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_ADMISSION_DATE")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Additional Patient
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FROM_CAR")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Describe Injury
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_DESCRIBE_INJURY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Patient Type
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblPatientType" class="lbl"></asp:Label>
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
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Employer Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_NAME")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_CITY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewEmployerState" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    ZIP
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ZIP")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_PHONE")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Date Of First Treatment
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_FIRST_TREATMENT")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    <asp:Label ID="lblView" runat="server" Text="Chart No." class="lbl" Font-Bold="true"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label ID="lblViewChartNo" runat="server" class="lbl"></asp:Label>
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
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;" id="tblF">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 500px;">
                                                                                            &nbsp;<b class="txt3">Adjuster Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%--<a id="lnkViewAdjusterName" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+adjustername+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' >--%>
                                                                                                                    <asp:Label runat="server" ID="lblViewAdjusterName" class="lbl"></asp:Label>
                                                                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>--%></a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAdjusterName" runat="server" TargetControlID="lnkViewAdjusterName" 
                                                                                                                    PopupControlID="pnlAdjuster" OffsetX="100" OffsetY="-300" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            Address
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_ADDRESS")%>
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            City
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_CITY")%>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            State
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_STATE")%>
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                            Zip
                                                                                                                        </td>
                                                                                                                        <td class="td-CaseDetails-lf-data-ch">
                                                                                                                            &nbsp;
                                                                                                                            <%# DataBinder.Eval(Container.DataItem, "SZ_ZIP")%>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PHONE")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Extension
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EXTENSION")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    FAX
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Email
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
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
                                                                       
                                                                            
                                                                        </tr>
                                                                    </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:aspxpagecontrol>
                 </td>
            </tr>
            <tr>
                <dx:ASPxPopupControl ID="AddAdjusterPop" runat="server" CloseAction="CloseButton"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="AddAdjusterPop" HeaderText="Adjuster Information" HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
                    EnableViewState="True" Width="525px" ToolTip="Add Adjuster" PopupHorizontalOffset="0"
                    PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
                    RenderIFrameForPopupElements="Default" Height="400px">

                       <ClientSideEvents CloseButtonClick="ClosePopup" />
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>
                <dx:ASPxPopupControl ID="SecInsuracePop" runat="server" CloseAction="CloseButton"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ClientInstanceName="SecInsuracePop" HeaderText="Secondary Insurance Information" HeaderStyle-HorizontalAlign="Left"
                    HeaderStyle-BackColor="#B5DF82" AllowDragging="True" EnableAnimation="False"
                    EnableViewState="True" Width="900px" ToolTip="Patient Information" PopupHorizontalOffset="0"
                    PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
                    RenderIFrameForPopupElements="Default" Height="400px">
                    <ClientSideEvents CloseButtonClick="ClosePopup1" />
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>
                <td class="ContentLabel" colspan="6">
                    <asp:TextBox ID="txtCompanyIDForNotes" runat="server" Visible="false" Width="10px"></asp:TextBox>
                    <asp:TextBox ID="txtCompanyID" runat="server" Style="visibility:hidden;" Width="10px"></asp:TextBox>
                    <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Style="visibility:hidden;"></asp:TextBox>
                    <asp:Button ID="btnPatientUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                        OnClick="btnPatientUpdate_Click" />
                    <asp:Button ID="btnPatientClear" runat="server" Text="Clear" Width="80px" CssClass="Buttons" />
                    <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                    <asp:TextBox ID="txtAdjusterid" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox Width="70%" ID="txtDateOfInjury" runat="server" onkeypress="return clickButton1(event,'/')" MaxLength="10" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtJobTitle" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtWorkActivites" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCarrierCaseNo" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtAssociateDiagnosisCode" runat="server" Visible="False" />
                    <asp:TextBox ID="txtAccidentID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                    <asp:TextBox Width="99%" ID="txtAccidentAddress" runat="server" CssClass="text-box" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtAccidentCity" runat="server" CssClass="text-box" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtAccidentState" runat="server" CssClass="text-box" Visible="false"></asp:TextBox>
                    <asp:TextBox Height="100px" Width="98%" ID="txtListOfPatient" runat="server" TextMode="MultiLine" MaxLength="250" Visible="false"></asp:TextBox>

                    
                    <asp:HiddenField ID="hdadjusterCode" runat="server" />
                    <asp:HiddenField ID="hdnattorney" runat="server" />
                   <%-- <asp:Panel ID="pnlSearchInsuranceCompany" runat="server" Style="width:220px;height:100px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
                        <table width="100%">
                            <tr>
                                <td width="30%"> <div class="lbl">Code</div></td>
                                <td width="70%"><asp:TextBox ID="txtSearchCode" runat="server" width="80%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td width="30%"> <div class="lbl">Name</div></td>
                                <td width="70%"><asp:TextBox ID="txtSearchName" runat="server" width="80%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnSearchInsCompany" runat="server" Text="Search" CssClass="Buttons" OnClick="btnSearchInsCompany_Click"/>
                                </td>
                            </tr>
                        </table>       
                    </asp:Panel>--%>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" Visible="false">
        <contenttemplate>
            <asp:Panel ID="pnlShowNotes" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="Iframe2" src="../Bill_Sys_PopupNotes.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
        
            <asp:Panel ID="pnlShowAtornyInfo" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="Iframe1" src="../Bill_Sys_PopupAttorny.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
         
         <asp:Panel ID="pnlShowReminder" runat="server" Style="width:1100px;height:420px;">
            <iframe id="Iframe3" src="Bill_Sys_Case_Reminder.aspx" height="420px" width="1100px" frameborder="0" 
            visible="false" class="iframe">
            
            
            </iframe>
                   
         </asp:Panel>
         
          <asp:Panel ID="pnlSearchInsuranceCompany" runat="server" Style="width:220px;height:100px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <table width="100%">
                <tr>
                    <td width="30%"> <div class="lbl">Code</div></td>
                    <td width="70%"><asp:TextBox ID="txtSearchCode" runat="server" width="80%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="30%"> <div class="lbl">Name</div></td>
                    <td width="70%"><asp:TextBox ID="txtSearchName" runat="server" width="80%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearchInsCompany" runat="server" Text="Search" CssClass="Buttons" OnClick="btnSearchInsCompany_Click"/>
                    </td>
                </tr>
            </table>       
         </asp:Panel>
        
        
        </contenttemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
