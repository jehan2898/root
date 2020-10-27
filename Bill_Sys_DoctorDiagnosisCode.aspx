<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_DoctorDiagnosisCode.aspx.cs"
    Inherits="Bill_Sys_DoctorDiagnosisCode" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" style="text-align: center" bgcolor="#FBFBFB">
    <form id="frmDoctorDiagnosis" runat="server">
        <div align="center">
            <table cellpadding="0" cellspacing="0" class="simple-table">
                <tr>
                    <td width="9%" height="18">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <div align="right">
                            <span class="top-menu">Home | Logout</span></div>
                    </td>
                    <td width="8%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg" class="top-menu">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="9%" class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
                    <td width="8%" class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="35px" bgcolor="#000000">
                        <div align="left">
                        </div>
                        <div align="left">
                            <span class="pg-bl-usr">Billing company name</span></div>
                    </td>
                    <td width="12%" height="35px" bgcolor="#000000">
                        <div align="right">
                            <span class="usr">Admin</span></div>
                    </td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
                            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label></span></td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="18" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th width="19%" scope="col" style="height: 29px">
                                    <div align="left">
                                        <a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div>
                                </th>
                                <th width="81%" scope="col" style="height: 29px">
                                    <div align="right">
                                        <span class="sub-menu"></span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                    <td class="top-menu" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="6" class="usercontrol">
                                    <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                                </td>
                            </tr>
                            <tr>
                                <th width="9%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                                <th scope="col" style="height: 20px">
                                    <div align="left" class="band">
                                        Doctor Type </div>
                                </th>
                                <th width="8%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                            </tr>
                            <tr>
                                <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
                                    <div align="left">
                                        <table width="55%" border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td colspan="6" height="30">
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Doctor Type
                                                    </div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <cc1:ExtendedDropDownList ID="extddlDoctorType" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_DOCTOR_TYPE" Flag_Key_Value="DOCTOR_TYPE_LIST" Selected_Text="--- Select ---" />
                                                </td>
                                                <td class="tablecellLabel">
                                                    <div class="lbl">
                                                        Diagnosis Code
                                                    </div>
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    <cc1:ExtendedDropDownList ID="extddlDiagnosisCode" Width="255px" runat="server" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_DIAGNOSIS_CODE" Flag_Key_Value="DIAGNOSIS_CODE_LIST" Selected_Text="--- Select ---" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="btn-gray" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="btn-gray" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="btn-gray" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </th>
                            </tr>
                            <tr>
                                <th scope="col">
                                    <div align="left">
                                    </div>
                                    <div align="left" class="band">
                                        Doctor Diagnosis Code List</div>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataGrid ID="grdDoctorDiagnosis" runat="server" OnDeleteCommand="grdDoctorDiagnosis_DeleteCommand"
                                        OnPageIndexChanged="grdDoctorDiagnosis_PageIndexChanged" OnSelectedIndexChanged="grdDoctorDiagnosis_SelectedIndexChanged">
                                        <FooterStyle />
                                        <SelectedItemStyle />
                                        <PagerStyle />
                                        <AlternatingItemStyle />
                                        <ItemStyle />
                                        <Columns>
                                            <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="SZ_DC_DIAGNOSIS_ID" HeaderText="DIAGNOSIS ID" Visible="false">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DOCTOR_TYPE_ID" HeaderText="DOCTOR TYPE ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DOCTOR_TYPE" HeaderText="Doctor Type" Visible="True"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code" Visible="True"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY ID" Visible="false"></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                                        </Columns>
                                        <HeaderStyle />
                                    </asp:DataGrid>
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
