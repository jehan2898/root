<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateDignosisCodeCase.aspx.cs"
    Inherits="Bill_Sys_AssociateDignosisCodeCase" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="AJAX Pages/Css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../js/lib/jquery-1.11.2.min.js"></script>
    <script src="../js/lib/AssociateDignosisCode.js" type="text/javascript"></script>
    <script src="../validation.js"></script>
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
    <asp:ScriptManager ID="scriptmanager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <div class="container-fluid">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 80%;
                    padding-top: 3px; height: 100%; vertical-align: top;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="LeftTop">
                            </td>
                            <td class="CenterTop">
                            </td>
                            <td class="RightTop">
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftCenter" style="height: 100%">
                            </td>
                            <td class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                                AutoGenerateColumns="False">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <a href="#" onclick="return openTypePage('a')">
                                                                <img src="Images/actionEdit.gif" style="border: none;" />
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                            <div id="Div2" style="color: red" visible="true">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <div>
                                                <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
                                                    CssClass="ajax__tab_theme">
                                                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0">
                                                        <HeaderTemplate>
                                                            <div style="width: 200px; height: 200px;" class="lbl">
                                                                Associate Diagnosis Code</div>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <div style="width: 99%">
                                                                <div style="width: 100%; margin-bottom: 30px">
                                                                    <div style="width: 48%; float: left; margin-right: 20px">
                                                                        <div class="page-title" style="padding-left: 15px; margin-bottom: 10px">
                                                                            Search</div>
                                                                        <div class="col-md-12">
                                                                            <asp:UpdatePanel ID="UP_Speciality" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div style="overflow: scroll; height: 150px; width: 100%; margin-bottom: 20px">
                                                                                        <asp:DataGrid Width="100%" Height="150px" ID="grdSpeciality" CssClass="GridTable"
                                                                                            runat="server" AutoGenerateColumns="False">
                                                                                            <ItemStyle CssClass="GridRow" />
                                                                                            <Columns>
                                                                                                <asp:TemplateColumn HeaderText="Select" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="chkSelectspecialty" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                                <asp:BoundColumn DataField="description" HeaderText="Speciality" ItemStyle-HorizontalAlign="Left"
                                                                                                    HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="code" HeaderText="Code" ItemStyle-HorizontalAlign="Left"
                                                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                                                                </asp:BoundColumn>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="GridHeader" />
                                                                                        </asp:DataGrid>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <table>
                                                                                <tr style="padding: 10px">
                                                                                    <td>
                                                                                        <asp:Label ID="lblSpeciality" runat="server" Text="Speciality" Visible="false"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                                            Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                            Selected_Text="---Select---" Width="140px" OldText="" StausText="False" Visible="false">
                                                                                        </extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="padding: 10px">
                                                                                    <td>
                                                                                        Type :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:RadioButtonList ID="rbl_SZ_TYPE_ID" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_SZ_TYPE_ID_OnSelectedIndexChanged">
                                                                                            <asp:ListItem Text="ICD10" Value="ICD10" Selected="True" style="font-family: Cambria;
                                                                                                font-weight: 900; font-size: smaller"></asp:ListItem>
                                                                                            <asp:ListItem Text="ICD9" Value="ICD9" style="font-family: Cambria; font-weight: 900;
                                                                                                font-size: smaller"></asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="padding: 10px">
                                                                                    <td>
                                                                                        Search :
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtSearchDignosisCode" runat="server" CssClass="search-input " Height="25px"
                                                                                            Width="173px"></asp:TextBox>
                                                                                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajDignosisCode" EnableCaching="true"
                                                                                            DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtSearchDignosisCode"
                                                                                            ServiceMethod="GetDignosisCodes" ServicePath="~/AJAX Pages/PatientService.asmx"
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
                                                                                        Dignosis Type :
                                                                                    </td>
                                                                                    <td>
                                                                                        <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="173px"
                                                                                            Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                                                            Flag_Key_Value="DIAGNOSIS_TYPE_LIST" Height="25px" Font-Size="Small"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Code :
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
                                                                                        Description :
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
                                                                                        <asp:UpdatePanel ID="UP_btnSearch" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="col-md-4">
                                                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="btn-primary"
                                                                                                        OnClick="btnSearch_Click" />
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Assign" Width="80px" CssClass="btn-primary"
                                                                                            OnClick="btnSubmit_OnClick" OnClientClick="if(!CheckSelectedDGCodes()) return false;" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <asp:UpdatePanel ID="UP_grdDignosisSearch" runat="server">
                                                                                <ContentTemplate>
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdDignosisSearch"
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
                                                                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UP_btnSearch"
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
                                                                                    <div style="margin-top: 30px">
                                                                                        <div style="height: 200px; background-color: Gray; overflow-y: scroll; margin-bottom: 30px">
                                                                                            <asp:DataGrid ID="grdDiagonosisCode" runat="server" Width="100%" CssClass="GridTable"
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
                                                                </div>
                                                                <div style="width: 48%; float: right">
                                                                    <div class="page-title" style="padding-left: 15px; margin-bottom: 10px">
                                                                        Selected code(s)</div>
                                                                    <div class="row">
                                                                        <div id="headerInsurance" style="text-align: left">
                                                                        </div>
                                                                        <br />
                                                                        <div id="divDiagnosis" runat="server" style="padding-left: 30px">
                                                                            <%--<table id="tblDignosis" cellpadding="20" runat="server" cellspacing="20">
                                                                            </table>--%>
                                                                            <ul id="ulInsurance" style="list-style: none; text-align: left">
                                                                            </ul>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <table style="width: 100%">
                                                                <tr style="width: 100%">
                                                                    <td colspan="2">
                                                                        Total Count :
                                                                        <asp:Label ID="lblDiagnosisCount" Font-Bold="true" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td colspan="2">
                                                                        <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                            &nbsp;<asp:DataGrid Width="100%" ID="grdSelectedDiagnosisCode" CssClass="GridTable"
                                                                                runat="server" AutoGenerateColumns="False">
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
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tablecellLabel" colspan="2" rowspan="3">
                                                                        &nbsp; &nbsp;<asp:TextBox Width="100%" ID="txtSearchText" runat="server" Visible="False"></asp:TextBox>
                                                                        <asp:CheckBox ID="chkDiagnosisCode" runat="server" Text="Diagnosis Code" Visible="False" />
                                                                        <asp:CheckBox ID="chkDiagnosisCodeDescription" runat="server" Text="Diagnosis Code Description"
                                                                            Visible="False" />
                                                                        <asp:Button ID="Button1" runat="server" Width="80px" CssClass="Buttons" Text="Search"
                                                                            OnClick="btnSearch_Click" Visible="False"></asp:Button>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                        &nbsp; &nbsp;
                                                                        <asp:TextBox ID="TextBox2" runat="server" Width="10px" Visible="False"></asp:TextBox><asp:TextBox
                                                                            ID="TextBox3" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="overflow: scroll; height: 300px; width: 100%; text-align: right" class="hiddencol">
                                                                <asp:DataGrid Width="100%" Height="200px" ID="grdNormalDgCode" CssClass="GridTable"
                                                                    runat="server" AutoGenerateColumns="False" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged"
                                                                    Visible="false">
                                                                    <ItemStyle CssClass="GridRow" />
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
                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                </asp:DataGrid>
                                                            </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <ajaxToolkit:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1">
                                                        <HeaderTemplate>
                                                            <div style="width: 200px; height: 200px;" class="lbl">
                                                                De-associate Diagnosis Code</div>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="100%" scope="col">
                                                                        <div class="blocktitle">
                                                                            <div class="div_blockcontent">
                                                                                <table width="100%" border="0">
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            &nbsp;<asp:DataGrid Width="100%" ID="grdAssignedDiagnosisCode" runat="server" CssClass="GridTable"
                                                                                                AutoGenerateColumns="False" AllowPaging="True" PageSize="20" PagerStyle-Mode="NumericPages" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged">
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
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Button ID="btnDeassociateDiagCode" runat="server" Text="De-Associate" Width="104px"
                                                                                                CssClass="Buttons" OnClick="btnDeassociateDiagCode_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                </ajaxToolkit:TabContainer>
                                                <div>
                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnSpeciality" runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdnDiagnosis" runat="server"></asp:HiddenField>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

</asp:Content>
