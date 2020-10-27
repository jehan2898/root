<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateDiagnosisCodeVisit.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_AssociateDiagnosisCodeVisit" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function SelectAllDiagnosis(ival) {
            var f = document.getElementById('carTabPage_grdDiagnosisCode');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function SelectAll(ival) {
            var f = document.getElementById('carTabPage_grdAssociatedDiagCode');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function callforSearch() {
            document.getElementById('hdnSearch').value = 'true';
        }  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
        <tr>
            <td><asp:Label ID="lblMsg" runat="server" Text="" Visible="false" ForeColor="Red" Font-Bold="false" Font-Names="Verdana" Font-Size="Smaller"></asp:Label></td>
        </tr>
        <tr>
            <td>
            <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
            Width="100%" Height="220">
            <TabPages>
                <dx:TabPage Text="Associate Diagnosis Code" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%"
                    ActiveTabStyle-BackColor="White" TabStyle-BackColor="#B1BEE0">
                    <ContentCollection>
                        <dx:ContentControl>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <table width="70%">
                                            <tr>
                                                <td>
                                                    Diagnosis Type
                                                </td>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server" Width="105px"
                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---"
                                                        Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                </td>
                                                <td>
                                                    Code
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Description
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Specialty
                                                </td>
                                                <td>
                                                    <extddl:ExtendedDropDownList ID="extddlSpecialityDia" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_SPECIALTY" Procedure_Name="SP_MST_SPECIALTY_LHR" Selected_Text="---Select---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right">
                                                    <dx:ASPxButton ID="btnSeacrh" runat="server" OnClick="btnSeacrh_Click" Text="Search">
                                                        <ClientSideEvents Click="function(s, e) {Callback.PerformCallback();LoadingPanel.Show();}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td colspan="2" align="left">
                                                    <dx:ASPxButton ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign">
                                                        <ClientSideEvents Click="function(s, e) {Callback.PerformCallback();LoadingPanel.Show();}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="grdid" runat="server">
                                        <dx:ASPxGridView ID="grdDiagnosisCode" ClientInstanceName="grdDiagnosisCode" runat="server"
                                            Width="100%" KeyFieldName="SZ_DIAGNOSIS_CODE_ID" SettingsBehavior-AllowSort="true"
                                            AutoGenerateColumns="False" SettingsPager-PageSize="10" Settings-VerticalScrollableHeight="300"
                                            SettingsCustomizationWindow-Height="180" SettingsPager-Mode="ShowPager">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="chk" Width="30px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllDiagnosis(this.checked);"
                                                            ToolTip="Select All" />
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataTextColumn FieldName="SZ_DIAGNOSIS_CODE_ID" Caption="SZ_DIAGNOSIS_CODE_ID"
                                                    HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                                <dx:GridViewDataTextColumn FieldName="SZ_DIAGNOSIS_CODE" Caption="Diagnosis Code"
                                                    HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Width="70px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SZ_DESCRIPTION" Caption="Description" HeaderStyle-HorizontalAlign="Center"
                                                    CellStyle-HorizontalAlign="Left" Width="450px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SZ_DIAGNOSIS_TYPE_ID" Caption="SZ_DIAGNOSIS_TYPE_ID"
                                                    HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right" Visible="false">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowVerticalScrollBar="true" ShowFilterRow="true" ShowGroupPanel="true" />
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsBehavior AllowSelectByRowClick="true" />
                                            <SettingsPager Position="Bottom" />
                                            <Styles>
                                                <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                </FocusedRow>
                                                <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                </AlternatingRow>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="De-Associate Diagnosis Code" ActiveTabStyle-Font-Bold="true" TabStyle-Width="100%"
                    ActiveTabStyle-BackColor="White" TabStyle-BackColor="#B1BEE0">
                    <ContentCollection>
                        <dx:ContentControl>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="grdAssociatedDiagCode" ClientInstanceName="grdAssociatedDiagCode"
                                            runat="server" Width="100%" KeyFieldName="SZ_ASSOCIATED_DIAG_CODE_ID" SettingsBehavior-AllowSort="true"
                                            AutoGenerateColumns="False" SettingsPager-PageSize="10" Settings-VerticalScrollableHeight="300"
                                            SettingsCustomizationWindow-Height="180" SettingsPager-Mode="ShowPager">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="chk" Width="30px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                            ToolTip="Select All" />
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataTextColumn FieldName="SZ_ASSOCIATED_DIAG_CODE_ID" Caption="SZ_ASSOCIATED_DIAG_CODE_ID"
                                                    HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                                <dx:GridViewDataTextColumn FieldName="SZ_DIAGNOSIS_CODE" Caption="Diagnosis Code"
                                                    HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Width="90px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SZ_DESCRIPTION" Caption="Description" HeaderStyle-HorizontalAlign="Center"
                                                    CellStyle-HorizontalAlign="Left" Width="350px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SZ_DIAGNOSIS_TYPE_ID" Caption="SZ_DIAGNOSIS_TYPE_ID"
                                                    HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right" Visible="false">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="SZ_PROCEDURE_GROUP_ID"
                                                    HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right" Visible="false">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Speciality" Caption="Speciality" Width="90px" HeaderStyle-HorizontalAlign="Center"
                                                    CellStyle-HorizontalAlign="Left" Visible="true">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowVerticalScrollBar="true" ShowFilterRow="true" ShowGroupPanel="true" />
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsBehavior AllowSelectByRowClick="true" />
                                            <SettingsPager Position="Bottom" />
                                            <Styles>
                                                <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                </FocusedRow>
                                                <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                </AlternatingRow>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dx:ASPxButton ID="btnDeAssociate" runat="server" OnClick="btnDeAssociate_Click"
                                            Text="De-Associate">
                                            <ClientSideEvents Click="function(s, e) {Callback.PerformCallback();LoadingPanel.Show();}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl></td>
        </tr>
    </table>
        
        <div>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtEventProcID" runat="server" Visible="false"></asp:TextBox>
                        <asp:HiddenField ID="hdnSearch" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
        <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback">
            <ClientSideEvents CallbackComplete="function(s, e) { LoadingPanel.Hide(); }" />
        </dx:ASPxCallback>
    </div>
    </form>
</body>
</html>
