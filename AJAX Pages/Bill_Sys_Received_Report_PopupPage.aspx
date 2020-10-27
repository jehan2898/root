<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Received_Report_PopupPage.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Received_Report_PopupPage" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <script type="text/javascript">
        function SelectAllDiagnosis(ival) {
            var f = document.getElementById('tabcontainerDiagnosisCode_grdDiagnosisCode');
            var grdvalidation= document.getElementById('grdDiagnosisCode');
            var str = 1;
            var bfFlag = false;	
            for (var i = 0; i < f.getElementsByTagName("input").length; i++)
             {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") 
                {
                    if (f.getElementsByTagName("input").item(i).disabled == false) 
                    {
                        f.getElementsByTagName("input").item(i).checked = ival;
                        
                    }
                }
            }
            for(var j=0; j<grdvalidation.getElementsByTagName("input").length ;j++ )
		        {		
				  if(grdvalidation.getElementsByTagName("input").item(i).name.indexOf('chkall') !=-1)
		            {
		                if(grdvalidation.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(grdvalidation.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                    bfFlag = true;
			                }
			            }
			        }			
		        }
            
             if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
		        else
				{
					return false;
				}
        }

        function SelectAll(ival) {
            var f = document.getElementById('tabcontainerDiagnosisCode_grdAssociatedDiagCode');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") 
                {
                    if (f.getElementsByTagName("input").item(i).disabled == false) 
                    {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function callforSearch() 
        {
            document.getElementById('hdnSearch').value = 'true';
        }  
    </script>

    <script type="text/javascript" src="Registration/validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%" border="0">
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server" Text="" Visible="false" ForeColor="Red" Font-Bold="true"
                        Font-Names="Verdana" Font-Size="Smaller">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="ContentLabel" style="height: auto;">
                    <table width="100%" border="0" align="center" class="ContentTable" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td class="ContentLabel" style="text-align: left; height: 20px;" colspan="6">
                                <asp:Label CssClass="message-text" ID="Label1" runat="server" Visible="False" Font-Bold="True"
                                    ForeColor="Red"></asp:Label>
                                <div id="ErrorDiv" style="color: red;" visible="true">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td id="redingdoctd1" runat="server" style="width: 131px;" align="left">
                                Reading Doctor</td>
                            <td id="redingdoctd2" runat="server" style="width: 278px" align="left">
                                <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="213px"
                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_READINGDOCTOR" Selected_Text="---Select---"
                                    Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();" AutoPost_back="True" />
                            </td>
                            <td id="extratd" runat="server" colspan="2">
                            </td>
                            <td style="width: 6%">
                                <dx:ASPxButton ID="Btn_Update" runat="server" OnClick="Btn_Update_Click" Text="Update">
                                    <ClientSideEvents Click="function(s, e) {Callback.PerformCallback();LoadingPanel.Show();}" />
                                </dx:ASPxButton>
                            </td>
                            <td width="20%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="left">
                                <b>Diagnosis Code Section </b>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0">
            <tr>
                <td>
                    <dx:ASPxPageControl ID="tabcontainerDiagnosisCode" runat="server" ActiveTabIndex="0"
                        EnableHierarchyRecreation="True" Width="100%">
                        <TabPages>
                            <dx:TabPage Text="Associate Diagnosis Code" ActiveTabStyle-Font-Bold="true" ActiveTabStyle-BackColor="White"
                                TabStyle-BackColor="#B1BEE0">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <table width="60%">
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
                                                                Speciality
                                                            </td>
                                                            <td>
                                                                <extddl:ExtendedDropDownList ID="extddlSpecialityDia" runat="server" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="GET_SPECIALTY" Procedure_Name="SP_MST_SPECIALTY_LHR" Selected_Text="---Select---"
                                                                    Enabled="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="right">
                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                <dx:ASPxButton ID="btnSeacrh" runat="server" OnClick="btnSeacrh_Click" Text="Search">
                                                                    <ClientSideEvents Click="function(s, e) {LoadingPanel.Show();}" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td colspan="2" align="left">
                                                                <dx:ASPxButton ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign">
                                                                    <ClientSideEvents Click="function(s, e) {LoadingPanel.Show();}" />
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
                                                            <dx:GridViewDataTextColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Center"
                                                                Visible="false" />
                                                            <dx:GridViewDataTextColumn FieldName="SZ_DESCRIPTION" Caption="Description" HeaderStyle-HorizontalAlign="Center"
                                                                CellStyle-HorizontalAlign="Left" Width="450px">
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
                            <dx:TabPage Text="De-Associate Diagnosis Code" ActiveTabStyle-Font-Bold="true" ActiveTabStyle-BackColor="White"
                                TabStyle-BackColor="#B1BEE0">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <table width="91%">
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
                                                            <dx:GridViewDataTextColumn FieldName="Speciality" Caption="Speciality" Width="90px"
                                                                HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" Visible="true">
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
                    </dx:ASPxPageControl>
                </td>
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
                        <asp:TextBox ID="txtDiagnosisSetID" runat="server" Visible="false" Width="10px"></asp:TextBox>
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
    </form>
</body>
</html>
