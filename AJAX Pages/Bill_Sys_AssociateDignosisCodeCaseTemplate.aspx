<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateDignosisCodeCaseTemplate.aspx.cs"
    Inherits="Bill_Sys_AssociateDignosisCodeCaseTemplate" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link type="text/css" rel="Stylesheet" runat="server" href="" />
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
    <script type="text/javascript">
    function LoadPage()
    {
     window.resizeTo(700,400)
     window.moveTo(screen.availWidth/4,screen.availHeight/4)
    }
    
    function OnSave()
     {            
          alert('1')
          window.open('../TM/RTFEditter.aspx?hidden='','AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
     }
    
    </script>
    <script type="text/javascript" src="validation.js"></script>
    <script type="text/javascript" src="Ajax Pages/BillTransaction.js"></script>
</head>
<body onload="LoadPage()">
    <form id="FORM1" method="post" name="FORM1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    
    <table width="100%" border="0">
        <tr>
            <td style="width:100%;text-align:center;font-family:Calibri;font-size:14px;">
                <asp:Label runat="server" id="lblErrorMessage" ForeColor="Red"></asp:Label>
                <asp:Label runat="server" id="lblMessage" ForeColor="Green"></asp:Label>
            </td>
        </tr>
    </table>
    
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 80%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <dx:ASPxGridView ID="grdPatientDeskList" runat="server" SettingsPager-PageSize="20"
                                            CssClass="GridTable" AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Name" FieldName="SZ_PATIENT_NAME" Visible="true"
                                                    HeaderStyle-Font-Bold="true" Settings-AllowSort="False">
                                                    <%--<HeaderStyle CssClass="GridHeader" />--%>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GridHeader" BackColor="#B5DF82" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Insurance Company" FieldName="SZ_INSURANCE_NAME"
                                                    HeaderStyle-Font-Bold="true" Visible="true" Settings-AllowSort="False">
                                                    <%--<HeaderStyle CssClass="GridHeader" />--%>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GridHeader" BackColor="#B5DF82" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Accident Date" FieldName="DT_ACCIDENT" Visible="true"
                                                    HeaderStyle-Font-Bold="true" Settings-AllowSort="False">
                                                    <%--<HeaderStyle CssClass="GridHeader" />--%>
                                                    <HeaderStyle HorizontalAlign="Center" CssClass="GridHeader" BackColor="#B5DF82" />
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false" Style="color: Red;"></asp:Label>
                                        <div id="ErrorDiv" style="color: red" visible="true">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 19px;" class="SectionDevider" align="right">
                                        <asp:Button ID="btnTemplateManager" runat="server" Text="TemplateManager" Width="132px"
                                            CssClass="Buttons" OnClick="btnTemplateManager_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
                                            Height="100">
                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0" CssClass="ajax__tab_header">
                                                <HeaderTemplate>
                                                    <div style="width: 200px; height: 20px; font-weight: bold; font-size: small; ">
                                                        Associate Diagnosis Code</div>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100%" scope="col">
                                                                <%-- <div class="blocktitle">
                                                                    <div class="div_blockcontent">--%>
                                                                <table width="100%" border="0">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 80%">
                                                                                <tr id="trDoctorType" runat="server">
                                                                                    <td runat="server" class="ContentLabel">
                                                                                        Diagnosis Type &nbsp;&nbsp;
                                                                                    </td>
                                                                                    <td runat="server" class="ContentLabel">
                                                                                        <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText="" Procedure_Name="SP_MST_DIAGNOSIS_TYPE"
                                                                                            Selected_Text="--- Select ---" StausText="False" Width="105px" />
                                                                                    </td>
                                                                                    <td runat="server" class="ContentLabel">
                                                                                        Code &nbsp;&nbsp;
                                                                                    </td>
                                                                                    <td runat="server">
                                                                                        <asp:TextBox ID="txtDiagonosisCode" runat="server" MaxLength="50" Width="55px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td runat="server" class="ContentLabel">
                                                                                        Description &nbsp;&nbsp;
                                                                                    </td>
                                                                                    <td runat="server" class="ContentLabel">
                                                                                        <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" Width="110px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" colspan="6">
                                                                                        <asp:Button ID="btnOK" runat="server" CssClass="Buttons" OnClick="btnOK_Click" Text="Add"
                                                                                            Visible="False" Width="80px" />
                                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="Buttons" OnClick="btnCancel_Click"
                                                                                            Text="Cancel" Visible="False" Width="80px" />
                                                                                        <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                        <asp:Button ID="btnSeacrh" runat="server" CssClass="Buttons" OnClick="btnSeacrh_Click"
                                                                                            Text="Search" Width="80px" />
                                                                                        &nbsp;<asp:Button ID="btnAssign" runat="server" CssClass="Buttons" OnClick="btnAssign_Click"
                                                                                            Text="Assign" Width="80px" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            &nbsp; Search Result&nbsp; &nbsp;&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="font-size: 14px; vertical-align: top; width: 35%; font-family: arial;
                                                                                        text-align: left">
                                                                                        <asp:Label ID="lblSpeciality" runat="server" Text="Speciality" Visible="False"></asp:Label>
                                                                                         <asp:Label ID="Lblext" runat="server"  Visible="False"></asp:Label>
                                                                                    </td>
                                                                                    <td id="Td3" runat="server" class="ContentLabel">
                                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                                            Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                            Selected_Text="---Select---" Width="140px" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                                <tr runat="server" id="Tr1">
                                                                                    <td width="100%" runat="server" id="Td1">
                                                                                        <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                            <dx:ASPxGridView ID="grdNormalDgCode" AutoGenerateColumns="False" KeyFieldName="SZ_DIAGNOSIS_CODE"
                                                                                                Visible="false" Width="100%" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged"
                                                                                                SettingsPager-PageSize="20" runat="server">
                                                                                                <Columns>
                                                                                                    <dx:GridViewDataColumn ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                                        <HeaderStyle Font-Bold="True" />
                                                                                                        <Settings AllowSort="False" />
                                                                                                        <DataItemTemplate>
                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                        </DataItemTemplate>
                                                                                                    </dx:GridViewDataColumn>
                                                                                                    <dx:GridViewDataColumn Caption="Diagnosis Code Id" FieldName="SZ_DIAGNOSIS_CODE_ID"
                                                                                                        Visible="False" ShowInCustomizationForm="True">
                                                                                                        <Settings AllowSort="False" />
                                                                                                        <HeaderStyle Font-Bold="True" />
                                                                                                    </dx:GridViewDataColumn>
                                                                                                    <dx:GridViewDataColumn Caption="Diagnosis Code" FieldName="SZ_DIAGNOSIS_CODE" ShowInCustomizationForm="True"
                                                                                                        VisibleIndex="1">
                                                                                                        <Settings AllowSort="False" />
                                                                                                        <HeaderStyle  Font-Bold="True" />
                                                                                                    </dx:GridViewDataColumn>
                                                                                                    <dx:GridViewDataColumn Caption="company" FieldName="SZ_COMPANY_ID" Visible="False"
                                                                                                        ShowInCustomizationForm="True">
                                                                                                        <Settings AllowSort="False" />
                                                                                                        <HeaderStyle  Font-Bold="True" />
                                                                                                    </dx:GridViewDataColumn>
                                                                                                    <dx:GridViewDataColumn Caption="Description" FieldName="SZ_DESCRIPTION" ShowInCustomizationForm="True"
                                                                                                        VisibleIndex="2">
                                                                                                        <Settings AllowSort="False" />
                                                                                                        <HeaderStyle  Font-Bold="True" />
                                                                                                    </dx:GridViewDataColumn>
                                                                                                </Columns>
                                                                                                <SettingsPager PageSize="30">
                                                                                                </SettingsPager>
                                                                                            </dx:ASPxGridView>
                                                                                        </div>
                                                                                        <%--<div style="overflow: scroll; height: 300px; width: 100%;">--%>
                                                                                        <dx:ASPxGridView ID="grdSelectedDiagnosisCode" KeyFieldName="DIAGNOSIS_CODE_ID" Visible="false"
                                                                                            CssClass="GridTable" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                                            SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330" Settings-VerticalScrollableHeight="330">
                                                                                            <Columns>
                                                                                                <%-- 0--%>
                                                                                                <dx:GridViewDataColumn Visible="false" Caption="DIAGNOSIS_CODE_ID" FieldName="SZ_DIAGNOSIS_CODE_ID"
                                                                                                    Settings-AllowSort="False" Width="25px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Diagnosis Code" FieldName="SZ_DIAGNOSIS_CODE" HeaderStyle-Font-Bold="true"
                                                                                                    Visible="true" Width="25px" Settings-AllowSort="False">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Diagnosis Code Description" FieldName="SZ_DESCRIPTION"
                                                                                                    Settings-AllowSort="False" HeaderStyle-Font-Bold="true" Visible="true" Width="25px">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="COMPANY_ID" FieldName="SZ_COMPANY_ID" Settings-AllowSort="False"
                                                                                                    HeaderStyle-Font-Bold="true" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Diagnosis Code" FieldName="SZ_DIAGNOSIS_CODE" Settings-AllowSort="False"
                                                                                                    HeaderStyle-Font-Bold="true" Visible="true">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="SZ_PROCEDURE_GROUP_ID" FieldName="SZ_PROCEDURE_GROUP_ID"
                                                                                                    HeaderStyle-Font-Bold="true" Settings-AllowSort="False" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Speciality" FieldName="SZ_PROCEDURE_GROUP" HeaderStyle-Font-Bold="true"
                                                                                                    Visible="true">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                            </Columns>
                                                                                        </dx:ASPxGridView>
                                                                                        <%--</div>--%>
                                                                                    </td>
                                                                                    <%--<div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                        &nbsp;
                                                                                       
                                                                                    </div>--%>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            Total Count :
                                                                            <asp:Label ID="lblDiagnosisCount" Font-Bold="True" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <%--<tr>
                                                                                <td colspan="2">
                                                                                    <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                        &nbsp;
                                                                                        <dx:ASPxGridView ID="grdSelectedDiagnosisCode" KeyFieldName="DIAGNOSIS_CODE_ID" Visible="false"
                                                                                            CssClass="GridTable" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                                            SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330" Settings-VerticalScrollableHeight="330">
                                                                                            <Columns>
                                                                                                <%-- 0
                                                                                                <dx:GridViewDataColumn Visible="false" Caption="DIAGNOSIS_CODE_ID" FieldName="SZ_DIAGNOSIS_CODE_ID"
                                                                                                    Settings-AllowSort="False" Width="25px">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Diagnosis Code" FieldName="SZ_DIAGNOSIS_CODE" HeaderStyle-Font-Bold="true"
                                                                                                    Visible="true" Width="25px" Settings-AllowSort="False">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Diagnosis Code Description" FieldName="SZ_DESCRIPTION"
                                                                                                    Settings-AllowSort="False" HeaderStyle-Font-Bold="true" Visible="true" Width="25px">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="COMPANY_ID" FieldName="SZ_COMPANY_ID" Settings-AllowSort="False"
                                                                                                    HeaderStyle-Font-Bold="true" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Diagnosis Code" FieldName="SZ_DIAGNOSIS_CODE" Settings-AllowSort="False"
                                                                                                    HeaderStyle-Font-Bold="true" Visible="true">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="SZ_PROCEDURE_GROUP_ID" FieldName="SZ_PROCEDURE_GROUP_ID"
                                                                                                    HeaderStyle-Font-Bold="true" Settings-AllowSort="False" Visible="false">
                                                                                                </dx:GridViewDataColumn>
                                                                                                <dx:GridViewDataColumn Caption="Speciality" FieldName="SZ_PROCEDURE_GROUP" HeaderStyle-Font-Bold="true"
                                                                                                    Visible="true">
                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                </dx:GridViewDataColumn>
                                                                                            </Columns>
                                                                                        </dx:ASPxGridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>--%>
                                                                    <tr>
                                                                        <td class="tablecellLabel" colspan="2" rowspan="3">
                                                                            &nbsp; &nbsp;<asp:TextBox Width="100%" ID="txtSearchText" runat="server" Visible="False"></asp:TextBox>
                                                                            <asp:CheckBox ID="chkDiagnosisCode" runat="server" Text="Diagnosis Code" Visible="False" />
                                                                            <asp:CheckBox ID="chkDiagnosisCodeDescription" runat="server" Text="Diagnosis Code Description"
                                                                                Visible="False" />
                                                                            <asp:Button ID="btnSearch" runat="server" Width="80px" CssClass="Buttons" Text="Search"
                                                                                OnClick="btnSearch_Click" Visible="False"></asp:Button>
                                                                               
                                                                            <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                            &nbsp; &nbsp;
                                                                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox><asp:TextBox
                                                                                ID="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                            <asp:TextBox ID="txtUserId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                            <asp:TextBox ID="txtDoctorId" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                            <asp:TextBox ID="txtSpeciality" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <%-- </div>
                                                                </div>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1" CssClass="ajax__tab_theme">
                                                <HeaderTemplate>
                                                    <div style="width: 250px; height: 20px; font-weight: bold; font-family: verdana,tahoma,helvetica;
                                                        font-size: 11px;">
                                                        De-Associate Diagnosis Code</div>
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
                                                                                    &nbsp;
                                                                                    <%--   OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged" --%>
                                                                                    <div style="overflow:scroll; height:220px;">
                                                                                    <dx:ASPxGridView ID="grdAssignedDiagnosisCode" KeyFieldName="SZ_DIAGNOSIS_CODE_ID" 
                                                                                        Visible="true" CssClass="GridTable" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                                        SettingsPager-Mode="ShowAllRecords" SettingsCustomizationWindow-Height="330" Settings-VerticalScrollableHeight="330">
                                                                                        <Columns>
                                                                                            <%-- 0--%>
                                                                                            <dx:GridViewDataColumn Visible="true" Caption="DIAGNOSIS_CODE_ID" FieldName="SZ_DIAGNOSIS_CODE_ID"
                                                                                                HeaderStyle-Font-Bold="true" Settings-AllowSort="False" Width="25px">
                                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                                <DataItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                                </DataItemTemplate>
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn FieldName="SZ_DIAGNOSIS_CODE_ID" Caption="SZ_DIAGNOSIS_CODE_ID"
                                                                                                Visible="false">
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn FieldName="SZ_DIAGNOSIS_CODE" Caption="Diagnosis Code" Visible="true"
                                                                                                Settings-AllowSort="False" HeaderStyle-Font-Bold="true">
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn FieldName="SZ_DESCRIPTION" Caption="Diagnosis Code Description"
                                                                                                Visible="true" Settings-AllowSort="False" HeaderStyle-Font-Bold="true">
                                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn Caption="COMPANY_ID" FieldName="SZ_COMPANY_ID" Visible="false"
                                                                                                Settings-AllowSort="False">
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn Caption="SZ_PROCEDURE_GROUP_ID" FieldName="SZ_PROCEDURE_GROUP_ID"
                                                                                                Settings-AllowSort="False" Visible="false">
                                                                                            </dx:GridViewDataColumn>
                                                                                            <dx:GridViewDataColumn Caption="Speciality" FieldName="SZ_PROCEDURE_GROUP" Visible="true"
                                                                                                Settings-AllowSort="False" HeaderStyle-Font-Bold="true">
                                                                                                <HeaderStyle CssClass="GridHeader" />
                                                                                            </dx:GridViewDataColumn>
                                                                                        </Columns>
                                                                                    </dx:ASPxGridView>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
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
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
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
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <asp:TextBox ID="txtCurrentPage" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
