<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ReceivedReportPopupPage.aspx.cs" Inherits="Bill_Sys_ReceivedReportPopupPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/Doctor.ascx" TagName="Doc" TagPrefix="UserDoc" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        function validate() {
            if (document.getElementById('_ctl0_ContentPlaceHolder1_extddlSpeciality') != null) {
                if (document.getElementById('_ctl0_ContentPlaceHolder1_extddlSpeciality').value == 'NA') {
                    alert('Select Speciality ...!');
                    return false;
                }
                else
                    return true;
            }
            else {
                alert('Select Speciality ...!');
                return false;
            }
        }
    
    </script>
    <script type="text/javascript" src="Registration/validation.js"></script>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <script>


         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="height: auto; width: 100%; float: left;">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: auto;
                                    vertical-align: top;">
                                    <tr>
                                        <td style="width: 100%; height: auto; float: left;" class="TDPart">
                                            <table width="100%" border="0" align="center" class="ContentTable" cellpadding="0"
                                                cellspacing="0">
                                                <tr id="tdSerach" runat="server">
                                                    <td colspan="6">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="6" style="height: auto;">
                                                        <table width="100%">
                                                            <%--<tr>
                                                        <td class="ContentLabel" colspan="6" style="height: 20px; text-align: left">
                                                         Add Patient 
                                                        </td>
                                                    </tr>--%>
                                                            <tr>
                                                                <td class="ContentLabel" style="text-align: left; height: 20px;" colspan="6">
                                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True"
                                                                        ForeColor="Red"></asp:Label>
                                                                    <div id="ErrorDiv" style="color: red;" visible="true">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                                <td colspan="6">
                                                                    <UserDoc:Doc  ID="docname" runat="server" />
                                                                </td>
                                                            </tr>--%>
                                                            <%--<tr>
                                                                <td id="redingdoctd1" runat="server" style="width: 131px;" align="left">
                                                                    Reading Doctor
                                                                </td>
                                                                <td id="redingdoctd2" runat="server" style="width: 278px" align="left">
                                                                    <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="213px"
                                                                        Connection_Key="Connection_String" Procedure_Name="SP_GET_LHR_READINGDOCTORS"
                                                                        Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();"
                                                                        AutoPost_back="False" />
                                                                </td>
                                                                <td id="extratd" runat="server" colspan="2">
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <asp:Button ID="Btn_Update" runat="server" Text="Update" Width="84px" CssClass="Buttons"
                                                                        OnClick="Btn_Update_Click" />
                                                                </td>
                                                                <td width="20%">
                                                                </td>
                                                            </tr>--%>
                                                             <tr>
                                                                <td colspan="2">
                                                                <UserDoc:Doc ID="docname" runat="server" />
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                <asp:Button ID="Btn_Update" runat="server" Text="Update" Width="84px" CssClass="Buttons"
                                                                        OnClick="Btn_Update_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                
                                                                </td>
                                                                <td>
                                                                <h3>OR</h3>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="LblReferDoc" runat="server" Text="Reffering Doctor"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxTextBox ID="TxtReferDoc" runat="server" MaxLength="50" Width="180px">
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                                <td id="Td7" runat="server">
                                                                    <asp:Label ID="LblSpeciality" runat="server" Text="Speciality"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxComboBox ID="CmbSpeciality" Width="180px" runat="server" Selected_Text="--- Select ---"
                                                                    Visible="true" ClientInstanceName="CmbSpeciality" ></dx:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <asp:Label ID="LblOffice" runat="server" Text="Office"></asp:Label>
                                                                </td>
                                                                <td>
                                                                <dx:ASPxComboBox ID="CmbOffice" Width="180px" runat="server" Selected_Text="--- Select ---"
                                                                    Visible="true" ClientInstanceName="CmbOffice" ></dx:ASPxComboBox>
                                                                </td>
                                                                <td id="Td9" runat="server"></td>
                                                                <td></td>
                                                                <td>
                                                                <asp:Button ID="BtnAdd" runat="server" Text="Add" Width="84px" CssClass="Buttons"
                                                                        OnClick="BtnAdd_Click" Visible="false" />
                                                                    <%--<asp:Button ID="BtnAddOffice" runat="server" Text="Add" Width="84px" 
                                                                        CssClass="Buttons" onclick="BtnAddOffice_Click"/>--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="left">
                                                                    <b>Diagnosis Code Section </b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" colspan="6" align="left">
                                                                    <cc1:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
                                                                        CssClass="ajax__tab_theme" TabStripPlacement="Top">
                                                                        <cc1:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0">
                                                                            <HeaderTemplate>
                                                                                <div style="width: 200px; height: 200px; float: left;" class="lbl">
                                                                                    Associate Diagnosis Code</div>
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td width="100%" scope="col" align="left">
                                                                                            <div class="blocktitle">
                                                                                                <div class="div_blockcontent">
                                                                                                    <table width="100%" border="0">
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
                                                                                                                <table border="0" cellpadding="3" cellspacing="4" class="ContentTable" style="width: 85%">
                                                                                                                    <tr runat="server" id="trDoctorType">
                                                                                                                        <td id="Td1" class="ContentLabel" runat="server">
                                                                                                                            Diagnosis Type :
                                                                                                                        </td>
                                                                                                                        <td id="Td2" class="ContentLabel" runat="server">
                                                                                                                            <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px"
                                                                                                                                Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                                                                                                Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                                                        </td>
                                                                                                                        <td id="Td3" class="ContentLabel" runat="server">
                                                                                                                            Code :
                                                                                                                        </td>
                                                                                                                        <td id="Td4" runat="server">
                                                                                                                            <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td id="Td5" class="ContentLabel" runat="server">
                                                                                                                            Description :
                                                                                                                        </td>
                                                                                                                        <td id="Td6" class="ContentLabel" runat="server">
                                                                                                                            <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            Speciality :
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <extddl:ExtendedDropDownList ID="extddlSpecialityDia" runat="server" Connection_Key="Connection_String"
                                                                                                                                Flag_Key_Value="GET_SPECIALTY" Procedure_Name="SP_MST_SPECIALTY_LHR" Selected_Text="---Select---" Enabled="false" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="4" align="center">
                                                                                                                            <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                                                            <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" OnClick="btnSeacrh_Click" />
                                                                                                                            &nbsp;<asp:Button ID="btnAssign" OnClick="btnAssign_Click" runat="server" Text="Assign"
                                                                                                                                Width="80px" Visible="true" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                                <%--<table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 80%">
                                                                                        <tr runat="server" id="trDoctorType">
                                                                                            <td id="Td1" class="ContentLabel" runat="server" >
                                                                                                Diagnosis Type &nbsp;&nbsp;</td>
                                                                                            <td id="Td2"  class="ContentLabel" runat="server">
                                                                                                <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px"
                                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                                                                    Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                            </td>
                                                                                            <td id="Td3" class="ContentLabel" runat="server" >
                                                                                                Code &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td id="Td4" runat="server"  >
                                                                                                <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                                                            </td>
                                                                                            <td id="Td5" class="ContentLabel" runat="server" >
                                                                                                Description &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td id="Td6"  class="ContentLabel" runat="server">
                                                                                                <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        
                                                                                        <tr>
                                                                                            <td colspan="6" align="left">
                                                                                                
                                                                                                
                                                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                                <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                                                                    OnClick="btnSeacrh_Click" />
                                                                                                &nbsp;<asp:Button
                                                                                        ID="btnAssign" runat="server" Text="Assign" Width="80px" CssClass="Buttons" OnClick="btnAssign_Click" /></td>
                                                                                        </tr>
                                                                                    </table>--%>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <%--<td colspan="2" align="left">
                                                                                    &nbsp; <b>Search result</b>&nbsp; &nbsp;&nbsp;
                                                                                </td>--%>
                                                                                                        </tr>
                                                                                                        <%--<tr>
                                                                                <td colspan="2">
                                                                                    <table>
                                                                                        <tr>
                                                                                           <td style="font-size: 14px; vertical-align: top; width: 35%; font-family: arial;
                                                                                                        text-align: left">
                                                                                                <asp:Label ID="lblSpeciality" runat="server" Text="Speciality"></asp:Label>
                                                                                            </td>
                                                                                            <td id="Td7" runat="server" class="ContentLabel">
                                                                                                <extddl:extendeddropdownlist id="extddlSpeciality" runat="server" connection_key="Connection_String"
                                                                                                    procedure_name="SP_MST_PROCEDURE_GROUP" flag_key_value="GET_PROCEDURE_GROUP_LIST"
                                                                                                    selected_text="---Select---" width="140px" OldText="" StausText="False">
                                                                                                </extddl:extendeddropdownlist>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>--%>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
                                                                                                                <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                                                                    <tr runat="server" id="Tr1">
                                                                                                                        <td width="100%" runat="server" id="Td8">
                                                                                                                            <div style="overflow: auto; height: auto; width: 100%;">
                                                                                                                                <asp:DataGrid Width="99%" ID="grdNormalDgCode" CssClass="GridTable" runat="server"
                                                                                                                                    AutoGenerateColumns="False" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                                                                                                                                    OnPageIndexChanged="grdNormalDgCode_PageIndexChanged" OnSelectedIndexChanged="grdNormalDgCode_SelectedIndexChanged">
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
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
                                                                                                                <asp:Label ID="lblDiagnosisCount" Font-Bold="true" runat="server" Visible="false"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
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
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="tablecellLabel" colspan="2" rowspan="3">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1">
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
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                </td>
                                                                <asp:TextBox ID="txtSource" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox><asp:TextBox
                                                                    ID="txtDiagnosisSetID" runat="server" Visible="false" Width="10px"></asp:TextBox><asp:TextBox
                                                                        ID="txtCompanyID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                        
                                                                        </tr>
                                                            <%-- <tr>
                                                                                    <td style="width: 131px">
                                                                                        &nbsp;</td>
                                                                                    <td align="left" style="width:278px;" colspan="2">
                                                                                        &nbsp;</td>
                                                                                </tr>--%>
                                                            <%-- <tr>
                                                                                    <td colspan="6" align="left" >
                                                                                       <b>Upload Section </b> 
                                                                                    </td>
                                                                                </tr>--%>
                                                            <tr>
                                                                <td style="width: 98%;" valign="top" colspan="2">
                                                                    <%--<table border="0" class="ContentTable" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: auto;" align="left">
                                                                            Upload Report :
                                                                        </td>
                                                                        <td>
                                                                            <asp:FileUpload ID="fuUploadReport" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="left">
                                                                            <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons" OnClick="btnUploadFile_Click" />
                                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons"
                                                                                        OnClick="btnCancel_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>--%>
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
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
