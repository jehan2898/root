<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_NeuroLogicalExamination.aspx.cs" Inherits="Bii_Sys_IM_NeuroLogicalExamination"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td width="100%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 100%;" class="TDPart">
                            <table style="width: 100%;">
                                <tr>
                                    <td width="25%" class="lbl" style="height: 30px">
                                        Patient's Name
                                    </td>
                                    <td style="width: 40%; height: 30px">
                                        <asp:TextBox ID="txtPatientName" runat="server" Width="90%" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td width="10%" class="lbl" style="height: 30px">
                                        Date of accident
                                    </td>
                                    <td style="width: 25%; height: 30px">
                                        <asp:TextBox ID="txtDOA" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="20%">
                                        Date of Examination</td>
                                    <td style="width: 40%">
                                        <asp:TextBox ID="txtDOE" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="lbl" width="10%">
                                        Date of birth</td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtDOB" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent"
                                            BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" class="TDPart">
                            <table width="100%">
                                <tr>
                                    <td colspan="4" class="lbl">
                                        <b>NEUROLOGICAL EXAMINATION</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="20%">
                                        <b>CEREBRAL :</b></td>
                                    <td class="lbl" colspan="4">
                                        <asp:TextBox ID="txtNeurological_Cerebral" runat="server" Text="" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="20%" style="height: 39px">
                                        <b>CEREBELLAR :</b></td>
                                    <td class="lbl" style="width: 42%; height: 39px;">
                                        <asp:TextBox ID="txtNeurological_Cerebellar" Text="" runat="server" Width="85%"></asp:TextBox>
                                    </td>
                                    <td class="lbl" style="width: 14%; height: 39px;">
                                        Rhomberg test was-</td>
                                    <td width="20%" class="lbl" style="height: 39px">
                                        <asp:RadioButtonList ID="rdlRhomberg_test" runat="server" Width="60%" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Postive</asp:ListItem>
                                            <asp:ListItem Value="1">Negative</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="20%">
                                        <b>FUNCTIONAL :</b></td>
                                    <td class="lbl" colspan="3">
                                        <asp:CheckBox ID="chkAdl_Transfer" runat="server" Text="ADL transfer"></asp:CheckBox>/
                                        <asp:CheckBox ID="chkAmbulating_Independently" runat="server" Text="Ambulating independently ">
                                        </asp:CheckBox>/
                                        <asp:CheckBox ID="chkWith_Assistance" runat="server" Text="With assistance."></asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" visible="false">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtrdlRhomberg_test" runat="server" Visible="false"></asp:TextBox>
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
                                        <asp:CheckBox ID="chkPhysical_Examination_None" runat="server" Text="None at present">
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
                                        <asp:CheckBox ID="chkchkPhysical_Examination_Bruising" runat="server" Text="Bruising ">
                                        </asp:CheckBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Bruising" runat="server" Width="95%"></asp:TextBox>
                                    </td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Neuromuscular_Findings" runat="server"
                                            Text="Neuromuscular Findings "></asp:CheckBox></td>
                                    <td width="25%" class="lbl">
                                        <asp:TextBox ID="txtPhysical_Examination_Neuromuscular_Findings" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%" style="height: 22px">
                                        <asp:CheckBox ID="chkchkPhysical_Examination_Burns" runat="server" Text="Burns"></asp:CheckBox></td>
                                    <td class="lbl" style="width: 25%; height: 22px;">
                                        <asp:TextBox ID="txtPhysical_Examination_Burns" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%; height: 22px;">
                                        <asp:CheckBox ID="chkPhysical_Examination_Abnormal_Restricted_Rom" runat="server"
                                            Text="Abnormal/Restricted ROM"></asp:CheckBox>
                                    </td>
                                    <td width="25%" class="lbl" style="height: 22px">
                                        <asp:TextBox ID="txtPhysical_Examination_Abnormal_Restricted_Rom" runat="server"
                                            Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkchkPhysical_Examination_Crepitation" runat="server" Text="Crepitation ">
                                        </asp:CheckBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Crepitation" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Active_Rom" runat="server" Text="Active ROM">
                                        </asp:CheckBox></td>
                                    <td width="25%" class="lbl">
                                        <asp:TextBox ID="txtPhysical_Examination_Active_Rom" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Deformity" runat="server" Text="Deformity">
                                        </asp:CheckBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Deformity" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Passive_Rom" runat="server" Text="Passive ROM">
                                        </asp:CheckBox></td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Passive_Rom" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Edema" runat="server" Text="Edema "></asp:CheckBox>
                                    </td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Edema" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Gait" runat="server" Text="Gait " /></td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Gait" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Hematoma_Lump_Swelling" runat="server"
                                            Text="Hematoma/Lump/Swelling  " /></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Hematoma_Lump_Swelling" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Hematoma_Palpable_Muscle_Spasm" runat="server"
                                            Text="Palpable Muscle Spasm " /></td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Hematoma_Palpable_Muscle_Spasm" runat="server"
                                            Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Joint_Effusion" runat="server" Text="Joint Effusion " />
                                    </td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Joint_Effusion" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Reflexes" runat="server" Text="Reflexes" />
                                    </td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Reflexes" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Laceration_Sutures" runat="server" Text="Laceration/Sutures " /></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Laceration_Sutures" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Sensation" runat="server" Text="Sensation ">
                                        </asp:CheckBox></td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Sensation" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_PAIN_TENDERNESS" runat="server" Text="Pain/Tenderness" /></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_PAIN_TENDERNESS" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="CHK_PHYSICAL_EXAMINATION_STRENGTH" runat="server" Text="Strength (Weakness">
                                        </asp:CheckBox></td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="TXT_PHYSICAL_EXAMINATION_STRENGTH" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Scar" runat="server" Text="Scar" /></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Scar" runat="server" Width="95%"></asp:TextBox></td>
                                    <td class="lbl" style="width: 25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Wasting_Muscle_Atrophy" runat="server"
                                            Text="Wasting/Muscle Atrophy " />
                                    </td>
                                    <td class="lbl" width="25%">
                                        <asp:TextBox ID="txtPhysical_Examination_Wasting_Muscle_Atrophy" runat="server" Width="95%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="lbl" width="25%">
                                        <asp:CheckBox ID="chkPhysical_Examination_Other" runat="server" Text="Other Findings " /></td>
                                    <td class="lbl" style="width: 25%" colspan="3">
                                        <asp:TextBox ID="txtPhysical_Examination_Other" runat="server" Width="99%"></asp:TextBox></td>
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
                                        <asp:TextBox ID="txtDescribed_diagnostic_Text" Text="" runat="server" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                                        <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
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
