<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dignosis_Code_Visit_Popup.aspx.cs"
    Inherits="AJAX_Pages_Dignosis_Code_Visit_Popup" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="Css/StyleSheet.css" />
    <link href="Css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/lib/jquery-1.11.2.min.js"></script>
    <script src="../js/lib/DignosisCode.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        function ReloadParent() {
            parent.location.reload();
        }
    </script>
    <script type="text/javascript">
        function SubmitDGCode() {
            debugger;
            var array = $('#hdnDiagnosis').val();
            if (array != "") {
                window.parent.fncParent(array);
            }
            else {
                alert("Please Select Diagnosis Code");
                return false;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function OnDiagnosisSelected(s, e) {
            alert(s.GetSelectedItem().texts[0]);
            alert(s.GetSelectedItem().texts[1]);
            alert(s.GetSelectedItem().texts[2]);
            alert(s.GetSelectedItem().texts[3]);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <asp:ScriptManager ID="scriptmanager1" runat="server" AsyncPostBackTimeout="36000">
        </asp:ScriptManager>
        <div style="width: 100%">
            <div style="width: 48%; float: left; margin-right: 20px">
                <div class="page-title" style="padding-left: 15px; margin-bottom: 10px">
                    Search</div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <div>
                                    Type :
                                </div>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbl_SZ_TYPE_ID" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="ICD10" Value="ICD10" Selected="True" style="font-family: Cambria;
                                        font-weight: 900; font-size: smaller"></asp:ListItem>
                                    <asp:ListItem Text="ICD9" Value="ICD9" style="font-family: Cambria; font-weight: 900;
                                        font-size: smaller"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    Search :</div>
                            </td>
                            <td>
                                <%--<dx:ASPxComboBox ID="cmbDiagnosisCodes" runat="server" Width="370px" DropDownWidth="550"
                                    DropDownStyle="DropDownList" ValueField="sz_diagnosys_code_id" ValueType="System.String"
                                    TextFormatString="{0} {1} {2} {3}" EnableCallbackMode="true" FilterMinLength="3"
                                    IncrementalFilteringMode="Contains" CallbackPageSize="30">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Type" FieldName="sz_diagnosys_code_id" Width="80px" />
                                        <dx:ListBoxColumn Caption="Type" FieldName="sz_diagnosis_type" Width="80px" />
                                        <dx:ListBoxColumn Caption="ICD 9 Code" FieldName="sz_icd9_code" Width="70px" />
                                        <dx:ListBoxColumn Caption="ICD 9 Description" FieldName="sz_icd9_description" Width="170px" />
                                        <dx:ListBoxColumn Caption="ICD 10 Code" FieldName="sz_icd10_code" Width="70px" />
                                        <dx:ListBoxColumn Caption="ICD 10 Description" FieldName="sz_icd10_description" Width="0px" />
                                    </Columns>
                                    <ClientSideEvents ValueChanged="function(s, e) {OnDiagnosisSelected(s,e);}" />
                                </dx:ASPxComboBox>--%>
                                <asp:TextBox ID="txtSearchDignosisCode" runat="server" CssClass="search-input "></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajDignosisCode" EnableCaching="true"
                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtSearchDignosisCode"
                                    ServiceMethod="GetDignosisCodes" ServicePath="PatientService.asmx" UseContextKey="true"
                                    ContextKey="SZ_COMPANY_ID">
                                </ajaxToolkit:AutoCompleteExtender>
                                <asp:LinkButton ID="lnkAddDignosisCode" Text="Select" runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <div style="font-size: medium; padding-left: 50px; font-weight: bold;
                                    color: Red; font-family: Cambria">
                                    OR
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    Dignosis Type :</div>
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
                                <div>
                                    Code :</div>
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
                                <div>
                                    Description :</div>
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
                                <asp:UpdatePanel ID="UP_Search" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="btn-primary"
                                                OnClick="btnSeacrh_Click" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                &nbsp&nbsp&nbsp <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" CssClass="btn-primary"
                                    OnClientClick="SubmitDGCode(); return false" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
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
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UP_Search"
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
                                <%--AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages"   OnPageIndexChanged="grdDiagonosisCode_PageIndexChanged"--%>
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
            </div>
            <div style="width: 48%; float: right">
                <div class="page-title" style="padding-left: 15px; margin-bottom: 10px">
                    Selected code(s)</div>
                <div class="row">
                    <div id="headerInsurance" style="text-align: left">
                    </div>
                    <br />
                    <div id="divDiagnosis" runat="server">
                        <table id="tblDignosis" cellpadding="20" cellspacing="20">
                        </table>
                        <ul id="ulInsurance" style="list-style: none; text-align: left">
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px">
        </asp:TextBox>
        <asp:HiddenField ID="hdnDiagnosis" runat="server"></asp:HiddenField>
    </div>
    </form>
</body>
</html>
