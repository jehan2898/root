<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CO_Chiro_Treatment_Plan.aspx.cs" Inherits="Bill_Sys_CO_Chiro_Treatment_Plan"
    Title="Untitled Page" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" style="width: 842px">
             <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left" style="height: 22px" class="lbl">
                                <asp:Label ID="lbl_RE" runat="server" CssClass="lbl" Text="RE" Width="28px"></asp:Label></td>
                            <td align="left" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px; width: 398px;" class="lbl">
                                <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="300px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                            <td align="right" style="height: 22px" class="lbl">
                                Date Of Examination</td>
                            <td align="right" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px">
                                <asp:TextBox ID="TXT_DOE" runat="server" Width="90px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 22px" class="lbl">
                                <asp:Label ID="lbl_AGE" runat="server" Text="Age" CssClass="lbl" Width="73px"></asp:Label></td>
                            <td align="left" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px; width: 398px;" class="lbl">
                                <asp:TextBox ID="TXT_AGE" runat="server" Width="90px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                            <td align="right" style="height: 22px" class="lbl">
                                Date Of Accident</td>
                            <td align="right" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px">
                                <asp:TextBox ID="TXT_DOA" runat="server" Width="90px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" class="lbl">
                                <asp:Label ID="lbl_SEX" runat="server" Text="Sex" CssClass="lbl" Width="75px"></asp:Label></td>
                            <td align="left">
                                &nbsp;</td>
                            <td colspan="4" class="lbl">
                                &nbsp;&nbsp;<asp:RadioButtonList ID="RDO_SEX" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" CssClass="lbl" Enabled="False">
                                    <asp:ListItem Value="0">M</asp:ListItem>
                                    <asp:ListItem Value="1">F</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lbl_TREATMENT_PLAN" Font-Bold runat="server" Text="TREATMENT PLAN"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="100%">
                    <tbody>
                        <tr>
                            <td align="justify">
                                <p class="lbl">
                                    Chiropractic Manipulative (CMT) is a form of manual treatment that influences joints
                                    and neurological function. This treatment may be accomplished using a variety of
                                    techniques. Treatment will be consisted of gentle chiropratic manipulation to the
                                    appropriate parts of the spine. Various soft tissue techniques such as ischemic
                                    compression to myofascial trigger point, stretching, and manual release therapy
                                    will be utilized. The frequency of treatment will be modified as appropriate Chiropractic
                                    manipulative therapy places emphasis on correction of the joint segmental dysfunction
                                    of vertebra, which are resistant to normal segmental joint motion. This treatment
                                    protocal has been proven to be effective in eventual correction of intercaseus disrelationships
                                    to remove nerve interference and restore function. No other discipline renders this
                                    treatment. The patient will be educated and instructed in specific everyday home
                                    exercise. The short-term goal of this plan is to decrease pain and increase range
                                    of motion. The long term goal is to restore and maximize patients prior function.
                                </p>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table>
                    <tbody>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_MANAGEMENT_PLAN" Font-Bold=True runat="server" Text="MANAGEMENT PLAN" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="justify" colspan="3">
                                <p class="lbl">
                                    The patient will be scheduled to undergo the following diagnostic test, which are
                                    clinically necessary to form an effective treatment plan and confirm the initial
                                    diagnosis.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:CheckBox ID="CHK_X_RAY_TO_RULE_OUT_FRACTURE_DISLOCATION_BONE_PATHOLOGY" runat="server"
                                    Text="X-Ray to rule out fracture,dislocation,or bone pathology." CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_MANAGEMENT_PLAN_CERVICAL_SPINE" runat="server" Text="Cervical Spine" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_MANAGEMENT_PLAN_THORACIC_SPINE" runat="server" Text="Thoracic Spine" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_MANAGEMENT_PLAN_LUMBAR_SPINE" runat="server" Text="Lumbar Spine" CssClass="lbl" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lbl_OTHER_MANAGEMENT_PLAN" runat="server" Text="Other:"></asp:Label>
                            </td>
                            <td valign="bottom">
                                <asp:TextBox ID="TXT_OTHER_MANAGEMENT_PLAN" TextMode="multiline" Width="600px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td valign="top">
                                <asp:CheckBox ID="CHK_MANAGEMENT_PLAN_MAGNETIC_RESONANCE_IMAGING" runat="server" CssClass="lbl" Text=" " />
                            </td>
                            <td valign="bottom">
                                <p class="lbl">
                                    Magnectic Resonance Imaging to rule out suspected dise maero trauma. MRI would be
                                    able to determine the size, location, and severity of disc involvement and confirm
                                    if there is neuroforaminal encroachment causing nerve compression or spinal stenosis
                                    with herniated nucleus pulposis that may be compressing the spinal cord.
                                </p>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="70%" align="center">
                    <tbody>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_MANAGEMENT_PLAN_MAGNETIC_RESONANCE_IMAGING_CERVICAL_SPINE"
                                    runat="server" Text="Cervical Spine" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_MANAGEMENT_PLAN_MAGNETIC_RESONANCE_IMAGING_THORACIC_SPINE"
                                    runat="server" Text="Thoracic Spine" CssClass="lbl" />
                            </td>
                            <td class="lbl">
                                <asp:CheckBox ID="CHK_MANAGEMENT_PLAN_MAGNETIC_RESONANCE_IMAGING_LUMBAR_SPINE" runat="server"
                                    Text="Lumbar Spine" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="70%" align="center">
                    <tbody>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lbl_MANAGEMENT_PLAN_MAGNETIC_RESONANCE_IMAGING_OTHER" runat="server"
                                    Text="Other:" CssClass="lbl"></asp:Label>
                            </td>
                            <td valign="bottom">
                                <asp:TextBox ID="TXT_MANAGEMENT_PLAN_MAGNETIC_RESONANCE_IMAGING_OTHER" TextMode="multiline"
                                    Width="600px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="80%">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_DISABILITY_AND_PROGNOSIS" Font-Bold=True runat="server" Text="DISABILITY AND PROGNOSIS" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_PATIENT_REPORTED" runat="server" Text="The patient reported that he/she is currently" CssClass="lbl" Width="240px"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_EMPLOYED" runat="server" Text="employed" CssClass="lbl" />,
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_STUDENT" runat="server" Text="a student" CssClass="lbl" Width="85px" />,
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_FULL_TIME" runat="server" Text="full time" CssClass="lbl" />,
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_PART_TIME" runat="server" Text="part time" CssClass="lbl" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_" runat="server" Text="employee and occupation is" CssClass="lbl"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_PART_TIME" Width="600px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_PATIENT_NOT_BEEN_BACK_TO_WORK_SINCE_THE_ACCIDENT" runat="server" CssClass="lbl" Text=" " />
                            </td>
                            <td class="lbl">
                                The patient states he/she has not been back to work since the accident due to the
                                injury.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_PATIENT_DECLARED_THAT_NOT_WORKING_AT_THE_TIME_OF_THE_ACCIDENT"
                                    runat="server" CssClass="lbl" Text=" " />
                            </td>
                            <td class="lbl">
                                The patient also declared that he/she not working at the time of the accident
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 6px">
                                <asp:CheckBox ID="CHK_PATIENT_WORK_ON_DUTY" runat="server" CssClass="lbl" Text=" " />
                            </td>
                            <td style="height: 6px" class="lbl">
                                The patient states that this accident is related to the date and time when he/she
                                was on duty and was working as
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="lbl">
                                (Occupation)&nbsp;&nbsp;<asp:TextBox ID="TXT_PATIENT_WORK_OCCUPATION_ON_DUTY" Width="600px"
                                    runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="lbl">
                                (Employer)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TXT_PATIENT_EMPLOYER_ON_DUTY"
                                    Width="600px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="70%">
                    <tbody>
                        <tr>
                            <td colspan="4" class="lbl">
                                The patient may perform his/her duties with the following restrictions:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_LIMITED_LIFTING" runat="server" Text="Limited lifting" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_LIMITED_BENDING" runat="server" Text="Limited bending" CssClass="lbl" Width="112px" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_TWISTING" runat="server" Text="Twisting" CssClass="lbl" Width="79px" />
                            </td>
                            <td class="lbl">
                                <asp:CheckBox ID="CHK_PULLING" runat="server" Text="Pulling" Width="75px" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_CARRYING" runat="server" Text="Carrying" CssClass="lbl" Width="75px" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_SITTING" runat="server" Text="Sitting" CssClass="lbl" Width="74px" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_STANDING_TIME" runat="server" Text="Standing for no more than" CssClass="lbl" />
                            </td>
                            <td class="lbl">
                                <asp:TextBox ID="TXT_STANDING_TIME" Width="60px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                                minutes at a time.
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <p class="lbl">
                                    I am prescribing a conservative chiropractic spinal manipulative treatment plan
                                    <asp:TextBox ID="TXT_CONSERVATIVE_CHIROPRACTIC_MANIPULATIVE_TREATMENT_PLAN_TIME_PER_WEEK"
                                        Width="60px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                                    times per week for
                                    <asp:TextBox ID="TXT_WEEKS_UNTIL_RE_EVALUATION" Width="60px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                                    weeks until re-evaluation. Treatment plan will be modified as clinically necessary.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_PROGONIOUS_DEFERRED_FURTHER_OBSERVATION_AND_MANAGEMENT" runat="server"
                                    Text="Progonious is deferred further observation and management" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_PATIENTS_PROGNOSIS_IS_GUARDED_AT_THIS_TIME" runat="server"
                                    Text="The patient's prognosis is guarded at this time" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="lbl">
                                    Based on the physical examination and the presented history, it is my professional
                                    opinion that the patient's condition is causally related to the incident described
                                    in this initial examination.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                Thank you,
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                Cordially,
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Buttons" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="lbl">
                                <asp:TextBox ID="TXT_EVENT_ID" runat="server" Width="31px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_SEX" runat="server" Width="29px" Visible="False"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
