<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_IM_FollowUpExam.aspx.cs" Inherits="_Default" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="TDPart">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <table width="100%">
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="lblPatiantName" runat="server" Text="Patient's Name" Width="110px"
                                            CssClass="ContentLabel"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtPatientName" runat="server" Width="441px" CssClass="textboxCSS"></asp:TextBox></td>
                                    <td style="width: 100px">
                                        <asp:Label ID="lblDateOfAccident" runat="server" Text="Date of Accident" CssClass="ContentLabel"
                                            Width="131px"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtDateOfAccident" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="lblDOS" runat="server" Text="DOS" CssClass="ContentLabel" Width="106px"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtDos" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                            </table>
                            &nbsp;<br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Label ID="lbl_CommunityMedical" runat="server" Font-Bold="False" Font-Size="X-Large"
                                Text="COMMUNITY    MEDICAL   CARE   ON   N.Y  ,  P.C " Width="481px" CssClass="ContentLabel"></asp:Label><br />
                            <br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Label ID="lbl_address" runat="server" Text="Label" CssClass="ContentLabel"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblTelephoneNo" runat="server" Text="Tel."
                                CssClass="ContentLabel"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtProviderPhone" runat="server"></asp:TextBox><br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp;
                            <asp:Label ID="lbl_Fax" runat="server" Text="Fax." CssClass="ContentLabel"></asp:Label>
                            &nbsp; &nbsp;<asp:TextBox ID="txtProviderFax" runat="server"></asp:TextBox><br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<table>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td colspan="3">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Label ID="lbl_FollowUp" runat="server" Font-Size="Large" Text="FOLLOW-UP  EXAMINATION"
                                            CssClass="ContentLabel" Width="304px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">
                                        <asp:Label ID="lblPatientsName" runat="server" Text="PATIENT'S NAME" Width="198px"
                                            CssClass="ContentLabel"></asp:Label></td>
                                    <td style="width: 130px">
                                        <asp:TextBox ID="txtPatinetName" runat="server" Width="441px" CssClass="textboxCSS"></asp:TextBox></td>
                                    <td style="width: 130px">
                                        <asp:Label ID="lblDoa" runat="server" Text="DATE OF ACCIDENT" CssClass="ContentLabel"
                                            Width="195px" Height="15px"></asp:Label></td>
                                    <td style="width: 130px">
                                        <asp:TextBox ID="txtDoa" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 140px; height: 28px">
                                        <asp:Label ID="lblDateOfExamination1" runat="server" Text="DATE OF EXAMINATION" CssClass="ContentLabel"
                                            Width="220px"></asp:Label></td>
                                    <td style="width: 100px; height: 28px">
                                        <asp:TextBox ID="txtDoe" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
                                    <td style="width: 100px; height: 28px">
                                        <asp:Label ID="lblDateOfBirth" runat="server" Text="DATE OF BIRTH" CssClass="ContentLabel"
                                            Width="165px"></asp:Label></td>
                                    <td style="width: 100px; height: 28px">
                                        <asp:TextBox ID="txtDob" runat="server" CssClass="textboxCSS"></asp:TextBox></td>
                                </tr>
                            </table>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Label ID="lblListAnyChanges" runat="server" Font-Bold="True" Text="LIST ANY CHANGES REVEALED BY YOUR MOST RECENT EXAMINATION IN THE FOLLOWING:"
                                CssClass="ContentLabel"></asp:Label><br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Label ID="lblAreaOfEnquiry" runat="server" Text="AREA OF INJURY, TYPE/NATURE OF INJURY, PATIENT’S SUBJECTIVE COMPLAINTS OR YOUR OBJECTIVE FINDINGS:"
                                CssClass="ContentLabel"></asp:Label><br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:TextBox ID="txtRevealedByRecentExamination" runat="server" Height="41px" Width="1154px"
                                CssClass="textboxCSS"></asp:TextBox><br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Label ID="lblListAdditionalBodyParts" runat="server" Text="LIST ADDITIONAL BODY PARTS AFFECTED BY THIS INJURY, IF ANY:"
                                CssClass="ContentLabel"></asp:Label><br />
                            <br />
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:TextBox ID="txtAdditionalBodyPartsAffected" runat="server" Height="36px" Width="1156px"
                                CssClass="textboxCSS"></asp:TextBox><br />
                            <br />
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Label ID="lblArrivalOfPatient" runat="server" Text="HOW THE PATIENT ARRIVED TO THE OFFICE TODAY:"
                                CssClass="ContentLabel"></asp:Label><br />
                            <br />
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:CheckBox ID="chkPatientArrivedBus" runat="server" Text="Bus" Width="52px" CssClass="ContentLabel" /></td>
                                    <td style="width: 100px">
                                        <asp:CheckBox ID="chkPatientArrivedSubway" runat="server" Text="Subway" Width="55px"
                                            CssClass="ContentLabel" /></td>
                                    <td style="width: 100px">
                                        <asp:CheckBox ID="chkPatientArrivedTaxi" runat="server" Text="Taxi" CssClass="ContentLabel" /></td>
                                    <td style="width: 100px">
                                        <asp:CheckBox ID="chkPatientArrivedWalking" runat="server" Text="Walking" Width="65px"
                                            CssClass="ContentLabel" /></td>
                                    <td style="width: 100px">
                                        <asp:CheckBox ID="chkPatientArrivedAmbulate" runat="server" Text="Ambulate" CssClass="ContentLabel" /></td>
                                    <td style="width: 100px">
                                        <asp:CheckBox ID="chkPatientArrivedPrivateCar" runat="server" Text="Private Car"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:CheckBox ID="chkPatientArrivedOther" runat="server" Text="Other" Width="61px"
                                            CssClass="ContentLabel" /></td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtPatientOtherTransport" runat="server" Width="352px" CssClass="textboxCSS"></asp:TextBox></td>
                                </tr>
                            </table>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
                            <table>
                                <tr>
                                    <td style="width: 440px">
                                        <asp:Label ID="lblDescribeTreatment" runat="server" Text="DESCRIBE TREATMENT RENDERED TODAY:"
                                            Width="363px" CssClass="ContentLabel"></asp:Label></td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 440px; height: 28px">
                                        <asp:CheckBox ID="chkTreatment99211" runat="server" Text="99211 – Community Medical Care of P.C. OR OTHER OUTPATIENT VISIT"
                                            Height="25px" Width="425px" CssClass="ContentLabel" /></td>
                                    <td style="width: 440px; height: 28px">
                                        <asp:CheckBox ID="chkTreatment99214" runat="server" Text="99214 – Community Medical Care of P.C. OR OTHER OUTPATIENT VISIT"
                                            Height="26px" Width="545px" CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 440px; height: 26px">
                                        <asp:CheckBox ID="chkTreatment99212" runat="server" Text="99212 – Community Medical Care of P.C. OR OTHER OUTPATIENT VISIT"
                                            Width="425px" CssClass="ContentLabel" /></td>
                                    <td style="width: 440px; height: 26px">
                                        <asp:CheckBox ID="chkTreatment99215" runat="server" Text="99215 – Community Medical Care of P.C. OR OTHER OUTPATIENT VISIT"
                                            Width="545px" CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 440px">
                                        <asp:CheckBox ID="chkTreatment99213" runat="server" Text="99213 – Community Medical Care of P.C. OR OTHER OUTPATIENT VISIT"
                                            Width="425px" CssClass="ContentLabel" /></td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table>
                                <tr>
                                    <td style="width: 341px; height: 9px">
                                        <asp:Label ID="lbl_CurrentComplaints" runat="server" Font-Size="Large" Font-Underline="True"
                                            Text="CURRENT COMPLAINTS:" CssClass="ContentLabel"></asp:Label></td>
                                    <td style="width: 485px; height: 9px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chkHeadachas" runat="server" Text="HEADACHES" CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chkKneePain" runat="server" Text="RT / LT KNEE PAIN" CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px; height: 21px">
                                        <asp:CheckBox ID="chkDizziness" runat="server" Text="DIZZINESS" CssClass="ContentLabel" /></td>
                                    <td style="width: 485px; height: 21px">
                                        <asp:CheckBox ID="chkShoulderPain" runat="server" Text="RT / LT SHOULDER PAIN" CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chkJawPain" runat="server" Text="JAW PAIN" CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chkLowerBackStiffness" runat="server" Text="LOWER BACK PAIN & STIFFNESS"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px; height: 20px;">
                                        <asp:CheckBox ID="chkNeckPainStiffness" runat="server" Text="NECK PAIN AND STIFFNESS"
                                            CssClass="ContentLabel" /></td>
                                    <td style="width: 485px; height: 20px;">
                                        <asp:CheckBox ID="chkUpperBackPainStiffness" runat="server" Text="UPPER BACK PAIN & STIFFNESS"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chkChestPain" runat="server" Text="CHEST PAIN" Width="93px" CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chkUnableToLiftObjects" runat="server" Text="UNABLE TO LIFT HEAVY OBJECTS"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px; height: 21px">
                                        <asp:CheckBox ID="chkDificultyInBreathing" runat="server" Text="DIFFICULTY IN BREATHING"
                                            CssClass="ContentLabel" /></td>
                                    <td style="width: 485px; height: 21px">
                                        <asp:CheckBox ID="chkShootingPainLeg" runat="server" Text="SHOOTING PAIN DOWN IN THE RT / LT LEG"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chkRestrictionNeckMotion" runat="server" Text="RESTRICTION OF NECK MOTION"
                                            CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chkDifficultToWalkAfterSitting" runat="server" Text="DIFFICULTY IN RISING TO WALK AFTER SITTING"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chk_NumbnessTinglingToes" runat="server" Text="NUMBNESS / TINGLING IN FINGERS / TOES"
                                            CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chkDifficultInProlongedRiding" runat="server" Text="DIFFICULTY IN PROLONGED RIDING IN AN AUTOMOBILE"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chkAnxietyStress" runat="server" Text="ANXIETY / STRESS" CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chkInsomnia" runat="server" Text="INSOMNIA" CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chkVisualProblems" runat="server" Text="VISUAL PROBLEMS" CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chk_MemoryProblems" runat="server" Text="MEMORY PROBLEMS" CssClass="ContentLabel" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 341px">
                                        <asp:CheckBox ID="chkFatigue" runat="server" Text="FATIGUE" CssClass="ContentLabel" /></td>
                                    <td style="width: 485px">
                                        <asp:CheckBox ID="chkConcentrationProblems" runat="server" Text="CONCENTRATION PROBLEMS"
                                            CssClass="ContentLabel" /></td>
                                </tr>
                            </table>
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;&nbsp;<br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
