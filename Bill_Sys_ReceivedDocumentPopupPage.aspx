<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ReceivedDocumentPopupPage.aspx.cs"
    Inherits="ReceivedDocumentPopupPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                                            <tr>
                                                                <td style="width: 131px;" align="left">
                                                                    Reading Doctor
                                                                </td>
                                                                <td style="width: 278px" align="left">
                                                                    <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="150px"
                                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_READINGDOCTOR" Selected_Text="---Select---"
                                                                        Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();" />
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                                <td width="10%">
                                                                </td>
                                                                <td width="20%">
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
                                                                                                            <td colspan="2">
                                                                                                                <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 80%">
                                                                                                                    <tr id="tr2" runat="server">
                                                                                                                        <td id="Td7" runat="server" class="ContentLabel">
                                                                                                                            Type &nbsp;&nbsp;
                                                                                                                        </td>
                                                                                                                        <td id="Td9" runat="server" class="ContentLabel">
                                                                                                                            <asp:RadioButtonList ID="rbl_SZ_TYPE_ID" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                                                                                                <asp:ListItem Text="ICD10" Value="ICD10" Selected="True" style="font-family: Cambria;
                                                                                                                                    font-weight: 900; font-size: smaller"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="ICD9" Value="ICD9" style="font-family: Cambria; font-weight: 900;
                                                                                                                                    font-size: smaller"></asp:ListItem>
                                                                                                                            </asp:RadioButtonList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr id="trDoctorType" runat="server">
                                                                                                                        <td id="Td1" runat="server" class="ContentLabel">
                                                                                                                            Diagnosis Type &nbsp;&nbsp;
                                                                                                                        </td>
                                                                                                                        <td id="Td2" runat="server" class="ContentLabel">
                                                                                                                            <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Connection_Key="Connection_String"
                                                                                                                                Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText="" Procedure_Name="SP_MST_DIAGNOSIS_TYPE"
                                                                                                                                Selected_Text="--- Select ---" StausText="False" Width="105px" />
                                                                                                                        </td>
                                                                                                                        <td id="Td3" runat="server" class="ContentLabel">
                                                                                                                            Code &nbsp;&nbsp;
                                                                                                                        </td>
                                                                                                                        <td id="Td4" runat="server">
                                                                                                                            <asp:TextBox ID="txtDiagonosisCode" runat="server" MaxLength="50" Width="55px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td id="Td5" runat="server" class="ContentLabel">
                                                                                                                            Description &nbsp;&nbsp;
                                                                                                                        </td>
                                                                                                                        <td id="Td6" runat="server" class="ContentLabel">
                                                                                                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" Width="110px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td align="left" colspan="6">
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
                                                                                                                <table id="tblDiagnosisCodeFirst" runat="server" width="100%">
                                                                                                                    <tr id="Tr1" runat="server">
                                                                                                                        <td id="Td8" runat="server" width="100%">
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
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
                                                                                                                <asp:Label ID="lblDiagnosisCount" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
                                                                                                                <div style="overflow: auto; height: auto; width: 100%;">
                                                                                                                    &nbsp;<asp:DataGrid ID="grdSelectedDiagnosisCode" runat="server" AutoGenerateColumns="False"
                                                                                                                        CssClass="GridTable" Visible="False" Width="100%">
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
                                                                                                                        <ItemStyle CssClass="GridRow" />
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
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
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
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Width="104px" CssClass="Buttons"
                                                                        OnClick="btnAdd_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 131px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" style="width: 278px;" colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="left">
                                                                    <b>Upload Section </b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 98%;" valign="top" colspan="2">
                                                                    <table border="0" class="ContentTable" style="width: 100%">
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
                                                                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons"
                                                                                    OnClick="btnUploadFile_Click" />
                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons"
                                                                                    OnClick="btnCancel_Click" Visible="false" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtDiagnosisSetID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="10px"></asp:TextBox>
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
