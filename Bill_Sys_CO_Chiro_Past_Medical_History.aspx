<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CO_Chiro_Past_Medical_History.aspx.cs" Inherits="Bill_Sys_CO_Chiro_Past_Medical_History"
    Title="Untitled Page" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart">
             <table width="100%">
                    <tbody>
                        <tr>
                            <td align="left" style="height: 22px" class="lbl">
                                <asp:Label ID="lbl_RE" runat="server" Text="RE" CssClass="lbl"></asp:Label></td>
                            <td align="left" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px" class="lbl">
                                <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="300px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                            <td align="right" style="height: 22px" class="lbl">
                                <asp:Label ID="lbl_DOE" runat="server" Text="DOE" CssClass="lbl"></asp:Label></td>
                            <td align="right" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px">
                                <asp:TextBox ID="TXT_DOE" runat="server" Width="90px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 22px" class="lbl">
                                <asp:Label ID="lbl_AGE" runat="server" Text="Age" CssClass="lbl"></asp:Label></td>
                            <td align="left" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px" class="lbl">
                                <asp:TextBox ID="TXT_AGE" runat="server" Width="90px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                            <td align="right" style="height: 22px" class="lbl">
                                <asp:Label ID="lbl_DOA" runat="server" Text="DOA" CssClass="lbl"></asp:Label></td>
                            <td align="right" style="height: 22px">
                                &nbsp;</td>
                            <td align="left" style="height: 22px">
                                <asp:TextBox ID="TXT_DOA" runat="server" Width="90px" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" class="lbl">
                                <asp:Label ID="lbl_SEX" runat="server" Text="Sex" CssClass="lbl"></asp:Label></td>
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
                            <td colspan="3">
                                <strong>
                                    <asp:Label ID="lbl_PASTMEDICALHISTORY" runat="server" Text="PAST MEDICAL HISTORY" CssClass="lbl" Font-Bold="True"></asp:Label></strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="lbl_TRAUMA" runat="server" Text="Trauma" CssClass="lbl"></asp:Label>
                            </td>
                            <td style="width: 591px">
                                <asp:TextBox ID="TXT_TRAUMA" runat="server" Width="600px" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_TRAUMA_DEMES" runat="server" Text="Demes" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="lbl_SURGERIES" runat="server" Text="Surgeries" CssClass="lbl"></asp:Label>
                            </td>
                            <td style="width: 591px">
                                <asp:TextBox ID="TXT_SURGERIES" runat="server" Width="600px" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_SURGERIES_DEMES" runat="server" Text="Demes" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="lbl_MEDICATION" runat="server" Text="Medication" CssClass="lbl"></asp:Label>
                            </td>
                            <td style="width: 591px">
                                <asp:TextBox ID="TXT_MEDICATION" runat="server" Width="600px" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_MEDICATION_DEMES" runat="server" Text="Demes" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="lbl_ALLERGIES" runat="server" Text="Allergies" CssClass="lbl"></asp:Label>
                            </td>
                            <td style="width: 591px">
                                <asp:TextBox ID="TXT_ALLERGIES" runat="server" Width="600px" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_ALLERGIES_DEMES" runat="server" Text="Demes" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="lb_SYSTEMIC_DISEASES_EVENTS" runat="server" Text="Systemic Diseases/Events" CssClass="lbl" Width="149px"></asp:Label>
                            </td>
                            <td style="width: 591px">
                                <asp:TextBox ID="TXT_SYSTEMIC_DISEASES_EVENTS" runat="server" Width="600px" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_SYSTEMIC_DISEASES_EVENTS_DEMES" runat="server" Text="Demes" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:CheckBox ID="CHK_PATIENT_DENIES_ABUSE_OF_ALCOHOL_DRUGS" runat="server" Text="Patient denies abuse of alcohol and drugs" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p class="lbl">
                                    <asp:CheckBox ID="CHK_PATIENT_SMOKING" runat="server" Text="Patient is" CssClass="lbl" />
                                    &nbsp;
                                    <asp:RadioButton ID="RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKINGCURRENTLY" runat="server"
                                        Text="currently smoking for" GroupName="RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKING" CssClass="lbl" />
                                    <asp:TextBox ID="TXT_SMOKING_YEARS" runat="server" Width="60px" CssClass="textboxCSS"></asp:TextBox>
                                    years
                                    <asp:TextBox ID="TXT_CIGARETTES_PER_DAY" runat="server" Width="60px" CssClass="textboxCSS"></asp:TextBox>
                                    cigarettes a day/
                                    <asp:RadioButton ID="RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKINGQUIT" runat="server" Text="quit smoking"
                                        GroupName="RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKING" />
                                    <asp:TextBox ID="TXT_QUIT_SMOKING_MONTHS" runat="server" Width="60px" CssClass="textboxCSS"></asp:TextBox>
                                    months</p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p class="lbl">
                                    <asp:CheckBox ID="CHK_PATIENT_EXERCISE_DETAILS" runat="server" Text="Patient is" CssClass="lbl" />
                                    &nbsp;&nbsp;<asp:RadioButtonList ID="RDO_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE"
                                        runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="298px" CssClass="lbl">
                                        <asp:ListItem Value="0">Currently/</asp:ListItem>
                                        <asp:ListItem Value="1">was/</asp:ListItem>
                                        <asp:ListItem Value="2">was not performing exercise</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:TextBox ID="TXT_EXERCISE_DAYS_A_WEEK" runat="server" Width="60px" CssClass="textboxCSS"></asp:TextBox>
                                    days a week for
                                    <asp:TextBox ID="TXT_EXERCISE_MINUTES" runat="server" Width="60px" CssClass="textboxCSS"></asp:TextBox>
                                    min at&nbsp;
                                    <asp:RadioButtonList ID="RDO_HOME_OR_GYM" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Width="104px" CssClass="lbl">
                                        <asp:ListItem Value="0">home/</asp:ListItem>
                                        <asp:ListItem Value="1">gym</asp:ListItem>
                                    </asp:RadioButtonList></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p>
                                    <asp:CheckBox ID="CHK_PATIENT_L_R_HAND_DOMINENT" runat="server" Text="Patient is" CssClass="lbl" />&nbsp;&nbsp;<asp:RadioButtonList ID="RDO_HAND_DOMINATION" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Width="199px">
                                        <asp:ListItem Value="0">left/</asp:ListItem>
                                        <asp:ListItem Value="1">Right Hand Dominant</asp:ListItem>
                                    </asp:RadioButtonList></p>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_PAST_MEDICAL_HEADACHE" runat="server" Text="Headache" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_FRONT" runat="server" Text="Front" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_PARIETAL" runat="server" Text="Parietal" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_TEMPORAL" runat="server" Text="Temporal" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_OCCIPITAL" runat="server" Text="Occipital" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_DIFFUSE" runat="server" Text="Diffuse" CssClass="lbl" />
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="CHK_CONSTANT_INTERMITTENT" runat="server" CssClass="lbl" Text=" " />
                                - &nbsp;
                                <asp:RadioButtonList ID="RDO_CONSTANT_INTERMITTENT" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="176px">
                                    <asp:ListItem Value="0">Constant/</asp:ListItem>
                                    <asp:ListItem Value="1">Intermittent</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td colspan="4">
                                <strong>
                                    <asp:Label ID="lbl_QUALITYOFHEADACHE" runat="server" Text="Quality of Headache:" CssClass="lbl" Font-Bold="True"></asp:Label></strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 180px">
                                <asp:CheckBox ID="CHK_VERTIGO" runat="server" Text="Vertigo" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_VISUAL_DISTURBANCES" runat="server" Text="Visual disturbances" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_TMJ_PAIN" runat="server" Text="TMJ Pain" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_QUALITY_HEADACHE_TINNITUS" runat="server" Text="Tinnitus" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 180px; height: 20px;">
                                <asp:CheckBox ID="CHK_PHOTOPHOBIA" runat="server" Text="Photophobia" CssClass="lbl" />
                            </td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_POST_TRAUMATIC" runat="server" Text="Post-Traumatic" CssClass="lbl" />
                            </td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_TENSION" runat="server" Text="Tension" CssClass="lbl" />
                            </td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_MIGRAINE" runat="server" Text="Migraine" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 20px">
                                <asp:CheckBox ID="CHK_CLUSTER" runat="server" Text="Cluster" CssClass="lbl" /></td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 20%">
                                <asp:CheckBox ID="CHK_CERVICAL_PAIN_RATED" runat="server" Text="Cervical pain rated" CssClass="lbl" Width="94%" />
                            </td>
                            <td style="width: 10%">
                                <asp:TextBox ID="TXT_CERVICAL_PAIN_RATE" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                &nbsp;<asp:RadioButtonList ID="RDO_CERVICAL_WITH_WITHOUT_RADIATION" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                    <asp:ListItem Value="0">with/</asp:ListItem>
                                    <asp:ListItem Value="1">without radiation to</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 20%">
                                <asp:RadioButtonList ID="RDO_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                    <asp:ListItem Value="0">Bilateral/</asp:ListItem>
                                    <asp:ListItem Value="1">Lft/</asp:ListItem>
                                    <asp:ListItem Value="2">Rt</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 25%">
                                <asp:TextBox ID="TXT_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT" Width="90%" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:CheckBox ID="CHK_THORACIC_PAIN_RATED" runat="server" Text="Thoracic pain rated" CssClass="lbl" Width="93%" />
                            </td>
                            <td style="width: 10%">
                                <asp:TextBox ID="TXT_THORACIC_PAIN_RATED" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                &nbsp;<asp:RadioButtonList ID="RDO_THORACIC_WITH_WITHOUT_RADIATION" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                    <asp:ListItem Value="0">with/</asp:ListItem>
                                    <asp:ListItem Value="1">without radiation to</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 20%">
                                &nbsp;<asp:RadioButtonList ID="RDO_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                    <asp:ListItem Value="0">Bilateral/</asp:ListItem>
                                    <asp:ListItem Value="1">Lft/</asp:ListItem>
                                    <asp:ListItem Value="2">Rt</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 30%">
                                <asp:TextBox ID="TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT" Width="90%" runat="server" CssClass="textboxCSS" Height="20px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:CheckBox ID="CHK_LUMBAR_PAIN_RATED" runat="server" Text="Lumbar pain rated" CssClass="lbl" Width="94%" />
                            </td>
                            <td style="height: 10%">
                                <asp:TextBox ID="TXT_LUMBAR_PAIN_RATED" runat="server" Width="90%" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                <asp:RadioButtonList ID="RDO_LUMBAR_WITH_WITHOUT_RADIATION" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="100%">
                                    <asp:ListItem Value="0">with/</asp:ListItem>
                                    <asp:ListItem Value="1">without radiation to</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 20%">
                                &nbsp;<asp:RadioButtonList ID="RDO_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                    <asp:ListItem Value="0">Bilateral/</asp:ListItem>
                                    <asp:ListItem Value="1">Lft/</asp:ListItem>
                                    <asp:ListItem Value="2">Rt</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 30%">
                                <asp:TextBox ID="TXT_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT" Width="90%" runat="server" CssClass="textboxCSS" Height="18px"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_SHOULDER_PAIN" runat="server" CssClass="lbl" Text=" " />
                                -&nbsp;&nbsp;<asp:RadioButtonList ID="RDO_SHOULDER_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="164px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Shoulder Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_ARM_PAIN" runat="server" Text=" " />
                                -&nbsp;
                                <asp:RadioButtonList ID="RDO_RIGHT_LEFT_ARM_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Arm Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 213px; height: 20px;">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_ELBOW_PAIN" runat="server" Text=" " />
                                -&nbsp;
                                <asp:RadioButtonList ID="RDO_RIGHT_LEFT_ELBOW_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="142px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Elbow Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_FOREARM_PAIN" runat="server" Text=" " />
                                -<asp:RadioButtonList ID="RDO_RIGHT_LEFT_FOREARM_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="141px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Forarm Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td style="height: 15px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_HAND_PAIN" runat="server" Text=" " />
                                - &nbsp; &nbsp;<asp:RadioButtonList ID="RDO_RIGHT_LEFT_HAND_PAIN" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Hand Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td colspan="2" style="height: 15px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_DIGIT_PAIN" runat="server" CssClass="lbl" Text=" " />
                                - &nbsp; &nbsp;<asp:RadioButtonList ID="RDO_RIGHT_LEFT_DIGIT_PAIN" runat="server"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow" Width="141px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Digit Pain</asp:ListItem>
                                </asp:RadioButtonList>
                                &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;<asp:RadioButtonList ID="RDO_RIGHT_LEFT_ACCURATE_DIGIT_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 11px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_CHEST_PAIN" runat="server" Text="Chest Pain" CssClass="lbl" />
                                - &nbsp; &nbsp;
                                <asp:RadioButtonList ID="RDO_WITH_WITHOUT_BREATHING" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="237px" CssClass="lbl">
                                    <asp:ListItem Value="0">with/</asp:ListItem>
                                    <asp:ListItem Value="1">without breathing difiiculty</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td style="height: 23px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_HIP_JOINT_PAIN" runat="server" CssClass="lbl" Text=" " />
                                -&nbsp;<asp:RadioButtonList ID="RDO_RIGHT_LEFT_HIP_JOINT_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="153px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Hip joint Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="height: 23px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_GLUTEAL_PAIN" runat="server" CssClass="lbl" Text=" " />
                                -&nbsp;
                                <asp:RadioButtonList ID="RDO_RIGHT_LEFT_GLUTEAL_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="140px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Gluteal Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 213px; height: 23px;">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_THIGH_PAIN" runat="server" CssClass="lbl" Text=" " />
                                -&nbsp;<asp:RadioButtonList ID="RDO_RIGHT_LEFT_THIGH_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="142px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Thigh Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="height: 23px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_KNEE_PAIN" runat="server" Text=" " />
                                -<asp:RadioButtonList ID="RDO_RIGHT_LEFT_KNEE_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="133px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Knee Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_LEG_PAIN" runat="server" CssClass="lbl" Text=" " />
                                - 
                                <asp:RadioButtonList ID="RDO_RIGHT_LEFT_LEG_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="127px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Leg Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td>
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_ANKLE_PAIN" runat="server" Text=" " />
                                -&nbsp;
                                <asp:RadioButtonList ID="RDO_RIGHT_LEFT_ANKLE_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="140px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Ankle Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td colspan="2">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_FOOT_PAIN" runat="server" CssClass="lbl" Text=" " />
                                -&nbsp;
                                <asp:RadioButtonList ID="RDO_RIGHT_LEFT_FOOT_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="133px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Foot Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 27px">
                                <asp:CheckBox ID="CHK_RIGHT_LEFT_TOE_PAIN" runat="server" CssClass="lbl" Text=" " />
                                - 
                                <asp:RadioButtonList ID="RDO_RIGHT_LEFT_TOE_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="104px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Toes</asp:ListItem>
                                </asp:RadioButtonList>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="RDO_RIGHT_LEFT_ACCURATE_TOE_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="144px">
                                    <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td colspan="2" style="width: 388px; height: 12px">
                                <asp:CheckBox ID="CHK_NUMBNESS_TINGLING_PARENTHESIA" runat="server" CssClass="lbl" Text=" " />
                                -
                                <asp:RadioButtonList ID="RDO_NUMBNESS_TINGLING_PARENTHESIA" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="87%">
                                    <asp:ListItem Value="0">Numbness/</asp:ListItem>
                                    <asp:ListItem Value="1">Tingling/</asp:ListItem>
                                    <asp:ListItem Value="2">Paresthesia</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td colspan="2" style="height: 12px">
                                <asp:TextBox ID="TXT_NUMBNESS_TINGLING_PARENTHESIA" Width="400px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 388px">
                                <asp:CheckBox ID="CHK_CONTUSION" runat="server" Text="Contusion of:" CssClass="lbl" />
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="TXT_CONTUSION" Width="400px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 388px">
                                <asp:CheckBox ID="CHK_OTHER" runat="server" Text="Other:" CssClass="lbl" />
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="TXT_OTHER" Width="400px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <strong>
                                    <asp:Label ID="lbl_PHYSICALEXAMINATION" runat="server" Text="PHYSICAL EXAMINATION" CssClass="lbl" Font-Bold="True"></asp:Label></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_GENERALAPPREARANCE" runat="server" Text="General Apprearance:" CssClass="lbl" Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <asp:Label ID="lbl_GENERAL_APPEARANCE_WEIGHT" runat="server" Text="Weight:" CssClass="lbl"></asp:Label>
                                    <asp:TextBox ID="TXT_GENERAL_APPEARANCE_WEIGHT" Width="60px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                                    <asp:Label ID="lbl_LB" runat="server" Text="lb" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lbl_GENERAL_APPEARANCE_HEIGHT" runat="server" Text="Height:" CssClass="lbl"></asp:Label>
                                    <asp:TextBox ID="TXT_GENERAL_APPEARANCE_HEIGHT" Width="60px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                                    <asp:Label ID="lbl_BP" runat="server" Text="Bp: R" CssClass="lbl"></asp:Label>
                                    <asp:TextBox ID="TXT_GENERAL_APPEARANCE_R" Width="60px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                                    <asp:Label ID="lbl_MMHGR" runat="server" Text="mmHg" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lbl_GENERAL_APPEARANCE_L" runat="server" Text="L"></asp:Label>
                                    <asp:TextBox ID="TXT_GENERAL_APPEARANCE_L" Width="60px" runat="server" CssClass="textboxCSS"></asp:TextBox>
                                    <asp:Label ID="lbl_MMHGL" runat="server" Text="mmHg" CssClass="lbl"></asp:Label></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <asp:CheckBox ID="CHK_WELL_NOURISHED_AND_MAINTAINED" runat="server" Text="Well Nourished and Maintained" CssClass="lbl" />
                                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:RadioButtonList ID="RDO_WELL_NOURISHED_AND_MAINTAINED"
                                        runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="143px">
                                        <asp:ListItem Value="0">Good</asp:ListItem>
                                        <asp:ListItem Value="1">Fair</asp:ListItem>
                                        <asp:ListItem Value="2">Poor</asp:ListItem>
                                    </asp:RadioButtonList></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_PATIENT_APPEARS_TO_BE_ALERT_AND_ORIENTED" runat="server" Text="Patient appears to be alert and oriented." CssClass="lbl" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_GAIT" runat="server" Text="Gait:" CssClass="lbl" Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_NO_GAIT_DEVIATION" runat="server" Text="No gait deviation" CssClass="lbl" />
                            </td>
                            <td>
                                <asp:CheckBox ID="CHK_VISUAL_LIMP_IN_RIGHT_LEFT" runat="server" Text="Visual Limp in" CssClass="lbl" Width="100px" />
                                &nbsp;&nbsp;
                                <asp:RadioButtonList ID="RDO_VISUAL_LIMP_IN_RIGHT_LEFT" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="105px">
                                    <asp:ListItem Value="0">Right/</asp:ListItem>
                                    <asp:ListItem Value="1">Left</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td colspan="2">
                                <asp:CheckBox ID="CHK_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC" runat="server" Text="Gait abnormality presents" CssClass="lbl" />&nbsp;<asp:RadioButtonList
                                    ID="RDO_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="138px">
                                    <asp:ListItem Value="0">antalgic/</asp:ListItem>
                                    <asp:ListItem Value="1">ataxic</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_USE_CANE" runat="server" Text="Use Cane" CssClass="lbl" />
                            </td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_NEEDS_CANE" runat="server" Text="Needs Cane" CssClass="lbl" Width="94px" />
                            </td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_NEEDS_CRUTCH" runat="server" Text="Needs Crutch" CssClass="lbl" />
                            </td>
                            <td style="height: 20px">
                                <asp:CheckBox ID="CHK_UNABLE_TO_WALK_ON_TOES_HEALS" runat="server" Text="Unable to walk on" CssClass="lbl" />
                                &nbsp;
                                <asp:RadioButtonList ID="RDO_UNABLE_TO_WALK_ON_TOES_HEALS" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="0">toes/</asp:ListItem>
                                    <asp:ListItem Value="1">heals</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_AMBULATION" runat="server" Text="Ambulation:" CssClass="lbl" Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 11px">
                                <asp:CheckBox ID="CHK_AMBULATION_NORMAL" runat="server" Text="Normal" CssClass="lbl" />
                            </td>
                            <td style="height: 11px">
                                <asp:CheckBox ID="CHK_AMBULATION_PAIN" runat="server" Text="Pain" CssClass="lbl" />
                            </td>
                            <td style="height: 11px">
                                <asp:CheckBox ID="CHK_AMBULATION_GUARDED" runat="server" Text="Guarded" CssClass="lbl" />
                            </td>
                            <td style="height: 11px">
                                <asp:CheckBox ID="CHK_AMBULATION_IMPAIRED" runat="server" Text="Impaired" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_AMBULATION_NEEDS_ASSISTANCE" runat="server" Text="Needs Assistance" CssClass="lbl" />
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="CHK_AMBULATION_WHEELCHAIR" runat="server" Text="Wheelchair" CssClass="lbl" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="lbl" style="height: 22px">
                                <asp:TextBox ID="TXT_EVENT_ID" runat="server" Width="27px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE" runat="server" Width="27px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_HOME_OR_GYM" runat="server" Width="28px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_HAND_DOMINATION" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_CERVICAL_WITH_WITHOUT_RADIATION" runat="server" Width="26px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT1" runat="server" Width="21px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_THORACIC_WITH_WITHOUT_RADIATION" runat="server" Width="25px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1" runat="server" Width="21px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_LUMBAR_WITH_WITHOUT_RADIATION" runat="server" Width="28px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT1" runat="server" Width="23px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_SHOULDER_PAIN" runat="server" Width="26px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_ARM_PAIN" runat="server" Width="23px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_ELBOW_PAIN" runat="server" Width="27px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_FOREARM_PAIN" runat="server" Width="17px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_HAND_PAIN" runat="server" Width="23px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_DIGIT_PAIN" runat="server" Width="29px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN" runat="server" Width="23px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_HIP_JOINT_PAIN" runat="server" Width="23px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_GLUTEAL_PAIN" runat="server" Width="24px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_THIGH_PAIN" runat="server" Width="26px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_KNEE_PAIN" runat="server" Width="20px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_LEG_PAIN" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_ANKLE_PAIN" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_FOOT_PAIN" runat="server" Width="22px" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" class="lbl">
                                <asp:TextBox ID="TXT_RIGHT_LEFT_TOE_PAIN" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN" runat="server" Width="25px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_NUMBNESS_TINGLING_PARENTHESIA1" runat="server" Width="24px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_WELL_NOURISHED_AND_MAINTAINED" runat="server" Width="24px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_VISUAL_LIMP_IN_RIGHT_LEFT" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_UNABLE_TO_WALK_ON_TOES_HEALS" runat="server" Width="21px" Visible="False"></asp:TextBox>
                              
                                <asp:TextBox ID="TXT_CONSTANT_INTERMITTENT" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_WITH_WITHOUT_BREATHING" runat="server" Width="28px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_CURRENTLY_SMOKING_OR_QUIT_SMOKING" runat="server" Width="20px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_SEX" runat="server" Width="26px" Visible="False"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
