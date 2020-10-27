<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_MedicalHistory.aspx.cs" Inherits="Bill_Sys_IM_MedicalHistory"
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
                        <td width="25%" class="lbl">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtPatientName" runat="server" Width="90%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="13%" class="lbl">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOA" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%" >
                            Date of Examination</td>
                        <td width="20%">
                            <asp:TextBox ID="txtDOE" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" width="10%">
                            Date of birth</td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOB" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
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
                        <td width="100%" class="lbl">
                            
                                <b>MEDICAL HISTORY</b> 
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            Does the patient’s medical history reveal any pre-existing condition(s) that may
                            affect the treatment and/or prognosis?&nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="1" width="12%" class="lbl">
                            <asp:RadioButtonList ID="rdlMedicalHistoryAffectTreatnent" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">YES</asp:ListItem>
                                <asp:ListItem Value="1">NO</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="17%" class="lbl">
                            If Yes, list and describe
                        </td>
                        <td width="60%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                
                <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlMedicalHistoryAffectTreatnent" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl">
                            <asp:TextBox ID="txtMedicalHistoryAffectTreatnentdescribe" runat="server" TextMode="MultiLine"
                                Width="90%" Height="40%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="25%" class="lbl">
                            NONE
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalNone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Diabetes
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalDiabetes" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Heart Attack
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalHeartAttack" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Seizures
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalSeizures" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            High Blood Pressure
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalHighBloodPressure" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Angina/Chest Pain
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalAnginaChestPain" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Phlebitis
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalPhlebitis" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Mitral Valve Prolapse
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalMitralValveProlapse" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Angioplasty
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalAngioplasty" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Hepatitis
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalHepatitis" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Bleeding Disorder
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalBleedingDisorder" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Stroke/TIA’s
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalStrokeTIA" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Ulcers
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalUlcers" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Circulation Disorder
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicaCirculationDisorder" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Anemia
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalAnemia" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Hiatal Hernia
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalHiatalHernia" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Back Pain
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalBackPain" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Arthritis
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalArthritis" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Osteoporosis
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalOsteoporosis" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Scar Former
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalScarFormer" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Thyroid Disorder
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalThyroidDisorder" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Asthma
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalAsthma" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Kidney Disorder
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalKidneyDisorder" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Cirrhosis
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalCirrhosis" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            Cancer
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtMedicalCancer" runat="server"></asp:TextBox>
                        </td>
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
                        <td colspan="4" width="100%" class="lbl">
                            <b>ALLERGIES</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesNone" Text="None" runat="server" />
                        </td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesPenicillin" Text="Penicillin" runat="server" />
                        </td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesAspirin" Text="Aspirin" runat="server" />
                        </td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesCodeine" Text="Codeine" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesNovocaine" Text="Novocaine" runat="server" />
                        </td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesIodine" Text="Iodine" runat="server" />
                        </td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesTape" Text="Tape" runat="server" />
                        </td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkAllergiesOther" Text="Other" runat="server" />
                        </td>
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
                        <td colspan="3" width="100%" class="lbl" style="height: 15px">
                            <b>MEDICATION</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="33%">
                            1.
                            <asp:TextBox ID="txtMedications1" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="33%">
                            2.
                            <asp:TextBox ID="txtMedications2" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="33%">
                            3.&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:TextBox ID="txtMedications3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="33%" style="height: 22px">
                            4.
                            <asp:TextBox ID="txtMedications4" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="30%" style="height: 22px">
                            5.
                            <asp:TextBox ID="txtMedications5" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="33%" style="height: 22px">
                            None.
                            <asp:TextBox ID="txtMedications6" runat="server"></asp:TextBox>
                        </td>
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
                        <td colspan="3" width="100%" class="lbl" style="height: 15px">
                            <b>PREVIOUS SURGERIES AND HOSPITALIZATIONS</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="33%">
                            1.
                            <asp:TextBox ID="txtPreviousSurgrerise1" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="33%">
                            2.
                            <asp:TextBox ID="txtPreviousSurgrerise2" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="33%">
                            3. &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:TextBox ID="txtPreviousSurgrerise3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="33%">
                            4.
                            <asp:TextBox ID="txtPreviousSurgrerise4" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="33%">
                            5.
                            <asp:TextBox ID="txtPreviousSurgrerise5" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="33%">
                            None.
                            <asp:TextBox ID="txtPreviousSurgrerise6" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--<table width="100%">
        <tr>
            <td width="100%" class="TDPart" style="height: 100px">
                <table width="100%">
                    <tr>
                        <td width="100%" class="TDPart">
                            <table width="100%">
                                <tr>
                                    <td width="25%" class="lbl">
                                        Patient's Name
                                    </td>
                                    <td width="40%">
                                        <asp:TextBox ID="txtPatient3" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td width="10%" class="lbl">
                                        Date of accident
                                    </td>
                                    <td width="20%">
                                        <asp:TextBox ID="DOA3" runat="server" Width="60%"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="DOA3"
                                            PopupButtonID="imgbtnFromDate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" class="lbl" width="20%">
                                        Date of Examination</td>
                                    <td colspan="1" width="20%">
                                        <asp:TextBox ID="DOE3" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/cal.gif" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="DOE3"
                                            PopupButtonID="imgbtnFromDate" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>--%>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart" style="height: 216px">
                <table width="100%">
                    <tr>
                        <td colspan="4" width="100%" class= "lbl">
                            <b>FAMILY HISTORY</b></td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            NONE
                        </td>
                        <td width="24%" class="lbl">
                            <asp:TextBox ID="txtFamilyNone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            Diabetes
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtFamilyDiabetes" runat="server"></asp:TextBox>
                        </td>
                        <td width="16%" class="lbl">
                            High Blood Pressure
                        </td>
                        <td width="16%" class="lbl">
                            <asp:TextBox ID="txtFamilyBloodPressure" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" class="lbl">
                            Bleeding Tendencies
                        </td>
                        <td width="16%" class="lbl">
                            <asp:TextBox ID="txtFamilyBleedingTendencies" runat="server"></asp:TextBox>
                        </td>
                        <td width="16%" class="lbl">
                            Cancer
                        </td>
                        <td width="16%" class="lbl">
                            <asp:TextBox ID="txtFamilyCancer" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" class="lbl">
                            Orthopedics Disease
                        </td>
                        <td width="16%" class="lbl">
                            <asp:TextBox ID="txtFamilyOrthopedicsDisease" runat="server"></asp:TextBox>
                        </td>
                        <td width="16%" class="lbl">
                            Bone or Joint Problems
                        </td>
                        <td width="16%" class="lbl">
                            <asp:TextBox ID="txtFamilyBoneJointProblems" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class= "lbl">
                            <b>SOCIAL HISTORY</b>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="lbl" style="width: %">
                            <asp:CheckBox ID="chkSmoking" Text="Smoking" runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:TextBox ID="txtSmokingPacksPerDay" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="15%" class="lbl">
                            Packs Per day for
                        </td>
                        <td width="10%" class="lbl">
                            <asp:TextBox ID="txtOuitSmokingThisYear" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="60%">
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
                            <asp:CheckBox ID="chkQuitSmoking" Text="Quit Smoking" runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkQuitSmokingThisYear" Text="this year/<1 year" runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkQuitSmoking1_5Year" Text="1-5 years " runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkQuitSmokingGreterThan10Years" Text=">10 years" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkAlcohol" Text="Alcohol" runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkAlcoholDaily" Text="Daily" runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkAlcohol1_2Week" Text="1-2 x/week" runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkAlcohol1_2Month" Text="1-2 x/month" runat="server" />
                        </td>
                        <td width="10%" class="lbl">
                            <asp:CheckBox ID="chkAlcohol1_2Year" Text="1-2 x/year" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            Quantity
                        </td>
                        <td width="10%" class="lbl">
                            <asp:TextBox ID="txtQuantityAlcohol" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkRecreationaldrugs" Text="Recreational drugs" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            What?
                        </td>
                        <td width="60%">
                            <asp:TextBox ID="txtRecreationaldrugs" Width="90%" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtVitalSignsPulse" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%" style="height: 14px">
                            Height
                        </td>
                        <td class="lbl" width="10%" style="height: 14px">
                            <asp:TextBox ID="txtVitalSignsHeight" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="10%" style="height: 14px">
                            Blood Pressure
                        </td>
                        <td class="lbl" width="10%" style="height: 14px">
                            <asp:TextBox ID="txtVitalSignsBloodPressure" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%" style="height: 14px">
                            Weight
                        </td>
                        <td class="lbl" style="height: 14px; width: 10%;">
                            <asp:TextBox ID="txtVitalSignsWeight" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%" colspan="9">
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click"  />
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                            
                        </td>
                    </tr>
                </table>
                
                
                 <table width="100%">
                <tr>
               
                <td width="25%" class="lbl">
                <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                </td>
              
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
