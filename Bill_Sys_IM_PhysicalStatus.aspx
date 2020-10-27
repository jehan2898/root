<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_PhysicalStatus.aspx.cs" Inherits="Bill_Sys_IM_PhysicalStatus"
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
                        <td width="10%" class="lbl">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOA" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%">
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
                        <td colspan="2" width="100%" class="lbl">
                            <b>PHYSICAL EXAMINATION</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="100%" class="lbl">
                            <b>GENERAL APPEARANCE</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chkAppearanceWellDeveloped" Text="WELL DEVELOPED" runat="server" />
                        </td>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chkAppearanceWellNourished" Text="WELL NOURISHED" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chkAppearanceMildDistress" Text="MILD DISTRESS" runat="server" />
                        </td>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chkAppearanceModerateDistress" Text="MODERATE DISTRESS" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chkAppearanceSevereDistress" Text="SEVERE DISTRESS SECONDARY TO PAIN"
                                runat="server" />
                        </td>
                        <td width="50%" class="lbl">
                            <asp:CheckBox ID="chkAppearanceBriefAssessmentOfMentalStatus" Text="BRIEF ASSESSMENT OF MENTAL STATUS (ORIENTED TO TIME, PLACE AND PERSON)"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>HEAD</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            Normocephalic, Atraumatic
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            Eyes : Pupils were equal and reacted to light and accommodation Extraocular muscles
                            were intact.
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            Ears : No blood noted in the external auditory canal.
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>CHEST</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkNoDeformities" Text="No Deformities" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkPainUponPalpation" runat="server" />
                        </td>
                        <td class="lbl" width="20%">
                            <asp:RadioButtonList ID="rdlPainUponPalpation" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Positive/</asp:ListItem>
                                <asp:ListItem Value="1">Negative pain upon palpation</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlPainUponPalpation" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>HEART</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkHeartRhythmRate" Text="The heart was in regular rhythm and rate; normal S1, S2."
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkHeartOther" Text="Other :" runat="server" />
                        </td>
                        <td class="lbl" width="80%">
                            <asp:TextBox ID="txtHeartOther" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--<table width="100%">
        <tr>
            <td width="100%" class="TDPart" style="height: 75px">
                <table width="100%">
                    <tr>
                        <td width="25%" class="lbl">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtPatientName4" runat="server" Width="90%"></asp:TextBox>
                        </td>
                        <td width="10%" class="lbl">
                            Date of accident
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="DOA5" runat="server" Width="60%"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="DOA5"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="lbl" width="20%">
                            Date of Examination</td>
                        <td colspan="1" width="20%">
                            <asp:TextBox ID="DOE5" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="DOE5"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>--%>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>LUNGS</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkLungsClearToAuscultaion" Text="Clear to auscultation bilaterally."
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkLungsOther" Text="Other :" runat="server" />
                        </td>
                        <td class="lbl" width="80%">
                            <asp:TextBox ID="txtLungsOther" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>ABDOMEN</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkAbdomenSoftNormative" Text="Soft, normoactive bowel sounds."
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkAbdomenTenderness" runat="server" />
                        </td>
                        <td class="lbl" width="20%">
                            <asp:RadioButtonList ID="rdlTenderness" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Positive/</asp:ListItem>
                                <asp:ListItem Value="1">Negative Tenderness.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlTenderness" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>BACK No</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkNoKyphoscoliosis" Text="No kyphoscoliosis." runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkBackLumbarLordosis" runat="server" />
                        </td>
                        <td class="lbl" width="20%">
                            <asp:RadioButtonList ID="rdlBackLumbarLordosis" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Normal/</asp:ListItem>
                                <asp:ListItem Value="1">Increased lumbar lordosis.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlBackLumbarLordosis" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="100%" class="lbl">
                            <b>EXTREMITIES</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="lbl">
                            <asp:CheckBox ID="chkExtremitiesNoEdema" Text="No edema." runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="15%">
                            <asp:CheckBox ID="chkExtremitiesEdemaAt" Text="Edema at:" runat="server" />
                        </td>
                        <td class="lbl" width="80%">
                            <asp:TextBox ID="txtExtremitiesEdemaAt" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="15%">
                            Pulses 2+ throughout
                        </td>
                        <td class="lbl" width="80%">
                            <asp:TextBox ID="txtPulsesThroughout" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table width="80%">
                    <tr>
                        <td class="lbl" width="17%" style="height: 20px">
                            <b>SHOULDER </b>
                        </td>
                        <td class="lbl" width="10%" style="height: 20px">
                            NORMAL ROM
                        </td>
                        <td class="lbl" width="25%" style="height: 20px">
                            PATIENT’S ROM / STRENGTH
                        </td>
                        <td class="lbl" width="12%" style="height: 20px">
                            <asp:CheckBox ID="chkShoulderLeft" Text="- LEFT" runat="server" />
                        </td>
                        <td class="lbl" width="13%" style="height: 20px">
                            <asp:CheckBox ID="chkShoulderRight" Text="- RIGHT" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td class="lbl" width="17%">
                            FLEXION
                        </td>
                        <td class="lbl" width="10%">
                            150
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtShoulderFlexionPatientRom150" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderFlexionDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderFlexionSharp" Text="s" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderFlexionSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderFlexionInflame" Text="I" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            EXTENSION
                        </td>
                        <td class="lbl" width="10%">
                            150
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtShoulderExtensionPatientRom150" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderExtensionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderExtensionDull" Text="D" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderExtensionSharp" Text="H" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderExtensionSpasm" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderExtensionInflame" Text="N" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            ABDUCTION
                        </td>
                        <td class="lbl" width="10%">
                            150
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtShoulderAbductionPatientRom150" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderAbductionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderAbductionDull" Text="U" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderAbductionSharp" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderAbductionSpasm" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderAbductionInflame" Text="F" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            ADDUCTION
                        </td>
                        <td class="lbl" width="10%">
                            30
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtShoulderaAdductionPatientRom30" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderaAdductionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderaAdductionDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderaAdductionSharp" Text="R" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderaAdductionSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderaAdductionInflame" Text="L" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            INTERNAL ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtShoulderInternalRotationPatientRom40" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderInternalRotationNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderInternalRotationDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderInternalRotationSharp" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderInternalRotationSpasm" Text="M" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkShoulderInternalRotationInflame" Text="A" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="17%">
                            EXTERNAL ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            90
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtShoulderExternalRotationPatientRom190" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="txtShoulderExternalRotationNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="txtShoulderExternalRotationDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="txtShoulderExternalRotationSharp" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="txtShoulderExternalRotationSpasm" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="txtShoulderExternalRotationInflame" Text="M" runat="server" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlPainInJointLeft" runat="server" Visible="false"  Width="2px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtrdlPAinAcrossShoulders" runat="server" Visible="false"  Width="2px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtrdlLimitationOnMovementLeft" runat="server" Visible="false"  Width="2px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width="20%" class="lbl">
                            PAIN IN JOINT
                        </td>
                        <td width="15%" class="lbl">
                            <asp:RadioButtonList ID="rdlPainInJointLeft" runat="server" Width="90%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">left</asp:ListItem>
                                <asp:ListItem Value="1">right</asp:ListItem>
                                <asp:ListItem Value="2">both</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td width="15%" class="lbl">
                            SHOULDERS
                        </td>
                        <td width="5%" class="lbl">
                            <asp:CheckBox ID="chkShouldersSymmetrical" runat="server" />
                        </td>
                        <td width="15%" class="lbl">
                            - SYMMETRICAL
                        </td>
                        <td width="5%" class="lbl">
                            <asp:CheckBox ID="chkShouldersAsymmetrical" Text="M" runat="server" />
                        </td>
                        <td width="15%" class="lbl">
                            - ASYMMETRICAL
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" class="lbl">
                            PAIN ACROSS SHOULDER
                        </td>
                        <td width="15%" class="lbl">
                            <asp:RadioButtonList ID="rdlPAinAcrossShoulders" runat="server" Width="90%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">left</asp:ListItem>
                                <asp:ListItem Value="1">right</asp:ListItem>
                                <asp:ListItem Value="2">both</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td width="15%" class="lbl">
                            <asp:CheckBox ID="chkPAinAcrossShouldersMild" runat="server" />&nbsp;- MILD
                        </td>
                        <td width="5%" class="lbl">
                            <asp:CheckBox ID="chkPAinAcrossShouldersModerate" runat="server" />
                        </td>
                        <td width="15%" class="lbl">
                            - MODERATE
                        </td>
                        <td width="5%" class="lbl">
                            <asp:CheckBox ID="chkPAinAcrossShouldersSevere" Text="M" runat="server" />
                        </td>
                        <td width="15%" class="lbl">
                            - SEVERE PAIN
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" class="lbl">
                            LIMITATION OF MOVEMEN
                        </td>
                        <td width="15%" class="lbl">
                            <asp:RadioButtonList ID="rdlLimitationOnMovementLeft" runat="server" Width="90%"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">left</asp:ListItem>
                                <asp:ListItem Value="1">right</asp:ListItem>
                                <asp:ListItem Value="2">both</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td width="15%" class="lbl">
                            ON PALPATION
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
                <table width="100%">
                    <tr>
                        <td class="lbl" style="height: 8px">
                            <asp:RadioButtonList ID="rdlCrepitus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 8px">
                            - CREPITIS PRESENT</td>
                        <td class="lbl" style="height: 8px">
                            <asp:RadioButtonList ID="rdlDropArmTest" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 8px">
                            - DROP ARM TEST</td>
                        <td class="lbl" style="height: 8px">
                            <asp:RadioButtonList ID="rdlApprehensionSign" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="height: 8px">
                            - APPREHENSION SIGN</td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlPainfulImpingement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">(+)</asp:ListItem>
                                <asp:ListItem Value="1">(-)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            - PAINFUL ARC / IMPINGEMENT SIGN</td>
                    </tr>
                </table>
                
                   <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlDropArmTest" runat="server" Visible="false" Width="2px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtrdlApprehensionSign" runat="server" Visible="false"  Width="2px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtrdlPainfulImpingement" runat="server" Visible="false"  Width="2px"></asp:TextBox>
                        </td>
                        
                        <td>
                            <asp:TextBox ID="txtrdlCrepitus" runat="server" Visible="false"  Width="2px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td class="lbl" style="width: 20%">
                            <b>CERVICAL SPINE</b>
                        </td>
                        <td class="lbl" width="12%" align="left">
                            NORMAL ROM
                        </td>
                        <td class="lbl" width="25%">
                            PATIENT’S ROM / STRENGTH
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%; height: 6px;">
                            FLEXION
                        </td>
                        <td class="lbl" width="10%" style="height: 6px">
                            60
                        </td>
                        <td class="lbl" width="10%" style="height: 6px">
                            <asp:TextBox ID="txtCeravicalSpineFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="10%" style="height: 6px">
                            <asp:CheckBox ID="chkCeravicalSpineFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="10%" style="height: 6px">
                            <asp:CheckBox ID="chkCeravicalSpineFlexionDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="10%" style="height: 6px">
                            <asp:CheckBox ID="chkCeravicalSpineFlexionSharp" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="10%" style="height: 6px">
                            <asp:CheckBox ID="chkCeravicalSpineFlexioSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="10%" style="height: 6px">
                            <asp:CheckBox ID="chkCeravicalSpineFlexioSpasmInflame" Text="I" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%">
                            EXTENSION
                        </td>
                        <td class="lbl" width="10%">
                            50
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtCeravicalSpineExtensionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineExtensionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineExtensionDull" Text="D" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineExtensionSharp" Text="H" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineExtensionSpasm" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineExtensionInflame" Text="N" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%">
                            LEFT ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            80
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtCeravicalSpineLeftRotaionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineLeftRotaionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineLeftRotaionDull" Text="U" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineLeftRotaionSharp" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="ChecchkCeravicalSpineLeftRotaionSpasm" Text="A" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineLeftRotaionInflame" Text="F" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%">
                            RIGHT ROTATION
                        </td>
                        <td class="lbl" width="10%">
                            80
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtCeravicalSpinerightRoatationPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpinerightRoatationNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpinerightRoatationDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpinerightRoatationSharp" Text="R" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpinerightRoatationSpasm" Text="S" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpinerightRoatationInflame" Text="L" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%">
                            LT LATERAL FLEXION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtCeravicalSpineLTLateralFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkSpineLTLateralFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkSpineLTLateralFlexionDull" Text="L" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkSpineLTLateralFlexionSharp" Text="P" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkSpineLTLateralFlexionSpasm" Text="M" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkSpineLTLateralFlexionInflame" Text="A" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 20%">
                            RT LATERAL FLEXION
                        </td>
                        <td class="lbl" width="10%">
                            40
                        </td>
                        <td class="lbl" width="10%">
                            <asp:TextBox ID="txtCeravicalSpineRTLateralFlexionPatientRom" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineRTLateralFlexionNormal" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineRTLateralFlexionDull" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineRTLateralFlexionSharp" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineRTLateralFlexionSpasm" Text="" runat="server" />
                        </td>
                        <td class="lbl" width="5%">
                            <asp:CheckBox ID="chkCeravicalSpineRTLateralFlexionInflame" Text="M" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%" colspan="8">
                            <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
