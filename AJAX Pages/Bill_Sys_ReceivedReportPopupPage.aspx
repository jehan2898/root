<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ReceivedReportPopupPage.aspx.cs" Inherits="Bill_Sys_ReceivedReportPopupPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../js/lib/jquery-1.11.2.min.js"></script>
    <script src="../js/lib/PoupupDignosisCode.js" type="text/javascript"></script>
    <style>
        .hiddencol {
            display: none;
        }

        .page-title {
            font-family: Cambria;
            font-size: 18px;
            background-color: #336699;
            width: 100%;
            margin-top: 20px;
            color: White;
            min-height: 32px;
        }

        .GridTable {
            border: #8babe4 1px solid;
        }

        .GridHeader {
            font-weight: bold;
            font-size: 12px;
            font-family: arial;
            background-color: #8babe4;
        }

            .GridHeader th {
                padding: 3px;
            }

        .GridRow {
            border: #8babe4 1px solid;
            background-color: #fff;
        }

            .GridRow td {
                border: #8babe4 1px solid;
                padding: 2px;
                font-size: 12px;
                font-family: arial;
            }

        .SectionDevider {
            height: 10px;
        }

        .ui-widget {
            position: absolute;
        }
    </style>
    <script type="text/javascript">
        function ReloadParent() {
            parent.location.reload();
        }
    </script>
    <style type="text/css">
        .ajax__tab_theme .ajax__tab_header {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
        }

        .ajax__tab_theme1 .ajax__tab_header1 {
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
            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
        </div>
        <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
            Height="520px" Width="100%" OnActiveTabChanged="tabcontainerDiagnosisCode_ActiveTabChanged">


            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0" CssClass="ajax__tab_header">
                <HeaderTemplate>
                    <div style="width: 200px; height: 20px; font-weight: bold; font-size: small;">
                        Associate Diagnosis Code
                    </div>
                </HeaderTemplate>
                <ContentTemplate>

                    <div class="container-fluid">
                        <asp:ScriptManager ID="scriptmanager1" runat="server" AsyncPostBackTimeout="36000">
                        </asp:ScriptManager>
                        <div style="width: 100%">
                            <div style="width: 48%; float: left; margin-right: 20px">
                                <div class="page-title" style="padding-left: 15px; margin-bottom: 10px">
                                    Search
                                </div>
                                <div class="col-md-6">
                                    <table>

                                        <tr>
                                            <td>
                                                <div class="col-md-offset-2 col-md-4">
                                                    Speciality :
                                                </div>
                                            </td>
                                            <td>
                                                <extddl:ExtendedDropDownList ID="extddlSpecialityDia" runat="server" Connection_Key="Connection_String" Enabled="False" Flag_Key_Value="GET_SPECIALTY" OldText="" Procedure_Name="SP_MST_SPECIALTY_LHR" Selected_Text="---Select---" StausText="False" />
                                            </td>
                                        </tr>
                                        <tr style="height: 5px">
                                            <td></td>
                                        </tr>

                                        <tr style="padding: 10px">
                                            <td>
                                                <div class="col-md-4">
                                                    Type :
                                                </div>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rbl_SZ_TYPE_ID" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="ICD10" Value="ICD10" Selected="True" style="font-family: Cambria; font-weight: 900; font-size: smaller"></asp:ListItem>
                                                    <asp:ListItem Text="ICD9" Value="ICD9" style="font-family: Cambria; font-weight: 900; font-size: smaller"></asp:ListItem>
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
                                                    ServiceMethod="GePopuptDignosisCodes" ServicePath="~/AJAX Pages/PatientService.asmx" UseContextKey="true"
                                                    ContextKey="SZ_COMPANY_ID">
                                                </ajaxToolkit:AutoCompleteExtender>
                                                <asp:LinkButton ID="lnkAddDignosisCode" Text="Select" runat="server"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr style="padding: 10px">
                                            <td></td>
                                            <td>
                                                <div class="col-md-4" style="font-size: medium; padding-left: 50px; font-weight: bold; color: Red; font-family: Cambria">
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
                                            <td></td>
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
                                            <td></td>
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
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-md-4">
                                                            <asp:Button ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="btn-primary" OnClick="btnSeacrh_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" CssClass="btn-primary"
                                                     OnClientClick="if(!CheckSelectedDGCodes()) return false;" OnClick="btnSubmit_Click"/>
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
                                                    <asp:DataGrid ID="grdDiagonosisCode" runat="server" Width="99%" CssClass="GridTable" OnPageIndexChanged="grdDiagonosisCode_PageIndexChanged"
                                                        AutoGenerateColumns="false">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkAssociateDiagnosisCode" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="sz_diagnosis_type" HeaderText="DIAGNOSIS TYPE" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION" Visible="true"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_TYPE_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
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
                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeader" />
                                    </asp:DataGrid>
                                </div>
                                X
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
                                        <ul id="ulInsurance" style="list-style: none; text-align: left">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" ID="tabpnlDeassociate" TabIndex="1" CssClass="ajax__tab_theme">
                <HeaderTemplate>
                    <div style="width: 250px; height: 20px; font-weight: bold; font-family: verdana,tahoma,helvetica; font-size: 11px;">
                        De-Associate Diagnosis Code
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div>
                        <asp:DataGrid Width="100%" ID="grdAssignedDiagnosisCode" runat="server"
                            CssClass="GridTable" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="Diagnosis CodeID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="Diagnosis Code Description"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                            <PagerStyle Mode="NumericPages" />

                        </asp:DataGrid>
                    </div>

                    <td>
                        <div align="center">
                            <asp:Button ID="btnDeassociateDiagCode" runat="server" Text="De-Associate" Width="104px"
                                CssClass="Buttons" OnClick="btnDeassociateDiagCode_Click" />
                        </div>
                    </td>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" TabIndex="1" CssClass="ajax__tab_theme">
                <HeaderTemplate>

                    <div style="width: 250px; height: 20px; font-weight: bold; font-family: verdana,tahoma,helvetica; font-size: 11px;">
                        Update
                    </div>
                </HeaderTemplate>
                <ContentTemplate>


                    <div>
                        <tr>
                            <td class="ContentLabel" style="text-align: left; height: 20px;" colspan="6" align="center">
                                <asp:Label CssClass="message-text" ID="Label1" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
                                <div id="ErrorDiv" style="color: red;" visible="true">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td id="redingdoctd1" runat="server" style="width: 131px;" align="left">Reading Doctor</td>
                            <td id="redingdoctd2" runat="server" style="width: 278px" align="left">
                                <extddl:ExtendedDropDownList ID="extddlReadingDoctor" runat="server" Width="213px" Connection_Key="Connection_String"
                                    Procedure_Name="SP_MST_READINGDOCTOR" Selected_Text="---Select---" Flag_Key_Value="GETDOCTORLIST" Flag_ID="txtCompanyID.Text.ToString();" />
                            </td>

                            <td id="extratd" runat="server" colspan="2"></td>
                            <td style="width: 10%">
                                <asp:Button
                                    ID="Btn_Update" runat="server" Text="Update" Width="84px" CssClass="Buttons" OnClick="Btn_Update_Click" /></td>
                            <td width="20%"></td>
                        </tr>
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
