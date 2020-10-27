<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_IM_PlanOfCare.aspx.cs" Inherits="Bill_Sys_IM_PlanOfCare" Title="Untitled Page" %>

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
                        <td style="width: 36%">
                            <asp:TextBox ID="txtPatientName" runat="server" Width="90%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 13%">
                            Date of accident
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtDOA" runat="server" Width="70%" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="20%" >
                            Date of Examination</td>
                        <td style="width: 36%">
                            <asp:TextBox ID="txtDOE" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="lbl" style="width: 13%">
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
                        <td class="lbl" width="100%">
                            <b>PLAN OF CARE</b></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="100%" style="height: 15px">
                            What is your proposed treatment?
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="100%" colspan="2">
                            <asp:TextBox ID="txtProposed_Treatment" runat="server" Text="" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" style="width: 33%; height: 22px">
                            <b>Medication(s):</b></td>
                        <td class="lbl" style="width: 200px; height: 22px">
                            (a) list medications prescribed:</td>
                        <td class="lbl" width="40%" style="height: 22px">
                            <asp:TextBox ID="txtMedication_List" Text="" runat="server" Width="86%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="33%">
                        </td>
                        <td class="lbl" width="33%">
                            (b) list over-the-counter medications advised:
                        </td>
                        <td class="lbl" width="40%">
                            <asp:TextBox ID="txtOver_The_Counter_Medication" runat="server" Text="" Width="86%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 33%">
                            Medication restrictions:
                        </td>
                        <td class="lbl" colspan="2" width="90%">
                            <asp:RadioButtonList ID="rdlMedication_Restriction" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">None</asp:ListItem>
                                <asp:ListItem Value="1">May affect patient’s ability to return to work, make patient drowsy, or other issue. </asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 33%">
                            Explain:</td>
                        <td class="lbl" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="3">
                            <asp:TextBox ID="txtMedication_Restriction" runat="server" Text="" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
                <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlMedication_Restriction" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="40%">
                            Does the patient need diagnostic tests or referrals?
                        </td>
                        <td class="lbl">
                            <asp:RadioButtonList ID="rdlPatient_Need_Diagnostic" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                <asp:ListItem Value="1">No If Yes, check all that apply: </asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
                
                 <table width="100%" visible="false">
                <tr>
                <td>
                <asp:TextBox  ID="txtrdlPatient_Need_Diagnostic" runat="server" Visible="false"></asp:TextBox>
                </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="25%" class="lbl">
                            <b>Tests</b></td>
                        <td width="25%" class="lbl">
                        </td>
                        <td width="25%" class="lbl">
                            <b>Referrals</b></td>
                        <td width="25%" class="lbl">
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkTest_CT_Scan" runat="server" Text="CT Scan" /></td>
                        <td width="25%" class="lbl">
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkReferrals_Chiropractor" runat="server" Text="Chiropractor" />
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="chkReferrals_Internist_Physician" runat="server" Text="Internist/Family Physician" Visible="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%" style="height: 21px">
                            <asp:CheckBox ID="chkTest_Emg_Ncv" runat="server" Text="EMG/NCS" /></td>
                        <td class="lbl" width="25%" style="height: 21px">
                        </td>
                        <td class="lbl" width="25%" style="height: 21px">
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Internist/Family Physician" visible="false"/></td>
                        <td class="lbl" width="25%" style="height: 21px">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkTest_MRI" runat="server" Text="MRI (Specify)"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtTest_MRI" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkReferrals_Occupational_Therapist" runat="server" Text="Occupational Therapist" /></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_REFERRALS_OROTHOPEDIC_EVALUATION" runat="server" Text="Occupational Therapist" Visible="false"/><td class="lbl" width="25%">
                            </td>
                        <td class="lbl" width="25%">
                        </td>
                    </tr>
                    
                    
                      <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_REFERRALS_NERVE_BLOCKS" runat="server" Text="X-Ray(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_REFERRALS_NERVE_BLOCKS" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_VS_NCT" runat="server" Text="Specialist in " /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_VS_NCT" runat="server" Text="" Width="86%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkTest_Labs" runat="server" Text="Labs(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtTest_Labs" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkReferrals_Physical_Therapist" runat="server" Text="Physical Therapist" /></td>
                        <td class="lbl" width="25%">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkTest_X_Ray" runat="server" Text="V_sNCT(Specify)" CssClass="lbl" Width="120px" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtTest_X_Ray" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkReferrals_Specialist_In" runat="server" Text="Specialist in " /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtReferrals_Specialist_In" runat="server" Text="" Width="86%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkTest_Other" runat="server" Text="Other(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtTest_Other" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="chkReferrals_Other" runat="server" Text="Other(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="txtReferrals_Other" runat="server" Text="" Width="86%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="4">
                            Assistive devices prescribed for this patient:</td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_EMS_Unit" runat="server" Text="E.M.S UNIT" /></td>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_Wrist_Support" runat="server" Text="LEFT/RIGHT WRIST SUPPORT" /></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chkAssistive_Device_Cervical_Pillow" runat="server" Text="CERVICAL PILLOW" /></td>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_Elbow_Support" runat="server" Text="LEFT/RIGHT ELBOW SUPPORT" /></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_Lumbosacral_Back_Support" runat="server" Text="LUMBOSACRAL BACK SUPPORT" /></td>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_Ankle_Support" runat="server" Text="LEFT/RIGHT ANKLE SUPPORT" /></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_Lumbar_Cushion" runat="server" Text="LUMBAR CUSHION" /></td>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_Knee_Support" runat="server" Text="LEFT/RIGHT  KNEE  SUPPORT" /></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chkAssistive_Device_Massager" runat="server" Text="MASSAGER" /></td>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chk_Assistive_Device_Cane" runat="server" Text="CANE" /></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2">
                            <asp:CheckBox ID="chkAssistive_Device_Other" runat="server" Text="Other" /></td>
                        <td class="lbl" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="4">
                            <asp:TextBox ID="txtAssistive_Device_Other" runat="server" Text="" Width="96%"></asp:TextBox></td>
                    </tr>
                </table>
                <%--<table width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 24%" class="lbl">
                                Patient's Name
                            </td>
                            <td width="40%">
                                <asp:TextBox ID="txtPatientsName10" runat="server" Width="90%"></asp:TextBox>
                            </td>
                            <td class="lbl" width="20%">
                                Date of accident
                            </td>
                            <td width="20%">
                                <asp:TextBox ID="txtDAO10" runat="server" Width="60%"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender21" runat="server" PopupButtonID="imgbtnFromDate"
                                    TargetControlID="txtDAO10">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 24%" class="lbl" colspan="1">
                                Date of Examination</td>
                            <td width="20%" colspan="1">
                                <asp:TextBox ID="txtDoE1" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender22" runat="server" PopupButtonID="imgbtnFromDate"
                                    TargetControlID="txtDoE1">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </tbody>
                </table>--%>
                <table width="100%">
                    <tr>
                        <td colspan="9" class="lbl" style="height: 30px">
                            WHEN IS PATIENT’S NEXT FOLLOW-UP VISIT?</td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Next_Visit_Within_A_Weelk" runat="server" Text="WITHIN A WEEK" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Next_Visit_1_TO_2_Wks" runat="server" Text="- 1-2 WKS" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Next_Visit_3_TO_4_Wks" runat="server" Text="- 3-4 WKS" /></td>
                        <td class="lbl" >
                            <asp:CheckBox ID="chkPatient_Next_Visit_5_TO_6_Wks" runat="server" Text="- 5-6 WKS" />
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Next_Visit_7_TO_8_Wks" runat="server" Text="- 7-8 WKS">
                            </asp:CheckBox>
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="chkPatient_Next_Visit_Needed_Month" runat="server" Text="Visit Needed Month"></asp:CheckBox>
                        </td>
                        <td class="lbl">
                            <asp:TextBox ID="txtPatient_Next_Visit_Needed_Month" Text="" runat="server"> </asp:TextBox>
                            MONTHS WKS</td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2" style="height: 22px">
                            <asp:CheckBox ID="chkPatient_As_Needed" runat="server" Text="AS NEEDED" /></td>
                        <td colspan="5" style="height: 22px">
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                         <asp:TextBox ID="txtEventID" runat="server" Visible="false" ></asp:TextBox>
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
