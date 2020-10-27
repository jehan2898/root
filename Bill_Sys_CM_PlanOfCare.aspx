<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CM_PlanOfCare.aspx.cs" Inherits="Bill_Sys_CM_PlanOfCare" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td width="100%" class="TDPart">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHeading" runat="server" Text="INITIAL EXAMINATION" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="left" valign="middle">
                            <asp:Label ID="lbl_PATIENT_NAME" runat="server" Text="Patient's Name"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:TextBox ID="TXT_PATIENT_NAME" runat="server" Width="472px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        <td align="left" valign="middle">
                            <asp:Label ID="lbl_DOA" runat="server" Font-Bold="False" Text="Date Of Accident"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:TextBox ID="TXT_DOA" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle">
                            <asp:Label ID="LBL_DOE" runat="server" Font-Bold="False" Text="Date Of Examination"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:TextBox ID="TXT_DOE" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
                        <td align="left" valign="middle">
                            <asp:Label ID="LBL_DOB" runat="server" Font-Bold="False" Text="Date Of Birth"></asp:Label>
                        </td>
                        <td align="left" valign="middle">
                            <asp:TextBox ID="TXT_DOB" runat="server" Width="85px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox></td>
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
                            <b><u>PLAN OF CARE</u></b></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="100%">
                            What is your proposed treatment?
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="100%" colspan="2">
                            <asp:TextBox ID="TXT_PROPOSED_TREATMENT" runat="server" Text="" Width="95%"></asp:TextBox>
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
                            <asp:TextBox ID="TXT_MEDICATION_LIST" Text="" runat="server" Width="86%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="33%">
                        </td>
                        <td class="lbl" width="33%">
                            (b) list over-the-counter medications advised:
                        </td>
                        <td class="lbl" width="40%">
                            <asp:TextBox ID="TXT_OVER_THE_COUNTER_MEDICATION" runat="server" Text="" Width="86%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="width: 33%">
                            Medication restrictions:
                        </td>
                        <td class="lbl" colspan="2" width="90%">
                            <asp:RadioButtonList ID="RDO_MEDICATION_RESTRICTION" runat="server" RepeatDirection="Horizontal">
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
                            <asp:TextBox ID="TXT_MEDICATION_RESTRICTION" runat="server" Text="" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="lbl" width="40%" style="height: 39px">
                            Does the patient need diagnostic tests or referrals?
                        </td>
                        <td class="lbl" style="height: 39px">
                            <asp:RadioButtonList ID="RDO_PATIENT_NEED_DIAGNOSTIC" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                <asp:ListItem Value="1">No If Yes, check all that apply: </asp:ListItem>
                            </asp:RadioButtonList></td>
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
                            <asp:CheckBox ID="CHK_TEST_CT_SCAN" runat="server" Text="CT Scan" /></td>
                        <td width="25%" class="lbl">
                        </td>
                        <td width="25%" class="lbl">
                            <asp:CheckBox ID="CHK_REFERRALS_CHIROPRACTOR" runat="server" Text="Chiropractor" />
                        </td>
                        <td width="25%" class="lbl">
                            <%--<asp:CheckBox ID="CHK_REFERRALS_INTERNIST_FAMILY_PHYSICIAN" runat="server" Text="Internist/Family Physician" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%" style="height: 21px">
                            <asp:CheckBox ID="CHK_TEST_EMG_NCV" runat="server" Text="EMG/NCS" /></td>
                        <td class="lbl" width="25%" style="height: 21px">
                        </td>
                        <td class="lbl" width="25%" style="height: 21px">
                            <asp:CheckBox ID="CHK_REFERRALS_INTERNIST_FAMILY_PHYSICIAN" runat="server" Text="Internist/Family Physician" /></td>
                        <td class="lbl" width="25%" style="height: 21px">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_TEST_MRI" runat="server" Text="MRI (Specify)"></asp:CheckBox>
                        </td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_TEST_MRI" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_REFERRALS_OCCUPATIONAL_THERAPIST" runat="server" Text="Occupational Therapist" /></td>
                        <td class="lbl" width="25%">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_TEST_LABS" runat="server" Text="Labs(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_TEST_LABS" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_REFERRALS_PHYSICAL_THERAPIST" runat="server" Text="Physical Therapist" /></td>
                        <td class="lbl" width="25%">
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_TEST_X_RAY" runat="server" Text="X-Ray(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_TEST_X_RAY" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_REFERRALS_SPECIALIST_IN" runat="server" Text="Specialist in " /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_REFERRALS_SPECIALIST_IN" runat="server" Text="" Width="86%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_TEST_OTHER" runat="server" Text="Other(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_TEST_OTHER" runat="server" Text="" Width="86%"></asp:TextBox></td>
                        <td class="lbl" width="25%">
                            <asp:CheckBox ID="CHK_REFERRALS_OTHER" runat="server" Text="Other(Specify)" /></td>
                        <td class="lbl" width="25%">
                            <asp:TextBox ID="TXT_REFERRALS_OTHER" runat="server" Text="" Width="86%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="4">
                            Assistive devices prescribed for this patient:</td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_ASSISTIVE_DEVICE_CANE" runat="server" Text="Cane" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_CRUTCHES" runat="server" Text="Crutches" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_ORTHOTICS" runat="server" Text="Orthotics" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_WALKER" runat="server" Text="Walker" /></td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="1">
                            <asp:CheckBox ID="CHK_WHEEL_CHAIR" runat="server" Text="Wheelchair" /></td>
                        <td class="lbl" colspan="3">
                            <asp:CheckBox ID="CHK_ASSISTIVE_DEVICE_OTHER" runat="server" Text="Other" /></td>
                    </tr>
                    <%-- <tr>
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
                    </tr>--%>
                    <tr>
                        <td class="lbl" colspan="4">
                            <asp:TextBox ID="TXT_ASSISTIVE_DEVICE_OTHER" runat="server" Text="" Width="96%"></asp:TextBox></td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="9" class="lbl" style="height: 30px">
                            WHEN IS PATIENT’S NEXT FOLLOW-UP VISIT?</td>
                    </tr>
                    <tr>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_WITHIN_A_WEEK" runat="server" Text="WITHIN A WEEK" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_1_TO_2_WKS" runat="server" Text="- 1-2 WKS" /></td>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_3_TO_4_WKS" runat="server" Text="- 3-4 WKS" /></td>
                        <td class="lbl" >
                            <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_5_TO_6_WEEKS" runat="server" Text="- 5-6 WKS" />
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_7_TO_8_WKS" runat="server" Text="- 7-8 WKS">
                            </asp:CheckBox>
                        </td>
                        <td class="lbl">
                            <asp:CheckBox ID="CHK_PATIENT_NEXT_VISIT_NEEDED_MONTH" runat="server"></asp:CheckBox>
                        </td>
                        <td class="lbl">
                            <asp:TextBox ID="TXT_PATIENT_NEXT_VISIT_NEEDED_MONTH" Text="" runat="server"> </asp:TextBox>
                            MONTHS WKS</td>
                    </tr>
                    <tr>
                        <td class="lbl" colspan="2" style="height: 22px">
                            <asp:CheckBox ID="CHK_PATIENT_AS_NEEDED" runat="server" Text="AS NEEDED" /></td>
                        <td colspan="5" style="height: 22px">
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="Buttons" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save & Next" CssClass="Buttons" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                 <table width="100%" visible="false">
                    <tr visible="false">
                    <td visible="false" style="height: 22px">
                    <asp:TextBox ID="txtRDO_MEDICATION_RESTRICTION" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td visible="false" style="height: 22px">
                         <asp:TextBox ID="txtRDO_PATIENT_NEED_DIAGNOSTIC" runat="server" Visible="false"></asp:TextBox>
                    </td>
                   
                    <td>
                    <asp:TextBox ID="txtEventID" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
</asp:Content>
