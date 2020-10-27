<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateDignosisCodeCase.aspx.cs"
    Inherits="Bill_Sys_AssociateDignosisCodeCase" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    <script>
//        function validate() {
//            if (document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_extddlSpeciality') != null) {
//                if (document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_extddlSpeciality').value == 'NA') {
//                    alert('Select Speciality ...!');
//                    return false;
//                }
//                else
//                    return true;
//            }
//            else {
//                alert('Select Speciality ...!');
//                return false;
//            }
//        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
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
                                        <div id="ErrorDiv" style="color: red" visible="true">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ajaxToolkit:TabContainer ID="tabcontainerDiagnosisCode" runat="Server" ActiveTabIndex="0"
                                            CssClass="ajax__tab_theme">
                                            <ajaxToolkit:TabPanel runat="server" ID="tabpnlAssociate" TabIndex="0">
                                                <HeaderTemplate>
                                                    <div style="width: 200px; height: 200px;" class="lbl">
                                                        Associate Diagnosis Code</div>
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="100%" scope="col">
                                                                <div class="blocktitle">
                                                                    <div class="div_blockcontent">
                                                                        <table width="100%" border="0">
                                                                            <tr>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30%;">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr style="width: 100%">
                                                                                                        <td style="text-align: center; font-size: medium; font-weight: normal; width: 50%">
                                                                                                            Search
                                                                                                        </td>
                                                                                                        <td style="text-align: center; font-size: medium; font-weight: normal; width: 50%">
                                                                                                            Parameters
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 6px">
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr runat="server" id="trDoctorType">
                                                                                            <td id="Td1" runat="server" style="text-align: left; width: 110px">
                                                                                                Diagnosis Type
                                                                                            </td>
                                                                                            <td id="Td2" style="text-align: left" runat="server">
                                                                                                &nbsp; &nbsp;<extddl:ExtendedDropDownList ID="extddlDiagnosisType" runat="server"
                                                                                                    Width="113px" Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE"
                                                                                                    Selected_Text="--- Select ---" Flag_Key_Value="DIAGNOSIS_TYPE_LIST" OldText=""
                                                                                                    StausText="False"></extddl:ExtendedDropDownList>
                                                                                            </td>
                                                                                            <%--<td id="Td3" class="ContentLabel" runat="server" >
                                                                                                Code &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td id="Td4" runat="server"  >
                                                                                                <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                                                            </td>--%>
                                                                                            <%--<td id="Td5" class="ContentLabel" runat="server" >
                                                                                                Description &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td id="Td6"  class="ContentLabel" runat="server">
                                                                                                <asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                                            </td>--%>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td id="Td3" runat="server" style="text-align: left">
                                                                                                Code &nbsp;&nbsp;
                                                                                            </td>
                                                                                            <td id="Td4" runat="server" style="text-align: left">
                                                                                                &nbsp &nbsp<asp:TextBox ID="txtDiagonosisCode" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td id="Td5" runat="server" style="text-align: left">
                                                                                                Description
                                                                                            </td>
                                                                                            <td id="Td6" runat="server">
                                                                                                &nbsp; &nbsp;<asp:TextBox ID="txtDescription" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 6px">
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="6" align="left">
                                                                                                <asp:Button ID="btnOK" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                                                                    OnClick="btnOK_Click" Visible="False" />
                                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons"
                                                                                                    OnClick="btnCancel_Click" Visible="False" />
                                                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                                                                                    ID="btnSeacrh" runat="server" Text="Search" Width="80px" CssClass="Buttons" OnClick="btnSeacrh_Click" />
                                                                                                &nbsp;<%--<asp:Button
                                                                                        ID="btnAssign" runat="server" Text="Assign" Width="80px" CssClass="Buttons" OnClick="btnAssign_Click" />--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 380px">
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="width: 70%">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                &nbsp; Search result&nbsp; &nbsp;&nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td style="font-size: 14px; vertical-align: top; width: 35%; font-family: arial;
                                                                                                            text-align: left">
                                                                                                            <asp:Label ID="lblSpeciality" runat="server" Text="Speciality" Visible="false"></asp:Label>
                                                                                                        </td>
                                                                                                        <td id="Td7" runat="server" class="ContentLabel">
                                                                                                            <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                                                                                Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                                                                Selected_Text="---Select---" Width="140px" OldText="" StausText="False" Visible="false"></extddl:ExtendedDropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                                                    <tr runat="server" id="Tr1">
                                                                                                        <td width="100%" runat="server" id="Td8" style="text-align: right">
                                                                                                            <div style="overflow:scroll;height: 150px; width: 100%;" >
                                                                                                                <asp:DataGrid Width="60%" Height="150px" ID="grdSpeciality" CssClass="GridTable"
                                                                                                                    runat="server" AutoGenerateColumns="False">
                                                                                                                    <ItemStyle CssClass="GridRow" />
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateColumn HeaderText="Select" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:CheckBox ID="chkSelectspecialty" runat="server" />
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateColumn>
                                                                                                                        <asp:BoundColumn DataField="description" HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                                                                                        <asp:BoundColumn DataField="code" HeaderText="Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                                                                                                    </Columns>
                                                                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                                                                </asp:DataGrid>
                                                                                                            </div>
                                                                                                            <div style="height:10px">
                                                                                                            </div>
                                                                                                            <div style="overflow: scroll; height: 300px; width: 100%; text-align: right">
                                                                                                                <asp:DataGrid Width="100%" Height="200px" ID="grdNormalDgCode" CssClass="GridTable"
                                                                                                                    runat="server" AutoGenerateColumns="False" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged">
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
                                                                                            <td>
                                                                                                <asp:Button ID="btnAssign" runat="server" Text="Assign" Width="80px" CssClass="Buttons"
                                                                                                    OnClick="btnAssign_Click" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <%--<tr>
                                                                                <td colspan="2">
                                                                                    &nbsp; Search result&nbsp; &nbsp;&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
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
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table width="100%" id="tblDiagnosisCodeFirst" runat="server">
                                                                                        <tr runat="server" id="Tr1">
                                                                                            <td width="100%" runat="server" id="Td8">
                                                                                                <div style="overflow: scroll; height: 300px; width: 100%;">
                                                                                                    <asp:DataGrid Width="90%" ID="grdNormalDgCode" CssClass="GridTable" runat="server"
                                                                                                        AutoGenerateColumns="False" OnPageIndexChanged="grdNormalDgCode_PageIndexChanged">
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
                                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                                            <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION">
                                                                                                            </asp:BoundColumn>
                                                                                                            
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
                                                                                <td>
                                                                                    <asp:Button ID="btnAssign" runat="server" Text="Assign" Width="80px" CssClass="Buttons"
                                                                                        OnClick="btnAssign_Click" />
                                                                                </td>
                                                                            </tr>--%>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    Total Count :
                                                                                    <asp:Label ID="lblDiagnosisCount" Font-Bold="true" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
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
                                                                                    <asp:Button ID="btnSearch" runat="server" Width="80px" CssClass="Buttons" Text="Search"
                                                                                        OnClick="btnSearch_Click" Visible="False"></asp:Button>
                                                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                    &nbsp; &nbsp;
                                                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox><asp:TextBox
                                                                                        ID="txtDiagnosisSetID" runat="server" Width="10px" Visible="False"></asp:TextBox>
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
                                                                                    &nbsp;<asp:DataGrid Width="100%" ID="grdAssignedDiagnosisCode" runat="server"
                                                                                        CssClass="GridTable" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdAssignedDiagnosisCode_PageIndexChanged" PageSize="20">
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
                                                                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" ></asp:BoundColumn>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="GridHeader" />
                                                                                        <PagerStyle Mode="NumericPages"  />
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
  
</asp:Content>
