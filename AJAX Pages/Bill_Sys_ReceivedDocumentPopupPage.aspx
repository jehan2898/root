<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ReceivedDocumentPopupPage.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_ReceivedDocumentPopupPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Received Document Popup</title>
    <script type="text/javascript" src="../Registration/validation.js"></script>
    <%--<link href="Css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../js/lib/jquery-1.11.2.min.js"></script>
    <script src="../js/lib/PaidBills.js" type="text/javascript"></script>
    <style>
        .hiddencol
        {
            display: none;
        }
        .page-title
        {
            font-family: Cambria;
            font-size: 18px;
            background-color: #336699;
            width: 100%;
            margin-top: 20px;
            color: White;
            min-height: 32px;
        }
        .GridTable
        {
            border: #8babe4 1px solid;
        }
        
        .GridHeader
        {
            font-weight: bold;
            font-size: 12px;
            font-family: arial;
            background-color: #8babe4;
        }
        
        .GridHeader th
        {
            padding: 3px;
        }
        
        .GridRow
        {
            border: #8babe4 1px solid;
            background-color: #fff;
        }
        
        .GridRow td
        {
            border: #8babe4 1px solid;
            padding: 2px;
            font-size: 12px;
            font-family: arial;
        }
        
        .SectionDevider
        {
            height: 10px;
        }
        
        .ui-widget
        {
            position: absolute;
        }
    </style>
    <style type="text/css">
        .ajax__tab_theme .ajax__tab_header
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
        }
        
        .ajax__tab_theme1 .ajax__tab_header1
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
     <script type="text/javascript">
         function CheckSelectedDGCodes() {
             debugger;
             var SeletedDGCodes = document.getElementById('<%=hdnDiagnosis.ClientID%>');
             if (SeletedDGCodes.value == "") {
                 alert("Please Select Diagnosis Code");
                 return false;
             }
             else {
                 return true;
             }
         }
    </script>
    <div>
        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True"
            ForeColor="Red"></asp:Label>
    </div>
    <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="2"
        Height="520px" Width="100%">
        <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0" CssClass="ajax__tab_header">
            <HeaderTemplate>
                <div style="width: 200px; height: 20px; font-weight: bold; font-size: small;">
                    Associate Diagnosis Code
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <div class="container">
                    <asp:ScriptManager ID="scriptmanager1" runat="server" AsyncPostBackTimeout="36000">
                    </asp:ScriptManager>
                    <div style="width: 100%">
                        <div style="width: 48%; float: left; margin-right: 20px">
                            <div class="page-title" style="padding-left: 15px; margin-bottom: 10px">
                                Search
                            </div>
                            <div class="col-md-6">
                                <table>
                                    <tr style="padding: 10px">
                                        <td>
                                            <div class="col-md-4">
                                                Type :
                                            </div>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rbl_SZ_TYPE_ID" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rbl_SZ_TYPE_ID_OnSelectedIndexChanged">
                                                <asp:ListItem Text="ICD10" Value="ICD10" Selected="True" style="font-family: Cambria;
                                                    font-weight: 900; font-size: smaller"></asp:ListItem>
                                                <asp:ListItem Text="ICD9" Value="ICD9" style="font-family: Cambria; font-weight: 900;
                                                    font-size: smaller"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr style="padding: 10px">
                                        <td>
                                            <div class="col-md-4">
                                                Search :
                                            </div>
                                        </td>
                                        <td>
                                            
                                            <asp:TextBox ID="txtSearchDignosisCode" runat="server" CssClass="search-input "></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajDignosisCode" EnableCaching="true"
                                                DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtSearchDignosisCode"
                                                ServiceMethod="GePopuptDignosisCodes" ServicePath="~/AJAX Pages/PatientService.asmx"
                                                UseContextKey="true" ContextKey="SZ_COMPANY_ID">
                                            </ajaxToolkit:AutoCompleteExtender>
                                            <asp:LinkButton ID="lnkAddDignosisCode" Text="Select" runat="server"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr style="padding: 10px">
                                        <td>
                                        </td>
                                        <td>
                                            <div class="col-md-4" style="font-size: medium; padding-left: 50px; font-weight: bold;
                                                color: Red; font-family: Cambria">
                                                OR
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="col-md-offset-2 col-md-4">
                                                Dignosis Type :
                                            </div>
                                        </td>
                                        <td>
                                            <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="173px"
                                                Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                Flag_Key_Value="DIAGNOSIS_TYPE_LIST"></extddl:ExtendedDropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height: 5px">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="col-md-4">
                                                Code :
                                            </div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="173px" MaxLength="50">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 5px">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="col-md-4">
                                                Description :
                                            </div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDescription" runat="server" Width="173px" MaxLength="50">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 5px">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="btn-primary"
                                                            OnClick="btnSeacrh_Click" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAssign" runat="server" Text="Assign" Width="80px" CssClass="btn-primary"
                                                OnClientClick="if(!CheckSelectedDGCodes()) return false;" OnClick="btnAssign_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-6">
                                <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdPatientSearch"
                                                        DisplayAfter="10" DynamicLayout="true">
                                                        <ProgressTemplate>
                                                            <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Loading...
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                        DisplayAfter="10" DynamicLayout="true">
                                                        <ProgressTemplate>
                                                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Loading...
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="row" style="margin-top: 30px">
                                            <%--AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"--%>
                                            <div style="height: 200px; background-color: Gray; overflow-y: scroll;">
                                                <asp:DataGrid ID="grdDiagonosisCode" runat="server" Width="99%" CssClass="GridTable"
                                                    AutoGenerateColumns="false">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkAssociateDiagnosisCode" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="sz_diagnosis_type" HeaderText="DIAGNOSIS TYPE" ItemStyle-CssClass="hiddencol"
                                                            HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID"
                                                            ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION" Visible="true">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_TYPE_ID" HeaderText="COMPANY" Visible="False">
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </asp:DataGrid>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="overflow: auto; height: auto; width: 100%;">
                                &nbsp;<asp:DataGrid Width="100%" ID="grdSelectedDiagnosisCode" CssClass="GridTable"
                                    runat="server" AutoGenerateColumns="False" Visible="false">
                                    <ItemStyle CssClass="GridRow" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                            Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="GridHeader" />
                                </asp:DataGrid>
                            </div>
                            <div style="display: none">
                                <div id="proc" runat="server" style="overflow: scroll; height: 150px; width: 100%;">
                                    <asp:DataGrid Width="100%" ID="grdProCode" runat="server" CssClass="GridTable" AutoGenerateColumns="False"
                                        Visible="false">
                                        <ItemStyle CssClass="GridRow" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CODE" HeaderText="Procedure CodeID" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Procedure Code"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectProc" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeader" />
                                        <PagerStyle Mode="NumericPages" />
                                    </asp:DataGrid>
                                </div>
                                <div style="overflow: auto; height: auto; width: 100%;">
                                    <asp:DataGrid ID="grdNormalDgCode" runat="server" AutoGenerateColumns="False" CssClass="GridTable"
                                        OnPageIndexChanged="grdNormalDgCode_PageIndexChanged" OnSelectedIndexChanged="grdNormalDgCode_SelectedIndexChanged"
                                        Width="99%">
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID"
                                                Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <ItemStyle CssClass="GridRow" HorizontalAlign="Left" />
                                    </asp:DataGrid>
                                </div>
                                <table>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" Width="104px" CssClass="Buttons"
                                                OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div style="width: 48%; float: right">
                            <div class="page-title" style="padding-left: 8px; margin-bottom: 10px">
                                Selected code(s)
                            </div>
                            <div class="row">
                                <div id="headerInsurance" style="text-align: left">
                                </div>
                                <br />
                                <div id="divDiagnosis" runat="server" style="padding-left: 0px">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1" CssClass="ajax__tab_theme">
            <HeaderTemplate>
                <div style="width: 250px; height: 20px; font-weight: bold; font-family: verdana,tahoma,helvetica;
                    font-size: 11px;">
                    De-Associate Diagnosis Code
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <div>
                    <asp:DataGrid Width="100%" ID="grdAssignedDiagnosisCode" runat="server" CssClass="GridTable"
                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </div>
                <td>
                    <div align="center" style="padding-top: 10px">
                        <asp:Button ID="btnDeassociateDiagCode" runat="server" Text="De-Associate" Width="104px"
                            OnClick="btnDeassociateDiagCode_Click" />
                    </div>
                </td>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" TabIndex="1" CssClass="ajax__tab_theme">
            <HeaderTemplate>
                <div style="width: 250px; height: 20px; font-weight: bold; font-family: verdana,tahoma,helvetica;
                    font-size: 11px;">
                    Upload Section
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td class="ContentLabel" style="text-align: left; height: 20px;" align="center">
                                <asp:Label CssClass="message-text" ID="Label1" runat="server" Visible="False" Font-Bold="True"
                                    ForeColor="Red"></asp:Label>
                                <div id="ErrorDiv" style="color: red;" visible="true">
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                Reading Doctor :
                            </td>
                            <td>
                                <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="150px"
                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_READINGDOCTOR" Selected_Text="---Select---"
                                    Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();" OldText="" StausText="False" />
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Upload Report :
                            </td>
                            <td>
                                <asp:FileUpload ID="fuUploadReport" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 10px">
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" OnClick="btnUploadFile_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons"
                                    OnClick="btnCancel_Click" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <div>
        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px">
        </asp:TextBox>
        <asp:HiddenField ID="hdnDiagnosis" runat="server"></asp:HiddenField>
        <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
        <asp:TextBox ID="txtDiagnosisSetID" runat="server" Visible="false" Width="10px"></asp:TextBox>
        <asp:Label ID="lblDiagnosisCount" Font-Bold="true" runat="server" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
