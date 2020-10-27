<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_FUIM_Plan.aspx.cs" Inherits="Bill_Sys_FUIM_Plan" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                   <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                    <td>
                    
        <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" Text=" " Visible="False" Width="50%"></asp:Label></td>
                <td style="width: 13%">
                </td>
                <td style="width: 13%">
                </td>
            </tr>
                            <tr>
                                <td style="width: 15%">
        <asp:Label ID="lbl_Patientname" runat="server" Text="Patient's Name" Width="90%" CssClass="lbl"></asp:Label></td>
                                <td style="width: 50%">
        <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="80%" CssClass="textboxCSS" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                <td style="width: 13%">
        <asp:Label ID="lbl_DateOfAccident" runat="server" Text="Date of Accident" CssClass="lbl" Width="100%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:Label></td>
                                <td style="width: 13%">
        <asp:TextBox ID="TXT_DOA" runat="server" CssClass="textboxCSS" Width="90%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 15%; height: 26px;">
        <asp:Label ID="lbl_DOS" runat="server" Text="DOS" CssClass="lbl" Width="90%"></asp:Label></td>
                                <td style="width: 50%; height: 26px;">
                                    <asp:TextBox ID="TXT_DOS" runat="server" CssClass="textboxCSS" Width="35%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                                <td style="width: 13%; height: 26px;">
                                </td>
                                <td style="width: 13%; height: 26px;">
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%">
                            <tr>
                                <td style="width: 100px" class="lbl">
                         
        
        <asp:Label ID="lbl_Plain" runat="server" Font-Bold="False" Font-Size="Medium" Font-Underline="True"
            Text="PLAIN" CssClass="lbl" Width="25%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" class="lbl">
        
        <asp:Label ID="lbl_PlanText" runat="server" Text="BASED ON THIS EXAMINATION, THE PATIENT NEEDS DIAGNOSTIC TESTS OR REFERRALS:" CssClass="lbl" Width="90%"></asp:Label>
                                </td>
                            </tr>
                        </table>
        <br />
        <table style="width: 100%">
            <tr>
                <td style="width: 25%">
        <asp:Label ID="lbl_Test" runat="server" Text="TESTS:" CssClass="lbl" Width="50%"></asp:Label></td>
                <td style="width: 25%">
                </td>
                <td style="width: 25%" class="lbl">
        <asp:Label ID="lbl_REFERRALS" runat="server" Text="REFERRALS:" CssClass="lbl" Width="50%"></asp:Label></td>
                <td style="width: 25%">
                </td>
            </tr>
            <tr>
                <td style="width: 25%; height: 30px;" class="lbl">
                    <asp:CheckBox ID="CHK_TEST_CT_SCAN" runat="server"
            Text="CT SCAN" Width="50%" CssClass="lbl" /></td>
                <td style="width: 25%; height: 30px;" class="lbl">
        <asp:CheckBox ID="CHK_TEST_EMG_NCV" runat="server" Text="EMG/NCV" Width="50%" CssClass="lbl" /></td>
                <td style="width: 25%; height: 30px;" class="lbl">
                    <asp:CheckBox ID="CHK_REFERRALS_CHIROPRACTOR"
            runat="server" Text="CHIROPRACTOR" Width="62%" CssClass="lbl" /></td>
                <td style="width: 80%;" class="lbl">
                    <asp:CheckBox ID="CHK_REFERRALS_INTERNIST_FAMILY_PHYSICIAN"
            runat="server" Text="INTERNIST/FAMILY PHYSICIAN" Height="25px" Width="100%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 25%" abbr="lbl" class="lbl">
        <asp:CheckBox ID="CHK_TEST_MRI" runat="server" Text="MRI" Width="50%" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
        <asp:TextBox ID="TXT_TEST_MRI" runat="server" CssClass="textboxCSS" Width="70%"></asp:TextBox></td>
                <td style="width: 25%" class="lbl">
        <asp:CheckBox ID="CHK_REFERRALS_OCCUPATIONAL_THERAPIST" runat="server" Text="OCCUPATIONAL THERAPIST" Height="21px" Width="95%" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
                </td>
            </tr>
            <tr>
                <td style="width: 25%" abbr="lbl" class="lbl">
        <asp:CheckBox ID="CHK_TEST_LABS" runat="server" Text="LABS" Width="50%" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
        <asp:TextBox ID="TXT_TEST_LABS" runat="server" CssClass="textboxCSS" Width="70%"></asp:TextBox></td>
                <td style="width: 25%" class="lbl">
        <asp:CheckBox ID="CHK_REFERRALS_PHYSICAL_THERAPIST" runat="server" Text="PHYSICAL THERAPIST" Width="80%" Height="15px" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
                </td>
            </tr>
            <tr>
                <td style="width: 25%" abbr="lbl" class="lbl">
        <asp:CheckBox ID="CHK_TEST_X_RAY" runat="server" Text="X-RAY" Width="50%" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
        <asp:TextBox ID="TXT_TEST_X_RAY" runat="server" CssClass="textboxCSS" Width="70%"></asp:TextBox></td>
                <td style="width: 25%" class="lbl">
        <asp:CheckBox ID="CHK_REFERRALS_SPECIALIST_IN" runat="server" Text="SPECIALIST IN" Width="80%" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
        <asp:TextBox ID="TXT_REFERRALS_SPECIALIST" runat="server" CssClass="textboxCSS" Width="70%"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 25%" abbr="lbl" class="lbl">
        <asp:CheckBox ID="CHK_TEST_OTHER" runat="server" Text="OTHER" Width="50%" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
        <asp:TextBox ID="TXT_TEST_OTHER" runat="server" CssClass="textboxCSS" Width="70%"></asp:TextBox></td>
                <td style="width: 25%" class="lbl">
        <asp:CheckBox ID="CHK_REFERRALS_OTHER" runat="server" Text="OTHER" Width="80%" CssClass="lbl" /></td>
                <td style="width: 25%" abbr="lbl">
        <asp:TextBox ID="TXT_REFERRALS_OTHER" runat="server" CssClass="textboxCSS" Width="70%"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 25%; height: 21px">
                </td>
                <td style="width: 25%; height: 21px">
                </td>
                <td style="width: 25%; height: 21px">
                </td>
                <td style="width: 25%; height: 21px">
                </td>
            </tr>
        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 100%">
        
        <asp:Label ID="lbl_accesiveDeviceOrMedications" runat="server" Text="BASED ON MOST RECENT EXAMINATION, LIST CHANGES TO THE ORIGINAL TREATMENT PLAN, PRESCRIPTION MEDICATIONS" CssClass="lbl" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" class="lbl">
        
        <asp:Label ID="lbl_AssesiveDevice" runat="server" Text="OR ASSISTIVE DEVICES, IF ANY" CssClass="lbl" Width="50%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" class="lbl">
                        <asp:TextBox ID="TXT_LIST_CHANGES_OF_TREATMENT" runat="server" Height="38px" Width="90%" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                         
        <table width="100%">
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:Label ID="lbl_SUPPLIES" runat="server" Text="SUPPLIES:" Width="65%" CssClass="lbl"></asp:Label></td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 19px;" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_EMS_UNIT" runat="server" Text="E.M.S. UNIT" Width="65%" CssClass="lbl" /></td>
                <td style="width: 50%; height: 19px;" class="lbl">
        <asp:CheckBox
            ID="CHK_SUPPLIES_RT_LT_WRIST_SUPPORT" runat="server" Text="RT / LT WRIST SUPPORT" Width="65%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_CERVICAL_PILLOW" runat="server" Text="CERVICAL PILLOW" Width="65%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_RT_LT_ELBOW_SUPPORT" runat="server" Text="RT / LT ELBOW SUPPORT" Width="65%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_LUMBOSACRAL_BACK_SUPPORT" runat="server" Text="LUMBOSACRAL BACK SUPPORT (LSO)" Width="65%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_SUPPLIES_RT_LT_ANKLE_SUPPORT" runat="server" Text="RT / LT ANKLE SUPPORT" Width="65%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox ID="CHK_SUPPLIES_LUMBAR_CUSHION" runat="server" Text="LUMBAR CUSHION" Width="65%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_RT_LT_KNEE_SUPPORT"
            runat="server" Text="RT / LT KNEE SUPPORT" Width="65%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_MASSAGER" runat="server" Text="MASSAGER" Width="65%" CssClass="lbl" /></td>
                <td style="width: 50%" class="lbl">
                    <asp:CheckBox
            ID="CHK_SUPPLIES_CANE" runat="server" Text="CANE" Width="65%" CssClass="lbl" /></td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_ROM_TESTING" runat="server" Text="ROM TESTING" Width="65%" CssClass="lbl" /></td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%" class="lbl">
        <asp:CheckBox ID="CHK_SUPPLIES_BIOFEEDBACK_TRAINING_SESSIONS" runat="server" Text="BIOFEEDBACK TRAINING SESSIONS" Height="20px" Width="65%" CssClass="lbl" /></td>
                <td style="width: 50%">
                </td>
            </tr>
        </table>
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <table width="100%">
                            <tr>
                                <td colspan="8" style="width: 100%" class="lbl">
         
        <asp:Label ID="lbl_NextVisit" runat="server" Text="WHEN IS PATIENT’S NEXT FOLLOW-UP VISIT?" CssClass="lbl" Width="346px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%" class="lbl">
         
        <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_WITHIN_A_WEEK" runat="server" Text="WITHIN A WEEK" Width="90%" CssClass="lbl" />
                                </td>
                                <td style="width: 12%" class="lbl">
        <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_1_TO_2_WKS" runat="server" Text="1-2 WKS" Width="86%" CssClass="lbl" />
                                </td>
                                <td style="width: 12%" class="lbl">
        <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_3_TO_4_WKS" runat="server" Text="3-4 WKS" Width="88%" CssClass="lbl" />
                                </td>
                                <td style="width: 12%" class="lbl">
        <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_5_TO_6_WEEKS" runat="server" Text="5-6 WEEKS" Width="85%" CssClass="lbl" />
                                </td>
                                <td style="width: 12%" class="lbl">
        <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_TO_7_TO_8_WKS" runat="server" Text="7-8 WKS" Width="90%" CssClass="lbl" />
                                </td>
                                <td style="width: 5%">
        <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_NEEDED_MONTH" runat="server" Text=" " />
                                </td>
                                <td style="width: 12%">
        <asp:TextBox ID="TXT_PATIENT_NEXT_VISIT_NEEDED_MONTH" runat="server" CssClass="textboxCSS" Width="100%"></asp:TextBox>
                                </td>
                                <td style="width: 12%" class="lbl">
        <asp:Label ID="lbl_MONTHS_WKS" runat="server" Text="MONTHS WKS" CssClass="lbl" Width="85%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%" class="lbl">
        
        <asp:CheckBox ID="CHK_PATIENT_AS_NEEDED" runat="server" Text="- AS NEEDED" Width="92%" CssClass="lbl" />
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                                <td style="width: 100px">
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <asp:TextBox ID="TXT_EVENT_ID" runat="server" Width="10%" Visible="False"></asp:TextBox></td>
                                <td style="width: 12%">
                                    <asp:Button ID="BTN_PREVIOUS" runat="server" OnClick="BTN_PREVIOUS_Click" Text="Previous"
                                        Width="91%" CssClass="Buttons" /></td>
                                <td style="width: 25%">
                                    <asp:Button ID="BTN_SAVE_NEXT" runat="server" Text="Save & Next" Width="45%" OnClick="BTN_SAVE_NEXT_Click" CssClass="Buttons" /></td>
                                <td style="width: 25%">
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
               