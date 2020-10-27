<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CO_Chiro.aspx.cs" Inherits="Bill_Sys_CO_Chiro" Title="Untitled Page" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <table class="ContentTable" width="100%">
    
        <tr>
            <td class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="center" class="lbl">
                            <asp:Label ID="lblHeading" runat="server" Text="CHIRO" Style="font-weight: bold;" Font-Bold="True" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                </table>
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
                                    RepeatLayout="Flow" CssClass="lbl" Enabled="False" BorderColor="Transparent" BackColor="Transparent" ForeColor="Black">
                                    <asp:ListItem Value="0">M</asp:ListItem>
                                    <asp:ListItem Value="1">F</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td class="lbl">
                                <asp:CheckBox ID="CHK_MVA" runat="server" Text="MVA" CssClass="lbl" /></td>
                            <td class="lbl">
                                <asp:CheckBox ID="CHK_W_C" runat="server" Text="W/C" CssClass="lbl" /></td>
                            <td class="lbl">
                                <asp:CheckBox ID="CHK_LIEN" runat="server" Text="Lien" CssClass="lbl" /></td>
                            <td colspan="2" class="lbl">
                                <asp:CheckBox ID="CHK_PRIVATE" runat="server" Text="Private" CssClass="lbl" /></td>
                        </tr>
                        <tr>
                            <td class="lbl" colspan="5">
                                _____________________________________________________________________________________________________________________</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <p class="lbl">
                                    The above patient presented him/herself tody for an examination and treatement to
                                    this office due to persistent pain. This information was obtained from</p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <p class="lbl">
                                    the patient by his/her<asp:RadioButtonList ID="RDO_INFORMATION_MODE" runat="server"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="312px" CssClass="lbl">
                                        <asp:ListItem Value="0">own description/</asp:ListItem>
                                        <asp:ListItem Value="1">interpreter</asp:ListItem>
                                        <asp:ListItem Value="2">legal guardian</asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;
                                    <asp:TextBox ID="TXT_INFORMATION_MODE" runat="server" Width="300px" CssClass="textboxCSS"></asp:TextBox></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                ___________________________________________________________________________________________
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <p class="lbl">
                                    MVA in which this patient stated that he/she was the
                                    <asp:CheckBox ID="CHK_DRIVER" runat="server" Text="Driver" CssClass="lbl" />
                                    <asp:CheckBox ID="CHK_PEDESTRIAN" runat="server" Text="Pedestrian" CssClass="lbl" />
                                    <asp:CheckBox ID="CHK_BIKE_RIDER" runat="server" Text="Bike Rider" CssClass="lbl" />
                                    <asp:CheckBox ID="CHK_PASSENGER" runat="server" Text="Front Passenger" CssClass="lbl" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <p>
                                    <asp:CheckBox ID="CHK_LEFT_RIGHT_PASSENGER" runat="server" CssClass="lbl" Text=" " />
                                    &nbsp;&nbsp;<asp:RadioButtonList ID="RDO_LEFT_RIGHT_PASSENGER" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" CssClass="lbl">
                                        <asp:ListItem Value="0">Left/</asp:ListItem>
                                        <asp:ListItem Value="1">Right/</asp:ListItem>
                                        <asp:ListItem Value="2">Center Passenger</asp:ListItem>
                                    </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CHK_MOTOR_CYCLE"
                                        runat="server" Text="Motorcycle" CssClass="lbl" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 22px">
                                <p class="lbl">
                                    Patient states he/she /&nbsp;
                                    <asp:RadioButtonList ID="RDO_SEAT_BELT_WEARING" runat="server" CssClass="lbl" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Width="304px">
                                        <asp:ListItem Value="0">was/</asp:ListItem>
                                        <asp:ListItem Value="1">was not wearing the seat belt and there</asp:ListItem>
                                    </asp:RadioButtonList>&nbsp; /&nbsp;
                                    <asp:RadioButtonList ID="RDO_AIR_BAG_DEPLOYMENT" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" CssClass="lbl">
                                        <asp:ListItem Value="0">was/</asp:ListItem>
                                        <asp:ListItem Value="1">was not air bag deployment</asp:ListItem>
                                    </asp:RadioButtonList></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 20px">
                                <p class="lbl">
                                    Impact on the Vehicle:&nbsp; /&nbsp;<asp:RadioButtonList ID="RDO_FRONT_REAR_IMPACT"
                                        runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="111px" CssClass="lbl">
                                        <asp:ListItem Value="0">Front/</asp:ListItem>
                                        <asp:ListItem Value="1">Rear</asp:ListItem>
                                    </asp:RadioButtonList>
                                    ,&nbsp; /&nbsp;
                                    <asp:RadioButtonList ID="RDO_LEFT_RIGHT_SIDE_IMPACT" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" CssClass="lbl">
                                        <asp:ListItem Value="0">Left Side/</asp:ListItem>
                                        <asp:ListItem Value="1">Right Side</asp:ListItem>
                                    </asp:RadioButtonList></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <p class="lbl">
                                    Impact on the Body:
                                    <asp:CheckBox ID="CHK_ANTERIOR" runat="server" Text="Anterior" CssClass="lbl" />
                                    /
                                    <asp:CheckBox ID="CHK_POSTERIOR" runat="server" Text="Posterior" CssClass="lbl" />
                                    /
                                    <asp:CheckBox ID="CHK_MEDIAL" runat="server" Text="Medical" CssClass="lbl" />
                                    /
                                    <asp:CheckBox ID="CHK_LATERAL" runat="server" Text="Lateral" CssClass="lbl" />
                                    /
                                    <asp:CheckBox ID="CHK_LEFT" runat="server" Text="Left" CssClass="lbl" />
                                    /
                                    <asp:CheckBox ID="CHK_RIGHT" runat="server" Text="Right" CssClass="lbl" />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                ___________________________________________________________________________________________</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <p class="lbl">
                                    According to the patient, he/she was in good state of health before he/she was involved
                                    in the accident and did not experience any of current symptoms before the accident.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                ___________________________________________________________________________________________</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <p class="lbl">
                                    The patient reports he/she immediately experienced these symptoms after the accident:
                                </p>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 17px; width: 153px;">
                                <asp:CheckBox ID="CHK_LOSE_OF_CONSCIOUSNESS" runat="server" Text="Lose of Consciousness" CssClass="lbl" Width="155px" Height="18px" />
                            </td>
                            <td style="height: 17px; width: 159px;">
                                <asp:CheckBox ID="CHK_SHOCK" runat="server" Text="Shock" CssClass="lbl" />
                            </td>
                            <td style="height: 17px; width: 158px;">
                                <asp:CheckBox ID="CHK_HEADACHE" runat="server" Text="Headache" CssClass="lbl" />
                            </td>
                            <td style="height: 17px; width: 165px;">
                                <asp:CheckBox ID="CHK_DIZZINESS" runat="server" Text="Dizziness" CssClass="lbl" />
                            </td>
                            <td style="height: 17px">
                                <asp:CheckBox ID="CHK_BLURRED_VISION" runat="server" Text="Blurred Vision" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 153px; height: 22px;">
                                <asp:CheckBox ID="CHK_NAUSEA" runat="server" Text="Nausea" CssClass="lbl" Width="76px" />
                            </td>
                            <td style="width: 159px; height: 22px;">
                                <asp:CheckBox ID="CHK_VOMITTING" runat="server" Text="Vomiting" CssClass="lbl" />
                            </td>
                            <td style="width: 158px; height: 22px;">
                                <asp:CheckBox ID="CHK_TINNITUS" runat="server" Text="Tinnitus" CssClass="lbl" />
                            </td>
                            <td style="height: 22px; width: 165px;">
                                <asp:CheckBox ID="CHK_FACIAL_PAIN" runat="server" Text="Facial Pain" CssClass="lbl" />
                            </td>
                            <td style="height: 22px">
                                <asp:CheckBox ID="CHK_NECK_PAIN" runat="server" Text="Neck Pain" CssClass="lbl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 153px; height: 21px;">
                                <asp:CheckBox ID="CHK_UPPER_BACK_PAIN" runat="server" Text="Upper Back Pain" CssClass="lbl" />
                            </td>
                            <td style="width: 159px; height: 21px;">
                                <asp:CheckBox ID="CHK_MID_BACK_PAIN" runat="server" Text="Mid Back Pain" CssClass="lbl" />
                            </td>
                            <td style="width: 158px; height: 21px;" class="lbl">
                                <asp:CheckBox ID="CHK_LOW_BACK_PAIN" runat="server" Text="Low Back Pain" CssClass="lbl" />
                            </td>
                            <td style="width: 165px; height: 21px">
                                <asp:CheckBox ID="CHK_R_L_KNEE_PAIN" runat="server" CssClass="lbl" Text=" " />
                                <asp:RadioButtonList ID="RDO_R_L_KNEE_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="129px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Knee pain</asp:ListItem>
                                </asp:RadioButtonList>&nbsp;
                            </td>
                            <td style="height: 21px">
                                <asp:CheckBox ID="CHK_R_L_SHOULDER_PAIN" runat="server" CssClass="lbl" Text=" " />
                                <asp:RadioButtonList
                                    ID="RDO_R_L_SHOULDER_PAIN" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                    Width="147px" Height="16px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Shoulder Pain</asp:ListItem>
                                </asp:RadioButtonList>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 153px; height: 19px">
                                <asp:CheckBox ID="CHK_R_L_HIP_PAIN" runat="server" CssClass="lbl" Text=" " />
                                -<asp:RadioButtonList ID="RDO_R_L_HIP_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" CssClass="lbl">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Hip Pain</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 159px; height: 19px">
                                <asp:CheckBox ID="CHK_R_L_ARM_LEG" runat="server" CssClass="lbl" Text=" " />
                                -&nbsp;
                                <asp:RadioButtonList ID="RDO_R_L_ARM_LEG" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="118px" CssClass="lbl" Height="19px">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Arm Leg</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td style="width: 158px; height: 19px">
                                <asp:CheckBox ID="CHK_R_L_HAND_FOOT" runat="server" CssClass="lbl" Text=" " />
                                -
                                <asp:RadioButtonList ID="RDO_R_L_HAND_FOOT" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="122px" CssClass="lbl">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Hand Foot</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td colspan="2" style="height: 19px" class="lbl">
                                <asp:CheckBox ID="CHK_R_L_DIGITS" runat="server" CssClass="lbl" Text=" " /><asp:RadioButtonList ID="RDO_R_L_DIGITS" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" CssClass="lbl">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Digits</asp:ListItem>
                                </asp:RadioButtonList>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:RadioButtonList ID="RDO_R_L_ACCURATE_DIGITS_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" CssClass="lbl" >
                                    <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                </asp:RadioButtonList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" class="lbl">
                                <asp:CheckBox ID="CHK_R_L_TOES" runat="server" CssClass="lbl" Text=" " />-
                                 
                                <asp:RadioButtonList ID="RDO_R_L_TOES" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="108px" CssClass="lbl">
                                    <asp:ListItem Value="0">R/</asp:ListItem>
                                    <asp:ListItem Value="1">L Toes</asp:ListItem>
                                </asp:RadioButtonList>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:RadioButtonList ID="RDO_R_L_ACCURATE_TOES_PAIN" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Width="145px" CssClass="lbl">
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
                            <td align="left" valign="top" style="height: 31px">
                                <asp:Label ID="lbl_OTHER_PAIN" runat="server" Text="Pain Other:" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="left" valign="bottom" style="height: 31px">
                                <asp:TextBox ID="TXT_OTHER_PAIN" runat="server" Width="656px" TextMode="MultiLine" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 18px">
                                <p class="lbl">
                                    Ambulance &nbsp; &nbsp;<asp:RadioButtonList ID="RDO_AMBULANCE_CALLED" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Width="165px" CssClass="lbl">
                                        <asp:ListItem Value="0">was/</asp:ListItem>
                                        <asp:ListItem Value="1">was not called</asp:ListItem>
                                    </asp:RadioButtonList></p>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 14px">
                                <p class="lbl">
                                    <asp:CheckBox ID="CHK_PATIENT_BROUGHT_EMERGENCY_ROOM" runat="server" Text="The patient was brought to the Emergency Room of" CssClass="lbl" Width="309px" />&nbsp;
                                    <asp:TextBox ID="TXT_PATIENT_BROUGHT_EMERGENCY_ROOM" runat="server" Width="300px" CssClass="textboxCSS"></asp:TextBox>
                                    &nbsp;Hospital
                                </p>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lbl_PRESCRIPTION_DETAILS" runat="server" Text="In the Hospital the patient was prescribed" CssClass="lbl"></asp:Label>
                            </td>
                            <td valign="bottom">
                                <asp:TextBox ID="TXT_PRESCRIPTION_DETAILS" runat="server" Width="500px" TextMode="MultiLine" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 22px">
                                <p>
                                    <asp:CheckBox ID="CHK_MULTIPLE_X_RAY" runat="server" Text="Multiple X-rays" CssClass="lbl" />
                                    &nbsp;<asp:RadioButtonList ID="RDO_MULTIPLE_X_RAY_TAKEN" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Width="169px" CssClass="lbl">
                                        <asp:ListItem Value="0">were/</asp:ListItem>
                                        <asp:ListItem Value="1">were not taken</asp:ListItem>
                                    </asp:RadioButtonList><asp:RadioButtonList ID="RDO_POSITIVE_NEGATIVE_FRACTURE" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Width="207px" CssClass="lbl">
                                        <asp:ListItem Value="0">Positive/</asp:ListItem>
                                        <asp:ListItem Value="1">Negative For Fracture</asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;<asp:TextBox ID="TXT_X_RAY_DETAILS" runat="server" Width="284px" CssClass="textboxCSS"></asp:TextBox>
                                </p>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 198px; height: 13px;">
                                <asp:CheckBox ID="CHK_ADVANCED_DIAGNOSTIC_IMAGING" runat="server" Text="Advanced Diagnostic Imaging" CssClass="lbl" />
                            </td>
                            <td style="height: 13px" class="lbl">
                                &nbsp;&nbsp; 
                                <asp:TextBox ID="TXT_ADVANCED_DIAGNOSTIC_IMAGING" runat="server" Width="568px" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 18px">
                                <asp:CheckBox ID="CHK_AMBULANCE_DECLINED" runat="server" Text="Patient declined the ambulance and went home to recuperate" CssClass="lbl" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 200px; height: 30px">
                                <asp:CheckBox ID="CHK_HOSPITAL_NAME_AND_PRIVATE_TRANSPORT_ON" runat="server" Text="Patient went to" CssClass="lbl" Width="103px" />
                            </td>
                            <td style="height: 30px" colspan="3">
                                &nbsp;
                                <asp:TextBox ID="TXT_HOSPITAL_NAME" runat="server" Width="570px" CssClass="textboxCSS"></asp:TextBox>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 200px; height: 30px">
                                <asp:Label ID="lbl_PRIVATE_TRANSPORT_ON" runat="server" Text="Hospital by private transport on" CssClass="lbl" Width="99%"></asp:Label>
                            </td>
                            <td colspan="3" style="height: 30px">
                                &nbsp;
                                <asp:TextBox ID="TXT_PRIVATE_TRANSPORT_ON" runat="server" Width="571px" CssClass="textboxCSS"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <asp:CheckBox ID="CHK_PATIENT_WENT_TO_PRIVATE_DOCTOR" runat="server" Text="Patient went to his/her private doctor" CssClass="lbl" Height="20px" Width="223px" />
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_PATIENT_WENT_TO_PRIVATE_DOCTOR" runat="server" Width="541px" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td valign="top" style="height: 21px">
                                <asp:Label ID="lbl_PRIVATE_DOCTOR_PRESCRIPTION_DETAILS" runat="server" Text="and was prescribed" CssClass="lbl" Width="116px"></asp:Label>
                            </td>
                            <td valign="bottom" style="height: 21px; width: 679px;">
                                <asp:TextBox ID="TXT_PRIVATE_DOCTOR_PRESCRIPTION_DETAILS" runat="server" Width="669px"
                                    TextMode="MultiLine" CssClass="textboxCSS"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center">                                
                                <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td  class="lbl">
                                <asp:TextBox ID="TXT_EVENT_ID" runat="server" CssClass="textboxCSS" Width="3%" Visible="False"></asp:TextBox>
                                  <asp:TextBox ID="TXT_INFORMATION_MODE1" runat="server" Width="27px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_LEFT_RIGHT_PASSENGER" runat="server" Width="36px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_SEAT_BELT_WEARING" runat="server" Width="31px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_AIR_BAG_DEPLOYMENT" runat="server" Width="29px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_FRONT_REAR_IMPACT" runat="server" Width="31px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_LEFT_RIGHT_SIDE_IMPACT" runat="server" Width="29px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_KNEE_PAIN" runat="server" Width="28px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_SHOULDER_PAIN" runat="server" Width="32px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_HIP_PAIN" runat="server" Width="28px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_ARM_LEG" runat="server" Width="25px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_HAND_FOOT" runat="server" Width="26px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_DIGITS" runat="server" Width="26px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_ACCURATE_DIGITS_PAIN" runat="server" Width="20px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_TOES" runat="server" Width="27px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_R_L_ACCURATE_TOES_PAIN" runat="server" Width="24px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_AMBULANCE_CALLED" runat="server" Width="32px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_MULTIPLE_X_RAY_TAKEN" runat="server" Width="22px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_POSITIVE_NEGATIVE_FRACTURE" runat="server" Width="20px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_SEX" runat="server" Width="29px" Visible="False"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
