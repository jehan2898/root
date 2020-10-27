<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_CO_Patient_Intake3.aspx.cs" Inherits="Bill_Sys_CO_Patient_Intake3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table class="ContentTable" width="100%">
        <tr>
            <td class="TDPart" align="center" valign="top">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblFormHeader" runat="server" Text="PATIENT  INTAKE - (PAGE  III of IV)" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="height: 21px">
                        </td>
                    </tr>
                </table>
                <table width="99%">
                    <tbody>
                        <tr>
                            <td align="left" colspan="6" valign="top">
                                <asp:Label ID="lbl_ConfidentialQuestionaire" runat="server" Font-Bold="True" Text="CONFIDENTIAL NEW PATIENT QUESTIONAIRE"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle">
                                <asp:Label ID="lbl_TreatmentPresentCondition" runat="server" Text="What treatments have you're received for this present Condition?" Width="215px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_TREATMENT_SURGERY" runat="server" Text="Surgery" />
                                <asp:CheckBox ID="CHK_TREATMENT_SPINAL_INJECTIONS" runat="server" Text="Spinal Injections" />
                                <asp:CheckBox ID="CHK_TREATMENT_PHYSICAL_THERAPY" runat="server" Text="Physical Therapy" />
                                <asp:CheckBox ID="CHK_TREATMENT_CHIROPRACTIC" runat="server" Text="Chiropractic" />
                                <asp:CheckBox ID="CHK_TREATMENT_MEDICINE" runat="server" Text="Medicine" />
                                <asp:CheckBox ID="CHK_TREATMENT_XRAY_ACUPUNCTURE" runat="server" Text="X-Ray Acupuncture" />
                                <asp:CheckBox ID="CHK_TREATMENT_OTHER" runat="server" Text="Other" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_OtherTreatmentPresentCondition" runat="server" Text="Other"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_TREATMENT_OTHER" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="middle">
                                <asp:Label ID="lbl_TreatedSameCondition" runat="server" Text="Have your been treated previously for the same condition?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="top" class="lbl">
                                <asp:RadioButtonList ID="rdblstTREATED_PREVIOUSLY" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblstTREATED_PREVIOUSLY_SelectedIndexChanged">
                                  <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                 <asp:ListItem Value="1" Text="No"></asp:ListItem>                                                 
                                 </asp:RadioButtonList>
                                <asp:TextBox ID="txtrdblstTREATED_PREVIOUSLY" runat="server" Visible="false" Width="109px">-1</asp:TextBox>
                          </td>
                        
                        </tr>
                        <tr>
                            <td align="left"  valign="middle">
                                <asp:Label ID="lbl_IFYes" runat="server" Text="If yes, by"></asp:Label>
                            </td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle" class="lbl">
                                <asp:CheckBox ID="CHK_TREATED_PREVIOUSLY_BY_MD" runat="server" Text="MD" />
                                <asp:CheckBox ID="CHK_TREATED_PREVIOUSLY_BY_CHIROPRACTOR" runat="server" Text="Chiropractor" />
                                <asp:CheckBox ID="CHK_TREATED_PREVIOUSLY_BY_PHYSICAL_THERAPIST" runat="server" Text="Physical Therapist" />
                                <asp:CheckBox ID="CHK_TREATED_PREVIOUSLY_BY_OTHER" runat="server" Text="Other" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_BeenTreatedPreviously" runat="server" Text="Other"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_TREATED_PREVIOUSLY_OTHER" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_WhatMakesProblembetter" runat="server" Text="What makes your problem better?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="top" class="lbl">
                                <asp:CheckBox ID="CHK_MAKES_BETTER_NOTHING" runat="server" Text="Nothing" />
                                <asp:CheckBox ID="CHK_MAKES_BETTER_LYING_DOWN" runat="server" Text="Lying down" />
                                <asp:CheckBox ID="CHK_MAKES_BETTER_WALKING" runat="server" Text="Walking" />
                                <asp:CheckBox ID="CHK_MAKES_BETTER_STANDING" runat="server" Text="Standing" />
                                <asp:CheckBox ID="CHK_MAKES_BETTER_SITTING" runat="server" Text="Sitting" />
                                <asp:CheckBox ID="CHK_MAKES_BETTER_MOVEMENT_EXERCISE" runat="server" Text="Movement/Exercise" />
                                <asp:CheckBox ID="CHK_MAKES_BETTER_INACTIVITY" runat="server" Text="Inactivity" />
                                <asp:CheckBox ID="CHK_MAKES_BETTER_OTHER" runat="server" Text="Other" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_OtherBetter" runat="server" Text="Other"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                            </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_MAKES_BETTER_OTHER" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_WhatMakesProblemWorse" runat="server" Text="What makes your problem worse?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="top" class="lbl">
                                <asp:CheckBox ID="CHK_MAKES_WORSE_NOTHING" runat="server" Text="Nothing" />
                                <asp:CheckBox ID="CHK_MAKES_WORSE_LYING_DOWN" runat="server" Text="Lying down" />
                                <asp:CheckBox ID="CHK_MAKES_WORSE_WALKING" runat="server" Text="Walking" />
                                <asp:CheckBox ID="CHK_MAKES_WORSE_STANDING" runat="server" Text="Standing" />
                                <asp:CheckBox ID="CHK_MAKES_WORSE_SITTING" runat="server" Text="Sitting" />
                                <asp:CheckBox ID="CHK_MAKES_WORSE_MOVEMENT_EXERCISE" runat="server" Text="Movement/Exercise" />
                                <asp:CheckBox ID="CHK_MAKES_WORSE_INACTIVITY" runat="server" Text="Inactivity" />
                                <asp:CheckBox ID="CHK_MAKES_WORSE_OTHER" runat="server" Text="Other" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_OtherWorse" runat="server" Text="Other"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_MAKES_WORSE_OTHER" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_DoyouWork" runat="server" Text="Do you work?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="top" class="lbl">
                             <asp:RadioButtonList ID="rdblstDO_YOU_WORK" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblstDO_YOU_WORK_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>                                                     
                                                </asp:RadioButtonList> 
                                                  <asp:TextBox ID="txtrdblstDO_YOU_WORK" runat="server" Visible="false" Width="102px">-1</asp:TextBox>
                                
                            
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_WorkIfYes" runat="server" Text="If yes"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="top" class="lbl">
                                <asp:CheckBox ID="CHK_WORK_SITTING_MORE_THAN_50_OF_WORKDAY" runat="server" Text="Sitting more than 50% of workday" />
                                <asp:CheckBox ID="CHK_WORK_LIGHT_MANUAL_LABOR" runat="server" Text="Light Manual Labor" />
                                <asp:CheckBox ID="CHK_WORK_MANUAL_LABOR" runat="server" Text="Manual Labor" />
                                <asp:CheckBox ID="CHK_WORK_HEAVY_MANUAL_LABOR" runat="server" Text="Heavy Manual Labor" />
                                <asp:CheckBox ID="CHK_WORK_OTHER" runat="server" Text="Other" />
                                </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_OtherWork" runat="server" Text="Other"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <asp:TextBox ID="TXT_WORK_OTHER" runat="server" Height="50px" TextMode="MultiLine" Width="487px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_ComplaintAbilitytoWork" runat="server" Text="Are your complaints affecting your ability to work or otherwise be active?" Height="29px"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left" colspan="4" valign="middle">
                                <table>
                                    <tr>
                                        <td align="left" valign="middle" class="lbl">
                                            <asp:CheckBox ID="CHK_COMPLAINT_AFFECT_NO_AFFECT" runat="server" Text="No effect" /></td>
                                        <td align="left" valign="middle">
                                            <asp:CheckBox ID="CHK_COMPLAINT_AFFECT_SOME_RESTRICTION" runat="server" Text="Some physical restrictions(able to perform light duty housework and household tasks)" CssClass="lbl" Width="220px" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:CheckBox ID="CHK_COMPLAINT_AFFECT_LIMITED_ASSISTANCE" runat="server" Text="Need limited assistance with everyday tasks" Width="243px" /></td>
                                        <td align="left" valign="middle" class="lbl">
                                            <asp:CheckBox ID="CHK_COMPLAINT_AFFECT_NEED_ASSISTANCE" runat="server" Text="Need assistance often" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle" class="lbl">
                                            <asp:CheckBox ID="CHK_COMPLAINT_AFFECT_HAVE_INABILITY" runat="server" Text="Have a significant inability to function without assistance?" /></td>
                                        <td align="left" valign="middle" class="lbl">
                                            <asp:CheckBox ID="CHK_COMPLAINT_AFFECT_CANNOT_CARE" runat="server" Text="Cannot care for self" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_CurrentlyTakingMedications" runat="server" Text="Are you currently taking medication?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="top" class="lbl">
                                <asp:RadioButtonList ID="rdblstTAKING_MEDICATION" runat="server" RepeatDirection="Horizontal" CssClass="lbl" OnSelectedIndexChanged="rdblstTAKING_MEDICATION_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                     
                                                   
                                                </asp:RadioButtonList>
                                                  <asp:TextBox ID="txtrdblstTAKING_MEDICATION" runat="server" Visible="false" Width="79px">-1</asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lbl_IfYesMedications" runat="server" Text="If yes"></asp:Label></td>
                            <td align="center" valign="middle">
                                <asp:Label ID="lbl_Colon15" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_TAKING_MEDICATION" runat="server" Height="47px" TextMode="MultiLine" Width="202px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_AlergictoMedications" runat="server" Text="Are you allergic to any drugs or medication?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="top">
                             <asp:RadioButtonList ID="rdblstALLERGIC_TO_DRUGS" runat="server" RepeatDirection="Horizontal" CssClass="lbl" OnSelectedIndexChanged="rdblstALLERGIC_TO_DRUGS_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                     
                                                   
                                                </asp:RadioButtonList>
                                                  <asp:TextBox ID="txtrdblstALLERGIC_TO_DRUGS" runat="server" Visible="false" Width="30px">-1</asp:TextBox>
                                &nbsp;
                               
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lbl_IfYesAlergic" runat="server" Text="If yes, by"></asp:Label></td>
                            <td align="center" valign="middle">
                                <asp:Label ID="lbl_Colon17" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_ALLERGIC_TO_DRUGS" runat="server" Height="47px" TextMode="MultiLine" Width="202px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lblDoyouSmoke" runat="server" Text="Do you smoke?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="top">
                              
                                 <asp:RadioButtonList ID="rdblstDO_YOU_SMOKE" runat="server" RepeatDirection="Horizontal" CssClass="lbl" Width="99px" OnSelectedIndexChanged="rdblstDO_YOU_SMOKE_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                     
                                              
                                                </asp:RadioButtonList>
                                                  <asp:TextBox ID="txtrdblstDO_YOU_SMOKE" runat="server" Visible="false" Width="86px">-1</asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lbl_SmokingPacks" runat="server" Text="How many Packs/Day?"></asp:Label></td>
                            <td align="center" valign="middle">
                                <asp:Label ID="lbl_Colon19" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_HOW_MANY_PACKS" runat="server" Width="202px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_SufferTypeofAlergies" runat="server" Text="Do you suffer from any type of allergies?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="top">
                                <asp:RadioButtonList ID="rdblstSUFFER_ALLERGIES" runat="server" RepeatDirection="Horizontal" CssClass="lbl" Width="99px" OnSelectedIndexChanged="rdblstSUFFER_ALLERGIES_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                     
                                                   
                                                </asp:RadioButtonList>
                                                  <asp:TextBox ID="txtrdblstSUFFER_ALLERGIES" runat="server" Visible="false" Width="71px">-1</asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lbl_IfYesTypeofAlergies" runat="server" Text="If Yes"></asp:Label></td>
                            <td align="center" valign="middle">
                                <asp:Label ID="lbl_Colon21" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_SUFFER_ALLERGIES" runat="server" Height="47px" TextMode="MultiLine" Width="202px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_HadSurgery" runat="server" Text="Have you had any surgery?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="top">
                                <asp:RadioButtonList ID="rdblstHAD_SURGERY" runat="server" RepeatDirection="Horizontal" CssClass="lbl" Width="99px" OnSelectedIndexChanged="rdblstHAD_SURGERY_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                                     
                                                   
                                                </asp:RadioButtonList>
                                                  <asp:TextBox ID="txtrdblstHAD_SURGERY" runat="server" Visible="false" Width="70px">-1</asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lbl_IfYesSurgery" runat="server" Text="If Yes"></asp:Label></td>
                            <td align="center" valign="middle">
                                <asp:Label ID="lbl_Colon23" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_HAD_SURGERY" runat="server" Height="47px" TextMode="MultiLine" Width="202px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="top">
                                <asp:Label ID="lbl_WomenPregnent" runat="server" Text="Women: Are you pregnant?"></asp:Label></td>
                            <td align="center" valign="middle" style="width: 5px">
                                </td>
                            <td align="left"  valign="top">
                                 <asp:RadioButtonList ID="rdblstARE_YOU_PREGNANT" runat="server" RepeatDirection="Horizontal" CssClass="lbl" Width="158px" OnSelectedIndexChanged="rdblstARE_YOU_PREGNANT_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Not Sure"></asp:ListItem>
                                                     
                                                   
                                                </asp:RadioButtonList>
                                                  <asp:TextBox ID="txtrdblstARE_YOU_PREGNANT" runat="server" Visible="false" Width="48px">-1</asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblPatientInitials" runat="server" Text="Patient’s Initials"></asp:Label></td>
                            <td align="center" valign="middle">
                                <asp:Label ID="lbl_Colon25" runat="server" Font-Bold="True" Text=":"></asp:Label></td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="TXT_PATIENT_INITIAL" runat="server" Width="202px"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TDPart" valign="top">
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

</asp:content>
