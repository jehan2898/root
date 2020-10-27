<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_CurrentComplain.aspx.cs" Inherits="Bill_Sys_IM_CurrentComplain"
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
                        <td style="width: 31%">
                            <asp:TextBox ID="txtPatientName" runat="server" Width="90%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 12%">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOA" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%">
                            Date of Examination</td>
                        <td style="width: 31%">
                            <asp:TextBox ID="txtDOE" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 12%">
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
                        <td colspan="4" width="100%" align="left" class="lbl">
                            <b>CURRENT COMPLAINTS</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" width="100%" align="left" class="lbl">
                            Patient’s subjective complaints: Check all that apply and identify specific affected
                            body part(s).
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkComlaintsNumbness" Text="Numbness/Tingling" runat="server" /></td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtComlaintsNumbness" runat="server" Width="95%"></asp:TextBox></td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkComlaintsSwelling" Text="Swelling" runat="server" /></td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtComlaintsSwelling" runat="server" Width="95%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkComlaintsPain" Text="Pain" runat="server" /></td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtComlaintsPain" runat="server" Width="95%"></asp:TextBox></td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkComlaintsWeakness" Text="Weakness" runat="server" /></td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtComlaintsWeakness" runat="server" Width="95%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkComlaintsStiffness" Text="Stiffness" runat="server" /></td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtComlaintsStiffness" runat="server" Width="95%"></asp:TextBox></td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkComlaintsOthers" Text="Others(specify)" runat="server" /></td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtComlaintsOthers" runat="server" Width="95%"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <%--<table width="100%">
                    <tr>
                        <td width="25%" class="lbl">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtPatientName1" runat="server" Width="90%"></asp:TextBox>
                        </td>
                        <td width="10%" class="lbl">
                            Date of accident
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtDAO1" runat="server" Width="60%"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDAO1"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="lbl" width="20%">
                            Date of Examination</td>
                        <td colspan="1" width="20%">
                            <asp:TextBox ID="txtDoE1" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDoE1"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                </table>--%>
                <table width="100%">
                    <tr>
                        <td width="25%" style="height: 20px" class="lbl">
                            <asp:CheckBox ID="chkHeadaches" Text="Headaches" runat="server" />
                        </td>
                        <td width="25%" style="height: 20px" class="lbl">
                            <asp:CheckBox ID="chkKneePain" Text="RT / LT Knee Paine" runat="server" />
                        </td>
                        <td width="25%" style="height: 20px" class="lbl">
                            <asp:CheckBox ID="chkDizziness" Text="Dizziness" runat="server" />
                        </td>
                        <td width="25%" style="height: 20px" class="lbl">
                            <asp:CheckBox ID="chkShoulderPain" Text="RT / LT Shoulder Pain" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkJawPain" Text="Jaw Pain" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="ChkLowerBackPainStiffness" Text="Lower Back Pain & Stiffness" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkNeckPainStiffness" Text="Neck Pain & Stiffness" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkUpperBackPainStiffness" Text="Upper Back Pain & Stiffness" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkChestPain" Text="Chest Pain" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkUnabelToLiftObjects" Text="Unabel To Lift Heavy Objects" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkDiffcultyInBreathing" Text="Diffculty In Breathing" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkShootingPainLeg" Text="Shooting Pain Down The RT/LF Leg" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkRestrictionNeckMotion" Text="Restriction Of Neck Motion" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkDifficultToWalkAfterSitting" Text="Difficult In Rising TO Walk After Sitting"
                                runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkNumbnessTinglingToes" Text="Numbness/Tingling In Fingers/Toes"
                                runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkDifficultInProlongedRiding" Text="Difficult In Prolonged Riding In An Automobile"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkAnxietyStress" Text="Anxiety/Stress" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkInsomnia" Text="Insomnia" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkVisualProblems" Text="Visual Problems" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkMemoryProblems" Text="Memory Problems" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl" style="height: 29px">
                            <asp:CheckBox ID="chkFatigue" Text="Fatigue" runat="server" />
                        </td>
                        <td width="25%" class="lbl" style="height: 29px">
                            <asp:CheckBox ID="chkConcentrationProblems" Text="Concentration Problems" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" class="lbl">
                    <tr>
                        <td class="lbl" width="2%">
                            Patient
                        </td>
                        <td width="10%" class="lbl">
                            <asp:RadioButtonList ID="rdlatientStatesDenied" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">states</asp:ListItem>
                                <asp:ListItem Value="1">denied</asp:ListItem>
                                
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" style="width: 23%">
                            &nbsp;loss of consciousness but was
                        </td>
                        <td width="15%" class="lbl">
                            <asp:RadioButtonList ID="rdlDazedDizzyNervous" runat="server" RepeatDirection="Horizontal" Width="165px">
                                <asp:ListItem Value="0">DAZED</asp:ListItem>
                                <asp:ListItem Value="1">DIZZY </asp:ListItem>
                                <asp:ListItem Value="2">NERVOUS</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                        &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlatientStatesDenied" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlDazedDizzyNervous" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" style="width: 23%">
                            After the accident the patient
                        </td>
                        <td  width="25%">
                            <asp:RadioButtonList ID="rdlPatientAfterAccident" runat="server" RepeatDirection="Horizontal" Width="380px" RepeatLayout="Flow">
                                <asp:ListItem Value="0">went home to rest     </asp:ListItem>
                                <asp:ListItem Value="1">was taken by ambulance</asp:ListItem>
                                <asp:ListItem Value="2">took self  to</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl" width="25%">
                            the Emergency Room at
                        </td>
                        <td>
                        &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlPatientAfterAccident" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                                 <td class="lbl" style="width: 40%">
                            <asp:TextBox ID="txtHospital" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 40%">
                            hospital Patient was hospitalized.</td>
                    </tr>
                    <tr>
                        <td colspan="100%" align="left" class="lbl">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="100%" align="left" class="lbl">
                            Patient was evaluated in the emergency room where multiple X-rays were taken of
                        </td>
                    </tr>
                    <tr>
                        <td colspan="100%" align="left" class="lbl">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <asp:TextBox ID="txtMultiole_X_Ray_Taken" runat="server" TextMode="MultiLine" Height="40%"
                            Width="100%"></asp:TextBox></tr>
                    <tr>
                        <td class="lbl" style="width: 10%">
                            Patient was medicated and was released on
                        </td>
                        <td class="lbl" style="width: 40%">
                            <asp:TextBox ID="txtReleasedOn" Width="105%" runat="server" Height="19px"></asp:TextBox>
                        </td>
                        <td width="50%">
                        &nbsp;
                        </td>
                        <td width="50%">
                        &nbsp;
                        </td>
                        
                    </tr>
                    
                </table>
                <table width="100%">
                    <tr>
                        <td width="7%" class="lbl">
                            Patient treated by
                        </td>
                        <td width="13%" class="lbl">
                            <asp:RadioButtonList ID="rdlPatientTreatedHimOrHer" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">himself</asp:ListItem>
                                <asp:ListItem Value="1">herself</asp:ListItem>
                                <asp:ListItem Value="2">took self  to</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="25%" class="lbl">
                            with analgesic with mild effect an
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlPatientTreatedHimOrHer" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" style="width: 20%">
                            <asp:RadioButtonList ID="rdlDays" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">next day </asp:ListItem>
                                <asp:ListItem Value="1">within a few days </asp:ListItem>
                                <asp:ListItem Value="2">took self  to</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="40%" class="lbl">
                            to my office seeking medical attention.
                        </td>
                    </tr>
                </table>
                <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlDays" runat="server" Visible="false"></asp:TextBox>
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
                <table width="100%">
                    <tr>
                        <td width="15%" class="lbl">
                            Information was obtained from the patient</td>
                        <td width="30%" class="lbl">
                            <asp:RadioButtonList ID="rdlInformationMode" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">by his/her own description </asp:ListItem>
                                <asp:ListItem Value="1">through an interpreter.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                
                
                 <table width="100%" visible="false">
                    <tr>
                        <td style="height: 22px">
                            <asp:TextBox ID="txtrdlInformationMode" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td  class="lbl" width="20%">
                            <asp:RadioButtonList ID="rdlNumbnessTingling" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >Numbness /</asp:ListItem>
                                <asp:ListItem Value="1" >tingling in</asp:ListItem>
                                
                          </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                        <asp:RadioButtonList ID="RDO_NUMBNESS_TINGLING_LOWER_UPPER" runat="server" RepeatDirection="Horizontal" Width="188px">
                                <asp:ListItem Value="2" >upper /</asp:ListItem>
                                <asp:ListItem Value="3" >lower extremities.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                
                
                 <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlNumbnessTingling" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRDO_NUMBNESS_TINGLING_LOWER_UPPER" runat="server" Visible="false"></asp:TextBox>
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
                        <td width="13%" class="lbl">
                            Pain was described as
                        </td>
                        <td width="45%" class="lbl">
                            <asp:RadioButtonList ID="rdlPainDescription" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">constant </asp:ListItem>
                                <asp:ListItem Value="2">sharp/</asp:ListItem>
                                <asp:ListItem Value="2">intermittent extremities.</asp:ListItem>
                                <asp:ListItem Value="3">stabbing</asp:ListItem>
                                <asp:ListItem Value="4">burning </asp:ListItem>
                                <asp:ListItem Value="5">dull</asp:ListItem>
                                <asp:ListItem Value="6">shooting</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
                
                
                  <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlPainDescription" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="20%">
                            Pain was radiating from neck to
                        </td>
                        <td class="lbl" width="37%">
                            <asp:RadioButtonList ID="rdlPainRadiatingNeck" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">right</asp:ListItem>
                                <asp:ListItem Value="1">left</asp:ListItem>
                                <asp:ListItem Value="2">both shoulder and .</asp:ListItem>
                                <asp:ListItem Value="3">right</asp:ListItem>
                                <asp:ListItem Value="4">left</asp:ListItem>
                                <asp:ListItem Value="5">both</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="lbl">
                            scapular region / upper extremities.
                        </td>
                    </tr>
                </table>
                
                     <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlPainRadiatingNeck" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                
           
                    <tr>
                        <td width="11%" class="lbl">
                            Pain was radiating from lower back to
                        </td>
                        <td width="7%" class="lbl">
                            <asp:RadioButtonList ID="rdlPainRadingLowerBack" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">right</asp:ListItem>
                                <asp:ListItem Value="1">left</asp:ListItem>
                                <asp:ListItem Value="2">both </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="30%" class="lbl">
                            lower extremities / buttocks.
                        </td>
                    </tr>
                </table>
                
                
                    <table width="100%" visible="false">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtrdlPainRadingLowerBack" runat="server" Visible="false"></asp:TextBox>
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
                        <td width="30%" class="lbl">
                            PAIN SCALE RATING
                        </td>
                        <td width="30%" class="lbl">
                            0 - No Pain 10 - Worst Pain
                        </td>
                        <td width="30%" class="lbl">
                            <asp:TextBox ID="txtPainScaleRating" runat="server" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%" class="lbl">
                            VISUAL ANALOG SCALE
                        </td>
                        <td width="30%" class="lbl">
                            Pain on scale of 0 to 10
                        </td>
                        <td width="30%" class="lbl">
                            <asp:TextBox ID="txtVisualAnalogScale" runat="server" Width="90%"></asp:TextBox>
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
                <table width="100%">
                    <tr>
                        <td width="30%" class="lbl">
                            PAIN IS EXACERBATED BY
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" style="width: 29%">
                            <asp:CheckBox ID="chkGoingUpDownStairs" Text="Going/UpDownStairs" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkCarryingHeavyObjects" Text="Carrying Heavy Objects" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkBendingDown" Text="Bending Down" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 29%">
                            <asp:CheckBox ID="chkSquatting" Text="Squatting" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkLyingDown" Text="Lying Down" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkPushing" Text="Pushing" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 29%">
                            <asp:CheckBox ID="chkProlongedSitting" Text="Prolonged Sitting" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkPulling" Text="Pulling" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkLifting" Text="Lifting" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 29%">
                            <asp:CheckBox ID="chkProlongedWalking" Text="Prolonged Walking" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkDeepBreathing" Text="Deep Breathing" runat="server" />
                        </td>
                        <td width="30%" class="lbl">
                            <asp:CheckBox ID="chkWeatherChange" Text="Weather Change" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 29%">
                            <asp:CheckBox ID="chkProlongedStandingFromSitting" Text="Standing Up From a Sitting Position "
                                runat="server" />
                        </td>
                        <td class="lbl" style="width: 29%">
                            <asp:CheckBox ID="chkProlongedStanding" Text="Prolonged standing" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <%--<table width="100%">
                    <tr>
                        <td width="25%" class="lbl">
                            Patient's Name
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtPatient2" runat="server" Width="90%"></asp:TextBox>
                        </td>
                        <td width="10%" class="lbl">
                            Date of accident
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="DOA2" runat="server" Width="60%"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="DOA2"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="lbl" width="20%">
                            Date of Examination</td>
                        <td colspan="1" width="20%">
                            <asp:TextBox ID="DOE2" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/cal.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="DOE2"
                                PopupButtonID="imgbtnFromDate" />
                        </td>
                    </tr>
                </table>--%>
                <table width="100%">
                    <tr>
                        <td colspan="4" width="100%" class="lbl">
                            Type/nature of injury: Check all that apply and identify specific affected body
                            part(s).</td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkAbrasion" Text="Abrasion" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtAbrasion" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkInfectiousDisease" Text="Infectious Disease" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtInfectiousDisease" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkAmputation" Text="Amputation" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtAmputation" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkInhalationExposure" Text="Inhalation Exposure" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtInhalationExposure" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkAvulsion" Text="Avulsion" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtAvulsion" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkLaceration" Text="Laceration" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtLaceration" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkBite" Text="Bite" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtBite" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkNeedleStick" Text="Needle Stick" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtNeedleStick" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkBurn" Text="Burn" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtBurn" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkPoisoningEffects" Text="Poisoning/Toxic Effects" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtPoisoningEffects" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkContusionHematoma" Text="Contusion/Hematoma" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtContusionHematoma" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkPsychological" Text="Psychological" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtPsychological" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkCrushInjury" Text="Crush Injury" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtCrushInjury" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkPunctureWound" Text="Puncture Wound " runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtPunctureWound" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkDermatitis" Text="Dermatitis" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtDermatitis" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkRepetitiveStrainInjury" Text="Repetitive Strain Injury" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtRepetitiveStrainInjury" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkDislocation" Text="Dislocation" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtDislocation" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkSpinalCordInjury" Text="Spinal Cord Injury" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtSpinalCordInjury" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl" style="height: 22px">
                            <asp:CheckBox ID="chkFracture" Text="Fracture" runat="server" />
                        </td>
                        <td width="25%" class="lbl" style="height: 22px">
                            <asp:TextBox ID="txtFracture" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl" style="height: 22px">
                            <asp:CheckBox ID="chkSprainStrain" Text="Sprain/Strain" runat="server" />
                        </td>
                        <td width="25%" class="lbl" style="height: 22px">
                            <asp:TextBox ID="txtSprainStrain" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkHernia" Text="Hernia" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtHernia" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkVisionLoss" Text="Vision Loss" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtVisionLoss" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkHearingLoss" Text="Hearing Loss" runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtHearingLoss" Width="90%" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkTornLigamentTendonMuscle" Text="TornLigament Tendon or Muscle"
                                runat="server" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:TextBox ID="txtTornLigamentTendonMuscle" Width="90%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="12%" class="lbl">
                            <asp:CheckBox ID="chkInjuryOther" Text="Other (specify)" runat="server" />
                        </td>
                        <td width="80%" class="lbl">
                            <asp:TextBox ID="txtInjuryOther" runat="server" Width="90%" Height="40%" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%" colspan="2">
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
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
