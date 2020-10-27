<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CO_Referral.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="Bill_Sys_CO_Referral" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%">
 <tr>
 <td width="100%"  class="TDPart">
        <table class="ContentTable" width="100%">
            <tr>
                <td class="TDPart">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblHeading" runat="server" Text="PHYSICAL/ OCCUPATIONAL THERAPHY/ CHIROPRACTIC REFERRAL"
                                    Style="font-weight: bold;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td style="width: 808px">
                                    <table width="85%">
                                        <tr>
                                            <td align="left" valign="middle" class="lbl">
                                                <asp:Label ID="lbl_txt_patient_name" Width="130px" runat="server" Font-Bold="False"
                                                    Text="Patient’s Name" CssClass="lbl"></asp:Label>
                                            </td>
                                            <td align="left" valign="middle" class="lbl">
                                                <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="400px" CssClass="textboxCSS" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td align="right" valign="middle" class="lbl">
                                                <asp:Label ID="lbl_date" runat="server" Font-Bold="False" Text="Dates" CssClass="lbl"></asp:Label>
                                            </td>
                                            <td align="right" valign="middle" class="lbl">
                                                <asp:TextBox ID="TXT_DOB" runat="server" Width="85px" CssClass="textboxCSS" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="left" style="height: 18px" class="lbl">
                                                <asp:Label ID="lbl_diagnosis" runat="server" Text="Diagnosis:" CssClass="lbl"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="left">
                                                <asp:TextBox ID="TXT_DIAGNOSIS" runat="server" Width="769px" TextMode="multiLine"
                                                    Height="40px" CssClass="textboxCSS"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="65%">
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lbl_precautions" runat="server" Text="Precautions:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TXT_PRECAUTIONS" runat="server" Width="450px" CssClass="textboxCSS"></asp:TextBox>
                                            </td>
                                            <td align="left" colspan="2" style="width: 192px">
                                                <asp:Label ID="lbl_weight_bearing" runat="server" Text="Weight Bearing" Width="112px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="90%">
                                        <tr>
                                            <td style="height: 26px; width: 83px;" class="lbl">
                                                <asp:Label ID="lbl_frequency" runat="server" Text="Frequency" CssClass="lbl"></asp:Label>
                                            </td>
                                            <td style="height: 26px; width: 246px;" class="lbl" colspan="2">
                                            
                                                <asp:RadioButtonList ID="RDO_WEEK" runat="server" RepeatDirection="Horizontal" CssClass="lbl" RepeatLayout="Flow" Width="86px">
                                                    <asp:ListItem Value="0" Text="2"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="3"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="4"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="lblWeeks" runat="server" Text="  x A Week x " CssClass="lbl" Width="65px"></asp:Label>&nbsp;
                                                <asp:TextBox ID="TXT_WEEK_S" runat="server" Width="250PX" CssClass="textboxCSS"></asp:TextBox>
                                            </td>
                                            <td align="left" style="height: 26px; width: 84px;" class="lbl">
                                                <asp:Label ID="lbl_Week" runat="server" Text="Week/s" CssClass="lbl"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td style="height: 18px" class="lbl">
                                                <asp:CheckBox ID="CHK_NWB" runat="server" Text="NWB" />
                                                <asp:CheckBox ID="CHK_PWB" runat="server" Text="PWB" />
                                                <asp:CheckBox ID="CHK_FWB" runat="server" Text="FWB" />
                                                <asp:CheckBox ID="CHK_WBAT" runat="server" Text="WBAT" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="CHK_EVALUATE_TREAT" runat="server" Font-Bold="True" Text="Evaluate & Treat">
                                                </asp:CheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 70%">
                                        <tr>
                                            <td align="left" valign="middle">
                                                <asp:Label ID="lbl_goals" Font-Bold="True" runat="server" Text="Goals"></asp:Label>
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:CheckBox ID="CHK_PAIN" runat="server" Text=" " />
                                            </td>
                                            <td align="left" valign="middle">
                                                <img src="Images/down-arrow.gif" />
                                                <asp:Label ID="lbl_pain" runat="server" Text="Pain"></asp:Label>
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:CheckBox ID="CHK_ROM" runat="server" Text=" " />
                                            </td>
                                            <td>
                                                <img src="Images/up-arrow.gif" />
                                                <asp:Label ID="lbl_rom" runat="server" Text="Rom"></asp:Label>
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:CheckBox ID="CHK_STRENGTH" runat="server" Text=" " />
                                            </td>
                                            <td align="left" valign="middle" style="width: 40px">
                                                <img src="Images/up-arrow.gif" />
                                                <asp:Label ID="lbl_strength" runat="server" Text="Strength"></asp:Label>
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:CheckBox ID="CHK_IMPROVE_FUNCTION" runat="server" Text=" " />
                                            </td>
                                            <td align="left" valign="middle" style="width: 109px">
                                                <asp:Label ID="lbl_improve_function" runat="server" Text="Improve Function"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px">
                                            </td>
                                            <td align="left" valign="middle" style="height: 10px">
                                                <asp:CheckBox ID="CHK_OTHER_GOALS" runat="server" Text=" " />
                                            </td>
                                            <td align="left" valign="middle" style="height: 10px">
                                                <asp:Label ID="lbl_Others" runat="server" Text="Others"></asp:Label>
                                            </td>
                                            <td colspan="4" align="left" valign="middle" style="height: 10px">
                                                <asp:TextBox ID="TXT_OTHER_GOALS" runat="server" Width="200px" CssClass="textboxCSS"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="middle" style="height: 10px">
                                                <asp:CheckBox ID="CHK_SWELLING" runat="server" Text=" " />
                                            </td>
                                            <td align="left" valign="middle" style="width: 109px; height: 10px" class="lbl">
                                                <img src="Images/down-arrow.gif" />
                                                <asp:Label ID="lbl_Swelling" runat="server" Text="Swelling" CssClass="lbl" Width="55px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="70%">
                                        <tr>
                                            <td colspan="3" align="left">
                                                <asp:Label ID="lblModalities" runat="server" Font-Bold="True" Text="Modalities"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_US" runat="server" Text="Us" />
                                            </td>
                                            <td style="width: 173px" class="lbl">
                                                <asp:CheckBox ID="CHK_MOIST_HEAT" runat="server" Text="Moist Heat" />
                                            </td>
                                            <td align="left" valign="middle" class="lbl">
                                                <asp:CheckBox ID="CHK_TRACTION" runat="server" Text="Traction" />
                                                <asp:TextBox ID="TXT_LBS" runat="server" Width="120px" CssClass="textboxCSS"></asp:TextBox>
                                                <asp:Label ID="lbl_lbs" runat="server" Text="Lbs" CssClass="lbl"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_INTERFERENTIAL" runat="server" Text="Interferential" />
                                            </td>
                                            <td style="width: 173px" class="lbl">
                                                <asp:CheckBox ID="CHK_ICE" runat="server" Text="Ice" />
                                            </td>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_CONTINUOUS" runat="server" Text="Continuous" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_TENS" runat="server" Text="Tens" />
                                            </td>
                                            <td style="width: 173px" class="lbl">
                                                <asp:CheckBox ID="CHK_SPRAY_STRENGTH" runat="server" Text="Spray Strength" />
                                            </td>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_INTERMITTENT" runat="server" Text="Intermittent" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_PARAFIN_BATH" runat="server" Text="Parafin Bath" />
                                            </td>
                                            <td colspan="2" align="left" class="lbl">
                                                <asp:CheckBox ID="CHK_CONTRAST_BATH" runat="server" Text="Contrast Bath" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_ELECTRICAL_STIM" runat="server" Text="Electrical Stim" />
                                            </td>
                                            <td colspan="2" align="left" class="lbl">
                                                <asp:CheckBox ID="CHK_HEATER_TRACTION" runat="server" Text="Heater Traction" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="left">
                                                <asp:Label ID="lbl_Manual_Therapie" runat="server" Font-Bold="true" Text="Manual Therapies"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_GENTLE_MASSAGE" runat="server" Text="Gentle Massage" />
                                            </td>
                                            <td style="width: 173px" class="lbl">
                                                <asp:CheckBox ID="CHK_MANUAL_THERAPY_STRETCHING" runat="server" Text="Stretching" />
                                            </td>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_SMT" runat="server" Text="Smt (Spinal Minip Therapy)" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_MYOFACIAL_RELEASE" runat="server" Text="Myofacial Release" />
                                            </td>
                                            <td style="width: 173px" class="lbl">
                                                <asp:CheckBox ID="CHK_ISOMETIC_STABILIZATION" runat="server" Text="Isometic Stabilization" CssClass="lbl" Width="156px" />
                                            </td>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_CMT" runat="server" Text="Cmt (Chiropratic Minip Therapy)" CssClass="lbl" Width="209px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px" class="lbl">
                                                <asp:CheckBox ID="CHK_JOBST_TECHNIQUES" runat="server" Text="Jobst Techniques" />
                                            </td>
                                            <td style="width: 173px; height: 20px" class="lbl">
                                                <asp:CheckBox ID="CHK_CERVICAL" runat="server" Text="Cervical" />
                                            </td>
                                            <td style="height: 20px" class="lbl">
                                                <asp:CheckBox ID="CHK_ACTIVATOR" runat="server" Text="Activator(Low Force Tech)" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px" class="lbl">
                                                <asp:CheckBox ID="CHK_CRANIOSACRAL" runat="server" Text="Craniosacral" />
                                            </td>
                                            <td style="width: 173px; height: 20px" class="lbl">
                                                <asp:CheckBox ID="CHK_LUMBAR" runat="server" Text="Lumbar" />
                                            </td>
                                            <td style="height: 20px" class="lbl">
                                                <asp:CheckBox ID="CHK_PNF" runat="server" Text="Pnf" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="70%">
                                        <tr>
                                            <td colspan="3" align="left" class="lbl">
                                                <asp:Label ID="lbl_Manual_Therapies" runat="server" Font-Bold="true" Text="Manual Therapies"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_ROM_PROM" runat="server" Text="Rom Prom" />
                                            </td>
                                            <td colspan="2" align="left" class="lbl">
                                                <asp:CheckBox ID="CHK_STRETCHING_EXERCISE" runat="server" Text="Stretching (Functional)" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_AROM" runat="server" Text="Arom" />
                                            </td>
                                            <td colspan="2" align="left" class="lbl">
                                                <asp:CheckBox ID="CHK_BIOMECHANICS" runat="server" Text="Biomechanics Training" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 22px" class="lbl">
                                                <asp:CheckBox ID="CHK_STRENGTHENING_EXERCISE" runat="server" Text="Strengthening Exercise" />
                                            </td>
                                            <td colspan="2" align="left" style="height: 22px" class="lbl">
                                                <asp:CheckBox ID="CHK_HOME_EXERCISE" runat="server" Text="Home Exercise Program" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lbl">
                                                <asp:CheckBox ID="CHK_POSTURE_EXERCISE" runat="server" Text="Posture Exercise" />
                                            </td>
                                            <td colspan="2" align="left" class="lbl">
                                                <asp:CheckBox ID="CHK_THERPEUTIC_EXERCISE" runat="server" Text="Therapeutic Exercise" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="left" class="lbl">
                                                <asp:CheckBox ID="CHK_GAIT_TRAINING" runat="server" Text="Gait Training" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="70%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Specific_Instructions" Width="150px" runat="server" Text="Specific Instructions"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TXT_SPECIFIC_INSTRUCTIONS" runat="server" Width="550PX" CssClass="textboxCSS"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Physicians_Signature" runat="server" Text="Physician's Signature"></asp:Label>
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl__u_pin_number" runat="server" Text="U-Pin #"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TXT_U_PIN_NUMBER" runat="server" Width="250PX" CssClass="textboxCSS"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="center" style="width: 824px">
                                                <asp:TextBox ID="TXT_I_EVENT" runat="server" Text="1" CssClass="textboxCSS" Width="36px"></asp:TextBox>
                                                <asp:Button ID="css_btnSave" runat="server" Text="Save" OnClick="css_btnSave_Click"
                                                    CssClass="Buttons" Width="84px" />&nbsp;
                                                <asp:TextBox ID="TXT_WEEK" runat="Server" Visible="false" Width="10px"></asp:TextBox>
                                                <asp:TextBox ID="txtCaseID" runat="Server" Visible="false" Width="10px"></asp:TextBox>
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
    </td>
         </tr>
     </table>
      </asp:Content>
