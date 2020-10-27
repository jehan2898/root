<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_PhysicalStatus.aspx.cs" Inherits="Bill_Sys_CM_PhysicalStatus"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHeading" runat="server" Text="INITIAL EXAMINATION" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="left" valign="middle">
                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Text="Patient's Name"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="472px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        <td align="left" valign="middle">
                            <asp:Label ID="lbl_DOA" runat="server" Font-Bold="False" Text="Date Of Accident" ></asp:Label>
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
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table width="100%">
                    <tr>
                        <td colspan="2" width="100%" class="lbl">
                            <b>PHYSICAL EXAMINATION</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="100%" class="lbl">
                            <b>GENERAL APPEARANCE</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="lbl" style="height: 20px">
                            <asp:CheckBox ID="CHK_APPEARANCE_WELL_DEVELOPED" Text="WELL DEVELOPED" runat="server" />
                        </td>
                        <td width="50%" class="lbl" style="height: 20px">
                            <asp:CheckBox ID="CHK_APPEARANCE_WELL_NOURISHED" Text="WELL NOURISHED" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="CHK_APPEARANCE_MILD_DISTRESS" Text="MILD DISTRESS" runat="server" />
                        </td>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="CHK_APPEARANCE_MODERATE_DISTRESS" Text="MODERATE DISTRESS" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="CHK_APPEARANCE_SEVERE_DISTRESS" Text="SEVERE DISTRESS SECONDARY TO PAIN"
                                runat="server" />
                        </td>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="CHK_APPEARANCE_ASSESSMENT_OF_MENTAL_STATUS" Text="BRIEF ASSESSMENT OF MENTAL STATUS (ORIENTED TO TIME, PLACE AND PERSON)"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>HEAD</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            Normocephalic, Atraumatic
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            Eyes : Pupils were equal and reacted to light and accommodation Extraocular muscles
                            were intact.
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            Ears : No blood noted in the external auditory canal.
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>CHEST</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="CHK_NO_DEFORMITIES" Text="No Deformities" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="CHK_PAIN_UPON_PALPATION" runat="server" />
                        </td>
                        <td class="lbl" width="95%">
                            <asp:RadioButtonList ID="RDO_PAIN_UPON_PALPATION" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Positive/</asp:ListItem>
                                <asp:ListItem Value="1">Negative pain upon palpation</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>HEART</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="CHK_HEART_RHYTHM_RATE" Text="The heart was in regular rhythm and rate; normal S1, S2."
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="13%">
                            <asp:CheckBox ID="CHK_HEART_OTHER" Text="Other :" runat="server" />
                        </td>
                        <td class="lbl" width="87%">
                            <asp:TextBox ID="TXT_HEART_OTHER" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart" style="height: 368px">
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl" style="height: 15px">
                            <b>LUNGS</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="CHK_LUNGS_CLEAR_TO_AUSCULTATION" Text="Clear to auscultation bilaterally."
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="12%">
                            <asp:CheckBox ID="CHK_LUNGS_OTHER" Text="Other :" runat="server" />
                        </td>
                        <td class="lbl" width="88%">
                            <asp:TextBox ID="TXT_LUNGS_OTHER" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>ABDOMEN</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="CHK_ABDOMEN_SOFT_NORMATIVE" Text="Soft, normoactive bowel sounds."
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="3%">
                            <asp:CheckBox ID="CHK_ABDOMEN_TENDERNESS" runat="server" />
                        </td>
                        <td class="lbl" width="97%">
                            <asp:RadioButtonList ID="RDO_TENDERNESS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Positive/</asp:ListItem>
                                <asp:ListItem Value="1">Negative Tenderness.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>BACK</b></td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="CHK_NO_KYPHOSCOLIOSIS" Text="No kyphoscoliosis." runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="3%">
                            <asp:CheckBox ID="CHK_BACK_LUMBAR_LORDOSIS" runat="server" />
                        </td>
                        <td class="lbl" width="97%">
                            <asp:RadioButtonList ID="RDO_BACK_LUMBAR_LORDOSIS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Normal/</asp:ListItem>
                                <asp:ListItem Value="1">Increased lumbar lordosis.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl" style="height: 21px">
                            <b>EXTREMITIES</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="CHK_EXTREMITIES_NO_EDEMA" Text="No edema." runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="15%">
                            <asp:CheckBox ID="CHK_EXTREMITIES_EDEMA_AT" Text="Edema at:" runat="server" />
                        </td>
                        <td class="lbl" width="80%">
                            <asp:TextBox ID="TXT_EXTREMITIES_EDEMA_AT" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%" style="height: 29px">
                            Pulses 2+ throughout
                        </td>
                        <td class="lbl" width="80%" style="height: 29px">
                            <asp:TextBox ID="TXT_PULSES_THROUGHOUT" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="lbl" style="height: 15px">
                <b>EXAMINATION</b>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table width="80%">
                    <tr>
                        <td class="lbl" width="20%">
                            <b>SHOULDER </b>
                        </td>
                        <td class="lbl" width="15%">
                            NORMAL ROM
                        </td>
                        <td class="lbl" width="38%">
                            PATIENT’S ROM / STRENGTH
                        </td>
                        <td class="lbl" width="12%">
                            <asp:CheckBox ID="CHK_SHOULDER_LEFT" Text="- LEFT" runat="server" />
                        </td>
                        <td class="lbl" width="13%" style="height: 20px">
                            <asp:CheckBox ID="CHK_SHOULDER_RIGHT" Text="- RIGHT" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td class="lbl" width="21%">
                            FLEXION
                        </td>
                        <td class="lbl" width="10%">
                            150
                        </td>
                        <td class="lbl" width="28%">
                            <asp:TextBox ID="TXT_SHOULDER_FLEXION_PATIENT_ROM_150" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="CHK_SHOULDER_FLEXION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_FLEXION_DULL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_FLEXION_SHARP" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_FLEXION_SPASM" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_FLEXION_INFLAME" Text="I" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="21%">
                            EXTENSION
                        </td>
                        <td class="lbl" width="10%">
                            150
                        </td>
                        <td class="lbl" width="28%">
                            <asp:TextBox ID="TXT_SHOULDER_EXTENSION_PATIENT_ROM_150" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_DULL" Text="D" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_SHARP" Text="H" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_SPASM" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTENSION_INFLAME" Text="N" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="21%" style="height: 29px">
                            ABDUCTION
                        </td>
                        <td class="lbl" width="10%" style="height: 29px">
                            150
                        </td>
                        <td class="lbl" width="28%" style="height: 29px">
                            <asp:TextBox ID="TXT_SHOULDER_ABDUCTION_PATIENT_ROM_150" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="4%" style="height: 29px">
                            <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="4%" style="height: 29px">
                            <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_DULL" Text="U" runat="server" />
                        </td>
                        <td class="lbl" width="4%" style="height: 29px">
                            <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_SHARP" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="4%" style="height: 29px">
                            <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_SPASM" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="4%" style="height: 29px">
                            <asp:CheckBox ID="CHK_SHOULDER_ABDUCTION_INFLAME" Text="F" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="21%">
                            ADDUCTION
                        </td>
                        <td class="lbl" width="10%">
                            30
                        </td>
                        <td class="lbl" width="28%">
                            <asp:TextBox ID="TXT_SHOULDER_ADDUCTION_PATIENT_ROM_30" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_DULL" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_SHARP" Text="R" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_SPASM" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_ADDUCTION_INFLAME" Text="L" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="21%">
                            INTERNAL ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="28%">
                            <asp:TextBox ID="TXT_SHOULDER_INTERNAL_ROTATION_PATIENT_ROM_40" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_DULL" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_SHARP" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_SPASM" Text="M" runat="server" />
                        </td>
                        <td class="lbl" width="4%">
                            <asp:CheckBox ID="CHK_SHOULDER_INTERNAL_ROTATION_INFLAME" Text="A" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            EXTERNAL ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            90
                        </td>
                        <td class="lbl" width="28%">
                            <asp:TextBox ID="TXT_SHOULDER_EXTERNAL_ROTATION_PATIENT_ROM_90" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_DULL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_SHARP" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_SPASM" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="CHK_SHOULDER_EXTERNAL_ROTATION_INFLAME" Text="M" runat="server" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="20%" class="lbl" style="height: 39px">
                            PAIN IN JOINT
                        </td>
                        <td width="15%" class="lbl" style="height: 39px">
                            <asp:RadioButtonList ID="RDO_PAIN_IN_JOINT_LEFT" runat="server" Width="90%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">left</asp:ListItem>
                                <asp:ListItem Value="1">right</asp:ListItem>
                                <asp:ListItem Value="2">both</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td width="15%" class="lbl" style="height: 39px">
                            SHOULDERS
                        </td>
                        <td width="5%" class="lbl" style="height: 39px">
                            <asp:CheckBox ID="CHK_SHOULDERS_SYMMETRICAL" runat="server" />
                        </td>
                        <td width="15%" class="lbl" style="height: 39px">
                            - SYMMETRICAL
                        </td>
                        <td width="5%" class="lbl" style="height: 39px">
                            <asp:CheckBox ID="CHK_SHOULDERS_ASYMMETRICAL" Text="" runat="server" />
                        </td>
                        <td width="15%" class="lbl" style="height: 39px">
                            - ASYMMETRICAL
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" class="lbl">
                            PAIN ACROSS SHOULDER
                        </td>
                        <td width="15%" class="lbl">
                            <asp:RadioButtonList ID="RDO_PAIN_ACROSS_SHOULDERS_LEFT" runat="server" Width="90%"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">left</asp:ListItem>
                                <asp:ListItem Value="1">right</asp:ListItem>
                                <asp:ListItem Value="2">both</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td width="15%" class="lbl">
                            <asp:CheckBox ID="CHK_PAIN_ACROSS_SHOULDERS_MILD" runat="server" />&nbsp;- MILD
                        </td>
                        <td width="5%" class="lbl">
                            <asp:CheckBox ID="CHK_PAIN_ACROSS_SHOULDERS_MODERATE" runat="server" />
                        </td>
                        <td width="15%" class="lbl">
                            - MODERATE
                        </td>
                        <td width="5%" class="lbl">
                            <asp:CheckBox ID="CHK_PAIN_ACROSS_SHOULDERS_SEVERE" Text="" runat="server" />
                        </td>
                        <td width="15%" class="lbl">
                            - SEVERE PAIN
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" class="lbl">
                            LIMITATION OF MOVEMEN
                        </td>
                        <td width="15%" class="lbl">
                            <asp:RadioButtonList ID="RDO_LIMITATION_ON_MOVEMENT_LEFT" runat="server" Width="90%"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">left</asp:ListItem>
                                <asp:ListItem Value="1">right</asp:ListItem>
                                <asp:ListItem Value="2">both</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td width="15%" class="lbl">
                            ON PALPATION
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" style="height: 49px">
                            <asp:RadioButtonList ID="RDO_CREPITUS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 49px">
                            - CREPITIS PRESENT</td>
                        <td class="lbl" style="height: 49px">
                            <asp:RadioButtonList ID="RDO_DROP_ARM_TEST" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 49px">
                            - DROP ARM TEST</td>
                        <td class="lbl" style="height: 49px">
                            <asp:RadioButtonList ID="RDO_APPREHENSION_SIGN" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 49px">
                            - APPREHENSION SIGN</td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:RadioButtonList ID="RDO_PAINFUL_IMPINGEMENT" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            - PAINFUL ARC / IMPINGEMENT SIGN</td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="height: 25px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td class="lbl" style="width: 20%">
                            <b>CERVICAL SPINE</b>
                        </td>
                        <td class="lbl" width="10%" align="left">
                            NORMAL ROM
                        </td>
                        <td class="lbl" width="25%">
                            PATIENT’S ROM / STRENGTH
                        </td>
                        <td class="lbl" width="20%">
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td class="lbl" style="width: 20%">
                            FLEXION
                        </td>
                        <td class="lbl" width="10%">
                            60
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="TXT_CERVICAL_SPINE_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_DULL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_SHARP" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_SPASM" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_FLEXION_INFLAME" Text="I" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%; height: 41px;">
                            EXTENSION
                        </td>
                        <td class="lbl" width="10%" style="height: 41px">
                            50
                        </td>
                        <td class="lbl" width="20%" style="height: 41px">
                            <asp:TextBox ID="TXT_CERVICAL_SPINE_EXTENSION_PATIENT_ROM" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="6%" style="height: 41px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 41px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_DULL" Text="D" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 41px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_SHARP" Text="H" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 41px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_SPASM" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 41px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_EXTENSION_INFLAME" Text="N" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%">
                            LEFT ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            80
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="TXT_CERVICAL_SPINE_LEFT_ROTATION_PATIENT_ROM" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_DULL" Text="U" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_SHARP" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_SPASM" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LEFT_ROTATION_INFLAME" Text="F" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 25%">
                            RIGHT ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            80
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="TXT_CERVICAL_SPINE_RIGHT_ROTATION_PATIENT_ROM" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_DULL" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_SHARP" Text="R" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_SPASM" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RIGHT_ROTATION_INFLAME" Text="L" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 25%; height: 22px;">
                            LT LATERAL FLEXION
                        </td>
                        <td class="lbl" width="10%" style="height: 22px">
                            40
                        </td>
                        <td class="lbl" width="20%" style="height: 22px">
                            <asp:TextBox ID="TXT_CERVICAL_SPINE_LT_LATERAL_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="6%" style="height: 22px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 22px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_DULL" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 22px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_SHARP" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 22px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_SPASM" Text="M" runat="server" />
                        </td>
                        <td class="lbl" width="6%" style="height: 22px">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_LT_LATERAL_FLEXION_INFLAME" Text="A" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 25%">
                            RT LATERAL FLEXION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="20%">
                            <asp:TextBox ID="TXT_CERVICAL_SPINE_RT_LATERAL_FLEXION_PATIENT_ROM" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_NORMAL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_DULL" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_SHARP" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_SPASM" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="6%">
                            <asp:CheckBox ID="CHK_CERVICAL_SPINE_RT_LATERAL_FLEXION_INFLAME" Text="M" runat="server" />
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
                
                
                  <table width="100%" visible="false">
                <tr>
                <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_PAIN_UPON_PALPATION" runat="server" Visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_TENDERNESS" runat="server" Visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtxRDO_BACK_LUMBAR_LORDOSIS" runat="server" Visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_PAIN_IN_JOINT_LEFT" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                
                <tr>
                <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_PAIN_ACROSS_SHOULDERS_LEFT" runat="server" Visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_LIMITATION_ON_MOVEMENT_LEFT" runat="server" Visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_CREPITUS" runat="server" Visible="false"></asp:TextBox>
                </td>
                     <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_DROP_ARM_TEST" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                
                
                <tr>
                <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_PAINFUL_IMPINGEMENT" runat="server" Visible="false"></asp:TextBox>
                </td>
                
                     <td visible="false" style="height: 22px">
                     
                <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                </td>
                 <td visible="false" style="height: 22px">
                <asp:TextBox ID="txtRDO_APPREHENSION_SIGN" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
