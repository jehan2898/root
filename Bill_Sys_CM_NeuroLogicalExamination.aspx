<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_NeuroLogicalExamination.aspx.cs" Inherits="Bill_Sys_CM_NeuroLogicalExamination"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHeading" runat="server" Text="INITIAL EXAMINATION" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Text="Patient's Name"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="472px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lbl_DOA" runat="server" Font-Bold="False" Text="Date Of Accident"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOE" runat="server" Font-Bold="False" Text="Date Of Examination"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOE" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="LBL_DOB" runat="server" Font-Bold="False" Text="Date Of Birth"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="TXT_DOB" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 19px">
                                            <b><u>NEUROLOGICAL EXAMINATION </u></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="20%">
                                            <b>CEREBRAL :</b></td>
                                        <td class="lbl" colspan="4">
                                            <asp:TextBox ID="TXT_NEUROLOGICAL_CEREBRAL" runat="server" Text="" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="10%">
                                            <b>CEREBELLAR :</b></td>
                                        <td class="lbl" style="width: 42%">
                                            <asp:TextBox ID="TXT_NEUROLOGICAL_CEREBELLAR" Text="" runat="server" Width="85%"></asp:TextBox>
                                        </td>
                                        <td class="lbl" style="width: 14%">
                                            Rhomberg test was-</td>
                                        <td width="30%" class="lbl">
                                            <asp:RadioButtonList ID="RDO_RHOMBERG_TEST" runat="server" Width="60%" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Postive</asp:ListItem>
                                                <asp:ListItem Value="1">Negative</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="20%">
                                            <b>FUNCTIONAL :</b></td>
                                        <td class="lbl" colspan="3">
                                            <asp:CheckBox ID="CHK_ADL_TRANSFER" runat="server" Text="ADL transfer"></asp:CheckBox>/
                                            <asp:CheckBox ID="CHK_AMBULATING_INDEPENDENTLY" runat="server" Text="Ambulating independently ">
                                            </asp:CheckBox>/
                                            <asp:CheckBox ID="CHK_WITH_ASSISTANCE" runat="server" Text="With assistance."></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" colspan="4" style="height: 15px">
                                            Physical examination: Check all relevant objective findings and identify specific
                                            affected body part(s).</td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_NONE" runat="server" Text="None at present">
                                            </asp:CheckBox>
                                        </td>
                                        <td class="lbl" style="width: 25%">
                                        </td>
                                        <td class="lbl" style="width: 25%">
                                        </td>
                                        <td width="25%" class="lbl">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_BRUISING" runat="server" Text="Bruising ">
                                            </asp:CheckBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_BRUISING" runat="server" Width="95%"></asp:TextBox>
                                        </td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_NEUROMUSCULAR_FINDINGS" runat="server"
                                                Text="Neuromuscular Findings "></asp:CheckBox></td>
                                        <td width="25%" class="lbl">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_NEUROMUSCULAR_FINDINGS" runat="server"
                                                Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%" style="height: 22px">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_BURNS" runat="server" Text="Burns"></asp:CheckBox></td>
                                        <td class="lbl" style="width: 25%; height: 22px;">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_BURNS" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%; height: 22px;">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_ABNORMAL_RESTRICTED_ROM" runat="server"
                                                Text="Abnormal/Restricted ROM"></asp:CheckBox>
                                        </td>
                                        <td width="25%" class="lbl" style="height: 22px">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_ABNORMAL_RESTRICTED_ROM" runat="server"
                                                Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_CREPITATION" runat="server" Text="Crepitation ">
                                            </asp:CheckBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_CREPITATION" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_ACTIVE_ROM" runat="server" Text="Active ROM">
                                            </asp:CheckBox></td>
                                        <td width="25%" class="lbl">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_ACTIVE_ROM" runat="server" Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_DEFORMITY" runat="server" Text="Deformity">
                                            </asp:CheckBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_DEFORMITY" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_PASSIVE_ROM" runat="server" Text="Passive ROM">
                                            </asp:CheckBox></td>
                                        <td class="lbl" width="25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_PASSIVE_ROM" runat="server" Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_EDEMA" runat="server" Text="Edema "></asp:CheckBox>
                                        </td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_EDEMA" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_GAIT" runat="server" Text="Gait " /></td>
                                        <td class="lbl" width="25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_GAIT" runat="server" Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_HEMATOMA_LUMP_SWELLING" runat="server"
                                                Text="Hematoma/Lump/Swelling  " /></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_HEMATOMA_LUMP_SWELLING" runat="server"
                                                Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_PALPABLE_MUSCLE_SPASM" runat="server"
                                                Text="Palpable Muscle Spasm " /></td>
                                        <td class="lbl" width="25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_PALPABLE_MUSCLE_SPASM" runat="server" Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_JOINT_EFFUSION" runat="server" Text="Joint Effusion " />
                                        </td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_JOINT_EFFUSION" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_REFLEXES" runat="server" Text="Reflexes" />
                                        </td>
                                        <td class="lbl" width="25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_REFLEXES" runat="server" Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_LACERATION_SUTURES" runat="server" Text="Laceration/Sutures " /></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_LACERATION_SUTURES" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_SENSATION" runat="server" Text="Sensation ">
                                            </asp:CheckBox></td>
                                        <td class="lbl" width="25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_SENSATION" runat="server" Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_PAIN_TENDERNESS" runat="server" Text="Pain/Tenderness" /></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_PAIN_TENDERNESS" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_STRENGTH" runat="server" Text="Strength (Weakness) ">
                                            </asp:CheckBox></td>
                                        <td class="lbl" width="25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_STRENGTH" runat="server" Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_SCAR" runat="server" Text="Scar" /></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_SCAR" runat="server" Width="95%"></asp:TextBox></td>
                                        <td class="lbl" style="width: 25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_WASTING_MUSCLE_ATROPHY" runat="server"
                                                Text="Wasting/Muscle Atrophy " />
                                        </td>
                                        <td class="lbl" width="25%">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_WASTING_MUSCLE_ATROPHY" runat="server"
                                                Width="95%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_OTHER" runat="server" Text="Other Findings " /></td>
                                        <td class="lbl" style="width: 25%" colspan="3">
                                            <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_OTHER" runat="server" Width="99%"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td class="lbl" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            Described any diagnostic test(s) rendered at this visit:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl" width="100%">
                                            <asp:TextBox ID="TXT_DESCRIBE_DIAGNOSTIC_TEST" Text="" runat="server" Width="97%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                    
                </table>
                
                <table>
                <tr>
                <td>
                 <table width="100%" visible="false">
                    <tr visible="false">
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_RHOMBERG_TEST" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    
                   
                    <td>
                    <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    </tr>
                    </table>
                </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
