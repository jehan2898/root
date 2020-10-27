<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_CO_Patient_Intake2.aspx.cs" Inherits="Bill_Sys_CO_Patient_Intake2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" align="center" valign="top" style="width: 842px">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblFormHeader" runat="server" Text="PATIENT  INTAKE - (PAGE  II of IV)" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        </td>
                    </tr>
                </table>
                <table width="99%">
                    <tbody>
                        <tr>
                            <td align="left" colspan="6" valign="middle">
                                <asp:Label ID="lbl_InsurnaceInformation" runat="server" Font-Bold="True" Text="INSURANCE  INFORMATION"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 167px" >
                                <asp:Label ID="lbl_InsuredsName" runat="server" Text="Insured's Name"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="top" style="width: 164px" >
                                <asp:TextBox ID="TXT_INSURED_NAME" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_INSURED_NAME1" runat="server" Width="1px"></asp:Label>
                            </td>
                            <td align="left" valign="middle" style="width: 100%">
                                <asp:Label ID="lbl_NameofInsuranceCompany" runat="server" Text="Name  of  Insurance Company" Width="100%"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TXT_INSURANCE_COMPANY_NAME" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_INSURANCE_COMPANY_NAME1" runat="server"></asp:Label>
                           </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 167px">
                                <asp:Label ID="lbl_PatientRelationshiptoInsured" runat="server" Text="Patient's  Relationship to Insured" Width="100%"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px" >
                                </td>
                            <td valign="top"  colspan="4" class="lbl">
                                <asp:RadioButtonList ID="rdblstPATIENT_RELATIONSHIP_TO_INSURED" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblstPATIENT_RELATIONSHIP_TO_INSURED_SelectedIndexChanged">
                                                    <asp:ListItem Value="5" Text="Self"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="Spouse"></asp:ListItem>
                                                     <asp:ListItem Value="7" Text="Child"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="Driver"></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                               
                                                <asp:TextBox ID="txtrdblstPATIENT_RELATIONSHIP_TO_INSURED" runat="server" Visible="false" Width="1px">-1</asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_OtherRelationshipInsured" runat="server" Text="Other"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_PATIENT_RELATIONSHIP_TO_INS_INSURED_OTHER" runat="server" Width="242px" Height="31px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="height: 10px; width: 167px;" >
                                <asp:Label ID="lbl_InsuredAddress" runat="server" Text="Address"></asp:Label></td>
                            <td align="center" valign="middle" style="height: 10px; width: 5px;">
                                </td>
                            <td align="left" valign="top" style="height: 10px; width: 100%;" >
                                <asp:TextBox ID="TXT_INSURED_ADDRESS" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_INSURED_ADDRESS1" runat="server" Width="86%"></asp:Label>
                            </td>
                            <td align="left" valign="top" style="height: 10px; width: 191px;">
                                <asp:Label ID="lbl_InsuredPhoneNo" runat="server" Text="Insurence Phone #"></asp:Label></td>
                            <td align="center" valign="middle" style="height: 10px;">
                                </td>
                            <td align="left" valign="top" style="height: 10px">
                                <asp:TextBox ID="TXT_INSURANCE_PHONE_EXTENSION" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="TXT_INSURANCE_PHONE" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_INSURANCE_PHONE1" runat="server" Width="1px"></asp:Label>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_InsurancePolicyNo" runat="server" Text="Insurence Policy #"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_INSURANCE_POLICY_NUMBER" runat="server" Width="1px" Visible="False"></asp:TextBox>
                                <asp:Label ID="TXT_INSURANCE_POLICY_NUMBER1" runat="server" Width="7px"></asp:Label>
                               </td> 
                               </tr>
                        <tr>
                            <td align="left" colspan="6" valign="middle">
                                <asp:Label ID="lbl_SecondaryInsurance" runat="server" Font-Bold="True" Text="SECONDARY INSURANCE"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 100%" >
                                <asp:Label ID="lbl_SecondaryInsuranceConame" runat="server" Text="Name of Insurance Company" Width="100%"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="top" style="width: 164px" >
                                <asp:TextBox ID="TXT_SECONDARY_INSURANCE" runat="server" Width="260px"></asp:TextBox></td>
                            <td align="left" valign="top" style="width: 191px">
                                <asp:Label ID="lbl_SecondaryInsAddress" runat="server" Text="Address" Width="83px"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_SECONDARY_INSURANCE_ADDRESS" runat="server" Width="190px" Height="26px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px" >
                                <asp:Label ID="lbl_InsPhoneNo" runat="server" Text="Insurance Phone #"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="middle" style="width: 100%" >
                                <asp:TextBox ID="TXT_SECONDARY_INSURANCE_PHONE_EXTENSION" runat="server" Width="63px"></asp:TextBox>
                                <asp:TextBox ID="TXT_SECONDARY_INSURANCE_PHONE" runat="server" Width="172px"></asp:TextBox></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_SecondaryInsPolicyNo" runat="server" Text="Policy #"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_SECONDARY_INSURANCE_POLICY" runat="server" Width="188px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" valign="middle">
                                <asp:Label ID="lbl_PatientQuestionery" runat="server" Font-Bold="True" Text="CONFIDENTIAL NEW PATIENT QUESTIONAIRE"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px" >
                                <asp:Label ID="lbl_MajorComplaints" runat="server" Text="Major Complaints('s)"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="middle" colspan="4">
                                <asp:TextBox ID="TXT_MORE_COMPLAINTS" runat="server" Height="50px" TextMode="MultiLine"
                                    Width="213px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" valign="middle">
                                <asp:Label ID="lbl_PresentPastSymptoms" runat="server" Font-Bold="True" Text="PRESENT AND PAST SYMPTOMS"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px" >
                                <asp:Label ID="lbl_NeckPain" runat="server" Text="Neck Pain"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="middle" style="width: 164px" class="lbl" >
                                <asp:CheckBox ID="CHK_NECK_PAIN_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_NECK_PAIN_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lblMiddleBackPain" runat="server" Text="Middle Back Pain" Width="159px"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_MIDDLE_BACK_PAIN_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_MIDDLE_BACK_PAIN_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px; height: 7px;" >
                                <asp:Label ID="lbl_LowBackPain" runat="server" Text="Low Back Pain" Width="89px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px; height: 7px;">
                                </td>
                            <td align="left" valign="middle" style="width: 164px; height: 7px;" class="lbl" >
                                <asp:CheckBox ID="CHK_LOW_BACK_PAIN_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_LOW_BACK_PAIN_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px; height: 7px;">
                                <asp:Label ID="lbl_Headache" runat="server" Text="Headache"></asp:Label></td>
                            <td align="center" valign="middle" style="height: 7px;">
                                </td>
                            <td align="left" valign="middle" class="lbl" style="height: 7px">
                                <asp:CheckBox ID="CHK_HEADACHE_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_HEADACHE_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px; height: 25px;" >
                                <asp:Label ID="lbl_Dizziness" runat="server" Text="Dizziness" Width="62px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px; height: 25px;">
                                </td>
                            <td align="left" valign="middle" style="width: 164px; height: 25px;" class="lbl" >
                                <asp:CheckBox ID="CHK_DIZZINESS_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_DIZZINESS_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px; height: 25px;">
                                <asp:Label ID="lbl_Convulsions" runat="server" Text="Convulsions"></asp:Label></td>
                            <td align="center" valign="middle" style="height: 25px;">
                                </td>
                            <td align="left" valign="middle" class="lbl" style="height: 25px">
                                <asp:CheckBox ID="CHK_CONVULSIONS_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_CONVULSIONS_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px" >
                                <asp:Label ID="lbl_FaintingVisual" runat="server" Text="Fainting, Visual" Width="97px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="middle" style="width: 164px" class="lbl" >
                                <asp:CheckBox ID="CHK_FAINTING_VISUAL_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_FAINTING_VISUAL_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Disturbance" runat="server" Text="Disturbance,Nausea"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_DISTURBANCE_NAUSEA_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_DISTURBANCE_NAUSEA_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="width: 167px" >
                                <asp:Label ID="lbl_Shoulder_pain" runat="server" Text="Shoulder Pain"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="middle" style="width: 164px" class="lbl" >
                                <asp:CheckBox ID="CHK_SHOULDER_PAIN_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_SHOULDER_PAIN_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_PaininUpperArmsOrElbows" runat="server" Text="Pain in Upper Arms or Elbows"></asp:Label></td>
                            <td align="center" valign="middle">
                            </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_PAIN_IN_UPPER_ARMS_OR_ELBOWS_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_PAIN_IN_UPPER_ARMS_OR_ELBOWS_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_PaininUpperLegorHip" runat="server" Text="Pain in Upper Leg or Hip" Width="134px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_PAIN_IN_UPPER_LEG_OR_HIP_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_PAIN_IN_UPPER_LEG_OR_HIP_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_PaininLowerLegorKnee" runat="server" Text="Pain in Lower Leg or Knee" Width="152px"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_PAIN_IN_LOWER_LEG_OR_KNEE_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_PAIN_IN_LOWER_LEG_OR_KNEE_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_PaininAnkleorFoot" runat="server" Text="Pain in Ankle or Foot"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_PAIN_IN_ANKLE_OR_FOOT_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_PAIN_IN_ANKLE_OR_FOOT_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_SwellingorStiffnessofJoints" runat="server" Text="Swelling/Stiffness of Joints"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_SWELLING_STIFFNESS_OF_JOINTS_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_SWELLING_STIFFNESS_OF_JOINTS_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_MuscularIncoordination" runat="server" Text="Muscular Incoordination" Width="143px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_MUSCULAR_IN_COORDINATION_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_MUSCULAR_IN_COORDINATION_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Jawpain" runat="server" Text="Jaw Pain"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_JAW_PAIN_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_JAW_PAIN_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_Tinnitus" runat="server" Text="Tinnitus (Ear Noises)"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_TINNITUS_EAR_NOISES_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_TINNITUS_EAR_NOISES_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_ChestPain" runat="server" Text="Chest Pain"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_CHEST_PAIN_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_CHEST_PAIN_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_RapidHeartBeat" runat="server" Text="Rapid Heart Beat"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_RAPID_HEART_BEAT_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_RAPID_HEART_BEAT_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_LossofAppetite" runat="server" Text="Loss of Appetite"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_LOSS_OF_APPETITE_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_LOSS_OF_APPETITE_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_BloodDisorder" runat="server" Text="Blood Disorder"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_BLOOD_DISORDER_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_BLOOD_DISORDER_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_ExcessiveThirst" runat="server" Text="Excessive Thirst"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_EXCESSIVE_THIRST_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_EXCESSIVE_THIRST_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_Chronic_Cough" runat="server" Text="Chronic Cough"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_CHRONIC_COUGH_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_CHRONIC_COUGH_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Chronic_Sinusitis" runat="server" Text="Chronic Sinusitis"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_CHRONIC_SINUSITIS_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_CHRONIC_SINUSITIS_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_GeneralFatigue" runat="server" Text="General Fatigue"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_GENERAL_FATIGUE_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_GENERAL_FATIGUE_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_PainfulUrination" runat="server" Text="Painful Urination"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_PAINFUL_URINATION_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_PAINFUL_URINATION_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_FrequentUrination" runat="server" Text="Frequent Urination"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_FREQUENT_URINATION_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_FREQUENT_URINATION_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_AbdominalPain" runat="server" Text="Abdominal Pain"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_ABDOMINAL_PAIN_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_ABDOMINAL_PAIN_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_DifficultyinSwallowing" runat="server" Text="Difficulty in Swallowing"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_DIFFICULTY_IN_SWALLOWING_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_DIFFICULTY_IN_SWALLOWING_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Depression" runat="server" Text="Depression"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_DEPRESSION_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_DEPRESSION_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_HighBloodPressure" runat="server" Text="High Blood Pressure"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_HIGH_BLOOD_PRESSURE_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_HIGH_BLOOD_PRESSURE_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Angina" runat="server" Text="Angina"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_ANGINA_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_ANGINA_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_HeartAttack" runat="server" Text="Heart Attack"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_HEART_ATTACK_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_HEART_ATTACK_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Stroke" runat="server" Text="Stroke"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_STROKE_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_STROKE_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_Asthma" runat="server" Text="Asthma"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_ASTHMA_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_ASTHMA_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Cancer" runat="server" Text="Cancer"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_CANCER_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_CANCER_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_Emphysemia" runat="server" Text="Emphysemia (Lung Disorders)"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_EMPHYSEMIA_LUNG_DISORDER_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_EMPHYSEMIA_LUNG_DISORDER_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Arthritis" runat="server" Text="Arthritis"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_ARTHRITIS_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_ARTHRITIS_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_Diabetes" runat="server" Text="Diabetes"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" valign="middle" style="width: 164px" class="lbl">
                                <asp:CheckBox ID="CHK_DIABETES_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_DIABETES_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px">
                                <asp:Label ID="lbl_Ulcer" runat="server" Text="Ulcer"></asp:Label></td>
                            <td align="center" valign="middle">
                                </td>
                            <td align="left" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_ULCER_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_ULCER_PAST" runat="server" Text="Past" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px; height: 20px;">
                                <asp:Label ID="lbl_BladderInfection" runat="server" Text="Bladder Infection"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px; height: 20px;">
                                </td>
                            <td align="left"  valign="middle" style="width: 164px; height: 20px;" class="lbl">
                                <asp:CheckBox ID="CHK_BLADDER_INFECTION_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_BLADDER_INFECTION_PAST" runat="server" Text="Past" /></td>
                            <td align="left" valign="middle" style="width: 191px; height: 20px;">
                                <asp:Label ID="lbl_Colitis" runat="server" Text="Colitis"></asp:Label></td>
                            <td align="center" valign="middle" style="height: 20px;">
                                </td>
                            <td align="left" valign="middle" class="lbl" style="height: 20px">
                                <asp:CheckBox ID="CHK_COLITIS_PRESENT" runat="server" Text="Present" />
                                <asp:CheckBox ID="CHK_COLITIS_PAST" runat="server" Text="Past" /></td>
                        </tr>                     
                        
                        <tr>
                        </tr>
                    
                        
                        <tr>
                            
                            <td align="left" valign="middle" style="width: 100%" colspan="6">
                                <asp:Label ID="lbl_DescribeCurrentPain" runat="server" Text="Please Describe Character of your Current Pain" Width="289px"></asp:Label>
                                </td>
                                
                                
                            
                          </tr>
                          <tr>
                            <td align="left" colspan="4" valign="middle">
                                <table width="100%">
                                    <tr>
                                        <td align="left" valign="middle" style="width: 118px" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_SHARP_SHOOTING" runat="server" Text="Sharp/Shooting" /></td>
                                        <td align="left" valign="middle" style="width: 180px" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_SHARP_DULL" runat="server" Text="Sharp/Dull" /></td>
                                        <td align="left" valign="middle" style="width: 125px" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_ACHES" runat="server" Text="Aches" /></td>
                                        <td align="left" valign="middle" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_DULL" runat="server" Text="Dull" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle" style="width: 118px; height: 20px;" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_WEAKNESS" runat="server" Text="Weakness" /></td>
                                        <td align="left" valign="middle" style="width: 180px; height: 20px;" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_THROBBING_GNAWING" runat="server" Text="Throbbing/Gnawing" /></td>
                                        <td align="left" valign="middle" style="width: 125px; height: 20px;" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_NUMBNESS" runat="server" Text="Numbness" /></td>
                                        <td align="left" valign="middle" class="lbl" style="height: 20px">
                                            <asp:CheckBox ID="CHK_PAIN_SHOOTING" runat="server" Text="Shooting" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle" style="width: 118px" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_SORENESS" runat="server" Text="Soreness" /></td>
                                        <td align="left" valign="middle" style="width: 180px" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_GRIPPING_CONSTRICTING" runat="server" Text="Gripping/Constricting" /></td>
                                        <td align="left" valign="middle" style="width: 125px" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_BURNING" runat="server" Text="Burning" /></td>
                                        <td align="left" valign="middle" class="lbl">
                                            <asp:CheckBox ID="CHK_PAIN_TINGLING" runat="server" Text="Tingling" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle" style="width: 167px">
                                <asp:Label ID="lbl_Problembegin" runat="server" Text="Did your Problem begin" Width="151px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td colspan="4" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_PROBLEM_BEGIN_ACCIDENT" runat="server" Text="Due to an accident" />
                                <asp:CheckBox ID="CHK_PROBLEM_BEGIN_MULTIPLE_INCIDENT" runat="server" Text="Multiple incidents" />
                                <asp:CheckBox ID="CHK_PROBLEM_BEGIN_NO_REASON" runat="server" Text="Gradually no specific reason" />
                                <asp:CheckBox ID="CHK_PROBLEM_BEGIN_OTHER" runat="server" Text="Other" /></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top" style="width: 100%">
                                <asp:Label ID="lbl_OtherProblemBegin" runat="server" Text="Other"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_PROBLEM_BEGIN_OTHER" runat="server" Height="50px" TextMode="MultiLine"
                                    Width="240px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top" style="width: 100%">
                                <asp:Label ID="lbl_DescribeHowyourProblemBegin" runat="server" Text="Describe how your Problem Began" Width="100%"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_DESCRIBE_PROBLEM_BEGIN" runat="server" Height="50px" TextMode="MultiLine" Width="240px"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TDPart" valign="top" style="width: 842px">
                <table style="width: 100%">
                    <tr>
                        <td align="center">
                           <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                            <asp:Button ID="css_btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="css_btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="TXT_I_EVENT" runat="server" Visible="false">1</asp:TextBox></td>
        </tr>
    </table>
  </asp:Content>