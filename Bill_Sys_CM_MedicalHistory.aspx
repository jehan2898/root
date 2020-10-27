<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_MedicalHistory.aspx.cs" Inherits="Bill_Sys_CM_MedicalHistory"
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
                                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Text="Patient's Name" ></asp:Label>
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
                                        <td align="left">
                                            <asp:Label ID="lbl_medical_history" Font-Bold="True" runat="server" Text="MEDICAL HISTORY"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_pre_existing_condition" runat="server" Text="Does the patient’s medical history reveal any pre-existing condition(s) that may affect the treatment and/or prognosis?"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <%--<asp:RadioButton ID="RDO_MEDICAL_HISTORY_AFFECT_TREATMENT_YES" GroupName="RDO_MEDICAL_HISTORY_AFFECT_TREATMENT"
                                                runat="server" Text="Yes" />
                                                
                                                <asp:RadioButton ID="RDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO" runat="server" GroupName="RDO_MEDICAL_HISTORY_AFFECT_TREATMENT"
                                                Text="No If Yes, list and describe" />--%>
                                                 <asp:RadioButtonList ID="RDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO" runat="server"  RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0"> YES</asp:ListItem>
                                                        <asp:ListItem Value="1">NO</asp:ListItem>
                                                        </asp:RadioButtonList>
                             
                                                
                                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            If Yes, list and describe
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TXT_MEDICAL_HISTORY_AFFECT_TREATMENT_DESCRIBE" TextMode="multiLine"
                                                runat="server" Width="765px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                <tr>
                                <td visible="false"> <asp:TextBox ID ="txtRDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO" runat="server" Visible="false"></asp:TextBox></td>
                                <td visible="false"> <asp:TextBox ID ="txtEventID" runat="server" Visible="false"></asp:TextBox></td>
                                </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="left" colspan="6">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Medical"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_medical_none" runat="server" Text="None"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_NONE" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_medical_diabetes" runat="server" Text="Diabetes"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_DIABETES" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_HEART_ATTACK" runat="server" Text="Heart Attack"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_HEART_ATTACK" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_SEIZURES" runat="server" Text="Seizures"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_SEIZURES" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_HIGH_BLOOD_PRESSURE" runat="server" Text="High Blood Pressure"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_HIGH_BLOOD_PRESSURE" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_ANGINA_CHEST_PAIN" runat="server" Text="Angina/Chest Pain"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_ANGINA_CHEST_PAIN" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_PHLEBITIS" runat="server" Text="Phlebitis"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_PHLEBITIS" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_MITRAL_VALVE_PROLAPSE" runat="server" Text="Mitral Valve Prolapse"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_MITRAL_VALVE_PROLAPSE" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_ANGIOPLASTY" runat="server" Text="Angioplasty"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_ANGIOPLASTY" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_HEPATITIS" runat="server" Text="Hepatitis"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_HEPATITIS" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_BLEEDING_DISORDER" runat="server" Text="Bleeding Disorder"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_BLEEDING_DISORDER" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_STROKE_TIA" runat="server" Text="Stroke/TIA’s"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_STROKE_TIA" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_ULCERS" runat="server" Text="Ulcers"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_ULCERS" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_CIRCULATION_DISORDER" runat="server" Text="Circulation Disorder"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_CIRCULATION_DISORDER" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_ANEMIA" runat="server" Text="Anemia"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_ANEMIA" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_HIATAL_HERNIA" runat="server" Text="Hiatal Hernia"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_HIATAL_HERNIA" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_BACK_PAIN" runat="server" Text="Back Pain"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_BACK_PAIN" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_ARTHRITIS" runat="server" Text="Arthritis"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_ARTHRITIS" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_OSTEOPOROSIS" runat="server" Text="Osteoporosis"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_OSTEOPOROSIS" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_SCAR_FORMER" runat="server" Text="Scar Former"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_SCAR_FORMER" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_THYROID_DISORDER" runat="server" Text="Thyroid Disorder"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_THYROID_DISORDER" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_ASTHMA" runat="server" Text="Asthma"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_ASTHMA" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_KIDNEY_DISORDER" runat="server" Text="Kidney Disorder"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_KIDNEY_DISORDER" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_CIRRHOSIS" runat="server" Text="Cirrhosis"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_CIRRHOSIS" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lbl_MEDICAL_CANCER" runat="server" Text="Cancer"></asp:Label>
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:TextBox ID="TXT_MEDICAL_CANCER" runat="server" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="4" align="left">
                                            <asp:Label ID="lbl_allergies" Font-Bold="True" runat="server" Text="ALLERGIES"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_NONE" runat="server" Text="None" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_PENICILLIN" runat="server" Text="Penicillin" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_ASPIRIN" runat="server" Text="Aspirin" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_CODEINE" runat="server" Text="Codeine" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_NOVOCAINE" runat="server" Text="Novocaine" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_IODINE" runat="server" Text="Iodine" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_TAPE" runat="server" Text="Tape" />
                                        </td>
                                        <td class="lbl">
                                            <asp:CheckBox ID="CHK_ALLERGIES_OTHER" runat="server" Text="Other" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="3" align="left">
                                            <asp:Label ID="lbl_medication" Font-Bold="True" runat="server" Text="MEDICATION"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="1."></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_MEDICATIONS_1" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="3."></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_MEDICATIONS_3" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="5."></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_MEDICATIONS_5" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="2."></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_MEDICATIONS_2" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="4."></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_MEDICATIONS_4" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="6.None"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_MEDICATIONS_6" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td width="100%" class="TDPart">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="3" width="100%" class="lbl" style="height: 15px">
                                                        <b>PREVIOUS SURGERIES AND HOSPITALIZATIONS</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl" width="33%">
                                                        1.
                                                        <asp:TextBox ID="TXT_PREVIOUS_SURGERIES_1" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="lbl" width="33%">
                                                        3.
                                                        <asp:TextBox ID="TXT_PREVIOUS_SURGERIES_3" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="lbl" style="width: 10%">
                                                        5. &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp;&nbsp;
                                                        <asp:TextBox ID="TXT_PREVIOUS_SURGERIES_5" runat="server"></asp:TextBox></td>
                                                    <td class="lbl" style="width: 32%">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="lbl" width="33%">
                                                        2.
                                                        <asp:TextBox ID="TXT_PREVIOUS_SURGERIES_2" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="lbl" width="33%">
                                                        4.
                                                        <asp:TextBox ID="TXT_PREVIOUS_SURGERIES_4" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="lbl" style="width: 33%">6 None &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .<asp:TextBox ID="TXT_PREVIOUS_SURGERIES_6" runat="server"></asp:TextBox></td>
                                                    <td class="lbl" width="33%">
                                                       <%-- 6 None.--%>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="4" width="100%" class="lbl">
                                            <b>FAMILY HISTORY</b></td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="lbl">
                                            NONE
                                        </td>
                                        <td width="24%" class="lbl">
                                            <asp:TextBox ID="TXT_FAMILY_NONE" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="25%" class="lbl">
                                            Diabetes
                                        </td>
                                        <td width="25%" class="lbl">
                                            <asp:TextBox ID="TXT_FAMILY_DIABETES" runat="server"></asp:TextBox>
                                        </td>
                                        <td width="16%" class="lbl">
                                            High Blood Pressure
                                        </td>
                                        <td width="16%" class="lbl">
                                            <asp:TextBox ID="TXT_FAMILY_BLOOD_PRESSURE" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="16%" class="lbl">
                                            Bleeding Tendencies
                                        </td>
                                        <td width="16%" class="lbl">
                                            <asp:TextBox ID="TXT_FAMILY_BLEEDING_TENDENCIES" runat="server"></asp:TextBox>
                                        </td>
                                        <td width="16%" class="lbl">
                                            Cancer
                                        </td>
                                        <td width="16%" class="lbl">
                                            <asp:TextBox ID="TXT_FAMILY_CANCER" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="16%" class="lbl">
                                            Orthopedics Disease
                                        </td>
                                        <td width="16%" class="lbl">
                                            <asp:TextBox ID="TXT_FAMILY_ORTHOPEDICS_DISEASE" runat="server"></asp:TextBox>
                                        </td>
                                        <td width="16%" class="lbl">
                                            Bone or Joint Problems
                                        </td>
                                        <td width="16%" class="lbl">
                                            <asp:TextBox ID="TXT_FAMILY_BONE_JOINT_PROBLEMS" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td class="lbl">
                                            <b>SOCIAL HISTORY</b>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td class="lbl" style="width: 10%">
                                            <asp:CheckBox ID="CHK_SMOKING" Text="Smoking" runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:TextBox ID="TXT_SMOKING_PACKS_PER_DAY" Width="90%" runat="server"></asp:TextBox>
                                        </td>
                                        <td width="15%" class="lbl">
                                            Packs Per day for
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:TextBox ID="TXT_SMOKING_YEARS" Width="90%" runat="server"></asp:TextBox>
                                        </td>
                                        <td width="55%">
                                            Years.
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" class="lbl">
                                    <tr>
                                        <td colspan="4" width="100%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_QUIT_SMOKING" Text="Quit Smoking" runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_QUIT_SMOKING_THIS_YEAR" Text="this year/<1 year" runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_QUIT_SMOKING_1_5_YEARS" Text="1-5 years " runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_QUIT_SMOKING_GREATER_THAN_10_YEARS" Text=">10 years" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_ALCOHOL" Text="Alcohol" runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_ALCOHOL_DAILY" Text="Daily" runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_ALCOHOL_1_2WEEK" Text="1-2 x/week" runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_ALCOHOL_1_2MONTH" Text="1-2 x/month" runat="server" />
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:CheckBox ID="CHK_ALCOHOL_1_2YEAR" Text="1-2 x/year" runat="server" />
                                        </td>
                                        <td class="lbl" width="5%">
                                            Quantity
                                        </td>
                                        <td width="10%" class="lbl">
                                            <asp:TextBox ID="TXT_ALCOHOL_QUANTITY" Width="90%" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td class="lbl" width="25%">
                                            <asp:CheckBox ID="CHK_RECREATIONAL_DRUGS" Text="Recreational drugs" runat="server" />
                                        </td>
                                        <td class="lbl" width="5%">
                                            What?
                                        </td>
                                        <td width="60%">
                                            <asp:TextBox ID="TXT_RECREATIONAL_DRUGS_WHAT" Width="90%" runat="server"></asp:TextBox>
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
                                <table>
                                    <tr>
                                        <td class="lbl" width="10%" style="height: 14px">
                                            <b>VITAL SIGNS</b>
                                        </td>
                                        <td class="lbl" width="5%" style="height: 14px">
                                            Pulse
                                        </td>
                                        <td class="lbl" width="10%" style="height: 14px">
                                            <asp:TextBox ID="TXT_VITAL_SIGNS_PULSE" Width="90%" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="lbl" width="5%" style="height: 14px">
                                            Height
                                        </td>
                                        <td class="lbl" width="10%" style="height: 14px">
                                            <asp:TextBox ID="TXT_VITAL_SIGNS_HEIGHT" Width="90%" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="lbl" width="10%" style="height: 14px">
                                            Blood Pressure
                                        </td>
                                        <td class="lbl" width="10%" style="height: 14px">
                                            <asp:TextBox ID="TXT_VITAL_SIGNS_BLOOD_PRESSURE" Width="90%" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="lbl" width="5%" style="height: 14px">
                                            Weight
                                        </td>
                                        <td class="lbl" style="height: 14px; width: 10%;">
                                            <asp:TextBox ID="TXT_VITAL_SIGNS_WEIGHT" Width="90%" runat="server"></asp:TextBox>
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
            </td>
        </tr>
    </table>
</asp:Content>
